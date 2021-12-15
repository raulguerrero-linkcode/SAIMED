using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Globalization;

namespace ListadePagos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void BuscarConscutivo()
        {
            int Consecutivo = 1;
            ConAccess conecta = new ConAccess();
            OleDbDataReader leer=conecta.RecordInfo("Select numpago from consecutivo");
            while (leer.Read())
            {
                Consecutivo =int.Parse(leer["numpago"].ToString());
            }
            conecta.CierraConexion();

            textBox4.Text = Consecutivo.ToString();
        }

        public void ActualizarConsecutivo()
        {
            int Consecutivo = int.Parse(textBox4.Text)+1;
            ConAccess conecta = new ConAccess();
            string Query = "Update consecutivo set numpago=' " + Consecutivo.ToString() + "'";
            conecta.Excute(Query);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validacion() == true)
            {
                guardar();
                ActualizarConsecutivo();
                Limpiar();
                MessageBox.Show("Se guardo correctamente el pago del cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button3_Click(sender, e);
            }

        }


        public bool ComboClientes(ComboBox Combo)
        {
            string Query = "";
            ConAccess conecta = new ConAccess();
            Query = "select  distinct(nombrecliente) as nombre from registropagos";
            DataTable conten = conecta.Lectura(Query);
            Combo.DataSource = conten;
            Combo.ValueMember = conten.Columns[0].ToString().ToUpper().Trim();
            Combo.DisplayMember = conten.Columns[0].ToString().ToUpper().Trim();
            Combo.Text = "";
            return true;
        }

        public bool ComboVendedores(ComboBox Combo)
        {
            string Query = "";
            ConAccess conecta = new ConAccess();
            Query = "select  distinct(vendedor) from registropagos";
            DataTable conten = conecta.Lectura(Query);
            Combo.DataSource = conten;
            Combo.ValueMember = conten.Columns[0].ToString().ToUpper().Trim();
            Combo.DisplayMember = conten.Columns[0].ToString().ToUpper().Trim();
            Combo.Text = "";
            return true;
        }


        public void Limpiar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            textBox4.Text = "";
            BuscarConscutivo();
            ComboClientes(comboBox1);
            ComboVendedores(comboBox2);

            ComboClientes(comboBox3);
            ComboVendedores(comboBox4);

            comboBox1.Focus();


        }

        public bool validacion()
        {
            if (comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Seleccione o ingrese el nombre del cliente", "Datos del cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
                return false;
            }


            if (comboBox2.Text.Trim() == "")
            {
                MessageBox.Show("Seleccione o ingrese el vendedor", "Seleccione vendedor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox2.Focus();
                return false;
            }


            if (textBox3.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese la cantidad a registrar", "Registro de dinero", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox3.Focus();
                return false;
            }

            if (textBox1.Text.Trim() == "" && textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese un numero de factura o remision", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return false;
            }

            return true;
        }
        
        public void guardar()
        {
            string Query = "";
            ConAccess conecta = new ConAccess();

            decimal total = decimal.Parse(textBox3.Text.Trim());
            Numalet let = null;
            let = new Numalet();
            //al uso en México (creo):
            let.MascaraSalidaDecimal = "00/100 M.N.";
            let.SeparadorDecimalSalida = " pesos";
            //observar que sin esta propiedad queda "veintiuno pesos" en vez de "veintiún pesos":
            let.ApocoparUnoParteEntera = true;
            //let.ConvertirDecimales = true;
            string cantidadletra = let.ToCustomCardinal(total.ToString("##.00", CultureInfo.InvariantCulture));


            Query = "Insert into registropagos(Consecutivo,Fecha,Fechacod,Nombrecliente,Vendedor,Numremision,numfactura,cantidad,cantletra) values(";
            Query = Query + "'" + textBox4.Text + "'";
            Query = Query + ",'" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "'";
            Query = Query + ",'" + dateTimePicker1.Value.ToString("yyyyMMdd") + "'";
            Query = Query + ",'" + comboBox1.Text.Trim() + "'";
            Query = Query + ",'" + comboBox2.Text.Trim() + "'";
            Query = Query + ",'" + textBox1.Text.Trim() + "'";
            Query = Query + ",'" + textBox2.Text.Trim() + "'";
            Query = Query + ",'" + textBox3.Text.Trim() + "'";
            Query = Query + ",'" + cantidadletra+ "')";

            conecta.Excute(Query);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            Limpiar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Lv.Columns.Clear();
            Lv.Items.Clear();
            Lv.Columns.Add("Consecutivo", 70);
            Lv.Columns.Add("Nombre", 150);
            Lv.Columns.Add("Vendedor", 150);
            Lv.Columns.Add("Fecha", 90);
            Lv.Columns.Add("Num Remision", 85);
            Lv.Columns.Add("Num Factura", 85);
            Lv.Columns.Add("Total", 90);
            Lv.Columns.Add("Cantidad Letra", 250);


            ConAccess conecta = new ConAccess();
            string query="Select * from registropagos where cantidad<>''";
            if (comboBox3.Text.Trim() != "") query = query + " and nombrecliente like '%" + comboBox3.Text.Trim() + "%'";
            if (comboBox4.Text.Trim() != "") query = query + " and vendedor like '%" + comboBox4.Text.Trim() + "%'";
            if (checkBox1.Checked == false)
            {
                query = query + " and fechacod between '" + dateTimePicker2.Value.ToString("yyyyMMdd") + "' and '" + dateTimePicker3.Value.ToString("yyyyMMdd") + "'";
            }

            OleDbDataReader leer = conecta.RecordInfo(query);
            decimal Acumula = 0;
            decimal Importe = 0;
            while (leer.Read())
            {

                string consecutivo = leer["consecutivo"].ToString();
                string fecha = leer["fecha"].ToString();
                string Nombre = leer["nombrecliente"].ToString();
                string vendedor = leer["vendedor"].ToString();
                string numremision = leer["numremision"].ToString();
                string numfactura = leer["numfactura"].ToString();
                string cantidad= leer["cantidad"].ToString();
                string letra = leer["cantletra"].ToString(); 
                Importe = decimal.Parse(cantidad.ToString());
                Acumula = Acumula + Importe;

                ListViewItem lvi = new ListViewItem(consecutivo);
                lvi.SubItems.Add(Nombre);
                lvi.SubItems.Add(vendedor);
                lvi.SubItems.Add(fecha);
                lvi.SubItems.Add(numremision);
                lvi.SubItems.Add(numfactura);
                lvi.SubItems.Add(cantidad);
                lvi.SubItems.Add(letra);
                Lv.Items.Add(lvi);

            }
            conecta.CierraConexion();

            label16.Text = Acumula.ToString("##.00", CultureInfo.InvariantCulture);
            label14.Text = Lv.Items.Count.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (Lv.Items.Count == 0)
            {
                MessageBox.Show("No hay informacion para eliminar", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int Contador = 0;
            int i = 0;
            for (i = 0; i < Lv.Items.Count; i++)
            {
                if (Lv.Items[i].Checked) Contador++;
            }

            if (Contador == 0)
            {
                MessageBox.Show("No hay informacion seleccionada para eliminar", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            DialogResult reply = MessageBox.Show("Desea eliminar la información seleccionada?", "Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
            {
                return;
            }

            ConAccess conecta = new ConAccess();
           
            for (i = 0; i < Lv.Items.Count; i++)
            {
                if (Lv.Items[i].Checked)
                {
                    string clave = Lv.Items[i].Text;
                    string fecha = Lv.Items[i].SubItems[3].Text;

                    string query = "Delete from registropagos where consecutivo=" + clave + " and fecha='" + fecha + "'";
                    conecta.Excute(query);
                }
            }


            MessageBox.Show("Se eliminaron los registros seleccionados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button3_Click(sender, e);


        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            Limpiar();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MandarExportar();
        }

        private void MandarExportar()
        {

            DialogResult reply = MessageBox.Show("Desea exportar la informacion a excel?", "Exportación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
            {
                return;
            }

            string[,] Informacion;
            int contador = 0;

            Informacion = new string[Lv.Items.Count, 8];


            for (int i = 0; i < Lv.Items.Count; i++)
            {
                Informacion[contador, 0] = Lv.Items[i].Text.ToString();
                Informacion[contador, 1] = Lv.Items[i].SubItems[1].Text.ToString();
                Informacion[contador, 2] = Lv.Items[i].SubItems[2].Text.ToString();
                Informacion[contador, 3] = Lv.Items[i].SubItems[3].Text.ToString();
                Informacion[contador, 4] = Lv.Items[i].SubItems[4].Text.ToString();
                Informacion[contador, 5] = Lv.Items[i].SubItems[5].Text.ToString();
                Informacion[contador, 6] = Lv.Items[i].SubItems[6].Text.ToString();
                Informacion[contador, 7] = Lv.Items[i].SubItems[7].Text.ToString();
                contador++;
            }

            if (contador == 0)
            {
                MessageBox.Show("No hay información seleccionada", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            ReportesNKB.MandarCantidadesR(Informacion, Lv.Items.Count);
            this.Cursor = Cursors.Default;
        }
    }
}
