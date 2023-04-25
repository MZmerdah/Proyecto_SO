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

namespace Client
{
    public partial class Invitar : Form
    {
        Socket server;
        string nombre1;
        string nombre2;
        string nombre3;
        juego jg;
        public Invitar()
        {
            InitializeComponent();
        }

        private void Invitar_Load(object sender, EventArgs e)
        {
            empezar.Enabled = false;
            label1.Text = nombre1;
            label2.Text = nombre2;
            label3.Text = nombre3;

        }
        public void setServer(Socket a)
        {
            this.server = a;
        }
        public void setNombre(string nombre1, string nombre2, string nombre3)
        {
            this.nombre1 = nombre1;
            this.nombre2 = nombre2;
            this.nombre3 = nombre3;
        }
        public void RecibirInvitacion(Socket socket)
        {
            byte[] buffer = new byte[1024];
            int bytesRecibidos = socket.Receive(buffer);
            string invitacion = Encoding.ASCII.GetString(buffer, 0, bytesRecibidos);
            MessageBox.Show(invitacion);
        }

        public void EnviarRespuesta(string respuesta, Socket socket)
        {
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(respuesta.ToUpper());
            socket.Send(respuestaBytes);
        }
        public void RecibirRespuesta(Socket socket, string label)
        {

            //recibe todas las respuestas
            //si todas son no messagebox y se cierra formulario
            //si hay almenos 1 se activa boton(siempre y cuando se haya escogido un mapa)
            //si no se escoge mapa messagebox

            byte[] buffer = new byte[1024];
            int bytesRecibidos = socket.Receive(buffer);
            string respuesta = Encoding.ASCII.GetString(buffer, 0, bytesRecibidos);

            if (respuesta.Equals("ACEPTAR"))
            {
                // Cambiar el color del label a verde
                this.Controls[label].BackColor = Color.Green;
                empezar.Enabled = true;

            }
            else if (respuesta.Equals("RECHAZAR"))
            {
                // Cambiar el color del label a rojo
                this.Controls[label].BackColor = Color.Red;
            }
            else
            {
                // Enviar un mensaje de respuesta inválido
                MessageBox.Show("Respuesta inválida recibida: " + respuesta);
            }

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            jg = new juego();
            //set server
            //set nombres juugadores
            //etc
            jg.ShowDialog();
            //abre el juego
            //cuando se abre el juego timer 3, 2, 1, YA
        }
    }
}
