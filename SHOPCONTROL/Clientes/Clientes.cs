using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SHOPCONTROL.Utilerias;

namespace SHOPCONTROL
{
    public partial class Clientes : Form
    {
        private ListViewColumnSorter lvwColumnSorter;

        public Clientes()
        {
            InitializeComponent();
            this.Lv.ListViewItemSorter = new Sorter();
        }

        public string CLAVE = "";
        public string NOMBRE = "";
        public string EMPRESA= "";
        public string CORREO= "";
        public string CORREO2= "";
        public string TELEFONO= "";
        public string CELULAR= "";
        public string DIRECCION= "";
        public string FACTURA= "";
        public string RFC= "";
        public string DFISCAL= "";

        public string CALLEE= "";
        public string COLONIAE = "";
        public string MUNICIPIOE = "";
        public string ESTADOE = "";
        public string CODPE = "";
        public string PAISE= "";
        public string CALLEF= "";
        public string COLONIAF= "";
        public string MUNICIPIOF = "";
        public string ESTADOF = "";
        public string CODF= "";
        public string PAISF= "";

        public string FECHAMOD = "";
        public string FCODMOD = "";
        public string SINCRONIZADO = "";
        public string ACTIVIDAD= "";
        public string NUMEXTERIOR= "";
        public bool BandConsecutivo;

        public string OBSERVACIONESFACT = "";

        public string METODOPAGO = "";
        public string CVBANCO = "";
        public string NUMCUENTA = "";
        public string VENDEDOR = "";
        public string FORMAPAGO = "";

        public string TIPOPAGO = "";
        public string DIASPAGO = "0";
        public string NOMBREFACTURA = "";
        //
        public string ACTIVO = "";
        public float SALDO = 0.0f;
        public float MONEDERO = 0.0f;
        public string DCREDITO = "";
        public float SALDOCREDITO = 0.0f;

        public void Recolectar()
        {

            VENDEDOR = comboBox8.Text;
            METODOPAGO = comboBox2.Text;
            CVBANCO = comboBox7.Text;
            NUMCUENTA = textBox24.Text;
            FORMAPAGO = comboBox3.Text;

            if (FORMAPAGO == "") FORMAPAGO = "PAGO EN UNA SOLA EXHIBICIÓN";

            OBSERVACIONESFACT = textBox23.Text;
            CLAVE = textBox1.Text;
            NOMBRE = textBox2.Text;
            EMPRESA = textBox10.Text;
            CORREO = textBox3.Text;
            CORREO2 = textBox4.Text;
            TELEFONO = textBox5.Text;
            CELULAR = textBox6.Text;

            if (CORREO == "") CORREO = "info@soluciones-sia.com";
            if (CORREO2 == "") CORREO = "info@soluciones-sia.com";

            CALLEE = textBox7.Text;
            COLONIAE = textBox12.Text;
            MUNICIPIOE = textBox13.Text;
            ESTADOE = textBox14.Text;
            CODPE = textBox15.Text;
            PAISE= textBox16.Text;
            NUMEXTERIOR = textBox22.Text;
            DIRECCION = "CALLE " + CALLEE + " COL. " + COLONIAE + " NUM. " + NUMEXTERIOR + " C.P " + CODPE + " " + MUNICIPIOE + " " + ESTADOE + "," + PAISE;
            FACTURA = "NO";
            
            
            if (radioButton1.Checked == true) FACTURA = "SI";
            RFC=  textBox8.Text;

            CALLEF = textBox21.Text;
            COLONIAF = textBox20.Text;
            MUNICIPIOF = textBox19.Text;
            ESTADOF = textBox18.Text;
            CODF = textBox17.Text;
            PAISF = textBox9.Text;
            DFISCAL = "CALLE " + CALLEF + " COL. " + COLONIAF + " C.P " + CODF + " " + MUNICIPIOF + " "  + ESTADOF + "," + PAISF;
            FECHAMOD = DateTime.Now.ToString("dd/MM/yyyy");
            FCODMOD = DateTime.Now.ToString("yyyyMMdd");
            SINCRONIZADO = "0";
            ACTIVIDAD = comboBox1.Text;

            if (NUMCUENTA.Length >= 0 && NUMCUENTA.Length < 4) NUMCUENTA = "0000";

            if (radioButton4.Checked == true)
            {
                TIPOPAGO = "Contado";
                DIASPAGO = "0";
            }
            else
            {
                TIPOPAGO = "Credito";
                DIASPAGO = textBox25.Text;
            }

            NOMBREFACTURA = textBox26.Text;
            NOMBREFACTURA = NOMBREFACTURA.Trim();

            //---       
            ACTIVO = activo(comboBox4);
            SALDO = float.Parse(textBox27.Text);
            MONEDERO = float.Parse(textBox28.Text);
            //DCREDITO
            SALDOCREDITO = float.Parse(textBox29.Text);

        }
        public string activo(ComboBox cmb) {
            string contenido = cmb.Text;
            if (contenido.Equals("ACTIVO")) return "1";
            else return "0";
        }
        public string activo(string opc)
        {
            if (opc.Equals("1")) { return "ACTIVO"; }
            else if (opc.Equals("0")) {return "INACTIVO";}
            else return "";
        }

