using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SHOPCONTROL
{
    public partial class TIPOS : Form
    {
        public TIPOS()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode) Buscar();
        }

        public void Buscar()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 60);
            Lv.Columns.Add("Tipo", 90);
            
            string Query = "";
            SqlDataReader leer = null;

            conectorSql conecta = new conectorSql();
            
            Query = "select * from Cat_tipos";
            Query = Query + " where idtipo <> ''";
            
            if (textBox1.Text != "") Query = Query + " and descripcion like '%" + textBox1.Text + "%'";
            Query = Query + "order by idtipo desc";

            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string clave = leer["idtipo"].ToString();
                ListViewItem Lvi = new ListViewItem(clave);

                Lvi.SubItems.Add(leer["descripcion"].ToString());
                Lv.Items.Add(Lvi);
            }
            conecta.CierraConexion();
            
        }

        private void Agregar_Click(object sender, EventArgs e)
        {
            
            panel2.Visible = false;
            panel3.Visible = true;

        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();

            if (MessageBox.Show("Seguro, ¿desea eliminar?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for(int I = 0; I < Lv.Items.Count;I++)
                {
                    string idtipos = Lv.Items[I].Text;
                    if(Lv.Items[I].Checked == true)
                    {
                        string consulta = "delete from Cat_tipos where idtipo ='" + idtipos + "'";
                        conecta.Excute(consulta);
                        conecta.CierraConexion();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string Query = "insert into Cat_tipos(";
            Query = Query + "idtipo";
            Query = Query + ",descripcion)";
            Query = Query + " values(";
            Query = Query + "'" + textBox2.Text + "'";
            Query = Query + ",'" + textBox3.Text + "')";

            conecta.Excute(Query);
            MessageBox.Show("Se guardo correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void TIPOS_Load(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Buscar();
        }
    }
}
