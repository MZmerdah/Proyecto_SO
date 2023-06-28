using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Threading;
using System.Windows.Threading;

namespace Client
{
    public partial class Principal : Form
    {
        int estado;
        Socket server;
        string username;
        PartidasGanadas pg;
        HoraFecha hf;
        GanadasDia gd;
        Ganadores10min gm;
        Jugadores jug;
        Invitar inv;
        Thread atender;
        User user;
        juego jg;
        Darse_de_baja db;
        CambiarContraseña cc;

        int maxSeleccion = 1;
        int numeroConectados = 0;
        string[] conectados = new string[100]; //vector para almacenar todos los usuarios conectados
        string usuarioOrig = string.Empty;
        delegate void DelegadoParaEscribir(string mensaje);
        delegate void PartidaNueva(juego jg, int num, string jugador1, string jugador2, string jugador3);
        delegate void MostrarConectados(string[] vector);
        delegate void DelegadoInvitado(string a, string usuarioActual);
        delegate void DelegadoInvitadoAceptaRechaza(string a, string acepta);
        delegate void DelegadoChat(string mensaje, string username);
        delegate void DelegadoEmpezarPartidaParticipantes(string anfitrion, string nombre2, string nombre3, int numpartida);
        delegate void DelegadoActualizarTanque(int partida, string player, int x, int y, int direccion);
        delegate void DelegadoDesconectar(bool usuarioDadoDeBaja);
        delegate void DelegadoContraCambiada(bool contracambiada);
        delegate void DelegadoCuboRoto(int numpartida, int numcubo, int numimg);
        delegate void DelegadoPowerUp(int numpartida, int numpowerup, int vidas, string oponente);
        delegate void DelegadoVida(int numpartida, string oponente, int vidas);
        delegate void DelegadoActualizarBala(int numpartida, string oponente, int x, int y, string direccion);
        string anfitrion;

        public Principal()
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
            label1.Text = "Usuario: " + username;
            invitar.Visible = false;
        }

        public void setServer(Socket a)   //Se utiliza para pasar traspasar los datos entre formularios
        {
            this.server = a;
        }
        public void setStatus (int a)
        {
            this.estado = a;
        }
        
        public int getStatus()
        {
            return estado;
        }

        public void setUser(string a, string usuarioOrig)  
        {
            this.username = a;
            this.usuarioOrig = usuarioOrig;
        }

        public void PonContador(string contador)
        {
            servicios_rec.Text = contador;
        }
        public void MuestraInvitacion(string a, string usuarioActual)
        {
            if (a.ToUpper() != usuarioOrig.ToUpper())
            {
                panel3.Visible = true;
                label4.Text = "Invitación de " + a;
            }
        }
        public void ActualizaChat(string mensaje, string username)
        {
            jg.SetRespuesta(mensaje, username);
        }

        public void EmpiezaPartida(string nombre1, string nombre2, string nombre3, int numpartida)
        {
            jg = new juego();
            jg.SetSocket(server);
            jg.setUsuario(usuarioOrig);
            jg.SetNumero(numpartida);
            jg.setNombre(nombre1, nombre2, nombre3);
            jg.Show();
        }

