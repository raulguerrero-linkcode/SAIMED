using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SHOPCONTROL.ClienteBloqueado
{
    public partial class ClienteBloqueadoC : Form
    {
        public ClienteBloqueadoC()
        {
            InitializeComponent();
        }


        public string CLAVE = "";
        public string NOMBRE = "";
        public string OBSERVACIONES = "";
        public string FECHA = "";
        public string FECHACOD = "";
        public string USUARIO = "";
        public bool BANDNUEVOUSUARIO = false;
        private void ClienteBloqueadoC_Load(object sender, EventArgs e)
        {

            //muestra la tabla
            CargarInformacion();
            //OCULTAR PANEL
            panel2.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargarInformacion();
        }
       

        private void button3_Click(object sender, EventArgs e)
        {
            
            conectorSql conecta = new conectorSql();
            string consulta = "";

            Recolectainfo();
            if (Validacion())
            { 
                if (BANDNUEVOUSUARIO==true)
                {
                    //nuevo usuario bloqueado
                    consulta = "Insert into ClientesBloqueados(idcliente,observacion,fechacod,fecha,emitio) values(";
                    consulta = consulta + "'" + CLAVE + "','" + OBSERVACIONES + "','" + FECHACOD + "','" + FECHA + "','" + USUARIO + "')";
                    conecta.Excute(consulta);
                    conecta.CierraConexion();
                    MessageBox.Show("Se guardo correctamente el bloqueo", "SAIMED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarInformacion();
                    panel2.Visible = false;
                    panel1.Visible = true;
                }
                else
                {
                    // modificacion del usuario
                    consulta = "update ClientesBloqueados set observacion= '" + OBSERVACIONES + "', emitio='" + USUARIO +"' where idcliente='" + CLAVE + "'";
                    conecta.Excute(consulta);
                    conecta.CierraConexion();
                    MessageBox.Show("Se actualizo correctamente el bloqueo", "SAIMED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarInformacion();
                    panel2.Visible = false;
                    panel1.Visible = true;
                }

            }



        }


        public void Recolectainfo()
        {
            CLAVE = textBox3.Text.Trim();
            NOMBRE = textBox4.Text.Trim();
            FECHA = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            FECHACOD= dateTimePicker1.Value.ToString("yyyyMMdd");
            OBSERVACIONES = textBox5.Text.Trim();
            USUARIO = valoresg.USUARIOSIS;

        }

        public bool Validacion()
        {
            if (CLAVE == "")
            {
                MessageBox.Show("Ingrese la clave del paciente", "SAIMED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }


            if (NOMBRE == "")
            {
                MessageBox.Show("Ingrese el nombre del paciente ", "SAIMED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }


            if (OBSERVACIONES == "")
            {
                MessageBox.Show("Ingrese las observaciones del motivo por cual se restringe el servicio al paciente", "SAIMED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            
            }

            if (BANDNUEVOUSUARIO==true)
            {
                if (CLAVE != "")
                {
                    conectorSql conecta = new conectorSql();
                    string consulta = "Select * from ClientesBloqueados where idcliente='" + CLAVE + "'";
                    bool existepaciente = conecta.ExisteRegistro(consulta);
                    if (existepaciente)
                    {
                        MessageBox.Show("El paciente ya se encuentra bloqueado, verifique", "SAIMED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }




            return true;
        }

        public void CargarInformacion()
        {

            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Clave", 60).Tag = "STRING";
            Lv.Columns.Add("Nombre", 120).Tag = "STRING";
            Lv.Columns.Add("Observaciones", 100).Tag = "STRING";
            Lv.Columns.Add("Fecha", 90).Tag = "STRING";
            Lv.Columns.Add("Emitio", 150).Tag = "STRING";

            conectorSql conecta = new conectorSql();

            string Query = "select pacientes.clave, pacientes.nombre + ' ' +pacientes.apaterno + ' ' + pacientes.amaterno as Nombrec, ClientesBloqueados.observacion, ClientesBloqueados.fecha,";
            Query = Query + " ClientesBloqueados.emitio from ClientesBloqueados ";
            Query = Query + " inner join pacientes on pacientes.clave=ClientesBloqueados.idcliente ";
            Query = Query + " Where pacientes.clave <>''";
            if (textBox1.Text != "") Query = Query + " and pacientes.clave='" + textBox1.Text + "'";
            if (textBox2.Text != "") Query = Query + " and pacientes.nombre='" + textBox2.Text + "'";
            Query = Query + " order by pacientes.nombre asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["clave"].ToString());
                lvi.SubItems.Add(leer["Nombrec"].ToString());
                lvi.SubItems.Add(leer["observacion"].ToString());
                lvi.SubItems.Add(leer["fecha"].ToString());
                lvi.SubItems.Add(leer["emitio"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            label9.Text = Lv.Items.Count.ToString() + " Listado de pacientes bloqueados ";

        }


  
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
            LimpiarNuevo();
            BANDNUEVOUSUARIO = true;

            textBox3.Focus();
        }

        public void LimpiarNuevo()
        {
            textBox3.Text = "";
            textBox3.Enabled = true;
            textBox4.Text = "";
            textBox5.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }


        private void button7_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string consulta = "";

            if (MessageBox.Show("Se eliminaran las filas seleccionas", "SAIMED", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < Lv.Items.Count; i++)
                {
                    string idcliente = Lv.Items[i].Text;
                    if (Lv.Items[i].Checked == true)
                    {
                        consulta = "Delete from ClientesBloqueados where idcliente='" + idcliente + "'";
                        conecta.Excute(consulta);
                        conecta.CierraConexion();

                    }
                }
            }
            //MessageBox.Show("Se eliminaron los pacientes seleccionados de la lista de bloque", "SAIMED", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarInformacion();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void button2_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) CargarInformacion();
        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string fecha = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) CargarInformacion();
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
            BANDNUEVOUSUARIO = false;
            textBox3.Text = Lv.Items[index].Text;
            textBox4.Enabled = true;
            BuscarPacienteBloqueado(textBox3.Text);
            panel1.Visible = false;
            panel2.Visible = true;
        }


        public void BuscarPacienteBloqueado(string idpaciente)
        {

            textBox3.Enabled = false;
            conectorSql conecta = new conectorSql();
            string Query = "Select pacientes.nombre + ' ' + pacientes.apaterno + ' ' + pacientes.amaterno as Nombrecom, ClientesBloqueados.observacion, ClientesBloqueados.fecha from ClientesBloqueados ";
            Query = Query + " inner join pacientes on pacientes.clave=ClientesBloqueados.idcliente";
            Query = Query + " where ClientesBloqueados.idcliente='" + idpaciente +"'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                textBox4.Text = leer["Nombrecom"].ToString();
                textBox5.Text = leer["observacion"].ToString();
                dateTimePicker1.Value = DateTime.Parse(leer["fecha"].ToString());
            }
            conecta.CierraConexion();
        
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) BuscarNombrePaciente();
        }

        public void BuscarNombrePaciente()
        {
            bool existe = false;
            conectorSql conecta = new conectorSql();
            string query = "Select nombre from pacientes where clave='" + textBox3.Text.Trim() + "'";
            SqlDataReader leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                existe = true;
                textBox4.Text = leer["nombre"].ToString();
                textBox4.Enabled = false;
            }
            conecta.CierraConexion();

            if (existe==false)
            {
                MessageBox.Show("La clave del paciente no existe", "SAIMED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

   

    }
}

