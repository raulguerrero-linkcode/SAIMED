using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;


using CrystalDecisions.CrystalReports.Engine;
using SHOPCONTROL.Utilerias;

namespace SHOPCONTROL.HistorialClinica
{
    public partial class RegistroCitas : Form
    {
        public RegistroCitas()
        {
            InitializeComponent();
        }

        public string HoraEntradaDoc = "";
        public string HoraSalidaDoc = "";

        public string HoraEntradaComedor= "";
        public string HoraSalidaComedor = "";
        public string DCOMEDOR = "";
        public int tiempoCon = 0;
        public string cvdoctor= "";
        public decimal Tminentrada = 0;
        public decimal Tminsalida = 0;

        public decimal tminEntraComedor = 0;
        public decimal tminSalComedor= 0;
        public int TiempoPred = 0;
        public string cvpaciente = "";
        public string nombrepaciente = "";
        public string Sel_cvdoctor = "";
        public string Sel_FechaCitare="";

        public string CURP = "";

        string NOMBRE = "";
        string APATERNO = "";
        string AMATERNO = "";
        string GENERO = "";
        string ESCOLARIDAD = "";
        string EMAIL = "";
        string EDAD = "";
        string ECivil = "";
        string NoHijos = "";
        string OCUPACION = "";
        string TELEFONO = "";
        string CALLE = "";
        string NoCalle = "";
        string CP = "";
        string COLONIA = "";
        string MUNICIPIO = "";
        string CIUDAD = "";
        string ESTADO = "";
        string Pregunta1 = "";
        string Pregunta2 = "";
        string Pregunta3 = "";
        string RecibeAvisos = "";

        string NoExpediente = ""; //
        string SERVICIO = "";
        string MEDICO = "";
        string TURNO = "";
        string OBSERVACIONES = "";
        string FECHA = "";
        string FCOD = "";

        string LUGARNAC = "";
        string FECHANAC = "";
        string STATUS = "1";
        string pathFoto = "";

        string CELULAR = "1";
        string CLAVE = "1";
        string EMAIL2 = "1";

        string EXPGINECO = "";
        string EXPDENTAL = "";
        string EXPOFTAM = "";



        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            //ConsecutivoTurnos();
            BuscarinfoDoctor();
            BuscarClaveTurno();
            GenerarCitasdiarias();
        }

