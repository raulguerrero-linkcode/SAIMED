using MessageBird;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SHOPCONTROL.Analisys
{
    class SMSNotification
    {
        
        public static void SendNotification(string mensaje, long numero) 
        {
           
                string cfnFile = @"\\SRV-DATACENTER\tmp\EmailConf.xml";
                bool cfnExist = File.Exists(cfnFile);
                XDocument xdoc = XDocument.Load(cfnExist ? @"\\SRV-DATACENTER\tmp\EmailConf.xml" : @"C:\tmp\EmailConf.xml");

                string AccessKey = xdoc.Descendants("SMSAccessToken").First().Value; // your access key here

                Client client = Client.CreateDefault(AccessKey);
                // const long Msisdn = +523315395915; // your phone number here

                long Msisdn = long.Parse("+52" + numero); // your phone number here


                MessageBird.Objects.Message message = client.SendMessage("TestMessage", mensaje, new[] { Msisdn });
                
          


        }

    }
}
