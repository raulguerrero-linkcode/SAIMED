using System;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace SHOPCONTROL
{
    public partial class CatVendedores : Form
    {

        public string cvconcepto = "";
        public string nombre = "";
        public string porcentaje= "";
        public string comision = "";

        public CatVendedores()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            Limpiar();
            panel1.Visible = true;
            panel3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = true;
        }
        public void BuscarBancoInfo(string clave)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from vendedores where cvvendedor='" + clave + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox1.Text = leer["cvvendedor"].ToString();
                textBox1.Enabled = false;

                string valor= leer["comision"].ToString();
                if (valor == "SI") radioButton1.Checked = true;
                else radioButton2.Checked = true;

                textBox2.Text = leer["nombre"].ToString();
                textBox10.Text = leer["porcentaje"].ToString();

            }
            conecta.CierraConexion();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            RecolectaR();
            if (validacion() == true)
            {
                if (textBox1.Enabled == true)
                {
                    Guardar();
                    textBox11.Text = nombre;
                    Limpiar();
                    MessageBox.Show("Se guardo correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarInfo();
                }
                else
                {
                    Actualizar();
                    textBox11.Text = nombre;
                    Limpiar();
                    MessageBox.Show("Se actualizo correctamente la información", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarInfo();
                }
            }
        }
        public void Limpiar()
        {
            panel1.Visible = false;
            panel3.Visible = true;

            textBox1.Text = "";
            textBox10.Text = "";
            textBox2.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            panel2.Visible = false;
           
        }
        public void Guardar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Insert into vendedores(cvvendedor,nombre,comision,porcentaje) values(";
            Query = Query + "'" + cvconcepto + "'";
            Query = Query + ",'" + nombre + "'";
            Query = Query + ",'" + comision + "'";
            Query = Query + ",'" + porcentaje + "')";
            conecta.Excute(Query);
        }

        public void Actualizar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Update vendedores set ";
            Query = Query + "nombre='" + nombre + "'";
            Query = Query + ",comision='" + comision + "'";
            Query = Query + ",porcentaje='" + porcentaje + "'";
            Query = Query + "where cvvendedor='" + cvconcepto+ "'";
            conecta.Excute(Query);
        }
        public void CargarInfo()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 60).Tag = "NUMBER";
            Lv.Columns.Add("Nombre del Vendedor", 250).Tag = "STRING";
            Lv.Columns.Add("Comision", 90).Tag = "STRING";
            Lv.Columns.Add("Porcentaje", 90).Tag = "STRING";       

            conectorSql conecta = new conectorSql();
            string Query = "Select * from vendedores where cvvendedor<>''";
            if (textBox11.Text != "") Query = Query + " and nombre like '%" + textBox11.Text + "%'";
            Query = Query + " order by nombre asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["cvvendedor"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["comision"].ToString());
                lvi.SubItems.Add(leer["porcentaje"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label15.Text = Lv.Items.Count.ToString() + " Vendedores ";
        }
        public void RecolectaR()
        {
            if (radioButton1.Checked == true) comision = "SI";
            else comision = "NO";

            cvconcepto = textBox1.Text;
            nombre= textBox2.Text;
            porcentaje = textBox10.Text;

        }
        public bool validacion()
        {
            if (cvconcepto == "")
            {
                MessageBox.Show("Ingrese la clave que desea asignar al vendedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (nombre == "")
            {
                MessageBox.Show("Ingrese el nombre del vendedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (porcentaje == "")
            {
                MessageBox.Show("Ingrese el porcentaje de comision por venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            


            return true;
        }

        private void CatBancos_Load(object sender, EventArgs e)
        {
            CargarInfo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CargarInfo();
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
            textBox1.Text= Lv.Items[index].Text;
            BuscarBancoInfo(textBox1.Text);
            panel1.Visible = true;
            panel3.Visible = false;
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string Query = "";
            for (int i = 0; i < Lv.Items.Count; i++)
            {
                if (Lv.Items[i].Checked == true)
                {
                   
                    int total = 0;
                    Query = "Select count(*) as total from Pedidos where vendedor='" + Lv.Items[i].SubItems[1].Text + "'";
                    SqlDataReader leer = conecta.RecordInfo(Query);
                    while (leer.Read())
                    {
                        total=int.Parse(leer["total"].ToString());
                    }
                    conecta.CierraConexion();

                    if (total == 0)
                    {
                        Query = "Delete from vendedores where cvvendedor='" + Lv.Items[i].Text + "'";
                        conecta.Excute(Query);
                    }
                    else
                    {
                        MessageBox.Show("No es posible eliminar, ya que existen  pedidos del vendedor: " + Lv.Items[i].SubItems[1].Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            CargarInfo();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("¿Desea exportar la información a excel?", "Exportación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;

            ReportesNKB.RBusquedaVendedores(textBox11.Text);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                panel2.Visible = true;
                textBox10.Focus();
            }
            else
            {
                panel2.Visible = false;
                textBox10.Text = "0";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true) 
            {
                panel2.Visible = false;
                textBox10.Text = "0";
            }
        }
    }
}