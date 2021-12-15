using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace SHOPCONTROL
{
    public partial class CancelaRemision : Form
    {
        public CancelaRemision()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Lv.Columns.Add("Num. Pedido", 80);
            Lv.Columns.Add("Unidad", 70);
            Lv.Columns.Add("Cantidad", 50);
            Lv.Columns.Add("Descripción", 180);
            Lv.Columns.Add("Unitario", 90);
            Lv.Columns.Add("Total", 90);

            conectorSql conecta = new conectorSql();
            string Query = "Select * from DetallesPedido where numpedido='" + textBox2.Text + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["numpedido"].ToString());
                lvi.SubItems.Add(leer["unidad"].ToString());
                lvi.SubItems.Add(leer["cantidad"].ToString());
                lvi.SubItems.Add(leer["descripcion"].ToString());
                decimal valor = decimal.Parse(leer["preunitario"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));
                valor = decimal.Parse(leer["precio"].ToString());
                lvi.SubItems.Add(valor.ToString("##.00", CultureInfo.InvariantCulture));          
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label1.Text=Lv.Items.Count.ToString();
        }

        public string OBSERVACION;
        public string NUMREMISION;
        public string FECHACOD;
        public string FECHA;
        public string EMITIO;
        public void recolecta()
        {
            NUMREMISION = textBox2.Text;
            OBSERVACION = textBox7.Text;
            FECHA = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            FECHACOD = dateTimePicker1.Value.ToString("yyyyMMdd");
            EMITIO = Modremision.EMITE;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Insert into cancelaciones (numpedido,observacion,emitio,fecha,fechacod) values(";
            Query = Query + "'" + NUMREMISION + "'";
            Query = Query + ",'" + OBSERVACION + "'";
            Query = Query + ",'" + EMITIO+ "'";
            Query = Query + ",'" + FECHA+ "'";
            Query = Query + ",'" + FECHACOD+ "')";
            conecta.Excute(Query);

            Query = "Update pagos set estatus='CANCELADO' where numpedido='" + NUMREMISION + "'";
            conecta.Excute(Query);

            Query = "Update Creditos set estatus='CANCELADO' where numpedido='" + NUMREMISION + "'";
            conecta.Excute(Query);

            Query = "Update Pedidos set estatuspedido='CANCELADO' where numpedido='" + NUMREMISION + "'";
            conecta.Excute(Query);

            Query = "Delete from CobroenVentana where numpedido='" + NUMREMISION + "'";
            conecta.Excute(Query);

            BuscarFactura();
            MessageBox.Show("Se cancelo correctamente el pedido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
        }

        public void BuscarFactura()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from facturas where numpedido='" + NUMREMISION + "' and estatus='FACTURADO' and numfactura<>'0'";
            bool existe = conecta.ExisteRegistro(Query);
            if (existe == true)
            {
                MessageBox.Show("El pedido ya se encuentra facturado es necesario, cancelar la factura.\nSe enviara a cancelar factura ", "Cancelar Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);
             //   CancelarFactura cancelarfactura = new CancelarFactura();
             //   cancelarfactura.ShowDialog();
            }
        }

        private void CancelaRemision_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button3_Click(sender, e);
        }
    }
}
