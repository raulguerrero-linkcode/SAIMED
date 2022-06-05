using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
namespace SHOPCONTROL.HistorialClinica
{
    public partial class EstudioColposcopico : Form
    {
        public EstudioColposcopico()
        {
            InitializeComponent();
        }
        string NoExpediente = "";
        string TABLA1 = "";
        string TABLA2 = "";
        string TABLA3 = "";
        string ECTratMedico = "";
        string ECTratBiopsia = "";
        string ECTratCono = "";
        string ECTratPAP = "";
        string ECTratNEG = "";
        string ECTratPositivo = "";

        string ECRHVCervix = "";
        string ECRHEColposcopia = "";
        string ECRHZTransformacion = "";
        string ECRHEAcetoblanco = "";
        string ECRHPuntilleo = "";
        string ECRHMosaico = "";
        string ECRHEGlandular = "";
        string ECRHAEpitelial = "";
        string ECRHEVaginal = "";
        string ECRHShiller = "";
        string ECRHVAnormales = "";
        string ECRHQNAboth = "";

        string ECObservaciones = "";
        string ECFecha = "";
        string ECCODFecha = "";

        string ECTPUM = "";
        string ECTANTFAM = "";
        string ECTALERGIAS = "";
        string ECTCOMEZON = "";
        string ECTPAP = "";
        string ECTPLOMO = "";
        string ECTDIABETES = "";
        string ECTTABACO = "";
        string ECTFLUJO = "";
        string ECTMPF = "";
        string ECTIVSA = "";
        string ECTHIPERTENSION = "";
        string ECTALCOHOL = "";
        string ECTSANGRADO = "";
        string ECTDOCOLPO = "";
        string ECTPS = "";
        string ECTCANCER= "";
        string ECTDROGAS = "";
        string ECTDOCPAP = "";
        string ECTG = "";
        string ECTP = "";
        string ECTC = "";
        string ECTA = "";
        string ECTCIRUGIAS = "";
        string ECTOTROS = "";

        string ECTN1 = "";
        string ECTN2 = "";
        string ECTN3 = "";
        string ECTN4 = "";
        string ECTN5 = "";
        string ECTN6 = "";
        string ECTN7 = "";
        string ECTN8 = "";
        string ECTN9 = "";
        string ECTN10 = "";
        string ECTN11 = "";
        string ECTN12 = "";
        string ECTNTUMOR = "";

        string ECTD1 = "";
        string ECTD2 = "";
        string ECTD3 = "";
        string ECTD4 = "";
        string ECTD5 = "";
        string ECTD6 = "";
        string ECTD7 = "";
        string ECTD8 = "";
        string ECTD9 = "";

        string TEXTCOMEZON= "";
        string TEXTTABACO = "";
        string TEXTPLOMO= "";
        string TEXTFLUJO= "";
        string TEXTALCHOL = "";
        string TEXTSANGRADO = "";
        string TEXTDROGAS = "";
        string TEXTCIRUGIAS = "";
        string TEXTDIABETIS= "";
        string TEXTHIPER = "";
        string TEXCANCER= "";

        string PAREJASSEXUAL= "";
        string MO_FEPAP= "";
        string MO_FECCOL= "";





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



        Image imgVagina; //imagen por defecto
        int consecutivo = 0;
        private bool EDITAR = false;

        string[] tabla1 = new string[25];
        string[] tabla2 = new string[13];
        string[] tabla3 = new string[9];

