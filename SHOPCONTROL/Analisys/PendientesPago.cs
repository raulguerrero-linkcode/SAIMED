using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHOPCONTROL.Analisys
{
    public partial class PendientesPago : Form
    {
        public PendientesPago()
        {
            InitializeComponent();
        }

        private void PendientesPago_Load(object sender, EventArgs e)
        {
           
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            string cvcliente = "";
            Lv.Items.Clear();
            Lv.Columns.Clear();
            Lv.Columns.Add("cvcliente", 80);
            Lv.Columns.Add("idcliente", 80);

            Lv.Columns.Add("nombre", 80);
            Lv.Columns.Add("factura", 80);
            Lv.Columns.Add("activo", 80);
            Lv.Columns.Add("fechamod", 80);
            Lv.Columns.Add("metodopago", 80);
            Lv.Columns.Add("formapago", 80);
            Lv.Columns.Add("diascredito", 80);
            Lv.Columns.Add("saldo", 80);
            Lv.Columns.Add("numpedido", 80);
            Lv.Columns.Add("fecha", 80);
            Lv.Columns.Add("concepto", 80);
            Lv.Columns.Add("cvconcepto", 80);
            Lv.Columns.Add("estatus", 80);
            Lv.Columns.Add("fechapago", 80);
            Lv.Columns.Add("pagocon", 80);
            Lv.Columns.Add("tipopago", 80);
            Lv.Columns.Add("cvproducto", 80);
            Lv.Columns.Add("descripcion", 80);
            Lv.Columns.Add("cantidad", 80);
            Lv.Columns.Add("precio", 80);
            Lv.Columns.Add("total", 80);
            Lv.Columns.Add("iva", 80);
            Lv.Columns.Add("totalgeneral", 80);
            Lv.Columns.Add("Pago_Con", 80);
            Lv.Columns.Add("cambio", 80);


            Lv.BeginUpdate();
            conectorSql conecta = new conectorSql();
            string Query = "Select * from v_pedidos where nombre like'%" + nombre.Text + "%'";

            // int cantColumnas = 27;
            int contador = 1;

            System.Data.SqlClient.SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                cvcliente = leer["cvcliente"].ToString();
                ListViewItem lvi = new ListViewItem(cvcliente);

                lvi.SubItems.Add(leer["idcliente"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["factura"].ToString());
                lvi.SubItems.Add(leer["activo"].ToString());
                lvi.SubItems.Add(leer["fechamod"].ToString());
                lvi.SubItems.Add(leer["metodopago"].ToString());
                lvi.SubItems.Add(leer["formapago"].ToString());
                lvi.SubItems.Add(leer["diascredito"].ToString());
                lvi.SubItems.Add(leer["saldo"].ToString());
                lvi.SubItems.Add(leer["numpedido"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                lvi.SubItems.Add(leer["concepto"].ToString());
                lvi.SubItems.Add(leer["cvconcepto"].ToString());
                lvi.SubItems.Add(leer["estatus"].ToString());
                lvi.SubItems.Add(leer["fechapago"].ToString());
                lvi.SubItems.Add(leer["pagocon"].ToString());
                lvi.SubItems.Add(leer["tipopago"].ToString());
                lvi.SubItems.Add(leer["cvproducto"].ToString());
                lvi.SubItems.Add(leer["descripcion"].ToString());
                lvi.SubItems.Add(leer["cantidad"].ToString());
                lvi.SubItems.Add(leer["precio"].ToString());
                lvi.SubItems.Add(leer["total"].ToString());
                lvi.SubItems.Add(leer["iva"].ToString());
                lvi.SubItems.Add(leer["totalgeneral"].ToString());
                lvi.SubItems.Add(leer["Pago_Con"].ToString());
                lvi.SubItems.Add(leer["cambio"].ToString());

                Lv.Items.Add(lvi);

                lvi.UseItemStyleForSubItems = false;
                contador++;
            }
            conecta.CierraConexion();
            Lv.EndUpdate();
        }
    }
}
