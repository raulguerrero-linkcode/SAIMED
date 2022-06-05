using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace SHOPCONTROL.JOSEFORMS
{
    public partial class INVENTARIO : Form
    {
        public INVENTARIO()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            decimal total = 0;

            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Num. Recibo", 55).Tag = "NUMBER";

            Lv.Columns.Add("Nombre cliente", 150).Tag = "STRING";
            Lv.Columns.Add("Categoria", 120).Tag = "STRING";
            Lv.Columns.Add("Fecha", 95).Tag = "STRING";

            Lv.Columns.Add("Sub total", 80).Tag = "STRING";
            Lv.Columns.Add("Descripción", 170).Tag = "STRING";
            Lv.Columns.Add("Clave producto", 80).Tag = "STRING";


            conectorSql conecta = new conectorSql();
            string Query = "Select DetallesRecibos.numrecibo as clave, recibos.nombrerecibo as Nombrecliente,";
            Query = Query + " DetallesRecibos.fechacod,DetallesRecibos.cvproducto as cvpr, Cat_Categorias.descripcion as nomcat,";
            Query = Query + " DetallesRecibos.fecha as date, DetallesRecibos.precio as tol, DetallesRecibos.descripcion as nomdes";
            
            Query = Query + " from productos ";
            Query = Query + " inner join DetallesRecibos on DetallesRecibos.cvproducto=Productos.cvproducto ";
            Query = Query + " inner join Recibos on Recibos.numrecibo=DetallesRecibos.numrecibo ";
            Query = Query + " inner join Cat_Categorias on Cat_Categorias.idcategoria=cast(productos.categoria as int) ";

            Query = Query + " where DetallesRecibos.cvproducto <>''";
            
            if (checkBox1.Checked == false) Query = Query + " and DetallesRecibos.fechacod between '" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.ToString("yyyyMMdd") + "'";
            if (textBox2.Text != "") Query = Query + " and DetallesRecibos.numrecibo='" + textBox2.Text + "'";
            if (textBox1.Text != "") Query = Query + " and Productos.nombre like '%" + textBox1.Text + "%'";

            if (comboBox1.Text != "") Query = Query + " and  categoria='" + comboBox1.SelectedValue.ToString() + "'";

            

            Query = Query + " order by DetallesRecibos.fecha asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["clave"].ToString());
                lvi.SubItems.Add(leer["NombreCliente"].ToString());
                lvi.SubItems.Add(leer["nomcat"].ToString());
                lvi.SubItems.Add(leer["date"].ToString());
                decimal valor = decimal.Parse(leer["tol"].ToString());
                total = total + valor;
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                
                lvi.SubItems.Add(leer["nomdes"].ToString());
                lvi.SubItems.Add(leer["cvpr"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label1.Text = total.ToString("#,#.00", CultureInfo.InvariantCulture);
            label15.Text = Lv.Items.Count.ToString() + " Recibos ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("¿Desea exportar la información a excel?", "Exportación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;

          
            string idcategoria = "";
            if (comboBox1.Text != "") idcategoria = comboBox1.SelectedValue.ToString();


            ReportesNKB.ReporteRecibos(dateTimePicker1.Value.ToString("yyyyMMdd"), dateTimePicker2.Value.ToString("yyyyMMdd"),idcategoria, textBox2.Text,textBox1.Text, checkBox1.Checked);
            
        }

        private void INVENTARIO_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            //if (checkBox1.Checked == true) timer1.Enabled = true;

            combos.Categoriaproducto(comboBox1);
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender,e);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Reportespdf reporte = new Reportespdf();
            string CADENA = "";

            string idcategoria = "";
            if (comboBox1.Text != "") idcategoria = comboBox1.SelectedValue.ToString();

            CADENA = reporte.ReporteDeProductosjo(dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString(), textBox2.Text, idcategoria, textBox1.Text);

            try
            {
                System.Diagnostics.Process.Start(CADENA);
            }
            catch (Exception er)
            {
                MessageBox.Show("" + er, "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}
