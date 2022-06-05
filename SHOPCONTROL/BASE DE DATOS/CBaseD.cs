using System;
using System.Data.SqlClient;
using System.Windows.Forms;

class CBaseD
    {

        private static SqlConnection con;
        private static SqlCommand comm;
        private static SqlConnection conCad;
        public  static Boolean CREADABASE;
        private static void Abrirconexion()
        {
            con = new SqlConnection();
            string miValor = Registro.ReadRegSHOPCONTROL("CON", "CCliente");
            con.ConnectionString = miValor;

            con.Open();
        }



        private static void CierraConexion()
        {
            con.Close();
        }

        private static bool Excute(String Ejecuta)
        {
            try
            {
                Abrirconexion();
                comm = new SqlCommand(Ejecuta, con);
                comm.ExecuteNonQuery();
                CierraConexion();
                return true;
            }
            catch (SqlException exx)
            {
                CierraConexion();
                return false;
            }
        }


        private static string[] instanciasInstaladas()
        {
            Microsoft.Win32.RegistryKey rk;
            rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server", false);
            string[] s;
            s = ((string[])rk.GetValue("InstalledInstances"));
            return s;
        }

        public static void CargaInstancias(ComboBox Listado)
        {
            string[] instancias;
            instancias = instanciasInstaladas();
            if (Listado.Items.Count>0) Listado.Items.Clear();
            foreach (string s in instancias)
            {
                if (s == "MSSQLSERVER")
                {
                    Listado.Items.Add("(local)");
                }
                else
                {
                    Listado.Items.Add(@"(local)\" + s);
                }
            }
            Listado.Text = "(local)";
        }
        //primero crear base sin usuario despues asignar un usuario a la base de datos
        public static bool CrearBase(string NombreBase, string NServidor) 
        {
            try
            {
                string CadenaConexion = "Data Source=" + NServidor + ";Integrated Security=True";
                con = new SqlConnection();
                con.ConnectionString = CadenaConexion;
                con.Open();

                string s = "CREATE DATABASE " + NombreBase;
                comm = new SqlCommand(s, con);
                comm.ExecuteNonQuery();
                con.Close();
                comm = null;
                CREADABASE = true;
                return true;
            }
            catch (SqlException e)
            {
                if (e.Number==1801)CREADABASE = false;
                return false;
            }
        }

        public static bool CrearUsuarioBD(string NombreBase, string NServidor)
        {
            string CadenaConexion = "Data Source=" + NServidor + "; initial catalog = " + NombreBase + ";Integrated Security=True";
            con = new SqlConnection();
            con.ConnectionString = CadenaConexion;
            con.Open();

            string s = "exec sp_addlogin 'usia', 'SIA123'";
            comm = new SqlCommand(s, con);
            comm.ExecuteNonQuery();
            comm = null;

            s = "exec sp_grantdbaccess 'usia'";
            comm = new SqlCommand(s, con);
            comm.ExecuteNonQuery();
            comm = null;

            s = "exec sp_addrolemember 'db_owner','SIA123'";
            comm = new SqlCommand(s, con);
            comm.ExecuteNonQuery();
            comm = null;
            con.Close();
            
            return true;
        }

    }

