namespace Client
{
    partial class Jugadores
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._2jugadores = new System.Windows.Forms.RadioButton();
            this._3jugadores = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this._1jugador = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // _2jugadores
            // 
            this._2jugadores.AutoSize = true;
            this._2jugadores.Location = new System.Drawing.Point(75, 101);
            this._2jugadores.Name = "_2jugadores";
            this._2jugadores.Size = new System.Drawing.Size(80, 17);
            this._2jugadores.TabIndex = 0;
            this._2jugadores.TabStop = true;
            this._2jugadores.Text = "2 jugadores";
            this._2jugadores.UseVisualStyleBackColor = true;
            this._2jugadores.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // _3jugadores
            // 
            this._3jugadores.AutoSize = true;
            this._3jugadores.Location = new System.Drawing.Point(75, 143);
            this._3jugadores.Name = "_3jugadores";
            this._3jugadores.Size = new System.Drawing.Size(80, 17);
            this._3jugadores.TabIndex = 1;
            this._3jugadores.TabStop = true;
            this._3jugadores.Text = "3 jugadores";
            this._3jugadores.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Selecciona los jugadores de la partida";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "ACEPTAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Aceptar_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(144, 215);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "CANCELAR";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Rechazar_Click);
            // 
            // _1jugador
            // 
            this._1jugador.AutoSize = true;
            this._1jugador.Location = new System.Drawing.Point(75, 63);
            this._1jugador.Name = "_1jugador";
            this._1jugador.Size = new System.Drawing.Size(69, 17);
            this._1jugador.TabIndex = 6;
            this._1jugador.TabStop = true;
            this._1jugador.Text = "1 jugador";
            this._1jugador.UseVisualStyleBackColor = true;
            // 
            // Jugadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 259);
            this.Controls.Add(this._1jugador);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._3jugadores);
            this.Controls.Add(this._2jugadores);
            this.Name = "Jugadores";
            this.Text = "Jugadores";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton _2jugadores;
        private System.Windows.Forms.RadioButton _3jugadores;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton _1jugador;
    }
}