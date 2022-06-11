using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
namespace SHOPCONTROL
{
    public partial class CortePagosDiario : Form
    {
        public CortePagosDiario()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DateTime Fechainicia = dateTimePicker1.Value.AddDays(-1);
            //DateTime FechaFinal = dateTimePicker1.Value;
            //valoresg.AlinearPagosNoEncontrados(FechaFinal.ToString("yyyyMMdd"));
            
            //conectorSql conecta = new conectorSql();
            //int ultimoRecibo=0;

            
            //string Query = "Update parametros set ultimoRecibo='" + ultimoRecibo.ToString() + "'";
            //Reportespdf reporte = new Reportespdf();
            //string cadena = reporte.ReporteDePagos(Fechainicia.ToString("dd/MM/yyyy"), FechaFinal.ToString("dd/MM/yyyy"), dateTimePicker2.Value.ToString("HHmmss"), dateTimePicker3.Value.ToString("HHmmss"));
            //try
            //{
            //    System.Diagnostics.Process.Start(cadena);
            //}
            //catch (Exception er)
            //{
            //    MessageBox.Show("" + er, "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    throw;
            //}

        }


        private void CortePagosDiario_Load(object sender, EventArgs e)
        {
           // dateTimePicker3.Value = DateTime.Parse("17:00:00");
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

          //  dateTimePicker2.Value = DateTime.Parse("17:00:00");
            BuscarNull();

        }

        public void BuscarNull()
        {
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            string query = "select * from pagos where fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "'";
            //   query=query + " and horacodpago is null and Horapago<>''";
            SqlDataReader leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                                
                string Horapago = leer["horapago"].ToString();
                if (Horapago != "")
                {
                    string numpedido = leer["numpedido"].ToString();
                    string fechacod = leer["fechacod"].ToString();
                    DateTime HoraReg = DateTime.Parse(Horapago);

                    string consulta = "Update pagos set horacodpago='" + HoraReg.ToString("HHmmss") + "' where numpedido='" + numpedido + "' and fechacod='" + fechacod + "' and horapago='" + Horapago + "'";
                    conecta2.Excute2(consulta);
                    conecta2.CierraConexion();
                }
            }
            conecta.CierraConexion();

