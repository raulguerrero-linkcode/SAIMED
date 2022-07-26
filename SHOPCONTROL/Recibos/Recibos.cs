using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

using CrystalDecisions.CrystalReports.Engine;
using System.Runtime.InteropServices;
using SHOPCONTROL.Inventarios;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using CrystalDecisions.Shared;
//using System.Collections.Generic; //MODIFICADO POR JOSE 26-11-19
namespace SHOPCONTROL
{
    public partial class Recibos : Form
    {
        private ListViewColumnSorter lvwColumnSorter;
        private ListViewItem itemActual;

        public Recibos()
        {
            InitializeComponent();
            this.Lv.ListViewItemSorter = new Sorter();
        }

        public string CVCLIENTE = "";
        public string NOMBRE = "";
        public string EMPRESA = "";
        public string CORREO = "";


        public string CLAVEPRODUCTO = "";
        public string NUMREMISION = "";
        public string FECHA = "";
        public string FECHACOD = "";

        public string UNIDAD = "";
        public string CANTIDAD = "";
        public string NOMBREPRO = "";
        public string PRECIOUNITARIO = "";
        public string PRECIOTOTAL = "";

        public string CADCLAVE = "";
        public string CADUNIDAD = "";
        public string CADCANTIDAD = "";
        public string CADNOMBREPRO = "";
        public string CADPRECIOUNITARIO = "";
        public string CADPRECIOTOTAL = "";


        public string SUBTOTAL = "";
        public string IVA = "";
        public string TOTAL = "";

        public string TPAGADO = "";
        public string EMITIO = "";

        public string ABONO = "";
        public string NUMPAGOS = "";
        public string PAGOSDE = "";
        public string TIPOPAGOSCRED = "";
        public string PORPAGAR = "";
        public string ESTATUSCRED = "";

        public string FACTURADA = "";
        public string MES = "";
        public string AYO = "";
        public string CANTLETRA = "";
        public string TDISTRIBUIDOR = "";
        public string TGANANCIA = "";
        public string NUMPEDIDO = "";
        public string TIPODEPAGO = "";
        public string BANCO = "";
        public string NUMCUENTA = "";

        public string AYOPEDIDO = "";
        public string MESPEDIDO = "";

        public int numfactura = 0;
        public string estatus = "RECIBO";
        public string idsistemapadre = "";
        public int edocomprobante = 0;
        public string tipo = "";
        public string RFCEmitio = "";
        public string CondicionesPago = "";
        public string FormaPago = "";
        public decimal Descuento = 0;
        public string motivoDescuento = "";
        public string metodoPago = "";
        public decimal subtotal = 0;
        public decimal total = 0;
        public string REClave = "";
        public string ReNombre = "";
        public string ReRFC = "";
        public string ReCalle = "";
        public string ReCodpostal = "";
        public string ReColonia = "";
        public string ReEstado = "";
        public string ReLocalidad = "";
        public string ReMunicipio = "";
        public string ReNoExterior = "";
        public string ReNoInterior = "";
        public string ReTel = "";
        public string RePais = "";
        public string ReReferencia = "";
        public string Recorreo = "";
        public decimal TImpuestosRetenido = 0;
        public decimal TImpuestoTrasladado = 0;
        public string RImpuesto = "";
        public decimal RImporte = 0;
        public string TImpuesto = "";
        public decimal TImporte = 0;
        public int TTasa = 0;
        public string Notas = "";
        public string moneda = "";
        public decimal TipoCambio = 0;
        public string Vendedor = "";
        public string OrdCompra = "";
        public string Otros = "";
        public string numCtaPago = "";

        public string numcotizacion = "0";
        public string numremision = "0";
        public string fechacotiza = "";
        public string fcodcotiza = "";
        public string fecharemision = "";
        public string fcodremision = "";
        public string estatuspedido = "";
        public string VENDEDORGEN = "";

        string TIPORECIBO = "";
        string COLONIARECIBO = "";
        string PESOAPROX = "";
        string ENTREGADO = "";

        public string FECHACODMOD = "";
        public string FECHAMODIFICA = "";

        public string FECHAENTREGA = "";
        public string FECHACODENTREGA = "";
        public string BMODIFICARECBO = "";
        public string NCLIENTE = "";
        public void Recolectar()
        {

            if (textBox2.Text == "") textBox2.Text = "PUBLICO EN GENERAL";
            if (textBox10.Text == "") textBox10.Text = "DIRECCION";
            CVCLIENTE = textBox1.Text.Trim();
            if (CVCLIENTE == "") CVCLIENTE = "0";

            NOMBRE = textBox2.Text;
            EMPRESA = textBox10.Text;
            CORREO = textBox3.Text;
            this.NCLIENTE = this.textBox15.Text;


            NUMPEDIDO = label17.Text;
            SUBTOTAL = label9.Text;
            IVA = label12.Text;
            TOTAL = textBox9.Text;
            TDISTRIBUIDOR = label32.Text;
            TGANANCIA = label31.Text;
            MES = DateTime.Now.Month.ToString();
            AYO = DateTime.Now.Year.ToString();
            FECHA = DateTime.Now.ToString("dd/MM/yyyy");
            FECHACOD = DateTime.Now.ToString("yyyyMMdd");
            EMITIO = Modremision.EMITE;
            CANTLETRA = label33.Text;
            FACTURADA = "NO";

            numcotizacion = "0";
            numremision = "0";
            fechacotiza = "";
            fcodcotiza = "";
            fecharemision = "";
            fcodremision = "";

            MESPEDIDO = MES;
            AYOPEDIDO = AYO;
            numfactura = 0;
            estatus = "RECIBO";
            estatuspedido = "RECIBO";
            idsistemapadre = "0";
            edocomprobante = 1;

            tipo = "FA";
            RFCEmitio = "";




            if (FormaPago == "") FormaPago = "PAGO EN UNA SOLA EXHIBICIÓN";

            Descuento = 0;
            motivoDescuento = "";
            subtotal = 0;
            total = 0;
            REClave = textBox1.Text;
            ReNombre = textBox2.Text;
            ReLocalidad = "";

            ReNoInterior = "0";
            ReTel = textBox5.Text;
            ReReferencia = "";

            RePais = "MEXICO";

            conectorSql conecta = new conectorSql();
            string Query = "Select * from clientes where cvcliente='" + REClave + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                RePais = leer["PaisF"].ToString();
                ReReferencia = "";
            }
            conecta.CierraConexion();


            TImpuestosRetenido = 0;
            TImpuestoTrasladado = 0.16M;
            RImpuesto = "IVA";
            RImporte = 0;
            TImpuesto = "IVA";
            TImporte = 0.16M;
            TTasa = 16;
            Notas = textBox16.Text;
            moneda = "Pesos";
            TipoCambio = 1;
            Vendedor = label1.Text;
            OrdCompra = "OC-" + label17.Text;
            Otros = "";

            VENDEDORGEN = label1.Text;
            VENDEDORGEN = comboBox8.Text;
            Vendedor = VENDEDORGEN;

            COLONIARECIBO = textBox12.Text;
            PESOAPROX = "0";

            ENTREGADO = "NO";
            if (VENDEDORGEN == "MOSTRADOR") ENTREGADO = "SI";
            if (VENDEDORGEN == "PEDIDO") ENTREGADO = "NO";

            if (VENDEDORGEN == "PEDIDO")
                Notas = Notas + "   FECHA DE ENTREGA APROX. " + dateTimePicker4.Value.ToString("dd/MM/yyyy");


            FECHAENTREGA = dateTimePicker4.Value.ToString("dd/MM/yyyy");
            FECHACODENTREGA = dateTimePicker4.Value.ToString("yyyyMMdd");
        }

