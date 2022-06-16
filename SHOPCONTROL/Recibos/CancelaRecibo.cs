using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace SHOPCONTROL
{
    public partial class CancelaRecibo : Form
    {
        public CancelaRecibo()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            recolecta();
            Lv.Columns.Clear();
            Lv.Items.Clear();
            Lv.Columns.Add("Num. Recibo", 80);
            Lv.Columns.Add("Clave", 70);
            Lv.Columns.Add("Unidad", 70);
            Lv.Columns.Add("Cantidad", 50);
            Lv.Columns.Add("Descripción", 180);
            Lv.Columns.Add("Unitario", 90);
            Lv.Columns.Add("Total", 90);
            conectorSql conecta = new conectorSql();
            string Query = "Select * from DetallesRecibos where numrecibo='" + textBox2.Text + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                NUMREMISION = leer["numrecibo"].ToString();
                ListViewItem lvi = new ListViewItem(leer["numrecibo"].ToString());
                lvi.SubItems.Add(leer["cvproducto"].ToString());
                lvi.SubItems.Add(leer["unidad"].ToString());
                lvi.SubItems.Add(leer["cantidad"].ToString());
                lvi.SubItems.Add(leer["descripcion"].ToString());
                decimal valor = decimal.Parse(leer["preunitario"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                valor = decimal.Parse(leer["precio"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));          
                Lv.Items.Add(lvi);
                label11.Text = leer["cvcliente"].ToString();
            }
            conecta.CierraConexion();

            Query = "Select * from recibos where numrecibo='" + textBox2.Text + "'";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                NUMCLIENTE = leer["cvcliente"].ToString();
                NOMBRECLIENTE = leer["nombrerecibo"].ToString();
            }

            label1.Text=Lv.Items.Count.ToString();

            Query = "Select * from cancelaciones where numpedido='" + NUMREMISION + "' and cvcliente='" + NUMCLIENTE + "'";
            bool existeReg = conecta.ExisteRegistro(Query);
            if (existeReg == true)
            {
                MessageBox.Show("El recibo  ya fue cancelado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
                return;
            }
        }

        public string NUMCLIENTE;
        public string NOMBRECLIENTE;
        public string OBSERVACION;
        public string NUMREMISION;
        public string FECHACOD;
        public string FECHA;
        public string EMITIO;
        public string NUMCANCELA;
        public string CANCELATODO;
        public string TOTALC;
        public string CANTIDADLETRA;
        public string VENDEDOR;


        public void recolecta()
        {
            NUMREMISION = textBox2.Text;
            OBSERVACION = textBox7.Text;
            FECHA = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            FECHACOD = dateTimePicker1.Value.ToString("yyyyMMdd");
            NUMCANCELA = label8.Text.Trim();
            EMITIO = Modremision.EMITE;
            CANCELATODO="NO";
            if (checkBox1.Checked == true) CANCELATODO = "SI";
            VENDEDOR = "";
            TOTALC = label10.Text;
            CANTIDADLETRA = label9.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string Query = "";

            Query = "Select * from cancelaciones where numpedido='" + NUMREMISION + "' and cvcliente='" + NUMCLIENTE + "'";
            bool existeReg = conecta.ExisteRegistro(Query);
            if (existeReg == true)
            {
                MessageBox.Show("El recibo  ya fue cancelado", "Aviso", MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            Query = " Insert into cancelaciones (numpedido,numcancela,cvcliente,ncliente,observacion,emitio,fecha,fechacod,cancelatodo,totalc,CantidadLetra,vendedor) values(";
            Query = Query + "'" + NUMREMISION + "'";
            Query = Query + ",'" + NUMCANCELA+ "'";
            Query = Query + ",'" + NUMCLIENTE + "'";
            Query = Query + ",'" + NOMBRECLIENTE + "'";
            Query = Query + ",'" + OBSERVACION + "'";
            Query = Query + ",'" + EMITIO+ "'";
            Query = Query + ",'" + FECHA+ "'";
            Query = Query + ",'" + FECHACOD+ "'";
            Query = Query + ",'" + CANCELATODO + "'";
            Query = Query + ",'" + TOTALC+ "'";
            Query = Query + ",'" + CANTIDADLETRA+ "'";
            Query = Query + ",'" + VENDEDOR+ "')";
            conecta.Excute(Query);
            conecta.CierraConexion();

            Query = "";
            for (int i = 0; i < listView1.Items.Count; i++)
            {

                Query = "Select * from DetalleCancela where numpedido='" + NUMREMISION + "' and clave='" + listView1.Items[i].SubItems[1].Text + "'";
                bool existedetalle = conecta.ExisteRegistro(Query);
                if (existedetalle == false)
                {
                    Query = "Insert into DetalleCancela(numpedido,numcancela,clave,unidad,describe,cantidad,unitario,total) values(";
                    Query = Query + "'" + NUMREMISION + "'";
                    Query = Query + ",'" + NUMCANCELA + "'";
                    Query = Query + ",'" + listView1.Items[i].SubItems[1].Text + "'";
                    Query = Query + ",'" + listView1.Items[i].SubItems[2].Text + "'";
                    Query = Query + ",'" + listView1.Items[i].SubItems[4].Text + "'";
                    Query = Query + ",'" + listView1.Items[i].SubItems[3].Text + "'";
                    Query = Query + ",'" + listView1.Items[i].SubItems[5].Text + "'";
                    Query = Query + ",'" + listView1.Items[i].SubItems[6].Text + "')";
                    conecta.Excute(Query);

                    string cvcliente = NUMCLIENTE;
                    string numpedido = NUMREMISION;
                    float cantidre = float.Parse(listView1.Items[i].SubItems[6].Text) * -1;
                    string cantidad = cantidre.ToString();
                    string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                    string fechacod = DateTime.Now.ToString("yyyyMMdd");
                    string Horapago = DateTime.Now.ToString("HH:mm:ss");
                    string HoracodPago = DateTime.Now.ToString("HHmmss");
                    string concepto = "DEVOLUCION DE RECIBO " + NUMREMISION;
                    string cvconcepto = "10";
                    string remisionHist = NUMREMISION;
                    string estatus = "PAGADO";
                    string fechapago = DateTime.Now.ToString("dd/MM/yyyy");
                    string fcodpago = DateTime.Now.ToString("yyyyMMdd");
                    string emitiopago = valoresg.USUARIOSIS;
                    string pagocon = "EFECTIVO";
                    string bandera = "1";
                    pagocon = "EFECTIVO";
                    string observacion = "";
                    string numremision = NUMREMISION;
                    string ayo = DateTime.Now.Year.ToString();
                    string mes = DateTime.Now.Month.ToString();
                    string numRecibo = NUMREMISION;
                    string tipopago = pagocon;
                    string observa = "0";
                    string numpago = "0";

                    string query = "insert into pagos (cvcliente";
                    query = query + ", numpedido";
                    query = query + ",cantidad";
                    query = query + ",fecha";
                    query = query + ",fechacod";
                    query = query + ",concepto";
                    query = query + ",cvconcepto";
                    query = query + ",remisionHist";
                    query = query + ",estatus";
                    query = query + ",fechapago";
                    query = query + ",fcodpago";
                    query = query + ",emitiopago";
                    query = query + ",pagocon";
                    query = query + ",observacion";
                    query = query + ",numremision";
                    query = query + ",ayo";
                    query = query + ",mes";
                    query = query + ",numRecibo";
                    query = query + ",tipopago";
                    query = query + ",observa";
                    query = query + ",bandera";
                    query = query + ",Horapago";
                    query = query + ",Horacodpago";
                    query = query + ",numpago) values(";
                    query = query + "'" + cvcliente + "'";
                    query = query + ",'" + numpedido + "'";
                    query = query + ",'" + cantidad + "'";
                    query = query + ",'" + fecha + "'";
                    query = query + ",'" + fechacod + "'";
                    query = query + ",'" + concepto + "'";
                    query = query + ",'" + cvconcepto + "'";
                    query = query + ",'" + remisionHist + "'";
                    query = query + ",'" + estatus + "'";
                    query = query + ",'" + fechapago + "'";
                    query = query + ",'" + fcodpago + "'";
                    query = query + ",'" + emitiopago + "'";
                    query = query + ",'" + pagocon + "'";
                    query = query + ",'" + observacion + "'";
                    query = query + ",'" + numremision + "'";
                    query = query + ",'" + ayo + "'";
                    query = query + ",'" + mes + "'";
                    query = query + ",'" + numRecibo + "'";
                    query = query + ",'" + tipopago + "'";
                    query = query + ",'" + observa + "'";
                    query = query + ",'" + bandera + "'";
                    query = query + ",'" + Horapago + "'";
                    query = query + ",'" + HoracodPago + "'";
                    query = query + ",'" + numpago + "')";
                    conecta.Excute(query);
                }
            }

             ActualizaCancela();
            if (checkBox1.Checked == true)
            {
                Query = "Update pagos set estatus='CANCELADO' where numrecibo ='" + NUMREMISION + "'";
                conecta.Excute(Query);

                //Query = "Update Creditos set estatus='CANCELADO' where numrecibo='" + NUMREMISION + "'";
                //conecta.Excute(Query);

                Query = "Update Recibos set estatusrecibo='CANCELADO' where numrecibo='" + NUMREMISION + "'";
                conecta.Excute(Query);

                Query = "Update citas set  estatus='SIN PAGAR', recibopago='0' where recibopago='" + NUMREMISION + "' and cvpaciente='" + label11.Text + "'";
                conecta.Excute(Query);


                Query = "update DetallesRecibos  set descripcion =  'CANCELADO ' + descripcion ,precio = 0 ,tganancia = 0 where numrecibo = '" + NUMREMISION + "' and cvpaciente='" + label11.Text + "'";
                conecta.Excute(Query);


            }
            else
            {
                decimal Cantidad = decimal.Parse(label10.Text) * -1;

                //Query="Insert into pagos (cvcliente";
                //Query=Query + ",numpedido";
                //Query=Query + ",cantidad";
                //Query=Query + ",fecha";
                //Query=Query + ",fechacod";
                //Query=Query + ",concepto";
                //Query=Query + ",cvconcepto";
                //Query=Query + ",remisionHist";
                //Query=Query + ",estatus";
                //Query=Query + ",fechapago";
                //Query=Query + ",fcodpago";
                //Query=Query + ",emitiopago";
                //Query=Query + ",pagocon";
                //Query=Query + ",observacion";
                //Query=Query + ",numremision";
                //Query=Query + ",ayo";
                //Query=Query + ",mes";
                //Query=Query + ",numRecibo";
                //Query=Query + ",tipopago";
                //Query=Query + ",observa";
                //Query=Query + ",numpago";
                //Query=Query + ",bandera";
                //Query=Query + ",Horapago";
                //Query=Query + ",Horacodpago)";
                //Query = Query + " values(";
                //Query = Query + "'" + NUMCLIENTE + "'";
                //Query = Query + "'" + NUMREMISION + "'";
                //Query = Query + ",'" + Cantidad + "'";
                //Query = Query + ",'" + FECHA + "'";
                //Query = Query + ",'" + FECHACOD+ "'";
                //Query = Query + ",'4'";
                //Query = Query + "'" + NUMREMISION + "'";
                //Query = Query + "'PAGADO'";
                //Query = Query + ",'" + FECHA + "'";
                //Query = Query + ",'" + FECHACOD + "'";
                //Query = Query + ",'" + EMITIO+ "'";
                //Query = Query + ",'EFECTIVO'";


            }
            //Query = "Delete from CobroenVentana where numpedido='" + NUMREMISION + "'";
            //conecta.Excute(Query);
            // Enviar correo de cancelación
            MailNotifications mail = new MailNotifications();
            mail.SendMailOnlySubjectAndMSG("Se canceló el pedido del cliente: " + NUMCLIENTE, "Se ha cancelado el recibo del cliente : " + NOMBRECLIENTE + ", con fecha " + FECHA + ", emitido por " + EMITIO + "y con un importe de " + TOTALC + " (" + CANTIDADLETRA);

            //BuscarFactura();
            MessageBox.Show("Se cancelo correctamente el recibo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Dispose();
        }

        public void BuscarFactura()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from facturas where numpedido='" + NUMREMISION + "' and estatus='FACTURADO' and numfactura<>'0'";
            bool existe = conecta.ExisteRegistro(Query);
            if (existe == true)
            {
                MessageBox.Show("El pedido ya se encuentra facturado es necesario, cancelar la factura.\nSe enviara a cancelar factura ", "Cancelar Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //CancelarFactura cancelarfactura = new CancelarFactura();
                //cancelarfactura.ShowDialog();
            }
        }

        public bool ActualizaCancela()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = int.Parse(label8.Text) + 1;
            string Query = "update consecutivos set numcancela='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }


        private void CancelaRemision_Load(object sender, EventArgs e)
        {
            ConsecutivoCancelacion();
            if (Modremision.CANCELANUMRECIBO != "") textBox2.Text = Modremision.CANCELANUMRECIBO;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Lv.Items.Count; i++)
            {
                Lv.Items[i].Checked = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Lv.Items.Count; i++)
            {
                Lv.Items[i].Checked = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ConsecutivoCancelacion();
        }

        public void ConsecutivoCancelacion()
        {
            
                string Numero = "1";
                conectorSql conecta = new conectorSql();
                string Query = "Select numcancela from consecutivos where numcancela <>''";
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    Numero = leer["numcancela"].ToString();
                }
                conecta.CierraConexion();
                label8.Text = Numero;
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("Num. Recibo", 80);
            listView1.Columns.Add("Clave", 70);
            listView1.Columns.Add("Unidad", 70);
            listView1.Columns.Add("Cantidad", 50);
            listView1.Columns.Add("Descripción", 180);
            listView1.Columns.Add("Unitario", 90);
            listView1.Columns.Add("Total", 90);

 
            for (int i = 0; i < Lv.Items.Count; i++)
            {
                if (Lv.Items[i].Checked == true)
                {
                    ListViewItem lvi = new ListViewItem(Lv.Items[i].Text);
                    lvi.SubItems.Add(Lv.Items[i].SubItems[1].Text);
                    lvi.SubItems.Add(Lv.Items[i].SubItems[2].Text);
                    lvi.SubItems.Add(Lv.Items[i].SubItems[3].Text);
                    lvi.SubItems.Add(Lv.Items[i].SubItems[4].Text);
                    lvi.SubItems.Add(Lv.Items[i].SubItems[5].Text);
                    lvi.SubItems.Add(Lv.Items[i].SubItems[6].Text);
                    listView1.Items.Add(lvi);
                }
                
            }
            SumaTotales();
            if (Lv.Items.Count == listView1.Items.Count) checkBox1.Checked = true;
        }


        public void SumaTotales()
        {
            decimal acumulado = 0;
            decimal acumulado2 = 0;
            decimal acumulado3 = 0;



            for (int i = 0; i < listView1.Items.Count; i++)
            {
                decimal total = decimal.Parse(listView1.Items[i].SubItems[6].Text);
                acumulado = acumulado + total;

            }

            decimal subtotal = acumulado;            
            label10.Text = subtotal.ToString("##.00", CultureInfo.InvariantCulture); //subtotal
            Numalet let = null;
            let = new Numalet();
            //al uso en México (creo):
            let.MascaraSalidaDecimal = "00/100 M.N.";
            let.SeparadorDecimalSalida = " pesos";
            //observar que sin esta propiedad queda "veintiuno pesos" en vez de "veintiún pesos":
            let.ApocoparUnoParteEntera = true;
            //let.ConvertirDecimales = true;
            label9.Text = let.ToCustomCardinal(subtotal.ToString("##.00", CultureInfo.InvariantCulture));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                button4_Click(sender, e);
                button6_Click(sender, e);
            }
            else
            { 
                
                button5_Click(sender,e);
                listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("Num. Recibo", 80);
            listView1.Columns.Add("Clave", 70);
            listView1.Columns.Add("Unidad", 70);
            listView1.Columns.Add("Cantidad", 50);
            listView1.Columns.Add("Descripción", 180);
            listView1.Columns.Add("Unitario", 90);
            listView1.Columns.Add("Total", 90);
            }
        }
    }
}
