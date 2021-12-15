using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
/// <summary>
/// Summary description for Conexion
/// </summary>
public class Conexion
{
	public Conexion()
	{
		//
		// TODO: Add constructor logic here
		//
	}

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


    public Conexion(string CadenaConecta)
    {
        CADENACONEXION = CadenaConecta;
    }

    public SqlConnection conCad;
    public SqlConnection con;
    public SqlCommand comm;
    public string CADENACONEXION = "";
    public void Abrirconexion()
    {
        con = new SqlConnection();
        string miValor = Registro.ReadRegSHOPCONTROL("CON", "CCliente");
        string opcionserver = Registro.ReadRegSHOPCONTROL("CON", "OPCIONSERVER");
        if (opcionserver == "0") miValor = Registro.ReadRegSHOPCONTROL("CONCUERNAVACA", "CCliente");
        if (opcionserver == "1") miValor = Registro.ReadRegSHOPCONTROL("CONMOLINA", "CCliente");
        if (opcionserver == "2") miValor = Registro.ReadRegSHOPCONTROL("CONBELLASARTES", "CCliente");

        CADENACONEXION = miValor;
        if (CADENACONEXION == "")
            con.ConnectionString = @"Data Source=TEZOYUCA-PC\MSQLAGUA;Initial Catalog=AGUA;UID=sa;PWD=sicapez123";
        else
            con.ConnectionString = CADENACONEXION;

        con.Open();
    }

    public void CierraConexion()
    {
        con.Close();
    }


    public void EnvioMensaje(string mensaje)
    {
        string path = @"C:\log.txt";
        TextWriter tw = new StreamWriter(path, true);
        tw.WriteLine(mensaje);
        tw.Close();

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

            EnvioMensaje(ex.Message);
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
            Abrirconexion();
            da = new SqlDataAdapter(cadena, con);
            da.Fill(dt);
            da.Dispose();
            CierraConexion();
        }
        catch (Exception ex)
        {
            EnvioMensaje(ex.Message);
            CierraConexion();
            return null;
        }
        return dt;
    }

    public SqlDataReader RecordInfo(String cadena)
    {
        SqlDataReader lea;
        try
        {
            Abrirconexion();
            comm = new SqlCommand(cadena, con);
            lea = comm.ExecuteReader();
        }
        catch (Exception ex)
        {
            EnvioMensaje( "Aplicacion de error " + cadena + " " + ex.Message);
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
            comm = new SqlCommand(Ejecuta, con);
            comm.ExecuteNonQuery();
            CierraConexion();
            return true;
        }
        catch (Exception e)
        {
            EnvioMensaje(e.Message);
            CierraConexion();
            return false;
        }
    }

    public bool ExisteRegistro(String Query)
    {
        SqlDataReader leer;
        int cuantos = 0;
        try
        {
            Abrirconexion();
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
        catch (Exception ex)
        {
            EnvioMensaje(ex.Message);
            CierraConexion();
            return false;
        }
    }

}
