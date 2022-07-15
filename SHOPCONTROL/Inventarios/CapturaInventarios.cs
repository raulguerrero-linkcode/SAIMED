using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SHOPCONTROL.Inventarios
{
    public partial class CapturaInventarios : Form
    {
        public CapturaInventarios()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProcesaBusqueda();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Lv_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (Lv.SelectedItems.Count == 1)
            {
                
                Cantidad cantidad = new Cantidad(Lv.SelectedItems[0].SubItems[0].Text, long.Parse(Lv.SelectedItems[0].SubItems[4].Text));
                cantidad.ShowDialog();
                ProcesaBusqueda();
                cantidad.Dispose();
                Lv.SelectedItems.Clear();
            }

            

        }

        private void ProcesaBusqueda()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 90);
            Lv.Columns.Add("Nombre", 350);
            Lv.Columns.Add("Categoría", 90);
            Lv.Columns.Add("Unidad", 70);
            Lv.Columns.Add("Existencia", 60);
            Lv.Visible = true;
            Lv.BeginUpdate();

            string Query = "Select cvproducto,nombre,categoria,unidad,cantidad";
            Query = Query + " from productos";
            Query = Query + " inner join Cat_Categorias on Cat_Categorias.idcategoria=productos.categoria ";
            Query = Query + " inner join Cat_tipos on Cat_tipos.idtipo = Productos.idtipo";                  //AGREGADO POR JOSE 02-12-2019
            Query = Query + " where cvproducto <> ''";


            //Query = Query + "Cat_tipos.descripcion as nomtipo from Productos inner join Cat_tipos on Cat_tipos.idtipo = Productos.idtipo where cvproducto <> ''";
            if (textBox11.Text != "") Query = Query + " and nombre like '%" + textBox11.Text + "%'";
            // if (comboBox2.Text != "") Query = Query + " and  categoria='" + comboBox2.SelectedValue.ToString() + "'";
            // f (comboBox4.Text != "") Query = Query + " and marca='" + comboBox2.Text + "'";
            // if (comboBox7.Text != "") Query = Query + " and unidad='" + comboBox7.Text.Trim() + "'";
            if (textBox23.Text != "") Query = Query + " and cvproducto='" + textBox23.Text.Trim() + "'";
            if (txtQR.Text != "") Query = Query + " and codbarras='" + txtQR.Text.Trim() + "'";

            // if (comboBox8.Text != "") Query = Query + "and Cat_tipos.idtipo = '" + comboBox8.SelectedValue.ToString().Trim() + "'";

            conectorSql conecta = new conectorSql();
            Query = Query + " order by nombre asc, Cat_Categorias.descripcion asc";
            SqlDataReader leer = conecta.RecordInfo(Query);

            if (leer.HasRows)
            {
                while (leer.Read())
                {

                    string clave = leer["cvproducto"].ToString();
                    ListViewItem lvi = new ListViewItem(clave);

                    lvi.SubItems.Add(leer["nombre"].ToString());
                    lvi.SubItems.Add(leer["categoria"].ToString());
                    lvi.SubItems.Add(leer["unidad"].ToString());
                    lvi.SubItems.Add(leer["cantidad"].ToString());
                    Lv.Items.Add(lvi);

                    lvi.UseItemStyleForSubItems = false;
                }

                Lv.EndUpdate();
                
            }
            else
            {

                if (MessageBox.Show("Desea dar de alta un nuevo producto?", "Producto no encontrado", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    Productos productos = new Productos();
                    productos.Show();
                }
                
                

            }
            conecta.CierraConexion();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    }


