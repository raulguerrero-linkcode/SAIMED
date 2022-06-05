using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace SHOPCONTROL
{
    public partial class RequisicionMaterial : Form
    {

        public string CLAVE = "";
        public string NOMBRE = "";
        public string CANTIDAD = "";
        public string JUSTIFICA = "";
        public string FECHACOD = "";
        public string FECHA = "";
        public string EMITE = "";
        public string ESTATUS = "";
        public string TIPO = "";
        public string CVPACIENTE = "";

        public RequisicionMaterial()
        {
            InitializeComponent();
        }

        private void RequisicionMaterial_Load(object sender, EventArgs e)
        {

        }

        public void Recolecta()
        {
            CLAVE = textBox1.Text.Trim();
            NOMBRE = textBox2.Text.Trim();
            CANTIDAD= textBox3.Text.Trim();
            if (CANTIDAD == "") CANTIDAD = "1";
            JUSTIFICA= textBox4.Text.Trim();
            FECHA = DateTime.Now.ToString("dd/MM/yyyy");
            FECHACOD = DateTime.Now.ToString("yyyyMMdd");
            EMITE = valoresg.USUARIOSIS;
            TIPO = comboBox1.Text.Trim();
            if (TIPO == "") TIPO = "NO APLICA";
            ESTATUS = "CAPTURADO";
            CVPACIENTE = textBox5.Text.Trim();
        }

        public bool valida()
        {
            if (CLAVE == "")
            {
                MessageBox.Show("Ingrese una clave para su requisicion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (NOMBRE == "")
            {
                MessageBox.Show("Ingrese una nombre de producto o servicio solicitado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (JUSTIFICA == "")
            {
                MessageBox.Show("Justifique el porque requiere el servicio o producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
                return false;
            }
            return true;

        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Guardar();
            Consultarinfo();
        }
        public void Guardar()
        {
            Recolecta();
            if (valida())
            {
                conectorSql conecta = new conectorSql();
                string query="Insert into Requisicion (clave";
                query=query + ", nombre";
                query=query + ", justifica";
                query=query + ", cantidad";
                query=query + ", fechacod";
                query=query + ", fecha";
                query=query + ", emite";
                query=query + ", estatus";
                query=query + ", respuesta";
                query=query + ", fcodresp";
                query = query + ", fresp)";
                query = query + " values(";
                query = query + "'" + CLAVE +"'" ;
                query = query + "'" + NOMBRE+ "'";
                query = query + "'" + JUSTIFICA + "'";
                query = query + "'" + CANTIDAD+ "'";
                query = query + "'" + FECHACOD + "'";
                query = query + "'" + FECHA+ "'";
                query = query + "'" + EMITE+ "'";
                query = query + "'" + ESTATUS+ "'";
                query = query + "''";
                query = query + "''";
                query = query + "'')";
                conecta.Excute(query);
                conecta.CierraConexion();

                MessageBox.Show("Se guardo correctamente la informacion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Consultarinfo()
        {

            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 60).Tag = "NUMBER";
            Lv.Columns.Add("Nombre", 50).Tag = "STRING";
            Lv.Columns.Add("Cantidad", 80).Tag = "STRING";
            Lv.Columns.Add("Fecha", 400).Tag = "STRING";
            Lv.Columns.Add("Estatus", 80).Tag = "STRING";
            Lv.Columns.Add("Clave Paciente", 80).Tag = "STRING";
            Lv.Columns.Add("Nombre Paciente", 80).Tag = "STRING";
        
            conectorSql conecta = new conectorSql();
            string Query = "Select * from Requisicion where emite='" +valoresg.USUARIOSIS+ "'";
            Query = Query + " order by fechacod desc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["clave"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["cantidad"].ToString());
                lvi.SubItems.Add(leer["Fecha"].ToString());
                lvi.SubItems.Add(leer["estatus"].ToString());
                lvi.SubItems.Add(leer["cvpaciente"].ToString());
                lvi.SubItems.Add(".");
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            CambioDeColoresCelda();
        }


        private void CambioDeColoresCelda()
        {

            int columna = 0;
            columna = 5;

            Lv.BeginUpdate();

            foreach (ListViewItem item in Lv.Items)
            {
                int indice = 0;
                item.UseItemStyleForSubItems = false;

                foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                {
                    if (indice == columna)
                    {
                        if (subitem.Text == "APROBADO") subitem.BackColor = Color.FromArgb(96, 204, 69);
                        if (subitem.Text == "RECHAZADO") subitem.BackColor = Color.FromArgb(255, 192, 192);
                        if (subitem.Text == "CAPTURADO") subitem.BackColor = Color.FromArgb(253, 252, 223);
                        if (subitem.Text == "PENDIENTE") subitem.BackColor = Color.FromArgb(247, 198, 85);
                    }
                    indice++;
                }
            }
            Lv.EndUpdate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Consultarinfo();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Lv.Items.Count; i++)
            {
                Lv.Items[i].Checked = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < Lv.Items.Count; i++)
            {
                Lv.Items[i].Checked = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBox1.Text = "R" + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmm") ;
        }


    }
}
