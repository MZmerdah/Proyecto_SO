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
    public partial class Jugadores : Form
    {
        public int OpcionSeleccionada { get; private set; }
        public Jugadores()
        {
            InitializeComponent();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        public int GetOpcionSeleccionada()
        {
            return this.OpcionSeleccionada;
        }
        public void SetOpcionSeleccionada(int OpcionSeleccionada)
        {
            this.OpcionSeleccionada = OpcionSeleccionada;
        }

        
        private void Aceptar_Click(object sender, EventArgs e)
        {
            if (_2jugadores.Checked == true)
            {
                OpcionSeleccionada = 1;
                this.Close();
                MessageBox.Show("Selecciona a 1 jugadador de la lista de conectados");
                DialogResult = DialogResult.OK;
                
            }
            else if (_3jugadores.Checked == true)
            {
                OpcionSeleccionada = 2;
                this.Close();
                MessageBox.Show("Selecciona a 2 jugadador de la lista de conectados");

            }
            else if (_1jugador.Checked == true)
            {
                OpcionSeleccionada = 0;
                this.Close();
            }


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Rechazar_Click(object sender, EventArgs e)
        {
            this.Close();
            MessageBox.Show("No se iniciará ninguna partida");
        }
    }
}
