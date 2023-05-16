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

        
        private void button1_Click(object sender, EventArgs e)
        {
            
            if(radioButton1.Checked == true)
            {
                OpcionSeleccionada = 1;
                this.Close();
                MessageBox.Show("Selecciona a 1 jugadador de la lista de conectados");
                DialogResult = DialogResult.OK;
               
            }
            else if (radioButton2.Checked == true)
            {
                OpcionSeleccionada = 2;
                this.Close();
                MessageBox.Show("Selecciona a 2 jugadador de la lista de conectados");

            }
            else if (radioButton3.Checked == true)
            {
                OpcionSeleccionada = 3;
                this.Close();
                MessageBox.Show("Selecciona a 3 jugadador de la lista de conectados");
               
            }
            else if (radioButton4.Checked == true)
            {
                //Poner que se abra un formulario para elegir el terreno de juego y luego que se abra el juego con un jugador
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            MessageBox.Show("No se iniciará ninguna partida");
        }
    }
}
