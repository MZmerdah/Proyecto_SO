using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public class Tanque : PictureBox
    {
        public int direccion; //direccion que mira el tanque: 0 = arriba, 1 = derecha, 2 = abajo, 3 = izquierda
        int vidas;
        private Dictionary<int, Bitmap> imagenes;
        public PictureBox img_tanque = new PictureBox();
        PictureBox img_vidas = new PictureBox();


        public Tanque(int x, int y, int direccion_, int vidas_, Dictionary<int, Bitmap> imagenes_, int x_vidas, int y_vidas)
        {
            direccion = direccion_;
            vidas = vidas_;
            imagenes = imagenes_;
            NuevaVida();
            NuevaDireccion();
            img_tanque.Location = new Point(x, y);
            img_tanque.Image = imagenes[direccion];
            img_tanque.SizeMode = PictureBoxSizeMode.StretchImage;
            //img_tanque.Visible = true;
            //juego form = (juego)Application.OpenForms[0];
            //form.Controls.Add(img_tanque);

            img_vidas.Image = Properties.Resources._5vidas;
            img_vidas.Size = new Size(107, 49);
            img_vidas.SizeMode = PictureBoxSizeMode.StretchImage;
            img_vidas.Location = new Point(x_vidas, y_vidas);
            img_vidas.Visible = true;
            //juego form = (juego)Application.OpenForms[0]; // Suponiendo que el formulario se llama Form1
            //form.Controls.Add(img_vidas);
        }

        public void Mover(int dx, int dy)
        {
            this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
        }
        public void Disparar()
        {

        }
        public double DistanciaTanques(Tanque tanque)
        {
            double dx = tanque.Location.X - this.Location.X;
            double dy = tanque.Location.Y - this.Location.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        public void NuevaVida()
        {
            switch (vidas)
            {
                case 5:
                    img_vidas.Image = Properties.Resources._5vidas;
                    break;
                case 4:
                    img_vidas.Image = Properties.Resources._4vidas;
                    break;
                case 3:
                    img_vidas.Image = Properties.Resources._3vidas;
                    break;
                case 2:
                    img_vidas.Image = Properties.Resources._2vidas;
                    break;
                case 1:
                    img_vidas.Image = Properties.Resources._1vida;
                    break;
                case 0:
                    img_vidas.Image = Properties.Resources._0vidas;
                    break;
            }
        }

        public void NuevaDireccion()
        {
            switch (direccion)
            {
                case 0: //arriba 
                    this.Image = imagenes[direccion];
                    this.Size = new Size(30, 45);
                    break;
                case 1: //derecha
                    this.Image = imagenes[direccion];
                    this.Size = new Size(45, 30);
                    break;
                case 2: //abajo
                    this.Image = imagenes[direccion]; 
                    this.Size = new Size(30, 45);
                    break;
                case 3: //izquierda
                    this.Image = imagenes[direccion];
                    this.Size = new Size(45, 30);
                    break;
            }
        }

        public void Disparado()
        {
            vidas--;
            NuevaVida();
        }
    }

    public class ListaTanques
    {
        int num = 0;
        Tanque[] tanques = new Tanque[100];

        public void Añadir(Tanque tanque)
        {
            this.tanques[this.num] = tanque;
            juego form = (juego)Application.OpenForms[0];
            form.Controls.Add(tanque.img_tanque);
        }

        public Tanque GetTanque(int i)
        {
            return this.tanques[i];
        }
        public void MovimientoMaquina(Tanque tanque) //hacer el movimiento del tanque que se envia como parametro
        {
            Tanque enemigo_mas_cerca = null;
            double distancia_mas_corta = double.MaxValue;
            for (int i = 0; i < this.num; i++)
            {
                double distancia = tanque.DistanciaTanques(this.tanques[i]); //encuentra al enemigo mas cercano para atacarle
                if (distancia < distancia_mas_corta)
                {
                    enemigo_mas_cerca = this.tanques[i]; 
                    distancia_mas_corta = distancia;
                }
            }
            if (enemigo_mas_cerca != null)
            {
                double dx = enemigo_mas_cerca.Location.X - tanque.Location.X;
                double dy = enemigo_mas_cerca.Location.Y - tanque.Location.Y;
                double angulo = Math.Atan2(dy, dx);
                int nuevaDireccion = (int)Math.Round(angulo * 180 / Math.PI) + 90;
                if (nuevaDireccion == 0)
                    tanque.direccion = 0;
                else if (nuevaDireccion == 90)
                    tanque.direccion = 1;
                else if (nuevaDireccion == 180)
                    tanque.direccion = 2;
                else if (nuevaDireccion == 270)
                    tanque.direccion = 3;
                int dxInt = (int)(5 * Math.Cos(angulo));
                int dyInt = (int)(5 * Math.Sin(angulo));
                tanque.Mover(dxInt, dyInt);
                if (Math.Abs(dx) < 20 && Math.Abs(dy) < 20) //cuando este a una cierta distancia del enemigo, disparara
                {
                    tanque.Disparar();
                }
            }
        }
    }
    public class Balas : PictureBox
    {
        private int direccion;
        private int velocidad;
        int num;

        public Balas(int x, int y, int direccion_)
        {
            direccion = direccion_;
            velocidad = 10;
            this.Location = new Point(x, y);
            this.Image = Properties.Resources.bala;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void Mover_Bala()
        {
            // Move the bullet in the current direction by the current speed
            int dx = 0, dy = 0;
            switch (direccion)
            {
                case 0: // Up
                    dy = velocidad;
                    break;
                case 1: // Right
                    dx = velocidad;
                    break;
                case 2: // Down
                    dy = velocidad;
                    break;
                case 3: // Left
                    dx = -velocidad;
                    break;
            }
            this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
        }

        public bool Tanque_Disparado(Tanque tanque)
        {
            // Check if the bullet collides with the given tank
            Rectangle bulletRect = new Rectangle(this.Location, this.Size);
            Rectangle tankRect = new Rectangle(tanque.Location, tanque.Size);
            return bulletRect.IntersectsWith(tankRect);
        }
    }
}






