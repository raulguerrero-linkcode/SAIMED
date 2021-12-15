using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace SHOPCONTROL
{
    public partial class RegistroConvenio : Form
    {
        public RegistroConvenio()
        {
            InitializeComponent();
        }

        private void RegistroConvenio_Load(object sender, EventArgs e)
        {

        }

        public void BuscarInfoRecibo()
        {
            decimal totalgeneral = 0;
            string totalletra = "";
            string vendedor = "";
            string nombrerecibo = "";
            string direccion = "";
            string entregado = "";
            string colonia = "";
            string compro = "";

            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;
            string Query = "Select * from numrecibos where numrecibo='" + textBox1.Text + "'";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                totalgeneral = decimal.Parse(leer["totalgeneral"].ToString());
                totalletra = leer["totalletra"].ToString();
                vendedor = leer["vendedor"].ToString();
                nombrerecibo = leer["nombrerecibo"].ToString();
                direccion = leer["direccion"].ToString();
                entregado = leer["entregado"].ToString();
                colonia = leer["colonia"].ToString();
                compro = leer["compro"].ToString().Substring(1,25);
            }
            conecta.CierraConexion();
        
        }
    }
}
