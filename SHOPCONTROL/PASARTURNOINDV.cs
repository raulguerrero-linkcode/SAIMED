using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace SHOPCONTROL
{

    
    public partial class PASARTURNOINDV : Form
    {
        public PASARTURNOINDV()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VolveraLlamar(CVDOCTOR, label2.Text.Trim());
            label2.BackColor = Color.FromArgb(255, 192, 255);

        }

        public void VolveraLlamar(string cvdoctor, string turno)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Update citas set voz='0' where cvdoctor='" + cvdoctor + "'";
            Query = Query + " and fechacod='" + DateTime.Now.ToString("yyyyMMdd")+ "'";
            Query =Query + " and estatusserv='EN CONSULTA' and voz='1' and idturno='" + turno +"'";
            conecta.Excute(Query);
            conecta.CierraConexion();
        }

        public void LLamarSiguiente(string cvdoctor, Label turnotext, Label turnoSig)
        {
            conectorSql conecta = new conectorSql();
            string Query = "";
            string idturno = "";
            Query= "Select top(1) idturno from citas where cvdoctor='" + cvdoctor + "'";
            Query = Query + " and fechacod='" + DateTime.Now.ToString("yyyyMMdd") + "'";
            Query = Query + " and estatusserv='POR ATENDER'  and estatus='PAGADO' order by progresivo asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                idturno = leer["idturno"].ToString();
            }
            conecta.CierraConexion();

            Query = "Update citas set voz='0', estatusserv='EN CONSULTA' where cvdoctor='" + cvdoctor + "'";
            Query = Query + " and fechacod='" + DateTime.Now.ToString("yyyyMMdd") + "'";
            Query = Query + " and estatusserv='POR ATENDER'  and estatus='PAGADO' and idturno='" + idturno + "'";
            conecta.Excute(Query);
            conecta.CierraConexion();

            turnotext.Text = idturno.Trim();
            turnotext.BackColor = Color.White;


            Query = "Select top(1) idturno from citas where cvdoctor='" + cvdoctor + "'";
            Query = Query + " and fechacod='" + DateTime.Now.ToString("yyyyMMdd") + "'";
            Query = Query + " and estatusserv='POR ATENDER'  and estatus='PAGADO' order by progresivo asc";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                idturno = leer["idturno"].ToString();
            }
            conecta.CierraConexion();

            turnoSig.Text = idturno.Trim();
            turnoSig.BackColor = Color.White;

        }

        public void TurnoActual(string cvdoctor, Label turnoactual)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;
            string Query = "";
            string idturno = "";
            Query = "Select top(1) idturno from citas where cvdoctor='" + cvdoctor + "'";
            Query = Query + " and fechacod='" + DateTime.Now.ToString("yyyyMMdd") + "'";
            Query = Query + " and estatusserv='EN CONSULTA'  and estatus='PAGADO' order by progresivo desc";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                idturno = leer["idturno"].ToString();
            }
            conecta.CierraConexion();

            turnoactual.Text = idturno;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // consultorio unidad 1 clave 2
            LLamarSiguiente(CVDOCTOR, label2,label10);
            label2.BackColor = Color.White;

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        public string CVDOCTOR = "";
        private void PASARTURNO_Load(object sender, EventArgs e)
        {
            combos.ComboDoctores(comboBox1);
            Limpiar();
            panel1.Visible = true;
        }

        public void Limpiar()
        {
            label2.Text = "";
            label10.Text = "";

        }

        public string CONSULTORIO = "";
        private void button3_Click_1(object sender, EventArgs e)
        {
            CONSULTORIO = comboBox1.Text.Trim();
            CVDOCTOR = comboBox1.SelectedValue.ToString();
            Limpiar();
            TurnoActual(CVDOCTOR, label2);
            label1.Text = CONSULTORIO;
            panel1.Visible = false;
        }
    }
}
