using System;
using System.Windows.Forms;

namespace SHOPCONTROL
{
    public partial class VistaDescripcion : Form
    {
        public VistaDescripcion()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            valoresg.TEXTODESCRIPCION = textBox1.Text;
            valoresg.DETALLE1 = textBox2.Text;
            this.Dispose();
        }

        private void VistaDescripcion_Load(object sender, EventArgs e)
        {
            textBox1.Text = valoresg.TEXTODESCRIPCION;
            textBox2.Text = valoresg.DETALLE1;
        }
    }
}
