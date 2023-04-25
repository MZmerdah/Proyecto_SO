namespace Client
{
    partial class juego
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bottomwall = new System.Windows.Forms.PictureBox();
            this.topwall = new System.Windows.Forms.PictureBox();
            this.leftwall = new System.Windows.Forms.PictureBox();
            this.rightwall = new System.Windows.Forms.PictureBox();
            this.tankP3 = new System.Windows.Forms.PictureBox();
            this.tankP2 = new System.Windows.Forms.PictureBox();
            this.tankP1 = new System.Windows.Forms.PictureBox();
            this.P3_vidas = new System.Windows.Forms.PictureBox();
            this.P2_vidas = new System.Windows.Forms.PictureBox();
            this.P1_vidas = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.bottomwall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topwall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftwall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightwall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tankP3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tankP2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tankP1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.P3_vidas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.P2_vidas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.P1_vidas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(439, 345);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "3";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "VIDAS:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(87, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Jugador 1 ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(344, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "Jugador 2";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(598, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 22);
            this.label5.TabIndex = 4;
            this.label5.Text = "Jugador 3";
            // 
            // bottomwall
            // 
            this.bottomwall.BackColor = System.Drawing.SystemColors.Desktop;
            this.bottomwall.Location = new System.Drawing.Point(16, 663);
            this.bottomwall.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bottomwall.Name = "bottomwall";
            this.bottomwall.Size = new System.Drawing.Size(880, 12);
            this.bottomwall.TabIndex = 7;
            this.bottomwall.TabStop = false;
            // 
            // topwall
            // 
            this.topwall.BackColor = System.Drawing.SystemColors.Desktop;
            this.topwall.Location = new System.Drawing.Point(16, 41);
            this.topwall.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.topwall.Name = "topwall";
            this.topwall.Size = new System.Drawing.Size(880, 12);
            this.topwall.TabIndex = 7;
            this.topwall.TabStop = false;
            // 
            // leftwall
            // 
            this.leftwall.BackColor = System.Drawing.SystemColors.Desktop;
            this.leftwall.Location = new System.Drawing.Point(16, 47);
            this.leftwall.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.leftwall.Name = "leftwall";
            this.leftwall.Size = new System.Drawing.Size(13, 629);
            this.leftwall.TabIndex = 7;
            this.leftwall.TabStop = false;
            // 
            // rightwall
            // 
            this.rightwall.BackColor = System.Drawing.SystemColors.Desktop;
            this.rightwall.Location = new System.Drawing.Point(883, 47);
            this.rightwall.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rightwall.Name = "rightwall";
            this.rightwall.Size = new System.Drawing.Size(13, 629);
            this.rightwall.TabIndex = 7;
            this.rightwall.TabStop = false;
            // 
            // tankP3
            // 
            this.tankP3.BackColor = System.Drawing.Color.Transparent;
            this.tankP3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tankP3.Enabled = false;
            this.tankP3.Image = global::Client.Properties.Resources.grey_tank;
            this.tankP3.Location = new System.Drawing.Point(348, 188);
            this.tankP3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tankP3.Name = "tankP3";
            this.tankP3.Size = new System.Drawing.Size(40, 55);
            this.tankP3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.tankP3.TabIndex = 6;
            this.tankP3.TabStop = false;
            this.tankP3.Visible = false;
            this.tankP3.Click += new System.EventHandler(this.tankP3_Click);
            // 
            // tankP2
            // 
            this.tankP2.BackColor = System.Drawing.Color.Transparent;
            this.tankP2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tankP2.Enabled = false;
            this.tankP2.Image = global::Client.Properties.Resources.pink_tank;
            this.tankP2.Location = new System.Drawing.Point(37, 601);
            this.tankP2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tankP2.Name = "tankP2";
            this.tankP2.Size = new System.Drawing.Size(40, 55);
            this.tankP2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.tankP2.TabIndex = 6;
            this.tankP2.TabStop = false;
            this.tankP2.Visible = false;
            // 
            // tankP1
            // 
            this.tankP1.BackColor = System.Drawing.Color.Transparent;
            this.tankP1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tankP1.Enabled = false;
            this.tankP1.Image = global::Client.Properties.Resources.blue_right;
            this.tankP1.Location = new System.Drawing.Point(107, 619);
            this.tankP1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tankP1.Name = "tankP1";
            this.tankP1.Size = new System.Drawing.Size(60, 37);
            this.tankP1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.tankP1.TabIndex = 6;
            this.tankP1.TabStop = false;
            this.tankP1.Visible = false;
            // 
            // P3_vidas
            // 
            this.P3_vidas.BackColor = System.Drawing.Color.Transparent;
            this.P3_vidas.Image = global::Client.Properties.Resources._5vidas;
            this.P3_vidas.Location = new System.Drawing.Point(699, -7);
            this.P3_vidas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.P3_vidas.Name = "P3_vidas";
            this.P3_vidas.Size = new System.Drawing.Size(143, 60);
            this.P3_vidas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.P3_vidas.TabIndex = 8;
            this.P3_vidas.TabStop = false;
            this.P3_vidas.Visible = false;
            // 
            // P2_vidas
            // 
            this.P2_vidas.BackColor = System.Drawing.Color.Transparent;
            this.P2_vidas.Image = global::Client.Properties.Resources._5vidas;
            this.P2_vidas.Location = new System.Drawing.Point(442, -7);
            this.P2_vidas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.P2_vidas.Name = "P2_vidas";
            this.P2_vidas.Size = new System.Drawing.Size(143, 60);
            this.P2_vidas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.P2_vidas.TabIndex = 8;
            this.P2_vidas.TabStop = false;
            this.P2_vidas.Visible = false;
            this.P2_vidas.Click += new System.EventHandler(this.P2_vidas_Click);
            // 
            // P1_vidas
            // 
            this.P1_vidas.BackColor = System.Drawing.Color.Transparent;
            this.P1_vidas.Image = global::Client.Properties.Resources._5vidas;
            this.P1_vidas.Location = new System.Drawing.Point(193, -7);
            this.P1_vidas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.P1_vidas.Name = "P1_vidas";
            this.P1_vidas.Size = new System.Drawing.Size(143, 60);
            this.P1_vidas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.P1_vidas.TabIndex = 8;
            this.P1_vidas.TabStop = false;
            this.P1_vidas.Visible = false;
            // 
            // juego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(909, 681);
            this.Controls.Add(this.bottomwall);
            this.Controls.Add(this.topwall);
            this.Controls.Add(this.leftwall);
            this.Controls.Add(this.rightwall);
            this.Controls.Add(this.tankP3);
            this.Controls.Add(this.tankP2);
            this.Controls.Add(this.tankP1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.P3_vidas);
            this.Controls.Add(this.P2_vidas);
            this.Controls.Add(this.P1_vidas);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(927, 728);
            this.MinimumSize = new System.Drawing.Size(927, 728);
            this.Name = "juego";
            this.Text = "Battle Tank";
            this.Load += new System.EventHandler(this.juego_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.P1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.P1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.bottomwall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topwall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftwall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightwall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tankP3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tankP2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tankP1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.P3_vidas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.P2_vidas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.P1_vidas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox tankP1;
        private System.Windows.Forms.PictureBox tankP2;
        private System.Windows.Forms.PictureBox tankP3;
        private System.Windows.Forms.PictureBox rightwall;
        private System.Windows.Forms.PictureBox leftwall;
        private System.Windows.Forms.PictureBox topwall;
        private System.Windows.Forms.PictureBox bottomwall;
        private System.Windows.Forms.PictureBox P1_vidas;
        private System.Windows.Forms.PictureBox P2_vidas;
        private System.Windows.Forms.PictureBox P3_vidas;
    }
}