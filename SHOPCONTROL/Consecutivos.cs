using System;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace SHOPCONTROL
{
    public partial class Consecutivos : Form
    {
        public Consecutivos()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Consecutivos_Load(object sender, EventArgs e)
        {
            cargarInfo();

        }
        public void cargarInfo()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from consecutivos where numproducto<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox1.Text = leer["numpago"].ToString();
                textBox2.Text = leer["numprov"].ToString();
                textBox3.Text = leer["numcliente"].ToString();
                textBox5.Text = leer["numproducto"].ToString();
                textBox7.Text = leer["numempresa"].ToString();
                
                textBox8.Text = leer["numpedido"].ToString();
                textBox4.Text = leer["numrecibo"].ToString();
                textBox6.Text = leer["numgasto"].ToString();

            }
            conecta.CierraConexion();
        }

        public void ActualizaConsecutivo()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Update consecutivos set ";
            Query = Query + "numprov='" + textBox2.Text + "'";
            Query = Query + ",numcliente='" + textBox3.Text + "'";
            Query = Query + ",numproducto='" + textBox5.Text + "'";
            Query = Query + ",numempresa='" + textBox7.Text + "'";
            Query = Query + ",numpago='" + textBox1.Text + "'";
            Query = Query + ",numrecibo='" + textBox4.Text + "'";
            Query = Query + ",numpedido='" + textBox8.Text + "'";
            Query = Query + ",numgasto='" + textBox6.Text + "'";

            conecta.Excute(Query);
            MessageBox.Show("Se actualizo correctamente los consecutivos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cargarInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActualizaConsecutivo();
        }
    }
}
