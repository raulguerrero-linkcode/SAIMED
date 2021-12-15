using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SHOPCONTROL.HistorialClinica
{
    public partial class HistorialPaciente : Form
    {
        public HistorialPaciente()
        {
            InitializeComponent();
        }

        public string tipoEstudio = "";
        public string noExpediente = "";
        public string clavepaciente = "";
        public string PacientesdelDia = "";
        public void buscarRegitros() {
            string col2 = "";
            string Query = "";
            string Consulta = "";

            switch(tipoEstudio){
                case "Oftalmologico":
                    Query = "SELECT HCCVCliente,HCFECHA,HCMConsulta,consecutivo FROM HClinicaO WHERE HCCVCliente='"+noExpediente+"'";
                    col2 = "Motivo de consulta";
                    break;
                case "Dental":
                    Query = "SELECT NoExpediente,FECHA, MOTIVO,consecutivo FROM EDental WHERE NoExpediente='" + noExpediente + "'";
                    col2 = "Motivo de consulta";
                    break;
                case "Prenatal":
                    Query = "SELECT NoExpediente,FECHA, OBSERVACIONES1,consecutivo FROM CPrenatal WHERE NoExpediente='" + noExpediente + "'";
                    col2 = "Observaciones";
                    break;
                case "Colposcopico":

                    Query = "SELECT  ";
                    Query = Query + "  pacientes.nombre + ' ' + pacientes.APATERNO + ' ' + pacientes.AMATERNO as nombrepac ";
                    Query = Query + "  ,pacientes.expgineco as noexpediente";
                    Query = Query + "  ,pacientes.clave as clavepa";

                    Query = Query + " FROM citas inner join pacientes on pacientes.clave=citas.cvpaciente";
                    Query = Query + " where  citas.cvpaciente<>''";
                    if (textBox2.Text != "") Query = Query + " and pacientes.expgineco='" + textBox2.Text.Trim() + "'";
                    if (textBox1.Text != "") Query = Query + " and pacientes.clave='" + textBox1.Text.Trim() + "'";
                    if (textBox3.Text != "") Query = Query + " and pacientes.nombre='" + textBox3.Text.Trim() + "'";
                    if (textBox4.Text != "") Query = Query + " and pacientes.APATERNO='" + textBox4.Text.Trim() + "'";
                    if (textBox5.Text != "") Query = Query + " and pacientes.AMATERNO='" + textBox5.Text.Trim() + "'";
                    Query = Query + " order by pacientes.expgineco asc";
                
                    col2 = "Obervaciones";

                    break;
            }
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 60);
            Lv.Columns.Add("Paciente", 250);
            if (tipoEstudio=="Colposcopico") Lv.Columns.Add("Expediente Ginecologia", 90);
            if (tipoEstudio == "Oftalmologico") Lv.Columns.Add("Expediente Oftamologo", 90);
            if (tipoEstudio == "Dental") Lv.Columns.Add("Expediente Dental", 9);

            
            int cantColumnas = 6;
            Lv.BeginUpdate();
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["clavepa"].ToString());
              
                switch (tipoEstudio)
                {
                    case "Oftalmologico":
                        lvi.SubItems.Add(leer["HCFECHA"].ToString());
                        lvi.SubItems.Add(leer["HCMConsulta"].ToString());
                        break;
                    case "Dental":
                        lvi.SubItems.Add(leer["FECHA"].ToString());
                        lvi.SubItems.Add(leer["MOTIVO"].ToString());
                        break;
                    case "Prenatal":
                        lvi.SubItems.Add(leer["FECHA"].ToString());
                        lvi.SubItems.Add(leer["OBSERVACIONES1"].ToString());
                        break;
                    case "Colposcopico":
                        lvi.SubItems.Add(leer["nombrepac"].ToString());
                        lvi.SubItems.Add(leer["noexpediente"].ToString());
                        break;
                }
                
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            Lv.EndUpdate();
        }
        public void mostrarDatosPaciente() {

           textBox2.Text = noExpediente;
           textBox1.Text = clavepaciente;

        }
        private void Lv_DoubleClick(object sender, EventArgs e)
        {
           
                    this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void HistorialPaciente_Load(object sender, EventArgs e)
        {
            mostrarDatosPaciente();
            buscarRegitros();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buscarRegitros();
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


        public void DetallesModifica2(int index)
        {
            valoresg.CLAVEPAC = Lv.Items[index].Text;
            noExpediente= Lv.Items[index].SubItems[2].Text;

            if (tipoEstudio == "Colposcopico") valoresg.BNUMEXPGINECO = noExpediente;
            if (tipoEstudio == "Oftalmologico") valoresg.BNUMEXPOFTAMO = noExpediente;
            if (tipoEstudio == "Dental") valoresg.BNUMEXPDENTAL = noExpediente;
            if (tipoEstudio == "Prenatal") valoresg.BNUMEXPGINECO = noExpediente;
        }
    }
}
