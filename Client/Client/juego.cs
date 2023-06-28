using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Properties;

namespace Client
{
    public partial class juego : Form
    {
        int? numpartida = null;
        int numjugadores = 3;
        string anfitrion;
        string username;
        string miUsuario;
        string nombre1;
        string nombre2;
        string nombre3;
        Socket server;
        delegate void AñadirMensaje(string[] vector);
        GameOver fin = new GameOver();

        int vida, vidas2; //contador de vidas
        int direccion = 0;

        bool arriba, abajo, derecha, izq; //para detectar cuando se presionan las teclas
        bool p_arriba, p_abajo, p_derecha, p_izq; //para detectar la dirección que mira el tanque

        string jugador = "P1"; //asignamos que jugador es: P1, P2 o P3

        int velocidad;
        int num_balas = 0;
        private DateTime inicioPowerup;
        private DateTime movimientoTanque;//cada 3 segundos cambiara de direccion

        List<PictureBox> tanques = new List<PictureBox>();
        List<PictureBox> balas = new List<PictureBox>();
        List<PictureBox> balasEnemigas = new List<PictureBox>();
        List<PictureBox> paredes = new List<PictureBox>();
        List<PictureBox> CuboPowerUp = new List<PictureBox>();
        List<Image> tanque1 = new List<Image>
        {
            Properties.Resources.blue_tank1,
            Properties.Resources.blue_right,
            Properties.Resources.blue_down,
            Properties.Resources.blue_left,
        };
        List<Image> tanque2 = new List<Image>
        {
            Properties.Resources.pink_tank,
            Properties.Resources.pink_right,
            Properties.Resources.pink_down,
            Properties.Resources.pink_left,
        };
        List<Image> tanque3 = new List<Image>
        {
            Properties.Resources.grey_tank,
            Properties.Resources.grey_right,
            Properties.Resources.grey_down,
            Properties.Resources.grey_left,
        };
        List<Image> imagenvidas = new List<Image>
        {
            Properties.Resources._0vidas,
            Properties.Resources._1vida,
            Properties.Resources._2vidas,
            Properties.Resources._3vidas,
            Properties.Resources._4vidas,
            Properties.Resources._5vidas,
        };
        List<Image> powerUps = new List<Image>
        {
            Properties.Resources.red_heart,
            Properties.Resources.speedboost,
            //Properties.Resources.slow,
        };

        public void setNombre1(string nombre)//para cuando solo haya 1 jugador
        {
            this.nombre1 = nombre;
            jugador1.Text = nombre1.Replace("\n", string.Empty);
            jugador = "P1";
        }
        public void setNombre(string nombre1, string nombre2, string nombre3)
        {
            this.nombre1 = nombre1;
            this.nombre2 = nombre2;
            this.nombre3 = nombre3;
            jugador1.Text = nombre1.Replace("\n", string.Empty);
            jugador2.Text = nombre2.Replace("\n", string.Empty);
            jugador3.Text = nombre3.Replace("\n", string.Empty);

            //asignamos un tanque a cada jugador
            if (nombre1 == miUsuario)
                jugador = "P1";
            else if (nombre2 == miUsuario)
                jugador = "P2";
            else if (nombre3 == miUsuario)
                jugador = "P3";
            if (nombre3 == "")
                numjugadores = 2;
        }
        public void setUsuario(string usuario)
        {
            miUsuario = usuario;
        }

        public void SetSocket(Socket socket)
        {
            server = socket;
        }

        public void SetNumero(int num)
        {
            numpartida = num;
        }

