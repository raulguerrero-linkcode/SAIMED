using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
namespace SHOPCONTROL
{
    public partial class RegistroPagos : Form
    {
        public RegistroPagos()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BuscarRecibosyFacturas();
           

        }

        public void BuscarRecibosyFacturas()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Num. Pedido", 55).Tag = "NUMBER";
            Lv.Columns.Add("Nombre cliente", 170).Tag = "STRING";
            Lv.Columns.Add("Fecha", 80).Tag = "STRING";
            Lv.Columns.Add("subtotal", 70).Tag = "STRING";
            Lv.Columns.Add("iva", 60).Tag = "STRING";
            Lv.Columns.Add("total", 70).Tag = "STRING";
            Lv.Columns.Add("Estatus del Pago", 90).Tag = "STRING";
            Lv.Columns.Add("Descripción", 200).Tag = "STRING";
            Lv.Columns.Add("Año", 0).Tag = "STRING";
            Lv.Columns.Add("Vendedor", 100).Tag = "STRING";
            Lv.Columns.Add("Tipo Documento", 100).Tag = "STRING";
            Lv.Columns.Add("Id Cliente", 50).Tag = "STRING";

            conectorSql conecta = new conectorSql();
            string Query = "Select distinct(numpedido), pedidos.nombre as Nombrecliente,pedidos.cvcliente ";
            Query = Query + ",fecha,total,iva,totalgeneral,status,compro,ayo,estatuspedido,estatuspago,pedidos.vendedor";
            Query = Query + " from pedidos ";
            Query = Query + " inner join clientes on clientes.cvcliente=pedidos.cvcliente";
            Query = Query + " where numpedido<>''";

