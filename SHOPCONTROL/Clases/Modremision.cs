using System;
public static class Modremision
    {

    public static DateTime FECHAQUINCENA1;
    public static DateTime FECHAQUINCENA2;
    
    public static string BASEDATOS { get; set; }
    public static string SERVER { get; set; }
    public static string USUARIO { get; set; }
    public static string CONTRASEÑA { get; set; }


    public static string NOMBREACCEDE { get; set; }
    public static string EMITE { get; set; }

    public static string CVCLIENTE{ get; set; }
    public static string CVPRODUCTO { get; set; }

    public static string CANCELANUMRECIBO { get; set; }
   

    //public static void GuardaBitacora(string cvempleado,string modulo, string Antes, string Despues)
    //{
    //    conectorSql conecta = new conectorSql();
    //    string Query = "Insert into bitacora(cvempleado,fechacod,fecha,hora,cvusuario,modulo,antes,cambio)";
    //    Query = Query + "'" + cvempleado + "','" + DateTime.Now.ToString("yyyyMMdd") + "','";
    //    Query = Query + "'" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
    //    Query = Query + ",'" + DateTime.Now.ToString("HH:mm:ss") + "'";
    //    Query = Query + ",'" + VarGlobales.USUARIO + "'";
    //    Query = Query + ",'" + modulo + "'";
    //    Query = Query + ",'" + Antes+ "'";
    //    Query = Query + ",'" + Despues + "')";

    //    conecta.Excute(Query);
    //}




    }
