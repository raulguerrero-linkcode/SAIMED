using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Data.SqlClient;
using System.Threading;
namespace SHOPCONTROL
{



    public partial class VISORTURNOS : Form
    {
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        List<VoiceInfo> vocesInfo = new List<VoiceInfo>();
        public VISORTURNOS()
        {
            InitializeComponent();
            foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
            {
                vocesInfo.Add(voice.VoiceInfo);
                cbVoces.Items.Add(voice.VoiceInfo.Name);

            }
            cbVoces.SelectedIndex = 0;
        }

        private void VISORTURNOS_Load(object sender, EventArgs e)
        {
            Columnas();
            ListadoTurnos();
        }
        public void Columnas()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("TURNO", 200).Tag = "NUMBER";
            Lv.Columns.Add("PACIENTE", 500).Tag = "STRING";
            Lv.Columns.Add("DOCTOR", 300).Tag = "STRING";
            Lv.Columns.Add("ESTATUS", 300).Tag = "STRING";
            Lv.Columns.Add("VOZ", 0).Tag = "STRING";
            Lv.Columns.Add("IDCITA", 0).Tag = "STRING";

        }

        public void HablarTexto(string texto)
        {
            try
            {

            int indice;

            double Volumen = 5;
            double Rate = double.Parse("-3");

            indice = 1; ///1 2
            String nombre = vocesInfo.ElementAt(indice).Name;
            synthesizer.SelectVoice(nombre);

            synthesizer.Volume = (int)Volumen;
            synthesizer.Rate = (int)Rate;
            synthesizer.Speak(texto);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message.ToString());
            }

        }

        public string[,] TurnosEnConsulta;
        public string[,] TurnosAtender1;
        public Thread HiloCitasConsulta = null;
        public Thread HiloCitasPorAtender = null;
        public void CargarDatosListview()
        {
            tasks Enconsulta = new tasks();
            tasks EnAtender = new tasks();

            HiloCitasConsulta = new Thread(Enconsulta.CitasEnConsulta);
            HiloCitasPorAtender = new Thread(Enconsulta.CitasporAtender);

            HiloCitasConsulta.Start();
            HiloCitasPorAtender.Start();

      

        }

        public void arrayTurnosEnConsulta()
        {
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();

           string[,] TurnosEnConsulta = null;
            TurnosEnConsulta = new string[20,6];
            string query = "Select cvdoctor from doctores order by cvdoctor asc";
            SqlDataReader leer = conecta.RecordInfo(query);
            int contador = 0;
            while (leer.Read())
            {
                string cvdoctor = leer["cvdoctor"].ToString();
                string consulta = "SELECT TOP (1) doctores.cvdoctor, citas.idturno, citas.nombre as paciente,Doctores.nombre as consultorio,estatusserv,voz,idcita";
                consulta = consulta + " FROM Citas";
                consulta = consulta + " inner join doctores on Doctores.cvdoctor = citas.cvdoctor";
                consulta = consulta + " WHERE(fechacod = '" + DateTime.Now.ToString("yyyyMMdd") + "')";
                consulta = consulta + "  AND(estatus = 'PAGADO')  and estatusserv = 'POR ATENDER'";
                consulta = consulta + " and Citas.cvdoctor='" + cvdoctor + "'";
                consulta = consulta + " order by idcita desc";
                SqlDataReader leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    string idturno = leer2["idturno"].ToString();
                    string paciente = leer2["paciente"].ToString();
                    if (paciente.Contains(".")) paciente = paciente.Replace(".", "");
                    string consultorio = leer2["consultorio"].ToString();
                    string estatus = leer2["estatusserv"].ToString();
                    string voz = "1";
                    string idcita = leer2["idcita"].ToString();

                    TurnosEnConsulta[contador,0] = idturno;
                    TurnosEnConsulta[contador,1] = paciente;
                    TurnosEnConsulta[contador,2] = consultorio;
                    TurnosEnConsulta[contador,3] = estatus;
                    TurnosEnConsulta[contador,4] = voz;
                    TurnosEnConsulta[contador,5] = idcita;
                }
                conecta2.CierraConexion();
                contador++;
            }
            conecta.CierraConexion();
        }


        public void arrayTurnosPorAtender1()
        {
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();

            string[,] TurnosAtender1 = null;
            TurnosAtender1 = new string[20,6];
            string query = "Select cvdoctor from doctores order by cvdoctor asc";
            SqlDataReader leer = conecta.RecordInfo(query);
            int contador = 0;
            while (leer.Read())
            {
                string cvdoctor = leer["cvdoctor"].ToString();
                string consulta = "SELECT TOP (1) doctores.cvdoctor, citas.idturno, citas.nombre as paciente,Doctores.nombre as consultorio,estatusserv,idcita";
                consulta = consulta + " FROM Citas";
                consulta = consulta + " inner join doctores on Doctores.cvdoctor = citas.cvdoctor";
                consulta = consulta + " WHERE(fechacod = '" + DateTime.Now.ToString("yyyyMMdd") + "')";
                consulta = consulta + "  AND(estatus = 'PAGADO')  and estatusserv = 'POR ATENDER'";
                consulta = consulta + " and Citas.cvdoctor='" + cvdoctor + "'";
                consulta = consulta + " order by idcita asc";
                SqlDataReader leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    string idturno = leer2["idturno"].ToString();
                    string paciente = leer2["paciente"].ToString();
                    if (paciente.Contains(".")) paciente = paciente.Replace(".", "");
                    string consultorio = leer2["consultorio"].ToString();
                    string estatus = leer2["estatusserv"].ToString();
                    string voz = "1";
                    string idcita = leer2["idcita"].ToString();

                    TurnosAtender1[contador,0] = idturno;
                    TurnosAtender1[contador,1] = paciente;
                    TurnosAtender1[contador,2] = consultorio;
                    TurnosAtender1[contador,3] = estatus;
                    TurnosAtender1[contador,4] = voz;
                    TurnosAtender1[contador,5] = idcita;
                }
                conecta2.CierraConexion();
                contador++;
            }
            conecta.CierraConexion();
        }

        public void ListadoTurnos()
        {

          
            Lv.Items.Clear();
            conectorSql conecta2 = new conectorSql();
            conectorSql conecta = new conectorSql();
            Lv.BeginUpdate();
            string query = "Select cvdoctor from doctores order by abrevia asc";
            SqlDataReader leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                string cvdoctor = leer["cvdoctor"].ToString();
                string consulta = "SELECT TOP (1) doctores.cvdoctor, citas.idturno, citas.nombre as paciente,Doctores.nombre as consultorio,estatusserv,voz,idcita";
                consulta = consulta + " FROM Citas";
                consulta = consulta + " inner join doctores on Doctores.cvdoctor = citas.cvdoctor";
                consulta = consulta + " WHERE(fechacod = '" + DateTime.Now.ToString("yyyyMMdd") + "')";
                consulta = consulta + "  AND(estatus = 'PAGADO')  and estatusserv in  ('EN CONSULTA', 'POR ATENDER')";
                consulta = consulta + " and Citas.cvdoctor='" + cvdoctor+"'";
                consulta = consulta + " order by progresivo desc";

                SqlDataReader leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    string turno = leer2["idturno"].ToString();
                    string consultorio = leer2["consultorio"].ToString();
                    string paciente = leer2["paciente"].ToString();
                    string voz = leer2["voz"].ToString();
                    if (paciente.Contains(".")) paciente = paciente.Replace(".", "");

                    //if (turno.Contains("-")) turno = turno.Replace("-", " ");
                    if (consultorio.Contains("DRA. ")) consultorio = consultorio.Replace("DRA. ", "");
                    if (consultorio.Contains("DRA ")) consultorio = consultorio.Replace("DRA ", "");
                    if (consultorio.Contains("DR. ")) consultorio = consultorio.Replace("DR. ", "");
                    if (consultorio.Contains("DR ")) consultorio = consultorio.Replace("DR ", "");

                    ListViewItem lvi = new ListViewItem(turno);
                    lvi.SubItems.Add(paciente.Trim());
                    lvi.SubItems.Add(consultorio);
                    lvi.SubItems.Add(leer2["estatusserv"].ToString());
                    lvi.SubItems.Add(voz);
                    lvi.SubItems.Add(leer2["idcita"].ToString());
                    Lv.Items.Add(lvi);

                   

                    for (int i = 0; i < 5; i++)
                    {
                        lvi.SubItems[i].BackColor = Color.FromArgb(250, 175, 250);
                    }


                }
                conecta2.CierraConexion();
            }
            conecta.CierraConexion();



            //query = "Select cvdoctor from doctores order by abrevia asc";
            //leer = conecta.RecordInfo(query);
            //while (leer.Read())
            //{
            //    string cvdoctor = leer["cvdoctor"].ToString();
            //    string consulta = "SELECT TOP (1) doctores.cvdoctor, citas.idturno, citas.nombre as paciente,Doctores.nombre as consultorio,estatusserv,idcita";
            //    consulta = consulta + " FROM Citas";
            //    consulta = consulta + " inner join doctores on Doctores.cvdoctor = citas.cvdoctor";
            //    consulta = consulta + " WHERE(fechacod = '" + DateTime.Now.ToString("yyyyMMdd") + "')";
            //    consulta = consulta + "  AND(estatus = 'PAGADO')  and estatusserv = 'POR ATENDER'";
            //    consulta = consulta + " and Citas.cvdoctor='" + cvdoctor + "'";
            //    consulta = consulta + " order by progresivo asc";
            //    SqlDataReader leer2 = conecta2.RecordInfo(consulta);
            //    while (leer2.Read())
            //    {
            //        string turno =leer2["idturno"].ToString();
            //        string paciente = leer2["paciente"].ToString();
            //        string consultorio = leer2["consultorio"].ToString();
            //        if (paciente.Contains(".")) paciente = paciente.Replace(".", "");
            //        //if (turno.Contains("-")) turno = turno.Replace("-", " ");
            //        if (consultorio.Contains("DRA. ")) consultorio = consultorio.Replace("DRA. ", "");
            //        if (consultorio.Contains("DRA ")) consultorio = consultorio.Replace("DRA ", "");
            //        if (consultorio.Contains("DR. ")) consultorio = consultorio.Replace("DR. ", "");
            //        if (consultorio.Contains("DR ")) consultorio = consultorio.Replace("DR ", "");


            //        ListViewItem lvi = new ListViewItem(turno);
            //        lvi.SubItems.Add(paciente.Trim());
            //        lvi.SubItems.Add(leer2["consultorio"].ToString());
            //        lvi.SubItems.Add(leer2["estatusserv"].ToString());
            //        lvi.SubItems.Add("1");
            //        lvi.SubItems.Add(leer2["idcita"].ToString());
            //        Lv.Items.Add(lvi);

            //        for (int i = 0; i < 4; i++)
            //        {
            //            lvi.SubItems[i].BackColor = Color.FromArgb(251, 204, 250);
            //        }
            //    }
            //    conecta2.CierraConexion();

            //}
            //conecta.CierraConexion();
            Thread.Sleep(2000); // 2 segundos
            Lv.EndUpdate();

            

            for (int i = 0; i < Lv.Items.Count; i++)
            {
                if (Lv.Items[i].SubItems[4].Text == "0")
                {
                    string turno =Lv.Items[i].Text;
                    string paciente = Lv.Items[i].SubItems[1].Text;
                    string consultorio = Lv.Items[i].SubItems[2].Text;

                    if (consultorio.Contains("C.")) consultorio = consultorio.Replace("C.", "");
                    string idcita = Lv.Items[i].SubItems[5].Text;

                    // if (turno.Contains("-")) turno = turno.Replace("-", " ");
                    Thread.Sleep(3000); // 2 segundos
                    HablarTexto("TURNO " + turno);
                    HablarTexto(paciente);
                    Thread.Sleep(3000); // 2 segundos
                    HablarTexto("DOCTOR " + consultorio);
                    Lv.Items[i].SubItems[4].Text = "1";
                    string horallamada = DateTime.Now.ToString("HH:mm:00");

                    string consulta = "Update citas set voz='1', horallamada='" + horallamada + "' where IDturno='" + turno + "'";
                    conecta.Excute(consulta);
                    conecta.CierraConexion();
                }
            }

        }

        public int totalfilasexistentes = 0;
        public int fila = 0;
        public bool ENTROCAMBIAR = false;
        conectorSql conectaturno = null;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (tasks.TerminoEnConsulta == 0 && tasks.TerminoPorAtender == 0)
            //{
            //    CargarDatosListview();
            //    totalfilasexistentes = Lv.Items.Count;
            //}

            //if (tasks.TerminoEnConsulta == 1)
            //{
            //    TurnosEnConsulta = tasks.TurnosEnConsulta;
            //    HiloCitasConsulta.Abort();

            //    for (int i = 0; i < Lv.Items.Count; i++)
            //    {
            //        Lv.Items[i].Text = "";
            //        Lv.Items[i].SubItems[1].Text = "";
            //        Lv.Items[i].SubItems[2].Text = "";
            //        Lv.Items[i].SubItems[3].Text = "";
            //        Lv.Items[i].SubItems[4].Text = "";
            //        Lv.Items[i].SubItems[5].Text = "";

            //        Lv.Items[i].BackColor = Color.FromArgb(251, 204, 250);
            //        for (int ii = 1; ii <= 5; ii++)
            //        {
            //            Lv.Items[i].SubItems[ii].BackColor = Color.FromArgb(251, 204, 250);
            //        }

            //    }


            //    for (int i = 0; i < 20; i++)
            //    {
            //        string idturno=TurnosEnConsulta[i, 0] ;
            //        string paciente=TurnosEnConsulta[i, 1] ;
            //        string consultorio =TurnosEnConsulta[i, 2] ;
            //        string estatus=TurnosEnConsulta[i, 3] ;
            //        string voz=TurnosEnConsulta[i, 4] ;
            //        string idcita=TurnosEnConsulta[i, 5] ;

            //        if (paciente == "")
            //        {
            //            tasks.TerminoEnConsulta = 0;
            //            return;
            //        }
            //        Lv.Items[i].Text = idturno;
            //        Lv.Items[i].SubItems[1].Text = paciente;
            //        Lv.Items[i].SubItems[2].Text = consultorio;
            //        Lv.Items[i].SubItems[3].Text = estatus;
            //        Lv.Items[i].SubItems[4].Text = voz;
            //        Lv.Items[i].SubItems[5].Text = idcita;

            //        Lv.Items[i].BackColor= Color.FromArgb(250, 175, 250);
            //        for (int ii = 1; ii <= 5; ii++)
            //        {
            //            Lv.Items[i].SubItems[ii].BackColor = Color.FromArgb(250, 175, 250);
            //        }

            //        if (voz=="0")
            //        {
            //            conectaturno = new conectorSql();
            //            HablarTexto("TURNO " + idturno);
            //            HablarTexto(paciente);
            //            HablarTexto(consultorio);
            //            Lv.Items[i].SubItems[4].Text = "1";
            //            string consulta = "Update citas set voz='1' where idcita='" + idcita + "'";
            //            conectaturno.Excute(consulta);
            //            conectaturno.CierraConexion();
            //            conectaturno = null;
            //        }



            //        fila++;
            //    }



            //    tasks.TerminoEnConsulta = 0;
            //}

            //if (tasks.TerminoPorAtender == 1)
            //{
            //    fila--;
            //    TurnosAtender1 = tasks.TurnosPorAtender;
            //    HiloCitasPorAtender.Abort();
            //    for (int i = 0; i < 20; i++)
            //    {
            //        string idturno = TurnosAtender1[i, 0];
            //        string paciente = TurnosAtender1[i, 1];
            //        string consultorio = TurnosAtender1[i, 2];
            //        string estatus = TurnosAtender1[i, 3];
            //        string voz = TurnosAtender1[i, 4];
            //        string idcita = TurnosAtender1[i, 5];

            //        if (paciente == "")
            //        {
            //            tasks.TerminoPorAtender = 0;
            //            return;
            //        }

            //        if (Lv.Items.Count<=fila)
            //        {
            //            ListViewItem lvi = new ListViewItem(idturno);
            //            lvi.SubItems.Add(paciente.Trim());
            //            lvi.SubItems.Add(consultorio);
            //            lvi.SubItems.Add(estatus);
            //            lvi.SubItems.Add(voz);
            //            lvi.SubItems.Add(idcita);
            //            Lv.Items.Add(lvi);

            //        }
            //        else
            //        {
            //            Lv.Items[fila].Text = idturno;
            //            Lv.Items[fila].SubItems[1].Text = paciente;
            //            Lv.Items[fila].SubItems[2].Text = consultorio;
            //            Lv.Items[fila].SubItems[3].Text = estatus;
            //            Lv.Items[fila].SubItems[4].Text = voz;
            //            Lv.Items[fila].SubItems[5].Text = idcita;
            //        }
            //        ///

            //        Lv.Items[fila].BackColor = Color.FromArgb(251, 204, 250);
            //        for (int ii = 1; ii <= 5; ii++)
            //        {
            //            Lv.Items[fila].SubItems[ii].BackColor = Color.FromArgb(251, 204, 250);
            //        }

            //        fila++;
            //    }

            //    if (totalfilasexistentes>Lv.Items.Count)
            //    {
            //        int resultado = totalfilasexistentes - Lv.Items.Count;
            //        for (int i = 0; i < resultado; i++)
            //        {
            //            Lv.Items[fila].Text = "";
            //            Lv.Items[fila].SubItems[1].Text = "";
            //            Lv.Items[fila].SubItems[2].Text = "";
            //            Lv.Items[fila].SubItems[3].Text = "";
            //            Lv.Items[fila].SubItems[4].Text = "";
            //            Lv.Items[fila].SubItems[5].Text = "";
            //            fila++;
            //        }
            //    }

            //    tasks.TerminoPorAtender = 0;
            //}

            ListadoTurnos();
        }

        private void Lv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Dispose();
        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void VISORTURNOS_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void VISORTURNOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Dispose();
        }

        private void cbVoces_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