            Query = Query + " and fechacod between '" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.ToString("yyyyMMdd") + "'";
            if (textBox11.Text != "") Query = Query + " and numpedido='" + textBox11.Text + "'";
            if (textBox18.Text != "") Query = Query + " and pedidos.cvcliente='" + textBox18.Text + "'";
            Query = Query + " order by pedidos.numpedido desc";

            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["numpedido"].ToString());
                lvi.SubItems.Add(leer["NombreCliente"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                decimal valor = decimal.Parse(leer["total"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                valor = decimal.Parse(leer["iva"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                valor = decimal.Parse(leer["totalgeneral"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(leer["estatuspago"].ToString());
                lvi.SubItems.Add(leer["compro"].ToString());
                lvi.SubItems.Add(leer["ayo"].ToString());
                lvi.SubItems.Add(leer["vendedor"].ToString());
                lvi.SubItems.Add("FACTURA");
                lvi.SubItems.Add(leer["cvcliente"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();


            //---------------------------------------------------------------------------------------
            //---------------------------------------------------------------------------------------
            //Aqui van los recibos
            Query = "Select distinct(numrecibo), nombrerecibo as Nombrecliente, cvcliente ";
            Query = Query + ",fecha,total,iva,totalgeneral,compro,ayo,estatusrecibo,condicionpago,vendedor";
            Query = Query + " from recibos ";
            Query = Query + " where nombrerecibo<>''";

            Query = Query + " and fechacod between '" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.ToString("yyyyMMdd") + "'";
            if (textBox11.Text != "") Query = Query + " and numrecibo='" + textBox1.Text + "'";
            if (textBox18.Text != "") Query = Query + " and cvcliente='" + textBox18.Text + "'";
            Query = Query + " order by numrecibo desc";

            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["numrecibo"].ToString());
                lvi.SubItems.Add(leer["NombreCliente"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                decimal valor = decimal.Parse(leer["total"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                valor = decimal.Parse(leer["iva"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                valor = decimal.Parse(leer["totalgeneral"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(leer["condicionpago"].ToString());
                lvi.SubItems.Add(leer["compro"].ToString());
                lvi.SubItems.Add(leer["ayo"].ToString());
                lvi.SubItems.Add(leer["vendedor"].ToString());
                lvi.SubItems.Add("RECIBO");
                lvi.SubItems.Add(leer["cvcliente"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();

            //---------------------------------------------------------------------------------------
            //---------------------------------------------------------------------------------------

            label15.Text = Lv.Items.Count.ToString() + " Registros";
            CambioDeColoresCelda();
            CambioDeColoresCeldaPagos();


        }

        private void CambioDeColoresCelda()
        {

            int columna = 0;
            columna = 10;

            Lv.BeginUpdate();

            foreach (ListViewItem item in Lv.Items)
            {
                int indice = 0;
                item.UseItemStyleForSubItems = false;

                foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                {
                    if (indice == columna)
                    {
                        if (subitem.Text == "FACTURA") subitem.BackColor = Color.FromArgb(203, 248, 215);
                        if (subitem.Text == "RECIBO") subitem.BackColor = Color.FromArgb(185, 251, 249);
                    }
                    indice++;
                }
            }
            Lv.EndUpdate();
        }

        private void CambioDeColoresCeldaPagos()
        {

            int columna = 0;
            columna = 6;

            Lv.BeginUpdate();

            foreach (ListViewItem item in Lv.Items)
            {
                int indice = 0;
                item.UseItemStyleForSubItems = false;

                foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                {
                    if (indice == columna)
                    {
                        if (subitem.Text == "POR PAGAR") subitem.BackColor = Color.FromArgb(252, 252, 116);
                        if (subitem.Text == "PAGADO") subitem.BackColor = Color.FromArgb(96, 204, 69);
                        if (subitem.Text == "CANCELADO") subitem.BackColor = Color.FromArgb(255, 192, 192);
                    }
                    indice++;
                }
            }
            Lv.EndUpdate();
        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        public void DetallesModificaVer(int index)
        {
            string numpedido = Lv.Items[index].Text;
            label2.Text = numpedido;
            string ayo = Lv.Items[index].SubItems[8].Text;
            string estatus = Lv.Items[index].SubItems[10].Text;
            string estatusPago = Lv.Items[index].SubItems[6].Text;
            string cvcliente = Lv.Items[index].SubItems[11].Text;

            string nombre = Lv.Items[index].SubItems[1].Text;
            string total = Lv.Items[index].SubItems[5].Text;
            label50.Text = numpedido;
            label51.Text = ayo;
            label53.Text = estatus;
            label25.Text = nombre;
            label26.Text = total;
            label11.Text = numpedido;
            label10.Text = total;
            label16.Text = estatusPago;
            label19.Text = estatus;
            label20.Text = cvcliente;
        }

        private void Lv_DoubleClick(object sender, EventArgs e)
        {
            if (Lv.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = Lv.SelectedIndices;
                foreach (int item in seleccion)
                {
                    LimpiarPagos();
                    DetallesModificaVer(item);
                    BuscarInfo();
                }
            }
            
          
        }

        public void BuscarInfo()
        {
            listView1.Items.Clear();
            listView1.Columns.Clear();

            listView1.Columns.Add("Num Pago", 100).Tag = "NUMBER";
            listView1.Columns.Add("Monto", 150).Tag = "STRING";
            listView1.Columns.Add("Fecha", 90).Tag = "STRING";
            listView1.Columns.Add("Observacion", 150).Tag = "STRING";
            listView1.Columns.Add("Estatus", 100).Tag = "STRING";
            listView1.Columns.Add("Emitio", 100).Tag = "STRING";
            listView1.Columns.Add("Num pedido", 100).Tag = "NUMBER";

            conectorSql conecta = new conectorSql();
            string Query = "Select * from pagos where numpedido='" + label50.Text + "' and ayo='" + label51.Text + "' and estatus='PAGADO'";
            Query = Query + " order by fechacod desc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["numpago"].ToString());
                lvi.SubItems.Add(leer["cantidad"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                lvi.SubItems.Add(leer["observacion"].ToString());
                lvi.SubItems.Add(leer["estatus"].ToString());
                string emitio = leer["emitiopago"].ToString();
                lvi.SubItems.Add(emitio);
                lvi.SubItems.Add(leer["numpedido"].ToString());
                listView1.Items.Add(lvi);                
            }
            conecta.CierraConexion();
            SumadePagos();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            ConsecutivoPago();
            decimal restaporpagar = decimal.Parse(label10.Text) - decimal.Parse(label18.Text);
            label9.Text = restaporpagar.ToString();

            if (restaporpagar <= 0)
            {
                MessageBox.Show("Se encuentra totalmente pagado, gracias", "Pagado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            panel1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void RegistroPagos_Load(object sender, EventArgs e)
        {

        }

        public void SumadePagos()
        {
            decimal acumulador = 0;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                acumulador = acumulador + decimal.Parse(listView1.Items[i].SubItems[1].Text);
            }
            label18.Text = acumulador.ToString();
        }

        private void label9_DoubleClick(object sender, EventArgs e)
        {
            textBox4.Text = label9.Text;
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CalculaInterno();
            }
        }
        public void CalculaInterno()
        {
            decimal acumulado = decimal.Parse(label18.Text);
            decimal acuenta = decimal.Parse(textBox4.Text);
            decimal Totalacuenta = acumulado + acuenta;

            decimal totalReg = decimal.Parse(label10.Text);
            decimal liquidacion = totalReg - Totalacuenta;
            label8.Text = liquidacion.ToString();
           
        }

        public void ConsecutivoPago()
        {
                string Numero = "1";
                conectorSql conecta = new conectorSql();
                string Query = "Select numpago  from consecutivos where numpago<>''";
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    Numero = leer["numpago"].ToString();
                }
                conecta.CierraConexion();
                label23.Text = Numero;
        }

        public void LimpiarPagos()
        {

            textBox4.Text = "";
            label8.Text = "";
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string cvcliente = label20.Text;
            string numpedido = label2.Text;
            string cantidad = textBox4.Text;
            string fecha = DateTime.Now.ToString("dd/MM/yyyy");
            string fechacod = DateTime.Now.ToString("yyyyMMdd");
            string concepto = "PAGO A CUENTA POR " + label19.Text + " - NUM " + numpedido;
            string cvconcepto = "4";
            string remisionHist = label11.Text;
            string estatus = "PAGADO";
            string fechapago = DateTime.Now.ToString("dd/MM/yyyy");
            string fcodpago = DateTime.Now.ToString("yyyyMMdd");
            string emitiopago = valoresg.USUARIOSIS;
            string pagocon = "EFECTIVO";
            string observacion = textBox2.Text;
            string numremision = label2.Text;
            string ayo = DateTime.Now.Year.ToString();
            string mes = DateTime.Now.Month.ToString();
            string numRecibo = label11.Text;
            string tipopago = label19.Text;

            CalculaInterno();
            decimal totalPagoCuenta = decimal.Parse(textBox4.Text);
            decimal restapaga = decimal.Parse(label9.Text);

            if (totalPagoCuenta > restapaga) 
            {
                MessageBox.Show("El pago que va a realizar es mayor de lo que resta por pagar, verifique que sea igual", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            conectorSql conecta = new conectorSql();
            string Query = "Delete from pagos where numpedido='" + label11.Text + "' and estatus='POR PAGAR'";
            conecta.Excute(Query);

            ConsecutivoPago();
            string Numpago = label23.Text;

            Query = "Insert into pagos (";
            Query = Query + "cvcliente";
            Query = Query + ",numpedido";
            Query = Query + ",cantidad";
            Query = Query + ",fecha";
            Query = Query + ",fechacod";
            Query = Query + ",concepto";
            Query = Query + ",cvconcepto";
            Query = Query + ",remisionHist";
            Query = Query + ",estatus";
            Query = Query + ",fechapago";
            Query = Query + ",fcodpago";
            Query = Query + ",emitiopago";
            Query = Query + ",pagocon";
            Query = Query + ",observacion";
            Query = Query + ",numremision";
            Query = Query + ",ayo";
            Query = Query + ",mes";
            Query = Query + ",numRecibo";
            Query = Query + ",observa";
            Query = Query + ",numpago";
            Query = Query + ",tipopago)";
            Query = Query + " values(";

            Query = Query + "'" + cvcliente + "'";
            Query = Query + ",'" + numpedido + "'";
            Query = Query + ",'" + cantidad + "'";
            Query = Query + ",'" + fecha + "'";
            Query = Query + ",'" + fechacod + "'";
            Query = Query + ",'" + concepto + "'";
            Query = Query + ",'" + cvconcepto + "'";
            Query = Query + ",'" + remisionHist + "'";
            Query = Query + ",'" + estatus + "'";
            Query = Query + ",'" + fechapago + "'";
            Query = Query + ",'" + fcodpago + "'";
            Query = Query + ",'" + emitiopago + "'";
            Query = Query + ",'" + pagocon + "'";
            Query = Query + ",'" + observacion + "'";
            Query = Query + ",'" + numremision + "'";
            Query = Query + ",'" + ayo + "'";
            Query = Query + ",'" + mes + "'";
            Query = Query + ",'" + numRecibo + "'";
            Query = Query + ",'" + observacion + "'";
            Query = Query + ",'" + Numpago + "'";
            Query = Query + ",'" + tipopago + "')";
            conecta.Excute(Query);

            // CalculaInterno();
            decimal Restaliquita = decimal.Parse(label8.Text);
            if (Restaliquita <= 0)
            {
                string consulta = "";

                if (tipopago == "FACTURA")
                    consulta = "Update pedidos set estatuspago='PAGADO' where numpedido='" + numpedido + "' and cvcliente='" + cvcliente + "'";
                else
                    consulta = "Update recibos set condicionpago='PAGADO' where numrecibo='" + numpedido + "' and cvcliente='" + cvcliente + "'";

                conecta.Excute(consulta);
            }


            ActualizaNumPago();
            BuscarInfo();
            BuscarRecibosyFacturas();
            panel1.Visible = false;
            LimpiarPagos();
            MessageBox.Show("Se guardo correctamente el pago del cliente", "Pagos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool ActualizaNumPago()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = int.Parse(label23.Text) + 1;
            string Query = "update consecutivos set numpago='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }
    }
}
