using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace SHOPCONTROL
{
    public partial class Productos : Form
    {
        public Productos()
        {
            InitializeComponent();
        }

        
        public string CLAVE = "";
        public string NOMBRE = "";
        public string DESCRIBE= "";
        public string CATEGORIA= "";
        public string UNIDAD= "";
        public string CANTIDAD= "";
        public string MINIMO= "";
        public string MAXIMO= "";

        public decimal DISTRIBUIDOR = 0;

        public decimal PORCENTAJE1 = 0;
        public decimal PORCENTAJE2 = 0;
        public decimal PORCENTAJE3 = 0;

        public decimal PRECIO1= 0;
        public decimal PRECIO2= 0;
        public decimal PRECIO3= 0;

        public decimal GANANCIA1= 0;
        public decimal GANANCIA2= 0;
        public decimal GANANCIA3= 0;
        public string CAUSAIVA = "NO";
        public string CVPROVEEDOR = "";

        public string TIPOS = "";           //AGREGADO POR JOSE 04-12-2019
        public decimal PRODUCTOSERVICIO = 0;  //AGREGADO POR JOSE 02-12-2019

        public string CODIGODEBARRAS = "";
        public string UBICACION = "";
        public string MARCA = "";


        public string PORCDESCUENTO= "";
        public string PASILLO= "";
        public string ALTURA= "";
        public string SUCURSAL= "";

        public string PrecioAnt = "0";

        public string FECHAMODIFICA = "";
        public string FCODMODIFICA = "";
        public string EMITE = "";
        public void Recolectar()
        {

            if (textBox3.Text == "") textBox3.Text = "0";

            CLAVE = textBox1.Text;
            NOMBRE = textBox2.Text;
            DESCRIBE = textBox7.Text;
            CATEGORIA = comboBox1.SelectedValue.ToString();
            TIPOS = comboBox8.SelectedValue.ToString();
            UNIDAD = comboBox5.Text;
            MINIMO= textBox5.Text;
            MAXIMO= textBox6.Text;
            CANTIDAD = textBox4.Text;
            if (textBox4.Text == "") CANTIDAD = "0";
            if (textBox5.Text == "") MINIMO = "1";
            if (textBox6.Text == "") MAXIMO = "10";
            if (comboBox5.Text == "") UNIDAD = "NO APLICA";

            PORCDESCUENTO = textBox3.Text;
            if (textBox3.Text == "") PORCDESCUENTO = "0";

            SUCURSAL = textBox22.Text;
            PASILLO = textBox21.Text;
            ALTURA = comboBox6.Text;


            if (comboBox3.Text != "") MARCA = comboBox3.Text;
            else MARCA = "GENERAL";

            if (CATEGORIA == "") CATEGORIA = "GENERAL";

            if (textBox9.Text.Trim() == "") textBox9.Text="0";
            if (textBox12.Text.Trim() == "") textBox12.Text = "0";
            if (textBox13.Text.Trim() == "") textBox13.Text = "0";

            if (textBox16.Text.Trim() == "") textBox16.Text = "0";
            if (textBox15.Text.Trim() == "") textBox15.Text = "0";
            if (textBox14.Text.Trim() == "") textBox14.Text = "0";

            if (textBox19.Text.Trim() == "") textBox19.Text = "0";
            if (textBox18.Text.Trim() == "") textBox18.Text = "0";
            if (textBox17.Text.Trim() == "") textBox17.Text = "0";

            //textBox8.Text = "0.01";

            PORCENTAJE1 = decimal.Parse(textBox9.Text);
            PRECIO1 = decimal.Parse(textBox12.Text);
            GANANCIA1= decimal.Parse(textBox13.Text);

            PORCENTAJE2 = decimal.Parse(textBox16.Text);
            PRECIO2 = decimal.Parse(textBox15.Text);
            GANANCIA2 = decimal.Parse(textBox14.Text);


            PORCENTAJE3 = decimal.Parse(textBox19.Text);
            PRECIO3 = decimal.Parse(textBox18.Text);
            GANANCIA3 = decimal.Parse(textBox17.Text);

            DISTRIBUIDOR = 0.01M;
            if (radioButton1.Checked == true) CAUSAIVA = "SI";
            if (radioButton2.Checked == true) CAUSAIVA = "NO";

            if (radioButton3.Checked == true) PRODUCTOSERVICIO = 1;  //AGREGADO POR JOSE 02-12-2019
            if (radioButton4.Checked == true) PRODUCTOSERVICIO = 2;  //AGREGADO POR JOSE 02-12-2019



            CODIGODEBARRAS = UPC.Text;
            UBICACION = textBox20.Text;
            FECHAMODIFICA = DateTime.Now.ToString("dd/MM/yyyy");
            FCODMODIFICA = DateTime.Now.ToString("yyyyMMdd");
            EMITE = valoresg.USUARIOSIS;
            
        }

        public bool Validacion()
        {

            if (CLAVE== "")
            {
                MessageBox.Show("Ingrese la clave correspondiente al producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return false;
            }

            if (NOMBRE == "")
            {
                MessageBox.Show("Ingrese el nombre completo del producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return false;
            }

        

            if (CATEGORIA== "")
            {
                MessageBox.Show("Ingrese la categoría del producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return false;
            }

            //AGREGADO POR JOSE 02-12-2019
            if(radioButton3.Checked == false && radioButton4.Checked == false)
            {
                MessageBox.Show("Ingrese el tipo (PRODUCTO Ó SERVICIO)", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return false;
            } //////////////////////

            if (UNIDAD == "")
            {
                MessageBox.Show("Ingrese la unidad del producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return false;
            }

            if (CANTIDAD== "")
            {
                MessageBox.Show("Ingrese la cantidad actual del producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return false;
            }

            if (MINIMO == "")
            {
                MessageBox.Show("Ingrese la cantidad minima de compra del producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return false;
            }

            if (MAXIMO == "")
            {
                MessageBox.Show("Ingrese la cantidad maxima de compra del producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return false;
            }


            if (DESCRIBE == "")
            {
                MessageBox.Show("Ingrese la descripción detallada del producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return false;
            }

            if (DISTRIBUIDOR == 0)
            {
                MessageBox.Show("Ingrese el precio de distribuidor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return false;
            }

            if (PRECIO1== 0)
            {
                MessageBox.Show("Ingrese el precio publico 1", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return false;
            }

            if (GANANCIA1== 0)
            {
                MessageBox.Show("Calcule la ganancia del precio publico 1", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return false;
            }

            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 90);
            Lv.Columns.Add("Nombre", 350);
            Lv.Columns.Add("Categoría", 90);
            Lv.Columns.Add("Tipo", 90);       //AGREGADO POR JOSE 02-12-2019
            Lv.Columns.Add("Unidad", 70);
            Lv.Columns.Add("Existencia", 60);
            Lv.Columns.Add("Precio Proveedor", 70);
            Lv.Columns.Add("Precio 1", 70);
            Lv.Columns.Add("% Descuento", 70);
            Lv.Columns.Add("Con IVA", 60);
            Lv.Columns.Add("Fec. Modificacion", 85);
            Lv.Visible = false;
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            string Query = "Select cvproducto,nombre,categoria,unidad,cantidad,causaiva,fechaModifica,minimo";
            Query = Query + ",Cat_Categorias.descripcion as nomcategoria, Cat_tipos.descripcion as nomtipo "; //AGREGADO POR JOSE 02-12-2019
            Query = Query + " from productos";
            Query = Query + " inner join Cat_Categorias on Cat_Categorias.idcategoria=productos.categoria ";
            Query = Query + " inner join Cat_tipos on Cat_tipos.idtipo = Productos.idtipo";                  //AGREGADO POR JOSE 02-12-2019
            Query = Query + " where cvproducto <> '' and  activo=1";

            //Query = Query + "Cat_tipos.descripcion as nomtipo from Productos inner join Cat_tipos on Cat_tipos.idtipo = Productos.idtipo where cvproducto <> ''";
            if (textBox11.Text != "") Query = Query + " and nombre like '%" + textBox11.Text + "%'";
            if (comboBox2.Text!= "") Query = Query + " and  categoria='" + comboBox2.SelectedValue.ToString()+ "'";
            if (comboBox4.Text != "") Query = Query + " and marca='" + comboBox2.Text + "'";
            if (comboBox7.Text != "") Query = Query + " and unidad='" + comboBox7.Text.Trim() + "'";
            if (textBox23.Text != "") Query = Query + " and cvproducto='" + textBox23.Text.Trim() + "'";
            if (checkBox1.Checked == true) Query = Query + " and cantidad<=(minimo+2)";
            if (comboBox8.Text != "") Query = Query + "and Cat_tipos.idtipo = '" + comboBox8.SelectedValue.ToString().Trim() + "'";


            Query = Query + " order by nombre asc, Cat_Categorias.descripcion asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string clave = leer["cvproducto"].ToString();
                ListViewItem lvi = new ListViewItem(clave);
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["nomcategoria"].ToString());
                lvi.SubItems.Add(leer["nomtipo"].ToString());        //AGREGADO POR JOSE 02-12-2019
                lvi.SubItems.Add(leer["unidad"].ToString());                
                lvi.SubItems.Add(leer["cantidad"].ToString());

                string valor = leer["cantidad"].ToString();
                decimal num = decimal.Parse(valor.ToString());

                string consulta = "Select * from ListaPrecios where cvproducto='" + clave + "'";
                SqlDataReader leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    decimal Distribuidor = decimal.Parse(leer2["distribuidor"].ToString());
                    decimal publico1 = decimal.Parse(leer2["publico1"].ToString());
                    decimal descuento =0;
                    if (leer2["porcdescuento"].ToString()!="")
                        descuento = decimal.Parse(leer2["porcdescuento"].ToString());
                    else
                       descuento =0;
                    
                    
                    lvi.SubItems.Add(Distribuidor.ToString("##.0000", CultureInfo.InvariantCulture));
                    lvi.SubItems.Add(publico1.ToString("##.0000", CultureInfo.InvariantCulture));
                    lvi.SubItems.Add(descuento.ToString("##.0000", CultureInfo.InvariantCulture) + " %");
                }
                conecta2.CierraConexion();
                lvi.SubItems.Add(leer["causaiva"].ToString());
                lvi.SubItems.Add(leer["fechaModifica"].ToString());


                decimal num5 = decimal.Parse(leer["minimo"].ToString());
                Lv.Items.Add(lvi);

                lvi.UseItemStyleForSubItems = false;
                if (num <= (num5 + 2))
                {
                    lvi.BackColor = Color.FromArgb(0xd9, 0xdf, 0xfb);
                    lvi.SubItems[5].BackColor = Color.FromArgb(0xf7, 190, 0x81);
                }
                if (num < num5)
                {
                    lvi.BackColor = Color.FromArgb(0xd9, 0xdf, 0xfb);
                    lvi.SubItems[5].BackColor = Color.FromArgb(0xb2, 5, 5);
                    lvi.SubItems[5].ForeColor = Color.FromArgb(0xff, 0xff, 0xff);
                }
                

               
            }
            conecta.CierraConexion();
            label15.Text = Lv.Items.Count.ToString() + " Registros ";
            Lv.Visible = true;
            
                label39.Text = Lv.Items.Count.ToString() + " Registros";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            combos.MarcasProductos(comboBox3);
            panel1.Visible = true;
            panel3.Visible = false;
            panel6.Visible = false;
            panel4.Visible = false;
            Limpiar();

            /*
            conectorSql conecta = new conectorSql();
            string consulta = "Select numproducto +3 as numproducto from Consecutivos";
            SqlDataReader leer2 = conecta.RecordInfo(consulta);
            while (leer2.Read())
            {
                int NumProducto = int.Parse(leer2["numproducto"].ToString());

                textBox1.Text = NumProducto.ToString();
            }
            conecta.CierraConexion();
            */

            button1.Enabled = true;

        }

        public void Limpiar()
        {
            panel6.Visible = true;
            panel4.Visible = true;
            panel3.Visible = true;
            BandConsecutivo = false;
            textBox1.Text = "";
            textBox1.Enabled = true;
            textBox2.Text = "";
            comboBox5.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox9.Text = "";
            comboBox1.Text = "";
            comboBox8.Text = "";  //GREGADO POR JOSE 04-12-2019 
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";

            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
            comboBox3.Text = "";

            textBox3.Text = "";
            textBox21.Text = "";
            textBox22.Text = "";
            comboBox6.Text = "";
            comboBox6.Text = "";

            radioButton3.Checked = false;  //agregado por jose 02-12-2019
            radioButton4.Checked = false;  //agregado por jose 02-12-2019

            combos.Categoriaproducto(comboBox1);
            combos.Categoriaproducto(comboBox2);
            combos.CatalogoTipos(comboBox8); //agregado por jose 04-12-2019
            textBox1.Focus();
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
            panel1.Visible = false;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now.AddDays(-15);

            combos.MarcasProductos(comboBox3);
            combos.MarcasProductos(comboBox4);
            combos.Categoriaproducto(comboBox1);
            combos.Categoriaproducto(comboBox2);
            combos.Unidadproducto(comboBox7);
            combos.CatalogoTipos(comboBox8); //GREGADO POR JOSE 04-12-2019

        }

        public void BuscarInformacion(string clave)
        {
            Limpiar();
            string valor="";
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            string Query = "Select * from productos where cvproducto='" + clave + "' and activo=1";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox1.Text = clave;
                textBox2.Text = leer["nombre"].ToString();
                comboBox1.SelectedValue = leer["categoria"].ToString(); //EDITADO POR JOSE 29-11-2019
               
                decimal entero = decimal.Parse(leer["idtipo"].ToString()); //AGREGADO POR JOSE 02-12-2019
                if (entero == 1) radioButton3.Checked = true;       //AGREGADO POR JOSE 02-12-2019
                if (entero == 2) radioButton4.Checked = true;       //AGREGADO POR JOSE 02-12-2019

                comboBox5.Text = leer["unidad"].ToString();
                textBox4.Text = leer["cantidad"].ToString();
                textBox5.Text = leer["minimo"].ToString();
                textBox6.Text = leer["maximo"].ToString();
                textBox7.Text = leer["descripcion"].ToString();
                UPC.Text = leer["codbarras"].ToString();
                textBox20.Text = leer["ubicacion"].ToString();
                comboBox3.Text = leer["marca"].ToString();

                string consulta = "Select * from ListaPrecios where cvproducto='" + clave + "'";
                SqlDataReader leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    //textBox8.Text = leer2["distribuidor"].ToString();
                    textBox9.Text = leer2["porciento1"].ToString();
                    textBox12.Text = leer2["publico1"].ToString();
                    textBox13.Text = leer2["ganancia1"].ToString();

                    textBox16.Text = leer2["porciento2"].ToString();
                    textBox15.Text = leer2["publico2"].ToString();
                    textBox14.Text = leer2["ganancia2"].ToString();

                    textBox19.Text = leer2["porciento3"].ToString();
                    textBox18.Text = leer2["publico3"].ToString();
                    textBox17.Text = leer2["ganancia3"].ToString();

                    textBox3.Text = leer2["porcdescuento"].ToString();


                 }

                PrecioAnt = textBox12.Text;

                conecta2.CierraConexion();
                valor = leer["causaiva"].ToString();
                if (valor == "SI") radioButton1.Checked = true;
                else radioButton2.Checked = true;

               // valor = leer["cvproveedor"].ToString();
              
            }
            conecta.CierraConexion();

            BuscarinfoHistorial();

        }

        public void BuscarinfoHistorial()
        {
            string fecha1 = dateTimePicker1.Value.ToString("yyyyMMdd");
            string fecha2 = dateTimePicker2.Value.ToString("yyyyMMdd");
            
            string clave = textBox1.Text.Trim();
            string Query = "";
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("Recibo", 60);
            listView1.Columns.Add("Clave Cliente", 50);
            listView1.Columns.Add("Cliente", 90);
            listView1.Columns.Add("Describe", 170);
            listView1.Columns.Add("Cantidad", 80);
            listView1.Columns.Add("Precio Unitario", 80);
            listView1.Columns.Add("Precio ", 80);
            listView1.Columns.Add("Ganancia", 80);
            listView1.Columns.Add("Fecha", 80);

            Query = "Select detallesrecibos.numrecibo,";
            Query = Query + " detallesrecibos.cvcliente,";
            Query = Query + " clientes.nombre,";
            Query = Query + "detallesrecibos.descripcion,";
            Query = Query + "detallesrecibos.cantidad,";
            Query = Query + " detallesrecibos.preunitario,";
            Query = Query + " detallesrecibos.precio,";
            Query = Query + " detallesrecibos.tganancia,";
            Query = Query + " detallesrecibos.fecha";

            Query = Query + " from detallesRecibos ";
            Query = Query + " FULL OUTER JOIN clientes on detallesrecibos.cvcliente=clientes.cvcliente";
            Query = Query + " where detallesrecibos.cvproducto='" + clave + "'";
            Query = Query + " AND detallesrecibos.fechacod between '" + fecha1 + "' and '" + fecha2 + "'";
            Query = Query + " order by detallesrecibos.fechacod desc";
            listView1.Visible = false;
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string numrecibo = leer["numrecibo"].ToString();
                ListViewItem lvi = new ListViewItem(numrecibo);
                lvi.SubItems.Add(leer["cvcliente"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["descripcion"].ToString());
                lvi.SubItems.Add(leer["cantidad"].ToString());
                lvi.SubItems.Add(leer["preunitario"].ToString());
                lvi.SubItems.Add(leer["precio"].ToString());
                lvi.SubItems.Add(leer["tganancia"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                listView1.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label33.Text = listView1.Items.Count.ToString() + " Recibos ";
            listView1.Visible = true;

        
        }

        public void Actualizar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "update productos set";
            Query = Query + " nombre='" + NOMBRE + "'";
            Query = Query + ",descripcion='" + DESCRIBE + "'";
            Query = Query + ",categoria='" + CATEGORIA + "'";
            Query = Query + ",unidad='" + UNIDAD + "'";
            Query = Query + ",cantidad='" + CANTIDAD + "'";
            Query = Query + ",minimo='" + MINIMO + "'";
            Query = Query + ",maximo='" + MAXIMO + "'";
            Query = Query + ",codbarras='" + CODIGODEBARRAS + "'";
            Query = Query + ",ubicacion='" + UBICACION + "'";
            Query = Query + ",marca='" + MARCA + "'";

            Query = Query + ",idtipo='" + PRODUCTOSERVICIO + "'";    //AGREGADO POR JOSE 02-12-2019

            Query = Query + ",fechaModifica='" + FECHAMODIFICA + "'";
            Query = Query + ",fcodmodifica='" + FCODMODIFICA + "'";
            Query = Query + ",emitio='" + EMITE + "'";
            Query = Query + ",pasillo='" + PASILLO + "'";
            Query = Query + ",altura='" + ALTURA+ "'";
            Query = Query + ",sucursal='" + SUCURSAL+ "'";

            Query = Query + ",causaiva='" + CAUSAIVA+ "'";            
            Query = Query + " where cvproducto='" + CLAVE + "'";
            conecta.Excute(Query);

            Query = "update ListaPrecios set ";
            Query = Query + " distribuidor='" + DISTRIBUIDOR + "'";
            Query = Query + ",publico1='" + PRECIO1.ToString() + "'";
            Query = Query + ",porciento1='" + PORCENTAJE1.ToString() + "'";
            Query = Query + ",ganancia1='" + GANANCIA1.ToString() + "'";
            Query = Query + ",publico2='" + PRECIO2.ToString() + "'";
            Query = Query + ",porciento2='" + PORCENTAJE2.ToString() + "'";
            Query = Query + ",ganancia2='" + GANANCIA2.ToString() + "'";
            Query = Query + ",publico3='" + PRECIO3.ToString() + "'";
            Query = Query + ",porciento3='" + PORCENTAJE3.ToString() + "'";
            Query = Query + ",ganancia3='" + GANANCIA3.ToString() + "'";
            Query = Query + ",porcdescuento='" + PORCDESCUENTO.Trim() + "'"; 
            Query = Query + " where cvproducto='" + CLAVE + "'";
            conecta.Excute(Query);

        }

        public void Guardar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "insert into productos(";
            Query = Query + "cvproducto";
            Query=Query + ",nombre";
            Query = Query + ",descripcion";
            Query = Query + ",categoria";
            Query = Query + ",unidad";
            Query = Query + ",cantidad";
            Query = Query + ",minimo";
            Query = Query + ",causaiva";
            Query = Query + ",codbarras";
            Query = Query + ",ubicacion";
            Query = Query + ",marca";

            Query = Query + ",idtipo";                //AGREGADO POR JOSE 02-12-2019

            Query = Query + ",fechaModifica";
            Query = Query + ",fcodmodifica";
            Query = Query + ",emitio";
            Query = Query + ",pasillo";
            Query = Query + ",altura";
            Query = Query + ",sucursal";

            Query = Query + ",maximo)";
            Query = Query + " values(";

            Query = Query + "'" + CLAVE + "'" ;
            Query = Query + ",'" + NOMBRE+ "'";
            Query = Query + ",'" + DESCRIBE + "'";
            Query = Query + ",'" + CATEGORIA + "'";
            Query = Query + ",'" + UNIDAD + "'";
            Query = Query + ",'" + CANTIDAD + "'";
            Query = Query + ",'" + MINIMO + "'";
            Query = Query + ",'" + CAUSAIVA+ "'";
            Query = Query + ",'" + CODIGODEBARRAS + "'";
            Query = Query + ",'" + UBICACION + "'";
            Query = Query + ",'" + MARCA + "'";

            Query = Query + ",'" + PRODUCTOSERVICIO + "'";    //AGREGADO POR JOSE 02-12-2019

            Query = Query + ",'" + FECHAMODIFICA + "'";
            Query = Query + ",'" + FCODMODIFICA + "'";
            Query = Query + ",'" + EMITE + "'";
            Query = Query + ",'" + PASILLO + "'";
            Query = Query + ",'" + ALTURA + "'";
            Query = Query + ",'" + SUCURSAL + "'";

            Query = Query + ",'" + MAXIMO + "')";
            conecta.Excute(Query);

            /*
             * Obtener el Id recién creado y agregarlo a la tabla lista precios
             * 
             */
            Query = "Select cvproducto from Productos where nombre = '" + NOMBRE + "' and descripcion ='" + DESCRIBE + "' AND categoria ='" + CATEGORIA + "'";
            conectorSql conecta1 = new conectorSql();
           
            SqlDataReader leer3 = conecta1.RecordInfo(Query);
            int NumProducto = 0;
            while (leer3.Read())
            {
                NumProducto = int.Parse(leer3["cvproducto"].ToString());

                textBox1.Text = NumProducto.ToString();
            }
            conecta.CierraConexion();

            textBox1.Text = NumProducto.ToString();

            Query ="Insert into ListaPrecios(cvproducto";
            Query = Query + ",distribuidor";
            Query = Query + ",publico1";
            Query = Query + ",porciento1";
            Query = Query + ",ganancia1";
            Query = Query + ",publico2";
            Query = Query + ",porciento2";
            Query = Query + ",ganancia2";
            Query = Query + ",publico3";
            Query = Query + ",porciento3";
            Query = Query + ",porcdescuento";
            Query = Query + ",ganancia3)";
            Query = Query + " values(";
            Query = Query + "" + NumProducto.ToString() + "";
            Query = Query + ",'" + DISTRIBUIDOR + "'";
            Query = Query + ",'" + PRECIO1.ToString()+ "'";
            Query = Query + ",'" + PORCENTAJE1.ToString() +"'";
            Query = Query + ",'" + GANANCIA1.ToString()+ "'";
            Query = Query + ",'" + PRECIO2.ToString() + "'";
            Query = Query + ",'" + PORCENTAJE2.ToString() + "'";
            Query = Query + ",'" + GANANCIA2.ToString() + "'";
            Query = Query + ",'" + PRECIO3.ToString() + "'";
            Query = Query + ",'" + PORCENTAJE3.ToString() + "'";
            Query = Query + ",'" + PORCDESCUENTO.Trim() + "'";
            Query = Query + ",'" + GANANCIA3.ToString() + "')";
            conecta.Excute(Query);

        }

        public bool ExisteInfo()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from productos where cvproducto='" + CLAVE + "'";
            return conecta.ExisteRegistro(Query);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //CalcularTresPrecios();
            Recolectar();
            if (Validacion() == true)
            {
               
                if (ExisteInfo() == false)
                {
                    if (BandConsecutivo == true) ActualizarConsecutivo();
                    Guardar();
                    MessageBox.Show("Se guardo correctamente la información registrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                else
                {
                    Actualizar();
                    MessageBox.Show("Se actualizo correctamente la información registrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }

               
            }
        }

        public void ActualizarConsecutivo()
        {
            int Numero = int.Parse(textBox1.Text);
            Numero++;
            conectorSql conecta = new conectorSql();
            string Query = "Update Consecutivos set numproducto='" + Numero.ToString() + "'";
            conecta.Excute(Query);
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
        public void DetallesModifica(int index)
        {
            textBox1.Text = Lv.Items[index].Text;
            textBox1.Enabled = false;
            BuscarInformacion(textBox1.Text);
            panel1.Visible = true;
            panel3.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("¿Desea eliminar la información seleccionada?", "Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;

            conectorSql conecta = new conectorSql();
            string Query = "";
            int contar = 0;

            string clave = "";
            string descripcion="";
            for (int i = 0; i < Lv.Items.Count; i++)
            {
                clave=Lv.Items[i].Text;
                descripcion = Lv.Items[i].SubItems[1].Text;
                if (Lv.Items[i].Checked == true)
                {
                    Query = "update productos set activo=0, descripcion='ELIMINADO' where cvproducto='" + clave + "'";
                    conecta.Excute(Query);
                    Query = "Delete from ListaPrecios where cvproducto='" + clave + "'";
                    conecta.Excute(Query);

                    contar++;
                }
            }
            string cfnFile = @"\\SRV-DATACENTER\tmp\EmailConf.xml";
            bool cfnExist = File.Exists(cfnFile);
            XDocument xdoc = XDocument.Load(cfnExist ? @"\\SRV-DATACENTER\tmp\EmailConf.xml" : @"C:\tmp\EmailConf.xml");
            string EnableMail = xdoc.Descendants("EnableSendMails").First().Value;
            if (EnableMail.Equals("1"))
            {
                MailNotifications mail = new MailNotifications();
                mail.SendMailOnlySubjectAndMSG("Eliminación de producto id: " + clave + " (" + descripcion + ")", "Eliminación permanente de producto hecha por el usuario: " + valoresg.IdEmployee + " " + valoresg.Nombre_Completo.Trim());
            }
            refreshData();

            MessageBox.Show("Se eliminaron " + contar.ToString() + " registros del sistema", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("¿Desea exportar la información a excel?", "Exportación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;

            string porsurtir="NO";
            if (checkBox1.Checked==true) porsurtir="SI";

            string idcategoria = "";
            if (comboBox2.Text != "") idcategoria = comboBox2.SelectedValue.ToString();

            string TIPOS = "";
            if(comboBox8.Text !="") TIPOS = comboBox8.SelectedValue.ToString();

            ReportesNKB.RBusquedaProductos(textBox11.Text, idcategoria, TIPOS, comboBox4.Text.Trim(), textBox23.Text.Trim(), comboBox7.Text.Trim(), porsurtir);
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode) CalculoPrecio1();
        }

        public void CalculoPrecio1()
        {
            if (textBox9.Text == "")
            {
                MessageBox.Show("Ingrese el porcentaje de ganancia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox9.Focus();
                return;
            }

          
            decimal PorcentajeP = 0;
            bool NumeroEs = decimal.TryParse(textBox9.Text, out PorcentajeP);
            if (NumeroEs == false)
            {
                MessageBox.Show("Ingrese el porcentaje de ganancia valido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox9.Text = "";
                textBox9.Focus();
                return;
            }

            decimal Distribuidor=0.01M;
            decimal porcentaje = decimal.Parse(textBox9.Text);
            decimal ResPrecio = ((porcentaje * Distribuidor) / 100) + Distribuidor;
            decimal Ganancia = ResPrecio - Distribuidor;


            if (Ganancia <= 0)
            {
                MessageBox.Show("Ingrese porcentaje de ganancia debe ser mayor a 0", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox9.Text = "";
                textBox9.Focus();
                return;

            }

            textBox12.Text = ResPrecio.ToString("##.0000", CultureInfo.InvariantCulture);
            textBox13.Text = Ganancia.ToString("##.0000", CultureInfo.InvariantCulture);
        }

        public void CalculoPrecio1_porciento()
        {
            decimal Distribuidor =0;
            if (Distribuidor == 0)
            {
                Distribuidor = 1;
            }

            if (textBox12.Text == "")
            {
                MessageBox.Show("Ingrese el precio publico", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox12.Focus();
                return;
            }

            decimal PublicoP = 0;
            bool NumeroEs = decimal.TryParse(textBox12.Text,out PublicoP);
            if (NumeroEs==false)
            {
                MessageBox.Show("Ingrese el precio publico valido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox12.Text = "";
                textBox12.Focus();
                return;
            }

            if (PublicoP <= Distribuidor)
            {
                MessageBox.Show("Ingrese el precio publico debe ser mayor al del distribuidor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox12.Text = "";
                textBox12.Focus();
                return;
            
            }

            decimal ResPrecio = decimal.Parse(textBox12.Text); //precio publico
            decimal Ganancia = ResPrecio - Distribuidor;

            decimal porcentaje = ((Ganancia * 100) / Distribuidor);

            textBox9.Text = porcentaje.ToString("##.0000", CultureInfo.InvariantCulture);
            textBox13.Text = Ganancia.ToString("##.0000", CultureInfo.InvariantCulture);
        }

        private void textBox16_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode) CalculoPrecio2();
        }
        public void CalculoPrecio2()
        {
            if (textBox16.Text == "") textBox16.Text = "0";

            decimal Distribuidor = 0;
            decimal porcentaje = decimal.Parse(textBox16.Text);
            decimal ResPrecio = ((porcentaje * Distribuidor) / 100) + Distribuidor;
            decimal Ganancia = ResPrecio - Distribuidor;

            textBox15.Text = ResPrecio.ToString("##.0000", CultureInfo.InvariantCulture);
            textBox14.Text = Ganancia.ToString("##.0000", CultureInfo.InvariantCulture);

            if (porcentaje<=0) textBox15.Text = "0";
        }

        private void textBox19_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode) CalculoPrecio3();
        }
        public void CalculoPrecio3()
        {
            if (textBox19.Text == "") textBox19.Text = "0";
            decimal Distribuidor =0.01M;
            decimal porcentaje = decimal.Parse(textBox19.Text);
            decimal ResPrecio = ((porcentaje * Distribuidor) / 100) + Distribuidor;
            decimal Ganancia = ResPrecio - Distribuidor;

            textBox18.Text = ResPrecio.ToString("##.0000", CultureInfo.InvariantCulture);
            textBox17.Text = Ganancia.ToString("##.0000", CultureInfo.InvariantCulture);

            if (porcentaje<=0) textBox18.Text = "0";
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode) CalculoPrecio1_porciento();
        }

        private void textBox15_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode) CalculoPrecio2_porciento();

        }

        public void CalculoPrecio2_porciento()
        {
            if (textBox15.Text == "")
            {
                MessageBox.Show("Ingrese el precio publico", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            decimal Distribuidor = 0.01M;
            decimal ResPrecio = decimal.Parse(textBox15.Text); //precio publico
            decimal Ganancia = ResPrecio - Distribuidor;

            decimal porcentaje = ((Ganancia * 100) / Distribuidor);

            textBox16.Text = porcentaje.ToString("##.0000", CultureInfo.InvariantCulture);
            textBox14.Text = Ganancia.ToString("##.0000", CultureInfo.InvariantCulture);
        }

        private void textBox18_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode) CalculoPrecio3_porciento();

        }
        public void CalculoPrecio3_porciento()
        {
            decimal Distribuidor = 0.01M;
            decimal ResPrecio = decimal.Parse(textBox18.Text); //precio publico
            decimal Ganancia = ResPrecio - Distribuidor;

            decimal porcentaje = ((Ganancia * 100) / Distribuidor);

            textBox19.Text = porcentaje.ToString("##.0000", CultureInfo.InvariantCulture);
            textBox17.Text = Ganancia.ToString("##.0000", CultureInfo.InvariantCulture);
        }

        public void CalcularTresPrecios()
        {
            CalculoPrecio1();
            CalculoPrecio2();
            CalculoPrecio3();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CalcularTresPrecios();
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox9.Focus();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        bool BandConsecutivo;

        private void button9_Click(object sender, EventArgs e)
        {
            BandConsecutivo = true;
            textBox1.Text = numconsecutivo();
            textBox1.Enabled = false;
            textBox2.Focus();
        }

        public string numconsecutivo()
        {
            string Numero = "1";
            conectorSql conecta = new conectorSql();
            string Query = "Select numproducto from consecutivos where numproducto<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                Numero = leer["numproducto"].ToString();
            }
            conecta.CierraConexion();

            return Numero;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Lv.Items.Count == 0)
            {
                MessageBox.Show("No existe ningún elemento en la lista!\n Es necesario que existan elementos en la lista para ser importados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[,] Informacion = new string[Lv.Items.Count, 3];
            int contador = 0;
            for (int i = 0; i < Lv.Items.Count; i++)
            {

                if (Lv.Items[i].Checked == true)
                {
                Informacion[contador, 0] = Lv.Items[i].Text.ToString();
                Informacion[contador, 1] = Lv.Items[i].SubItems[1].Text.ToString();
                Informacion[contador, 2] = Lv.Items[i].SubItems[4].Text.ToString();
                contador++;
                }        
            }
            this.Cursor = Cursors.WaitCursor;
            ReportesNKB.importaExistencias(Informacion, Lv.Items.Count);
            this.Cursor = Cursors.Default;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) BuscarInformacion(textBox1.Text.Trim());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            BuscarinfoHistorial();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            LeerArchivoExcel importarpro = new LeerArchivoExcel();
            importarpro.Show();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) { JOSE_FROMS.CodigoQR QR = new JOSE_FROMS.CodigoQR(); QR.Show(); }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            int n;

            if (textBox2.Text=="")
            {
                MessageBox.Show("El nombre del producto debe tener una descripción válida");
                return;
            }

            if (comboBox1.Text == "")
            {
                MessageBox.Show("El producto debe contener una Categoría válilda");
                return;
            }


            //
            if (textBox12.Text == "")
            {
                MessageBox.Show("El producto debe tener un precio (en número), no puede estar en blanco");
                return;
            }

            if (textBox5.Text == "")
            {
                MessageBox.Show("El producto debe tener una unidad de medida, no puede estar en blanco");
                return;
            }


            if (textBox3.Text == "")
            {
                MessageBox.Show("El producto debe tener una marca, no puede estar en blanco");
                return;
            }


            if (textBox4.Text == "")
            {
                MessageBox.Show("El producto debe tener una cantidad existente, no puede estar en blanco");
                return;
            }

            try
            {
                
                button1.Enabled = false;
                conectorSql conecta = new conectorSql();

                 string CATEGORIA = "";

                 string categonbr = "Select idCategoria from Cat_Categorias where descripcion = '" + comboBox1.Text + "'";
                conectorSql conecta1 = new conectorSql();
            
                SqlDataReader leer = conecta.RecordInfo(categonbr);
                while (leer.Read())
                {
                   CATEGORIA = leer["idCategoria"].ToString();
             
                }
                

                string SQLexiste = "";
                if (textBox1.Text.Length==0)
                {
                    SQLexiste = "Select count(*) as conteo from Productos where cvproducto = (SELECT MAX(cvproducto)+1 from Productos) and activo = 1";
                } else
                {
                    SQLexiste = "Select count(*) as conteo from Productos where cvproducto = " + textBox1.Text +" and activo = 1";
                }

                string resultadoExiste = "0";

                SqlDataReader datos = conecta.RecordInfo(SQLexiste);
                while (datos.Read())
                {
                    resultadoExiste = datos["conteo"].ToString();

                }

                conecta.CierraConexion();
                int ResultadosExisteInt = Int16.Parse(resultadoExiste);

                if (ResultadosExisteInt == 1)
                {
                    // Si el producto existe entonces habrá que actualizar los datos

                    conectorSql conectaUpdate1 = new conectorSql();
                    string QueryUpdateActual = "";
                    // ueryUpdateActual = "delete from Productos where cvproducto ='" + textBox1.Text + "'";

                    QueryUpdateActual = "update productos  set cantidad = " + textBox4.Text + ", marca ='" + comboBox3.Text + "', nombre = '" + textBox2.Text +"', descripcion = '" + textBox7.Text + "', unidad ='"+ comboBox5.Text +"'  where cvproducto=" + textBox1.Text;
                    conecta.Excute(QueryUpdateActual);
                    QueryUpdateActual = "update ListaPrecios set publico1 =" + textBox12.Text +", porciento1 = " + textBox9.Text + ", ganancia1= " + textBox13.Text +"   where cvproducto=" + textBox1.Text ;
                    conecta.Excute(QueryUpdateActual);


                    //conectaUpdate1.Excute(QueryUpdateActual);
                    //conectaUpdate1.CierraConexion();

                    //ResultadosExisteInt = 0;

                    string cfnFile = @"\\SRV-DATACENTER\tmp\EmailConf.xml";
                    bool cfnExist = File.Exists(cfnFile);
                    XDocument xdoc = XDocument.Load(cfnExist ? @"\\SRV-DATACENTER\tmp\EmailConf.xml" : @"C:\tmp\EmailConf.xml");

                    //XDocument xdoc = XDocument.Load("./EmailConf.xml");
                    string EnableMail = xdoc.Descendants("EnableSendMails").First().Value;
                    if (EnableMail.Equals("1"))
                    {
                        MailNotifications mail = new MailNotifications();
                        mail.SendMailOnlySubjectAndMSG("Actualización de producto: " + textBox1.Text + " " + textBox2.Text, "Actualización de detalles de producto hecha por el usuario: " + valoresg.IdEmployee + " " + valoresg.Nombre_Completo.Trim() + "<br> Artículo: " + textBox1.Text + "<br> Descripción: " + textBox2.Text + "<br>Precio anterior: $" + PrecioAnt.ToString() + "<br>Precio nuevo $" + textBox12.Text);
                    }

                }

                // Validate if the Product was added before by 
                if (ResultadosExisteInt == 0)
                {

                    string query = "";
                    query = query + "INSERT INTO Productos ";
                    query = query + "(";
                    query = query + "[nombre] ";
                    query = query + ",[descripcion] ";
                    query = query + ",[categoria] ";
                    query = query + ",[unidad] ";
                    query = query + ",[cantidad] ";
                    query = query + ",[minimo] ";
                    query = query + ",[maximo] ";
                    query = query + ",[causaiva] ";
                    query = query + ",[marca] ";
                    query = query + ",[codbarras] ";
                    query = query + ",[ubicacion] ";
                    query = query + ",[fechaModifica] ";
                    query = query + ",[fcodmodifica] ";
                    query = query + ",[emitio] ";
                    query = query + ",[causaAdicional]  ";
                    query = query + ",[pasillo] ";
                    query = query + ",[altura] ";
                    query = query + ",[sucursal] ";
                    query = query + ",[idtipo] ";
                    query = query + ",[activo] ";
                    query = query + ",[cvusuario]) ";
                    query = query + " VALUES ( ";
                    // query = query + "'" + textBox1.Text + "',";

                    if (string.IsNullOrEmpty(textBox2.Text))
                    {
                        textBox2.Text = "0";
                    }
                    if (string.IsNullOrEmpty(textBox7.Text))
                    {
                        textBox7.Text = "0";
                    }


                    if (string.IsNullOrEmpty(textBox5.Text))
                    {
                        textBox5.Text = "0";
                    }
                    if (string.IsNullOrEmpty(textBox6.Text))
                    {
                        textBox6.Text = "0";
                    }

                 


                    query = query + "'" + textBox2.Text + "',";
                    query = query + "'" + textBox7.Text + "',";
                    query = query + "'" + CATEGORIA + "',";
                    query = query + "'" + comboBox5.Text + "',";
                    query = query + "" + textBox4.Text + ",";
                    query = query + "" + textBox5.Text + ",";
                    query = query + "" + textBox6.Text + ",";
                    query = query + "'" + CAUSAIVA + "',";
                    query = query + "'" + comboBox3.Text + "',";
                    query = query + "'" + UPC.Text + "',";
                    query = query + "'" + textBox20.Text + "',";
                    query = query + "'" + DateTime.Today.ToShortDateString()  + "',";
                    query = query + "'" + DateTime.Now.ToString("yyyyMMdd") + "',";
                    query = query + "'" + valoresg.USUARIOSIS + "',";
                    query = query + "'',";
                    query = query + "'" + textBox21.Text + "',";
                    query = query + "'" + comboBox6.Text + "',";
                    query = query + "'"+ valoresg.SERVER_LOCATION +"',";
                    query = query + "1,";
                    query = query + "1,";
                    query = query + "'" + valoresg.USUARIOSIS + "')";


                    // Save the product on table "Productos" 
                    bool producto = conecta.Excute(query);


                    /*
             * Obtener el Id recién creado y agregarlo a la tabla lista precios
             * 
             */
                    string Query = "Select cvproducto from Productos where nombre = '" + textBox2.Text + "' and descripcion ='" + textBox7.Text + "' AND categoria ='" + CATEGORIA + "'";
                    

                    SqlDataReader leer3 = conecta.RecordInfo(Query);
                    int NumProducto = 0;
                    while (leer3.Read())
                    {
                        NumProducto = int.Parse(leer3["cvproducto"].ToString());

                        textBox1.Text = NumProducto.ToString();
                    }
                    // conecta.CierraConexion();

                    textBox1.Text = NumProducto.ToString();

                    // Lista de precios
                    Query = "";

                    Query = "Insert into ListaPrecios(cvproducto";
                    Query = Query + ",distribuidor";
                    Query = Query + ",publico1";
                    Query = Query + ",porciento1";
                    Query = Query + ",ganancia1";
                    Query = Query + ",publico2";
                    Query = Query + ",porciento2";
                    Query = Query + ",ganancia2";
                    Query = Query + ",publico3";
                    Query = Query + ",porciento3";
                    Query = Query + ",porcdescuento";
                    Query = Query + ",ganancia3)";
                    Query = Query + " values(";
                    Query = Query + "'" + textBox1.Text + "'";
                    Query = Query + ",'" + DISTRIBUIDOR + "'";
                    Query = Query + ",'" + textBox12.Text + "'";
                    Query = Query + ",'" + textBox9.Text + "'";
                    Query = Query + ",'" + textBox13.Text + "'";
                    Query = Query + ",'" + textBox15.Text + "'";
                    Query = Query + ",'" + textBox16.Text + "'";
                    Query = Query + ",'" + textBox14.Text + "'";
                    Query = Query + ",'" + textBox18.Text + "'";
                    Query = Query + ",'" + textBox19.Text + "'";
                    Query = Query + ",'" + PORCDESCUENTO.Trim() + "'";
                    Query = Query + ",'" + textBox17.Text + "')";
                    bool precios = conecta.Excute(Query);

                    conecta.CierraConexion();

                   /*
                    conectorSql conectaUpdate = new conectorSql();
                    string QueryUpdate = "";
                    QueryUpdate = "update consecutivos set numproducto =" + textBox1.Text + "";

                    conectaUpdate.Excute(QueryUpdate);
                    */


                    MessageBox.Show("Artículo con ID: " + textBox1.Text  + "  agregado /o modificado satisfactoriamente");
                    // this.Dispose();
                    Limpiar();
                    /*
                    conectorSql conecta11 = new conectorSql();
                    string consulta = "Select numproducto +3 as numproducto from Consecutivos";
                    SqlDataReader leer211 = conecta11.RecordInfo(consulta);
                    while (leer211.Read())
                    {
                        int NumProducto = int.Parse(leer211["numproducto"].ToString());

                        textBox1.Text = NumProducto.ToString();
                    }
                    conecta11.CierraConexion();
                    */
                }

                panel1.Visible = false;
                panel3.Visible = true;
                refreshData();
            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message); 
            } finally
            {
                button1.Enabled = true;
            }

        }

        private void button9_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void refreshData()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 90);
            Lv.Columns.Add("Nombre", 350);
            Lv.Columns.Add("Categoría", 90);
            Lv.Columns.Add("Tipo", 90);       //AGREGADO POR JOSE 02-12-2019
            Lv.Columns.Add("Unidad", 70);
            Lv.Columns.Add("Existencia", 60);
            Lv.Columns.Add("Precio Proveedor", 70);
            Lv.Columns.Add("Precio 1", 70);
            Lv.Columns.Add("% Descuento", 70);
            Lv.Columns.Add("Con IVA", 60);
            Lv.Columns.Add("Fec. Modificacion", 85);
            Lv.Visible = false;
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            string Query = "Select cvproducto,nombre,categoria,unidad,cantidad,causaiva,fechaModifica,minimo";
            Query = Query + ",Cat_Categorias.descripcion as nomcategoria, Cat_tipos.descripcion as nomtipo "; //AGREGADO POR JOSE 02-12-2019
            Query = Query + " from productos";
            Query = Query + " inner join Cat_Categorias on Cat_Categorias.idcategoria=productos.categoria ";
            Query = Query + " inner join Cat_tipos on Cat_tipos.idtipo = Productos.idtipo";                  //AGREGADO POR JOSE 02-12-2019
            Query = Query + " where cvproducto <> ''";


            //Query = Query + "Cat_tipos.descripcion as nomtipo from Productos inner join Cat_tipos on Cat_tipos.idtipo = Productos.idtipo where cvproducto <> ''";
            if (textBox11.Text != "") Query = Query + " and nombre like '%" + textBox11.Text + "%'";
            if (comboBox2.Text != "") Query = Query + " and  categoria='" + comboBox2.SelectedValue.ToString() + "'";
            if (comboBox4.Text != "") Query = Query + " and marca='" + comboBox2.Text + "'";
            if (comboBox7.Text != "") Query = Query + " and unidad='" + comboBox7.Text.Trim() + "'";
            if (textBox23.Text != "") Query = Query + " and cvproducto='" + textBox23.Text.Trim() + "'";
            if (checkBox1.Checked == true) Query = Query + " and cantidad<=(minimo+2)";
            if (comboBox8.Text != "") Query = Query + "and Cat_tipos.idtipo = '" + comboBox8.SelectedValue.ToString().Trim() + "'";


            Query = Query + " order by nombre asc, Cat_Categorias.descripcion asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string clave = leer["cvproducto"].ToString();
                ListViewItem lvi = new ListViewItem(clave);
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["nomcategoria"].ToString());
                lvi.SubItems.Add(leer["nomtipo"].ToString());        //AGREGADO POR JOSE 02-12-2019
                lvi.SubItems.Add(leer["unidad"].ToString());
                lvi.SubItems.Add(leer["cantidad"].ToString());

                string valor = leer["cantidad"].ToString();
                decimal num = decimal.Parse(valor.ToString());

                string consulta = "Select * from ListaPrecios where cvproducto='" + clave + "'";
                SqlDataReader leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    decimal Distribuidor = decimal.Parse(leer2["distribuidor"].ToString());
                    decimal publico1 = decimal.Parse(leer2["publico1"].ToString());
                    decimal descuento = 0;
                    if (leer2["porcdescuento"].ToString() != "")
                        descuento = decimal.Parse(leer2["porcdescuento"].ToString());
                    else
                        descuento = 0;


                    lvi.SubItems.Add(Distribuidor.ToString("##.0000", CultureInfo.InvariantCulture));
                    lvi.SubItems.Add(publico1.ToString("##.0000", CultureInfo.InvariantCulture));
                    lvi.SubItems.Add(descuento.ToString("##.0000", CultureInfo.InvariantCulture) + " %");
                }
                conecta2.CierraConexion();
                lvi.SubItems.Add(leer["causaiva"].ToString());
                lvi.SubItems.Add(leer["fechaModifica"].ToString());


                decimal num5 = decimal.Parse(leer["minimo"].ToString());
                Lv.Items.Add(lvi);

                lvi.UseItemStyleForSubItems = false;
                if (num <= (num5 + 2))
                {
                    lvi.BackColor = Color.FromArgb(0xd9, 0xdf, 0xfb);
                    lvi.SubItems[5].BackColor = Color.FromArgb(0xf7, 190, 0x81);
                }
                if (num < num5)
                {
                    lvi.BackColor = Color.FromArgb(0xd9, 0xdf, 0xfb);
                    lvi.SubItems[5].BackColor = Color.FromArgb(0xb2, 5, 5);
                    lvi.SubItems[5].ForeColor = Color.FromArgb(0xff, 0xff, 0xff);
                }



            }
            conecta.CierraConexion();
            label15.Text = Lv.Items.Count.ToString() + " Registros ";
            Lv.Visible = true;

            label39.Text = Lv.Items.Count.ToString() + " Registros";
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

