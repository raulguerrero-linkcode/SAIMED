namespace SHOPCONTROL.Analisys
{
    partial class NotificacionClientesSMS
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
            this.Lv = new System.Windows.Forms.ListView();
            this.fechaini = new System.Windows.Forms.Label();
            this.fechafin = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.notificationStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lv
            // 
            this.Lv.BackColor = System.Drawing.Color.White;
            this.Lv.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lv.FullRowSelect = true;
            this.Lv.GridLines = true;
            this.Lv.HideSelection = false;
            this.Lv.Location = new System.Drawing.Point(4, 82);
            this.Lv.Name = "Lv";
            this.Lv.Size = new System.Drawing.Size(1347, 545);
            this.Lv.TabIndex = 4;
            this.Lv.UseCompatibleStateImageBehavior = false;
            this.Lv.View = System.Windows.Forms.View.Details;
            this.Lv.SelectedIndexChanged += new System.EventHandler(this.Lv_SelectedIndexChanged);
            // 
            // fechaini
            // 
            this.fechaini.AutoSize = true;
            this.fechaini.Location = new System.Drawing.Point(13, 13);
            this.fechaini.Name = "fechaini";
            this.fechaini.Size = new System.Drawing.Size(35, 13);
            this.fechaini.TabIndex = 5;
            this.fechaini.Text = "label1";
            this.fechaini.Click += new System.EventHandler(this.label1_Click);
            // 
            // fechafin
            // 
            this.fechafin.AutoSize = true;
            this.fechafin.Location = new System.Drawing.Point(12, 45);
            this.fechafin.Name = "fechafin";
            this.fechafin.Size = new System.Drawing.Size(35, 13);
            this.fechafin.TabIndex = 6;
            this.fechafin.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(299, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(605, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Seleccione a un usuario para notificarle vía SMS acerca de su próxima cita";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1207, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 67);
            this.button1.TabIndex = 8;
            this.button1.Text = "Enviar notificaciones a todos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // notificationStatus
            // 
            this.notificationStatus.AutoSize = true;
            this.notificationStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notificationStatus.Location = new System.Drawing.Point(299, 45);
            this.notificationStatus.Name = "notificationStatus";
            this.notificationStatus.Size = new System.Drawing.Size(0, 20);
            this.notificationStatus.TabIndex = 9;
            // 
            // NotificacionClientesSMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1363, 639);
            this.Controls.Add(this.notificationStatus);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fechafin);
            this.Controls.Add(this.fechaini);
            this.Controls.Add(this.Lv);
            this.Name = "NotificacionClientesSMS";
            this.Text = "Próximas citas";
            this.Load += new System.EventHandler(this.PendientesPago_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView Lv;
        private System.Windows.Forms.Label fechaini;
        private System.Windows.Forms.Label fechafin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label notificationStatus;
    }
}