            query = "select * from pagos where fechacod='" + dateTimePicker1.Value.AddDays(-1).ToString("yyyyMMdd") + "'";
            //query=query + " and horacodpago is null and Horapago<>''";
            leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                
                string Horapago = leer["horapago"].ToString();
                if (Horapago != "")
                {
                    string numpedido = leer["numpedido"].ToString();
                    string fechacod = leer["fechacod"].ToString();
                    DateTime HoraReg = DateTime.Parse(Horapago);

                    string consulta = "Update pagos set horacodpago='" + HoraReg.ToString("HHmmss") + "' where numpedido='" + numpedido + "' and fechacod='" + fechacod + "' and Horapago ='" + Horapago + "'";
                    conecta2.Excute2(consulta);
                    conecta2.CierraConexion();
                }
            }
            conecta.CierraConexion();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Reportespdf reporte = new Reportespdf();
            //DateTime Fechainicia = dateTimePicker1.Value.AddDays(-1);
            //DateTime FechaFinal = dateTimePicker1.Value;
            //string cadena = reporte.ReporteDeRecibos(Fechainicia.ToString("dd/MM/yyyy"), FechaFinal.ToString("dd/MM/yyyy"), dateTimePicker2.Value.ToString("HHmmss"), dateTimePicker3.Value.ToString("HHmmss"));
            //try
            //{
            //    System.Diagnostics.Process.Start(cadena);
            //}
            //catch (Exception er)
            //{
            //    MessageBox.Show("" + er, "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    throw;
            //}

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("¿Impresion de corte diario?", "Impresion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.Yes)
            {
                mandarReporteCristal();
            }
        }

        public void mandarReporteCristalRecibos()
        {

            ReportDocument cryRpt = new ReportDocument();
            string CadenaReporte2 = @"\\SRV-DATACENTER\\tmp\\reports\\TicketCorteRecibos.rpt";
            DataSet ds = new DataSet();

            string fecha1 = dateTimePicker1.Value.ToString("yyyyMMdd");
            string fecha2 = dateTimePicker2.Value.ToString("yyyyMMdd");

            HistorialClinica.DataSetCorteTableAdapters.DetallesRecibosTableAdapter detalles = new HistorialClinica.DataSetCorteTableAdapters.DetallesRecibosTableAdapter();
            HistorialClinica.DataSetCorte.DetallesRecibosDataTable tdetalles = new HistorialClinica.DataSetCorte.DetallesRecibosDataTable();
            detalles.Fill(tdetalles, fecha1, fecha2);

            HistorialClinica.DataSetCorteTableAdapters.DetallesRecibos1TableAdapter detalles2 = new HistorialClinica.DataSetCorteTableAdapters.DetallesRecibos1TableAdapter();
            HistorialClinica.DataSetCorte.DetallesRecibos1DataTable tdetalles2 = new HistorialClinica.DataSetCorte.DetallesRecibos1DataTable();
            detalles2.Fill(tdetalles2, fecha1, fecha2);


            HistorialClinica.DataSetCorteTableAdapters.LogoEmpresaTableAdapter logoemp = new HistorialClinica.DataSetCorteTableAdapters.LogoEmpresaTableAdapter();
            HistorialClinica.DataSetCorte.LogoEmpresaDataTable tlogoemp = new HistorialClinica.DataSetCorte.LogoEmpresaDataTable();
            logoemp.Fill(tlogoemp);

            HistorialClinica.DataSetCorteTableAdapters.ParametrosReciboTableAdapter parametro = new HistorialClinica.DataSetCorteTableAdapters.ParametrosReciboTableAdapter();
            HistorialClinica.DataSetCorte.ParametrosReciboDataTable tparametro = new HistorialClinica.DataSetCorte.ParametrosReciboDataTable();
            parametro.Fill(tparametro);

            decimal tdebito = 0;
            decimal tcredito = 0;
            decimal tefectivo = 0;


            ds.Clear();
            ds.Tables.Add(tdetalles);
            ds.Tables.Add(tlogoemp);
            ds.Tables.Add(tparametro);
            ds.Tables.Add(tdetalles2);

            cryRpt.Load(CadenaReporte2);
            cryRpt.SetDataSource(ds);
            cryRpt.SetParameterValue("tdebito", tdebito.ToString());
            cryRpt.SetParameterValue("tcredito", tcredito.ToString());
            cryRpt.SetParameterValue("tefectivo", tefectivo.ToString());
            // string NombreArchivo = @"C:\TicketCorteRecibos.pdf";
            // cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, NombreArchivo);
            cryRpt.PrintToPrinter(1, false, 0, 0);
            cryRpt.Close();
            cryRpt.Dispose();
        

        }

        public void mandarReporteCristal()
        {
          
                ReportDocument cryRpt = new ReportDocument();

                string CadenaReporte = @"\\SRV-DATACENTER\\tmp\\reports\\TicketCorte.rpt";
                // string CadenaReporte = @"C:\tmp\TicketCorte12.rpt";
                DataSet ds = new DataSet();

                string fecha1 = dateTimePicker1.Value.ToString("yyyyMMdd");
                string fecha2 = dateTimePicker2.Value.ToString("yyyyMMdd");

                HistorialClinica.DataSetCorteTableAdapters.DetallesRecibosTableAdapter detalles = new HistorialClinica.DataSetCorteTableAdapters.DetallesRecibosTableAdapter();
                HistorialClinica.DataSetCorte.DetallesRecibosDataTable tdetalles = new HistorialClinica.DataSetCorte.DetallesRecibosDataTable();
                detalles.Fill(tdetalles, fecha1,fecha2);

                HistorialClinica.DataSetCorteTableAdapters.DetallesRecibos1TableAdapter detalles2 = new HistorialClinica.DataSetCorteTableAdapters.DetallesRecibos1TableAdapter();
                HistorialClinica.DataSetCorte.DetallesRecibos1DataTable tdetalles2 = new HistorialClinica.DataSetCorte.DetallesRecibos1DataTable();
                detalles2.Fill(tdetalles2, fecha1, fecha2);


                HistorialClinica.DataSetCorteTableAdapters.LogoEmpresaTableAdapter logoemp = new HistorialClinica.DataSetCorteTableAdapters.LogoEmpresaTableAdapter();
                HistorialClinica.DataSetCorte.LogoEmpresaDataTable tlogoemp = new HistorialClinica.DataSetCorte.LogoEmpresaDataTable();
                logoemp.Fill(tlogoemp);

                HistorialClinica.DataSetCorteTableAdapters.ParametrosReciboTableAdapter parametro = new HistorialClinica.DataSetCorteTableAdapters.ParametrosReciboTableAdapter();
                HistorialClinica.DataSetCorte.ParametrosReciboDataTable tparametro = new HistorialClinica.DataSetCorte.ParametrosReciboDataTable();
                parametro.Fill(tparametro); 

                decimal tdebito = 0;
                decimal tcredito = 0;
                decimal tefectivo = 0;

                conectorSql conecta = new conectorSql();
                string Query = "Select sum(cantidad) as total from pagos where fechacod between '" + fecha1 + "' and '" + fecha2 + "' and pagocon='EFECTIVO'";
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    string TOTAL = leer["total"].ToString();
                    if (TOTAL == "") TOTAL = "0";
                    tefectivo = decimal.Parse(TOTAL);
                }
                conecta.CierraConexion();

                Query = "Select sum(cantidad) as total from pagos where fechacod between '" + fecha1 + "' and '" + fecha2 + "' and pagocon='DEBITO'";
                leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    string TOTAL = leer["total"].ToString();
                    if (TOTAL == "") TOTAL = "0";
                    tdebito = decimal.Parse(TOTAL);
                }
                conecta.CierraConexion();

                Query = "Select sum(cantidad) as total from pagos where fechacod between '" + fecha1 + "' and '" + fecha2 + "' and pagocon='CREDITO'";
                leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    string TOTAL = leer["total"].ToString();
                    if (TOTAL == "") TOTAL = "0";
                    tcredito = decimal.Parse(TOTAL);
                }
                conecta.CierraConexion();

                ds.Clear();
                ds.Tables.Add(tdetalles);
                ds.Tables.Add(tlogoemp);
                ds.Tables.Add(tparametro);
                ds.Tables.Add(tdetalles2);

                cryRpt.Load(CadenaReporte);
                cryRpt.SetDataSource(ds);
                cryRpt.SetParameterValue("tdebito", tdebito.ToString());
                cryRpt.SetParameterValue("tcredito", tcredito.ToString());
                cryRpt.SetParameterValue("tefectivo", tefectivo.ToString());
                // string NombreArchivo = @"C:\tmp\TicketCorte.pdf";
                // cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, NombreArchivo);
                cryRpt.PrintToPrinter(1, false, 0, 0);
                cryRpt.Close();
                cryRpt.Dispose();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            mandarReporteCristalRecibos();
        }
    }
}
