namespace TextoAVozCSharp
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnHablar = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.cbVoces = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbVolumen = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRate = new System.Windows.Forms.TrackBar();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolumen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRate)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnHablar
            // 
            this.BtnHablar.Location = new System.Drawing.Point(540, 39);
            this.BtnHablar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnHablar.Name = "BtnHablar";
            this.BtnHablar.Size = new System.Drawing.Size(100, 28);
            this.BtnHablar.TabIndex = 9;
            this.BtnHablar.Text = "Hablar";
            this.BtnHablar.UseVisualStyleBackColor = true;
            this.BtnHablar.Click += new System.EventHandler(this.BtnHablar_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label2.Location = new System.Drawing.Point(305, 22);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(128, 17);
            this.Label2.TabIndex = 8;
            this.Label2.Text = "Texto a reproducir:";
            // 
            // txtTexto
            // 
            this.txtTexto.Location = new System.Drawing.Point(247, 42);
            this.txtTexto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(284, 22);
            this.txtTexto.TabIndex = 7;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label1.Location = new System.Drawing.Point(57, 22);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(114, 17);
            this.Label1.TabIndex = 6;
            this.Label1.Text = "Seleccionar Voz:";
            // 
            // cbVoces
            // 
            this.cbVoces.FormattingEnabled = true;
            this.cbVoces.Location = new System.Drawing.Point(19, 42);
            this.cbVoces.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbVoces.Name = "cbVoces";
            this.cbVoces.Size = new System.Drawing.Size(201, 24);
            this.cbVoces.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbVolumen);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbRate);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(19, 95);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(621, 185);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuracion";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 102);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Volumen";
            // 
            // tbVolumen
            // 
            this.tbVolumen.Location = new System.Drawing.Point(64, 102);
            this.tbVolumen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbVolumen.Maximum = 100;
            this.tbVolumen.Name = "tbVolumen";
            this.tbVolumen.Size = new System.Drawing.Size(351, 56);
            this.tbVolumen.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Rate";
            // 
            // tbRate
            // 
            this.tbRate.Location = new System.Drawing.Point(64, 39);
            this.tbRate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbRate.Minimum = -10;
            this.tbRate.Name = "tbRate";
            this.tbRate.Size = new System.Drawing.Size(351, 56);
            this.tbRate.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(796, 424);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnHablar);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtTexto);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.cbVoces);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Texto a Vox en C#";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolumen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button BtnHablar;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtTexto;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ComboBox cbVoces;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar tbVolumen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tbRate;
    }
}

