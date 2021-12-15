using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Data.SqlClient;
namespace SHOPCONTROL
{
    public partial class LicenciaRegistro : Form
    {
        public LicenciaRegistro()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void LicenciaRegistro_Load(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from parametros where modelosis<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox5.Text = leer["nombrecliente"].ToString();
                textBox6.Text = leer["notificarcorreo"].ToString();
                textBox1.Text = leer["modelosis"].ToString();
                textBox8.Text = leer["usuariofolio"].ToString();
                textBox9.Text = leer["contrafolio"].ToString();
            }
            conecta.CierraConexion();
        }

        public void EnviarCorreo()
        {
            string Cadena = "Solicitud de licencia \n\n\n";
            Cadena = Cadena + "Nombre de Cliente; " + textBox5.Text.ToUpper() + "\n";
            Cadena = Cadena + "Correo de Cliente: " + textBox6.Text + "\n";
            Cadena = Cadena + "Fecha de Envio; " +DateTime.Now.ToString("dd/MM/yyyy")+ "\n\n";
            Cadena = Cadena + "Serie Registrada; " + textBox1.Text + "\n";
            Cadena = Cadena + "Cantidad de Timbres: " + textBox2.Text + "\n";

         MailMessage objEmail = new MailMessage();
        objEmail.From = new MailAddress("ventas@soluciones-sia.com","Solicitud de Licencia por Timbrado");
        objEmail.ReplyTo = new MailAddress("ventas@soluciones-sia.com");
        //Destinatario
        objEmail.To.Add("ventas@soluciones-sia.com");
      
       
        objEmail.Priority = MailPriority.Normal;
        objEmail.Subject = "Licencia Bill Line " + textBox2.Text;
        objEmail.Body = Cadena;
        SmtpClient objSmtp = new SmtpClient();
        objSmtp.Host = "mail.soluciones-sia.com ";
        objSmtp.Port = 587;
        objSmtp.Credentials = new System.Net.NetworkCredential("ventas@soluciones-sia.com", "Nkbsia123");
        objSmtp.Send(objEmail);

        conectorSql conecta = new conectorSql();
        string Query = "Update parametros set nombrecliente='" + textBox5.Text.ToUpper() + "'";
        Query = Query + " ,notificarcorreo='" + textBox6.Text + "'";
        Query = Query + " ,sistema='" + textBox3.Text + "'";
        Query = Query + " ,usuarioFolio='" + textBox8.Text + "'";
        Query = Query + " ,contrafolio='" + textBox9.Text + "'";
        conecta.Excute(Query);

        MessageBox.Show("Se envio correctamente la información solciitada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool conectadoInternet=InternetDisponible.IsConnectedToInternet();
            if (conectadoInternet == false)
            {
                MessageBox.Show("No tiene conexion a internet verifique para realizar esta operación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            EnviarCorreo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Licenciamiento cantidad = new Licenciamiento();
           int totalLicencia= cantidad.SaberCuantosTimbres(textBox3.Text);
            int cantimbres=int.Parse(textBox2.Text);
            if (totalLicencia >= cantimbres)
            {
                conectorSql conecta = new conectorSql();
                string Query = "update parametros set sistema='" + textBox3.Text + "'";
                conecta.Excute(Query);

                Query = "Select * from LlavesSistema where cvllave=''";
                bool existevacio = conecta.ExisteRegistro(Query);
                if (existevacio == true)
                { 
                    Query="Insert into LlavesSistema(cvllave,fechacod) values('" + textBox3.Text + "','" + DateTime.Now.ToString("yyyyMMdd") + "')";
                    conecta.Excute(Query);
                }

                MessageBox.Show("Se libero correctamente la licencia del sistema", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Licenciamiento licencia = new Licenciamiento();
            textBox7.Text= licencia.LicenciaFinal(int.Parse(textBox2.Text));
        }


    }
}
