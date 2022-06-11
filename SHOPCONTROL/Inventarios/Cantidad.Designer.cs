namespace SHOPCONTROL.Inventarios
{
    partial class Cantidad
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
            this.cantidadInventario = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.piezaslabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cantidadInventario
            // 
            this.cantidadInventario.Location = new System.Drawing.Point(24, 43);
            this.cantidadInventario.Name = "cantidadInventario";
            this.cantidadInventario.Size = new System.Drawing.Size(164, 20);
            this.cantidadInventario.TabIndex = 0;
            this.cantidadInventario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(70, 85);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Inventario actual:";
            // 
            // piezaslabel
            // 
            this.piezaslabel.AutoSize = true;
            this.piezaslabel.Location = new System.Drawing.Point(112, 13);
            this.piezaslabel.Name = "piezaslabel";
            this.piezaslabel.Size = new System.Drawing.Size(13, 13);
            this.piezaslabel.TabIndex = 3;
            this.piezaslabel.Text = "0";
            // 
            // Cantidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 115);
            this.Controls.Add(this.piezaslabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cantidadInventario);
            this.Name = "Cantidad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cantidad a agregar";
            this.Load += new System.EventHandler(this.Cantidad_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cantidadInventario;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label piezaslabel;
    }
}