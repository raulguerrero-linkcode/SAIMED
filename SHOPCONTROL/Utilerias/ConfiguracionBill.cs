using System;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.SqlClient;
namespace SHOPCONTROL
{
    public partial class ConfiguracionBill : Form
    {
        public ConfiguracionBill()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            valoresg.SOLICITUDTIMBRES = "NO";
            this.Dispose();
        }

        public string NOMBRECLIENTE = "";
        public string NOTIFICARCORREO = "";
        public string USUARIOFOLIO = "";
        public string CONTRASEÑAFOLIO = "";
        public string SERIAL = "";
        public string BMODIFICARECIBO = "";
        public void BuscarInformacionCliente()
        {
            
            conectorSql conecta = new conectorSql();
            string Query = "Select * from parametros where modelosis<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                NOMBRECLIENTE = leer["nombrecliente"].ToString();
                NOTIFICARCORREO = leer["notificarcorreo"].ToString();
                SERIAL = leer["modelosis"].ToString();
                USUARIOFOLIO = leer["usuariofolio"].ToString();
                CONTRASEÑAFOLIO = leer["contrafolio"].ToString();
            }
            conecta.CierraConexion();


            Query = "Select * from ParametrosFactura where nombre<>''";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                NOMBRECLIENTE = leer["nombre"].ToString();
                NOTIFICARCORREO = leer["correo"].ToString();
                SERIAL = leer["IDSerial"].ToString();
                USUARIOFOLIO = leer["usuario"].ToString();
                CONTRASEÑAFOLIO = leer["contrafac"].ToString();
            }
            conecta.CierraConexion();
            textBox4.Text = SERIAL;



            Query = "Select * from ParametrosRecibo where NombreComercial<>''";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox8.Text = leer["NombreComercial"].ToString();
                textBox9.Text = leer["Nombreencargado"].ToString();
                textBox10.Text = leer["InfoAdicional"].ToString();
                textBox11.Text = leer["LugarExpedicion"].ToString();
                textBox12.Text = leer["direccion"].ToString();
            }
            conecta.CierraConexion();


        }

        public string USUARIO = "";
        public string CONTRASEÑARFC = "";
        public string DIRECCIONGUARDADO = "";
        public string VENDEDORESAPLICA = "";
        public string APLICARCOMOPAGADO = "";
        public string HABILITAPRECIOS = "";
        public string VENTCOBRO = "";
        public string OCULTARDISTRIBUIDOR = "";
        public string MOSTRARCONVENIO = "";
        public string IVA;

        public string ABRIRFACTURA;
        public string IMPRESIONDIRECTA;
        public string VERSIONSISTEMA;
        
        public string CALCULOIVA;


        public string RDIRECCION;
        public string RENCARGADO;
        public string RINFORMACION;
        public string RLUGAR;
        public string RNOMBRECOMERCIAL;


        private void button3_Click(object sender, EventArgs e)
        {
            USUARIO = "";
            CONTRASEÑARFC = "";
            DIRECCIONGUARDADO = "";
            IVA = textBox2.Text;
            if (IVA == "") IVA = "0.16";
            if (checkBox1.Checked == true) VENDEDORESAPLICA = "SI";
            else VENDEDORESAPLICA = "NO";

            APLICARCOMOPAGADO = "NO";
            if (checkBox2.Checked == true) APLICARCOMOPAGADO = "SI";

            HABILITAPRECIOS = "NO";
            if (checkBox3.Checked == true) HABILITAPRECIOS = "SI";

            VENTCOBRO = "NO";
            if (checkBox4.Checked == true) VENTCOBRO = "SI";

            OCULTARDISTRIBUIDOR = "NO";
            if (checkBox5.Checked == true) OCULTARDISTRIBUIDOR = "SI";


            ABRIRFACTURA = "NO";
            if (checkBox7.Checked == true) ABRIRFACTURA = "SI";

            IMPRESIONDIRECTA = "NO";
            if (checkBox6.Checked == true) IMPRESIONDIRECTA = "SI";

            MOSTRARCONVENIO = "NO";

            CALCULOIVA = "1";
            if (radioButton2.Checked == true) CALCULOIVA = "2";

            VERSIONSISTEMA = textBox3.Text.ToUpper();

            BMODIFICARECIBO = "NO";
            if (checkBox9.Checked == true) BMODIFICARECIBO = "SI";


            this.RNOMBRECOMERCIAL = this.textBox8.Text;
            this.RENCARGADO = this.textBox9.Text;
            this.RINFORMACION = this.textBox10.Text;
            this.RLUGAR = this.textBox11.Text;
            this.RDIRECCION = this.textBox12.Text;


            conectorSql conecta = new conectorSql();
            string Query = "Select * from parametros where habilitarprecio<>''";
            bool existe = conecta.ExisteRegistro(Query);
            if (existe == true)
            {
                Query = "Update parametros set usuariofolio='" + USUARIO + "',contrafolio='" + CONTRASEÑARFC + "', dirrespaldo='" + DIRECCIONGUARDADO + "'";
                Query=Query +" ,ObligatorioVendedor='" + VENDEDORESAPLICA + "'";
                Query = Query + " ,pasarpagado='" + APLICARCOMOPAGADO + "'";
                Query = Query + " ,habilitarprecio='" + HABILITAPRECIOS + "'";
                Query = Query + " ,ventcobro='" + VENTCOBRO + "'";
                Query = Query + " ,ocultardistribuidor='" + OCULTARDISTRIBUIDOR + "'";
                Query = Query + " ,conveniopago='" + MOSTRARCONVENIO + "'";
                Query = Query + " ,abrirfactura='" + ABRIRFACTURA + "'";
                Query = Query + " ,impresiondirecta='" + IMPRESIONDIRECTA+ "'";
                Query = Query + " ,versionbill='" + VERSIONSISTEMA + "'";
                Query = Query + " ,calculaiva='" + CALCULOIVA + "'";
                Query = Query + " ,iva='" + IVA + "'";
                Query = Query + " ,modificaRecibo='" + BMODIFICARECIBO + "'";
                
                conecta.Excute(Query);
                MessageBox.Show("Se guardaron correctamente los datos de timbrado fiscal", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Query = "Insert into parametros (usuariofolio,contrafolio,dirRespaldo,ObligatorioVendedor";
                Query = Query + ",pasarpagado,habilitarprecio,ventcobro,ocultardistribuidor,iva,abrirfactura,impresiondirecta,versionbill,calculaiva,conveniopago,modificaRecibo)";
                Query = Query + "  values('" + USUARIO + "'";
                Query = Query + ",'" + CONTRASEÑARFC + "'";
                Query = Query + ",'" + DIRECCIONGUARDADO + "'";
                Query = Query + ",'" + VENDEDORESAPLICA + "'";
                Query = Query + ",'" + APLICARCOMOPAGADO + "'";
                Query = Query + ",'" + HABILITAPRECIOS + "'";
                Query = Query + ",'" + VENTCOBRO + "'";
                Query = Query + ",'" + OCULTARDISTRIBUIDOR + "'";
                Query = Query + ",'" + IVA + "'";
                Query = Query + ",'" + ABRIRFACTURA + "'";
                Query = Query + ",'" + IMPRESIONDIRECTA + "'";
                Query = Query + ",'" + VERSIONSISTEMA + "'";
                Query = Query + ",'" + CALCULOIVA + "'";
                Query = Query + ",'" + MOSTRARCONVENIO + "'";
                Query = Query + ",'" + BMODIFICARECIBO + "')";
                
                conecta.Excute(Query);
                conecta.CierraConexion();

                return;
            }


            string query = "Select * from ParametrosRecibo where NombreComercial<>''";
            if (!conecta.ExisteRegistro(query))
            {
                query = (((("Insert into ParametrosRecibo(NombreComercial,Nombreencargado,InfoAdicional,direccion,LugarExpedicion) values('" + this.RNOMBRECOMERCIAL + "'") + ",'" + this.RENCARGADO + "'") + ",'" + this.RINFORMACION + "'") + ",'" + this.RDIRECCION + "'") + ",'" + this.RLUGAR + "')";
                conecta.Excute(query);
                conecta.CierraConexion();
            }
            else
            {
                query = (((("Update ParametrosRecibo set NombreComercial='" + this.RNOMBRECOMERCIAL + "'") + ",Nombreencargado='" + this.RENCARGADO + "'") + ",InfoAdicional='" + this.RINFORMACION + "'") + ",direccion='" + this.RDIRECCION + "'") + ",LugarExpedicion='" + this.RLUGAR + "'";
                conecta.Excute(query);
                conecta.CierraConexion();
            }
            MessageBox.Show("Se guardaron correctamente los datos de timbrado fiscal", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }


        private void ConfiguracionBill_Load(object sender, EventArgs e)
        {
            CargarfotosPantalla();
            TotalFacturasReg();
            BuscarConfiguracion();
            BuscarInformacionCliente();
            CargarInfo();
            string miValor = Registro.ReadRegSHOPCONTROL("CON", "BADMAQUINASERVER");
            if (miValor == "1") checkBox8.Checked = true;
            else checkBox8.Checked = false;
            if (valoresg.SOLICITUDTIMBRES == "SI")
            {
                valoresg.SOLICITUDTIMBRES = "NO";
                tabControl1.SelectedTab = this.tabControl1.TabPages["tabPage2"];
            }
        }

        public void TotalFacturasReg()
        {
            int totalFacturado = 0;
            int totalCancelado = 0;

            conectorSql conecta = new conectorSql();
            string Query = "Select count(*) total from facturas where estatus='FACTURADO' ";
            Query = Query + " and ayo='" + DateTime.Now.Year + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                totalFacturado = int.Parse(leer["total"].ToString());
            }
            conecta.CierraConexion();


            Query = "Select count(*) total from facturas where estatus='CANCELADO' ";
            Query = Query + " and ayo='" + DateTime.Now.Year + "'";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                totalCancelado = int.Parse(leer["total"].ToString());
            }
            conecta.CierraConexion();
            totalCancelado = totalCancelado * 2;
            int total = totalFacturado + totalCancelado;
            label18.Text = total.ToString();

            // Licenciamiento licencia = new Licenciamiento();
            int totalComprado = 0;
            int totalRegistrado = 0;
            string LicenciaTotal = "";
            Query = "Select cvllave from LlavesSistema where cvllave<>'' ";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                LicenciaTotal = leer["cvllave"].ToString();
                totalComprado = totalComprado; // + licencia.CuantosTimbresGeneral(LicenciaTotal);
                totalRegistrado = totalRegistrado; // + licencia.SaberCuantosTimbres(LicenciaTotal);
            }
            conecta.CierraConexion();

            int Restan = totalComprado - total;
            if (Restan < 10)
            {
                MessageBox.Show("Comuniquese con  su proveedor Soluciones SIA.\nCorreo: ventas@soluciones-sia.com\nQuedan pocos timbres para facturar\nQuedan " + Restan + " Timbres.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            label20.Text = Restan.ToString();

        }
        public void CargarInfo()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Llave", 200).Tag = "STRING";
            Lv.Columns.Add("Fecha", 120).Tag = "STRING";
            Lv.Columns.Add("Total", 0).Tag = "STRING";

            // Licenciamiento licencia = new Licenciamiento();

            int acumulador = 0;
            conectorSql conecta = new conectorSql();
            string Query = "Select * from llavesSistema where cvllave<>''";
            Query = Query + " order by fechacod desc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string llave=leer["cvllave"].ToString();
                ListViewItem lvi = new ListViewItem(llave);
                string fechaR = leer["fechacod"].ToString();
                string fecha = fechaR.Substring(6, 2) + "/" + fechaR.Substring(4, 2)  + "/" + fechaR.Substring(0, 4);
                lvi.SubItems.Add(fecha);
                int cuantos = 10; // licencia.SaberCuantosTimbres(llave);
                lvi.SubItems.Add(cuantos.ToString());
                Lv.Items.Add(lvi);
                acumulador = acumulador + cuantos;
            }
            conecta.CierraConexion();
            label8.Text =acumulador.ToString() ;
        }
        public void BuscarConfiguracion()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            radioButton2.Checked = false;
            radioButton1.Checked = false;

            conectorSql conecta = new conectorSql();
            string Query = "Select * from parametros where habilitarprecio<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
               

                string valor = leer["ObligatorioVendedor"].ToString();
                if (valor == "SI") checkBox1.Checked = true;

                valor = leer["pasarpagado"].ToString();
                if (valor == "SI") checkBox2.Checked = true;

                valor = leer["habilitarprecio"].ToString();
                if (valor == "SI") checkBox3.Checked = true;

                valor = leer["ventcobro"].ToString();
                if (valor == "SI") checkBox4.Checked = true;


                valor = leer["ocultardistribuidor"].ToString();
                if (valor == "SI") checkBox5.Checked = true;


                valor = leer["abrirfactura"].ToString();
                if (valor == "SI") checkBox7.Checked = true;


                valor = leer["impresiondirecta"].ToString();
                if (valor == "SI") checkBox6.Checked = true;

                valor = leer["iva"].ToString();
                textBox2.Text = valor;

                valor = leer["versionbill"].ToString();
                textBox3.Text = valor;

                valor = leer["calculaiva"].ToString();
                if (valor == "1") radioButton1.Checked = true;
                else radioButton2.Checked = true;

                valor = leer["modificarecibo"].ToString();
                if (valor == "SI") checkBox9.Checked = true;
                
            }
            conecta.CierraConexion();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("Ingrese la cantidad de folios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool conectadoInternet = InternetDisponible.IsConnectedToInternet();
            if (conectadoInternet == false)
            {
                MessageBox.Show("No tiene conexion a internet verifique para realizar esta operación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // EnviarCorreo();
        }
        /*
        public void EnviarCorreo()
        {
            string Cadena = "Solicitud de licencia \n\n\n";
            Cadena = Cadena + "Cliente: " + NOMBRECLIENTE + "\n";
            Cadena = Cadena + "Notificar al correo: " + NOTIFICARCORREO + "\n";
            Cadena = Cadena + "Usuario Folios: " + USUARIOFOLIO + "\n";
            Cadena = Cadena + "Serie Registrada: " + textBox4.Text + "\n";
            Cadena = Cadena + "Fecha de Envio; " + DateTime.Now.ToString("dd/MM/yyyy") + "\n\n";
            Cadena = Cadena + "Cantidad de Timbres: " + textBox5.Text + "\n";

            MailMessage objEmail = new MailMessage();
            objEmail.From = new MailAddress("ventas@soluciones-sia.com", "Solicitud de Licencia por Timbrado");
            objEmail.ReplyTo = new MailAddress("ventas@soluciones-sia.com");
            //Destinatario
            objEmail.To.Add("ventas@soluciones-sia.com");


            objEmail.Priority = MailPriority.Normal;
            objEmail.Subject = "Licencia Bill Line " + textBox5.Text;
            objEmail.Body = Cadena;
            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "mail.soluciones-sia.com ";
            objSmtp.Port = 587;
            objSmtp.Credentials = new System.Net.NetworkCredential("ventas@soluciones-sia.com", "Nkbsia123");
            objSmtp.Send(objEmail);

            //conectorSql conecta = new conectorSql();
            //string Query = "Update parametros set nombrecliente='" + textBox5.Text.ToUpper() + "'";
            //Query = Query + " ,notificarcorreo='" + textBox6.Text + "'";
            //Query = Query + " ,sistema='" + textBox3.Text + "'";
            //conecta.Excute(Query);

            MessageBox.Show("Se envio correctamente la información solcitada, gracias.\nEnvie la Transacción o Voucher de pago a ventas@soluciones-sia.com", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        */
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox6.Text=="")
            {
                MessageBox.Show("Ingrese la llave recibida, por favor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CompararLlaves() == true)
            {
                MessageBox.Show("La llave que intenta ingresar ya se encuentra registrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            conectorSql conecta = new conectorSql();

            string Query = "Delete from llavesSistema where fechacod='" + DateTime.Now.ToString("yyyyMMdd") + "'";
            conecta.Excute(Query);

            Query = "Insert into llavesSistema(cvllave,fechacod)  values('" + textBox6.Text + "','" + DateTime.Now.ToString("yyyyMMdd") + "')";
            conecta.Excute(Query);

            CargarInfo();
            EnviarCorreoPorLiberacion();
        }

        public bool CompararLlaves()
        {
            bool comparar = false;
            for (int i = 0; i < Lv.Items.Count; i++)
            {
                if (Lv.Items[i].Text == textBox6.Text) comparar = true;
            }
            return comparar;
        }

        public void EnviarCorreoPorLiberacion()
        {
            string ParteLlaves = "";
            for (int i = 0; i < Lv.Items.Count; i++)
            {
                ParteLlaves = ParteLlaves + "Llave " + (i+1).ToString() + " :" + Lv.Items[i].Text + "\n";
                ParteLlaves = ParteLlaves + "Fecha :" + Lv.Items[i].SubItems[1].Text + "\n"; ;
                ParteLlaves = ParteLlaves + "Total :" + Lv.Items[i].SubItems[2].Text + "\n\n\n"; ;
            }

            string Cadena = "Ingreso Codigo de Liberacion para Licencia \n\n\n";
            Cadena = Cadena + "Cliente: " + NOMBRECLIENTE + "\n";
            Cadena = Cadena + "Notificar al correo: " + NOTIFICARCORREO + "\n";
            Cadena = Cadena + "Usuario Folios: " + USUARIOFOLIO + "\n";
            Cadena = Cadena + "Serie Registrada: " + textBox4.Text + "\n";
            Cadena = Cadena + "Fecha de Envio: " + DateTime.Now.ToString("dd/MM/yyyy") + "\n\n";
            Cadena = Cadena + "Cantidad de Timbres: " + textBox5.Text + "\n\n";

            Cadena = Cadena + ParteLlaves;
         

            MailMessage objEmail = new MailMessage();
            objEmail.From = new MailAddress("ventas@soluciones-sia.com", "Liberacion de Licencia por Timbrado");
            // objEmail.ReplyTo = new MailAddress("ventas@soluciones-sia.com");
            //Destinatario
            objEmail.To.Add("ventas@soluciones-sia.com");


            objEmail.Priority = MailPriority.Normal;
            objEmail.Subject = "Liberacion de Licencia Bill Line " + textBox5.Text;
            objEmail.Body = Cadena;
            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "mail.soluciones-sia.com ";
            objSmtp.Port = 587;
            objSmtp.Credentials = new System.Net.NetworkCredential("ventas@soluciones-sia.com", "Nkbsia123");
            objSmtp.Send(objEmail);

            //conectorSql conecta = new conectorSql();
            //string Query = "Update parametros set nombrecliente='" + textBox5.Text.ToUpper() + "'";
            //Query = Query + " ,notificarcorreo='" + textBox6.Text + "'";
            //Query = Query + " ,sistema='" + textBox3.Text + "'";
            //conecta.Excute(Query);

            MessageBox.Show("Se almaneno correctamente la llave, gracias", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Lv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                panel3.Visible = true;
            }


        }

        public void EliminarConcepto()
        {
            try
            {
                conectorSql conecta = new conectorSql();
                if (Lv.SelectedItems.Count > 0)
                {
                    ListView.SelectedIndexCollection seleccion = Lv.SelectedIndices;
                    foreach (int item in seleccion)
                    {
                        Lv.Items.RemoveAt(item);
                       
                        string Query = "Delete from llavesSistema where cvllave='" + LLAVER + "' and fechacod='" + FECHALLAVE + "'";
                        conecta.Excute(Query);
                    }

                    CargarInfo();
                }
            }
            catch (Exception)
            {

                //  throw;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            panel3.Visible = false;
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox7.Text=="Administr@tor2014")
            {
                EliminarConcepto();
                panel3.Visible = false;
                CargarInfo();
            }
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
        public string LLAVER = "";
        public string FECHALLAVE = "";
        public void DetallesModifica(int index)
        {
           LLAVER = Lv.Items[index].Text;
           FECHALLAVE= Lv.Items[index].SubItems[1].Text;
           DateTime fechaL = DateTime.Parse(FECHALLAVE);
           FECHALLAVE = fechaL.ToString("yyyyMMdd");
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            // Licenciamiento licencia = new Licenciamiento();
            if (e.KeyCode == Keys.F2 && textBox1.Text == "MASTERSIA")
            {
                // string Producir = 10; //licencia.LicenciaFinal(int.Parse(textBox5.Text));
                textBox6.Text = "";
            }

            if (e.KeyCode == Keys.Enter)
            {
                button5_Click(sender, e);
            }
        }
        string pathFoto = "";
        private void button1_Click(object sender, EventArgs e)
        {
         
                using (OpenFileDialog opfDialog = new OpenFileDialog())
                {
                    try
                    {
                        pathFoto = ClaseFotos.AbrirExplorar(opfDialog);
                      pictureBox1.Image = System.Drawing.Image.FromFile(pathFoto);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No ha seleccionado ninguna foto ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int claver = listView1.Items.Count + 1;
            ClaseFotos.GuardarFotoPantalla(pathFoto,claver.ToString());
            CargarfotosPantalla();
            pictureBox1.Image = null;
           
        }

        public void CargarfotosPantalla()
        {

            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("Clave", 40).Tag = "NUMBER";
            listView1.Columns.Add("foto", 350).Tag = "STRING";
            conectorSql conecta = new conectorSql();
            string Query = "Select * from imgpantalla where clave<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["clave"].ToString());
                lvi.SubItems.Add(leer["foto"].ToString());

                listView1.Items.Add(lvi);
            }
            conecta.CierraConexion();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Delete from imgpantalla where clave<>''";
            conecta.Excute(Query);
            CargarfotosPantalla();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
           if (checkBox8.Checked==true)
               Registro.WriteRegSHOPCONTROL("CON", "BADMAQUINASERVER", "1");
            else
               Registro.WriteRegSHOPCONTROL("CON", "BADMAQUINASERVER", "0");
        }
    }
}
