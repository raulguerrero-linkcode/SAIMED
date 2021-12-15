using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
namespace SHOPCONTROL
{
    public partial class CambioaCliente : Form
    {
        public CambioaCliente()
        {
            InitializeComponent();
        }

        public CambioaCliente(string ventpedido, string total)
        {
            InitializeComponent();
            label6.Text = ventpedido;
            label7.Text = total;
        }

        private void CambioaCliente_Load(object sender, EventArgs e)
        {
          
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GuardarCobro();
                this.Dispose();
            }
           
        }

        public void CalcularCambio()
        {
            decimal total = decimal.Parse(label7.Text);
            decimal recibio = 0;
            if (textBox2.Text!="")recibio = decimal.Parse(textBox2.Text);

            decimal resultado = recibio - total;
            label4.Text = resultado.ToString("##.00", CultureInfo.InvariantCulture);
        }

        public void GuardarCobro()
        {
            string tipopago = "";
            if (radioButton1.Checked == true) tipopago = "EFECTIVO";
            if (radioButton2.Checked == true) tipopago = "CHEQUE";
            if (radioButton3.Checked == true) tipopago = "TRANSFERENCIA";
            if (radioButton4.Checked == true) tipopago = "DEBITO";
            if (radioButton5.Checked == true) tipopago = "CREDITO";
            if (radioButton6.Checked == true) tipopago = "DEPOSITO";

            conectorSql conecta = new conectorSql();
            string Query = "Insert into CobroenVentana(numpedido,total,recibio,cambio,fecha,fechacod,ayo,mes,tipopago,emitio)";
            Query = Query + " values(";
            Query = Query + "'" + label6.Text + "'";
            Query = Query + ",'" + label7.Text + "'";
            Query = Query + ",'" + textBox2.Text+ "'";
            Query = Query + ",'" + label4.Text + "'";
            Query = Query + ",'" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
            Query = Query + ",'" + DateTime.Now.ToString("yyyyMMdd") + "'";
            Query = Query + ",'" + DateTime.Now.Year.ToString() + "'";
            Query = Query + ",'" + DateTime.Now.Month.ToString() + "'";
            Query = Query + ",'" + tipopago + "'";
            Query = Query + ",'" + valoresg.USUARIOSIS + "')";
            conecta.Excute(Query);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio();
        }
    }
}
