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
    public partial class Invitacion : Form
    {
        string anfitrion;
        string username;
        Socket socket;

        public Invitacion()
        {
            InitializeComponent();
        }

        public void SetSocket(Socket socket)
        {
            this.socket = socket;
        }

        public void SetAnfitrion(string anfitrion, string username)
        {
            this.anfitrion = anfitrion;
            this.username = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnviarDatos($"991/Invitacion/{anfitrion}/{username}/aceptar", socket);
            this.Close();
        }

        private void EnviarDatos(string respuesta, Socket socket)
        {
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(respuesta.ToUpper());
            socket.Send(respuestaBytes);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EnviarDatos($"991/Invitacion/{anfitrion}/{username}/rechazar", socket);
            this.Close();
        }
    }
}
