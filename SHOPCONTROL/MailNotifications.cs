using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Xml.Linq;

namespace SHOPCONTROL
{
    class MailNotifications
    {
        public void SendMail (string usuario, string ubicacion)
        {

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                
                XDocument xdoc = XDocument.Load("C:\\tmp\\EmailConf.xml");                
                message.From = new MailAddress(xdoc.Descendants("emailFrom").First().Value);
                // message.To.Add(new MailAddress(xdoc.Descendants("emailTo").First().Value));

                string[] mails = xdoc.Descendants("emailTo").First().Value.Split(';');

                foreach (var mail in mails)
                {
                    message.To.Add(new MailAddress(mail));
                }


                message.Subject = xdoc.Descendants("Subject").First().Value;
                message.IsBodyHtml = true; 
                message.Body = xdoc.Descendants("Body").First().Value + " ~ userid:" + usuario + " Location:" + ubicacion;
                smtp.Port =  Int16.Parse(xdoc.Descendants("Port").First().Value);
                smtp.Host = xdoc.Descendants("Host").First().Value;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(xdoc.Descendants("User").First().Value, xdoc.Descendants("Password").First().Value);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

        }


        /*
         *  Method for send email to the user who created account into SAIMED (As confirmation and )
         * 
         * 
         * 
         * */
        public void SendMail(string email, string datos, bool envio)
        {

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            //  XDocument xdoc = XDocument.Load("//SRV-DATACENTER/tmp/EmailConf.xml");
            string cfnFile = "//SRV-DATACENTER/tmp/EmailConf.xml";
            bool cfnExist = File.Exists(cfnFile);
            XDocument xdoc = XDocument.Load(cfnExist ? "//SRV-DATACENTER/tmp/EmailConf.xml" : "C:\\tmp\\EmailConf.xml");
            string vserver = xdoc.Descendants("ConnStr").First().Value;

            message.From = new MailAddress(xdoc.Descendants("emailFrom").First().Value);
            message.To.Add(new MailAddress(email));

            string[] mails = xdoc.Descendants("emailTo").First().Value.Split(';');

            foreach (var mail in mails)
            {
                message.CC.Add(new MailAddress(mail));
            }


            message.Subject = "Creación de cuenta sistema SAIMED";
            message.IsBodyHtml = true;
            message.Body = datos;
            smtp.Port = Int16.Parse(xdoc.Descendants("Port").First().Value);
            smtp.Host = xdoc.Descendants("Host").First().Value;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(xdoc.Descendants("User").First().Value, xdoc.Descendants("Password").First().Value);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

        }



        /*
         *  Method for send email to the user who singed into SAIMED (As confirmation and care about the userId
         * 
         * 
         * 
         * */
        public void SendMail(string usuario, string ubicacion, string email, string nombre, bool envio)
        {

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            //  XDocument xdoc = XDocument.Load("//SRV-DATACENTER/tmp/EmailConf.xml");
            string cfnFile = "//SRV-DATACENTER/tmp/EmailConf.xml";
            bool cfnExist = File.Exists(cfnFile);
            XDocument xdoc = XDocument.Load(cfnExist ? "//SRV-DATACENTER/tmp/EmailConf.xml" : "C:\\tmp\\EmailConf.xml");
            string vserver = xdoc.Descendants("ConnStr").First().Value;

            message.From = new MailAddress(xdoc.Descendants("emailFrom").First().Value);
            message.To.Add(new MailAddress(email));

            // Temp 
            string[] mails = xdoc.Descendants("emailTo").First().Value.Split(';');

            foreach (var mail in mails)
            {
                message.CC.Add(new MailAddress(mail));
            }



            message.Subject = "Notificación de inicio de sesión";
            message.IsBodyHtml = true;
            message.Body = "Hola " + nombre + "<br>" + xdoc.Descendants("Body").First().Value + "<br> userid:" + valoresg.IdEmployee + "<br> Location:" + ubicacion + "<br> Si no has sido tú, favor de reportarlo a tu supervisor inmediato!";
            smtp.Port = Int16.Parse(xdoc.Descendants("Port").First().Value);
            smtp.Host = xdoc.Descendants("Host").First().Value;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(xdoc.Descendants("User").First().Value, xdoc.Descendants("Password").First().Value);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

        }

