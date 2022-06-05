using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
//using BarcodeLib;

namespace SHOPCONTROL.JOSE_FROMS
{
    public partial class CodigoQR : Form
    {
        public CodigoQR()
        {
            InitializeComponent();
        }


        private void CodigoQR_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image imgFinal = (Image)panel1.BackgroundImage.Clone();

            SaveFileDialog Guardar = new SaveFileDialog();
            Guardar.AddExtension = true;
            Guardar.Filter = "Image PNG (*.png)|*.png";
            Guardar.ShowDialog();
            if (!string.IsNullOrEmpty(Guardar.FileName))
            {
                imgFinal.Save(Guardar.FileName, ImageFormat.Png);
            }
            imgFinal.Dispose();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
                //BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
                //codigo.IncludeLabel = true;
                //panel1.BackgroundImage = codigo.Encode(BarcodeLib.TYPE.CODE128, textBox1.Text, Color.Black, Color.White, 200, 150);
                //button1.Enabled = true;
        }
    }
}
