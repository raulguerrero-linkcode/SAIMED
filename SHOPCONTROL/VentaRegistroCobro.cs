using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
//using System.Collections.Generic;  //MODIFICADO POR JOSE 26-11-19
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace SHOPCONTROL
{
    public partial class VentaRegistroCobro : Form
    {
        public string NUMPEDIDO = "";
        public VentaRegistroCobro()
        {
            InitializeComponent();
        }

        public VentaRegistroCobro(string npedido)
        {
            InitializeComponent();
            valoresg.NUMPEDIDOREGISTRAR = npedido;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string cfnFile = @"\\SRV-DATACENTER\tmp\EmailConf.xml";
            bool cfnExist = File.Exists(cfnFile);
            XDocument xdoc = XDocument.Load(cfnExist ? @"\\SRV-DATACENTER\tmp\EmailConf.xml" : @"C:\tmp\EmailConf.xml");

            //XDocument xdoc = XDocument.Load("./EmailConf.xml");
            string EnableMail = xdoc.Descendants("EnableSendMails").First().Value;
            if (EnableMail.Equals("1"))
            {
                MailNotifications mail = new MailNotifications();
                mail.SendMailOnlySubjectAndMSG("Número de recibo " + textBox1.Text.Trim() + " No cobrado al cliente Id:" + label19.Text , "El usuario: " + valoresg.IdEmployee + " " + valoresg.Nombre_Completo.Trim() + "<br> No registró el cobro del recibo: " + textBox1.Text.Trim() + "<br> Importe: " + label11.Text);

            }

            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;
            // Registro de pagos 
            string query = "insert into DetallesPagos (numrecibo, cvcliente, debito, credito, efectivo, totalRecibo, totalCambio, totalNumRecibo, STATUS) values(" + textBox1.Text.Trim() + "," + label19.Text + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + label11.Text + ",'NO COBRADO')";
            conecta.Excute(query);
            conecta.CierraConexion();

            NoexisteRecibo();
            valoresg.NUMPEDIDOREGISTRAR = "";
            this.Dispose();
        }

        public void NoexisteRecibo()
        {
            if (textBox1.Text == "") return;

            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;
            string Query = "Select cantidad from pagos where numpedido='" + NUMPEDIDO + "' and bandera='1'";
            bool existe = conecta.ExisteRegistro(Query);
            if (existe == false)
            {
                string cvcliente = "";
                string numpedido = NUMPEDIDO;
                string cantidad = "0";
                string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                string fechacod = DateTime.Now.ToString("yyyyMMdd");
                string Horapago = DateTime.Now.ToString("hh:mm:ss");
                string concepto = "PAGO AL PEDIDO NUM " + NUMPEDIDO;
                string cvconcepto = "4";
                string remisionHist = NUMPEDIDO;
                string estatus = "PAGADO";
                string fechapago = DateTime.Now.ToString("dd/MM/yyyy");
                string fcodpago = DateTime.Now.ToString("yyyyMMdd");
                string emitiopago = valoresg.USUARIOSIS;
                string pagocon = "EFECTIVO";
                string bandera = "1";
                pagocon = "EFECTIVO";
                if (radioButton3.Checked == true) pagocon = "EFECTIVO";
                if (radioButton4.Checked == true) pagocon = "DEPOSITO";


                string observacion = "";
                string numremision = NUMPEDIDO;
                string ayo = DateTime.Now.Year.ToString();
                string mes = DateTime.Now.Month.ToString();
                string numRecibo = NUMPEDIDO;
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
                query = query + ",'" + numpago + "')";
                conecta.Excute(query);
            }
        
        }


        private void button20_Click(object sender, EventArgs e)
        {
            
            NUMPEDIDO = textBox1.Text.Trim();
            valoresg.NUMRECIBOREG = textBox1.Text.Trim();
            valoresg.CVPACIENTECITAR = label14.Text;


            if (label5.Text == label4.Text)
            {
                MessageBox.Show("Se realizo el pago total del recibo, gracias", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                label5.Text = "0";
                label4.Text = "0";
                label11.Text = "0";

                textBox2.Text = "";
                label9.Text = "0";
                radioButton3.Checked = true;
                textBox1.Text = "";
                textBox1.Focus();
                return;
            }
            
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;
            string query = "Delete from pagos where numpedido='" + NUMPEDIDO + "' and bandera<>'1'";
            conecta.Excute(query);


            string cvcliente = label19.Text;
            string numpedido = NUMPEDIDO;
            string cantidad = label10.Text;
            string fecha = DateTime.Now.ToString("dd/MM/yyyy");
            string fechacod = DateTime.Now.ToString("yyyyMMdd");
            string Horapago = DateTime.Now.ToString("HH:mm:ss");
            string HoracodPago= DateTime.Now.ToString("HHmmss");
            string concepto = "PAGO AL RECIBO NUM " + NUMPEDIDO;
            string cvconcepto="4";
            string remisionHist=NUMPEDIDO;
            string estatus="PAGADO";
            string fechapago = DateTime.Now.ToString("dd/MM/yyyy");
            string fcodpago = DateTime.Now.ToString("yyyyMMdd") ;
            string emitiopago = valoresg.USUARIOSIS ;


            string pagocon="EFECTIVO";
            string bandera = "1";
            pagocon = "EFECTIVO";
            if (comboBox1.SelectedIndex==0) pagocon = "EFECTIVO";
            if (comboBox1.SelectedIndex == 1) pagocon = "DEBITO";
            //if (comboBox1.SelectedIndex == 2) pagocon = "CREDITO"; //MODIFICADO POR JOSE 10-12-2019
            
            string observacion ="";
            string numremision=NUMPEDIDO;
            string ayo=DateTime.Now.Year.ToString();
            string mes=DateTime.Now.Month.ToString();
            string numRecibo=NUMPEDIDO;
            string tipopago=pagocon;
            string observa="0";
            string numpago = label17.Text;
            string recibio = textBox2.Text.ToString();
            string cambio = label9.Text.ToString();

            // Registro de pagos 
            query = "insert into DetallesPagos (numrecibo, cvcliente, debito, credito, efectivo, totalRecibo, totalCambio, totalNumRecibo,STATUS) values(" + numpedido + "," + cvcliente + "," + debito.Text + "," + credito.Text + "," + efectivo.Text + "," + recibio + "," + cambio + "," + label11.Text + ",'PAGADO')";
            conecta.Excute(query);


            query = "Delete from pagos where numpedido='" + numRecibo + "' and cantidad='0'";
            conecta.Excute(query);

            query="insert into pagos (cvcliente";
            query=query + ", numpedido";
            query=query + ",cantidad";
            query=query + ",fecha";
            
            query=query + ",fechacod";
            query=query + ",concepto";
            query=query + ",cvconcepto";
            query=query + ",remisionHist";
            query=query + ",estatus";
            query=query + ",fechapago";
            query=query + ",fcodpago";
            query=query + ",emitiopago";
            query=query + ",pagocon";
            query=query + ",observacion";
            query=query + ",numremision";
            query=query + ",ayo";
            query=query + ",mes";
            query=query + ",numRecibo";
            query=query + ",tipopago";
            query=query + ",observa";
            query = query + ",bandera";
            query = query + ",recibio";
            query = query + ",cambio";
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
            query = query + ",'" + bandera+ "'";
            query = query + ",'" + recibio + "'";
            query = query + ",'" + cambio+ "'";

            query = query + ",'" + Horapago+ "'";
            query = query + ",'" + HoracodPago+ "'";
            query = query + ",'" + numpago + "')";
            conecta.Excute(query);

            label5.Text = "0";
            label4.Text = "0";
            label11.Text = "0";
            textBox2.Text = "";
            label9.Text = "0";
            radioButton3.Checked = true;
            textBox1.Text = "";

            textBox2.Focus();
            OpenCashDrawer();
            actualizaConsecutivoPAGO();

            ActivarCitaDirecto();

            //else
            //{
            //    if (label19.Text != "0")
            //    {
            //        DialogResult reply = MessageBox.Show("¿Desea activar citas con el recibo?", "Activacion de citas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (reply == DialogResult.No)
            //            return;


            //        ActivacionCitas activar = new ActivacionCitas();
            //        activar.Show();
            //    }

            //}

            MessageBox.Show(" Se guardo correctamente el cobro", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
            this.Dispose();
            
        }


        public void OpenCashDrawer()
        {
            RawPrinter envio = new RawPrinter();

            byte[] source = new byte[] { 0x1b, 0x70, 0x30, 0x37, 0x79 };
            IntPtr destination = new IntPtr(0);
            destination = Marshal.AllocCoTaskMem(5);
            Marshal.Copy(source, 0, destination, 5);
            //RawPrinterHelper.SendBytesToPrinter("EPSON TM-T88IV Receipt", destination, 5);

            envio.SendBytesToPrinter("EPSON TM-T88IV Receipt", destination, 5);
            Marshal.FreeCoTaskMem(destination);
        }

        public void Limpiar()
        {
            label5.Text = "0";
            label4.Text = "0";
            label11.Text = "0";

            textBox2.Text = "";
            label9.Text = "0";
            radioButton3.Checked = true;
            textBox1.Text = "";
            textBox2.Focus();
        }

        public void Cargarinfo()
        {

            listView1.Items.Clear();
            listView1.Columns.Clear();

            listView1.Columns.Add("Num Ticket", 40).Tag = "STRING";
            listView1.Columns.Add("Fecha", 60).Tag = "STRING";
            listView1.Columns.Add("Hora", 60).Tag = "STRING";
            listView1.Columns.Add("Servicio", 190).Tag = "STRING";
            listView1.Columns.Add("cvservicio", 0).Tag = "STRING";
            listView1.Columns.Add("cvdoctor", 0).Tag = "STRING";

            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;
            string Query = "Select * from citas where estatus='SIN PAGAR' and cvpaciente='" + label19.Text + "'  order by fechacod desc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["progresivo"].ToString());

                lvi.SubItems.Add(leer["fecha"].ToString());
                lvi.SubItems.Add(leer["horainicia"].ToString());

                string cvservicio = "" ;
                string nombre = "";

                for (int i = 0; i < Lv2.Items.Count; i++)
                {
                    cvservicio = Lv2.Items[i].SubItems[5].Text;
                    nombre = Lv2.Items[i].SubItems[2].Text;
                }
                lvi.SubItems.Add(nombre);
                lvi.SubItems.Add(cvservicio);
                lvi.SubItems.Add(leer["cvdoctor"].ToString());

                listView1.Items.Add(lvi);
            }
            conecta.CierraConexion();



        }

        public void ActivarCitaDirecto()
        {
            if (listView1.Items.Count == 0) return;
            conectorSql conecta = new conectorSql();

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                    string consulta = "";
                    string numprogresivo = listView1.Items[i].Text;
                    string FECHA = listView1.Items[i].SubItems[1].Text;
                    string cvdoctor = listView1.Items[i].SubItems[5].Text;

                    consulta = "Update citas set ReciboPago='" + NUMPEDIDO + "' , estatus='PAGADO' where cvpaciente='" + label19.Text +"' and progresivo='" + numprogresivo + "' and fecha='" + FECHA + "' and cvdoctor='" + cvdoctor + "'";
                    conecta.Excute(consulta);
            }
        }

        private void VentaRegistroCobro_Load(object sender, EventArgs e)
        {
            //Limpiar();
            if (valoresg.NUMPEDIDOREGISTRAR != "")
            {
                textBox1.Text = valoresg.NUMPEDIDOREGISTRAR;
                buscarNumpedido();
                if (label19.Text != "0") Cargarinfo();

                textBox1.Text = valoresg.NUMPEDIDOREGISTRAR;
                valoresg.NUMPEDIDOREGISTRAR = "";
            }


            if (valoresg.AGENDA_CVPACIENTE!="")
            {
                label14.Text=valoresg.AGENDA_CVPACIENTE;
                label15.Text = valoresg.AGENDA_FCITAPROX;
                label16.Text = valoresg.AGENDA_RECIBO;

                valoresg.AGENDA_CVPACIENTE="";
                valoresg.AGENDA_FCITAPROX = "";
                valoresg.AGENDA_RECIBO = "";
            }

            CalcularPagoTotal();

            cargaConsecutivoPAGO();
            comboBox1.Text = "EFECTIVO";
            textBox2.Focus();
        }


        public int NUMPAGO = 0;
        public void cargaConsecutivoPAGO()
        {
            NUMPAGO = 1;
            conectorSql conecta = new conectorSql();
            string Query = "Select numpago from consecutivos where numpago <>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                NUMPAGO = int.Parse(leer["numpago"].ToString());
            }
            conecta.CierraConexion();
            label17.Text = NUMPAGO.ToString();
        }

        public bool actualizaConsecutivoPAGO()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = NUMPAGO + 1;
            string Query = "update consecutivos set numpago='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }

        public void BuscarRecibo()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from pagos where numrecibo='" + NUMPEDIDO + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                
            }
            conecta.CierraConexion();
        }

        public void buscarNumpedido()
        {
            NUMPEDIDO = textBox1.Text;
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;
            decimal totalgen = 0;
            string Query = "Select top(1) totalgeneral from recibos where numrecibo='" + NUMPEDIDO + "'";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                totalgen = decimal.Parse(leer["totalgeneral"].ToString());
            }
            conecta.CierraConexion();

            //MODIFICADO POR JOSE 10-12-2019
            /*              
            decimal acumula = 0;
            Query = "Select cantidad from pagos where numpedido='" + NUMPEDIDO + "' and bandera='1'";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                acumula = acumula + decimal.Parse(leer["cantidad"].ToString());

            }
            conecta.CierraConexion();
            */

            label19.Text = "0";

            Lv2.Items.Clear();
            Lv2.Columns.Clear();
            Lv2.Columns.Add("Cliente", 90).Tag = "STRING";
            Lv2.Columns.Add("Cantidad", 50).Tag = "STRING";
            Lv2.Columns.Add("Describe", 450).Tag = "STRING";
            Lv2.Columns.Add("Precio", 90).Tag = "STRING";
            Lv2.Columns.Add("Total", 90).Tag = "STRING";
            Lv2.Columns.Add("Clave", 0).Tag = "STRING";

            // Query = "Select top(1) * from detallesrecibos  where numrecibo='" + NUMPEDIDO + "'";
            Query = "Select * from detallesrecibos  where numrecibo='" + NUMPEDIDO + "'";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                label19.Text = leer["cvcliente"].ToString();
                ListViewItem lvi = new ListViewItem(leer["cvcliente"].ToString());
                lvi.SubItems.Add(leer["cantidad"].ToString());
                lvi.SubItems.Add(leer["descripcion"].ToString());
                decimal valor = decimal.Parse(leer["preunitario"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                valor = decimal.Parse(leer["precio"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(leer["cvproducto"].ToString());
                Lv2.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label20.Text = Lv2.Items.Count.ToString();

            //label4.Text = acumula.ToString();  //MODIFICADO POR JOSE 10-12-2019
            label5.Text = totalgen.ToString();
            //decimal porpagar = acumula - totalgen;  //MODIFICADO POR JOSE 10-12-2019
            label11.Text = totalgen.ToString();
            textBox2.Focus();
        
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscarNumpedido();
                if (label19.Text != "0") Cargarinfo();
            } 
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.Text.Trim() == "") textBox2.Text = "0";
                calcularcambio();
                button20_Click(sender,e);
            }
        }

        public void calcularcambio()
        {

            decimal totalacum = decimal.Parse(label4.Text);
            decimal totalped = decimal.Parse(label5.Text);

            decimal Resultado2 = totalped - totalacum;

            decimal recibido = decimal.Parse(textBox2.Text);
            label10.Text = textBox2.Text;

            decimal resultado = recibido-Resultado2 ;

            if (resultado < 0) resultado = 0;
            
            decimal registroDentro = recibido - resultado;
            label10.Text = registroDentro.ToString();

            label9.Text = resultado.ToString();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim()!="")calcularcambio();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            cargaConsecutivoPAGO();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            valoresg.NUMRECIBOREG = textBox1.Text.Trim();
            valoresg.CVPACIENTECITAR = label14.Text;
            ActivacionCitas ACTIVAR = new ActivacionCitas();
            ACTIVAR.Show();
        }

        private void debito_TextChanged(object sender, EventArgs e)
        {
            /*
            this.textBox2.Text = "0";
            decimal debitoText = decimal.Parse(this.debito.Text);
            decimal totalText = decimal.Parse(this.textBox2.Text);
            decimal total = debitoText + totalText;
            textBox2.Text = total.ToString();
            */
            CalcularPagoTotal();

        }

        private void CalcularPagoTotal()
        {

            if (this.debito.Text =="")
            {
                this.debito.Text = "0";
            }

            if (this.credito.Text == "")
            {
                this.credito.Text = "0";
            }

            if (this.efectivo.Text == "")
            {
                this.efectivo.Text = "0";
            }

            try
            {
                decimal PorPagar = decimal.Parse(label11.Text);

                decimal debitoText = decimal.Parse(this.debito.Text);
                decimal creditoText = decimal.Parse(this.credito.Text);
                decimal efectivoText = decimal.Parse(this.efectivo.Text);

                decimal totalPagado = debitoText + creditoText + efectivoText;

                textBox2.Text = totalPagado.ToString();

                if (totalPagado < PorPagar)
                {
                    button20.Enabled = false;
                } else
                {
                    button20.Enabled = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Revise los montos ingresados");
            }

            
        }

        private void credito_TextChanged(object sender, EventArgs e)
        {
            CalcularPagoTotal();
        }

        private void efectivo_TextChanged(object sender, EventArgs e)
        {
            CalcularPagoTotal();
        }
    }
}
