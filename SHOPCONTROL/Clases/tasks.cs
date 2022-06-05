using System;
using System.Data.SqlClient;
public  class tasks
    {

    public static string[,] TurnosEnConsulta = null;

    public static string[,] TurnosPorAtender = null;
    public static int TerminoEnConsulta=0;
    public static int TerminoPorAtender = 0;
    public void CitasEnConsulta()
    {
        TerminoEnConsulta = 0;
  

        conectorSql conecta = new conectorSql();
        conectorSql conecta2 = new conectorSql();
        TurnosEnConsulta= null;
        TurnosEnConsulta = new string[20,6];
        for (int i = 0; i < 20; i++)
        {
            TurnosEnConsulta[i, 0] = "";
            TurnosEnConsulta[i, 1] = "";
            TurnosEnConsulta[i, 2] = "";
            TurnosEnConsulta[i, 3] = "";
            TurnosEnConsulta[i, 4] = "";
            TurnosEnConsulta[i, 5] = "";
        }
        string query = "Select cvdoctor from doctores order by cvdoctor asc";
        SqlDataReader leer = conecta.RecordInfo(query);
        int contador = 0;
        while (leer.Read())
        {
            string cvdoctor = leer["cvdoctor"].ToString();
            string consulta = "SELECT TOP (1) doctores.cvdoctor, citas.idturno, citas.nombre as paciente,Doctores.nombre as consultorio,estatusserv,voz,idcita";
            consulta = consulta + " FROM Citas";
            consulta = consulta + " inner join doctores on Doctores.cvdoctor = citas.cvdoctor";
            consulta = consulta + " WHERE(fechacod = '" + DateTime.Now.ToString("yyyyMMdd") + "')";
            consulta = consulta + "  AND(estatus = 'PAGADO')  and estatusserv = 'EN CONSULTA'";
            consulta = consulta + " and Citas.cvdoctor='" + cvdoctor + "'";
            consulta = consulta + " order by idcita desc";
            SqlDataReader leer2 = conecta2.RecordInfo(consulta);
            while (leer2.Read())
            {
                string idturno = leer2["idturno"].ToString();
                string paciente = leer2["paciente"].ToString();
                if (paciente.Contains(".")) paciente = paciente.Replace(".", "");
                string consultorio = leer2["consultorio"].ToString();
                if (consultorio.Contains("C.")) consultorio = consultorio.Replace("C.", "");
                if (idturno.Contains("-")) idturno = idturno.Replace("-", " ");

                string estatus = leer2["estatusserv"].ToString();
                string voz = leer2["voz"].ToString();
                string idcita = leer2["idcita"].ToString();

                TurnosEnConsulta[contador,0] = idturno;
                TurnosEnConsulta[contador,1] = paciente.Trim();
                TurnosEnConsulta[contador,2] = consultorio.Trim();
                TurnosEnConsulta[contador,3] = estatus;
                TurnosEnConsulta[contador,4] = voz;
                TurnosEnConsulta[contador,5] = idcita;

                contador++;
            }
            conecta2.CierraConexion();
            
        }
        conecta.CierraConexion();
        TerminoEnConsulta = 1;
    }



    public void CitasporAtender()
    {
        TerminoPorAtender = 0;

        conectorSql conecta = new conectorSql();
        conectorSql conecta2 = new conectorSql();
        TurnosPorAtender = null;
        TurnosPorAtender = new string[20,6];
        for (int i = 0; i < 20; i++)
        {
            TurnosPorAtender[i, 0] = "";
            TurnosPorAtender[i, 1] = "";
            TurnosPorAtender[i, 2] = "";
            TurnosPorAtender[i, 3] = "";
            TurnosPorAtender[i, 4] = "";
            TurnosPorAtender[i, 5] = "";
        }

        string query = "Select cvdoctor from doctores order by cvdoctor asc";
        SqlDataReader leer = conecta.RecordInfo(query);
        int contador = 0;
        while (leer.Read())
        {
            string cvdoctor = leer["cvdoctor"].ToString();
            string consulta = "SELECT TOP (1) doctores.cvdoctor, citas.idturno, citas.nombre as paciente,Doctores.nombre as consultorio,estatusserv,idcita";
            consulta = consulta + " FROM Citas";
            consulta = consulta + " inner join doctores on Doctores.cvdoctor = citas.cvdoctor";
            consulta = consulta + " WHERE(fechacod = '" + DateTime.Now.ToString("yyyyMMdd") + "')";
            consulta = consulta + "  AND(estatus = 'PAGADO')  and estatusserv = 'POR ATENDER'";
            consulta = consulta + " and Citas.cvdoctor='" + cvdoctor + "'";
            consulta = consulta + " order by idcita asc";
            SqlDataReader leer2 = conecta2.RecordInfo(consulta);
            while (leer2.Read())
            {
                string idturno = leer2["idturno"].ToString();
                string paciente = leer2["paciente"].ToString();
                if (paciente.Contains(".")) paciente = paciente.Replace(".", "");
                if (idturno.Contains("-")) idturno = idturno.Replace("-", " ");

                string consultorio = leer2["consultorio"].ToString();
                if (consultorio.Contains("C.")) consultorio = consultorio.Replace("C.", "");

                string estatus = leer2["estatusserv"].ToString();
                string voz = "1";
                string idcita = leer2["idcita"].ToString();

                TurnosPorAtender[contador,0] = idturno;
                TurnosPorAtender[contador,1] = paciente.Trim();
                TurnosPorAtender[contador,2] = consultorio.Trim();
                TurnosPorAtender[contador,3] = estatus;
                TurnosPorAtender[contador,4] = voz;
                TurnosPorAtender[contador,5] = idcita;

                contador++;
            }
            conecta2.CierraConexion();
          
        }
        conecta.CierraConexion();
        TerminoPorAtender = 1;
    }

}

