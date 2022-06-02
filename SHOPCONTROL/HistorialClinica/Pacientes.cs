using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using SHOPCONTROL.Utilerias;

namespace SHOPCONTROL.HistorialClinica
{
    public partial class Pacientes : Form
    {
        public Pacientes()
        {
            InitializeComponent();
        }

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

        string LUGARNAC="";
        string FECHANAC="";
        string STATUS = "1";
        string pathFoto = "";

        string CELULAR= "1";
        string CLAVE = "1";
        string EMAIL2= "1";

        string EXPGINECO = "";
        string EXPDENTAL= "";
        string EXPOFTAM= "";


        public void Recolecta()
        {
            NOMBRE = textBox1.Text.Trim();
            APATERNO = textBox2.Text.Trim();
            AMATERNO = textBox3.Text.Trim();
            GENERO = comboBox5.Text;
            ESCOLARIDAD = comboBox2.Text;
            EMAIL = textBox4.Text.Trim();
            EDAD = "0";
            ECivil = comboBox1.Text;
            NoHijos = textBox6.Text.Trim();
            OCUPACION = textBox7.Text.Trim();
            TELEFONO = textBox8.Text.Trim();
            CALLE = textBox9.Text.Trim();
            NoCalle = "ND";
            CP = "0";
            COLONIA = "ND";
            MUNICIPIO = textBox13.Text.Trim();
            CIUDAD = "ND";
            ESTADO = "ND";

            Pregunta1 = "ND";
            Pregunta2 = comboBox3.Text.Trim();
            Pregunta3 = "ND";
            RecibeAvisos = "ND";

            NoExpediente = "ND";
            SERVICIO = "ND";
            MEDICO = "ND";
            TURNO = "ND";
            OBSERVACIONES = "ND";
            FECHA = dateTimePicker1.Value.ToShortDateString();
            FCOD = dateTimePicker1.Value.ToString("yyyyMMdd");

            LUGARNAC = textBox23.Text.Trim();
            FECHANAC = DOB.Value.ToShortDateString();

            CLAVE = textBox24.Text;
            CELULAR = textBox26.Text;
            EMAIL2 = "ND";

            EXPGINECO = textBox19.Text;
            EXPDENTAL = textBox12.Text;
            EXPOFTAM = textBox14.Text;

        }

