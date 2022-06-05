using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace SHOPCONTROL
{
    public partial class BrapidaCliente : Form
    {
        public BrapidaCliente()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            string cvcliente = "";
            string cvclienteotro = "";
            Lv.Columns.Add("Clave", 80);
            Lv.Columns.Add("Razon social", 250);
            Lv.Columns.Add("Telefono", 100);
            Lv.Columns.Add("Correo", 100);
            Lv.Columns.Add("Direccion", 250);
            Lv.Columns.Add("Nombre comercial", 100);
            Lv.Columns.Add("¿Factura?", 100);
            int cantColumnas = 6;
            int contador = 1;
            Lv.BeginUpdate();
            conectorSql conecta = new conectorSql();
            string Query = "Select * from Clientes where cvcliente<>'' ";
            if (textBox2.Text != "") Query = Query + " and nombre like '%" + textBox2.Text + "%'";
            if (textBox1.Text != "") Query = Query + " and empresa like '%" + textBox1.Text + "%'";
            Query = Query + " order by nombre asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                cvcliente = leer["cvcliente"].ToString();
                ListViewItem lvi = new ListViewItem(cvcliente);
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["telefono"].ToString());
                lvi.SubItems.Add(leer["email"].ToString());
                lvi.SubItems.Add(leer["direccion"].ToString());
                lvi.SubItems.Add(leer["empresa"].ToString());
                lvi.SubItems.Add(leer["factura"].ToString());
                Lv.Items.Add(lvi);

                lvi.UseItemStyleForSubItems = false;
                int resultado = contador % 2;
                if (resultado==0)
                {

                    lvi.BackColor = Color.FromArgb(217, 223, 251);
                    for (int i = 1; i < cantColumnas; i++)
                    {
                        lvi.SubItems[i].BackColor = Color.FromArgb(217, 223, 251);
                    }
                }
                else
                {
                    lvi.BackColor = Color.FromArgb(243, 243, 243);
                    for (int i = 1; i < cantColumnas; i++)
                    {
                        lvi.SubItems[i].BackColor = Color.FromArgb(243, 243, 243);
                    }
                }
                cvclienteotro = cvcliente;
                contador++;

            }
            conecta.CierraConexion();
            Lv.EndUpdate();
            label1.Text = Lv.Items.Count.ToString() + " Registros "; 
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
            Modremision.CVCLIENTE= Lv.Items[index].Text;
            valoresg.VIENEBUSQUEDAPEDIDO = "SI";
            valoresg.VIENEBUSQUEDARECIBO = "SI";
            this.Dispose();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);
        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BrapidaCliente_Load(object sender, EventArgs e)
        {

        }
    }
}
