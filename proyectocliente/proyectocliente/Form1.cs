using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace proyectocliente
{
    public partial class Form1 : Form
    {
        int nForm;
        Socket server;
        Thread atender;
        List<Form1> formularios = new List<Form1>();
        public Form1(int nForm, Socket server)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numform.Text = nForm.ToString();
        }
        public void TomaRespuesta1(string mensaje) //numero de partidas ganadas por 1 jugador
        {
            MessageBox.Show("El jugador ha ganado " + mensaje + " partidas.");
        }

        public void TomaRespuesta2(string mensaje) //ganadores de partidas de más de 9 mins
        {
            MessageBox.Show("Los ganadores de las partidas de más de 9 mins son: " + mensaje);
        }

        public void TomaRespuesta3(string mensaje) //hora y fecha de la partida numero 3
        {
            MessageBox.Show(mensaje);
        }
        public void TomaRespuesta4(string mensaje) //partidas ganadas por un jugador en 1 dia
        {
            MessageBox.Show("Las partidas son: " + mensaje);
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                string mensaje = "1/" + nForm + "/" + nombre.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
            else if (radioButton2.Checked)
            {
                string mensaje = "2/" + nForm + "/" + 9;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);


            }
            else if (radioButton3.Checked)
            {
                string mensaje = "3/" + nForm + "/" + 3;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }

            else
            {
                string mensaje = "4/" + nForm + "/" + nombre.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);


            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
    }
}
