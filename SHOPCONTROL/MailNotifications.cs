using System;
using System.IO;
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

            //  XDocument xdoc = XDocument.Load("//SRV-DATACENTER/tmp/EmailConf.xml");
                string cfnFile = "//SRV-DATACENTER/tmp/EmailConf.xml";
                bool cfnExist = File.Exists(cfnFile);
                XDocument xdoc = XDocument.Load(cfnExist ? "//SRV-DATACENTER/tmp/EmailConf.xml" : "C:\\tmp\\EmailConf.xml");
                string vserver = xdoc.Descendants("ConnStr").First().Value;
                
                message.From = new MailAddress(xdoc.Descendants("emailFrom").First().Value);
                message.To.Add(new MailAddress(xdoc.Descendants("emailTo").First().Value));
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
                message.To.Add(new MailAddress(xdoc.Descendants("emailTo").First().Value));
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
                string cfnFile = "//SRV-DATACENTER/tmp/EmailConf.xml";
                bool cfnExist = File.Exists(cfnFile);
                XDocument xdoc = XDocument.Load(cfnExist ? "//SRV-DATACENTER/tmp/EmailConf.xml" : "C:\\tmp\\EmailConf.xml");
                string vserver = xdoc.Descendants("ConnStr").First().Value;

                message.From = new MailAddress(senderMsg);
                message.To.Add(new MailAddress(destinationMsg));
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

            string cfnFile = "//SRV-DATACENTER/tmp/EmailConf.xml";
            bool cfnExist = File.Exists(cfnFile);
            XDocument xdoc = XDocument.Load(cfnExist ? "//SRV-DATACENTER/tmp/EmailConf.xml" : "C:\\tmp\\EmailConf.xml");
            string vserver = xdoc.Descendants("ConnStr").First().Value;

            message.From = new MailAddress(xdoc.Descendants("emailFrom").First().Value);
            message.To.Add(new MailAddress(xdoc.Descendants("emailTo").First().Value));
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

            string cfnFile = "//SRV-DATACENTER/tmp/EmailConf.xml";
            bool cfnExist = File.Exists(cfnFile);
            XDocument xdoc = XDocument.Load(cfnExist ? "//SRV-DATACENTER/tmp/EmailConf.xml" : "C:\\tmp\\EmailConf.xml");
            string vserver = xdoc.Descendants("ConnStr").First().Value;

            message.From = new MailAddress(xdoc.Descendants("emailFrom").First().Value);
            message.To.Add(new MailAddress(xdoc.Descendants("emailTo").First().Value));
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