namespace Client
{
    partial class Principal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.consultasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horaYFechaDeUnaPartidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuántasPartidasGanéElDiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ShowConectados = new System.Windows.Forms.DataGridView();
            this.Desconectar = new System.Windows.Forms.Button();
            this.servicios_rec = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.invitar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShowConectados)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consultasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(811, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // consultasToolStripMenuItem
            // 
            this.consultasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem,
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem,
            this.horaYFechaDeUnaPartidaToolStripMenuItem,
            this.cuántasPartidasGanéElDiaToolStripMenuItem});
            this.consultasToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consultasToolStripMenuItem.Name = "consultasToolStripMenuItem";
            this.consultasToolStripMenuItem.Size = new System.Drawing.Size(119, 23);
            this.consultasToolStripMenuItem.Text = "CONSULTAS";
            // 
            // cuantasPartidasHeGanadoEnTotalToolStripMenuItem
            // 
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem.Name = "cuantasPartidasHeGanadoEnTotalToolStripMenuItem";
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem.Size = new System.Drawing.Size(413, 26);
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem.Text = "¿Cuántas partidas ha ganado el jugador...";
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem.Click += new System.EventHandler(this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem_Click);
            // 
            // quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem
            // 
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem.Name = "quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem";
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem.Size = new System.Drawing.Size(413, 26);
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem.Text = "¿Quién ha ganado una partida de más de 10min?";
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem.Click += new System.EventHandler(this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem_Click);
            // 
            // horaYFechaDeUnaPartidaToolStripMenuItem
            // 
            this.horaYFechaDeUnaPartidaToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.horaYFechaDeUnaPartidaToolStripMenuItem.Name = "horaYFechaDeUnaPartidaToolStripMenuItem";
            this.horaYFechaDeUnaPartidaToolStripMenuItem.Size = new System.Drawing.Size(413, 26);
            this.horaYFechaDeUnaPartidaToolStripMenuItem.Text = "Hora y fecha de una partida";
            this.horaYFechaDeUnaPartidaToolStripMenuItem.Click += new System.EventHandler(this.horaYFechaDeUnaPartidaToolStripMenuItem_Click);
            // 
            // cuántasPartidasGanéElDiaToolStripMenuItem
            // 
            this.cuántasPartidasGanéElDiaToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cuántasPartidasGanéElDiaToolStripMenuItem.Name = "cuántasPartidasGanéElDiaToolStripMenuItem";
            this.cuántasPartidasGanéElDiaToolStripMenuItem.Size = new System.Drawing.Size(413, 26);
            this.cuántasPartidasGanéElDiaToolStripMenuItem.Text = "¿Cuántas partidas gané el día...";
            this.cuántasPartidasGanéElDiaToolStripMenuItem.Click += new System.EventHandler(this.cuántasPartidasGanéElDiaToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 382);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Usuario: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(408, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "*Haz click en el menú superior para hacer una consulta.";
            // 
            // ShowConectados
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.RosyBrown;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.ShowConectados.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ShowConectados.BackgroundColor = System.Drawing.Color.RosyBrown;
            this.ShowConectados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ShowConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ShowConectados.Location = new System.Drawing.Point(23, 65);
            this.ShowConectados.Margin = new System.Windows.Forms.Padding(4);
            this.ShowConectados.Name = "ShowConectados";
            this.ShowConectados.RowHeadersWidth = 51;
            this.ShowConectados.Size = new System.Drawing.Size(149, 185);
            this.ShowConectados.TabIndex = 4;
            this.ShowConectados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ShowConectados_CellContentClick);
            // 
            // Desconectar
            // 
            this.Desconectar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Desconectar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Desconectar.FlatAppearance.BorderSize = 2;
            this.Desconectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Desconectar.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Desconectar.Location = new System.Drawing.Point(16, 404);
            this.Desconectar.Margin = new System.Windows.Forms.Padding(4);
            this.Desconectar.Name = "Desconectar";
            this.Desconectar.Size = new System.Drawing.Size(141, 33);
            this.Desconectar.TabIndex = 5;
            this.Desconectar.Text = "Desconectar";
            this.Desconectar.UseVisualStyleBackColor = false;
            this.Desconectar.Click += new System.EventHandler(this.Desconectar_Click);
            // 
            // servicios_rec
            // 
            this.servicios_rec.AutoSize = true;
            this.servicios_rec.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servicios_rec.Location = new System.Drawing.Point(41, 85);
            this.servicios_rec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.servicios_rec.Name = "servicios_rec";
            this.servicios_rec.Size = new System.Drawing.Size(0, 19);
            this.servicios_rec.TabIndex = 6;
            this.servicios_rec.TextChanged += new System.EventHandler(this.servicios_rec_TextChanged);
            this.servicios_rec.Click += new System.EventHandler(this.servicios_rec_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Wheat;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.servicios_rec);
            this.panel1.Location = new System.Drawing.Point(241, 246);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(51, 33);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.RosyBrown;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ShowConectados);
            this.panel2.Location = new System.Drawing.Point(13, 65);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(203, 265);
            this.panel2.TabIndex = 9;
            // 
            // invitar
            // 
            this.invitar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.invitar.FlatAppearance.BorderSize = 2;
            this.invitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.invitar.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.invitar.Location = new System.Drawing.Point(241, 287);
            this.invitar.Margin = new System.Windows.Forms.Padding(4);
            this.invitar.Name = "invitar";
            this.invitar.Size = new System.Drawing.Size(116, 44);
            this.invitar.TabIndex = 10;
            this.invitar.Text = "INVITAR";
            this.invitar.UseVisualStyleBackColor = false;
            this.invitar.Click += new System.EventHandler(this.invitar_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.button1.Location = new System.Drawing.Point(183, 404);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 33);
            this.button1.TabIndex = 11;
            this.button1.Text = "Modos de juego";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.button2.Location = new System.Drawing.Point(364, 404);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 33);
            this.button2.TabIndex = 12;
            this.button2.Text = "Shop";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.radioButton1.Location = new System.Drawing.Point(483, 154);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(87, 23);
            this.radioButton1.TabIndex = 13;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1 Player";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.radioButton2.Location = new System.Drawing.Point(483, 182);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(94, 23);
            this.radioButton2.TabIndex = 14;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "2 Players";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.radioButton3.Location = new System.Drawing.Point(483, 210);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(94, 23);
            this.radioButton3.TabIndex = 15;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "3 Players";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.label3.Location = new System.Drawing.Point(479, 113);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(240, 19);
            this.label3.TabIndex = 16;
            this.label3.Text = "¿Cuantos jugadores quereis jugar?";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button3.Cursor = System.Windows.Forms.Cursors.Default;
            this.button3.FlatAppearance.BorderSize = 3;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.button3.Location = new System.Drawing.Point(507, 277);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(137, 54);
            this.button3.TabIndex = 17;
            this.button3.Text = "PLAY";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(811, 458);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.invitar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Desconectar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.Principal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShowConectados)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem consultasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cuantasPartidasHeGanadoEnTotalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horaYFechaDeUnaPartidaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cuántasPartidasGanéElDiaToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView ShowConectados;
        private System.Windows.Forms.Button Desconectar;
        private System.Windows.Forms.Label servicios_rec;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button invitar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
    }
}