        public bool Validacion()
        {
            if (NOMBRE == "")
            {
                MessageBox.Show("Ingrese el nombre completo del cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return false;
            }

            if (CORREO== "")
            {
                MessageBox.Show("Ingrese el correo electronico del cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return false;
            }

            if (TELEFONO== "")
            {
                MessageBox.Show("Ingrese el telefono del cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return false;
            }

            if (radioButton1.Checked == true)
            {

                if (RFC == "")
                {
                    MessageBox.Show("Ingrese el RFC del cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox8.Focus();
                    return false;
                }

                if (ACTIVIDAD == "")
                {
                    MessageBox.Show("Seleccione la Actividad Fiscal", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox1.Focus();
                    return false;
                }

            }

            if (CALLEF== "")
            {
                MessageBox.Show("Ingrese la calle", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox21.Focus();
                return false;
            }

            if (COLONIAF == "")
            {
                MessageBox.Show("Ingrese la colonia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox20.Focus();
                return false;
            }


            if (MUNICIPIOF == "")
            {
                MessageBox.Show("Ingrese el municipio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox19.Focus();
                return false;
            }

            if (ESTADOF == "")
            {
                MessageBox.Show("Ingrese el estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox18.Focus();
                return false;
            }

            if (CODF == "")
            {
                MessageBox.Show("Ingrese el codigo postal", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox17.Focus();
                return false;
            }

            if (PAISF == "")
            {
                MessageBox.Show("Ingrese el país", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox9.Focus();
                return false;
            }
            //
            if (ACTIVO == "")
            {
                MessageBox.Show("Seleccione Estatus del cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox4.Focus();
                return false;
            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CargarInfo();

        }
        public void CargarInfo()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 80).Tag = "NUMBER";
            Lv.Columns.Add("Razón Social", 250).Tag = "STRING";
            Lv.Columns.Add("Telefono", 100).Tag = "STRING";
            Lv.Columns.Add("Correo", 100).Tag = "STRING";
            Lv.Columns.Add("Direccion", 250).Tag = "STRING";
            Lv.Columns.Add("Nombre Comercial", 100).Tag = "STRING";
            Lv.Columns.Add("¿Factura?", 100).Tag = "STRING";

            conectorSql conecta = new conectorSql();
            string Query = "Select * from Clientes where nombre<>''";
            if (textBox11.Text != "") Query = Query + " and nombre like '%" + textBox11.Text + "%'";
            Query = Query + " order by cvcliente asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["cvcliente"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["telefono"].ToString());
                lvi.SubItems.Add(leer["email"].ToString());
                lvi.SubItems.Add(leer["direccion"].ToString());
                lvi.SubItems.Add(leer["empresa"].ToString());
                lvi.SubItems.Add(leer["factura"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label15.Text = Lv.Items.Count.ToString() + " Registros "; 
        }
        private void button4_Click(object sender, EventArgs e)
        {
            BandConsecutivo = false;
            textBox1.Enabled = true;
            button8.Enabled = true;
            BandModificar = false;
            panel1.Visible = true;
            panel3.Visible = false;
            Limpiar();
            textBox1.Focus();
        }

        public void Limpiar()
        {
            textBox1.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";

            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
            textBox20.Text = "";
            textBox21.Text = "";
            comboBox1.Text = "";
            textBox23.Text = "";
            comboBox3.Text = "";

            comboBox2.SelectedIndex = 2;
            comboBox7.Text = "";
            textBox24.Text = "0000";
            radioButton1.Checked = false;
            radioButton2.Checked = true;
            combos.ComboBancos(comboBox7);
            combos.ComboVendedores(comboBox8);
            combos.ComboFormadePago(comboBox2);

            radioButton4.Checked = true;
            radioButton3.Checked = false;
            textBox25.Text = "0";
            textBox2.Focus();
            //
            comboBox4.Text = "";
            textBox27.Text = "";
            textBox28.Text = "";
            textBox29.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            combos.ComboFormadePago(comboBox2);
            combos.ComboBancos(comboBox7);
        }

        public void BuscarInformacion(string clave)
        {
            Limpiar();
            textBox1.Enabled = false;
            conectorSql conecta = new conectorSql();
            string Query = "Select * from clientes where cvcliente='" + clave + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox1.Text = clave;
                textBox2.Text = leer["nombre"].ToString();
                textBox10.Text = leer["empresa"].ToString();
                textBox3.Text = leer["email"].ToString();
                textBox4.Text = leer["email2"].ToString();
                textBox5.Text = leer["telefono"].ToString();
                textBox6.Text = leer["celular"].ToString();
                
                textBox7.Text = leer["calleE"].ToString();
                textBox12.Text = leer["ColoniaE"].ToString();
                textBox13.Text = leer["MunicipioE"].ToString();
                textBox14.Text = leer["EstadoE"].ToString();
                textBox15.Text = leer["CodE"].ToString();
                textBox16.Text = leer["PaisE"].ToString();
                
                textBox8.Text = leer["rfc"].ToString();
                textBox22.Text = leer["numF"].ToString();
                

                textBox21.Text = leer["calleF"].ToString();
                textBox20.Text = leer["ColoniaF"].ToString();
                textBox19.Text = leer["MunicipioF"].ToString();
                textBox18.Text = leer["EstadoF"].ToString();
                textBox17.Text = leer["CodF"].ToString();
                textBox9.Text = leer["PaisF"].ToString();

                comboBox1.Text = leer["actividad"].ToString();
                textBox23.Text= leer["observafact"].ToString();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                string valor = leer["factura"].ToString();
                if (valor == "SI") radioButton1.Checked = true;
                else radioButton2.Checked = true;

                comboBox2.Text = leer["metodopago"].ToString();
                comboBox7.Text = leer["cvbanco"].ToString();
                textBox24.Text = leer["numcuenta"].ToString();
                comboBox8.Text = leer["vendedor"].ToString();
                comboBox3.Text = leer["formapago"].ToString();

                radioButton4.Checked = false;
                radioButton3.Checked = false;
                valor = leer["tipopago"].ToString();
                if (valor == "Contado") radioButton4.Checked = true;
                else radioButton3.Checked = true;

                valor = leer["diascredito"].ToString();
                if (valor != "0")
                {
                    panel13.Visible = true;
                }
                textBox25.Text = valor;

                panel13.Visible = true;
                textBox25.Text = leer["diascredito"].ToString();
                textBox26.Text = leer["nombrefactura"].ToString();
                //
                valor = leer["activo"].ToString();
                comboBox4.Text = activo(valor);
                textBox27.Text = leer["saldo"].ToString();
                textBox28.Text = leer["saldocredito"].ToString();
                textBox29.Text = leer["monedero"].ToString();

            }
            conecta.CierraConexion();
        }

        public void Actualizar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "update clientes set";
            Query = Query + " nombre='" + NOMBRE + "'";
            Query = Query + ",telefono='" + TELEFONO + "'";
            Query = Query + ",email='" + CORREO + "'";
            Query = Query + ",email2='" + CORREO2 + "'";
            Query = Query + ",celular='" + CELULAR + "'";
            Query = Query + ",direccion='" + DIRECCION + "'";
            Query = Query + ",rfc='" + RFC + "'";
            Query = Query + ",direfiscal='" + DFISCAL + "'";
            Query = Query + ",empresa='" + EMPRESA + "'";
            Query = Query + ",factura='" + FACTURA + "'";
            Query = Query + ",calleE='" + CALLEE + "'";
            Query = Query + ",ColoniaE='" + COLONIAE + "'";
            Query = Query + ",MunicipioE='" + MUNICIPIOE + "'";
            Query = Query + ",EstadoE='" + ESTADOE + "'";
            Query = Query + ",CodE='" + CODPE + "'";
            Query = Query + ",PaisE='" + PAISE + "'";
            Query = Query + ",CalleF='" + CALLEF + "'";
            Query = Query + ",ColoniaF='" + COLONIAF + "'";
            Query = Query + ",MunicipioF='" + MUNICIPIOF + "'";
            Query = Query + ",EstadoF='" + ESTADOF+ "'";
            Query = Query + ",CodF='" +CODF + "'";
            Query = Query + ",PaisF='" + PAISF+ "'";
            Query = Query + ",fechamod='" + FECHAMOD + "'";
            Query = Query + ",fcodmod='" + FCODMOD + "'";
            Query = Query + ",sincronizado='" + SINCRONIZADO + "'";
            Query = Query + ",numf='" + NUMEXTERIOR+ "'";
            Query = Query + ",actividad='" + ACTIVIDAD + "'";
            Query = Query + ",observafact='" + OBSERVACIONESFACT + "'";

            Query = Query + ",numcuenta='" + NUMCUENTA+ "'";
            Query = Query + ",cvbanco='" + CVBANCO+ "'";
            Query = Query + ",metodopago='" + METODOPAGO + "'";
            Query = Query + ",vendedor='" + VENDEDOR+ "'";
            Query = Query + ",formapago='" + FORMAPAGO+ "'";

            Query = Query + ",tipopago='" + TIPOPAGO + "'";
            Query = Query + ",diascredito='" + DIASPAGO+ "'";
            Query = Query + ",nombrefactura='" + NOMBREFACTURA + "'";
            //
            Query = Query + ",activo='" + ACTIVO + "'";
            Query = Query + ",saldo='" + SALDO + "'";
            Query = Query + ",saldocredito='" + SALDOCREDITO + "'";
            //Query = Query + ",dcredito='" + DCREDITO + "'";
            Query = Query + ",monedero='" + MONEDERO + "'";


            Query = Query + " where cvcliente='" + CLAVE + "'";
            conecta.Excute(Query);


            //Query = "Update facturas set ";
            //Query=Query + " RENombre='" + NOMBRE + "'";
            //Query = Query + " ,ReRFC='" + RFC + "'";
            //Query = Query + " ,ReCalle='" + CALLEF + "'";
            //Query = Query + " ,ReCodpostal='" + CODF + "'";
            //Query = Query + " ,ReColonia='" + COLONIAF + "'";
            //Query = Query + " ,ReEstado='" + ESTADOF+ "'";
            //Query = Query + " ,ReLocalidad=''";
            //Query = Query + " ,ReMunicipio='" + MUNICIPIOF+ "'";
            //Query = Query + " ,ReNoExterior='" + NUMEXTERIOR+ "'";
            //Query = Query + " ,ReNoInterior=''";
            //Query = Query + " ,ReTel='" + TELEFONO+ "'";
            //Query = Query + " ,RePais='" + PAISF+ "'";
            //Query = Query + " ,Recorreo='" + CORREO+ "'";
            //Query = Query + " ,formapago='" + FORMAPAGO+ "'";
            //Query = Query + " where REclave='" + CLAVE + "' and ayo='" + DateTime.Now.Year + "' and mes='" + DateTime.Now.Month + "'";
            conecta.Excute(Query);
        }

        public void Guardar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "insert into clientes(";
            Query=Query + "cvcliente";
            Query=Query + ",nombre";
            Query=Query + ",telefono";
            Query=Query + ",email";
            Query=Query + ",email2";
            Query=Query + ",celular";
            Query=Query + ",direccion";
            Query=Query + ",rfc";
            Query=Query + ",direfiscal";
            Query=Query + ",empresa";
            Query = Query + ",calleE";
            Query = Query + ",ColoniaE";
            Query = Query + ",MunicipioE";
            Query = Query + ",EstadoE";
            Query = Query + ",CodE";
            Query = Query + ",PaisE";
            Query = Query + ",CalleF";
            Query = Query + ",ColoniaF";
            Query = Query + ",MunicipioF";
            Query = Query + ",EstadoF";
            Query = Query + ",CodF";
            Query = Query + ",PaisF";
            Query = Query + ",fechamod";
            Query = Query + ",fcodmod";
            Query = Query + ",sincronizado";
            Query = Query + ",actividad";
            Query = Query + ",numf";
            Query = Query + ",observafact";

            Query = Query + ",numcuenta";
            Query = Query + ",cvbanco";
            Query = Query + ",metodopago";
            Query = Query + ",vendedor";
            Query = Query + ",formapago";

            Query = Query + ",tipopago";
            Query = Query + ",diascredito";
            Query = Query + ",nombrefactura";
            Query = Query + ",factura";

            Query = Query + ",activo";
            Query = Query + ",saldo";
            //Query = Query + ",dcredito";
            Query = Query + ",saldocredito";
            Query = Query + ",monedero)";
            Query = Query + " values(";

            Query = Query + "'" + CLAVE + "'" ;
            Query = Query + ",'" + NOMBRE+ "'";
            Query = Query + ",'" + TELEFONO + "'";
            Query = Query + ",'" + CORREO + "'";
            Query = Query + ",'" + CORREO2 + "'";
            Query = Query + ",'" + CELULAR + "'";
            Query = Query + ",'" + DIRECCION + "'";
            Query = Query + ",'" + RFC + "'";
            Query = Query + ",'" + DFISCAL + "'";
            Query = Query + ",'" + EMPRESA + "'";

            Query = Query + ",'" + CALLEE + "'";
            Query = Query + ",'" + COLONIAE + "'";
            Query = Query + ",'" + MUNICIPIOE + "'";
            Query = Query + ",'" + ESTADOE + "'";
            Query = Query + ",'" + CODPE + "'";
            Query = Query + ",'" + PAISE + "'";
            Query = Query + ",'" + CALLEF + "'";
            Query = Query + ",'" + COLONIAF + "'";
            Query = Query + ",'" + MUNICIPIOF + "'";
            Query = Query + ",'" + ESTADOF + "'";
            Query = Query + ",'" + CODF + "'";
            Query = Query + ",'" + PAISF+ "'";
            Query = Query + ",'" + FECHAMOD + "'";
            Query = Query + ",'" + FCODMOD + "'";
            Query = Query + ",'" + SINCRONIZADO + "'";
            Query = Query + ",'" + ACTIVIDAD+ "'";
            Query = Query + ",'" + NUMEXTERIOR + "'";
            Query = Query + ",'" + OBSERVACIONESFACT+ "'";

            Query = Query + ",'" + NUMCUENTA + "'";
            Query = Query + ",'" + CVBANCO+ "'";
            Query = Query + ",'" + METODOPAGO+ "'";
            Query = Query + ",'" + VENDEDOR+ "'";
            Query = Query + ",'" + FORMAPAGO+ "'";

            Query = Query + ",'" + TIPOPAGO + "'";
            Query = Query + ",'" + DIASPAGO+ "'";
            Query = Query + ",'" + NOMBREFACTURA + "'";
            Query = Query + ",'" + FACTURA + "'";

            Query = Query + ",'" + ACTIVO + "'";
            Query = Query + "," + SALDO + "";
            //Query = Query + ",'" + DCREDITO + "'";
            Query = Query + "," + SALDOCREDITO + "";
            Query = Query + "," + MONEDERO + ")";
            conecta.Excute(Query);   
        }

        public bool ExisteInfo()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from clientes where cvcliente='" + CLAVE + "'";
            return conecta.ExisteRegistro(Query);
        }

        public void ActualizarConsecutivo()
        {
            int Numero = int.Parse(textBox1.Text);
            Numero++;
            conectorSql conecta = new conectorSql();
            string Query = "Update Consecutivos set numcliente='" + Numero.ToString() + "'";
            conecta.Excute(Query);
        }


        
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (ValidateData.IsValidEmail(textBox4.Text)==false)
            {
                MessageBox.Show("Correo electrónico nó valido, favor de verificar", "Error en formato de correo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Recolectar();
           
            if (Validacion() == true)
            {
                if (ExisteInfo() == false)
                {
                    Guardar();
                    if (BandConsecutivo == true) ActualizarConsecutivo();

                    MessageBox.Show("Se guardo correctamente la información registrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                   
                }
                else
                {
                    Actualizar();
                    MessageBox.Show("Se actualizo correctamente la información registrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                panel1.Visible = false;
                panel3.Visible = true;
                textBox11.Text = NOMBRE;
                CargarInfo();
            }
        }
        public bool BandModificar;
        private void Lv_DoubleClick(object sender, EventArgs e)
        {

            if (Lv.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = Lv.SelectedIndices;
                foreach (int item in seleccion)
                {
                    DetallesModifica(item);
                }
            }
        }
        public void DetallesModifica(int index)
        {
            BandModificar = true;
           
            button8.Enabled = false;
            textBox1.Text = Lv.Items[index].Text;
            BuscarInformacion(textBox1.Text);
            textBox1.Enabled = false;
            panel1.Visible = true;
            panel3.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("¿Desea eliminar la información seleccionada?", "Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;

            conectorSql conecta = new conectorSql();
            string Query = "";
            int contar = 0;
            for (int i = 0; i < Lv.Items.Count; i++)
            {
                string clave=Lv.Items[i].Text;
                if (Lv.Items[i].Checked == true)
                {
                    Query = "Delete from clientes where cvcliente='" + clave + "'";
                    conecta.Excute(Query);
                    contar++;
                }
            }

            MessageBox.Show("Se eliminaron " + contar.ToString() + " registros del sistema", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("¿Desea exportar la información a excel?", "Exportación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;

            ReportesNKB.RBusquedaClientes(textBox11.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            BandConsecutivo = true;
            textBox1.Text = numconsecutivo();
            textBox1.Enabled = false;
            textBox2.Focus();
        }

        public string numconsecutivo()
        {
            string Numero = "1";
            conectorSql conecta = new conectorSql();
            string Query = "Select numcliente from consecutivos where numcliente<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                Numero = leer["numcliente"].ToString();
            }
            conecta.CierraConexion();

            return Numero;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox7.Text = textBox21.Text;
            textBox12.Text = textBox20.Text;
            textBox13.Text = textBox19.Text;
            textBox14.Text = textBox18.Text;
            textBox15.Text = textBox17.Text;
            textBox16.Text = textBox9.Text;
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) CargarInfo();
        }

        private void Lv_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Sorter s = (Sorter)Lv.ListViewItemSorter;
            s.Column = e.Column;

            if (s.Order == System.Windows.Forms.SortOrder.Ascending)
            {
                s.Order = System.Windows.Forms.SortOrder.Descending;
            }
            else
            {
                s.Order = System.Windows.Forms.SortOrder.Ascending;
            }
            Lv.Sort(); 
        }

        private void button10_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            textBox8.Text = ".";
            textBox21.Text = ".";
            textBox20.Text = ".";
            textBox19.Text = ".";
            textBox18.Text = ".";
            textBox17.Text = "0";
            button9_Click(sender, e);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) BuscarInformacion(textBox1.Text);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CatBancos bancos = new CatBancos();
            bancos.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            CatFormasdepago forma = new CatFormasdepago();
            forma.Show();
        }

        private void comboBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            combos.ComboFormadePago(comboBox3);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

