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
    public partial class NotasEvolucion : Form
    {
        public NotasEvolucion()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pacientes registro = new Pacientes();
            registro.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            ListadoDeCitas();
            panel1.Visible = true;

        }

        private void NotasEvolucion_Load(object sender, EventArgs e)
        {
            combos.ComboDoctores(comboBox2);
            combos.ComboDoctores(comboBox1);
            ListadoDeCitas();

            comboBox2.SelectedValue = valoresg.Area_Cvdoctor;
            textBox7.Text = valoresg.Area_usuario;
            textBox8.Text = valoresg.Area_Contra;
            button4_Click(sender, e);

        }


        public void BuscarPaciente()
        {
            conectorSql conecta = new conectorSql();
            string query = "Select NoExpediente,email,celular, nombre + ' ' + apaterno + ' ' + amaterno as nombrec, edad from  pacientes where clave='" + label32.Text.Trim() + "'";
            SqlDataReader leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                label34.Text = leer["nombrec"].ToString();
                label16.Text = leer["edad"].ToString();
            }
            conecta.CierraConexion();
        }

        public string CONSULTAGENERA = "";
        private void button2_Click(object sender, EventArgs e)
        {
            ListadoDeCitas();
        }
        public void ListadoDeCitas()
        {
            int acumulapagados = 0;
            int acumulaocupados = 0;
            int acumulalibres = 0;
            int acumulacancelado= 0;
            Lv.Items.Clear();
            Lv.Columns.Clear();

            Lv.Columns.Add("Ticket", 50).Tag = "NUMBER";
            Lv.Columns.Add("Doctor", 0).Tag = "STRING";
            Lv.Columns.Add("Clave", 60).Tag = "STRING";
            Lv.Columns.Add("Nombre", 200).Tag = "STRING";
            Lv.Columns.Add("Expediente", 90).Tag = "STRING";
            Lv.Columns.Add("Fecha", 80).Tag = "STRING";
            Lv.Columns.Add("Hora Inicia", 70).Tag = "STRING";
            Lv.Columns.Add("Estatus", 80).Tag = "STRING";
            Lv.Columns.Add("Tipo", 0).Tag = "STRING";
            Lv.Columns.Add("Servicio", 170).Tag = "STRING";
            Lv.Columns.Add("Emitio", 80).Tag = "STRING";
            Lv.Columns.Add("Recibo de Pago", 80).Tag = "STRING";

            conectorSql conecta = new conectorSql();
            string query = "Select citas.cvpaciente, citas.progresivo, citas.cvdoctor,citas.numexpediente, citas.fecha, citas.horainicia, citas.estatus,citas.tipo,";
            query = query + " pacientes.expgineco, pacientes.expdental, pacientes.expoftamolgo,";
            query=query +" citas.nombreservicio,citas.emite,citas.recibopago, pacientes.nombre + ' ' + pacientes.apaterno + ' ' + pacientes.amaterno as nombrepac ";
            query = query + " from citas ";
            query = query + " left join pacientes on citas.cvpaciente=pacientes.clave "; 
            query=query +" where fechacod='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "' and cvdoctor='" + comboBox1.SelectedValue.ToString() + "' order by progresivo asc";
            SqlDataReader leer = conecta.RecordInfo(query);
            CONSULTAGENERA = query;
            while (leer.Read())
            {
                ListViewItem lvi = new ListViewItem(leer["progresivo"].ToString());
                lvi.SubItems.Add(leer["cvdoctor"].ToString());
                lvi.SubItems.Add(leer["cvpaciente"].ToString());
                lvi.SubItems.Add(leer["nombrepac"].ToString());

                if (TIPOEXPEDIENTE == "GINECO") lvi.SubItems.Add(leer["expgineco"].ToString());
                if (TIPOEXPEDIENTE == "DENTAL") lvi.SubItems.Add(leer["expdental"].ToString());
                if (TIPOEXPEDIENTE == "OFTA") lvi.SubItems.Add(leer["expoftamolgo"].ToString());
                if (TIPOEXPEDIENTE != "OFTA" && TIPOEXPEDIENTE != "DENTAL" && TIPOEXPEDIENTE != "GINECO") lvi.SubItems.Add("0");
                
                lvi.SubItems.Add(leer["fecha"].ToString());
                lvi.SubItems.Add(leer["horainicia"].ToString());
                string estatus = leer["estatus"].ToString();
                if (estatus == "PAGADO") acumulapagados++;
                if (estatus == "LIBRE") acumulalibres++;
                if (estatus == "SIN PAGAR") acumulaocupados++;
                if (estatus == "CANCELADO") acumulacancelado++;
                lvi.SubItems.Add(estatus);
                lvi.SubItems.Add(leer["tipo"].ToString());
                lvi.SubItems.Add(leer["nombreservicio"].ToString());
                lvi.SubItems.Add(leer["emite"].ToString());
                lvi.SubItems.Add(leer["recibopago"].ToString());
                Lv.Items.Add(lvi);
            }
            conecta.CierraConexion();
            CambioDeColoresCelda();

            label25.Text = acumulapagados.ToString();
            label26.Text = acumulaocupados.ToString();
            label27.Text = acumulalibres.ToString();
            label28.Text = acumulacancelado.ToString();

        }


        private void CambioDeColoresCelda()
        {

            int columna = 0;
            columna = 7;

            Lv.BeginUpdate();

            foreach (ListViewItem item in Lv.Items)
            {
                int indice = 0;
                item.UseItemStyleForSubItems = false;

                foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                {
                    if (indice == columna)
                    {
                        if (subitem.Text == "ATENDIDO") subitem.BackColor = Color.FromArgb(96, 204, 69);
                        if (subitem.Text == "CANCELADO") subitem.BackColor = Color.FromArgb(255, 192, 192);
                        if (subitem.Text == "CANCELADO-DOCTOR") subitem.BackColor = Color.FromArgb(255, 192, 192);
                        if (subitem.Text == "LIBRE") subitem.BackColor = Color.FromArgb(253, 252, 223);
                        if (subitem.Text == "SIN PAGAR") subitem.BackColor = Color.FromArgb(247, 198, 85);
                        if (subitem.Text == "OCUPADO") subitem.BackColor = Color.FromArgb(247, 198, 85);
                        if (subitem.Text == "PAGADO") subitem.BackColor = Color.FromArgb(170, 242, 253);
                    }
                    indice++;
                }
            }
            Lv.EndUpdate();
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

        public bool BANDERA = false;
        public void DetallesModifica2(int index)
        {
            string numticket = Lv.Items[index].Text;
            string cvdoctor = Lv.Items[index].SubItems[1].Text;
            string cvpaciente = Lv.Items[index].SubItems[2].Text;
            string expediente = Lv.Items[index].SubItems[4].Text;
            string fecha = Lv.Items[index].SubItems[5].Text;
            string horainicia = Lv.Items[index].SubItems[6].Text;
            string estatus = Lv.Items[index].SubItems[7].Text;

            string numrecibo = Lv.Items[index].SubItems[11].Text;
            string servicio = Lv.Items[index].SubItems[9].Text;
            label11.Text = fecha;
            label10.Text = numticket;
            label33.Text = expediente;
            label32.Text = cvpaciente;
            label22.Text = numrecibo;
            label36.Text = servicio;
            label49.Text = cvpaciente;
            BuscarPaciente();
            label45.Text = numticket;
            label47.Text = label34.Text;

        }
        public void DetallesModifica(int index)
        {
            string numticket = Lv.Items[index].Text;
            string cvdoctor = Lv.Items[index].SubItems[1].Text;
            string cvpaciente= Lv.Items[index].SubItems[2].Text;
            string expediente = Lv.Items[index].SubItems[4].Text;
            string fecha = Lv.Items[index].SubItems[5].Text;
            string horainicia = Lv.Items[index].SubItems[6].Text;
            string estatus = Lv.Items[index].SubItems[7].Text;

            string numrecibo = Lv.Items[index].SubItems[11].Text;
            string servicio = Lv.Items[index].SubItems[9].Text;
            label11.Text = fecha;
            label10.Text = numticket;
            label33.Text = expediente;
            label32.Text = cvpaciente;
            label22.Text = numrecibo;
            label36.Text = servicio;

            BuscarPaciente();
            label45.Text = numticket;
            label47.Text = label34.Text;

           // combos.ComboServicio(comboBox3);
            label17.Text = DateTime.Now.ToString("HH:mm:00");

            if (estatus == "LIBRE")
            {
                MessageBox.Show("El espacio se encuentra libre espere a que sea ocupado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar();
                listView1.Items.Clear();
                listView1.Columns.Clear();

                return;
            }

            if (estatus == "CANCELADO")
            {
                MessageBox.Show("Se cancelo la cita podra otro paciente utilizar el tiempo  vacio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar();
                listView1.Items.Clear();
                listView1.Columns.Clear();
                return;
            }


            if (estatus == "SIN PAGAR")
            {
                BANDERA = true;
                cargaConsecutivo();
            }

            if (estatus == "PAGADO")
            {
                BANDERA = true;
                cargaConsecutivo();
            }

          
            CargarNotas();
            panel2.Visible = true;
            panel1.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Recolecta();
            if (Existeinfo() == true) return;
            if (Validacion() == false) return;
            Guardar();
            actualizaConsecutivo();
            MessageBox.Show("Se guardo correctamente la nota de evolucion, seleccione el proximo paciente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListadoDeCitas();
            Limpiar();
            panel2.Visible = false;
            ListadoDeCitas();
            panel1.Visible = true;

        }

        public void Limpiar()
        {
          
            label10.Text = "0";
            label32.Text = "";
            label33.Text = "";
            label34.Text = "";
     
            textBox3.Text = "";
            dateTimePicker2.Value = DateTime.Now;
            cargaConsecutivo();
        }
        public void CargarNotas()
        {
            listView1.Items.Clear();
            listView1.Columns.Clear();

            listView1.Columns.Add("Recibo", 60).Tag = "NUMBER";
            listView1.Columns.Add("Turno", 50).Tag = "STRING";
            listView1.Columns.Add("Fecha", 80).Tag = "STRING";
            listView1.Columns.Add("Nota", 400).Tag = "STRING";
            listView1.Columns.Add("Emite", 80).Tag = "STRING";
            conectorSql conecta = new conectorSql();
            string Query = "Select TOP (25) * from NEvolucion where cvpaciente='" + label32.Text + "' and cvdoctor='" + comboBox1.SelectedValue.ToString() + "'";
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

        public string numexpediente = "";
        public string doctor = "";
        public string fechareg = "";
        public string fcod= "";

        public string Horainicia = "";
        public string informe = "";
        public string Horatermina = "";
        public decimal tiempodentro = 0;
        public string emite = "";
        public decimal tminentrada = 0;
        public decimal tminsalida = 0;
        public string cvpaciente = "";
        public decimal folionota = 0;
        public string edad = "";
        public string fechaprox = "";
        public string fcodprox = "";
        public string numturno = "";
        public string numrecibo= "";
        public string proxtrata = "";
        public string trataterminado = "";
        string numrecibog = "";
        public void Recolecta()
        {

            emite = valoresg.USUARIOSIS;
            folionota = decimal.Parse(label5.Text);
            numturno = label10.Text;
            numrecibog = label22.Text;
            numexpediente = label33.Text;
            cvpaciente = label32.Text.Trim();
            Horainicia = label17.Text;
            fechareg = DateTime.Now.ToString("dd/MM/yyyy");
            fcod = DateTime.Now.ToString("yyyyMMdd");
            informe = textBox3.Text;

            DateTime horaentra = DateTime.Parse(label17.Text);
            tminentrada = (horaentra.Hour * 60 )+ horaentra.Minute;
            doctor = comboBox1.SelectedValue.ToString();
            edad = label16.Text;
            fechaprox = dateTimePicker2.Value.ToString("dd/MM/yyyy");
            fcodprox = dateTimePicker2.Value.ToString("yyyyMMdd");
            proxtrata = textBox9.Text.Trim();

            trataterminado = "NO";
            if (checkBox1.Checked == true) trataterminado = "SI";
        }

        public bool Validacion()
        {
           

        

            if (informe == "")
            {
                MessageBox.Show("Ingrese el tratamiento a realizar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return false;
            }

            return true;
        }

        public bool Existeinfo()
        {
            bool existe;
            conectorSql conecta = new conectorSql();
            string Query = "Select * from NEvolucion where cvpaciente='" + cvpaciente + "' and numturno='" +  numturno+ "' and cvdoctor='" + doctor + "'";
            existe = conecta.ExisteRegistro(Query);
            if (existe == true)
            {
                MessageBox.Show("El turno ya esta atendido por el doctor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            return existe;
        }


        public bool Guardar()
        {
            DateTime Hfinal = DateTime.Now;
            Horatermina = Hfinal.ToString("HH:mm:00");
            tminsalida = (Hfinal.Hour * 60) + Hfinal.Minute;
            tiempodentro = tminsalida - tminentrada;

            conectorSql conecta = new conectorSql();
            string Query = "Insert into NEvolucion(";
            Query = Query + "cvnota";
            Query = Query + ",cvpaciente";
            Query = Query + ",cvdoctor";
            Query = Query + ",numexpediente";
            Query = Query + ",horainicia";
            Query = Query + ",horatermina";
            Query = Query + ",hmininicia";
            Query = Query + ",hmintermina";
            Query = Query + ",ttiempo";
            Query = Query + ",fecha";
            Query = Query + ",fechacod";
            Query = Query + ",emite";
            Query = Query + ",informe";
            Query = Query + ",edad";
            Query = Query + ",fechaprox";
            Query = Query + ",fcodprox";
            Query = Query + ",numturno";
            Query = Query + ",numrecibo)";
            Query = Query + " values(";
            Query = Query + "'" + folionota + "'";
            Query = Query + ",'" + cvpaciente+ "'";
            Query = Query + ",'" + doctor + "'";
            Query = Query + ",'" + numexpediente+ "'";
            Query = Query + ",'" + Horainicia + "'";
            Query = Query + ",'" + Horatermina + "'";
            Query = Query + ",'" + tminentrada + "'";
            Query = Query + ",'" + tminsalida+ "'";
            Query = Query + ",'" + tiempodentro + "'";
            Query = Query + ",'" + fechareg + "'";
            Query = Query + ",'" + fcod + "'";
            Query = Query + ",'" + emite + "'";
            Query = Query + ",'" + informe+ "'";
            Query = Query + ",'" + edad + "'";
            Query = Query + ",'" + fechaprox + "'";
            Query = Query + ",'" + fcodprox+ "'";
            Query = Query + ",'" + numturno + "'";
            Query = Query + ",'" + numrecibog + "')";
            conecta.Excute(Query);

            Query = "Update citas set estatus='ATENDIDO'";
            Query = Query + " , horatermina='" + Horatermina +"'";
            Query = Query + " , hrealinicia='" + Horainicia + "'";
            Query = Query + " , hrealmininicia='" + tminentrada + "'";
            Query = Query + " , hmintermina='" + tminsalida+ "'";
            Query = Query + " , ttiempo='" + tiempodentro+ "'";
            Query = Query + " where cvpaciente='" + cvpaciente + "' and progresivo='" + numturno + "' and cvdoctor='" + doctor + "'";
            conecta.Excute(Query);


            Query = "Insert into CitasProx(cvpaciente,numturno,cvdoctor,fecha,fechacod,realizar,estatus,fechaAgenda,trataterminado,fcodAgenda) values(";
            Query = Query + "'" + cvpaciente + "'";
            Query = Query + ",'0'";
            Query = Query + ",'0'";
            Query = Query + ",'" + fechaprox+ "'";
            Query = Query + ",'" + fcodprox+ "'";
            Query = Query + ",'" + proxtrata + "'";
            Query = Query + ",'CAPTURADO'";
            Query = Query + ",''";
            Query = Query + ",'" + trataterminado + "'";
            Query = Query + ",'')";

            return conecta.Excute(Query);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (BANDERA==true) cargaConsecutivo();
        }

        public void cargaConsecutivo()
        {
            string Numero = "1";
            conectorSql conecta = new conectorSql();
            string Query = "Select notas from consecutivos where notas <>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                Numero = leer["notas"].ToString();
            }
            conecta.CierraConexion();
            label5.Text = Numero;
        }
        public bool actualizaConsecutivo()
        {
            conectorSql conecta = new conectorSql();
            int Siguiente = int.Parse(label5.Text) + 1;
            string Query = "update consecutivos set notas='" + Siguiente.ToString() + "'";
            return conecta.Excute(Query);
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

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) BuscarPaciente();
        }

        public string AREATRATAMIENTO = "";
        public string TIPOEXPEDIENTE = "";
        public string SELEC_CVDoctor = "";
        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedValue = comboBox2.SelectedValue;
            comboBox1.Enabled = false;
            SELEC_CVDoctor = comboBox1.SelectedValue.ToString();
            string usuario = textBox7.Text;
            string contra = textBox8.Text;


            conectorSql conecta = new conectorSql();
            string Query = "Select * from doctores where cvdoctor='" + comboBox2.SelectedValue.ToString() + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                AREATRATAMIENTO = leer["Areatratar"].ToString();
                TIPOEXPEDIENTE = leer["tipoexpediente"].ToString();
            }
            conecta.CierraConexion();

            Query = "Select * from usuarios where cvusuario='" + usuario + "' and  contra='" + contra + "'";
            bool existe = conecta.ExisteRegistro(Query);
            if (existe == false)
            {
                MessageBox.Show("Verifique el usuario o contraseña", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            conecta.CierraConexion();

            comboBox4.Items.Clear();
            if (TIPOEXPEDIENTE == "GINECO")
            {
                comboBox4.Items.Add("ESTUDIO COLPOSCOPICO");
                comboBox4.Items.Add("CONTROL PRENATAL");
                comboBox4.SelectedIndex = 0;
            }

            if (TIPOEXPEDIENTE == "DENTAL")
            {
                comboBox4.Items.Add("EXPEDIENTE DENTAL");
                comboBox4.SelectedIndex = 0;
            }
            
            valoresg.USUARIOSIS = usuario;
            ListadoDeCitas();
            panel3.Visible = false;
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button4_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (Lv.Items.Count == 0)
            {
                MessageBox.Show("Consulte la información del area de tratamiento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ReportesNKB.RinformeAtencionMedica(CONSULTAGENERA, comboBox1.Text.Trim(), dateTimePicker1.Value.ToString("dd/MM/yyyy"));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (TIPOEXPEDIENTE == "GINECO")
            {
                if (comboBox4.SelectedIndex == 0)
                {
                    valoresg.CLAVEPAC = label32.Text;
                    valoresg.BNUMEXPGINECO = label33.Text;
                    EstudioColposcopico estudio = new EstudioColposcopico();
                    estudio.Show();
                }
            }


            if (TIPOEXPEDIENTE == "DENTAL")
            {
                if (comboBox4.SelectedIndex == 0)
                {
                    valoresg.CLAVEPAC = label32.Text;
                    valoresg.BNUMEXPDENTAL = label33.Text;
                    Dental estudio = new Dental();
                    estudio.Show();
                }
            }
            
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
                if (contar > 1) Query = " or cvdoctor='" + leer["cvdoctor"].ToString() + "'";

            }
            conecta.CierraConexion();
            Query = Query + ")";
            return cadenar;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            listView4.Items.Clear();
            listView4.Columns.Clear();
            listView4.Columns.Add("ID Archivo", 50);
            listView4.Columns.Add("Nombre", 100);
            listView4.Columns.Add("Fecha", 90);
            listView4.Columns.Add("emite", 90);
            listView4.Columns.Add("Estatus", 100);
            listView4.Columns.Add("extension", 0);
            listView4.Columns.Add("cvdoctor", 0);
            conectorSql conecta = new conectorSql();

            string cadenaregresa = ConformaQuery();
            string query = "Select top(25) * from archivos where cvpaciente='" + label32.Text  +"'";
            if (cadenaregresa != "") query = query + " and " + cadenaregresa;
            query = query + " order by clave desc";

            //string query = "Select top(20) * from archivos where cvpaciente='" + label32.Text + "' and cvdoctor='" + SELEC_CVDoctor  + "' order by clave desc";
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

            label38.Text = clave;
            label39.Text = nombre;
            label40.Text = extension;
            label44.Text = cvdoctor;
        }

        private void listView4_DoubleClick(object sender, EventArgs e)
        {
            button11_Click(sender, e);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string cadena = ClaseArchivos.EscribirArchivoBytes(label38.Text, label32.Text,label40.Text);

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

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox8.Focus();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = listView1.SelectedIndices;
                foreach (int item in seleccion)
                {
                    DetallesModificaLV1(item);
                }
            }
        }

        public void DetallesModificaLV1(int index)
        {
            string fecha = listView1.Items[index].SubItems[2].Text;
            string nota= listView1.Items[index].SubItems[3].Text;
            label42.Text = fecha;
            textBox1.Text = nota;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            panel5.Visible = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Update citas set estatus='CANCELADO-DOCTOR', observa='" +comboBox5.Text.Trim() + "'";
            Query = Query + " , horatermina='0'";
            Query = Query + " , hrealinicia='0'";
            Query = Query + " , hrealmininicia='0'";
            Query = Query + " , hmintermina='0'";
            Query = Query + " , ttiempo='0'";
            Query = Query + " where cvpaciente='" + cvpaciente + "' and progresivo='" + label45.Text + "' and cvdoctor='" + doctor + "'";
            conecta.Excute(Query);
            MessageBox.Show("Se cancelo la cita del paciente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            panel6.Visible = false;
            ListadoDeCitas();


        }

        private void button10_Click(object sender, EventArgs e)
        {
           if (label47.Text!="" && label47.Text!="0") 
            panel6.Visible = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
        }

    }
}
