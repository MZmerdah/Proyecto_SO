using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Client
{
    public partial class CambiarContraseña : Form
    {
        string miUsuario;
        Socket server;


        public CambiarContraseña()
        {
            InitializeComponent();
        }

        public void setUsuario(string usuario)
        {
            miUsuario = usuario;
        }
        public void SetSocket(Socket socket)
        {
            server = socket;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (muestranuevacontra.Checked == true)
            {
                nuevacontra.PasswordChar = '\0';
            }
            else
            {
                nuevacontra.PasswordChar = '*';
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void EnviarDatos(string respuesta, Socket socket)
        {
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(respuesta.ToUpper());
            socket.Send(respuestaBytes);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usuario.Text))
            {
                MessageBox.Show("Escribe tu nombre de usuario");
                return;
            }
            else if (string.IsNullOrEmpty(contractual.Text))
            {
                MessageBox.Show("Escribe la contraseña actual");
                return;
            }
            else if (string.IsNullOrEmpty(nuevacontra.Text))
            {
                MessageBox.Show("Escribe la nueva contraseña");
                return;
            }
            else if (string.IsNullOrEmpty(repitecontra.Text))
            {
                MessageBox.Show("Escribe de nuevo la nueva contraseña");
                return;
            }

            if (string.IsNullOrEmpty(usuario.Text) && string.IsNullOrEmpty(contractual.Text) && string.IsNullOrEmpty(nuevacontra.Text) && string.IsNullOrEmpty(repitecontra.Text))
            {
                MessageBox.Show("Sin datos no podemos cambiar nada...");
                return;
            }

            if (usuario.Text != miUsuario && usuario.Text != null)
            {
                MessageBox.Show("No le puedes cambiar la contraseña a otro usuario...");
                return;
            }


            string nombreusuario = usuario.Text;
            string actualcontraseña = contractual.Text;
            string contranueva = nuevacontra.Text;

            string mensaje = "720/" + nombreusuario + "/" + actualcontraseña + "/" + contranueva;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);


        }

        private void muestracontracatual_CheckedChanged(object sender, EventArgs e)
        {
            if (muestracontracatual.Checked == true)
            {
                contractual.PasswordChar = '\0';
            }
            else
            {
                contractual.PasswordChar = '*';
            }
        }

        private void muestrarepitecontra_CheckedChanged(object sender, EventArgs e)
        {
            if (muestrarepitecontra.Checked == true)
            {
                repitecontra.PasswordChar = '\0';
            }
            else
            {
                repitecontra.PasswordChar = '*';
            }
        }

        private void CANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
