using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
namespace SHOPCONTROL
{
    public partial class BrapidaProducto : Form
    {
        public BrapidaProducto()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BuscarPermiso();
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 60);
            Lv.Columns.Add("Nombre", 320);
            Lv.Columns.Add("Marca", 80);
            Lv.Columns.Add("Categoría", 80);
            Lv.Columns.Add("Unidad", 60);
            Lv.Columns.Add("Existencia", 50);
            if (VERDISTRIBUIDOR==true) Lv.Columns.Add("Distribuidor", 90);
            Lv.Columns.Add("Precio 1", 90);
            Lv.Columns.Add("Causa IVA", 70);

            int cantColumnas = 8;
            if (VERDISTRIBUIDOR == true) cantColumnas = 9;
            string otramarca = "";
            string marca = "";
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            string Query = "Select cvproducto,marca,nombre,categoria,unidad";
            Query = Query + ",cantidad,causaiva,Cat_Categorias.descripcion as nomcategoria ";
            Query = Query + " from productos  "; ; 
            Query = Query + " inner join Cat_Categorias on Cat_Categorias.idcategoria=productos.categoria";
            Query = Query + " where cvproducto<>'' ";
            if (textBox2.Text != "") Query = Query + " and nombre like '%" + textBox2.Text + "%'";
            Query = Query + " order by nombre asc, Cat_Categorias.descripcion asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            Lv.BeginUpdate();
            while (leer.Read())
            {
                marca = leer["marca"].ToString();
                string clave = leer["cvproducto"].ToString();
                ListViewItem lvi = new ListViewItem(clave);
                lvi.SubItems.Add(leer["nombre"].ToString());

                lvi.SubItems.Add(marca);
                lvi.SubItems.Add(leer["nomcategoria"].ToString());
                lvi.SubItems.Add(leer["unidad"].ToString());
                lvi.SubItems.Add(leer["cantidad"].ToString());

                string consulta = "Select * from ListaPrecios where cvproducto='" + clave + "'";
                SqlDataReader leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    decimal Distribuidor = decimal.Parse(leer2["distribuidor"].ToString());
                    decimal publico1 = decimal.Parse(leer2["publico1"].ToString());

                    if (VERDISTRIBUIDOR == true) lvi.SubItems.Add("$ " + Distribuidor.ToString("##.00", CultureInfo.InvariantCulture));
                    lvi.SubItems.Add("$ " + publico1.ToString("##.00", CultureInfo.InvariantCulture));
                }
                conecta2.CierraConexion();
                lvi.SubItems.Add(leer["causaiva"].ToString());
                Lv.Items.Add(lvi);

                //lvi.UseItemStyleForSubItems = false;
                //if (marca != otramarca)
                //{

                //    lvi.BackColor = Color.FromArgb(217, 223, 251);
                //    for (int i = 1; i < cantColumnas; i++)
                //    {
                //        lvi.SubItems[i].BackColor=Color.FromArgb(217, 223, 251);
                //    }
                //}
                //else
                //{
                //    lvi.BackColor = Color.FromArgb(243, 243, 243);
                //    for (int i = 1; i < cantColumnas; i++)
                //    {
                //        lvi.SubItems[i].BackColor = Color.FromArgb(243, 243, 243);
                //    }
                //}
                //otramarca = marca;
            }
            conecta.CierraConexion();
            Lv.EndUpdate();
            label1.Text = Lv.Items.Count.ToString() + " Productos / Servicios "; 

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
            Modremision.CVPRODUCTO= Lv.Items[index].Text;
            this.Dispose();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);
        }

        private void BrapidaProducto_Load(object sender, EventArgs e)
        {
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);
        }
        public bool VERDISTRIBUIDOR = false;
        public void BuscarPermiso()
        {
            VERDISTRIBUIDOR = false;
            conectorSql conecta = new conectorSql();
            string Query = "Select ocultardistribuidor from parametros where ocultardistribuidor<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string valor = leer["ocultardistribuidor"].ToString();
                if (valor == "SI") VERDISTRIBUIDOR = false;
                else VERDISTRIBUIDOR = true;
            }
            conecta.CierraConexion();
        }
    }
}
