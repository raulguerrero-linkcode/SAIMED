using System;
using System.Data.SqlClient;

public class valoresg
{

    public static string UBICACION { get; set; }
    public static string TEXTODESCRIPCION { get; set; }
    public static string DETALLE1 { get; set; }

    public static string USUARIOSIS { get; set; }
    public static string NOMBREUSUARIO { get; set; }

    public static string NUMPEDIDO { get; set; }
    public static string NUMPEDIDOCARGAR { get; set; }
    public static string AYOPEDIDO { get; set; }

    public static string VENTANAPAGO { get; set; }
    public static string VENTTOTAL { get; set; }
    public static string VENTNUMPEDIDO { get; set; }

    public static string SOLICITUDTIMBRES { get; set; }

    public static string VIENEBUSQUEDAPEDIDO { get; set; }

    public static string CVREPORTE { get; set; }

    public static string TOTALPEDIDO { get; set; }
    public static string NUMPEDIDOREGISTRAR { get; set; }


    public static string VIENEBUSQUEDARECIBO { get; set; }

    public static string BANDIMAGEN { get; set; }
    public static string EXPEDIENTE { get; set; }
    public static string NUMPRODUCTOSURTIR { get; set; }
    public static string CLAVEPAC { get; set; }


    public static int CONSECUTIVOPACIENTE { get; set; }

    public static string AGENDA_FCITAPROX { get; set; }
    public static string AGENDA_RECIBO { get; set; }
    public static string AGENDA_CVPACIENTE { get; set; }

    public static string CVPACIENTECITAR { get; set; }
    public static string NUMRECIBOREG { get; set; }


    public static string BNUMEXPDENTAL { get; set; }
    public static string BNUMEXPGINECO { get; set; }
    public static string BNUMEXPOFTAMO { get; set; }

    public static string Reagendar_cvpaciente { get; set; }
    public static string Reagendar_cvdoctor { get; set; }
    public static string Reagendar_fecha { get; set; }
    public static string Reagendar_numRecibo { get; set; }
    public static string Reagendar_servicio { get; set; }
    public static string Reagendar_cvservicio { get; set; }
    public static string Reagendar_estatus { get; set; }
    public static string Reagendar_Nompaciente { get; set; }
    public static string Reagendar_NomArea { get; set; }

    public static string Area_Cvdoctor { get; set; }
    public static string Area_Contra { get; set; }
    public static string Area_usuario { get; set; }
    public static string Nombre_Completo { get; set; }
    public static string IdEmployee { get; set; }
    public static string CURP { get; set; }
    public static string SERVICIONOMBRE { get; set; }
    public static string FECHACITA { get; set; }

    public static string EmpEmail { get; set; }

    public static string SERVER_LOCATION { get; set; }

    public static void Bitacora(string emitio, string realizo, string modulo)
    {
        conectorSql conecta = new conectorSql();
        string Query = "Insert into bitacora(emitio,realizo,modulo,fecha,fechacod,hora)";
        Query = Query + " values(";
        Query = Query + "'" + emitio + "'";
        Query = Query + ",'" + realizo + "'";
        Query = Query + ",'" + modulo + "'";
        Query = Query + ",'" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
        Query = Query + ",'" + DateTime.Now.ToString("yyyyMMdd") + "'";
        Query = Query + ",'" + DateTime.Now.ToString("HH:mm:ss") + "')";
        conecta.Excute(Query);
        conecta.CierraConexion();
    }

    public static void AlinearPagosNoEncontrados(string Fechacod)
    {
        conectorSql conecta = new conectorSql();
        conectorSql conecta2 = new conectorSql();
        string Query = "Select numrecibo,vendedor,hora,horacod from recibos where fechacod='" + Fechacod + "' order by numrecibo asc";
        SqlDataReader leer = conecta.RecordInfo(Query);
        while (leer.Read())
        {
            string numrecibo = leer["numrecibo"].ToString();
            string vendedor = leer["vendedor"].ToString();
            string hora = leer["hora"].ToString();
            string horacod = leer["horacod"].ToString();
            if (numrecibo == "6048")
                hora = hora;

            string consulta = "Select * from pagos where numpedido='" + numrecibo + "' and bandera='1'";
            bool existe = conecta2.ExisteRegistro(consulta);
            conecta2.CierraConexion();
            if (existe == false)
            {
                string cvcliente = "";
                string numpedido = numrecibo;
                string cantidad = "0";
                string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                string fechacod = DateTime.Now.ToString("yyyyMMdd");
                string Horapago = DateTime.Now.ToString("hh:mm:ss");
                string concepto = "PAGO AL PEDIDO NUM " + numrecibo;
                string cvconcepto = "4";
                string remisionHist = numrecibo;
                string estatus = "PAGADO";
                string fechapago = DateTime.Now.ToString("dd/MM/yyyy");
                string fcodpago = DateTime.Now.ToString("yyyyMMdd");
                string emitiopago = valoresg.USUARIOSIS;
                string pagocon = "EFECTIVO";
                string bandera = "1";
                pagocon = "EFECTIVO";
                string observacion = "";
                string numremision = numrecibo;
                string ayo = DateTime.Now.Year.ToString();
                string mes = DateTime.Now.Month.ToString();
                string numRecibo = numrecibo;
                string tipopago = pagocon;
                string observa = "0";
                string numpago = "0";

                string query = "insert into pagos (cvcliente";
                query = query + ", numpedido";
                query = query + ",cantidad";
                query = query + ",fecha";

                query = query + ",fechacod";
                query = query + ",concepto";
                query = query + ",cvconcepto";
                query = query + ",remisionHist";
                query = query + ",estatus";
                query = query + ",fechapago";
                query = query + ",fcodpago";
                query = query + ",emitiopago";
                query = query + ",pagocon";
                query = query + ",observacion";
                query = query + ",numremision";
                query = query + ",ayo";
                query = query + ",mes";
                query = query + ",numRecibo";
                query = query + ",tipopago";
                query = query + ",observa";
                query = query + ",bandera";
                query = query + ",Horapago";
                query = query + ",numpago) values(";
                query = query + "'" + cvcliente + "'";
                query = query + ",'" + numpedido + "'";
                query = query + ",'" + cantidad + "'";
                query = query + ",'" + fecha + "'";
                query = query + ",'" + fechacod + "'";
                query = query + ",'" + concepto + "'";
                query = query + ",'" + cvconcepto + "'";
                query = query + ",'" + remisionHist + "'";
                query = query + ",'" + estatus + "'";
                query = query + ",'" + fechapago + "'";
                query = query + ",'" + fcodpago + "'";
                query = query + ",'" + emitiopago + "'";
                query = query + ",'" + pagocon + "'";
                query = query + ",'" + observacion + "'";
                query = query + ",'" + numremision + "'";
                query = query + ",'" + ayo + "'";
                query = query + ",'" + mes + "'";
                query = query + ",'" + numRecibo + "'";
                query = query + ",'" + tipopago + "'";
                query = query + ",'" + observa + "'";
                query = query + ",'" + bandera + "'";
                query = query + ",'" + Horapago + "'";
                query = query + ",'" + numpago + "')";
                conecta2.Excute(query);

                Bitacora("BILL LINE", "INGRESADO EN PAGOS EL RECIBO" + numrecibo + " CON CANTIDAD DE 0", "PAGOS");
            }

        }
        conecta.CierraConexion();

    }
    //public static string HISTORIAL_D_PÀCIENTE { get; set; }
}
