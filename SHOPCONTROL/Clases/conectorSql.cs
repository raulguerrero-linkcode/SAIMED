using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using System.Linq;

/// <summary>
/// Descripción breve de conectorSql
/// </summary>
public class conectorSql
{
	public conectorSql()
	{
        //
		// TODO: Agregar aquí la lógica del constructor
		//
	}


    public SqlConnection conCad;
    public SqlConnection con;
    public SqlCommand comm;

    public string CADENACONEXION="";
    
    public bool Abrirconexion()
    {
        con = new SqlConnection();

        string cfnFile = @"\\SRV-DATACENTER\tmp\EmailConf.xml";
        bool cfnExist = File.Exists(cfnFile);
        XDocument xdoc = XDocument.Load(cfnExist ? @"\\SRV-DATACENTER\tmp\EmailConf.xml" : @"C:\tmp\EmailConf.xml");
       //XDocument xdoc = XDocument.Load("./EmailConf.xml");
        string vserver = xdoc.Descendants("ConnStr").First().Value;
        con.ConnectionString = @vserver;
        CADENACONEXION = @vserver;


        try
        {
            con.Open();
            return true;
        }
        catch (SqlException ex)
        {
            if (ex.Number == 53)
            {
                con.Close();
                return false;
            }
        }
        return true;
    }

    public void CierraConexion()
    {
        con.Close();    
    }
    
    //--- prueba para saber si esta abierta la conexion

    public bool PruebaConexionCadena(string cadena)
    {
        try
        {
            AbrirconexionCadena(cadena);
            conCad.Close();
            return true;
        }
        catch (Exception ex)
        {
            conCad.Close();
            return false;
        }
    }

    public void AbrirconexionCadena(string Cadena)
    {
        conCad = new SqlConnection();

        conCad.ConnectionString = Cadena;
        conCad.Open();
    }


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
           
            MessageBox.Show(ex.Message);
           CierraConexion();
           return false;
        }
    }

    public DataTable Lectura(String cadena)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da;

        try
        {
            if (Abrirconexion())
            {
                da = new SqlDataAdapter(cadena, con);
                da.Fill(dt);
                da.Dispose();
                CierraConexion();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            CierraConexion();
            return null;
        }
        return dt;
    }

    public SqlDataReader RecordInfo(String cadena)
    {
        SqlDataReader lea=null;
        try
        {

            if (Abrirconexion())
            {
                comm = new SqlCommand(cadena, con);
                comm.CommandTimeout = 0;
                lea = comm.ExecuteReader();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            CierraConexion();
            return null;
        }
        return lea;
    }

    public bool Excute(String Ejecuta)
    {
        try
        {
            if (Abrirconexion())
            {
                comm = new SqlCommand(Ejecuta, con);
                comm.ExecuteNonQuery();
            }
            return true;
        }
        catch (Exception e)
        {
            // MessageBox.Show(e.Message);
            CierraConexion();
            return false;
        }    
    }

    public bool Excute2(String Ejecuta)
    {
        try
        {
            if (Abrirconexion())
            {
                comm = new SqlCommand(Ejecuta, con);
                comm.ExecuteNonQuery();
            }
            return true;
        }
        catch (Exception e)
        {
            CierraConexion();
            return false;
        }


    }

    public bool ExisteRegistro(String Query)
    {
        SqlDataReader leer;
        int cuantos=0;
        try
        {
            if (Abrirconexion())
            {
                comm = new SqlCommand(Query, con);
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
            return true;
            }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            CierraConexion();
            return false;
        }
    }

}
