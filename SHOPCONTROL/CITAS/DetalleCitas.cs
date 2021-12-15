using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
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
    }
}
