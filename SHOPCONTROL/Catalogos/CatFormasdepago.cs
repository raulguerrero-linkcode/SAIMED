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
    public partial class CatFormasdepago : Form
    {

        public string cvbanco = "";
        public string nombre = "";
        public string cuenta = "";
        public string interbancaria = "";
        public string sucursal = "";
        public string NOMBREPERSONA= "";
        public CatFormasdepago()
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
            string Query = "Select * from formadepagos where cvforma='" + clave + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox1.Text = leer["cvforma"].ToString();
                textBox1.Enabled = false;
                textBox2.Text = leer["nombre"].ToString();
             
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
            textBox2.Text = "";
         
        }
        public void Guardar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Insert into formadepago(cvforma,nombre) values(";
            Query = Query + "'" + cvbanco + "'";
            Query = Query + ",'" + nombre + "')";
            conecta.Excute(Query);
        }

        public void Actualizar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Update formadepago set ";
            Query = Query + "nombre='" + nombre + "'";
            Query = Query + "where cvempresa='" + cvbanco + "'";
            conecta.Excute(Query);
        }
        public void CargarInfo()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 60).Tag = "STRING";
            Lv.Columns.Add("Forma de Pago", 220).Tag = "STRING";
         

            conectorSql conecta = new conectorSql();
            string Query = "Select * from formadepago where cvforma<>''";
            if (textBox11.Text != "") Query = Query + " and nombre like '%" + textBox11.Text + "%'";
            Query = Query + " order by nombre asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["cvforma"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label15.Text = Lv.Items.Count.ToString() + " Formas de pago";
        }
        public void RecolectaR()
        {
            cvbanco = textBox1.Text;
            nombre= textBox2.Text;
        
        }
        public bool validacion()
        {
            if (cvbanco == "")
            {
                MessageBox.Show("Ingrese la clave que desea asignar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (nombre == "")
            {
                MessageBox.Show("Ingrese el nombre ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Query = "Select count(*) as total from formadepago where metodopago='" + Lv.Items[i].SubItems[2].Text.ToUpper() + "'";
                    SqlDataReader leer = conecta.RecordInfo(Query);
                    while (leer.Read())
                    {
                        total=int.Parse(leer["total"].ToString());
                    }
                    conecta.CierraConexion();

                    if (total == 0)
                    {
                        Query = "Delete from formadepago where cvforma='" + Lv.Items[i].Text + "'";
                        conecta.Excute(Query);
                    }
                    else
                    {
                        MessageBox.Show("No es posible eliminar, ya que existen " + total.ToString() + " pedidos con este numero de cuenta " + Lv.Items[i].SubItems[2].Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            ReportesNKB.RBusquedaBancos(textBox11.Text);
        }
    }
}