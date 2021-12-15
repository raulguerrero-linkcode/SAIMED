using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Windows.Forms;

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

    public  SqlConnection con;
    public SqlCommand comm;

    public void Abrirconexion()
    {
        con = new SqlConnection();

        con.ConnectionString = @"Data Source=MVELAZQUEZ;Initial Catalog=EVA2;UID=sa;PWD=eva123";

            //ConfigurationManager.ConnectionStrings["ConexionTAGS"].ConnectionString;
            
            
         //"Data Source=MAURICIO;Initial Catalog=MYANI;Integrated Security=True"; //mi maquina local
         //"Data Source=174.132.243.202;Initial Catalog=control;UID=userlic;PWD=Pa$$f3"; // Servidor Fingerchec.mx
         //"Data Source=174.132.243.202;Initial Catalog=controls;UID=ctrl;PWD=C0ntr0l"; // Servidor GMRobotic
        //"Data Source=174.132.243.202;Initial Catalog=control;UID=userlic;PWD=Pa$$f3"; // Servidor Fingerchec.mx

        con.Open();
    }

    public void CierraConexion()
    {
        con.Close();    
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
            
            MessageBox.Show(ex.Message);
           CierraConexion();
           return false;
        }
    }
    public bool PruebaConexionCadena(string Cadena)
    {
        try
        {
            con = new SqlConnection();
            con.ConnectionString = Cadena;
            con.Open();
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
            Abrirconexion();
            da = new SqlDataAdapter(cadena, con);
            da.Fill(dt);
            da.Dispose();
            CierraConexion();
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
        SqlDataReader lea;
        try
        {
            Abrirconexion();
            comm = new SqlCommand(cadena, con);
            lea = comm.ExecuteReader();
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
            Abrirconexion();
            comm = new SqlCommand(Ejecuta, con);
            comm.ExecuteNonQuery();
            CierraConexion();
            return true;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
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
            MessageBox.Show(ex.Message);
            CierraConexion();
            return false;
        }
    }

}
