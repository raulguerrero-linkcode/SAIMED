using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace SHOPCONTROL
{
    public partial class CambiarDiaCita : Form
    {
        public CambiarDiaCita()
        {
            InitializeComponent();
        }


        public string HoraEntradaDoc = "";
        public string HoraSalidaDoc = "";

        public string HoraEntradaComedor = "";
        public string HoraSalidaComedor = "";
        public string DCOMEDOR = "";
        public int tiempoCon = 0;
        public string cvdoctor = "";
        public decimal Tminentrada = 0;
        public decimal Tminsalida = 0;

        public decimal tminEntraComedor = 0;
        public decimal tminSalComedor = 0;
        public int TiempoPred = 0;
        public string cvpaciente = "";
        public string nombrepaciente = "";
        public string Sel_cvdoctor = "";
        public string Sel_FechaCitare = "";


        private void CambiarDiaCita_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(1);
            label6.Text = valoresg.Reagendar_cvpaciente;
            label7.Text = "";
            label8.Text = valoresg.Reagendar_cvdoctor;
            label10.Text = valoresg.Reagendar_numRecibo;
            label11.Text = valoresg.Reagendar_fecha;
            label13.Text = valoresg.Reagendar_servicio;
            label7.Text = valoresg.Reagendar_Nompaciente;
            label9.Text = valoresg.Reagendar_NomArea;
            label20.Text = "";
            label15.Text = "";
            label17.Text = "";
            textBox1.Text = "";
        }




        public void GenerarCitasdiarias()
        {

            string Query = "Select * from citas where fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and cvdoctor='" + label8.Text + "'";
            conectorSql conecta = new conectorSql();
            bool existecitas = conecta.ExisteRegistro(Query);
            int contador = 1;
            if (existecitas == false)
            {
                if (DCOMEDOR == "SI")
                {

                    decimal trecorre = Tminentrada;
                    while (trecorre <= (tminEntraComedor - tiempoCon))
                    {
                        decimal ConverHora = trecorre / 60;
                        decimal ConMinutos = trecorre % 60;
                        cvdoctor = label8.Text;
                        string CadHora = ConverHora.ToString();
                        string cadena = CadHora;
                        CadHora = "";
                        for (int i = 0; i < cadena.Length; i++)
                        {
                            if (cadena.Substring(i, 1) == ".") break;
                            CadHora = CadHora + cadena.Substring(i, 1);
                        }


                        string Cadmin = ConMinutos.ToString();
                        if (CadHora.Length <= 1) CadHora = "0" + CadHora;
                        if (Cadmin.Length <= 1) Cadmin = "0" + Cadmin;

                        string ReHoraEntrada = CadHora + ":" + Cadmin;

                        DateTime ReFecha = dateTimePicker1.Value;

                        Query = "Insert into citas(cvdoctor";
                        Query = Query + ",numexpediente";
                        Query = Query + ",horainicia";
                        Query = Query + ",horatermina";
                        Query = Query + ",hmininicia";
                        Query = Query + ",hmintermina";
                        Query = Query + ",ttiempo";
                        Query = Query + ",estatus";
                        Query = Query + ",tipo";
                        Query = Query + ",fecha";
                        Query = Query + ",fechacod";
                        Query = Query + ",emite";
                        Query = Query + ",observa";
                        Query = Query + ",progresivo";
                        Query = Query + ",cvservicio)";

                        Query = Query + " values(";
                        Query = Query + "'" + cvdoctor + "'";
                        Query = Query + ",'0'";
                        Query = Query + ",'" + ReHoraEntrada + "'";
                        Query = Query + ",'00:00'";
                        Query = Query + ",'" + trecorre + "'";
                        Query = Query + ",'0'";
                        Query = Query + ",'" + tiempoCon + "'";
                        Query = Query + ",'LIBRE'";
                        Query = Query + ",'NO REGISTRADO'";
                        Query = Query + ",'" + ReFecha.ToString("dd/MM/yyyy") + "'";
                        Query = Query + ",'" + ReFecha.ToString("yyyyMMdd") + "'";
                        Query = Query + ",'" + valoresg.USUARIOSIS + "'";
                        Query = Query + ",''";
                        Query = Query + ",'" + contador.ToString() + "'";
                        Query = Query + ",'0')";
                        conecta.Excute(Query);

                        contador++;
                        trecorre = trecorre + tiempoCon;
                    }


                    trecorre = tminSalComedor;
                    while (trecorre <= Tminsalida)
                    {
                        decimal ConverHora = trecorre / 60;
                        decimal ConMinutos = trecorre % 60;
                        cvdoctor = label8.Text;
                        string CadHora = ConverHora.ToString();
                        string cadena = CadHora;
                        CadHora = "";
                        for (int i = 0; i < cadena.Length; i++)
                        {
                            if (cadena.Substring(i, 1) == ".") break;
                            CadHora = CadHora + cadena.Substring(i, 1);
                        }


                        string Cadmin = ConMinutos.ToString();
                        if (CadHora.Length <= 1) CadHora = "0" + CadHora;
                        if (Cadmin.Length <= 1) Cadmin = "0" + Cadmin;

                        string ReHoraEntrada = CadHora + ":" + Cadmin;

                        DateTime ReFecha = dateTimePicker1.Value;

                        Query = "Insert into citas(cvdoctor";
                        Query = Query + ",numexpediente";
                        Query = Query + ",horainicia";
                        Query = Query + ",horatermina";
                        Query = Query + ",hmininicia";
                        Query = Query + ",hmintermina";
                        Query = Query + ",ttiempo";
                        Query = Query + ",estatus";
                        Query = Query + ",tipo";
                        Query = Query + ",fecha";
                        Query = Query + ",fechacod";
                        Query = Query + ",emite";
                        Query = Query + ",observa";
                        Query = Query + ",progresivo";
                        Query = Query + ",cvservicio)";

                        Query = Query + " values(";
                        Query = Query + "'" + cvdoctor + "'";
                        Query = Query + ",'0'";
                        Query = Query + ",'" + ReHoraEntrada + "'";
                        Query = Query + ",'00:00'";
                        Query = Query + ",'" + trecorre + "'";
                        Query = Query + ",'0'";
                        Query = Query + ",'" + tiempoCon + "'";
                        Query = Query + ",'LIBRE'";
                        Query = Query + ",'NO REGISTRADO'";
                        Query = Query + ",'" + ReFecha.ToString("dd/MM/yyyy") + "'";
                        Query = Query + ",'" + ReFecha.ToString("yyyyMMdd") + "'";
                        Query = Query + ",'" + valoresg.USUARIOSIS + "'";
                        Query = Query + ",''";
                        Query = Query + ",'" + contador.ToString() + "'";
                        Query = Query + ",'0')";
                        conecta.Excute(Query);

                        contador++;
                        trecorre = trecorre + tiempoCon;
                    }
                }
                else // cuando no tiene comedor
                {
                    decimal trecorre = Tminentrada;
                    while (trecorre <= (Tminsalida - tiempoCon))
                    {
                        decimal ConverHora = trecorre / 60;
                        decimal ConMinutos = trecorre % 60;
                        cvdoctor =label8.Text;
                        string CadHora = ConverHora.ToString();
                        string cadena = CadHora;
                        CadHora = "";
                        for (int i = 0; i < cadena.Length; i++)
                        {
                            if (cadena.Substring(i, 1) == ".") break;
                            CadHora = CadHora + cadena.Substring(i, 1);
                        }


                        string Cadmin = ConMinutos.ToString();
                        if (CadHora.Length <= 1) CadHora = "0" + CadHora;
                        if (Cadmin.Length <= 1) Cadmin = "0" + Cadmin;

                        string ReHoraEntrada = CadHora + ":" + Cadmin;

                        DateTime ReFecha = dateTimePicker1.Value;

                        Query = "Insert into citas(cvdoctor";
                        Query = Query + ",numexpediente";
                        Query = Query + ",horainicia";
                        Query = Query + ",horatermina";
                        Query = Query + ",hmininicia";
                        Query = Query + ",hmintermina";
                        Query = Query + ",ttiempo";
                        Query = Query + ",estatus";
                        Query = Query + ",tipo";
                        Query = Query + ",fecha";
                        Query = Query + ",fechacod";
                        Query = Query + ",emite";
                        Query = Query + ",observa";
                        Query = Query + ",progresivo";
                        Query = Query + ",cvservicio)";

                        Query = Query + " values(";
                        Query = Query + "'" + cvdoctor + "'";
                        Query = Query + ",'0'";
                        Query = Query + ",'" + ReHoraEntrada + "'";
                        Query = Query + ",'00:00'";
                        Query = Query + ",'" + trecorre + "'";
                        Query = Query + ",'0'";
                        Query = Query + ",'" + tiempoCon + "'";
                        Query = Query + ",'LIBRE'";
                        Query = Query + ",'NO REGISTRADO'";
                        Query = Query + ",'" + ReFecha.ToString("dd/MM/yyyy") + "'";
                        Query = Query + ",'" + ReFecha.ToString("yyyyMMdd") + "'";
                        Query = Query + ",'" + valoresg.USUARIOSIS + "'";
                        Query = Query + ",''";
                        Query = Query + ",'" + contador.ToString() + "'";
                        Query = Query + ",'0')";
                        conecta.Excute(Query);

                        contador++;
                        trecorre = trecorre + tiempoCon;
                    }


                }

            }


            //contador--;
            //Query = "Update consecutivos set turnos='" + contador.ToString() + "'";
            //conecta.Excute(Query);
            //conecta.CierraConexion();

            ListadoDeCitas();
        }

        public void ListadoDeCitas()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Num. Ticket", 40).Tag = "NUMBER";
            Lv.Columns.Add("Doctor", 0).Tag = "STRING";
            Lv.Columns.Add("Clave Paciente", 50).Tag = "STRING";
            Lv.Columns.Add("Nombre", 150).Tag = "STRING";
            Lv.Columns.Add("Fecha", 90).Tag = "STRING";
            Lv.Columns.Add("Hora Inicia", 75).Tag = "STRING";
            Lv.Columns.Add("Tiempo", 75).Tag = "STRING";
            Lv.Columns.Add("Estatus", 70).Tag = "STRING";
            Lv.Columns.Add("Agendado", 120).Tag = "STRING";
            Lv.Columns.Add("Servicio", 120).Tag = "STRING";
            //     Lv.Columns.Add("Observa", 100).Tag = "STRING";
            Lv.Columns.Add("Emitio", 80).Tag = "STRING";
            Lv.Columns.Add("Recibo de Pago", 80).Tag = "STRING";

            conectorSql conecta = new conectorSql();

            string query = "Select citas.progresivo,citas.nombre,citas.ttiempo,citas.cvdoctor,citas.cvpaciente,citas.fecha,citas.horainicia,citas.estatus,citas.tipo, citas.NombreServicio, citas.emite, citas.recibopago ";
            query = query + " from citas  ";
            query = query + " where fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and citas.cvdoctor='" + label8.Text +"' order by progresivo asc";
            SqlDataReader leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["progresivo"].ToString());
                lvi.SubItems.Add(leer["cvdoctor"].ToString());
                lvi.SubItems.Add(leer["cvpaciente"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                lvi.SubItems.Add(leer["horainicia"].ToString());
                lvi.SubItems.Add(leer["ttiempo"].ToString());
                lvi.SubItems.Add(leer["estatus"].ToString());
                lvi.SubItems.Add(leer["tipo"].ToString());
                lvi.SubItems.Add(leer["NombreServicio"].ToString());
                lvi.SubItems.Add(leer["emite"].ToString());
                lvi.SubItems.Add(leer["recibopago"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            CambioDeColoresCelda();
        }


        private void CambioDeColoresCelda()
        {

            int columna = 0;
            columna = 7;

            Lv.BeginUpdate();

            foreach (ListViewItem item in Lv.Items)
            {
                int indice = 0;
                item.UseItemStyleForSubItems = false;

                foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                {
                    if (indice == columna)
                    {
                        if (subitem.Text == "ATENDIDO") subitem.BackColor = Color.FromArgb(96, 204, 69);
                        if (subitem.Text == "CANCELADO") subitem.BackColor = Color.FromArgb(255, 192, 192);
                        if (subitem.Text == "LIBRE") subitem.BackColor = Color.FromArgb(253, 252, 223);
                        if (subitem.Text == "SIN PAGAR") subitem.BackColor = Color.FromArgb(247, 198, 85);
                        if (subitem.Text == "PAGADO") subitem.BackColor = Color.FromArgb(170, 242, 253);
                    }
                    indice++;
                }
            }
            Lv.EndUpdate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BuscarinfoDoctor();
            GenerarCitasdiarias();
        }


        public void BuscarinfoDoctor()
        {
            conectorSql conecta = new conectorSql();
            string query = "Select * from doctores where cvdoctor='" +  label8.Text+ "'";
            SqlDataReader leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                HoraEntradaDoc = leer["HoraEntrada"].ToString();
                HoraSalidaDoc = leer["HoraSalida"].ToString();

                HoraEntradaComedor = leer["HoraEComedor"].ToString();
                HoraSalidaComedor = leer["HoraSComedor"].ToString();
                tiempoCon = int.Parse(leer["TiempoConsulta"].ToString());
                TiempoPred = tiempoCon;
                DCOMEDOR = leer["dcomedor"].ToString();

            }
            conecta.CierraConexion();

            DateTime tentrada = DateTime.Parse(HoraEntradaDoc);
            Tminentrada = (tentrada.Hour * 60) + tentrada.Minute;

            DateTime tsalida = DateTime.Parse(HoraSalidaDoc);
            Tminsalida = (tsalida.Hour * 60) + tsalida.Minute;

            DateTime tentradacomedor = DateTime.Parse(HoraEntradaComedor);
            tminEntraComedor = (tentradacomedor.Hour * 60) + tentradacomedor.Minute;

            DateTime tsalidaComedor = DateTime.Parse(HoraSalidaComedor);
            tminSalComedor = (tsalidaComedor.Hour * 60) + tsalidaComedor.Minute;

        }

        private void button20_Click(object sender, EventArgs e)
        {
            string numticket = label20.Text;
            string nombre =label7.Text;
            string cvdoctor = label8.Text;
            string cvservicio = "RMAN";
            string observa = textBox1.Text.Trim();
            string pvisita = "NO";
            string cvpaciente = label6.Text.Trim();
            int timepoCon = TiempoPred;
            string nombreservicio = "";
           
            cvservicio = "RMAN";
            nombreservicio = label13.Text.Trim();
            

            DateTime HoraInicia = DateTime.Parse(label17.Text);
            // DateTime FechaProg = DateTime.Parse(label15.Text);

            //DateTime.ParseExact(this.Text, "dd/MM/yyyy", null);
            DateTime FechaProg = DateTime.ParseExact(label15.Text, "dd/MM/yyyy", null);
            DateTime FechaAnterior = DateTime.ParseExact(label11.Text, "dd/MM/yyyy", null);


            string tipor = "REPROGRAMADO";
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();

            string Query = "Update citas set  nombre='" + nombre + "',  cvservicio='" + cvservicio + "'";
            Query = Query + "  , observa='" + observa + "' , emite='" + valoresg.USUARIOSIS + "', Estatus='REPROGRAMADO', tipo='" + tipor + "', NombreServicio='" + nombreservicio + "'";
            Query = Query + "  , cvpaciente='" + cvpaciente + "', ttiempo='" + timepoCon + "' , ReciboPago='" + label10.Text + "'";
            Query = Query + "  , primeravez='" + pvisita + "'    where progresivo='" + numticket + "' and fechacod='" + FechaProg.ToString("yyyyMMdd") + "' and cvdoctor='" + label8.Text + "'";
            conecta.Excute(Query);
            conecta.CierraConexion();


           
            Query = "Update citas set  nombre='',  cvservicio=''";
            Query = Query + "  , observa='' , emite='" + valoresg.USUARIOSIS + "', Estatus='CANCELADO', tipo='" + tipor + "', NombreServicio='' , ReciboPago='0'";
            Query = Query + "   where fechacod='" + FechaAnterior.ToString("yyyyMMdd") + "' and cvdoctor='" + label8.Text + "' and cvpaciente='" + cvpaciente + "' and ReciboPago='" + label10.Text + "'";
            conecta.Excute(Query);
            conecta.CierraConexion();

            Query = "Insert into citasReprog(";
            Query = Query + "  cvpaciente";
            Query = Query + "  ,numturnonuevo";
            Query = Query + "  ,cvdoctor";
            Query = Query + "  ,fecha";
            Query = Query + "  ,fechacod";
            Query = Query + "  ,fechaAntes";
            Query = Query + "  ,fcodantes";
            Query = Query + "  ,realizar";
            Query = Query + "  ,fechaAgenda";
            Query = Query + "  ,fcodAgenda";
            Query = Query + "  ,horaagenda";
            Query = Query + "  ,servicio";
            Query = Query + "  ,observa";
            Query = Query + "  ,numrecibo)";
            Query = Query + "  values (";
            Query = Query + "'" + cvpaciente + "'";
            Query = Query + ",'" + numticket + "'";
            Query = Query + ",'" + label8.Text + "'";
            Query = Query + ",'" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
            Query = Query + ",'" + DateTime.Now.ToString("yyyyMMdd") + "'";
            Query = Query + ",'" + FechaAnterior.ToString("dd/MM/yyyy") + "'";
            Query = Query + ",'" + FechaAnterior.ToString("yyyyMMdd") + "'";
            Query = Query + ",'" + valoresg.USUARIOSIS + "'";
            Query = Query + ",'" + FechaProg.ToString("dd/MM/yyyy") + "'";
            Query = Query + ",'" + FechaProg.ToString("yyyyMMdd") + "'";
            Query = Query + ",'" + HoraInicia.ToString("HH:mm:00") + "'";
            Query = Query + ",'" + nombreservicio + "'";
            Query = Query + ",'" + textBox1.Text.Trim()+ "'";
            Query = Query + ",'" + label10.Text + "')";
            conecta.Excute(Query);
            conecta.CierraConexion();


            /*
            * 
            * Send Email Notification it could be used when the user tried to log with ADMIN Account
            * (string usuario, string ubicacion, string subject, string msg, int send_msg)
           */

            MailNotifications mail = new MailNotifications();
            mail.SendMail(valoresg.IdEmployee, valoresg.UBICACION, "Se reprogramó una cita con Num de recibo " + label10.Text + "en Sucursal: " + valoresg.UBICACION , "<br> Usuario que reprogramó: " + valoresg.Nombre_Completo  + " (" + valoresg.IdEmployee + ") <br>Rol de: " + valoresg.USUARIOSIS + "<br> ha realizado una reprogramación sobre el num de recibo:<br> " + label10.Text + "<br>Paciente: " + nombre + "<br>Servicio: " + cvservicio +"<br>Fecha Anterior: " +  FechaAnterior.ToString("dd/MM/yyyy")  + "<br>Fecha nueva: " + FechaProg.ToString("dd/MM/yyyy") + "<br>Hora: " + HoraInicia.ToString("HH:mm:00"), 1);

            MessageBox.Show("Se reprogramó la cita", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();

        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Lv.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = Lv.SelectedIndices;
                foreach (int item in seleccion)
                {
                    DetallesModifica2(item);
                }
            }
        }

        public string Gnumticket = "";
        public string Gestatus = "";
        public string nombreservicio = "";
        public string Sel_numrecibo = "";
        public void DetallesModifica2(int index)
        {

            Gnumticket = Lv.Items[index].Text;
            Gestatus = Lv.Items[index].SubItems[5].Text;
            cvpaciente = Lv.Items[index].SubItems[2].Text;
            nombrepaciente = Lv.Items[index].SubItems[3].Text;
            nombreservicio = Lv.Items[index].SubItems[9].Text;
            string horaselec = Lv.Items[index].SubItems[5].Text;

            label20.Text = Gnumticket;
            label15.Text = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            label17.Text =horaselec;
     
        }
    }
}
