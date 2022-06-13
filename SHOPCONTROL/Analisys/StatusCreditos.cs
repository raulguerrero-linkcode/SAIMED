using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace SHOPCONTROL.Analisys
{
    public partial class StatusCreditos : Form
    {
        public StatusCreditos()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {
                    }

        private void StatusCreditos_Load(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();

            SqlDataReader leer = conecta.RecordInfo("select distinct nombre from Doctores");
            while (leer.Read())
            {
                unidad.Items.Add(leer["nombre"].ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {


            string cvcliente = "";
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Fecha", 80);
            Lv.Columns.Add("iddoctor", 80);
            Lv.Columns.Add("unidad", 80);
            Lv.Columns.Add("cvpreserv", 80);
            Lv.Columns.Add("cvpaciente", 80);
            Lv.Columns.Add("NomCompleto", 80);
            Lv.Columns.Add("cantidad", 80);
            Lv.Columns.Add("cvproducto", 80);
            Lv.Columns.Add("precio", 80);
            Lv.Columns.Add("nombre", 80);
            Lv.Columns.Add("TELEFONO", 80);
            Lv.Columns.Add("estatus", 80);
            Lv.Columns.Add("emitio", 80);
            Lv.Columns.Add("numrecibo", 80);
            Lv.Columns.Add("fechacita", 80);
            Lv.Columns.Add("numticket", 80);



            Lv.BeginUpdate();


            StringBuilder query = new StringBuilder();
            query.Append("Select * from v_statusCreditos where ");

            if (AllDatesCheck.Checked == false)
            {
                query.AppendLine(" Fecha between '" + dateTimePicker1.Value + "' and '" + dateTimePicker2.Value + "' and");
            }

            if (unidad.Text.Length == 0)
            {
                MessageBox.Show("Se requiere seleccionar una unidad");
                return;
            }

            query.AppendLine("  unidad = '" + unidad.Text + "'");

            if (idCliente.Text.Length>0)
            {
                query.AppendLine(" and cvpaciente = '" + idCliente.Text + "'");
            }

            conectorSql conecta = new conectorSql();
            SqlDataReader leer = conecta.RecordInfo(query.Replace("{", string.Empty).Replace("}", string.Empty).ToString());
            while (leer.Read())
            {

                CultureInfo culture = new CultureInfo("en-US");
                DateTime tempDate = Convert.ToDateTime(leer["Fecha"].ToString(), culture);

                string FECHA = tempDate.ToShortDateString();
                ListViewItem lvi = new ListViewItem(FECHA);

                // lvi.SubItems.Add(leer["Fecha"].ToString());
                lvi.SubItems.Add(leer["iddoctor"].ToString());
                lvi.SubItems.Add(leer["unidad"].ToString());
                lvi.SubItems.Add(leer["cvpreserv"].ToString());
                lvi.SubItems.Add(leer["cvpaciente"].ToString());
                lvi.SubItems.Add(leer["NomCompleto"].ToString());
                lvi.SubItems.Add(leer["cantidad"].ToString());
                lvi.SubItems.Add(leer["cvproducto"].ToString());
                lvi.SubItems.Add(leer["precio"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["TELEFONO"].ToString());
                lvi.SubItems.Add(leer["estatus"].ToString());
                lvi.SubItems.Add(leer["emitio"].ToString());
                lvi.SubItems.Add(leer["numrecibo"].ToString());
                lvi.SubItems.Add(leer["fechacita"].ToString());
                lvi.SubItems.Add(leer["numticket"].ToString());

                Lv.Items.Add(lvi);

                lvi.UseItemStyleForSubItems = false;
            }
            conecta.CierraConexion();
            Lv.EndUpdate();

        }
    }
}
