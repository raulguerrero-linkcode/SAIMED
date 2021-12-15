using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Data.SqlClient;
namespace SHOPCONTROL
{
    public partial class FacturarPedidoM : Form
    {
        public FacturarPedidoM()
        {
            InitializeComponent();
            toolTip1.InitialDelay = 0; 
        }

        private ToolTip toolTip = new ToolTip();
        private bool isShown = false; 


        public string CVCLIENTE = "";
        public string NOMBRE = "";
        public string EMPRESA= "";
        public string CORREO= "";
     

        public string CLAVEPRODUCTO= "";
        public string NUMREMISION= "";
        public string FECHA= "";
        public string FECHACOD = "";
        
        public string UNIDAD= "";
        public string CANTIDAD = "";
        public string NOMBREPRO= "";
        public string PRECIOUNITARIO= "";
        public string PRECIOTOTAL= "";

        public string CADCLAVE = "";
        public string CADUNIDAD = "";
        public string CADCANTIDAD = "";
        public string CADNOMBREPRO = "";
        public string CADPRECIOUNITARIO = "";
        public string CADPRECIOTOTAL = "";

        
        public string SUBTOTAL= "";
        public string IVA= "";
        public string TOTAL= "";

        public string ESTATUS= "";
        public string TPAGADO= "";
        public string EMITIO = "";

        public string ABONO= "";
        public string NUMPAGOS= "";
        public string PAGOSDE= "";
        public string TIPOPAGOSCRED= "";
        public string PORPAGAR= "";
        public string ESTATUSCRED= "";

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

        public int numfactura=0;
        public string estatus="CAPTURADO";
        public string idsistemapadre="";
        public int edocomprobante = 0;
        public string tipo="";
        public string RFCEmitio="";
        public string CondicionesPago="";
        public string FormaPago="";
        public decimal Descuento = 0;
        public string motivoDescuento="";
        public string metodoPago="";
        public decimal subtotal = 0;
        public decimal total = 0;
        public string REClave="";
        public string ReNombre="";
        public string ReRFC="";
        public string ReCalle="";
        public string ReCodpostal="";
        public string ReColonia="";
        public string ReEstado="";
        public string ReLocalidad="";
        public string ReMunicipio="";
        public string ReNoExterior="";
        public string ReNoInterior="";
        public string ReTel="";
        public string RePais="";
        public string ReReferencia="";
        public string Recorreo="";
        public decimal TImpuestosRetenido = 0;
        public decimal TImpuestoTrasladado = 0;
        public string RImpuesto="";
        public decimal RImporte = 0;
        public string TImpuesto="";
        public decimal TImporte = 0;
        public int TTasa = 0;
        public string Notas="";
        public string moneda="";
        public decimal TipoCambio=0;
        public string Vendedor="";
        public string OrdCompra="";
        public string Otros="";
        public string numCtaPago = "";

        public string numcotizacion = "0";
        public string numremision = "0";
        public string fechacotiza = "";
        public string fcodcotiza = "";
        public string fecharemision = "";
        public string fcodremision = "";
        public string estatuspedido = "";
        public string VENDEDORGEN = "";
        public void Recolectar()
        {
          
            CVCLIENTE = textBox1.Text;
            NOMBRE = textBox2.Text;
            EMPRESA = textBox10.Text;
            CORREO = textBox3.Text;

            BANCO = label55.Text;
            ESTATUS = label52.Text;
            NUMPEDIDO = label17.Text;
            SUBTOTAL = label9.Text;
            IVA = label12.Text;
            TOTAL= textBox9.Text;
            TDISTRIBUIDOR = label32.Text;
            TGANANCIA= label31.Text;
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
            numfactura=0;
            estatus="CAPTURADO";
            estatuspedido = "CAPTURADO";
            idsistemapadre="0";
            edocomprobante = 1;

            tipo = "FA";
            RFCEmitio = "";

            if (radioButton1.Checked == true)
                CondicionesPago = "Contado";
            else
                CondicionesPago = textBox17.Text + " dias";

            FormaPago = label66.Text ;
            if (FormaPago == "") FormaPago = "PAGO EN UNA SOLA EXHIBICIÓN";

            Descuento = 0;
            motivoDescuento = "";

            metodoPago = label52.Text;
            subtotal = 0;
            total = 0;

            REClave = textBox1.Text;
            ReNombre = textBox2.Text;
            ReRFC = textBox19.Text;


            ReCalle = textBox23.Text;
            ReCodpostal = textBox22.Text;
            ReColonia = textBox21.Text;
            ReEstado = textBox5.Text;
            ReLocalidad = "";
            ReMunicipio = textBox20.Text;
            ReNoExterior = textBox24.Text;
            ReNoInterior = "0";
            ReTel = label37.Text;
            ReReferencia = "";
            Recorreo = textBox25.Text;
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
            Vendedor = comboBox8.Text;
            OrdCompra = "OC-" + label17.Text;
            Otros = "";
            numCtaPago = label48.Text;
            VENDEDORGEN = label1.Text;
            if (BANSELECCIONAVENDEDOR == true)
            {
                VENDEDORGEN = comboBox8.Text;
                Vendedor = VENDEDORGEN;
            }

        }

