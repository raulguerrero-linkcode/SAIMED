using System;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SHOPCONTROL.Analisys
{
    public partial class NotificacionClientesSMS : Form
    {
        public NotificacionClientesSMS()
        {
            InitializeComponent();
        }

        private void PendientesPago_Load(object sender, EventArgs e)
        {
            string cvcliente = "";
            Lv.Items.Clear();
            Lv.Columns.Clear();
            Lv.Columns.Add("FechaCita", 80);
            Lv.Columns.Add("ClaveDoctor", 80);

            Lv.Columns.Add("HoraCita", 80);
            Lv.Columns.Add("TiempoCita", 80);
            Lv.Columns.Add("Estatus", 80);
            Lv.Columns.Add("TipoCita", 80);

            Lv.Columns.Add("IdCita", 80);
            Lv.Columns.Add("NOMBRE", 80);
            Lv.Columns.Add("EMAIL", 80);
            Lv.Columns.Add("TELEFONO", 80);


            Lv.Columns.Add("Observaciones", 80);
            Lv.Columns.Add("ClaveServicio", 80);
            Lv.Columns.Add("Servicio", 80);
            Lv.Columns.Add("ClavePaciente", 80);
            Lv.Columns.Add("ReciboPago", 80);




            Lv.BeginUpdate();
            conectorSql conecta = new conectorSql();
            string Query = "Select * from [CEPAMM].[dbo].[v_proximasCitas] order by fechaCita asc";

            int contador = 1;

            System.Data.SqlClient.SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {

                // CultureInfo culture = new CultureInfo("en-US");
                CultureInfo culture = new CultureInfo("en-US");
                DateTime tempDate = Convert.ToDateTime(leer["FechaCita"].ToString(), culture);

                cvcliente = tempDate.ToShortDateString();
                ListViewItem lvi = new ListViewItem(cvcliente);

                lvi.SubItems.Add(leer["ClaveDoctor"].ToString());
                lvi.SubItems.Add(leer["HoraCita"].ToString());
                lvi.SubItems.Add(leer["TiempoCita"].ToString());
                lvi.SubItems.Add(leer["Estatus"].ToString());
                lvi.SubItems.Add(leer["TipoCita"].ToString());
                lvi.SubItems.Add(leer["IdCita"].ToString());
                lvi.SubItems.Add(leer["NOMBRE"].ToString());
                lvi.SubItems.Add(leer["EMAIL"].ToString());
                lvi.SubItems.Add(leer["TELEFONO"].ToString());
                lvi.SubItems.Add(leer["Observaciones"].ToString());
                lvi.SubItems.Add(leer["ClaveServicio"].ToString());
                lvi.SubItems.Add(leer["Servicio"].ToString());
                lvi.SubItems.Add(leer["ClavePaciente"].ToString());
                lvi.SubItems.Add(leer["ReciboPago"].ToString());


               

                Lv.Items.Add(lvi);

                lvi.UseItemStyleForSubItems = false;
                contador++;
            }
            conecta.CierraConexion();
            Lv.EndUpdate();


            string QueryStr = "Select min(fechaCita) as minima, Max(fechaCita) as maxima from [CEPAMM].[dbo].[v_proximasCitas] ";

            conecta = new conectorSql();

            System.Data.SqlClient.SqlDataReader leerFechas = conecta.RecordInfo(QueryStr);
            while (leerFechas.Read())
            {
                CultureInfo culture = new CultureInfo("en-US");
                DateTime tempDate = Convert.ToDateTime(leerFechas["minima"].ToString(), culture);

                fechaini.Text = "Fecha inicio:  " + tempDate.ToShortDateString();

                tempDate = Convert.ToDateTime(leerFechas["maxima"].ToString(), culture);

                fechafin.Text = "Fecha fin:     " + tempDate.ToShortDateString();
            }
            conecta.CierraConexion();

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (Lv.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = Lv.SelectedIndices;
                foreach (int item in seleccion)
                {
                    // Nombre del cliente
                    string Nombre = Lv.Items[item].SubItems[7].Text;

                    //  Telefono a notificar
                    //string telefono = Lv.Items[item].SubItems[9].Text;
                    string cfnFile = @"//SRV-DATACENTER/tmp/EmailConf.xml";
                    bool cfnExist = File.Exists(cfnFile);
                    XDocument xdoc = XDocument.Load(cfnExist ? @"//SRV-DATACENTER/tmp/EmailConf.xml" : @"C:\\tmp\\EmailConf.xml");

                    string Telefono = xdoc.Descendants("SMSAccessTestingPhone").First().Value;

                    if (Telefono == "0")
                    {
                        Telefono = Lv.Items[item].SubItems[9].Text; 
                    }

                    // Fecha y hora
                    string fechaHora = Lv.Items[item].SubItems[0].Text + " a las " + Lv.Items[item].SubItems[2].Text;

                    // Servicio
                    string Servicio = Lv.Items[item].SubItems[12].Text;

                    // Id Cita 6
                    string idCita = Lv.Items[item].SubItems[6].Text;
                    string Fecha = Lv.Items[item].SubItems[0].Text;

                    DialogResult result1 = MessageBox.Show("Se enviará notificación de cita próxima por SMS a " + Nombre + " al teléfono " + Telefono + System.Environment.NewLine + " ¿Desea continuar?",
                               "Envío de notificación SMS",
                               MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        string Mensaje = xdoc.Descendants("SMSMessage").First().Value;

                        StringBuilder MensajeNotificacion = new StringBuilder(Mensaje);

                        MensajeNotificacion.Replace("$NOMBRE", Nombre);
                        MensajeNotificacion.Replace(@"$FECHA", fechaHora);
                        MensajeNotificacion.Replace(@"$SERVICIO", Servicio);

                        SendNotificationsSMS(Nombre, Telefono, fechaHora, Servicio, idCita, Fecha, MensajeNotificacion.ToString());
                    }
                }
            }

        }
        private void Lv_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Lv.Items.Count; i++)
            {
                int ii = 1;
                

                    // Nombre del cliente
                    string Nombre = Lv.Items[i].SubItems[7].Text;

                    //  Telefono a notificar
                    //string telefono = Lv.Items[i].SubItems[9].Text;
                    string Telefono = "3315395915";


                    // Fecha y hora
                    string fechaHora = Lv.Items[i].SubItems[0].Text + " a las " + Lv.Items[i].SubItems[2].Text;

                    // Servicio
                    string Servicio = Lv.Items[i].SubItems[12].Text;

                    // Id Cita 6
                    string idCita = Lv.Items[i].SubItems[6].Text;
                    string Fecha = Lv.Items[i].SubItems[0].Text;

                    string Mensaje = "Estimado(a) " + Nombre + " Le confirmamos que tiene una cita con nosotros el " + fechaHora + " al servicio de  " + Servicio;

                    notificationStatus.Text = "Enviando notificación a " + Nombre + " Favor de esperar... ";

                    SendNotificationsSMS(Nombre, Telefono, fechaHora, Servicio, idCita, Fecha, Mensaje);
                    ii++;
            }

            notificationStatus.Text = "";
            MessageBox.Show("Se han enviado las notificaciones a todos los usuarios seleccionados");

        }

        


        public void SendNotificationsSMS(string Nombre, string Telefono, string fechaHora, string Servicio, string idCita, string Fecha, string Mensaje)
        {
            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "insert into SMSNotifications (idCita,fecha,Nombre) values (" + idCita + ",'" + Fecha + "','" + Nombre + "')";
            // Query = Query + ", fechaNotificacion='" + getdate() + "'";

            try
            {
                if (conecta.Excute(Query))
                {
                    SMSNotification.SendNotification(Mensaje, long.Parse(Telefono));
                    // MessageBox.Show("Notificación enviada satisfactoriamente");

                }

            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un");
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Ocurrió un error al guardar y enviar la notificación al usuario seleccionado: technical error:" + ex.Message);
                // throw new Exception("Ocurrió un error al guardar y enviar la notificación al usuario seleccionado: technical error:" + ex.Message);
            }
            finally
            {
                conecta.CierraConexion();
            };
        }

    }
}
    