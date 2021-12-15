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
    public partial class CatBancos : Form
    {

        public string cvbanco = "";
        public string nombre = "";
        public string cuenta = "";
        public string interbancaria = "";
        public string sucursal = "";
        public string NOMBREPERSONA= "";
        public CatBancos()
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
            string Query = "Select * from bancos where cvbanco='" + clave + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox1.Text = leer["cvbanco"].ToString();
                textBox1.Enabled = false;
                textBox2.Text = leer["nombre"].ToString();
                textBox10.Text = leer["cuenta"].ToString();
                textBox3.Text = leer["interbancaria"].ToString();
                textBox5.Text = leer["sucursal"].ToString();
                textBox4.Text = leer["nombredeposito"].ToString();
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
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
           
        }
        public void Guardar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Insert into bancos(cvbanco,nombre,cuenta,interbancaria,sucursal,nombredeposito) values(";
            Query = Query + "'" + cvbanco + "'";
            Query = Query + ",'" + nombre + "'";
            Query = Query + ",'" + cuenta + "'";
            Query = Query + ",'" + interbancaria + "'";
            Query = Query + ",'" + sucursal + "'";
            Query = Query + ",'" + NOMBREPERSONA + "')";
            conecta.Excute(Query);
        }

        public void Actualizar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Update Bancos set ";
            Query = Query + "nombre='" + nombre + "'";
            Query = Query + ",cuenta='" + cuenta + "'";
            Query = Query + ",interbancaria='" + interbancaria + "'";
            Query = Query + ",sucursal='" + sucursal + "'";
            Query = Query + ",nombredeposito='" + NOMBREPERSONA + "'";
            Query = Query + "where cvbanco='" + cvbanco + "'";
            conecta.Excute(Query);
        }
        public void CargarInfo()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 60).Tag = "STRING";
            Lv.Columns.Add("Banco", 120).Tag = "STRING";
            Lv.Columns.Add("Cuenta", 100).Tag = "STRING";
            Lv.Columns.Add("Sucursal", 90).Tag = "STRING";
            Lv.Columns.Add("Clabe", 150).Tag = "STRING";
            Lv.Columns.Add("Depositar a ", 100).Tag = "STRING";

            conectorSql conecta = new conectorSql();
            string Query = "Select * from Bancos where cvbanco<>''";
            if (textBox11.Text != "") Query = Query + " and nombre like '%" + textBox11.Text + "%'";
            Query = Query + " order by cvbanco asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["cvbanco"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["cuenta"].ToString());
                lvi.SubItems.Add(leer["sucursal"].ToString());
                lvi.SubItems.Add(leer["interbancaria"].ToString());
                lvi.SubItems.Add(leer["nombredeposito"].ToString());             
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label15.Text = Lv.Items.Count.ToString() + " Cuentas de Banco ";
        }
        public void RecolectaR()
        {
            cvbanco = textBox1.Text;
            nombre= textBox2.Text;
            cuenta = textBox10.Text;
            interbancaria= textBox3.Text;
            NOMBREPERSONA= textBox4.Text;
            sucursal = textBox5.Text;
        }
        public bool validacion()
        {
            if (cvbanco == "")
            {
                MessageBox.Show("Ingrese la clave que desea asignar al banco", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (nombre == "")
            {
                MessageBox.Show("Ingrese el nombre del banco", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cuenta == "")
            {
                MessageBox.Show("Ingrese el numero de cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //if (interbancaria == "")
            //{
            //    MessageBox.Show("Ingrese la clabe interbancaria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}

            if (NOMBREPERSONA == "")
            {
                MessageBox.Show("Ingrese el nombre de persona u organización a quien se realiza el deposito", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Query = "Select count(*) as total from pedidos where numcuenta='" + Lv.Items[i].SubItems[2].Text + "'";
                    SqlDataReader leer = conecta.RecordInfo(Query);
                    while (leer.Read())
                    {
                        total=int.Parse(leer["total"].ToString());
                    }
                    conecta.CierraConexion();

                    if (total == 0)
                    {
                        Query = "Delete from bancos where cvbanco='" + Lv.Items[i].Text + "'";
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