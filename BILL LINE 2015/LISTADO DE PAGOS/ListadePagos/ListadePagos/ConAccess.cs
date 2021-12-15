using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Runtime.InteropServices;


  class ConAccess
    {
        public  OleDbConnection  con;
        public OleDbCommand comm;
        private string CadenaConexion;
        private Registro reg = new Registro();
        
        public ConAccess()
        {
            CadenaConexion = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory.ToString() + "Biopagos.mdb;Persist Security Info=False";
        }

        public ConAccess(string llave)
        {

            if (llave == "MARCAJES")
            { 
              CadenaConexion=@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory.ToString() + "marcajes.mdb;Persist Security Info=False";
            }

         }

        public ConAccess(bool bandera, string DirBdAccess)
        {
            if (bandera)
            {
                CadenaConexion = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DirBdAccess + ";Persist Security Info=False";
            }

        }

        public void Abrirconexion()
        {
            con = new OleDbConnection();
            con.ConnectionString = CadenaConexion;
            con.Open();
        }

        public void CierraConexion()
        {
            con.Close();
            con.Dispose();
        }

        //--- prueba para saber si esta abierta la conexion
        public bool PruebaConexion()
        {
            try
            {
                Abrirconexion();
                CierraConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CierraConexion();   
                return false;
            }
        }

        public DataTable Lectura(String cadena)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da;

            try
            {
                Abrirconexion();
                da = new OleDbDataAdapter(cadena, con);
                da.Fill(dt);
                da.Dispose();
                CierraConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CierraConexion();
                return null;
            }
            return dt;
        }

        public OleDbDataReader RecordInfo(String cadena)
        {
            OleDbDataReader lea;
            try
            {
                Abrirconexion();
                comm = new OleDbCommand(cadena, con);
                lea = comm.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CierraConexion();
                return null;
            }
            return lea;
        }

        public bool Excute(String Ejecuta)
        {
            try
            {
                Abrirconexion();
                comm = new OleDbCommand(Ejecuta, con);
                comm.ExecuteNonQuery();
                CierraConexion();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CierraConexion();
                return false;
            }
        }

        public bool ExisteRegistro(String Query)
        {
            OleDbDataReader leer;
            int cuantos = 0;
            try
            {
                Abrirconexion();
                comm = new OleDbCommand(Query, con);
                leer = comm.ExecuteReader();
                while (leer.Read())
                {
                    cuantos++;
                }
                if (cuantos == 0)
                {
                    leer.Close();
                    CierraConexion();
                    return false;
                }
                else
                {
                    leer.Close();
                    CierraConexion();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CierraConexion();
                return false;
            }
        }

    }

