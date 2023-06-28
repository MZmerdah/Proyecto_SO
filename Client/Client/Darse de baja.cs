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
    public partial class Darse_de_baja : Form
    {
        Socket server;
        string miUsuario;
        public Darse_de_baja()
        {
            InitializeComponent();
        }
        public void SetSocket(Socket socket)
        {
            server = socket;
        }

        public void setUsuario(string usuario)
        {
            miUsuario = usuario;
        }
        private void EnviarDatos(string respuesta, Socket socket)
        {
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(respuesta.ToUpper());
            socket.Send(respuestaBytes);
        }
        private void BAJA_Click(object sender, EventArgs e)
        {
            if (nombre.Text == miUsuario || nombre.Text == null )
            {

                string usuario = nombre.Text;
                string contraseña = contra.Text;
                string mensaje = "888/" + usuario + "/" + contraseña;

                if (string.IsNullOrEmpty(nombre.Text))
                {
                    if (string.IsNullOrEmpty(contra.Text))
                    {
                        MessageBox.Show("Rellena los datos");

                    }
                    else
                    {
                        MessageBox.Show("No sabia que había un sin-nombre entre nosotros");

                    }

                    return;
                }
                else if (string.IsNullOrEmpty(contra.Text))
                {
                    if (string.IsNullOrEmpty(nombre.Text))
                    {
                        MessageBox.Show("Rellena los datos");
                    }
                    else
                    {
                        MessageBox.Show("Escribe la contraseña");
                    }

                    return;
                }

                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Close();
            }
            else if(nombre.Text != miUsuario && nombre.Text != null)
            {
                MessageBox.Show("Solo te puedes eliminar a ti...");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
