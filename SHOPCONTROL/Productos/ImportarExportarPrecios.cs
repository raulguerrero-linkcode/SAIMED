using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
namespace SHOPCONTROL
{
    public partial class ImportarExportarPrecios : Form
    {
        public ImportarExportarPrecios()
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

        public string CODIGODEBARRAS = "";
        public string UBICACION = "";
        public string MARCA = "";

        public string FECHAMODIFICA = "";
        public string FCODMODIFICA = "";
        public string EMITE = "";
       
       
  
        private void button3_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 90);
            Lv.Columns.Add("Nombre", 380);
            Lv.Columns.Add("Precio Público 1", 120);
            Lv.Columns.Add("Precio Público 2", 120);
            
           
            Lv.Visible = false;
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            string Query = "Select Productos.cvproducto as clave, Productos.nombre as Nombre, Productos.marca, Productos.categoria, ListaPrecios.publico1 as precioUno, ListaPrecios.publico2 as precioDos from ListaPrecios  inner join Productos on Productos.cvproducto = ListaPrecios.cvproducto where ListaPrecios.cvproducto <>'' ";
            if (textBox11.Text != "") Query = Query + " and Productos.nombre like '%" + textBox11.Text + "%'";
            if (comboBox2.Text != "") Query = Query + " and Productos.categoria ='" + comboBox2.Text + "'";
            if (comboBox4.Text != "") Query = Query + " and Productos.marca ='" + comboBox4.Text + "'";
            Query = Query + " order by Productos.nombre asc, Productos.marca asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string clave = leer["clave"].ToString();
                ListViewItem lvi = new ListViewItem(clave);
                lvi.SubItems.Add(leer["Nombre"].ToString());
                lvi.SubItems.Add(leer["precioUno"].ToString());
                lvi.SubItems.Add(leer["precioDos"].ToString());
          
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label15.Text = Lv.Items.Count.ToString() + " Registros ";

            this.Cursor = Cursors.Default;
            
            Lv.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



        public void actualiza(string cv, string p1, string p2)
        {
            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "update ListaPrecios set";
            Query = Query + " publico1 ='" + p1 + "'";
            Query = Query + ", publico2 ='" + p2 + "'";
       
            Query = Query + " where cvproducto ='" + cv + "'";
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

            Query = Query + ",fechaModifica";
            Query = Query + ",fcodmodifica";
            Query = Query + ",emitio";

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

            Query = Query + ",'" + FECHAMODIFICA + "'";
            Query = Query + ",'" + FCODMODIFICA + "'";
            Query = Query + ",'" + EMITE + "'";

            Query = Query + ",'" + MAXIMO + "')";
            conecta.Excute(Query);   

            Query="Insert into ListaPrecios(cvproducto";
            Query = Query + ",distribuidor";
            Query = Query + ",publico1";
            Query = Query + ",porciento1";
            Query = Query + ",ganancia1";
            Query = Query + ",publico2";
            Query = Query + ",porciento2";
            Query = Query + ",ganancia2";
            Query = Query + ",publico3";
            Query = Query + ",porciento3";
            Query = Query + ",ganancia3)";
            Query = Query + " values(";
            Query = Query + "'" + CLAVE+ "'";
            Query = Query + ",'" + DISTRIBUIDOR + "'";
            Query = Query + ",'" + PRECIO1.ToString()+ "'";
            Query = Query + ",'" + PORCENTAJE1.ToString() +"'";
            Query = Query + ",'" + GANANCIA1.ToString()+ "'";
            Query = Query + ",'" + PRECIO2.ToString() + "'";
            Query = Query + ",'" + PORCENTAJE2.ToString() + "'";
            Query = Query + ",'" + GANANCIA2.ToString() + "'";
            Query = Query + ",'" + PRECIO3.ToString() + "'";
            Query = Query + ",'" + PORCENTAJE3.ToString() + "'";
            Query = Query + ",'" + GANANCIA3.ToString() + "')";
            conecta.Excute(Query);

        }

