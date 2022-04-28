using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Linq;
using System.IO;

namespace SHOPCONTROL
{
    public partial class EntradaUsuario : Form
    {
        public EntradaUsuario()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Configurar configuraven = new Configurar();
            configuraven.Show();
        }

        public string USUARIO = "";
        public string CONTRASEÑA = "";
        public string NOMBRECOMPLETO = "";
        public string LocationSrv = "";
        public string CVDOCTORAREA = "0";

        public bool EvaluaUsuario()
        {
            bool existe = false;
            conectorSql conecta = new conectorSql();
            // string Query = "Select * from usuarios where cvusuario='" + USUARIO + "'";
            string Query = "Select IdEmployee from EmployeeCredentials where IdEmployee=" + USUARIO + "";
            existe = conecta.ExisteRegistro(Query);
            return existe;
        }

        
        public bool EntrarUsuarioContra()
        {

            bool existe = false;
            valoresg.Area_Contra = "";
            valoresg.Area_Cvdoctor = "";
            valoresg.Area_usuario = "";

            // string Decoded_Password = SecurityUsr.Base64Encode(CONTRASEÑA);

            conectorSql conecta = new conectorSql();
            // string Query = "Select * from usuarios where cvusuario='" + USUARIO + "' and contra='" + CONTRASEÑA + "'";
            string Query = "SELECT IdEmployee,Name as nombre,FirstLastName,SecondLastName,Age,Email,Role,usr.contra,usr.cvdoctor FROM EmployeeCredentials ec left join Usuarios usr on ec.Role = usr.cvusuario where IdEmployee =" + USUARIO ;
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                existe = true;
                NOMBRECOMPLETO = leer["nombre"].ToString() + ' ' + leer["FirstLastName"].ToString();
                CVDOCTORAREA= leer["cvdoctor"].ToString();
                valoresg.Area_Contra = leer["contra"].ToString();
                valoresg.Area_Cvdoctor= leer["cvdoctor"].ToString();
                valoresg.Area_usuario = USUARIO;
                valoresg.USUARIOSIS = leer["Role"].ToString(); ;
                valoresg.UBICACION = comboBox2.Text;
                valoresg.Nombre_Completo = leer["nombre"].ToString() + ' ' + leer["FirstLastName"].ToString(); 
                valoresg.IdEmployee = leer["IdEmployee"].ToString();

                MessageBox.Show("Se autoriza el acceso a " + NOMBRECOMPLETO," Ingreso exitoso " , MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            conecta.CierraConexion();
            /*
             * 
             * Send Email Notification it could be used when the user tried to log with ADMIN Account
             * 
            */
            MailNotifications mail = new MailNotifications();
            mail.SendMail(valoresg.USUARIOSIS, valoresg.UBICACION);
            return existe;
        }


        public void Recolecta()
        {
            USUARIO = textBox1.Text;
            CONTRASEÑA = textBox2.Text;
            string cfnFile = "//SRV-DATACENTER/tmp/EmailConf.xml";
            bool cfnExist = File.Exists(cfnFile);
            XDocument xdoc = XDocument.Load(cfnExist ? "//SRV-DATACENTER/tmp/EmailConf.xml" : "C:\\tmp\\EmailConf.xml");

            //XDocument xdoc = XDocument.Load("./EmailConf.xml");
            LocationSrv = xdoc.Descendants("CurrentLocation").First().Value;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            /*
             * Validate usr on employees table 
             *  Once validate employees the application will take the role and permissions
             *  EX.  202070 is Raul Guerrero and his Role is ADMIN
            */

            /*if (comboBox2.Text.Length==0)
            {
                MessageBox.Show("Se debe seleccionar una clínica", "Seleccion de clínica", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return;
            }
            */
            
            
            try
            {
                string opcionserver = Registro.ReadRegSHOPCONTROL("CON", "OPCIONSERVER");
                opcionserver = "0";
                comboBox2.Text = LocationSrv;
                // if (opcionserver == "0") comboBox2.Text = "CUERNAVACA";
                // if (opcionserver == "1") comboBox2.Text = "MOLINA";
                // if (opcionserver == "2") comboBox2.Text = "BELLAS ARTES";

                //CTablas.TablaUsuario();
            Recolecta();
            

                if (EvaluaUsuario())
            {
                if (EntrarUsuarioContra())
                {
                    Modremision.EMITE = valoresg.USUARIOSIS;
                    Modremision.NOMBREACCEDE = NOMBRECOMPLETO;
                    Modremision.SERVER = LocationSrv; //Registro.ReadRegSHOPCONTROL("CON", "Server");
                    Modremision.USUARIO = Registro.ReadRegSHOPCONTROL("CON", "User");
                    Modremision.CONTRASEÑA = Registro.ReadRegSHOPCONTROL("CON", "Pass");
                    Modremision.BASEDATOS = Registro.ReadRegSHOPCONTROL("CON", "BD");

                       if (CVDOCTORAREA=="0")
                        {
                            Bienvenidos pantallainicial = new Bienvenidos();
                            pantallainicial.Text = "Bienvenido(a) " + NOMBRECOMPLETO;
                            pantallainicial.Show();
                            this.Hide();
                        }
                       else
                        {
                            SHOPCONTROL.HistorialClinica.NotasEvolucion nota = new HistorialClinica.NotasEvolucion();
                            nota.Show();
                            this.Hide();
                        }
                    }
                else
                {
                    MessageBox.Show("La contraseña es incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Focus();
                }

            }
            else
            {
                MessageBox.Show("El usuario ingresado no existe", "No existe usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
            }
            catch (Exception)
            {
                CTablas.TablaUsuario();
                MessageBox.Show("vuelva a intenta ingresar con ADMIN", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button2_Click(sender, e);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox2.Focus();
        }

        private void EntradaUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                // Registro.CreateRegSHOPCONTROL();
                // string opcionserver = Registro.ReadRegSHOPCONTROL("CON", "OPCIONSERVER");
                // if (opcionserver == "0") comboBox2.Text = "CUERNAVACA";
                // if (opcionserver == "1") comboBox2.Text = "MOLINA";
                // if (opcionserver == "2") comboBox2.Text = "BELLAS ARTES";
                Informacion();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                  
            }
            textBox1.Focus();

        }
        public void Informacion()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from ParametrosRecibo where NombreComercial<>''";
            SqlDataReader leer = conecta.RecordInfo(Query) ;
            while (leer.Read())
            {
                label7.Text = leer["NombreComercial"].ToString();
                label8.Text = leer["InfoAdicional"].ToString();
            }
            conecta.CierraConexion();
            pictureBox2.Image= ClaseFotos.ConsultarFotoEmpresa("0");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text!="")
            {
                if (comboBox2.SelectedIndex == 0) Registro.WriteRegSHOPCONTROL("CON", "OPCIONSERVER", "0");
                if (comboBox2.SelectedIndex == 1) Registro.WriteRegSHOPCONTROL("CON", "OPCIONSERVER", "1");
                if (comboBox2.SelectedIndex == 2) Registro.WriteRegSHOPCONTROL("CON", "OPCIONSERVER", "2");

                Informacion();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
