using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;
using System.Data.SqlClient;

namespace SHOPCONTROL.HistorialClinica
{
    public partial class Editor : Form
    {
        public string NOEXPEDIENTE;
        public string NOMBREIMAGEN;
        public int CONSECUTIVO;
        byte[] IMAGEARRAY;
        public Image imagenFondo; //imagen editada
        public Image imagenNueva; //iamgen por defecto
        Color paintColor;
        bool choose = false;
        bool draw = false;
        int x, y, lx, ly = 0;

        Item currentItem;

        public Editor()
        {
            InitializeComponent();
        }
        public enum Item
        {
            Rectangulo, Elipse, Linea, Texto, Pluma, Goma
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            choose = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            choose = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (choose)
            {
                Bitmap bmp = (Bitmap)pictureBox1.Image.Clone();
                int _x = e.X;
                int _y= e.Y;
                if (_y < 0 || _y>120 || _x<0 || _x>120)
                {
                    MessageBox.Show("Seleccione un color de la paleta de colores");
                }
                else {
                    paintColor = bmp.GetPixel(_x, _y);
                    pictureBox2.BackColor = paintColor;
                }
            }

        }

        private void Editor_Load(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            pictureBox3.Image = imagenFondo;
            List<PictureBox> pictureList = new List<PictureBox>();
            pictureList.Add(negro);
            pictureList.Add(blanco);
            pictureList.Add(rojo);
            pictureList.Add(azul);
            pictureList.Add(verde);
            pictureList.Add(amarillo);

            foreach (PictureBox item in pictureList)
            {
                item.Click += delegate
                {
                    paintColor = item.BackColor;
                    pictureBox2.BackColor = paintColor;
                    panel2.Visible = false;
                };
            }
            paintColor = rojo.BackColor;
            pictureBox2.BackColor = paintColor;

        }
        public void asignaColor(object sender, EventArgs e)
        {
            
        }
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            draw = true;
            x = e.X;
            y = e.Y;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                Graphics g = pictureBox3.CreateGraphics();
                switch (currentItem)
                {
                    case Item.Rectangulo:
                        g.FillRectangle(new SolidBrush(paintColor), x, y, e.X - x, e.Y - y);
                        break;
                    case Item.Texto:
                        break;
                    case Item.Pluma:
                        g.FillEllipse(new SolidBrush(paintColor), e.X - x + x, e.Y - y + y, trackBar1.Value, trackBar1.Value);
                        break;
                    case Item.Goma:
                        g.FillEllipse(new SolidBrush(pictureBox3.BackColor), e.X - x + x, e.Y - y + y, trackBar1.Value, trackBar1.Value);
                        break;
                    default:
                        break;
                }
                g.Dispose();
            }
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
            lx = e.X;
            ly = e.Y;

            if (currentItem == Item.Linea)
            {
                Graphics g = pictureBox3.CreateGraphics();
                g.DrawLine(new Pen(new SolidBrush(paintColor)), new Point(x, y), new Point(lx, ly));
                g.Dispose();
            }
            if (currentItem == Item.Elipse)
            {
                Graphics g = pictureBox3.CreateGraphics();
                g.FillEllipse(new SolidBrush(paintColor), x, y, e.X - x, e.Y - y);
                g.Dispose();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            currentItem = Item.Linea;
            toolStripLabel1.Text = "Linea";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            currentItem = Item.Rectangulo;
            toolStripLabel1.Text = "Cuadrado";

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            currentItem = Item.Elipse;
            toolStripLabel1.Text = "Circulo";

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            currentItem = Item.Pluma;
            toolStripLabel1.Text = "Pluma";

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            currentItem = Item.Goma;
            toolStripLabel1.Text = "Goma";

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Bitmap bmp = (Bitmap)pictureBox1.Image.Clone();
            paintColor = bmp.GetPixel(e.X, e.Y);
            pictureBox2.BackColor = paintColor;
            bmp.Dispose();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("¿Desea restablecer la imagen?", "Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.Yes)
            {
                pictureBox3.Image = null;
                pictureBox3.BackgroundImage = null;
                pictureBox3.BackgroundImage = imagenNueva;
                
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            convierteImagen();
            Guardar();
            MessageBox.Show("Edición guardada exitosamente ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentItem==Item.Texto)
            {
                panel2.Visible = true;
                textBox1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox3.CreateGraphics();
            g.DrawString(textBox1.Text, new Font( "Tahoma", 12, FontStyle.Regular), new SolidBrush(paintColor), new PointF(x, y));
            g.Dispose();
            textBox1.Text = "";
            panel2.Visible = false;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            currentItem = Item.Texto;
            toolStripLabel1.Text = "Cuadro de Texto";

        }

        public void Guardar() {

            conectorSql mycon = new conectorSql();
            string Query = "";
            bool existereg;
            // primero elimina la foto anterior si tiene
            Query = "Delete from imagenesclinica where noExpediente='" + NOEXPEDIENTE 
                + "' and nombre='" + NOMBREIMAGEN +"' and consecutivo="+CONSECUTIVO+"";
            existereg = mycon.Excute(Query);
            
            Query = "insert into imagenesclinica(";
            Query += "noExpediente,";
            Query += "nombre,";
            Query += "imagenEdit,";
            Query += "consecutivo)";
            Query += "values(@noExpediente,@nombre,@imagenEdit,@consecutivo)";

            mycon.Abrirconexion();
            SqlCommand SqlCom = new SqlCommand(Query, mycon.con);

            SqlCom.Parameters.AddWithValue("noExpediente",NOEXPEDIENTE);
            SqlCom.Parameters.AddWithValue("nombre", NOMBREIMAGEN);
            SqlCom.Parameters.AddWithValue("imagenEdit", IMAGEARRAY);
            SqlCom.Parameters.AddWithValue("consecutivo", CONSECUTIVO);
            SqlCom.ExecuteNonQuery();
            mycon.CierraConexion();
        }
        public void convierteImagen() {
            
            Bitmap bmp = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            Graphics g = Graphics.FromImage(bmp);
            Rectangle rect = pictureBox3.RectangleToScreen(pictureBox3.ClientRectangle);
            g.CopyFromScreen(rect.Location, Point.Empty, pictureBox3.Size);
            g.Dispose();
 
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Bmp);

            byte[] byteFoto = ms.ToArray();
            ms.Close();
            IMAGEARRAY = byteFoto;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("¿Desea salir del editor? Los cambios no guardados se perderan", "Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.Yes)
                this.Dispose();
        }
    }
}
