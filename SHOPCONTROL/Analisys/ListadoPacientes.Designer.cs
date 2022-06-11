namespace SHOPCONTROL.Analisys
{
    partial class ListadoPacientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListadoPacientes));
            this.Lv = new System.Windows.Forms.ListView();
            this.button6 = new System.Windows.Forms.Button();
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
            this.Lv.Size = new System.Drawing.Size(1284, 607);
            this.Lv.TabIndex = 143;
            this.Lv.UseCompatibleStateImageBehavior = false;
            this.Lv.View = System.Windows.Forms.View.Details;
            this.Lv.SelectedIndexChanged += new System.EventHandler(this.Lv_SelectedIndexChanged);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(12, 21);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(84, 46);
            this.button6.TabIndex = 197;
            this.button6.Text = "&Exportar";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // ListadoPacientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 699);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.Lv);
            this.Name = "ListadoPacientes";
            this.Text = "ListadoPacientes";
            this.Load += new System.EventHandler(this.ListadoPacientes_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView Lv;
        private System.Windows.Forms.Button button6;
    }
}