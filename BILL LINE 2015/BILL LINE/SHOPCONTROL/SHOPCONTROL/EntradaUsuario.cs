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

        public bool EvaluaUsuario()
        {
            bool existe = false;
            conectorSql conecta = new conectorSql();
            string Query = "Select * from usuarios where cvusuario='" + USUARIO + "'";
            existe = conecta.ExisteRegistro(Query);
            return existe;
        }

        public bool EntrarUsuarioContra()
        {
            bool existe = false;
            valoresg.Area_Contra = "";
            valoresg.Area_Cvdoctor = "";
            valoresg.Area_usuario = "";
            conectorSql conecta = new conectorSql();
            string Query = "Select * from usuarios where cvusuario='" + USUARIO + "' and contra='" + CONTRASEÑA + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                existe = true;
                NOMBRECOMPLETO = leer["nombre"].ToString();
                valoresg.Area_Contra = CONTRASEÑA;
                valoresg.Area_Cvdoctor= leer["cvdoctor"].ToString();
                valoresg.Area_usuario = USUARIO;

            }
            conecta.CierraConexion();
            return existe;
        }


        public void Recolecta()
        {
            USUARIO = textBox1.Text;
            CONTRASEÑA = textBox2.Text;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
            CTablas.TablaUsuario();
            Recolecta();
            valoresg.USUARIOSIS = textBox1.Text;
            if (EvaluaUsuario())
            {
                if (EntrarUsuarioContra())
                {
                    Modremision.EMITE = USUARIO;
                    Modremision.NOMBREACCEDE = NOMBRECOMPLETO;
                    Modremision.SERVER = Registro.ReadRegSHOPCONTROL("CON", "Server");
                    Modremision.USUARIO = Registro.ReadRegSHOPCONTROL("CON", "User");
                    Modremision.CONTRASEÑA = Registro.ReadRegSHOPCONTROL("CON", "Pass");
                    Modremision.BASEDATOS = Registro.ReadRegSHOPCONTROL("CON", "BD");

                    Bienvenidos pantallainicial = new Bienvenidos();
                    pantallainicial.Show();
                    this.Hide();
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
                Registro.CreateRegSHOPCONTROL();
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
    }
}
