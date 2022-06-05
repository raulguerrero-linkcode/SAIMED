using System;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace SHOPCONTROL.HistorialClinica
{
    public partial class CatDoctores : Form
    {
        public string cvdoctor= "";
        public string nombre = "";
        public string horaentrada = "";
        public string horasalida= "";
        public string especialidad= "";
        public string horaecomedor= "";
        public string horascomedor = "";
        public string tiempocon = "";
        public string Dcomedor = "";
        public string tipoexpediente = "";
        public string AtivadoDoc = "NO";
        public CatDoctores()
        {
            InitializeComponent();
        }

        public void Recolecta()
        {
            cvdoctor = textBox2.Text.Trim();
            nombre = textBox3.Text.Trim();
            especialidad = textBox4.Text;
            tiempocon = textBox5.Text;
            horaentrada = dateTimePicker3.Value.ToString("HH:mm:00");
            horasalida = dateTimePicker1.Value.ToString("HH:mm:00");

            Dcomedor = "NO";
            if (checkBox1.Checked == true) Dcomedor = "SI";

            AtivadoDoc = "NO";
            if (checkBox1.Checked == true) AtivadoDoc = "SI";

            horaecomedor = dateTimePicker4.Value.ToString("HH:mm:00");
            horascomedor= dateTimePicker2.Value.ToString("HH:mm:00");
            if (radioButton1.Checked == true) tipoexpediente = "GINECO";
            if (radioButton2.Checked == true) tipoexpediente = "DENTAL";
            if (radioButton3.Checked == true) tipoexpediente = "OFTA";
            if (radioButton4.Checked == true) tipoexpediente = "NIN";


        }
        public bool Valida()
        {
            if (cvdoctor== "")
            {
                MessageBox.Show("Ingrese clave para el doctor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (nombre== "")
            {
                MessageBox.Show("Ingrese el nombre del doctor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (tiempocon == "")
            {
                MessageBox.Show("Ingrese el tiempo de consulta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public bool Guardar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Insert into Doctores(cvdoctor,nombre,especialidad,HoraEntrada,HoraSalida,TiempoConsulta,HoraEcomedor,HoraSComedor, Dcomedor,tipoexpediente, activo) values('" + cvdoctor + "','" + nombre + "','" + especialidad + "','" + horaentrada + "','" + horasalida + "','" + tiempocon + "','" + horaecomedor + "','" + horascomedor + "','" + Dcomedor + "','" + tipoexpediente+ "','" + AtivadoDoc + "')";
           bool registrado= conecta.Excute(Query);
            conecta.CierraConexion();
            return registrado;
        }

        public bool Actualizar()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Update Doctores set nombre='" + nombre + "'";
            Query = Query + ",especialidad='" + especialidad + "'";
            Query = Query + ",HoraEntrada='" + horaentrada + "'";
            Query = Query + ",HoraSalida='" + horasalida+ "'";
            Query = Query + ",TiempoConsulta='" + tiempocon+ "'";
            Query = Query + ",HoraEcomedor='" + horaecomedor+ "'";
            Query = Query + ",HoraSComedor='" + horascomedor+ "'";
            Query = Query + ",Dcomedor='" + Dcomedor + "'";
            Query = Query + ",activo='" + AtivadoDoc + "'";
            Query = Query + ",tipoexpediente='" + tipoexpediente + "'";
            Query = Query + " where cvdoctor='" + cvdoctor + "'";
            bool registrado = conecta.Excute(Query);
            conecta.CierraConexion();
            return registrado;
        }

        public void Elimina()
        {
            conectorSql conecta = new conectorSql();
            string Query = "delete doctores where cvservicio='" + cvdoctor+ "'";
            conecta.Excute(Query);
            conecta.CierraConexion();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Catservicios_Load(object sender, EventArgs e)
        {

        }

        public void Buscarinfo()
        {
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 40).Tag = "NUMBER";
            Lv.Columns.Add("Nombre", 200).Tag = "STRING";
            Lv.Columns.Add("Hora Entrada", 100).Tag = "STRING";
            Lv.Columns.Add("Hora Salida", 100).Tag = "STRING";
            Lv.Columns.Add("Tiempo ", 100).Tag = "STRING";
            Lv.Columns.Add("Salida Comedor", 100).Tag = "STRING";
            Lv.Columns.Add("Entrada Comedor", 100).Tag = "STRING";
            Lv.Columns.Add("Especialidad", 0).Tag = "STRING";
            Lv.Columns.Add("Comedor", 20).Tag = "STRING";
            Lv.Columns.Add("tipo", 0).Tag = "STRING";
            Lv.Columns.Add("Activo", 0).Tag = "STRING";

            conectorSql conecta = new conectorSql();
            string query = "Select * from Doctores  where cvdoctor<>''";
            if (textBox1.Text != "") query = query + " and nombre='" + textBox1.Text.Trim() + "'";
            query = query + " order by nombre asc";
            SqlDataReader leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["cvdoctor"].ToString());
                lvi.SubItems.Add(leer["nombre"].ToString());
                lvi.SubItems.Add(leer["HoraEntrada"].ToString());
                lvi.SubItems.Add(leer["HoraSalida"].ToString());
                lvi.SubItems.Add(leer["TiempoConsulta"].ToString());
                lvi.SubItems.Add(leer["HoraEcomedor"].ToString());
                lvi.SubItems.Add(leer["HoraSComedor"].ToString());
                lvi.SubItems.Add(leer["Especialidad"].ToString());
                lvi.SubItems.Add(leer["DCOMEDOR"].ToString());
                lvi.SubItems.Add(leer["tipoexpediente"].ToString());
                lvi.SubItems.Add(leer["activo"].ToString());

                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Recolecta();
            if (Valida() == false) return;
            if (textBox2.Enabled == true)
            {
                if (Guardar() == true)
                {
                    MessageBox.Show("Se registro correctamente el doctor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    dateTimePicker1.Value = DateTime.Parse("08:00:00");
                    dateTimePicker2.Value = DateTime.Parse("08:00:00");
                    dateTimePicker3.Value = DateTime.Parse("08:00:00");
                    dateTimePicker4.Value = DateTime.Parse("08:00:00");

                }
            }
            else
            {
                if (Actualizar() == true)
                {
                    MessageBox.Show("Se actualizo correctamente el doctor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox3.Text = "";
                    dateTimePicker1.Value = DateTime.Parse("08:00:00");
                    dateTimePicker2.Value = DateTime.Parse("08:00:00");
                    dateTimePicker3.Value = DateTime.Parse("08:00:00");
                    dateTimePicker4.Value = DateTime.Parse("08:00:00");

                }
            }
            Buscarinfo();
            panel1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Buscarinfo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = true;

            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            dateTimePicker1.Value = DateTime.Parse("08:00:00");
            dateTimePicker2.Value = DateTime.Parse("08:00:00");
            dateTimePicker3.Value = DateTime.Parse("08:00:00");
            dateTimePicker4.Value = DateTime.Parse("08:00:00");

            panel1.Visible = true;
            textBox2.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string query = "";
            int contador = 0;

            DialogResult reply = MessageBox.Show("¿Desea Eliminar los doctores seleccionados?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.No)
                return;


            for (int i = 0; i < Lv.Items.Count; i++)
            {
                if (Lv.Items[i].Checked == true)
                {
                    query = "Delete from doctores where cvdoctor='" + Lv.Items[i].Text + "'";
                    bool eliminado=conecta.Excute(query);
                    if (eliminado == true) contador++;
                }
            }

            MessageBox.Show("Se eliminaron " + contador.ToString() + "  doctores", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Buscarinfo();
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

            
            string clave = Lv.Items[index].Text;
            string nombre= Lv.Items[index].SubItems[1].Text;
            string horaentrada = Lv.Items[index].SubItems[2].Text;
            string horasalida= Lv.Items[index].SubItems[3].Text;
            string tiempo= Lv.Items[index].SubItems[4].Text;
            string horaecomedor= Lv.Items[index].SubItems[5].Text;
            string horascomedor = Lv.Items[index].SubItems[6].Text;
            string especialidad= Lv.Items[index].SubItems[7].Text;
            string dcomedor= Lv.Items[index].SubItems[8].Text;
            string tipoexp = Lv.Items[index].SubItems[9].Text;
            string activo= Lv.Items[index].SubItems[10].Text;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;


            if (tipoexp == "GINECO") radioButton1.Checked = true;
            if (tipoexp == "DENTAL") radioButton2.Checked = true;
            if (tipoexp == "OFTA") radioButton3.Checked = true;
            if (tipoexp == "NIN") radioButton4.Checked = true;

            dateTimePicker3.Value = DateTime.Parse(horaentrada);
            dateTimePicker1.Value = DateTime.Parse(horasalida);
            textBox5.Text = tiempo;
            textBox2.Text = clave;
            textBox3.Text = nombre;

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            if (dcomedor == "SI") checkBox1.Checked = true;
            if (activo == "SI") checkBox2.Checked = true;

            if (horaecomedor!="") dateTimePicker4.Value = DateTime.Parse(horaecomedor);
            if (horascomedor != "") dateTimePicker2.Value = DateTime.Parse(horascomedor);
            textBox4.Text = especialidad;
            textBox2.Enabled = false;

            panel1.Visible = true;


        }
    }
}
