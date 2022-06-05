using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;


using CrystalDecisions.CrystalReports.Engine;

namespace SHOPCONTROL
{
    public partial class GastosEmpresa : Form
    {
        public string numgasto = "";
        public string nombre = "";
        public string cantidad= "";
        public string descripcion = "";
        public string fechaaplica = "";
        public string faplicacod= "";
        public string fecharealizo = "";
        public string frealizocod = "";
        public string horacod = "";
        public string hora= "";
        public string emitio = "";
        public string telefono= "";
        public string tipo = "";

        public GastosEmpresa()
        {
            InitializeComponent();
        }


        public void Consecutivo()
        {
            int num = 0;
            conectorSql conecta=new conectorSql();
            string Query = "Select numgasto from Consecutivos where numgasto<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                num = int.Parse(leer["numgasto"].ToString());
            }
            conecta.CierraConexion();
            textBox1.Text = num.ToString();
            textBox1.Enabled = false;
        }


        public void ActualizaConsecutivo()
        {
            int Siguiente=int.Parse(textBox1.Text) + 1;
            conectorSql conecta = new conectorSql();
            string Query = "update Consecutivos set numgasto='" + Siguiente.ToString() + "'";
            conecta.Excute(Query);
            conecta.CierraConexion();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            recolecta();
            if (Validacion() == false) return;

            if (Guardar())
            {
                ActualizaConsecutivo();


                DialogResult reply = MessageBox.Show("¿Desea mandar a imprimir?", "Impresion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (reply == DialogResult.Yes)
                   MandarReporteCristal();

                valoresg.Bitacora(valoresg.USUARIOSIS, "SE GUARDO  EL PAGO DE " + nombre + " DESCRIPCION " + descripcion, "PAGO DE PROVEEDOR/TRABAJADORES");

                MessageBox.Show("Se guardo correctamente ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
            }

        }

        public void recolecta()
        {
            numgasto = textBox1.Text.Trim();
            nombre = textBox2.Text.Trim();
            cantidad = textBox3.Text.Trim();
            descripcion = textBox4.Text.Trim();
            fechaaplica = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            faplicacod = dateTimePicker1.Value.ToString("yyyyMMdd");
            fecharealizo = DateTime.Now.ToString("dd/MM/yyyy");
            frealizocod = DateTime.Now.ToString("yyyyMMdd");
            hora = DateTime.Now.ToString("HH:mm:ss");
            horacod = DateTime.Now.ToString("HHMMss");
            emitio = valoresg.USUARIOSIS;
            telefono = textBox5.Text;
            tipo = comboBox1.Text;
        }

        public bool Validacion()
        {
            if (nombre == "")
            {
                MessageBox.Show("Ingrese el nombre de a quien corresponda", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (descripcion == "")
            {
                MessageBox.Show("Ingrese la descripcion del concepto de salida de dinero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cantidad == "")
            {
                MessageBox.Show("Ingrese la cantidad de dinero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return false;
            }

            return true;
        
        }

        public bool Guardar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Insert into gastosreg(";
            Query = Query + "cvgasto";
            Query = Query + ",nombre";
            Query = Query + ",descripcion";
            Query = Query + ",costo";
            Query = Query + ",fecha";
            Query = Query + ",fechacod";
            Query = Query + ",hora";
            Query = Query + ",horacod";
            Query = Query + ",emitio";
            Query = Query + ",telefono";
            Query = Query + ",tipo";
            Query = Query + ",fecharealizo";
            Query = Query + ",frealizocod) values (";
            Query = Query + "'" + numgasto + "'";
            Query = Query + ",'" + nombre+ "'";
            Query = Query + ",'" + descripcion + "'";
            Query = Query + ",'" + cantidad + "'";
            Query = Query + ",'" + fechaaplica+ "'";
            Query = Query + ",'" + faplicacod + "'";
            Query = Query + ",'" + hora + "'";
            Query = Query + ",'" + horacod + "'";
            Query = Query + ",'" + emitio + "'";
            Query = Query + ",'" + telefono+ "'";
            Query = Query + ",'" + tipo+ "'";
            Query = Query + ",'" + fecharealizo + "'";
            Query = Query + ",'" + frealizocod+ "')";
            conecta.Excute(Query);
            conecta.CierraConexion();
            return true;
        }

        public void Cargarinfo()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            string cvcliente = "";
            string cvclienteotro = "";
            Lv.Columns.Add("Clave", 80);
            Lv.Columns.Add("Nombre", 300);
            Lv.Columns.Add("Descripcion", 100);
            Lv.Columns.Add("Fecha", 100);
            Lv.Columns.Add("Cantidad", 100);
            Lv.Columns.Add("Tipo", 100);
            int cantColumnas = 6;
            int contador = 1;
            Lv.BeginUpdate();
            conectorSql conecta = new conectorSql();
            string Query = "Select * from gastosreg where cvgasto<>'' ";
            if (comboBox2.Text != "" && comboBox2.Text != "TODOS") Query = Query + " and tipo='" + comboBox2.Text + "'";
            if (textBox6.Text != "") Query = Query + " and nombre like '%" + textBox6.Text + "%'";
            Query = Query + " and fechacod between '" + dateTimePicker2.Value.ToString("yyyyMMdd") + "' and '" + dateTimePicker3.Value.ToString("yyyyMMdd") + "'";
            Query = Query + " order by nombre asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                cvcliente = leer["cvgasto"].ToString();
                ListViewItem lvi = new ListViewItem(cvcliente);
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["descripcion"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                lvi.SubItems.Add(leer["costo"].ToString());
                lvi.SubItems.Add(leer["tipo"].ToString());
                Lv.Items.Add(lvi);

                lvi.UseItemStyleForSubItems = false;
                int resultado = contador % 2;
                if (resultado == 0)
                {

                    lvi.BackColor = Color.FromArgb(217, 223, 251);
                    for (int i = 1; i < cantColumnas; i++)
                    {
                        lvi.SubItems[i].BackColor = Color.FromArgb(217, 223, 251);
                    }
                }
                else
                {
                    lvi.BackColor = Color.FromArgb(243, 243, 243);
                    for (int i = 1; i < cantColumnas; i++)
                    {
                        lvi.SubItems[i].BackColor = Color.FromArgb(243, 243, 243);
                    }
                }
                cvclienteotro = cvcliente;
                contador++;

            }
            conecta.CierraConexion();
            Lv.EndUpdate();
            label11.Text = Lv.Items.Count.ToString() + " Registros "; 

        
        }

        public void Limpiar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 3;
            dateTimePicker1.Value = DateTime.Now;
            Consecutivo();
        }

        private void GastosEmpresa_Load(object sender, EventArgs e)
        {
            Limpiar();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }


        public void MandarReporteCristal()
        {
            string NOMBREEMPRESA = "";
            string ADICIONALINFO = "";
            string REGIMEN = "";
            string DIRECCION = "";

            ReportDocument cryRpt = new ReportDocument();
            string CadenaReporte = "C:\\tmp\\reports\\DocGastoind.rpt";

            conectorSql conecta = new conectorSql();
            string Query = "Select * from ParametrosFactura where nombre<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                NOMBREEMPRESA = leer["nombrecomercial"].ToString();
                ADICIONALINFO = leer["infoadicional"].ToString();
                REGIMEN = leer["regimen"].ToString();
                DIRECCION = "Calle : " + leer["calle"].ToString();
                DIRECCION = DIRECCION + " Num. " + leer["numext"].ToString();
                DIRECCION = DIRECCION + " ," + leer["colonia"].ToString();
                DIRECCION = DIRECCION + " ," + leer["municipio"].ToString();
                DIRECCION = DIRECCION + " ," + leer["estado"].ToString();
                DIRECCION = DIRECCION + " C.P " + leer["codpostal"].ToString();

            }
            conecta.CierraConexion();

            cryRpt.Load(CadenaReporte);

            string consulta = "SELECT cvempresa, foto FROM Logoempresa where cvempresa='0'";
            ReciboUsuario CodigoBidimensional = GetData(consulta);
            cryRpt.SetDataSource(CodigoBidimensional);

            cryRpt.SetParameterValue("parametro1", ADICIONALINFO);
            cryRpt.SetParameterValue("NombreEmpresa", NOMBREEMPRESA);
            cryRpt.SetParameterValue("direccion", DIRECCION);
            
            
            cryRpt.SetParameterValue("numrecibo", numgasto);
            cryRpt.SetParameterValue("nombre", nombre);
            cryRpt.SetParameterValue("concepto", descripcion);
            cryRpt.SetParameterValue("total", cantidad);
            cryRpt.SetParameterValue("tiporecibo", tipo);
            cryRpt.SetParameterValue("telefono", telefono);
            cryRpt.SetParameterValue("fechaaplico", fechaaplica);
            cryRpt.SetParameterValue("emitio", emitio);

            cryRpt.PrintToPrinter(1, false, 0, 0);
            cryRpt.Close();
            cryRpt.Dispose();

            MessageBox.Show("Se mando a imprimir correctamente", "Impresion de Recibo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private ReciboUsuario GetData(string query)
        {
            conectorSql conecta = new conectorSql();
            conecta.Abrirconexion();

            string conString = conecta.CADENACONEXION;
            conecta.CierraConexion();
            SqlCommand cmd = new SqlCommand(query);

            SqlDataAdapter sda2 = new SqlDataAdapter();

            ReciboUsuario ReporteM = new ReciboUsuario();
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(ReporteM, "DataTable3");
                }
            }

            return ReporteM;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cargarinfo();
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Cargarinfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reportespdf reporte = new Reportespdf();
            DateTime Fechainicia = dateTimePicker1.Value.AddDays(-1);
            DateTime FechaFinal = dateTimePicker1.Value;
            string cadena = reporte.ReportePorGastos(Fechainicia.ToString("dd/MM/yyyy"), FechaFinal.ToString("dd/MM/yyyy"), dateTimePicker3.Value.ToString("HHmmss"), "");
            try
            {
                System.Diagnostics.Process.Start(cadena);
            }
            catch (Exception er)
            {
                MessageBox.Show("" + er, "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string Query = "";

            DialogResult reply = MessageBox.Show("¿Desea eliminar los pagos seleccionados?", "Eliminar pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;


            for (int i = 0; i < Lv.Items.Count; i++)
            {
                if (Lv.Items[i].Checked == true)
                {
                    string cvgasto = Lv.Items[i].Text;
                    string nombreM = Lv.Items[i].SubItems[1].Text;
                    string descripcionM= Lv.Items[i].SubItems[2].Text;

                    Query = "Delete from gastosreg where cvgasto='" + cvgasto + "'";
                    conecta.Excute(Query);
                    conecta.CierraConexion();
                    valoresg.Bitacora(valoresg.USUARIOSIS, "ELIMINO EL PAGO DE " + nombreM + " DESCRIPCION " + descripcionM, "PAGO DE PROVEEDOR/TRABAJADORES");
                }
            }

            Cargarinfo();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


    }
}
