namespace Client
{
    partial class CambiarContraseña
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.usuario = new System.Windows.Forms.TextBox();
            this.contractual = new System.Windows.Forms.TextBox();
            this.nuevacontra = new System.Windows.Forms.TextBox();
            this.repitecontra = new System.Windows.Forms.TextBox();
            this.muestracontracatual = new System.Windows.Forms.CheckBox();
            this.muestranuevacontra = new System.Windows.Forms.CheckBox();
            this.muestrarepitecontra = new System.Windows.Forms.CheckBox();
            this.ACEPTAR = new System.Windows.Forms.Button();
            this.CANCELAR = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(211, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rellena los datos";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre de usuario";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Contraseña actual";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nueva contraseña";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Repite la nueva contraseña";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // usuario
            // 
            this.usuario.Location = new System.Drawing.Point(239, 74);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(100, 20);
            this.usuario.TabIndex = 5;
            // 
            // contractual
            // 
            this.contractual.Location = new System.Drawing.Point(239, 113);
            this.contractual.Name = "contractual";
            this.contractual.PasswordChar = '*';
            this.contractual.Size = new System.Drawing.Size(100, 20);
            this.contractual.TabIndex = 6;
            // 
            // nuevacontra
            // 
            this.nuevacontra.Location = new System.Drawing.Point(239, 151);
            this.nuevacontra.Name = "nuevacontra";
            this.nuevacontra.PasswordChar = '*';
            this.nuevacontra.Size = new System.Drawing.Size(100, 20);
            this.nuevacontra.TabIndex = 7;
            // 
            // repitecontra
            // 
            this.repitecontra.Location = new System.Drawing.Point(239, 193);
            this.repitecontra.Name = "repitecontra";
            this.repitecontra.PasswordChar = '*';
            this.repitecontra.Size = new System.Drawing.Size(100, 20);
            this.repitecontra.TabIndex = 8;
            // 
            // muestracontracatual
            // 
            this.muestracontracatual.AutoSize = true;
            this.muestracontracatual.Location = new System.Drawing.Point(394, 116);
            this.muestracontracatual.Name = "muestracontracatual";
            this.muestracontracatual.Size = new System.Drawing.Size(151, 17);
            this.muestracontracatual.TabIndex = 9;
            this.muestracontracatual.Text = "Mostrar Contraseña Actual";
            this.muestracontracatual.UseVisualStyleBackColor = true;
            this.muestracontracatual.CheckedChanged += new System.EventHandler(this.muestracontracatual_CheckedChanged);
            // 
            // muestranuevacontra
            // 
            this.muestranuevacontra.AutoSize = true;
            this.muestranuevacontra.Location = new System.Drawing.Point(394, 154);
            this.muestranuevacontra.Name = "muestranuevacontra";
            this.muestranuevacontra.Size = new System.Drawing.Size(153, 17);
            this.muestranuevacontra.TabIndex = 10;
            this.muestranuevacontra.Text = "Mostrar Nueva Contraseña";
            this.muestranuevacontra.UseVisualStyleBackColor = true;
            this.muestranuevacontra.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // muestrarepitecontra
            // 
            this.muestrarepitecontra.AutoSize = true;
            this.muestrarepitecontra.Location = new System.Drawing.Point(394, 193);
            this.muestrarepitecontra.Name = "muestrarepitecontra";
            this.muestrarepitecontra.Size = new System.Drawing.Size(118, 17);
            this.muestrarepitecontra.TabIndex = 11;
            this.muestrarepitecontra.Text = "Mostrar Contraseña";
            this.muestrarepitecontra.UseVisualStyleBackColor = true;
            this.muestrarepitecontra.CheckedChanged += new System.EventHandler(this.muestrarepitecontra_CheckedChanged);
            // 
            // ACEPTAR
            // 
            this.ACEPTAR.Location = new System.Drawing.Point(117, 234);
            this.ACEPTAR.Name = "ACEPTAR";
            this.ACEPTAR.Size = new System.Drawing.Size(95, 32);
            this.ACEPTAR.TabIndex = 12;
            this.ACEPTAR.Text = "ACEPTAR";
            this.ACEPTAR.UseVisualStyleBackColor = true;
            this.ACEPTAR.Click += new System.EventHandler(this.button1_Click);
            // 
            // CANCELAR
            // 
            this.CANCELAR.Location = new System.Drawing.Point(330, 234);
            this.CANCELAR.Name = "CANCELAR";
            this.CANCELAR.Size = new System.Drawing.Size(89, 32);
            this.CANCELAR.TabIndex = 13;
            this.CANCELAR.Text = "CANCELAR";
            this.CANCELAR.UseVisualStyleBackColor = true;
            this.CANCELAR.Click += new System.EventHandler(this.CANCELAR_Click);
            // 
            // CambiarContraseña
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 303);
            this.Controls.Add(this.CANCELAR);
            this.Controls.Add(this.ACEPTAR);
            this.Controls.Add(this.muestrarepitecontra);
            this.Controls.Add(this.muestranuevacontra);
            this.Controls.Add(this.muestracontracatual);
            this.Controls.Add(this.repitecontra);
            this.Controls.Add(this.nuevacontra);
            this.Controls.Add(this.contractual);
            this.Controls.Add(this.usuario);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CambiarContraseña";
            this.Text = "CambiarContraseña";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox usuario;
        private System.Windows.Forms.TextBox contractual;
        private System.Windows.Forms.TextBox nuevacontra;
        private System.Windows.Forms.TextBox repitecontra;
        private System.Windows.Forms.CheckBox muestracontracatual;
        private System.Windows.Forms.CheckBox muestranuevacontra;
        private System.Windows.Forms.CheckBox muestrarepitecontra;
        private System.Windows.Forms.Button ACEPTAR;
        private System.Windows.Forms.Button CANCELAR;
    }
}