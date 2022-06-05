using System;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace SHOPCONTROL.HistorialClinica
{
    public partial class Catservicios : Form
    {
        public string cvservicio = "";
        public string nombre = "";

        public Catservicios()
        {
            InitializeComponent();
        }

        public void Recolecta()
        {
            cvservicio = textBox2.Text.Trim();
            nombre = textBox3.Text.Trim();

        }
        public bool Valida()
        {
            if (cvservicio == "")
            {
                MessageBox.Show("Ingrese clave para el servicio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (nombre== "")
            {
                MessageBox.Show("Ingrese el nombre del servicio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public bool Guardar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Insert into CatServicios(cvservicio,nombre) values('" + cvservicio + "','" + nombre + "')";
           bool registrado= conecta.Excute(Query);
            conecta.CierraConexion();
            return registrado;
        }

        public bool Actualizar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Update CatServicios set nombre='" + nombre + "' where cvservicio='" + cvservicio + "'";
            bool registrado = conecta.Excute(Query);
            conecta.CierraConexion();
            return registrado;
        }

        public void Elimina()
        {
            conectorSql conecta = new conectorSql();
            string Query = "delete CatServicios where cvservicio='" + cvservicio + "'";
            conecta.Excute(Query);
            conecta.CierraConexion();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Catservicios_Load(object sender, EventArgs e)
        {

        }

        public void Buscarinfo()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 40).Tag = "NUMBER";
            Lv.Columns.Add("Nombre", 600).Tag = "STRING";

            conectorSql conecta = new conectorSql();
            string query = "Select * from catservicios  where cvservicio<>''";
            if (textBox1.Text != "") query = query + " and nombre='" + textBox1.Text.Trim() + "'";
            query = query + " order by nombre asc";
            SqlDataReader leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["cvservicio"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Recolecta();
            if (Valida() == false) return;
            if (textBox2.Enabled == true)
            {
                if (Guardar() == true)
                {
                    MessageBox.Show("Se registro correctamente el servicio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
            }
            else
            {
                if (Actualizar() == true)
                {
                    MessageBox.Show("Se actualizo correctamente el servicio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
            }
            Buscarinfo();
            panel1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Buscarinfo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = true;

            textBox2.Text = "";
            textBox3.Text = "";
            panel1.Visible = true;
            textBox2.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string query = "";
            int contador = 0;

            DialogResult reply = MessageBox.Show("¿Desea Eliminar los servicios seleccionados?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;


            for (int i = 0; i < Lv.Items.Count; i++)
            {
                if (Lv.Items[i].Checked == true)
                {
                    query = "Delete from catservicios where cvservicio='" + Lv.Items[i].Text + "'";
                    bool eliminado=conecta.Excute(query);
                    if (eliminado == true) contador++;
                }
            }

            MessageBox.Show("Se eliminaron " + contador.ToString() + "  servicios", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Buscarinfo();
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

            
            string clave = Lv.Items[index].Text;
            string nombre= Lv.Items[index].SubItems[1].Text;

            textBox2.Text = clave;
            textBox3.Text = nombre;
            textBox2.Enabled = false;

            panel1.Visible = true;


        }
    }
}
