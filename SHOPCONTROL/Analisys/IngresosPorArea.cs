using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace SHOPCONTROL.Analisys
{
    public partial class IngresosPorArea : Form
    {
        public IngresosPorArea()
        {
            InitializeComponent();
        }



        private void button3_Click(object sender, EventArgs e)
        {

            string cvcliente = "";
            Lv.Items.Clear();
            Lv.Columns.Clear();
            Lv.Columns.Add("Fecha", 80);
            Lv.Columns.Add("Total", 80);

            Lv.Columns.Add("UNIDAD_1", 80);
            Lv.Columns.Add("UNIDAD_2", 80);
            Lv.Columns.Add("UNIDAD_3", 80);
            Lv.Columns.Add("UNIDAD_4", 80);

            Lv.Columns.Add("C_ENDONCIA", 100);
            Lv.Columns.Add("C_GINECOLOGIA", 100);
            Lv.Columns.Add("C_ULTRASONIDO", 100);
            Lv.Columns.Add("LABORATORIO", 80);
            Lv.Columns.Add("OFTALMOLOGIA", 90);
            Lv.Columns.Add("OPTOMETRIA", 90);
            Lv.Columns.Add("ORTODONCIA", 90);
            Lv.Columns.Add("ORTOPEDIA", 90);
            Lv.Columns.Add("RAYOS_X_DENTAL", 100);


            Lv.BeginUpdate();
            conectorSql conecta = new conectorSql();

            string Query = "";

            if (AllDatesCheck.Checked)
            {
                Query = "SELECT * FROM [CEPAMM].[dbo].[v_ingresos_area] order by Fecha asc ";
            } else
            {
                Query = "SELECT * FROM [CEPAMM].[dbo].[v_ingresos_area] where Fecha between '" + dateTimePicker1.Value.ToShortDateString() + "' and '" + dateTimePicker2.Value.ToShortDateString() + "' order by Fecha asc ";
            }

            
            int contador = 1;
            decimal TotalCalculado = 0;
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {

                // CultureInfo culture = new CultureInfo("en-US");

                CultureInfo culture = new CultureInfo("en-US");
                DateTime tempDate = Convert.ToDateTime(leer["Fecha"].ToString(), culture);

                cvcliente = tempDate.ToShortDateString();
                ListViewItem lvi = new ListViewItem(cvcliente);

                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["TOTAL"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["C_ENDONCIA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["C_GINECOLOGIA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["C_ULTRASONIDO"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["LABORATORIO"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["OFTALMOLOGIA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["OPTOMETRIA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["ORTODONCIA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["ORTOPEDIA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["RAYOS_X_DENTAL"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["UNIDAD_1"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["UNIDAD_2"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["UNIDAD_3"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["UNIDAD_4"].ToString())));


                TotalCalculado = TotalCalculado + decimal.Parse(leer["TOTAL"].ToString());

                Lv.Items.Add(lvi);

                lvi.UseItemStyleForSubItems = false;
                contador++;
            }


            // Mostrar total en labelTotal
            labelTotal.Text = (String.Format("{0:C}", decimal.Parse(TotalCalculado.ToString())));
            conecta.CierraConexion();
            Lv.EndUpdate();

        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /*
         * 
         * Exportar la información a un archivo de Excel
         * 
        */
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("¿Desea exportar la información a excel?", "Exportación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;


            string idcategoria = "";

            ReportesNKB.ReporteIngresosUnidad(dateTimePicker1.Value.ToShortDateString(), dateTimePicker1.Value.ToShortDateString());

            MessageBox.Show("Reporte creado con éxito!");

        }
    }
}
