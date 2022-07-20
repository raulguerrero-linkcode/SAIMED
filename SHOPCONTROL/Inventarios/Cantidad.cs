using System;
using System.Windows.Forms;

namespace SHOPCONTROL.Inventarios
{
    public partial class Cantidad : Form
    {
        private string item;
        private long cantidades;
        private string desc;

        public Cantidad(string item, string desc, long cantidades)
        {
            InitializeComponent();
            this.item = item;
            this.desc = desc;
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
                Query = Query + " where cvproducto='" + this.item + "' and nombre='" + desc + "'";

                if (conecta.Excute(Query))
                {
                    MessageBox.Show("Inventario actualizado exitosamente, nuevo inventario:" + decimal.Parse(suma.ToString()));

                }else
                {
                    MessageBox.Show("Inventario no actualizado, revise la información");

                }

                conecta.CierraConexion();
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
