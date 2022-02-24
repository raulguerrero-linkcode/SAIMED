using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SHOPCONTROL
{
    class MailNotifications
    {
        public void SendMail (string htmlString)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("raul.guerrero@linkcode.com.mx");
                message.To.Add(new MailAddress("raul.guerrero2703@gmail.com"));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "Test";
                smtp.Port = 587;
                smtp.Host = "smtp.ionos.mx"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("raul.guerrero@linkcode.com.mx", "Fm9fytmf7q$");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception E) {
                Console.WriteLine("Enter a number: " + E.Message);
            }
        }
    }
}
