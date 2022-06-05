using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace SHOPCONTROL
{
    public partial class CorreoElectronico : Form
    {
        public string NOMBRE;
        public string CORREO;
        public string CONTRASEÑA;
        public string PUERTO;
        public string SALIDASMTP;
        public string CIFRADA;
        public string CUERPO;


        public CorreoElectronico()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Recolectar();
            if (ExisteInfo() == false)
            {
                GuardarFacturacion();
                MessageBox.Show("Se guardo correctamente la información de correo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ActualizarFacturacion();
                MessageBox.Show("Se actualizo correctamente la información de correo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


      
        public bool ExisteInfo()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from configuracorreo where nombre<>''";
            return conecta.ExisteRegistro(Query);
        }

        public void GuardarFacturacion()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Insert into configuracorreo(correo,nombre,contraseña,puerto,ssl,Cuerpo,salidasmtp";
            Query = Query + ") values(";           
            Query = Query + "'"  + CORREO +  "'";
            Query = Query + ",'" + NOMBRE+ "'";
            Query = Query + ",'" + CONTRASEÑA + "'";
            Query = Query + ",'" + PUERTO+ "'";
            Query = Query + ",'" + CIFRADA+ "'";
            Query = Query + ",'" + CUERPO+ "'";
            Query = Query + ",'" + SALIDASMTP+ "')";

            conecta.Excute(Query);

        }

        public void ActualizarFacturacion()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Update configuracorreo set ";
            Query = Query + " nombre='" + NOMBRE+ "'";
            Query = Query + ", correo='" + CORREO+ "'";
            Query = Query + ", contraseña='" + CONTRASEÑA+ "'";
            Query = Query + ", puerto='" + PUERTO+ "'";
            Query = Query + ", ssl='" + CIFRADA+ "'";
            Query = Query + ", Cuerpo='" + CUERPO+ "'";
            Query = Query + ", salidasmtp='" + SALIDASMTP + "'";
            
            conecta.Excute(Query);

        }


        public void Recolectar()
        {
            NOMBRE = textBox18.Text;
            CORREO = textBox1.Text;
            CONTRASEÑA = textBox2.Text;
            SALIDASMTP = textBox4.Text;
            PUERTO = textBox3.Text;
            CIFRADA = "NO";
            if (radioButton1.Checked == true) CIFRADA = "SI";
            CUERPO = textBox5.Text;
        }

        private void FacElectronica_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;

            conectorSql conecta = new conectorSql();
            string Query = "Select * from configuracorreo ";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox18.Text = leer["nombre"].ToString();
                textBox1.Text = leer["correo"].ToString();
                textBox2.Text = leer["contraseña"].ToString();
                textBox4.Text = leer["salidasmtp"].ToString();
                textBox3.Text = leer["puerto"].ToString();
                textBox5.Text = leer["cuerpo"].ToString();
                CIFRADA = leer["ssl"].ToString();

                if (CIFRADA == "SI") radioButton1.Checked = true;
                else radioButton2.Checked = true;
            }
            conecta.CierraConexion();
  
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            EnviarCorreo();
        }

        public void EnviarCorreo()
        {
            Recolectar();
            //Configurando el cliente SMTP
            SmtpClient client = new SmtpClient();
            client.Host = SALIDASMTP;
            client.Port = int.Parse(PUERTO);
            if (CIFRADA=="SI")
            client.EnableSsl = true;
            else
                client.EnableSsl = false;

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(CORREO, CONTRASEÑA);
            client.TargetName = NOMBRE;
            //Preparando archivo adjunto
            //MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes("Contenido de un documento de texto muy interesante."));

            //Enviando correo
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(CORREO,NOMBRE);
            mail.To.Add(new MailAddress(CORREO)); // destino
            mail.Subject = "Prueba de Correo de Facturacion.";
            mail.IsBodyHtml = true;
            mail.Body = "<h2>Prueba de Correo!</h2><br/><br/>Visiten: <a href='http://www.soluciones-sia.com'>SOLUCIONES SIA</a>";

            //mail.Attachments.Add(new Attachment(ms, "Documento.txt"));
            try
            {
                client.Send(mail);
                MessageBox.Show("Se envio el correo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception E)
            {
                MessageBox.Show("Error"+ E.Message.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

    }
}