        public bool ExisteInfo()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from productos where cvproducto='" + CLAVE + "'";
            return conecta.ExisteRegistro(Query);
        }


        string chosen_file = "";
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("Para importar un archivo de existencias de productos es necesario seleccionar el archivo de excel correcto, (exportado anteriormente).\n¿Desea importar la información desde un archivo de excel?", "Exportación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;


            Lv.Items.Clear();

            openFileDialog1.InitialDirectory = "C:";
            openFileDialog1.Title = "Seleccione archivo de excel";
            openFileDialog1.Filter = "Archivos de Excel|*.xlsx";

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                chosen_file = openFileDialog1.FileName;

                label1.Text = "";
                

                label1.Text = chosen_file;
                chosen_file = label1.Text;

                Cursor = Cursors.WaitCursor;
                Lv.Visible = false;

                readFile(chosen_file);

                Cursor = Cursors.Default;
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
                string clave = "";
                string pe1 = "", pe2 = "";
                
                ListViewItem lvi;
                
                // Asignamos las hojas
                xlHojas = xlLibro.Sheets;
                //Asignamos la hoja con la que queremos trabajar: 
                xlHoja1 = (Microsoft.Office.Interop.Excel._Worksheet)xlHojas["Hoja1"];
                int j = 4;
                // recorremos las celdas que queremos y sacamos los datos 

                Lv.Items.Clear();
                Lv.Columns.Clear();

                Lv.Columns.Add("Clave", 90);
                Lv.Columns.Add("Nombre", 380);
                Lv.Columns.Add("Precio público 1", 100);
                Lv.Columns.Add("Precio público 2", 100);
                string query = "";
                bool exitereg = false;

                for (int i = 0; i < 9000; i++)
                {
                    clave = (string)xlHoja1.get_Range("A" + j, Missing.Value).Text;
                    clave = clave.Trim();

                    if (clave == "") break;


                    producto = (string)xlHoja1.get_Range("B" + j, Missing.Value).Text;
                    producto = producto.Trim();

                    pe1 = (string)xlHoja1.get_Range("C" + j, Missing.Value).Text;
                    pe1 = pe1.Trim();

                    pe2 = (string)xlHoja1.get_Range("D" + j, Missing.Value).Text;
                    pe2 = pe2.Trim();

                    conectorSql conecta = new conectorSql();

                    query = "Select cvproducto from productos where cvproducto ='" + clave + "'";
                    exitereg = conecta.ExisteRegistro(query);
                    conecta.CierraConexion();

                    if (exitereg)
                    {
                        actualiza(clave, pe1, pe2);

                        lvi = new ListViewItem(clave);
                        lvi.SubItems.Add(producto);
                        lvi.SubItems.Add(pe1);
                        lvi.SubItems.Add(pe2);
                        Lv.Items.Add(lvi);

                    }

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
                label1.Text = "-";
                label15.Text = Lv.Items.Count.ToString() + " registros actualizados...";
                Lv.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "\n");

            }
        }

     

        private void button10_Click(object sender, EventArgs e)
        {
            if (Lv.Items.Count == 0)
            {
                MessageBox.Show("No existe ningún elemento en la lista!\n Es necesario que existan elementos en la lista para ser importados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[,] Informacion = new string[Lv.Items.Count, 4];
            int contador = 0;
            for (int i = 0; i < Lv.Items.Count; i++)
            {

                Informacion[contador, 0] = Lv.Items[i].Text.ToString();
                Informacion[contador, 1] = Lv.Items[i].SubItems[1].Text.ToString();
                Informacion[contador, 2] = Lv.Items[i].SubItems[2].Text.ToString();
                Informacion[contador, 3] = Lv.Items[i].SubItems[3].Text.ToString();
                contador++;
            }
            this.Cursor = Cursors.WaitCursor;
            ReportesNKB.importaPrecios(Informacion, Lv.Items.Count);
            this.Cursor = Cursors.Default;
        }

        private void SeleccionAll(ListView lv)
        {
            if (lv.Items.Count == 0) return;

            for (int i = 0; i < lv.Items.Count; i++)
            {
                lv.Items[i].Checked = true;
            }

        }

        private void QuitarAll(ListView lv)
        {
            if (lv.Items.Count == 0) return;

            for (int i = 0; i < lv.Items.Count; i++)
            {
                lv.Items[i].Checked = false;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionAll(Lv);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QuitarAll(Lv);
        }

        private void ImportarExportarPrecios_Load(object sender, EventArgs e)
        {
            combos.MarcasProductos(comboBox4);
            combos.Categoriaproducto(comboBox2);
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);
        }

        private void comboBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);

        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);

        }
    }
}

