using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

using System.Data.SqlClient;
namespace SHOPCONTROL
{
    public partial class ReporteGeneral : Form
    {
        public ReporteGeneral()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Update detallesfacturas set cvproducto=cvunica where cvproducto=''";
            conecta.Excute(Query);

            Query = "Update detallesfacturas set cvunica=cvproducto where len(cvunica)=1";
            conecta.Excute(Query);


            //if (valoresg.CVREPORTE == "FACTURACION") ReporteFacturacion();
             if (radioButton1.Checked == true) ListadoFacturas();
             if (radioButton2.Checked == true) ListadoFacturasAvanzado();
        }
        public void ListadoFacturas()
        {
            decimal RecTotalImporte = 0;
            decimal RecTAdicional = 0;
            decimal Rttotal = 0;

            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Num. Interno", 90);
            Lv.Columns.Add("Num. Factura", 90);
            Lv.Columns.Add("Fecha Realizo", 110);
            Lv.Columns.Add("Subtotal", 100, HorizontalAlignment.Right);
            Lv.Columns.Add("I.V.A", 100, HorizontalAlignment.Right);
            Lv.Columns.Add("Total", 110,HorizontalAlignment.Right);

            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            int contador = 0;
            string Query = "Select * from facturas where numpedido<>''";
            Query = Query + "  and fechacod between '" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and '" + dateTimePicker2.Value.ToString("yyyyMMdd") + "'";
            Query = Query + "  and facturas.estatus='FACTURADO'";
            if (textBox11.Text != "") Query = Query + "  and numpedido='" + textBox11.Text + "'";
            Query = Query + "  order by numpedido asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string numpedido = leer["numpedido"].ToString();
                string numfact = leer["numfactura"].ToString();
                string fecha= leer["fecha"].ToString();

                string consulta = "Select cvproducto, descripcion, importe Timporte,valorIVA as TIVA from DetallesFacturas ";
                consulta = consulta + " where numpedido='" + numpedido + "' order by descripcion asc";
                decimal Timporte = 0;
                decimal TotalIVA = 0;
                decimal Ttotal = 0;

                SqlDataReader leer2 = conecta2.RecordInfo(consulta); // por recibo
                while (leer2.Read())
                {

                    if (leer2["Timporte"].ToString() != "") Timporte =Timporte+ decimal.Parse(leer2["Timporte"].ToString());
                    else Timporte = Timporte + 0;

                    if (leer2["TIVA"].ToString() != "") TotalIVA = TotalIVA + decimal.Parse(leer2["TIVA"].ToString());
                    else TotalIVA = TotalIVA + 0;

                    RecTotalImporte = RecTotalImporte + Timporte;
                    RecTAdicional = RecTAdicional + TotalIVA;
                }
                conecta2.CierraConexion();
                Ttotal = Timporte + TotalIVA;


                Rttotal = Rttotal + Ttotal;

                ListViewItem lvi = new ListViewItem(numpedido);
                lvi.SubItems.Add(numfact);
                lvi.SubItems.Add(fecha);

                lvi.SubItems.Add(Timporte.ToString("#,#.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(TotalIVA.ToString("#,#.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(Ttotal.ToString("#,#.00", CultureInfo.InvariantCulture));

                Lv.Items.Add(lvi);

            }
            conecta.CierraConexion();
            label1.Text = Rttotal.ToString("#,#.00", CultureInfo.InvariantCulture);
        }

        public void ListadoFacturasAvanzado()
        {
            decimal RecTotalImporte = 0;
            decimal RecTAdicional = 0;
            decimal Rttotal = 0;

            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Num. Interno", 90);
            Lv.Columns.Add("Num. Factura", 90);
            Lv.Columns.Add("Nombre", 290);
            Lv.Columns.Add("Fecha Realizo", 110);
            Lv.Columns.Add("Subtotal", 100, HorizontalAlignment.Right);
            Lv.Columns.Add("Adicional", 100, HorizontalAlignment.Right);
            Lv.Columns.Add("Total", 110, HorizontalAlignment.Right);


            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            int contador = 0;
            string Query = "Select * from facturas where numpedido<>''";
            Query = Query + "  and fechacod between '" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and '" + dateTimePicker2.Value.ToString("yyyyMMdd") + "'";
            Query = Query + "  and facturas.estatus='FACTURADO'";
            if (textBox11.Text != "") Query = Query + "  and numpedido='" + textBox11.Text + "'";
            Query = Query + "  order by numpedido asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string numpedido = leer["numpedido"].ToString();
                string numfact = leer["numfactura"].ToString();
                string fecha = leer["fecha"].ToString();
                string renombre= leer["Renombre"].ToString();


                decimal Timporte = decimal.Parse(leer["subtotal"].ToString());
                decimal Tadicional = decimal.Parse(leer["timpuestosretenido"].ToString()) ;
                decimal Ttotal = decimal.Parse(leer["total"].ToString());
                RecTotalImporte = RecTotalImporte + Timporte;
                RecTAdicional = RecTAdicional + Tadicional;
                Rttotal = Rttotal + Ttotal;

                ListViewItem lvi = new ListViewItem(numpedido);
                lvi.SubItems.Add(numfact);
                lvi.SubItems.Add(renombre);
                lvi.SubItems.Add(fecha);
                lvi.SubItems.Add(Timporte.ToString("#,#.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(Tadicional.ToString("#,#.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(Ttotal.ToString("#,#.00", CultureInfo.InvariantCulture));

                Lv.Items.Add(lvi);

            }
            conecta.CierraConexion();
            label1.Text = Rttotal.ToString("#,#.00", CultureInfo.InvariantCulture);
        }


        public void ReporteFacturacion()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Num. Interno", 55);
            Lv.Columns.Add("Nombre", 180);
            Lv.Columns.Add("Fecha Realizo", 80);
            Lv.Columns.Add("Subtotal", 70);
            Lv.Columns.Add("Adicional", 60);
            Lv.Columns.Add("Total", 70);
            Lv.Columns.Add("Estatus ", 100);
            Lv.Columns.Add("Año", 0);
            Lv.Columns.Add("Fecha Facturada", 100);
            conectorSql conecta = new conectorSql();
            string Query = "Select distinct(facturas.numpedido), facturas.renombre as Nombrecliente ";
            Query = Query + ",facturas.fecha,facturas.total,facturas.timpuestosretenido as adicional,facturas.subtotal,facturas.estatus,facturas.ayo";
            Query = Query + " ,facturas.Fechafactura";
            Query = Query + " from Facturas ";
            Query = Query + " where facturas.numpedido<>''";

            Query = Query + " and facturas.fechacod between '" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.ToString("yyyyMMdd") + "'";
            if (textBox11.Text != "") Query = Query + " and facturas.numpedido='" + textBox11.Text + "'";
            if (textBox18.Text != "") Query = Query + " and facturas.reclave='" + textBox18.Text + "'";
            //Query = Query + " and facturas.estatus='FACTURADO'";
            Query = Query + " order by facturas.numpedido desc";

            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["numpedido"].ToString());
                lvi.SubItems.Add(leer["NombreCliente"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                decimal valor = decimal.Parse(leer["subtotal"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                valor = decimal.Parse(leer["adicional"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                valor = decimal.Parse(leer["total"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(leer["estatus"].ToString());
                lvi.SubItems.Add(leer["ayo"].ToString());
                lvi.SubItems.Add(leer["Fechafactura"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label15.Text = Lv.Items.Count.ToString() + " Registros";
            CambioDeColoresCelda();
        }
        private void CambioDeColoresCelda()
        {

            int columna = 0;
            columna = 9;

            Lv.BeginUpdate();

            foreach (ListViewItem item in Lv.Items)
            {
                int indice = 0;
                item.UseItemStyleForSubItems = false;

                foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                {
                    if (indice == columna)
                    {
                        if (subitem.Text == "CAPTURADO") subitem.BackColor = Color.FromArgb(252, 252, 116);
                        if (subitem.Text == "FACTURADO") subitem.BackColor = Color.FromArgb(96, 204, 69);
                        if (subitem.Text == "CANCELADO") subitem.BackColor = Color.FromArgb(255, 192, 192);

                    }
                    indice++;
                }
            }
            Lv.EndUpdate();
        }

        private void ReporteGeneral_Load(object sender, EventArgs e)
        {
            BusquedaUsuario();
            if (valoresg.CVREPORTE == "FACTURACION")
            {
                label13.Text = "Información de Facturas";

            }

            DateTime Fecha = DateTime.Now; //DateTime.Parse("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year.ToString());
            dateTimePicker1.Value = Fecha;            
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Reportespdf reportes = new Reportespdf();
            string Cadena = "";
            if (valoresg.CVREPORTE == "FACTURACION")
            {
                Cadena = reportes.ReporteFacturacionsAvanzado(dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString(), textBox11.Text, textBox18.Text);
            }

            try
            {
                System.Diagnostics.Process.Start(Cadena);
            }
            catch (Exception er)
            {
                MessageBox.Show("" + er, "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);
        }

        private void textBox18_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reportespdf reportes = new Reportespdf();
            string Cadena = "";
            if (valoresg.CVREPORTE == "FACTURACION")
            {
                Cadena = reportes.ReportePorConcepto(dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString(), textBox11.Text, textBox18.Text);
            }

            try
            {
                System.Diagnostics.Process.Start(Cadena);
            }
            catch (Exception er)
            {
                MessageBox.Show("" + er, "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reportespdf reportes = new Reportespdf();
            string Cadena = "";
            if (valoresg.CVREPORTE == "FACTURACION")
            {
                Cadena = reportes.ReportePorReciboDetallado(dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString(), textBox11.Text, textBox18.Text);
            }

            try
            {
                System.Diagnostics.Process.Start(Cadena);
            }
            catch (Exception er)
            {
                MessageBox.Show("" + er, "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            conectorSql conecta=new conectorSql();
            conectorSql conecta2 = new conectorSql();
            conectorSql conecta3 = new conectorSql();

            //SqlDataReader leer2 = null;
            //SqlDataReader leer3 = null; 
            //string Query = "Select * from facturas where fechacod between '" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.ToString("yyyyMMdd") + "'";
            //SqlDataReader leer = conecta.RecordInfo(Query);
            //while (leer.Read())
            //{
            //    string numpedido = leer["numpedido"].ToString();
            //    decimal totalFactura = decimal.Parse(leer["total"].ToString());
            //    decimal AdicionalFac = decimal.Parse(leer["Rimporte"].ToString());


            //    //string consulta = "Select * from DetallesPedido where numpedido='" + numpedido + "' order by descripcion asc";
            //    //leer2 = conecta2.RecordInfo(consulta);
            //    //while (leer2.Read())
            //    //{
            //    //   string precio = leer2["preunitario"].ToString();
            //    //   string preciototal = leer2["precio"].ToString();
            //    //   string cvproducto=leer2["cvproducto"].ToString();

            //    string consulta2 = "Select sum(importe) as totalImporte, sum(adicional) as totalAdicional from detallesFacturas where numpedido='" + numpedido + "'";
            //       leer3 = conecta3.RecordInfo(consulta2);
            //       while (leer3.Read())
            //       {
            //           //decimal valorunitario = decimal.Parse(leer3["valorunitario"].ToString());
            //           decimal total = decimal.Parse(leer3["totalImporte"].ToString());
            //           decimal adicional = decimal.Parse(leer3["totalAdicional"].ToString());
            //           total = total + adicional;

            //           if (total!= totalFactura)
            //           {
            //               MessageBox.Show("Corregir esta factura" + numpedido + " Total Detalle:" + total + " Total Factura: " + totalFactura, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //           }
            //       }
            //       conecta3.CierraConexion();


            //    //}
            //    //conecta2.CierraConexion();

            //}
            //conecta.CierraConexion();


            string Query = "Select * from facturas where fechacod between '" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.ToString("yyyyMMdd") + "'";
            Query =Query + " order by numpedido desc";
            SqlDataReader leer = conecta.RecordInfo(Query);

            while (leer.Read())
            {
                string fecha = leer["fecha"].ToString();
                string fechacod = leer["fechacod"].ToString();
                string numpedido = leer["numpedido"].ToString();
                DateTime FechaR = DateTime.Parse(fecha);
                string consulta = "Update detallesfacturas set fechacod='" + fechacod + "' , fecha='" + fecha + "', mes='" + FechaR.Month.ToString() + "', ayo='"+ FechaR.Year.ToString() + "' where numpedido='" + numpedido + "'";
                conecta2.Excute(consulta);
            }
            conecta.CierraConexion();

            MessageBox.Show("Termino de alinear los campos vacios", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        public string NUMFACTURA = "";
        public void DetallesModifica(int index)
        {
            NUMFACTURA = Lv.Items[index].SubItems[1].Text;
        }

        private void Lv_DoubleClick(object sender, EventArgs e)
        {
            string cadenaDocumentopdf = CADDIRECCION + "\\FACTURA-" + NUMFACTURA + ".pdf";
            try
            {
                System.Diagnostics.Process.Start(cadenaDocumentopdf);
                this.Dispose();
            }
            catch (Exception er)
            {
                MessageBox.Show("" + er.Message.ToString(), "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }


        public string USUARIOFAC = "";
        public string CONTRASEÑAFAC = "";
        public string CORREOFAC = "";
        public string CADDIRECCION = "";
        public void BusquedaUsuario()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from parametros where usuariofolio<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                USUARIOFAC = leer["usuarioFolio"].ToString();
                CONTRASEÑAFAC = leer["contraFolio"].ToString();
                CORREOFAC = leer["correover"].ToString();
                CADDIRECCION = leer["dirRespaldo"].ToString();
            }
            conecta.CierraConexion();
        }
    }
}
