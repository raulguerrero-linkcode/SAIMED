using System;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
namespace SHOPCONTROL
{
    public partial class PagosCreditos : Form
    {
        public PagosCreditos()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            BrapidaCliente brapida = new BrapidaCliente();
            brapida.Show();
        }

        private void PagosCreditos_Load(object sender, EventArgs e)
        {
            combos.EmitioRemisiones(comboBox1);
            combos.ListaClientes(comboBox2);
        }

        private void PagosCreditos_Activated(object sender, EventArgs e)
        {
            if (Modremision.CVCLIENTE != "")
            {
                comboBox2.Text = Modremision.CVCLIENTE;
                Modremision.CVCLIENTE = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BuscarCreditos();
        }

        public void BuscarCreditos()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Remision", 70);
            Lv.Columns.Add("Nombre cliente", 200);
            Lv.Columns.Add("Fecha", 90);
            Lv.Columns.Add("subtotal", 80);
            Lv.Columns.Add("iva", 80);
            Lv.Columns.Add("total", 80);
            Lv.Columns.Add("Estatus", 70);
            Lv.Columns.Add("Descripción", 350);
            Lv.Columns.Add("Año", 0);

            conectorSql conecta = new conectorSql();
            string Query = "Select distinct(numremision), clientes.nombre as Nombrecliente ";
            Query = Query + ",fecha,total,iva,totalgeneral,status,compro,ayo";
            Query = Query + " from Remisiones ";
            Query = Query + " inner join clientes on clientes.cvcliente=remisiones.cvcliente";
            Query = Query + " where numremision<>''";

            if (checkBox1.Checked == false) Query = Query + " and fechacod between '" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.ToString("yyyyMMdd") + "'";
            if (textBox2.Text != "") Query = Query + " and numremision='" + textBox2.Text + "'";
            if (comboBox1.Text != "") Query = Query + " and emitio='" + comboBox1.Text + "'";
            if (comboBox2.Text != "") Query = Query + " and cvcliente='" + comboBox2.Text + "'";
            Query = Query + " and status='CREDITO'";
            Query = Query + " order by numremision asc";

            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["numremision"].ToString());
                lvi.SubItems.Add(leer["NombreCliente"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                decimal valor = decimal.Parse(leer["total"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                valor = decimal.Parse(leer["iva"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                valor = decimal.Parse(leer["totalgeneral"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                lvi.SubItems.Add(leer["status"].ToString());
                lvi.SubItems.Add(leer["compro"].ToString());
                lvi.SubItems.Add(leer["ayo"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label15.Text = Lv.Items.Count.ToString() + " Remisiones "; 
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) BuscarCreditos();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) BuscarCreditos();

        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) BuscarCreditos();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
