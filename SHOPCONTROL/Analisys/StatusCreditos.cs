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
    public partial class StatusCreditos : Form
    {
        public StatusCreditos()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {
                    }

        private void StatusCreditos_Load(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();

            SqlDataReader leer = conecta.RecordInfo("select distinct nombre from Doctores");
            while (leer.Read())
            {
                unidad.Items.Add(leer["nombre"].ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {


            string cvcliente = "";
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Fecha", 80);
            Lv.Columns.Add("iddoctor", 80);
            Lv.Columns.Add("unidad", 80);
            Lv.Columns.Add("cvpreserv", 80);
            Lv.Columns.Add("cvpaciente", 80);
            Lv.Columns.Add("NomCompleto", 80);
            Lv.Columns.Add("cantidad", 80);
            Lv.Columns.Add("cvproducto", 80);
            Lv.Columns.Add("precio", 80);
            Lv.Columns.Add("nombre", 80);
            Lv.Columns.Add("TELEFONO", 80);
            Lv.Columns.Add("estatus", 80);
            Lv.Columns.Add("emitio", 80);
            Lv.Columns.Add("numrecibo", 80);
            Lv.Columns.Add("fechacita", 80);
            Lv.Columns.Add("numticket", 80);



            Lv.BeginUpdate();


            StringBuilder query = new StringBuilder();
            query.Append("Select * from v_statusCreditos where ");

            if (AllDatesCheck.Checked == false)
            {
                query.AppendLine(" Fecha between '" + dateTimePicker1.Value + "' and '" + dateTimePicker2.Value + "' and");
            }

            if (unidad.Text.Length == 0)
            {
                MessageBox.Show("Se requiere seleccionar una unidad");
                return;
            }

            query.AppendLine("  unidad = '" + unidad.Text + "'");

            if (idCliente.Text.Length>0)
            {
                query.AppendLine(" and cvpaciente = '" + idCliente.Text + "'");
            }

            if (nombrePaciente.Text.Length > 0)
            {
                query.AppendLine(" and NomCompleto like'%" + nombrePaciente.Text + "%'");
            }



            conectorSql conecta = new conectorSql();
            SqlDataReader leer = conecta.RecordInfo(query.Replace("{", string.Empty).Replace("}", string.Empty).ToString());
            while (leer.Read())
            {

                // CultureInfo culture = new CultureInfo("en-US");
                // DateTime tempDate = Convert.ToDateTime(leer["Fecha"].ToString(), culture);

                DateTime tempDate = new DateTime(1900, 01, 01);
                string temporal = leer["Fecha"].ToString();
                CultureInfo culture = new CultureInfo("en-US");
                try
                {

                    tempDate = Convert.ToDateTime(leer["Fecha"].ToString(), culture);
                }
                catch (Exception)
                {
                    tempDate = new DateTime(1900, 01, 01);

                }


                string FECHA = tempDate.ToShortDateString();
                ListViewItem lvi = new ListViewItem(FECHA);

                // lvi.SubItems.Add(leer["Fecha"].ToString());
                lvi.SubItems.Add(leer["iddoctor"].ToString());
                lvi.SubItems.Add(leer["unidad"].ToString());
                lvi.SubItems.Add(leer["cvpreserv"].ToString());
                lvi.SubItems.Add(leer["cvpaciente"].ToString());
                lvi.SubItems.Add(leer["NomCompleto"].ToString());
                lvi.SubItems.Add(leer["cantidad"].ToString());
                lvi.SubItems.Add(leer["cvproducto"].ToString());
                lvi.SubItems.Add(leer["precio"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["TELEFONO"].ToString());
                lvi.SubItems.Add(leer["estatus"].ToString());
                lvi.SubItems.Add(leer["emitio"].ToString());
                lvi.SubItems.Add(leer["numrecibo"].ToString());
                lvi.SubItems.Add(leer["fechacita"].ToString());
                lvi.SubItems.Add(leer["numticket"].ToString());

                Lv.Items.Add(lvi);

                lvi.UseItemStyleForSubItems = false;
            }
            conecta.CierraConexion();
            Lv.EndUpdate();

        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Lv.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = Lv.SelectedIndices;
                foreach (int item in seleccion)
                {
                    // Nombre del cliente
                    string Nombre = Lv.Items[item].SubItems[5].Text;

                    //  Telefono a notificar
                    //string telefono = Lv.Items[item].SubItems[9].Text;
                    string cfnFile = @"\\SRV-DATACENTER\tmp\EmailConf.xml";
                    bool cfnExist = File.Exists(cfnFile);
                    XDocument xdoc = XDocument.Load(cfnExist ? @"\\SRV-DATACENTER\tmp\EmailConf.xml" : @"C:\tmp\EmailConf.xml");

                    string Telefono = xdoc.Descendants("SMSAccessTestingPhone").First().Value;

                    if (Telefono == "0")
                    {
                        Telefono = Lv.Items[item].SubItems[10].Text;
                    }

                    // Fecha y hora
                    string fechaHora = Lv.Items[item].SubItems[0].Text;

                    // Servicio
                    string Servicio = Lv.Items[item].SubItems[2].Text;

                    // Id Cita 6
                    string idCita = Lv.Items[item].SubItems[7].Text;
                    string Fecha = Lv.Items[item].SubItems[0].Text;

                    if (Telefono.Length==0)
                    {
                        MessageBox.Show("El paciente no tiene un número de teléfono para notificar!");
                        break;
                    }

                    DialogResult result1 = MessageBox.Show("Se enviará notificación de falta de pago por SMS a " + Nombre + " al teléfono " + Telefono + System.Environment.NewLine + " ¿Desea continuar?",
                               "Envío de notificación SMS",
                               MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        string Mensaje = xdoc.Descendants("SMSMessageFaltaPago").First().Value;

                        StringBuilder MensajeNotificacion = new StringBuilder(Mensaje);

                        MensajeNotificacion.Replace("$NOMBRE", Nombre);
                        MensajeNotificacion.Replace(@"$FECHA", fechaHora);
                        MensajeNotificacion.Replace(@"$SERVICIO", Servicio);

                        SendNotificationsSMS(Nombre, Telefono, fechaHora, Servicio, idCita, Fecha, MensajeNotificacion.ToString());
                    }
                }
            }



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
