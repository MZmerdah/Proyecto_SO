﻿using System;
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
using System.Windows;


namespace Client
{
    public partial class PartidasGanadas : Form
    {
        Socket server;
        string username;

        delegate void MostrarRespuesta(string mensaje);

        public void setrespuesta(string a)
        {
            string mensaje = string.Empty;
            if (a == "1/NoExist")
            {
                mensaje = "El jugador " + usernameconsulta.Text + " no existe.";
            }

            else
            {
                mensaje = "El jugador " + usernameconsulta.Text + " ha ganado " + a + " partidas.";
            }

            MostrarRespuesta delegadoMuestra = new MostrarRespuesta(MuestraRespuesta);
            label2.Invoke(delegadoMuestra, new object[] { mensaje });
           

       
        }

        public void MuestraRespuesta(string mensaje)
        {
            label2.Text = mensaje;
        }

        public PartidasGanadas()
        {
            InitializeComponent();
        }

        public void setServer(Socket a)   
        {
            this.server = a;
        }

        private void PartidasGanadas_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usernameconsulta.Text != "")
            {
                string mensaje = "1/" + usernameconsulta.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

               
            }
        }
    }
}
