using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace SHOPCONTROL
{
    public partial class CatConceptospago : Form
    {

        public string cvconcepto = "";
        public string nombre = "";
        public string precio= "";
        public CatConceptospago()
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
            string Query = "Select * from ConceptosPago where cvconcepto='" + clave + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox1.Text = leer["cvconcepto"].ToString();
                textBox1.Enabled = false;
                textBox2.Text = leer["nombre"].ToString();
                textBox10.Text = leer["precio"].ToString();
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
        
           
        }
        public void Guardar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Insert into ConceptosPago(cvconcepto,nombre,precio) values(";
            Query = Query + "'" + cvconcepto + "'";
            Query = Query + ",'" + nombre + "'";
            Query = Query + ",'" + precio + "')";
            conecta.Excute(Query);
        }

        public void Actualizar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Update ConceptosPago set ";
            Query = Query + "nombre='" + nombre + "'";
            Query = Query + ",precio='" + precio + "'";
            Query = Query + "where cvconcepto='" + cvconcepto + "'";
            conecta.Excute(Query);
        }
        public void CargarInfo()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 60).Tag = "NUMBER";
            Lv.Columns.Add("Nombre", 120).Tag = "STRING";
            Lv.Columns.Add("Precio", 100).Tag = "STRING";       

            conectorSql conecta = new conectorSql();
            string Query = "Select * from ConceptosPago where cvconcepto<>''";
            if (textBox11.Text != "") Query = Query + " and nombre like '%" + textBox11.Text + "%'";
            Query = Query + " order by cvconcepto asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["cvconcepto"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["precio"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label15.Text = Lv.Items.Count.ToString() + " Conceptos de Pago ";
        }
        public void RecolectaR()
        {
            cvconcepto = textBox1.Text;
            nombre= textBox2.Text;
            precio = textBox10.Text;
        }
        public bool validacion()
        {
            if (cvconcepto == "")
            {
                MessageBox.Show("Ingrese la clave que desea asignar al concepto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (nombre == "")
            {
                MessageBox.Show("Ingrese el nombre del concepto de pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (precio == "")
            {
                MessageBox.Show("Ingrese el precio para el concepto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //if (interbancaria == "")
            //{
            //    MessageBox.Show("Ingrese la clabe interbancaria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}


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
                    if (Lv.Items[i].Text == "1")
                    {
                        MessageBox.Show("No es posible eliminar el concepto de DESCUENTOS", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (Lv.Items[i].Text == "2")
                    {
                        MessageBox.Show("No es posible eliminar el concepto de HONORARIOS", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (Lv.Items[i].Text == "3")
                    {
                        MessageBox.Show("No es posible eliminar el concepto de ARRENDAMIENTO", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (Lv.Items[i].Text == "4")
                    {
                        MessageBox.Show("No es posible eliminar el concepto de DONATIVO", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    int total = 0;
                    Query = "Select count(*) as total from DetallesPedido where cvproducto='" +  Lv.Items[i].Text + "' and descripcion='" + Lv.Items[i].SubItems[1].Text + "'";
                    SqlDataReader leer = conecta.RecordInfo(Query);
                    while (leer.Read())
                    {
                        total=int.Parse(leer["total"].ToString());
                    }
                    conecta.CierraConexion();

                    if (total == 0)
                    {
                        Query = "Delete from ConceptosPago where cvconcepto='" + Lv.Items[i].Text + "'";
                        conecta.Excute(Query);
                    }
                    else
                    {
                        MessageBox.Show("No es posible eliminar, ya que existen  pedidos con este concepto de pago: " + Lv.Items[i].SubItems[1].Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            ReportesNKB.RBusquedaConceptosPago(textBox11.Text);
        }
    }
}