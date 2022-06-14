using System;
using System.Windows.Forms;

namespace SHOPCONTROL.Inventarios
{
    public partial class Cantidad : Form
    {
        private string item;
        private long cantidades;

        public Cantidad(string item, long cantidades)
        {
            InitializeComponent();
            this.item = item;
            this.cantidades = cantidades;
        }
                
        private void button1_Click(object sender, EventArgs e)
        {
            if (long.TryParse(cantidadInventario.Text, out long isparsable))
            {
                conectorSql conecta = new conectorSql();

                long suma = cantidades + long.Parse(cantidadInventario.Text);

                string Query = "Update Productos set ";
                Query = Query + "cantidad=" + suma + "";
                Query = Query + " where cvproducto='" + this.item + "'";
                conecta.Excute(Query);
                conecta.CierraConexion();
                MessageBox.Show("Inventario actualizado exitosamente, nuevo inventario:" + decimal.Parse(suma.ToString()));
                // Refresh();
                this.Dispose();
            }
            else
            {
                cantidadInventario.Text = "Capture cantidades en número";
                cantidadInventario.Focus();
                cantidadInventario.SelectAll();
            }
        }

        private void Cantidad_Load(object sender, EventArgs e)
        {
            piezaslabel.Text = cantidades.ToString();

        }
    }
}