        public void ActualizaInvitado(string invitado, string acepta)
        {
            inv.actualizaInvitados(invitado, acepta);
        }
        private void ActualizarTanque(int numpartida, string oponente, int x, int y, int direccion)
        {
            jg.ActualizarMovimientoTanques(numpartida, oponente, x, y, direccion);
        }
        private void CuboRoto(int numpartida, int numcubo, int numimg)
        {
            jg.ActualizarCuboRoto(numpartida, numcubo, numimg);
        }
        private void ActualizarBala(int numpartida,string oponente, int x, int y, string direccion)
        {
            jg.NuevaBala(numpartida, oponente, x, y, direccion);
        }
        private void ActualizarPowerUp(int numpartida, int numpowerup, int vidas, string oponente)
        {
            jg.ActualizarPowerUp(numpartida, numpowerup, vidas, oponente);
        }
        private void ActualizarVidas(int numpartida, string oponente, int vidas)
        {
            jg.ActualizarVidas(numpartida, oponente, vidas);
        }
        private void DesconectarPorDelegado(bool usuarioDadoDeBaja)
        {
            if (usuarioDadoDeBaja)
            {
                if (db != null)
                    db.Close();

                MessageBox.Show("Te has dado de baja correctamente.");
                //Mensaje de desconexión
                estado = 1;
                string mensaje = "0/" + usuarioOrig;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Nos desconectamos
                atender.Abort();
                this.BackColor = Color.Lavender;
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                user = new User();
                user.setServer(server);
                this.Hide();
                user.ShowDialog();
            }
            else
            {
                MessageBox.Show("El usuario y la contraseña no coinciden.", "ATENCIÓN");
            }
        }

        private void ContraCambiada(bool contracambiada)
        {
            if (contracambiada)
            {
                if (cc != null)
                    cc.Close();

                MessageBox.Show("¡Contraseña cambiada con éxito!");
            }
            else
            {
                MessageBox.Show("No ha sido posible cambiar la contraseña.", "ATENCIÓN");
            }

        }
        public void MuestraConectados(string[] vector)
        {
            ShowConectados.RowHeadersVisible = false;
            ShowConectados.ColumnHeadersVisible = false;
            ShowConectados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            ShowConectados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ShowConectados.ColumnCount = 1;
            ShowConectados.RowCount = Convert.ToInt32(vector[0]) + 1;
            ShowConectados.MultiSelect = true;

            int i = 0;
            while (i < numeroConectados + 1)
            {

                if (i == 0)
                {
                    ShowConectados.Rows[i].Cells[0].Value = "Número de conectados: " + vector[i];
                }
                else
                {
                    ShowConectados.Rows[i].Cells[0].Value = conectados[i - 1];
                }
                i++;
            }

        }
        

        //////////////////////////////////////// ATENDER SERVER/////////////////////////////////////////////

        public void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                string mensaje = "";
                server.Receive(msg2);
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                mensaje = trozos[1].Split('\0')[0];
                int numpartida;