        public void Recolecta()
        {
            NoExpediente = textBox2.Text.Trim();
         
            TABLA2 = RecolectaListCheckBox(checkedListBox2,tabla2);
            TABLA3 = RecolectaListCheckBox(checkedListBox3,tabla3);
            recolectaValordeTablas();

            ECTratMedico = RecolectaValorChkBox(checkBox1);
            ECTratBiopsia = RecolectaValorChkBox(checkBox2);
            ECTratCono = RecolectaValorChkBox(checkBox3);
            ECTratPAP = RecolectaValorChkBox(checkBox4);
            ECTratNEG = RecolectaValorChkBox(checkBox5);
            ECTratPositivo = RecolectaValorChkBox(checkBox6);

            ECRHVCervix = comboBox1.Text;
            ECRHEColposcopia = comboBox2.Text;
            ECRHZTransformacion = comboBox3.Text;
            ECRHEAcetoblanco = comboBox4.Text;
            ECRHPuntilleo = comboBox5.Text;
            ECRHMosaico = comboBox6.Text;
            ECRHEGlandular = comboBox7.Text;
            ECRHAEpitelial = comboBox8.Text;
            ECRHEVaginal = comboBox9.Text;
            ECRHShiller = comboBox10.Text;
            ECRHVAnormales = comboBox11.Text;
            ECRHQNAboth = comboBox12.Text;

            ECObservaciones = textBox1.Text.Trim();
            ECFecha = DateTime.Now.ToString("dd/MM/yyy");
            ECCODFecha = DateTime.Now.ToString("yyyyMMdd");

            consecutivo = int.Parse(label24.Text);
            MO_FEPAP = "NO";
            MO_FECCOL = "NO";
            if (checkBox18.Checked == true) MO_FEPAP = "SI";
            if (checkBox19.Checked == true) MO_FECCOL= "SI";

            
             PAREJASSEXUAL = textBox14.Text;
             TEXTCOMEZON = textBox15.Text;
             TEXTTABACO = textBox16.Text;
             TEXTPLOMO = textBox17.Text;
             TEXTFLUJO = textBox18.Text;
             TEXTALCHOL = textBox19.Text;
             TEXTSANGRADO = textBox20.Text;
             TEXTDROGAS = textBox21.Text;
             TEXTCIRUGIAS = textBox22.Text;
             TEXTDIABETIS = textBox23.Text;
             TEXTHIPER = textBox24.Text;
             TEXCANCER = textBox25.Text;
        }
        public bool Validacion() {
            if (ECRHVCervix.Equals(""))
            {
                MessageBox.Show("Seleccione volumen de cervix ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
                return false;
            }
            if (ECRHEColposcopia.Equals(""))
            {
                MessageBox.Show("Seleccione estudio de colposcopia ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox2.Focus();
                return false;
            }
            if (ECRHZTransformacion.Equals(""))
            {
                MessageBox.Show("Seleccione zona de transformación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox3.Focus();
                return false;
            }
            if (ECRHEAcetoblanco.Equals(""))
            {
                MessageBox.Show("Seleccione epitelio acetoblanco ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox4.Focus();
                return false;
            }
            if (ECRHPuntilleo.Equals(""))
            {
                MessageBox.Show("Seleccione puntileo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox5.Focus();
                return false;
            }
            if (ECRHMosaico.Equals(""))
            {
                MessageBox.Show("Seleccione mosaico", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox6.Focus();
                return false;
            }
            if (ECRHEGlandular.Equals(""))
            {
                MessageBox.Show("Seleccione eversion glandular", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox7.Focus();
                return false;
            }
            if (ECRHAEpitelial.Equals(""))
            {
                MessageBox.Show("Seleccione atrofia epitelial", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox8.Focus();
                return false;
            }
            if (ECRHEVaginal.Equals(""))
            {
                MessageBox.Show("Seleccione exudado vaginal", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox9.Focus();
                return false;
            }
            if (ECRHShiller.Equals(""))
            {
                MessageBox.Show("Seleccione shiller", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox10.Focus();
                return false;
            }
            if (ECRHVAnormales.Equals(""))
            {
                MessageBox.Show("Seleccione vasos anormales", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox11.Focus();
                return false;
            }
            if (ECRHQNAboth.Equals(""))
            {
                MessageBox.Show("Seleccione quistes de naboth", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox12.Focus();
                return false;
            }
            if (checkedListBox3.CheckedItems.Count==0)
            {
                MessageBox.Show("No ha seleccionado el diagnóstico", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkedListBox3.Focus();
                return false;
            }
            List<CheckBox> chksList = new List<CheckBox>();
            chksList.Add(checkBox1);
            chksList.Add(checkBox2);
            chksList.Add(checkBox3);
            chksList.Add(checkBox4);
            chksList.Add(checkBox5);
            chksList.Add(checkBox6);
          
            return true;
        }
        public void Guardar() {
            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "insert into EColposcopico(";
            Query += "NoExpediente,";
            Query += "ECFecha,";
            Query += "ECCODFecha,";
            Query += "TABLA1,";
            Query += "TABLA2,";
            Query += "TABLA3,";
            Query += "ECTratMedico,";
            Query += "ECTratBiopsia,";
            Query += "ECTratCono,";
            Query += "ECTratPAP,";
            Query += "ECTratNEG,";
            Query += "ECTratPositivo,";
            Query += "ECRHVCervix,";
            Query += "ECRHEColposcopia,";
            Query += "ECRHZTransformacion,";
            Query += "ECRHEAcetoblanco,";
            Query += "ECRHPuntilleo,";
            Query += "ECRHMosaico,";
            Query += "ECRHEGlandular,";
            Query += "ECRHAEpitelial,";
            Query += "ECRHEVaginal,";
            Query += "ECRHShiller,";
            Query += "ECRHVAnormales,";
            Query += "ECRHQNAboth,";
            Query += "ECObservaciones,";

            Query += "ECTPUM,";
            Query += "ECTANTFAM,";
            Query += "ECTALERGIAS,";
            Query += "ECTCOMEZON,";
            Query += "ECTPAP,";
            Query += "ECTPLOMO,";
            Query += "ECTDIABETES,";
            Query += "ECTTABACO,";
            Query += "ECTFLUJO,";
            Query += "ECTMPF,";
            Query += "ECTIVSA,";
            Query += "ECTHIPERTENSION,";
            Query += "ECTALCOHOL,";
            Query += "ECTSANGRADO,";
            Query += "ECTDOCOLPO,";
            Query += "ECTPS,";
            Query += "ECTCANCER,";
            Query += "ECTDROGAS,";
            Query += "ECTDOCPAP,";
            Query += "ECTG,";
            Query += "ECTP,";
            Query += "ECTC,";
            Query += "ECTA,";
            Query += "ECTCIRUGIAS,";
            Query += "ECTOTROS,";
            Query += "ECTN1,";
            Query += "ECTN2,";
            Query += "ECTN3,";
            Query += "ECTN4,";
            Query += "ECTN5,";
            Query += "ECTN6,";
            Query += "ECTN7,";
            Query += "ECTN8,";
            Query += "ECTN9,";
            Query += "ECTN10,";
            Query += "ECTN11,";
            Query += "ECTN12,";
            Query += "ECTNTUMOR,";
            Query += "ECTD1,";
            Query += "ECTD2,";
            Query += "ECTD3,";
            Query += "ECTD4,";
            Query += "ECTD5,";
            Query += "ECTD6,";
            Query += "ECTD7,";
            Query += "ECTD8, ";

            Query += "textocomezon, ";
            Query += "textotabaco, ";
            Query += "textoplomo, ";
            Query += "textoflujo, ";
            Query += "textoalchol, ";
            Query += "textosangrado, ";
            Query += "textodrogas, ";
            Query += "textocirugia, ";
            Query += "textodiabet, ";
            Query += "textohiper, ";
            Query += "textocancer, ";

            Query += "numpasex, ";
            Query += "mo_fechadcol, ";
            Query += "mo_fechapap, ";


            Query += "emitio, ";
            Query += "cvpaciente, ";
            Query += "Fechamod, ";


            Query += "consecutivo) ";
            Query += "values(";
            Query += "'" + NoExpediente + "',";
            Query += "'" + ECFecha + "',";
            Query += "'" + ECCODFecha + "',";
            Query += "'" + TABLA1 + "',";
            Query += "'" + TABLA2 + "',";
            Query += "'" + TABLA3 + "',";
            Query += "'" + ECTratMedico + "',";
            Query += "'" + ECTratBiopsia + "',";
            Query += "'" + ECTratCono + "',";
            Query += "'" + ECTratPAP + "',";
            Query += "'" + ECTratNEG + "',";
            Query += "'" + ECTratPositivo + "',";
            Query += "'" + ECRHVCervix + "',";
            Query += "'" + ECRHEColposcopia + "',";
            Query += "'" + ECRHZTransformacion + "',";
            Query += "'" + ECRHEAcetoblanco + "',";
            Query += "'" + ECRHPuntilleo + "',";
            Query += "'" + ECRHMosaico + "',";
            Query += "'" + ECRHEGlandular + "',";
            Query += "'" + ECRHAEpitelial + "',";
            Query += "'" + ECRHEVaginal + "',";
            Query += "'" + ECRHShiller + "',";
            Query += "'" + ECRHVAnormales + "',";
            Query += "'" + ECRHQNAboth + "',";
            Query += "'" + ECObservaciones + "',";

            Query += "'" + ECTPUM + "',";
            Query += "'" + ECTANTFAM + "',";
            Query += "'" + ECTALERGIAS + "',";
            Query += "'" + ECTCOMEZON + "',";
            Query += "'" + ECTPAP + "',";
            Query += "'" + ECTPLOMO + "',";
            Query += "'" + ECTDIABETES + "',";
            Query += "'" + ECTTABACO + "',";
            Query += "'" + ECTFLUJO + "',";
            Query += "'" + ECTMPF + "',";
            Query += "'" + ECTIVSA + "',";
            Query += "'" + ECTHIPERTENSION + "',";
            Query += "'" + ECTALCOHOL + "',";
            Query += "'" + ECTSANGRADO + "',";
            Query += "'" + ECTDOCOLPO + "',";
            Query += "'" + ECTPS + "',";
            Query += "'" + ECTCANCER + "',";
            Query += "'" + ECTDROGAS + "',";
            Query += "'" + ECTDOCPAP + "',";
            Query += "'" + ECTG + "',";
            Query += "'" + ECTP + "',";
            Query += "'" + ECTC + "',";
            Query += "'" + ECTA + "',";
            Query += "'" + ECTCIRUGIAS + "',";
            Query += "'" + ECTOTROS + "',";
            Query += "'" + ECTN1 + "',";
            Query += "'" + ECTN2 + "',";
            Query += "'" + ECTN3 + "',";
            Query += "'" + ECTN4 + "',";
            Query += "'" + ECTN5 + "',";
            Query += "'" + ECTN6 + "',";
            Query += "'" + ECTN7 + "',";
            Query += "'" + ECTN8 + "',";
            Query += "'" + ECTN9 + "',";
            Query += "'" + ECTN10 + "',";
            Query += "'" + ECTN11 + "',";
            Query += "'" + ECTN12 + "',";
            Query += "'" + ECTNTUMOR + "',";
            Query += "'" + ECTD1 + "',";
            Query += "'" + ECTD2 + "',";
            Query += "'" + ECTD3 + "',";
            Query += "'" + ECTD4 + "',";
            Query += "'" + ECTD5 + "',";
            Query += "'" + ECTD6 + "',";
            Query += "'" + ECTD7 + "',";
            Query += "'" + ECTD8 + "',";

            Query += "'" + TEXTCOMEZON + "',";
            Query += "'" + TEXTTABACO + "',";
            Query += "'" + TEXTPLOMO+ "',";
            Query += "'" + TEXTFLUJO+ "',";
            Query += "'" + TEXTALCHOL + "',";
            Query += "'" + TEXTSANGRADO + "',";
            Query += "'" + TEXTDROGAS + "',";
            Query += "'" + TEXTCIRUGIAS + "',";
            Query += "'" + TEXTDIABETIS + "',";
            Query += "'" + TEXTHIPER+ "',";
            Query += "'" + TEXCANCER+ "',";

            Query += "'" + PAREJASSEXUAL + "',";
            Query += "'" + MO_FECCOL+ "',";
            Query += "'" + MO_FEPAP + "',";

            Query += "'" + valoresg.USUARIOSIS + "',";
            Query += "'" + textBox13.Text.Trim() + "',";
            Query += "'" + DateTime.Now.ToString("dd/MM/yyyy") + "',";
            
            Query += "'" + consecutivo + "')";

            conecta.Excute(Query);
        }
        public void BuscarInformacion(string clave, int _consecutivo) { 
            LimpiarControles(this);
            conectorSql conecta = new conectorSql();
            //string Query = "SELECT  * FROM  EColposcopico WHERE NoExpediente='" + clave + "'";
            string Query = "select * from EColposcopico where NoExpediente = '" + clave + "' ";
            Query += " and consecutivo = " + _consecutivo + "";
            SqlDataReader leer = conecta.RecordInfo(Query);
            textBox2.Text = clave;
            while (leer.Read())
            {
                TABLA1 = leer["TABLA1"].ToString();

                checkBox7.Checked = AsignaValorChk(leer["ECTComezon"].ToString());
                checkBox8.Checked = AsignaValorChk(leer["ECTPlomo"].ToString());
                checkBox10.Checked = AsignaValorChk(leer["ECTTabaco"].ToString());
                checkBox11.Checked = AsignaValorChk(leer["ECTFlujo"].ToString());
                checkBox13.Checked = AsignaValorChk(leer["ECTAlcohol"].ToString());
                checkBox14.Checked = AsignaValorChk(leer["ECTSangrado"].ToString());
                checkBox16.Checked = AsignaValorChk(leer["ECTDrogas"].ToString());
                checkBox17.Checked = AsignaValorChk(leer["ECTCirugias"].ToString());
                checkBox9.Checked = AsignaValorChk(leer["ECTDiabetes"].ToString());
                checkBox12.Checked = AsignaValorChk(leer["ECTHipertension"].ToString());


                string cadena = leer["ECTPUM"].ToString();
                textBox37.Text = cadena;
                textBox3.Text = leer["ECTANTFAM"].ToString();
                textBox4.Text = leer["ECTALERGIAS"].ToString();
                textBox6.Text = leer["ECTMPF"].ToString();
                textBox7.Text = leer["ECTANTFAM"].ToString();
               
                textBox14.Text = leer["ECTPS"].ToString();
                textBox5.Text = leer["ECTIVSA"].ToString();

                textBox8.Text = leer["ECTG"].ToString();
                textBox9.Text = leer["ECTP"].ToString();
                textBox10.Text = leer["ECTC"].ToString();
                textBox11.Text = leer["ECTA"].ToString();

                textBox7.Text = leer["ECTPAP"].ToString();

                textBox30.Text = leer["ECTDOCPAP"].ToString();
                textBox27.Text = leer["ECTDOCOLPO"].ToString();



                TABLA2 = leer["TABLA2"].ToString();
                AsignaValorChkList(checkedListBox2,TABLA2);
                TABLA3 = leer["TABLA3"].ToString();
                AsignaValorChkList(checkedListBox3,TABLA3);
                checkBox1.Checked = AsignaValorChk(leer["ECTratMedico"].ToString());
                checkBox2.Checked = AsignaValorChk(leer["ECTratBiopsia"].ToString());
                checkBox3.Checked = AsignaValorChk(leer["ECTratCono"].ToString());
                checkBox4.Checked = AsignaValorChk(leer["ECTratPAP"].ToString());
                checkBox5.Checked = AsignaValorChk(leer["ECTratNEG"].ToString());
                checkBox6.Checked = AsignaValorChk(leer["ECTratPositivo"].ToString());


                comboBox1.Text = leer["ECRHVCervix"].ToString();
                comboBox2.Text = leer["ECRHEColposcopia"].ToString();
                comboBox3.Text = leer["ECRHZTransformacion"].ToString();
                comboBox4.Text = leer["ECRHEAcetoblanco"].ToString();
                comboBox5.Text = leer["ECRHPuntilleo"].ToString();
                comboBox6.Text = leer["ECRHMosaico"].ToString();
                comboBox7.Text = leer["ECRHEGlandular"].ToString();
                comboBox8.Text = leer["ECRHAEpitelial"].ToString();
                comboBox9.Text = leer["ECRHEVaginal"].ToString();
                comboBox10.Text = leer["ECRHShiller"].ToString();
                comboBox11.Text = leer["ECRHVAnormales"].ToString();
                comboBox12.Text = leer["ECRHQNAboth"].ToString();
                textBox1.Text = leer["ECObservaciones"].ToString();

                textBox15.Text = leer["textocomezon"].ToString();
                textBox16.Text = leer["textotabaco"].ToString();
                textBox17.Text = leer["textoplomo"].ToString();
                textBox18.Text = leer["textoflujo"].ToString();
                textBox19.Text = leer["textoalchol"].ToString();
                textBox20.Text = leer["textosangrado"].ToString();
                textBox21.Text = leer["textodrogas"].ToString();
                textBox22.Text = leer["textocirugia"].ToString();
                textBox23.Text = leer["textodiabet"].ToString();
                textBox24.Text = leer["textohiper"].ToString();
                textBox25.Text = leer["textocancer"].ToString();

                textBox13.Text = leer["cvpaciente"].ToString();

                textBox14.Text = leer["numpasex"].ToString();
                string valor = leer["mo_fechapap"].ToString();
                if (valor == "SI") checkBox18.Checked = true;
                else checkBox18.Checked = false;

                valor = leer["mo_fechadcol"].ToString();
                if (valor == "SI") checkBox19.Checked = true;
                else checkBox19.Checked = false;

                label24.Text = _consecutivo.ToString();
            }

            if (ExisteImagen(clave, vagina.Name,_consecutivo))
            {
                vagina.Image = ClaseFotos.ConsultarImagenExpediente(clave, vagina.Name, _consecutivo);
            }
        }
        public string Clave_Paciente = "";
        public void BuscarNoExpedinte(string expediente, string clavepaciente)
        {
            conectorSql conecta = new conectorSql();
            //string Query ="SELECT * FROM  Pacientes WHERE  clave='" + clavepaciente+ "' and expgineco<>''";
            //bool existeexpe = conecta.ExisteRegistro(Query);
            //if (existeexpe == false)
            //{
            //    MessageBox.Show("El paciente no tiene expediente de ginecologia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            
            string Query = "SELECT NOMBRE,APATERNO,AMATERNO,EDAD, clave,expgineco FROM  Pacientes WHERE  clave<>''";
            if (expediente != "") Query = Query + " and  expgineco='" + expediente + "'";
            if (clavepaciente!="") Query = Query + " and  clave='" + clavepaciente+ "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                label18.Text = leer["NOMBRE"].ToString() + " " + leer["APATERNO"].ToString() + " " + leer["AMATERNO"].ToString();
                label19.Text = leer["EDAD"].ToString();
                label39.Text = leer["CLAVE"].ToString();
                textBox2.Text= leer["expgineco"].ToString();

            }
            conecta.CierraConexion();
            Clave_Paciente = clavepaciente;
            pbFoto.Image = ClaseFotos.ConsultarFotoPaciente(label39.Text);
            buscarRegitros();

        }
        public string  RecolectaListCheckBox(CheckedListBox CLB,string[] table)
        {
            string valor="";
            if (CLB.CheckedItems.Count != 0)
            {
                foreach (int indice in CLB.CheckedIndices)
                {
                    table[indice] = "x";
                    valor += indice + " ";
                }
            }
            return valor;
        }
        public void AsignaValorChkList(CheckedListBox CLB, string indices)
        {
            
            string[] arrayIndices = indices.Split(' ');
            int i;
            foreach (string item in arrayIndices)
            {
                if (!item.Equals(""))
                {
                    i = int.Parse(item);
                    CLB.SetItemChecked(i, true);
                }   
            }
        }
        public bool AsignaValorChk(string valor) {
            if (valor.Equals("") || valor=="NO")
            {
                return false;
            }
            else { return true; }
        }
        public string RecolectaValorChkBox(CheckBox chk)
        {
            if (chk.Checked)
            {
                return "x";
            }
            else { return ""; }
        }
        public bool  SinPlanDeTrabajo(List<CheckBox> list ) {
            foreach (CheckBox chk in list )
            {
                if (chk.Checked)
                {
                    return false;
                }
            }
            return true;

        }
        public void LimpiarControles(Control cntr)
        {           
            foreach (Control c in cntr.Controls)
            {
                if (c is Panel)
                    LimpiarControles((Panel)c);
                else if (c is TextBox)
                    c.Text = "";
                else if (c is ComboBox)
                {
                    c.Text = "";
                }
                else if (c is CheckBox)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)c;
                    chk.Checked = false;
                }
                else if (c is CheckedListBox)
                {
                    CheckedListBox ckkList = new CheckedListBox();
                    ckkList = (CheckedListBox)c;

                    foreach (int indice in ckkList.CheckedIndices)
                    {
                        ckkList.SetItemChecked(indice, false);
                    }
                }
            }
            TABLA1 = "";
            TABLA2 = "";
            TABLA3 = "";
            vagina.Image = imgVagina;
            resetTablas(tabla1);
            resetTablas(tabla2);
            resetTablas(tabla3);
            cargaConsecutivo();
        }
        public void limpiarLabels() {
            label18.Text = "..";
            label19.Text = "..";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            RecolectaPacienteInfo();
            Recolecta();
            if (Validacion())
            {
                if (EDITAR)
                {
                    EliminaRegistro(NoExpediente, consecutivo);
                }
                else {
                    actualizaConsecutivo();
                }
                ActualizarDatosPaciente();
                Guardar();
                MessageBox.Show("Se guardo correctamente la información", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NuevoEstudioCol();
                textBox2.Focus();
            }
        }
       
        public bool ExisteImagen(string clave, string _nombre, int _consecutivo)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select noExpediente from imagenesclinica where noExpediente='" + clave + "' and nombre='" + _nombre + "' and consecutivo=" + _consecutivo + "";
            return conecta.ExisteRegistro(Query);
        }
        public void EliminaRegistro(string clave, int _consecutivo) {
            conectorSql conecta = new conectorSql();
            string Query = "DELETE FROM EColposcopico where NoExpediente='" + clave + "' AND consecutivo =" + _consecutivo + "";
            
            conecta.Excute(Query);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            label71.Text = textBox13.Text;
            label72.Text = textBox2.Text;

          BuscarNoExpedinte(textBox2.Text.Trim(), textBox13.Text.Trim());
          BuscarInformacionBasica(textBox13.Text.Trim());
        }

       

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button3_Click(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NuevoEstudioCol();
        }

        public void NuevoEstudioCol()
        {
            activaNuevo();
           // LimpiarControles(this);
            IniciaNuevoE();
            LimpiarControles(tabControl1);
            Lv.Items.Clear();
            Lv.Columns.Clear();

            BuscarNoExpedinte(textBox2.Text.Trim(), textBox13.Text.Trim());
            BuscarInformacionBasica(textBox13.Text);
            buscarRegitros();
            cargaConsecutivo();

            textBox15.Visible = false;
            textBox16.Visible = false;
            textBox17.Visible = false;
            textBox18.Visible = false;
            textBox19.Visible = false;
            textBox20.Visible = false;
            textBox21.Visible = false;
            textBox22.Visible = false;
            textBox23.Visible = false;
            textBox24.Visible = false;
            textBox25.Visible = false;
            textBox27.Visible = false;
            
            textBox30.Visible= false;
            label33.Visible = true;
            label32.Visible = true;
         
         
            textBox13.Enabled = true;
            textBox2.Enabled = true;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            comboBox8.SelectedIndex = 0;
            comboBox9.SelectedIndex = 0;
            comboBox10.SelectedIndex = 0;
            comboBox11.SelectedIndex = 0;
            comboBox12.SelectedIndex = 0;
            comboBox1.Text= "Ninguno";
            comboBox2.Text = "Ninguno";
            comboBox3.Text = "Ninguno";
            comboBox4.Text = "Ninguno";
            comboBox5.Text = "Ninguno";
            comboBox6.Text = "Ninguno";
            comboBox7.Text = "Ninguno";
            comboBox8.Text = "Ninguno";
            comboBox9.Text = "Ninguno";
            comboBox10.Text = "Ninguno";
            comboBox11.Text = "Ninguno";
            comboBox12.Text = "Ninguno";
            imgVagina = vagina.Image;
            label77.Text = label24.Text;
            textBox5.Focus();

        }

        private void vagina_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Equals(""))
            {
                MessageBox.Show("Ingresa el número de expediente primero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox2.Focus();
            }
            else
            {
                valoresg.BANDIMAGEN = vagina.Name;
                HistorialClinica.Editor myEditor = new HistorialClinica.Editor();
                myEditor.imagenFondo = vagina.Image;
                myEditor.imagenNueva = imgVagina;
                myEditor.NOMBREIMAGEN = vagina.Name;
                myEditor.NOEXPEDIENTE = textBox2.Text.Trim();
                myEditor.CONSECUTIVO = int.Parse(label24.Text);
                myEditor.Show();
            }
        }

        private void EstudioColposcopico_Activated(object sender, EventArgs e)
        {
            
            if ( valoresg.CLAVEPAC != "" &&  valoresg.CLAVEPAC != null)
            {
                IniciaNuevoE();
                textBox2.Text = valoresg.BNUMEXPGINECO;
                textBox13.Text = valoresg.CLAVEPAC;
                label71.Text = textBox13.Text;
                label72.Text = textBox2.Text;

                NuevoEstudioCol();
                BuscarInformacionBasica(textBox13.Text.Trim());
                BuscarNoExpedinte(textBox2.Text.Trim(), textBox13.Text.Trim());
                textBox2.Text = valoresg.BNUMEXPGINECO;
                textBox13.Text = valoresg.CLAVEPAC;
               
               // int Num_Estudio = 0;
               // if (Lv.Items.Count > 0)
               // {
               //     Num_Estudio = int.Parse( Lv.Items[0].SubItems[4].Text);

               // }

               // textBox13.Enabled = false;
               // textBox2.Enabled = false;
               //// textBox13.Text = Clave_Paciente;
               // BuscarInformacion(textBox2.Text, Num_Estudio);
               // label24.Text = Num_Estudio.ToString();
               // activaEditar();
                valoresg.CLAVEPAC = "";
                valoresg.BNUMEXPGINECO = "";
            }
          

            //if (valoresg.BANDIMAGEN == vagina.Name && ExisteImagen(textBox2.Text, vagina.Name, int.Parse(label24.Text)))
            //{
            //    vagina.Image = ClaseFotos.ConsultarImagenExpediente(textBox2.Text.Trim(), vagina.Name, int.Parse(label24.Text));
            //    valoresg.BANDIMAGEN = "";
            //}
        }

        private void EstudioColposcopico_Load(object sender, EventArgs e)
        {
           
        }

        int NUMEXPEDIENTEG = 0;
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
            textBox2.Text = NUMEXPEDIENTEG.ToString();
        }

        public void ActualizarDatosPaciente()
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
            Query += ",TELEFONO='" + TELEFONO + "'";
            Query += ",CALLE='" + CALLE + "'";
            Query += ",MUNICIPIO='" + MUNICIPIO+ "'";
            Query += ",Pregunta2='" + Pregunta2 + "'";
            Query += ",CELULAR='" + CELULAR + "'";
            Query += ",expgineco='" + textBox2.Text.Trim() + "'";
            Query = Query + " where clave='" + CLAVE + "'";
            conecta.Excute(Query);

        }

        public void RecolectaPacienteInfo()
        {
            NOMBRE = textBox39.Text.Trim();
            APATERNO = textBox32.Text.Trim();
            AMATERNO = textBox35.Text.Trim();
            GENERO = comboBox14.Text;
            ESCOLARIDAD = comboBox15.Text;
            EMAIL = textBox38.Text.Trim();
            EDAD = textBox36.Text.Trim();
            ECivil = comboBox16.Text;
            NoHijos = textBox34.Text.Trim();
            TELEFONO = textBox33.Text.Trim();
            CALLE = textBox28.Text.Trim();
            MUNICIPIO = textBox29.Text.Trim();
            Pregunta2 = comboBox13.Text.Trim();
            CLAVE = textBox13.Text;
            CELULAR = textBox31.Text;
        }


        public void BuscarInformacionBasica(string claveind)
        {
            conectorSql conecta = new conectorSql();
            string Query = "SELECT  * FROM  Pacientes WHERE  clave<>'' ";
            if (claveind != "") Query = Query + " and clave='" + claveind + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox39.Text = leer["NOMBRE"].ToString();
                textBox32.Text = leer["APATERNO"].ToString();
                textBox35.Text = leer["AMATERNO"].ToString();
                comboBox14.Text = leer["GENERO"].ToString();
                comboBox15.Text = leer["ESCOLARIDAD"].ToString();
                textBox38.Text = leer["EMAIL"].ToString();
                textBox36.Text = leer["EDAD"].ToString();
                comboBox16.Text = leer["ECivil"].ToString();
                textBox34.Text = leer["NoHijos"].ToString();

                textBox33.Text = leer["TELEFONO"].ToString();
                comboBox13.Text = leer["Pregunta2"].ToString();
                textBox31.Text = leer["CELULAR"].ToString();

                textBox28.Text = leer["CALLE"].ToString();
                textBox29.Text = leer["MUNICIPIO"].ToString();
            }
            conecta.CierraConexion();
            pbFoto.Image = ClaseFotos.ConsultarFotoPaciente(claveind);

        }


        public void IniciaNuevoE()
        {
            imgVagina = vagina.Image;
            resetTablas(tabla1);
            resetTablas(tabla2);
            resetTablas(tabla3);
            cargaConsecutivo();

            checkBox7.Checked = false;
            checkBox10.Checked = false;
            checkBox8.Checked = false;
            checkBox11.Checked = false;
            checkBox13.Checked = false;
            checkBox14.Checked = false;
            checkBox16.Checked = false;
            checkBox17.Checked = false;
            checkBox9.Checked = false;
            checkBox12.Checked = false;
            checkBox15.Checked = false;

            textBox15.Visible = false;
            textBox16.Visible = false;
            textBox17.Visible = false;
            textBox18.Visible = false;
            textBox19.Visible = false;
            textBox20.Visible = false;
            textBox21.Visible = false;
            textBox22.Visible = false;
            textBox23.Visible = false;
            textBox24.Visible = false;
            textBox25.Visible = false;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            comboBox8.SelectedIndex = 0;
            comboBox9.SelectedIndex = 0;
            comboBox10.SelectedIndex = 0;
            comboBox11.SelectedIndex = 0;
            comboBox12.SelectedIndex = 0;

            textBox13.Focus();

        }

        public void resetTablas(string[] table) {
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = "";
            }
        }
        public void recolectaValordeTablas() {
            ECTPUM =textBox37.Text.Trim();
            ECTANTFAM = textBox3.Text.Trim();
            ECTALERGIAS = textBox4.Text.Trim();
            ECTCOMEZON = "NO";
            ECTPLOMO = "NO";
            ECTDIABETES = "NO";
            ECTTABACO = "NO";
            ECTFLUJO = "NO";
            ECTTABACO = "NO";
            ECTALCOHOL = "NO";
            ECTSANGRADO = "NO";



            if (checkBox7.Checked == true) ECTCOMEZON = "SI";
            if (checkBox8.Checked == true) ECTPLOMO = "SI";
          
            if (checkBox8.Checked == true) ECTPLOMO = "SI";
            if (checkBox10.Checked == true) ECTTABACO = "SI";
            if (checkBox11.Checked == true) ECTFLUJO = "SI";

            if (checkBox13.Checked == true) ECTALCOHOL = "SI";
            if (checkBox14.Checked == true) ECTSANGRADO = "SI";

            if (checkBox16.Checked == true) ECTDROGAS = "SI";
            if (checkBox17.Checked == true) ECTCIRUGIAS = "SI";

            if (checkBox9.Checked == true) ECTDIABETES = "SI";
            if (checkBox12.Checked == true) ECTHIPERTENSION = "SI";
            if (checkBox15.Checked == true) ECTCANCER = "SI";

            ECTPAP = textBox7.Text;
            ECTOTROS = textBox12.Text;
            ECTPS = textBox14.Text.Trim();
            ECTIVSA = textBox5.Text.Trim();
            ECTMPF = textBox6.Text.Trim();
            ECTDOCPAP = textBox30.Text;
            ECTDOCOLPO = textBox27.Text;
        

            ECTG = textBox8.Text;
            ECTP = textBox9.Text;
            ECTC = textBox10.Text;
            ECTA = textBox11.Text;



            ECTN1 = tabla2[0];
            ECTN7 = tabla2[1];
            ECTN2 = tabla2[2];
            ECTN8 = tabla2[3];
            ECTN3 = tabla2[4];
            ECTN9 = tabla2[5];
            ECTN4 = tabla2[6];
            ECTN10 = tabla2[7];
            ECTN5 = tabla2[8];
            ECTN11 = tabla2[9];
            ECTN6 = tabla2[10];
            ECTN12 = tabla2[11];
            ECTNTUMOR = tabla2[12];

            ECTD1 = tabla3[0];
            ECTD2 = tabla3[1];
            ECTD3 = tabla3[2];
            ECTD4 = tabla3[3];
            ECTD5 = tabla3[4];
            ECTD6 = tabla3[5];
            ECTD7 = tabla3[6];
            ECTD8 = tabla3[7];
            ECTD9 = tabla3[8];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mandarReporteCristal();
        }

        public void mandarReporteCristal() {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Ingrese numero de expediente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!ExisteRegistro(textBox2.Text,int.Parse(label24.Text)))
            {
                MessageBox.Show("Guarde los cambios realizados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else { 
                ReportDocument cryRpt = new ReportDocument();
            
                string CadenaReporte = "C:\\tmp\\reports\\DEColposcopico.rpt";

                DataSet ds = new DataSet();

                DEstudioColposcopicoTableAdapters.EColposcopicoTableAdapter ta = new DEstudioColposcopicoTableAdapters.EColposcopicoTableAdapter();
                DEstudioColposcopico.EColposcopicoDataTable estudio = new DEstudioColposcopico.EColposcopicoDataTable();
                ta.Fill(estudio, textBox2.Text, int.Parse(label24.Text));

                DEstudioColposcopicoTableAdapters.imagenesclinicaTableAdapter tap1 = new DEstudioColposcopicoTableAdapters.imagenesclinicaTableAdapter();
                DEstudioColposcopico.imagenesclinicaDataTable dtVagina = new DEstudioColposcopico.imagenesclinicaDataTable();
                tap1.Fill(dtVagina, textBox2.Text, int.Parse(label24.Text));

                DEstudioColposcopicoTableAdapters.PacientesTableAdapter tap = new DEstudioColposcopicoTableAdapters.PacientesTableAdapter();
                DEstudioColposcopico.PacientesDataTable paciente = new DEstudioColposcopico.PacientesDataTable();
                tap.Fill(paciente, textBox2.Text);
 
                ds.Clear();
                ds.Tables.Add(estudio);
                ds.Tables.Add(dtVagina);
                ds.Tables.Add(paciente);

                cryRpt.Load(CadenaReporte);
                cryRpt.SetDataSource(ds);

                string NombreArchivo = @"C:\Colposcopico_" + textBox2.Text + "_" + int.Parse(label24.Text) + ".pdf";
                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, NombreArchivo);
                //cryRpt.PrintToPrinter(1, false, 0, 0);
                cryRpt.Close();
                cryRpt.Dispose();

                MessageBox.Show("Se genero el archivo exitosamente ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try
                {
                    System.Diagnostics.Process.Start(NombreArchivo);
                    this.Dispose();
                }
                catch (Exception er)
                {
                    MessageBox.Show("Verifique que no se encuentre abierto el Archivo PDF", "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }

            }
        }

        public bool ExisteRegistro(string _numExpediente, int _consecutivo) {
            conectorSql conecta = new conectorSql();
            string Query = "Select noExpediente from EColposcopico where noExpediente='" + _numExpediente + "' and consecutivo=" + _consecutivo + "";
            return conecta.ExisteRegistro(Query);
        }

        public void cargaConsecutivo() {
            string Numero = "1";
            conectorSql conecta = new conectorSql();
            string Query = "Select colposcopico from consecutivos where colposcopico <>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                Numero = leer["colposcopico"].ToString();
            }
            conecta.CierraConexion();
            label24.Text = Numero;
        }
        public bool actualizaConsecutivo() {
            conectorSql conecta = new conectorSql();
            int Siguiente = int.Parse(label24.Text) + 1;
            string Query = "update consecutivos set colposcopico='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }

        public void activaNuevo()
        {
            lbEditar.Visible = false;
            lbNuevo.Visible = true;
            EDITAR = false;
        }
        public void activaEditar()
        {
            lbNuevo.Visible = false;
            lbEditar.Visible = true;
            EDITAR = true;
        }

     

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Ingrese numero de expediente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
            }
            else
            {
                HistorialClinica.HistorialPaciente myHistorial = new HistorialPaciente();
                myHistorial.tipoEstudio = "Colposcopico";
                myHistorial.noExpediente = textBox2.Text.Trim();
                myHistorial.clavepaciente = textBox13.Text.Trim();
                myHistorial.Show();
            }
        }


        public void buscarRegitros()
        {
            string col2 = "";
            string Query = "";
            Query = "SELECT top(10) ecolposcopico.NoExpediente,ecolposcopico.ECFecha, ecolposcopico.ECObservaciones,ecolposcopico.consecutivo ";
            Query = Query + "  ,pacientes.nombre + ' ' + pacientes.APATERNO + ' ' + pacientes.AMATERNO as nombrepac ";
            Query = Query + "  ,pacientes.expgineco as noexpediente";
            Query = Query + "  ,pacientes.clave as clavepa";

            Query = Query + " FROM EColposcopico inner join pacientes on pacientes.expgineco=ecolposcopico.NoExpediente ";
            Query = Query + " where  ecolposcopico.NoExpediente<>''";
            if (textBox2.Text != "") Query = Query + " and ecolposcopico.NoExpediente='" + textBox2.Text.Trim() + "'";
            if (Clave_Paciente != "") Query = Query + " and pacientes.clave='" + Clave_Paciente + "'";
            Query = Query + " order by ECCODFecha desc";
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 0);
            Lv.Columns.Add("Paciente", 0);
            Lv.Columns.Add("Num. Expediente", 0);
            Lv.Columns.Add("Fecha", 100);
            Lv.Columns.Add("Num. Estudio", 100);
            Lv.Columns.Add("Obsrva", 0);

            int cantColumnas = 6;
            Lv.BeginUpdate();
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = conecta.RecordInfo(Query);
            int i = 0;
            label70.Text = "";
            while (leer.Read())
            {
                if (i == 0) label70.Text = leer["ECFecha"].ToString();
                ListViewItem lvi = new ListViewItem(leer["clavepa"].ToString());
                lvi.SubItems.Add(leer["nombrepac"].ToString());
                lvi.SubItems.Add(leer["noexpediente"].ToString());
                lvi.SubItems.Add(leer["ECFecha"].ToString());
                lvi.SubItems.Add(leer["consecutivo"].ToString());
                lvi.SubItems.Add(leer["ECObservaciones"].ToString());
                Lv.Items.Add(lvi);
                i++;
            }
            conecta.CierraConexion();
            Lv.EndUpdate();
        }

        private void EstudioColposcopico_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.F3 == e.KeyCode)
            {
                HistorialPaciente buscar = new HistorialPaciente();
                buscar.Show();
            }
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox18.Checked == true)
            {
                label32.Visible = true;
                textBox30.Visible = true;
            }
            else
            {
                label32.Visible = false;
                textBox30.Visible = false;
            }
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            label33.Visible = checkBox19.Checked;
            textBox27.Visible= checkBox19.Checked;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            textBox15.Visible = checkBox7.Checked;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            textBox16.Visible = checkBox10.Checked;

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            textBox17.Visible = checkBox8.Checked;

        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            textBox18.Visible = checkBox11.Checked;

        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            textBox19.Visible = checkBox13.Checked;

        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            textBox20.Visible = checkBox14.Checked;
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            textBox21.Visible = checkBox16.Checked;

        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            textBox22.Visible = checkBox17.Checked;

        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            textBox23.Visible = checkBox9.Checked;

        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            textBox24.Visible = checkBox12.Checked;
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            textBox25.Visible = checkBox15.Checked;
        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
              
                button3_Click(sender, e);
            }
        }

