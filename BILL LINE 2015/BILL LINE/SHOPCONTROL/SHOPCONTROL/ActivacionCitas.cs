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
    public partial class ActivacionCitas : Form
    {
        string CVCLIENTE = "";
        string NUMRECIBO = "";
        public ActivacionCitas()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ActivacionCitas_Load(object sender, EventArgs e)
        {

        }

        public void Cargarinfo()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Num Ticket", 90).Tag = "STRING";
            Lv.Columns.Add("Fecha", 90).Tag = "STRING";
            Lv.Columns.Add("Hora", 90).Tag = "STRING";
            Lv.Columns.Add("Servicio", 190).Tag = "STRING";

            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;
            string Query = "Select * from citas where estatus='SIN PAGAR' and cvpaciente='" + CVCLIENTE + "' order by fechacod desc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["progresivo"].ToString());

                lvi.SubItems.Add(leer["fecha"].ToString());
                lvi.SubItems.Add(leer["horainicia"].ToString());

                string cvservicio = leer["cvservicio"].ToString();
                string nombre = "";
                if (cvservicio != "")
                {
                    string consulta = "Select nombre from productos where cvproducto='" + cvservicio + "'";
                    leer2 = conecta2.RecordInfo(consulta);
                    while (leer2.Read())
                    {
                        nombre = leer2["NOMBRE"].ToString();
                    }
                    conecta2.CierraConexion();

                }
                lvi.SubItems.Add(nombre);
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
        }
        private void ActivacionCitas_Activated(object sender, EventArgs e)
        {
            if (valoresg.CVPACIENTECITAR != "")
            {
                CVCLIENTE = valoresg.CVPACIENTECITAR;
                valoresg.CVPACIENTECITAR = "";
                label4.Text = valoresg.NUMRECIBOREG;
                Cargarinfo();
            }
        }

        private void Lv_DoubleClick(object sender, EventArgs e)
        {

        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (Lv.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = Lv.SelectedIndices;
                foreach (int item in seleccion)
                {
                    DetallesModifica(item);
                }
            }
        }

        public string NUMTICKET;
        public   string FECHA;
        public void DetallesModifica(int index)
        {
            NUMTICKET= Lv.Items[index].Text;
            FECHA= Lv.Items[index].SubItems[1].Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            
            for (int i = 0; i < Lv.Items.Count; i++)
            {
                if (Lv.Items[i].Checked == true)
                {
                    string consulta = "";
                    string numprogresivo = Lv.Items[i].Text;
                    string FECHA = Lv.Items[i].SubItems[1].Text;

                    consulta = "Update citas set ReciboPago='" + label4.Text + "' , estatus='PAGADO' where cvpaciente='" + CVCLIENTE + "' and progresivo='" + numprogresivo + "' and fecha='" +  FECHA+ "'";
                    conecta.Excute(consulta);
                
                }
            }

            MessageBox.Show("Se aplico correctamente el recibo a la citas seleccionadas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
        }
    }
}