        public bool Validacion()
        {
            if (NOMBRE.Equals(""))
            {
                MessageBox.Show("Ingrese el nombre(s) ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return false;
            }
            if (APATERNO.Equals(""))
            {
                MessageBox.Show("Ingrese el apellido paterno ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return false;
            }
            if (AMATERNO.Equals(""))
            {
                MessageBox.Show("Ingrese el apellido materno ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return false;
            }
            if (GENERO.Equals("")) GENERO = "MASCULINO";

            if (ESCOLARIDAD.Equals("")) ESCOLARIDAD = "NINGUNA";
            if (EMAIL == "") EMAIL = "ND";
            /*
            if (EDAD.Equals(""))
            {
                MessageBox.Show("Ingrese la edad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return false;
            }
            */
            if (ECivil == "") ECivil = "NO REGISTRADO";

            if (OCUPACION.Equals("")) OCUPACION = "NINGUNA";

            if (TELEFONO.Equals("")) TELEFONO = "S/N";
            if (CALLE.Equals("")) CALLE = "ND";
            if (COLONIA.Equals("")) COLONIA = "ND";
            if (MUNICIPIO.Equals("")) MUNICIPIO = "ND";
            if (CIUDAD.Equals("")) CIUDAD = "ND";
            if (ESTADO.Equals("")) ESTADO = "ND";
            if (NoExpediente.Equals("")) NoExpediente = "0";

        
            if (MEDICO.Equals("")) MEDICO = "MEDICO EN TURNO";
            if (LUGARNAC.Equals("")) LUGARNAC = "NO APLICA";
            return true;
        }

        public bool Guarda()
        {
            try
            {
                conectorSql conecta = new conectorSql();
                string Query = "";
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
                Query += ",'" + GetDifferenceInYears(DOB.Value, DateTime.Now) + "'";
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
                Query += ",'" + DOB.Value.ToShortDateString() + "'";

                Query += ",'" + CLAVE + "'";
                Query += ",'" + EMAIL2 + "'";
                Query += ",'" + CELULAR + "'";

                Query += ",'" + EXPDENTAL + "'";
                Query += ",'" + EXPGINECO + "'";
                Query += ",'" + EXPOFTAM + "'";
                Query += ",'" + curp.Text + "'";
                Query += ",'" + STATUS + "')";

                conecta.Excute(Query);

                ClaseFotos.GuardarFoto(pathFoto, CLAVE);

                RegistroCitas rcitas = new RegistroCitas();
                rcitas.Show();
                return true;
            }
            catch (Exception e)
            {
                // MessageBox.Show("Este cliente ya está dado de alta con los datos proporcionados o un error de sistema impide esta creación: " + e.Message);
                Console.WriteLine("IOException source: {0}", e.Source);
                return false;
            }
            
        }


        public int GetDifferenceInYears(DateTime startDate, DateTime endDate)
        {
            //Excel documentation says "COMPLETE calendar years in between dates"
            int years = endDate.Year - startDate.Year;

            if (startDate.Month == endDate.Month &&// if the start month and the end month are the same
                endDate.Day < startDate.Day// AND the end day is less than the start day
                || endDate.Month < startDate.Month)// OR if the end month is less than the start month
            {
                years--;
            }

            return years;
        }


        public bool ExistePaciente()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from pacientes where clave='" + textBox24.Text.Trim() + "'";
            bool existe = conecta.ExisteRegistro(Query);
            return existe;
        }

        public void BuscarInformacion(string clave, string claveind)
        {
            Limpiar(this);
            conectorSql conecta = new conectorSql();
            string Query = "SELECT  * FROM  Pacientes WHERE  clave<>'' ";
            if (claveind != "") Query = Query + " and clave='" + claveind + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            textBox19.Text = clave;
            while (leer.Read())
            {
                timer1.Enabled = false;
                textBox1.Text = leer["NOMBRE"].ToString();
                textBox2.Text = leer["APATERNO"].ToString();
                textBox3.Text = leer["AMATERNO"].ToString();
                comboBox5.Text = leer["GENERO"].ToString();
                comboBox2.Text = leer["ESCOLARIDAD"].ToString();
                textBox4.Text = leer["EMAIL"].ToString();
                // textBox5.Text = leer["EDAD"].ToString();
                comboBox1.Text = leer["ECivil"].ToString();
                textBox6.Text = leer["NoHijos"].ToString();
                //textBox7.Text = leer["OCUPACION"].ToString();
                textBox8.Text = leer["TELEFONO"].ToString();
                curp.Text = leer["CURP"].ToString();
                comboBox3.Text= leer["Pregunta2"].ToString();
                textBox23.Text = leer["LUGARNAC"].ToString();

                dateTimePicker1.Value = new DateTime(DateTime.Parse(leer["FECHA"].ToString()).Year,
                                            DateTime.Parse(leer["FECHA"].ToString()).Month,
                                            DateTime.Parse(leer["FECHA"].ToString()).Day);

                DOB.Value = new DateTime(DateTime.Parse(leer["FECHANAC"].ToString()).Year,
                                            DateTime.Parse(leer["FECHANAC"].ToString()).Month,
                                            DateTime.Parse(leer["FECHANAC"].ToString()).Day);
                textBox24.Text = leer["CLAVE"].ToString();
                textBox26.Text = leer["CELULAR"].ToString();


                textBox9.Text = leer["CALLE"].ToString();
                textBox13.Text = leer["MUNICIPIO"].ToString();

                string expediente = leer["NoExpediente"].ToString();

                textBox19.Text = leer["expgineco"].ToString();
                textBox12.Text = leer["expdental"].ToString();
                textBox14.Text = leer["expoftamolgo"].ToString();


            }
            conecta.CierraConexion();

            pbFoto.Image = ClaseFotos.ConsultarFotoPaciente(claveind);

            BuscarServicios();
        }

        public void BuscarServicios()
        {
            //if (textBox24.Text.Trim()=="") return;
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;
            Lv2.Items.Clear();
            if (Lv2.Columns.Count == 0) ColumnasProducto();
            string Query = "SELECT DetallesPreServicio.cvpreserv, productos.nombre, DetallesPreServicio.cantidad,DetallesPreServicio.cvproducto, DetallesPreServicio.fecha  FROM  DetallesPreServicio  ";
            Query = Query + " inner join productos on productos.cvproducto=DetallesPreServicio.cvproducto WHERE  DetallesPreServicio.cvpaciente<>''  ";
            Query = Query + " and  DetallesPreServicio.estatus='CAPTURADO'";
            if (textBox24.Text.Trim() != "") Query = Query + " and DetallesPreServicio.cvpaciente='" + textBox24.Text.Trim() + "'";
            if (textBox17.Text.Trim() != "") Query = Query + "and DetallesPreServicio.cvpaciente='" + textBox17.Text.Trim() + "'";
            Query = Query + " order by cvpreserv desc  ";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["cvpreserv"].ToString());
                lvi.SubItems.Add(leer["cvproducto"].ToString());
                lvi.SubItems.Add(leer["cantidad"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                Lv2.Items.Add(lvi);
            }
            conecta.CierraConexion();
        }

        public void BuscarRecibos()
        {
            if (textBox24.Text.Trim() == "") return;
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;
            listView2.Items.Clear();
            listView2.Columns.Clear();
            listView2.Columns.Add("Num. Recibo", 90);
            listView2.Columns.Add("Fecha", 90);
            listView2.Columns.Add("Total General", 70);
            listView2.Columns.Add("Emitio", 90);
            
            string Query = "SELECT * from recibos   ";
            Query = Query + " where cvcliente='" + textBox24.Text.Trim() + "'";
            Query = Query + " order by fechacod desc";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["numrecibo"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                lvi.SubItems.Add(leer["totalgeneral"].ToString());
                lvi.SubItems.Add(leer["emitio"].ToString());
                listView2.Items.Add(lvi);
            }
            conecta.CierraConexion();
        }


        public void BuscarCitas()
        {
            if (textBox24.Text.Trim() == "") return;
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("Num. Turno", 80);
            listView1.Columns.Add("Fecha", 100);
            listView1.Columns.Add("Hora", 90);
            listView1.Columns.Add("Nombre Servicio", 200);
            listView1.Columns.Add("Estatus", 100);
            listView1.Columns.Add("Num. de Recibo de Pago", 150);

            string Query = "SELECT top(30) * from citas   ";
            Query = Query + " where cvpaciente='" + textBox24.Text.Trim() + "'";
            Query = Query + " order by fechacod desc";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["progresivo"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                lvi.SubItems.Add(leer["horainicia"].ToString());
                lvi.SubItems.Add(leer["nombreservicio"].ToString());
                lvi.SubItems.Add(leer["estatus"].ToString());
                lvi.SubItems.Add(leer["recibopago"].ToString());
                listView1.Items.Add(lvi);
            }
            conecta.CierraConexion();
        }

        public void BuscarDetalleRecibo()
        {
            if (textBox24.Text.Trim() == "") return;
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;
            listView3.Items.Clear();
            listView3.Columns.Clear();
            listView3.Columns.Add("Cantidad", 45);
            listView3.Columns.Add("Unidad", 0);
            listView3.Columns.Add("Descripcion", 200);
            listView3.Columns.Add("Precio Unitario", 60);
            listView3.Columns.Add("Total", 60);

            string Query = "SELECT * from detallesrecibos   ";
            Query = Query + " where  numrecibo='" +  label45.Text + "' and fecha='" +  label46.Text + "'";
            Query = Query + " order by descripcion asc";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["cantidad"].ToString());
                lvi.SubItems.Add(leer["unidad"].ToString());
                lvi.SubItems.Add(leer["descripcion"].ToString());
                lvi.SubItems.Add(leer["preunitario"].ToString());
                lvi.SubItems.Add(leer["precio"].ToString());
                listView3.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label47.Text = listView3.Items.Count.ToString();
        }

        public void Actualizar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "update Pacientes set";
            Query += " NOMBRE='" + NOMBRE + "'";
            Query += ",APATERNO='" + APATERNO + "'";
            Query += ",AMATERNO='" + AMATERNO + "'";
            Query += ",GENERO='" + GENERO + "'";
            Query += ",ESCOLARIDAD='" + ESCOLARIDAD + "'";
            Query += ",EMAIL='" + EMAIL + "'";
            Query += ",EDAD='" + EDAD + "'";
            Query += ",ECivil='" + ECivil + "'";
            Query += ",NoHijos='" + NoHijos + "'";
            Query += ",OCUPACION='" + OCUPACION + "'";
            Query += ",TELEFONO='" + TELEFONO + "'";
            Query += ",CALLE='" + CALLE + "'";
            Query += ",NoCalle='" + NoCalle + "'";
            Query += ",CP='" + CP + "'";
            Query += ",COLONIA='" + COLONIA + "'";
            Query += ",MUNICIPIO='" + MUNICIPIO + "'";
            Query += ",CIUDAD='" + CIUDAD + "'";
            Query += ",ESTADO='" + ESTADO + "'";
            Query += ",Pregunta1='" + Pregunta1 + "'";
            Query += ",Pregunta2='" + Pregunta2 + "'";
            Query += ",Pregunta3='" + Pregunta3 + "'";
            Query += ",RecibeAvisos='" + RecibeAvisos + "'";
            Query += ",SERVICIO='" + SERVICIO + "'";
            Query += ",MEDICO='" + MEDICO + "'";
            Query += ",TURNO='" + TURNO + "'";
            Query += ",OBSERVACIONES='" + OBSERVACIONES + "'";
            Query += ",FECHA='" + FECHA + "'";
            Query += ",FCOD='" + FCOD + "'";
            Query += ",LUGARNAC='" + LUGARNAC + "'";
            Query += ",FECHANAC='" + FECHANAC + "'";
            Query += ",STATUS='" + STATUS + "'";
            Query += ",EMAIL2='" + EMAIL2+ "'";
            Query += ",CELULAR='" + CELULAR+ "'";
            Query += ",NoExpediente='" + NoExpediente + "'";

            Query += ",expdental='" + EXPDENTAL + "'";
            Query += ",expgineco='" + EXPGINECO+ "'";
            Query += ",expoftamolgo='" + EXPOFTAM+ "'";


            Query = Query + " where clave='" + CLAVE+ "'";
            conecta.Excute(Query);

            ClaseFotos.GuardarFotoPaciente(pathFoto, NoExpediente);
        }

