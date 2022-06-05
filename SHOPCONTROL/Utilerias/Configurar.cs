using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;


public partial class Configurar : Form
    {
        private string NServidor = "";
        private string NUsuario = "";
        private string NPassword = "";
        private bool Integridad = false;
        private string NBaseDatos = "";
        private string CadenaConexion = "";

        private static SqlConnection con;
        private static SqlCommand comm;
        private static SqlConnection conCad;


        private void ValoresPred()
        {
            NUsuario = "sa";
            NPassword = "SIA123";
            Integridad = false;

            checkBox1.Checked = false;
            textBox1.Text = NUsuario;
            textBox2.Text = NPassword;
        }

        private bool Abrirconexion(string Cadena)
        {
            try
            {
                con = new SqlConnection();
                con.ConnectionString = Cadena;
                con.Open();
                con.Close();
                return true;
            }
            catch (SqlException exx)
            {
                 MessageBox.Show("Error en la conexion a la base de datos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return false;
            }
        }

        private void CierraConexion()
        {
            con.Close();
        }

        private bool Excute(String Ejecuta)
        {
            try
            {
                Abrirconexion(CadenaConexion);
                comm = new SqlCommand(Ejecuta, con);
                comm.ExecuteNonQuery();
                CierraConexion();
                return true;
            }
            catch (SqlException exx)
            {
                 MessageBox.Show("Error en la conexion a la base de datos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public Configurar()
        {
            InitializeComponent();
        }

        private void RecolectaInfo()
        {
            NServidor = textBox3.Text.Trim();
            NUsuario = textBox1.Text.Trim();
            NPassword = textBox2.Text.Trim();
            if (checkBox1.Checked == false)
                Integridad = false;
            else
                Integridad = true;

            NBaseDatos = textBox4.Text.Trim();
        }

        private bool Validacion()
        {
            if (NServidor == "")
            {
                MessageBox.Show("Ingrese el Nombre o IP del servidor", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (Integridad == false)
            {
                if (NUsuario == "")
                {
                    MessageBox.Show("Ingrese el usuario de la base datos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (NPassword == "")
                {
                    MessageBox.Show("Ingrese el password del usuario de la base de datos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            RecolectaInfo();
            if (Validacion())
            {
                if (Integridad)
                    CadenaConexion = "Data Source=" + NServidor + ";Integrated Security=True";
                else
                    CadenaConexion = "Data Source=" + NServidor + ";user id = " +NUsuario + "; password = "+NPassword;

            }
            
            Conexion miprueba = new Conexion();
            bool exito= miprueba.PruebaConexionCadena(CadenaConexion);
            if (exito)
            {
                MessageBox.Show("La conexion al server fue exitosa!!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox4.Enabled = true;
                button2.Enabled = true;
            }
            else
        {
            MessageBox.Show("La conexion al server no fue exitosa!!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        }


        private void GuardarInfo()
        {

        if (comboBox2.SelectedIndex == 0) Registro.WriteRegSHOPCONTROL("CON", "OPCIONSERVER", "0");
        if (comboBox2.SelectedIndex == 1) Registro.WriteRegSHOPCONTROL("CON", "OPCIONSERVER", "1");
        if (comboBox2.SelectedIndex == 2) Registro.WriteRegSHOPCONTROL("CON", "OPCIONSERVER", "2");

        Registro.WriteRegSHOPCONTROL("CON", "Server", NServidor);
        Registro.WriteRegSHOPCONTROL("CON", "BD", NBaseDatos);

         if (comboBox2.SelectedIndex == 0) 
         {
            Registro.WriteRegSHOPCONTROL("CONCUERNAVACA", "Server", NServidor);
            Registro.WriteRegSHOPCONTROL("CONCUERNAVACA", "BD", NBaseDatos);
         }

        if (comboBox2.SelectedIndex == 1)
        {
            Registro.WriteRegSHOPCONTROL("CONMOLINA", "Server", NServidor);
            Registro.WriteRegSHOPCONTROL("CONMOLINA", "BD", NBaseDatos);
        }

        if (comboBox2.SelectedIndex == 2)
        {
            Registro.WriteRegSHOPCONTROL("CONBELLASARTES", "Server", NServidor);
            Registro.WriteRegSHOPCONTROL("CONBELLASARTES", "BD", NBaseDatos);
        }



        if (Integridad)
            {
                Registro.WriteRegSHOPCONTROL("CON", "Integrada", "1");
            if (comboBox2.SelectedIndex == 0) Registro.WriteRegSHOPCONTROL("CONCUERNAVACA", "Integrada", "1");
            if (comboBox2.SelectedIndex == 1) Registro.WriteRegSHOPCONTROL("CONMOLINA", "Integrada", "1");
            if (comboBox2.SelectedIndex == 2) Registro.WriteRegSHOPCONTROL("CONBELLASARTES", "Integrada", "1");

        }
        else
            {
                Registro.WriteRegSHOPCONTROL("CON", "Integrada", "0");
                Registro.WriteRegSHOPCONTROL("CON", "User", NUsuario);
                Registro.WriteRegSHOPCONTROL("CON", "Pass", NPassword);


            if (comboBox2.SelectedIndex == 0)
            {
                Registro.WriteRegSHOPCONTROL("CONCUERNAVACA", "Integrada", "0");
                Registro.WriteRegSHOPCONTROL("CONCUERNAVACA", "User", NUsuario);
                Registro.WriteRegSHOPCONTROL("CONCUERNAVACA", "Pass", NPassword);
            }
            if (comboBox2.SelectedIndex == 1)
            {
                Registro.WriteRegSHOPCONTROL("CONMOLINA", "Integrada", "0");
                Registro.WriteRegSHOPCONTROL("CONMOLINA", "User", NUsuario);
                Registro.WriteRegSHOPCONTROL("CONMOLINA", "Pass", NPassword);
            }

            if (comboBox2.SelectedIndex == 2)
            {
                Registro.WriteRegSHOPCONTROL("CONBELLASARTES", "Integrada", "0");
                Registro.WriteRegSHOPCONTROL("CONBELLASARTES", "User", NUsuario);
                Registro.WriteRegSHOPCONTROL("CONBELLASARTES", "Pass", NPassword);
            }

        }
        Registro.WriteRegSHOPCONTROL("CON", "CCliente", CadenaConexion);


            if (comboBox2.SelectedIndex==0) Registro.WriteRegSHOPCONTROL("CONCUERNAVACA", "CCliente", CadenaConexion);
            if (comboBox2.SelectedIndex == 1) Registro.WriteRegSHOPCONTROL("CONMOLINA", "CCliente", CadenaConexion);
            if (comboBox2.SelectedIndex == 2) Registro.WriteRegSHOPCONTROL("CONBELLASARTES", "CCliente", CadenaConexion);
    }

    private void TraerValores()
        {
            try
            {
                string opcionserver = Registro.ReadRegSHOPCONTROL("CON", "OPCIONSERVER");

            if (opcionserver == "0") comboBox2.SelectedIndex = 0;
            if (opcionserver == "1") comboBox2.SelectedIndex = 1;
            if (opcionserver == "2") comboBox2.SelectedIndex = 2;

            string vserver = Registro.ReadRegSHOPCONTROL("CON", "Server");
                string miValor = Registro.ReadRegSHOPCONTROL("CON", "CCliente");
                string vusuario = "";
                string vpass = "";
                string vintegra = Registro.ReadRegSHOPCONTROL("CON", "Integrada");
                if (vintegra == "0")
                {
                    vusuario = Registro.ReadRegSHOPCONTROL("CON", "User");
                    vpass = Registro.ReadRegSHOPCONTROL("CON", "Pass");
                }
            string vbase = Registro.ReadRegSHOPCONTROL("CON", "BD");

            if (opcionserver=="0")
            {
                vserver = Registro.ReadRegSHOPCONTROL("CONCUERNAVACA", "Server");
                miValor = Registro.ReadRegSHOPCONTROL("CONCUERNAVACA", "CCliente");
                vusuario = "";
                vpass = "";
                vintegra = Registro.ReadRegSHOPCONTROL("CONCUERNAVACA", "Integrada");
                if (vintegra == "0")
                {
                    vusuario = Registro.ReadRegSHOPCONTROL("CONCUERNAVACA", "User");
                    vpass = Registro.ReadRegSHOPCONTROL("CONCUERNAVACA", "Pass");
                }
                vbase = Registro.ReadRegSHOPCONTROL("CONCUERNAVACA", "BD");
            }

            if (opcionserver == "1")
            {
                vserver = Registro.ReadRegSHOPCONTROL("CONMOLINA", "Server");
                miValor = Registro.ReadRegSHOPCONTROL("CONMOLINA", "CCliente");
                vusuario = "";
                vpass = "";
                vintegra = Registro.ReadRegSHOPCONTROL("CONMOLINA", "Integrada");
                if (vintegra == "0")
                {
                    vusuario = Registro.ReadRegSHOPCONTROL("CONMOLINA", "User");
                    vpass = Registro.ReadRegSHOPCONTROL("CONMOLINA", "Pass");
                }
                vbase = Registro.ReadRegSHOPCONTROL("CONMOLINA", "BD");
            }

            if (opcionserver == "2")
            {
                vserver = Registro.ReadRegSHOPCONTROL("CONBELLASARTES", "Server");
                miValor = Registro.ReadRegSHOPCONTROL("CONBELLASARTES", "CCliente");
                vusuario = "";
                vpass = "";
                vintegra = Registro.ReadRegSHOPCONTROL("CONBELLASARTES", "Integrada");
                if (vintegra == "0")
                {
                    vusuario = Registro.ReadRegSHOPCONTROL("CONBELLASARTES", "User");
                    vpass = Registro.ReadRegSHOPCONTROL("CONBELLASARTES", "Pass");
                }
                vbase = Registro.ReadRegSHOPCONTROL("CONBELLASARTES", "BD");
            }

            textBox3.Text = vserver;
                textBox4.Text = vbase;
                if (vintegra == "0")
                {
                    checkBox1.Checked = false;
                    textBox2.Text = vpass;
                    textBox1.Text = vusuario;
                }
                else
                {
                    checkBox1.Checked = true;
                }

            }catch(Exception e)
            {
                Limpiar();
            }
        
        }
        private void Limpiar()
        {
            checkBox1.Checked = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

         private void button2_Click(object sender, EventArgs e)
        {
            try
            {

            RecolectaInfo();
            if (Validacion())
            {

                if (NBaseDatos == "")
                {
                    MessageBox.Show("Ingrese el nombre de la base datos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return ;
                }

                if (Integridad)
                    CadenaConexion = "Data Source=" + NServidor + "; initial catalog = "+ NBaseDatos + ";Integrated Security=True";
                else
                    CadenaConexion = "Data Source=" + NServidor + ";initial catalog = " + NBaseDatos +";user id = " + NUsuario + "; password = " + NPassword;

            }
            
            bool exito=Abrirconexion(CadenaConexion);
            if (exito)
            {
                // MessageBox.Show("La conexion fue exitosa !!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                GuardarInfo();
                this.Dispose();
            }
            else
            {
                DialogResult reply = MessageBox.Show("¿Desea crear la base de datos ?", "Base de datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (reply == DialogResult.No)
                    return;
                CBaseD.CrearBase(NBaseDatos, NServidor);
                
                MessageBox.Show("Se creo correctamente la base de datos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Se guardo correctamente su configuracion!!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            GuardarInfo();
          }

        private void Configurar_Load(object sender, EventArgs e)
        {
            Limpiar();
            TraerValores();

            if (textBox3.Text.Trim() == "")
            {
                string hostname = Dns.GetHostName();
                textBox3.Text = hostname + "\\SQLEXPRESS";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBox2.Text!="") BusquedaConfiguracion(comboBox2.SelectedIndex.ToString());
    }

    public void BusquedaConfiguracion(string opcionserver)
    {
        string vserver = "";
        string miValor = "";
        string vusuario = "";
        string vpass = "";
        string vintegra = "";
        string vbase = "";
        try
        {


            if (opcionserver == "0")
            {
                vserver = Registro.ReadRegSHOPCONTROL("CONCUERNAVACA", "Server");
                miValor = Registro.ReadRegSHOPCONTROL("CONCUERNAVACA", "CCliente");
                vusuario = "";
                vpass = "";
                vintegra = Registro.ReadRegSHOPCONTROL("CONCUERNAVACA", "Integrada");
                if (vintegra == "0")
                {
                    vusuario = Registro.ReadRegSHOPCONTROL("CONCUERNAVACA", "User");
                    vpass = Registro.ReadRegSHOPCONTROL("CONCUERNAVACA", "Pass");
                }
                vbase = Registro.ReadRegSHOPCONTROL("CONCUERNAVACA", "BD");
            }

            if (opcionserver == "1")
            {
                vserver = Registro.ReadRegSHOPCONTROL("CONMOLINA", "Server");
                miValor = Registro.ReadRegSHOPCONTROL("CONMOLINA", "CCliente");
                vusuario = "";
                vpass = "";
                vintegra = Registro.ReadRegSHOPCONTROL("CONMOLINA", "Integrada");
                if (vintegra == "0")
                {
                    vusuario = Registro.ReadRegSHOPCONTROL("CONMOLINA", "User");
                    vpass = Registro.ReadRegSHOPCONTROL("CONMOLINA", "Pass");
                }
                vbase = Registro.ReadRegSHOPCONTROL("CONMOLINA", "BD");
            }

            if (opcionserver == "2")
            {
                vserver = Registro.ReadRegSHOPCONTROL("CONBELLASARTES", "Server");
                miValor = Registro.ReadRegSHOPCONTROL("CONBELLASARTES", "CCliente");
                vusuario = "";
                vpass = "";
                vintegra = Registro.ReadRegSHOPCONTROL("CONBELLASARTES", "Integrada");
                if (vintegra == "0")
                {
                    vusuario = Registro.ReadRegSHOPCONTROL("CONBELLASARTES", "User");
                    vpass = Registro.ReadRegSHOPCONTROL("CONBELLASARTES", "Pass");
                }
                vbase = Registro.ReadRegSHOPCONTROL("CONBELLASARTES", "BD");
            }

            textBox3.Text = vserver;
            textBox4.Text = vbase;
            if (vintegra == "0")
            {
                checkBox1.Checked = false;
                textBox2.Text = vpass;
                textBox1.Text = vusuario;
            }
            else
            {
                checkBox1.Checked = true;
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show("No existe configuracion para el servidor seleccionado", "SAIMED", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
    
    
}
}
