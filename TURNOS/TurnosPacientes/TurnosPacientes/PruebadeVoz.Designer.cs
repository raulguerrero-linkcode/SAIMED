namespace TurnosPacientes
{
    partial class PruebadeVoz
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbVolumen = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRate = new System.Windows.Forms.TrackBar();
            this.BtnHablar = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.cbVoces = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolumen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRate)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbVolumen);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbRate);
            this.groupBox1.Controls.Add(this.Label1);
            this.groupBox1.Controls.Add(this.cbVoces);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox1.Location = new System.Drawing.Point(17, 117);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox1.Size = new System.Drawing.Size(828, 228);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuracion";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 168);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Volumen";
            // 
            // tbVolumen
            // 
            this.tbVolumen.Location = new System.Drawing.Point(87, 168);
            this.tbVolumen.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbVolumen.Maximum = 100;
            this.tbVolumen.Name = "tbVolumen";
            this.tbVolumen.Size = new System.Drawing.Size(468, 56);
            this.tbVolumen.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 90);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Rate";
            // 
            // tbRate
            // 
            this.tbRate.Location = new System.Drawing.Point(87, 90);
            this.tbRate.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbRate.Minimum = -10;
            this.tbRate.Name = "tbRate";
            this.tbRate.Size = new System.Drawing.Size(468, 56);
            this.tbRate.TabIndex = 0;
            // 
            // BtnHablar
            // 
            this.BtnHablar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnHablar.Location = new System.Drawing.Point(712, 48);
            this.BtnHablar.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnHablar.Name = "BtnHablar";
            this.BtnHablar.Size = new System.Drawing.Size(133, 34);
            this.BtnHablar.TabIndex = 15;
            this.BtnHablar.Text = "Hablar";
            this.BtnHablar.UseVisualStyleBackColor = true;
            this.BtnHablar.Click += new System.EventHandler(this.BtnHablar_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label2.Location = new System.Drawing.Point(399, 27);
            this.Label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(128, 17);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "Texto a reproducir:";
            // 
            // txtTexto
            // 
            this.txtTexto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTexto.Location = new System.Drawing.Point(321, 52);
            this.txtTexto.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(377, 22);
            this.txtTexto.TabIndex = 13;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label1.Location = new System.Drawing.Point(68, 20);
            this.Label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(114, 17);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "Seleccionar Voz:";
            // 
            // cbVoces
            // 
            this.cbVoces.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbVoces.FormattingEnabled = true;
            this.cbVoces.Location = new System.Drawing.Point(17, 45);
            this.cbVoces.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbVoces.Name = "cbVoces";
            this.cbVoces.Size = new System.Drawing.Size(267, 24);
            this.cbVoces.TabIndex = 11;
            // 
            // PruebadeVoz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 350);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnHablar);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtTexto);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PruebadeVoz";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracion de Voz";
            this.Load += new System.EventHandler(this.PruebadeVoz_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolumen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar tbVolumen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tbRate;
        internal System.Windows.Forms.Button BtnHablar;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtTexto;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ComboBox cbVoces;
    }
}