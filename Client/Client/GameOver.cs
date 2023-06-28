using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class GameOver : Form
    {
        List<Image> imagenvidas = new List<Image>
        {
            Properties.Resources._0vidas,
            Properties.Resources._1vida,
            Properties.Resources._2vidas,
            Properties.Resources._3vidas,
            Properties.Resources._4vidas,
            Properties.Resources._5vidas,
        };
        List<Image> tanque = new List<Image>
        {
            Properties.Resources.blue_tank1,
            Properties.Resources.pink_tank,
            Properties.Resources.grey_tank,
        };
        int jug;
        int vida;
        string nombre;
        public GameOver()
        {
            InitializeComponent();
        }

        private void GameOver_Load(object sender, EventArgs e)
        {
            tanquee.Image = tanque[jug];
            tanquee.Visible = true;
            P2_vidas.Image = imagenvidas[vida];
            label5.Text = nombre;
            P2_vidas.Visible = true;
        }

        public void Ganador(int a, int jugador, int vidas, string nom)//a=0 -> ganador a=-1 -> perdedor
        {
            if (a == 0)
                label1.Text = "¡Felicidades! ¡Has ganado!";
            else
                label1.Text = "Vaya...más suerte a la próxima...";
            jug = jugador;
            vida = vidas;
            nombre = nom;
        }

        private void Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