        public bool ExisteExpedientePac()
        {

            conectorSql conecta = new conectorSql();
            string query="Select * from pacientes where NoExpediente='" + textBox19.Text.Trim() + "'";
            bool existe = conecta.ExisteRegistro(query);
            return existe;

        }

        public bool ExisteInfo(string Usrmail )
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from Pacientes where email='" + Usrmail + "'";

            bool result = conecta.ExisteRegistro(Query);

            if (result)
            {
                MessageBox.Show("Este cliente ya está dado de alta con el correo indicado" );
            }
            else
            {
                result = true;
            }



            return result ;
        }

        public void Limpiar(Control cnt)
        {
            foreach (Control c in cnt.Controls)
            {


                if (c is TabControl)
                {
                    Limpiar(c);
                }

                if (c is Panel)
                {
                    Limpiar(c);
                }
                if (c is TextBox)
                {
                    c.Text = "";
                }
                if (c is ComboBox)
                {
                    c.Text = "";
                }
                if (c is DateTimePicker)
                {
                    c.ResetText();
                }
            }
            pathFoto = "";
            pbFoto.Image = null;
            dateTimePicker2.Value = DateTime.Now;
           
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
            textBox24.Text= Numero.ToString();
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
            textBox19.Text = NUMEXPEDIENTEG.ToString();
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
            textBox14.Text = NUMEXPEDIENTEG.ToString();
        }

