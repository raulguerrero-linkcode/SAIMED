using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

using CrystalDecisions.CrystalReports.Engine;

namespace SHOPCONTROL.HistorialClinica
{
    public partial class HCOftalmologia : Form
    {

        public HCOftalmologia()
        {
            InitializeComponent();
        }

        public string HCCVCliente = "";
        public string HCFecha = "";
        public string HCCODFecha = "";
        //Historia Clinica HC
        public string HCInterrogatorio = ""; //para checkBox
        public string HCMConsulta = "";
        public string HCPActual = "";
        //Heredo de Familiares HF - Sistemicos S
        public string HCHFSDM = "";
        public string HCHFSHAS = "";
        public string HCHFSCA = "";
        public string HCHFSOtros = "";
        //Oftalmologicos
        public string HCHFODM = "";
        public string HCHFOHAS = "";
        public string HCHFOCA = "";
        public string HCHFOOtros = "";
        //Antecedentes personales AP - Sistemicos S
        public string HCAPSDM = "";
        public string HCAPSEvolucion = "";
        public string HCAPSUG = "";
        public string HCAPSControl = "";
        public string HCAPSCancer = "";
        public string HCAPSTransfuncionales = "";
        public string HCAPSAlergicos = "";
        public string HCAPSQuirurgicos = "";
        public string HCAPSMedicamentos = "";

        public string HCAPSHAS = "";
        public string HCAPSEvolucion2 = "";
        public string HCAPSControl2 = "";
        public string HCAPSCardiopatia = "";
        public string HCAPSENFEndocrina = "";
        public string HCAPSENFNeurologica = "";
        public string HCAPSAR = "";
        public string HCAPSInfecciosos = "";
        public string HCAPSOtros = "";

        public string HCAPCIRUGIAP1 = "";
        public string HCAPCIRUGIAF1 = "";
        public string HCAPCIRUGIAP2 = "";
        public string HCAPCIRUGIAF2 = "";
        public string HCAPCIRUGIAP3 = "";
        public string HCAPCIRUGIAF3 = "";
        //Antecedentes personales AP - Oftalmoloficos
        public string HCAPOCatarata = "";
        public string HCAPOGlaucoma = "";
        public string HCAPORetinopatia = "";
        public string HCAPOEstrabismo = "";
        public string HCAPOTruma = "";
        public string HCAPOOtros = "";
        public string HCAPOUltimoExamen = "";
        public string HCAPOMedicamentos = "";
        //Laser
        public string HCLP1 = "";
        public string HCLPF1 = "";
        public string HCLP2 = "";
        public string HCLPF2 = "";
        //AMSLER
        public string HCAMSLER1 = "";
        public string HCAMSLER2 = "";
        //Exploracion oftamologica EO
        public string HCEOAVSCOD = "";
        public string HCEOAVSCEST = "";
        public string HCEOAVSCOI = "";
        public string HCEOCCOD = "";
        public string HCEOCCEST = "";
        public string HCEOCCOI = "";
        //Lensometria
        public string HCLOD = "";
        public string HCLOI = "";
        public string HCLQMOD = "";
        public string HCLADD1 = "";
        public string HCLADD2 = "";
        public string HCLQMOI = "";
        //Refraccion R Objetivo O
        public string HCLROD = "";
        public string HCLROI = "";
        //Refraccion R Subjetivo S
        public string HCLRSOD = "";
        public string HCLRSOI = "";
        public string HCLRSDIP = "";
        public string HCLRSCV = "";
        public string HCLRSADDOD = "";
        public string HCLRSOI2 = "";
        public string HCLRSVC = "";
        public string HCLRSObservaciones = "";
        //Movimientos oculares
        public string HCMOculares = "";
        //Reflejos
        public string HCRFMotorOD = "";
        public string HCRFMotorOI = "";
        public string HCRConsensualOD = "";
        public string HCRConsensualOI = "";
        public string HCRDEFPUPILAROD = "";
        public string HCRDEFPUPILAROI = "";
        //Cristalino
        //T/O
        public string HCTOAIOTRO = "";
        public string HCTOAIOD = "";
        public string HCTOAIOI = "";
        //Angulo
        //Papilas P
        public string HCPExcavacionOD = "";
        public string HCPColoracionOD = "";
        public string HCPBordesOD = "";

        public string HCPExcavacionOI = "";
        public string HCPColoracionOI = "";
        public string HCPBordesOI = "";
        //Vitreo V
        public string HCVDPVODPARCIAL = "";
        public string HCVDPVODTOTAL = "";
        public string HCVDPVOIPARCIAL = "";
        public string HCVDPVOITOTAL = "";
        public string HCVSineresis = "";
        public string HCVHialosis = "";
        public string HCVHemorragia = "";
        public string HCVSineresisOI = "";
        public string HCVHialosisOI = "";
        public string HCVHemorragiaOI = "";

        public string HCDiagnostico = "";
        public string HCPlan = "";
        public string HCComentario = "";

        int CONSECUTIVO = 0;
        private bool EDITAR = false;

        List<PictureBox> pbList= new List<PictureBox>();
        List<Image> imgList = new List<Image>();
           

