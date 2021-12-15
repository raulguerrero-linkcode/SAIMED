using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Speech.Synthesis;

namespace TurnosPacientes
{
    public partial class Form1 : Form
    {

        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        List<VoiceInfo> vocesInfo = new List<VoiceInfo>();
        public int REPETICIONES = 0;
        public int CONTADORDEPTO = 0;
        public int DIVISORESN= 0;

        public int CONTADOR = 0;
        public int REPIMAGEN = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListaDoctoresDepto();
            IngresarPantalla();
        }

        public void HablarTurno(string textohablar)
        {
            int indice;

            double Volumen = 100;
            double Rate =0; //-10 a 10

            indice = 2;
            String nombre = vocesInfo.ElementAt(indice).Name;
            synthesizer.SelectVoice(nombre);

            synthesizer.Volume = (int)Volumen;
            synthesizer.Rate = (int)Rate;
            synthesizer.Speak(textohablar);

        }

        public void ListaDoctoresDepto()
        {

            listView1.Items.Clear();
            listView1.Columns.Clear();

            listView1.Columns.Add("Cvdoctor", 50).Tag = "NUMBER";
            listView1.Columns.Add("Doctor", 90).Tag = "STRING";
            listView1.Columns.Add("Fecha", 70).Tag = "STRING";
            listView1.Columns.Add("Turno", 80).Tag = "STRING";
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;
            string Query = "Select * from  doctores where cvdoctor<>'' order by cvdoctor asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            int contador =1;
            while (leer.Read())
            {
                string cvdoctor = leer["cvdoctor"].ToString();
                string progresivo = "0";
                ListViewItem lvi = new ListViewItem(cvdoctor);
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(DateTime.Now.ToString("dd/MM/yyyy"));

                string consulta = "Select TOP(1) * from citas where  cvdoctor='" + cvdoctor + "' and fechacod='" + DateTime.Now.ToString("yyyyMMdd")  + "' and estatus='OCUPADO'";
                consulta = consulta + " order by progresivo asc";
                leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    progresivo = leer2["progresivo"].ToString();  
                }
                conecta2.CierraConexion();
                
                lvi.SubItems.Add(progresivo);
                listView1.Items.Add(lvi);
                contador++;

            }
            conecta.CierraConexion();
            CONTADORDEPTO = contador-1;
            label1.Text = "0";
            label2.Text = ".";

            label4.Text = "0";
            label3.Text = ".";

            label6.Text = "0";
            label5.Text = ".";

            label8.Text = "0";
            label7.Text = ".";

        }

        public void IngresarPantalla()
        {
            int divisores = CONTADORDEPTO / 4;
            int residuo = CONTADORDEPTO % 4;

            if (divisores == 0 && residuo > 0) divisores = 1;
            DIVISORESN = divisores;

            if (REPETICIONES == 1)
            {
                int xx = 0;
                label1.Text = listView1.Items[xx].SubItems[3].Text;
                label2.Text = listView1.Items[xx].SubItems[1].Text;

                if (listView1.Items.Count > 1)
                {
                    xx++;
                    label4.Text = listView1.Items[xx].SubItems[3].Text;
                    label3.Text = listView1.Items[xx].SubItems[1].Text;
                }

                if (listView1.Items.Count > 2)
                {
                    xx++;
                    label6.Text = listView1.Items[xx].SubItems[3].Text;
                    label5.Text = listView1.Items[xx].SubItems[1].Text;
                }

                if (listView1.Items.Count > 3)
                {
                    xx++;
                    label8.Text = listView1.Items[xx].SubItems[3].Text;
                    label7.Text = listView1.Items[xx].SubItems[1].Text;
                }
            }

            if (REPETICIONES == 2)
            {
                int xx = 4;
                label1.Text = listView1.Items[xx].SubItems[3].Text;
                label2.Text = listView1.Items[xx].SubItems[1].Text;

                if (listView1.Items.Count > 5)
                {
                    xx++;
                    label4.Text = listView1.Items[xx].SubItems[3].Text;
                    label3.Text = listView1.Items[xx].SubItems[1].Text;
                }

                if (listView1.Items.Count > 6)
                {
                    xx++;
                    label6.Text = listView1.Items[xx].SubItems[3].Text;
                    label5.Text = listView1.Items[xx].SubItems[1].Text;
                }

                if (listView1.Items.Count > 7)
                {
                    xx++;
                    label8.Text = listView1.Items[xx].SubItems[3].Text;
                    label7.Text = listView1.Items[xx].SubItems[1].Text;
                }
            }


            if (REPETICIONES == 3)
            {
                int xx = 8;
                label1.Text = listView1.Items[xx].SubItems[3].Text;
                label2.Text = listView1.Items[xx].SubItems[1].Text;

                if (listView1.Items.Count > 9)
                {
                    xx++;
                    label4.Text = listView1.Items[xx].SubItems[3].Text;
                    label3.Text = listView1.Items[xx].SubItems[1].Text;
                }

                if (listView1.Items.Count > 10)
                {
                    xx++;
                    label6.Text = listView1.Items[xx].SubItems[3].Text;
                    label5.Text = listView1.Items[xx].SubItems[1].Text;
                }

                if (listView1.Items.Count > 11)
                {
                    xx++;
                    label8.Text = listView1.Items[xx].SubItems[3].Text;
                    label7.Text = listView1.Items[xx].SubItems[1].Text;
                }
            }





        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            REPETICIONES++;
            if (REPETICIONES <= DIVISORESN)
            {
                ListaDoctoresDepto();
                IngresarPantalla();
                if (REPETICIONES == DIVISORESN)REPETICIONES = 0;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (REPIMAGEN == 3) // cada 2 minutos
            {
                CONTADOR++;
                conectorSql conecta = new conectorSql();
                string Query = "Select * from imgpantalla where clave='" + CONTADOR.ToString() + "'";

                bool existe = conecta.ExisteRegistro(Query);
                if (existe == true)
                    pictureBox1.Image = ClaseFotos.ConsultarFotoTurnos(CONTADOR.ToString());
                else
                    CONTADOR = 0;

                REPIMAGEN = 0;
            }

            REPIMAGEN++;
        }
    }
}