        public bool Validacion()
        {
            if (NOMBRE == "")
            {
                MessageBox.Show("Ingrese el nombre completo del cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return false;
            }


          

            if (ESTATUS == "")
            {
                MessageBox.Show("Seleccione el Estatus de la remision", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return false;
            }



            if (radioButton2.Checked == true)
            {
                if (textBox17.Text == "")
                {
                    MessageBox.Show("Ingrese la cantidad de dias de credito", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox17.Focus();
                    return false;
                }
            }

            if (BANSELECCIONAVENDEDOR == true)
            {
                if (comboBox8.Text== "")
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

            Lv.Columns.Add("Num. Pedido", 55).Tag = "NUMBER";
            Lv.Columns.Add("Nombre cliente", 180).Tag = "STRING";
            Lv.Columns.Add("Fecha", 80).Tag = "STRING";
            Lv.Columns.Add("subtotal", 70).Tag = "STRING";
            Lv.Columns.Add("iva", 60).Tag = "STRING";
            Lv.Columns.Add("total", 70).Tag = "STRING";
            Lv.Columns.Add("Forma Pago", 50).Tag = "STRING";
            Lv.Columns.Add("Descripción", 320).Tag = "STRING";
            Lv.Columns.Add("Año", 0).Tag = "STRING";
            Lv.Columns.Add("Estatus Pedido", 110).Tag = "STRING";

            conectorSql conecta = new conectorSql();
            string Query = "Select distinct(numpedido), pedidos.nombre as Nombrecliente ";
            Query = Query + ",fecha,total,iva,totalgeneral,status,compro,ayo,estatuspedido";
            Query = Query + " from pedidos ";
            Query = Query + " inner join clientes on clientes.cvcliente=pedidos.cvcliente";
            Query=Query + " where numpedido<>''";

            if (checkBox1.Checked == false) Query = Query + " and fechacod between '" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.ToString("yyyyMMdd") + "'";
            if (textBox11.Text != "") Query = Query + " and numpedido='" + textBox11.Text + "'";
            if (comboBox5.Text != "") Query = Query + " and estatuspedido='" + comboBox5.Text + "'";
            if (textBox18.Text != "") Query = Query + " and pedidos.cvcliente='" + textBox18.Text + "'";
            if (textBox6.Text != "") Query = Query + " and pedidos.nombre like '%" + textBox6.Text + "%'";
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
                lvi.SubItems.Add(leer["status"].ToString());
                lvi.SubItems.Add(leer["compro"].ToString());
                lvi.SubItems.Add(leer["ayo"].ToString());
                lvi.SubItems.Add(leer["estatuspedido"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label15.Text = Lv.Items.Count.ToString() + " Pedidos ";
            CambioDeColoresCelda();
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
            comboBox1.Visible = false;
            ColumnasProducto();
            Lv2.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
          
            textBox8.Text = "";
         
            textBox10.Text = "";
            textBox11.Text = "";
          
         
            textBox16.Text = "";
            label52.Text= "";
         
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

            textBox19.Text = "";
            label37.Text = "";
            label48.Text = "0";
            label52.Text= "No Identificado";
            checkBox1.Checked = true;
            combos.ComboVendedores(comboBox8);
            label67.Visible = false;
            textBox1.Focus();
        }

        public void LimpiarCliente()
        {
            label48.Text = "";
            label55.Text = "";
            label52.Text = "";
            textBox2.Text = "";
            textBox10.Text = "";

            label37.Text="";
            textBox19.Text = "";
            textBox5.Text = "";
            textBox20.Text = "";
            textBox21.Text = "";
            textBox22.Text = "";
            textBox23.Text = "";
            textBox24.Text = "";
            textBox25.Text = "";
            label66.Text = "";

   


        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            label67.Visible = false;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            
            BuscarParametros();
            combos.UnidadesProductos(comboBox4);
            label1.Text = valoresg.USUARIOSIS;
            Limpiar();
            RFCEmpresa();
            textBox11.Focus();
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
        public void ConsecutivoRemision()
        {
            if (label67.Visible == false)
            {
                string Numero = "1";
                conectorSql conecta = new conectorSql();
                string Query = "Select numpedido  from consecutivos where numpedido<>''";
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    Numero = leer["numpedido"].ToString();
                }
                conecta.CierraConexion();
                label17.Text = Numero;
            }
        }

        public bool ActualizaRemision()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = int.Parse(label17.Text) + 1;
            string Query = "update consecutivos set numpedido='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }


        public void BuscarInformacion(string clave)
        {
            Limpiar();
            LimpiarCliente();
            conectorSql conecta = new conectorSql();
            string Query = "";
            if (clave != "")
            {
                Query = "Select * from clientes where cvcliente='" + clave + "'";
                bool existeCliente = conecta.ExisteRegistro(Query);
                if (existeCliente == false)
                {
                    MessageBox.Show("No existe la clave del cliente, verifique por favor", "Aviso", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }

            Query = "Select * from clientes where cvcliente='" + clave + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox1.Text = clave;
                textBox2.Text = leer["nombre"].ToString();
                textBox10.Text = leer["empresa"].ToString();
                textBox5.Text = leer["direccion"].ToString();
                textBox19.Text = leer["RFC"].ToString();
                label37.Text = leer["telefono"].ToString();
                textBox16.Text = leer["observafact"].ToString();

                label52.Text = leer["metodopago"].ToString();
                label48.Text = leer["numcuenta"].ToString();
                label55.Text = leer["cvbanco"].ToString();
                label66.Text = leer["formapago"].ToString();

                textBox20.Text = leer["MunicipioE"].ToString();
                textBox23.Text= leer["calleF"].ToString();
                textBox22.Text = leer["CodF"].ToString();
                textBox21.Text = leer["ColoniaF"].ToString();
                textBox5.Text = leer["EstadoF"].ToString();
                ReLocalidad = "";
                textBox20.Text = leer["MunicipioF"].ToString();
                textBox24.Text = leer["numF"].ToString();
                ReNoInterior = "0";
                RePais = leer["PaisF"].ToString();
                ReReferencia = "";
                textBox25.Text = leer["email"].ToString();

                string valor= leer["tipopago"].ToString();
                if (valor == "Contado") radioButton1.Checked = true;
                else radioButton2.Checked = true;

                textBox17.Text = leer["diascredito"].ToString();

                if (label48.Text.Length < 4) label48.Text = "0000";
                comboBox8.Text = leer["vendedor"].ToString();
            }
            conecta.CierraConexion();
        }


        public void BuscarProducto(string clave)
        {
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
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
               radioButton3.Checked = false;
               radioButton4.Checked = false;
               if (leer["causaiva"].ToString() == "SI") radioButton3.Checked = true;
               else radioButton4.Checked = true;


                string consulta = "select * from ListaPrecios where cvproducto='" + clave + "'";
                SqlDataReader leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    decimal distribuidor=decimal.Parse(leer2["distribuidor"].ToString());
                    decimal precio1=decimal.Parse(leer2["publico1"].ToString());
                    decimal precio2 = decimal.Parse(leer2["publico2"].ToString());
                    decimal precio3 = decimal.Parse(leer2["publico3"].ToString());
                    if (precio1 > 0 && precio1 != distribuidor) comboBox3.Items.Add(precio1.ToString());
                    if (precio2 > 0 && precio2 != distribuidor) comboBox3.Items.Add(precio2.ToString());
                    if (precio3 > 0 && precio3 != distribuidor) comboBox3.Items.Add(precio3.ToString());
                    comboBox3.Text = precio1.ToString();
                    label20.Text= distribuidor.ToString();          

                }
                conecta2.CierraConexion();
                if (HABILITAPRECIO == true) comboBox3.DropDownStyle = ComboBoxStyle.DropDown;
                else comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
                  
                textBox4.Focus();

            }
            conecta.CierraConexion();
        }

    
        public void Guardar()
        {
            CADUNIDAD = "";
            CADCANTIDAD = "";
            CADCLAVE = "";
            CADNOMBREPRO = "";
            CADPRECIOUNITARIO = "";
            CADPRECIOTOTAL = "";
            for (int i = 0; i < Lv2.Items.Count; i++)
            {

                CADUNIDAD = CADUNIDAD + Lv2.Items[i].Text;
                CADUNIDAD = CADUNIDAD + "\n";

                CADCANTIDAD = CADCANTIDAD + Lv2.Items[i].SubItems[1].Text;
                CADCANTIDAD =CADCANTIDAD + "\n";
                
                CADCLAVE = CADCLAVE+ Lv2.Items[i].SubItems[2].Text;
                CADCLAVE = CADCLAVE + "\n";

                CADNOMBREPRO =CADNOMBREPRO + Lv2.Items[i].SubItems[3].Text;
                CADNOMBREPRO = CADNOMBREPRO  +"\n";

                CADPRECIOUNITARIO = CADPRECIOUNITARIO+ "$ " +Lv2.Items[i].SubItems[4].Text;
                CADPRECIOUNITARIO = CADPRECIOUNITARIO + "\n";

                CADPRECIOTOTAL = CADPRECIOTOTAL + "$ " + Lv2.Items[i].SubItems[5].Text;
                CADPRECIOTOTAL = CADPRECIOTOTAL + "\n";
      
            }

            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            string Query = "";
            Query = "insert into Pedidos(";
            Query=Query + "numpedido";
            Query = Query + ",cvcliente";
            Query = Query + ",nombre";
            Query = Query + ",fecha";
           Query=Query + ",fechacod";
           Query=Query + ",total";
           Query=Query + ",iva";
           Query=Query + ",totalgeneral";
           Query=Query + ",status";
           Query=Query + ",emitio";
           Query=Query + ",facturada";
           Query=Query + ",mes";
           Query=Query + ",ayo";
           Query=Query + ",totalletra";
           Query=Query + ",tdistribuidor";
           Query=Query + ",tganancia";
           Query=Query + ",compro";
           Query=Query + ",cantidades";
           Query=Query + ",precunitarios";
           Query=Query + ",pretotales";
           Query=Query + ",unidades";
           Query = Query + ",tipopago";
           Query = Query + ",banco";
           Query = Query + ",numcuenta";
           Query = Query + ",notas";
           Query = Query + ",condicionPago";

           Query = Query + ",numremision";
           Query = Query + ",numcotizacion";
           Query = Query + ",fcodcotiza";
           Query = Query + ",fechacotiza";
           Query = Query + ",fcodremision";
           Query = Query + ",fecharemision";
           Query = Query + ",estatuspedido";

           Query = Query + ",estatuspago";
           Query = Query + ",vendedor";

            Query = Query + ",claves)";

            Query = Query + " values(";

            Query = Query + "'" + NUMPEDIDO + "'" ;
            Query = Query + ",'" + CVCLIENTE+ "'";
            Query = Query + ",'" + textBox2.Text.ToUpper() +"'";
            Query = Query + ",'" + FECHA + "'";
            Query = Query + ",'" + FECHACOD + "'";
            Query = Query + ",'" + SUBTOTAL + "'";
            Query = Query + ",'" + IVA + "'";
            Query = Query + ",'" + TOTAL + "'";
            Query = Query + ",'" + ESTATUS + "'";
            Query = Query + ",'" + EMITIO + "'";
            Query = Query + ",'" + FACTURADA + "'";
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

            Query = Query + ",'" + metodoPago + "'";
            Query = Query + ",'" + BANCO+ "'";
            Query = Query + ",'" + numCtaPago + "'";
            Query = Query + ",'" + Notas + "'";
            Query = Query + ",'" + CondicionesPago + "'";

            Query = Query + ",'" + numremision + "'";
            Query = Query + ",'" + numcotizacion + "'";
            Query = Query + ",'" + fcodcotiza + "'";
            Query = Query + ",'" + fechacotiza+ "'";
            Query = Query + ",'" + fcodremision+ "'";
            Query = Query + ",'" + fecharemision+ "'";
            Query = Query + ",'" + estatuspedido + "'";

            Query = Query + ",'POR PAGAR'";
            Query = Query + ",'" + VENDEDORGEN+ "'";

            Query = Query + ",'" + CADCLAVE + "')";
            conecta.Excute(Query);

            for (int i = 0; i < Lv2.Items.Count; i++)
            {

                string UNI= Lv2.Items[i].Text;
                CANTIDAD = Lv2.Items[i].SubItems[1].Text;
                CLAVEPRODUCTO = Lv2.Items[i].SubItems[2].Text;
                NOMBREPRO= Lv2.Items[i].SubItems[3].Text;
                PRECIOUNITARIO= Lv2.Items[i].SubItems[4].Text;
                PRECIOTOTAL= Lv2.Items[i].SubItems[5].Text;
                string DISTRIBUIDOR= Lv2.Items[i].SubItems[7].Text;
                string  GANANCIA= Lv2.Items[i].SubItems[8].Text;
                string Nota1 = Lv2.Items[i].SubItems[9].Text;
                string causaIva= Lv2.Items[i].SubItems[10].Text;

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
                if (cantfinal <= 0) cantfinal = 1;

                consulta = "update productos set cantidad='" + cantfinal.ToString() + "' where cvproducto='" + CLAVEPRODUCTO + "'";
                conecta2.Excute(consulta);
                conecta2.CierraConexion();

                Query = "Insert into DetallesPedido(";
                Query=Query + "numpedido";
                Query=Query + ",cvcliente";
                Query=Query + ",cvproducto";
                Query=Query + ",descripcion";
                Query=Query + ",cantidad";
                Query=Query + ",preunitario";
                Query=Query + ",precio";
                Query=Query + ",fecha";
                Query=Query + ",fechacod";
                Query=Query + ",mes";
                Query=Query + ",ayo";
                Query=Query + ",emitio";
                Query=Query + ",tdistribuidor";
                Query = Query + ",tganancia";
                Query = Query + ",nota1";
                Query = Query + ",causaiva";
                Query = Query + ",unidad)";

                Query = Query + " values(";
                Query = Query + "'" + NUMPEDIDO + "'";
                Query = Query + ",'" + CVCLIENTE+ "'";
                Query = Query + ",'" + CLAVEPRODUCTO+ "'";
                Query = Query + ",'" + NOMBREPRO + "'";
                Query = Query + ",'" + CANTIDAD+ "'";
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
                Query = Query + ",'" + causaIva + "'";
                Query = Query + ",'" + UNI + "')";

                conecta.Excute(Query);
            }
        }

        public bool ExisteInfo()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from pedidos where numpedido='" + NUMPEDIDO+ "'";
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

            //if (Lv.SelectedItems.Count > 0)
            //{
            //    ListView.SelectedIndexCollection seleccion = Lv.SelectedIndices;
            //    foreach (int item in seleccion)
            //    {
            //        DetallesModifica(item);
            //    }
            //}
        }

        public void ModificaciondePedido(string numpedido)
        {
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            conectorSql conecta3 = new conectorSql();
            label67.Visible = false;
            SqlDataReader leer3 = null;
            label67.Visible = false;
            string Query = "Select * from pedidos where numpedido='" + numpedido + "'";
            label17.Text = numpedido;
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string claveusuario = leer["cvcliente"].ToString();
                label67.Visible = true;
                Query = "Select * from clientes where cvcliente='" + claveusuario + "'";
                SqlDataReader leer2 = conecta2.RecordInfo(Query);
                while (leer2.Read())
                {
                    textBox1.Text = claveusuario;
                    textBox2.Text = leer2["nombre"].ToString();
                    textBox10.Text = leer2["empresa"].ToString();
                    textBox5.Text = leer2["direccion"].ToString();
                    textBox19.Text = leer2["RFC"].ToString();
                    label37.Text = leer2["telefono"].ToString();
                    textBox16.Text = leer2["observafact"].ToString();

                    label52.Text = leer2["metodopago"].ToString();
                    label48.Text = leer2["numcuenta"].ToString();
                    label55.Text = leer2["cvbanco"].ToString();
                    label66.Text = leer2["formapago"].ToString();

                    textBox20.Text = leer2["MunicipioE"].ToString();
                    textBox23.Text = leer2["calleF"].ToString();
                    textBox22.Text = leer2["CodF"].ToString();
                    textBox21.Text = leer2["ColoniaF"].ToString();
                    textBox5.Text = leer2["EstadoF"].ToString();
                    ReLocalidad = "";
                    textBox20.Text = leer2["MunicipioF"].ToString();
                    textBox24.Text = leer2["numF"].ToString();
                    ReNoInterior = "0";
                    RePais = leer2["PaisF"].ToString();
                    ReReferencia = "";
                    textBox25.Text = leer2["email"].ToString();

                    string valor = leer2["tipopago"].ToString();
                    if (valor == "Contado") radioButton1.Checked = true;
                    else radioButton2.Checked = true;

                    textBox17.Text = leer2["diascredito"].ToString();

                    if (label48.Text.Length < 4) label48.Text = "0000";
                    comboBox8.Text = leer2["vendedor"].ToString();
                }
                conecta2.CierraConexion();


                ColumnasProducto();
                Query = "Select * from detallespedido where numpedido='" + numpedido + "'";
                leer2 = conecta2.RecordInfo(Query);
                while (leer2.Read())
                {
                    string cvproducto = leer2["cvproducto"].ToString();

                    string Nombre = leer2["descripcion"].ToString();
                    string unidad = leer2["unidad"].ToString();
                    decimal cantidad = decimal.Parse( leer2["cantidad"].ToString());
                    decimal precio = decimal.Parse( leer2["preunitario"].ToString());
                    decimal total = precio * cantidad;
                    string NumPrecio = "0";


                    decimal TotalDistribuidor = decimal.Parse(leer2["tdistribuidor"].ToString());
                    decimal Ganancia = decimal.Parse(leer2["tganancia"].ToString());

                    string CausaIVA = "NO";
                    CausaIVA = leer2["causaiva"].ToString();

                    //string consulta = "Select causaiva from productos where cvproducto='" + cvproducto + "'";
                    //leer3 = conecta3.RecordInfo(consulta);
                    //while (leer3.Read())
                    //{
                    //    CausaIVA = leer3["causaiva"].ToString();
                    //}
                    //conecta3.CierraConexion();


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
            string numpedido = Lv.Items[index].Text;
            string ayo = Lv.Items[index].SubItems[8].Text;

            string estatuspedido = label53.Text;
            if (estatuspedido == "CANCELADO")
            {
                MessageBox.Show("El pedido se encuentra cancelado no es posible realizar esta operación", "Modificar Pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (estatuspedido == "FACTURADO")
            {
                MessageBox.Show("El pedido ya se encuentra facturado", "Modificar Pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
         
            DialogResult reply = MessageBox.Show("¿Desea modificar el pedido seleccionado?", "Modificar pedido", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;
            panel3.Visible = false;
            panel1.Visible = true;
            Lv2.Items.Clear();
            Lv2.Columns.Clear();
            ModificaciondePedido(numpedido);
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
                string clave=Lv.Items[i].Text;
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

            ReportesNKB.RBusquedaRemisiones(dateTimePicker1.Value.ToString("yyyyMMdd"), dateTimePicker2.Value.ToString("yyyyMMdd"), textBox11.Text, comboBox5.Text, checkBox1.Checked);
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
                textBox3.Focus();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ConsecutivoRemision();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
              if (textBox3.Text!="")  BuscarProducto(textBox3.Text);
              else textBox9.Focus();

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AgregarProducto();
            LimpiarProducto();
        }

        public void AgregarProductoBase()
        {
            conectorSql conecta = new conectorSql();

            string NumeroProducto = "1";
            string Query2 = "Select numproducto  from consecutivos where numproducto<>''";
            SqlDataReader leer2 = conecta.RecordInfo(Query2);
            while (leer2.Read())
            {
                NumeroProducto = leer2["numproducto"].ToString();
            }
            conecta.CierraConexion();

            int NumProductoSig = int.Parse(NumeroProducto) + 1;
            string cvproducto="";
            string nombre = textBox8.Text.Trim() ;
            string descripcion = textBox8.Text.Trim();
            string categoria = "GENERAL";
            string unidad=comboBox4.Text;
            string cantidad = textBox4.Text;
            string minimo="100";
            string maximo="1000";
            string causaiva="NO";
            string marca="GENERAL";
            string codbarras="";
            string ubicacion="";
            string fechaModifica=DateTime.Now.ToString("dd/MM/yyyy");
            string fcodmodifica = DateTime.Now.ToString("yyyyMMdd");
            string emitio=valoresg.USUARIOSIS;
            string causaAdicional = "NO";

            
            string distribuidor="1";
            string publico1=comboBox3.Text.Trim();
            string porciento1="0";
            string ganancia1 = publico1;
            string publico2="0";
            string porciento2="0";
            string ganancia2="0";
            string publico3="0";
            string porciento3="0";
            string ganancia3 = "0";


            string Query = "Select * from productos where nombre='" + textBox8.Text.Trim() + "'";
            bool existe = conecta.ExisteRegistro(Query);
            if (existe == false)
            { 
               Query="Insert into productos (cvproducto";
               Query=Query + ", nombre";
               Query=Query + ",descripcion";
               Query=Query + ",categoria";
               Query=Query + ",unidad";
               Query=Query + ",cantidad";
               Query=Query + ",minimo";
               Query=Query + ",maximo";
               Query=Query + ",causaiva";
               Query=Query + ",marca";
               Query=Query + ",codbarras";
               Query=Query + ",ubicacion";
               Query=Query + ",fechaModifica";
               Query=Query + ",fcodmodifica";
               Query=Query + ",emitio";
               Query = Query + ",causaAdicional)";
               Query = Query + " values(";
               Query = Query + "'" + cvproducto+ "'";
               Query = Query + ",'" + nombre+ "'";
               Query = Query + ",'" + descripcion + "'";
               Query = Query + ",'" + categoria+ "'";
               Query = Query + ",'" + unidad+ "'";
               Query = Query + ",'" + cantidad+ "'";
               Query = Query + ",'" + minimo+ "'";
               Query = Query + ",'" + maximo+ "'";
               Query = Query + ",'" + causaiva+ "'";
               Query = Query + ",'" + marca+ "'";
               Query = Query + ",'" + codbarras+ "'";
               Query = Query + ",'" + ubicacion + "'";
               Query = Query + ",'" + fechaModifica+ "'";
               Query = Query + ",'" + fcodmodifica + "'";
               Query = Query + ",'" + emitio+ "'";
               Query = Query + ",'" + causaAdicional+ "')";
               conecta.Excute(Query);

               Query = "Insert into  ListaPrecios(";
               Query=Query + " cvproducto";
               Query=Query + ",distribuidor";
               Query=Query + ",publico1";
               Query=Query + ",porciento1";
               Query=Query + ",ganancia1";
               Query=Query + ",publico2";
               Query=Query + ",porciento2";
               Query=Query + ",ganancia2";
               Query=Query + ",publico3";
               Query=Query + ",porciento3";
               Query = Query + ",ganancia3)"; ;
               Query = Query + " values(";
               Query = Query + "'" + cvproducto + "'";
               Query = Query + ",'" + distribuidor + "'";
               Query = Query + ",'" + publico1 + "'";
               Query = Query + ",'" + porciento1+ "'";
               Query = Query + ",'" + ganancia1+ "'";
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


        public void ColumnasProducto()
        {
            if (Lv2.Items.Count == 0)
            {
                Lv2.Items.Clear();
                Lv2.Columns.Clear();
                Lv2.Columns.Add("Unidad", 80);
                Lv2.Columns.Add("Cantidad", 80);
                Lv2.Columns.Add("Clave", 60);
                Lv2.Columns.Add("Nombre", 550);
                Lv2.Columns.Add("Unitario", 100);
                Lv2.Columns.Add("Total", 100);
                Lv2.Columns.Add("nprecio", 0);
                Lv2.Columns.Add("distribuidor", 0);
                Lv2.Columns.Add("ganancia", 0);
                Lv2.Columns.Add("Detalle", 0);
                Lv2.Columns.Add("ConIVA", 0);
                Lv2.Columns.Add("Tiva", 0);
            }
        }
        public void AgregarProducto()
        {
            ColumnasProducto();

            decimal Numero = 0;
            bool esDecimal = decimal.TryParse(comboBox3.Text, out Numero);
            if (esDecimal == false)
            {
                MessageBox.Show("El precio debe ser numerico, verifique", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string Nombre = textBox8.Text ;

            if (textBox26.Text.Trim() != "") Nombre = Nombre + " - No. ECO " + textBox26.Text.ToUpper();
            if (textBox27.Text.Trim() != "") Nombre = Nombre + " - MCA " + textBox27.Text.ToUpper();
            if (textBox28.Text.Trim() != "") Nombre = Nombre + " - N/S " + textBox28.Text.ToUpper();

            string unidad = comboBox4.Text;
            decimal cantidad = decimal.Parse(textBox4.Text);
            
            decimal PrecioConIVA=decimal.Parse(comboBox3.Text);
            if (radioButton3.Checked == true) PrecioConIVA=PrecioConIVA / (1 + IVAParametro);

            decimal precio = PrecioConIVA;
            decimal total = precio * cantidad;
            string NumPrecio = comboBox3.SelectedIndex.ToString();

            decimal PreDistribuidor = decimal.Parse(label20.Text);
            decimal TotalDistribuidor = cantidad * PreDistribuidor;
            decimal Ganancia = total - TotalDistribuidor;

            decimal TotalIva = total * IVAParametro;
            if (radioButton4.Checked==true) TotalIva = 0;

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
            lvi.SubItems.Add(label49.Text);
            lvi.SubItems.Add(TotalIva.ToString("##.00", CultureInfo.InvariantCulture));
            Lv2.Items.Add(lvi);
            SumaTotales();
            label44.Text = Lv2.Items.Count.ToString();

            AgregarProductoBase();

            LimpiarProductoNuevo();
            textBox3.Focus();

        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) comboBox3.Focus();
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                float Numero=0;
                bool esEntero = float.TryParse(textBox4.Text, out Numero);
                if (esEntero == false)
                {
                    MessageBox.Show("El tipo de dato debe ser numero , verifique", "Aviso", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    textBox4.Focus();
                    return;
                }
             textBox3.Text = "";
             AgregarProducto();
             LimpiarProducto();
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

        public bool APLICARCOMOPAGADO= false;
        public bool VENTANACOBRO = false;
        public bool HABILITAPRECIO = false;
        public bool MOSTRARCONVENIO = false;
        public void BuscarParametros()
        {

            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            conectorSql conecta = new conectorSql();
            string Query = "Select * from parametros where iva<>''";
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

                
            }
            conecta.CierraConexion();

            if (HABILITAPRECIO == true) comboBox3.DropDownStyle = ComboBoxStyle.DropDown;
         
           
        }
        public void SumaTotales()
        {
            decimal acumulado = 0;
            decimal acumulado2 = 0;
            decimal acumulado3 = 0;
            decimal AcumulaIva = 0;
            for (int i = 0; i < Lv2.Items.Count; i++)
            {
                decimal total = decimal.Parse(Lv2.Items[i].SubItems[5].Text);
                acumulado = acumulado + total;
                
                decimal total2 = decimal.Parse(Lv2.Items[i].SubItems[8].Text);
                acumulado2 = acumulado2 + total2;
                decimal total3 = decimal.Parse(Lv2.Items[i].SubItems[7].Text);
                acumulado3 = acumulado3 + total3;

                decimal CantIva = decimal.Parse(Lv2.Items[i].SubItems[11].Text);
                AcumulaIva = AcumulaIva + CantIva;
            }


            decimal subtotal = acumulado;
           // decimal resiva = subtotal * IVAParametro;
            decimal netopago = subtotal + AcumulaIva;

            label9.Text = subtotal.ToString("##.00", CultureInfo.InvariantCulture); //subtotal
            label12.Text = AcumulaIva.ToString("##.00", CultureInfo.InvariantCulture); //total del iva
            textBox9.Text = netopago.ToString("##.00", CultureInfo.InvariantCulture);
            label31.Text = acumulado2.ToString("##.00", CultureInfo.InvariantCulture); //ganancia
            label32.Text = acumulado3.ToString("##.00", CultureInfo.InvariantCulture); // inversion

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

        public void GuardarInfo()
        {
            Recolectar();

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
       


            if (Validacion() == true)
            {
                if (ExisteInfo() == false)
                {

                    valoresg.AYOPEDIDO = AYOPEDIDO;
                    valoresg.NUMPEDIDO = NUMPEDIDO;
                    valoresg.NUMPEDIDOCARGAR = NUMPEDIDO;

                    Guardar();
                    GuardarFactura();
                    ActualizaRemision();
                    GuardaPAGADO();

                    //if (radioButton2.Checked == true && textBox13.Text.Trim()=="")
                    //{
                    //    GuardaCreditoSincalculo();
                    //}

                    //AplicarNumremision(valoresg.NUMPEDIDO, valoresg.AYOPEDIDO);
                    //AplicarNumCotizacion(NUMPEDIDO, AYOPEDIDO);
                   
                    //if (VENTANACOBRO == true) VentanaCobroAbrir();

                    MessageBox.Show("Se guardo correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    LimpiarCliente();

                    if (ReRFC.Length >= 0 && ReRFC.Length < 10)
                    {
                        MessageBox.Show("No tiene RFC o esta incompleto\nVerifique la información de su cliente si desea que facture posteriormente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //mandar nota de remision
                        //Reportespdf reporte = new Reportespdf();
                        //string cadena = reporte.ReportePedido(valoresg.NUMPEDIDO, valoresg.AYOPEDIDO);
                        //try
                        //{
                        //    System.Diagnostics.Process.Start(cadena);
                        //    this.Dispose();
                        //}
                        //catch (Exception er)
                        //{
                        //    MessageBox.Show("" + er, "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    throw;
                        //}
                    }
                    else
                    {
                        DialogResult reply = MessageBox.Show("¿Desea facturar ?", "Facturación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (reply == DialogResult.Yes)
                        {
                            FacturarPedido Rfacturar = new FacturarPedido();
                            Rfacturar.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Se mantendra en espera para facturar posteriormente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ////mandar nota de remision
                            ////AplicarNumremision(valoresg.NUMPEDIDO, valoresg.AYOPEDIDO);
                            //Reportespdf reporte = new Reportespdf();
                            //string cadena = reporte.ReportePedido(valoresg.NUMPEDIDO, valoresg.AYOPEDIDO);

                            //try
                            //{
                            //    System.Diagnostics.Process.Start(cadena);
                            //    this.Dispose();
                            //}
                            //catch (Exception er)
                            //{
                            //    MessageBox.Show("" + er, "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    throw;
                            //}
                        }
                    }

                }
                else
                {

                    DialogResult reply = MessageBox.Show("¿Desea modificar la información a facturar?", "Facturación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (reply == DialogResult.No)
                    {
                        return;
                    }


                    conectorSql conecta = new conectorSql();
                    string Query = "Delete from pedidos where numpedido='" + label17.Text + "'";
                    conecta.Excute(Query);

                    Query = "Delete from DetallesPedido where numpedido='" + label17.Text + "'";
                    conecta.Excute(Query);
                    Query = "Delete from Facturas where numpedido='" + label17.Text + "'";
                    conecta.Excute(Query);
                    Query = "Delete from DetallesFacturas where numpedido='" + label17.Text + "'";
                    conecta.Excute(Query);
                    Query = "Delete from Pagos where numremision='" + label17.Text + "'";
                    conecta.Excute(Query);
                    

                    Guardar();
                    GuardarFactura();
                    GuardaPAGADO();


                    MessageBox.Show("Se modifico correctamente la factura", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    LimpiarCliente();

                    reply = MessageBox.Show("¿Desea facturar el pedido?", "Facturación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (reply == DialogResult.Yes)
                    {
                         FacturarPedido Rfacturar = new FacturarPedido();
                         Rfacturar.ShowDialog();
                    }
                    else
                    {
                         MessageBox.Show("Se mantendra en espera para facturar posteriormente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                
                }
            }

           

        }



        public void AplicarNumremision(string numpedido, string ayo)
        {
            int Numeroremision = 0;
            conectorSql conecta = new conectorSql();
            string Query = "Select numremision from Consecutivos where numremision<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                Numeroremision=int.Parse(leer["numremision"].ToString());
            }
            conecta.CierraConexion();

            Query = "Update pedidos set numremision='" + Numeroremision.ToString() + "'";
            Query = Query + " ,fcodremision='" + DateTime.Now.ToString("yyyyMMdd") + "'";
            Query = Query + " ,fecharemision='" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
            Query = Query + " where numpedido='" + numpedido + "' and ayo='" + ayo + "'";
            conecta.Excute(Query);

            Query = "Update pagos set numremision='" + Numeroremision.ToString() + "', remisionHist='" + Numeroremision.ToString() + "'";
            Query = Query + " where numpedido='" + numpedido + "' and ayo='" + ayo + "' and (cvconcepto='11' or cvconcepto='1')";
            conecta.Excute(Query);

            Numeroremision++;
            Query = "Update Consecutivos set numremision='" + Numeroremision.ToString() + "'";
            conecta.Excute(Query);
        }

        public void GuardaPAGADO()
        {
            string EstatusPag = "POR PAGAR";
            if (APLICARCOMOPAGADO ==true) EstatusPag = "PAGADO";
            conectorSql conecta = new conectorSql();
            string Query = "";
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
            Query = Query + ",numremision"; //RECIBO ASIGNADO
            Query = Query + ",ayo";
            Query = Query + ",mes";

            Query = Query + ",estatus)";
            Query = Query + " values(";
            Query = Query + "'" + CVCLIENTE + "'";
            Query = Query + ",'" + NUMPEDIDO + "'";
            Query = Query + ",'" + TOTAL + "'";
            Query = Query + ",'" + Fecha.ToString("dd/MM/yyyy") + "'";
            Query = Query + ",'" + Fecha.ToString("yyyyMMdd") + "'";
            Query = Query + ",'PAGO TOTAL POR PEDIDO - " + NUMPEDIDO + "'";
            Query = Query + ",'1'"; //pendiente por verificar el cobro ESTATUS 1
            Query = Query + ",'" + NUMPEDIDO+ "'";
            Query = Query + ",''";
            Query = Query + ",''";
            Query = Query + ",'.'";
            Query = Query + ",''";
            Query = Query + ",''";
            Query = Query + ",'0'";
            Query = Query + ",'" +  DateTime.Now.Year.ToString()+  "'";
            Query = Query + ",'" + DateTime.Now.Month.ToString() + "'";

            Query = Query + ",'" + EstatusPag + "')"; // MODULO DE PAGOS CAMBIAR A PAGADO 
            conecta.Excute(Query);        
        }

        public void GuardaCreditoSincalculo()
        {
            string EstatusPag = "POR PAGAR";
            //if (APLICARCOMOPAGADO == true) EstatusPag = "PAGADO";
            conectorSql conecta = new conectorSql();
            string Query = "";
            DateTime Fecha = DateTime.Parse(FECHA);
            Query = "Select * from pagos where cvconcepto='11' where numpedido='" + NUMPEDIDO + "' and ayo='" + DateTime.Now.Year.ToString()+ "'";
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

                Query = Query + ",estatus)";
                Query = Query + " values(";
                Query = Query + "'" + CVCLIENTE + "'";
                Query = Query + ",'" + NUMPEDIDO + "'";
                Query = Query + ",'" + TOTAL + "'";
                Query = Query + ",'" + Fecha.ToString("dd/MM/yyyy") + "'";
                Query = Query + ",'" + Fecha.ToString("yyyyMMdd") + "'";
                Query = Query + ",'CREDITO POR PEDIDO - " + NUMPEDIDO + "'";
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

                Query = Query + ",'" + EstatusPag + "')"; // MODULO DE PAGOS CAMBIAR A PAGADO 
                conecta.Excute(Query);
            }
        }

        //public void GuardaCredito()
        //{
        //    TIPOPAGOSCRED = comboBox2.Text;

        //    conectorSql conecta = new conectorSql();
        //    string Query = "insert into creditos(cvcliente";
        //    Query =Query + ",numpedido";
        //    Query =Query + ",total";
        //    Query =Query + ",primerpago";
        //    Query =Query + ",porpagar";
        //    Query =Query + ",tipopago";
        //    Query =Query + ",numpagos";
        //    Query =Query + ",estatus";
        //    Query =Query + ",fechacod";
        //    Query =Query + ",fecha";
        //    Query = Query + ",emite)";
        //    Query = Query + " values(";
        //    Query = Query + "'" + CVCLIENTE +"'"; 
        //    Query = Query + ",'" + NUMPEDIDO+ "'"; 
        //    Query = Query + ",'" + TOTAL + "'"; 
        //    Query = Query + ",'" + ABONO + "'"; 
        //    Query = Query + ",'" + PORPAGAR + "'"; 
        //    Query = Query + ",'" + TIPOPAGOSCRED + "'"; 
        //    Query = Query + ",'" + NUMPAGOS + "'"; 
        //    Query = Query + ",'" + ESTATUSCRED + "'"; 
        //    Query = Query + ",'" + FECHA + "'"; 
        //    Query = Query + ",'" + FECHACOD + "'";
        //    Query = Query + ",'" + EMITIO + "')";

        //    conecta.Excute(Query);
        //    DateTime Fecha = DateTime.Parse(FECHA);

        //    Query = "Insert into Pagos (cvcliente";
        //    Query = Query + ",numpedido";
        //    Query = Query + ",cantidad";
        //    Query = Query + ",fecha";
        //    Query = Query + ",fechacod";
        //    Query = Query + ",concepto";
        //    Query = Query + ",cvconcepto";
        //    Query = Query + ",remisionHist";
            
        //    Query = Query + ",fechapago";
        //    Query = Query + ",fcodpago";
        //    Query = Query + ",emitiopago";
        //    Query = Query + ",pagocon";
        //    Query = Query + ",observacion";
        //    Query = Query + ",numremision";
        //    Query = Query + ",ayo";
        //    Query = Query + ",mes";

        //    Query = Query + ",estatus)";
        //    Query = Query + " values(";
        //    Query = Query + "'" + CVCLIENTE + "'";
        //    Query = Query + ",'" + NUMPEDIDO + "'";
        //    Query = Query + ",'" + ABONO + "'";
        //    Query = Query + ",'" + Fecha.ToString("dd/MM/yyyy") + "'";
        //    Query = Query + ",'" + Fecha.ToString("yyyyMMdd") + "'";
        //    Query = Query + ",'PRIMER PAGO POR EL PEDIDO " + NUMPEDIDO + "'";
        //    Query = Query + ",'11'";
        //    Query = Query + ",'" + NUMPEDIDO + "'";
            
        //    Query = Query + ",''";
        //    Query = Query + ",''";
        //    Query = Query + ",'.'";
        //    Query = Query + ",''";
        //    Query = Query + ",''";
        //    Query = Query + ",'0'";
        //    Query = Query + ",'" + DateTime.Now.Year.ToString() + "'";
        //    Query = Query + ",'" + DateTime.Now.Month.ToString() + "'";

        //    Query = Query + ",'POR PAGAR')";
        //    conecta.Excute(Query);

        //    int contador = 1;
        //    int numpagos = int.Parse(NUMPAGOS);
        //    for (int i = 0; i < numpagos; i++)
        //    {

        //        if (TIPOPAGOSCRED == "MENSUAL")Fecha = Fecha.AddMonths(1);
        //        if (TIPOPAGOSCRED == "QUINCENAL") Fecha = Fecha.AddDays(15);
        //        if (TIPOPAGOSCRED == "SEMANAL") Fecha = Fecha.AddDays(8);


        //        Query="Insert into Pagos (cvcliente";
        //        Query = Query + ",numpedido";
        //        Query = Query + ",cantidad";
        //        Query = Query + ",fecha";
        //        Query = Query + ",fechacod";
        //        Query = Query + ",concepto";
        //        Query = Query + ",cvconcepto";
        //        Query = Query + ",remisionHist";
        //        Query = Query + ",fechapago";
        //        Query = Query + ",fcodpago";
        //        Query = Query + ",emitiopago";
        //        Query = Query + ",pagocon";
        //        Query = Query + ",observacion";
        //        Query = Query + ",numremision";
        //        Query = Query + ",ayo";
        //        Query = Query + ",mes";

        //        Query = Query + ",estatus)";
        //        Query = Query + " values(";
        //        Query = Query + "'" + CVCLIENTE + "'";
        //        Query = Query + ",'" +  NUMPEDIDO + "'";
        //        Query = Query + ",'" + PAGOSDE + "'";
        //        Query = Query + ",'" + Fecha.ToString("dd/MM/yyyy") + "'";
        //        Query = Query + ",'" + Fecha.ToString("yyyyMMdd") + "'";
        //        Query = Query + ",'"  + contador.ToString() + "-PAGO DE CREDITO POR EL PEDIDO " + NUMPEDIDO + "'";
        //        Query = Query + ",'4'";
        //        Query = Query + ",'" + NUMPEDIDO + "'";
        //        Query = Query + ",''";
        //        Query = Query + ",''";
        //        Query = Query + ",'.'";
        //        Query = Query + ",''";
        //        Query = Query + ",''";
        //        Query = Query + ",'0'"; //NUMERO DE REMISION RECIBO INDIVIDUAL

        //        Query = Query + ",'" + DateTime.Now.Year.ToString() + "'";
        //        Query = Query + ",'" + DateTime.Now.Month.ToString() + "'";
        //        Query = Query + ",'POR PAGAR')";

        //        conecta.Excute(Query);
        //        contador++;

        //    }
        //}



       

        private void button1_Click(object sender, EventArgs e)
        {
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


            if (valoresg.VIENEBUSQUEDAPEDIDO == "SI")
            {
                textBox18.Text = Modremision.CVCLIENTE;
                valoresg.VIENEBUSQUEDAPEDIDO = "";
            }
            if (Modremision.CVCLIENTE != "")
            {
                textBox1.Text = Modremision.CVCLIENTE;
                BuscarInformacion(textBox1.Text);
                Modremision.CVCLIENTE = "";
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
                comboBox5.Text = "";
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
            TImpuestoTrasladado= decimal.Parse(label12.Text);
            TImporte = decimal.Parse(label12.Text);

            conectorSql conecta = new conectorSql();
            string Query = "Insert into Facturas(";
            Query=Query + " numfactura";
            Query=Query + ",estatus";
            Query=Query + ",idsistemapadre";
            Query=Query + ",edocomprobante";
            Query=Query + ",tipo";
            Query=Query + ",RFCEmitio";
            Query=Query + ",CondicionesPago";
            Query=Query + ",FormaPago";
            Query=Query + ",Descuento";
            Query=Query + ",motivoDescuento";
            Query=Query + ",metodoPago";
            Query=Query + ",subtotal";
            Query=Query + ",total";
            Query=Query + ",REClave";
            Query=Query + ",ReNombre";
            Query=Query + ",ReRFC";
            Query=Query + ",ReCalle";
            Query=Query + ",ReCodpostal";
            Query=Query + ",ReColonia";
            Query=Query + ",ReEstado";
            Query=Query + ",ReLocalidad";
            Query=Query + ",ReMunicipio";
            Query=Query + ",ReNoExterior";
            Query=Query + ",ReNoInterior";
            Query=Query + ",ReTel";
            Query=Query + ",RePais";
            Query=Query + ",ReReferencia";
            Query=Query + ",Recorreo";
            Query=Query + ",TImpuestosRetenido";
            Query=Query + ",TImpuestoTrasladado";
            Query=Query + ",RImpuesto";
            Query=Query + ",RImporte";
            Query=Query + ",TImpuesto";
            Query=Query + ",TImporte";
            Query=Query + ",TTasa";
            Query=Query + ",Notas";
            Query=Query + ",moneda";
            Query=Query + ",TipoCambio";
            Query=Query + ",Vendedor";
            Query=Query + ",OrdCompra";
            Query=Query + ",Otros";
            Query=Query + ",numCtaPago";
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


            Query = Query + ",cvcliente";
            Query = Query + ",direccion";
            Query = Query + ",observaciones";
            Query = Query + ",cantletra";

            Query=Query + ",ayo";
            Query = Query + ",mes)";

            Query = Query + " values(";
            Query = Query + "'" + NUMPEDIDO + "'" ;
            Query = Query + ",'" + estatus + "'";
            Query = Query + ",'" + idsistemapadre+ "'";
            Query = Query + ",'" + edocomprobante+ "'";
            Query = Query + ",'" + tipo+ "'";
            Query = Query + ",'" + RFCEmitio+ "'";
            Query = Query + ",'" + CondicionesPago+ "'";
            Query = Query + ",'" + FormaPago+ "'";
            Query = Query + ",'" + Descuento+ "'";
            Query = Query + ",'" + motivoDescuento+ "'";
            Query = Query + ",'" + metodoPago+ "'";
            Query = Query + ",'" + subtotal+ "'";
            Query = Query + ",'" + total+ "'";
            Query = Query + ",'" + REClave+ "'";
            Query = Query + ",'" + ReNombre+ "'";
            Query = Query + ",'" + ReRFC+ "'";
            Query = Query + ",'" + ReCalle+ "'";
            Query = Query + ",'" + ReCodpostal+ "'";
            Query = Query + ",'" + ReColonia+ "'";
            Query = Query + ",'" + ReEstado+ "'";
            Query = Query + ",'" + ReLocalidad+ "'";
            Query = Query + ",'" + ReMunicipio+ "'";
            Query = Query + ",'" + ReNoExterior+ "'";
            Query = Query + ",'" + ReNoInterior+ "'";
            Query = Query + ",'" + ReTel+ "'";
            Query = Query + ",'" + RePais+ "'";
            Query = Query + ",'" + ReReferencia+ "'";
            Query = Query + ",'" + Recorreo+ "'";
            Query = Query + ",'" + TImpuestosRetenido+ "'";
            Query = Query + ",'" + TImpuestoTrasladado+ "'";
            Query = Query + ",'" + RImpuesto+ "'";
            Query = Query + ",'" + RImporte+ "'";
            Query = Query + ",'" + TImpuesto+ "'";
            Query = Query + ",'" + TImporte+ "'";
            Query = Query + ",'" + TTasa+ "'";
            Query = Query + ",'" + Notas+ "'";
            Query = Query + ",'" + moneda+ "'";
            Query = Query + ",'" + TipoCambio+ "'";
            Query = Query + ",'" + Vendedor+ "'";
            Query = Query + ",'" + OrdCompra+ "'";
            Query = Query + ",'" + Otros+ "'";
            Query = Query + ",'" + numCtaPago+ "'";
            Query = Query + ",'" + NUMPEDIDO+ "'";

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
            Query = Query + ",'" + EMITIO+  "'";


            Query = Query + ",'" + CVCLIENTE + "'";
            Query = Query + ",'" + ReCalle + " Num." + ReNoExterior + "," + ReColonia + "," + ReMunicipio+ "," + ReEstado  + " C.P " + ReCodpostal + "'";
            Query = Query + ",'" +  Notas+ "'";
            Query = Query + ",'" +  CANTLETRA+ "'";


            Query = Query + ",'" + AYOPEDIDO+ "'";
            Query = Query + ",'" + MESPEDIDO + "')";
            conecta.Excute(Query);



            string numfacturaD="0";
            int numpartida=0;
            float cantidad=0;
            string descripcion="";
            float importe=0;
            string cvproducto="";
            string unidad="";
            float valorUnitario=0;
            float valorIva = 0;
            string pedimentonum = "";
            string pedimentonombre="";
            string pedimentofecha="";
            float iva=16;
            string notas1 = "";
            string notas2="";

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
                valorIva= float.Parse(Lv2.Items[i].SubItems[11].Text);
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

         
                Query = Query + ",fecha";
                Query = Query + ",fechacod"; 
                Query = Query + ",mes";
                Query = Query + ",ayo";
                Query = Query + ",cvcliente";
                Query = Query + ",Valoriva";
                Query = Query + ",Adicional";
                Query = Query + ",cvunica";

                Query = Query + ",numpedido)";

                Query = Query + " values(";
                Query = Query + "'" + NUMPEDIDO + "'";
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

                Query = Query + ",'" + DateTime.Now.ToString("dd/MM/yyyy")+ "'";
                Query = Query + ",'" + DateTime.Now.ToString("yyyyMMdd") + "'";
                Query = Query + ",'" + DateTime.Now.Month.ToString() + "'";
                Query = Query + ",'" + DateTime.Now.Year.ToString() +"'";
                Query = Query + ",'" + CVCLIENTE + "'";
                Query = Query + ",'" + valorIva + "'";
                Query = Query + ",'0'";
                Query = Query + ",'" + cvproducto+ "'";

                Query = Query + ",'" + NUMPEDIDO + "')";
                conecta.Excute(Query);
            }

        }

  

      

        //public void CalcularCredito()
        //{
           
        //    decimal pagosde = 0;

        //    decimal total = decimal.Parse(textBox9.Text);
        //    decimal primerpago = decimal.Parse(textBox12.Text);
        //    decimal numpagos = decimal.Parse(textBox13.Text);

        //    decimal porpagar= total - primerpago;
        //    pagosde = porpagar / numpagos;
        //    textBox14.Text = pagosde.ToString("##.00", CultureInfo.InvariantCulture);

        //    label5.Text = porpagar.ToString("##.00", CultureInfo.InvariantCulture);

        //    PAGOSDE = textBox14.Text;
        //    NUMPAGOS = numpagos.ToString();
        //    TIPOPAGOSCRED = comboBox2.ToString();
        //    ABONO = primerpago.ToString("##.00", CultureInfo.InvariantCulture);
        //    PORPAGAR = porpagar.ToString("##.00", CultureInfo.InvariantCulture);
        //    ESTATUSCRED = "POR PAGAR";
        //}

       

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

        private void button20_Click(object sender, EventArgs e)
        {
           
            GuardarInfo();
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                panel13.Visible = false;
                textBox17.Text = "0";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                panel13.Visible = true;
                textBox17.Focus();
            }
        }


        private void button15_Click(object sender, EventArgs e)
        {
            CancelaRemision cancela = new CancelaRemision();
            cancela.Show();
        }

      

      
     

        private void button14_Click(object sender, EventArgs e)
        {
           

        }

       
     

        public void BuscarConceptoPago(string clave)
        {
            for (int i = 0; i < Lv.Items.Count; i++)
            {
                if (Lv.Items[i].Text=="NO APLICA" && Lv.Items[i].SubItems[2].Text == clave)
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
            string ayo = Lv.Items[index].SubItems[8].Text;
            string estatus = Lv.Items[index].SubItems[9].Text;
            label50.Text = numpedido;
            label51.Text = ayo;
            label53.Text = estatus;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            valoresg.NUMPEDIDO = label50.Text;
            valoresg.AYOPEDIDO = label51.Text;
            string estatuspedido = label53.Text;
            if (estatuspedido == "FACTURADO")
            {
                MessageBox.Show("El pedido ya se encuentra facturado, verifique", "Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (estatuspedido == "CANCELADO")
            {
                MessageBox.Show("El pedido ya se encuentra cancelado, realize otro pedido", "Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // verificar primero si el cliente tiene RFC en  pedido
            conectorSql conecta = new conectorSql();
            string Query = "Select * from facturas where numpedido='" + label50.Text + "'  and ReRFC<>'' and ReCalle<>'' and ReCodpostal<>'0'";
            bool existe = conecta.ExisteRegistro(Query);
            if (existe == false)
            {
                MessageBox.Show("Verifique la información del cliente, para facturar el pedido por favor\nFalta información para facturar", "Error en RFC del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            DialogResult reply = MessageBox.Show("¿Desea facturar el num pedido " + label50.Text + " ?", "Facturación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.Yes)
            {
                FacturarPedido Rfacturar = new FacturarPedido();
                Rfacturar.ShowDialog();
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            string numpedido = label50.Text;
            string ayo = label51.Text;
            string estatuspedido = label53.Text;
            if (estatuspedido == "CANCELADO")
            {
                MessageBox.Show("El pedido ya se encuentra cancelado, realize otro pedido", "Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Reportespdf reporte = new Reportespdf();
            string cadena = reporte.ReporteNotaRemision(numpedido, ayo);
            try
            {
                System.Diagnostics.Process.Start(cadena);
            }
            catch (Exception er)
            {
                MessageBox.Show("" + er, "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
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

            Query = "Update pedidos set numcotizacion='" + Numcotizacion.ToString() + "'";
            Query = Query + " ,fcodcotiza='" + DateTime.Now.ToString("yyyyMMdd") + "'";
            Query = Query + " ,fechacotiza='" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
            Query = Query + " where numpedido='" + numpedido + "' and ayo='" + ayo + "'";
            conecta.Excute(Query);

            Numcotizacion++;
            Query = "Update Consecutivos set numcotiza='" + Numcotizacion.ToString() + "'";
            conecta.Excute(Query);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string numpedido = label50.Text;
            string ayo = label51.Text;

           

            conectorSql conecta = new conectorSql();
            string numcotizacion = "";
            string Query = "Select  numcotizacion from pedidos where numpedido='" + numpedido + "' and ayo='" + ayo + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                numcotizacion = leer["numcotizacion"].ToString();
            }
            conecta.CierraConexion();

            string estatus = label53.Text;
            if (estatus == "CANCELADO")
            {
                MessageBox.Show("El pedido se encuentra cancelado, verifique", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (numcotizacion == "0" || numcotizacion == "")
            {
                

                if (estatus == "FACTURADO")
                {
                    MessageBox.Show("El pedido ya se encuentra facturado, verifique", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                AplicarNumCotizacion(numpedido, ayo);
            }


            Reportespdf reporte = new Reportespdf();
            string cadena = reporte.ReporteCotizacion(numpedido, ayo);
            try
            {
                System.Diagnostics.Process.Start(cadena);
            }
            catch (Exception er)
            {
                MessageBox.Show("" + er, "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            valoresg.NUMPEDIDO = label50.Text;
            valoresg.AYOPEDIDO = label51.Text;
            string estatuspedido = label53.Text;

            //if (estatuspedido == "CAPTURADO")
            //{
            //    MessageBox.Show("El pedido no esta facturado, verifique", "Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //if (estatuspedido == "CANCELADO")
            //{
            //    MessageBox.Show("El pedido ya se encuentra cancelado", "Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //if (estatuspedido == "FACTURADO" || estatuspedido == "CAPTURADO")
            //{

            DialogResult reply = MessageBox.Show("¿Desea cancelar la factura  del pedido " + label50.Text + " ?", "Facturación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.Yes)
            {
                CancelarFactura Rfacturar = new CancelarFactura();
                Rfacturar.ShowDialog();
            }
            //}
        }

        private void button24_Click(object sender, EventArgs e)
        {
            LimpiarProductoNuevo();
        }
        public void LimpiarProductoNuevo()
        {
            label30.Text = "";
            textBox28.Text = "";
            textBox27.Text = "";
            textBox26.Text = "";
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
            valoresg.VIENEBUSQUEDAPEDIDO = "NO";
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
            Clientes mostrar = new Clientes();
            mostrar.Show();
        }

        private void Lv2_DoubleClick(object sender, EventArgs e)
        {
            KeyEventArgs m =new KeyEventArgs(Keys.Enter);
            if (Lv2.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = Lv2.SelectedIndices;
                foreach (int item in seleccion)
                {
                    DetallesModificaVerProducto(item,sender, m);
                }
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            string numpedido = label50.Text;
            string ayo = label51.Text;


            Reportespdf reporte = new Reportespdf();
            string cadena = reporte.ReportePedido(numpedido, ayo);

            try
            {
                System.Diagnostics.Process.Start(cadena);
            }
            catch (Exception er)
            {
                MessageBox.Show("" + er, "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
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
                comboBox4.Text= Lv2.Items[index].Text;
                comboBox3.Text = Lv2.Items[index].SubItems[4].Text;
                textBox4.Focus();              
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void Pedidospro_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void textBox8_Click(object sender, EventArgs e)
        {
  
        }

        private void textBox8_MouseMove(object sender, MouseEventArgs e)
        {
            if (textBox8 == this.GetChildAtPoint(e.Location))
            {
                if (!isShown)
                {
                    toolTip.Show(textBox8.Text, this, e.Location);
                    isShown = true;
                }
            }
            else
            {
                toolTip.Hide(textBox8);
                isShown = false;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Se desplegara la ultima información guardada de la factura", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);
        }

        private void label66_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
          
        }

        private void label52_DoubleClick(object sender, EventArgs e)
        {
           
            combos.ComboFormadePago(comboBox1);
            comboBox1.Visible = true;
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
          
        }

        private void comboBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
         
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                label52.Text = comboBox1.Text;
                comboBox1.Visible = false;
            }
        }

     
    }
}