        public void Recolecta() {
            HCCVCliente = textBox1.Text.Trim();
            HCFecha = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            HCCODFecha = dateTimePicker1.Value.ToString("yyyMMdd");

            HCInterrogatorio = comboBox9.Text;
            HCMConsulta = textBox2.Text.Trim();
            HCPActual = textBox3.Text.Trim();
            //Heredo de Familiares HF - Sistemicos S
            HCHFSDM = textBox4.Text.Trim();
            HCHFSHAS = textBox5.Text.Trim();
            HCHFSCA = textBox6.Text.Trim();
            HCHFSOtros = textBox7.Text.Trim();
            //Oftalmologicos
            HCHFODM = textBox11.Text.Trim();
            HCHFOHAS = textBox10.Text.Trim();
            HCHFOCA = textBox9.Text.Trim();
            HCHFOOtros = textBox8.Text.Trim();
            //Antecedentes personales AP - Sistemicos S
            HCAPSDM = textBox12.Text.Trim();
            HCAPSEvolucion = textBox13.Text.Trim();
            HCAPSUG = textBox14.Text.Trim();
            HCAPSControl = textBox15.Text.Trim();
            HCAPSCancer = textBox16.Text.Trim();
            HCAPSTransfuncionales = textBox17.Text.Trim();
            HCAPSAlergicos = textBox18.Text.Trim();
            HCAPSQuirurgicos = textBox19.Text.Trim();
            HCAPSMedicamentos = textBox20.Text.Trim();

            HCAPSHAS = textBox21.Text.Trim();
            HCAPSEvolucion2 = textBox22.Text.Trim();
            HCAPSControl2 = textBox23.Text.Trim();
            HCAPSCardiopatia = textBox26.Text.Trim();
            HCAPSENFEndocrina = textBox25.Text.Trim();
            HCAPSENFNeurologica = textBox24.Text.Trim();
            HCAPSAR = textBox27.Text.Trim();
            HCAPSInfecciosos = textBox28.Text.Trim();
            HCAPSOtros = textBox29.Text.Trim();

            HCAPCIRUGIAP1 = textBox36.Text.Trim();
            HCAPCIRUGIAF1 = dateTimePicker2.Value.ToString("dd/MM/yyyy");
            HCAPCIRUGIAP2 = textBox39.Text.Trim();
            HCAPCIRUGIAF2 = dateTimePicker3.Value.ToString("dd/MM/yyyy");
            HCAPCIRUGIAP3 = textBox41.Text.Trim();
            HCAPCIRUGIAF3 = dateTimePicker4.Value.ToString("dd/MM/yyyy");
            //Antecedentes personales AP - Oftalmoloficos
            HCAPOCatarata = textBox30.Text.Trim();
            HCAPOGlaucoma = textBox31.Text.Trim();
            HCAPORetinopatia = textBox32.Text.Trim();
            HCAPOEstrabismo = textBox33.Text.Trim();
            HCAPOTruma = textBox34.Text.Trim();
            HCAPOOtros = textBox35.Text.Trim();
            HCAPOUltimoExamen = textBox42.Text.Trim();
            HCAPOMedicamentos = textBox40.Text.Trim();
            //Laser
            HCLP1 = textBox37.Text.Trim();
            HCLPF1 = dateTimePicker5.Value.ToString("dd/MM/yyyy");
            HCLP2 = textBox38.Text.Trim();
            HCLPF2 = dateTimePicker6.Value.ToString("dd/MM/yyyy");
            //AMSLER
            HCAMSLER1 = comboBox1.Text;
            HCAMSLER2 = comboBox2.Text;
            //Exploracion oftamologica EO
            HCEOAVSCOD = textBox43.Text.Trim();
            HCEOAVSCEST = textBox45.Text.Trim();
            HCEOAVSCOI = textBox44.Text.Trim();
            HCEOCCOD = textBox46.Text.Trim();
            HCEOCCEST = textBox47.Text.Trim();
            HCEOCCOI = textBox48.Text.Trim();
            //Lensometria
            HCLOD = textBox49.Text.Trim();
            HCLOI = textBox50.Text.Trim();
            HCLQMOD = textBox55.Text.Trim();
            HCLADD1 = textBox51.Text.Trim();
            HCLADD2 = textBox52.Text.Trim();
            HCLQMOI = textBox56.Text.Trim();
            //Refraccion R Objetivo O
            HCLROD = textBox57.Text.Trim();
            HCLROI = textBox58.Text.Trim();
            //Refraccion R Subjetivo S
            HCLRSOD = textBox59.Text.Trim();
            HCLRSOI = textBox60.Text.Trim();
            HCLRSDIP = textBox61.Text.Trim();
            HCLRSCV = textBox62.Text.Trim();
            HCLRSADDOD = textBox63.Text.Trim();
            HCLRSOI2 = textBox65.Text.Trim();
            HCLRSVC = textBox64.Text.Trim();
            HCLRSObservaciones = textBox68.Text.Trim();
            //Movimientos oculares
            HCMOculares = textBox69.Text.Trim();
            //Reflejos
            HCRFMotorOD = textBox72.Text.Trim();
            HCRFMotorOI = textBox73.Text.Trim();
            HCRConsensualOD = textBox71.Text.Trim();
            HCRConsensualOI = textBox74.Text.Trim();
            HCRDEFPUPILAROD = textBox70.Text.Trim();
            HCRDEFPUPILAROI = textBox75.Text.Trim();
            //Cristalino
            //T/O
            HCTOAIOTRO = textBox76.Text.Trim();
            HCTOAIOD = textBox77.Text.Trim();
            HCTOAIOI = textBox78.Text.Trim();
            //Angulo
            //Papilas P
            HCPExcavacionOD = comboBox3.Text;
            HCPColoracionOD = comboBox4.Text;
            HCPBordesOD = comboBox5.Text;

            HCPExcavacionOI = comboBox6.Text;
            HCPColoracionOI = comboBox7.Text;
            HCPBordesOI = comboBox8.Text;
            //Vitreo V
            HCVDPVODPARCIAL = textBox85.Text.Trim();
            HCVDPVODTOTAL = textBox89.Text.Trim();
            HCVDPVOIPARCIAL = textBox90.Text.Trim();
            HCVDPVOITOTAL = textBox91.Text.Trim();
            HCVSineresis = textBox86.Text.Trim();
            HCVHialosis = textBox87.Text.Trim();
            HCVHemorragia = textBox88.Text.Trim();
            HCVSineresisOI = textBox92.Text.Trim();
            HCVHialosisOI = textBox93.Text.Trim();
            HCVHemorragiaOI = textBox94.Text.Trim();

            HCDiagnostico = textBox95.Text.Trim();
            HCPlan = textBox96.Text.Trim();
            HCComentario = textBox97.Text.Trim();

            CONSECUTIVO = int.Parse(label14.Text);
        }
        
