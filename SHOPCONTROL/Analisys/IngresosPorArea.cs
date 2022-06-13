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

            Lv.Columns.Add("Dental", 80);

            Lv.Columns.Add("Endodoncia", 80);
            Lv.Columns.Add("Farmacia", 80);
            Lv.Columns.Add("Ginecología", 80);
            Lv.Columns.Add("Inventario", 80);

            Lv.Columns.Add("Laboratorio", 80);
            Lv.Columns.Add("Maxilofacial", 80);
            Lv.Columns.Add("Oftamología", 80);
            Lv.Columns.Add("Óptica", 80);


            Lv.Columns.Add("Ortodoncia", 80);
            Lv.Columns.Add("Ortopedia-Ortodoncia", 80);
            Lv.Columns.Add("Papanicolaou", 80);
            Lv.Columns.Add("Papelería", 80);
            Lv.Columns.Add("Prótesis", 80);
            Lv.Columns.Add("Rayos X Dental", 80);
            Lv.Columns.Add("Sin Categoria", 80);
            Lv.Columns.Add("Ultrasonido", 80);


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
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["DENTAL"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["ENDODONCIA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["FARMACIA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["GINECOLOGIA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["INVENTARIO"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["LABORATORIO"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["MXILOFACIAL"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["OFTALMOLOGIA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["OPTICA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["ORTODONCIA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["ORTOPEDIA_ORTODONCIA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["PAPANICOLAO"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["PAPELERIA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["PROTESIS"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["RAYOS_X_DENTAL"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["SIN_CATEGORIA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["TIENDA"].ToString())));
                lvi.SubItems.Add(String.Format("{0:C}", decimal.Parse(leer["ULTRASONIDO"].ToString())));


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