        private void EnviarDatos(string respuesta, Socket socket)
        {
            if (numpartida != null)
            {
                byte[] respuestaBytes = Encoding.ASCII.GetBytes(respuesta);
                socket.Send(respuestaBytes);
            }
        }
        public void SetRespuesta(string mensaje, string username)
        {
            if (username == miUsuario)
                return;

            Chat.Text += username + ": " + mensaje + Environment.NewLine;
        }
        private void Enviar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Mensaje.Text))
            {
                return;
            }
            else
            {
                string mensaje = (string)Mensaje.Text; //texto del mensaje
                Chat.Text += "Yo: " + Mensaje.Text + Environment.NewLine;
                Mensaje.Clear();
                EnviarDatos($"997/{mensaje}/{miUsuario}", server);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            arriba = false;
            abajo = false;
            izq = false;
            derecha = false;
        }

        private void Mostrar1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

        private void Ocultar_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        public void RecibirMensaje(Socket socket)
        {
            //recibe el mensaje 


            byte[] buffer = new byte[1024];
            int bytesRecibidos = socket.Receive(buffer);
            string respuesta = Encoding.ASCII.GetString(buffer, 0, bytesRecibidos);


            // byte[] msg2 = new byte[80];
            // string mensaje = "";
            // server.Receive(msg2);
            // string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
            //// int codigo = Convert.ToInt32(trozos[0]);
            // mensaje = trozos[3].Split('\0')[3];

            Chat.Text = username + ": " + respuesta;

        }

        public juego()
        {
            InitializeComponent();

        }

        private void juego_Load(object sender, EventArgs e)
        {
            nueva_partida();

            Ocultar.Checked = true;
            groupBox1.Visible = false;
            Ocultar.Focus();
        }

        private void Jugador_KeyDown(object sender, KeyEventArgs e) //Cuando se presione la tecla se activa el evento
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                abajo = true;
                arriba = false;
                izq = false;
                derecha = false;
            }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                arriba = true;
                abajo = false;
                izq = false;
                derecha = false;
            }

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                izq = true;
                abajo = false;
                arriba = false;
                derecha = false;
            }

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                derecha = true;
                abajo = false;
                arriba = false;
                izq = false;
            }

            if (e.KeyCode == Keys.Space)
            {
                if (Mensaje.TextLength == 0)
                {
                    if (jugador == "P1")
                    {
                        DispararBalas(tankP1);
                    }
                    else if (jugador == "P2")
                    {
                        DispararBalas(tankP2);
                    }
                    else if (jugador == "P3")
                    {
                        DispararBalas(tankP3);
                    }
                }
            }
        }

        private void Jugador_KeyUp(object sender, KeyEventArgs e) //Cuando se suelte la tecla se deja de mover el personaje
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                abajo = false;
            }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                arriba = false;
            }

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                izq = false;
            }

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                derecha = false;
            }

        }

        private void MoverTanque(PictureBox tanque, List<Image> imagen_tanque)
        {
            if (izq == true)
            {
                tanque.Left -= velocidad;
                ActualizarImagen(tanque, 3, imagen_tanque);
                p_arriba = false;
                p_abajo = false;
                p_derecha = false;
                p_izq = true;
                MovimientoTanque(tanque);
            }

            if (derecha == true)
            {
                tanque.Left += velocidad;
                ActualizarImagen(tanque, 1, imagen_tanque);
                p_arriba = false;
                p_abajo = false;
                p_derecha = true;
                p_izq = false;
                MovimientoTanque(tanque);
            }

            if (arriba == true)
            {
                tanque.Top -= velocidad;
                ActualizarImagen(tanque, 0, imagen_tanque);
                p_arriba = true;
                p_abajo = false;
                p_derecha = false;
                p_izq = false;
                MovimientoTanque(tanque);
            }

            if (abajo == true)
            {
                tanque.Top += velocidad;
                ActualizarImagen(tanque, 2, imagen_tanque);
                p_arriba = false;
                p_abajo = true;
                p_derecha = false;
                p_izq = false;
                MovimientoTanque(tanque);
            }
        }

        private void ActualizarImagen(PictureBox tanque, int direccion, List<Image> imagentanque)
        {
            for (int i = 0; i <= direccion; i++)
            {
                if (direccion == 0 || direccion == 2)
                    tanque.Size = new Size(30, 45);
                else
                    tanque.Size = new Size(45, 30);

                tanque.Image = imagentanque[i];
            }
        }

        private void DispararBalas(PictureBox tanque)
        {
            if (num_balas < 7)
            {
                string direccion;
                int x = tanque.Location.X + tanque.Size.Width / 2;
                int y = tanque.Location.Y + tanque.Size.Height / 2;

                PictureBox bala = new PictureBox();
                bala.Size = new Size(5, 5);
                bala.SizeMode = PictureBoxSizeMode.StretchImage;
                bala.Image = Properties.Resources.bala2;
                bala.Location = new Point(x, y);
                this.Controls.Add(bala);
                balas.Add(bala);
                num_balas++;

                if (p_arriba == true)
                    direccion = "arriba";
                else if (p_abajo == true)
                    direccion = "abajo";
                else if (p_izq == true)
                    direccion = "izq";
                else 
                    direccion = "derecha";

                bala.Tag = direccion;

                EnviarDatos($"51/{numpartida}/{jugador}/{x}/{y}/{direccion}", server);

            }
        }

        private void MoverBalas()
        {
            for (int i = 0; i < balas.Count; i++)
            {
                PictureBox bala = balas[i];

                if (bala.Tag.ToString() == "arriba")
                    bala.Top -= 25;
                else if (bala.Tag.ToString() == "abajo")
                    bala.Top += 25;
                else if (bala.Tag.ToString() == "izq")
                    bala.Left -= 25;
                else if (bala.Tag.ToString() == "derecha")
                    bala.Left += 25;

                foreach (PictureBox pared in paredes)
                {
                    if (Choque(bala, pared) == 0)
                    {
                        EliminarBalas(bala);
                        break;
                    }
                }

                foreach (PictureBox cubo in CuboPowerUp)
                {
                    if (Choque(bala, cubo) == 0)
                    {
                        int index = PowerUp(cubo);
                        int num_cubo = CuboPowerUp.IndexOf(cubo);
                        EnviarDatos($"52/{numpartida}/{num_cubo}/{index}", server);
                        EliminarBalas(bala);
                        break;
                    }
                }

                if (jugador != "P1" )
                {
                    if (Choque(bala, tankP3) == 0)
                        EliminarBalas(bala);
                    if (Choque(bala, tankP2) == 0)
                        EliminarBalas(bala);
                }
                if (jugador != "P2")
                {
                    if (Choque(bala, tankP1) == 0)
                        EliminarBalas(bala);
                    if (Choque(bala, tankP3) == 0)
                        EliminarBalas(bala);
                }
                if (jugador != "P3")
                {
                    if (Choque(bala, tankP1) == 0)
                        EliminarBalas(bala);
                    if (Choque(bala, tankP2) == 0)
                        EliminarBalas(bala);
                }

            }
            for (int i = 0; i < balasEnemigas.Count; i++)
            {
                PictureBox bala = balasEnemigas[i];

                if (bala.Tag.ToString() == "arriba")
                    bala.Top -= 25;
                else if (bala.Tag.ToString() == "abajo")
                    bala.Top += 25;
                else if (bala.Tag.ToString() == "izq")
                    bala.Left -= 25;
                else if (bala.Tag.ToString() == "derecha")
                    bala.Left += 25;

                foreach (PictureBox pared in paredes)
                {
                    if (Choque(bala, pared) == 0)
                    {
                        EliminarBalas(bala);
                        break;
                    }
                }
                foreach (PictureBox cubo in CuboPowerUp)
                {
                    if (Choque(bala, cubo) == 0)
                    {
                        EliminarBalas(bala);
                        break;
                    }
                }

                if (Choque(bala, tankP1) == 0 && jugador == "P1")
                {
                    if (vida != 0)
                    {
                        vida--;
                        ContadorVidas(tankP1, P1_vidas, vida, 0);
                    }
                    EliminarBalas(bala);
                    break;

                }
                else if (Choque(bala, tankP2) == 0 && jugador == "P2")
                {
                    if (vida != 0)
                    {
                        vida--;
                        ContadorVidas(tankP2, P2_vidas, vida, 0);
                    }
                    EliminarBalas(bala);
                    break;

                }
                else if (Choque(bala, tankP3) == 0 && jugador == "P3")
                {
                    if (vida != 0)
                    {
                        vida--;
                        ContadorVidas(tankP3, P3_vidas, vida, 0);
                    }
                    EliminarBalas(bala);
                    break;

                }
            }
        }
        public void NuevaBala(int num, string oponente, int x, int y, string direccion)
        {
            if (num == numpartida && oponente!= jugador)
            {
                PictureBox bala = new PictureBox();
                bala.Size = new Size(5, 5);
                bala.SizeMode = PictureBoxSizeMode.StretchImage;
                bala.Image = Properties.Resources.bala2;
                bala.Location = new Point(x, y);
                bala.Tag = direccion;
                this.Controls.Add(bala);
                balasEnemigas.Add(bala);
            }
        }

        private void EliminarBalas(PictureBox bala)
        {
            for (int i = 0; i < balasEnemigas.Count; i++)
            {
                if (bala == balasEnemigas[i])
                {
                    balasEnemigas.Remove(bala);
                }
            }
            for (int i = 0; i < balas.Count; i++)
            {
                if (bala == balas[i])
                {
                    balas.Remove(bala);
                    num_balas--;
                }
            }
            bala.Left = 0;
            bala.Top = 0;
            bala.Size = new Size(0, 0);
            bala.Dispose();
        }
        private void MoverTanquesEnemigos(PictureBox tanque, List<Image> imagentanque)
        {
            if (direccion == 1)
            {
                tanque.Left += velocidad;//derecha
            }
            else if (direccion == 3)
            {
                tanque.Left -= velocidad;//izq
            }
            else if (direccion == 2)
            {
                tanque.Top += velocidad;//abajo
            }
            else if (direccion == 0)
            {
                tanque.Top -= velocidad;//arriba
            }

            ActualizarImagen(tanque, direccion, imagentanque);

        }

        private void ContadorVidas(PictureBox tanque, PictureBox vidas, int contadorvidas, int a)
        {
            for (int i = 1; i <= contadorvidas; i++)
            {
                if (contadorvidas == i)
                    vidas.Image = imagenvidas[i];
            }
            if (contadorvidas == 0)
            {
                vidas.Image = imagenvidas[0];

                tanque.Controls.Remove(tanque);
                tanques.Remove(tanque);
                tanque.Dispose();
                tanque.Width = 0;
                tanque.Height = 0;
                GameOver();
            }
            if (a == 0)
                EnviarDatos($"54/{numpartida}/{jugador}/{vida}", server);
        }
        public void ActualizarVidas(int num, string oponente, int vidas)
        {
            if (num == numpartida)
            {
                if (oponente == "P1")
                    ContadorVidas(tankP1, P1_vidas, vidas, -1);
                else if (oponente == "P2")
                    ContadorVidas(tankP2, P2_vidas, vidas, -1);
                else if (oponente == "P3")
                    ContadorVidas(tankP3, P3_vidas, vidas, -1);
            }
        }

        private void ChoquePared(PictureBox tanque, List<PictureBox> paredes)
        {
            for (int i = 0; i < paredes.Count; i++)
            {
                PictureBox pared = paredes[i];

                if (Choque(tanque, pared) == 0)
                {
                    if (tanque.Left < pared.Right && tanque.Right > pared.Right)
                    {
                        tanque.Left = pared.Right;
                    }
                    else if (tanque.Right > pared.Left && tanque.Left < pared.Left)
                    {
                        tanque.Left = pared.Left - tanque.Width;
                    }
                    else if (tanque.Top < pared.Bottom && tanque.Bottom > pared.Bottom)
                    {
                        tanque.Top = pared.Bottom;
                    }
                    else if (tanque.Bottom > pared.Top && tanque.Top < pared.Top)
                    {
                        tanque.Top = pared.Top - tanque.Height;
                    }
                }
            }
        }
        private void ChoqueTanques(PictureBox tanque, PictureBox tanque2)
        {
            if (Choque(tanque, tanque2) == 0)
            {
                if (tanque.Left < tanque2.Right && tanque.Right > tanque2.Right)
                {
                    tanque.Left = tanque2.Right;
                }
                else if (tanque.Right > tanque2.Left && tanque.Left < tanque2.Left)
                {
                    tanque.Left = tanque2.Left - tanque.Width;
                }
                else if (tanque.Top < tanque2.Bottom && tanque.Bottom > tanque2.Bottom)
                {
                    tanque.Top = tanque2.Bottom;
                }
                else if (tanque.Bottom > tanque2.Top && tanque.Top < tanque2.Top)
                {
                    tanque.Top = tanque2.Top - tanque.Height;
                }
            }
        }
        private int Choque(PictureBox tanque, PictureBox pared) //tmb se usa para las balas y los cubos
        {
            if (tanque.Bounds.IntersectsWith(pared.Bounds) )//falta afegir les coords limit x a q no falli res
            {
                if (pared.Tag == null)
                {
                    return 0;
                }
                else //choca con un powerup --> lo pilla
                {
                    return 1;
                }
            }
            return -1;
        }

        private int PowerUp(PictureBox cubo) //cuando una bala toca un cubo de powerup, genera un powerup random
        {
            Random random = new Random();
            int index = random.Next(powerUps.Count);
            cubo.Image = powerUps[index];
            if (index == 0)
                cubo.Tag = "vida"; //recuperar 1 vida
            else if (index == 1)
                cubo.Tag = "velocidadx2"; //duplica tu velocidad
            return index;
        }

        public void ActualizarCuboRoto(int num, int numcubo, int index)
        {
            if (num == numpartida)
            {
                PictureBox cubo = CuboPowerUp[numcubo];
                cubo.Image = powerUps[index];
                if (index == 0)
                    cubo.Tag = "vida"; //recuperar 1 vida
                else if (index == 1)
                    cubo.Tag = "velocidadx2"; //duplica tu velocidad
                else if (index == 2)
                    cubo.Tag = "velocidad_rival/2"; //hace mas lentos a los rivales
            }
        }

        private void AplicarPowerUp(string jugador_, PictureBox powerup)
        {
            if (powerup.Tag.ToString() == "vida" && vida != 5 && vida != 0)
            {
                if (jugador_ == "P1")
                {
                    vida++;
                    ContadorVidas(tankP1, P1_vidas, vida, -1);
                }
                else if (jugador_ == "P2")
                {
                    vida++;
                    ContadorVidas(tankP2, P2_vidas, vida, -1);
                }
                else if (jugador_ == "P3")
                {
                    vida++;
                    ContadorVidas(tankP3, P3_vidas, vida, -1);
                }
            }
            else if (powerup.Tag.ToString() == "velocidadx2")
            {
                velocidad = 20;
                inicioPowerup = DateTime.Now;
            }

            int num_powerup = CuboPowerUp.IndexOf(powerup);
            EnviarDatos($"53/{numpartida}/{num_powerup}/{vida}/{jugador}", server);

        }

        public void ActualizarPowerUp(int num, int numpowerup, int vidas, string oponente)
        {
            if (num == numpartida)
            {
                PictureBox powerup = CuboPowerUp[numpowerup];
                ActualizarVidas(num, oponente, vidas);

                this.Controls.Remove(powerup);
                powerup.Size = new Size(0, 0);
                powerup.Top = 0;
                powerup.Left = 0;
                //CuboPowerUp.Remove(powerup);
                //powerup.Dispose();
            }
        }

        private void MovimientoTanque(PictureBox tanque)
        {
            int x = tanque.Left;
            int y = tanque.Top;
            int direccion = 0;

            if (p_abajo)
                direccion = 2;
            else if (p_arriba)
                direccion = 0;
            else if (p_derecha)
                direccion = 1;
            else if (p_izq)
                direccion = 3;

            EnviarDatos($"50/{numpartida}/{jugador}/{x}/{y}/{direccion}", server);
        }

        public void ActualizarMovimientoTanques(int num, string oponente, int x, int y, int direccion)
        {
            //segun lo que recibamos del servidor iremos moviendo los otros tanques
            if (num == numpartida)
            {
                if (oponente == "P1")
                {
                    tankP1.Left = x;
                    tankP1.Top = y;
                    ActualizarImagen(tankP1, direccion, tanque1);
                }
                if (oponente == "P2")
                {
                    tankP2.Left = x;
                    tankP2.Top = y;
                    ActualizarImagen(tankP2, direccion, tanque2);
                }
                if (oponente == "P3")
                {
                    tankP3.Left = x;
                    tankP3.Top = y;
                    ActualizarImagen(tankP3, direccion, tanque3);
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e) //la partida del juego
        {

            TimeSpan tiempoTranscurrido = DateTime.Now - inicioPowerup;

            if (tiempoTranscurrido.TotalSeconds >= 10)
            {
                velocidad = 10;
            }

            if (jugador == "P1")
            {
                MoverTanque(tankP1, tanque1);
            }
            else if (jugador == "P2")
            {
                MoverTanque(tankP2, tanque2);
            }
            else if (jugador == "P3")
            {
                MoverTanque(tankP3, tanque3);
            }

            if (numpartida == null) //un jugador vs la maquina
            {
                TimeSpan tiempo = DateTime.Now - movimientoTanque;
                if (tiempo.TotalSeconds >= 1.5) // cada 1.5 segundos cambiara de direccion
                {
                    movimientoTanque = DateTime.Now;
                    Random random = new Random();
                    direccion = random.Next(0, 3);
                }
                MoverTanquesEnemigos(tankP2, tanque2);

                for(int i= 0; i < balas.Count; i++)
                {
                    PictureBox bala = balas[i];
                    if (Choque(bala, tankP2) == 0)
                    {
                        vidas2--;
                        ContadorVidas(tankP2, P2_vidas, vidas2, 0);
                    }
                }
            }

            foreach (PictureBox tanque in tanques)
            {
                ChoquePared(tanque, paredes);
                ChoquePared(tanque, CuboPowerUp);
            }

            for (int i = 0; i < CuboPowerUp.Count; i++)
            {
                PictureBox powerup = CuboPowerUp[i];
                {
                    if (jugador == "P1" && Choque(tankP1, powerup) == 1)
                    {
                        AplicarPowerUp(jugador, powerup);
                    }
                    else if (jugador == "P2" && Choque(tankP2, powerup) == 1)
                    {
                        AplicarPowerUp(jugador, powerup);
                    }
                    else if (jugador == "P3" && Choque(tankP3, powerup) == 1)
                    {
                        AplicarPowerUp(jugador, powerup);
                    }
                }
            }

            ChoqueTanques(tankP1, tankP2);
            ChoqueTanques(tankP1, tankP3);
            ChoqueTanques(tankP2, tankP1);
            ChoqueTanques(tankP2, tankP3);
            ChoqueTanques(tankP3, tankP1);
            ChoqueTanques(tankP3, tankP2);

            MoverBalas();
        }

        private void GameOver()
        {
            int ganador;
            int jug;
            if (vida == 0)
                ganador = -1;
            else ganador = 0;
            if (jugador == "P1")
                jug = 0;
            else if (jugador == "P2")
                jug = 1;
            else
                jug = 2;

            fin.Ganador(ganador, jug, vida, miUsuario);
            fin.Show();
            this.Close();
        }
        private void nueva_partida() //crea un nuevo juego con los componentes necesarios
        {
            vida = 5;
            
            tankP1.Left = 28;
            tankP1.Top = 49;
            tankP1.Visible = true;
            tankP1.Size = new Size(30, 45);
            tankP1.Image = Properties.Resources.blue_tank1;
            P1_vidas.Visible = true;
            P1_vidas.Image = Properties.Resources._5vidas;
            p_arriba = true;

            tankP2.Left = 28;
            tankP2.Top = 488;
            tankP2.Visible = true;
            P2_vidas.Visible = true;
            tankP2.Size = new Size(30, 45);
            tankP2.Image = Properties.Resources.pink_tank;
            P2_vidas.Image = Properties.Resources._5vidas;
            tanques.Add(tankP1);
            tanques.Add(tankP2);
            velocidad = 10;
        
            if (numpartida == null || numjugadores == 2)
            {
                tankP3.Left = 1;
                tankP3.Top = 1;
                tankP3.Visible = false;
                tankP3.Size = new Size(0, 0);
                tankP3.Left = 0;
                tankP3.Top = 0;
                P3_vidas.Visible = false;
                tankP3.Controls.Remove(tankP3);
                tankP3.Enabled = false;
                jugador3.Visible = false;
                vidas2 = 5;
            }
            else
            {
                tankP3.Left = 626;
                tankP3.Top = 49;
                tankP3.Visible = true;
                tankP3.Size = new Size(30, 45);
                tankP3.Image = Properties.Resources.grey_tank;
                P3_vidas.Visible = true;
                P3_vidas.Image = Properties.Resources._5vidas;
                tanques.Add(tankP3);
            }
            movimientoTanque=DateTime.Now;

            //añadir las paredes en la lista
            paredes.Add(pared_abajo);
            paredes.Add(pared_arriba);
            paredes.Add(pared_derecha);
            paredes.Add(pared_izq);
            paredes.Add(pared1);
            paredes.Add(pared2);
            paredes.Add(pared3);
            paredes.Add(pared4);
            paredes.Add(pared5);
            paredes.Add(pared6);
            paredes.Add(pared7);
            paredes.Add(pared8);
            paredes.Add(pared9);
            paredes.Add(pared10);
            paredes.Add(pared11);
            paredes.Add(pared12);
            paredes.Add(pared13);
            paredes.Add(pared14);
            paredes.Add(pared15);
            paredes.Add(pared16);
            paredes.Add(pared17);
            paredes.Add(pared18);
            paredes.Add(pared19);
            paredes.Add(pared20);
            paredes.Add(pared21);
            paredes.Add(pared22);
            paredes.Add(pared23);
            paredes.Add(pared24);
            paredes.Add(pared25);
            paredes.Add(pared26);
            paredes.Add(pared27);
            paredes.Add(pared28);
            paredes.Add(pared29);
            paredes.Add(pared30);
            paredes.Add(pared31);
            paredes.Add(pared32);
            paredes.Add(pared33);
            paredes.Add(pared34);
            paredes.Add(pared35);
            paredes.Add(pared36);
            paredes.Add(pared37);
            paredes.Add(pared38);
            paredes.Add(pared39);
            paredes.Add(pared40);
            paredes.Add(pared41);

            //añadir los cubos de powerups en la lista
            CuboPowerUp.Add(powerup1);
            CuboPowerUp.Add(powerup2);
            CuboPowerUp.Add(powerup3);
            CuboPowerUp.Add(powerup4);
            CuboPowerUp.Add(powerup5);
            CuboPowerUp.Add(powerup6);
            CuboPowerUp.Add(powerup7);
            CuboPowerUp.Add(powerup8);
            CuboPowerUp.Add(powerup9);
            CuboPowerUp.Add(powerup10);

            timer1.Start();
        }

    }
}