        public bool Validacion()
        {
            if (NOMBRE == "")
            {
                MessageBox.Show("Ingrese el nombre completo del cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return false;
            }



            if (BANSELECCIONAVENDEDOR == true)
            {
                if (comboBox8.Text == "")
                {
                    MessageBox.Show("Seleccione Vendedor por favor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox8.Focus();
                    return false;
                }
            }


            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Num. Recibo", 55).Tag = "NUMBER";
            Lv.Columns.Add("Nombre cliente", 120).Tag = "STRING";
            Lv.Columns.Add("Fecha", 95).Tag = "STRING";
            Lv.Columns.Add("subtotal", 75).Tag = "STRING";
            Lv.Columns.Add("iva", 55).Tag = "STRING";
            Lv.Columns.Add("total", 80).Tag = "STRING";
            Lv.Columns.Add("Descripción", 170).Tag = "STRING";
            Lv.Columns.Add("Año", 0).Tag = "STRING";
            Lv.Columns.Add("Estatus", 90).Tag = "STRING";
            Lv.Columns.Add("¿Entregado?", 80).Tag = "STRING";
            Lv.Columns.Add("¿TIpo de recibo?", 80).Tag = "STRING";
            Lv.Columns.Add("Emitio", 80).Tag = "STRING";
            Lv.Columns.Add("Id Cliente", 0).Tag = "STRING";
            Lv.Columns.Add("Id Doctor", 0).Tag = "STRING";
            Lv.Columns.Add("Id Turno", 0).Tag = "STRING";
            Lv.Columns.Add("Reimpreso", 0).Tag = "NUMBER";

            conectorSql conecta = new conectorSql();
            string Query = "Select distinct(numrecibo), recibos.nombrerecibo as Nombrecliente, recibos.cvcliente ";
            Query = Query + ",fecha,total,iva,totalgeneral,compro,ayo,estatusrecibo,colonia,emitio,entregado,tiporecibo,iddoctor,idturno,printed";
            // Query = Query + ",fecha,total,iva,totalgeneral,compro,ayo,estatusrecibo,colonia,emitio,entregado,tiporecibo,iddoctor,idturno";
            Query = Query + " from recibos ";
            Query = Query + " inner join clientes on clientes.cvcliente=recibos.cvcliente";
            Query = Query + " where numrecibo<>''";

            if (checkBox1.Checked == false) Query = Query + " and fechacod between '" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.ToString("yyyyMMdd") + "'";
            if (textBox11.Text != "") Query = Query + " and numrecibo='" + textBox11.Text + "'";
            if (textBox6.Text != "") Query = Query + " and nombrerecibo like '%" + textBox6.Text + "%'";

            if (textBox18.Text != "") Query = Query + " and recibos.cvcliente='" + textBox18.Text + "'";
            if (comboBox1.Text != "") Query = Query + " and entregado='" + comboBox1.Text + "'";
            if (comboBox2.Text != "") Query = Query + " and colonia='" + comboBox2.Text + "'";

            Query = Query + " order by recibos.numrecibo desc";

            SqlDataReader leer = conecta.RecordInfo(Query);

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
                lvi.SubItems.Add(leer["compro"].ToString());
                lvi.SubItems.Add(leer["ayo"].ToString());
                lvi.SubItems.Add(leer["estatusrecibo"].ToString());
                lvi.SubItems.Add(leer["entregado"].ToString());
                lvi.SubItems.Add(leer["tiporecibo"].ToString());
                lvi.SubItems.Add(leer["emitio"].ToString());
                lvi.SubItems.Add(leer["cvcliente"].ToString());
                lvi.SubItems.Add(leer["iddoctor"].ToString());
                lvi.SubItems.Add(leer["idturno"].ToString());
                

                int printed = 0;
                lvi.SubItems.Add(printed.ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label15.Text = Lv.Items.Count.ToString() + " Recibos ";
            CambioDeColoresCelda();
            CambioDeColoresCeldaEntregas();
        }


        private void CambioDeColoresCeldaEntregas()
        {

            int columna = 0;
            columna = 11;

            Lv.BeginUpdate();

            foreach (ListViewItem item in Lv.Items)
            {
                int indice = 0;
                item.UseItemStyleForSubItems = false;

                foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                {
                    if (indice == columna)
                    {
                        if (subitem.Text == "SI") subitem.BackColor = Color.FromArgb(192, 255, 192);

                        if (subitem.Text == "NO") subitem.BackColor = Color.FromArgb(255, 255, 192);
                    }
                    indice++;
                }
            }
            Lv.EndUpdate();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            label67.Visible = false;
            panel1.Visible = true;
            panel3.Visible = false;
            LimpiarCliente();
            Limpiar();
        }

        public void Limpiar()
        {

            valoresg.AGENDA_FCITAPROX = "";
            valoresg.AGENDA_CVPACIENTE = "";
            valoresg.AGENDA_RECIBO = "";

            button20.Visible = true;
            ColumnasProducto();
            Lv2.Items.Clear();
            label40.Text = "0";
            label41.Text = "0";
            label42.Text = "0";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

            textBox12.Text = "";
            textBox8.Text = "";

            textBox10.Text = "";
            textBox11.Text = "";
            textBox16.Text = "";

            comboBox3.Text = "";
            comboBox4.Text = "";

            label32.Text = "0";
            label31.Text = "0";
            label9.Text = "0";
            label12.Text = "0";
            textBox9.Text = "";
            label20.Text = "0";
            label30.Text = "0";
            label33.Text = "";


            textBox5.Text = "";

            //checkBox1.Checked = true;
            combos.ComboVendedores(comboBox8);

            textBox1.Focus();
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            if (checkBox2.Checked == true) timer2.Enabled = true;
            listView1.Visible = true;
        }

        public void LimpiarCliente()
        {

            textBox2.Text = "";
            textBox10.Text = "";

            textBox5.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button20.Visible = true;
            panel1.Visible = false;
            panel3.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            valoresg.AGENDA_FCITAPROX = "";
            valoresg.AGENDA_CVPACIENTE = "";
            valoresg.AGENDA_RECIBO = "";

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            dateTimePicker4.Value = DateTime.Now.AddDays(1);

            combos.UnidadesProductos(comboBox4);
            label1.Text = valoresg.USUARIOSIS;
            Limpiar();
            BuscarParametros();
            textBox11.Focus();
           // if (checkBox2.Checked == true) timer2.Enabled = true;
           // listView1.Visible = true;
        }

        public string CLAVEPRESERVICIO = "";
        public string FECHADECITA = "";

        public void IngresarPreServicios()
        {
            CLAVEPRESERVICIO = "";

            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            if (textBox1.Text.Trim() == "") return;

            string consulta = "delete from DetallesPreServicio where (cvpaciente='0' or cvpaciente='') and  estatus='CAPTURADO'";
            conecta.Excute(consulta);
            conecta.CierraConexion();
            string Query = "Select * from DetallesPreServicio where cvpaciente='" + textBox1.Text + "' and  estatus='CAPTURADO' order by fechacod desc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                CLAVEPRESERVICIO = leer["cvpreserv"].ToString();
                FECHADECITA = leer["fecha"].ToString();
                string cvproducto = leer["cvproducto"].ToString();
                string cantidad = leer["cantidad"].ToString();
                label40.Text = leer["iddoctor"].ToString();
                string numticket = leer["numticket"].ToString();

                if (label40.Text != "0")
                {
                    string consulta2 = "Select idturno from citas where cvdoctor='" + label40.Text.Trim() + "' ";
                    consulta2 = consulta2 + " and progresivo='" + numticket + "' ";
                    consulta2 = consulta2 + " and fechacod='" + DateTime.Now.ToString("yyyyMMdd") + "'";

                    SqlDataReader leer2 = conecta2.RecordInfo(consulta2);
                    while (leer2.Read())
                    {
                        label41.Text = leer2["idturno"].ToString();
                    }
                    conecta2.CierraConexion();

                    consulta2 = "Select  nombre from Doctores where cvdoctor='" + label40.Text.Trim() + "'";
                    leer2 = conecta2.RecordInfo(consulta2);
                    while (leer2.Read())
                    {
                        label42.Text = leer2["nombre"].ToString();
                    }
                    conecta2.CierraConexion();
                }


                BuscarProducto(cvproducto);
                textBox3.Text = cvproducto;
                textBox4.Text = cantidad;
                AgregarProducto();
                LimpiarProducto();
            }
            conecta.CierraConexion();
        }

        private void CambioDeColoresCelda()
        {

            int columna = 0;
            columna = 7;

            Lv.BeginUpdate();

            foreach (ListViewItem item in Lv.Items)
            {
                int indice = 0;
                item.UseItemStyleForSubItems = false;

                foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                {
                    if (indice == columna)
                    {
                        if (subitem.Text == "RECIBO") subitem.BackColor = Color.FromArgb(96, 204, 69);

                        if (subitem.Text == "CANCELADO") subitem.BackColor = Color.FromArgb(255, 192, 192);
                    }
                    indice++;
                }
            }
            Lv.EndUpdate();
        }
        public void ConsecutivoRemision()
        {
            if (label67.Visible == false)
            {
                string Numero = "1";
                conectorSql conecta = new conectorSql();
                string Query = "Select numrecibo from consecutivos where numrecibo <>''";
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    Numero = leer["numrecibo"].ToString();
                }
                conecta.CierraConexion();
                label17.Text = Numero;
            }
        }

        public bool ActualizaRemision()
        {
            conectorSql conecta = new conectorSql();
            try
            {
                int Siguiente = int.Parse(label17.Text) + 1;
                string Query = "update consecutivos set numrecibo='" + Siguiente.ToString() + "'";
                return conecta.Excute(Query);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conecta.CierraConexion();
            }
            
            
        }


        public void BuscarInformacion(string clave)
        {
            Limpiar();
            LimpiarCliente();
            conectorSql conecta = new conectorSql();
            try
            {
                string Query = "";
                if (clave != "")
                {
                    Query = "Select * from clientes where cvcliente='" + clave + "'";
                    bool existeCliente = conecta.ExisteRegistro(Query);
                    if (existeCliente == false)
                    {
                        MessageBox.Show("No existe la clave del cliente, verifique por favor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                Query = "Select * from clientes where cvcliente='" + clave + "'";
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    textBox1.Text = clave;
                    textBox2.Text = leer["nombre"].ToString();
                    textBox10.Text = leer["direccion"].ToString();

                    if (clave == "0")
                    {
                        textBox2.Text = "";
                        textBox10.Text = "";
                    }

                    textBox5.Text = leer["telefono"].ToString();
                    textBox16.Text = leer["observafact"].ToString();

                    ReLocalidad = "";

                    ReNoInterior = "0";
                    RePais = leer["PaisF"].ToString();
                    ReReferencia = "";


                    string valor = leer["tipopago"].ToString();

                    comboBox8.Text = leer["vendedor"].ToString();
                    IngresarPreServicios(); // solo para clinica de registro de pacientes 2015

                    textBox2.Focus();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conecta.CierraConexion();
            }
            

           
        }





        public void BuscarProducto(string clave)
        {
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            AgregarProductoBase();
            label28.Text = "0";
            label49.Text = "NO";
            string Query = "Select * from productos where cvproducto='" + clave + "'";
            if (clave.Length > 0)
            {
                bool existePro = conecta.ExisteRegistro(Query);
                if (existePro == false)
                {
                    MessageBox.Show("No existe la clave del producto o servicio verifique", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimpiarProductoNuevo();
                    return;
                }
            }

            SqlDataReader leer = conecta.RecordInfo(Query);
            comboBox3.Items.Clear();
            while (leer.Read())
            {
                label30.Text = clave;
                textBox8.Text = leer["nombre"].ToString();

                comboBox4.Text = leer["unidad"].ToString();
                textBox4.Text = "1";
                label49.Text = leer["causaiva"].ToString();


                this.label28.Text = leer["cantidad"].ToString();
                if (decimal.Parse(this.label28.Text) < 1)
                {
                    this.label28.BackColor = Color.Red;
                    this.label28.ForeColor = Color.White;
                }
                else
                {
                    this.label28.BackColor = Color.FromArgb(240, 240, 240);
                    this.label28.ForeColor = Color.Black;
                }


                string consulta = "select * from ListaPrecios where cvproducto='" + clave + "'";
                SqlDataReader leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    decimal distribuidor = decimal.Parse(leer2["distribuidor"].ToString());
                    decimal precio1 = decimal.Parse(leer2["publico1"].ToString());
                    decimal precio2 = decimal.Parse(leer2["publico2"].ToString());
                    decimal precio3 = decimal.Parse(leer2["publico3"].ToString());
                    if (precio1 > 0 && precio1 != distribuidor) comboBox3.Items.Add(precio1.ToString());
                    if (precio2 > 0 && precio2 != distribuidor) comboBox3.Items.Add(precio2.ToString());
                    if (precio3 > 0 && precio3 != distribuidor) comboBox3.Items.Add(precio3.ToString());
                    comboBox3.Text = precio1.ToString();
                    label20.Text = distribuidor.ToString();

                }
                conecta2.CierraConexion();
                if (HABILITAPRECIO == true) comboBox3.DropDownStyle = ComboBoxStyle.DropDown;
                else comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;

                textBox4.Focus();

            }
            conecta.CierraConexion();

            if ((decimal.Parse(this.label28.Text) < 1) && (this.textBox3.Text.Trim() != "") && comboBox4.Text != "SERV")
            {
                this.LimpiarProductoNuevo();
                if (MessageBox.Show("Usted tiene " + this.label28.Text + " productos , Desea actualizar su inventario de productos?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                    // Notificar por correo la falta de inventario
                    MailNotifications mail = new MailNotifications();
                    mail.SendMailOnlySubjectAndMSG("Falta inventario para venta en sucursal Cuernavaca", "El producto " + clave + " ( " + NOMBREPRO + ") no tiene existencias, favor de tomar acciones inmediatas");

                    valoresg.NUMPRODUCTOSURTIR = clave;
                    // Productos nproductos = new Productos();
                    // nproductos.Show();
                    // valoresg.NUMPRODUCTOSURTIR = cvproducto;
                    // new Productos().Show();
                    new CapturaInventarios().ShowDialog();
                }
            }

        }


        public void Guardar()
        {

            string TELEFONOCLIENTE = textBox5.Text;
            TIPORECIBO = "PAGADO";
            if (radioButton1.Checked == true) TIPORECIBO = "PAGADO";
            if (radioButton2.Checked == true) TIPORECIBO = "POR PAGAR";

            CADUNIDAD = "";
            CADCANTIDAD = "";
            CADCLAVE = "";
            CADNOMBREPRO = "";
            CADPRECIOUNITARIO = "";
            if (label40.Text == "") label40.Text = "0";
            if (label41.Text == "") label41.Text = "0";
            if (label42.Text == "") label42.Text = "0";


            CADPRECIOTOTAL = "";
            for (int i = 0; i < Lv2.Items.Count; i++)
            {

                CADUNIDAD = CADUNIDAD + Lv2.Items[i].Text;
                CADUNIDAD = CADUNIDAD + "\n";

                CADCANTIDAD = CADCANTIDAD + Lv2.Items[i].SubItems[1].Text;
                CADCANTIDAD = CADCANTIDAD + "\n";

                CADCLAVE = CADCLAVE + Lv2.Items[i].SubItems[2].Text;
                CADCLAVE = CADCLAVE + "\n";

                CADNOMBREPRO = CADNOMBREPRO + Lv2.Items[i].SubItems[3].Text;
                CADNOMBREPRO = CADNOMBREPRO + "\n";

                CADPRECIOUNITARIO = CADPRECIOUNITARIO + "$ " + Lv2.Items[i].SubItems[4].Text;
                CADPRECIOUNITARIO = CADPRECIOUNITARIO + "\n";

                CADPRECIOTOTAL = CADPRECIOTOTAL + "$ " + Lv2.Items[i].SubItems[5].Text;
                CADPRECIOTOTAL = CADPRECIOTOTAL + "\n";

            }

            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            string Query = "";
            Query = "insert into Recibos(";
            Query = Query + "numrecibo";
            Query = Query + ",nombrerecibo";
            Query = Query + ",direccion";

            Query = Query + ",cvcliente";
            Query = Query + ",fecha";
            Query = Query + ",fechacod";
            Query = Query + ",total";
            Query = Query + ",iva";
            Query = Query + ",totalgeneral";
            Query = Query + ",emitio";

            Query = Query + ",mes";
            Query = Query + ",ayo";
            Query = Query + ",totalletra";
            Query = Query + ",tdistribuidor";
            Query = Query + ",tganancia";
            Query = Query + ",compro";
            Query = Query + ",cantidades";
            Query = Query + ",precunitarios";
            Query = Query + ",pretotales";
            Query = Query + ",unidades";


            Query = Query + ",notas";
            Query = Query + ",condicionPago";

            Query = Query + ",numremision";
            Query = Query + ",fcodremision";
            Query = Query + ",fecharemision";
            Query = Query + ",estatusrecibo";
            Query = Query + ",vendedor";
            Query = Query + ",tiporecibo";
            Query = Query + ",colonia";
            Query = Query + ",aproxpeso";
            Query = Query + ",entregado";

            Query = Query + ",fechaentrega";
            Query = Query + ",fcodentrega";
            Query = Query + ",telefono";

            Query = Query + ",hora";
            Query = Query + ",horacod";

            Query = Query + ",tdescuento";
            Query = Query + ",ncliente";


            Query = Query + ",iddoctor";
            Query = Query + ",idturno";

            Query = Query + ",claves)";

            Query = Query + " values(";

            Query = Query + "'" + NUMPEDIDO + "'";
            Query = Query + ",'" + textBox2.Text.Trim() + "'";
            Query = Query + ",'" + textBox10.Text.Trim() + "'";
            Query = Query + ",'" + CVCLIENTE + "'";
            Query = Query + ",'" + FECHA + "'";
            Query = Query + ",'" + FECHACOD + "'";
            Query = Query + ",'" + SUBTOTAL + "'";
            Query = Query + ",'" + IVA + "'";
            Query = Query + ",'" + TOTAL + "'";
            Query = Query + ",'" + EMITIO + "'";
            Query = Query + ",'" + MES + "'";
            Query = Query + ",'" + AYO + "'";
            Query = Query + ",'" + CANTLETRA + "'";
            Query = Query + ",'" + TDISTRIBUIDOR + "'";
            Query = Query + ",'" + TGANANCIA + "'";
            Query = Query + ",'" + CADNOMBREPRO + "'";
            Query = Query + ",'" + CADCANTIDAD + "'";
            Query = Query + ",'" + CADPRECIOUNITARIO + "'";
            Query = Query + ",'" + CADPRECIOTOTAL + "'";
            Query = Query + ",'" + CADUNIDAD + "'";



            Query = Query + ",'" + Notas + "'";
            if (TIPORECIBO == "CREDITO") Query = Query + ",'POR PAGAR'";
            else Query = Query + ",'PAGADO'";

            Query = Query + ",'" + numremision + "'";

            Query = Query + ",'" + fcodremision + "'";
            Query = Query + ",'" + fecharemision + "'";
            Query = Query + ",'" + estatuspedido + "'";
            Query = Query + ",'" + VENDEDORGEN + "'";
            Query = Query + ",'" + TIPORECIBO + "'";
            Query = Query + ",'" + COLONIARECIBO + "'";
            Query = Query + ",'" + PESOAPROX + "'";
            Query = Query + ",'" + ENTREGADO + "'";

            Query = Query + ",'" + FECHAENTREGA + "'";
            Query = Query + ",'" + FECHACODENTREGA + "'";

            Query = Query + ",'" + TELEFONOCLIENTE + "'";

            Query = Query + ",'" + DateTime.Now.ToString("HH:mm:ss") + "'";
            Query = Query + ",'" + DateTime.Now.ToString("HHmmss") + "'";

            Query = Query + ",'" + label26.Text.Trim() + "'";

            Query = Query + ",'" + NCLIENTE + "'";

            Query = Query + ",'" + label40.Text + "'";
            Query = Query + ",'" + label41.Text + "'";

            Query = Query + ",'" + CADCLAVE + "')";
            conecta.Excute(Query);
            conecta.CierraConexion();

            Query = "Insert into comisiones(cvvendedor,numrecibo,total,cancelado) values('" + this.VENDEDORGEN + "','" + this.NUMPEDIDO + "','" + this.label37.Text.Trim() + "','SI')";
            conecta.Excute(Query);
            conecta.CierraConexion();
            


            int Contador = 1;
            for (int i = 0; i < Lv2.Items.Count; i++)
            {

                string UNI = Lv2.Items[i].Text;
                CANTIDAD = Lv2.Items[i].SubItems[1].Text;
                CLAVEPRODUCTO = Lv2.Items[i].SubItems[2].Text;
                NOMBREPRO = Lv2.Items[i].SubItems[3].Text;
                PRECIOUNITARIO = Lv2.Items[i].SubItems[4].Text;

                PRECIOTOTAL = Lv2.Items[i].SubItems[6].Text;
                string DISTRIBUIDOR = Lv2.Items[i].SubItems[8].Text;
                string GANANCIA = Lv2.Items[i].SubItems[9].Text;
                string Nota1 = Lv2.Items[i].SubItems[10].Text;
                string CAUSAIVA = Lv2.Items[i].SubItems[11].Text;
                string comisionR = Lv2.Items[i].SubItems[13].Text;

                decimal cantprod = decimal.Parse(CANTIDAD);
                decimal canttotal = 0;
                string consulta = "Select cantidad from productos where cvproducto='" + CLAVEPRODUCTO + "'";
                SqlDataReader leer = conecta2.RecordInfo(consulta);
                while (leer.Read())
                {
                    canttotal = decimal.Parse(leer["cantidad"].ToString());
                }
                conecta2.CierraConexion();

                decimal cantfinal = canttotal - cantprod;
                // if (cantfinal <= 0) cantfinal = 1;
                consulta = "update productos set cantidad=" + cantfinal.ToString() + " where cvproducto=" + CLAVEPRODUCTO + "";
                conecta2.Excute(consulta);
                conecta2.CierraConexion();

                Query = "Insert into DetallesRecibos(";
                Query = Query + "numrecibo";
                Query = Query + ",cvcliente";
                Query = Query + ",cvproducto";
                Query = Query + ",descripcion";
                Query = Query + ",cantidad";
                Query = Query + ",preunitario";
                Query = Query + ",precio";
                Query = Query + ",fecha";
                Query = Query + ",fechacod";
                Query = Query + ",mes";
                Query = Query + ",ayo";
                Query = Query + ",emitio";
                Query = Query + ",tdistribuidor";
                Query = Query + ",tganancia";
                Query = Query + ",nota1";
                Query = Query + ",cvunica";
                Query = Query + ",causaiva";

                Query = Query + ",descuento";
                Query = Query + ",comision";
                Query = Query + ",progresivo";

                Query = Query + ",unidad)";

                Query = Query + " values(";
                Query = Query + "'" + NUMPEDIDO + "'";
                Query = Query + ",'" + CVCLIENTE + "'";
                Query = Query + ",'" + CLAVEPRODUCTO + "'";
                Query = Query + ",'" + NOMBREPRO + "'";
                Query = Query + ",'" + CANTIDAD + "'";
                Query = Query + ",'" + PRECIOUNITARIO + "'";
                Query = Query + ",'" + PRECIOTOTAL + "'";
                Query = Query + ",'" + FECHA + "'";
                Query = Query + ",'" + FECHACOD + "'";
                Query = Query + ",'" + MES + "'";
                Query = Query + ",'" + AYO + "'";
                Query = Query + ",'" + EMITIO + "'";
                Query = Query + ",'" + DISTRIBUIDOR + "'";
                Query = Query + ",'" + GANANCIA + "'";
                Query = Query + ",'" + Nota1 + "'";
                Query = Query + ",'" + CLAVEPRODUCTO + "'";
                Query = Query + ",'" + CAUSAIVA + "'";

                Query = Query + ",'" + Descuento + "'";
                Query = Query + ",'" + comisionR + "'";
                Query = Query + ",'" + Contador.ToString() + "'";

                Query = Query + ",'" + UNI + "')";

                conecta.Excute(Query);
                
                Query = "Update DetallesPreServicio set estatus='COBRADO' , numrecibo='" + NUMPEDIDO + "' where cvpaciente='" + CVCLIENTE + "' and cvproducto='" + CLAVEPRODUCTO + "' and cvpreserv='" + CLAVEPRESERVICIO + "'";
                conecta.Excute(Query);
                
                Contador++;
            }
        }

        public bool ExisteInfo()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from recibos where numrecibo ='" + NUMPEDIDO + "'";
            return conecta.ExisteRegistro(Query);
        }
        public string EmpresaRFC;
        public string EmpresaCorreo;
        public void RFCEmpresa()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from empresa where cvempresa<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                EmpresaRFC = leer["RFC"].ToString();
                EmpresaCorreo = leer["email"].ToString();
            }
            conecta.CierraConexion();
        }


        private void Lv_DoubleClick(object sender, EventArgs e)
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

        public void ModificaciondeRecibo(string numpedido)
        {
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            conectorSql conecta3 = new conectorSql();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            FECHAMODIFICA = "";
            FECHACOD = "";

            label67.Visible = false;
            SqlDataReader leer3 = null;
            string Query = "Select * from recibos where numrecibo ='" + numpedido + "'";
            label17.Text = numpedido;
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string claveusuario = leer["cvcliente"].ToString();
                textBox2.Text = leer["nombrerecibo"].ToString();
                textBox10.Text = leer["direccion"].ToString();

                FECHACODMOD = leer["fechacod"].ToString();
                FECHAMODIFICA = leer["fecha"].ToString();

                textBox16.Text = leer["notas"].ToString();

                Query = "Select * from clientes where cvcliente='" + claveusuario + "'";
                SqlDataReader leer2 = conecta2.RecordInfo(Query);
                while (leer2.Read())
                {
                    textBox1.Text = claveusuario;

                    textBox5.Text = leer2["telefono"].ToString();
                    textBox16.Text = leer2["observafact"].ToString();

                    ReLocalidad = "";
                    ReNoInterior = "0";
                    RePais = leer2["PaisF"].ToString();
                    ReReferencia = "";

                    string valor = leer2["tipopago"].ToString();

                    comboBox8.Text = leer2["vendedor"].ToString();
                }
                conecta2.CierraConexion();


                string valor2 = leer["tiporecibo"].ToString();
                if (valor2 == "CONTADO") radioButton1.Checked = true;
                else radioButton2.Checked = true;




                ColumnasProducto();
                Query = "Select * from detallesrecibos where numrecibo='" + numpedido + "'";
                leer2 = conecta2.RecordInfo(Query);
                while (leer2.Read())
                {
                    string cvproducto = leer2["cvproducto"].ToString();

                    string Nombre = leer2["descripcion"].ToString();
                    string unidad = leer2["unidad"].ToString();
                    decimal cantidad = decimal.Parse(leer2["cantidad"].ToString());
                    decimal precio = decimal.Parse(leer2["preunitario"].ToString());
                    decimal total = precio * cantidad;
                    string NumPrecio = "0";
                    decimal TotalDistribuidor = decimal.Parse(leer2["tdistribuidor"].ToString());
                    decimal Ganancia = decimal.Parse(leer2["tganancia"].ToString());

                    string CausaIVA = "NO";
                    CausaIVA = leer2["causaiva"].ToString();

                    decimal TotalIva = total * IVAParametro;
                    if (CausaIVA == "NO") TotalIva = 0;

                    ListViewItem lvi = new ListViewItem(unidad);
                    lvi.SubItems.Add(cantidad.ToString("##.00", CultureInfo.InvariantCulture));
                    lvi.SubItems.Add(cvproducto);
                    lvi.SubItems.Add(Nombre);
                    lvi.SubItems.Add(precio.ToString("##.00", CultureInfo.InvariantCulture));
                    lvi.SubItems.Add(total.ToString("##.00", CultureInfo.InvariantCulture));
                    lvi.SubItems.Add(NumPrecio);
                    lvi.SubItems.Add(TotalDistribuidor.ToString("##.00", CultureInfo.InvariantCulture));
                    lvi.SubItems.Add(Ganancia.ToString("##.00", CultureInfo.InvariantCulture));
                    lvi.SubItems.Add(valoresg.DETALLE1);
                    lvi.SubItems.Add(CausaIVA);
                    lvi.SubItems.Add(TotalIva.ToString("##.00", CultureInfo.InvariantCulture));
                    Lv2.Items.Add(lvi);
                    SumaTotales();
                }
                conecta2.CierraConexion();
                label44.Text = Lv2.Items.Count.ToString();
                label67.Visible = true;
            }
            conecta.CierraConexion();

        }
        public void DetallesModifica(int index)
        {
            button20.Visible = true;
            if (BMODIFICARECBO == "NO")
            {
                MessageBox.Show("No tiene permisos para modificar recibos, cancele el recibo y vuelva a capturar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button20.Visible = false;
            }
            string numpedido = Lv.Items[index].Text;
            string ayo = Lv.Items[index].SubItems[8].Text;

            string estatuspedido = label53.Text;
            if (estatuspedido == "CANCELADO")
            {
                MessageBox.Show("El recibo se encuentra cancelado no es posible realizar esta operación", "Modificar recibo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //DialogResult reply = MessageBox.Show("¿Desea modificar el recibo seleccionado?", "Modificar recibo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (reply == DialogResult.No)
                //    return;
                panel3.Visible = false;
                panel1.Visible = true;
                Lv2.Items.Clear();
                Lv2.Columns.Clear();
                ModificaciondeRecibo(numpedido);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("¿Desea eliminar la información seleccionada?", "Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;

            conectorSql conecta = new conectorSql();
            string Query = "";
            int contar = 0;
            for (int i = 0; i < Lv.Items.Count; i++)
            {
                string clave = Lv.Items[i].Text;
                if (Lv.Items[i].Checked == true)
                {
                    Query = "Delete from clientes where cvcliente='" + clave + "'";
                    conecta.Excute(Query);
                    contar++;
                }
            }

            MessageBox.Show("Se eliminaron " + contar.ToString() + " registros del sistema", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("¿Desea exportar la información a excel?", "Exportación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;

            ReportesNKB.RBusquedaRemisionesRecibos(dateTimePicker1.Value.ToString("yyyyMMdd"), dateTimePicker2.Value.ToString("yyyyMMdd"), textBox11.Text, checkBox1.Checked);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LimpiarCliente();
                BuscarInformacion(textBox1.Text);
                CVCLIENTE = textBox1.Text.Trim();
                if (textBox1.Text == "0") textBox2.Focus();
                else textBox3.Focus();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ConsecutivoRemision();
            }
            catch (Exception er)
            {
                MessageBox.Show("Error dentro de cargar pacientes" + er.Message.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }


        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox3.Text != "") BuscarProducto(textBox3.Text);
                if (textBox3.Text == "") button20.Focus();
            }
        }

        public void CargarPacientesporPagar()
        {
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("Clave Paciente", 100).Tag = "NUMBER";
            listView1.Columns.Add("Fecha", 100).Tag = "NUMBER";
            conectorSql conecta = new conectorSql();
            string Query = "update  detallespreservicio set  estatus='NINGUNO'  where cvpaciente<>'' and estatus='CAPTURADO' and fechacod<'" + DateTime.Now.ToString("yyyyMMdd") + "'";
            conecta.Excute(Query);
            conecta.CierraConexion();

            Query = "Select distinct(cvpaciente) as clave from detallespreservicio where cvpaciente<>'' and estatus='CAPTURADO' and fechacod='" + DateTime.Now.ToString("yyyyMMdd") + "' order by cvpaciente desc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["clave"].ToString());
                lvi.SubItems.Add("HOY");
                listView1.Items.Add(lvi);
            }
            conecta.CierraConexion();
            conecta.CierraConexion();

            Query = "Select distinct(cvpaciente) as clave, fechacod, fecha from detallespreservicio where cvpaciente<>'' and estatus='CAPTURADO'  and fechacod>'" + DateTime.Now.ToString("yyyyMMdd") + "' order by fechacod asc";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["clave"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                listView1.Items.Add(lvi);
            }
            conecta.CierraConexion();
            conecta.CierraConexion();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            PrecioProductoActualiza(textBox3.Text);
            AgregarProducto();
            LimpiarProducto();
        }

        public void ColumnasProducto()
        {
            if (Lv2.Items.Count == 0)
            {
                Lv2.Items.Clear();
                Lv2.Columns.Clear();
                Lv2.Columns.Add("Unidad", 80);
                Lv2.Columns.Add("Cantidad", 80);
                Lv2.Columns.Add("Clave", 60);
                Lv2.Columns.Add("Nombre", 450);
                Lv2.Columns.Add("Unitario", 100);
                Lv2.Columns.Add("Desc", 100);
                Lv2.Columns.Add("Total", 100);
                Lv2.Columns.Add("nprecio", 0);
                Lv2.Columns.Add("distribuidor", 0);
                Lv2.Columns.Add("ganancia", 0);
                Lv2.Columns.Add("Detalle", 0);
                Lv2.Columns.Add("ConIVA", 0);
                Lv2.Columns.Add("Tiva", 0);
                Lv2.Columns.Add("TComision", 0);

            }
        }
        public void AgregarProducto()
        {
            ColumnasProducto();
            if (textBox3.Text == "") return;
            if (comboBox3.Text == "") return;

            decimal Numero = 0;
            bool esDecimal = decimal.TryParse(comboBox3.Text, out Numero);
            if (esDecimal == false)
            {
                MessageBox.Show("El precio debe ser númerico, verifique", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox3.Focus();
                return;
            }

            if (label30.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese la clave de un producto o servicio, verifique", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return;
            }


            string cvproducto = label30.Text;
            string Nombre = textBox8.Text;


            string unidad = comboBox4.Text;
            decimal cantidad = decimal.Parse(textBox4.Text);
            if (textBox13.Text == "") textBox13.Text = "0";
            decimal Pordescuento = decimal.Parse(textBox13.Text);

            if (textBox14.Text == "") textBox14.Text = "0";

            decimal PorComision = decimal.Parse(textBox14.Text);

            decimal existencia = decimal.Parse(label28.Text);
            decimal porSurtir = cantidad - existencia;

            if (decimal.Parse(this.label28.Text) < cantidad)
            {

                // Notificar por correo la falta de inventario
                MailNotifications mail = new MailNotifications();
                mail.SendMailOnlySubjectAndMSG("Falta inventario para venta en sucursal Cuernavaca", "El producto " + cvproducto + " ( " + Nombre + ") no tiene existencias, favor de tomar acciones inmediatas");

                if (MessageBox.Show("Usted tiene " + this.label28.Text + " productos en existencia \nNo es posible surtir el recibo necesita " + porSurtir.ToString() + " productos , Desea actualizar su inventario de productos?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    valoresg.NUMPRODUCTOSURTIR = cvproducto;

                    // new Productos().Show();
                    new CapturaInventarios().ShowDialog();

                }

                this.LimpiarProductoNuevo();
            }
            else
            {





                decimal PrecioConIVA = decimal.Parse(comboBox3.Text);
                if (CALCULOIVA == "2" && radioButton3.Checked == true) PrecioConIVA = PrecioConIVA / (1 + IVAParametro);

                if (this.radioButton3.Checked)
                {
                    Nombre = Nombre + " **";
                }

                //decimal num8 = PrecioConIVA;
                //decimal num9 = num8 * cantidad;
                //string str4 = this.comboBox3.SelectedIndex.ToString();

                //decimal num10 = ((num9 * Pordescuento) / 100M) * -1M;
                //decimal num11 = decimal.Parse(this.label20.Text);
                //decimal num12 = cantidad * num11;

                //decimal num13 = (num9 + num10) - num12;
                //decimal num14 = ((num9 + num10) * PorComision) / 100M;
                ////decimal num15 = (num9 + num10) * this.IVAParametro;

                decimal precio = PrecioConIVA;
                decimal total = precio * cantidad;
                string NumPrecio = comboBox3.SelectedIndex.ToString();
                decimal Rdescuento = (total * Pordescuento) / 100 * -1;

                decimal PreDistribuidor = decimal.Parse(label20.Text);
                decimal TotalDistribuidor = cantidad * PreDistribuidor;


                decimal Ganancia = (total + Rdescuento); // - TotalDistribuidor;
                decimal ReComision = (total + Rdescuento) * PorComision / 100;


                decimal TotalIva = (total * IVAParametro) + (total) - (Rdescuento);
                if (radioButton4.Checked == true) TotalIva = 0;

                ListViewItem lvi = new ListViewItem(unidad);
                lvi.SubItems.Add(cantidad.ToString("##.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(cvproducto);
                lvi.SubItems.Add(Nombre);
                lvi.SubItems.Add(precio.ToString("##.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(Rdescuento.ToString("##.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(Ganancia.ToString("##.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(NumPrecio);
                lvi.SubItems.Add(TotalDistribuidor.ToString("##.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(Ganancia.ToString("##.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(valoresg.DETALLE1);
                lvi.SubItems.Add(label49.Text);
                lvi.SubItems.Add(TotalIva.ToString("##.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(ReComision.ToString("##.00", CultureInfo.InvariantCulture));
                Lv2.Items.Add(lvi);
                SumaTotales();


                label44.Text = Lv2.Items.Count.ToString();
                LimpiarProductoNuevo();
                textBox3.Focus();

            }
        }


        public void PrecioProductoActualiza(string cvproducto)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Update ListaPrecios set publico1='" + comboBox3.Text + "' where cvproducto='" + cvproducto + "'";
            conecta.Excute(Query);
        }

        public void AgregarProductoBase()
        {
            conectorSql conecta = new conectorSql();
            if (textBox8.Text.Trim() == "") return;
            string NumeroProducto = "1";
            string Query2 = "Select numproducto  from consecutivos where numproducto<>''";
            SqlDataReader leer2 = conecta.RecordInfo(Query2);
            while (leer2.Read())
            {
                NumeroProducto = leer2["numproducto"].ToString();
            }
            conecta.CierraConexion();

            string nombre = textBox8.Text.Trim();
            string descripcion = textBox8.Text.Trim();
            string categoria = "GENERAL";
            string unidad = comboBox4.Text;
            string cantidad = textBox4.Text;
            string minimo = "100";
            string maximo = "1000";
            string causaiva = "NO";
            string marca = "GENERAL";
            string codbarras = "";
            string ubicacion = "";
            string fechaModifica = DateTime.Now.ToString("dd/MM/yyyy");
            string fcodmodifica = DateTime.Now.ToString("yyyyMMdd");
            string emitio = valoresg.USUARIOSIS;
            string causaAdicional = "NO";


            string distribuidor = "1";
            string publico1 = comboBox3.Text.Trim();
            string porciento1 = "0";
            string ganancia1 = publico1;
            string publico2 = "0";
            string porciento2 = "0";
            string ganancia2 = "0";
            string publico3 = "0";
            string porciento3 = "0";
            string ganancia3 = "0";
            int NumProductoSig = int.Parse(NumeroProducto) + 1;


            string Query = "Select * from productos where nombre='" + textBox8.Text.Trim() + "'";
            bool existe = conecta.ExisteRegistro(Query);
            if (existe == false)
            {
                textBox3.Text = NumProductoSig.ToString();
                string cvproducto = textBox3.Text;

                Query = "Insert into productos (cvproducto";
                Query = Query + ", nombre";
                Query = Query + ",descripcion";
                Query = Query + ",categoria";
                Query = Query + ",unidad";
                Query = Query + ",cantidad";
                Query = Query + ",minimo";
                Query = Query + ",maximo";
                Query = Query + ",causaiva";
                Query = Query + ",marca";
                Query = Query + ",codbarras";
                Query = Query + ",ubicacion";
                Query = Query + ",fechaModifica";
                Query = Query + ",fcodmodifica";
                Query = Query + ",emitio";
                Query = Query + ",causaAdicional)";
                Query = Query + " values(";
                Query = Query + "'" + cvproducto + "'";
                Query = Query + ",'" + nombre + "'";
                Query = Query + ",'" + descripcion + "'";
                Query = Query + ",'" + categoria + "'";
                Query = Query + ",'" + unidad + "'";
                Query = Query + ",'" + cantidad + "'";
                Query = Query + ",'" + minimo + "'";
                Query = Query + ",'" + maximo + "'";
                Query = Query + ",'" + causaiva + "'";
                Query = Query + ",'" + marca + "'";
                Query = Query + ",'" + codbarras + "'";
                Query = Query + ",'" + ubicacion + "'";
                Query = Query + ",'" + fechaModifica + "'";
                Query = Query + ",'" + fcodmodifica + "'";
                Query = Query + ",'" + emitio + "'";
                Query = Query + ",'" + causaAdicional + "')";
                conecta.Excute(Query);

                Query = "Insert into  ListaPrecios(";
                Query = Query + " cvproducto";
                Query = Query + ",distribuidor";
                Query = Query + ",publico1";
                Query = Query + ",porciento1";
                Query = Query + ",ganancia1";
                Query = Query + ",publico2";
                Query = Query + ",porciento2";
                Query = Query + ",ganancia2";
                Query = Query + ",publico3";
                Query = Query + ",porciento3";
                Query = Query + ",ganancia3)"; ;
                Query = Query + " values(";
                Query = Query + "'" + cvproducto + "'";
                Query = Query + ",'" + distribuidor + "'";
                Query = Query + ",'" + publico1 + "'";
                Query = Query + ",'" + porciento1 + "'";
                Query = Query + ",'" + ganancia1 + "'";
                Query = Query + ",'" + publico2 + "'";
                Query = Query + ",'" + porciento2 + "'";
                Query = Query + ",'" + ganancia2 + "'";
                Query = Query + ",'" + publico3 + "'";
                Query = Query + ",'" + porciento3 + "'";
                Query = Query + ",'" + ganancia3 + "')";
                conecta.Excute(Query);

                Query = "Update consecutivos set numproducto='" + NumProductoSig.ToString() + "'";
                conecta.Excute(Query);
            }
        }


        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) comboBox3.Focus();
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                float Numero = 0;
                bool esEntero = float.TryParse(textBox4.Text, out Numero);
                if (esEntero == false)
                {
                    MessageBox.Show("El tipo de dato debe ser numero , verifique", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox4.Focus();
                    return;
                }
                PrecioProductoActualiza(textBox3.Text);
                AgregarProducto();
                LimpiarProducto();
                textBox3.Text = "";
            }
        }
        public void LimpiarProducto()
        {
            valoresg.DETALLE1 = "";
            comboBox4.Text = "";
            textBox4.Text = "";
            textBox8.Text = "";
            comboBox3.Text = "";

        }
        decimal IVAParametro;
        public bool BANSELECCIONAVENDEDOR = false;

        public bool APLICARCOMOPAGADO = false;
        public bool VENTANACOBRO = false;
        public bool HABILITAPRECIO = false;
        public bool MOSTRARCONVENIO = false;
        public string CALCULOIVA = "";
        public void BuscarParametros()
        {
            BMODIFICARECBO = "NO";
            try
            {
                CALCULOIVA = "1";
                comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
                conectorSql conecta = new conectorSql();
                string Query = "Select * from parametros where habilitarprecio<>''";
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    IVAParametro = decimal.Parse(leer["iva"].ToString());
                    string valor = leer["ObligatorioVendedor"].ToString();
                    if (valor == "SI") BANSELECCIONAVENDEDOR = true;
                    else BANSELECCIONAVENDEDOR = false;


                    valor = leer["pasarpagado"].ToString();
                    if (valor == "SI") APLICARCOMOPAGADO = true;
                    else APLICARCOMOPAGADO = false;

                    valor = leer["ventcobro"].ToString();
                    if (valor == "SI") VENTANACOBRO = true;
                    else VENTANACOBRO = false;

                    valor = leer["habilitarprecio"].ToString();
                    if (valor == "SI") HABILITAPRECIO = true;
                    else HABILITAPRECIO = false;

                    valor = leer["conveniopago"].ToString();
                    if (valor == "SI") MOSTRARCONVENIO = true;
                    else MOSTRARCONVENIO = false;

                    CALCULOIVA = leer["calculaiva"].ToString();

                    BMODIFICARECBO = leer["modificarecibo"].ToString();

                }
                conecta.CierraConexion();

                if (HABILITAPRECIO == true) comboBox3.DropDownStyle = ComboBoxStyle.DropDown;
            }
            catch (Exception E)
            {

                MessageBox.Show(E.Message.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        public void SumaTotales()
        {
            decimal acumulado = 0;
            decimal acumulado2 = 0;
            decimal acumulado3 = 0;

            decimal AcumulaIva = 0;
            decimal AcumulaDesc = 0;
            decimal AcumulaComision = 0;

            for (int i = 0; i < Lv2.Items.Count; i++)
            {

                if (Lv2.Items[i].SubItems[9].Text=="")
                {
                    Lv2.Items[i].SubItems[9].Text = "0";
                }
                decimal total = decimal.Parse(Lv2.Items[i].SubItems[6].Text);
                acumulado = acumulado + total;

                if (Lv2.Items[i].SubItems[9].Text=="")
                {
                    Lv2.Items[i].SubItems[9].Text = "0";
                }
                decimal total2 = decimal.Parse(Lv2.Items[i].SubItems[9].Text);
                acumulado2 = acumulado2 + total2;

                if (Lv2.Items[i].SubItems[8].Text=="")
                {
                    Lv2.Items[i].SubItems[8].Text = "0";
                }
                decimal total3 = decimal.Parse(Lv2.Items[i].SubItems[8].Text);
                acumulado3 = acumulado3 + total3;

                if (Lv2.Items[i].SubItems[12].Text=="")
                {
                    Lv2.Items[i].SubItems[12].Text = "0";
                }
                decimal CantIva = decimal.Parse(Lv2.Items[i].SubItems[12].Text);
                AcumulaIva = AcumulaIva + CantIva;


                if (Lv2.Items[i].SubItems[5].Text=="")
                {
                    Lv2.Items[i].SubItems[5].Text = "0";
                }
                decimal Descuento = decimal.Parse(Lv2.Items[i].SubItems[5].Text);
                AcumulaDesc = AcumulaDesc + Descuento;

                if (Lv2.Items[i].SubItems[13].Text=="")
                {
                    Lv2.Items[i].SubItems[13].Text = "0";
                }
                decimal Comision = decimal.Parse(Lv2.Items[i].SubItems[13].Text);
                AcumulaComision = AcumulaComision + Comision;


            }


            decimal subtotal = acumulado;
            // decimal resiva = subtotal * IVAParametro;
            decimal netopago = subtotal + AcumulaIva;

            label9.Text = subtotal.ToString("##.00", CultureInfo.InvariantCulture); //subtotal
            label12.Text = AcumulaIva.ToString("##.00", CultureInfo.InvariantCulture); //total del iva
            textBox9.Text = netopago.ToString("##.00", CultureInfo.InvariantCulture);
            label31.Text = acumulado2.ToString("##.00", CultureInfo.InvariantCulture); //ganancia
            label32.Text = acumulado3.ToString("##.00", CultureInfo.InvariantCulture); // inversion
            label26.Text = AcumulaDesc.ToString("##.00", CultureInfo.InvariantCulture); // inversion
            label37.Text = AcumulaComision.ToString("##.00", CultureInfo.InvariantCulture); // inversion


            Numalet let = null;
            let = new Numalet();
            //al uso en México (creo):
            let.MascaraSalidaDecimal = "00/100 M.N.";
            let.SeparadorDecimalSalida = " pesos";
            //observar que sin esta propiedad queda "veintiuno pesos" en vez de "veintiún pesos":
            let.ApocoparUnoParteEntera = true;
            //let.ConvertirDecimales = true;
            label33.Text = let.ToCustomCardinal(netopago.ToString("##.00", CultureInfo.InvariantCulture));
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) GuardarInfo();
        }

        public void BuscarRecibo(string numreciboR)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from recibos where numrecibo='" + numreciboR + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {

            }
            conecta.CierraConexion();

        }

        public void GuardarInfo()
        {
          

            decimal totalAcobrar;
            bool esnumero = decimal.TryParse(textBox9.Text, out totalAcobrar);

            if (Lv2.Items.Count <= 0)
            {
                MessageBox.Show("No existen productos o servicios para el pedido verifique", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (esnumero == false)
            {
                MessageBox.Show("Tipo de dato no identificado se volvera a realizar la suma de los productos o servicios", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SumaTotales();
                textBox9.Focus();
                return;
            }
            if (totalAcobrar <= 0)
            {
                MessageBox.Show("El total debe ser mayor a 0, verifique los productos o servicios", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            valoresg.TOTALPEDIDO = "";
            valoresg.NUMPEDIDOREGISTRAR = "";


            if (Validacion() == true)
            {
                if (ExisteInfo() == false)
                {

                    valoresg.AYOPEDIDO = AYOPEDIDO;
                    valoresg.NUMPEDIDO = NUMPEDIDO;
                    valoresg.NUMPEDIDOCARGAR = NUMPEDIDO;

                    NUMRECIBOG = NUMPEDIDO;
                    valoresg.TOTALPEDIDO = textBox9.Text;
                    valoresg.NUMPEDIDOREGISTRAR = NUMPEDIDO;

                    valoresg.AGENDA_FCITAPROX = FECHADECITA;
                    valoresg.AGENDA_CVPACIENTE = CVCLIENTE;
                    valoresg.AGENDA_RECIBO = NUMPEDIDO;

                    Guardar();
                    ActualizaRemision();
                    GuardaPAGADO();
                    //AplicarNumremision(valoresg.NUMPEDIDO, valoresg.AYOPEDIDO);


                    //Reportespdf reporte = new Reportespdf();
                    //DialogResult reply = MessageBox.Show("¿Desea imprimir el recibo ?", "Recibo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (reply == DialogResult.No)
                    //    return;

                    MandarReporteCristal(valoresg.NUMPEDIDO, valoresg.AYOPEDIDO, label42.Text, label41.Text, "");

                   /// if (VENTANACOBRO == true) VentanaCobroAbrir();
                   // MessageBox.Show("Se guardo correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    LimpiarCliente();
                    label67.Visible = false;


                    //string cadena = reporte.ReporteRecibo(valoresg.NUMPEDIDO, valoresg.AYOPEDIDO);
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
                else
                {

                    //DialogResult reply = MessageBox.Show("¿Desea modificar la información del recibo?", "Recibo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (reply == DialogResult.No)
                    //{
                    //    return;
                    //}


                    valoresg.AYOPEDIDO = AYOPEDIDO;
                    valoresg.NUMPEDIDO = label17.Text;
                    valoresg.NUMPEDIDOCARGAR = label17.Text;

                    conectorSql conecta = new conectorSql();
                    string Query = "Delete from recibos where numrecibo='" + label17.Text + "'";
                    conecta.Excute(Query);

                    Query = "Delete from Detallesrecibos where numrecibo='" + label17.Text + "'";
                    conecta.Excute(Query);

                    Query = "Delete from Pagos where numrecibo='" + label17.Text + "' and cvclientE='" + textBox1.Text.Trim() + "'";
                    conecta.Excute(Query);


                    Guardar();
                    GuardaPAGADO();

                    MandarReporteCristal(label17.Text, valoresg.AYOPEDIDO, label42.Text.Trim(), label41.Text.Trim(), "");

                    Limpiar();
                    LimpiarCliente();
                    label67.Visible = false;

                    //Reportespdf reporte = new Reportespdf();
                    //string cadena = reporte.ReporteRecibo(label17.Text, AYOPEDIDO);
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
            }

        }


        public void MandarReporteCristal(string numrecibo, string ayorecibo, string consultorio, string turno, string mensajeReimpresion)
        {
            string NOMBREEMPRESA = "";
            string ADICIONALINFO = "";
            string REGIMEN = "";
            string IMPRESIONDIRECTA = "";
            string DIRECCION = "";
            string LUGAREXPIDE = "";

            string RFC = "";
            ReportDocument cryRpt = new ReportDocument();
            string CadenaReporte = @"C:\tmp\reports\ReciboTicket.rpt";
            // string CadenaReporte = @"\\SRV-DATACENTER\\tmp\\reports\\ReciboTicket.rpt";



            conectorSql conecta = new conectorSql();
            string Query = "Select * from ParametrosFactura where nombre<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                NOMBREEMPRESA = leer["nombrecomercial"].ToString();
                ADICIONALINFO = leer["infoadicional"].ToString() + " " + mensajeReimpresion;
                REGIMEN = leer["regimen"].ToString();
                DIRECCION = "Calle : " + leer["calle"].ToString();
                DIRECCION = DIRECCION + " Num. " + leer["numext"].ToString();
                DIRECCION = DIRECCION + " ," + leer["colonia"].ToString();
                DIRECCION = DIRECCION + " ," + leer["municipio"].ToString();
                DIRECCION = DIRECCION + " ," + leer["estado"].ToString();
                DIRECCION = DIRECCION + " C.P " + leer["codpostal"].ToString();
                RFC = leer["RFC"].ToString();
                LUGAREXPIDE = leer["LugarExpedicion"].ToString();
                IMPRESIONDIRECTA = leer["regimen"].ToString();
            }
            conecta.CierraConexion();

            try
            {
                cryRpt.Load(CadenaReporte);
                string consulta = "SELECT cvempresa, foto FROM Logoempresa where cvempresa='0'";
                ReciboUsuario CodigoBidimensional = GetData2(consulta, numrecibo);
                cryRpt.SetDataSource(CodigoBidimensional);

                cryRpt.SetParameterValue("Reimpressed", mensajeReimpresion);

                cryRpt.SetParameterValue("parametro1", ADICIONALINFO);
                cryRpt.SetParameterValue("regimen", REGIMEN);
                cryRpt.SetParameterValue("NombreEmpresa", NOMBREEMPRESA);
                cryRpt.SetParameterValue("direccion", DIRECCION);
                cryRpt.SetParameterValue("rfc", RFC);
                cryRpt.SetParameterValue("Lugarexpedir", LUGAREXPIDE);
                cryRpt.SetParameterValue("nombrecajero", valoresg.NOMBREUSUARIO.ToString());

                cryRpt.SetParameterValue("consultorio", consultorio);
                cryRpt.SetParameterValue("turno", turno);

                string NombreArchivo = @"C:/tmp/ReciboTicket_" + numrecibo.ToString() + ".pdf";
                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, NombreArchivo);

                cryRpt.PrintToPrinter(1, false, 0, 0);
            }
            catch (Exception)
            {
                // string NombreArchivo = @"C:/tmp/ReciboTicket_" + numrecibo.ToString() + ".pdf";
                // cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, NombreArchivo);
            } finally
            {
                cryRpt.Close();
                cryRpt.Dispose();

                MessageBox.Show("Se mando a imprimir correctamente", "Impresion de Recibo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            

            

            
            
        }


        private ReciboUsuario GetData2(string query, string numrecibo)
        {
            conectorSql sql = new conectorSql();
            
            sql.Abrirconexion();
            string CadenaConexion = sql.CADENACONEXION;
            sql.CierraConexion();
            string cmdText = "select  cvcliente,numrecibo,nombrerecibo,direccion,colonia, total, iva, totalgeneral";
            cmdText = ((cmdText + ",entregado, emitio,vendedor,tiporecibo,estatusrecibo,fechaentrega,fecha as fecharealizo" + ",cantidades,compro,precunitarios,pretotales,unidades,claves,notas,telefono,ncliente") + ",totalletra,tdescuento " + "from recibos ") + " where numrecibo='" + numrecibo + "'";
            SqlCommand command = new SqlCommand(query);
            SqlCommand command2 = new SqlCommand(cmdText);
            SqlCommand command3 = new SqlCommand("select numrecibo ,cvproducto,descripcion,cantidad,preunitario as valorunitario,precio as importe,unidad,descuento from detallesrecibos where numrecibo='" + numrecibo + "' order by progresivo asc");
            SqlDataAdapter adapter = new SqlDataAdapter();
            ReciboUsuario dataSet = new ReciboUsuario();
            using (SqlConnection connection = new SqlConnection(CadenaConexion))
            {
                using (SqlDataAdapter adapter2 = new SqlDataAdapter())
                {
                    command.Connection = connection;
                    adapter2.SelectCommand = command;
                    adapter2.Fill(dataSet, "DataTable3");

                    command2.Connection = connection;
                    adapter2.SelectCommand = command2;
                    adapter2.Fill(dataSet, "DataTable1");


                    command3.Connection = connection;
                    adapter2.SelectCommand = command3;
                    adapter2.Fill(dataSet, "DataTable2");


                    
                }
            }
            return dataSet;
        }



        private ReciboUsuario GetData(string query, string numrecibo)
        {
            conectorSql conecta = new conectorSql();
            conecta.Abrirconexion();

            string conString = conecta.CADENACONEXION;
            conecta.CierraConexion();
            string consulta = "select  numrecibo,nombrerecibo,direccion,colonia, total, iva, totalgeneral";
            consulta = consulta + ",entregado, emitio,vendedor,tiporecibo,estatusrecibo,fechaentrega,fecha as fecharealizo";
            consulta = consulta + ",cantidades,compro,precunitarios,pretotales,unidades,claves,notas,telefono from recibos ";
            consulta = consulta + " where numrecibo='" + numrecibo + "'";
            SqlCommand cmd = new SqlCommand(query);
            SqlCommand cmd2 = new SqlCommand(consulta);

            SqlDataAdapter sda2 = new SqlDataAdapter();

            ReciboUsuario ReporteM = new ReciboUsuario();
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(ReporteM, "DataTable3");

                    cmd2.Connection = con;
                    sda.SelectCommand = cmd2;
                    sda.Fill(ReporteM, "DataTable1");
                }
            }


            return ReporteM;
        }





        public void AplicarNumremision(string numpedido, string ayo)
        {
            int Numeroremision = 0;
            conectorSql conecta = new conectorSql();
            string Query = "Select numrecibo from Consecutivos where numrecibo<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                Numeroremision = int.Parse(leer["numrecibo"].ToString());
            }
            conecta.CierraConexion();

            Query = "Update recibos set numremision='" + Numeroremision.ToString() + "'";
            Query = Query + " ,fcodremision='" + DateTime.Now.ToString("yyyyMMdd") + "'";
            Query = Query + " ,fecharemision='" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
            Query = Query + " where numrecibo ='" + numpedido + "' and ayo='" + ayo + "'";
            conecta.Excute(Query);

            Query = "Update pagos set tipopago='RECIBO', remisionHist='" + Numeroremision.ToString() + "'";
            Query = Query + " where numrecibo='" + numpedido + "' and ayo='" + ayo + "' and (cvconcepto='11' or cvconcepto='1')";
            conecta.Excute(Query);

            Numeroremision++;
            Query = "Update Consecutivos set numremision='" + Numeroremision.ToString() + "'";
            conecta.Excute(Query);
        }

        public void GuardaPAGADO()
        {
            string EstatusPag = "POR PAGAR";
            if (APLICARCOMOPAGADO == true) EstatusPag = "PAGADO";

            if (radioButton1.Checked == true) EstatusPag = "PAGADO";
            else EstatusPag = "POR PAGAR";

            if (CVCLIENTE == "") CVCLIENTE = "0";

            conectorSql conecta = new conectorSql();
            string Query = "";

            // DateTime Fecha = ();

            Query = "Insert into Pagos (cvcliente";
            Query = Query + ",numpedido";
            Query = Query + ",cantidad";
            Query = Query + ",fecha";
            Query = Query + ",fechacod";
            Query = Query + ",concepto";
            Query = Query + ",cvconcepto";
            Query = Query + ",remisionHist";

            Query = Query + ",fechapago";
            Query = Query + ",fcodpago";
            Query = Query + ",emitiopago";
            Query = Query + ",pagocon";
            Query = Query + ",observacion";
            Query = Query + ",numremision"; //RECIBO ASIGNADO
            Query = Query + ",ayo";
            Query = Query + ",mes";
            Query = Query + ",estatus";
            Query = Query + ",tipopago";
            Query = Query + ",observa";
            Query = Query + ",bandera";
            Query = Query + ",numrecibo)";
            Query = Query + " values(";
            Query = Query + "'" + CVCLIENTE + "'";
            Query = Query + ",'" + NUMPEDIDO + "'";
            Query = Query + ",'" + TOTAL + "'";
            Query = Query + ",'" + FECHA.ToString() + "'";
            Query = Query + ",'" + FECHA.ToString() + "'";
            Query = Query + ",'REGISTRO DE RECIBO - " + NUMPEDIDO + "'";
            Query = Query + ",'1'"; //pendiente por verificar el cobro ESTATUS 1
            Query = Query + ",'" + NUMPEDIDO + "'";
            Query = Query + ",''";
            Query = Query + ",''";
            Query = Query + ",'.'";
            Query = Query + ",''";
            Query = Query + ",''";
            Query = Query + ",'0'";
            Query = Query + ",'" + DateTime.Now.Year.ToString() + "'";
            Query = Query + ",'" + DateTime.Now.Month.ToString() + "'";
            Query = Query + ",'" + EstatusPag + "'";
            Query = Query + ",'REGISTRO'";
            Query = Query + ",'RECIBO'";
            Query = Query + ",'0'";
            Query = Query + ",'" + NUMPEDIDO + "')"; // MODULO DE PAGOS CAMBIAR A PAGADO 
            conecta.Excute(Query);
        }

        public void GuardaCreditoSincalculo()
        {
            string EstatusPag = "POR PAGAR";
            //if (APLICARCOMOPAGADO == true) EstatusPag = "PAGADO";
            conectorSql conecta = new conectorSql();
            string Query = "";
            DateTime Fecha = DateTime.Parse(FECHA);
            Query = "Select * from pagos where cvconcepto='11' where numpedido='" + NUMPEDIDO + "' and ayo='" + DateTime.Now.Year.ToString() + "'";
            bool existeabono = conecta.ExisteRegistro(Query);

            if (existeabono == false)
            {
                Query = "Insert into Pagos (cvcliente";
                Query = Query + ",numpedido";
                Query = Query + ",cantidad";
                Query = Query + ",fecha";
                Query = Query + ",fechacod";
                Query = Query + ",concepto";
                Query = Query + ",cvconcepto";
                Query = Query + ",remisionHist";

                Query = Query + ",fechapago";
                Query = Query + ",fcodpago";
                Query = Query + ",emitiopago";
                Query = Query + ",pagocon";
                Query = Query + ",observacion";
                Query = Query + ",numremision"; //RECIBO ASIGNADO
                Query = Query + ",ayo";
                Query = Query + ",mes";
                Query = Query + ",estatus";
                Query = Query + ",numrecibo)";
                Query = Query + " values(";
                Query = Query + "'" + CVCLIENTE + "'";
                Query = Query + ",'" + NUMPEDIDO + "'";
                Query = Query + ",'" + TOTAL + "'";
                Query = Query + ",'" + Fecha.ToString("dd/MM/yyyy") + "'";
                Query = Query + ",'" + Fecha.ToString("yyyyMMdd") + "'";
                Query = Query + ",'POR PAGAR PEDIDO - " + NUMPEDIDO + "'";
                Query = Query + ",'2'"; //pendiente por verificar el cobro ESTATUS 2
                Query = Query + ",'" + NUMPEDIDO + "'";
                Query = Query + ",''";
                Query = Query + ",''";
                Query = Query + ",'.'";
                Query = Query + ",''";
                Query = Query + ",''";
                Query = Query + ",'0'";
                Query = Query + ",'" + DateTime.Now.Year.ToString() + "'";
                Query = Query + ",'" + DateTime.Now.Month.ToString() + "'";
                Query = Query + ",'" + EstatusPag + "'";
                Query = Query + ",'0')"; // MODULO DE PAGOS CAMBIAR A PAGADO 
                conecta.Excute(Query);
            }
        }

        public void GuardaCredito()
        {


            conectorSql conecta = new conectorSql();
            string Query = "insert into creditos(cvcliente";
            Query = Query + ",numpedido";
            Query = Query + ",total";
            Query = Query + ",primerpago";
            Query = Query + ",porpagar";
            Query = Query + ",tipopago";
            Query = Query + ",numpagos";
            Query = Query + ",estatus";
            Query = Query + ",fechacod";
            Query = Query + ",fecha";
            Query = Query + ",emite)";
            Query = Query + " values(";
            Query = Query + "'" + CVCLIENTE + "'";
            Query = Query + ",'" + NUMPEDIDO + "'";
            Query = Query + ",'" + TOTAL + "'";
            Query = Query + ",'" + ABONO + "'";
            Query = Query + ",'" + PORPAGAR + "'";
            Query = Query + ",'" + TIPOPAGOSCRED + "'";
            Query = Query + ",'" + NUMPAGOS + "'";
            Query = Query + ",'" + ESTATUSCRED + "'";
            Query = Query + ",'" + FECHA + "'";
            Query = Query + ",'" + FECHACOD + "'";
            Query = Query + ",'" + EMITIO + "')";

            conecta.Excute(Query);
            DateTime Fecha = DateTime.Parse(FECHA);

            Query = "Insert into Pagos (cvcliente";
            Query = Query + ",numpedido";
            Query = Query + ",cantidad";
            Query = Query + ",fecha";
            Query = Query + ",fechacod";
            Query = Query + ",concepto";
            Query = Query + ",cvconcepto";
            Query = Query + ",remisionHist";

            Query = Query + ",fechapago";
            Query = Query + ",fcodpago";
            Query = Query + ",emitiopago";
            Query = Query + ",pagocon";
            Query = Query + ",observacion";
            Query = Query + ",numremision";
            Query = Query + ",ayo";
            Query = Query + ",mes";
            Query = Query + ",estatus";
            Query = Query + ",numrecibo)";
            Query = Query + " values(";
            Query = Query + "'" + CVCLIENTE + "'";
            Query = Query + ",'" + NUMPEDIDO + "'";
            Query = Query + ",'" + ABONO + "'";
            Query = Query + ",'" + Fecha.ToString("dd/MM/yyyy") + "'";
            Query = Query + ",'" + Fecha.ToString("yyyyMMdd") + "'";
            Query = Query + ",'PRIMER PAGO POR EL PEDIDO " + NUMPEDIDO + "'";
            Query = Query + ",'11'";
            Query = Query + ",'" + NUMPEDIDO + "'";

            Query = Query + ",''";
            Query = Query + ",''";
            Query = Query + ",'.'";
            Query = Query + ",''";
            Query = Query + ",''";
            Query = Query + ",'0'";
            Query = Query + ",'" + DateTime.Now.Year.ToString() + "'";
            Query = Query + ",'" + DateTime.Now.Month.ToString() + "'";
            Query = Query + ",'POR PAGAR'";
            Query = Query + ",'0')";
            conecta.Excute(Query);

            int contador = 1;
            int numpagos = int.Parse(NUMPAGOS);
            for (int i = 0; i < numpagos; i++)
            {

                if (TIPOPAGOSCRED == "MENSUAL") Fecha = Fecha.AddMonths(1);
                if (TIPOPAGOSCRED == "QUINCENAL") Fecha = Fecha.AddDays(15);
                if (TIPOPAGOSCRED == "SEMANAL") Fecha = Fecha.AddDays(8);


                Query = "Insert into Pagos (cvcliente";
                Query = Query + ",numpedido";
                Query = Query + ",cantidad";
                Query = Query + ",fecha";
                Query = Query + ",fechacod";
                Query = Query + ",concepto";
                Query = Query + ",cvconcepto";
                Query = Query + ",remisionHist";
                Query = Query + ",fechapago";
                Query = Query + ",fcodpago";
                Query = Query + ",emitiopago";
                Query = Query + ",pagocon";
                Query = Query + ",observacion";
                Query = Query + ",numremision";
                Query = Query + ",ayo";
                Query = Query + ",mes";
                Query = Query + ",estatus";
                Query = Query + ",numrecibo)";
                Query = Query + " values(";
                Query = Query + "'" + CVCLIENTE + "'";
                Query = Query + ",'" + NUMPEDIDO + "'";
                Query = Query + ",'" + PAGOSDE + "'";
                Query = Query + ",'" + Fecha.ToString("dd/MM/yyyy") + "'";
                Query = Query + ",'" + Fecha.ToString("yyyyMMdd") + "'";
                Query = Query + ",'" + contador.ToString() + "-PAGO DEL PEDIDO " + NUMPEDIDO + "'";
                Query = Query + ",'4'";
                Query = Query + ",'" + NUMPEDIDO + "'";
                Query = Query + ",''";
                Query = Query + ",''";
                Query = Query + ",'.'";
                Query = Query + ",''";
                Query = Query + ",''";
                Query = Query + ",'0'"; //NUMERO DE REMISION RECIBO INDIVIDUAL

                Query = Query + ",'" + DateTime.Now.Year.ToString() + "'";
                Query = Query + ",'" + DateTime.Now.Month.ToString() + "'";
                Query = Query + ",'POR PAGAR'";
                Query = Query + ",'0')";

                conecta.Excute(Query);
                contador++;

            }
        }





        private void button1_Click(object sender, EventArgs e)
        {

            label67.Visible = false;
            Limpiar();
            LimpiarCliente();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Productos producto = new Productos();
            producto.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {

            BrapidaCliente brapidac = new BrapidaCliente();
            brapidac.Show();

            string cadena = "PRODUCTO NOMBRE EN SISTEMA     PZ";
            char[] arreglo = cadena.ToCharArray(0, cadena.Length);

        }

        private void RemisionPro_Activated(object sender, EventArgs e)
        {
            if (valoresg.VIENEBUSQUEDARECIBO == "SI")
            {
                textBox18.Text = Modremision.CVCLIENTE;
                valoresg.VIENEBUSQUEDARECIBO = "";
            }
            if (Modremision.CVCLIENTE != "")
            {
                textBox1.Text = Modremision.CVCLIENTE;
                BuscarInformacion(textBox1.Text);
                Modremision.CVCLIENTE = "";
                textBox3.Focus();
            }

            if (valoresg.CLAVEPAC != "")
            {
                textBox1.Text = valoresg.CLAVEPAC;
                BuscarInformacion(textBox1.Text);
                valoresg.CLAVEPAC = "";
                textBox3.Focus();
            }


            if (Modremision.CVPRODUCTO != "")
            {
                textBox3.Text = Modremision.CVPRODUCTO;
                BuscarProducto(textBox3.Text);
                Modremision.CVPRODUCTO = "";
                textBox4.Focus();
            }


            if (valoresg.NUMPEDIDOCARGAR != "")
            {
                textBox18.Text = "";

                textBox11.Text = valoresg.NUMPEDIDOCARGAR;
                button3_Click(sender, e);
                valoresg.NUMPEDIDOCARGAR = "";
                textBox11.Focus();
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            BrapidaProducto brapproducto = new BrapidaProducto();
            brapproducto.Show();
        }

        private void RemisionPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) button8_Click(sender, e);
            if (e.KeyCode == Keys.F3) button9_Click(sender, e);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void GuardarFactura()
        {
            RFCEmitio = EmpresaRFC;
            subtotal = decimal.Parse(label9.Text);
            total = decimal.Parse(textBox9.Text);
            TImpuestoTrasladado = decimal.Parse(label12.Text);
            TImporte = decimal.Parse(label12.Text);

            conectorSql conecta = new conectorSql();
            string Query = "Insert into Facturas(";
            Query = Query + " numfactura";
            Query = Query + ",estatus";
            Query = Query + ",idsistemapadre";
            Query = Query + ",edocomprobante";
            Query = Query + ",tipo";
            Query = Query + ",RFCEmitio";
            Query = Query + ",CondicionesPago";
            Query = Query + ",FormaPago";
            Query = Query + ",Descuento";
            Query = Query + ",motivoDescuento";
            Query = Query + ",metodoPago";
            Query = Query + ",subtotal";
            Query = Query + ",total";
            Query = Query + ",REClave";
            Query = Query + ",ReNombre";
            Query = Query + ",ReRFC";
            Query = Query + ",ReCalle";
            Query = Query + ",ReCodpostal";
            Query = Query + ",ReColonia";
            Query = Query + ",ReEstado";
            Query = Query + ",ReLocalidad";
            Query = Query + ",ReMunicipio";
            Query = Query + ",ReNoExterior";
            Query = Query + ",ReNoInterior";
            Query = Query + ",ReTel";
            Query = Query + ",RePais";
            Query = Query + ",ReReferencia";
            Query = Query + ",Recorreo";
            Query = Query + ",TImpuestosRetenido";
            Query = Query + ",TImpuestoTrasladado";
            Query = Query + ",RImpuesto";
            Query = Query + ",RImporte";
            Query = Query + ",TImpuesto";
            Query = Query + ",TImporte";
            Query = Query + ",TTasa";
            Query = Query + ",Notas";
            Query = Query + ",moneda";
            Query = Query + ",TipoCambio";
            Query = Query + ",Vendedor";
            Query = Query + ",OrdCompra";
            Query = Query + ",Otros";
            Query = Query + ",numCtaPago";
            Query = Query + ",numpedido";

            Query = Query + ",fecha";
            Query = Query + ",fechacod";
            Query = Query + ",hora";

            Query = Query + ",Fechafactura";
            Query = Query + ",Fcodfactura";
            Query = Query + ",Horafactura";
            Query = Query + ",imagenCBB";
            Query = Query + ",cadenaOriginal";
            Query = Query + ",UUID";
            Query = Query + ",selloCFD";
            Query = Query + ",selloSat";
            Query = Query + ",serieSat";
            Query = Query + ",Emitio";


            Query = Query + ",ayo";
            Query = Query + ",mes)";

            Query = Query + " values(";
            Query = Query + "'" + numfactura + "'";
            Query = Query + ",'" + estatus + "'";
            Query = Query + ",'" + idsistemapadre + "'";
            Query = Query + ",'" + edocomprobante + "'";
            Query = Query + ",'" + tipo + "'";
            Query = Query + ",'" + RFCEmitio + "'";
            Query = Query + ",'" + CondicionesPago + "'";
            Query = Query + ",'" + FormaPago + "'";
            Query = Query + ",'" + Descuento + "'";
            Query = Query + ",'" + motivoDescuento + "'";
            Query = Query + ",'" + metodoPago + "'";
            Query = Query + ",'" + subtotal + "'";
            Query = Query + ",'" + total + "'";
            Query = Query + ",'" + REClave + "'";
            Query = Query + ",'" + ReNombre + "'";
            Query = Query + ",'" + ReRFC + "'";
            Query = Query + ",'" + ReCalle + "'";
            Query = Query + ",'" + ReCodpostal + "'";
            Query = Query + ",'" + ReColonia + "'";
            Query = Query + ",'" + ReEstado + "'";
            Query = Query + ",'" + ReLocalidad + "'";
            Query = Query + ",'" + ReMunicipio + "'";
            Query = Query + ",'" + ReNoExterior + "'";
            Query = Query + ",'" + ReNoInterior + "'";
            Query = Query + ",'" + ReTel + "'";
            Query = Query + ",'" + RePais + "'";
            Query = Query + ",'" + ReReferencia + "'";
            Query = Query + ",'" + Recorreo + "'";
            Query = Query + ",'" + TImpuestosRetenido + "'";
            Query = Query + ",'" + TImpuestoTrasladado + "'";
            Query = Query + ",'" + RImpuesto + "'";
            Query = Query + ",'" + RImporte + "'";
            Query = Query + ",'" + TImpuesto + "'";
            Query = Query + ",'" + TImporte + "'";
            Query = Query + ",'" + TTasa + "'";
            Query = Query + ",'" + Notas + "'";
            Query = Query + ",'" + moneda + "'";
            Query = Query + ",'" + TipoCambio + "'";
            Query = Query + ",'" + Vendedor + "'";
            Query = Query + ",'" + OrdCompra + "'";
            Query = Query + ",'" + Otros + "'";
            Query = Query + ",'" + numCtaPago + "'";
            Query = Query + ",'" + NUMPEDIDO + "'";

            Query = Query + ",'" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
            Query = Query + ",'" + DateTime.Now.ToString("yyyyMMdd") + "'";
            Query = Query + ",'" + DateTime.Now.ToString("HH:mm:ss") + "'";

            Query = Query + ",''"; //PARAMETROS DE FECHA DE FACTURACION Y SELLADO
            Query = Query + ",''";
            Query = Query + ",''";
            Query = Query + ",''";
            Query = Query + ",''";
            Query = Query + ",''";
            Query = Query + ",''";
            Query = Query + ",''";
            Query = Query + ",''";
            Query = Query + ",'" + EMITIO + "'";


            Query = Query + ",'" + AYOPEDIDO + "'";
            Query = Query + ",'" + MESPEDIDO + "')";
            conecta.Excute(Query);



            string numfacturaD = "0";
            int numpartida = 0;
            float cantidad = 0;
            string descripcion = "";
            float importe = 0;
            string cvproducto = "";
            string unidad = "";
            float valorUnitario = 0;
            string pedimentonum = "";
            string pedimentonombre = "";
            string pedimentofecha = "";
            float iva = 16;
            string notas1 = "";
            string notas2 = "";

            for (int i = 0; i < Lv2.Items.Count; i++)
            {

                numfacturaD = "0";
                numpartida++;
                cantidad = float.Parse(Lv2.Items[i].SubItems[1].Text);
                descripcion = Lv2.Items[i].SubItems[3].Text;
                importe = float.Parse(Lv2.Items[i].SubItems[5].Text);
                cvproducto = Lv2.Items[i].SubItems[2].Text;
                unidad = Lv2.Items[i].Text;
                valorUnitario = float.Parse(Lv2.Items[i].SubItems[4].Text);
                pedimentonum = "";
                pedimentonombre = "";
                pedimentofecha = "";
                iva = 16;
                notas1 = Lv2.Items[i].SubItems[9].Text;
                string coniva = Lv2.Items[i].SubItems[10].Text;

                if (coniva == "NO") iva = 0;
                else iva = 16;

                Query = "Insert into DetallesFacturas(";
                Query = Query + "numfactura";
                Query = Query + ",numpartida";
                Query = Query + ",cantidad";
                Query = Query + ",descripcion";
                Query = Query + ",importe";
                Query = Query + ",cvproducto";
                Query = Query + ",unidad";
                Query = Query + ",valorUnitario";
                Query = Query + ",pedimentonum";
                Query = Query + ",pedimentonombre";
                Query = Query + ",pedimentofecha";
                Query = Query + ",iva";
                Query = Query + ",notas1";
                Query = Query + ",notas2";

                Query = Query + ",fechacod";
                Query = Query + ",fecha";
                Query = Query + ",mes";
                Query = Query + ",ayo";


                Query = Query + ",numpedido)";

                Query = Query + " values(";
                Query = Query + "'" + numfacturaD + "'";
                Query = Query + ",'" + numpartida + "'";
                Query = Query + ",'" + cantidad + "'";
                Query = Query + ",'" + descripcion + "'";
                Query = Query + ",'" + importe + "'";
                Query = Query + ",'" + cvproducto + "'";
                Query = Query + ",'" + unidad + "'";
                Query = Query + ",'" + valorUnitario + "'";
                Query = Query + ",'" + pedimentonum + "'";
                Query = Query + ",'" + pedimentonombre + "'";
                Query = Query + ",'" + pedimentofecha + "'";
                Query = Query + ",'" + iva + "'";
                Query = Query + ",'" + notas1 + "'";
                Query = Query + ",'" + notas2 + "'";

                Query = Query + ",'" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
                Query = Query + ",'" + DateTime.Now.ToString("yyyyMMdd") + "'";
                Query = Query + ",'" + DateTime.Now.Month.ToString() + "'";
                Query = Query + ",'" + DateTime.Now.Year.ToString() + "'";


                Query = Query + ",'" + NUMPEDIDO + "')";
                conecta.Excute(Query);
            }

        }



        private void button11_Click(object sender, EventArgs e)
        {
            //CalcularCredito();
        }

        /*
        public void CalcularCredito()
        {
           
            decimal pagosde = 0;

            decimal total = decimal.Parse(textBox9.Text);
            

            decimal porpagar= total - primerpago;
            pagosde = porpagar / numpagos;
            

            PAGOSDE = textBox14.Text;
            NUMPAGOS = numpagos.ToString();
            TIPOPAGOSCRED = comboBox2.ToString();
            ABONO = primerpago.ToString("##.00", CultureInfo.InvariantCulture);
            PORPAGAR = porpagar.ToString("##.00", CultureInfo.InvariantCulture);
            ESTATUSCRED = "POR PAGAR";
        }
        */


        private void textBox14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox9.Focus();
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);
        }

        private void comboBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);

        }


        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            valoresg.TEXTODESCRIPCION = textBox8.Text;
            valoresg.DETALLE1 = "";
            VistaDescripcion verdes = new VistaDescripcion();
            verdes.Show();
        }

        private void Lv2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                EliminarConcepto();
            }
        }


        public void EliminarConcepto()
        {
            try
            {

                if (Lv2.SelectedItems.Count > 0)
                {
                    ListView.SelectedIndexCollection seleccion = Lv2.SelectedIndices;
                    foreach (int item in seleccion)
                    {
                        Lv2.Items.RemoveAt(item);
                    }
                    SumaTotales();
                }
            }
            catch (Exception)
            {

                //  throw;
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {


        }




        private void button19_Click(object sender, EventArgs e)
        {
            Clientes nuevo = new Clientes();
            nuevo.Show();
        }

        public string NUMRECIBOG = "";
        private void button20_Click(object sender, EventArgs e)
        {
            if (Lv2.Items.Count == 0)
            {
                MessageBox.Show("Falta por ingresar productos o servicios para registrarlo, gracias", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            button20.Enabled = false;


            string consulta = "Update recibos set cvcliente='0' where cvcliente=''";
            conectorSql conecta = new conectorSql();
            conecta.Excute(consulta);

            NUMRECIBOG = "0";
            Recolectar();
            // registrar el cobro

            GuardarInfo();
            valoresg.CVPACIENTECITAR = CVCLIENTE;
            valoresg.AGENDA_CVPACIENTE = CVCLIENTE;
            valoresg.NUMPEDIDOREGISTRAR = NUMPEDIDO;


            //panel28.Visible = true;
            button20.Enabled = true;
            VentaRegistroCobro registro = new VentaRegistroCobro(NUMPEDIDO);
            registro.ShowDialog();


        }

        public void VentanaCobroAbrir()
        {
            CambioaCliente ventacambio = new CambioaCliente(label17.Text, textBox9.Text);
            ventacambio.ShowDialog();
        }

        //public void HilodeCobro()
        //{
        //    Thread t = new Thread(VentanaCobroAbrir);
        //    t.Start();
        //}

        /*
       
        private void button7_Click_1(object sender, EventArgs e)
        {
            if (validaCredito() == true)
            {
                CalcularCredito();
                GuardaCredito();
            }
            MessageBox.Show("Se guardo correctamente el credito", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        */
        private void button15_Click(object sender, EventArgs e)
        {
            Modremision.CANCELANUMRECIBO = label50.Text;
            CancelaRecibo cancela = new CancelaRecibo();
            cancela.ShowDialog();
        }






        private void button14_Click(object sender, EventArgs e)
        {


        }




        public void BuscarConceptoPago(string clave)
        {
            for (int i = 0; i < Lv.Items.Count; i++)
            {
                if (Lv.Items[i].Text == "NO APLICA" && Lv.Items[i].SubItems[2].Text == clave)
                    Lv.Items.RemoveAt(i);
            }

        }





        private void textBox15_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode) button14_Click(sender, e);
        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Lv.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = Lv.SelectedIndices;
                foreach (int item in seleccion)
                {
                    DetallesModificaVer(item);
                }
            }
        }

        public void DetallesModificaVer(int index)
        {
            string numpedido = Lv.Items[index].Text;
            string ayo = Lv.Items[index].SubItems[7].Text;
            string estatus = Lv.Items[index].SubItems[8].Text;
            label45.Text = Lv.Items[index].SubItems[13].Text;
            label46.Text = Lv.Items[index].SubItems[14].Text;
            label50.Text = numpedido;
            label51.Text = ayo;
            label53.Text = estatus;
            label36.Text = Lv.Items[index].SubItems[12].Text;
            // Label47 corresponds to IsPrinted value
            label47.Text = Lv.Items[index].SubItems[15].Text;
            PosicionVer = index;
        }



        private void button22_Click(object sender, EventArgs e)
        {

            string numpedido = label50.Text;
            string ayo = label51.Text;
            string estatuspedido = label53.Text;
            string nombrearea = "";
            string msgReimpressed = "";

            string sqlGetRecibo = "SELECT * FROM Recibos where numrecibo = " + label50.Text + "";
            conectorSql conectar = new conectorSql();
            SqlDataReader leerGetRecibo = conectar.RecordInfo(sqlGetRecibo);
            while (leerGetRecibo.Read())
            {
                nombrearea = leerGetRecibo["nombrerecibo"].ToString();
                ayo = leerGetRecibo["ayo"].ToString();
                estatuspedido = leerGetRecibo["estatusrecibo"].ToString();
                msgReimpressed = leerGetRecibo["printed"].ToString();

            }
            conectar.CierraConexion();

            if (estatuspedido == "CANCELADO")
            {
                MessageBox.Show("El pedido ya se encuentra cancelado, realize otro pedido", "Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (numpedido=="0")
            {
                MessageBox.Show("No se ha seleccionado ningún pedido", "Recibo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Only ADMIN can reimpressed tickets
            string user = valoresg.USUARIOSIS;
            
            // UPDATE MESSAGE in case the ticket were reimpressed before
            if (msgReimpressed == "1")
            {
                msgReimpressed = "COPIA DE TICKET SIN VALOR";
            }

            /*
             *   Update the ticket status to 1 in order to avoid reimpression
             *   Validate if the user is not ADMIN and the ticket was impressed before, then not print
             *   If the ticket is 0 that means the ticket never be printed before, then go ahead and print
            */
            if (msgReimpressed  == "0" || user == "ADMIN")
            {
                MessageBox.Show("Se mandara a imprimir el recibo seleccionado ", "Recibo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                try
                {
                    conectorSql conecta2 = new conectorSql();

                    // Update Ticket status FIRST
                    string update = "UPDATE Recibos SET printed = 1 WHERE numrecibo =  " + numpedido;
                    SqlDataReader updateReader = conecta2.RecordInfo(update);
                    
                    // Validate if idDoctor is distinct to zero, in that case we need to get the "nombre" from doctores table 
                    if (label45.Text != "0")
                    {
                        string consulta2 = "Select  nombre from Doctores where cvdoctor='" + label45.Text.Trim() + "'";
                        SqlDataReader leer2 = conecta2.RecordInfo(consulta2);
                        while (leer2.Read())
                        {
                            nombrearea = leer2["nombre"].ToString();
                        }
                        
                    } else
                    {
                        conecta2.CierraConexion();
                    }

                    MailNotifications mail = new MailNotifications();
                    if (label47.Text == "1")
                    {
                        mail.SendMailRePrintTickets(numpedido, ayo, nombrearea);
                    }
                    MandarReporteCristal(numpedido, ayo, nombrearea, label46.Text.Trim(), msgReimpressed);
                }
                catch (Exception E)
                {

                    MessageBox.Show("Ocurrió un error " + E.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            else
            {
                MessageBox.Show("El ticket sólo puede ser impreso una vez, consulte al Administrador para más información", "Recibo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            

            
        }


        public void AplicarNumCotizacion(string numpedido, string ayo)
        {
            int Numcotizacion = 0;
            conectorSql conecta = new conectorSql();
            string Query = "Select numcotiza from Consecutivos where numcotiza<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                Numcotizacion = int.Parse(leer["numcotiza"].ToString());
            }
            conecta.CierraConexion();

            Query = "Update recibos set numcotizacion='" + Numcotizacion.ToString() + "'";
            Query = Query + " ,fcodcotiza='" + DateTime.Now.ToString("yyyyMMdd") + "'";
            Query = Query + " ,fechacotiza='" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
            Query = Query + " where numrecibo ='" + numpedido + "' and ayo='" + ayo + "'";
            conecta.Excute(Query);

            Numcotizacion++;
            Query = "Update Consecutivos set numcotiza='" + Numcotizacion.ToString() + "'";
            conecta.Excute(Query);
        }


        private void button24_Click(object sender, EventArgs e)
        {
            LimpiarProductoNuevo();
        }
        public void LimpiarProductoNuevo()
        {
            label30.Text = "";
            textBox3.Text = "";
            comboBox4.Text = "";
            textBox4.Text = "";
            textBox8.Text = "";
            comboBox3.Text = "";
            comboBox3.Items.Clear();
            textBox3.Focus();
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CatBancos altabanco = new CatBancos();
            altabanco.Show();
        }

        private void textBox18_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void Lv_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Sorter s = (Sorter)Lv.ListViewItemSorter;
            s.Column = e.Column;

            if (s.Order == System.Windows.Forms.SortOrder.Ascending)
            {
                s.Order = System.Windows.Forms.SortOrder.Descending;
            }
            else
            {
                s.Order = System.Windows.Forms.SortOrder.Ascending;
            }
            Lv.Sort();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            valoresg.VIENEBUSQUEDARECIBO = "NO";
            BrapidaCliente cliente = new BrapidaCliente();
            cliente.Show();
        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HistorialClinica.Pacientes mostrar = new HistorialClinica.Pacientes();
            mostrar.Show();
        }

        private void Lv2_DoubleClick(object sender, EventArgs e)
        {
            KeyEventArgs m = new KeyEventArgs(Keys.Enter);
            if (Lv2.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = Lv2.SelectedIndices;
                foreach (int item in seleccion)
                {
                    DetallesModificaVerProducto(item, sender, m);
                }
            }
        }


        private void Lv2_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        public void DetallesModificaVerProducto(int index, object sender, KeyEventArgs e)
        {
            textBox3.Text = Lv2.Items[index].SubItems[2].Text;
            textBox3_KeyDown(sender, e);
            textBox4.Text = Lv2.Items[index].SubItems[1].Text;
            textBox8.Text = Lv2.Items[index].SubItems[3].Text;
            comboBox4.Text = Lv2.Items[index].Text;
            comboBox3.Text = Lv2.Items[index].SubItems[4].Text;
            textBox4.Focus();
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            panel10.Visible = false;
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            combos.ColoniasRecibosporentregar(comboBox2);
            panel10.Visible = true;
        }

        private void Lv_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void Lv_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Lv.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        public int PosicionVer;
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string query = "Update recibos set entregado='SI' where numrecibo='" + label50.Text + "' and ayo='" + label51.Text + "'";
            conecta.Excute(query);
            Lv.Items[PosicionVer].SubItems[11].Text = "SI";
            CambioDeColoresCeldaEntregas();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string query = "Update recibos set entregado='NO' where numrecibo='" + label50.Text + "' and ayo='" + label51.Text + "'";
            conecta.Excute(query);
            Lv.Items[PosicionVer].SubItems[11].Text = "NO";
            CambioDeColoresCeldaEntregas();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = false;
            radioButton1.Checked = false;
            if (comboBox8.Text == "MOSTRADOR")
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox10.Focus();
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox3.Focus();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            VentaRegistroCobro registro = new VentaRegistroCobro();
            registro.ShowDialog();
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            CortePagosDiario corted = new CortePagosDiario();
            corted.Show();
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            Modremision.CANCELANUMRECIBO = label50.Text;
            CancelaRecibo cancela = new CancelaRecibo();
            cancela.ShowDialog();
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            HistorialClinica.RegistroCitas regcitas = new HistorialClinica.RegistroCitas();
            regcitas.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            OpenCashDrawer();
        }

        //private void openCashDrawer()
        //{
        //    byte[] source = new byte[] { 0x1b, 0x70, 0x30, 0x37, 0x79 };
        //    IntPtr destination = new IntPtr(0);
        //    destination = Marshal.AllocCoTaskMem(5);
        //    Marshal.Copy(source, 0, destination, 5);
        //    RawPrinterHelper.SendBytesToPrinter("EPSON TM-T88IV Receipt", destination, 5);
        //    Marshal.FreeCoTaskMem(destination);
        //}

        public void OpenCashDrawer()
        {
            RawPrinter envio = new RawPrinter();

            byte[] source = new byte[] { 0x1b, 0x70, 0x30, 0x37, 0x79 };
            IntPtr destination = new IntPtr(0);
            destination = Marshal.AllocCoTaskMem(5);
            Marshal.Copy(source, 0, destination, 5);
            //RawPrinterHelper.SendBytesToPrinter("EPSON TM-T88IV Receipt", destination, 5);
            string cfnFile = @"\\SRV-DATACENTER\tmp\EmailConf.xml";
            bool cfnExist = File.Exists(cfnFile);
            XDocument xdoc = XDocument.Load(cfnExist ? @"\\SRV-DATACENTER\tmp\EmailConf.xml" : @"C:\tmp\EmailConf.xml");
            envio.SendBytesToPrinter(xdoc.Descendants("PrinterName").First().Value, destination, 5);
            Marshal.FreeCoTaskMem(destination);

        }

        private void button16_Click_2(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string Query = "";
            valoresg.CVPACIENTECITAR = label36.Text;
            valoresg.NUMRECIBOREG = label54.Text;

            Query = "Select * from citas where recibopago='" + label50.Text + "'";
            bool existere = conecta.ExisteRegistro(Query);
            if (existere == true)
            {
                MessageBox.Show("El num de recibo ya fue asignado, verifique", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            ActivacionCitas activador = new ActivacionCitas();
            activador.Show();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
               // CargarPacientesporPagar();

            }
            catch (Exception er)
            {
                MessageBox.Show("Error dentro de cargar pacientes" + er.Message.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //listView1.Visible = checkBox2.Checked;
            //if (checkBox2.Checked == true)
            //    timer2.Enabled = true;
            //else timer2.Enabled = false;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = listView1.SelectedIndices;
                foreach (int item in seleccion)
                {
                    DetallesModifica2(item);
                }
            }
        }

        public string ClavePaciente = "";
        public void DetallesModifica2(int index)
        {
            ClavePaciente = listView1.Items[index].Text;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Text = ClavePaciente;
            LimpiarCliente();
            BuscarInformacion(textBox1.Text);
            if (textBox1.Text == "0") textBox2.Focus();
            else textBox3.Focus();
        }

        private void button18_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            // cada vez que cambia 
    

        }

    }









}

