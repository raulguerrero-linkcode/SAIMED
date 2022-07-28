using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SHOPCONTROL.RolesAndUsers
{
    public partial class UsersAndRoles : Form
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

        public UsersAndRoles()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UsersAndRoles_Load(object sender, EventArgs e)
        {

            LoadAppRoles();
            blockUsersDataTxt();
            LimpiarChecks();
            button3.Enabled = false;
            button1.Enabled = false;

        }

        public void showUsersDataTxt()
        {
            IdEmployee.Enabled = true;
            EmpName.Enabled = true;
            EmpApPat.Enabled = true;
            EmpApMat.Enabled = true;
            EmpEmail.Enabled = true;
            TipoUsuario.Enabled = true;

        }

        public void blockUsersDataTxt()
        {
            IdEmployee.Enabled = false;
            EmpName.Enabled = false;
            EmpApPat.Enabled = false;
            EmpApMat.Enabled = false;
            EmpEmail.Enabled = false;
            TipoUsuario.Enabled = false;

        }

        public void LoadAppRoles()
        {
            //blockUsersDataTxt();
            // Cargar los usuarios (empleados) activos
            newRole.Visible = false;
            newRoleLabel.Visible = false;
            /// IdEmployee.Enabled = false;

            string cvcliente = "";
            Lv.Items.Clear();
            Lv.Columns.Clear();
            Lv.Columns.Add("IdEmployee", 80);
            Lv.Columns.Add("Name", 80);
            Lv.Columns.Add("FirstLastName", 100);
            Lv.Columns.Add("SecondLastName", 100);
            Lv.Columns.Add("Email", 120);
            Lv.Columns.Add("Role", 80);

            Lv.BeginUpdate();
            conectorSql conecta = new conectorSql();

            string SQLStr = "SELECT IdEmployee,Name,FirstLastName,SecondLastName, Email, Role FROM UsersManagement;";

            SqlDataReader leer = conecta.RecordInfo(SQLStr);
            while (leer.Read())
            {

                cvcliente = leer["IdEmployee"].ToString();
                ListViewItem lvi = new ListViewItem(cvcliente);

                lvi.SubItems.Add(leer["Name"].ToString());
                lvi.SubItems.Add(leer["FirstLastName"].ToString());
                lvi.SubItems.Add(leer["SecondLastName"].ToString());
                lvi.SubItems.Add(leer["Email"].ToString());
                lvi.SubItems.Add(leer["Role"].ToString().Trim());


                Lv.Items.Add(lvi);
                lvi.UseItemStyleForSubItems = false;

            }
            // conecta.CierraConexion();

            Lv.EndUpdate();

            TipoUsuario.Items.Clear();


            // Load The Role
            SQLStr = "SELECT RoleId, RoleName AS RoleName FROM Roles;";

            SqlDataReader leer1 = conecta.RecordInfo(SQLStr);
            while (leer1.Read())
            {
                TipoUsuario.Items.Add(leer1["RoleName"].ToString());
            }

            TipoUsuario.Items.Add("Nuevo tipo de usuario...");

            conecta.CierraConexion();

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
            // comboBox2.Visible = false;
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


        private void Lv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Lv_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            LimpiarChecks();
            IdEmployee.Text = "";
            EmpApPat.Text = "";
            EmpApMat.Text = "";
            EmpEmail.Text = "";
            EmpName.Text = "";

            if (Lv.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection seleccion = Lv.SelectedIndices;
                foreach (int item in seleccion)
                {
                    // id del usuario
                    IdEmployee.Text = Lv.Items[item].SubItems[0].Text;

                }
            }

            conectorSql conecta = new conectorSql();
            string srtQuery = "SELECT IdEmployee, Email, Name as EmpName, FirstLastName, SecondLastName,Role,[entra1],[entra2],[entra3],[entra4],[entra5],[entra6],[entra7],[entra8],[entra9],[entra10],[entra11],[entra12],[entra13],[entra14],[entra15],[entra16],[entra17],[entra18],[entra19],[entra20],[entra21],[entra22],[entra23],[entra24],[entra25],[entra26],[entra27],[entra28],[entra29],[entra30],[ESDOCTOR],[CVDOCTOR],[entra31],[entra32],[entra33] from UsersManagement where IdEmployee = " + IdEmployee.Text;
            SqlDataReader leer = conecta.RecordInfo(srtQuery);
            while (leer.Read())
            {
                IdEmployee.Enabled = true;
                IdEmployee.Text = leer["IdEmployee"].ToString();
                EmpName.Text = leer["EmpName"].ToString();

                EmpApPat.Text = leer["FirstLastName"].ToString();

                EmpApMat.Text = leer["SecondLastName"].ToString();

                EmpEmail.Text = leer["Email"].ToString();

                TipoUsuario.Text = leer["role"].ToString();

                IdEmployee.Enabled = false;
                CVDOCTOR = leer["cvdoctor"].ToString();

                ENTRA1 = leer["entra1"].ToString().Trim();
                ENTRA2 = leer["entra2"].ToString().Trim();
                ENTRA3 = leer["entra3"].ToString().Trim();
                ENTRA4 = leer["entra4"].ToString().Trim();
                ENTRA5 = leer["entra5"].ToString().Trim();
                ENTRA6 = leer["entra6"].ToString().Trim();
                ENTRA7 = leer["entra7"].ToString().Trim();
                ENTRA8 = leer["entra8"].ToString().Trim();
                ENTRA9 = leer["entra9"].ToString().Trim();
                ENTRA10 = leer["entra10"].ToString().Trim();
                ENTRA11 = leer["entra11"].ToString().Trim();
                ENTRA12 = leer["entra12"].ToString().Trim();
                ENTRA13 = leer["entra13"].ToString().Trim();
                ENTRA14 = leer["entra14"].ToString().Trim();
                ENTRA15 = leer["entra15"].ToString().Trim();
                ENTRA16 = leer["entra16"].ToString().Trim();
                ENTRA17 = leer["entra17"].ToString().Trim();
                ENTRA18 = leer["entra18"].ToString().Trim();
                ENTRA19 = leer["entra19"].ToString().Trim();
                ENTRA20 = leer["entra20"].ToString().Trim();
                ENTRA21 = leer["entra21"].ToString().Trim();
                ENTRA22 = leer["entra22"].ToString().Trim();
                ENTRA23 = leer["entra23"].ToString().Trim();
                ENTRA24 = leer["entra24"].ToString().Trim();
                ENTRA25 = leer["entra25"].ToString().Trim();
                ENTRA26 = leer["entra26"].ToString().Trim();
                ENTRA27 = leer["entra27"].ToString().Trim();
                ENTRA28 = leer["entra28"].ToString().Trim();
                ENTRA29 = leer["entra29"].ToString().Trim();
                ENTRA30 = leer["entra30"].ToString().Trim();
                ESDOCTOR = leer["ESDOCTOR"].ToString().Trim();

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

            button3.Enabled = true;
            showUsersDataTxt();
            IdEmployee.Enabled = false;
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IdEmployee.Text.Length <= 1)
            {
                MessageBox.Show("Nada que guardar");
                return;
            }

            if (EmpName.Text.Length <= 1)
            {
                MessageBox.Show("Falta el nombre");
                return;
            }

            if (EmpApPat.Text.Length <= 1)
            {
                MessageBox.Show("Falta apellido paterno");
                return;
            }

            if (EmpApMat.Text.Length <= 1)
            {
                MessageBox.Show("Falta apellido materno");
                return;
            }

            if (EmpEmail.Text.Length <= 1)
            {
                MessageBox.Show("Falta correo electrónico");
                return;
            }

            if (TipoUsuario.Text.Length <= 1)
            {
                MessageBox.Show("Falta el rol del usuario");
                return;
            }

            button1.Enabled = false;

            conectorSql conecta = new conectorSql();

            Recolecta();

            // Delete information and update the current
            string SqlDelete = "Delete from UsersManagement where IdEmployee=" + IdEmployee.Text;
            conecta.Excute(SqlDelete);

            string Query = "Insert into UsersManagement (IdEmployee, Name, FirstLastName, SecondLastName, Email, Role";
            Query = Query + ",entra1";
            Query = Query + ",entra2";
            Query = Query + ",entra3";
            Query = Query + ",entra4";
            Query = Query + ",entra5";
            Query = Query + ",entra6";
            Query = Query + ",entra7";
            Query = Query + ",entra8";
            Query = Query + ",entra9";
            Query = Query + ",entra10";
            Query = Query + ",entra11";
            Query = Query + ",entra12";
            Query = Query + ",entra13";
            Query = Query + ",entra14";
            Query = Query + ",entra15";
            Query = Query + ",entra16";
            Query = Query + ",entra17";
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
            Query = Query + "'" + IdEmployee.Text + "',";
            Query = Query + "'" + EmpName.Text + "',";
            Query = Query + "'" + EmpApPat.Text + "',";
            Query = Query + "'" + EmpApMat.Text + "',";
            Query = Query + "'" + EmpEmail.Text + "',";

            string tipoUsuario;

            if (newRole.Text=="")
            {
                tipoUsuario = TipoUsuario.Text.Trim();
            } else
            {
                tipoUsuario = newRole.Text.Trim();
            }

            Query = Query + "'" + tipoUsuario.Trim()  + "'";
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
            Query = Query + ",'" + CVDOCTOR + "'";
            Query = Query + ",'" + ENTRA30 + "')";
            conecta.Excute(Query);


            Query = "";
            Query = "insert into Roles (RoleName) values ('" + tipoUsuario.Trim() + "')";
            conecta.Excute(Query);

            




            conecta.CierraConexion();

            LoadAppRoles();
            IdEmployee.Text = "";
            EmpApPat.Text = "";
            EmpApMat.Text = "";
            EmpEmail.Text = "";
            EmpName.Text = "";
            TipoUsuario.Text = "";
            LimpiarChecks();
            newRole.Visible = false;
            newRoleLabel.Visible = false;
            IdEmployee.Enabled = false;
            button3.Enabled = true;
            
            blockUsersDataTxt();

            MessageBox.Show("Usuario creado y/o actualizado");
        }


        public void Recolecta()
        {
            USUARIO = newRole.Text;

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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IdEmployee.Text=="")
            {
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Está seguro de realizar la eliminación del usuario " + EmpName.Text + "?  ésta acción no se puede revertir", "Eliminar usuario " + IdEmployee.Text, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                conectorSql conecta = new conectorSql();

                Recolecta();

                // Delete information and update the current
                string SqlDelete = "Delete from UsersManagement where IdEmployee=" + IdEmployee.Text;
                conecta.Excute(SqlDelete);
                conecta.CierraConexion();
                LoadAppRoles();
                IdEmployee.Text = "";
                EmpApPat.Text = "";
                EmpApMat.Text = "";
                EmpEmail.Text = "";
                EmpName.Text = "";
                TipoUsuario.Text = "";
                LimpiarChecks();
                IdEmployee.Enabled = true;
                blockUsersDataTxt();
                button1.Enabled = false;
                MessageBox.Show("Usuario eliminado");   
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showUsersDataTxt();

            LoadAppRoles();
            IdEmployee.Text = generateNumber().ToString();
            IdEmployee.Enabled = false;
            EmpApPat.Text = "";
            EmpApMat.Text = "";
            EmpEmail.Text = "";
            EmpName.Text = "";
            TipoUsuario.Text = "";
            LimpiarChecks();
            button3.Enabled = false;
            button1.Enabled = true;

        }

        private long generateNumber() {

            Random rnd = new Random();
            return rnd.Next(1000, 999999);

        }

        private void checkBox31_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox31.Checked)
            {
                ActivarChecks();
            } else
            {
                LimpiarChecks();
            }
        }

        private void TipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TipoUsuario.Text== "Nuevo tipo de usuario...")
            {
                newRole.Visible = true;
                newRoleLabel.Visible = true;
            } else
            {
                newRole.Visible = false;
                newRoleLabel.Visible = false;
            }
        }
    }
}