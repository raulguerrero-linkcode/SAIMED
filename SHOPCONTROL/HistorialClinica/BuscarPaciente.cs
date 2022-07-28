using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SHOPCONTROL.HistorialClinica
{
    public partial class BuscarPaciente : Form
    {
        public BuscarPaciente()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Lv.Items.Clear();
            conectorSql conecta = new conectorSql();
            string Query = "SELECT NOMBRE,APATERNO,AMATERNO,EDAD,TELEFONO,STATUS, clave,expdental,expgineco,expoftamolgo FROM  Pacientes WHERE clave<>''";
            if (textBox1.Text.Trim()!="") Query=Query + "  and  APATERNO LIKE '%" + textBox1.Text + "%'";
            if (textBox3.Text.Trim() != "") Query = Query + " and  AMATERNO LIKE '%" + textBox3.Text + "%'";
            if (textBox2.Text.Trim() != "") Query = Query + "  and NOMBRE LIKE '%" + textBox2.Text + "%'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            string valor = "";
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["clave"].ToString());
                valor = leer["NOMBRE"].ToString() + " " + leer["APATERNO"].ToString() + " " + leer["AMATERNO"].ToString();
                lvi.SubItems.Add(valor);
                lvi.SubItems.Add(leer["EDAD"].ToString());

                if (valoresg.USUARIOSIS.Equals("ROOT"))
                {
                    lvi.SubItems.Add(leer["TELEFONO"].ToString());
                }
                else
                {
                    lvi.SubItems.Add("Confidencial");
                }
                
                lvi.SubItems.Add(status(leer["STATUS"].ToString()));
                lvi.SubItems.Add(leer["expdental"].ToString());
                lvi.SubItems.Add(leer["expgineco"].ToString());
                lvi.SubItems.Add(leer["expoftamolgo"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label4.Text = Lv.Items.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public string status(string valor) {
            if (valor.Equals("1"))
                return "ACTIVO";
            else
                return "INACTIVO";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void Lv_DoubleClick(object sender, EventArgs e)
        {
            if (Lv.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = Lv.SelectedIndices;
                foreach (int item in seleccion)
                {
                   
                    valoresg.CLAVEPAC= Lv.Items[item].Text;
                    valoresg.BNUMEXPDENTAL = Lv.Items[item].SubItems[5].Text;
                    valoresg.BNUMEXPGINECO = Lv.Items[item].SubItems[6].Text;
                    valoresg.BNUMEXPOFTAMO = Lv.Items[item].SubItems[7].Text;
                    this.Dispose();
                }
            }
        }

        private void BuscarPaciente_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(sender, e);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(sender, e);

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(sender, e);

        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}