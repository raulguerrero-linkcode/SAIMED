using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
namespace SHOPCONTROL
{
    public partial class importarXML : Form
    {
        public importarXML()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlSerializer serial = new XmlSerializer(typeof(Comprobante2));
            FileStream fs = new FileStream("FacturaViewerOutSrv.xml", FileMode.Open);

            Comprobante2 ds = (Comprobante2)serial.Deserialize(fs);
            int contador=ds.Conceptos.Length;

            fs.Close();

        }


       
    }
}
