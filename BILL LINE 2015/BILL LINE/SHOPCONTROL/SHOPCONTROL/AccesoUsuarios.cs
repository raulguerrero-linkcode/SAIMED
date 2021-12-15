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
    public partial class AccesoUsuarios : Form
    {
        public string USUARIO;
        public string NOMBRE;
        public string CONTRA;


        public string ENTRA1;
        public string ENTRA2;
        public string ENTRA3;
        public string ENTRA4;
        public string ENTRA5;
        public string ENTRA6;
        public string ENTRA7;
        public string ENTRA8;
        public string ENTRA9;
        public string ENTRA10;
        public string ENTRA11;
        public string ENTRA12;
        public string ENTRA13;
        public string ENTRA14;
        public string ENTRA15;
        public string ENTRA16;
        public string ENTRA17;
        public string ENTRA18;
        public string ENTRA19;
        public string ENTRA20;
        public string ENTRA21;
        public string ENTRA22;
        public string ENTRA23;
        public string ENTRA24;
        public string ENTRA25;
        public string ENTRA26;
        public string ENTRA27;
        public string ENTRA28;
        public string ENTRA29;
        public string ENTRA30;
        public string ENTRA31;
        public string ENTRA32;
        public string ENTRA33;
        public string ESDOCTOR;
        public string CVDOCTOR;

        public AccesoUsuarios()
        {
            InitializeComponent();
        }

        public void Recolecta()
        {
            USUARIO = textBox1.Text;
            NOMBRE = textBox3.Text;
            CONTRA = textBox2.Text;

            ENTRA1 = "NO";
            ENTRA2 = "NO";
            ENTRA3 = "NO";
            ENTRA4 = "NO";
            ENTRA5 = "NO";
            ENTRA6 = "NO";
            ENTRA7 = "NO";
            ENTRA8 = "NO";
            ENTRA9 = "NO";
            ENTRA10 = "NO";
            ENTRA11 = "NO";
            ENTRA12 = "NO";
            ENTRA13 = "NO";
            ENTRA14 = "NO";
            ENTRA15 = "NO";
            ENTRA16 = "NO";
            ENTRA17 = "NO";
            ENTRA18 = "NO";
            ENTRA19 = "NO";
            ENTRA20 = "NO";
            ENTRA21 = "NO";
            ENTRA22 = "NO";
            ENTRA23 = "NO";
            ENTRA24 = "NO";
            ENTRA25 = "NO";
            ENTRA26 = "NO";
            ENTRA27 = "NO";
            ENTRA28 = "NO";
            ENTRA29 = "NO";
            ENTRA30 = "NO";
            ESDOCTOR = "NO";
            CVDOCTOR = "0";

            if (checkBox1.Checked == true) ENTRA1 = "SI";
            if (checkBox2.Checked == true) ENTRA2 = "SI";
            if (checkBox3.Checked == true) ENTRA3 = "SI";
            if (checkBox4.Checked == true) ENTRA4 = "SI";
            if (checkBox5.Checked == true) ENTRA5 = "SI";
            if (checkBox6.Checked == true) ENTRA6 = "SI";
            if (checkBox7.Checked == true) ENTRA7 = "SI";
            if (checkBox8.Checked == true) ENTRA8 = "SI";
            if (checkBox9.Checked == true) ENTRA9 = "SI";
            if (checkBox10.Checked == true) ENTRA10 = "SI";
            if (checkBox11.Checked == true) ENTRA11 = "SI";
            if (checkBox12.Checked == true) ENTRA12 = "SI";
            if (checkBox13.Checked == true) ENTRA13 = "SI";
            if (checkBox14.Checked == true) ENTRA14 = "SI";
            if (checkBox15.Checked == true) ENTRA15 = "SI";
            if (checkBox16.Checked == true) ENTRA16 = "SI";
            if (checkBox17.Checked == true) ENTRA17 = "SI";
            if (checkBox18.Checked == true) ENTRA18 = "SI";
            if (checkBox19.Checked == true) ENTRA19 = "SI";
            if (checkBox20.Checked == true) ENTRA20 = "SI";
            if (checkBox21.Checked == true) ENTRA21 = "SI"; //catalogo de servicios
            if (checkBox22.Checked == true) ENTRA22 = "SI"; //catalogo de doctores
            if (checkBox23.Checked == true) ENTRA23 = "SI"; //catalogo de paciente
            if (checkBox24.Checked == true) ENTRA24 = "SI"; //catalogo de adm turnos
            if (checkBox25.Checked == true) ENTRA25 = "SI"; //catalogo de gastos
            if (checkBox26.Checked == true) ENTRA26 = "SI"; //catalogo de gastos
            if (checkBox27.Checked == true) ENTRA27 = "SI"; //catalogo de gastos
            if (checkBox28.Checked == true) ENTRA28 = "SI"; //catalogo de gastos
            if (checkBox29.Checked == true) ENTRA29 = "SI"; //catalogo de gastos
            if (checkBox30.Checked == true) ENTRA30 = "SI"; //configurar correo
            if (checkBox32.Checked == true) ESDOCTOR = "SI"; //configurar correo

            CVDOCTOR=comboBox2.SelectedValue.ToString();
        }

        private void AccesoUsuarios_Load(object sender, EventArgs e)
        {
            combos.ComboDoctores(comboBox2);
            LimpiarChecks();
            CargarInfo();
        }

        public void LimpiarChecks()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            checkBox13.Checked = false;
            checkBox14.Checked = false;
            checkBox15.Checked = false;
            checkBox16.Checked = false;
            checkBox17.Checked = false;
            checkBox18.Checked = false;
            checkBox19.Checked = false;
            checkBox20.Checked = false;
            checkBox21.Checked = false;
            checkBox22.Checked = false;
            checkBox23.Checked = false;
            checkBox24.Checked = false;
            checkBox25.Checked = false;
            checkBox26.Checked = false;
            checkBox27.Checked = false;
            checkBox28.Checked = false;
            checkBox29.Checked = false;
            checkBox30.Checked = false;
            checkBox32.Checked = false;
            comboBox2.Visible = false;
        }

        public void ActivarChecks()
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
            checkBox7.Checked = true;
            checkBox8.Checked = true;
            checkBox9.Checked = true;
            checkBox10.Checked = true;
            checkBox11.Checked = true;
            checkBox12.Checked = true;
            checkBox13.Checked = true;
            checkBox14.Checked = true;
            checkBox15.Checked = true;
            checkBox16.Checked = true;
            checkBox17.Checked = true;
            checkBox18.Checked = true;
            checkBox19.Checked = true;
            checkBox20.Checked = true;
            checkBox21.Checked = true;
            checkBox22.Checked = true;
            checkBox23.Checked = true;
            checkBox24.Checked = true;
            checkBox25.Checked = true;
            checkBox26.Checked = true;
            checkBox27.Checked = true;
            checkBox28.Checked = true;
            checkBox29.Checked = true;
            checkBox30.Checked = true;
            checkBox32.Checked = true;
        }

        public void CargarInfo()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Usuario", 60).Tag = "STRING";
            Lv.Columns.Add("Nombre", 180).Tag = "STRING";

            conectorSql conecta = new conectorSql();
            string Query = "Select * from usuarios where cvusuario<>''";
            if (textBox3.Text != "") Query = Query + " and nombre like '%" + textBox3.Text + "%'";
            Query = Query + " order by cvusuario asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["cvusuario"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label8.Text = Lv.Items.Count.ToString() + " Usuarios";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LimpiarChecks();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ActivarChecks();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            Recolecta();
            GuardarUsuario();
            CargarInfo();
        }

        public void GuardarUsuario()
        {
            conectorSql conecta = new conectorSql();
            string Query = "";

            Query = "Delete from usuarios where cvusuario='" + USUARIO + "'";
            conecta.Excute(Query);

            Query="Insert into usuarios (cvusuario";
            Query=Query + ", contra";
            Query=Query + ",nombre";
            Query=Query + ",entra1";
            Query=Query + ",entra2";
            Query=Query + ",entra3";
            Query=Query + ",entra4";
            Query=Query + ",entra5";
            Query=Query + ",entra6";
            Query=Query + ",entra7";
            Query=Query + ",entra8";
            Query=Query + ",entra9";
            Query=Query + ",entra10";
            Query=Query + ",entra11";
            Query=Query + ",entra12";
            Query=Query + ",entra13";
            Query=Query + ",entra14";
            Query=Query + ",entra15";
            Query=Query + ",entra16";
            Query=Query + ",entra17";
            Query = Query + ",entra18";
            Query = Query + ",entra19";
            Query = Query + ",entra20";
            Query = Query + ",entra21";
            Query = Query + ",entra22";
            Query = Query + ",entra23";
            Query = Query + ",entra24";
            Query = Query + ",entra25";
            Query = Query + ",entra26";
            Query = Query + ",entra27";
            Query = Query + ",entra28";
            Query = Query + ",entra29";
            Query = Query + ",ESDOCTOR";
            Query = Query + ",CVDOCTOR";

            Query = Query + ",entra30)";
            Query = Query + " values(";
            Query = Query + "'" + USUARIO + "'";
            Query = Query + ",'" + CONTRA + "'";
            Query = Query + ",'" + NOMBRE + "'";
            Query = Query + ",'" + ENTRA1 + "'";
            Query = Query + ",'" + ENTRA2 + "'";
            Query = Query + ",'" + ENTRA3 + "'";
            Query = Query + ",'" + ENTRA4 + "'";
            Query = Query + ",'" + ENTRA5 + "'";
            Query = Query + ",'" + ENTRA6 + "'";
            Query = Query + ",'" + ENTRA7 + "'";
            Query = Query + ",'" + ENTRA8 + "'";
            Query = Query + ",'" + ENTRA9 + "'";
            Query = Query + ",'" + ENTRA10 + "'";
            Query = Query + ",'" + ENTRA11 + "'";
            Query = Query + ",'" + ENTRA12 + "'";
            Query = Query + ",'" + ENTRA13 + "'";
            Query = Query + ",'" + ENTRA14 + "'";
            Query = Query + ",'" + ENTRA15 + "'";
            Query = Query + ",'" + ENTRA16 + "'";
            Query = Query + ",'" + ENTRA17 + "'";
            Query = Query + ",'" + ENTRA18 + "'";
            Query = Query + ",'" + ENTRA19 + "'";
            Query = Query + ",'" + ENTRA20 + "'";
            Query = Query + ",'" + ENTRA21 + "'";
            Query = Query + ",'" + ENTRA22 + "'";
            Query = Query + ",'" + ENTRA23 + "'";
            Query = Query + ",'" + ENTRA24 + "'";
            Query = Query + ",'" + ENTRA25 + "'";
            Query = Query + ",'" + ENTRA26 + "'";
            Query = Query + ",'" + ENTRA27 + "'";
            Query = Query + ",'" + ENTRA28 + "'";
            Query = Query + ",'" + ENTRA29 + "'";
            Query = Query + ",'" + ESDOCTOR + "'";
            Query = Query + ",'" + CVDOCTOR+ "'";
            Query = Query + ",'" + ENTRA30 + "')";
            conecta.Excute(Query);

            MessageBox.Show("Se guardo correctamente el usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        public void DetallesModifica(int index)
        {
            textBox1.Text = Lv.Items[index].Text;

            BuscarBancoInfo(textBox1.Text);
        
        }

        public void BuscarBancoInfo(string clave)
        {
            LimpiarChecks();
            string cvdoctor = "";
            conectorSql conecta = new conectorSql();
            string Query = "Select * from usuarios where cvusuario='" + clave + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox1.Text = leer["cvusuario"].ToString();
                textBox1.Enabled = false;
                textBox3.Text = leer["nombre"].ToString();
                textBox2.Text = leer["contra"].ToString();
                cvdoctor = leer["cvdoctor"].ToString();

                ENTRA1 = leer["entra1"].ToString();
                ENTRA2 = leer["entra2"].ToString();
                ENTRA3 = leer["entra3"].ToString();
                ENTRA4 = leer["entra4"].ToString();
                ENTRA5 = leer["entra5"].ToString();
                ENTRA6 = leer["entra6"].ToString();
                ENTRA7 = leer["entra7"].ToString();
                ENTRA8 = leer["entra8"].ToString();
                ENTRA9 = leer["entra9"].ToString();
                ENTRA10 = leer["entra10"].ToString();
                ENTRA11 = leer["entra11"].ToString();
                ENTRA12 = leer["entra12"].ToString();
                ENTRA13 = leer["entra13"].ToString();
                ENTRA14 = leer["entra14"].ToString();
                ENTRA15 = leer["entra15"].ToString();
                ENTRA16 = leer["entra16"].ToString();
                ENTRA17 = leer["entra17"].ToString();
                ENTRA18 = leer["entra18"].ToString();
                ENTRA19 = leer["entra19"].ToString();
                ENTRA20 = leer["entra20"].ToString();
                ENTRA21 = leer["entra21"].ToString();
                ENTRA22 = leer["entra22"].ToString();
                ENTRA23 = leer["entra23"].ToString();
                ENTRA24 = leer["entra24"].ToString();
                ENTRA25 = leer["entra25"].ToString();
                ENTRA26 = leer["entra26"].ToString();
                ENTRA27 = leer["entra27"].ToString();
                ENTRA28 = leer["entra28"].ToString();
                ENTRA29 = leer["entra29"].ToString();
                ENTRA30 = leer["entra30"].ToString();
                ESDOCTOR = leer["ESDOCTOR"].ToString();

                if (ENTRA1 == "SI") checkBox1.Checked = true;
                if (ENTRA2 == "SI") checkBox2.Checked = true;
                if (ENTRA3 == "SI") checkBox3.Checked = true;
                if (ENTRA4 == "SI") checkBox4.Checked = true;
                if (ENTRA5 == "SI") checkBox5.Checked = true;
                if (ENTRA6 == "SI") checkBox6.Checked = true;
                if (ENTRA7 == "SI") checkBox7.Checked = true;
                if (ENTRA8 == "SI") checkBox8.Checked = true;
                if (ENTRA9 == "SI") checkBox9.Checked = true;
                if (ENTRA10 == "SI") checkBox10.Checked = true;
                if (ENTRA11 == "SI") checkBox11.Checked = true;
                if (ENTRA12 == "SI") checkBox12.Checked = true;
                if (ENTRA13 == "SI") checkBox13.Checked = true;
                if (ENTRA14 == "SI") checkBox14.Checked = true;
                if (ENTRA15 == "SI") checkBox15.Checked = true;
                if (ENTRA16 == "SI") checkBox16.Checked = true;
                if (ENTRA17 == "SI") checkBox17.Checked = true;
                if (ENTRA18 == "SI") checkBox18.Checked = true;
                if (ENTRA19 == "SI") checkBox19.Checked = true;
                if (ENTRA20 == "SI") checkBox20.Checked = true;
                if (ENTRA21 == "SI") checkBox21.Checked = true;
                if (ENTRA22 == "SI") checkBox22.Checked = true;
                if (ENTRA23 == "SI") checkBox23.Checked = true;
                if (ENTRA24 == "SI") checkBox24.Checked = true;
                if (ENTRA25 == "SI") checkBox25.Checked = true;
                if (ENTRA26 == "SI") checkBox26.Checked = true;
                if (ENTRA27 == "SI") checkBox27.Checked = true;
                if (ENTRA28 == "SI") checkBox28.Checked = true;
                if (ENTRA29 == "SI") checkBox29.Checked = true;
                if (ENTRA30 == "SI") checkBox30.Checked = true;
                if (ESDOCTOR == "SI") checkBox32.Checked = true;

            }
            conecta.CierraConexion();
            comboBox2.SelectedValue = cvdoctor;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            LimpiarChecks();
            textBox1.Focus();

        }

        private void checkBox32_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Visible = checkBox32.Checked;
        }
    }
}
