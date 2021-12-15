using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Reflection;

namespace SHOPCONTROL
{
    public partial class LeerArchivoExcel : Form
    {

        string chosen_file = "";
        string producto, descripcion, us, existencia, preciopublico, preciominimo = "";
        public string[] ListTempProduct;
        public string[] ListTempDescription;
        public string[] ListTempUs;
        public string[] ListTempExistencia;

        public LeerArchivoExcel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.label1.Text = "...";
            this.label4.Text = "...";
            this.label6.Text = "...";
            this.Lv.Items.Clear();
            this.openFileDialog1.InitialDirectory = "C:";
            this.openFileDialog1.Title = "Seleccione archivo de excel";
            this.openFileDialog1.Filter = "Archivos de Excel|*.xlsx";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.chosen_file = this.openFileDialog1.FileName;
                this.textBox2.Text = "";
                this.label1.Text = "...";
                this.textBox2.Text = this.chosen_file;
                this.chosen_file = this.textBox2.Text;
                this.Cursor = Cursors.WaitCursor;
                this.Lv.Visible = false;
                this.readFile(this.chosen_file);
                this.Cursor = Cursors.Default;
            }

        }

        private void readFile(string direction)
        {
            try
            {
                Microsoft.Office.Interop.Excel._Application xlApp;
                Microsoft.Office.Interop.Excel._Workbook xlLibro;
                Microsoft.Office.Interop.Excel._Worksheet xlHoja1;
                Microsoft.Office.Interop.Excel.Sheets xlHojas;
                //asigno la ruta dónde se encuentra el archivo
                string fileName = direction;
                //inicializo la variable xlApp (referente a la aplicación)
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                //Muestra la aplicación Excel si está en true
                xlApp.Visible = false;
                //Abrimos el libro a leer (documento excel)
                xlLibro = xlApp.Workbooks.Open(fileName,
                Missing.Value,
                Missing.Value,
                Missing.Value,
                Missing.Value,
                Missing.Value,
                Missing.Value,
                Missing.Value,
                Missing.Value,
                Missing.Value,
                Missing.Value,
                Missing.Value,
                Missing.Value,
                Missing.Value,
                Missing.Value);

                string producto = "";
                string descripcion = "";
                string unidadAlmacen = "";
                string existencia = "";
                string preciopublico = "";
                string preciominimo = "";
                ListViewItem lvi;
                // Asignamos las hojas
                xlHojas = xlLibro.Sheets;
                //Asignamos la hoja con la que queremos trabajar: 
                xlHoja1 = (Microsoft.Office.Interop.Excel._Worksheet)xlHojas["Hoja1"];
                int j = 2;
                // recorremos las celdas que queremos y sacamos los datos 

                for (int i = 0; i < 9000; i++)
                {
                    producto = (string)xlHoja1.get_Range("A" + j, Missing.Value).Text;
                    producto = producto.Trim();

                    if (producto == "") break;


                    descripcion = (string)xlHoja1.get_Range("B" + j, Missing.Value).Text;
                    descripcion = descripcion.Trim();

                    unidadAlmacen = (string)xlHoja1.get_Range("C" + j, Missing.Value).Text;
                    unidadAlmacen = unidadAlmacen.Trim();

                    existencia = (string)xlHoja1.get_Range("D" + j, Missing.Value).Text;
                    existencia = existencia.Trim();

                    preciopublico = (string)xlHoja1.get_Range("E" + j, Missing.Value).Text;
                    preciopublico = preciopublico.Trim();

                    preciominimo = (string)xlHoja1.get_Range("F" + j, Missing.Value).Text;
                    preciominimo = preciominimo.Trim();

                    //string FECHA = DateTime.Now.ToString("dd/MM/yyyy");
                    //string FECHACOD = DateTime.Now.ToString("yyyyMMdd");

                    lvi = new ListViewItem(producto);
                    lvi.SubItems.Add(descripcion);
                    lvi.SubItems.Add(unidadAlmacen);
                    lvi.SubItems.Add(existencia);
                    lvi.SubItems.Add(preciopublico);
                    lvi.SubItems.Add(preciominimo);
                    Lv.Items.Add(lvi);

                    j++;
                }

                xlApp.Quit();
                System.Diagnostics.Process[] myProcesses;
                myProcesses = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                foreach (System.Diagnostics.Process instance in myProcesses)
                {
                    instance.CloseMainWindow();
                    instance.Kill();
                    instance.Close();
                }
                label1.Text = Lv.Items.Count.ToString() + " registros encontrados...";
                Lv.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "\n");

            }
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            int num = 0;
            int num2 = 0;
            if (this.Lv.Items.Count == 0)
            {
                MessageBox.Show("No existe ning\x00fan elemento en la lista!\n Es necesario que existan elementos en la lista ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    string query = "";
                    bool flag = false;
                    for (int i = 0; i < this.Lv.Items.Count; i++)
                    {
                        conectorSql sql = new conectorSql();
                        query = "Select nombre from productos where nombre ='" + this.Lv.Items[i].SubItems[1].Text.ToString().Trim() + "'";
                        flag = sql.ExisteRegistro(query);
                        sql.CierraConexion();
                        string cvproducto = this.Lv.Items[i].Text.ToString().Trim();
                        string text = this.Lv.Items[i].SubItems[1].Text;
                        string unidad = this.Lv.Items[i].SubItems[2].Text;
                        string marca = this.Lv.Items[i].SubItems[3].Text;
                        string categoria = this.Lv.Items[i].SubItems[4].Text;
                        string minimo = this.Lv.Items[i].SubItems[5].Text;
                        string maximo = this.Lv.Items[i].SubItems[6].Text;
                        string cantidad = this.Lv.Items[i].SubItems[7].Text;
                        string preciopublico = this.Lv.Items[i].SubItems[8].Text;
                        string precioproveedor = this.Lv.Items[i].SubItems[9].Text;
                        string causaIVA = this.Lv.Items[i].SubItems[10].Text;
                        if (!flag)
                        {
                            this.guardarProductos(cvproducto, text, unidad, cantidad, marca, categoria, minimo, maximo, causaIVA);
                            this.guardarListaPrecios(cvproducto, preciopublico, precioproveedor);
                            num2++;
                        }
                        else
                        {
                            query = "Select cvproducto from productos where cvproducto ='" + cvproducto + "'";
                            flag = sql.ExisteRegistro(query);
                            sql.CierraConexion();
                            if (flag)
                            {
                                this.actualizaProductos(cvproducto, text, unidad, cantidad, marca, categoria, minimo, maximo, causaIVA);
                                this.actualizaListaPrecios(cvproducto, preciopublico, precioproveedor);
                                num++;
                            }
                            else
                            {
                                this.guardarProductos(cvproducto, text, unidad, cantidad, marca, categoria, minimo, maximo, causaIVA);
                                this.guardarListaPrecios(cvproducto, preciopublico, precioproveedor);
                                num2++;
                            }
                        }
                    }
                    this.label3.Text = this.Lv.Items.Count.ToString() + " registros no insertados...";
                    this.label4.Text = num2.ToString() + " registros nuevos insertados...";
                    this.label6.Text = num.ToString() + " registros actualizados...";
                    this.Cursor = Cursors.Default;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception + "\n");
                }
            }

        }

        public static int VerificarChecks(ListView Lv)
        {
            int contador = 0;

            for (int i = 0; i < Lv.Items.Count; i++)
            {
                if (Lv.Items[i].Checked == true)
                {
                    contador++;
                }
            }

            return contador;
        }

        public void guardarProductos(string cvproducto, string nombre, string unidad, string cantidad, string marca, string categoria, string minimo, string maximo, string causaIVA)
        {
            conectorSql sql = new conectorSql();
            string ejecuta = "Delete from productos where cvproducto='" + cvproducto + "'";
            sql.Excute(ejecuta);
            sql.CierraConexion();
            ejecuta = "Insert into productos(cvproducto, nombre, descripcion, categoria, unidad";
            ejecuta = (((((ejecuta + ", cantidad, minimo, maximo, causaiva, marca, codbarras, ubicacion, fechaModifica, fcodmodifica, emitio, causaAdicional) values(") + "'" + cvproducto + "'") + ",'" + nombre + "'") + ",'" + nombre + "'") + ",'" + categoria + "'") + ",'" + unidad + "'";
            ejecuta = (((((((string.Concat(new object[] { ejecuta, ",'", float.Parse(cantidad), "'" }) + ",'" + minimo + "'") + ",'" + maximo + "'") + ",'" + causaIVA + "'") + ",'" + marca + "'") + ",'0'" + ",''") + ",'" + DateTime.Now.ToString("dd/MM/yyyy") + "'") + ",'" + DateTime.Now.ToString("yyyyMMdd") + "'") + ",'BILLLINE'" + ",'NO')";
            sql.Excute(ejecuta);
            sql.CierraConexion();
        }




        public void actualizaProductos(string clave, string nombre, string unidad, string cantidad, string marca, string categoria, string minimo, string maximo, string CausaIVA)
        {
            conectorSql sql = new conectorSql();
            string ejecuta = "Update Productos set ";
            ejecuta = ejecuta + "unidad='" + unidad.ToUpper() + "'";
            ejecuta = ((((((string.Concat(new object[] { ejecuta, ",cantidad='", float.Parse(cantidad), "'" }) + ",nombre='" + nombre.ToUpper() + "'") + ",marca='" + marca.ToUpper() + "'") + ",categoria='" + categoria.ToUpper() + "'") + ",minimo='" + minimo + "'") + ",maximo='" + maximo + "'") + ",causaiva='" + CausaIVA + "'") + "where cvproducto='" + clave + "'";
            sql.Excute(ejecuta);
            sql.CierraConexion();
        }



        public void guardarListaPrecios(string cvproducto, string preciopublico, string precioproveedor)
        {
            decimal num = decimal.Parse(preciopublico);
            decimal num2 = decimal.Parse(precioproveedor);
            decimal num3 = num - num2;
            conectorSql sql = new conectorSql();
            string ejecuta = "Delete from ListaPrecios where cvproducto='" + cvproducto + "'";
            sql.Excute(ejecuta);
            sql.CierraConexion();
            ejecuta = "Insert into ListaPrecios(cvproducto, distribuidor, publico1, porciento1, ganancia1 ";
            ejecuta = ((((((((ejecuta + ", publico2, porciento2, ganancia2, publico3, porciento3, ganancia3) values(") + "'" + cvproducto + "'") + ",'" + precioproveedor + "'") + ",'" + preciopublico + "'") + ",'0.0'") + ",'" + num3.ToString() + "'") + ",'0.0'" + ",'0.0'") + ",'0.0'" + ",'0.0'") + ",'0.0'" + ",'0.0')";
            sql.Excute(ejecuta);
            sql.CierraConexion();
        }








        public void actualizaListaPrecios(string cvproducto, string preciopublico, string precioproveedor)
        {
            decimal num = decimal.Parse(preciopublico);
            decimal num2 = decimal.Parse(precioproveedor);
            decimal num3 = num - num2;
            conectorSql sql = new conectorSql();
            string ejecuta = "Update ListaPrecios set ";
            ejecuta = (((ejecuta + "publico1='" + num.ToString() + "'") + ",distribuidor='" + num2.ToString() + "'") + ",ganancia1='" + num3.ToString() + "'") + "where cvproducto ='" + cvproducto + "'";
            sql.Excute(ejecuta);
            sql.CierraConexion();
        }

 

 
  

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("\x00bfDesea eliminar todos los productos ?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
            {
                conectorSql sql = new conectorSql();
                string ejecuta = "Delete from productos ";
                sql.Excute(ejecuta);
                sql.CierraConexion();
                ejecuta = "Delete from ListaPrecios";
                sql.Excute(ejecuta);
                sql.CierraConexion();
                MessageBox.Show("Se eliminaron correctamente todos los productos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void LeerArchivoExcel_Load(object sender, EventArgs e)
        {

        }
    }
}
