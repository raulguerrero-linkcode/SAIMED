using System;
using System.Globalization;
using System.Windows.Forms;

namespace SHOPCONTROL.Analisys
{
    public partial class ListadoPacientes : Form
    {
        public ListadoPacientes()
        {
            InitializeComponent();
        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ListadoPacientes_Load(object sender, EventArgs e)
        {
           
            Lv.Items.Clear();
            Lv.Columns.Clear();
            Lv.Columns.Add("clave", 80);
            Lv.Columns.Add("NOMBRE", 80);
            Lv.Columns.Add("APATERNO", 80);
            Lv.Columns.Add("AMATERNO", 80);
            Lv.Columns.Add("GENERO", 80);
            Lv.Columns.Add("ESCOLARIDAD", 80);
            Lv.Columns.Add("EMAIL", 80);
            Lv.Columns.Add("EDAD", 80);
            Lv.Columns.Add("ECivil", 80);
            Lv.Columns.Add("NoHijos", 80);
            Lv.Columns.Add("OCUPACION", 80);
            Lv.Columns.Add("TELEFONO", 80);
            Lv.Columns.Add("CALLE", 80);
            Lv.Columns.Add("NoCalle", 80);
            Lv.Columns.Add("CP", 80);
            Lv.Columns.Add("COLONIA", 80);
            Lv.Columns.Add("MUNICIPIO", 80);
            Lv.Columns.Add("CIUDAD", 80);
            Lv.Columns.Add("ESTADO", 80);
            Lv.Columns.Add("Pregunta1", 80);
            Lv.Columns.Add("Pregunta2", 80);
            Lv.Columns.Add("Pregunta3", 80);
            Lv.Columns.Add("RecibeAvisos", 80);
            Lv.Columns.Add("NoExpediente", 80);
            Lv.Columns.Add("SERVICIO", 80);
            Lv.Columns.Add("MEDICO", 80);
            Lv.Columns.Add("TURNO", 80);
            Lv.Columns.Add("OBSERVACIONES", 80);
            Lv.Columns.Add("FECHA", 80);
            Lv.Columns.Add("LUGARNAC", 80);
            Lv.Columns.Add("FECHANAC", 80);
            Lv.Columns.Add("STATUS", 80);
            Lv.Columns.Add("CELULAR", 80);
            Lv.Columns.Add("EMAIL2", 80);
            Lv.Columns.Add("expdental", 80);
            Lv.Columns.Add("expgineco", 80);
            Lv.Columns.Add("expoftamolgo", 80);
            Lv.Columns.Add("curp", 80);

            Lv.BeginUpdate();
            conectorSql conecta = new conectorSql();

            string Query = "";

            Query = "SELECT TOP 100 * FROM Pacientes";

            int contador = 1;

            System.Data.SqlClient.SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {

                // CultureInfo culture = new CultureInfo("en-US");
                DateTime tempDate = new DateTime(1900, 01, 01);
                string temporal = leer["Fecha"].ToString();

                try
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    tempDate = Convert.ToDateTime(leer["Fecha"].ToString(), culture);
                }
                catch (Exception)
                {
                    tempDate = new DateTime(1900, 01, 01);

                }


                //cvcliente = tempDate.ToShortDateString();
                ListViewItem lvi = new ListViewItem(leer["clave"].ToString());
                
                lvi.SubItems.Add(leer["NOMBRE"].ToString());
                lvi.SubItems.Add(leer["APATERNO"].ToString());
                lvi.SubItems.Add(leer["AMATERNO"].ToString());
                lvi.SubItems.Add(leer["GENERO"].ToString());
                lvi.SubItems.Add(leer["ESCOLARIDAD"].ToString());
                lvi.SubItems.Add(leer["EMAIL"].ToString());
                lvi.SubItems.Add(leer["EDAD"].ToString());
                lvi.SubItems.Add(leer["ECivil"].ToString());
                lvi.SubItems.Add(leer["NoHijos"].ToString());
                lvi.SubItems.Add(leer["OCUPACION"].ToString());
                lvi.SubItems.Add(leer["TELEFONO"].ToString());
                lvi.SubItems.Add(leer["CALLE"].ToString());
                lvi.SubItems.Add(leer["NoCalle"].ToString());
                lvi.SubItems.Add(leer["CP"].ToString());
                lvi.SubItems.Add(leer["COLONIA"].ToString());
                lvi.SubItems.Add(leer["MUNICIPIO"].ToString());
                lvi.SubItems.Add(leer["CIUDAD"].ToString());
                lvi.SubItems.Add(leer["ESTADO"].ToString());
                lvi.SubItems.Add(leer["Pregunta1"].ToString());
                lvi.SubItems.Add(leer["Pregunta2"].ToString());
                lvi.SubItems.Add(leer["Pregunta3"].ToString());
                lvi.SubItems.Add(leer["RecibeAvisos"].ToString());
                lvi.SubItems.Add(leer["NoExpediente"].ToString());
                lvi.SubItems.Add(leer["SERVICIO"].ToString());
                lvi.SubItems.Add(leer["MEDICO"].ToString());
                lvi.SubItems.Add(leer["TURNO"].ToString());
                lvi.SubItems.Add(leer["OBSERVACIONES"].ToString());
                lvi.SubItems.Add(leer["FECHA"].ToString());
                lvi.SubItems.Add(leer["LUGARNAC"].ToString());
                lvi.SubItems.Add(leer["FECHANAC"].ToString());
                lvi.SubItems.Add(leer["STATUS"].ToString());
                lvi.SubItems.Add(leer["CELULAR"].ToString());
                lvi.SubItems.Add(leer["EMAIL2"].ToString());
                lvi.SubItems.Add(leer["expdental"].ToString());
                lvi.SubItems.Add(leer["expgineco"].ToString());
                lvi.SubItems.Add(leer["expoftamolgo"].ToString());
                lvi.SubItems.Add(leer["curp"].ToString());
                // lvi.SubItems.Add(leer["NomCompleto"].ToString());


                Lv.Items.Add(lvi);

                lvi.UseItemStyleForSubItems = false;
                contador++;


            }

            conecta.CierraConexion();
            Lv.EndUpdate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("¿Desea exportar la información a excel?", "Exportación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;


            string idcategoria = "";

            ReportesNKB.ReportePacientes();

        }
    }
}
