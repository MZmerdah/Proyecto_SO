using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        string anfitrion;
        string username;
        string miUsuario;
        string nombre1;
        string nombre2;
        string nombre3;
        Socket server;
        delegate void AñadirMensaje(string[] vector);



        int y; //posiciones
        int x; //posiciones

        int vidas1, vidas2, vidas3; //contador de vidas

        bool arriba, abajo, derecha, izq; //para detectar cuando se presionan las teclas
        bool p_arriba, p_abajo, p_derecha, p_izq; //para detectar la dirección que mira el tanque

        int v_jugador; //velocidades

        List<PictureBox> balas = new List<PictureBox>();

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tankP3_Click(object sender, EventArgs e)
        {

        }

        private void P2_vidas_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public void setNombre(string nombre1, string nombre2, string nombre3)
        {
            this.nombre1 = nombre1;
            this.nombre2 = nombre2;
            this.nombre3 = nombre3;
            label1.Text = nombre1.Replace("\n", string.Empty);
            label2.Text = nombre2.Replace("\n", string.Empty); 
            label3.Text = nombre3.Replace("\n", string.Empty); 


        }
        public void setUsuario(string usuario)
        {
            miUsuario = usuario;
        }

        public void SetSocket(Socket socket)
        {
            server = socket;
        }

        public void SetUser( string username)
        {
           
            this.username = username;
        }
        //public string RecibirInvitacion(Socket socket)
        //{
        //    byte[] buffer = new byte[1024];
        //    int bytesRecibidos = socket.Receive(buffer);
        //    string mensaje = Encoding.ASCII.GetString(buffer, 0, bytesRecibidos);
        //    MessageBox.Show(mensaje);

        //    return mensaje;
        //}
       
        private void EnviarDatos(string respuesta, Socket socket)
        {
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(respuesta);
            socket.Send(respuestaBytes);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Mensaje.Text))
            {
                return;
            }
            else
            {
                string mensaje = (string)Mensaje.Text; //texto del mensaje
                EnviarDatos($"997/Mensaje/{mensaje}/{username}", server);
            }
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void mostrar_Click(object sender, EventArgs e)
        {
            if(groupBox1.Visible == true)
            {
                groupBox1.Visible = false;
                mostrar.Text = "Mostrar";
            }

            else if (groupBox1.Visible == false)
            {
                groupBox1.Visible = true;
                mostrar.Text = "Ocultar";
            }

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
        private void Chat_TextChanged(object sender, EventArgs e)
        {
           

        }

        int num_balas = 0;

        int t_bala = 10; //tiempo de la bala antes que desaparezca

        public juego()
        {
            InitializeComponent();

            nueva_partida();
        }

        private void juego_Load(object sender, EventArgs e)
        {
            nueva_partida();
            groupBox1.Visible = true;
            mostrar.Text = "Ocultar";
        }

        private void P1_KeyDown(object sender, KeyEventArgs e) //Cuando se presione la tecla se activa el evento
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                abajo = true;
            }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                arriba = true;
            }

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                izq = true;
            }

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                derecha = true;
            }

            if (e.KeyCode == Keys.Space)
            {

                if (num_balas < 7)
                {
                    PictureBox bala = new PictureBox();
                    bala.Size = new Size(5, 5);
                    bala.SizeMode = PictureBoxSizeMode.StretchImage;
                    bala.Image = Properties.Resources.bala2;
                    bala.Location = new Point(tankP1.Location.X + tankP1.Size.Width / 2, tankP1.Location.Y + tankP1.Size.Height / 2);
                    this.Controls.Add(bala);
                    balas.Add(bala);
                    num_balas++;

                    if (p_arriba == true)
                        bala.Tag = "arriba";
                    else if (p_abajo == true)
                        bala.Tag = "abajo";
                    else if (p_izq == true)
                        bala.Tag = "izq";
                    else if (p_derecha == true)
                        bala.Tag = "derecha";
                }
            }
        }

        private void P1_KeyUp(object sender, KeyEventArgs e) //Cuando se suelte la tecla se deja de mover el personaje
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
        private void timer1_Tick(object sender, EventArgs e) //la partida del juego
        {

            //int timer = Convert.ToInt32(label1.Text);
            //timer--;
            //label1.Text = timer.ToString();
            //if (timer == 0)
            //{
            //    label1.Text = "YA";
            //    timer1.Stop();
            //    label1.Enabled = true;
            //}

            if (izq == true)
            {
                tankP1.Left -= v_jugador;
                tankP1.Size = new Size(45, 30);
                tankP1.Image = Properties.Resources.blue_left;
                p_arriba = false;
                p_abajo = false;
                p_derecha = false;
                p_izq = true;
            }

            if (derecha == true)
            {
                tankP1.Left += v_jugador;
                tankP1.Size = new Size(45, 30);
                tankP1.Image = Properties.Resources.blue_right;
                p_arriba = false;
                p_abajo = false;
                p_derecha = true;
                p_izq = false;
            }

            if (arriba == true)
            {
                tankP1.Top -= v_jugador;
                tankP1.Size = new Size(30, 45);
                tankP1.Image = Properties.Resources.blue_tank1;
                p_arriba = true;
                p_abajo = false;
                p_derecha = false;
                p_izq = false;
            }

            if (abajo == true)
            {
                tankP1.Top += v_jugador;
                tankP1.Size = new Size(30, 45);
                tankP1.Image = Properties.Resources.blue_down;
                p_arriba = false;
                p_abajo = true;
                p_derecha = false;
                p_izq = false;
            }

            if (tankP1.Left <= 28)
            {
                tankP1.Left = 28;
            }

            if (tankP1.Left >= 626)
            {
                tankP1.Left = 626;
            }

            if (tankP1.Top <= 49)
            {
                tankP1.Top = 49;
            }

            if (tankP1.Top >= 503)
            {
                tankP1.Top = 503;
            }

            try
            {
                foreach (PictureBox bala in balas)
                {
                    if (bala.Tag.ToString() == "arriba")
                        bala.Top -= 20;
                    else if (bala.Tag.ToString() == "abajo")
                        bala.Top += 20;
                    else if (bala.Tag.ToString() == "izq")
                        bala.Left -= 20;
                    else if (bala.Tag.ToString() == "derecha")
                        bala.Left += 20;

                    //int t_bala_actual = 

                    // Detección de colisión
                    if (bala.Bounds.IntersectsWith(tankP2.Bounds))
                    {

                        if (vidas2 == 5)
                        {
                            P2_vidas.Image = Properties.Resources._4vidas;
                            vidas2 = 4;
                        }

                        else if (vidas2 == 4)
                        {
                            P2_vidas.Image = Properties.Resources._3vidas;
                            vidas2 = 3;
                        }

                        else if (vidas2 == 3)
                        {
                            P2_vidas.Image = Properties.Resources._2vidas;
                            vidas2 = 2;
                        }

                        else if (vidas2 == 2)
                        {
                            P2_vidas.Image = Properties.Resources._1vida;
                            vidas2 = 1;
                        }

                        else if (vidas2 == 1)
                        {
                            P2_vidas.Image = Properties.Resources._0vidas;
                            vidas2= 0;

                            this.Controls.Remove(tankP2);
                            tankP2.Dispose();

                        }

                        this.Controls.Remove(bala);
                        balas.Remove(bala);
                        bala.Dispose();
                        num_balas--;
                    }

                    if (bala.Bounds.IntersectsWith(tankP3.Bounds))
                    {
                        if (vidas3 == 5)
                        {
                            P3_vidas.Image = Properties.Resources._4vidas;
                            vidas3 = 4;
                        }

                        else if (vidas3 == 4)
                        {
                            P3_vidas.Image = Properties.Resources._3vidas;
                            vidas3 = 3;
                        }

                        else if (vidas3 == 3)
                        {
                            P3_vidas.Image = Properties.Resources._2vidas;
                            vidas3 = 2;
                        }

                        else if (vidas3 == 2)
                        {
                            P3_vidas.Image = Properties.Resources._1vida;
                            vidas3 = 1;
                        }

                        else if (vidas3 == 1)
                        {
                            P3_vidas.Image = Properties.Resources._0vidas;
                            vidas3 = 0;
                            this.Controls.Remove(tankP3);
                            tankP3.Dispose();
                        }

                        this.Controls.Remove(bala);
                        balas.Remove(bala);
                        bala.Dispose();
                        num_balas--;
                    }

                    if (bala.Top >= 539)
                    {
                        this.Controls.Remove(bala);
                        balas.Remove(bala);
                        bala.Dispose();
                        num_balas--;
                    }

                    if (bala.Left <= 12)
                    {
                        this.Controls.Remove(bala);
                        balas.Remove(bala);
                        bala.Dispose();
                        num_balas--;
                    }

                    if (bala.Left >= 662)
                    {
                        this.Controls.Remove(bala);
                        balas.Remove(bala);
                        bala.Dispose();
                        num_balas--;
                    }

                    if (bala.Top <= 33)
                    {
                        this.Controls.Remove(bala);
                        balas.Remove(bala);
                        bala.Dispose();
                        num_balas--;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void nueva_partida() //crea un nuevo juego con los componentes necesarios
        {
            tankP1.Left = 28;
            tankP1.Top = 49;
            tankP1.Visible = true;
            tankP1.Size = new Size(30, 45);
            tankP1.Image = Properties.Resources.blue_tank1;
            P1_vidas.Visible = true;
            vidas1 = 5;
            P1_vidas.Image = Properties.Resources._5vidas;

            tankP2.Left = 28;
            tankP2.Top = 488;
            tankP2.Visible = true;
            P2_vidas.Visible = true;
            tankP2.Size = new Size(30, 45);
            tankP2.Image = Properties.Resources.pink_tank;
            vidas2 = 5;
            P2_vidas.Image = Properties.Resources._5vidas;

            tankP3.Left = 626;
            tankP3.Top = 49;
            tankP3.Visible = true;
            tankP3.Size = new Size(30, 45);
            tankP3.Image = Properties.Resources.grey_tank;
            P3_vidas.Visible = true;
            vidas3 = 5;
            P3_vidas.Image = Properties.Resources._5vidas;

            v_jugador = 8;

            timer1.Start();
        }

    }
}
