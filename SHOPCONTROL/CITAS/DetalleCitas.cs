using System;
using System.Windows.Forms;
namespace SHOPCONTROL.CITAS
{
    public partial class DetalleCitas : Form
    {
        public DetalleCitas()
        {
            InitializeComponent();
        }

        string IDPACIENTE = "";
        string CVDOCTOR = "";
        string IDTURNO = "";
        public DetalleCitas(string idpaciente, string cvdoctor, string idturno)
        {
            InitializeComponent();
            IDPACIENTE = idpaciente;
            CVDOCTOR=cvdoctor  ;
            IDTURNO = idturno;
        }

        private void DetalleCitas_Load(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from ";
        }


        public void BusquedaCitaPago()
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