        public int NumTurno = 0;
        public void ConsecutivoTurnos()
        {
            conectorSql conecta = new conectorSql();
            NumTurno = 1;
            string Query = "Select turnos from consecutivos where turnos<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                NumTurno = int.Parse(leer["turnos"].ToString());
            }
            conecta.CierraConexion();
        
        }

        public void GenerarCitasdiarias()
        {

             string Query = "Select * from citas where fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and cvdoctor='" + comboBox1.SelectedValue.ToString()+ "'";
            conectorSql conecta = new conectorSql();
            bool existecitas = conecta.ExisteRegistro(Query);
            conecta.CierraConexion();
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
                        cvdoctor = comboBox1.SelectedValue.ToString();
                        string CadHora = ConverHora.ToString();
                        string cadena = CadHora;
                        CadHora = "";
                        for (int i = 0; i < cadena.Length; i++)
                        {
                            if (cadena.Substring(i, 1) == "ND") break;
                            CadHora = CadHora + cadena.Substring(i, 1);
                        }

                        string idturno = label40.Text + "-" + contador.ToString();
                        string Cadmin = ConMinutos.ToString();
                        if (CadHora.Length <= 1) CadHora = "0" + CadHora;
                        if (Cadmin.Length <= 1) Cadmin = "0" + Cadmin;

                        string ReHoraEntrada = CadHora + ":" + Cadmin;

                        DateTime ReFecha = dateTimePicker1.Value;

                        Query = "Delete from citas where fecha='" + ReFecha.ToString("dd/MM/yyyy") + "' and progresivo='" + contador.ToString() + "' and cvdoctor='" + cvdoctor + "'";
                        conecta.Excute(Query);
                        conecta.CierraConexion();

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
                        Query = Query + ",estatusserv";
                        Query = Query + ",idturno";
                        Query = Query + ",horapago";
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
                        Query = Query + ",'POR ATENDER'";
                        Query = Query + ",'" + idturno.ToString() + "'";
                        Query = Query + ",'00:00:00'";
                        Query = Query + ",'0')";
                        conecta.Excute(Query);
                        conecta.CierraConexion();
                        contador++;
                        trecorre = trecorre + tiempoCon;
                    }


                    trecorre = tminSalComedor;
                    while (trecorre <= Tminsalida)
                    {
                        decimal ConverHora = trecorre / 60;
                        decimal ConMinutos = trecorre % 60;
                        cvdoctor = comboBox1.SelectedValue.ToString();
                        string CadHora = ConverHora.ToString();
                        string cadena = CadHora;
                        CadHora = "";
                        for (int i = 0; i < cadena.Length; i++)
                        {
                            if (cadena.Substring(i, 1) == "ND") break;
                            CadHora = CadHora + cadena.Substring(i, 1);
                        }

                        string idturno = label40.Text + "-" + contador.ToString();
                        string Cadmin = ConMinutos.ToString();
                        if (CadHora.Length <= 1) CadHora = "0" + CadHora;
                        if (Cadmin.Length <= 1) Cadmin = "0" + Cadmin;

                        string ReHoraEntrada = CadHora + ":" + Cadmin;

                        DateTime ReFecha = dateTimePicker1.Value;
                        Query = "Delete from citas where fecha='" + ReFecha.ToString("dd/MM/yyyy") + "' and progresivo='" + contador.ToString() + "' and cvdoctor='" + cvdoctor + "'";
                        conecta.Excute(Query);
                        conecta.CierraConexion();


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
                        Query = Query + ",estatusserv";
                        Query = Query + ",idturno";
                        Query = Query + ",horapago";
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
                        Query = Query + ",'POR ATENDER'";
                        Query = Query + ",'" + idturno.ToString() + "'";
                        Query = Query + ",'00:00:00'";
                        Query = Query + ",'0')";
                        conecta.Excute(Query);

                        contador++;
                        trecorre = trecorre + tiempoCon;
                    }
                }
                else // cuando no tiene comedor
                {
                    decimal trecorre = Tminentrada;
                    while (trecorre <= (Tminsalida- tiempoCon))
                    {
                        decimal ConverHora = trecorre / 60;
                        decimal ConMinutos = trecorre % 60;
                        cvdoctor = comboBox1.SelectedValue.ToString();
                        string CadHora = ConverHora.ToString();
                        string cadena = CadHora;
                        CadHora = "";
                        for (int i = 0; i < cadena.Length; i++)
                        {
                            if (cadena.Substring(i, 1) == "ND") break;
                            CadHora = CadHora + cadena.Substring(i, 1);
                        }

                        string idturno = label40.Text + "-" + contador.ToString();
                        string Cadmin = ConMinutos.ToString();
                        if (CadHora.Length <= 1) CadHora = "0" + CadHora;
                        if (Cadmin.Length <= 1) Cadmin = "0" + Cadmin;

                        string ReHoraEntrada = CadHora + ":" + Cadmin;

                        DateTime ReFecha = dateTimePicker1.Value;

                        Query = "Delete from citas where fecha='" + ReFecha.ToString("dd/MM/yyyy") + "' and progresivo='" + contador.ToString() + "' and cvdoctor='" + cvdoctor + "'";
                        conecta.Excute(Query);
                        conecta.CierraConexion();

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
                        Query = Query + ",estatusserv";
                        Query = Query + ",idturno";
                        Query = Query + ",horapago";
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
                        Query = Query + ",'POR ATENDER'";
                        Query = Query + ",'" + idturno.ToString() + "'";
                        Query = Query + ",'00:00:00'";
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
            int atendidos = 0;
            int cancelados = 0;
            int porpagar = 0;
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
            Lv.Columns.Add("Agendado", 80).Tag = "STRING";
            Lv.Columns.Add("Servicio", 120).Tag = "STRING";
       //     Lv.Columns.Add("Observa", 100).Tag = "STRING";
            Lv.Columns.Add("Emitio", 60).Tag = "STRING";
            Lv.Columns.Add("Recibo de Pago", 65).Tag = "STRING";
       //     Lv.Columns.Add("Telefono", 100).Tag = "STRING";
            Lv.Columns.Add("Observa", 100).Tag = "STRING";
            Lv.Columns.Add("Turno", 100).Tag = "STRING";
            Lv.Columns.Add("Hora Pago", 100).Tag = "STRING";
            Lv.Columns.Add("Hora Termina", 100).Tag = "STRING";

            conectorSql conecta = new conectorSql();
            int Horaminutos=(DateTime.Now.Hour*60  + DateTime.Now.Minute)-30;

            string query = "Select citas.progresivo, citas.nombre, citas.ttiempo, citas.cvdoctor, citas.cvpaciente, citas.fecha, citas.horainicia, citas.estatus, citas.tipo, citas.NombreServicio, citas.emite, citas.recibopago, citas.telefono, citas.observa , citas.idturno,citas.horapago,citas.horatermina";
            query = query + " from citas  ";
            query=query + " where fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd") +"' and citas.cvdoctor='" + comboBox1.SelectedValue.ToString() + "'";
            if (checkBox2.Checked == true) query = query + " and hmininicia>='" + Horaminutos.ToString() + "'";
            if (textBox13.Text.Trim()!="") query = query + " and citas.nombre like '%" + textBox13.Text.Trim() + "%'";
            query = query + " order by progresivo asc";
          
            
            SqlDataReader leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                string progresivo = leer["progresivo"].ToString();
                string cvdoctor = leer["cvdoctor"].ToString();
                ListViewItem lvi = new ListViewItem(progresivo);
                lvi.SubItems.Add(cvdoctor);
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
                // lvi.SubItems.Add(leer["telefono"].ToString());
                lvi.SubItems.Add(leer["observa"].ToString());
                string cadturno = leer["idturno"].ToString();
                string estatus = leer["estatus"].ToString();

                if (estatus == "ATENDIDO") atendidos++;
                if (estatus == "CANCELADO") cancelados++;
                if (estatus == "POR PAGAR") porpagar++;


                if (cadturno.Trim() == "")
                {
                    string idturno = label40.Text + "-" + progresivo.ToString();
                    string consulta = "Update citas set idturno='" + idturno + "', estatusserv='POR ATENDER' where progresivo='" + progresivo + "' and cvdoctor='" + cvdoctor + "' and fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "'";
                    conecta.Excute(consulta);
                    conecta.CierraConexion();
                    lvi.SubItems.Add(idturno);
                }
                else
                {
                    lvi.SubItems.Add(leer["idturno"].ToString());
                }
                lvi.SubItems.Add(leer["horapago"].ToString());
                lvi.SubItems.Add(leer["horatermina"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            CambioDeColoresCelda();
            label37.Text = DateTime.Now.ToString("HH:mm:00");
            label45.Text = atendidos.ToString();
            label46.Text = cancelados.ToString();
            label47.Text = porpagar.ToString();

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
                        if (subitem.Text == "REPROGRAMADO") subitem.BackColor = Color.FromArgb(252, 158, 224);
                        if (subitem.Text == "LIBRE") subitem.BackColor = Color.FromArgb(253, 252, 223);
                        if (subitem.Text == "SIN PAGAR" || subitem.Text == "OCUPADO") subitem.BackColor = Color.FromArgb(247, 198, 85);
                        if (subitem.Text == "PAGADO") subitem.BackColor = Color.FromArgb(170, 242, 253);
                    }
                    indice++;
                }
            }
            Lv.EndUpdate();
        }

        public void BuscarinfoDoctor()
        {
            conectorSql conecta = new conectorSql();
            string query = "Select * from doctores where cvdoctor='" +  comboBox1.SelectedValue.ToString()+  "'";
            SqlDataReader leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                HoraEntradaDoc = leer["HoraEntrada"].ToString();
                HoraSalidaDoc = leer["HoraSalida"].ToString();

                HoraEntradaComedor = leer["HoraEComedor"].ToString();
                HoraSalidaComedor= leer["HoraSComedor"].ToString();
                tiempoCon = int.Parse(leer["TiempoConsulta"].ToString());
                TiempoPred = tiempoCon;
                DCOMEDOR= leer["dcomedor"].ToString();

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

        private void RegistroCitas_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            ConsecutivoTurnos();
            combos.ComboDoctores(comboBox1);
        }

        //----- agregacion de productos -------------------------..

        public int NumPreServ = 0;
        public void ConsecutivoPreserv()
        {
            NumPreServ = 1;
            conectorSql conecta = new conectorSql();
            string Query = "Select * from consecutivos where preserv<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                NumPreServ = int.Parse(leer["preserv"].ToString());
            }
            conecta.CierraConexion();
        }

        public void ActualizarPreserv()
        {
            NumPreServ++;
            conectorSql conecta = new conectorSql();
            string Query = "Update consecutivos set preserv='" + NumPreServ.ToString() + "'";
            conecta.Excute(Query);
            conecta.CierraConexion();
        }

        public void ColumnasProducto()
        {
            if (Lv2.Items.Count == 0)
            {
                Lv2.Items.Clear();
                Lv2.Columns.Clear();
                Lv2.Columns.Add("Clave", 50);
                Lv2.Columns.Add("Id Producto", 80);
                Lv2.Columns.Add("Cantidad", 80);
                Lv2.Columns.Add("Nombre", 280);
                Lv2.Columns.Add("Fecha", 80);
            }
        }
        public void AgregarProducto()
        {

            //if (Lv2.Items.Count>1)
            //{
            //    MessageBox.Show("Solo se puede registrar un servicio a la vez para la cita", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    textBox7.Focus();
            //    return;
            //}


            if (textBox7.Text == "")
            {
                MessageBox.Show("Ingrese la clave de un producto o servicio, verifique", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox7.Focus();
                return;
            }
            

            ColumnasProducto();
            if (textBox11.Text == "") textBox11.Text = "1";

            ConsecutivoPreserv();
            if (Lv2.Items.Count > 0)
            {
                bool existeServ = false;
                for (int i = 0; i < Lv2.Items.Count; i++)
                {
                    if (NumPreServ.ToString() != Lv2.Items[i].Text)
                    {
                        existeServ = true;
                        break;
                    }
                }
                if (existeServ)
                {
                    MessageBox.Show("Tiene servicios pendientes por pagar, elimine y vuelva a ingresar los servicios, verifique", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string cvproducto = textBox7.Text;
            string Nombre = textBox10.Text;
            string fecha = dateTimePicker2.Value.ToString("dd/MM/yyyy");
            decimal cantidad = decimal.Parse(textBox11.Text);
            bool existeprod = false;
            for (int i = 0; i < Lv2.Items.Count; i++)
            {
                if (Lv2.Items[i].SubItems[1].Text == cvproducto)
                {
                    existeprod = true;
                    break;
                }
            }


            if (existeprod == true)
            {
                MessageBox.Show("Existen servicios o productos igual al que desea ingresar, eliminelo y vuelva a insertar nuevamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ListViewItem lvi = new ListViewItem(NumPreServ.ToString());
            lvi.SubItems.Add(cvproducto);
            lvi.SubItems.Add(cantidad.ToString());
            lvi.SubItems.Add(Nombre);
            lvi.SubItems.Add(fecha);
            Lv2.Items.Add(lvi);

            textBox7.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            button1.Focus();

        }


        //---------------------------------------------------------------
        public void BuscarPaciente()
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;
            bool clientebloqueado = false;
            string mensajebloq = "";
            string query = "Select * from ClientesBloqueados where idcliente='" + textBox6.Text.Trim() + "'";
            leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                mensajebloq = leer["observacion"].ToString().ToUpper();
                clientebloqueado = true;
            }
            conecta.CierraConexion();

            if (clientebloqueado)
            {
                MessageBox.Show("NO SE LE BRINDARA ATENCION AL PACIENTE BLOQUEADO CAUSA:\n\n" + mensajebloq, "SAIMED", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox6.Text = "";
                return;
            }

            query = "Select NoExpediente,email,celular, nombre + ' ' + apaterno + ' ' + amaterno as nombrec from  pacientes where clave='" + textBox6.Text.Trim() + "'";
            leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                textBox1.Text = leer["nombrec"].ToString();
                textBox5.Text = leer["CELULAR"].ToString();
                comboBox4.SelectedIndex = 1;
            }
            conecta.CierraConexion();
        }

        public void LimpiarDatos()
        {
            label8.Text = "0";
            label11.Text = "";
            textBox3.Text = "";
            textBox1.Text = "";
            textBox9.Text = "";
            textBox14.Text = "";
            dateTimePicker3.Enabled = false;
            textBox2.Text = "";

            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            textBox6.Text = "";
            textBox5.Text = "";
            textBox2.Text = "";
        }

        private void Lv_DoubleClick(object sender, EventArgs e)
        {
            if (Lv.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = Lv.SelectedIndices;
                foreach (int item in seleccion)
                {
                    DetallesModifica(item);
                }
            }
        }

        string NOMBREPACIENTE= "";
        string CLAVEPACIENTE= "";
        public void DetallesModifica(int index)
        {
            LimpiarDatos();

            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            textBox12.Text = "";

            string numticket = Lv.Items[index].Text;
            string cvdoctor= Lv.Items[index].SubItems[1].Text;
            string cv = Lv.Items[index].SubItems[2].Text;
            string nombre = Lv.Items[index].SubItems[3].Text;
            string fecha= Lv.Items[index].SubItems[4].Text;
            string horainicia = Lv.Items[index].SubItems[5].Text;
            string estatus = Lv.Items[index].SubItems[7].Text;
            NOMBREPACIENTE = nombre;
            CLAVEPACIENTE=cv;
            label11.Text = fecha;
            label8.Text = numticket;
            combos.ComboDoctores(comboBox2);
            comboBox2.SelectedValue = cvdoctor;

            //ring hora = leer2["horainicia"].ToString().Replace(".", ":");
            string[] validacion = horainicia.Replace(".",":").Split(':');

            try
            {
                int horaVal = int.Parse(validacion[0]);
                int minutoVal = int.Parse(validacion[1]);
                int segundoVal = 0;

                if (horaVal > 23)
                {
                    horaVal = 0;
                }

                if (minutoVal > 60)
                {
                    minutoVal = 35;
                }
                string horaFinal = horaVal.ToString() + ':' + minutoVal.ToString() + ':' + segundoVal.ToString();

                dateTimePicker3.Value = DateTime.Parse(horaFinal);
            }
            catch (Exception)
            {

                dateTimePicker3.Value = DateTime.Now;
            }
            

            
            panel5.Visible = false;

            if (cvdoctor == "1" || cvdoctor == "2" || cvdoctor == "2")
            {
                panel5.Visible = true;
            }

            textBox3.Text = TiempoPred.ToString();
            dateTimePicker2.Value = dateTimePicker3.Value.AddMinutes(TiempoPred);

            comboBox5.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;

            if (estatus == "PAGADO")
            {
                Lv.Visible = true;
                panel3.Visible = false;
                MessageBox.Show("El espacio se encuentra pagado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (estatus == "SIN PAGAR")
            {
                Lv.Visible = true;
                panel3.Visible = false;
                MessageBox.Show("El espacio ya se encuentra ocupado , falta el pago", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //if (estatus == "CANCELADO")
            //{
            //    Lv.Visible = true;
            //    panel3.Visible = false;
            //    MessageBox.Show("El espacio se encuentra ocupado, aun sin pago", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;

            //}

            if (estatus == "ATENDIDO")
            {
                Lv.Visible = true;
                panel3.Visible = false;
                MessageBox.Show("El paciente ya se atendido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }



            if (label27.Text != "" && label27.Text != "0")
            {
                textBox6.Text = label27.Text;
                BuscarPaciente();
            }
          
            Lv.Visible = false;
            panel3.Visible = true;
            textBox3.Text = TiempoPred.ToString();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lv.Visible = true;
            panel3.Visible = false;
        }

        public void ProcesoGuardarPaciente()
        {

            cargaConsecutivo();
            RecolectaPacienteReg();

            if (GuardarCliente()) {
                GuardaPacienteDirecto();
                actualizaConsecutivo();
            };
        }

        public bool GuardarCliente()
        {


            try
            {
                conectorSql conecta = new conectorSql();
                string Query = "";

                Query = "Delete from clientes where cvcliente='" + CLAVE + "'";
                conecta.Excute(Query);
                conecta.CierraConexion();

                Query = "set IDENTITY_INSERT clientes on insert into clientes(";
                Query = Query + "cvcliente";
                Query = Query + ",nombre";
                Query = Query + ",telefono";
                Query = Query + ",email";
                Query = Query + ",email2";
                Query = Query + ",celular";
                Query = Query + ",direccion";
                Query = Query + ",rfc";
                Query = Query + ",direfiscal";
                Query = Query + ",empresa";
                Query = Query + ",calleE";
                Query = Query + ",ColoniaE";
                Query = Query + ",MunicipioE";
                Query = Query + ",EstadoE";
                Query = Query + ",CodE";
                Query = Query + ",PaisE";
                Query = Query + ",CalleF";
                Query = Query + ",ColoniaF";
                Query = Query + ",MunicipioF";
                Query = Query + ",EstadoF";
                Query = Query + ",CodF";
                Query = Query + ",PaisF";
                Query = Query + ",fechamod";
                Query = Query + ",fcodmod";
                Query = Query + ",sincronizado";
                Query = Query + ",actividad";
                Query = Query + ",numf";
                Query = Query + ",observafact";

                Query = Query + ",numcuenta";
                Query = Query + ",cvbanco";
                Query = Query + ",metodopago";
                Query = Query + ",vendedor";
                Query = Query + ",formapago";

                Query = Query + ",tipopago";
                Query = Query + ",diascredito";
                Query = Query + ",nombrefactura";
                Query = Query + ",factura";

                Query = Query + ",activo";
                Query = Query + ",saldo";
                //Query = Query + ",dcredito";
                Query = Query + ",saldocredito";
                Query = Query + ",idcliente";
                Query = Query + ",curp";
                Query = Query + ",monedero)";
                Query = Query + " values(";

                Query = Query + "'" + CLAVE + "'";
                Query = Query + ",'" + NOMBRE + " " + APATERNO + " " + AMATERNO + "'";
                Query = Query + ",'" + TELEFONO + "'";
                Query = Query + ",'" + EMAIL + "'";
                Query = Query + ",'" + EMAIL2 + "'";
                Query = Query + ",'" + CELULAR + "'";
                Query = Query + ",'" + CALLE + " ," + NoCalle + " " + MUNICIPIO + " " + ESTADO + "'";
                Query = Query + ",'XAXX010101000'";
                Query = Query + ",'" + CALLE + " ," + NoCalle + " " + MUNICIPIO + " " + ESTADO + "'";
                Query = Query + ",'" + NOMBRE + " " + APATERNO + " " + AMATERNO + "'";

                Query = Query + ",'" + CALLE + "'";
                Query = Query + ",'" + COLONIA + "'";
                Query = Query + ",'" + MUNICIPIO + "'";
                Query = Query + ",'" + ESTADO + "'";
                Query = Query + ",'" + CP + "'";
                Query = Query + ",'MÈXICO'";
                Query = Query + ",'" + CALLE + "'";
                Query = Query + ",'" + COLONIA + "'";
                Query = Query + ",'" + MUNICIPIO + "'";
                Query = Query + ",'" + ESTADO + "'";
                Query = Query + ",'" + CP + "'";
                Query = Query + ",'MÈXICO'";
                Query = Query + ",'" + FECHA + "'";
                Query = Query + ",'" + FCOD + "'";
                Query = Query + ",'0'";
                Query = Query + ",'FISICA'";
                Query = Query + ",'" + NoCalle + "'";
                Query = Query + ",''";

                Query = Query + ",'0000'";
                Query = Query + ",'0'";
                Query = Query + ",'No Identificado'";
                Query = Query + ",'NO APLICA'";
                Query = Query + ",'PAGO EN UNA SOLA EXHIBICIÓN'";

                Query = Query + ",'Contado'";
                Query = Query + ",'0'";
                Query = Query + ",''";
                Query = Query + ",'SI'";

                Query = Query + ",'1'";
                Query = Query + ",'0'";
                //Query = Query + ",'" + DCREDITO + "'";
                Query = Query + ",'0'";
                Query = Query + ",'" + CLAVE + "'," + CURP + "'";
                Query = Query + "','0')";
                return conecta.Excute(Query);
            }
            catch (Exception e)
            {

                throw;
            }
            


        }


        public void cargaConsecutivo()
        {
            string Numero = "1";
            conectorSql conecta = new conectorSql();
            string Query = "Select paciente from consecutivos where paciente <>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                Numero = leer["paciente"].ToString();
            }
            conecta.CierraConexion();
            textBox6.Text = Numero.ToString();
        }

        public void RecolectaPacienteReg()
        {
            string cvdoctorRM = comboBox1.SelectedValue.ToString();

            CURP = textBox14.Text.Trim();
            NOMBRE = textBox1.Text.Trim();
            APATERNO = "";
            AMATERNO = "";
            GENERO = "FEMENINO";
            ESCOLARIDAD = "";
            EMAIL = "";
            EDAD = textBox9.Text.Trim();
            ECivil = "";
            NoHijos = "0";
            OCUPACION = "";
            TELEFONO = textBox5.Text.Trim();
            CALLE = "";
            NoCalle = "ND";
            CP = "0";
            COLONIA = "ND";
            MUNICIPIO = "ND";
            CIUDAD = "ND";
            ESTADO = "ND";

            Pregunta1 = "ND";
            Pregunta2 = "ND";
            Pregunta3 = "ND";
            RecibeAvisos = "ND";

            NoExpediente = "";
            SERVICIO = "ND";
            MEDICO = "ND";
            TURNO = "ND";
            OBSERVACIONES = "ND";
            FECHA = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            FCOD = dateTimePicker1.Value.ToString("yyyyMMdd");

            LUGARNAC = "ND";
            FECHANAC = "";

            CLAVE = textBox6.Text;
            CELULAR = textBox5.Text;
            EMAIL2 = "ND";
            EXPGINECO = "";
            EXPDENTAL = "";
            EXPOFTAM = "";

            if (cvdoctorRM == "1") EXPGINECO = textBox12.Text.Trim();
            if (cvdoctorRM == "2") EXPDENTAL = textBox12.Text.Trim();
            if (cvdoctorRM == "7") EXPOFTAM = textBox12.Text.Trim();

        }

        public void GuardaPacienteDirecto()
        {
            conectorSql conecta = new conectorSql();
            string Query = "";

            Query = "Delete from pacientes where clave='" + CLAVE+ "'";
            conecta.Excute(Query);
            conecta.CierraConexion();

            Query = "insert into Pacientes(";
            Query += "NOMBRE,";
            Query += "APATERNO,";
            Query += "AMATERNO,";
            Query += "GENERO,";
            Query += "ESCOLARIDAD,";
            Query += "EMAIL,";
            Query += "EDAD,";
            Query += "ECivil,";
            Query += "NoHijos,";
            Query += "OCUPACION,";
            Query += "TELEFONO,";
            Query += "CALLE,";
            Query += "NoCalle,";
            Query += "CP,";
            Query += "COLONIA,";
            Query += "MUNICIPIO,";
            Query += "CIUDAD,";
            Query += "ESTADO,";
            Query += "Pregunta1,";
            Query += "Pregunta2,";
            Query += "Pregunta3,";
            Query += "RecibeAvisos,";
            Query += "NoExpediente,";
            Query += "SERVICIO,";
            Query += "MEDICO,";
            Query += "TURNO,";
            Query += "OBSERVACIONES,";
            Query += "FECHA,";
            Query += "FCOD,";
            Query += "LUGARNAC,";
            Query += "FECHANAC,";

            Query += "CLAVE,";
            Query += "EMAIL2,";
            Query += "CELULAR,";

            Query += "expdental,";
            Query += "expgineco,";
            Query += "expoftamolgo,";
            Query += "curp,";


            Query += "STATUS)";
            Query += "values(";
            Query += "'" + NOMBRE + "'";
            Query += ",'" + APATERNO + "'";
            Query += ",'" + AMATERNO + "'";
            Query += ",'" + GENERO + "'";
            Query += ",'" + ESCOLARIDAD + "'";
            Query += ",'" + EMAIL + "'";
            Query += ",'" + EDAD + "'";
            Query += ",'" + ECivil + "'";
            Query += ",'" + NoHijos + "'";
            Query += ",'" + OCUPACION + "'";
            Query += ",'" + TELEFONO + "'";
            Query += ",'" + CALLE + "'";
            Query += ",'" + NoCalle + "'";
            Query += ",'" + CP + "'";
            Query += ",'" + COLONIA + "'";
            Query += ",'" + MUNICIPIO + "'";
            Query += ",'" + CIUDAD + "'";
            Query += ",'" + ESTADO + "'";
            Query += ",'" + Pregunta1 + "'";
            Query += ",'" + Pregunta2 + "'";
            Query += ",'" + Pregunta3 + "'";
            Query += ",'" + RecibeAvisos + "'";
            Query += ",'" + NoExpediente + "'";
            Query += ",'" + SERVICIO + "'";
            Query += ",'" + MEDICO + "'";
            Query += ",'" + TURNO + "'";
            Query += ",'" + OBSERVACIONES + "'";
            Query += ",'" + FECHA + "'";
            Query += ",'" + FCOD + "'";
            Query += ",'" + LUGARNAC + "'";
            Query += ",'" + FECHANAC + "'";

            Query += ",'" + CLAVE + "'";
            Query += ",'" + EMAIL2 + "'";
            Query += ",'" + CELULAR + "'";

            Query += ",'" + EXPDENTAL + "'";
            Query += ",'" + EXPGINECO + "'";
            Query += ",'" + EXPOFTAM + "'";
            Query += ",'" + CURP+ "'";

            Query += ",'" + STATUS + "')";
            conecta.Excute(Query);
            conecta.CierraConexion();
          

        }

        public bool ExistePaciente()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from pacientes where clave='" + textBox6.Text.Trim() + "'";
            bool existe = conecta.ExisteRegistro(Query);
            return existe;
        }

        public int NUMEXPEDIENTEG = 0;
        public void cargaConsecutivoExpedienteGine()
        {
            NUMEXPEDIENTEG = 1;
            conectorSql conecta = new conectorSql();
            string Query = "Select numexpegine from consecutivos where numexpegine <>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                NUMEXPEDIENTEG = int.Parse(leer["numexpegine"].ToString());
            }
            conecta.CierraConexion();
            textBox12.Text = NUMEXPEDIENTEG.ToString();
        }

        public void cargaConsecutivoExpedienteDental()
        {
            NUMEXPEDIENTEG = 1;
            conectorSql conecta = new conectorSql();
            string Query = "Select numexpedental from consecutivos where numexpedental <>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                NUMEXPEDIENTEG = int.Parse(leer["numexpedental"].ToString());
            }
            conecta.CierraConexion();
            textBox12.Text = NUMEXPEDIENTEG.ToString();
        }

        public void cargaConsecutivoExpedienteOftamologo()
        {
            NUMEXPEDIENTEG = 1;
            conectorSql conecta = new conectorSql();
            string Query = "Select numexpeofta from consecutivos where numexpeofta <>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                NUMEXPEDIENTEG = int.Parse(leer["numexpeofta"].ToString());
            }
            conecta.CierraConexion();
            textBox12.Text = NUMEXPEDIENTEG.ToString();
        }

        public string existeNombre(string nombre)
        {
            string nombrereg = nombre;
            bool existeNom=false;
            string clave = "";
            conectorSql conecta = new conectorSql();
            string consulta = "select * from pacientes where nombre='" + nombre +"'";
            SqlDataReader leer = conecta.RecordInfo(consulta);
            while (leer.Read())
            {
                existeNom = true;
                clave = leer["clave"].ToString();
            }
            conecta.CierraConexion();
            return clave;
        }


        public string existemail(string clave)
        {
            string nombrereg = clave;
            bool existeNom = false;
            string email = "";
            conectorSql conecta = new conectorSql();
            string consulta = "select * from pacientes where clave='" + clave + "'";
            SqlDataReader leer = conecta.RecordInfo(consulta);
            while (leer.Read())
            {
                existeNom = true;
                email = leer["email"].ToString();
            }
            conecta.CierraConexion();
            return email;
        }

        private void button1_Click(object sender, EventArgs e)
        {
             if (textBox6.Text.Trim() == "")
            {
                timer1.Enabled = true;
                textBox6.Enabled = false;
            }

            if (textBox6.Text == "0" || textBox6.Text == "")
            {
                MessageBox.Show("Falta ingresar una clave para el paciente ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox6.Text = "";
                textBox6.Focus();
                return;
            }

            string claveNombreReg=existeNombre(textBox1.Text.ToUpper());
            if (claveNombreReg!="") 
            {
                checkBox1.Checked = false;
                timer1.Enabled = false;
                textBox6.Enabled = true;
                // MessageBox.Show("El nombre ya esta registrado la clave del paciente es "+ claveNombreReg.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox6.Text=claveNombreReg;
                BuscarPaciente();
                // return;
            }

            if (textBox14.Text.Trim()!="")
            {
                CURP = textBox14.Text.Trim();
                BusquedaExistenteCURP();
                if (existepacienteREGM==true)
                {
                    MessageBox.Show("El paciente ya se encuentra registrado su numero de paciente es:" + IDPACIENTEREGM.ToString(), "SAIMED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            
            if (checkBox1.Checked == true)
            {
                ProcesoGuardarPaciente();
                textBox14.Text = "";

                string cvdoctor2 = comboBox1.SelectedValue.ToString();
                if (checkBox7.Checked == true)
                {
                    if (cvdoctor2 == "1")
                    {
                        if (textBox12.Text.Trim() != "") actualizaConsecutivoExpedienteGine();
                    }
                    if (cvdoctor2 == "2")
                    {
                        if (textBox12.Text.Trim() != "") actualizaConsecutivoExpedienteDental();
                    }
                    if (cvdoctor2 == "7")
                    {
                        if (textBox12.Text.Trim() != "") actualizaConsecutivoExpedienteOftamo();
                    }
                }
            }



            string numticket = label8.Text;
            string nombre = textBox1.Text.Trim();
            string cvdoctor = comboBox1.SelectedValue.ToString();
            string cvservicio = "";
            string observa = textBox2.Text.Trim();
            string pvisita = comboBox4.Text;
            string cvpaciente = textBox6.Text.Trim();

            if (textBox3.Text.Trim() == "") textBox3.Text = "15";
            int  timepoCon = int.Parse(textBox3.Text.Trim());
            string nombreservicio = "";
            if (Lv2.Items.Count >= 1)
            {
                cvservicio = Lv2.Items[0].SubItems[1].Text;
                nombreservicio = Lv2.Items[0].SubItems[3].Text;
            }

            DateTime HoraInicia = dateTimePicker3.Value;

            if (pvisita == "PRIMERA VEZ") pvisita = "SI";
            else pvisita = "NO";

            string tipor = comboBox5.Text.ToString();

            Calculodetiempos();

            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();

            string telefono = textBox5.Text.Trim();

            string ValArea = label40.Text.Trim();
            string TURNOTICKET = ValArea + "-" + numticket;

            valoresg.SERVICIONOMBRE = cvservicio;

            string Query = "Update citas set  nombre='" + nombre + "',  cvservicio='" + cvservicio + "'";
            Query = Query + "  , observa='" + observa + "' , emite='" + valoresg.USUARIOSIS + "', Estatus='SIN PAGAR', tipo='" + tipor + "', NombreServicio='"+ nombreservicio + "'";
            Query = Query + "  , cvpaciente='" + cvpaciente + "', ttiempo='" + timepoCon + "',  telefono='" + telefono+ "'";
           // Query = Query + "  , estatusserv='POR ATENDER'";
          // Query = Query + "  , idturno='" + TURNOTICKET.ToString() + "'";
            Query = Query + "  , primeravez='" + pvisita + "'    where progresivo='" + numticket + "' and fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and cvdoctor='" + comboBox1.SelectedValue.ToString()+ "'";
            conecta.Excute(Query);

            if (checkBox5.Checked == true && cvdoctor != "1") EspacioLibreGinecologia();
            if (checkBox3.Checked == true && cvdoctor != "7") EspacioLibreOftamologo();
            if (checkBox4.Checked == true && cvdoctor != "2") EspacioLibreDental();
            if (checkBox6.Checked == true && cvdoctor != "3") EspacioLibreUltrasonido();


            if (timepoCon > TiempoPred) //-- si es mayor el tiempo de lo estimado 
            {
                decimal MHora = HoraInicia.Hour;
                decimal Mminutos = HoraInicia.Minute;
                decimal ttminutos = (MHora * 60) + Mminutos;

                DateTime TiempoInicia=dateTimePicker3.Value;
                DateTime TiempoFinal = dateTimePicker2.Value.AddMinutes(1);
                Query = "Select * from citas where fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and cvdoctor='" + comboBox1.SelectedValue.ToString() + "' and hmininicia>'" + ttminutos.ToString() + "' and progresivo>'" + numticket + "' order by progresivo asc";
                SqlDataReader leer2 = conecta.RecordInfo(Query);
                while (leer2.Read())
                {
                    string hora = leer2["horainicia"].ToString().Replace(".",":");
                    string[] validacion = hora.Split(':');

                    int horaVal = int.Parse(validacion[0]);
                    int minutoVal = int.Parse(validacion[1]);
                    int segundoVal = 0;

                    if (horaVal > 23)
                    {
                        horaVal = 0;
                    }

                    if (minutoVal > 60)
                    {
                        minutoVal = 35;
                    }

                    string horaFinal = horaVal.ToString() + ':' + minutoVal.ToString() + ':' + segundoVal.ToString();

                    TiempoInicia =DateTime.Parse(horaFinal.ToString());


                    int minutosconsulta=int.Parse(leer2["ttiempo"].ToString());
                    string progresivo= leer2["progresivo"].ToString();

                    TiempoInicia=TiempoFinal;
                    TiempoFinal=TiempoFinal.AddMinutes(minutosconsulta);


                    int Hmininicia = TiempoInicia.Hour * 60 + TiempoInicia.Minute;

                    string consulta = "Update citas set horainicia='" + TiempoInicia.ToString("HH:mm:00") + "', hmininicia='" + Hmininicia.ToString() + "', ttiempo='" + minutosconsulta.ToString() + "' where fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and cvdoctor='" + comboBox1.SelectedValue.ToString() + "' and progresivo='" + progresivo+ "'";
                    conecta2.Excute(consulta);
                    conecta2.CierraConexion();

                    valoresg.FECHACITA = " Con fecha " + dateTimePicker1.Value.ToString("yyyy-MM-dd") + " a las " + TiempoInicia.ToString("HH:mm:00");

                }
                conecta.CierraConexion();

                

            }

            GuardarRegistroServicio();

            DialogResult reply = MessageBox.Show("¿Impresion de ticket num. " + numticket+ "?", "Impresion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.Yes)
            {
                mandarReporteCristal();
            }

            /*
             * Enviar correo al cliente de su cita
             * 
             * */
            MailNotifications mails = new MailNotifications();
            //(string cita, string email, string nombre, string fecha , string servicio)
            // valida mail si es correcto
            string emailPaciente = existemail(cvpaciente);

            if (ValidateData.IsValidEmail(emailPaciente) == true)
            { 
                mails.SendMail(numticket + " su id único:" + cvpaciente, emailPaciente, nombre, "Con fecha: " + HoraInicia.ToString(), sel_nomArea, true);
                return;
            }


            MessageBox.Show("Se registro correctamente la cita ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Lv2.Items.Clear();
            Lv2.Columns.Clear();
            label27.Text = "0";

            panel3.Visible = false;
            ListadoDeCitas();
            panel3.Visible = false;
            Lv.Visible = true;
        }

        public bool existepacienteREGM = false;
        public int  IDPACIENTEREGM= 0;
        public void BusquedaExistenteCURP()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from pacientes where curp='" + CURP+"'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                IDPACIENTEREGM = int.Parse(leer["CLAVE"].ToString());
                existepacienteREGM = true;
            }
            conecta.CierraConexion();

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


        public void EspacioLibreGinecologia()
        {
            int HoraminSalida = dateTimePicker2.Value.Hour * 60;
            int Minsalida = dateTimePicker2.Value.Minute;
            int Hminsalida = HoraminSalida + Minsalida;

            string progresivo = "";
            conectorSql conecta = new conectorSql();
            string Query = "Select * from citas where cvdoctor='1' and estatus='LIBRE' and hmininicia>'" +Hminsalida.ToString() + "' order by hmininicia asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                progresivo = leer["progresivo"].ToString();
                break;
            }
            conecta.CierraConexion();

            string nombre = textBox1.Text.Trim();
            string cvdoctor = comboBox1.SelectedValue.ToString();
            string cvservicio = "";
            string observa = textBox2.Text.Trim();
            string pvisita = comboBox4.Text;
            string cvpaciente = textBox6.Text.Trim();
            int timepoCon = int.Parse(textBox3.Text.Trim());
            string nombreservicio = "";
            string telefono = textBox5.Text.Trim();
            string tipor = comboBox5.Text.ToString();

            if (Lv2.Items.Count >= 1)
            {
                cvservicio = Lv2.Items[0].SubItems[1].Text;
                nombreservicio = Lv2.Items[0].SubItems[3].Text;
            }
            if (pvisita == "PRIMERA VEZ") pvisita = "SI";
            else pvisita = "NO";

            Query = "Update citas set  nombre='" + nombre + "',  cvservicio='" + cvservicio + "'";
            Query = Query + "  , observa='" + observa + "' , emite='" + valoresg.USUARIOSIS + "', Estatus='SIN PAGAR', tipo='" + tipor + "', NombreServicio='" + nombreservicio + "'";
            Query = Query + "  , cvpaciente='" + cvpaciente + "',  telefono='" + telefono + "'";
            Query = Query + "  , primeravez='" + pvisita + "'    where progresivo='" + progresivo + "' and fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and cvdoctor='1'";
            conecta.Excute(Query);
            conecta.CierraConexion();

        }


        public void EspacioLibreDental()
        {
            int HoraminSalida = dateTimePicker2.Value.Hour * 60;
            int Minsalida = dateTimePicker2.Value.Minute;
            int Hminsalida = HoraminSalida + Minsalida;

            string progresivo = "";
            conectorSql conecta = new conectorSql();
            string Query = "Select * from citas where cvdoctor='2' and estatus='LIBRE' and hmininicia>'" + Hminsalida.ToString() + "' order by hmininicia asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                progresivo = leer["progresivo"].ToString();
                break;
            }
            conecta.CierraConexion();

            string nombre = textBox1.Text.Trim();
            string cvdoctor = "2";
            string cvservicio = "";
            string observa = textBox2.Text.Trim();
            string pvisita = comboBox4.Text;
            string cvpaciente = textBox6.Text.Trim();
            int timepoCon = int.Parse(textBox3.Text.Trim());
            string nombreservicio = "";
            string telefono = textBox5.Text.Trim();
            string tipor = comboBox5.Text.ToString();

            if (Lv2.Items.Count >= 1)
            {
                cvservicio = Lv2.Items[0].SubItems[1].Text;
                nombreservicio = Lv2.Items[0].SubItems[3].Text;
            }
            if (pvisita == "PRIMERA VEZ") pvisita = "SI";
            else pvisita = "NO";

            Query = "Update citas set  nombre='" + nombre + "',  cvservicio='" + cvservicio + "'";
            Query = Query + "  , observa='" + observa + "' , emite='" + valoresg.USUARIOSIS + "', Estatus='SIN PAGAR', tipo='" + tipor + "', NombreServicio='" + nombreservicio + "'";
            Query = Query + "  , cvpaciente='" + cvpaciente + "',  telefono='" + telefono + "'";
            Query = Query + "  , primeravez='" + pvisita + "'    where progresivo='" + progresivo + "' and fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and cvdoctor='2'";
            conecta.Excute(Query);
            conecta.CierraConexion();

        }

        public void EspacioLibreOftamologo()
        {
            int HoraminSalida = dateTimePicker2.Value.Hour * 60;
            int Minsalida = dateTimePicker2.Value.Minute;
            int Hminsalida = HoraminSalida + Minsalida;

            string progresivo = "";
            conectorSql conecta = new conectorSql();
            string Query = "Select * from citas where cvdoctor='7' and estatus='LIBRE' and hmininicia>'" + Hminsalida.ToString() + "' order by hmininicia asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                progresivo = leer["progresivo"].ToString();
                break;
            }
            conecta.CierraConexion();

            string nombre = textBox1.Text.Trim();
            string cvdoctor = "7";
            string cvservicio = "";
            string observa = textBox2.Text.Trim();
            string pvisita = comboBox4.Text;
            string cvpaciente = textBox6.Text.Trim();
            int timepoCon = int.Parse(textBox3.Text.Trim());
            string nombreservicio = "";
            string telefono = textBox5.Text.Trim();
            string tipor = comboBox5.Text.ToString();

            if (Lv2.Items.Count >= 1)
            {
                cvservicio = Lv2.Items[0].SubItems[1].Text;
                nombreservicio = Lv2.Items[0].SubItems[3].Text;
            }
            if (pvisita == "PRIMERA VEZ") pvisita = "SI";
            else pvisita = "NO";

            Query = "Update citas set  nombre='" + nombre + "',  cvservicio='" + cvservicio + "'";
            Query = Query + "  , observa='" + observa + "' , emite='" + valoresg.USUARIOSIS + "', Estatus='SIN PAGAR', tipo='" + tipor + "', NombreServicio='" + nombreservicio + "'";
            Query = Query + "  , cvpaciente='" + cvpaciente + "',  telefono='" + telefono + "'";
            Query = Query + "  , primeravez='" + pvisita + "'    where progresivo='" + progresivo + "' and fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and cvdoctor='7'";
            conecta.Excute(Query);
            conecta.CierraConexion();

        }

        public void EspacioLibreUltrasonido()
        {
            int HoraminSalida = dateTimePicker2.Value.Hour * 60;
            int Minsalida = dateTimePicker2.Value.Minute;
            int Hminsalida = HoraminSalida + Minsalida;

            string progresivo = "";
            conectorSql conecta = new conectorSql();
            string Query = "Select * from citas where cvdoctor='3' and estatus='LIBRE' and hmininicia>'" + Hminsalida.ToString() + "' order by hmininicia asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                progresivo = leer["progresivo"].ToString();
                break;
            }
            conecta.CierraConexion();

            string nombre = textBox1.Text.Trim();
            string cvdoctor = "3";
            string cvservicio = "";
            string observa = textBox2.Text.Trim();
            string pvisita = comboBox4.Text;
            string cvpaciente = textBox6.Text.Trim();
         
            string nombreservicio = "";
            string telefono = textBox5.Text.Trim();
            string tipor = comboBox5.Text.ToString();

            if (Lv2.Items.Count >= 1)
            {
                cvservicio = Lv2.Items[0].SubItems[1].Text;
                nombreservicio = Lv2.Items[0].SubItems[3].Text;
            }

            if (pvisita == "PRIMERA VEZ") pvisita = "SI";
            else pvisita = "NO";

            Query = "Update citas set  nombre='" + nombre + "',  cvservicio='" + cvservicio + "'";
            Query = Query + "  , observa='" + observa + "' , emite='" + valoresg.USUARIOSIS + "', Estatus='SIN PAGAR', tipo='" + tipor + "', NombreServicio='" + nombreservicio + "'";
            Query = Query + "  , cvpaciente='" + cvpaciente + "',   telefono='" + telefono + "'";
            Query = Query + "  , primeravez='" + pvisita + "'    where progresivo='" + progresivo + "' and fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and cvdoctor='3'";
            conecta.Excute(Query);
            conecta.CierraConexion();

        }

        public void mandarReporteCristal()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Ingrese nombre del paciente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ReportDocument cryRpt = new ReportDocument();

                string CadenaReporte = @"\\SRV-DATACENTER\\tmp\\reports\\ImpresoTicket.rpt";
                string cvdoctor = comboBox1.SelectedValue.ToString();
                string cvservicio = "0";
                string numexpediente="";
                DateTime FFechacod = dateTimePicker1.Value;
                string fechacod = FFechacod.ToString("yyyyMMdd");
                int turno = int.Parse(label8.Text) ;
                DataSet ds = new DataSet();

                DataSetTicketTableAdapters.CitasTableAdapter citas = new DataSetTicketTableAdapters.CitasTableAdapter();
                DataSetTicket.CitasDataTable tcitas = new DataSetTicket.CitasDataTable();
                citas.Fill(tcitas, cvdoctor,turno, fechacod);

                DataSetTicketTableAdapters.DoctoresTableAdapter doctor= new DataSetTicketTableAdapters.DoctoresTableAdapter();
                DataSetTicket.DoctoresDataTable tdoctor = new DataSetTicket.DoctoresDataTable();
                doctor.Fill(tdoctor, cvdoctor);

                DataSetTicketTableAdapters.CatServiciosTableAdapter servicios = new DataSetTicketTableAdapters.CatServiciosTableAdapter();
                DataSetTicket.CatServiciosDataTable tservicio = new DataSetTicket.CatServiciosDataTable();
                servicios.Fill(tservicio,cvservicio);

                DataSetTicketTableAdapters.LogoEmpresaTableAdapter logoemp= new DataSetTicketTableAdapters.LogoEmpresaTableAdapter();
                DataSetTicket.LogoEmpresaDataTable tlogoemp = new DataSetTicket.LogoEmpresaDataTable();
                logoemp.Fill(tlogoemp);

                DataSetTicketTableAdapters.ParametrosReciboTableAdapter parametro = new DataSetTicketTableAdapters.ParametrosReciboTableAdapter();
                DataSetTicket.ParametrosReciboDataTable tparametro = new DataSetTicket.ParametrosReciboDataTable();
                parametro.Fill(tparametro);

            

                ds.Clear();
                ds.Tables.Add(tcitas);
                ds.Tables.Add(tdoctor);
                ds.Tables.Add(tservicio);
                ds.Tables.Add(tlogoemp);
                ds.Tables.Add(tparametro);

                cryRpt.Load(CadenaReporte);
                
                cryRpt.SetDataSource(ds);
                // cryRpt.SetParameterValue("fecha1", 13);
                string NombreArchivo = @"C:\\tmp\\reports\\TicketImpreso.pdf";
                //// cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, NombreArchivo);
                cryRpt.PrintToPrinter(1, false, 0, 0);
                cryRpt.Close();
                cryRpt.Dispose();

                MessageBox.Show("Se mando a imprimir el ticket ", "Impresion de Ticket", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public string Gnumticket = "";
        public string Gestatus = "";
        public string nombreservicio = "";
        public string Sel_numrecibo = "";
        public string sel_nomArea = "";
        public void DetallesModifica2(int index)
        {

            Gnumticket = Lv.Items[index].Text;
            Gestatus = Lv.Items[index].SubItems[7].Text;
            cvpaciente = Lv.Items[index].SubItems[2].Text;
            nombrepaciente= Lv.Items[index].SubItems[3].Text;
            nombreservicio= Lv.Items[index].SubItems[9].Text;
            Sel_cvdoctor = comboBox1.SelectedValue.ToString();
            Sel_FechaCitare= Lv.Items[index].SubItems[4].Text;
            Sel_numrecibo = Lv.Items[index].SubItems[11].Text;
            sel_nomArea = comboBox1.Text;

            CLAVEPACIENTE = Lv.Items[index].SubItems[2].Text;
            NOMBREPACIENTE = Lv.Items[index].SubItems[3].Text;
            

            valoresg.Reagendar_cvdoctor = Sel_cvdoctor;
            valoresg.Reagendar_cvpaciente = cvpaciente;
            valoresg.Reagendar_cvservicio="RMAN";
            valoresg.Reagendar_servicio = "REPROGRAMADO - " + nombreservicio;
            valoresg.Reagendar_fecha = Sel_FechaCitare;
            valoresg.Reagendar_estatus = Gestatus;
            valoresg.Reagendar_numRecibo = Sel_numrecibo;
            valoresg.Reagendar_NomArea = sel_nomArea;
            valoresg.Reagendar_Nompaciente = nombrepaciente;
        }

        public void RegistrarRecibosmanual()
        {
            if (Gestatus == "SIN PAGAR" || Gestatus == "OCUPADO")
            {
                panel4.Visible = true;
                label34.Text = Gnumticket;
                label29.Text = cvpaciente;
                label31.Text = nombrepaciente;
                textBox4.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Gestatus == "LIBRE" || Gestatus == "ATENDIDO")
            {
                MessageBox.Show("No se puede cancelar un espacio libre", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult reply = MessageBox.Show("¿Desea Cancelar la cita num. " + Gnumticket + "?", "Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;

            conectorSql conecta = new conectorSql();
            string Query = "Update citas set  estatus='CANCELADO', recibopago='0', ttiempo='0' where progresivo='" + Gnumticket + "' and fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd")+ "' and cvdoctor='" + comboBox1.SelectedValue.ToString() + "'";
            conecta.Excute(Query);

            Query = "Delete DetallesPreServicio where cvpaciente='" + cvpaciente + "' and estatus='CAPTURADO' and fechacita='" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "' and numticket='" + Gnumticket + "'";
            conecta.Excute(Query);


            MessageBox.Show("Se cancelo la cita ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

      

        private void RegistroCitas_Activated(object sender, EventArgs e)
        {
            if (valoresg.CLAVEPAC != "")
            {
                label27.Text = valoresg.CLAVEPAC;
                textBox6.Text = valoresg.CLAVEPAC;
                textBox14.Text = valoresg.CURP;
                valoresg.CLAVEPAC = "";
                comboBox4.SelectedIndex = 1;
                BuscarPaciente();
            }



            if (Modremision.CVPRODUCTO != "")
            {
                textBox7.Text = Modremision.CVPRODUCTO;
                Modremision.CVPRODUCTO = "";
                BuscarInfo();
            }
        }

        public void BuscarNombrePaciente()
        {
            conectorSql conecta = new conectorSql();
            string query = "Select  nombre + ' ' + APATERNO + ' ' + AMATERNO as nombrec from pacientes where NoExpediente='" + textBox3.Text.Trim() + "'";
            SqlDataReader leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                textBox1.Text = leer["nombrec"].ToString();
                textBox14.Text = valoresg.CURP;
            }
            conecta.CierraConexion();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label27.Text = "0";
            this.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            BuscarPaciente bpaciente = new BuscarPaciente();
            bpaciente.Show();
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) BuscarPaciente();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox1.Text = "";

            dateTimePicker3.Enabled = false;
            textBox2.Text = "";

            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            textBox6.Text = "";
            textBox5.Text = "";
            textBox2.Text = "";

            Lv2.Items.Clear();
            Lv2.Columns.Clear();

            textBox7.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox9.Text = "";
            if (checkBox1.Checked == true) textBox1.Focus();
            else textBox6.Focus();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Pacientes regpaciente = new Pacientes();
            regpaciente.Show();
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) AgregarProducto();
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BuscarInfo();
                textBox11.Focus();
            }
        }

        private void textBox3_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Calculodetiempos();
                button1_Click(sender, e);
            }
        }

        public void BuscarInfo()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select cvproducto,nombre, unidad from productos where cvproducto='" + textBox7.Text.Trim() + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox10.Text = leer["nombre"].ToString();
                textBox7.Text = leer["cvproducto"].ToString();
            }
            conecta.CierraConexion();
            textBox11.Text = "1";
            textBox11.Focus();
        }

        public void Calculodetiempos()
        {
            int TiempoConsultaPred = tiempoCon;
            int TiempoConsulta = int.Parse(textBox3.Text);

            DateTime TiempoFinal = dateTimePicker3.Value.AddMinutes(TiempoConsulta);
            dateTimePicker2.Value = TiempoFinal;


        }

        private void button9_Click(object sender, EventArgs e)
        {
            BrapidaProducto bproducto = new BrapidaProducto();
            bproducto.Show();
        }


        public void GuardarRegistroServicio()
        {
            ConsecutivoPreserv();
            string cvpreser = NumPreServ.ToString(); ;
            string cvpaciente = textBox6.Text;
            string cantidad = textBox11.Text;
            string cvproducto = textBox7.Text;
            string status = "CAPTURADO";
            conectorSql conecta = new conectorSql();
            bool entroBand = false;
            string Query = "";

            string numprogresivo = label8.Text;
            string fechacita = label11.Text;
            string iddoctor = comboBox1.SelectedValue.ToString();

            for (int i = 0; i < Lv2.Items.Count; i++)
            {
                Query = "Select * from DetallesPreServicio where cvpaciente='" + cvpaciente + "' and cvproducto='" + Lv2.Items[i].SubItems[1].Text + "' and estatus='CAPTURADO'";
                bool existe = conecta.ExisteRegistro(Query);
                if (existe == false)
                {
                    DateTime Fechareg = dateTimePicker1.Value;  // pasar el preregistro el dia correcto
                    Query = "Insert into DetallesPreServicio (cvpreserv,cvpaciente,cantidad,cvproducto,estatus,emitio,fecha,fechacod,Fechacita,numticket,iddoctor) ";
                    Query = Query + " values (";
                    Query = Query + "'" + cvpreser + "','" + cvpaciente + "','" + Lv2.Items[i].SubItems[2].Text + "','" + Lv2.Items[i].SubItems[1].Text + "' ";
                    Query = Query + " ,'CAPTURADO','" + valoresg.USUARIOSIS + "','" + Fechareg.ToString("dd/MM/yyyy") + "','" + Fechareg.ToString("yyyyMMdd") + "' ";
                    Query = Query + " ,'" + fechacita + "','" + numprogresivo + "','" + iddoctor+ "')";
                    conecta.Excute(Query);
                    conecta.CierraConexion();
                    entroBand = true;
                }

            }


            if (entroBand == true) ActualizarPreserv();
        }

        private void Lv2_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Delete)
            {
                EliminarConcepto();
            }
        }


        public void EliminarConcepto()
        {
            try
            {
                conectorSql conecta = new conectorSql();
                if (Lv2.SelectedItems.Count > 0)
                {
                    ListView.SelectedIndexCollection seleccion = Lv2.SelectedIndices;
                    foreach (int item in seleccion)
                    {
                        Lv2.Items.RemoveAt(item);

                        string Query = "Delete from DetallesPreServicio where cvpreserv='" + Lv2.Items[item].Text + "' and cvpaciente='" + textBox6.Text.Trim() + "' and estatus='CAPTURADO'";
                        conecta.Excute(Query);
                        MessageBox.Show("Se elimino la informacion seleccionada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception)
            {

                //  throw;
            }
        }

        private void Lv2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RegistroCitas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10) RegistrarRecibosmanual();
            if (e.KeyCode == Keys.Escape) panel4.Visible=false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
            conectorSql conecta=new conectorSql();
            string consulta = "Select * from citas where recibopago='" + textBox4.Text.Trim() + "'";
            bool existeTi = conecta.ExisteRegistro(consulta);
            if (existeTi == true)
            {
                MessageBox.Show("El recibo ya se encuentra registrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Text = "";
                textBox4.Focus();
                return;
            }

            consulta = "Select * from pagos where numpedido='" + textBox4.Text.Trim() + "' and cantidad>'0'";
            existeTi = conecta.ExisteRegistro(consulta);
            if (existeTi == false)
            {
                MessageBox.Show("El numero de recibo no existe , no esta registrado en pagos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Text = "";
                textBox4.Focus();
                return;
            }


            string query="Update citas set recibopago='" + textBox4.Text.Trim() + "' , estatus='PAGADO'";
            if (textBox5.Text != "" && nombreservicio=="") query = query + ", servicio='" + textBox5.Text + "',  cvservicio='RMAN'";
            query=query + " where progresivo='" + label34.Text + "' and cvpaciente='"+ label29.Text+ "'";
            conecta.Excute(query);
            conecta.CierraConexion();

            query = "Update DetallesPreServicio set estatus='COBRADO', numrecibo='" + textBox4.Text.Trim()  + "' where fecha='" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "' and cvpaciente='" + label29.Text + "'";
            conecta.Excute(query);
            conecta.CierraConexion();

            button3_Click(sender, e);
            panel4.Visible = false;
        }


        private void button11_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void Lv_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {

        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button10_Click(sender, e);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (Gestatus != "PAGADO")
            {
                MessageBox.Show("Solo pueden ser reprogramada la cita pagada", "aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CambiarDiaCita reprogramar = new CambiarDiaCita();
            reprogramar.Show();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true) cargaConsecutivo();
        }


        public bool actualizaConsecutivo()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = int.Parse(CLAVE) + 1;
            string Query = "update consecutivos set paciente='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                timer1.Enabled = true;
                textBox6.Enabled = false;
                textBox1.Focus();
            }
            else
            {
                timer1.Enabled = false;
                textBox6.Enabled = true;
                button7_Click(sender, e);
            }
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox7.Focus();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Lv.Visible==true) ListadoDeCitas();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ReportDocument cryRpt = new ReportDocument();

            string CadenaReporte = @"\\SRV-DATACENTER\\tmp\\reports\\ReporteAreaDiario.rpt";

            DataSet ds = new DataSet();
            string fechacod = dateTimePicker1.Value.ToString("yyyyMMdd");
            string cvdoctor = comboBox1.SelectedValue.ToString();

            BaseReportCitasTableAdapters.LogoEmpresaTableAdapter tlogoempresa = new BaseReportCitasTableAdapters.LogoEmpresaTableAdapter();
            BaseReportCitas.LogoEmpresaDataTable logoempresa = new BaseReportCitas.LogoEmpresaDataTable();
            tlogoempresa.Fill(logoempresa);

            BaseReportCitasTableAdapters.ParametrosReciboTableAdapter tparametros = new BaseReportCitasTableAdapters.ParametrosReciboTableAdapter();
            BaseReportCitas.ParametrosReciboDataTable parametros = new BaseReportCitas.ParametrosReciboDataTable();
            tparametros.Fill(parametros);

            BaseReportCitasTableAdapters.DoctoresTableAdapter tdoctor = new BaseReportCitasTableAdapters.DoctoresTableAdapter();
            BaseReportCitas.DoctoresDataTable docttor = new BaseReportCitas.DoctoresDataTable();
            tdoctor.Fill(docttor, cvdoctor);

            BaseReportCitasTableAdapters.CitasTableAdapter tcitas = new BaseReportCitasTableAdapters.CitasTableAdapter();
            BaseReportCitas.CitasDataTable citasreg = new BaseReportCitas.CitasDataTable();
            tcitas.Fill(citasreg, fechacod,cvdoctor);


            ds.Clear();
            ds.Tables.Add(logoempresa);
            ds.Tables.Add(parametros);
            ds.Tables.Add(docttor);
            ds.Tables.Add(citasreg);

            cryRpt.Load(CadenaReporte);
            cryRpt.SetDataSource(ds);

            string NombreArchivo =  @"C:\\tmp\\reports\\ReporteDiarioCitas.pdf"; 
            // cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, NombreArchivo);
            cryRpt.PrintToPrinter(1, false, 0, 0);
            cryRpt.Close();
            cryRpt.Dispose();

            try
            {
                System.Diagnostics.Process.Start(NombreArchivo);
            }
            catch (Exception er)
            {
                MessageBox.Show("Verifique que no se encuentre abierto el Archivo PDF", "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue!=null && comboBox1.SelectedValue.ToString()!="")
            BuscarClaveTurno();
        }

       
        public void BuscarClaveTurno()
        {
            label40.Text = "";
            conectorSql conecta = new conectorSql();
            string Query = "Select abrevia from doctores where cvdoctor='" + comboBox1.SelectedValue.ToString() +"'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                label40.Text = leer["abrevia"].ToString();
            }
            conecta.CierraConexion();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                string cvdoctor = comboBox1.SelectedValue.ToString();
                if (cvdoctor == "1") cargaConsecutivoExpedienteGine();
                if (cvdoctor == "2") cargaConsecutivoExpedienteDental();
                if (cvdoctor == "7") cargaConsecutivoExpedienteOftamologo();
            }
        }

        public bool actualizaConsecutivoExpedienteGine()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = int.Parse(textBox12.Text) + 1;
            string Query = "update consecutivos set numexpegine='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }

        public bool actualizaConsecutivoExpedienteDental()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = int.Parse(textBox12.Text) + 1;
            string Query = "update consecutivos set numexpedental='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }

        public bool actualizaConsecutivoExpedienteOftamo()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = int.Parse(textBox12.Text) + 1;
            string Query = "update consecutivos set numexpeofta='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                textBox12.Enabled = false;
                string cvdoctor = comboBox1.SelectedValue.ToString();
                if (cvdoctor == "1") cargaConsecutivoExpedienteGine();
                if (cvdoctor == "2") cargaConsecutivoExpedienteDental();
                if (cvdoctor == "7") cargaConsecutivoExpedienteOftamologo();
            }
            else textBox12.Enabled = true;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true && comboBox1.SelectedValue.ToString() == "1")
            {
                MessageBox.Show("No se puede incluir en la misma area", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkBox5.Checked = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true && comboBox1.SelectedValue.ToString() == "2")
            {
                MessageBox.Show("No se puede incluir en la misma area dental y dental", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkBox4.Checked = false;
            }

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true && comboBox1.SelectedValue.ToString() == "3")
            {
                MessageBox.Show("No se puede incluir en la misma area", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkBox6.Checked = false;
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true && comboBox1.SelectedValue.ToString() == "7")
            {
                MessageBox.Show("No se puede incluir en la misma area", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkBox3.Checked = false;
            }

        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ListadoDeCitas();
        }

     

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ClienteBloqueado.ClienteBloqueadoC pacientebloque = new ClienteBloqueado.ClienteBloqueadoC();
            pacientebloque.Show();
        }

        private void historialDelPacienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // AGREGADO POR JOSE 16-12-2019
            if (NOMBREPACIENTE!= "")
            {
                HistorialClinica.Pacientes historiapaciente = new HistorialClinica.Pacientes();
                string CADENA = "";
                CADENA = historiapaciente.INFO_PACIENTE(CLAVEPACIENTE, NOMBREPACIENTE);
                historiapaciente.Show();
            }

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
