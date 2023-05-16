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
        string miUsuario;
        juego jg;
        public Invitar()
        {
            InitializeComponent();
        }

        private void Invitar_Load(object sender, EventArgs e)
        {
            empezar.Enabled = false;
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
            label1.Text = nombre1.Replace("\n", string.Empty);
            label2.Text = nombre2.Replace("\n", string.Empty); ;
            label3.Text = nombre3.Replace("\n", string.Empty); ;

            
        }

        public void setUsuario(string usuario)
        {
            miUsuario = usuario;
        }
        public string RecibirInvitacion(Socket socket)
        {
            byte[] buffer = new byte[1024];
            int bytesRecibidos = socket.Receive(buffer);
            string invitacion = Encoding.ASCII.GetString(buffer, 0, bytesRecibidos);
            MessageBox.Show(invitacion);

            return invitacion;
        }

        public void Invitacion(string respuesta, Socket socket)
        {
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(respuesta.ToUpper());
            socket.Send(respuestaBytes);
        }
        public void RecibirRespuesta(Socket socket, string label)
        {
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
            jg.SetSocket(server);
            jg.setUsuario(miUsuario);
            jg.SetUser(miUsuario);
            jg.setNombre(nombre1, nombre2, nombre3);
            jg.ShowDialog();
            //abre el juego
            //cuando se abre el juego timer 3, 2, 1, YA
        }

        private void Invitar_Shown(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(nombre1))
            {
                Invitacion($"990/Invitacion/{miUsuario}/{nombre1}", server);
            }

            if (!string.IsNullOrEmpty(nombre2))
            {
                Invitacion($"990/Invitacion/{miUsuario}/{nombre2}", server);
            }

            if (!string.IsNullOrEmpty(nombre3))
            {
                Invitacion($"990/Invitacion/{miUsuario}/{nombre3}", server);
            }

            empezar.Enabled = false;
        }

        public void actualizaInvitados(string invitado, string acepta)
        {
            if(label1.Text.ToUpper() == invitado)
            {
                if (acepta.ToUpper().StartsWith("ACEPTAR"))
                    label1.BackColor = Color.Green;
                else
                    label1.BackColor = Color.Red;
            }

            if (label2.Text.ToUpper() == invitado)
            {
                if (acepta.ToUpper().StartsWith("ACEPTAR"))
                    label2.BackColor = Color.Green;
                else
                    label2.BackColor = Color.Red;
            }

            if (label3.Text.ToUpper() == invitado)
            {
                if (acepta.ToUpper().StartsWith("ACEPTAR"))
                    label3.BackColor = Color.Green;
                else
                    label3.BackColor = Color.Red;
            }

            bool compruebaInv1 = !string.IsNullOrEmpty(label1.Text);
            bool compruebaInv2 = !string.IsNullOrEmpty(label2.Text);
            bool compruebaInv3 = !string.IsNullOrEmpty(label3.Text);

            if (label1.BackColor == Color.Red || label2.BackColor == Color.Red || label3.BackColor == Color.Red)
            {
                MessageBox.Show("Uno o más invitados han rechazado la invitación.");
                this.Close();
            }
            else if(compruebaInv1 && label1.BackColor == Color.Green && 
                compruebaInv2 && label2.BackColor == Color.Green && 
                compruebaInv3 && label3.BackColor == Color.Green)
            {
                empezar.Enabled = true;
            }
            else if(compruebaInv1 && label1.BackColor == Color.Green &&
                compruebaInv2 && label2.BackColor == Color.Green && !compruebaInv3)
            {
                empezar.Enabled = true;
            }
            else if(compruebaInv1 && label1.BackColor == Color.Green && !compruebaInv2 && !compruebaInv3)
            {
                empezar.Enabled = true;
            }
        }
    }
}