        public int NUM_ARCHIVOS = 0;
        public void cargaConsecutivoArchivos()
        {
            NUM_ARCHIVOS = 1;
            conectorSql conecta = new conectorSql();
            string Query = "Select numarchivos from consecutivos where numarchivos<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                NUM_ARCHIVOS = int.Parse(leer["numarchivos"].ToString());
            }
            conecta.CierraConexion();
            label28.Text = NUM_ARCHIVOS.ToString();
        }

        public bool actualizaConsecutivoExpediente()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = NUMEXPEDIENTEG + 1;
            string Query = "update consecutivos set numexpe='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }

        public bool actualizaConsecutivoArchivos()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = NUM_ARCHIVOS + 1;
            string Query = "update consecutivos set numarchivos='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }

        public bool actualizaConsecutivoExpedienteGine()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = int.Parse(textBox19.Text) + 1;
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
            int Siguiente = int.Parse(textBox14.Text) + 1;
            string Query = "update consecutivos set numexpeofta='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }

        public bool actualizaConsecutivo()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = int.Parse(textBox24.Text) + 1;
            string Query = "update consecutivos set paciente='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }

        public bool GuardarCliente()
        {
            string CURPUSR = curp.Text == "" ? "'XAXX010101000'" : curp.Text;
            string usercreated = valoresg.IdEmployee;
            DateTime ferchacreacion = DateTime.Now;

            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "insert into clientes(";
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
            Query = Query + ",curp";
            Query = Query + ",monedero";
            Query = Query + ",idcliente, usercreated, datecreated)";
            Query = Query + " values(";

            Query = Query + "'" + CLAVE+ "'";
            Query = Query + ",'" + NOMBRE + " " + APATERNO + " " + AMATERNO + "'";
            Query = Query + ",'" + TELEFONO + "'";
            Query = Query + ",'" + EMAIL+ "'";
            Query = Query + ",'" + EMAIL2+ "'";
            Query = Query + ",'" + CELULAR + "'";
            
            Query = Query + ",'" + CALLE + " ," + NoCalle + " " + MUNICIPIO + " " + ESTADO +"'";

            Query = Query + ",'" + CURPUSR + "'";

            Query = Query + ",'" + CALLE + " ," + NoCalle + " " + MUNICIPIO + " "  +ESTADO  + "'";

            Query = Query + ",'" + NOMBRE + " " + APATERNO + " " + AMATERNO  +"'";

            Query = Query + ",'" + CALLE + "'";
            Query = Query + ",'" + COLONIA + "'";
            Query = Query + ",'" + MUNICIPIO+ "'";
            Query = Query + ",'" + ESTADO+ "'";
            Query = Query + ",'" + CP+ "'";
            Query = Query + ",'MÈXICO'";
            Query = Query + ",'" + CALLE+ "'";
            Query = Query + ",'" + COLONIA+ "'";
            Query = Query + ",'" + MUNICIPIO+ "'";
            Query = Query + ",'" + ESTADO+ "'";
            Query = Query + ",'" + CP+ "'";
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
            Query = Query + ",'" + curp.Text + "'";
            Query = Query + ",'0'," + CLAVE + "," + usercreated + ",'" + ferchacreacion + "')";

            try
            {
                conecta.Excute(Query);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("IOException source: {0}", e.Source);
                return false;
            }

               
        }

        public void ActualizarCliente()
        {

            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "update clientes set ";
            Query = Query + " nombre='" + NOMBRE + " " + APATERNO + " " + AMATERNO + "'";
            Query = Query + ", telefono='" + TELEFONO + "'";
            Query = Query + ", email='" + EMAIL + "'";
            Query = Query + ", email2='" + EMAIL2 + "'";
            Query = Query + ",celular='" + CELULAR + "'";
            Query = Query + ", direccion='" + CALLE + " " + NoCalle + " " + MUNICIPIO + " " + ESTADO + "'";
            Query = Query + ",RFC='XAXX010101000'";
            Query = Query + ",direfiscal='" + CALLE + " " + NoCalle + " " + MUNICIPIO + " " + ESTADO + "'";
            Query = Query + ",empresa='" + NOMBRE + " " + APATERNO + " " + AMATERNO + "'";

            Query = Query + ",calleE='" + CALLE + "'";
            Query = Query + ",ColoniaE='" + COLONIA + "'";
            Query = Query + ",municipioE='" + MUNICIPIO + "'";
            Query = Query + ",EstadoE='" + ESTADO + "'";
            Query = Query + ",CodE='" + CP + "'";

            Query = Query + ",CalleF='" + CALLE + "'";
            Query = Query + ", ColoniaF='" + COLONIA + "'";
            Query = Query + ",MunicipioF='" + MUNICIPIO + "'";
            Query = Query + ",EstadoF='" + ESTADO + "'";
            Query = Query + ",CodF='" + CP + "'";
            Query = Query + ",fechamod='" + FECHA + "'";
            Query = Query + ",fcodmod='" + FCOD + "'";
            Query = Query + ",numf='" + NoCalle + "'";
            Query = Query + " where cvcliente ='" + CLAVE + "'";
            conecta.Excute(Query);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateData.IsValidEmail(textBox4.Text) == false)
            {
                MessageBox.Show("Correo electrónico nó valido, favor de verificar", "Error en formato de correo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Recolecta();
            if (Validacion())
            {
                if (ExisteInfo(EMAIL))
                {
                    bool guardaCliente = GuardarCliente();
                    bool guardaPaciente = Guarda();
                    if (guardaCliente==true && guardaPaciente==true)
                    {

                        MailNotifications mails = new MailNotifications();
                        mails.SendMail(EMAIL, "Se ha creado su cuenta en la sucursal " + valoresg.UBICACION + "<br>Número Id único: " + CLAVE + "<br>Nombre: " + NOMBRE + " " + APATERNO + " " + AMATERNO + "' <br>Recuerde conservar su número de cliente para futuras promociones.<br>Gracias por su preferencia", true);
                        // GuardarRegistroServicio();
                        actualizaConsecutivo();
                    if (textBox19.Text != "") actualizaConsecutivoExpedienteGine();
                    if (textBox12.Text != "") actualizaConsecutivoExpedienteDental();
                    if (textBox14.Text != "") actualizaConsecutivoExpedienteOftamo();

                    MessageBox.Show("Se guardo correctamente la información", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    valoresg.CLAVEPAC = CLAVE;
                    valoresg.CURP = curp.Text;

                    } else
                    {
                        MessageBox.Show("El usuario ya existe, se procede a programar la cita", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        valoresg.CLAVEPAC = CLAVE;
                        valoresg.CURP = curp.Text;
                    }
                    this.Dispose();
                    

                }
                else
                {
                    
                    ActualizarCliente();
                    GuardarRegistroServicio();
                    Actualizar();
                    MessageBox.Show("Se actualizo correctamente la información", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DialogResult reply = MessageBox.Show("¿Desea programar una cita ?", "Registrar cita", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (reply == DialogResult.No)
                        return;

                    valoresg.CLAVEPAC = CLAVE;
                    RegistroCitas rcitas = new RegistroCitas();
                    rcitas.Show();

                }
                Limpiar(this);
            }
        }


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
            NumPreServ ++;
            conectorSql conecta = new conectorSql();
            string Query = "Update consecutivos set preserv='" +  NumPreServ.ToString() +"'";
            conecta.Excute(Query);
            conecta.CierraConexion();
        }
            
       public void GuardarRegistroServicio()
        {
            ConsecutivoPreserv();
            string cvpreser = NumPreServ.ToString(); ;
            string cvpaciente=textBox24.Text;
            string cantidad=textBox11.Text;
            string cvproducto=textBox7.Text;
            string status="CAPTURADO";
            conectorSql conecta = new conectorSql();
            bool entroBand = false;
            string Query = "";
            //string Query = "Delete from DetallesPreServicio  where cvpaciente='" + cvpaciente + "' and fechacod='" + DateTime.Now.ToString("yyyyMMdd") + "' and estatus='CAPTURADO'";
            //conecta.Excute(Query);
            //conecta.CierraConexion();

            for (int i = 0; i < Lv2.Items.Count; i++)
            {
                Query="Select * from DetallesPreServicio where cvpaciente='" + cvpaciente + "' and cvproducto='" +Lv2.Items[i].SubItems[1].Text  + "' and estatus='CAPTURADO'";
                bool existe = conecta.ExisteRegistro(Query);
                if (existe == false)
                {
                    DateTime Fechareg = DateTime.Parse(Lv2.Items[i].SubItems[4].Text);
                    Query = "Insert into DetallesPreServicio (cvpreserv,cvpaciente,cantidad,cvproducto,estatus,emitio,fecha,fechacod) values(";
                    Query = Query + "'" + cvpreser + "','" + cvpaciente + "','" + Lv2.Items[i].SubItems[2].Text + "','" + Lv2.Items[i].SubItems[1].Text + "','" + status + "','" + valoresg.IdEmployee + "','" + Fechareg.ToString("dd/MM/yyyy") + "','" + Fechareg.ToString("yyyyMMdd") + "')";
                    conecta.Excute(Query);
                    conecta.CierraConexion();
                    entroBand = true;
                }

            }


           if (entroBand==true) ActualizarPreserv();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
                HistorialClinica.BuscarPaciente bPaciente = new HistorialClinica.BuscarPaciente();
                bPaciente.Show();
           
        }

        private void soloNumeros(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

       

        private void Pacientes_Load(object sender, EventArgs e)
        {
            combos.ComboDoctores(comboBox4);
            dateTimePicker2.Value = DateTime.Now;
            // textBox5.KeyPress += new KeyPressEventHandler(soloNumeros);
            button4_Click(sender, e);
            textBox1.Focus();
            textBox17.Visible = false;
        }

        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char) Keys.Enter)
            {
                button3_Click(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            label36.Visible = true;
            Lv2.Items.Clear();
            Lv2.Columns.Clear();
            textBox7.Text = "";
            textBox11.Text = "";
            textBox10.Text = "";
            comboBox1.Text = "NO REGISTRADO";
            comboBox5.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            label37.Visible = false;
            Limpiar(this);
            cargaConsecutivo();
            textBox1.Focus();
        }
        private void Pacientes_Activated(object sender, EventArgs e)
        {
            if (valoresg.CLAVEPAC != "")
            {
                label36.Visible = false;
                label37.Visible = true;
                timer1.Enabled = false;
                textBox24.Text = valoresg.CLAVEPAC;
                BuscarInformacion("", textBox24.Text.Trim());
                valoresg.CLAVEPAC = "";
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
                label18.Text= leer["unidad"].ToString();
            }
            conecta.CierraConexion();
            textBox11.Text = "1";
            textBox11.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox17.Text == "" && textBox1.Text == "")
            {
                MessageBox.Show("Ingresa el número de paciente primero");
                textBox17.Focus();
            }
            else
            {
                using (OpenFileDialog opfDialog = new OpenFileDialog())
                {
                    try
                    {
                        pathFoto = ClaseFotos.AbrirExplorar(opfDialog);
                        pbFoto.Image = System.Drawing.Image.FromFile(pathFoto);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No ha seleccionado ninguna foto ","Aviso",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
            }
        }
        public void guardarFoto() {
            //ClaseFotos
            
        }

        public string INFO_PACIENTE(string cvnom, string nom) // AGREGADO POR JOSE 16-12-2019
        {
            textBox24.Visible = false;
            textBox17.Visible = true;
            label48.Text = cvnom;
            textBox17.Text = label48.Text;
            //textBox1.Text = nom;
            Limpiar(this);
            conectorSql conecta = new conectorSql();
            string Query = "SELECT  * FROM  Pacientes WHERE  clave<>'' ";
            if (cvnom != "") Query = Query + " and clave='" + cvnom + "'";
            //if (nom != "") Query = Query + " and NOMBRE ='" + nom + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                timer1.Enabled = false;
                textBox1.Text = leer["NOMBRE"].ToString();
                textBox2.Text = leer["APATERNO"].ToString();
                textBox3.Text = leer["AMATERNO"].ToString();
                comboBox5.Text = leer["GENERO"].ToString();
                comboBox2.Text = leer["ESCOLARIDAD"].ToString();
                textBox4.Text = leer["EMAIL"].ToString();
                // textBox5.Text = leer["EDAD"].ToString();
                comboBox1.Text = leer["ECivil"].ToString();
                textBox6.Text = leer["NoHijos"].ToString();
                //textBox7.Text = leer["OCUPACION"].ToString();
                textBox8.Text = leer["TELEFONO"].ToString();
                curp.Text = leer["curp"].ToString();
                DOB.Text = leer["FECHANAC"].ToString();
                comboBox3.Text = leer["Pregunta2"].ToString();
                textBox23.Text = leer["LUGARNAC"].ToString();
                dateTimePicker1.Text = leer["FECHA"].ToString();
                textBox24.Text = leer["CLAVE"].ToString();
                textBox26.Text = leer["CELULAR"].ToString();


                textBox9.Text = leer["CALLE"].ToString();
                textBox13.Text = leer["MUNICIPIO"].ToString();

                string expediente = leer["NoExpediente"].ToString();

                textBox19.Text = leer["expgineco"].ToString();
                textBox12.Text = leer["expdental"].ToString();
                textBox14.Text = leer["expoftamolgo"].ToString();


            }
            conecta.CierraConexion();
            BuscarServicios();
            return cvnom;
        }
        private void textBox24_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                label36.Visible = false;
                label37.Visible = true;
                timer1.Enabled = false;
                if (ExistePaciente() == false)
                {
                    MessageBox.Show("No existe la clave del paciente, verifique", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                BuscarInformacion(textBox19.Text.Trim(), textBox24.Text.Trim());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label36.Visible == true) cargaConsecutivo();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            HistorialClinica.CamaraWeb camara = new HistorialClinica.CamaraWeb();
            camara.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BrapidaProducto bproducto = new BrapidaProducto();
            bproducto.Show();
        }



        public void AgregarProducto()
        {
         

            if (textBox7.Text == "")
            {
                MessageBox.Show("Ingrese la clave de un producto o servicio, verifique", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox7.Focus();
                return;
            }
            ColumnasProducto();
            if (textBox11.Text == "") return;

            ConsecutivoPreserv();
            if (Lv2.Items.Count > 0 )
            {
                bool existeServ=false;
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
            bool existeprod=false;
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

            label9.Text = Lv2.Items.Count.ToString();
            textBox7.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            label18.Text = "";
            dateTimePicker2.Value = DateTime.Now;
            textBox7.Focus();

            }

        private void button8_Click(object sender, EventArgs e)
        {
            AgregarProducto();
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

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button8_Click(sender, e);
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox11.Focus();
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BuscarInfo();
                textBox11.Focus();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string Query = "";
            for (int i = 0; i < Lv2.Items.Count; i++)
            {
                if (Lv2.Items[i].Checked == true)
                {
                    Query = "Delete from DetallesPreServicio where cvpreserv='" + Lv2.Items[i].Text + "' and cvpaciente='" + textBox24.Text.Trim() +"' and estatus='CAPTURADO'";
                    conecta.Excute(Query);
                }
            }
            MessageBox.Show("Se elimino la informacion seleccionada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BuscarServicios();
            
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

                        string Query = "Delete from DetallesPreServicio where cvpreserv='" + Lv2.Items[item].Text + "' and cvpaciente='" + textBox24.Text.Trim() + "' and estatus='CAPTURADO'";
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cargaConsecutivoExpedienteGine();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cargaConsecutivoExpedienteDental();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cargaConsecutivoExpedienteOftamologo();
        }

        private void Pacientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) button1_Click(sender, e);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            valoresg.CLAVEPAC = textBox24.Text;
            CamaraWeb camara = new CamaraWeb();
            camara.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog opfDialog = new OpenFileDialog())
            {
                try
                {
                    pathFoto = ClaseArchivos.AbrirExplorar(opfDialog);
                    textBox16.Text = pathFoto;
                    textBox15.Text = ClaseArchivos.Nombre_Archivo;
                    label29.Text = ClaseArchivos.Ext_Archivo;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No ha seleccionado ningun archivo ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
           string cadena=  ClaseArchivos.EscribirArchivoBytes(label34.Text, textBox24.Text,label33.Text);

            try
            {
                System.Diagnostics.Process.Start(cadena);
            }
            catch (Exception er)
            {
                MessageBox.Show("Verifique que no se encuentre abierto el Archivo\n" + er.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

            listView4.Items.Clear();
            listView4.Columns.Clear();
            listView4.Columns.Add("ID Archivo", 50);
            listView4.Columns.Add("Nombre", 250);
            listView4.Columns.Add("Fecha", 90);
            listView4.Columns.Add("emite", 90);
            listView4.Columns.Add("Estatus", 150);
            listView4.Columns.Add("extension", 0);
            listView4.Columns.Add("cvdoctor", 0);
            conectorSql conecta = new conectorSql();
            string query = "Select top(20) * from archivos where cvpaciente='" + textBox24.Text.Trim() + "' order by clave desc";
            SqlDataReader leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["clave"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                lvi.SubItems.Add(leer["emite"].ToString());
                lvi.SubItems.Add(leer["estatus"].ToString());
                lvi.SubItems.Add(leer["extension"].ToString());
                lvi.SubItems.Add(leer["cvdoctor"].ToString());
                listView4.Items.Add(lvi);
            }
            conecta.CierraConexion();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            cargaConsecutivoArchivos();
        }

        private void button14_Click(object sender, EventArgs e)
        {

            if (textBox16.Text == "")
            {
                MessageBox.Show("Seleccione un archivo desde el boton explorar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox15.Text== "")
            {
                MessageBox.Show("Seleccione nombre de archivo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            if (comboBox4.Text == "")
            {
                MessageBox.Show("Seleccione area de tratamiento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ClaseArchivos.GuardarArchivoBytes(pathFoto, label28.Text, textBox24.Text);
            ActualizarArchivos(label28.Text, textBox24.Text);
            actualizaConsecutivoArchivos();
            MessageBox.Show("Se guardo correctamente el archivo del paciente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button12_Click(sender, e);
           
        }
        public void ActualizarArchivos(string clave, string cvpaciente)
        {
            conectorSql conecta = new conectorSql();
            string query = "Update archivos set nombre='" + textBox15.Text.Trim() + "', fecha='" + dateTimePicker4.Value.ToString("dd/MM/yyyy") + "'";
            query = query + " , fechacod='" + dateTimePicker4.Value.ToString("yyyy/MM/dd") + "'";
            query = query + " , emite='" + valoresg.IdEmployee + "'";
            query = query + " , estatus='REGISTRADO'";
            query = query + " , extension='" + label29.Text.Trim()+ "'";
            query = query + " , HORA='" + DateTime.Now.ToString("HH:mm:00") + "'";
            query = query + " , cvdoctor='" + label44.Text.Trim() + "'";
            query = query + "  where clave='" + clave+ "' and cvpaciente='" + cvpaciente+ "'";
            conecta.Excute(query);
            conecta.CierraConexion();
        }

        private void listView4_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView4.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = listView4.SelectedIndices;
                foreach (int item in seleccion)
                {
                    DetallesModificaLV4(item);
                }
            }
        }

        public void DetallesModificaLV4(int index)
        {
            string clave = listView4.Items[index].Text;
            string nombre = listView4.Items[index].SubItems[1].Text;
            string extension= listView4.Items[index].SubItems[5].Text;
            string cvdoctor = listView4.Items[index].SubItems[6].Text;

            label34.Text = clave;
            label30.Text = nombre;
            label33.Text = extension;
            label44.Text = cvdoctor;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            
            conectorSql conecta = new conectorSql();
            DialogResult reply = MessageBox.Show("¿Desea eliminar el archivo seleccionado?", "Eliminar archivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;
          
            for (int i = 0; i < listView4.Items.Count; i++)
            {
                if (listView4.Items[i].Checked== true)
                {
                    string query = "Delete from archivos where clave='" + listView4.Items[i].Text + "' and cvpaciente='" + textBox24.Text.Trim() + "'";
                    conecta.Excute(query);
                    conecta.CierraConexion();
                }
            }
           
            MessageBox.Show("Se elimino correctamente el archivo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button12_Click(sender, e);
        }

        private void listView4_DoubleClick(object sender, EventArgs e)
        {
            button11_Click(sender, e);
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                BuscarCitas();
                BuscarRecibos();
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView2.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = listView2.SelectedIndices;
                foreach (int item in seleccion)
                {
                    DetallesModificaLV2(item);
                }
            }
        }

        public void DetallesModificaLV2(int index)
        {
            listView3.Items.Clear();
            listView3.Columns.Clear();
            string numrecibo = listView2.Items[index].Text;
            string fecha = listView2.Items[index].SubItems[1].Text;

            label45.Text = numrecibo;
            label46.Text = fecha;
            BuscarDetalleRecibo();
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox17_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                label36.Visible = false;
                label37.Visible = true;
                timer1.Enabled = false;
                BuscarInformacion(textBox19.Text.Trim(), textBox17.Text.Trim());
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

