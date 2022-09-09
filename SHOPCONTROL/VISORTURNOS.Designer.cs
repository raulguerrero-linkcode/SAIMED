namespace SHOPCONTROL
{
    partial class VISORTURNOS
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbVolumen = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRate = new System.Windows.Forms.TrackBar();
            this.Label1 = new System.Windows.Forms.Label();
            this.cbVoces = new System.Windows.Forms.ComboBox();
            this.Lv = new System.Windows.Forms.ListView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolumen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRate)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Lv);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1455, 894);
            this.panel1.TabIndex = 0;
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
            this.groupBox1.Location = new System.Drawing.Point(403, 327);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(621, 185);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuracion";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 136);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Volumen";
            // 
            // tbVolumen
            // 
            this.tbVolumen.Location = new System.Drawing.Point(65, 136);
            this.tbVolumen.Margin = new System.Windows.Forms.Padding(4);
            this.tbVolumen.Maximum = 100;
            this.tbVolumen.Name = "tbVolumen";
            this.tbVolumen.Size = new System.Drawing.Size(351, 45);
            this.tbVolumen.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Rate";
            // 
            // tbRate
            // 
            this.tbRate.Location = new System.Drawing.Point(65, 73);
            this.tbRate.Margin = new System.Windows.Forms.Padding(4);
            this.tbRate.Minimum = -10;
            this.tbRate.Name = "tbRate";
            this.tbRate.Size = new System.Drawing.Size(351, 45);
            this.tbRate.TabIndex = 0;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label1.Location = new System.Drawing.Point(51, 16);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(87, 13);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "Seleccionar Voz:";
            // 
            // cbVoces
            // 
            this.cbVoces.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbVoces.FormattingEnabled = true;
            this.cbVoces.Location = new System.Drawing.Point(13, 37);
            this.cbVoces.Margin = new System.Windows.Forms.Padding(4);
            this.cbVoces.Name = "cbVoces";
            this.cbVoces.Size = new System.Drawing.Size(201, 21);
            this.cbVoces.TabIndex = 11;
            this.cbVoces.SelectedIndexChanged += new System.EventHandler(this.cbVoces_SelectedIndexChanged);
            // 
            // Lv
            // 
            this.Lv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(204)))), ((int)(((byte)(250)))));
            this.Lv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Lv.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lv.FullRowSelect = true;
            this.Lv.GridLines = true;
            this.Lv.HideSelection = false;
            this.Lv.Location = new System.Drawing.Point(3, 4);
            this.Lv.Margin = new System.Windows.Forms.Padding(2);
            this.Lv.Name = "Lv";
            this.Lv.Size = new System.Drawing.Size(1450, 888);
            this.Lv.TabIndex = 5;
            this.Lv.UseCompatibleStateImageBehavior = false;
            this.Lv.View = System.Windows.Forms.View.Details;
            this.Lv.SelectedIndexChanged += new System.EventHandler(this.Lv_SelectedIndexChanged);
            this.Lv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Lv_MouseDoubleClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // VISORTURNOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1455, 894);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VISORTURNOS";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VISORTURNOS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.VISORTURNOS_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VISORTURNOS_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VISORTURNOS_KeyPress);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolumen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView Lv;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar tbVolumen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tbRate;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ComboBox cbVoces;
    }
}