        private void Lv_DoubleClick(object sender, EventArgs e)
        {
            textBox13.Enabled = false;
            textBox2.Enabled = false;
            textBox13.Text = Clave_Paciente;
            BuscarInformacion(textBox2.Text, int.Parse(label44.Text));
            label24.Text = label44.Text;
            label77.Text = label24.Text;

            activaEditar();

               
        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Lv.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = Lv.SelectedIndices;
                foreach (int item in seleccion)
                {
                    VerDetallesAbajo(item);
                }
            }
        }
        public void VerDetallesAbajo(int index)
        {

           label44.Text= Lv.Items[index].SubItems[4].Text;

        }

        public string ConformaQuery()
        {
            string cadenar = "";
            int contar = 1;
            conectorSql conecta = new conectorSql();
            string Query = "Select * from doctores where tipoexpediente='GINECO'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                if (contar == 1) Query = " (cvdoctor='" + leer["cvdoctor"].ToString() + "'";
                if (contar >1) Query = " or cvdoctor='" + leer["cvdoctor"].ToString() + "'";

            }
            conecta.CierraConexion();
            Query = Query + ")";
            return cadenar;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            listView4.Items.Clear();
            listView4.Columns.Clear();
            listView4.Columns.Add("ID Archivo", 100);
            listView4.Columns.Add("Nombre", 300);
            listView4.Columns.Add("Fecha", 100);
            listView4.Columns.Add("emite", 100);
            listView4.Columns.Add("Estatus", 150);
            listView4.Columns.Add("extension", 50);
            listView4.Columns.Add("cvdoctor", 0);
            conectorSql conecta = new conectorSql();
            string cadenaregresa = ConformaQuery();
            string query = "Select top(25) * from archivos where cvpaciente='" + textBox13.Text + "'";
            if (cadenaregresa!="") query = query + " and " + cadenaregresa;
            query = query + " order by clave desc";
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
            label50.Text= listView4.Items.Count.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string cadena = ClaseArchivos.EscribirArchivoBytes(label46.Text, textBox13.Text, label49.Text);

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
            string extension = listView4.Items[index].SubItems[5].Text;
            string cvdoctor = listView4.Items[index].SubItems[6].Text;

            label46.Text = clave;
            label47.Text = nombre;
            label49.Text = extension;
            label48.Text = cvdoctor;
        }

