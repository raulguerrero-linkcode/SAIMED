using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;

using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;


    class Reportespdf
    {
        public string ReporteRecibo(string Numrecibo, string ayo)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;
            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ReciboM "+   Numrecibo +" .pdf";
            //System.IO.File.Delete(CadenaDireccion);

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 10;
            int Totalfilas = 50;

            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);

            formatopdf.IngresaTextoBarraAbajoNegrita("NUM RECIBO. " + Numrecibo, 10);


            // formatopdf.FuenteNum(8);

            string Query = "select recibos.numrecibo, recibos.nombrerecibo as nombre, ";
            Query = Query + " clientes.cvcliente, ";
            Query = Query + " clientes.telefono, ";
            Query = Query + " clientes.email, ";
            Query = Query + " recibos.direccion, ";
            Query = Query + " clientes.rfc, ";
            Query = Query + " recibos.colonia, ";
            Query = Query + " recibos.tiporecibo, ";
            Query = Query + " recibos.aproxpeso, ";
            Query = Query + " clientes.factura, ";
            Query = Query + " clientes.empresa, ";
            Query = Query + " recibos.fecha, ";
            Query = Query + " recibos.notas, ";
            Query = Query + " recibos.total, ";
            Query = Query + " recibos.iva, recibos.totalgeneral,";
            Query = Query + " recibos.emitio, ";
            Query = Query + " recibos.totalletra, ";
            Query = Query + " recibos.numcuenta ";
            Query = Query + " from recibos ";
            Query = Query + " inner join clientes on clientes.cvcliente=recibos.cvcliente";

            Query = Query + "  where recibos.numrecibo='" + Numrecibo + "' ";
            Query = Query + "  and recibos.ayo='" + ayo + "'";
            string observa = "";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string cvcliente = leer["cvcliente"].ToString();
                string nombre = leer["nombre"].ToString();
                string correo = leer["email"].ToString();
                string telefono = leer["telefono"].ToString();
                string direccion = leer["direccion"].ToString();

                string empresa = leer["empresa"].ToString();
                string rfc = leer["rfc"].ToString();
                string factura = leer["factura"].ToString();
                string Fecha = leer["Fecha"].ToString();
                string total = leer["total"].ToString();
                string iva = leer["iva"].ToString();
                string totalgeneral = leer["totalgeneral"].ToString();
                string cantletra = leer["totalletra"].ToString();
                string colonia= leer["colonia"].ToString();
                string tiporecibo= leer["tiporecibo"].ToString();
                string pesoaprox= leer["aproxpeso"].ToString();

                observa = leer["notas"].ToString();


                formatopdf.IngresaTexto("ID Cliente:", 2);
                formatopdf.IngresaTexto(cvcliente, 4);
                formatopdf.IngresaTexto("E-mail:", 1);
                formatopdf.IngresaTexto(correo, 3);

                formatopdf.IngresaTexto("Nombre Comercial:", 2);
                formatopdf.IngresaTexto(empresa, 4);
                formatopdf.IngresaTexto("Telefono:", 1);
                formatopdf.IngresaTexto(telefono, 3);

                formatopdf.IngresaTexto("Nombre:", 2);
                formatopdf.IngresaTexto(nombre, 8);

                formatopdf.IngresaTexto("Dirección:", 2);
                formatopdf.IngresaTexto(direccion, 8);

                if (colonia != "")
                {
                    formatopdf.IngresaTexto("Colonia:", 2);
                    formatopdf.IngresaTextoNegrita(colonia, 4);
                    formatopdf.IngresaTexto("Aprox Peso:", 1);
                    formatopdf.IngresaTexto(pesoaprox, 2);
                    formatopdf.IngresaTextoNegrita(tiporecibo, 1);
                }

                formatopdf.IngresaTexto("Fecha recibo:", 2);
                formatopdf.IngresaTexto(Fecha, 8);



                formatopdf.IngresaTextoBarraAbajo(".", 10);
                formatopdf.IngresaTexto("INFORMACIÓN DEL RECIBO", 10);

                formatopdf.IngresaTextoFondonNegrita("CANT", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("UNIDAD", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("CLAVE", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("DESCRIPCIÓN", 5, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("P/U", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("IMPORTE", 1, 240, 240, 240);

                string consulta = "Select * from DetallesRecibos where numrecibo ='" + Numrecibo + "' and ayo='" + ayo + "'";
                leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    string cantidad = leer2["cantidad"].ToString();
                    string unidad = leer2["unidad"].ToString();
                    string cvproducto = leer2["cvproducto"].ToString();
                    string describe = leer2["descripcion"].ToString();
                    string nota1 = leer2["nota1"].ToString();
                    string preciou = leer2["preunitario"].ToString();
                    string importe = leer2["precio"].ToString();

                    formatopdf.IngresaTexto(cantidad, 1);
                    formatopdf.IngresaTexto(unidad, 1);
                    formatopdf.IngresaTexto(cvproducto, 1);
                    formatopdf.IngresaTexto(describe + " " + nota1, 5);
                    formatopdf.IngresaTexto(preciou, 1);
                    formatopdf.IngresaTexto(importe, 1);
                }
                conecta2.CierraConexion();



                formatopdf.IngresaTexto("\n.", 10);

                formatopdf.IngresaTexto("CANTIDAD EN LETRA:", 8);
                formatopdf.IngresaTextoBarraArribaNegrita("SUBTOTAL", 1);
                formatopdf.IngresaTextoBarraArriba("$ " + total, 1);

                formatopdf.IngresaTextoNegrita(cantletra.ToUpper(), 8);
                formatopdf.IngresaTextoNegrita("I.V.A", 1);
                formatopdf.IngresaTexto("$ " + iva, 1);

                formatopdf.IngresaTexto(".", 8);
                formatopdf.IngresaTextoNegrita("TOTAL", 1);
                formatopdf.IngresaTexto("$ " + totalgeneral, 1);

            }
            conecta.CierraConexion();

            formatopdf.IngresaTexto(observa, 10);

            formatopdf.IngresaTexto("\n\n\n.", 10);

            formatopdf.IngresaTextoBarraArriba("Nombre y Firma del Recibido", 3);
            formatopdf.IngresaTexto(".", 7);
            formatopdf.IngresaTexto("\n\n.", 10);
            formatopdf.IngresaTextoNegrita("NOTA: TODO MATERIAL SE DESCARGA A PIE DE CARRO.", 10);

            formatopdf.Cierradoc("ReciboM" +  Numrecibo+ ".pdf", valoresg.USUARIOSIS, CadenaDireccion);
            return CadenaDireccion;
        }

        public string ReportePedido(string Numpedido,string ayo)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;

            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\PedidoM.pdf";

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 10;
            int Totalfilas = 50;

            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);

            formatopdf.IngresaTextoBarraAbajoNegrita("NUM PEDIDO. " + Numpedido, 10);


           // formatopdf.FuenteNum(8);

            string Query = "select pedidos.numpedido, clientes.nombre, ";
            Query = Query + " clientes.cvcliente, ";
            Query = Query + " clientes.telefono, ";
            Query = Query + " clientes.email, ";
            Query = Query + " clientes.direccion, ";
            Query = Query + " clientes.rfc, ";
            Query = Query + " clientes.factura, ";
            Query = Query + " clientes.empresa, ";
            Query = Query + " pedidos.fecha, ";
            Query = Query + " pedidos.total, ";
            Query = Query + " pedidos.iva, pedidos.totalgeneral,";
            Query = Query + " pedidos.emitio, ";
            Query = Query + " pedidos.totalletra, ";
            Query = Query + " pedidos.tipopago, ";
            Query = Query + " pedidos.banco, ";
            Query = Query + " pedidos.numcuenta ";
            Query = Query + " from pedidos ";
            Query = Query + " inner join clientes on clientes.cvcliente=pedidos.cvcliente";

            Query = Query + "  where pedidos.numpedido='"+ Numpedido + "' ";
            Query = Query + "  and pedidos.ayo='" + ayo + "'";

            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string cvcliente = leer["cvcliente"].ToString();
                string nombre= leer["nombre"].ToString();
                string correo = leer["email"].ToString();
                string telefono = leer["telefono"].ToString();
                string direccion = leer["direccion"].ToString();

                string empresa = leer["empresa"].ToString();
                string rfc= leer["rfc"].ToString();
                string factura = leer["factura"].ToString();
                string Fecha = leer["Fecha"].ToString();
                string total= leer["total"].ToString();
                string iva= leer["iva"].ToString();
                string totalgeneral= leer["totalgeneral"].ToString();
                string cantletra = leer["totalletra"].ToString();
                
                formatopdf.IngresaTexto("Id Cliente:", 2);
                formatopdf.IngresaTexto(cvcliente, 4);
                formatopdf.IngresaTexto("E-mail:", 1);
                formatopdf.IngresaTexto(correo, 3);

                formatopdf.IngresaTexto("Nombre Comercial:", 2);
                formatopdf.IngresaTexto(empresa, 4);
                formatopdf.IngresaTexto("Telefono:", 1);
                formatopdf.IngresaTexto(telefono, 3);

                formatopdf.IngresaTexto("Nombre:", 2);
                formatopdf.IngresaTexto(nombre, 8);

                formatopdf.IngresaTexto("Dirección:", 2);
                formatopdf.IngresaTexto(direccion, 8);

                if (factura == "SI")
                {
                    formatopdf.IngresaTexto("RFC:", 2);
                    formatopdf.IngresaTexto(rfc, 4);
                    formatopdf.IngresaTexto("Fecha Pedido:", 2);
                    formatopdf.IngresaTexto(Fecha, 2);
                }
                else
                {
                    formatopdf.IngresaTexto("Fecha Pedido:", 2);
                    formatopdf.IngresaTexto(Fecha, 8);
                }

                formatopdf.IngresaTextoBarraAbajo(".",10);
                formatopdf.IngresaTexto("INFORMACIÓN DEL PEDIDO", 10);

                formatopdf.IngresaTextoFondonNegrita("CANT", 1,240,240,240);
                formatopdf.IngresaTextoFondonNegrita("UNIDAD", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("CLAVE", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("DESCRIPCIÓN", 7, 240, 240, 240);
               // formatopdf.IngresaTextoFondonNegrita("P/U", 1, 240, 240, 240);
                //formatopdf.IngresaTextoFondonNegrita("IMPORTE", 1, 240, 240, 240);

                string consulta = "Select * from DetallesPedido where numpedido='" + Numpedido + "' and ayo='" + ayo + "'";
                leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    string cantidad = leer2["cantidad"].ToString();
                    string unidad = leer2["unidad"].ToString();
                    string cvproducto = leer2["cvproducto"].ToString();
                    string describe = leer2["descripcion"].ToString();
                    string nota1 = leer2["nota1"].ToString();
                    string preciou = leer2["preunitario"].ToString();
                    string importe = leer2["precio"].ToString();

                    formatopdf.IngresaTexto(cantidad, 1);
                    formatopdf.IngresaTexto(unidad, 1);
                    formatopdf.IngresaTexto(cvproducto, 1);
                    formatopdf.IngresaTexto(describe + " " + nota1, 7);
                    //formatopdf.IngresaTexto(preciou, 1);
                    //formatopdf.IngresaTexto(importe, 1);
                }
                conecta2.CierraConexion();



                formatopdf.IngresaTexto("\n.", 10);

                //formatopdf.IngresaTexto("CANTIDAD EN LETRA:", 8);
                //formatopdf.IngresaTextoBarraArribaNegrita("SUBTOTAL", 1);
                //formatopdf.IngresaTextoBarraArriba("$ " +total, 1);

                //formatopdf.IngresaTextoNegrita(cantletra.ToUpper(), 8);
                //formatopdf.IngresaTextoNegrita("I.V.A", 1);
                //formatopdf.IngresaTexto("$ " + iva, 1);


                //formatopdf.IngresaTexto(".", 8);
                //formatopdf.IngresaTextoNegrita("TOTAL", 1);
                //formatopdf.IngresaTexto("$ " + totalgeneral, 1);

            }
            conecta.CierraConexion();

            formatopdf.IngresaTexto("\n\n\n.", 10);
            formatopdf.IngresaTextoBarraArriba("Nombre y Firma del Cliente", 3);
            formatopdf.IngresaTexto(".", 7);

            formatopdf.Cierradoc("PedidoM.pdf", "usuario", CadenaDireccion);
         
            return CadenaDireccion;
        }

        public string ReporteNotaRemision(string Numpedido, string ayo)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;

            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\RemisionM.pdf";

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 10;
            int Totalfilas = 50;

            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);

      

            // formatopdf.FuenteNum(8);

            string Query = "select pedidos.numpedido, clientes.nombre, ";
            Query = Query + " clientes.cvcliente, ";
            Query = Query + " clientes.telefono, ";
            Query = Query + " clientes.email, ";
            Query = Query + " clientes.direccion, ";
            Query = Query + " clientes.rfc, ";
            Query = Query + " clientes.factura, ";
            Query = Query + " clientes.empresa, ";
            Query = Query + " pedidos.fecha, ";
            Query = Query + " pedidos.total, ";
            Query = Query + " pedidos.iva, pedidos.totalgeneral,";
            Query = Query + " pedidos.emitio, ";
            Query = Query + " pedidos.totalletra, ";
            Query = Query + " pedidos.tipopago, ";
            Query = Query + " pedidos.banco, ";
            Query = Query + " pedidos.vendedor, ";

            Query = Query + " pedidos.numremision, ";
            Query = Query + " pedidos.fecharemision, ";

            Query = Query + " pedidos.numcuenta ";
            Query = Query + " from pedidos ";
            Query = Query + " inner join clientes on clientes.cvcliente=pedidos.cvcliente";

            Query = Query + "  where pedidos.numpedido='" + Numpedido + "' ";
            Query = Query + "  and pedidos.ayo='" + ayo + "'";

            leer = conecta.RecordInfo(Query);
            string nombre="";
            string totalgeneral="" ;
            string fecharemision = "";
            while (leer.Read())
            {
                string cvcliente = leer["cvcliente"].ToString();
                nombre = leer["nombre"].ToString();
                string correo = leer["email"].ToString();
                string telefono = leer["telefono"].ToString();
                string direccion = leer["direccion"].ToString();
                string vendedor = leer["vendedor"].ToString();

                string empresa = leer["empresa"].ToString();
                string rfc = leer["rfc"].ToString();
                string factura = leer["factura"].ToString();
                string Fecha = leer["Fecha"].ToString();
                string total = leer["total"].ToString();
                string iva = leer["iva"].ToString();
                totalgeneral = leer["totalgeneral"].ToString();
                string cantletra = leer["totalletra"].ToString();
                string numremision = leer["numremision"].ToString();
                fecharemision = leer["fecharemision"].ToString();


                formatopdf.IngresaTextoNegrita("NOTA DE REMISIÓN", 3);
                formatopdf.IngresaTexto(".", 5);
                formatopdf.IngresaTextoNegrita("NÚMERO: " + numremision, 2);
                
                formatopdf.IngresaTexto("Id Cliente:", 2);
                formatopdf.IngresaTexto(cvcliente, 4);
                formatopdf.IngresaTexto("E-mail:", 1);
                formatopdf.IngresaTexto(correo, 3);

                formatopdf.IngresaTexto("Nombre Comercial:", 2);
                formatopdf.IngresaTexto(empresa, 4);
                formatopdf.IngresaTexto("Telefono:", 1);
                formatopdf.IngresaTexto(telefono, 3);

                formatopdf.IngresaTexto("Nombre:", 2);
                formatopdf.IngresaTexto(nombre, 8);

                formatopdf.IngresaTexto("Dirección:", 2);
                formatopdf.IngresaTexto(direccion, 8);

                if (factura == "SI")
                {
                    formatopdf.IngresaTexto("RFC:", 1);
                    formatopdf.IngresaTexto(rfc, 2);
                    formatopdf.IngresaTexto("Fecha Pedido:", 2);
                    formatopdf.IngresaTexto(Fecha, 2);
                    formatopdf.IngresaTexto("VENDEDOR ", 1);
                    formatopdf.IngresaTexto(vendedor, 2);
                }
                else
                {
                    formatopdf.IngresaTexto("Fecha Pedido:", 2);
                    formatopdf.IngresaTexto(Fecha, 4);

                    formatopdf.IngresaTexto("VENDEDOR ", 1);
                    formatopdf.IngresaTexto(vendedor, 3);
                }

              
                formatopdf.IngresaTextoBarraAbajo(".", 10);
                formatopdf.IngresaTexto("INFORMACIÓN DETALLADA", 10);

                formatopdf.IngresaTextoFondonNegrita("CANT", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("UNIDAD", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("CLAVE", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("DESCRIPCIÓN", 5, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("P/U", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("IMPORTE", 1, 240, 240, 240);

                string consulta = "Select * from DetallesPedido where numpedido='" + Numpedido + "' and ayo='" + ayo + "'";
                leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    string cantidad = leer2["cantidad"].ToString();
                    string unidad = leer2["unidad"].ToString();
                    string cvproducto = leer2["cvproducto"].ToString();
                    string describe = leer2["descripcion"].ToString();
                    string nota1 = leer2["nota1"].ToString();
                    string preciou = leer2["preunitario"].ToString();
                    string importe = leer2["precio"].ToString();

                    formatopdf.IngresaTexto(cantidad, 1);
                    formatopdf.IngresaTexto(unidad, 1);
                    formatopdf.IngresaTexto(cvproducto, 1);
                    formatopdf.IngresaTexto(describe + " " + nota1, 5);
                    formatopdf.IngresaTexto(preciou, 1);
                    formatopdf.IngresaTexto(importe, 1);
                }
                conecta2.CierraConexion();



                formatopdf.IngresaTexto("\n.", 10);

                formatopdf.IngresaTexto("CANTIDAD EN LETRA:", 8);
                formatopdf.IngresaTextoBarraArribaNegrita("SUBTOTAL", 1);
                formatopdf.IngresaTextoBarraArriba("$ " + total, 1);

                formatopdf.IngresaTextoNegrita(cantletra.ToUpper(), 8);
                formatopdf.IngresaTextoNegrita("I.V.A", 1);
                formatopdf.IngresaTexto("$ " + iva, 1);


                formatopdf.IngresaTexto(".", 8);
                formatopdf.IngresaTextoNegrita("TOTAL", 1);
                formatopdf.IngresaTexto("$ " + totalgeneral, 1);

            }
            conecta.CierraConexion();

            formatopdf.IngresaTexto("\n\n\n.", 10);
            //string cadenatexto="Por el presente pagaré reconozo deber y tengo la obligación a pagar en cualquier parte, que se me requiera del pago a " + nombre;
            //cadenatexto=cadenatexto+ " a su orden del día " + fecharemision + " la cantidad de " + totalgeneral;
            //cadenatexto=cadenatexto+ " valor recibido en mercancía o servicio. Este pagaré ";
            //cadenatexto = cadenatexto + " Es mercantil esta regido por la Ley General de Titulos y Operaciones de Credito ";
            //cadenatexto = cadenatexto + " en su artículo 173 para final y demás correlativos por no ser pagaré domiciliado ";

            //formatopdf.IngresaTexto(cadenatexto, 7);
            //formatopdf.IngresaTexto(".", 3);
           
            formatopdf.IngresaTexto(".", 7);
            formatopdf.IngresaTextoBarraArriba("Nombre y Firma del Cliente", 3);

            formatopdf.Cierradoc("RemisionM.pdf", "usuario", CadenaDireccion);

            return CadenaDireccion;
        }


        public string ReporteFacturacion(string Fecha1, string Fecha2,string numpedidoG, string cvcliente)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;

            DateTime FechaEmpiaza = DateTime.Parse(Fecha1);
            DateTime FechaTermina = DateTime.Parse(Fecha2);
            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ReporteFacturacion.pdf";

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 10;
            int Totalfilas = 50;

            decimal AcumuladoSubtotal = 0;
            decimal AcumuladoIVA = 0;
            decimal AcumuladoTotal = 0;

            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);

            formatopdf.IngresaTextoNegrita("REPORTE DE FACTURACIÓN DEL " + FechaEmpiaza.ToString("dd/MM/yyyy") + " AL " + FechaTermina.ToString("dd/MM/yyyy"), 6);
            formatopdf.IngresaTexto(".", 2);
            formatopdf.IngresaTextoNegrita("MES " + FechaEmpiaza.Month.ToString(), 2);

            // formatopdf.FuenteNum(8);

            string Query = "select numfactura, ";
            Query = Query + " Reclave, ";
            Query = Query + " Renombre, ";
            Query = Query + " ReRFC, ";
            Query = Query + " numpedido, ";
            Query = Query + " Fechafactura, ";

            Query = Query + " subtotal, ";
            Query = Query + " total, ";
            Query = Query + " TImporte ";

            Query = Query + " from facturas ";
            Query = Query + "  where fcodfactura between '" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and '" + FechaTermina.ToString("yyyyMMdd") + "'";
            Query = Query + "  and estatus='FACTURADO'";
            if (numpedidoG != "") Query = Query + "  and numpedido='" + numpedidoG + "'";
            if (cvcliente!= "") Query = Query + "  and reclave='" + cvcliente+ "'";
            Query = Query + "  ORDER BY numfactura desc";
            leer = conecta.RecordInfo(Query);
            string nombre = "";
       
            formatopdf.IngresaTextoBarraAbajo(".", 10);         

            formatopdf.IngresaTextoFondonNegrita("ID Factura", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("ID Cliente", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Nombre", 3, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Num. Pedido", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Subtotal", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("I.V.A", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Total", 1, 240, 240, 240);

            while (leer.Read())
            {
                string numfactura = leer["numfactura"].ToString();
                nombre = leer["Renombre"].ToString();
                string idcliente = leer["reclave"].ToString();

                string ReRFC = leer["ReRFC"].ToString();
                string numpedido = leer["numpedido"].ToString();

                string Fechafactura = leer["Fechafactura"].ToString();
                string subtotal = leer["subtotal"].ToString();
                string total = leer["total"].ToString();
                string TImporte = leer["TImporte"].ToString();

                AcumuladoSubtotal = AcumuladoSubtotal + decimal.Parse(subtotal);
                AcumuladoIVA = AcumuladoIVA + decimal.Parse(TImporte);
                AcumuladoTotal = AcumuladoTotal + decimal.Parse(total);

                formatopdf.IngresaTexto(numfactura, 1);
                formatopdf.IngresaTexto(idcliente, 1);
                formatopdf.IngresaTexto(nombre, 3);
                formatopdf.IngresaTexto(numpedido, 1);
                formatopdf.IngresaTexto(Fechafactura, 1);
                formatopdf.IngresaTexto(subtotal, 1);
                formatopdf.IngresaTexto(TImporte, 1);
                formatopdf.IngresaTexto(total, 1);
     
            }
            conecta.CierraConexion();
            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTexto("Subtotal General.", 2);
            formatopdf.IngresaTexto("$ " + AcumuladoSubtotal.ToString("#,#.00", CultureInfo.InvariantCulture), 8);
            
            formatopdf.IngresaTexto("I.V.A General.", 2);
            formatopdf.IngresaTexto("$ " +AcumuladoIVA.ToString("#,#.00", CultureInfo.InvariantCulture), 8);

            formatopdf.IngresaTexto("Total General.", 2);
            formatopdf.IngresaTexto("$ "+ AcumuladoTotal.ToString("#,#.00", CultureInfo.InvariantCulture), 8);

            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTextoNegrita("FACTURAS CANCELADAS", 10);
            int CONTARCANCELAD = 0;
            Query = "select numfactura, ";
            Query = Query + " Reclave, ";
            Query = Query + " Renombre, ";
            Query = Query + " ReRFC, ";
            Query = Query + " numpedido, ";
            Query = Query + " Fechafactura, ";

            Query = Query + " subtotal, ";
            Query = Query + " total, ";
            Query = Query + " TImporte ";

            Query = Query + " from facturas ";
            Query = Query + "  where fcodfactura between '" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and '" + FechaTermina.ToString("yyyyMMdd") + "'";
            Query = Query + "  and estatus='CANCELADO'";
            if (numpedidoG != "") Query = Query + "  and numpedido='" + numpedidoG + "'";
            if (cvcliente != "") Query = Query + "  and reclave='" + cvcliente + "'";
            Query = Query + "  ORDER BY numfactura desc";
            leer = conecta.RecordInfo(Query);
            formatopdf.IngresaTextoBarraAbajo(".", 10);

            formatopdf.IngresaTextoFondonNegrita("ID Factura", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("ID Cliente", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Nombre", 3, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Num. Pedido", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Subtotal", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("I.V.A", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Total", 1, 240, 240, 240);

            while (leer.Read())
            {
                string numfactura = leer["numfactura"].ToString();
                nombre = leer["Renombre"].ToString();
                string idcliente = leer["reclave"].ToString();

                string ReRFC = leer["ReRFC"].ToString();
                string numpedido = leer["numpedido"].ToString();

                string Fechafactura = leer["Fechafactura"].ToString();
                string subtotal = leer["subtotal"].ToString();
                string total = leer["total"].ToString();
                string TImporte = leer["TImporte"].ToString();

                formatopdf.IngresaTexto(numfactura, 1);
                formatopdf.IngresaTexto(idcliente, 1);
                formatopdf.IngresaTexto(nombre, 3);
                formatopdf.IngresaTexto(numpedido, 1);
                formatopdf.IngresaTexto(Fechafactura, 1);
                formatopdf.IngresaTexto(subtotal, 1);
                formatopdf.IngresaTexto(TImporte, 1);
                formatopdf.IngresaTexto(total, 1);
                CONTARCANCELAD++;
            }
            conecta.CierraConexion();
            formatopdf.IngresaTexto("Num. Cancelados.", 2);
            formatopdf.IngresaTexto(CONTARCANCELAD.ToString(), 8);
            formatopdf.IngresaTexto("\n\n\n.", 10);

           
            formatopdf.IngresaTextoBarraArriba("Nombre y Firma", 3);
            formatopdf.IngresaTexto(".", 7);
            formatopdf.Cierradoc("ReporteFacturacion.pdf", "usuario", CadenaDireccion);

            return CadenaDireccion;
        }

        public string ReporteCotizacion(string Numpedido, string ayo)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;

            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\CotizacionM.pdf";

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 10;
            int Totalfilas = 50;

            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);



            // formatopdf.FuenteNum(8);

            string Query = "select pedidos.numpedido, clientes.nombre, ";
            Query = Query + " clientes.cvcliente, ";
            Query = Query + " clientes.telefono, ";
            Query = Query + " clientes.email, ";
            Query = Query + " clientes.direccion, ";
            Query = Query + " clientes.rfc, ";
            Query = Query + " clientes.factura, ";
            Query = Query + " clientes.empresa, ";
            Query = Query + " pedidos.fecha, ";
            Query = Query + " pedidos.total, ";
            Query = Query + " pedidos.iva, pedidos.totalgeneral,";
            Query = Query + " pedidos.emitio, ";
            Query = Query + " pedidos.totalletra, ";
            Query = Query + " pedidos.tipopago, ";
            Query = Query + " pedidos.banco, ";

            Query = Query + " pedidos.numcotizacion, ";
            Query = Query + " pedidos.fechacotiza, ";

            Query = Query + " pedidos.numcuenta ";
            Query = Query + " from pedidos ";
            Query = Query + " inner join clientes on clientes.cvcliente=pedidos.cvcliente";

            Query = Query + "  where pedidos.numpedido='" + Numpedido + "' ";
            Query = Query + "  and pedidos.ayo='" + ayo + "'";

            leer = conecta.RecordInfo(Query);
            string nombre = "";
            string totalgeneral = "";
            string fechacotiza = "";
            while (leer.Read())
            {
                string cvcliente = leer["cvcliente"].ToString();
                nombre = leer["nombre"].ToString();
                string correo = leer["email"].ToString();
                string telefono = leer["telefono"].ToString();
                string direccion = leer["direccion"].ToString();

                string empresa = leer["empresa"].ToString();
                string rfc = leer["rfc"].ToString();
                string factura = leer["factura"].ToString();
                string Fecha = leer["Fecha"].ToString();
                string total = leer["total"].ToString();
                string iva = leer["iva"].ToString();
                totalgeneral = leer["totalgeneral"].ToString();
                string cantletra = leer["totalletra"].ToString();
                string numcotizacion = leer["numcotizacion"].ToString();
                fechacotiza = leer["fechacotiza"].ToString();


                formatopdf.IngresaTextoNegrita("COTIZACIÓN", 3);
                formatopdf.IngresaTexto(".", 5);
                formatopdf.IngresaTextoNegrita("NÚMERO: " + numcotizacion, 2);

                formatopdf.IngresaTexto("Id Cliente:", 2);
                formatopdf.IngresaTexto(cvcliente, 4);
                formatopdf.IngresaTexto("E-mail:", 1);
                formatopdf.IngresaTexto(correo, 3);

                formatopdf.IngresaTexto("Nombre Comercial:", 2);
                formatopdf.IngresaTextoNegrita(empresa, 4);
                formatopdf.IngresaTexto("Telefono:", 1);
                formatopdf.IngresaTexto(telefono, 3);

                //formatopdf.IngresaTexto("Nombre:", 2);
                //formatopdf.IngresaTexto(nombre, 8);


                formatopdf.IngresaTexto(".", 6);
                formatopdf.IngresaTexto("Fecha Cotización", 2);
                formatopdf.IngresaTexto(fechacotiza, 2);

                formatopdf.IngresaTexto("Estimado Cliente", 10);
                formatopdf.IngresaTexto("A continuación le presentamos nuestra oferta económica", 10);
                formatopdf.IngresaTexto(".", 10);

                formatopdf.IngresaTextoFondonNegrita("SISTEMA DE CONTROL DE COTIZACIONES", 10, 172, 193, 250);
                formatopdf.IngresaTextoFondonNegrita("CANT", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("UNIDAD", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("CLAVE", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("DESCRIPCIÓN", 5, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("P/U", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("IMPORTE", 1, 240, 240, 240);

                string consulta = "Select * from DetallesPedido where numpedido='" + Numpedido + "' and ayo='" + ayo + "' and cvcliente='" + cvcliente+ "'";
                leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    string cantidad = leer2["cantidad"].ToString();
                    string unidad = leer2["unidad"].ToString();
                    string cvproducto = leer2["cvproducto"].ToString();
                    string describe = leer2["descripcion"].ToString();
                    string nota1 = leer2["nota1"].ToString();
                    string preciou = leer2["preunitario"].ToString();
                    string importe = leer2["precio"].ToString();

                    formatopdf.IngresaTexto(cantidad, 1);
                    formatopdf.IngresaTexto(unidad, 1);
                    formatopdf.IngresaTexto(cvproducto, 1);
                    formatopdf.IngresaTexto(describe + " " + nota1, 5);
                    formatopdf.IngresaTexto(preciou, 1);
                    formatopdf.IngresaTexto(importe, 1);
                }
                conecta2.CierraConexion();



                formatopdf.IngresaTexto("\n.", 10);

                formatopdf.IngresaTexto("CANTIDAD EN LETRA:", 8);
                formatopdf.IngresaTextoBarraArribaNegrita("SUBTOTAL", 1);
                formatopdf.IngresaTextoBarraArriba("$ " + total, 1);

                formatopdf.IngresaTextoNegrita(cantletra.ToUpper(), 8);
                formatopdf.IngresaTextoNegrita("I.V.A", 1);
                formatopdf.IngresaTexto("$ " + iva, 1);


                formatopdf.IngresaTexto(".", 8);
                formatopdf.IngresaTextoNegrita("TOTAL", 1);
                formatopdf.IngresaTexto("$ " + totalgeneral, 1);

            }
            conecta.CierraConexion();

            formatopdf.IngresaTexto("\n.", 10);

            formatopdf.IngresaTexto("Sin otro particular por el momento, quedo de usted", 5);
            formatopdf.IngresaTexto(".",5);
            formatopdf.IngresaTextoNegrita("ATENTAMENTE", 3);
            formatopdf.IngresaTexto(".", 7);

            formatopdf.IngresaTexto("\n\n.", 10);
            formatopdf.IngresaTextoBarraArriba("Ventas", 3);
            formatopdf.IngresaTexto(".", 7);

            formatopdf.Cierradoc("CotizacionM.pdf", "usuario", CadenaDireccion);

            return CadenaDireccion;
        }

        public string DocumentoConvenio(string Numpedido, string ayo)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;

            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ConvenioM.pdf";

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 10;
            int Totalfilas = 50;

            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);


            formatopdf.IngresaTextoNegrita("PAGARÉ UNICO", 3);
            formatopdf.IngresaTexto(".", 5);
            formatopdf.IngresaTextoNegrita("NÚMERO DE PEDIDO: " + Numpedido  , 2);

            string NombreEmpresa = "";
            string Responsable = "";
            string DireccionEmpresa = "";

            string cantidadtotal = "";
            string cantleta = "";
            string anticipo = "";

            string cadenaFormato = "Mediante este documento me obligo a pagar incondimionalmente a la orden de ";
            cadenaFormato = cadenaFormato + Responsable + " en " + DireccionEmpresa + "La cantidad de $" + cantidadtotal;
            cadenaFormato = cadenaFormato + "( " + cantleta + " )";
            cadenaFormato = cadenaFormato + ", dejando como anticipo la cantidad de $ " + anticipo;
            formatopdf.IngresaTextoNegrita(cadenaFormato, 10);
        
            string numpagos="0";
            string tipodepago="0";
            string numcliente="";
            string fechaAplica = "";
            cadenaFormato = "Pagaderos en " + numpagos + " partes con un tipo de pago " + tipodepago + " consecutivamente. por los ";
            cadenaFormato = cadenaFormato + " conceptos de adeudo asociado al cliente : " + numcliente + " a partir de la fecha ";
            cadenaFormato = cadenaFormato + fechaAplica + " .Conforme a la siguiente calendarización";

            string porcentajeMo = "";
            cadenaFormato = "Este pagaré y todos los pagos que se describen anteriormente, están sujetos a la condicion";
            cadenaFormato = cadenaFormato + " de que al no pagarse cualquiera de ellos a su vencimiento, serán exigibles";
            cadenaFormato = cadenaFormato + " a todos los que sigan en número de los ya vencidos; la falta de pago de este";
            cadenaFormato = cadenaFormato + " documento hasta el dia de su loquidación, causa intereses moratorios ";
            cadenaFormato = cadenaFormato + " del " + porcentajeMo+ " %  mensual, ";
            cadenaFormato = cadenaFormato + " con base en las Políticas de Comecialización ";
            cadenaFormato = cadenaFormato + " con " + NombreEmpresa + ", mismo que se hará pagadero en la ciudad juntamente ";
            cadenaFormato = cadenaFormato + " con el principal.";
            // formatopdf.FuenteNum(8);

            string Query = "select pedidos.numpedido, clientes.nombre, ";
            Query = Query + " clientes.cvcliente, ";
            Query = Query + " clientes.telefono, ";
            Query = Query + " clientes.email, ";
            Query = Query + " clientes.direccion, ";
            Query = Query + " clientes.rfc, ";
            Query = Query + " clientes.factura, ";
            Query = Query + " clientes.empresa, ";
            Query = Query + " pedidos.fecha, ";
            Query = Query + " pedidos.total, ";
            Query = Query + " pedidos.iva, pedidos.totalgeneral,";
            Query = Query + " pedidos.emitio, ";
            Query = Query + " pedidos.totalletra, ";
            Query = Query + " pedidos.tipopago, ";
            Query = Query + " pedidos.banco, ";

            Query = Query + " pedidos.numcotizacion, ";
            Query = Query + " pedidos.fechacotiza, ";

            Query = Query + " pedidos.numcuenta ";
            Query = Query + " from pedidos ";
            Query = Query + " inner join clientes on clientes.cvcliente=pedidos.cvcliente";

            Query = Query + "  where pedidos.numpedido='" + Numpedido + "' ";
            Query = Query + "  and pedidos.ayo='" + ayo + "'";

            leer = conecta.RecordInfo(Query);
            string nombre = "";
            string totalgeneral = "";
            string fechacotiza = "";
            while (leer.Read())
            {
                string cvcliente = leer["cvcliente"].ToString();
                nombre = leer["nombre"].ToString();
                string correo = leer["email"].ToString();
                string telefono = leer["telefono"].ToString();
                string direccion = leer["direccion"].ToString();

                string empresa = leer["empresa"].ToString();
                string rfc = leer["rfc"].ToString();
                string factura = leer["factura"].ToString();
                string Fecha = leer["Fecha"].ToString();
                string total = leer["total"].ToString();
                string iva = leer["iva"].ToString();
                totalgeneral = leer["totalgeneral"].ToString();
                string cantletra = leer["totalletra"].ToString();
                string numcotizacion = leer["numcotizacion"].ToString();
                fechacotiza = leer["fechacotiza"].ToString();


                formatopdf.IngresaTextoNegrita("COTIZACIÓN", 3);
                formatopdf.IngresaTexto(".", 5);
                formatopdf.IngresaTextoNegrita("NÚMERO: " + numcotizacion, 2);

                formatopdf.IngresaTexto("Id Cliente:", 2);
                formatopdf.IngresaTexto(cvcliente, 4);
                formatopdf.IngresaTexto("E-mail:", 1);
                formatopdf.IngresaTexto(correo, 3);

                formatopdf.IngresaTexto("Nombre Comercial:", 2);
                formatopdf.IngresaTextoNegrita(empresa, 4);
                formatopdf.IngresaTexto("Telefono:", 1);
                formatopdf.IngresaTexto(telefono, 3);

                //formatopdf.IngresaTexto("Nombre:", 2);
                //formatopdf.IngresaTexto(nombre, 8);


                formatopdf.IngresaTexto(".", 6);
                formatopdf.IngresaTexto("Fecha Cotización", 2);
                formatopdf.IngresaTexto(fechacotiza, 2);

                formatopdf.IngresaTexto("Estimado Cliente", 10);
                formatopdf.IngresaTexto("A continuación le presentamos nuestra oferta económica", 10);
                formatopdf.IngresaTexto(".", 10);

                formatopdf.IngresaTextoFondonNegrita("SISTEMA DE CONTROL DE COTIZACIONES", 10, 172, 193, 250);
                formatopdf.IngresaTextoFondonNegrita("CANT", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("UNIDAD", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("CLAVE", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("DESCRIPCIÓN", 5, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("P/U", 1, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita("IMPORTE", 1, 240, 240, 240);

                string consulta = "Select * from DetallesPedido where numpedido='" + Numpedido + "' and ayo='" + ayo + "' and cvcliente='" + cvcliente + "'";
                leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    string cantidad = leer2["cantidad"].ToString();
                    string unidad = leer2["unidad"].ToString();
                    string cvproducto = leer2["cvproducto"].ToString();
                    string describe = leer2["descripcion"].ToString();
                    string nota1 = leer2["nota1"].ToString();
                    string preciou = leer2["preunitario"].ToString();
                    string importe = leer2["precio"].ToString();

                    formatopdf.IngresaTexto(cantidad, 1);
                    formatopdf.IngresaTexto(unidad, 1);
                    formatopdf.IngresaTexto(cvproducto, 1);
                    formatopdf.IngresaTexto(describe + " " + nota1, 5);
                    formatopdf.IngresaTexto(preciou, 1);
                    formatopdf.IngresaTexto(importe, 1);
                }
                conecta2.CierraConexion();



                formatopdf.IngresaTexto("\n.", 10);

                formatopdf.IngresaTexto("CANTIDAD EN LETRA:", 8);
                formatopdf.IngresaTextoBarraArribaNegrita("SUBTOTAL", 1);
                formatopdf.IngresaTextoBarraArriba("$ " + total, 1);

                formatopdf.IngresaTextoNegrita(cantletra.ToUpper(), 8);
                formatopdf.IngresaTextoNegrita("I.V.A", 1);
                formatopdf.IngresaTexto("$ " + iva, 1);


                formatopdf.IngresaTexto(".", 8);
                formatopdf.IngresaTextoNegrita("TOTAL", 1);
                formatopdf.IngresaTexto("$ " + totalgeneral, 1);

            }
            conecta.CierraConexion();

            formatopdf.IngresaTexto("\n.", 10);

            formatopdf.IngresaTexto("Sin otro particular por el momento, quedo de usted", 5);
            formatopdf.IngresaTexto(".", 5);
            formatopdf.IngresaTextoNegrita("ATENTAMENTE", 3);
            formatopdf.IngresaTexto(".", 7);

            formatopdf.IngresaTexto("\n\n.", 10);
            formatopdf.IngresaTextoBarraArriba("Ventas", 3);
            formatopdf.IngresaTexto(".", 7);

            formatopdf.Cierradoc("CotizacionM.pdf", "usuario", CadenaDireccion);

            return CadenaDireccion;
        }


        public string ReporteFacturacionsAvanzado(string Fecha1, string Fecha2, string numpedidoG, string cvcliente)
        {
            decimal RecTotalImporte = 0;
            decimal RecTAdicional = 0;
            decimal Rttotal = 0;

            DateTime FechaEmpiaza = DateTime.Parse(Fecha1);
            DateTime FechaTermina = DateTime.Parse(Fecha2);
            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ReporteFacturacion.pdf";

            int columnas = 10;
            int Totalfilas = 50;
            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);

            formatopdf.IngresaTextoNegrita("REPORTE DE FACTURACIÓN DEL " + FechaEmpiaza.ToString("dd/MM/yyyy") + " AL " + FechaTermina.ToString("dd/MM/yyyy"), 6);
            formatopdf.IngresaTexto(".", 2);
            formatopdf.IngresaTextoNegrita("MES " + FechaEmpiaza.Month.ToString(), 2);



            formatopdf.IngresaTextoBarraAbajo(".", 10);

            formatopdf.IngresaTextoFondonNegrita("Num. Interno", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("ID Factura", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Subtotal", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("I.V.A", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Total", 2, 240, 240, 240);


            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            int contador = 0;
            string Query = "Select * from facturas where numpedido<>''";
            Query = Query + "  and fechacod between '" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and '" + FechaTermina.ToString("yyyyMMdd") + "'";
            Query = Query + "  and facturas.estatus='FACTURADO'";
            if (numpedidoG != "") Query = Query + "  and numpedido='" + numpedidoG + "'";
            Query = Query + "  order by numpedido asc";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string numpedido = leer["numpedido"].ToString();
                string numfact = leer["numfactura"].ToString();
                string fecha = leer["fecha"].ToString();
                string renombre = leer["Renombre"].ToString();


                decimal Timporte = decimal.Parse(leer["subtotal"].ToString());
                decimal Tadicional = decimal.Parse(leer["Timporte"].ToString());
                decimal Ttotal = decimal.Parse(leer["total"].ToString());
                RecTotalImporte = RecTotalImporte + Timporte;
                RecTAdicional = RecTAdicional + Tadicional;
                Rttotal = Rttotal + Ttotal;


                formatopdf.IngresaTexto(numpedido, 2);
                formatopdf.IngresaTexto(numfact, 2);
                formatopdf.IngresaTexto(fecha, 1);
                formatopdf.IngresaTextoDerecha(Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                formatopdf.IngresaTextoDerecha(Tadicional.ToString("#,#.00", CultureInfo.InvariantCulture), 1);
                formatopdf.IngresaTextoDerecha(Ttotal.ToString("#,#.00", CultureInfo.InvariantCulture), 2);


            }
            conecta.CierraConexion();

            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTextoDerecha("Subtotal Global.", 8);
            formatopdf.IngresaTextoDerecha("$ " + RecTotalImporte.ToString("#,#.00", CultureInfo.InvariantCulture), 2);

            formatopdf.IngresaTextoDerecha("I.V.A Global.", 8);
            formatopdf.IngresaTextoDerecha("$ " + RecTAdicional.ToString("#,#.00", CultureInfo.InvariantCulture), 2);

            formatopdf.IngresaTextoDerecha("Total Global.", 8);
            formatopdf.IngresaTextoDerecha("$ " + Rttotal.ToString("#,#.00", CultureInfo.InvariantCulture), 2);


            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTextoNegrita("FACTURAS CANCELADAS", 10);
            int CONTARCANCELAD = 0;
            Query = "select numfactura, ";
            Query = Query + " Reclave, ";
            Query = Query + " Renombre, ";
            Query = Query + " ReRFC, ";
            Query = Query + " numpedido, ";
            Query = Query + " Fechafactura, ";

            Query = Query + " subtotal, ";
            Query = Query + " total, ";
            Query = Query + " TImporte, ";
            Query = Query + " timpuestosretenido ";

            Query = Query + " from facturas ";
            Query = Query + "  where fechacod between '" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and '" + FechaTermina.ToString("yyyyMMdd") + "'";
            Query = Query + "  and estatus='CANCELADO'";
            if (numpedidoG != "") Query = Query + "  and numpedido='" + numpedidoG + "'";
            if (cvcliente != "") Query = Query + "  and reclave='" + cvcliente + "'";
            Query = Query + "  ORDER BY numpedido desc";
            leer = conecta.RecordInfo(Query);
            formatopdf.IngresaTextoBarraAbajo(".", 10);

            formatopdf.IngresaTextoFondonNegrita("ID Factura", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("ID Cliente", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Nombre", 3, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Num. Interno", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Subtotal", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("I.V.A", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Total", 1, 240, 240, 240);

            while (leer.Read())
            {
                string numfactura = leer["numfactura"].ToString();
                string nombre = leer["Renombre"].ToString();
                string idcliente = leer["reclave"].ToString();

                string ReRFC = leer["ReRFC"].ToString();
                string numpedido = leer["numpedido"].ToString();

                string Fechafactura = leer["Fechafactura"].ToString();
                string subtotal = leer["subtotal"].ToString();
                string total = leer["total"].ToString();
                string TImporte = leer["Timporte"].ToString();

                formatopdf.IngresaTexto(numfactura, 1);
                formatopdf.IngresaTexto(idcliente, 1);
                formatopdf.IngresaTexto(nombre, 3);
                formatopdf.IngresaTexto(numpedido, 1);
                formatopdf.IngresaTexto(Fechafactura, 1);
                formatopdf.IngresaTextoDerecha(subtotal, 1);
                formatopdf.IngresaTextoDerecha(TImporte, 1);
                formatopdf.IngresaTextoDerecha(total, 1);
                CONTARCANCELAD++;
            }
            conecta.CierraConexion();
            formatopdf.IngresaTexto("Num. Cancelados.", 2);
            formatopdf.IngresaTexto(CONTARCANCELAD.ToString(), 8);
            formatopdf.IngresaTexto("\n\n\n.", 10);


            formatopdf.IngresaTextoBarraArriba("Nombre y Firma", 3);
            formatopdf.IngresaTexto(".", 7);
            formatopdf.Cierradoc("ReporteFacturacion.pdf", "usuario", CadenaDireccion);

            return CadenaDireccion;

        }


        public string ReportePorConcepto(string Fecha1, string Fecha2, string numpedidoG, string cvcliente)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            conectorSql conecta3 = new conectorSql();
            SqlDataReader leer2 = null;

            DateTime FechaEmpiaza = DateTime.Parse(Fecha1);
            DateTime FechaTermina = DateTime.Parse(Fecha2);
            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ReporteConcepto.pdf";

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 10;
            int Totalfilas = 50;

            decimal AcumuladoSubtotal = 0;
            decimal AcumuladoIVA = 0;
            decimal AcumuladoTotal = 0;

            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);

            formatopdf.IngresaTextoNegrita("REPORTE POR CONCEPTO DEL " + FechaEmpiaza.ToString("dd/MM/yyyy") + " AL " + FechaTermina.ToString("dd/MM/yyyy"), 6);
            formatopdf.IngresaTexto(".", 2);
            formatopdf.IngresaTextoNegrita("MES " + FechaEmpiaza.Month.ToString(), 2);

            // formatopdf.FuenteNum(8);

            string Query = "select distinct(cvproducto) from detallesfacturas ";
            Query = Query + "  where fechacod between '" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and '" + FechaTermina.ToString("yyyyMMdd") + "'";
            Query = Query + "  order by cvproducto asc";

            leer = conecta.RecordInfo(Query);
            string nombre = "";



            formatopdf.IngresaTextoBarraAbajo(".", 10);
            formatopdf.IngresaTextoFondonNegrita("SKU Producto", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Nombre", 3, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Subtotal", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("I.V.A", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Total", 2, 240, 240, 240);

            AcumuladoSubtotal = 0;
            AcumuladoIVA = 0;
            while (leer.Read())
            {

                string cvunica = leer["cvproducto"].ToString();
                string nombrepro = "-";
                string consulta = "Select nombre from productos where cvproducto='" + cvunica + "'";
                leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    nombrepro = leer2["nombre"].ToString();
                    break;
                }
                conecta2.CierraConexion();

                decimal Timporte = 0;
                decimal TIVA = 0;
                decimal Ttotal = 0;

                consulta = "Select importe Timporte,valorIVA as TIVA, numpedido, fechacod from DetallesFacturas ";
                consulta = consulta + "  where fechacod between '" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
                consulta = consulta + "  and '" + FechaTermina.ToString("yyyyMMdd") + "'";
                consulta = consulta + "  and DetallesFacturas.cvproducto='" + cvunica.ToString() + "'";

                leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    string numpedido = leer2["numpedido"].ToString();
                    string fechacod = leer2["fechacod"].ToString();
                    consulta = "  Select * from facturas where numpedido='" + numpedido + "' and  fechacod='" + fechacod + "' and estatus='FACTURADO'";
                    bool EstaFacturado = conecta3.ExisteRegistro(consulta);
                    conecta3.CierraConexion();

                    if (EstaFacturado == true)
                    {
                        Timporte = Timporte + decimal.Parse(leer2["Timporte"].ToString());
                        TIVA = TIVA + decimal.Parse(leer2["TIVA"].ToString());
                    }

                }
                conecta2.CierraConexion();
                Ttotal = Timporte + TIVA;


                formatopdf.IngresaTexto(cvunica, 2);
                formatopdf.IngresaTexto(nombrepro, 3);
                formatopdf.IngresaTextoDerecha("$ " + Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                formatopdf.IngresaTextoDerecha("$ " + TIVA.ToString("#,#.00", CultureInfo.InvariantCulture), 1);
                formatopdf.IngresaTextoDerecha("$ " + Ttotal.ToString("#,#.00", CultureInfo.InvariantCulture), 2);

                AcumuladoSubtotal = AcumuladoSubtotal + Timporte;
                AcumuladoIVA = AcumuladoIVA + TIVA;

            }
            conecta.CierraConexion();
            AcumuladoTotal = AcumuladoSubtotal + AcumuladoIVA;
            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTextoDerecha("Subtotal Global.", 7);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoSubtotal.ToString("#,#.00", CultureInfo.InvariantCulture), 3);

            formatopdf.IngresaTextoDerecha("I.V.A Global.", 7);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoIVA.ToString("#,#.00", CultureInfo.InvariantCulture), 3);

            formatopdf.IngresaTextoDerecha("Total Global.", 7);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoTotal.ToString("#,#.00", CultureInfo.InvariantCulture), 3);

            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTexto("\n\n\n.", 10);


            formatopdf.IngresaTextoBarraArriba("Nombre y Firma", 3);
            formatopdf.IngresaTexto(".", 7);
            formatopdf.Cierradoc("ReporteConcepto.pdf", "usuario", CadenaDireccion);

            return CadenaDireccion;
        }

        public string ReportePorConceptoRECIBO(string Fecha1, string Fecha2, string numpedidoG, string cvcliente)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            conectorSql conecta3 = new conectorSql();
            SqlDataReader leer2 = null;

            DateTime FechaEmpiaza = DateTime.Parse(Fecha1);
            DateTime FechaTermina = DateTime.Parse(Fecha2);
            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ReporteConceptoRM.pdf";

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 10;
            int Totalfilas = 50;

            decimal AcumuladoSubtotal = 0;
            decimal AcumuladoIVA = 0;
            decimal AcumuladoTotal = 0;

            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);

            formatopdf.IngresaTextoNegrita("REPORTE POR CONCEPTOS DEL " + FechaEmpiaza.ToString("dd/MM/yyyy") + " AL " + FechaTermina.ToString("dd/MM/yyyy"), 6);
            formatopdf.IngresaTexto(".", 2);
            formatopdf.IngresaTextoNegrita("MES " + FechaEmpiaza.Month.ToString(), 2);

            // formatopdf.FuenteNum(8);

            string Query = "select distinct(cvproducto) from detallesrecibos ";
            Query = Query + "  where fechacod between '" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and '" + FechaTermina.ToString("yyyyMMdd") + "'";
            Query = Query + "  order by cvproducto asc";

            leer = conecta.RecordInfo(Query);
            string nombre = "";



            formatopdf.IngresaTextoBarraAbajo(".", 10);
            formatopdf.IngresaTextoFondonNegrita("SKU Producto", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Nombre", 3, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Subtotal", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("I.V.A", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Total", 2, 240, 240, 240);

            AcumuladoSubtotal = 0;
            AcumuladoIVA = 0;
            while (leer.Read())
            {

                string cvunica = leer["cvproducto"].ToString();
                string nombrepro = "-";
                string consulta = "Select nombre from productos where cvproducto='" + cvunica + "'";
                leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    nombrepro = leer2["nombre"].ToString();
                    break;
                }
                conecta2.CierraConexion();

                decimal Timporte = 0;
                decimal TIVA = 0;
                decimal Ttotal = 0;

                consulta = "Select precio Timporte,causaiva , numrecibo, fechacod from Detallesrecibos ";
                consulta = consulta + "  where fechacod between '" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
                consulta = consulta + "  and '" + FechaTermina.ToString("yyyyMMdd") + "'";
                consulta = consulta + "  and Detallesrecibos.cvproducto='" + cvunica.ToString() + "'";

                leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    string numpedido = leer2["numrecibo"].ToString();
                    string fechacod = leer2["fechacod"].ToString();
                    string causaiva = leer2["causaiva"].ToString();

                    consulta = "  Select * from recibos where numrecibo='" + numpedido + "' and  fechacod='" + fechacod + "' and estatusrecibo='RECIBO'";
                    bool EstaFacturado = conecta3.ExisteRegistro(consulta);
                    conecta3.CierraConexion();

                    if (EstaFacturado == true)
                    {
                        Timporte = Timporte + decimal.Parse(leer2["Timporte"].ToString());
                        if (causaiva == "SI") TIVA = TIVA + (decimal.Parse(leer2["Timporte"].ToString()) * 0.16m);
                        else TIVA = TIVA + 0;
                    }

                }
                conecta2.CierraConexion();
                Ttotal = Timporte + TIVA;


                formatopdf.IngresaTexto(cvunica, 2);
                formatopdf.IngresaTexto(nombrepro, 3);
                formatopdf.IngresaTextoDerecha("$ " + Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                formatopdf.IngresaTextoDerecha("$ " + TIVA.ToString("#,#.00", CultureInfo.InvariantCulture), 1);
                formatopdf.IngresaTextoDerecha("$ " + Ttotal.ToString("#,#.00", CultureInfo.InvariantCulture), 2);

                AcumuladoSubtotal = AcumuladoSubtotal + Timporte;
                AcumuladoIVA = AcumuladoIVA + TIVA;

            }
            conecta.CierraConexion();
            AcumuladoTotal = AcumuladoSubtotal + AcumuladoIVA;
            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTextoDerecha("Subtotal Global.", 7);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoSubtotal.ToString("#,#.00", CultureInfo.InvariantCulture), 3);

            formatopdf.IngresaTextoDerecha("I.V.A Global.", 7);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoIVA.ToString("#,#.00", CultureInfo.InvariantCulture), 3);

            formatopdf.IngresaTextoDerecha("Total Global.", 7);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoTotal.ToString("#,#.00", CultureInfo.InvariantCulture), 3);

            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTexto("\n\n\n.", 10);


            formatopdf.IngresaTextoBarraArriba("Nombre y Firma", 3);
            formatopdf.IngresaTexto(".", 7);
            formatopdf.Cierradoc("ReporteConceptoRM.pdf", "usuario", CadenaDireccion);

            return CadenaDireccion;
        }


        public string ReportePorReciboDetallado(string Fecha1, string Fecha2, string numpedidoG, string cvcliente)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;

            DateTime FechaEmpiaza = DateTime.Parse(Fecha1);
            DateTime FechaTermina = DateTime.Parse(Fecha2);
            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ReporteReciboDetallado.pdf";

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 10;
            int Totalfilas = 50;

            decimal AcumuladoSubtotal = 0;
            decimal AcumuladoAdicional = 0;
            decimal AcumuladoTotal = 0;

            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);

            formatopdf.IngresaTextoNegrita("REPORTE POR RECIBO DETALLADO DEL " + FechaEmpiaza.ToString("dd/MM/yyyy") + " AL " + FechaTermina.ToString("dd/MM/yyyy"), 6);
            formatopdf.IngresaTexto(".", 2);
            formatopdf.IngresaTextoNegrita("MES " + FechaEmpiaza.Month.ToString(), 2);

            // formatopdf.FuenteNum(8);

            //string Query = "select distinct(cvproducto), nombre,codbarras   from productos order by nombre asc";
            int contador = 0;
            string Query = "Select * from facturas where numpedido<>''";
            Query = Query + "  and fechacod between '" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and '" + FechaTermina.ToString("yyyyMMdd") + "'";
            Query = Query + "  and facturas.estatus='FACTURADO'";
            if (numpedidoG != "") Query = Query + "  and numpedido='" + numpedidoG + "'";
            Query = Query + "  order by numpedido asc";

            leer = conecta.RecordInfo(Query);
            string nombre = "";



            formatopdf.IngresaTextoBarraAbajo(".", 10);
            formatopdf.IngresaTextoFondonNegrita("Num. Interno", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Nombre", 4, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Subtotal", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("I.V.A", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Total", 2, 240, 240, 240);
            while (leer.Read())
            {
                contador++;
                string numpedido = leer["numpedido"].ToString();
                string nombrepro = leer["REnombre"].ToString();
                string numfact = leer["numfactura"].ToString();
                string subtotal = leer["subtotal"].ToString();
                string total = leer["total"].ToString();
                decimal Timporte = 0;
                decimal TIVA= 0;
                decimal Ttotal = 0;

                formatopdf.IngresaTextoFondonNegrita(numpedido, 2, 240, 240, 240);
                formatopdf.IngresaTextoFondonNegrita(" FACTURA_" + numfact + " " + nombrepro, 8, 240, 240, 240);

                string consulta = "Select cvproducto, descripcion, importe Timporte,VALORIVA as tiva from DetallesFacturas ";
                consulta = consulta + " where numpedido='" + numpedido + "' order by descripcion asc";
                Timporte = 0;
                TIVA = 0;
                Ttotal = 0;

                decimal RecTotalImporte = 0;
                decimal RecTIVA = 0;
                decimal Rttotal = 0;
                leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {

                    string cuenta = leer2["cvproducto"].ToString();
                    string nombreproducto = leer2["descripcion"].ToString();

                    if (leer2["Timporte"].ToString() != "") Timporte = decimal.Parse(leer2["Timporte"].ToString());
                    else Timporte = 0;


                    if (leer2["tiva"].ToString() != "") TIVA = decimal.Parse(leer2["tiva"].ToString());
                    else TIVA = 0;


                    RecTotalImporte = RecTotalImporte + Timporte;
                    RecTIVA = RecTIVA + TIVA;


                    Ttotal = Timporte + TIVA;
                    Rttotal = Rttotal + Ttotal;

                    formatopdf.IngresaTexto(cuenta, 2);
                    formatopdf.IngresaTexto(nombreproducto, 4);
                    formatopdf.IngresaTextoDerecha("$ " + Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 1);
                    formatopdf.IngresaTextoDerecha("$ " + TIVA.ToString("#,#.00", CultureInfo.InvariantCulture), 1);
                    formatopdf.IngresaTextoDerecha("$ " + Ttotal.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                }
                conecta2.CierraConexion();

                formatopdf.IngresaTexto(".", 6);
                formatopdf.IngresaTextoDerechaLineaArriba("$ " + RecTotalImporte.ToString("#,#.00", CultureInfo.InvariantCulture), 1);
                formatopdf.IngresaTextoDerechaLineaArriba("$ " + RecTIVA.ToString("#,#.00", CultureInfo.InvariantCulture), 1);
                formatopdf.IngresaTextoDerechaLineaArriba("$ " + Rttotal.ToString("#,#.00", CultureInfo.InvariantCulture), 2);


                AcumuladoSubtotal = AcumuladoSubtotal + RecTotalImporte;
                AcumuladoAdicional = AcumuladoAdicional + RecTIVA;

            }
            conecta.CierraConexion();



            AcumuladoTotal = AcumuladoSubtotal + AcumuladoAdicional;
            formatopdf.IngresaTexto(contador.ToString() + " Recibos " + "\n.", 10);
            formatopdf.IngresaTextoDerecha("Subtotal Global.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoSubtotal.ToString("#,#.00", CultureInfo.InvariantCulture), 2);

            formatopdf.IngresaTextoDerecha("Adicional Global.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoAdicional.ToString("#,#.00", CultureInfo.InvariantCulture), 2);

            formatopdf.IngresaTextoDerecha("Total Global.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoTotal.ToString("#,#.00", CultureInfo.InvariantCulture), 2);

            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTexto("\n\n\n.", 10);


            formatopdf.IngresaTextoBarraArriba("Nombre y Firma", 3);
            formatopdf.IngresaTexto(".", 7);
            formatopdf.Cierradoc("ReporteConceptoRecibo.pdf", "usuario", CadenaDireccion);

            return CadenaDireccion;
        }

        public string ReportePorReciboPagos(string Fecha1, string Fecha2, string HoraInicia, string HoraTermina)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;

            DateTime FechaEmpiaza = DateTime.Parse(Fecha1);
            DateTime FechaTermina = DateTime.Parse(Fecha2);
            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ReportePagoDiario" + DateTime.Now.ToString("yyyyMMdd")+".pdf";

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 10;
            int Totalfilas = 50;


            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);

            formatopdf.IngresaTextoNegrita("REPORTE DE PAGOS DEL " + FechaEmpiaza.ToString("dd/MM/yyyy") + " AL " + FechaTermina.ToString("dd/MM/yyyy"), 6);
            formatopdf.IngresaTexto(".", 2);
            formatopdf.IngresaTextoNegrita("MES " + FechaEmpiaza.Month.ToString(), 2);

            // formatopdf.FuenteNum(8);

            int contador = 0;
            string Query = "Select * from pagos where numpedido<>'' and bandera='1' ";
            Query = Query + "  and fechacod='" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and horacodpago>'" + HoraInicia+ "'";
            Query = Query + "  order by numpedido asc";

            leer = conecta.RecordInfo(Query);
            string nombre = "";

            formatopdf.IngresaTextoBarraAbajo(".", 10);
            formatopdf.IngresaTextoFondonNegrita("Num. Recibo", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Monto", 3, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Hora", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Emitio", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Pago Con", 1, 240, 240, 240);
            decimal AcumuladoRec = 0;
            decimal AcumTotalEfectivo = 0;
            decimal AcumTotalDeposito = 0;
            string NumreciboAntes = "";
            string NumreciboActual = "";
            formatopdf.IngresaTextoFondonNegrita("FECHA DEL CORTE " + FechaEmpiaza.ToString("dd/MM/yyyy"), 10, 240, 240, 240);

            while (leer.Read())
            {
                contador++;
                string numpedido = leer["numpedido"].ToString();
                string monto= leer["cantidad"].ToString();
                string fecha= leer["fecha"].ToString();
                string horapago = leer["horapago"].ToString();
                string emitio = leer["emitiopago"].ToString();
                string pagocon= leer["pagocon"].ToString();
                decimal Timporte = decimal.Parse(monto);
                AcumuladoRec = AcumuladoRec + Timporte;
                NumreciboActual=numpedido;

                if (pagocon == "EFECTIVO")
                    AcumTotalEfectivo = AcumTotalEfectivo + Timporte;
                else
                    AcumTotalDeposito = AcumTotalDeposito + Timporte;

                if (NumreciboActual != NumreciboAntes)
                {
                    decimal totalRecibo = 0;
                    bool entro = false;
                    string consulta = "Select * from recibos where numrecibo='" + numpedido + "'";
                    leer2 = conecta2.RecordInfo(consulta);
                    while (leer2.Read())
                    {
                        formatopdf.IngresaTextoBarraAbajoNegrita("Num. Recibo " +numpedido, 2);
                        formatopdf.IngresaTextoBarraAbajoNegrita("TOTAL A COBRAR: $ " + leer2["totalgeneral"].ToString(), 3);
                        entro = true;
                        totalRecibo = decimal.Parse(leer2["totalgeneral"].ToString());
                    }
                    conecta2.CierraConexion();

            
                    if (entro == false)
                    {
                        formatopdf.IngresaTextoBarraAbajoNegrita("Num. Recibo " + numpedido, 2);
                        formatopdf.IngresaTextoBarraAbajoNegrita("." , 3);
                    }



                    decimal TotalCobrado = 0;
                    consulta = "Select top 1 * from pagos where numpedido='" + numpedido + "' and bandera='1'";
                    leer2 = conecta2.RecordInfo(consulta);
                    while (leer2.Read())
                    {
                        TotalCobrado = TotalCobrado + decimal.Parse(leer2["cantidad"].ToString());
                    }
                    conecta2.CierraConexion();
                    formatopdf.IngresaTextoBarraAbajoNegrita("TOTAL COBRADO: $ " + TotalCobrado.ToString(), 4);




                    if (TotalCobrado >= totalRecibo)
                        formatopdf.IngresaTextoBarraAbajoNegrita(".",1);
                    else
                        formatopdf.IngresaTextoFondo(".", 1, 255, 255, 192); // faltan pagos
                    

                    
                    formatopdf.IngresaTextoNegrita(numpedido, 2);
                    formatopdf.IngresaTextoNegrita(Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 3);
                    formatopdf.IngresaTexto(fecha, 2);
                    formatopdf.IngresaTexto(horapago, 1);
                    formatopdf.IngresaTexto(emitio, 1);
                    formatopdf.IngresaTexto(pagocon, 1);
                    NumreciboAntes = numpedido;
                }
                else
                {
                    formatopdf.IngresaTextoNegrita(numpedido, 2);
                    formatopdf.IngresaTextoNegrita(Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 3);
                    formatopdf.IngresaTexto(fecha, 2);
                    formatopdf.IngresaTexto(horapago, 1);
                    formatopdf.IngresaTexto(emitio, 1);
                    formatopdf.IngresaTexto(pagocon, 1);

                    NumreciboAntes = numpedido;
                }
            }
            conecta.CierraConexion();


            
            
            
            formatopdf.IngresaTextoFondonNegrita("FECHA DEL CORTE "+ FechaTermina.ToString("dd/MM/yyyy"), 10, 240, 240, 240);

            //DIA SEGUNDO 
            Query = "Select * from pagos where numpedido<>'' and bandera='1' ";
            Query = Query + "  and fechacod='" + FechaTermina.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and horacodpago<='" + HoraInicia + "'";
            Query = Query + "  order by numpedido asc";

            NumreciboActual = "";
            NumreciboAntes = "";
            leer = conecta.RecordInfo(Query);
            nombre = "";
            while (leer.Read())
            {
                contador++;
                string numpedido = leer["numpedido"].ToString();
                string monto = leer["cantidad"].ToString();
                string fecha = leer["fecha"].ToString();
                string horapago = leer["horapago"].ToString();
                string emitio = leer["emitiopago"].ToString();
                string pagocon = leer["pagocon"].ToString();
                decimal Timporte = decimal.Parse(monto);
                AcumuladoRec = AcumuladoRec + Timporte;
                NumreciboActual = numpedido;

                if (pagocon == "EFECTIVO")
                    AcumTotalEfectivo = AcumTotalEfectivo + Timporte;
                else
                    AcumTotalDeposito = AcumTotalDeposito + Timporte;


                if (NumreciboActual != NumreciboAntes)
                {
                    bool entrorec = false;
                    decimal totalRecibo = 0;
                    string consulta = "Select top 1 * from recibos where numrecibo='" + numpedido + "'";
                    leer2 = conecta2.RecordInfo(consulta);
                    while (leer2.Read())
                    {
                        formatopdf.IngresaTextoBarraAbajoNegrita("Num. Recibo " + numpedido, 2);
                        formatopdf.IngresaTextoBarraAbajoNegrita("TOTAL A COBRAR: $ " + leer2["totalgeneral"].ToString(), 3);
                        entrorec = true;
                        totalRecibo = decimal.Parse(leer2["totalgeneral"].ToString());
                    }
                    conecta2.CierraConexion();


                    if (entrorec == false)
                    {
                        formatopdf.IngresaTextoBarraAbajoNegrita("Num. Recibo " + numpedido, 2);
                        formatopdf.IngresaTextoBarraAbajoNegrita("" , 3);
                    }
                    
                    decimal TotalCobrado = 0;
                    consulta = "Select * from pagos where numpedido='" + numpedido + "' and bandera='1'";
                    leer2 = conecta2.RecordInfo(consulta);
                    while (leer2.Read())
                    {
                        TotalCobrado = TotalCobrado + decimal.Parse(leer2["cantidad"].ToString());
                    }
                    conecta2.CierraConexion();
                    formatopdf.IngresaTextoBarraAbajoNegrita("TOTAL COBRADO: $ " + TotalCobrado.ToString(), 4);
                    if (TotalCobrado >= totalRecibo)
                        formatopdf.IngresaTextoBarraAbajoNegrita(".", 1);
                    else
                        formatopdf.IngresaTextoFondo(".", 1, 255, 255, 192); // faltan pagos





                    formatopdf.IngresaTextoNegrita(numpedido, 2);
                    formatopdf.IngresaTextoNegrita(Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 3);
                    formatopdf.IngresaTexto(fecha, 2);
                    formatopdf.IngresaTexto(horapago, 1);
                    formatopdf.IngresaTexto(emitio, 1);
                    formatopdf.IngresaTexto(pagocon, 1);
                    NumreciboAntes = numpedido;
                }
                else
                {
                    formatopdf.IngresaTextoNegrita(numpedido, 2);
                    formatopdf.IngresaTextoNegrita(Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 3);
                    formatopdf.IngresaTexto(fecha, 2);
                    formatopdf.IngresaTexto(horapago, 1);
                    formatopdf.IngresaTexto(emitio, 1);
                    formatopdf.IngresaTexto(pagocon, 1);
                    NumreciboAntes = numpedido;
                }
            }
            conecta.CierraConexion();




           
            formatopdf.IngresaTexto(contador.ToString() + " Recibos " + "\n.", 10);
            formatopdf.IngresaTextoDerecha("Total de Ingreso.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoRec.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTextoDerecha("Total de Efectivo.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumTotalEfectivo.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            formatopdf.IngresaTextoDerecha("Total de Deposito.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumTotalDeposito.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            formatopdf.IngresaTexto("\n\n\n.", 10);

            formatopdf.IngresaTextoBarraArriba("Nombre y Firma", 3);
            formatopdf.IngresaTexto(".", 7);
            formatopdf.Cierradoc("ReportePagoDiario" + DateTime.Now.ToString("yyyyMMdd") + ".pdf", "usuario", CadenaDireccion);

            return CadenaDireccion;
        }


        public string ReporteDeRecibos(string Fecha1, string Fecha2, string HoraInicia, string HoraTermina)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;

            DateTime FechaEmpiaza = DateTime.Parse(Fecha1);
            DateTime FechaTermina = DateTime.Parse(Fecha2);
            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ReporteRecibos" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 10;
            int Totalfilas = 50;

            int ContarVentaMostrador = 0;
            int ContarPedidos = 0;


            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);

            formatopdf.IngresaTextoNegrita("REPORTE DE RECIBOS DEL " + FechaEmpiaza.ToString("dd/MM/yyyy") + " AL " + FechaTermina.ToString("dd/MM/yyyy"), 6);
            formatopdf.IngresaTexto(".", 2);
            formatopdf.IngresaTextoNegrita("MES " + FechaEmpiaza.Month.ToString(), 2);

            // formatopdf.FuenteNum(8);

            int contador = 0;
            string Query = "Select * from recibos where numrecibo<>'' ";
            Query = Query + "  and fechacod='" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and horacod>'" + HoraInicia + "'";
            Query = Query + "  order by numrecibo asc";

            leer = conecta.RecordInfo(Query);
            string nombre = "";

            formatopdf.IngresaTextoFondonNegrita("FECHA DEL CORTE " + FechaEmpiaza.ToString("dd/MM/yyyy"), 10, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Num. Recibo", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Nombre", 3, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Hora", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Emitio", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Total", 2, 240, 240, 240);


            decimal AcumulaFechaCorte = 0;


            decimal AcumuladoRec = 0;
            decimal RestaRecibo = 0;
            decimal AcumulaDebeRecibo = 0;
            decimal AcumulaPagosRec = 0;
            decimal AcumTotalMostrador = 0;
            decimal AcumTotalPedido = 0;
            string NumreciboAntes = "";
            string NumreciboActual = "";
            while (leer.Read())
            {
                contador++;
                string numpedido = leer["numrecibo"].ToString();
                string monto= leer["totalgeneral"].ToString();
                string fecha = leer["fecha"].ToString();
                string horapago = leer["hora"].ToString();
                string emitio = leer["emitio"].ToString();
                string nombrerecibo= leer["nombrerecibo"].ToString();
                string esmostrador = leer["vendedor"].ToString();
                decimal Timporte = decimal.Parse(monto);
                AcumuladoRec = AcumuladoRec + Timporte;
                bool entromostrador = false;
                if (esmostrador == "MOSTRADOR")
                {
                    AcumTotalMostrador = AcumTotalMostrador + Timporte;
                    ContarVentaMostrador++;
                    entromostrador = true;
                }
                else
                {
                    AcumTotalPedido = AcumTotalPedido + Timporte;
                    ContarPedidos++;
                }


                


                if (entromostrador==false)
                    formatopdf.IngresaTextoBarraArribaNegrita(numpedido, 2);
                else
                    formatopdf.IngresaTextoFondonNegrita(numpedido, 2,240,240,240);
                formatopdf.IngresaTextoBarraArriba(nombrerecibo, 3);
                formatopdf.IngresaTextoBarraArriba(fecha, 1);
                formatopdf.IngresaTextoBarraArriba(horapago, 1);
                formatopdf.IngresaTextoBarraArriba(emitio, 1);
                formatopdf.IngresaTextoDerechaLineaArriba(Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 2);

                AcumulaPagosRec = 0;
                string consulta = "Select * from pagos where numrecibo='" + numpedido + "' and bandera='1' and estatus='PAGADO'";
                consulta = consulta + " order by fcodpago desc";
                leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    string fechapago = leer2["fechapago"].ToString();
                    string horapago2 = leer2["horapago"].ToString();
                    string emitiopago2 = leer2["emitiopago"].ToString();
                    string totalpago = leer2["cantidad"].ToString();
                    decimal totalMostrar = decimal.Parse(totalpago);
                    if (fecha == fechapago) AcumulaFechaCorte = AcumulaFechaCorte + totalMostrar;
                    
                    AcumulaPagosRec = AcumulaPagosRec + totalMostrar;
                    formatopdf.IngresaTexto(".", 5);
                    formatopdf.IngresaTexto(fechapago, 1);
                    formatopdf.IngresaTexto(horapago2, 1);
                    formatopdf.IngresaTexto(emitiopago2, 1);
                    formatopdf.IngresaTextoNegrita(totalMostrar.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                }
                conecta2.CierraConexion();


                //LINEA FINAL DE PAGOS REALIZADOS EN EL RECIBO SABER SI DEBE O NO HASTA LA FECHA DE CORTE 
                if (AcumulaPagosRec >= Timporte)
                    formatopdf.IngresaTextoBarraArriba(".", 7);
                else
                {
                    RestaRecibo = Timporte - AcumulaPagosRec;
                    AcumulaDebeRecibo = AcumulaDebeRecibo + RestaRecibo;
                    formatopdf.IngresaTextoBarraArriba("------ POR PAGAR ------ $ " + RestaRecibo.ToString("#,#.00", CultureInfo.InvariantCulture), 7);
                }
                formatopdf.IngresaTextoBarraArriba("Total", 1);
                formatopdf.IngresaTextoDerechaLineaArriba(AcumulaPagosRec.ToString("#,#.00", CultureInfo.InvariantCulture), 2);


            }
            conecta.CierraConexion();


            formatopdf.IngresaTextoFondonNegrita("FECHA DEL CORTE " + FechaTermina.ToString("dd/MM/yyyy"), 10, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Num. Recibo", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Nombre", 3, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Hora", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Emitio", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Total", 2, 240, 240, 240);

            
            //DIA SEGUNDO 
            Query = "Select * from recibos where numrecibo<>'' ";
            Query = Query + "  and fechacod='" + FechaTermina.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and horacod<='" + HoraTermina + "'";
            Query = Query + "  order by numrecibo asc";

            NumreciboActual = "";
            NumreciboAntes = "";
            leer = conecta.RecordInfo(Query);
            nombre = "";
            while (leer.Read())
            {
                contador++;
                string numpedido = leer["numrecibo"].ToString();
                string monto = leer["totalgeneral"].ToString();
                string fecha = leer["fecha"].ToString();
                string horapago = leer["hora"].ToString();
                string emitio = leer["emitio"].ToString();
                string nombrerecibo = leer["nombrerecibo"].ToString();
                string esmostrador = leer["vendedor"].ToString();
                decimal Timporte = decimal.Parse(monto);
                AcumuladoRec = AcumuladoRec + Timporte;
                bool entromostrador = false;
                if (esmostrador == "MOSTRADOR")
                {
                    AcumTotalMostrador = AcumTotalMostrador + Timporte;
                    ContarVentaMostrador++;
                    entromostrador = true;
                }
                else
                {
                    AcumTotalPedido = AcumTotalPedido + Timporte;
                    ContarPedidos++;
                }

                //manda al pdf a crear la linea

                if (entromostrador == false)
                    formatopdf.IngresaTextoBarraArribaNegrita(numpedido, 2);
                else
                    formatopdf.IngresaTextoFondonNegrita(numpedido, 2, 240, 240, 240);
                formatopdf.IngresaTextoBarraArriba(nombrerecibo, 3);
                formatopdf.IngresaTextoBarraArriba(fecha, 1);
                formatopdf.IngresaTextoBarraArriba(horapago, 1);
                formatopdf.IngresaTextoBarraArriba(emitio, 1);
                formatopdf.IngresaTextoDerechaLineaArriba(Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 2);


                AcumulaPagosRec = 0;
                string consulta = "Select * from pagos where numrecibo='" + numpedido + "' and bandera='1' and estatus='PAGADO'";
                consulta = consulta + " order by fcodpago desc";
                leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {
                    string fechapago = leer2["fechapago"].ToString();
                    string horapago2 = leer2["horapago"].ToString();
                    string emitiopago2 = leer2["emitiopago"].ToString();
                    string totalpago = leer2["cantidad"].ToString();
                    decimal totalMostrar = decimal.Parse(totalpago);
                    AcumulaPagosRec = AcumulaPagosRec + totalMostrar;
                    if (fecha == fechapago) AcumulaFechaCorte = AcumulaFechaCorte + totalMostrar;
                    formatopdf.IngresaTexto(".", 5);
                    formatopdf.IngresaTexto(fechapago, 1);
                    formatopdf.IngresaTexto(horapago2, 1);
                    formatopdf.IngresaTexto(emitiopago2, 1);
                    formatopdf.IngresaTextoNegrita(totalMostrar.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                }
                conecta2.CierraConexion();

                //---------------------------------------------------------------------------------------
                //LINEA FINAL DE PAGOS REALIZADOS EN EL RECIBO SABER SI DEBE O NO HASTA LA FECHA DE CORTE 
                if (AcumulaPagosRec >= Timporte)
                    formatopdf.IngresaTextoBarraArriba(".", 7);
                else
                {
                    RestaRecibo = Timporte - AcumulaPagosRec;
                    AcumulaDebeRecibo = AcumulaDebeRecibo + RestaRecibo;
                    formatopdf.IngresaTextoBarraArriba("------ POR PAGAR ------ $ " + RestaRecibo.ToString("#,#.00", CultureInfo.InvariantCulture), 7);
                }
                formatopdf.IngresaTextoBarraArriba("Total ", 1);
                formatopdf.IngresaTextoDerechaLineaArriba(AcumulaPagosRec.ToString("#,#.00", CultureInfo.InvariantCulture), 2);


            }
            conecta.CierraConexion();


            formatopdf.IngresaTextoBarraArriba(contador.ToString() + " Recibos " + "\n.", 10);
            formatopdf.IngresaTextoDerecha("Total en Mostrador.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumTotalMostrador.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            formatopdf.IngresaTextoDerecha("Total en Pedidos.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumTotalPedido.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTextoDerecha("Total de Ingreso.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoRec.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            formatopdf.IngresaTexto("\n.", 10);

            formatopdf.IngresaTextoDerecha("Pagos Registrado al Corte.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumulaFechaCorte.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            decimal PagadoOtraFecha = 0;
            PagadoOtraFecha = AcumuladoRec - (AcumulaFechaCorte + AcumulaDebeRecibo);
            if (PagadoOtraFecha > 0)
            {
                formatopdf.IngresaTextoDerecha("Pagos Registrado en Otras Fechas.", 8);
                formatopdf.IngresaTextoDerecha("$ " + PagadoOtraFecha.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            }


            formatopdf.IngresaTextoDerecha("Total por Cobrar.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumulaDebeRecibo.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            formatopdf.IngresaTexto("\n.", 10);




            formatopdf.IngresaTextoBarraArriba("Nombre y Firma", 3);
            formatopdf.IngresaTexto(".", 7);
            formatopdf.Cierradoc("ReporteRecibos" + DateTime.Now.ToString("yyyyMMdd") + ".pdf", "usuario", CadenaDireccion);

            return CadenaDireccion;
        }

        public string ReporteDePagos(string Fecha1, string Fecha2, string HoraInicia, string HoraTermina)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;

            DateTime FechaEmpiaza = DateTime.Parse(Fecha1);
            DateTime FechaTermina = DateTime.Parse(Fecha2);
            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ReportePagos" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 10;
            int Totalfilas = 50;


            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);

            formatopdf.IngresaTextoNegrita("REPORTE DE PAGOS DEL " + FechaEmpiaza.ToString("dd/MM/yyyy") + " AL " + FechaTermina.ToString("dd/MM/yyyy"), 6);
            formatopdf.IngresaTexto(".", 2);
            formatopdf.IngresaTextoNegrita("MES " + FechaEmpiaza.Month.ToString(), 2);

            // formatopdf.FuenteNum(8);

            int contador = 0;
            string Query = "Select * from pagos where numpedido<>'' and bandera='1' ";
            Query = Query + "  and fechacod='" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and horacodpago>'" + HoraInicia + "' and estatus='PAGADO'";
            Query = Query + "  order by numpedido asc";

            leer = conecta.RecordInfo(Query);
            string nombre = "";

            formatopdf.IngresaTextoBarraAbajo(".", 10);
            formatopdf.IngresaTextoFondonNegrita("Num. Recibo", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Monto", 3, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Hora", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Emitio", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Pago Con", 1, 240, 240, 240);
            decimal AcumuladoRec = 0;
            decimal AcumTotalEfectivo = 0;
            decimal AcumTotalDeposito = 0;
            string NumreciboAntes = "";
            string NumreciboActual = "";
            formatopdf.IngresaTextoFondonNegrita("FECHA DEL CORTE " + FechaEmpiaza.ToString("dd/MM/yyyy"), 10, 240, 240, 240);

            while (leer.Read())
            {
                contador++;
                string numpedido = leer["numpedido"].ToString();
                string monto = leer["cantidad"].ToString();
                string fecha = leer["fecha"].ToString();
                string horapago = leer["horapago"].ToString();
                string emitio = leer["emitiopago"].ToString();
                string pagocon = leer["pagocon"].ToString();
                decimal Timporte = decimal.Parse(monto);
                AcumuladoRec = AcumuladoRec + Timporte;
                NumreciboActual = numpedido;

                if (pagocon == "EFECTIVO")
                    AcumTotalEfectivo = AcumTotalEfectivo + Timporte;
                else
                    AcumTotalDeposito = AcumTotalDeposito + Timporte;

                    formatopdf.IngresaTextoNegrita(numpedido, 2);
                    formatopdf.IngresaTextoDerecha(Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 3);
                    formatopdf.IngresaTextoDerecha(fecha, 2);
                    formatopdf.IngresaTexto(horapago, 1);
                    formatopdf.IngresaTexto(emitio, 1);
                    formatopdf.IngresaTexto(pagocon, 1);
            }
            conecta.CierraConexion();
            

            formatopdf.IngresaTextoFondonNegrita("FECHA DEL CORTE " + FechaTermina.ToString("dd/MM/yyyy"), 10, 240, 240, 240);

            //DIA SEGUNDO 
            Query = "Select * from pagos where numpedido<>'' and bandera='1' ";
            Query = Query + "  and fechacod='" + FechaTermina.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and horacodpago<='" + HoraTermina + "' and estatus='PAGADO'";
            Query = Query + "  order by numpedido asc";

            NumreciboActual = "";
            NumreciboAntes = "";
            leer = conecta.RecordInfo(Query);
            nombre = "";
            while (leer.Read())
            {
                contador++;
                string numpedido = leer["numpedido"].ToString();
                string monto = leer["cantidad"].ToString();
                string fecha = leer["fecha"].ToString();
                string horapago = leer["horapago"].ToString();
                string emitio = leer["emitiopago"].ToString();
                string pagocon = leer["pagocon"].ToString();
                decimal Timporte = decimal.Parse(monto);
                AcumuladoRec = AcumuladoRec + Timporte;
                NumreciboActual = numpedido;

                if (pagocon == "EFECTIVO")
                    AcumTotalEfectivo = AcumTotalEfectivo + Timporte;
                else
                    AcumTotalDeposito = AcumTotalDeposito + Timporte;


                    formatopdf.IngresaTextoNegrita(numpedido, 2);
                    formatopdf.IngresaTextoDerecha(Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 3);
                    formatopdf.IngresaTextoDerecha(fecha, 2);
                    formatopdf.IngresaTexto(horapago, 1);
                    formatopdf.IngresaTexto(emitio, 1);
                    formatopdf.IngresaTexto(pagocon, 1);
            }
            conecta.CierraConexion();

            formatopdf.IngresaTexto(contador.ToString() + " Recibos " + "\n.", 10);
            formatopdf.IngresaTextoDerecha("Total de Ingreso.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoRec.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTextoDerecha("Total de Efectivo.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumTotalEfectivo.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            formatopdf.IngresaTextoDerecha("Total de Deposito.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumTotalDeposito.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            formatopdf.IngresaTexto("\n\n\n.", 10);

            formatopdf.IngresaTextoBarraArriba("Nombre y Firma", 3);
            formatopdf.IngresaTexto(".", 7);
            formatopdf.Cierradoc("ReportePagos" + DateTime.Now.ToString("yyyyMMdd") + ".pdf", "usuario", CadenaDireccion);

            return CadenaDireccion;
        }

        public string ReportePorGastos(string Fecha1, string Fecha2, string HoraInicia, string HoraTermina)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;

            DateTime FechaEmpiaza = DateTime.Parse(Fecha1);
            DateTime FechaTermina = DateTime.Parse(Fecha2);
            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ReporteGastos" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 10;
            int Totalfilas = 50;

            decimal AcumulaProveedor = 0;
            decimal AcumulaTrabajadores = 0;

            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);

            formatopdf.IngresaTextoNegrita("REPORTE DE GASTOS DEL " + FechaEmpiaza.ToString("dd/MM/yyyy") + " AL " + FechaTermina.ToString("dd/MM/yyyy"), 6);
            formatopdf.IngresaTexto(".", 2);
            formatopdf.IngresaTextoNegrita("MES " + FechaEmpiaza.Month.ToString(), 2);

            // formatopdf.FuenteNum(8);

            int contador = 0;
            string Query = "Select * from gastosreg where cvgasto<>''";
            Query = Query + "  and fechacod between '" + FechaEmpiaza.ToString("yyyyMMdd") + "' and '" + FechaTermina.ToString("yyyyMMdd") + "' ";
            Query = Query + "  order by cvgasto asc";

            leer = conecta.RecordInfo(Query);
            string nombre = "";

            formatopdf.IngresaTextoBarraAbajo(".", 10);
            formatopdf.IngresaTextoFondonNegrita("Num. Gasto", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Nombre", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Descripcion", 3, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Hora", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Emitio", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Monto", 1, 240, 240, 240);
            decimal AcumuladoRec = 0;
            decimal AcumTotalEfectivo = 0;
            decimal AcumTotalDeposito = 0;
            string NumreciboAntes = "";
            string NumreciboActual = "";
            while (leer.Read())
            {
                contador++;
                string cvgasto= leer["cvgasto"].ToString();
                string nombrepago = leer["nombre"].ToString();
                string describe = leer["descripcion"].ToString();
                string costo = leer["costo"].ToString();
                string fecha = leer["fecha"].ToString();
                string horapago = leer["hora"].ToString();
                string emitio = leer["emitio"].ToString();
                string tipo= leer["tipo"].ToString();
                decimal Timporte = decimal.Parse(costo);
                AcumuladoRec = AcumuladoRec + Timporte;
                NumreciboActual = cvgasto;


                if (tipo== "PROVEEDOR")
                    AcumulaProveedor = AcumulaProveedor + Timporte;
                else
                    AcumulaTrabajadores = AcumulaTrabajadores + Timporte;


                    formatopdf.IngresaTextoNegrita(cvgasto, 1);
                    formatopdf.IngresaTextoNegrita(nombrepago, 2);
                    formatopdf.IngresaTextoNegrita(describe, 3);
                    formatopdf.IngresaTexto(fecha, 1);
                    formatopdf.IngresaTexto(horapago, 1);
                    formatopdf.IngresaTexto(emitio, 1);
                    formatopdf.IngresaTextoNegrita(Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 1);
            }
            conecta.CierraConexion();

            
            formatopdf.IngresaTexto(contador.ToString() + " Recibos " + "\n.", 10);
            formatopdf.IngresaTextoDerecha("Total de Gastos.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoRec.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            formatopdf.IngresaTexto("\n.", 10);
            formatopdf.IngresaTextoDerecha("Total de Pagos a Proveedores.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumulaProveedor.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            formatopdf.IngresaTextoDerecha("Total de Pagos a Trabajadores.", 8);
            formatopdf.IngresaTextoDerecha("$ " + AcumulaTrabajadores.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
            formatopdf.IngresaTexto("\n\n\n.", 10);
            formatopdf.IngresaTextoBarraArriba("Nombre y Firma", 3);
            formatopdf.IngresaTexto(".", 7);
            formatopdf.Cierradoc("ReporteGastos" + DateTime.Now.ToString("yyyyMMdd") + ".pdf", "usuario", CadenaDireccion);

            return CadenaDireccion;
        }


        public string ReportePorReciboDetalladoRECIBOS(string Fecha1, string Fecha2, string numpedidoG, string cvcliente, string emitio, string tipopago, string entregado, string vendedor, string nombrerecibo, string colonia)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;

            DateTime FechaEmpiaza = DateTime.Parse(Fecha1);
            DateTime FechaTermina = DateTime.Parse(Fecha2);
            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ReporteReciboDetalladoRM.pdf";

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 20;
            int Totalfilas = 50;

            decimal AcumuladoSubtotal = 0;
            decimal AcumuladoAdicional = 0;
            decimal AcumuladoTotal = 0;

            string otroTipoPago = "CONTADO";
            if (otroTipoPago == "PAGADO") otroTipoPago = "CONTADO";
            if (otroTipoPago == "POR PAGAR") otroTipoPago = "CREDITO";

            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);

            string CadenaEncabezado = "REPORTE POR RECIBO DETALLADO DEL " + FechaEmpiaza.ToString("dd/MM/yyyy") + " AL " + FechaTermina.ToString("dd/MM/yyyy");
            if (tipopago != "") CadenaEncabezado = CadenaEncabezado + " TIPO DE PAGO " + tipopago;
            if (emitio != "") CadenaEncabezado = CadenaEncabezado + " EMITIDO POR " + emitio;
            if (vendedor != "") CadenaEncabezado = CadenaEncabezado + " VENTA : " + emitio;
            if (entregado != "") CadenaEncabezado = CadenaEncabezado + " ENTREGADO : " + entregado;
            if (colonia != "") CadenaEncabezado = CadenaEncabezado + " COLONIA : " + colonia;
          

            formatopdf.IngresaTextoNegrita(CadenaEncabezado,14);
            formatopdf.IngresaTexto(".", 2);
            formatopdf.IngresaTextoNegrita("MES " + FechaEmpiaza.Month.ToString(), 4);


            if (nombrerecibo != "") formatopdf.IngresaTextoNegrita(nombrerecibo, 20);

            
            // formatopdf.FuenteNum(8);

            //string Query = "select distinct(cvproducto), nombre,codbarras   from productos order by nombre asc";
            int contador = 0;
            string Query = "Select * from recibos where numrecibo<>''";
            Query = Query + "  and fechacod between '" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and '" + FechaTermina.ToString("yyyyMMdd") + "'";
            Query = Query + "  and estatusrecibo='RECIBO'";
            if (numpedidoG != "") Query = Query + "  and numrecibo='" + numpedidoG + "'";
            if (cvcliente != "") Query = Query + "  and cvcliente='" + cvcliente + "'";
            if (emitio != "") Query = Query + "  and emitio='" + emitio + "'";
            if (tipopago != "") Query = Query + "  and ( tiporecibo='" + tipopago + "' or tiporecibo='" + otroTipoPago+ "')";
            if (entregado != "") Query = Query + "  and entregado='" + entregado + "'";
            if (vendedor != "") Query = Query + "  and vendedor='" + vendedor + "'";
            if (nombrerecibo != "") Query = Query + "  and nombrerecibo='" + nombrerecibo + "'";
            if (colonia != "") Query = Query + "  and colonia='" + colonia + "'";
            Query = Query + "  order by numrecibo desc";

            leer = conecta.RecordInfo(Query);
            string nombre = "";



            formatopdf.IngresaTextoBarraAbajo(".", 20);

            formatopdf.IngresaTextoFondonNegrita("Num. Recibo", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Nombre", 3, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha Realizo", 2, 240, 240, 240);
          //  formatopdf.IngresaTextoFondonNegrita("Emitio", 1, 240, 240, 240);

            formatopdf.IngresaTextoFondonNegrita("Total", 2, 240, 240, 240);
           // formatopdf.IngresaTextoFondonNegrita("I.V.A", 2, 240, 240, 240);
           // formatopdf.IngresaTextoFondonNegrita("Total", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Tipo Pago", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Entregado", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha Entrega", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Vendido Por", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Emitio", 3, 240, 240, 240);
            
            
            while (leer.Read())
            {
                contador++;
                string numrecibo = leer["numrecibo"].ToString();
                string nombrepro = leer["nombrerecibo"].ToString();
                string fecha = leer["fecha"].ToString();

                string emitioR = leer["emitio"].ToString();
                string vendedorR = leer["vendedor"].ToString();
                string tipopagoR= leer["tiporecibo"].ToString();
                string entregadoR = leer["entregado"].ToString();
                string fechaentrega = leer["fechaentrega"].ToString();

                if (tipopagoR == "CREDITO") tipopagoR = "POR PAGAR";
                if (tipopagoR == "CONTADO") tipopagoR = "PAGADO";

                string subtotal = leer["total"].ToString();
                string iva = leer["iva"].ToString();
                string total = leer["totalgeneral"].ToString();
                decimal Timporte = decimal.Parse(subtotal);
                decimal TIVA = decimal.Parse(iva) ;
                decimal Ttotal = decimal.Parse(total);
                decimal RecTotalImporte = 0;
                decimal RecTIVA = 0;
                decimal Rttotal = 0;

                formatopdf.IngresaTexto(numrecibo, 2);
                formatopdf.IngresaTexto(nombrepro, 3);
                formatopdf.IngresaTexto(fecha, 2);
              //  formatopdf.IngresaTexto(emitioR, 1);


                //formatopdf.IngresaTextoDerecha("$ " + Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                //formatopdf.IngresaTextoDerecha("$ " + TIVA.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                formatopdf.IngresaTextoDerecha("$ " + Ttotal.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                formatopdf.IngresaTexto(tipopagoR, 2);
                formatopdf.IngresaTexto(entregadoR, 2);
                formatopdf.IngresaTexto(fechaentrega, 2);
                formatopdf.IngresaTexto(vendedorR, 2);
                formatopdf.IngresaTexto(emitioR, 3);


                Timporte = 0;
                TIVA = 0;
                Ttotal = 0;
                RecTotalImporte = 0;
                RecTIVA = 0;
                Rttotal = 0;

                string consulta = "Select cvproducto, descripcion, precio Timporte, causaIVA from DetallesRecibos";
                consulta = consulta + " where numrecibo='" + numrecibo + "' order by descripcion asc";
                Timporte = 0;
                TIVA = 0;
                Ttotal = 0;


                leer2 = conecta2.RecordInfo(consulta);
                while (leer2.Read())
                {

                    string cuenta = leer2["cvproducto"].ToString();
                    string nombreproducto = leer2["descripcion"].ToString();
                    string causaiva = leer2["causaiva"].ToString();

                    if (leer2["Timporte"].ToString() != "") Timporte = decimal.Parse(leer2["Timporte"].ToString());
                    else Timporte = 0;


                    if (causaiva == "SI")
                        TIVA = Timporte * 0.16m;
                    else TIVA = 0;


                    RecTotalImporte = RecTotalImporte + Timporte;
                    RecTIVA = RecTIVA + TIVA;


                    Ttotal = Timporte + TIVA;
                    Rttotal = Rttotal + Ttotal;

                    formatopdf.IngresaTexto(cuenta, 2);
                    formatopdf.IngresaTexto(nombreproducto, 5);
                    //formatopdf.IngresaTextoDerecha("$ " + Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                    //formatopdf.IngresaTextoDerecha("$ " + TIVA.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                    formatopdf.IngresaTextoDerecha("$ " + Ttotal.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                    formatopdf.IngresaTexto(".", 11);
                }
                conecta2.CierraConexion();

             
              
  
                AcumuladoSubtotal = AcumuladoSubtotal + RecTotalImporte;
                AcumuladoAdicional = AcumuladoAdicional + RecTIVA;

                formatopdf.IngresaTextoDerechaLineaArriba(".", 20);


            }
            conecta.CierraConexion();




            AcumuladoTotal = AcumuladoSubtotal + AcumuladoAdicional;
            formatopdf.IngresaTexto(contador.ToString() + " Recibos " + "\n.", 20);

            formatopdf.IngresaTextoDerecha("Subtotal Global.", 16);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoSubtotal.ToString("#,#.00", CultureInfo.InvariantCulture), 4);

            formatopdf.IngresaTextoDerecha("IVA Global.", 16);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoAdicional.ToString("#,#.00", CultureInfo.InvariantCulture), 4);

            formatopdf.IngresaTextoDerecha("Total Global.", 16);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoTotal.ToString("#,#.00", CultureInfo.InvariantCulture), 4);

            formatopdf.IngresaTexto("\n.", 20);
            formatopdf.IngresaTexto("\n\n\n.", 20);


            formatopdf.IngresaTextoBarraArriba("Nombre y Firma", 6);
            formatopdf.IngresaTexto(".", 14);

            formatopdf.CierradocHorizontal("ReporteConceptoReciboRM.pdf", "usuario", CadenaDireccion);

            return CadenaDireccion;
        }

        public string ReportePorAVANZADORECIBO(string Fecha1, string Fecha2, string numpedidoG, string cvcliente, string emitio, string tipopago, string entregado, string vendedor, string nombrerecibo, string colonia)
        {
            conectorSql conecta = new conectorSql();
            SqlDataReader leer = null;

            conectorSql conecta2 = new conectorSql();
            SqlDataReader leer2 = null;

            DateTime FechaEmpiaza = DateTime.Parse(Fecha1);
            DateTime FechaTermina = DateTime.Parse(Fecha2);
            string CadenaDireccion = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ReporteReciboDetalladoRM.pdf";

            //Document Doc = new Document();
            //PdfWriter.GetInstance(Doc, new FileStream(CadenaDireccion, FileMode.Create));
            //Doc.Open();
            int columnas = 20;
            int Totalfilas = 10;

            decimal AcumuladoSubtotal = 0;
            decimal AcumuladoAdicional = 0;
            decimal AcumuladoTotal = 0;

            formatopdf.IniciaTabla(columnas, Totalfilas + 9);
            formatopdf.ColocaBannerTabla();
            formatopdf.Fuente();
            formatopdf.FuenteNum(8);
            formatopdf.FuenteCOlor(46, 46, 46);

            string CadenaEncabezado = "REPORTE POR RECIBO  DEL " + FechaEmpiaza.ToString("dd/MM/yyyy") + " AL " + FechaTermina.ToString("dd/MM/yyyy");
            if (tipopago != "") CadenaEncabezado = CadenaEncabezado + " TIPO DE PAGO " + tipopago;
            if (emitio != "") CadenaEncabezado = CadenaEncabezado + " EMITIDO POR " + emitio;
            if (vendedor != "") CadenaEncabezado = CadenaEncabezado + " VENTA : " + emitio;
            if (entregado != "") CadenaEncabezado = CadenaEncabezado + " ENTREGADO : " + entregado;
            if (colonia != "") CadenaEncabezado = CadenaEncabezado + " COLONIA : " + colonia;


            formatopdf.IngresaTextoNegrita(CadenaEncabezado, 14);
            formatopdf.IngresaTexto(".", 2);
            formatopdf.IngresaTextoNegrita("MES " + FechaEmpiaza.Month.ToString(), 4);


            if (nombrerecibo != "") formatopdf.IngresaTextoNegrita(nombrerecibo, 20);


            // formatopdf.FuenteNum(8);
            string OtrotipoPago="CONTADO";
            if (tipopago == "PAGADO") OtrotipoPago = "CONTADO";
            if (tipopago == "POR PAGAR") OtrotipoPago = "CREDITO";

            //string Query = "select distinct(cvproducto), nombre,codbarras   from productos order by nombre asc";
            int contador = 0;
            string Query = "Select * from recibos where numrecibo<>''";
            Query = Query + "  and fechacod between '" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and '" + FechaTermina.ToString("yyyyMMdd") + "'";
            Query = Query + "  and estatusrecibo='RECIBO'";
            if (numpedidoG != "") Query = Query + "  and numrecibo='" + numpedidoG + "'";
            if (cvcliente != "") Query = Query + "  and cvcliente='" + cvcliente + "'";
            if (emitio != "") Query = Query + "  and emitio='" + emitio + "'";
            if (tipopago != "") Query = Query + "  and (tiporecibo='" + tipopago + "' or tiporecibo='" + OtrotipoPago + "')";
            if (entregado != "") Query = Query + "  and entregado='" + entregado + "'";
            if (vendedor != "") Query = Query + "  and vendedor='" + vendedor + "'";
            if (nombrerecibo != "") Query = Query + "  and nombrerecibo='" + nombrerecibo + "'";
            if (colonia != "") Query = Query + "  and colonia='" + colonia + "'";

            Query = Query + "  order by numrecibo desc";

            leer = conecta.RecordInfo(Query);
            string nombre = "";



            formatopdf.IngresaTextoBarraAbajo(".", 20);

            formatopdf.IngresaTextoFondonNegrita("Num. Recibo", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Nombre", 3, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha Realizo", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Emitio", 3, 240, 240, 240);

            //formatopdf.IngresaTextoFondonNegrita("Subtotal", 2, 240, 240, 240);
            //formatopdf.IngresaTextoFondonNegrita("I.V.A", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Total", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Tipo Pago", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Entregado", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha Entrega", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Vendedor", 2, 240, 240, 240);



            while (leer.Read())
            {
                contador++;
                string numrecibo = leer["numrecibo"].ToString();
                string nombrepro = leer["nombrerecibo"].ToString();
                string fecha = leer["fecha"].ToString();

                string emitioR = leer["emitio"].ToString();
                string vendedorR = leer["vendedor"].ToString();
                string tipopagoR = leer["tiporecibo"].ToString();
                string entregadoR = leer["entregado"].ToString();
                string fechaentrega = leer["fechaentrega"].ToString();


                if (tipopagoR == "CREDITO") tipopagoR = "POR PAGAR";

                string subtotal = leer["total"].ToString();
                string iva = leer["iva"].ToString();
                string total = leer["totalgeneral"].ToString();
                decimal Timporte = decimal.Parse(subtotal);
                decimal TIVA = decimal.Parse(iva);
                decimal Ttotal = decimal.Parse(total);

                formatopdf.IngresaTexto(numrecibo, 2);
                formatopdf.IngresaTexto(nombrepro, 3);
                formatopdf.IngresaTexto(fecha, 2);
                formatopdf.IngresaTexto(emitioR, 3);

                //formatopdf.IngresaTextoDerecha("$ " + Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                //formatopdf.IngresaTextoDerecha("$ " + TIVA.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                formatopdf.IngresaTexto("$ " + Ttotal.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                formatopdf.IngresaTexto(tipopagoR, 2);
                formatopdf.IngresaTexto(entregadoR, 2);
                formatopdf.IngresaTexto(fechaentrega, 2);
                formatopdf.IngresaTexto(vendedorR, 2);

                AcumuladoSubtotal = AcumuladoSubtotal + Timporte;
                AcumuladoAdicional = AcumuladoAdicional + TIVA;

                formatopdf.IngresaTextoDerechaLineaArriba(".", 20);

            }
            conecta.CierraConexion();


            AcumuladoTotal = AcumuladoSubtotal + AcumuladoAdicional;
            formatopdf.IngresaTexto(contador.ToString() + " Recibos " + "\n.", 20);

            formatopdf.IngresaTextoDerecha("Subtotal Global.", 16);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoSubtotal.ToString("#,#.00", CultureInfo.InvariantCulture), 4);

            formatopdf.IngresaTextoDerecha("I.V.A Global.", 16);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoAdicional.ToString("#,#.00", CultureInfo.InvariantCulture), 4);

            formatopdf.IngresaTextoDerecha("Total Global.", 16);
            formatopdf.IngresaTextoDerecha("$ " + AcumuladoTotal.ToString("#,#.00", CultureInfo.InvariantCulture), 4);

            formatopdf.IngresaTexto("\n\n\n.", 20);
        



            Query = "Select * from recibos where numrecibo<>''";
            Query = Query + "  and fechacod between '" + FechaEmpiaza.ToString("yyyyMMdd") + "' ";
            Query = Query + "  and '" + FechaTermina.ToString("yyyyMMdd") + "'";
            Query = Query + "  and estatusrecibo='CANCELADO'";
            if (numpedidoG != "") Query = Query + "  and numrecibo='" + numpedidoG + "'";
            if (cvcliente != "") Query = Query + "  and cvcliente='" + cvcliente + "'";
            if (emitio != "") Query = Query + "  and emitio='" + emitio + "'";
            if (tipopago != "") Query = Query + "  and tiporecibo='" + tipopago + "'";
            if (entregado != "") Query = Query + "  and entregado='" + entregado + "'";
            if (vendedor != "") Query = Query + "  and vendedor='" + vendedor + "'";
            if (nombrerecibo != "") Query = Query + "  and nombrerecibo='" + nombrerecibo + "'";
            if (colonia != "") Query = Query + "  and colonia='" + colonia + "'";

            Query = Query + "  order by numrecibo asc";

            leer = conecta.RecordInfo(Query);


            formatopdf.IngresaTextoFondonNegrita("RECIBOS CANCELADOS", 20, 240, 240, 240);
            formatopdf.IngresaTextoBarraAbajo(".", 20);

            formatopdf.IngresaTextoFondonNegrita("Num. Recibo", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Nombre", 3, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha Realizo", 2, 240, 240, 240);
            //  formatopdf.IngresaTextoFondonNegrita("Emitio", 1, 240, 240, 240);

            formatopdf.IngresaTextoFondonNegrita("Subtotal", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("I.V.A", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Total", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Tipo Pago", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Entregado", 1, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Fecha Entrega", 2, 240, 240, 240);
            formatopdf.IngresaTextoFondonNegrita("Vendedor", 2, 240, 240, 240);

            contador = 0;
            while (leer.Read())
            {
                contador++;
                string numrecibo = leer["numrecibo"].ToString();
                string nombrepro = leer["nombrerecibo"].ToString();
                string fecha = leer["fecha"].ToString();

                string emitioR = leer["emitio"].ToString();
                string vendedorR = leer["vendedor"].ToString();
                string tipopagoR = leer["tiporecibo"].ToString();
                string entregadoR = leer["entregado"].ToString();
                string fechaentrega = leer["fechaentrega"].ToString();

                string subtotal = leer["total"].ToString();
                string iva = leer["iva"].ToString();
                string total = leer["totalgeneral"].ToString();
                decimal Timporte = decimal.Parse(subtotal);
                decimal TIVA = decimal.Parse(iva);
                decimal Ttotal = decimal.Parse(total);

                formatopdf.IngresaTexto(numrecibo, 2);
                formatopdf.IngresaTexto(nombrepro, 3);
                formatopdf.IngresaTexto(fecha, 2);
                //  formatopdf.IngresaTexto(emitioR, 1);

                formatopdf.IngresaTextoDerecha("$ " + Timporte.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                formatopdf.IngresaTextoDerecha("$ " + TIVA.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                formatopdf.IngresaTextoDerecha("$ " + Ttotal.ToString("#,#.00", CultureInfo.InvariantCulture), 2);
                formatopdf.IngresaTextoDerecha(tipopagoR, 2);
                formatopdf.IngresaTextoDerecha(entregadoR, 1);
                formatopdf.IngresaTextoDerecha(fechaentrega, 2);
                formatopdf.IngresaTextoDerecha(vendedorR, 2);

                formatopdf.IngresaTextoDerechaLineaArriba(".", 20);

            }
            conecta.CierraConexion();

            formatopdf.IngresaTexto(contador.ToString() + " Cancelados", 20);


            formatopdf.IngresaTexto("\n\n\n.", 20);



            formatopdf.IngresaTextoBarraArriba("Nombre y Firma", 6);
            formatopdf.IngresaTexto(".", 14);

            formatopdf.CierradocHorizontal("ReporteConceptoReciboRM.pdf", "usuario", CadenaDireccion);

            return CadenaDireccion;
        }


    }

