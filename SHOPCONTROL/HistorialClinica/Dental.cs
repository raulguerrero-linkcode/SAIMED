using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace SHOPCONTROL.HistorialClinica
{
    public partial class Dental : Form
    {
        public Dental()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private Image odontogramaDefecto;
        string NoExpediente = "";
        string ODONTOLOGO="";
        string FECHA = "";
        string FCOD = "";
        string ANTFAM = "";
        string MOTIVO = "";
        string DOLOR = "";
        string CONTROL = "";
        string ENCIAS = "";
        string PROTESICA = "";
        string OTRO = "";
        string EACTUAL = "";

        string P1 = "";
        string P2 = "";
        string P3 = "";
        string P4 = "";
        string P5 = "";
        string P6 = "";
        string P7 = "";
        string P8 = "";
        string P9 = "";
        string P10 = "";
        string P11 = "";
        string P12 = "";
        string P13 = "";
        string P14 = "";
        string P15 = "";
        string P16 = "";
        string P17 = "";
        string P18 = "";
        string P19 = "";
        string P20 = "";

        string P21 = "";
        string P22 = "";
        string P23 = "";
        string P24 = "";
        string P25 = "";
        string P26 = "";
        string P27 = "";
        string P28 = "";
        string P29 = "";
        string P30 = "";
        string P31 = "";
        string P32 = "";
        string P33 = "";
        string P34 = "";
        string P35 = "";
        string P36 = "";
        string P37 = "";
        string P38 = "";
        string P39 = "";
        string P40 = "";

        string ASPECTO = "";
        string CARA = "";
        string LABIOS = "";
        string PALPACIONG = "";
        string GANGLIOS = "";
        string ATM = "";
        string OREJAS = "";
        string REGIONHT = "";
        string CARRILLOS = "";
        string MUCOSA = "";
        string ENCIA = "";
        string LENGUA = "";
        string PALADAR = "";
        string LABORATORIO = "";
        string MODELO = "";
        string TENSIONART = "";
        string OBSERVACIONES = "";
        string DERIVACIONES = "";

        string derivPeriodoncia = "";
        string derivEndodoncia = "";
        string derivCirugia = "";
        string derivEstomatologia = "";
        string derivRadiologia = "";
        string derivOtros = "";

        string DIAGNOSTICO = "";
        string SECFECHA1 = "";
        string SECDESCRIPCION1 = "";
        string SECFECHA2 = "";
        string SECDESCRIPCION2 = "";
        string SECFECHA3 = "";
        string SECDESCRIPCION3 = "";
        string SECFECHA4 = "";
        string SECDESCRIPCION4 = "";
        string SECFECHA5 = "";
        string SECDESCRIPCION5 = "";
        string SECFECHA6 = "";
        string SECDESCRIPCION6 = "";

        int CONSECUTIVO = 0;
        public bool EDITAR = false;

        string[] derivaciones = new string[6];

        public void recolectar() {
            NoExpediente = textBox1.Text.Trim();
            ODONTOLOGO = textBox2.Text.Trim();
            FECHA = dateTimePicker1.Value.ToString("dd/MM/yyy");
            FCOD = dateTimePicker1.Value.ToString("yyyMMdd");
            ANTFAM = textBox3.Text.Trim();
            MOTIVO = textBox4.Text.Trim();
            DOLOR = textBox5.Text.Trim();
            CONTROL = textBox6.Text.Trim();
            ENCIAS = textBox7.Text.Trim();
            PROTESICA = textBox8.Text.Trim();
            OTRO = textBox9.Text.Trim();
            EACTUAL = textBox10.Text.Trim();

            P1 = radioChecked(radioButton1, radioButton2);
            P2 = radioChecked(radioButton4, radioButton3);
            P3 = radioChecked(radioButton6, radioButton5);
            P4 = radioChecked(radioButton8, radioButton7);
            P5 = radioChecked(radioButton10, radioButton9);
            P6 = radioChecked(radioButton12, radioButton11);
            P7 = radioChecked(radioButton14, radioButton13);
            P8 = radioChecked(radioButton16, radioButton15);
            P9 = radioChecked(radioButton18, radioButton17);
            P10 = radioChecked(radioButton20, radioButton19);
            P11 = radioChecked(radioButton22, radioButton21);
            P12 = radioChecked(radioButton24, radioButton23);
            P13 = radioChecked(radioButton26, radioButton25);
            P14 = radioChecked(radioButton28, radioButton27);
            P15 = radioChecked(radioButton30, radioButton29);
            P16 = radioChecked(radioButton32, radioButton31);
            P17 = radioChecked(radioButton66, radioButton65);
            P18 = radioChecked(radioButton68, radioButton67);
            P19 = radioChecked(radioButton70, radioButton69);
            P20 = radioChecked(radioButton72, radioButton71);
            P21 = radioChecked(radioButton34, radioButton33);
            P22 = radioChecked(radioButton36, radioButton35);
            P23 = radioChecked(radioButton38, radioButton37);
            P24 = radioChecked(radioButton40, radioButton39);
            P25 = radioChecked(radioButton42, radioButton41);
            P26 = radioChecked(radioButton44, radioButton43);
            P27 = radioChecked(radioButton46, radioButton45);
            P28 = radioChecked(radioButton48, radioButton47);
            P29 = radioChecked(radioButton50, radioButton49);
            P30 = radioChecked(radioButton52, radioButton51);
            P31 = radioChecked(radioButton54, radioButton53);
            P32 = radioChecked(radioButton56, radioButton55);
            P33 = radioChecked(radioButton80, radioButton79);
            P34 = radioChecked(radioButton58, radioButton57);
            P35 = radioChecked(radioButton60, radioButton59);
            P36 = radioChecked(radioButton62, radioButton61);
            P37 = radioChecked(radioButton64, radioButton63);
            P38 = radioChecked(radioButton74, radioButton73);
            P39 = radioChecked(radioButton76, radioButton75);
            P40 = radioChecked(radioButton78, radioButton77);

            ASPECTO = textBox11.Text.Trim();
            CARA = textBox12.Text.Trim();
            LABIOS = textBox13.Text.Trim();
            PALPACIONG = textBox14.Text.Trim();
            GANGLIOS = textBox15.Text.Trim();
            ATM = textBox16.Text.Trim();
            OREJAS = textBox17.Text.Trim();
            REGIONHT = textBox18.Text.Trim();
            CARRILLOS = textBox19.Text.Trim();
            MUCOSA = textBox20.Text.Trim();
            ENCIA = textBox21.Text.Trim();
            LENGUA = textBox22.Text.Trim();
            PALADAR = textBox23.Text.Trim();
            LABORATORIO = textBox24.Text.Trim();
            MODELO = textBox25.Text.Trim();
            TENSIONART = textBox26.Text.Trim();
            OBSERVACIONES = textBox27.Text.Trim();
            DERIVACIONES = RecolectaCheckBoxList(checkedListBox1);

            derivPeriodoncia = derivaciones[0];
            derivEndodoncia = derivaciones[1];
            derivCirugia = derivaciones[2];
            derivEstomatologia = derivaciones[3];
            derivRadiologia = derivaciones[4];
            derivOtros = derivaciones[5];

            DIAGNOSTICO = textBox28.Text.Trim();
            SECFECHA1 = dateTimePicker2.Value.ToString("dd/MM/yyyy");
            SECDESCRIPCION1 = textBox29.Text.Trim();
            SECFECHA2 = dateTimePicker3.Value.ToString("dd/MM/yyyy");
            SECDESCRIPCION2 = textBox30.Text.Trim();
            SECFECHA3 = dateTimePicker4.Value.ToString("dd/MM/yyyy");
            SECDESCRIPCION3 = textBox31.Text.Trim();
            SECFECHA4 = dateTimePicker5.Value.ToString("dd/MM/yyyy");
            SECDESCRIPCION4 = textBox32.Text.Trim();
            SECFECHA5 = dateTimePicker6.Value.ToString("dd/MM/yyyy");
            SECDESCRIPCION5 = textBox33.Text.Trim();
            SECFECHA6 = dateTimePicker7.Value.ToString("dd/MM/yyyy");
            SECDESCRIPCION6 = textBox34.Text.Trim();

            CONSECUTIVO = int.Parse(label114.Text);

        }

        public string radioChecked(RadioButton rdb1, RadioButton rdb2)
        {
            if (rdb1.Checked)
            {
                return "SI";
            }
            else if (rdb2.Checked)
            {
                return "NO";
            }
            else {
                return "";
            }
        }

        public string RecolectaCheckBoxList(CheckedListBox CLB)
        {
            string valor = "";
            if (CLB.CheckedItems.Count != 0)
            {
                foreach (int indice in CLB.CheckedIndices)
                {
                    derivaciones[indice] = "x";
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

        public bool validacion() {
            if (ODONTOLOGO.Equals(""))
            {
                MessageBox.Show("Ingrese odontologo tratante ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return false;
            }
            if (ANTFAM.Equals(""))
            {
                MessageBox.Show("Ingrese antecedentes familiares ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return false;
            }
            if (MOTIVO.Equals(""))
            {
                MessageBox.Show("Ingrese motivo de consulta ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
                return false;
            }
            if (EACTUAL.Equals(""))
            {
                MessageBox.Show("Ingrese enfermedad actual", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox10.Focus();
                return false;
            }
            return true;
        }

        public void guardar() {
            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "insert into EDental(";
            Query += "NoExpediente,";
            Query += "ODONTOLOGO,";
            Query += "FECHA,";
            Query += "FCOD,";
            Query += "ANTFAM,";
            Query += "MOTIVO,";
            Query += "DOLOR,";
            Query += "CONTROL,";
            Query += "ENCIAS,";
            Query += "PROTESICA,";
            Query += "OTRO,";
            Query += "EACTUAL,";
            Query += "P1,";
            Query += "P2,";
            Query += "P3,";
            Query += "P4,";
            Query += "P5,";
            Query += "P6,";
            Query += "P7,";
            Query += "P8,";
            Query += "P9,";
            Query += "P10,";
            Query += "P11,";
            Query += "P12,";
            Query += "P13,";
            Query += "P14,";
            Query += "P15,";
            Query += "P16,";
            Query += "P17,";
            Query += "P18,";
            Query += "P19,";
            Query += "P20,";
            Query += "P21,";
            Query += "P22,";
            Query += "P23,";
            Query += "P24,";
            Query += "P25,";
            Query += "P26,";
            Query += "P27,";
            Query += "P28,";
            Query += "P29,";
            Query += "P30,";
            Query += "P31,";
            Query += "P32,";
            Query += "P33,";
            Query += "P34,";
            Query += "P35,";
            Query += "P36,";
            Query += "P37,";
            Query += "P38,";
            Query += "P39,";
            Query += "P40,";
            Query += "ASPECTO,";
            Query += "CARA,";
            Query += "LABIOS,";
            Query += "PALPACIONG,";
            Query += "GANGLIOS,";
            Query += "ATM,";
            Query += "OREJAS,";
            Query += "REGIONHT,";
            Query += "CARRILLOS,";
            Query += "MUCOSA,";
            Query += "ENCIA,";
            Query += "LENGUA,";
            Query += "PALADAR,";
            Query += "LABORATORIO,";
            Query += "MODELO,";
            Query += "TENSIONART,";
            Query += "OBSERVACIONES,";
            Query += "DERIVACIONES,";
            Query += "DIAGNOSTICO,";
            Query += "SECFECHA1,";
            Query += "SECDESCRIPCION1,";
            Query += "SECFECHA2,";
            Query += "SECDESCRIPCION2,";
            Query += "SECFECHA3,";
            Query += "SECDESCRIPCION3,";
            Query += "SECFECHA4,";
            Query += "SECDESCRIPCION4,";
            Query += "SECFECHA5,";
            Query += "SECDESCRIPCION5,";
            Query += "SECFECHA6,";
            Query += "SECDESCRIPCION6,";
            Query += "derivPeriodoncia,";
            Query += "derivEndodoncia,";
            Query += "derivCirugia,";
            Query += "derivEstomatologia,";
            Query += "derivRadiologia,";
            Query += "derivOtros,";
            Query += "consecutivo) ";
            Query += "values(";
            Query += "'" + NoExpediente + "',";
            Query += "'" + ODONTOLOGO + "',";
            Query += "'" + FECHA + "',";
            Query += "'" + FCOD + "',";
            Query += "'" + ANTFAM + "',";
            Query += "'" + MOTIVO + "',";
            Query += "'" + DOLOR + "',";
            Query += "'" + CONTROL + "',";
            Query += "'" + ENCIAS + "',";
            Query += "'" + PROTESICA + "',";
            Query += "'" + OTRO + "',";
            Query += "'" + EACTUAL + "',";
            Query += "'" + P1 + "',";
            Query += "'" + P2 + "',";
            Query += "'" + P3 + "',";
            Query += "'" + P4 + "',";
            Query += "'" + P5 + "',";
            Query += "'" + P6 + "',";
            Query += "'" + P7 + "',";
            Query += "'" + P8 + "',";
            Query += "'" + P9 + "',";
            Query += "'" + P10 + "',";
            Query += "'" + P11 + "',";
            Query += "'" + P12 + "',";
            Query += "'" + P13 + "',";
            Query += "'" + P14 + "',";
            Query += "'" + P15 + "',";
            Query += "'" + P16 + "',";
            Query += "'" + P17 + "',";
            Query += "'" + P18 + "',";
            Query += "'" + P19 + "',";
            Query += "'" + P20 + "',";
            Query += "'" + P21 + "',";
            Query += "'" + P22 + "',";
            Query += "'" + P23 + "',";
            Query += "'" + P24 + "',";
            Query += "'" + P25 + "',";
            Query += "'" + P26 + "',";
            Query += "'" + P27 + "',";
            Query += "'" + P28 + "',";
            Query += "'" + P29 + "',";
            Query += "'" + P30 + "',";
            Query += "'" + P31 + "',";
            Query += "'" + P32 + "',";
            Query += "'" + P33 + "',";
            Query += "'" + P34 + "',";
            Query += "'" + P35 + "',";
            Query += "'" + P36 + "',";
            Query += "'" + P37 + "',";
            Query += "'" + P38 + "',";
            Query += "'" + P39 + "',";
            Query += "'" + P40 + "',";
            Query += "'" + ASPECTO + "',";
            Query += "'" + CARA + "',";
            Query += "'" + LABIOS + "',";
            Query += "'" + PALPACIONG + "',";
            Query += "'" + GANGLIOS + "',";
            Query += "'" + ATM + "',";
            Query += "'" + OREJAS + "',";
            Query += "'" + REGIONHT + "',";
            Query += "'" + CARRILLOS + "',";
            Query += "'" + MUCOSA + "',";
            Query += "'" + ENCIA + "',";
            Query += "'" + LENGUA + "',";
            Query += "'" + PALADAR + "',";
            Query += "'" + LABORATORIO + "',";
            Query += "'" + MODELO + "',";
            Query += "'" + TENSIONART + "',";
            Query += "'" + OBSERVACIONES + "',";
            Query += "'" + DERIVACIONES + "',";
            Query += "'" + DIAGNOSTICO + "',";
            Query += "'" + SECFECHA1 + "',";
            Query += "'" + SECDESCRIPCION1 + "',";
            Query += "'" + SECFECHA2 + "',";
            Query += "'" + SECDESCRIPCION2 + "',";
            Query += "'" + SECFECHA3 + "',";
            Query += "'" + SECDESCRIPCION3 + "',";
            Query += "'" + SECFECHA4 + "',";
            Query += "'" + SECDESCRIPCION4 + "',";
            Query += "'" + SECFECHA5 + "',";
            Query += "'" + SECDESCRIPCION5 + "',";
            Query += "'" + SECFECHA6 + "',";
            Query += "'" + SECDESCRIPCION6 + "',";

            Query += "'" + derivPeriodoncia + "',";
            Query += "'" + derivEndodoncia + "',";
            Query += "'" + derivCirugia + "',";
            Query += "'" + derivEstomatologia + "',";
            Query += "'" + derivRadiologia + "',";
            Query += "'" + derivOtros + "',";
            Query += "'" + CONSECUTIVO + "')";
            conecta.Excute(Query);
        }

        public void buscarInformacion(string clave, int consecutivo) {
            limpiarControles(this);
            conectorSql conecta = new conectorSql();
            string Query = "SELECT  * FROM  EDental WHERE NoExpediente='" + clave + "'";
            Query += " and consecutivo = " + consecutivo + "";
            SqlDataReader leer = conecta.RecordInfo(Query);
            string opcion = "";
            textBox1.Text = clave;
            while (leer.Read())
            {
                textBox2.Text = leer["ODONTOLOGO"].ToString();
                dateTimePicker1.Text = leer["FECHA"].ToString();
                textBox3.Text = leer["ANTFAM"].ToString();
                textBox4.Text = leer["MOTIVO"].ToString();
                textBox5.Text = leer["DOLOR"].ToString();
                textBox6.Text = leer["CONTROL"].ToString();
                textBox7.Text = leer["ENCIAS"].ToString();
                textBox8.Text = leer["PROTESICA"].ToString();
                textBox9.Text = leer["OTRO"].ToString();
                textBox10.Text = leer["EACTUAL"].ToString();
                opcion = leer["P1"].ToString();
                radioCheck(opcion, radioButton1, radioButton2);
                opcion = leer["P2"].ToString();
                radioCheck(opcion, radioButton4, radioButton3);
                opcion = leer["P3"].ToString();
                radioCheck(opcion, radioButton6, radioButton5);
                opcion = leer["P4"].ToString();
                radioCheck(opcion, radioButton8, radioButton7);
                opcion = leer["P5"].ToString();
                radioCheck(opcion, radioButton10, radioButton9);
                opcion = leer["P6"].ToString();
                radioCheck(opcion, radioButton12, radioButton11);
                opcion = leer["P7"].ToString();
                radioCheck(opcion, radioButton14, radioButton13);
                opcion = leer["P8"].ToString();
                radioCheck(opcion, radioButton16, radioButton15);
                opcion = leer["P9"].ToString();
                radioCheck(opcion, radioButton18, radioButton17);
                opcion = leer["P10"].ToString();
                radioCheck(opcion, radioButton20, radioButton19);
                opcion = leer["P11"].ToString();
                radioCheck(opcion, radioButton22, radioButton21);
                opcion = leer["P12"].ToString();
                radioCheck(opcion, radioButton24, radioButton23);
                opcion = leer["P13"].ToString();
                radioCheck(opcion, radioButton26, radioButton25);
                opcion = leer["P14"].ToString();
                radioCheck(opcion, radioButton28, radioButton27);
                opcion = leer["P15"].ToString();
                radioCheck(opcion, radioButton30, radioButton29);
                opcion = leer["P16"].ToString();
                radioCheck(opcion, radioButton32, radioButton31);
                opcion = leer["P17"].ToString();
                radioCheck(opcion, radioButton66, radioButton65);
                opcion = leer["P18"].ToString();
                radioCheck(opcion, radioButton68, radioButton67);
                opcion = leer["P19"].ToString();
                radioCheck(opcion, radioButton70, radioButton69);
                opcion = leer["P20"].ToString();
                radioCheck(opcion, radioButton72, radioButton71);

                opcion = leer["P21"].ToString();
                radioCheck(opcion, radioButton34, radioButton33);
                opcion = leer["P22"].ToString();
                radioCheck(opcion, radioButton36, radioButton35);
                opcion = leer["P23"].ToString();
                radioCheck(opcion, radioButton38, radioButton37);
                opcion = leer["P24"].ToString();
                radioCheck(opcion, radioButton40, radioButton39);
                opcion = leer["P25"].ToString();
                radioCheck(opcion, radioButton42, radioButton41);
                opcion = leer["P26"].ToString();
                radioCheck(opcion, radioButton44, radioButton43);
                opcion = leer["P27"].ToString();
                radioCheck(opcion, radioButton46, radioButton45);
                opcion = leer["P28"].ToString();
                radioCheck(opcion, radioButton48, radioButton47);
                opcion = leer["P29"].ToString();
                radioCheck(opcion, radioButton50, radioButton49);
                opcion = leer["P30"].ToString();
                radioCheck(opcion, radioButton52, radioButton51);
                opcion = leer["P31"].ToString();
                radioCheck(opcion, radioButton54, radioButton53);
                opcion = leer["P32"].ToString();
                radioCheck(opcion, radioButton56, radioButton55);
                opcion = leer["P33"].ToString();
                radioCheck(opcion, radioButton80, radioButton79);
                opcion = leer["P34"].ToString();
                radioCheck(opcion, radioButton58, radioButton57);
                opcion = leer["P35"].ToString();
                radioCheck(opcion, radioButton60, radioButton59);
                opcion = leer["P36"].ToString();
                radioCheck(opcion, radioButton62, radioButton61);
                opcion = leer["P37"].ToString();
                radioCheck(opcion, radioButton64, radioButton63);
                opcion = leer["P38"].ToString();
                radioCheck(opcion, radioButton74, radioButton73);
                opcion = leer["P39"].ToString();
                radioCheck(opcion, radioButton76, radioButton75);
                opcion = leer["P40"].ToString();
                radioCheck(opcion, radioButton78, radioButton77);

                textBox11.Text = leer["ASPECTO"].ToString();
                textBox12.Text = leer["CARA"].ToString();
                textBox13.Text = leer["LABIOS"].ToString();
                textBox14.Text = leer["PALPACIONG"].ToString();
                textBox15.Text = leer["GANGLIOS"].ToString();
                textBox16.Text = leer["ATM"].ToString();
                textBox17.Text = leer["OREJAS"].ToString();
                textBox18.Text = leer["REGIONHT"].ToString();
                textBox19.Text = leer["CARRILLOS"].ToString();
                textBox20.Text = leer["MUCOSA"].ToString();
                textBox21.Text = leer["ENCIA"].ToString();
                textBox22.Text = leer["LENGUA"].ToString();
                textBox23.Text = leer["PALADAR"].ToString();
                textBox24.Text = leer["LABORATORIO"].ToString();
                textBox25.Text = leer["MODELO"].ToString();
                textBox26.Text = leer["TENSIONART"].ToString();
                textBox27.Text = leer["OBSERVACIONES"].ToString();

                DERIVACIONES = leer["DERIVACIONES"].ToString();
                AsignaValorChkList(checkedListBox1, DERIVACIONES);

                textBox28.Text = leer["DIAGNOSTICO"].ToString();
                dateTimePicker2.Text = leer["SECFECHA1"].ToString();
                textBox29.Text = leer["SECDESCRIPCION1"].ToString();
                dateTimePicker3.Text = leer["SECFECHA2"].ToString();
                textBox30.Text = leer["SECDESCRIPCION2"].ToString();
                dateTimePicker4.Text = leer["SECFECHA3"].ToString();
                textBox31.Text = leer["SECDESCRIPCION3"].ToString();
                dateTimePicker5.Text = leer["SECFECHA4"].ToString();
                textBox32.Text = leer["SECDESCRIPCION4"].ToString();
                dateTimePicker6.Text = leer["SECFECHA5"].ToString();
                textBox33.Text = leer["SECDESCRIPCION5"].ToString();
                dateTimePicker7.Text = leer["SECFECHA6"].ToString();
                textBox34.Text = leer["SECDESCRIPCION6"].ToString();

                if (ExisteImagen(clave, odontograma.Name, consecutivo))
                {
                    odontograma.Image = ClaseFotos.ConsultarImagenExpediente(clave, odontograma.Name, consecutivo);
                }
            }
            conecta.CierraConexion();

        }
        
        public void BuscaInformacionPaciente(string clave)
        {
            limpiarLabels();
            conectorSql conecta = new conectorSql();
            string Query = "SELECT NOMBRE,APATERNO,AMATERNO,EDAD,CALLE,NoCalle,GENERO,COLONIA,CP,TELEFONO,OCUPACION,LUGARNAC,FECHANAC FROM  Pacientes WHERE NoExpediente='" + clave + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            string _domicilio = "";
            while (leer.Read())
            {
                label103.Text = leer["NOMBRE"].ToString() + " " + leer["APATERNO"].ToString() + " " + leer["AMATERNO"].ToString();
                label104.Text = leer["EDAD"].ToString();
                _domicilio = "CALLE " + leer["CALLE"].ToString() + " No. " + leer["NoCalle"].ToString() + " COL. " + leer["COLONIA"].ToString();
                label106.Text = _domicilio;
                label107.Text = leer["CP"].ToString();
                label105.Text = leer["GENERO"].ToString();
                label110.Text = leer["TELEFONO"].ToString();
                label112.Text = leer["OCUPACION"].ToString();
                label108.Text = leer["LUGARNAC"].ToString();
                label109.Text = leer["FECHANAC"].ToString();
            }
            conecta.CierraConexion();
        }

        public void radioCheck(string opc, RadioButton rdb1, RadioButton rdb2)
        {
            if (opc.Equals("SI"))
            {
                rdb1.Checked = true;
                rdb2.Checked = false;
            }
            else if (opc.Equals("NO"))
            {
                rdb1.Checked = false;
                rdb2.Checked = true;
            }
            else {
                rdb1.Checked = false;
                rdb2.Checked = false;
            }
        }

        public void limpiarControles(Control cntr) {
            foreach (Control c in cntr.Controls)
            {
                if (c is Panel)
                    limpiarControles((Panel)c);
                if (c is TabControl)
                    limpiarControles((TabControl)c);
                if (c is TabPage)
                    limpiarControles((TabPage)c);
                if (c is TextBox)
                    c.Text = "";
                if (c is DateTimePicker)
                    c.ResetText();
                if (c is ComboBox)
                    c.Text = "";
                 if (c is RadioButton)
                {
                    RadioButton rdb = new RadioButton();
                    rdb = (RadioButton)c;
                    rdb.Checked = false;
                }
                if (c is CheckedListBox)
                {
                    CheckedListBox ckkList = new CheckedListBox();
                    ckkList = (CheckedListBox)c;

                    foreach (int indice in ckkList.CheckedIndices)
                    {
                        ckkList.SetItemChecked(indice, false);
                    }
                }
            }
            odontograma.Image = null;
            odontograma.Image = odontogramaDefecto;
            resetDerivaciones(derivaciones);
            cargaConsecutivo();
        }

        public void limpiarLabels() {
            label103.Text = "_____";
            label104.Text = "_____";
            label105.Text = "_____";
            label106.Text = "_____";
            label107.Text = "_____";
            label108.Text = "_____";
            label109.Text = "_____";
            label110.Text = "_____";
            label111.Text = "_____";
            label112.Text = "_____";
        }

        private void odontograma_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Ingresa el No. de Expediente primero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
            }
            else
            {
                valoresg.BANDIMAGEN = odontograma.Name;
                HistorialClinica.Editor myEditor = new HistorialClinica.Editor();
                myEditor.imagenFondo = odontograma.Image;
                myEditor.imagenNueva = odontogramaDefecto;
                myEditor.NOMBREIMAGEN = odontograma.Name;
                myEditor.NOEXPEDIENTE = textBox1.Text.Trim();
                myEditor.CONSECUTIVO = int.Parse(label114.Text);
                myEditor.Show();
            }
        }

        private void Dental_Activated(object sender, EventArgs e)
        {
            if (valoresg.EXPEDIENTE != "" && valoresg.CONSECUTIVOPACIENTE != 0)
            {
                textBox1.Text = valoresg.EXPEDIENTE;
                buscarInformacion(textBox1.Text.Trim(), valoresg.CONSECUTIVOPACIENTE);
                label114.Text =valoresg.CONSECUTIVOPACIENTE+"";
                activaEditar();
                valoresg.EXPEDIENTE = "";
                valoresg.CONSECUTIVOPACIENTE = 0;
            }
            else if (valoresg.EXPEDIENTE != "")
            {
                textBox1.Text = valoresg.EXPEDIENTE;
                BuscaInformacionPaciente(textBox1.Text.Trim());
                valoresg.EXPEDIENTE = "";
            }

            if (valoresg.BANDIMAGEN == odontograma.Name && ExisteImagen(textBox1.Text, odontograma.Name, int.Parse(label114.Text)))
            {

                odontograma.Image = ClaseFotos.ConsultarImagenExpediente(textBox1.Text.Trim(), odontograma.Name, int.Parse(label114.Text));
                valoresg.BANDIMAGEN = "";
            }
        }
        
        public void eliminaRegistro(string clave, int _consecutivo) {
            conectorSql conecta = new conectorSql();
            string Query = "DELETE FROM EDental where NoExpediente='" + clave + "' AND consecutivo ="+_consecutivo+"";
            conecta.Excute(Query);
        }
        public bool ExisteImagen(string clave, string _nombre, int _consecutivo)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select noExpediente from imagenesclinica where noExpediente='" + clave + "' and nombre='" + _nombre + "' and consecutivo=" + _consecutivo + "";
            return conecta.ExisteRegistro(Query);
        }
        private void Dental_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            tabPage3.Show();
            tabPage2.Show();
            tabPage1.Show();

            odontogramaDefecto = odontograma.Image;
            resetDerivaciones(derivaciones);
            cargaConsecutivo();
        }
        public void resetDerivaciones(string [] deriv) {
            for (int i = 0; i < deriv.Length; i++)
            {
                deriv[i] = "";
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            recolectar();
            if (validacion())
            {
                if (EDITAR)
                {
                    eliminaRegistro(NoExpediente, CONSECUTIVO);
                }
                else {
                    actualizaConsecutivo();
                }
                
                guardar();
                MessageBox.Show("Se guardo correctamente la información", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarLabels();
                limpiarControles(this);
                activaNuevo();
                textBox1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                HistorialClinica.BuscarPaciente bPaciente = new HistorialClinica.BuscarPaciente();
                bPaciente.Show();
            }
            else {
                BuscaInformacionPaciente(textBox1.Text.Trim());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            activaNuevo();
            limpiarLabels();
            limpiarControles(this);
            textBox1.Focus();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Ingrese numero de expediente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
             else if (!ExisteRegistro(textBox1.Text, int.Parse(label114.Text)))
             {
                 MessageBox.Show("Guarde los cambios realizados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
             else
             {
                 ReportDocument cryRpt = new ReportDocument();

                 string CadenaReporte = "C:\\tmp\\reports\\DDental.rpt";

                 DataSet ds = new DataSet();

                 DataSetDentalTableAdapters.PacientesTableAdapter taDental = new DataSetDentalTableAdapters.PacientesTableAdapter();
                 DataSetDental.PacientesDataTable paciente = new DataSetDental.PacientesDataTable();
                 taDental.Fill(paciente, textBox1.Text.Trim());

                 DataSetDentalTableAdapters.EDentalTableAdapter taDental2 = new DataSetDentalTableAdapters.EDentalTableAdapter();
                 DataSetDental.EDentalDataTable dental = new DataSetDental.EDentalDataTable();
                 taDental2.Fill(dental, textBox1.Text.Trim(), int.Parse(label114.Text));

                 DataSetDentalTableAdapters.imagenesclinicaTableAdapter taOdontograma = new DataSetDentalTableAdapters.imagenesclinicaTableAdapter();
                 DataSetDental.imagenesclinicaDataTable imgOdontograma = new DataSetDental.imagenesclinicaDataTable();
                 taOdontograma.Fill(imgOdontograma, textBox1.Text.Trim(), int.Parse(label114.Text));

                 ds.Clear();
                 ds.Tables.Add(paciente);
                 ds.Tables.Add(dental);
                 ds.Tables.Add(imgOdontograma);

                 cryRpt.Load(CadenaReporte);
                 cryRpt.SetDataSource(ds);

                 string NombreArchivo = @"C:\EDental_" + textBox1.Text + "_" + int.Parse(label114.Text) + ".pdf"; ;
                 cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, NombreArchivo);
                 //cryRpt.PrintToPrinter(1, false, 0, 0);
                 cryRpt.Close();
                 cryRpt.Dispose();

                 MessageBox.Show("El archivo ha sido creado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
        }

        public bool ExisteRegistro(string _numExpediente, int _consecutivo)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select NoExpediente from EDental where NoExpediente='" + _numExpediente + "' and consecutivo=" + _consecutivo + "";
            return conecta.ExisteRegistro(Query);
        }

        public void cargaConsecutivo()
        {
            string Numero = "1";
            conectorSql conecta = new conectorSql();
            string Query = "Select dental from consecutivos where dental <>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                Numero = leer["dental"].ToString();
            }
            conecta.CierraConexion();
            label114.Text = Numero;
        }
        public bool actualizaConsecutivo()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = int.Parse(label114.Text) + 1;
            string Query = "update consecutivos set dental='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Ingrese numero de expediente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
            }
            else {
                HistorialClinica.HistorialPaciente myHistorial = new HistorialPaciente();
                myHistorial.tipoEstudio = "Dental";
                myHistorial.noExpediente = textBox1.Text.Trim();
                myHistorial.Show();
            }
        }
        
        public void activaNuevo() {
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) ChecartodoSI();
            checkBox2.Checked = false;
        }

        public void ChecartodoSI()
        {
            radioButton1.Checked = true;
            radioButton4.Checked = true;
            radioButton6.Checked = true;
            radioButton8.Checked = true;
            radioButton10.Checked = true;
            radioButton12.Checked = true;
            radioButton14.Checked = true;
            radioButton16.Checked = true;
            radioButton18.Checked = true;
            radioButton20.Checked = true;
            radioButton22.Checked = true;
            radioButton24.Checked = true;
            radioButton26.Checked = true;
            radioButton28.Checked = true;
            radioButton30.Checked = true;
            radioButton32.Checked = true;

            radioButton66.Checked = true;
            radioButton68.Checked = true;
            radioButton70.Checked = true;
            radioButton72.Checked = true;

            radioButton34.Checked = true;
            radioButton36.Checked = true;
            radioButton38.Checked = true;
            radioButton40.Checked = true;
            radioButton42.Checked = true;
            radioButton44.Checked = true;
            radioButton46.Checked = true;
            radioButton48.Checked = true;
            radioButton50.Checked = true;
            radioButton52.Checked = true;
            radioButton54.Checked = true;
            radioButton56.Checked = true;
            radioButton80.Checked = true;
            radioButton58.Checked = true;
            radioButton60.Checked = true;
            radioButton62.Checked = true;
            radioButton64.Checked = true;

            radioButton74.Checked = true;
            radioButton76.Checked = true;
            radioButton78.Checked = true;
        }


        public void Quitartodos()
        {
            radioButton2.Checked = true;
            radioButton3.Checked = true;
            radioButton5.Checked = true;
            radioButton7.Checked = true;
            radioButton9.Checked = true;
            radioButton11.Checked = true;
            radioButton13.Checked = true;
            radioButton15.Checked = true;
            radioButton17.Checked = true;
            radioButton19.Checked = true;
            radioButton21.Checked = true;
            radioButton23.Checked = true;
            radioButton25.Checked = true;
            radioButton27.Checked = true;
            radioButton29.Checked = true;
            radioButton31.Checked = true;

            radioButton65.Checked = true;
            radioButton67.Checked = true;
            radioButton69.Checked = true;
            radioButton71.Checked = true;

            radioButton33.Checked = true;
            radioButton35.Checked = true;
            radioButton37.Checked = true;
            radioButton39.Checked = true;
            radioButton41.Checked = true;
            radioButton43.Checked = true;
            radioButton45.Checked = true;
            radioButton47.Checked = true;
            radioButton49.Checked = true;
            radioButton51.Checked = true;
            radioButton53.Checked = true;
            radioButton55.Checked = true;
            radioButton79.Checked = true;
            radioButton57.Checked = true;
            radioButton59.Checked = true;
            radioButton61.Checked = true;
            radioButton63.Checked = true;

            radioButton73.Checked = true;
            radioButton75.Checked = true;
            radioButton77.Checked = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked==true) Quitartodos();
            checkBox1.Checked = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
