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
        Thread atender;
        User user;

        int numeroConectados = 0;
        string conectados = string.Empty;

        delegate void DelegadoParaEscribir(string mensaje);

        delegate void MostrarConectados(string[] vector);
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

        public void setServer(Socket a)   //Se utiliza para pasar transpasar los datos entre formularios
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

        public void setUser(string a)  
        {
            this.username = a;
        }

        public void PonContador(string contador)
        {
            servicios_rec.Text = contador;
        }
       
        public void MuestraConectados(string[] vector)
        {
            ShowConectados.RowHeadersVisible = false;
            ShowConectados.ColumnHeadersVisible = false;
            ShowConectados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            ShowConectados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ShowConectados.RowCount = vector.Length;
            ShowConectados.ColumnCount = 1;

            int i = 0;
            while (i < vector.Length)
            {

                if (i == 0)
                {
                    ShowConectados.Rows[i].Cells[0].Value = "Número de conectados: " + vector[i];
                }
                else
                {
                    ShowConectados.Rows[i].Cells[0].Value = vector[i];
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
                server.Receive(msg2);
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje = trozos[1].Split('\0')[0];

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

                        string[] vector = new string[5];
                        vector = mensaje.Split(',');

                        MostrarConectados delegadoMuestra = new MostrarConectados(MuestraConectados);
                        ShowConectados.Invoke(delegadoMuestra, new object[] { vector });
                        //Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
                        //{
                        //    ShowConectados.RowHeadersVisible = false;
                        //    ShowConectados.ColumnHeadersVisible = false;
                        //    ShowConectados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        //    ShowConectados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                        //    ShowConectados.RowCount = vector.Length;
                        //    ShowConectados.ColumnCount = 1;

                        //    int i = 0;
                        //    while (i < vector.Length)
                        //    {

                        //        if (i == 0)
                        //        {
                        //            ShowConectados.Rows[i].Cells[0].Value = "Número de conectados: " + vector[i];
                        //        }
                        //        else
                        //        {
                        //            ShowConectados.Rows[i].Cells[0].Value = vector[i];
                        //        }
                        //        i++;
                        //    }
                        //}));


                        break;

                    case 6: // Respuesta servicios realizados 

                        // servicios_rec.Text = "Número total de servicios: " + mensaje;
                        DelegadoParaEscribir delegado = new DelegadoParaEscribir(PonContador);
                        servicios_rec.Invoke(delegado, new object[] {mensaje});
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
            gd.setName(username);
            gd.ShowDialog();
        }

       

        private void Desconectar_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            estado = 1;
            string mensaje = "0/"+username;

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
    }
}