        /*
         * Method for send appointments notifications
         * 
         * */
        public void SendMail(string cita, string email, string nombre, string fecha, string servicio, bool sendEmailNotification)
        {

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            //  XDocument xdoc = XDocument.Load("//SRV-DATACENTER/tmp/EmailConf.xml");
            string cfnFile = "//SRV-DATACENTER/tmp/EmailConf.xml";
            bool cfnExist = File.Exists(cfnFile);
            XDocument xdoc = XDocument.Load(cfnExist ? "//SRV-DATACENTER/tmp/EmailConf.xml" : "C:\\tmp\\EmailConf.xml");
            string vserver = xdoc.Descendants("ConnStr").First().Value;

            message.From = new MailAddress(xdoc.Descendants("emailFrom").First().Value);
            message.To.Add(new MailAddress(email));

            // Temp 
            string[] mails = xdoc.Descendants("emailTo").First().Value.Split(';');

            foreach (var mail in mails)
            {
                message.CC.Add(new MailAddress(mail));
            }

            message.Subject = "Confirmación de cita: " + nombre;
            message.IsBodyHtml = true;
            message.Body = "Hola " + nombre + "<br>" + "Se ha registrado exitosamente su cita con número: " + cita + " <br>" + fecha + "<br>Para el área de:" + servicio + "<br>Muchas gracias por su preferencia";
            smtp.Port = Int16.Parse(xdoc.Descendants("Port").First().Value);
            smtp.Host = xdoc.Descendants("Host").First().Value;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(xdoc.Descendants("User").First().Value, xdoc.Descendants("Password").First().Value);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

        }


        public void SendMail(string usuario, string ubicacion, string subject, string msg, int send_msg)
        {

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            if (send_msg == 1)
            {
                //  XDocument xdoc = XDocument.Load("//SRV-DATACENTER/tmp/EmailConf.xml");
                string cfnFile = "//SRV-DATACENTER/tmp/EmailConf.xml";
                bool cfnExist = File.Exists(cfnFile);
                XDocument xdoc = XDocument.Load(cfnExist ? "//SRV-DATACENTER/tmp/EmailConf.xml" : "C:\\tmp\\EmailConf.xml");
                string vserver = xdoc.Descendants("ConnStr").First().Value;

                message.From = new MailAddress(xdoc.Descendants("emailFrom").First().Value);

                string[] mails = xdoc.Descendants("emailTo").First().Value.Split(';');

                foreach (var mail in mails)
                {
                    message.To.Add(new MailAddress(mail));
                }



                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = msg;
                smtp.Port = Int16.Parse(xdoc.Descendants("Port").First().Value);
                smtp.Host = xdoc.Descendants("Host").First().Value;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(xdoc.Descendants("User").First().Value, xdoc.Descendants("Password").First().Value);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            

        }

        public void SendMail(string senderMsg, string destinationMsg, string subjectMsg, string msg)
        {

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                XDocument xdoc = XDocument.Load("C:\\tmp\\EmailConf.xml");
                message.From = new MailAddress(senderMsg);
                message.To.Add(new MailAddress(destinationMsg));


                // Temp 
                string[] mails = xdoc.Descendants("emailTo").First().Value.Split(';');

                foreach (var mail in mails)
                {
                    message.CC.Add(new MailAddress(mail));
                }


                message.Subject = subjectMsg;
                message.IsBodyHtml = true;
                message.Body = msg;
                smtp.Port = Int16.Parse(xdoc.Descendants("Port").First().Value);
                smtp.Host = xdoc.Descendants("Host").First().Value;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(xdoc.Descendants("User").First().Value, xdoc.Descendants("Password").First().Value);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

        }


        public void SendMailChangePasswordAdmin()
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            XDocument xdoc = XDocument.Load("C:\\tmp\\EmailConf.xml");
            message.From = new MailAddress(xdoc.Descendants("emailFrom").First().Value);

            string[] mails = xdoc.Descendants("emailTo").First().Value.Split(';');

            foreach (var mail in mails)
            {
                message.To.Add(new MailAddress(mail));
            }


            message.Subject = xdoc.Descendants("chgAdminSubject").First().Value;
            message.IsBodyHtml = true;
            message.Body = xdoc.Descendants("chgAdminBody").First().Value + " movimiento hecho por userid: " +  valoresg.USUARIOSIS  + " ubicación: " + valoresg.UBICACION; 
            smtp.Port = Int16.Parse(xdoc.Descendants("Port").First().Value);
            smtp.Host = xdoc.Descendants("Host").First().Value;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(xdoc.Descendants("User").First().Value, xdoc.Descendants("Password").First().Value);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }


        public void SendMailRePrintTickets(string ticket, string ayo, string area)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            XDocument xdoc = XDocument.Load("C:\\tmp\\EmailConf.xml");
            message.From = new MailAddress(xdoc.Descendants("emailFrom").First().Value);


            string[] mails = xdoc.Descendants("emailTo").First().Value.Split(';');

            foreach (var mail in mails)
            {
                message.To.Add(new MailAddress(mail));
            }

            
            message.Subject = xdoc.Descendants("TicketReimpressSubject").First().Value + " " + ticket;
            message.IsBodyHtml = true;
            message.Body = xdoc.Descendants("TicketReimpressBody").First().Value + " movimiento hecho por empleado: " + valoresg.Nombre_Completo + " (" + valoresg.IdEmployee + ") " + " userid: " + valoresg.USUARIOSIS + " ubicación: " + valoresg.UBICACION + " Número de pedido: " + ticket +  " área: " + area;
            smtp.Port = Int16.Parse(xdoc.Descendants("Port").First().Value);
            smtp.Host = xdoc.Descendants("Host").First().Value;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(xdoc.Descendants("User").First().Value, xdoc.Descendants("Password").First().Value);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }



    }   

}