        private void listView4_DoubleClick(object sender, EventArgs e)
        {
            button11_Click(sender, e);
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
          

          
            if (tabControl1.SelectedIndex == 2)
            {
                button12_Click(sender, e);
            }

            if (tabControl1.SelectedIndex == 4)
            {
                CargarNotasUltrasonido();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RecolectaPacienteInfo();
            ActualizarDatosPaciente();
            MessageBox.Show("Se actualizo la informacion basica del paciente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = listView1.SelectedIndices;
                foreach (int item in seleccion)
                {
                    DetallesModifica(item);
                }
            }
        }

        public void DetallesModifica(int index)
        {
            string fecha = listView1.Items[index].SubItems[2].Text;
            string informe = listView1.Items[index].SubItems[3].Text;
            label73.Text = fecha;
            textBox26.Text = informe;
        }


        public void CargarNotasUltrasonido()
        {
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("Recibo", 60).Tag = "NUMBER";
            listView1.Columns.Add("Turno", 50).Tag = "STRING";
            listView1.Columns.Add("Fecha", 80).Tag = "STRING";
            listView1.Columns.Add("Nota", 400).Tag = "STRING";
            listView1.Columns.Add("Emite", 80).Tag = "STRING";
            label73.Text = "";
            textBox26.Text = "";
            conectorSql conecta = new conectorSql();
            string Query = "Select TOP (25) * from NEvolucion where cvpaciente='" + label71.Text + "' and cvdoctor='3'";
            Query = Query + " order by fechacod desc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["numrecibo"].ToString());
                lvi.SubItems.Add(leer["numturno"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                lvi.SubItems.Add(leer["informe"].ToString());
                lvi.SubItems.Add(leer["emite"].ToString());
                listView1.Items.Add(lvi);
            }
            conecta.CierraConexion();
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox20.Checked == true)
            {
                cargaConsecutivoExpedienteGine();
                textBox2.Enabled = false;
            }
            else
            {
                textBox2.Enabled = true;
                textBox2.Text = "";
                textBox2.Focus();
            }
        }
    }
}
