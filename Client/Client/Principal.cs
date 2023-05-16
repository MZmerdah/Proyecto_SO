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
        string ListaConectados;
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

        int maxSeleccion = 1;
        int numeroConectados = 0;
        string[] conectados = new string[100]; //vector para almacenar todos los usuarios conectados
        string usuarioOrig = string.Empty;
        delegate void DelegadoParaEscribir(string mensaje);

        delegate void MostrarConectados(string[] vector);
        delegate void DelegadoInvitado(string a, string usuarioActual);
        delegate void DelegadoInvitadoAceptaRechaza(string a, string acepta);
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

        public void ActualizaInvitado(string invitado, string acepta)
        {
            inv.actualizaInvitados(invitado, acepta);
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
            while (i < vector.Length)
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
                        string[] vector = mensaje.Split(',');
                        int numeroConectadosNuevo = Convert.ToInt32(vector[0]);

                        // Comprobar si se ha conectado un nuevo usuario
                        if (numeroConectadosNuevo > numeroConectados /*&& nombreNuevo.ToUpper() != usuarioOrig.ToUpper()*/)
                        {
                            string nombreNuevo = vector[numeroConectadosNuevo];
                            MessageBox.Show("Se ha conectado un nuevo usuario: " + nombreNuevo);
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
                        else if  (numeroConectadosNuevo < numeroConectados)
                        {
                            string nombreAntiguo = vector[1].Split('\n')[0];
                            string nombreAntiguo2 = vector[1];
                            MessageBox.Show("El usuario " + nombreAntiguo + " se ha desconectado.");
                            int index = Array.IndexOf(conectados, nombreAntiguo2);
                            //para borrar el usuario desconectado de la lista de conectados
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

                        // servicios_rec.Text = "Número total de servicios: " + mensaje;
                        DelegadoParaEscribir delegado = new DelegadoParaEscribir(PonContador);
                        servicios_rec.Invoke(delegado, new object[] {mensaje});
                        break;
                    case 990: // Notificación de invitación a partida
                        anfitrion = trozos[2];
                        DelegadoInvitado delegadoInv = new DelegadoInvitado(MuestraInvitacion);
                        panel3.Invoke(delegadoInv, new object[] { anfitrion, trozos[3] });
                        
                        
                        break;
                    case 991:

                        if (trozos[2] == usuarioOrig)
                        {
                            DelegadoInvitadoAceptaRechaza delegadoAcepta = new DelegadoInvitadoAceptaRechaza(ActualizaInvitado);
                            inv.Invoke(delegadoAcepta, new object[] { trozos[3], trozos[4] });
                        }
                       
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

        private void ShowConectados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void servicios_rec_Click(object sender, EventArgs e)
        {

        }

        private void servicios_rec_TextChanged(object sender, EventArgs e)
        {

        }

        private void invitar_Click(object sender, EventArgs e) //es el de jugar
        {
            jug = new Jugadores();
            jug.ShowDialog();

            int opcion = jug.OpcionSeleccionada;

            switch(opcion)
            {
                //En función de cuantas personaes se puede elegir
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
            button4.Enabled = true;

            //if(ShowConectados.Rows.Count == 0 || ShowConectados.SelectedRows.Count == 0)
            //{
            //    MessageBox.Show("No tienes amigos para jugar");
            //}
            //else 
            //{
            //    if (ShowConectados.SelectedRows.Count > 3)
            //    {
            //    }
            //    else
            //    {
            //        //enviamos mensaje al servidor
            //       int select = ShowConectados.SelectedRows.Count;
            //       for (int i = 0; i < select; i++ )
            //       {
            //            string mensaje = "/1" + ShowConectados.SelectedRows[i].Cells[i].Value.ToString();
            //            //Enviamos el mensaje al servidor
            //            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            //            server.Send(msg);
            //       }

            //        string nombre1 = ShowConectados.SelectedRows[0].Cells[0].Value.ToString();aaa
            //        string nombre2 = ShowConectados.SelectedRows[1].Cells[1].Value.ToString();
            //        string nombre3 = ShowConectados.SelectedRows[2].Cells[2].Value.ToString();
                  

            //        //abrimos formulario
            //        inv = new Invitar();
            //        inv.setServer(server);
            //        inv.setNombre(nombre1, nombre2, nombre3); //hay que controlar el error de si no se selecciona mas de 1 jugador
            //        inv.ShowDialog();
                  
            //        //servidor envia mensaje que los que inivitas
            //        //aceptan o rechazan y lo envian al servidor
            //        //el servidor te lo dice 

                  
        //        }
        //    }

        }

        private void button3_Click(object sender, EventArgs e) //Empezar la partida según lo que hayamos seleccionado
        {
            juego partida = new juego();
            partida.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            inv = new Invitar();

            string nombre1 = string.Empty, nombre2 = string.Empty, nombre3 = string.Empty;

            switch(ShowConectados.SelectedRows.Count)
            {
                case 1:
                    nombre1 = (string) ShowConectados.SelectedRows[0].Cells[0].Value;
                    break;
                case 2:
                    nombre1 = (string)ShowConectados.SelectedRows[0].Cells[0].Value;
                    nombre2 = (string)ShowConectados.SelectedRows[1].Cells[0].Value;
                    break;
                case 3:
                    nombre1 = (string)ShowConectados.SelectedRows[0].Cells[0].Value;
                    nombre2 = (string)ShowConectados.SelectedRows[1].Cells[0].Value;
                    nombre3 = (string)ShowConectados.SelectedRows[2].Cells[0].Value;
                    break;
            }

            inv.setNombre(nombre1, nombre2, nombre3);
            inv.setUsuario(usuarioOrig);
            inv.setServer(server);
            inv.ShowDialog();

            jugar.Enabled = true;
            button4.Enabled = false;
        }

        private void ShowConectados_SelectionChanged(object sender, EventArgs e)
        {
            if(ShowConectados.SelectedRows.Count > maxSeleccion)
            {
                ShowConectados.SelectedRows[0].Selected = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EnviarDatos($"991/Invitacion/{anfitrion}/{username}/aceptar", server);
            panel3.Visible = false;
        }

        private void EnviarDatos(string respuesta, Socket socket)
        {
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(respuesta.ToUpper());
            socket.Send(respuestaBytes);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EnviarDatos($"991/Invitacion/{anfitrion}/{username}/rechazar", server);
            panel3.Visible = false;
        }
    }
}
