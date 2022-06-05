using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SHOPCONTROL.HistorialClinica
{
    public partial class ControlPrenatal : Form
    {
        public ControlPrenatal()
        {
            InitializeComponent();
        }

        string NoExpediente = "";
        string FECHA = "";
        string FCOD = "";
        string GSANGUINEO = "";
        string FRR = "";
        string GESTA = "";
        string PARA = "";
        string ABORTOS = "";
        string CESAREAS = "";
        string OBSERVACIONES1 = "";

        string HFECHA = "";
        string HEDAD = "";
        string HPESO = "";
        string HTA = "";
        string HUTERO = "";
        string HPRODUCTO = "";
        string HFOCO = "";
        string HEDEMA = "";
        string HTRATAMIENTO = "";
        string HOBSERVACIONES = "";

        string PFECHA = "";
        string PFCOD = "";
        string PHORA = "";
        string PARTO = "";
        string PARTOCAUSA1 = "";
        string PARTOCAUSA2 = "";
        string PALUMBRAMIENTO = "";
        string PEPISTOTOMIA = "";
        string PDESGARROS = "";
        string PRCAVIDAD = "";
        string PDURACION = "";

        string PROSEXO = "";
        string PROPESO = "";
        string PROMIN = "";
        string PROANORMALIDADES = "";
        string PROSITIPO = "";
        string PROTIPO = "";

        string AAGRUPO1 = "";
        string AARESULTADO1 = "";
        string AAGRUPO2 = "";
        string AARESULTADO2 = "";
        string AAOTROS = "";
        string AAPSICO = "";
        string AARESULTADO = "";
        string AAPBSERVACIONES = "";
        string AAPRESENTE = "";

        string RPPFECHA = "";
        string RPPPESO = "";
        string RPPTA = "";
        string RPPLACTANCIA = "";
        string RPPUERPERIO = "";
        string RPPCAUSA = "";
        string RPPOTROS = "";

        string EABDOMEN = "";
        string EGMAMARIAS = "";
        string EVULVA = "";
        string EEPISIOTOMIA = "";
        string EVVAGINOTOMIA = "";
        string EVDESGARROS = "";
        string EVLEUCEMIA = "";
        string ECDESGARROS = "";
        string ECEROSIONES = "";
        string EUTERO = "";
        string EANEXOS = "";
        string EEPEDIDOS = "";
        string EPAPANICOLAOU = "";
        string ETRATAMIENTO = "";
        string EMETODO = "";
        int CONSECUTIVO = 0;

        public void recolecta() {
            NoExpediente = textBox1.Text.Trim();
            FECHA = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            FCOD = dateTimePicker1.Value.ToString("yyyyMMdd");
            GSANGUINEO = textBox2.Text.Trim();
            FRR = textBox3.Text.Trim();
            GESTA = textBox4.Text.Trim();
            PARA = textBox5.Text.Trim();
            ABORTOS = textBox6.Text.Trim();
            CESAREAS = textBox7.Text.Trim();
            OBSERVACIONES1 = textBox8.Text.Trim();

            PFECHA = dateTimePicker3.Value.ToString("dd/MM/yyyy");
            PFCOD = dateTimePicker3.Value.ToString("dd/MM/yyyy");
            PHORA = dateTimePicker4.Value.ToString("HH:mm:ss");
            recolectaParto();
            //PARTO = textBox1.Text.Trim();
            //PARTOCAUSA = textBox1.Text.Trim();
            PALUMBRAMIENTO = recolectaRadioOpciones(radioButton7, radioButton8, radioButton9);
            PEPISTOTOMIA = recolectaRadioOpciones(radioButton12, radioButton11, radioButton10);
            PDESGARROS = recolectaRadioOpciones(radioButton15, radioButton14, radioButton13);
            PRCAVIDAD = recolectaRadioOpciones(radioButton18, radioButton17);
            PDURACION = recolectaRadioOpciones(radioButton21, radioButton20, radioButton19);

            PROSEXO = recolectaGenero(radioButton16, radioButton22);
            PROPESO = textBox22.Text.Trim();
            PROMIN = textBox23.Text.Trim();
            PROANORMALIDADES = recolectaRadioOpciones(radioButton23);
            //PROSITIPO = textBox1.Text.Trim();
            PROTIPO = recolectaDosOpciones(radioButton25, radioButton24);

            AAGRUPO1 = recolectaRadioOpciones(radioButton27,radioButton26,radioButton28);
            AARESULTADO1 = textBox25.Text.Trim();
            AAGRUPO2 = recolectaRadioOpciones(radioButton31, radioButton30, radioButton29);
            AARESULTADO2 = textBox26.Text.Trim();
            AAPSICO = recolectaRadioOpciones(radioButton34);
            AARESULTADO = textBox27.Text.Trim();
            AAPBSERVACIONES = textBox28.Text.Trim();
            AAPRESENTE = recolectaRadioOpciones(radioButton36, radioButton35);

            RPPFECHA = dateTimePicker6.Value.ToString("dd/MM/yyyy");
            RPPPESO = textBox46.Text.Trim();
            RPPTA = textBox47.Text.Trim();
            RPPLACTANCIA = textBox48.Text.Trim();
            RPPUERPERIO = recolectaPuerperio(radioButton38, radioButton37);
            //RPPCAUSA = textBox1.Text.Trim();
            RPPOTROS = textBox30.Text.Trim();

            EABDOMEN = textBox31.Text.Trim();
            EGMAMARIAS = textBox32.Text.Trim();
            EVULVA = textBox33.Text.Trim();
            EEPISIOTOMIA = textBox34.Text.Trim();
            EVVAGINOTOMIA = textBox35.Text.Trim();
            EVDESGARROS = textBox36.Text.Trim();
            EVLEUCEMIA = textBox37.Text.Trim();
            ECDESGARROS = textBox38.Text.Trim();
            ECEROSIONES = textBox39.Text.Trim();
            EUTERO = textBox40.Text.Trim();
            EANEXOS = textBox41.Text.Trim();
            EEPEDIDOS = textBox42.Text.Trim();
            EPAPANICOLAOU = textBox43.Text.Trim();
            ETRATAMIENTO = textBox44.Text.Trim();
            EMETODO = textBox45.Text.Trim();

            CONSECUTIVO = int.Parse(label71.Text);
        }
        public string CVPACIENTE="";
        public string TALLAHIS = "";
        public string CERVIX= "";
        public string CONSITENCIA = "";
        public string DILATACION = "";
        public string BORRAMIENTO = "";
        public string PELVIS = "";
        public string ALTURAPRES = "";
        public string SALIDALIQAMI = "";
        public string HCARACTERISTICA= "";
        public string HPRESENTACION= "";
        public string HIDX = "";
        public string HTACTOVAGINAL = "";
        public string HANTHERE = "";
        public string HPERSONALPAT = "";
        public string HPADECACTUAL = "";

        public string HULTRAPREV = "";
        public string HLABPREV = "";
        public string HSITUACION= "";

        public void recolectaHistorial()
        {
            HFECHA = dateTimePicker2.Text;
            NoExpediente = textBox1.Text.Trim();
            CVPACIENTE = textBox8.Text.Trim();
            HEDAD = textBox9.Text.Trim();
            HPESO = textBox10.Text.Trim();
            TALLAHIS = textBox52.Text.Trim();
            HTA = textBox11.Text.Trim();
            HFOCO = textBox14.Text.Trim();
            CERVIX = comboBox1.Text.Trim();
            CONSITENCIA = comboBox2.Text.Trim();
            DILATACION = comboBox3.Text.Trim();
            BORRAMIENTO = comboBox4.Text.Trim();
            PELVIS = comboBox7.Text.Trim();
            ALTURAPRES = comboBox5.Text.Trim();

            SALIDALIQAMI = comboBox6.Text.Trim();
            HUTERO = textBox12.Text.Trim();
            HPRODUCTO = textBox13.Text.Trim();
            HEDEMA = textBox15.Text.Trim();
            HTRATAMIENTO = textBox16.Text.Trim();

            HCARACTERISTICA = textBox53.Text.Trim();
            HPRESENTACION= textBox49.Text.Trim();
            HIDX= textBox16.Text.Trim();
            HTACTOVAGINAL = textBox15.Text.Trim();
            HANTHERE = textBox54.Text.Trim();
            HPERSONALPAT = textBox55.Text.Trim();
            HPADECACTUAL = textBox56.Text.Trim();
            HULTRAPREV = textBox57.Text.Trim();
            HLABPREV = textBox58.Text.Trim();

            HOBSERVACIONES = textBox17.Text.Trim();
            CONSECUTIVO = int.Parse(label71.Text);

        }
        public void recolectaParto() {
            if (radioButton1.Checked)
            {
                PARTO = "0";
                PARTOCAUSA1 = "";
                PARTOCAUSA2 = "";
            }
            else if (radioButton2.Checked)
            {
                PARTO = "1";
                PARTOCAUSA1 = "";
                PARTOCAUSA2 = "";
            }
            else if (radioButton3.Checked)
            {
                PARTO = "2";
                PARTOCAUSA1 = "";
                PARTOCAUSA2 = "";
            }
            else if (radioButton4.Checked)
            {
                PARTO = "3";
                PARTOCAUSA1 = "";
                PARTOCAUSA2 = "";
            }
            else if (radioButton5.Checked)
            {
                PARTO = "4";
                PARTOCAUSA1 = "";
                PARTOCAUSA2 = "";
            }
            else if (radioButton6.Checked)
            {
                PARTO = "5";
                PARTOCAUSA1 = textBox18.Text.Trim();
                PARTOCAUSA2 = textBox19.Text.Trim();
            }
            else if (radioButton7.Checked)
            {
                PARTO = "6";
                PARTOCAUSA1 = textBox20.Text.Trim();
                PARTOCAUSA2 = textBox21.Text.Trim();
            }
        }
        public string recolectaRadioOpciones(RadioButton rdb1, RadioButton rdb2, RadioButton rdb3) {
            if (rdb1.Checked)
                return "0";
            else if (rdb2.Checked)
                return "1";
            else if (rdb3.Checked)
                return "2";
            else return "";
        }
        public string recolectaRadioOpciones(RadioButton rdb1, RadioButton rdb2)        {
            if (rdb1.Checked)
                return "0";
            else if (rdb2.Checked)
                return "1";
            else return "";
        }
        public string recolectaRadioOpciones(RadioButton rdb1)
        {
            if (rdb1.Checked)
                return "1";
            else return "0";
        }
        public string recolectaGenero(RadioButton rdb1, RadioButton rdb2)
        {
            if (rdb1.Checked)
                return "M";
            else if (rdb2.Checked)
                return "F";
            else return "";
        }
        public string recolectaDosOpciones(RadioButton rdb1, RadioButton rdb2)
        {
            if (rdb1.Checked)
            {
                PROSITIPO = "";
                return "0";
            }
            else if (rdb2.Checked)
            {
                PROSITIPO = textBox24.Text.Trim();
                return "1";
            }
            else { PROSITIPO = ""; return ""; }
        }
        public string recolectaPuerperio(RadioButton rdb1, RadioButton rdb2)
        {
            if (rdb1.Checked)
            {
                RPPCAUSA = "";
                return "0";
            }
            else if (rdb2.Checked)
            {
                RPPCAUSA = textBox29.Text.Trim();
                return "1";
            }
            else { RPPCAUSA = ""; return ""; }
        }
        public void recuperaInfoParto(string opc, string val1, string val2) {
            if (opc.Equals("0"))
            {
                radioButton1.Checked = true;
            }
            else if (opc.Equals("1"))
            {
                radioButton2.Checked = true;
            }
            else if (opc.Equals("2"))
            {
                radioButton3.Checked = true;
            }
            else if (opc.Equals("3"))
            {
                radioButton4.Checked = true;
            }
            else if (opc.Equals("4"))
            {
                radioButton5.Checked = true;
                textBox18.Text = val1;
                textBox19.Text = val2;
            }
            else if (opc.Equals("5"))
            {
                radioButton6.Checked = true;
                textBox20.Text = val1;
                textBox21.Text = val2;
            }
        }
        public void recuperRadioOpciones(string opc, RadioButton rdb1, RadioButton rdb2, RadioButton rdb3) {
            if (opc.Equals("0"))
            {
                rdb1.Checked = true;
                rdb2.Checked = false;
                rdb3.Checked = false;
            }
            else if (opc.Equals("1"))
            {
                rdb1.Checked = false;
                rdb2.Checked = true;
                rdb3.Checked = false;
            }
            else if (opc.Equals("2"))
            {
                rdb1.Checked = false;
                rdb2.Checked = false;
                rdb3.Checked = true;
            }
            else {
                rdb1.Checked = false;
                rdb2.Checked = false;
                rdb3.Checked = false;
            }
        }
        public void recuperRadioOpciones(string opc, RadioButton rdb1, RadioButton rdb2)
        {
            if (opc.Equals("0"))
            {
                rdb1.Checked = true;
                rdb2.Checked = false;
            }
            else if (opc.Equals("1"))
            {
                rdb1.Checked = false;
                rdb2.Checked = true;
            }
            else
            {
                rdb1.Checked = false;
                rdb2.Checked = false;
            }
        }
        public void recuperRadioOpciones(string opc, RadioButton rdb1)
        {
            if (opc.Equals("1"))
            {
                rdb1.Checked = true;
            }
            else if (opc.Equals("0"))
            {
                rdb1.Checked = false;
            }
            else
            {
                rdb1.Checked = false;
            }
        }
        public void asignaGenero(string genero,RadioButton rdb1, RadioButton rdb2)
        {
            if (genero.Equals("M")){
                rdb1.Checked = true;
                rdb2.Checked = false;
            }

            else if (genero.Equals("F"))
            {
                rdb2.Checked = true;
                rdb1.Checked = false;
            }
            else {
                rdb1.Checked = false;
                rdb2.Checked = false;
            }
                
        }
        public bool validacion() {
            return true;
        }
        public bool validaCamposHistorial() {
            return true;
        }
        public void guardar() {
            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "insert into CPrenatal(";
            Query += "NoExpediente,";
            Query += "FECHA,";
            Query += "FCOD,";
            Query += "GSANGUINEO,";
            Query += "FRR,";
            Query += "GESTA,";
            Query += "PARA,";
            Query += "ABORTOS,";
            Query += "CESAREAS,";
            Query += "OBSERVACIONES1,";
            Query += "PFECHA,";
            Query += "PFCOD,";
            Query += "PHORA,";
            Query += "PARTO,";
            Query += "PARTOCAUSA1,";
            Query += "PARTOCAUSA2,";
            Query += "PALUMBRAMIENTO,";
            Query += "PEPISTOTOMIA,";
            Query += "PDESGARROS,";
            Query += "PRCAVIDAD,";
            Query += "PDURACION,";
            Query += "PROSEXO,";
            Query += "PROPESO,";
            Query += "PROMIN,";
            Query += "PROANORMALIDADES,";
            Query += "PROSITIPO,";
            Query += "PROTIPO,";
            Query += "AAGRUPO1,";
            Query += "AARESULTADO1,";
            Query += "AAGRUPO2,";
            Query += "AARESULTADO2,";
            Query += "AAOTROS,";
            Query += "AAPSICO,";
            Query += "AARESULTADO,";
            Query += "AAPBSERVACIONES,";
            Query += "AAPRESENTE,";
            Query += "RPPFECHA,";
            Query += "RPPPESO,";
            Query += "RPPTA,";
            Query += "RPPLACTANCIA,";
            Query += "RPPUERPERIO,";
            Query += "RPPCAUSA,";
            Query += "RPPOTROS,";
            Query += "EABDOMEN,";
            Query += "EGMAMARIAS,";
            Query += "EVULVA,";
            Query += "EEPISIOTOMIA,";
            Query += "EVVAGINOTOMIA,";
            Query += "EVDESGARROS,";
            Query += "EVLEUCEMIA,";
            Query += "ECDESGARROS,";
            Query += "ECEROSIONES,";
            Query += "EUTERO,";
            Query += "EANEXOS,";
            Query += "EEPEDIDOS,";
            Query += "EPAPANICOLAOU,";
            Query += "ETRATAMIENTO,";
            Query += "EMETODO,";
            Query += "consecutivo) ";
            Query += "values(";
            Query += "'" + NoExpediente + "',";
            Query += "'" + FECHA + "',";
            Query += "'" + FCOD + "',";
            Query += "'" + GSANGUINEO + "',";
            Query += "'" + FRR + "',";
            Query += "'" + GESTA + "',";
            Query += "'" + PARA + "',";
            Query += "'" + ABORTOS + "',";
            Query += "'" + CESAREAS + "',";
            Query += "'" + OBSERVACIONES1 + "',";
            Query += "'" + PFECHA + "',";
            Query += "'" + PFCOD + "',";
            Query += "'" + PHORA + "',";
            Query += "'" + PARTO + "',";
            Query += "'" + PARTOCAUSA1 + "',";
            Query += "'" + PARTOCAUSA2 + "',";
            Query += "'" + PALUMBRAMIENTO + "',";
            Query += "'" + PEPISTOTOMIA + "',";
            Query += "'" + PDESGARROS + "',";
            Query += "'" + PRCAVIDAD + "',";
            Query += "'" + PDURACION + "',";
            Query += "'" + PROSEXO + "',";
            Query += "'" + PROPESO + "',";
            Query += "'" + PROMIN + "',";
            Query += "'" + PROANORMALIDADES + "',";
            Query += "'" + PROSITIPO + "',";
            Query += "'" + PROTIPO + "',";
            Query += "'" + AAGRUPO1 + "',";
            Query += "'" + AARESULTADO1 + "',";
            Query += "'" + AAGRUPO2 + "',";
            Query += "'" + AARESULTADO2 + "',";
            Query += "'" + AAOTROS + "',";
            Query += "'" + AAPSICO + "',";
            Query += "'" + AARESULTADO + "',";
            Query += "'" + AAPBSERVACIONES + "',";
            Query += "'" + AAPRESENTE + "',";
            Query += "'" + RPPFECHA + "',";
            Query += "'" + RPPPESO + "',";
            Query += "'" + RPPTA + "',";
            Query += "'" + RPPLACTANCIA + "',";
            Query += "'" + RPPUERPERIO + "',";
            Query += "'" + RPPCAUSA + "',";
            Query += "'" + RPPOTROS + "',";
            Query += "'" + EABDOMEN + "',";
            Query += "'" + EGMAMARIAS + "',";
            Query += "'" + EVULVA + "',";
            Query += "'" + EEPISIOTOMIA + "',";
            Query += "'" + EVVAGINOTOMIA + "',";
            Query += "'" + EVDESGARROS + "',";
            Query += "'" + EVLEUCEMIA + "',";
            Query += "'" + ECDESGARROS + "',";
            Query += "'" + ECEROSIONES + "',";
            Query += "'" + EUTERO + "',";
            Query += "'" + EANEXOS + "',";
            Query += "'" + EEPEDIDOS + "',";
            Query += "'" + EPAPANICOLAOU + "',";
            Query += "'" + ETRATAMIENTO + "',";
            Query += "'" + EMETODO + "',";
            Query += "'" + CONSECUTIVO + "')";
            conecta.Excute(Query);
        }
        public void guardaHistorial()
        {
            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "insert into CPrenatalH(";
            Query += "NoExpediente,";
            Query += "HFECHA,";
            Query += "HEDAD,";
            Query += "HPESO,";
            Query += "HTA,";
            Query += "HUTERO,";
            Query += "HPRODUCTO,";
            Query += "HFOCO,";
            Query += "HEDEMA,";
            Query += "HTRATAMIENTO,";
            Query += "HOBSERVACIONES,";
            Query += "consecutivo) ";
            Query += "values(";
            Query += "'" + NoExpediente + "',";
            Query += "'" + HFECHA + "',";
            Query += "'" + HEDAD + "',";
            Query += "'" + HPESO + "',";
            Query += "'" + HTA + "',";
            Query += "'" + HUTERO + "',";
            Query += "'" + HPRODUCTO + "',";
            Query += "'" + HFOCO + "',";
            Query += "'" + HEDEMA + "',";
            Query += "'" + HTRATAMIENTO + "',";
            Query += "'" + HOBSERVACIONES + "',";
            Query += "'" + CONSECUTIVO + "')";
            conecta.Excute(Query);
        }
        public bool existeRegistro(string clave) {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from CPrenatal where NoExpediente='" + clave + "'";
            return conecta.ExisteRegistro(Query);
        }
        public bool existeItemHistorial(string clave, string fecha) {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from CPrenatalH where NoExpediente='" + clave + "' and HFECHA ='" + HFECHA + "'";
            return conecta.ExisteRegistro(Query);
        }
        public void eliminaRegistro(string clave) {
            conectorSql conecta = new conectorSql();
            string Query = "DELETE FROM CPrenatal where NoExpediente='" + clave + "'";
            conecta.Excute(Query);
        }
        public void eliminaItemHistorial(string clave, string fecha) {
            conectorSql conecta = new conectorSql();
            string Query = "DELETE FROM CPrenatalH where NoExpediente='" + clave + "' and HFECHA ='" + HFECHA + "'";
            conecta.Excute(Query);
        }
        public void recuperaInformacionPaciente(string clave) {
            limpiarLabels();
            conectorSql conecta = new conectorSql();
            string Query = "SELECT NOMBRE,APATERNO,AMATERNO,EDAD FROM  Pacientes WHERE NoExpediente='" + clave + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                label68.Text = leer["NOMBRE"].ToString() + " " + leer["APATERNO"].ToString() + " " + leer["AMATERNO"].ToString();
                label69.Text = leer["EDAD"].ToString();
            }
            conecta.CierraConexion();
        }
        public void recuperaInformacionExpediente(string clave) {
            limpiarControles(this);
            conectorSql conecta = new conectorSql();
            string valorRdb = "";
            string valor1 = "";
            string valor2 = "";
            int tempConsecutivo = 0;
            string Query = "SELECT  * FROM  CPrenatal WHERE NoExpediente='" + clave + "'";
            Query += "and consecutivo= (SELECT max(consecutivo) from CPrenatal where NoExpediente='"+clave+"')";
            SqlDataReader leer = conecta.RecordInfo(Query);
            textBox1.Text = clave;
            while (leer.Read())
            {
                dateTimePicker1.Text = leer["FECHA"].ToString();
                textBox2.Text = leer["GSANGUINEO"].ToString();
                textBox3.Text = leer["FRR"].ToString();
                textBox4.Text = leer["GESTA"].ToString();
                textBox5.Text = leer["PARA"].ToString();
                textBox6.Text = leer["ABORTOS"].ToString();
                textBox7.Text = leer["CESAREAS"].ToString();
                textBox8.Text = leer["OBSERVACIONES1"].ToString();
                dateTimePicker3.Text = leer["PFECHA"].ToString();
                dateTimePicker4.Text = leer["PHORA"].ToString();
                //
                valorRdb = leer["PARTO"].ToString();
                valor1 = leer["PARTOCAUSA1"].ToString();
                valor2 = leer["PARTOCAUSA2"].ToString();
                recuperaInfoParto(valorRdb,valor1,valor2);

                valorRdb = leer["PALUMBRAMIENTO"].ToString();
                recuperRadioOpciones(valorRdb, radioButton7, radioButton8, radioButton9);
                valorRdb = leer["PEPISTOTOMIA"].ToString();
                recuperRadioOpciones(valorRdb, radioButton12, radioButton11, radioButton10);
                valorRdb = leer["PDESGARROS"].ToString();
                recuperRadioOpciones(valorRdb, radioButton15, radioButton14, radioButton13);
                valorRdb = leer["PRCAVIDAD"].ToString();
                recuperRadioOpciones(valorRdb, radioButton18, radioButton17);
                valorRdb = leer["PDURACION"].ToString();
                recuperRadioOpciones(valorRdb, radioButton21, radioButton20, radioButton19);
                valorRdb = leer["PROSEXO"].ToString();
                asignaGenero(valorRdb, radioButton16, radioButton22);
                textBox22.Text = leer["PROPESO"].ToString();
                textBox23.Text = leer["PROMIN"].ToString();
                valorRdb = leer["PROANORMALIDADES"].ToString();
                recuperRadioOpciones(valorRdb,radioButton23);
                textBox24.Text = leer["PROSITIPO"].ToString();
                valorRdb = leer["PROTIPO"].ToString();
                recuperRadioOpciones(valorRdb, radioButton25, radioButton24);
                valorRdb = leer["AAGRUPO1"].ToString();
                recuperRadioOpciones(valorRdb, radioButton27, radioButton26, radioButton28);
                textBox25.Text = leer["AARESULTADO1"].ToString();
                valorRdb = leer["AAGRUPO2"].ToString();
                recuperRadioOpciones(valorRdb, radioButton31, radioButton30, radioButton29);
                textBox26.Text = leer["AARESULTADO2"].ToString();
                valorRdb = leer["AAOTROS"].ToString();
                valorRdb = leer["AAPSICO"].ToString();
                recuperRadioOpciones(valorRdb, radioButton34);
                textBox27.Text = leer["AARESULTADO"].ToString();
                textBox28.Text = leer["AAPBSERVACIONES"].ToString();
                valorRdb = leer["AAPRESENTE"].ToString();
                recuperRadioOpciones(valorRdb, radioButton36, radioButton35);

                dateTimePicker6.Text = leer["RPPFECHA"].ToString();
                textBox46.Text = leer["RPPPESO"].ToString();
                textBox47.Text = leer["RPPTA"].ToString();
                textBox48.Text = leer["RPPLACTANCIA"].ToString();
                valorRdb = leer["RPPUERPERIO"].ToString();
                recuperRadioOpciones(valorRdb, radioButton38, radioButton37);
                textBox29.Text = leer["RPPCAUSA"].ToString();
                textBox30.Text = leer["RPPOTROS"].ToString();
                textBox31.Text = leer["EABDOMEN"].ToString();
                textBox32.Text = leer["EGMAMARIAS"].ToString();
                textBox33.Text = leer["EVULVA"].ToString();
                textBox34.Text = leer["EEPISIOTOMIA"].ToString();
                textBox35.Text = leer["EVVAGINOTOMIA"].ToString();
                textBox36.Text = leer["EVDESGARROS"].ToString();
                textBox37.Text = leer["EVLEUCEMIA"].ToString();
                textBox38.Text = leer["ECDESGARROS"].ToString();
                textBox39.Text = leer["ECEROSIONES"].ToString();
                textBox40.Text = leer["EUTERO"].ToString();
                textBox41.Text = leer["EANEXOS"].ToString();
                textBox42.Text = leer["EEPEDIDOS"].ToString();
                textBox43.Text = leer["EPAPANICOLAOU"].ToString();
                textBox44.Text = leer["ETRATAMIENTO"].ToString();
                textBox45.Text = leer["EMETODO"].ToString();
                tempConsecutivo = int.Parse(leer["consecutivo"].ToString());
            }
            conecta.CierraConexion();

            recargaLv(clave, tempConsecutivo);

        }
        
        public void recuperaInformacionHistorial(string clave, int _consecutivo) {
            conectorSql conecta = new conectorSql();
            string Query = "SELECT  * FROM  CPrenatalH WHERE NoExpediente='" + clave + "' ";
            Query += "and consecutivo= " + _consecutivo + "";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                dateTimePicker2.Text = leer["HFECHA"].ToString();
                textBox9.Text = leer["HEDAD"].ToString();
                textBox10.Text = leer["HPESO"].ToString();
                textBox11.Text = leer["HTA"].ToString();
                textBox12.Text = leer["HUTERO"].ToString();
                textBox13.Text = leer["HPRODUCTO"].ToString();
                textBox14.Text = leer["HFOCO"].ToString();
                textBox15.Text = leer["HEDEMA"].ToString();
                textBox16.Text = leer["HTRATAMIENTO"].ToString();
                textBox17.Text = leer["HOBSERVACIONES"].ToString();
            }
            conecta.CierraConexion();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            limpiarControles(tabPage1);
            textBox9.Focus();
        }
        
        public void limpiarControles(Control cnt) {
            foreach (Control c in cnt.Controls)
            {
                if (c is Panel)
                {
                    limpiarControles(c);
                }
                else if (c is TabControl)
                {
                    limpiarControles(c);
                }
                else if (c is TabPage)
                {
                    limpiarControles(c);
                }
                else if (c is TextBox)
                {
                    c.Text = "";
                }
                else if (c is DateTimePicker)
                {
                    c.Refresh();
                }
                else if (c is RadioButton)
                {
                    RadioButton rdb = new RadioButton();
                    rdb =(RadioButton) c;
                    rdb.Checked = false;
                }
            }
            cargaConsecutivo();
        }
        public void limpiarLabels() {
            label68.Text = "_____";
            label69.Text = "_____";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            recolectaHistorial();
            if (validaCamposHistorial())
            {
                if (existeItemHistorial(NoExpediente,HFECHA))
                {
                    eliminaItemHistorial(NoExpediente, HFECHA);
                }
                guardaHistorial();
                MessageBox.Show("Se guardo correctamente la información", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarControles(tabPage1);
                textBox9.Focus();
                int tempConsecutivo=int.Parse(label71.Text);
                recargaLv(textBox1.Text.Trim(), tempConsecutivo);
            }
        }

        public void recargaLv(string clave, int _consecutivo)
        {
            Lv.Items.Clear();
            conectorSql conecta = new conectorSql();
            string Query = "SELECT  * FROM  CPrenatalH WHERE NoExpediente='" + clave + "'";
            Query += "and consecutivo= " + _consecutivo + "";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string fecha = leer["HFECHA"].ToString();
                ListViewItem lvi = new ListViewItem(fecha);
                lvi.SubItems.Add(leer["HEDAD"].ToString());
                lvi.SubItems.Add(leer["HPESO"].ToString());
                lvi.SubItems.Add(leer["HTA"].ToString());
                lvi.SubItems.Add(leer["HUTERO"].ToString());
                lvi.SubItems.Add(leer["HPRODUCTO"].ToString());
                lvi.SubItems.Add(leer["HFOCO"].ToString());
                lvi.SubItems.Add(leer["HEDEMA"].ToString());
                lvi.SubItems.Add(leer["HTRATAMIENTO"].ToString());
                lvi.SubItems.Add(leer["HOBSERVACIONES"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            recolecta();
            if (validacion())
            {
                //if (existeRegistro(NoExpediente))
                //{
                //    eliminaRegistro(NoExpediente);
                //}
                guardar();
                MessageBox.Show("Se guardo correctamente la información", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                actualizaConsecutivo();
                limpiarControles(this);
                textBox1.Focus();
            }
        }

        private void ControlPrenatal_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            tabPage3.Show();
            tabPage2.Show();
            tabPage1.Show();
            cargaConsecutivo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                HistorialClinica.BuscarPaciente bPaciente = new HistorialClinica.BuscarPaciente();
                bPaciente.Show();
            }
            else {
                recuperaInformacionPaciente(textBox1.Text.Trim());
                recuperaInformacionExpediente(textBox1.Text.Trim());
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void ControlPrenatal_Activated(object sender, EventArgs e)
        {
            if (valoresg.EXPEDIENTE != "")
            {
                textBox1.Text = valoresg.EXPEDIENTE;
                recuperaInformacionPaciente(textBox1.Text.Trim());
                recuperaInformacionExpediente(textBox1.Text.Trim());
                valoresg.EXPEDIENTE = "";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        public void cargaConsecutivo()
        {
            string Numero = "1";
            conectorSql conecta = new conectorSql();
            string Query = "Select prenatal from consecutivos where prenatal <>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                Numero = leer["prenatal"].ToString();
            }
            conecta.CierraConexion();
            label71.Text = Numero;
        }

        public bool actualizaConsecutivo()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = int.Parse(label71.Text) + 1;
            string Query = "update consecutivos set prenatal='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            limpiarControles(this);
            limpiarLabels();
        }
    }
}