                switch (codigo)
                {

                    case 1:   // Respuesta a partidas ganadas por un jugador x

                        pg.setrespuesta(mensaje);
                        break;

                    case 2: // Ganadores de partidas de >10min

                        gm = new Ganadores10min();
                        gm.setLista(mensaje);
                        gm.ShowDialog();
                        break;


                    case 3: // Respuesta a la hora y la fecha de una partida 
                        string[] respuestaHora = trozos[1].Split('\0');
                        string dia = trozos[1];
                        string mes = trozos[2];
                        string[] horaTrozo = trozos[3].Split('\0');
                        string ano = horaTrozo[0].Split('_')[0];
                        string fecha = horaTrozo[0].Split('_')[1];

                        hf.setrespuesta(trozos[0], dia, mes, ano, fecha);
                        break;

                    case 4:  // Respuesta a partidas un día x

                        gd.setrespuesta(mensaje);
                        break;

                    case 7: // Respuesta a lista de conectados 
                        string mensj = mensaje.Replace("\n7", "\n");
                        string[] vector = mensj.Split(',');
                        int numeroConectadosNuevo = Convert.ToInt32(vector[0]);

                        // Comprobar si se ha conectado un nuevo usuario
                        if (numeroConectadosNuevo > numeroConectados)
                        {
                            Notificacion.BeginInvoke((MethodInvoker)(() =>
                            {
                                Notificacion.Text = "Se ha conectado un nuevo usuario: " + vector[numeroConectadosNuevo];
                            }));
                            //añade a todos los conectados que recibe del servidor al vector conectados
                            if (numeroConectadosNuevo == 1) //si es el unico en conectarse
                            {
                                conectados[0] = vector[1];
                            }
                            else //si no es el primero en conectarse: para almacenar a todos los ya conectados
                            {
                                for (int i = 1; i < vector.Length; i++)
                                {
                                    conectados[i - 1] = vector[i];
                                }
                            }
                        }
                        // Comprobar si un usuario se ha desconectado
                        else if (numeroConectadosNuevo < numeroConectados)
                        {
                            //string nombreAntiguo = vector[1].Split('\n')[0];
                            string nombreAntiguo = vector[1];

                            Notificacion.BeginInvoke((MethodInvoker)(() =>
                            {
                                Notificacion.Text = "El usuario " + vector[1].Split('\n')[0] + " se ha desconectado";
                            }));

                            int index = Array.IndexOf(conectados, nombreAntiguo);
                            if (index >= 0)
                            {
                                Array.Copy(conectados, index + 1, conectados, index, conectados.Length - index - 1);
                                conectados[conectados.Length - 1] = null;
                            }
                        }
                        numeroConectados = numeroConectadosNuevo;
                        MostrarConectados delegadoMuestra = new MostrarConectados(MuestraConectados);
                        ShowConectados.Invoke(delegadoMuestra, new object[] { vector });
                        break;

                    case 6: // Respuesta servicios realizados 

                        DelegadoParaEscribir delegado = new DelegadoParaEscribir(PonContador);
                        servicios_rec.Invoke(delegado, new object[] { mensaje });
                        break;
                    case 990: // Notificación de invitación a partida
                        anfitrion = trozos[2];
                        string jugador = trozos[3].TrimEnd('\n', '\0');
                        if (jugador == usuarioOrig)
                        {
                            DelegadoInvitado delegadoInv = new DelegadoInvitado(MuestraInvitacion);
                            panel3.Invoke(delegadoInv, new object[] { anfitrion, trozos[3] });
                        }

                        break;
                    case 991:

                        if (trozos[2] == usuarioOrig)
                        {
                            DelegadoInvitadoAceptaRechaza delegadoAcepta = new DelegadoInvitadoAceptaRechaza(ActualizaInvitado);
                            inv.Invoke(delegadoAcepta, new object[] { trozos[3], trozos[4] });
                        }

                        break;
                    case 997:

                        string mensajechat = trozos[1];
                        string username = trozos[2];

                        if (jg != null)
                        {
                            DelegadoChat delegadoChat = new DelegadoChat(ActualizaChat);
                            jg.Invoke(delegadoChat, new object[] { mensajechat, username.Replace("\0", "") });
                            //jg.SetRespuesta(mensajechat, username);
                        }

                        break;
                    case 999:
                        numpartida = Convert.ToInt32(trozos[1]);
                        string anfitrionTmp = trozos[2];

                        string[] participantes = trozos[3].Split('-');

                        string nombre1 = participantes[0];

                        string nombre2 = string.Empty, nombre3 = string.Empty;

                        if (participantes.Length > 1 && !string.IsNullOrEmpty(participantes[1]))
                        {
                            nombre2 = participantes[1].Replace("\0", "");
                        }

                        if (participantes.Length > 2 && !string.IsNullOrEmpty(participantes[2]))
                        {
                            nombre3 = participantes[2].Replace("\0", "");
                        }

                        if (nombre1 == usuarioOrig || nombre2 == usuarioOrig || nombre3 == usuarioOrig)
                        {
                            if (jg == null)
                            {
                                DelegadoEmpezarPartidaParticipantes delegadoEmpezar = new DelegadoEmpezarPartidaParticipantes(EmpiezaPartida);
                                Invoke(delegadoEmpezar, new object[] { nombre1, nombre2, nombre3, numpartida });
                            }
                        }
                        break;
                    case 50:
                        numpartida = Convert.ToInt32(trozos[1]);
                        string player = trozos[2];
                        int x = Convert.ToInt32(trozos[3]);
                        int y = Convert.ToInt32(trozos[4]);
                        int direccion = Convert.ToInt32(trozos[5]);
                        DelegadoActualizarTanque tanque = new DelegadoActualizarTanque(ActualizarTanque);
                        Invoke(tanque, new object[] { numpartida, player, x, y, direccion });
                
                        break;
                    case 51:
                        numpartida = Convert.ToInt32(trozos[1]);
                        string op = trozos[2];
                        int b_x = Convert.ToInt32(trozos[3]);
                        int b_y = Convert.ToInt32(trozos[4]);
                        string direccion_b = trozos[5].TrimEnd('\0');

                        DelegadoActualizarBala bala = new DelegadoActualizarBala(ActualizarBala);
                        Invoke(bala, new object[] {numpartida, op, b_x, b_y, direccion_b });

                        break;
                    case 52:
                        numpartida = Convert.ToInt32(trozos[1]);
                        int numcubo = Convert.ToInt32(trozos[2]);
                        int numimg = Convert.ToInt32(trozos[3]);

                        DelegadoCuboRoto cubo = new DelegadoCuboRoto(CuboRoto);
                        Invoke(cubo, new object[] { numpartida, numcubo, numimg });
                        break;
                    case 53:
                        numpartida = Convert.ToInt32(trozos[1]);
                        int numpowerup = Convert.ToInt32(trozos[2]);
                        int vidas = Convert.ToInt32(trozos[3]);
                        string oponente = trozos[4].TrimEnd('\0');

                        DelegadoPowerUp powerup = new DelegadoPowerUp(ActualizarPowerUp);
                        Invoke(powerup, new object[] {numpartida, numpowerup, vidas, oponente });

                        break;
                    case 54:
                        numpartida = Convert.ToInt32(trozos[1]);
                        string oponente1 = trozos[2];
                        int vida = Convert.ToInt32(trozos[3]);

                        DelegadoVida actualizar_vidas = new DelegadoVida(ActualizarVidas);
                        Invoke(actualizar_vidas, new object[] {numpartida, oponente1, vida});

                        break;
                    case 888:
                        {
                            bool usuarioDadoDeBaja = trozos[1].StartsWith("Correct");
                            DelegadoDesconectar delDesconectar = new DelegadoDesconectar(DesconectarPorDelegado);
                            Invoke(delDesconectar, new object[] { usuarioDadoDeBaja });
                            break;
                        }
                    case 720:
                        bool contracambiada = trozos[1].StartsWith("Correct");
                        DelegadoContraCambiada delContra = new DelegadoContraCambiada(ContraCambiada);
                        Invoke(delContra, new object[] { contracambiada });
                        break;
                }
            }
        }


 //////////////////////////////////////// ATENDER SERVER/////////////////////////////////////////////
       
        
        private void cuantasPartidasHeGanadoEnTotalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pg = new PartidasGanadas();
            pg.setServer(server);
            pg.ShowDialog();
        }

        private void quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mensaje = "2/vacio";
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void horaYFechaDeUnaPartidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hf = new HoraFecha();
            hf.setServer(server);
            hf.ShowDialog();
        }

        private void cuántasPartidasGanéElDiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gd = new GanadasDia();
            gd.setServer(server);
            gd.setName(usuarioOrig);
            gd.ShowDialog();
        }
        private void Desconectar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Estás seguro de que te quieres desconectar?", "ATENCIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //Mensaje de desconexión
                estado = 1;
                string mensaje = "0/" + usuarioOrig;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Nos desconectamos
                atender.Abort();
                this.BackColor = Color.Lavender;
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                user = new User();
                user.setServer(server);
                this.Hide();
                user.ShowDialog();
            }

        }

        private void jugar_Click(object sender, EventArgs e) //es el de jugar
        {
            jug = new Jugadores();
            jug.ShowDialog();

            int opcion = jug.OpcionSeleccionada;

            switch(opcion)
            {
                //En función de cuantas personaes se puede elegir
                case 0:
                    juego jg = new juego();
                    jg.SetSocket(server);
                    jg.setNombre1(usuarioOrig);
                    jg.ShowDialog();
                    this.Close();
                    break;
                //abre el juego
                case 1:
                    ShowConectados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    ShowConectados.MultiSelect = false;
                    break;
                case 2:
                    //solo se puedan seleccionar 2 filas
                    ShowConectados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    ShowConectados.MultiSelect = true;
                    maxSeleccion = 2;
                    break;
                case 3:
                    //solo se puedan seleccionar 2 filas
                    ShowConectados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    ShowConectados.MultiSelect = true;
                    maxSeleccion = 3;
                    break;
            }
            jugar.Enabled = false;
            invitar.Visible = true;
            invitar.Enabled = true;
        }

        private void Invitar_Click(object sender, EventArgs e) // INVITAR
        {
            inv = new Invitar();

            string nombre1 = string.Empty, nombre2 = string.Empty;

            switch(ShowConectados.SelectedRows.Count)
            {
                case 1:
                    nombre1 = (string)ShowConectados.SelectedRows[0].Cells[0].Value;
                    break;
                case 2:
                    nombre1 = (string)ShowConectados.SelectedRows[0].Cells[0].Value;
                    nombre2 = (string)ShowConectados.SelectedRows[1].Cells[0].Value;
                    break;
            }

            if (nombre1.TrimEnd('\n') == usuarioOrig || nombre2.TrimEnd('\n') == usuarioOrig)
                MessageBox.Show("Por favor, no se seleccione a si mismo.");
            else
            {
                inv.setNombre(nombre1, nombre2);
                inv.setUsuario(usuarioOrig);
                inv.setServer(server);
                if (inv.ShowDialog() == DialogResult.OK)
                {
                    string participantes = usuarioOrig + "-" + nombre1.TrimEnd('\n');

                    if (!string.IsNullOrEmpty(nombre2))
                    {
                        participantes += "-" + nombre2.TrimEnd('\n');
                    }

                    EnviarDatos($"999/{usuarioOrig}/{participantes}", server);
                }

                jugar.Enabled = true;
                invitar.Enabled = false;
                invitar.Visible = false;
            }
        }

        private void ShowConectados_SelectionChanged(object sender, EventArgs e)
        {
            if(ShowConectados.SelectedRows.Count > maxSeleccion)
            {
                ShowConectados.SelectedRows[0].Selected = false;
            }
        }

        private void AceptarInvitacion_Click(object sender, EventArgs e)
        {
            EnviarDatos($"991/Invitacion/{anfitrion}/{username}/aceptar", server);
            panel3.Visible = false;
        }

        private void EnviarDatos(string respuesta, Socket socket)
        {
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(respuesta.ToUpper());
            socket.Send(respuestaBytes);
        }

        private void RechazarInvitacion_Click(object sender, EventArgs e)
        {
            EnviarDatos($"991/Invitacion/{anfitrion}/{username}/rechazar", server);
            panel3.Visible = false;
        }

        private void darseDeBajaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (db != null && db.Visible)
            {
                db.Close();
            }

            db = new Darse_de_baja();
            db.SetSocket(server);
            db.setUsuario(usuarioOrig);
            db.Show();
            //if(db.ShowDialog() == DialogResult.OK)
            //{
            //    //Mensaje de desconexión
            //    estado = 1;
            //    string mensaje = "0/" + usuarioOrig;

            //    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            //    server.Send(msg);

            //    // Nos desconectamos
            //    atender.Abort();
            //    this.BackColor = Color.Lavender;
            //    server.Shutdown(SocketShutdown.Both);
            //    server.Close();
            //    user = new User();
            //    user.setServer(server);
            //    this.Hide();
            //    user.ShowDialog();
            //}

        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cc != null && db.Visible)
            {
                cc.Close();
            }

            cc = new CambiarContraseña();
            cc.SetSocket(server);
            cc.setUsuario(usuarioOrig);
            cc.Show();
        }
    }
}
