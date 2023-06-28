namespace Client
{
    partial class Darse_de_baja
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.contra = new System.Windows.Forms.TextBox();
            this.BAJA = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Contraseña";
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(191, 75);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(100, 22);
            this.nombre.TabIndex = 2;
            this.nombre.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // contra
            // 
            this.contra.Location = new System.Drawing.Point(191, 142);
            this.contra.Name = "contra";
            this.contra.Size = new System.Drawing.Size(100, 22);
            this.contra.TabIndex = 3;
            // 
            // BAJA
            // 
            this.BAJA.Location = new System.Drawing.Point(127, 201);
            this.BAJA.Name = "BAJA";
            this.BAJA.Size = new System.Drawing.Size(91, 23);
            this.BAJA.TabIndex = 5;
            this.BAJA.Text = "ACEPTAR";
            this.BAJA.UseVisualStyleBackColor = true;
            this.BAJA.Click += new System.EventHandler(this.BAJA_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(91, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(226, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Rellena los datos para darte de baja";
            // 
            // Darse_de_baja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 253);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BAJA);
            this.Controls.Add(this.contra);
            this.Controls.Add(this.nombre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Darse_de_baja";
            this.Text = "Darse_de_baja";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.TextBox contra;
        private System.Windows.Forms.Button BAJA;
        private System.Windows.Forms.Label label4;
    }
}