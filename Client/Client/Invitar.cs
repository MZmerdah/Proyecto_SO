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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Client
{
    public partial class Invitar : Form
    {
        Socket server;
        string nombre2;
        string nombre3;
        string miUsuario;
        public Invitar()
        {
            InitializeComponent();
        }
        private void Invitar_Load(object sender, EventArgs e)
        {
            empezar.Enabled = false;
            label1.Visible = true;
        }
        public void setServer(Socket a)
        {
            this.server = a;
        }
        public void setNombre(string nombre2, string nombre3)
        {
            this.nombre2 = nombre2;
            this.nombre3 = nombre3;
            label2.Text = nombre2.Replace("\n", string.Empty); ;
            label3.Text = nombre3.Replace("\n", string.Empty); ;
        }

        public void setUsuario(string usuario)
        {
            miUsuario = usuario;
        }

        public void Invitacion(string respuesta, Socket socket)
        {
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(respuesta.ToUpper());
            socket.Send(respuestaBytes);
        }
        //public void RecibirRespuesta(Socket socket, string label)
        //{
        //    byte[] buffer = new byte[1024];
        //    int bytesRecibidos = socket.Receive(buffer);
        //    string respuesta = Encoding.ASCII.GetString(buffer, 0, bytesRecibidos);

        //    if (respuesta.Equals("ACEPTAR"))
        //    {
        //        // Cambiar el color del label a verde
        //        this.Controls[label].BackColor = Color.Green;
        //        empezar.Enabled = true;

        //    }
        //    else if (respuesta.Equals("RECHAZAR"))
        //    {
        //        // Cambiar el color del label a rojo
        //        this.Controls[label].BackColor = Color.Red;
        //    }
        //    else
        //    {
        //        // Enviar un mensaje de respuesta inválido
        //        MessageBox.Show("Respuesta inválida recibida: " + respuesta);
        //    }

        //}

        private void PartidaNueva_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        private void Invitar_Shown(object sender, EventArgs e)
        {
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

            bool compruebaInv2 = !string.IsNullOrEmpty(label2.Text);
            bool compruebaInv3 = !string.IsNullOrEmpty(label3.Text);

            if (label2.BackColor == Color.Red && label3.BackColor == Color.Green)
            {
                MessageBox.Show("Uno o más invitados han rechazado la invitación.");
                this.Close();
            }
            else if (label2.BackColor == Color.Red && label3.BackColor == Color.Red)
            {
                MessageBox.Show("Uno o más invitados han rechazado la invitación.");
                this.Close();
            }
            else if (label2.BackColor == Color.Green && label3.BackColor == Color.Red)
            {
                MessageBox.Show("Uno o más invitados han rechazado la invitación.");
                this.Close();
            }
            else if(!compruebaInv3 && label2.BackColor == Color.Red)
            {
                MessageBox.Show("Uno o más invitados han rechazado la invitación.");
                this.Close();
            }
            else if(compruebaInv3 && label3.BackColor == Color.Green &&
                compruebaInv2 && label2.BackColor == Color.Green)
            {
                empezar.Enabled = true;
            }
            else if(compruebaInv2 && label2.BackColor == Color.Green && !compruebaInv3)
            {
                empezar.Enabled = true;
            }
        }

        private void EnviarDatos(string respuesta, Socket socket)
        {
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(respuesta);
            socket.Send(respuestaBytes);
        }
    }
}
