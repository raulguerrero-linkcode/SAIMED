using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SHOPCONTROL
{
    public partial class Empresa : Form
    {

        public Empresa()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public string CLAVE = "";
        public string NOMBRE = "";
        public string EMPRESA = "";
        public string CORREO = "";
        public string CORREO2 = "";
        public string TELEFONO = "";
        public string CELULAR = "";
        public string DIRECCION = "";
        public string FACTURA = "";
        public string RFC = "";
        public string DFISCAL = "";

        public string CALLEE = "";
        public string COLONIAE = "";
        public string MUNICIPIOE = "";
        public string ESTADOE = "";
        public string CODPE = "";
        public string PAISE = "";
        public string CALLEF = "";
        public string COLONIAF = "";
        public string MUNICIPIOF = "";
        public string ESTADOF = "";
        public string CODF = "";
        public string PAISF = "";

        public string FECHAMOD = "";
        public string FCODMOD = "";
        public string SINCRONIZADO = "";
        public string ACTIVIDAD = "";
        public bool BandConsecutivo;
        public void Recolectar()
        {
            textBox1.Text = "1";
            CLAVE = textBox1.Text;
            NOMBRE = textBox2.Text;
            EMPRESA = textBox10.Text;
            CORREO = textBox3.Text;
            CORREO2 = textBox4.Text;
            TELEFONO = textBox5.Text;
            CELULAR = textBox6.Text;

            CALLEE = textBox7.Text;
            COLONIAE = textBox12.Text;
            MUNICIPIOE = textBox13.Text;
            ESTADOE = textBox14.Text;
            CODPE = textBox15.Text;
            PAISE = textBox16.Text;

            DIRECCION = "CALLE " + CALLEE + " COL. " + COLONIAE + " C.P " + CODPE + " " + MUNICIPIOE + " " + ESTADOE + "," + PAISE;
            FACTURA = "NO";


            if (radioButton1.Checked == true) FACTURA = "SI";
            RFC = textBox8.Text;

            CALLEF = textBox21.Text;
            COLONIAF = textBox20.Text;
            MUNICIPIOF = textBox19.Text;
            ESTADOF = textBox18.Text;
            CODF = textBox17.Text;
            PAISF = textBox9.Text;
            DFISCAL = "CALLE " + CALLEF + " COL. " + COLONIAF + " C.P " + CODF + " " + MUNICIPIOF + " " + ESTADOF + "," + PAISF;
            FECHAMOD = DateTime.Now.ToString("dd/MM/yyyy");
            FCODMOD = DateTime.Now.ToString("yyyyMMdd");
            SINCRONIZADO = "0";
            ACTIVIDAD = comboBox1.Text;
        }

        public bool Validacion()
        {
            if (NOMBRE == "")
            {
                MessageBox.Show("Ingrese el nombre completo del cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return false;
            }

            if (CORREO == "")
            {
                MessageBox.Show("Ingrese el correo electronico del cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return false;
            }

            if (TELEFONO == "")
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

            if (CALLEF == "")
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
            return true;
        }

        public void Guardar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "";

            Query = "Delete from empresa where cvempresa='1'";
            conecta.Excute(Query);

            Query = "insert into empresa(";
            Query = Query + "cvempresa";
            Query = Query + ",nombre";
            Query = Query + ",telefono";
            Query = Query + ",email";
            Query = Query + ",email2";
            Query = Query + ",celular";
            Query = Query + ",direccion";
            Query = Query + ",rfc";
            Query = Query + ",direfiscal";
            Query = Query + ",empresa";
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
            Query = Query + ",factura)";
            Query = Query + " values(";

            Query = Query + "'" + CLAVE + "'";
            Query = Query + ",'" + NOMBRE + "'";
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
            Query = Query + ",'" + PAISF + "'";
            Query = Query + ",'" + FECHAMOD + "'";
            Query = Query + ",'" + FCODMOD + "'";
            Query = Query + ",'" + SINCRONIZADO + "'";
            Query = Query + ",'" + ACTIVIDAD + "'";
            Query = Query + ",'" + FACTURA + "')";
            conecta.Excute(Query);
        }

        private void button1_Click(object sender, EventArgs e)
        {
             Recolectar();

             if (Validacion() == true)
             {            
               Guardar();
               MessageBox.Show("Se guardo correctamente la información", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
              
              }             
        }

        public void BuscarInformacion()
        {
            Limpiar();

            conectorSql conecta = new conectorSql();
            string Query = "Select * from empresa where cvempresa='1'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox1.Text = "1";
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


                textBox21.Text = leer["calleF"].ToString();
                textBox20.Text = leer["ColoniaF"].ToString();
                textBox19.Text = leer["MunicipioF"].ToString();
                textBox18.Text = leer["EstadoF"].ToString();
                textBox17.Text = leer["CodF"].ToString();
                textBox9.Text = leer["PaisF"].ToString();

                comboBox1.Text = leer["actividad"].ToString();

                radioButton1.Checked = false;
                radioButton2.Checked = false;
                string valor = leer["factura"].ToString();
                if (valor == "SI") radioButton1.Checked = true;
                else radioButton2.Checked = true;
            }
            conecta.CierraConexion();
        }

        public void Limpiar()
        {
        
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
           
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

            radioButton1.Checked = false;
            radioButton2.Checked = true;
            textBox2.Focus();
        }

        private void Empresa_Load(object sender, EventArgs e)
        {
            BuscarInformacion();
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
        {
            textBox7.Text = textBox21.Text;
            textBox12.Text = textBox20.Text;
            textBox13.Text = textBox19.Text;
            textBox14.Text = textBox18.Text;
            textBox15.Text = textBox17.Text;
            textBox16.Text = textBox9.Text;
        }

   
    }
}
