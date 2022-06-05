using System;
using System.Net.Mail;
using System.Net;

public  class EnviarCorreo
    {

      public string HOST;
      public string USUARIO;
      public string CONTRASEÑA;
      public string NombreLLegara;
      public int puerto;
      public bool HabilitadoSSl = false;
      public string MENSAJEERROR = "";

      public bool Adjunto = false;
      public string ArchivoEnvio = "";
      public bool EnviarCorreodePedido(string correocliente, bool adj,string ArchivoM)
      {
          MailMessage correo = new MailMessage();
          correo.From=new MailAddress(USUARIO,NombreLLegara);

          correo.To.Add(correocliente);
          correo.Subject = "Pedido Realizado ";
          correo.IsBodyHtml = false;
          correo.Priority = MailPriority.Normal;
         
          string Cadena = "Solicitud de Pedido \n\n\n";
          //Cadena = Cadena + "Nombre de Cliente; " + textBox5.Text.ToUpper() + "\n";
          //Cadena = Cadena + "Correo de Cliente: " + textBox6.Text + "\n";
          //Cadena = Cadena + "Fecha de Envio; " + DateTime.Now.ToString("dd/MM/yyyy") + "\n\n";
          //Cadena = Cadena + "Serie Registrada; " + textBox1.Text + "\n";
          //Cadena = Cadena + "Cantidad de Timbres: " + textBox2.Text + "\n";


          correo.Body = Cadena;

          SmtpClient smtp = new SmtpClient();
          smtp.Credentials = new NetworkCredential(USUARIO, CONTRASEÑA);
          smtp.Port = puerto;
          smtp.EnableSsl = HabilitadoSSl;

          if (adj == true)
          {
              Attachment attachment = new Attachment(@ArchivoM);
              correo.Attachments.Add(attachment);
              adj = false;
          }

          try
          {
              smtp.Send(correo);
              correo.Dispose();
              return true;
          }
          catch (Exception ex)
          {
              MENSAJEERROR = ex.Message.ToString();
              correo.Dispose();
              return false;
             
          }
        
      }

    }