        public bool Validacion() {
            if (HCInterrogatorio.Equals("")) {
                MessageBox.Show("Seleccione el tipo de interrogatorio ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox9.Focus();
                return false;
            }
            if (HCMConsulta.Equals(""))
            {
                MessageBox.Show("Ingrese el motivo de la consulta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return false;
            }
            if (HCPActual.Equals(""))
            {
                MessageBox.Show("Ingrese el padecimiento actual", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return false;
            }
            return true;
        }
        
        public void Guardar() {
            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "insert into HClinicaO(";
            Query += "HCCVCliente,";
            Query += "HCFecha,";
            Query += "HCCODFecha,";
            Query += "HCInterrogatorio,";
            Query += "HCMConsulta,";
            Query += "HCPActual,";
            Query += "HCHFSDM,";
            Query += "HCHFSHAS,";
            Query += "HCHFSCA,";
            Query += "HCHFSOtros,";
            Query += "HCHFODM,";
            Query += "HCHFOHAS,";
            Query += "HCHFOCA,";
            Query += "HCHFOOtros,";
            Query += "HCAPSDM,";
            Query += "HCAPSEvolucion,";
            Query += "HCAPSUG,";
            Query += "HCAPSControl,";
            Query += "HCAPSCancer,";
            Query += "HCAPSTransfuncionales,";
            Query += "HCAPSAlergicos,";
            Query += "HCAPSQuirurgicos,";
            Query += "HCAPSMedicamentos,";
            Query += "HCAPSHAS,";
            Query += "HCAPSEvolucion2,";
            Query += "HCAPSControl2,";
            Query += "HCAPSCardiopatia,";
            Query += "HCAPSENFEndocrina,";
            Query += "HCAPSENFNeurologica,";
            Query += "HCAPSAR,";
            Query += "HCAPSInfecciosos,";
            Query += "HCAPSOtros,";
            Query += "HCAPCIRUGIAP1,";
            Query += "HCAPCIRUGIAF1,";
            Query += "HCAPCIRUGIAP2,";
            Query += "HCAPCIRUGIAF2,";
            Query += "HCAPCIRUGIAP3,";
            Query += "HCAPCIRUGIAF3,";
            Query += "HCAPOCatarata,";
            Query += "HCAPOGlaucoma,";
            Query += "HCAPORetinopatia,";
            Query += "HCAPOEstrabismo,";
            Query += "HCAPOTruma,";
            Query += "HCAPOOtros,";
            Query += "HCAPOUltimoExamen,";
            Query += "HCAPOMedicamentos,";
            Query += "HCLP1,";
            Query += "HCLPF1,";
            Query += "HCLP2,";
            Query += "HCLPF2,";
            Query += "HCAMSLER1,";
            Query += "HCAMSLER2,";
            Query += "HCEOAVSCOD,";
            Query += "HCEOAVSCEST,";
            Query += "HCEOAVSCOI,";
            Query += "HCEOCCOD,";
            Query += "HCEOCCEST,";
            Query += "HCEOCCOI,";
            Query += "HCLOD,";
            Query += "HCLOI,";
            Query += "HCLQMOD,";
            Query += "HCLADD1,";
            Query += "HCLADD2,";
            Query += "HCLQMOI,";
            Query += "HCLROD,";
            Query += "HCLROI,";
            Query += "HCLRSOD,";
            Query += "HCLRSOI,";
            Query += "HCLRSDIP,";
            Query += "HCLRSCV,";
            Query += "HCLRSADDOD,";
            Query += "HCLRSOI2,";
            Query += "HCLRSVC,";
            Query += "HCLRSObservaciones,";
            Query += "HCMOculares,";
            Query += "HCRFMotorOD,";
            Query += "HCRFMotorOI,";
            Query += "HCRConsensualOD,";
            Query += "HCRConsensualOI,";
            Query += "HCRDEFPUPILAROD,";
            Query += "HCRDEFPUPILAROI,";
            Query += "HCTOAIOTRO,";
            Query += "HCTOAIOD,";
            Query += "HCTOAIOI,";
            Query += "HCPExcavacionOD,";
            Query += "HCPColoracionOD,";
            Query += "HCPBordesOD,";
            Query += "HCPExcavacionOI,";
            Query += "HCPColoracionOI,";
            Query += "HCPBordesOI,";
            Query += "HCVDPVODPARCIAL,";
            Query += "HCVDPVODTOTAL,";
            Query += "HCVDPVOIPARCIAL,";
            Query += "HCVDPVOITOTAL,";
            Query += "HCVSineresis,";
            Query += "HCVHialosis,";
            Query += "HCVHemorragia,";
            Query += "HCVSineresisOI,";
            Query += "HCVHialosisOI,";
            Query += "HCVHemorragiaOI,";
            Query += "HCDiagnostico,";
            Query += "HCPlan,";
            Query += "HCComentario,";
            Query += "consecutivo)";
            Query += "values(";
            Query += "'" + HCCVCliente + "'";
            Query += ",'" + HCFecha + "'";
            Query += ",'" + HCCODFecha + "'";
            Query += ",'" + HCInterrogatorio + "'";
            Query += ",'" + HCMConsulta + "'";
            Query += ",'" + HCPActual + "'";
            Query += ",'" + HCHFSDM + "'";
            Query += ",'" + HCHFSHAS + "'";
            Query += ",'" + HCHFSCA + "'";
            Query += ",'" + HCHFSOtros + "'";
            Query += ",'" + HCHFODM + "'";
            Query += ",'" + HCHFOHAS + "'";
            Query += ",'" + HCHFOCA + "'";
            Query += ",'" + HCHFOOtros + "'";
            Query += ",'" + HCAPSDM + "'";
            Query += ",'" + HCAPSEvolucion + "'";
            Query += ",'" + HCAPSUG + "'";
            Query += ",'" + HCAPSControl + "'";
            Query += ",'" + HCAPSCancer + "'";
            Query += ",'" + HCAPSTransfuncionales + "'";
            Query += ",'" + HCAPSAlergicos + "'";
            Query += ",'" + HCAPSQuirurgicos + "'";
            Query += ",'" + HCAPSMedicamentos + "'";
            Query += ",'" + HCAPSHAS + "'";
            Query += ",'" + HCAPSEvolucion2 + "'";
            Query += ",'" + HCAPSControl2 + "'";
            Query += ",'" + HCAPSCardiopatia + "'";
            Query += ",'" + HCAPSENFEndocrina + "'";
            Query += ",'" + HCAPSENFNeurologica + "'";
            Query += ",'" + HCAPSAR + "'";
            Query += ",'" + HCAPSInfecciosos + "'";
            Query += ",'" + HCAPSOtros + "'";
            Query += ",'" + HCAPCIRUGIAP1 + "'";
            Query += ",'" + HCAPCIRUGIAF1 + "'";
            Query += ",'" + HCAPCIRUGIAP2 + "'";
            Query += ",'" + HCAPCIRUGIAF2 + "'";
            Query += ",'" + HCAPCIRUGIAP3 + "'";
            Query += ",'" + HCAPCIRUGIAF3 + "'";
            Query += ",'" + HCAPOCatarata + "'";
            Query += ",'" + HCAPOGlaucoma + "'";
            Query += ",'" + HCAPORetinopatia + "'";
            Query += ",'" + HCAPOEstrabismo + "'";
            Query += ",'" + HCAPOTruma + "'";
            Query += ",'" + HCAPOOtros + "'";
            Query += ",'" + HCAPOUltimoExamen + "'";
            Query += ",'" + HCAPOMedicamentos + "'";
            Query += ",'" + HCLP1 + "'";
            Query += ",'" + HCLPF1 + "'";
            Query += ",'" + HCLP2 + "'";
            Query += ",'" + HCLPF2 + "'";
            Query += ",'" + HCAMSLER1 + "'";
            Query += ",'" + HCAMSLER2 + "'";
            Query += ",'" + HCEOAVSCOD + "'";
            Query += ",'" + HCEOAVSCEST + "'";
            Query += ",'" + HCEOAVSCOI + "'";
            Query += ",'" + HCEOCCOD + "'";
            Query += ",'" + HCEOCCEST + "'";
            Query += ",'" + HCEOCCOI + "'";
            Query += ",'" + HCLOD + "'";
            Query += ",'" + HCLOI + "'";
            Query += ",'" + HCLQMOD + "'";
            Query += ",'" + HCLADD1 + "'";
            Query += ",'" + HCLADD2 + "'";
            Query += ",'" + HCLQMOI + "'";
            Query += ",'" + HCLROD + "'";
            Query += ",'" + HCLROI + "'";
            Query += ",'" + HCLRSOD + "'";
            Query += ",'" + HCLRSOI + "'";
            Query += ",'" + HCLRSDIP + "'";
            Query += ",'" + HCLRSCV + "'";
            Query += ",'" + HCLRSADDOD + "'";
            Query += ",'" + HCLRSOI2 + "'";
            Query += ",'" + HCLRSVC + "'";
            Query += ",'" + HCLRSObservaciones + "'";
            Query += ",'" + HCMOculares + "'";
            Query += ",'" + HCRFMotorOD + "'";
            Query += ",'" + HCRFMotorOI + "'";
            Query += ",'" + HCRConsensualOD + "'";
            Query += ",'" + HCRConsensualOI + "'";
            Query += ",'" + HCRDEFPUPILAROD + "'";
            Query += ",'" + HCRDEFPUPILAROI + "'";
            Query += ",'" + HCTOAIOTRO + "'";
            Query += ",'" + HCTOAIOD + "'";
            Query += ",'" + HCTOAIOI + "'";
            Query += ",'" + HCPExcavacionOD + "'";
            Query += ",'" + HCPColoracionOD + "'";
            Query += ",'" + HCPBordesOD + "'";
            Query += ",'" + HCPExcavacionOI + "'";
            Query += ",'" + HCPColoracionOI + "'";
            Query += ",'" + HCPBordesOI + "'";
            Query += ",'" + HCVDPVODPARCIAL + "'";
            Query += ",'" + HCVDPVODTOTAL + "'";
            Query += ",'" + HCVDPVOIPARCIAL + "'";
            Query += ",'" + HCVDPVOITOTAL + "'";
            Query += ",'" + HCVSineresis + "'";
            Query += ",'" + HCVHialosis + "'";
            Query += ",'" + HCVHemorragia + "'";
            Query += ",'" + HCVSineresisOI + "'";
            Query += ",'" + HCVHialosisOI + "'";
            Query += ",'" + HCVHemorragiaOI + "'";
            Query += ",'" + HCDiagnostico + "'";
            Query += ",'" + HCPlan + "'";
            Query += ",'" + HCComentario + "'";
            Query += ",'" + CONSECUTIVO + "')";
            conecta.Excute(Query);
        }
       
        public void Limpiar(Control cnt)
        {
            cargaConsecutivo();
            foreach (Control c in cnt.Controls)
            {
                if (c is TabControl)
                {
                    Limpiar(c);
                }
                if (c is TabPage)
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
                    //DateTimePicker dt = new DateTimePicker();
                    //dt = (DateTimePicker) c;
                    //dt.ResetText();
                }
            }
            for (int i = 0; i < imgList.Count; i++)
            {
                pbList[i].Image = imgList[i];
            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            Recolecta();
            if (Validacion()) {
                if (EDITAR)
                {
                    eliminarRegistro(HCCVCliente, CONSECUTIVO);
                }
                else {
                    actualizaConsecutivo();
                }
                
                Guardar();
                MessageBox.Show("Se guardo correctamente la información registrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar(this);
                LimpiarLabels();
                activaNuevo();
                textBox1.Focus();
            }
        }
        
        public void BuscarInformacion(string clave, int _consecutivo) {
            Limpiar(this);
            conectorSql conecta = new conectorSql();
            string Query = "SELECT  * FROM  HClinicaO WHERE HCCVCliente='" + clave + "'";
            Query += " and consecutivo =" + _consecutivo + "";
            SqlDataReader leer = conecta.RecordInfo(Query);
            textBox1.Text = clave;
            while (leer.Read())
            {
                dateTimePicker1.Text = leer["HCFecha"].ToString();
                comboBox9.Text = leer["HCInterrogatorio"].ToString();
                textBox2.Text = leer["HCMConsulta"].ToString();
                textBox3.Text = leer["HCPActual"].ToString();
                textBox4.Text = leer["HCHFSDM"].ToString();
                textBox5.Text = leer["HCHFSHAS"].ToString();
                textBox6.Text = leer["HCHFSCA"].ToString();
                textBox7.Text = leer["HCHFSOtros"].ToString();
                textBox11.Text = leer["HCHFODM"].ToString();
                textBox10.Text = leer["HCHFOHAS"].ToString();
                textBox9.Text = leer["HCHFOCA"].ToString();
                textBox8.Text = leer["HCHFOOtros"].ToString();
                textBox12.Text = leer["HCAPSDM"].ToString();
                textBox13.Text = leer["HCAPSEvolucion"].ToString();
                textBox14.Text = leer["HCAPSUG"].ToString();
                textBox15.Text = leer["HCAPSControl"].ToString();
                textBox16.Text = leer["HCAPSCancer"].ToString();
                textBox17.Text = leer["HCAPSTransfuncionales"].ToString();
                textBox18.Text = leer["HCAPSAlergicos"].ToString();
                textBox19.Text = leer["HCAPSQuirurgicos"].ToString();
                textBox20.Text = leer["HCAPSMedicamentos"].ToString();
                textBox21.Text = leer["HCAPSHAS"].ToString();
                textBox22.Text = leer["HCAPSEvolucion2"].ToString();
                textBox23.Text = leer["HCAPSControl2"].ToString();
                textBox26.Text = leer["HCAPSCardiopatia"].ToString();
                textBox25.Text = leer["HCAPSENFEndocrina"].ToString();
                textBox24.Text = leer["HCAPSENFNeurologica"].ToString();
                textBox27.Text = leer["HCAPSAR"].ToString();
                textBox28.Text = leer["HCAPSInfecciosos"].ToString();
                textBox29.Text = leer["HCAPSOtros"].ToString();
                textBox36.Text = leer["HCAPCIRUGIAP1"].ToString();
                dateTimePicker2.Text = leer["HCAPCIRUGIAF1"].ToString();
                textBox39.Text = leer["HCAPCIRUGIAP2"].ToString();
                dateTimePicker3.Text = leer["HCAPCIRUGIAF2"].ToString();
                textBox41.Text = leer["HCAPCIRUGIAP3"].ToString();
                dateTimePicker4.Text = leer["HCAPCIRUGIAF3"].ToString();
                textBox30.Text = leer["HCAPOCatarata"].ToString();
                textBox31.Text = leer["HCAPOGlaucoma"].ToString();
                textBox32.Text = leer["HCAPORetinopatia"].ToString();
                textBox33.Text = leer["HCAPOEstrabismo"].ToString();
                textBox34.Text = leer["HCAPOTruma"].ToString();
                textBox35.Text = leer["HCAPOOtros"].ToString();
                textBox42.Text = leer["HCAPOUltimoExamen"].ToString();
                textBox40.Text = leer["HCAPOMedicamentos"].ToString();
                textBox37.Text = leer["HCLP1"].ToString();
                dateTimePicker5.Text = leer["HCLPF1"].ToString();
                textBox38.Text = leer["HCLP2"].ToString();
                dateTimePicker6.Text = leer["HCLPF2"].ToString();
                comboBox1.Text = leer["HCAMSLER1"].ToString();
                comboBox2.Text = leer["HCAMSLER2"].ToString();
                textBox43.Text = leer["HCEOAVSCOD"].ToString();
                textBox45.Text = leer["HCEOAVSCEST"].ToString();
                textBox44.Text = leer["HCEOAVSCOI"].ToString();
                textBox46.Text = leer["HCEOCCOD"].ToString();
                textBox47.Text = leer["HCEOCCEST"].ToString();
                textBox48.Text = leer["HCEOCCOI"].ToString();
                textBox49.Text = leer["HCLOD"].ToString();
                textBox50.Text = leer["HCLOI"].ToString();
                textBox55.Text = leer["HCLQMOD"].ToString();
                textBox51.Text = leer["HCLADD1"].ToString();
                textBox52.Text = leer["HCLADD2"].ToString();
                textBox56.Text = leer["HCLQMOI"].ToString();
                textBox57.Text = leer["HCLROD"].ToString();
                textBox58.Text = leer["HCLROI"].ToString();
                textBox59.Text = leer["HCLRSOD"].ToString();
                textBox60.Text = leer["HCLRSOI"].ToString();
                textBox61.Text = leer["HCLRSDIP"].ToString();
                textBox62.Text = leer["HCLRSCV"].ToString();
                textBox63.Text = leer["HCLRSADDOD"].ToString();
                textBox65.Text = leer["HCLRSOI2"].ToString();
                textBox64.Text = leer["HCLRSVC"].ToString();
                textBox68.Text = leer["HCLRSObservaciones"].ToString();
                textBox69.Text = leer["HCMOculares"].ToString();
                textBox72.Text = leer["HCRFMotorOD"].ToString();
                textBox73.Text = leer["HCRFMotorOI"].ToString();
                textBox71.Text = leer["HCRConsensualOD"].ToString();
                textBox74.Text = leer["HCRConsensualOI"].ToString();
                textBox70.Text = leer["HCRDEFPUPILAROD"].ToString();
                textBox75.Text = leer["HCRDEFPUPILAROI"].ToString();
                textBox76.Text = leer["HCTOAIOTRO"].ToString();
                textBox77.Text = leer["HCTOAIOD"].ToString();
                textBox78.Text = leer["HCTOAIOI"].ToString();
                comboBox3.Text = leer["HCPExcavacionOD"].ToString();
                comboBox4.Text = leer["HCPColoracionOD"].ToString();
                comboBox5.Text = leer["HCPBordesOD"].ToString();
                comboBox6.Text = leer["HCPExcavacionOI"].ToString();
                comboBox7.Text = leer["HCPColoracionOI"].ToString();
                comboBox8.Text = leer["HCPBordesOI"].ToString();
                textBox85.Text = leer["HCVDPVODPARCIAL"].ToString();
                textBox89.Text = leer["HCVDPVODTOTAL"].ToString();
                textBox90.Text = leer["HCVDPVOIPARCIAL"].ToString();
                textBox91.Text = leer["HCVDPVOITOTAL"].ToString();
                textBox86.Text = leer["HCVSineresis"].ToString();
                textBox87.Text = leer["HCVHialosis"].ToString();
                textBox88.Text = leer["HCVHemorragia"].ToString();
                textBox92.Text = leer["HCVSineresisOI"].ToString();
                textBox93.Text = leer["HCVHialosisOI"].ToString();
                textBox94.Text = leer["HCVHemorragiaOI"].ToString();
                textBox95.Text = leer["HCDiagnostico"].ToString();
                textBox96.Text = leer["HCPlan"].ToString();
                textBox97.Text = leer["HCComentario"].ToString();
            }
            conecta.CierraConexion();
        }
        
        public void BuscaInformacionPaciente(string clave) {
            LimpiarLabels();
            conectorSql conecta = new conectorSql();
            string Query = "SELECT NOMBRE,APATERNO,AMATERNO,EDAD,CALLE,NoCalle,GENERO,COLONIA,TELEFONO,OCUPACION FROM  Pacientes WHERE NoExpediente='" + clave + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            string _domicilio = "";
            while (leer.Read())
            {
                label5.Text = leer["NOMBRE"].ToString() + " " + leer["APATERNO"].ToString() + " " + leer["AMATERNO"].ToString();
                label6.Text = leer["EDAD"].ToString();
                _domicilio = "CALLE " + leer["CALLE"].ToString()+" No. "+ leer["NoCalle"].ToString() +" COL. " + leer["COLONIA"].ToString();
                label7.Text = _domicilio;
                label12.Text = leer["GENERO"].ToString();
                label16.Text = leer["TELEFONO"].ToString();
                label13.Text = leer["OCUPACION"].ToString();
            }
            conecta.CierraConexion();
        }
       
        public void LimpiarLabels() {
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label12.Text = "";
            label16.Text = "";
            label13.Text = "";
        }
        
        public string Generos(string _genero)
        {
            if (_genero.Equals("F"))
            {
                return "FEMENINO";
            }
            else if (_genero.Equals("M"))
            {
                return "MASCULINO";
            }
            else
            {
                return "";
            }
        }
        
        
        
        public void eliminarRegistro(string clave, int _consecutivo) {
            conectorSql conecta = new conectorSql();
            string Query = "DELETE FROM HClinicaO where HCCVCliente='" + clave + "' AND consecutivo =" + _consecutivo + "";
            conecta.Excute(Query);
        }
       
        private void button2_Click(object sender, EventArgs e)
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
        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void HCOftalmologia_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            tabPage7.Show();
            tabPage6.Show();
            tabPage5.Show();
            tabPage4.Show();
            tabPage3.Show();
            tabPage2.Show();
            tabPage1.Show();


            pbList.Add(pbpp1);
            pbList.Add(pbpp2);
            pbList.Add(pbbiomicro1);
            pbList.Add(pbbiomicro2);
            pbList.Add(pbcristalinood);
            pbList.Add(pbcristalinooi);
            pbList.Add(angulo1);
            pbList.Add(angulo2);
            pbList.Add(retina1);
            pbList.Add(retina2);

            cargaConsecutivo();

            foreach (PictureBox item in pbList)
            {
                imgList.Add(item.Image);
    
                item.Click += delegate
                {
                    if (textBox1.Text.Equals(""))
                    {
                        MessageBox.Show("Ingresa la clave", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Focus();
                    }
                    else
                    {
                        valoresg.BANDIMAGEN = item.Name;
                        HistorialClinica.Editor myEditor = new HistorialClinica.Editor();
                        myEditor.imagenFondo = item.Image;
                        myEditor.imagenNueva = imgList[idImage(item)]; //item.Image;
                        myEditor.NOMBREIMAGEN = item.Name;
                        myEditor.NOEXPEDIENTE = textBox1.Text.Trim();
                        myEditor.CONSECUTIVO = int.Parse(label14.Text);
                        myEditor.Show();
                    }
                };
            }
        }
        public int idImage(PictureBox pbbox) {
            int id=0;
            for (int i = 0; i < pbList.Count; i++)
            {
                if (pbbox.Name == pbList[i].Name)
                {
                    id = i;
                    break;
                }
            }
            return id;
        }
        
        private void HCOftalmologia_Activated(object sender, EventArgs e)
        {
            if (valoresg.EXPEDIENTE != "" && valoresg.CONSECUTIVOPACIENTE != 0)
            {
                textBox1.Text = valoresg.EXPEDIENTE;
                BuscarInformacion(valoresg.EXPEDIENTE, valoresg.CONSECUTIVOPACIENTE);
                
                label14.Text = valoresg.CONSECUTIVOPACIENTE + "";
                cargarImagenesExpediente(valoresg.EXPEDIENTE);
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

            if (valoresg.BANDIMAGEN == pbpp1.Name && ExisteImagen(textBox1.Text, pbpp1.Name, int.Parse(label14.Text)))
            {
                pbpp1.Image = ClaseFotos.ConsultarImagenExpediente(textBox1.Text.Trim(), pbpp1.Name, int.Parse(label14.Text));
                valoresg.BANDIMAGEN = "";
            }
            if (valoresg.BANDIMAGEN == pbpp2.Name && ExisteImagen(textBox1.Text, pbpp2.Name, int.Parse(label14.Text)))
            {
                pbpp2.Image = ClaseFotos.ConsultarImagenExpediente(textBox1.Text.Trim(), pbpp2.Name, int.Parse(label14.Text));
                valoresg.BANDIMAGEN = "";
            }
            if (valoresg.BANDIMAGEN == pbbiomicro1.Name && ExisteImagen(textBox1.Text, pbbiomicro1.Name, int.Parse(label14.Text)))
            {
                pbbiomicro1.Image = ClaseFotos.ConsultarImagenExpediente(textBox1.Text.Trim(), pbbiomicro1.Name, int.Parse(label14.Text));
                valoresg.BANDIMAGEN = "";
            }
            if (valoresg.BANDIMAGEN == pbbiomicro2.Name && ExisteImagen(textBox1.Text, pbbiomicro2.Name, int.Parse(label14.Text)))
            {
                pbbiomicro2.Image = ClaseFotos.ConsultarImagenExpediente(textBox1.Text.Trim(), pbbiomicro2.Name, int.Parse(label14.Text));
                valoresg.BANDIMAGEN = "";
            }
            if (valoresg.BANDIMAGEN == pbcristalinood.Name && ExisteImagen(textBox1.Text, pbcristalinood.Name, int.Parse(label14.Text)))
            {
                pbcristalinood.Image = ClaseFotos.ConsultarImagenExpediente(textBox1.Text.Trim(), pbcristalinood.Name, int.Parse(label14.Text));
                valoresg.BANDIMAGEN = "";
            }
            if (valoresg.BANDIMAGEN == pbcristalinooi.Name && ExisteImagen(textBox1.Text, pbcristalinooi.Name, int.Parse(label14.Text)))
            {
                pbcristalinooi.Image = ClaseFotos.ConsultarImagenExpediente(textBox1.Text.Trim(), pbcristalinooi.Name, int.Parse(label14.Text));
                valoresg.BANDIMAGEN = "";
            }
            if (valoresg.BANDIMAGEN == angulo1.Name && ExisteImagen(textBox1.Text, angulo1.Name, int.Parse(label14.Text)))
            {
                angulo1.Image = ClaseFotos.ConsultarImagenExpediente(textBox1.Text.Trim(), angulo1.Name, int.Parse(label14.Text));
                valoresg.BANDIMAGEN = ""; 
            }
            if (valoresg.BANDIMAGEN == angulo2.Name && ExisteImagen(textBox1.Text, angulo2.Name, int.Parse(label14.Text)))
            {
                angulo2.Image = ClaseFotos.ConsultarImagenExpediente(textBox1.Text.Trim(), angulo2.Name, int.Parse(label14.Text));
                valoresg.BANDIMAGEN = "";
            }
            if (valoresg.BANDIMAGEN == retina1.Name && ExisteImagen(textBox1.Text, retina1.Name, int.Parse(label14.Text)))
            {
                retina1.Image = ClaseFotos.ConsultarImagenExpediente(textBox1.Text.Trim(), retina1.Name, int.Parse(label14.Text));
                valoresg.BANDIMAGEN = "";
            }
            if (valoresg.BANDIMAGEN == retina2.Name && ExisteImagen(textBox1.Text, retina2.Name, int.Parse(label14.Text)))
            {
                retina2.Image = ClaseFotos.ConsultarImagenExpediente(textBox1.Text.Trim(), retina2.Name, int.Parse(label14.Text));
                valoresg.BANDIMAGEN = "";
            }
        }
        public void cargarImagenesExpediente(string clave) { 

            foreach (PictureBox item in pbList)
            {
                if (ExisteImagen(clave,item.Name, int.Parse(label14.Text)))
                {
                    item.Image = ClaseFotos.ConsultarImagenExpediente(clave, item.Name, int.Parse(label14.Text));
                }
            }
        }
        public bool ExisteImagen(string clave, string _nombre, int _consecutivo)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select noExpediente from imagenesclinica where noExpediente='" + clave + "' and nombre='" + _nombre + "' and consecutivo=" + _consecutivo + "";
            return conecta.ExisteRegistro(Query);
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)Keys.Enter)
            {
                button2_Click(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            activaNuevo();
            Limpiar(this);
            LimpiarLabels();
            textBox1.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
             if (textBox1.Text == "")
            {
                MessageBox.Show("Ingrese numero de expediente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
             else if (!ExisteRegistro(textBox1.Text, int.Parse(label14.Text)))
             {
                 MessageBox.Show("Guarde los cambios realizados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
             else
             {
                 ReportDocument cryRpt = new ReportDocument();

                 string CadenaReporte = @"\\SRV-DATACENTER\\tmp\\reports\\DOftalmologia.rpt";

                 DataSet ds = new DataSet();

                 DataSetOftalmologiaTableAdapters.PacientesTableAdapter taPaciente = new DataSetOftalmologiaTableAdapters.PacientesTableAdapter();
                 DataSetOftalmologia.PacientesDataTable daPaciente = new DataSetOftalmologia.PacientesDataTable();
                 taPaciente.Fill(daPaciente, textBox1.Text.Trim());

                 DataSetOftalmologiaTableAdapters.HClinicaOTableAdapter taOftalmologia = new DataSetOftalmologiaTableAdapters.HClinicaOTableAdapter();
                 DataSetOftalmologia.HClinicaODataTable daOftalmologia = new DataSetOftalmologia.HClinicaODataTable();
                 taOftalmologia.Fill(daOftalmologia, textBox1.Text.Trim(), int.Parse(label14.Text));

                 DataSetOftalmologiaTableAdapters.img1TableAdapter taImagenes = new DataSetOftalmologiaTableAdapters.img1TableAdapter();
                 DataSetOftalmologia.img1DataTable daImagenes = new DataSetOftalmologia.img1DataTable();
                 taImagenes.Fill(daImagenes, textBox1.Text.Trim(), int.Parse(label14.Text));

                 DataSetOftalmologiaTableAdapters.img2TableAdapter taImag2 = new DataSetOftalmologiaTableAdapters.img2TableAdapter();
                 DataSetOftalmologia.img2DataTable daImag2 = new DataSetOftalmologia.img2DataTable();
                 taImag2.Fill(daImag2, textBox1.Text.Trim(), int.Parse(label14.Text));

                 DataSetOftalmologiaTableAdapters.img3TableAdapter taImag3 = new DataSetOftalmologiaTableAdapters.img3TableAdapter();
                 DataSetOftalmologia.img3DataTable daImag3 = new DataSetOftalmologia.img3DataTable();
                 taImag3.Fill(daImag3, textBox1.Text.Trim(), int.Parse(label14.Text));

                 DataSetOftalmologiaTableAdapters.img4TableAdapter taImag4 = new DataSetOftalmologiaTableAdapters.img4TableAdapter();
                 DataSetOftalmologia.img4DataTable daImag4 = new DataSetOftalmologia.img4DataTable();
                 taImag4.Fill(daImag4, textBox1.Text.Trim(), int.Parse(label14.Text));

                 DataSetOftalmologiaTableAdapters.img5TableAdapter taImag5 = new DataSetOftalmologiaTableAdapters.img5TableAdapter();
                 DataSetOftalmologia.img5DataTable daImag5 = new DataSetOftalmologia.img5DataTable();
                 taImag5.Fill(daImag5, textBox1.Text.Trim(), int.Parse(label14.Text));

                 DataSetOftalmologiaTableAdapters.img6TableAdapter taImag6 = new DataSetOftalmologiaTableAdapters.img6TableAdapter();
                 DataSetOftalmologia.img6DataTable daImag6 = new DataSetOftalmologia.img6DataTable();
                 taImag6.Fill(daImag6, textBox1.Text.Trim(), int.Parse(label14.Text));

                 DataSetOftalmologiaTableAdapters.img7TableAdapter taImag7 = new DataSetOftalmologiaTableAdapters.img7TableAdapter();
                 DataSetOftalmologia.img7DataTable daImag7 = new DataSetOftalmologia.img7DataTable();
                 taImag7.Fill(daImag7, textBox1.Text.Trim(), int.Parse(label14.Text));

                 DataSetOftalmologiaTableAdapters.img8TableAdapter taImag8 = new DataSetOftalmologiaTableAdapters.img8TableAdapter();
                 DataSetOftalmologia.img8DataTable daImag8 = new DataSetOftalmologia.img8DataTable();
                 taImag8.Fill(daImag8, textBox1.Text.Trim(), int.Parse(label14.Text));

                 DataSetOftalmologiaTableAdapters.img9TableAdapter taImag9 = new DataSetOftalmologiaTableAdapters.img9TableAdapter();
                 DataSetOftalmologia.img9DataTable daImag9 = new DataSetOftalmologia.img9DataTable();
                 taImag9.Fill(daImag9, textBox1.Text.Trim(), int.Parse(label14.Text));

                 DataSetOftalmologiaTableAdapters.img10TableAdapter taImag10 = new DataSetOftalmologiaTableAdapters.img10TableAdapter();
                 DataSetOftalmologia.img10DataTable daImag10 = new DataSetOftalmologia.img10DataTable();
                 taImag10.Fill(daImag10, textBox1.Text.Trim(), int.Parse(label14.Text));

                 ds.Clear();
                 ds.Tables.Add(daPaciente);
                 ds.Tables.Add(daOftalmologia);
                 ds.Tables.Add(daImagenes);
                 ds.Tables.Add(daImag2);
                 ds.Tables.Add(daImag3);
                 ds.Tables.Add(daImag4);
                 ds.Tables.Add(daImag5);
                 ds.Tables.Add(daImag6);
                 ds.Tables.Add(daImag7);
                 ds.Tables.Add(daImag8);
                 ds.Tables.Add(daImag9);
                 ds.Tables.Add(daImag10);

                 cryRpt.Load(CadenaReporte);
                 cryRpt.SetDataSource(ds);

                 string NombreArchivo = @"C:\HClinicoOftalmologia_" + textBox1.Text + "_" + int.Parse(label24.Text) + ".pdf"; ;
                 // cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, NombreArchivo);
                 //cryRpt.PrintToPrinter(1, false, 0, 0);
                 cryRpt.Close();
                 cryRpt.Dispose();

                 MessageBox.Show("Se exporto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
        }

        public bool ExisteRegistro(string clave, int _consecutivo)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select HCCVCliente from HClinicaO where HCCVCliente='" + clave + "' and consecutivo=" + _consecutivo + "";
            return conecta.ExisteRegistro(Query);
        }

        public void cargaConsecutivo()
        {
            string Numero = "1";
            conectorSql conecta = new conectorSql();
            string Query = "Select oftamologia from consecutivos where oftamologia <>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                Numero = leer["oftamologia"].ToString();
            }
            conecta.CierraConexion();
            label14.Text = Numero;
        }
        public bool actualizaConsecutivo()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = int.Parse(label14.Text) + 1;
            string Query = "update consecutivos set oftamologia='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Ingrese numero de expediente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
            }
            else
            {
                HistorialClinica.HistorialPaciente myHistorial = new HistorialPaciente();
                myHistorial.tipoEstudio = "Oftalmologico";
                myHistorial.noExpediente = textBox1.Text.Trim();
                myHistorial.Show();
            }
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
    }
}