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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificacionClientesSMS));
            this.Lv = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.notificationStatus = new System.Windows.Forms.Label();
            this.BuscarPaciente = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lv
            // 
            this.Lv.BackColor = System.Drawing.Color.White;
            this.Lv.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lv.FullRowSelect = true;
            this.Lv.GridLines = true;
            this.Lv.HideSelection = false;
            this.Lv.Location = new System.Drawing.Point(12, 82);
            this.Lv.Name = "Lv";
            this.Lv.Size = new System.Drawing.Size(1339, 545);
            this.Lv.TabIndex = 4;
            this.Lv.UseCompatibleStateImageBehavior = false;
            this.Lv.View = System.Windows.Forms.View.Details;
            this.Lv.SelectedIndexChanged += new System.EventHandler(this.Lv_SelectedIndexChanged_1);
            this.Lv.DoubleClick += new System.EventHandler(this.Lv_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(273, 9);
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
            // BuscarPaciente
            // 
            this.BuscarPaciente.Image = ((System.Drawing.Image)(resources.GetObject("BuscarPaciente.Image")));
            this.BuscarPaciente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BuscarPaciente.Location = new System.Drawing.Point(179, 30);
            this.BuscarPaciente.Name = "BuscarPaciente";
            this.BuscarPaciente.Size = new System.Drawing.Size(85, 39);
            this.BuscarPaciente.TabIndex = 148;
            this.BuscarPaciente.Text = "Buscar";
            this.BuscarPaciente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BuscarPaciente.UseVisualStyleBackColor = true;
            this.BuscarPaciente.Click += new System.EventHandler(this.BuscarPaciente_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(50, 47);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(117, 22);
            this.dateTimePicker2.TabIndex = 147;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(50, 19);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(117, 22);
            this.dateTimePicker1.TabIndex = 146;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label35.Location = new System.Drawing.Point(12, 46);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(37, 23);
            this.label35.TabIndex = 145;
            this.label35.Text = "Al";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label34.Location = new System.Drawing.Point(12, 18);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(65, 23);
            this.label34.TabIndex = 144;
            this.label34.Text = "De";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NotificacionClientesSMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1363, 639);
            this.Controls.Add(this.BuscarPaciente);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.notificationStatus);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Lv);
            this.Name = "NotificacionClientesSMS";
            this.Text = "Próximas citas";
            this.Load += new System.EventHandler(this.PendientesPago_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView Lv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label notificationStatus;
        private System.Windows.Forms.Button BuscarPaciente;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
    }
}