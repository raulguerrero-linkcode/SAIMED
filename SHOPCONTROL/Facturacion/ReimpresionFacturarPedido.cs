using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DGMC.TimbraCFDI;
using System.IO;
using System.Configuration;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Xml;
using System.Xml.Serialization;
using System.Net.Mail;
using System.Net;


namespace SHOPCONTROL
{
    public partial class ReimpresionFacturarPedido : Form
    {
        public ReimpresionFacturarPedido()
        {
            InitializeComponent();
        }

        public string USUARIO;
        public string CONTRA;
        public string NOMBRE;
        public string RFC;
        public string LUGAREXPEDICION;
        public string REGIMEN;
        public string DIRCARPETA;
        public string IDSERIAL;

        public string CONESTADO;
        public string CONMUNICIPIO;
        public string CONCOLONIA;

        public string CALLEFIJO;
        public string MUNICIPIOFIJO;
        public string ESTADOFIJO;
        public string NUMEXTFIJO;
        public string NUMINTFIJO;
        public string PAISFIJO;
        public string LOCALIDADFIJO;
        public string CODPOSTALFIJO;
        public string COLONIAFIJO;
        public string PASARAPRODUCTIVO;
        public int Numcopias;



        public string USUARIOFAC = "";
        public string CONTRASEÑAFAC = "";
        public string CORREOFAC = "";
        public string CADDIRECCION = "";

        public string numfactura = "";
        public string estatus = "";
        public string idsistemapadre = "";
        public string edocomprobante = "";
        public string tipo = "";
        public string RFCEmitio = "";
        public string CondicionesPago = "";
        public string FormaPago = "";
        public string Descuento = "";
        public string motivoDescuento = "";
        public string metodoPago = "";
        public string subtotal = "";
        public string total = "";
        public string REClave = "";
        public string ReNombre = "";
        public string ReRFC = "";
        public string ReCalle = "";
        public string ReCodpostal = "";
        public string ReColonia = "";
        public string ReEstado = "";
        public string ReLocalidad = "";
        public string ReMunicipio = "";
        public string ReNoExterior = "";
        public string ReNoInterior = "";
        public string ReTel = "";
        public string RePais = "";
        public string ReReferencia = "";
        public string Recorreo = "";
        public string TImpuestosRetenido = "";
        public string TImpuestoTrasladado = "";
        public string RImpuesto = "";
        public string RImporte = "";
        public string TImpuesto = "";
        public string TImporte = "";
        public string TTasa = "";
        public string Notas = "";
        public string moneda = "";
        public string TipoCambio = "";
        public string Vendedor = "";
        public string OrdCompra = "";
        public string Otros = "";
        public string numCtaPago = "";
        public string numInterior = "";
        public string ayo = "";
        public string mes = "";
        public string Fecha = "";
        public string FechaCod = "";
        public string Hora = "";
        public string Fechafactura = "";
        public string Fcodfactura = "";
        public string Horafactura = "";
        public string imagenCBB = "";
        public string cadenaOriginal = "";
        public string UUID = "";
        public string selloCFD = "";
        public string selloSat = "";
        public string serieSat = "";
        public string Emitio = "";
        public string cvcontrato = "";
        public string cvcliente = "";
        public string direccion = "";
        public string observaciones = "";
        public string cantletra = "";
        public string numpedido = "";


        public string NOMBREDEFACTURA;
        public string NOMBRECOMERCIAL;
        public string DIRECCIONFISCALEMITIO;
        public string INFOADICIONAL;

        public string ABRIRPDF;
        public string IMPRESIONDIRECTA;

        public string CORREODESTINATARIO;
        public string CORREODESTINATARIO2;

        public void NombreSetFactura()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select nombrefactura from clientes where cvcliente='" + REClave + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                NOMBREDEFACTURA = leer["nombrefactura"].ToString();
            }
            conecta.CierraConexion();
           
        }

        public void ParametrosFacturacion()
        {
            DIRECCIONFISCALEMITIO = "";
            conectorSql conecta = new conectorSql();
            string Query = "Select * from parametrosfactura where nombre<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {

                USUARIO = leer["usuario"].ToString();
                //USUARIO = "mvpNUXmQfK8=";
                CONTRA = leer["contrafac"].ToString();
                NOMBRE = leer["nombre"].ToString();
                RFC = leer["rfc"].ToString();
                if (RFC=="")RFC = "AAA010101AAA";

                LUGAREXPEDICION = leer["lugarexpedicion"].ToString();
                REGIMEN = leer["regimen"].ToString();
                DIRCARPETA = leer["dircarpeta"].ToString();
                IDSERIAL = leer["idserial"].ToString();

                CALLEFIJO = leer["calle"].ToString();
                MUNICIPIOFIJO = leer["municipio"].ToString();
                LOCALIDADFIJO = leer["localidad"].ToString();
                ESTADOFIJO = leer["estado"].ToString();
                NUMEXTFIJO = leer["numext"].ToString();
                NUMINTFIJO = leer["numint"].ToString();
                PAISFIJO = leer["pais"].ToString();
                CODPOSTALFIJO = leer["codpostal"].ToString();
                COLONIAFIJO = leer["colonia"].ToString();
                PASARAPRODUCTIVO = leer["productivo"].ToString();
                Numcopias = int.Parse(leer["numcopias"].ToString());
                NOMBRECOMERCIAL = leer["nombrecomercial"].ToString();
                if (LOCALIDADFIJO.Trim()=="")
                    DIRECCIONFISCALEMITIO = CALLEFIJO + " Num. " + NUMEXTFIJO + ", " + COLONIAFIJO + ", " + MUNICIPIOFIJO + "," + ESTADOFIJO + ". C.P " +  CODPOSTALFIJO;
                else
                    DIRECCIONFISCALEMITIO = CALLEFIJO + " Num. " + NUMEXTFIJO + ", " + COLONIAFIJO + ", " + LOCALIDADFIJO + "," + MUNICIPIOFIJO + ", " + ESTADOFIJO + ". C.P " + CODPOSTALFIJO;

                INFOADICIONAL = leer["infoadicional"].ToString();
            }
            conecta.CierraConexion();
        }


        public void LimpiarVariables()
        {
          numfactura = "";
          estatus = "";
          idsistemapadre = "";
          edocomprobante = "";
          tipo = "";
          RFCEmitio = "";
          CondicionesPago = "";
          FormaPago = "";
          Descuento = "";
          motivoDescuento = "";
          metodoPago = "";
          subtotal = "";
          total = "";
          REClave = "";
          ReNombre = "";
          ReRFC = "";
          ReCalle = "";
          ReCodpostal = "";
          ReColonia = "";
          ReEstado = "";
          ReLocalidad = "";
          ReMunicipio = "";
          ReNoExterior = "";
          ReNoInterior = "";
          ReTel = "";
          RePais = "";
          ReReferencia = "";
          Recorreo = "";
          TImpuestosRetenido = "";
          TImpuestoTrasladado = "";
          RImpuesto = "";
          RImporte = "";
          TImpuesto = "";
          TImporte = "";
          TTasa = "";
          Notas = "";
          moneda = "";
          TipoCambio = "";
          Vendedor = "";
          OrdCompra = "";
          Otros = "";
          numCtaPago = "";
          numInterior = "";
          ayo = "";
          mes = "";
          Fecha = "";
          FechaCod = "";
          Hora = "";
          Fechafactura = "";
          Fcodfactura = "";
          Horafactura = "";
          imagenCBB = "";
          cadenaOriginal = "";
          UUID = "";
          selloCFD = "";
          selloSat = "";
          serieSat = "";
          Emitio = "";
          cvcontrato = "";
          cvcliente = "";
          direccion = "";
          observaciones = "";
          cantletra = "";
          numpedido = "";
        }

        public void BusquedaUsuario()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from parametros where abrirfactura<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                USUARIOFAC = leer["usuarioFolio"].ToString();
                CONTRASEÑAFAC = leer["contraFolio"].ToString();
                CORREOFAC = leer["correover"].ToString();
                CADDIRECCION = leer["dirRespaldo"].ToString();
                ABRIRPDF = leer["abrirfactura"].ToString();
                IMPRESIONDIRECTA = leer["impresiondirecta"].ToString();
            }
            conecta.CierraConexion();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        public void CorreoDestino()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select email,email2 from clientes where cvcliente='" + REClave + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                CORREODESTINATARIO = leer["email"].ToString();
                CORREODESTINATARIO2 = leer["email2"].ToString();
            }
            conecta.CierraConexion();

        }


        public void MandarFacturar(string numpedido, string ayo)
        {

            string foliofiscal = "";
            string certificadosat = "";
            string certificadoemisor = "";
            string fechayhora = "";
            string selloCFDI = "";
            string selloSat = "";
            string CadenaOriginal = "";
            string cantletra = "";
            string observacion = "";
            string metodopago = "";
            string RFCusuario = "";
            string nombrepro = "";

            string direccion = "";
            string conceptos = "";
            string cadprecios = "";
            string total = "";
            string numint = "";
            string iva = "";

            string expedido = "";
            string infoadiciona = "";
            string direccionfiscal = "";
            string vendedor = "";
            string cadprecios1 = "";
            string cadclaves = "";
            string cadunidad = "";
            string cadcantidad = "";
            string condicionpago = "";
            string numcuenta = "";
            string razonsocial = "";
            string nombrecomercial = "";
            string mrfc = "";
            string mregimen = "";



            ReportDocument cryRpt = new ReportDocument();
            int restanTimbres = int.Parse(label3.Text);
            string Query = "";
            conectorSql conecta2 = new conectorSql();
            conectorSql conecta = new conectorSql();
            LimpiarVariables();
            if (FacturarcionOnline(numpedido, ayo) == true && TieneTimbresFacturar()==true)
            {

                string CadenaReporte = Application.StartupPath + "\\Factura.rpt";

                Query = "Select * from facturas where numfactura='" + numpedido + "' and ayo='" + ayo + "'";
                SqlDataReader leer2 = conecta2.RecordInfo(Query);
                while (leer2.Read())
                {

                    foliofiscal = leer2["folioFiscal"].ToString();
                    certificadoemisor = leer2["certificadoEmisor"].ToString();
                    certificadosat = leer2["certificadoSat"].ToString();
                    fechayhora = leer2["fechahoracert"].ToString();
                    selloCFDI = leer2["selloCDFI"].ToString();
                    selloSat = leer2["selloSATM"].ToString();
                    CadenaOriginal = leer2["Cadena"].ToString();

                    cvcliente = leer2["cvcliente"].ToString();
                    cantletra = leer2["cantletra"].ToString();
                    observacion = leer2["observaciones"].ToString();
                    metodopago = leer2["metodopago"].ToString();
                    RFCusuario = leer2["ReRFC"].ToString();
                    nombrepro = leer2["ReNombre"].ToString();
                    direccion = leer2["ReCalle"].ToString() + " Num. " + leer2["ReNoexterior"].ToString() + " ," + leer2["ReColonia"].ToString() + ", " + leer2["ReMunicipio"].ToString() + ", " + leer2["ReEstado"].ToString() + " C.P " + leer2["ReCodPostal"].ToString();
                    total = leer2["total"].ToString();
                    subtotal = leer2["subtotal"].ToString();
                    iva = leer2["Timporte"].ToString();

                    vendedor = leer2["vendedor"].ToString();
                    condicionpago = leer2["CondicionesPago"].ToString();
                    FormaPago = leer2["formapago"].ToString();
                    numcuenta = leer2["numctapago"].ToString();
                }
                conecta2.CierraConexion();


                cadcantidad = "";
                cadunidad = "";
                cadclaves = "";
                conceptos = "";
                cadprecios = "";
                cadprecios1 = "";
                Query = "select * from DetallesFacturas where numfactura='" + numpedido + "' and ayo='" + ayo + "' order by numpartida asc";
                leer2 = conecta2.RecordInfo(Query);
                while (leer2.Read())
                {

                    cadcantidad = cadcantidad + leer2["cantidad"].ToString() + "\n";
                    cadunidad = cadunidad + leer2["unidad"].ToString() + "\n";
                    cadclaves = cadclaves + leer2["cvproducto"].ToString() + "\n";
                    conceptos = conceptos + leer2["descripcion"].ToString() + "\n";
                    decimal valorUnitario = decimal.Parse(leer2["valorunitario"].ToString());
                    decimal valorimporte= decimal.Parse(leer2["importe"].ToString());

                    cadprecios1 = cadprecios1 + " $ " + valorUnitario.ToString("#,#.00", CultureInfo.InvariantCulture) + "\n";
                    cadprecios = cadprecios + " $ " + valorimporte.ToString("#,#.00", CultureInfo.InvariantCulture) + "\n";

                }
                conecta2.CierraConexion();

                NombreSetFactura();

                cryRpt.Load(CadenaReporte);
                
                string consulta = "SELECT numrecibo, codbidimensional FROM facturasbidimensional where numrecibo='" + numpedido + "'";
                ImagenB CodigoBidimensional = GetData(consulta, numpedido);
                cryRpt.SetDataSource(CodigoBidimensional);

                //consulta = "SELECT cvempresa, foto FROM LogoEmpresa where cvempresa='0'";
                //LogEmpresa LogoEmpresa= GetDataEmpresa(consulta);
                //cryRpt.SetDataSource(LogoEmpresa);


                decimal TotalPesos = decimal.Parse(total);
                decimal SubTotalPesos = decimal.Parse(subtotal);
                decimal IVA= decimal.Parse(iva);
              
                cryRpt.SetParameterValue("selloCFDI", selloCFDI);
                cryRpt.SetParameterValue("selloSat", selloSat);
                cryRpt.SetParameterValue("cadenaOriginal", CadenaOriginal);
                cryRpt.SetParameterValue("numint", numpedido);
                cryRpt.SetParameterValue("contrato", cvcliente);
                cryRpt.SetParameterValue("nombrepro", nombrepro);
                cryRpt.SetParameterValue("direccion", direccion);

                

                cryRpt.SetParameterValue("cantletra", cantletra.ToUpper());
                cryRpt.SetParameterValue("observacion", observacion);
                cryRpt.SetParameterValue("foliofiscal", foliofiscal);
                cryRpt.SetParameterValue("certificadoSat", certificadosat);
                cryRpt.SetParameterValue("fechayHora", fechayhora);
                cryRpt.SetParameterValue("CertificadoEmisor", certificadoemisor);
                cryRpt.SetParameterValue("Metodopago", metodopago);
                cryRpt.SetParameterValue("RFCusuario", RFCusuario);

                cryRpt.SetParameterValue("mregimen", REGIMEN);
                cryRpt.SetParameterValue("mrfc", RFC);
                cryRpt.SetParameterValue("nombrecomercial", NOMBRECOMERCIAL);
                cryRpt.SetParameterValue("razonsocial", NOMBRE);

                cryRpt.SetParameterValue("numcuenta", numcuenta);
                cryRpt.SetParameterValue("condicionpago", condicionpago);

                cryRpt.SetParameterValue("cadcantidad", cadcantidad);
                cryRpt.SetParameterValue("cadunidad", cadunidad);
                cryRpt.SetParameterValue("cadclaves", cadclaves);
                cryRpt.SetParameterValue("conceptos", conceptos);
                cryRpt.SetParameterValue("cadprecios", cadprecios);
                cryRpt.SetParameterValue("cadprecios1", cadprecios1);
                
                cryRpt.SetParameterValue("vendedor", vendedor);
                cryRpt.SetParameterValue("tsubtotal","$ " + SubTotalPesos.ToString("#,#.00", CultureInfo.InvariantCulture));
                cryRpt.SetParameterValue("tiva", "$ " + IVA.ToString("#,#.00", CultureInfo.InvariantCulture));
                cryRpt.SetParameterValue("total", "$ " + TotalPesos.ToString("#,#.00", CultureInfo.InvariantCulture));

                cryRpt.SetParameterValue("direccionfiscal", DIRECCIONFISCALEMITIO);
                cryRpt.SetParameterValue("infoadicional", INFOADICIONAL);
                cryRpt.SetParameterValue("expedido", LUGAREXPEDICION);
                cryRpt.SetParameterValue("formapago", FormaPago);


                string NombreArchivo = "";
              
                if (NOMBREDEFACTURA.Trim() == "")
                    NombreArchivo = DIRCARPETA + "\\FACTURA_" + numpedido + ".pdf";
                else
                    NombreArchivo = DIRCARPETA + "\\" + NOMBREDEFACTURA + "_" + numpedido + ".pdf";

                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, NombreArchivo);
               
                
                if (IMPRESIONDIRECTA=="SI") cryRpt.PrintToPrinter(Numcopias, false, 0, 0);
                cryRpt.Close();
                cryRpt.Dispose();

                if (ABRIRPDF == "SI") AbrirPdfFactura(NombreArchivo);
                MessageBox.Show("Se facturo correctamente","Facturar Electronicamente",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                string CadenaReporte = Application.StartupPath + "\\FacturaAuxiliar.rpt";

                Query = "Select * from facturas where numfactura='" + numpedido + "' and ayo='" + ayo + "'";
                SqlDataReader leer2 = conecta2.RecordInfo(Query);
                while (leer2.Read())
                {

                    foliofiscal = leer2["folioFiscal"].ToString();
                    certificadoemisor = leer2["certificadoEmisor"].ToString();
                    certificadosat = leer2["certificadoSat"].ToString();
                    fechayhora = leer2["fechahoracert"].ToString();
                    selloCFDI = leer2["selloCDFI"].ToString();
                    selloSat = leer2["selloSATM"].ToString();
                    CadenaOriginal = leer2["Cadena"].ToString();

                    cvcliente = leer2["cvcliente"].ToString();
                    cantletra = leer2["cantletra"].ToString();
                    observacion = leer2["observaciones"].ToString();
                    metodopago = leer2["metodopago"].ToString();
                    RFCusuario = leer2["ReRFC"].ToString();
                    nombrepro = leer2["ReNombre"].ToString();
                    direccion = leer2["ReCalle"].ToString() + " Num. " + leer2["ReNoexterior"].ToString() + ", C.P " + leer2["ReCodPostal"].ToString() + " ," + leer2["ReColonia"].ToString() + ", " + leer2["ReMunicipio"].ToString() + ", " + leer2["ReEstado"].ToString();
                    total = leer2["total"].ToString();
                    subtotal = leer2["subtotal"].ToString();
                    iva = leer2["Timporte"].ToString();

                    vendedor = leer2["vendedor"].ToString();
                    condicionpago = leer2["CondicionesPago"].ToString();
                    FormaPago = leer2["formapago"].ToString();
                    numcuenta = leer2["numctapago"].ToString();
                }
                conecta2.CierraConexion();


                cadcantidad = "";
                cadunidad = "";
                cadclaves = "";
                conceptos = "";
                cadprecios = "";
                cadprecios1 = "";
                Query = "select * from DetallesFacturas where numfactura='" + numpedido + "' and ayo='" + ayo + "' order by numpartida asc";
                leer2 = conecta2.RecordInfo(Query);
                while (leer2.Read())
                {

                    cadcantidad = cadcantidad + leer2["cantidad"].ToString() + "\n";
                    cadunidad = cadunidad + leer2["unidad"].ToString() + "\n";
                    cadclaves = cadclaves + leer2["cvproducto"].ToString() + "\n";
                    conceptos = conceptos + leer2["descripcion"].ToString() + "\n";
                    cadprecios1 = cadprecios1 + " $ " + leer2["valorunitario"].ToString() + "\n";
                    cadprecios = cadprecios + " $ " + leer2["importe"].ToString() + "\n";

                }
                conecta2.CierraConexion();

                NombreSetFactura();

                cryRpt.Load(CadenaReporte);

                string consulta = "SELECT numrecibo, codbidimensional FROM facturasbidimensional where numrecibo='" + numpedido + "'";
                ImagenB CodigoBidimensional = GetData(consulta, numpedido);
                cryRpt.SetDataSource(CodigoBidimensional);


                decimal TotalPesos = decimal.Parse(total);
                decimal SubTotalPesos = decimal.Parse(subtotal);
                decimal IVA = decimal.Parse(iva);

                cryRpt.SetParameterValue("selloCFDI", selloCFDI);
                cryRpt.SetParameterValue("selloSat", selloSat);
                cryRpt.SetParameterValue("cadenaOriginal", CadenaOriginal);
                cryRpt.SetParameterValue("numint", numpedido);
                cryRpt.SetParameterValue("contrato", cvcliente);
                cryRpt.SetParameterValue("nombrepro", nombrepro);
                cryRpt.SetParameterValue("direccion", direccion);

                cryRpt.SetParameterValue("cantletra", cantletra.ToUpper());
                cryRpt.SetParameterValue("observacion", observacion);
                cryRpt.SetParameterValue("foliofiscal", foliofiscal);
                cryRpt.SetParameterValue("certificadoSat", certificadosat);
                cryRpt.SetParameterValue("fechayHora", fechayhora);
                cryRpt.SetParameterValue("CertificadoEmisor", certificadoemisor);
                cryRpt.SetParameterValue("Metodopago", metodopago);
                cryRpt.SetParameterValue("RFCusuario", RFCusuario);

                cryRpt.SetParameterValue("mregimen", REGIMEN);
                cryRpt.SetParameterValue("mrfc", RFC);
                cryRpt.SetParameterValue("nombrecomercial", NOMBRECOMERCIAL);
                cryRpt.SetParameterValue("razonsocial", NOMBRE);

                cryRpt.SetParameterValue("numcuenta", numcuenta);
                cryRpt.SetParameterValue("condicionpago", condicionpago);

                cryRpt.SetParameterValue("cadcantidad", cadcantidad);
                cryRpt.SetParameterValue("cadunidad", cadunidad);
                cryRpt.SetParameterValue("cadclaves", cadclaves);
                cryRpt.SetParameterValue("conceptos", conceptos);
                cryRpt.SetParameterValue("cadprecios", cadprecios);
                cryRpt.SetParameterValue("cadprecios1", cadprecios1);

                cryRpt.SetParameterValue("vendedor", vendedor);
                cryRpt.SetParameterValue("tsubtotal", "$ " + SubTotalPesos.ToString("#,#.00", CultureInfo.InvariantCulture));
                cryRpt.SetParameterValue("tiva", "$ " + IVA.ToString("#,#.00", CultureInfo.InvariantCulture));
                cryRpt.SetParameterValue("total", "$ " + TotalPesos.ToString("#,#.00", CultureInfo.InvariantCulture));

                cryRpt.SetParameterValue("direccionfiscal", DIRECCIONFISCALEMITIO);
                cryRpt.SetParameterValue("infoadicional", INFOADICIONAL);
                cryRpt.SetParameterValue("expedido", LUGAREXPEDICION);
                cryRpt.SetParameterValue("formapago", FormaPago);


                string NombreArchivo = "";

               NombreArchivo = DIRCARPETA + "\\RECIBOTEMP_" + numpedido + ".pdf";

                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, NombreArchivo);

                if (IMPRESIONDIRECTA == "SI") cryRpt.PrintToPrinter(2, false, 0, 0);
                cryRpt.Close();
                cryRpt.Dispose();

                if (ABRIRPDF == "SI") AbrirPdfFactura(NombreArchivo);
                MessageBox.Show("Se realizo un recibo auxiliar espere un momento y vuelva a intenta facturar en linea si su timbrado es mayor a 0", "Facturar Electronicamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }


        }


        public void ImpresiondePDFYatimbrada(string numpedido, string ayo)
        {

            MessageBox.Show("La factura ya se encuentra timbrada, se mostrara acontinuacion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string foliofiscal = "";
            string certificadosat = "";
            string certificadoemisor = "";
            string fechayhora = "";
            string selloCFDI = "";
            string selloSat = "";
            string CadenaOriginal = "";
            string cantletra = "";
            string observacion = "";
            string metodopago = "";
            string RFCusuario = "";
            string nombrepro = "";

            string direccion = "";
            string conceptos = "";
            string cadprecios = "";
            string total = "";
            string numint = "";
            string iva = "";

            string expedido = "";
            string infoadiciona = "";
            string direccionfiscal = "";
            string vendedor = "";
            string cadprecios1 = "";
            string cadclaves = "";
            string cadunidad = "";
            string cadcantidad = "";
            string condicionpago = "";
            string numcuenta = "";
            string razonsocial = "";
            string nombrecomercial = "";
            string mrfc = "";
            string mregimen = "";



            ReportDocument cryRpt = new ReportDocument();
            int restanTimbres = int.Parse(label3.Text);
            string Query = "";
            conectorSql conecta2 = new conectorSql();
            conectorSql conecta = new conectorSql();
            LimpiarVariables();

            string CadenaReporte = Application.StartupPath + "\\Factura.rpt";

            Query = "Select * from facturas where numfactura='" + numpedido + "'";
            SqlDataReader leer2 = conecta2.RecordInfo(Query);
            while (leer2.Read())
            {

                foliofiscal = leer2["folioFiscal"].ToString();
                certificadoemisor = leer2["certificadoEmisor"].ToString();
                certificadosat = leer2["certificadoSat"].ToString();
                fechayhora = leer2["fechahoracert"].ToString();
                selloCFDI = leer2["selloCDFI"].ToString();
                selloSat = leer2["selloSATM"].ToString();
                CadenaOriginal = leer2["Cadena"].ToString();

                cvcliente = leer2["cvcliente"].ToString();
                cantletra = leer2["cantletra"].ToString();
                observacion = leer2["observaciones"].ToString();
                metodopago = leer2["metodopago"].ToString();
                RFCusuario = leer2["ReRFC"].ToString();
                nombrepro = leer2["ReNombre"].ToString();
                direccion = leer2["ReCalle"].ToString() + " Num. " + leer2["ReNoexterior"].ToString() + " ," + leer2["ReColonia"].ToString() + ", " + leer2["ReMunicipio"].ToString() + ", " + leer2["ReEstado"].ToString() + " C.P " + leer2["ReCodPostal"].ToString()  ;
                total = leer2["total"].ToString();
                subtotal = leer2["subtotal"].ToString();
                iva = leer2["Timporte"].ToString();

                vendedor = leer2["vendedor"].ToString();
                condicionpago = leer2["CondicionesPago"].ToString();
                FormaPago = leer2["formapago"].ToString();
                numcuenta = leer2["numctapago"].ToString();

                CORREODESTINATARIO = leer2["REcorreo"].ToString();
                CORREODESTINATARIO2 = "";

                REClave = leer2["REClave"].ToString();

            }
            conecta2.CierraConexion();

            CorreoDestino();

            cadcantidad = "";
            cadunidad = "";
            cadclaves = "";
            conceptos = "";
            cadprecios = "";
            cadprecios1 = "";
            Query = "select * from DetallesFacturas where numfactura='" + numpedido + "' order by numpartida asc";
            leer2 = conecta2.RecordInfo(Query);
            while (leer2.Read())
            {

                cadcantidad = cadcantidad + leer2["cantidad"].ToString() + "\n";
                cadunidad = cadunidad + leer2["unidad"].ToString() + "\n";
                cadclaves = cadclaves + leer2["cvproducto"].ToString() + "\n";
                conceptos = conceptos + leer2["descripcion"].ToString() + "\n";
                decimal valorUnitario = decimal.Parse(leer2["valorunitario"].ToString());
                decimal valorimporte = decimal.Parse(leer2["importe"].ToString());

                cadprecios1 = cadprecios1 + " $ " + valorUnitario.ToString("#,#.00", CultureInfo.InvariantCulture) + "\n";
                cadprecios = cadprecios + " $ " + valorimporte.ToString("#,#.00", CultureInfo.InvariantCulture) + "\n";
            }
            conecta2.CierraConexion();

            NombreSetFactura();

            cryRpt.Load(CadenaReporte);

            string consulta = "SELECT numrecibo, codbidimensional FROM facturasbidimensional where numrecibo='" + numpedido + "'";
            ImagenB CodigoBidimensional = GetData(consulta, numpedido);
            cryRpt.SetDataSource(CodigoBidimensional);

            //consulta = "SELECT cvempresa, foto FROM LogoEmpresa where cvempresa='0'";
            //LogEmpresa LogoEmpresa= GetDataEmpresa(consulta);
            //cryRpt.SetDataSource(LogoEmpresa);


            decimal TotalPesos = decimal.Parse(total);
            decimal SubTotalPesos = decimal.Parse(subtotal);
            decimal IVA = decimal.Parse(iva);

            cryRpt.SetParameterValue("selloCFDI", selloCFDI);
            cryRpt.SetParameterValue("selloSat", selloSat);
            cryRpt.SetParameterValue("cadenaOriginal", CadenaOriginal);
            cryRpt.SetParameterValue("numint", numpedido);
            cryRpt.SetParameterValue("contrato", cvcliente);
            cryRpt.SetParameterValue("nombrepro", nombrepro);
            cryRpt.SetParameterValue("direccion", direccion);



            cryRpt.SetParameterValue("cantletra", cantletra.ToUpper());
            cryRpt.SetParameterValue("observacion", observacion);
            cryRpt.SetParameterValue("foliofiscal", foliofiscal);
            cryRpt.SetParameterValue("certificadoSat", certificadosat);
            cryRpt.SetParameterValue("fechayHora", fechayhora);
            cryRpt.SetParameterValue("CertificadoEmisor", certificadoemisor);
            cryRpt.SetParameterValue("Metodopago", metodopago);
            cryRpt.SetParameterValue("RFCusuario", RFCusuario);

            cryRpt.SetParameterValue("mregimen", REGIMEN);
            cryRpt.SetParameterValue("mrfc", RFC);
            cryRpt.SetParameterValue("nombrecomercial", NOMBRECOMERCIAL);
            cryRpt.SetParameterValue("razonsocial", NOMBRE);

            cryRpt.SetParameterValue("numcuenta", numcuenta);
            cryRpt.SetParameterValue("condicionpago", condicionpago);

            cryRpt.SetParameterValue("cadcantidad", cadcantidad);
            cryRpt.SetParameterValue("cadunidad", cadunidad);
            cryRpt.SetParameterValue("cadclaves", cadclaves);
            cryRpt.SetParameterValue("conceptos", conceptos);
            cryRpt.SetParameterValue("cadprecios", cadprecios);
            cryRpt.SetParameterValue("cadprecios1", cadprecios1);

            cryRpt.SetParameterValue("vendedor", vendedor);
            cryRpt.SetParameterValue("tsubtotal", "$ " + SubTotalPesos.ToString("#,#.00", CultureInfo.InvariantCulture));
            cryRpt.SetParameterValue("tiva", "$ " + IVA.ToString("#,#.00", CultureInfo.InvariantCulture));
            cryRpt.SetParameterValue("total", "$ " + TotalPesos.ToString("#,#.00", CultureInfo.InvariantCulture));

            cryRpt.SetParameterValue("direccionfiscal", DIRECCIONFISCALEMITIO);
            cryRpt.SetParameterValue("infoadicional", INFOADICIONAL);
            cryRpt.SetParameterValue("expedido", LUGAREXPEDICION);
            cryRpt.SetParameterValue("formapago", FormaPago);


            string NombreArchivo = "";
            string Archivo2 = DIRCARPETA + "\\XML_" + numpedido + ".xml";

            if (NOMBREDEFACTURA.Trim() == "")
                NombreArchivo = DIRCARPETA + "\\F" + numpedido + "_RIMP.pdf";
            else
                NombreArchivo = DIRCARPETA + "\\F" + numpedido + "_" + NOMBREDEFACTURA + "_RIMP.pdf";

            cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, NombreArchivo);

            ENVIARCORREO(CORREODESTINATARIO, NombreArchivo, Archivo2);

            if (IMPRESIONDIRECTA == "SI") cryRpt.PrintToPrinter(Numcopias, false, 0, 0);
            cryRpt.Close();
            cryRpt.Dispose();

            if (ABRIRPDF == "SI") AbrirPdfFactura(NombreArchivo);
            this.Dispose();

        }


        public void ENVIARCORREO(string correoDestino, string adjunto, string adjunto2)
        {
            string nombrecorreo = "";
            string correo = "";
            string contraseña = "";
            string salidasmtp = "";
            string puerto = "";
            string cuerpo = "";
            bool banssl = false;
            string valor = "";
            conectorSql conecta = new conectorSql();
            string Query = "Select * from configuracorreo ";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                nombrecorreo = leer["nombre"].ToString();
                correo = leer["correo"].ToString();
                contraseña = leer["contraseña"].ToString();
                salidasmtp = leer["salidasmtp"].ToString();
                puerto = leer["puerto"].ToString();
                cuerpo = leer["cuerpo"].ToString();
                valor = leer["ssl"].ToString();
                if (valor == "SI") banssl = true;
            }
            conecta.CierraConexion();


            SmtpClient client = new SmtpClient();
            client.Host = salidasmtp;
            client.Port = int.Parse(puerto);
            client.EnableSsl = banssl;

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(correo, contraseña);
            client.TargetName = nombrecorreo;
            //Preparando archivo adjunto


            //Enviando correo
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(correo, nombrecorreo);
            mail.To.Add(new MailAddress(correo)); // destino
            mail.To.Add(new MailAddress(correoDestino)); // destino
            mail.Subject = "Factura Electronica.";

            mail.IsBodyHtml = true;
            mail.Body = "<h2>Envio de Factura Electronica " + numpedido + " </h2><br/><br/>" + cuerpo;
            mail.Attachments.Add(new Attachment(adjunto, System.Net.Mime.MediaTypeNames.Application.Pdf));

            Attachment oAttch = new Attachment(adjunto2);
            mail.Attachments.Add(oAttch);
            //mail.Attachments.Add(new Attachment(adjunto2, System.Net.Mime.MediaTypeNames.Application.Zip));


            //mail.Attachments.Add(new Attachment(ms, "Documento.txt"));
            try
            {
                client.Send(mail);
                MessageBox.Show("Se envio el correo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception E)
            {
                MessageBox.Show("Error" + E.Message.ToString() + "\nINTENTE DE NUEVO MAS TARDE ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }



        }


        public bool TieneTimbresFacturar()
        {
            int totalFacturado = 0;
            int totalCancelado = 0;

            conectorSql conecta = new conectorSql();
            string Query = "Select count(*) total from facturas where estatus='FACTURADO' ";
            Query = Query + " and ayo='" + DateTime.Now.Year + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                totalFacturado = int.Parse(leer["total"].ToString());
            }
            conecta.CierraConexion();


            Query = "Select count(*) total from facturas where estatus='CANCELADO' ";
            Query = Query + " and ayo='" + DateTime.Now.Year + "'";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                totalCancelado = int.Parse(leer["total"].ToString());
            }
            conecta.CierraConexion();
      
            int total = totalFacturado + totalCancelado;
            label6.Text = total.ToString();

            // Licenciamiento licencia = new Licenciamiento();
            int totalComprado = 0;
            int totalRegistrado = 0;
            string LicenciaTotal = "";
            Query = "Select cvllave from LlavesSistema where cvllave<>'' ";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                LicenciaTotal = leer["cvllave"].ToString();
                totalComprado = totalComprado; // + licencia.CuantosTimbresGeneral(LicenciaTotal);
                totalRegistrado = totalRegistrado; // + licencia.SaberCuantosTimbres(LicenciaTotal);
            }
            conecta.CierraConexion();

            int Restan = totalComprado - total;
            label3.Text = Restan.ToString();
            if (Restan ==0)
            {
                return false;
            }

            return true;
        }

        public void AbrirPdfFactura(string Direccion)
        {
            try
            {
                System.Diagnostics.Process.Start(Direccion);
                this.Dispose();
            }
            catch (Exception er)
            {
                MessageBox.Show("" + er, "Error PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FacturarPedido_Load(object sender, EventArgs e)
        {
            ParametrosFacturacion();
            BusquedaUsuario();
            textBox1.Text = valoresg.NUMPEDIDO;
            textBox1.Focus();
        }


        private ImagenB GetData(string query, string numrecibo)
        {
            conectorSql conecta = new conectorSql();
            conecta.Abrirconexion();

            string conString = conecta.CADENACONEXION;
            conecta.CierraConexion();
         
            SqlCommand cmd = new SqlCommand(query);
            SqlCommand cmd2 = new SqlCommand("select  cvempresa, foto from LogoEmpresa where cvempresa='0'");

            SqlCommand cmd3 = new SqlCommand("select  numfactura as numrecibo, cantidad , unidad,cvproducto, descripcion, valorunitario , importe from DetallesFacturas where numfactura='" + numrecibo+"'");

            SqlDataAdapter sda2 = new SqlDataAdapter();

            ImagenB CodigoBid = new ImagenB();
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(CodigoBid, "DataTable1");

                    cmd2.Connection = con;
                    sda.SelectCommand = cmd2;
                    sda.Fill(CodigoBid, "DataTable2");

                    cmd3.Connection = con;
                    sda.SelectCommand = cmd3;
                    sda.Fill(CodigoBid, "DataTable3");
                }
            }




            return CodigoBid;
    

        }


  

        public bool FacturarcionOnline(string NUMRECIBO, string ayo)
        {
            bool FueFacturado = false;
            conectorSql conecta = new conectorSql();
            string Query = "Select * from facturas where numfactura='" + NUMRECIBO + "' and ayo='" + ayo+ "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                numfactura = leer["numfactura"].ToString();
                estatus = leer["estatus"].ToString();
                idsistemapadre = leer["idsistemapadre"].ToString();
                edocomprobante = leer["edocomprobante"].ToString();
                tipo = leer["tipo"].ToString();
                RFCEmitio = leer["RFCEmitio"].ToString();
                CondicionesPago = leer["CondicionesPago"].ToString();
                FormaPago = leer["FormaPago"].ToString();
                Descuento = leer["Descuento"].ToString();
                motivoDescuento = leer["motivoDescuento"].ToString();
                metodoPago = leer["metodoPago"].ToString();
                subtotal = leer["subtotal"].ToString();
                total = leer["total"].ToString();
                REClave = leer["REClave"].ToString();
                ReNombre = leer["ReNombre"].ToString();
                ReRFC = leer["ReRFC"].ToString();
                ReCalle = leer["ReCalle"].ToString();
                ReCodpostal = leer["ReCodpostal"].ToString();
                ReColonia = leer["ReColonia"].ToString();
                ReEstado = leer["ReEstado"].ToString();
                ReLocalidad = leer["ReLocalidad"].ToString();
                ReMunicipio = leer["ReMunicipio"].ToString();
                ReNoExterior = leer["ReNoExterior"].ToString();
                ReNoInterior = leer["ReNoInterior"].ToString();
                if (ReNoInterior.Length == 0) ReNoInterior = "0";

                ReTel = leer["ReTel"].ToString();
                RePais = leer["RePais"].ToString();
                ReReferencia = leer["ReReferencia"].ToString();
                Recorreo = leer["Recorreo"].ToString();
                TImpuestosRetenido = leer["TImpuestosRetenido"].ToString();
                TImpuestoTrasladado = leer["TImpuestoTrasladado"].ToString();
                RImpuesto = leer["RImpuesto"].ToString();
                RImporte = leer["RImporte"].ToString();
                TImpuesto = leer["TImpuesto"].ToString();
                TImporte = leer["TImporte"].ToString();
                TTasa = leer["TTasa"].ToString();
                Notas = leer["Notas"].ToString();
                moneda = leer["moneda"].ToString();
                TipoCambio = leer["TipoCambio"].ToString();
                Vendedor = leer["Vendedor"].ToString();
                OrdCompra = leer["OrdCompra"].ToString();
                Otros = leer["Otros"].ToString();
                numCtaPago = leer["numCtaPago"].ToString();
                numInterior = NUMRECIBO;
                ayo = leer["ayo"].ToString();
                mes = leer["mes"].ToString();
                Fecha = leer["Fecha"].ToString();
                FechaCod = leer["FechaCod"].ToString();
                Hora = leer["Hora"].ToString();
                Fechafactura = leer["Fechafactura"].ToString();
                Fcodfactura = leer["Fcodfactura"].ToString();
                Horafactura = leer["Horafactura"].ToString();
                imagenCBB = leer["imagenCBB"].ToString();
                cadenaOriginal = leer["cadenaOriginal"].ToString();
                UUID = leer["UUID"].ToString();
                selloCFD = leer["selloCFD"].ToString();
                selloSat = leer["selloSat"].ToString();
                serieSat = leer["serieSat"].ToString();
                Emitio = leer["Emitio"].ToString();
                cvcliente = leer["cvcliente"].ToString();
                direccion = leer["direccion"].ToString();
                observaciones = leer["observaciones"].ToString();
                cantletra = leer["cantletra"].ToString();
            }
            conecta.CierraConexion();




            string numpartida = "";
            string cantidad = "";
            string descripcion = "";
            string importe = "";
            string cvproducto = "";
            string unidad = "";
            string valorUnitario = "";
            string pedimentonum = "";
            string pedimentonombre = "";
            string pedimentofecha = "";
            string iva = "";
            string notas1 = "";
            string notas2 = "";
            string numpedido = "";
            string adicional = "";
            string cvunica = "";
            string fechacod = "";
            string fecha = "";

            int totalreg = 0;
            Query = "Select count(*) as total from  DetallesFacturas where numpedido='" + NUMRECIBO + "'";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                totalreg = int.Parse(leer["total"].ToString());
            }
            conecta.CierraConexion();
            string[,] MatrizDetalle = new string[totalreg, 17];
            int contador = 0;

            Query = "Select * from  DetallesFacturas where numpedido='" + NUMRECIBO + "'";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                numpartida = leer["numpartida"].ToString();
                cantidad = leer["cantidad"].ToString();
                descripcion = leer["descripcion"].ToString();
                importe = leer["importe"].ToString();
                cvproducto = leer["cvproducto"].ToString();
                unidad = leer["unidad"].ToString();
                valorUnitario = leer["valorUnitario"].ToString();
                pedimentonum = leer["pedimentonum"].ToString();
                pedimentonombre = leer["pedimentonombre"].ToString();
                pedimentofecha = leer["pedimentofecha"].ToString();
                iva = leer["iva"].ToString();
                notas1 = leer["notas1"].ToString();
                notas2 = leer["notas2"].ToString();
                numpedido = leer["numpedido"].ToString();
                adicional = leer["adicional"].ToString();
                cvunica = leer["cvunica"].ToString();

                MatrizDetalle[contador, 0] = numpartida;
                MatrizDetalle[contador, 1] = cantidad;
                MatrizDetalle[contador, 2] = descripcion;
                MatrizDetalle[contador, 3] = importe;
                MatrizDetalle[contador, 4] = cvproducto;
                MatrizDetalle[contador, 5] = unidad;
                MatrizDetalle[contador, 6] = valorUnitario;
                MatrizDetalle[contador, 7] = pedimentonum;
                MatrizDetalle[contador, 8] = pedimentonombre;
                MatrizDetalle[contador, 9] = pedimentofecha;
                MatrizDetalle[contador, 10] = iva;
                MatrizDetalle[contador, 11] = notas1;
                MatrizDetalle[contador, 12] = notas2;
                MatrizDetalle[contador, 13] = numpedido;
                MatrizDetalle[contador, 14] = adicional;
                MatrizDetalle[contador, 15] = cvunica;
                contador++;
            }
            conecta.CierraConexion();

            //Este ejemplo está dirigido a aquellos integradores que aún no generan el xml (CFDI)

            //Inicializamos el conector el parámetro indica el ambiente en el que se utilizará 
            //Fasle = Ambiente de pruebas
            //True = Ambiente de producción
            bool banderaPro = false;
            if (PASARAPRODUCTIVO == "SI") banderaPro = true;

            Conector conector = new Conector(banderaPro);

            //Establecemos las credenciales para el permiso de conexión
            //Ambiente de pruebas = mvpNUXmQfK8=

            //Ambiente de producción = Esta será asignado por el proveedor al salir a productivo
            conector.EstableceCredenciales(USUARIO);

            //Creamos un comprobante por medio de la entidad Comprobante
            Comprobante comprobante = new Comprobante();

            //Llenamos datos del comprobante
            comprobante.serie = "F";
            comprobante.folio = NUMRECIBO;
            comprobante.fecha = DateTime.Now;
            comprobante.formaDePago = FormaPago;
            comprobante.metodoDePago = metodoPago;
            comprobante.subTotal = decimal.Parse(subtotal);
            comprobante.total = decimal.Parse(total);
            //TIPO DE COMPROBANTE 
            //Ingreso: Factura 1, Rec honorarios 4, rec de arrendamiento 5, Rec donativos 7, Nota de cargo 3
            //Egreso: Nota de credito 2
            //Traslado: Carta porte  6
            comprobante.tipoDeComprobante = ComprobanteTipoDeComprobante.egreso;
            comprobante.LugarExpedicion = LUGAREXPEDICION;

            //Llenamos datos del emisor
            comprobante.Emisor = new ComprobanteEmisor();
            comprobante.Emisor.rfc = RFC.Trim();
            comprobante.Emisor.nombre = NOMBRE;

            if (NUMINTFIJO == "") NUMINTFIJO = "S/N";
            //Llenamos domicilio fiscal del emisor
            comprobante.Emisor.DomicilioFiscal = new t_UbicacionFiscal();
            comprobante.Emisor.DomicilioFiscal.calle = CALLEFIJO;
            comprobante.Emisor.DomicilioFiscal.noExterior = NUMEXTFIJO;
            comprobante.Emisor.DomicilioFiscal.noInterior = NUMINTFIJO;
            comprobante.Emisor.DomicilioFiscal.colonia = COLONIAFIJO;
            comprobante.Emisor.DomicilioFiscal.municipio = MUNICIPIOFIJO;
            comprobante.Emisor.DomicilioFiscal.estado = ESTADOFIJO;
            comprobante.Emisor.DomicilioFiscal.pais = PAISFIJO;
            comprobante.Emisor.DomicilioFiscal.codigoPostal = CODPOSTALFIJO;

            //Llenamos regimen fiscal del emisor
            comprobante.Emisor.RegimenFiscal = new ComprobanteEmisorRegimenFiscal[1];
            comprobante.Emisor.RegimenFiscal[0] = new ComprobanteEmisorRegimenFiscal();
            comprobante.Emisor.RegimenFiscal[0].Regimen = REGIMEN;

            ////Llena datos de expedido en (Solo en caso de que el comprobante haya sido expedido en una sucursal y no en la matriz
            //comprobante.Emisor.ExpedidoEn = new t_Ubicacion();
            //comprobante.Emisor.ExpedidoEn.calle = "Calle expedido en";
            //comprobante.Emisor.ExpedidoEn.noExterior = "2";
            //comprobante.Emisor.ExpedidoEn.noInterior = "B";
            //comprobante.Emisor.ExpedidoEn.colonia = "Colonia expedido en";
            //comprobante.Emisor.ExpedidoEn.municipio = "Municipio expedido en";
            //comprobante.Emisor.ExpedidoEn.estado = "Estado expedido en";
            //comprobante.Emisor.ExpedidoEn.pais = "Mexico";
            //comprobante.Emisor.ExpedidoEn.codigoPostal = "53120";

            //Llena datos del receptor
            comprobante.Receptor = new ComprobanteReceptor();
            comprobante.Receptor.rfc = ReRFC;
            comprobante.Receptor.nombre = ReNombre;

            if (ReNoExterior.Trim() == "") ReNoExterior = "S/N";
            if (ReCodpostal.Trim() == "") ReCodpostal = CODPOSTALFIJO;

            //Llena domicilio del receptor
            comprobante.Receptor.Domicilio = new t_Ubicacion();
            comprobante.Receptor.Domicilio.calle = ReCalle;
            comprobante.Receptor.Domicilio.noExterior = ReNoExterior;
            comprobante.Receptor.Domicilio.noInterior = ReNoInterior;
            comprobante.Receptor.Domicilio.colonia = ReColonia;
            comprobante.Receptor.Domicilio.municipio = ReMunicipio;
            comprobante.Receptor.Domicilio.estado = ReEstado;
            comprobante.Receptor.Domicilio.pais = RePais;
            comprobante.Receptor.Domicilio.codigoPostal = ReCodpostal;

            //Llenamos los conceptos
            comprobante.Conceptos = new ComprobanteConcepto[totalreg];

            //Concepto 1
            for (int i = 0; i < totalreg; i++)
            {
                comprobante.Conceptos[i] = new ComprobanteConcepto();
                comprobante.Conceptos[i].cantidad = 1;
                comprobante.Conceptos[i].unidad = MatrizDetalle[i, 5];

                comprobante.Conceptos[i].noIdentificacion = MatrizDetalle[i, 4];
                comprobante.Conceptos[i].descripcion = MatrizDetalle[i, 2];
                comprobante.Conceptos[i].valorUnitario = decimal.Parse(MatrizDetalle[i, 6]);
                comprobante.Conceptos[i].importe = decimal.Parse(MatrizDetalle[i, 3]);
            }

            ////Concepto 2
            //comprobante.Conceptos[1] = new ComprobanteConcepto();
            //comprobante.Conceptos[1].cantidad = 1;
            //comprobante.Conceptos[1].unidad = "PZA";
            //comprobante.Conceptos[1].noIdentificacion = "1";
            //comprobante.Conceptos[1].descripcion = "Prueba concepto 2";
            //comprobante.Conceptos[1].valorUnitario = 50;
            //comprobante.Conceptos[1].importe = 50;



            //---------------------------Llenamos impuestos--------------------------------------------------------
            //------------------------------------------------------------------------------------------------------
      
            comprobante.Impuestos = new ComprobanteImpuestos();
            comprobante.Impuestos.Traslados = new ComprobanteImpuestosTraslado[1];
            comprobante.Impuestos.Traslados[0] = new ComprobanteImpuestosTraslado();
            comprobante.Impuestos.Traslados[0].importe = decimal.Parse(TImporte);// 0; // total del impuesto
            comprobante.Impuestos.Traslados[0].impuesto = ComprobanteImpuestosTrasladoImpuesto.IVA;
            comprobante.Impuestos.Traslados[0].tasa = 16; // cuando es agua potable
            //----------------------------------------------------------------------------------------------------
            //-------------------------------------------------------------------------------------------------------
       
            //Timbramos el CFDI por medio del conector y guardamos resultado
            ResultadoTimbre resultadoTimbre;

            try
            {

                resultadoTimbre = conector.TimbraCFDI(comprobante);

                BitacoraParaFacturas(NUMRECIBO, "", "NO");
                //Verificamos el resultado
                if (resultadoTimbre.Exitoso)
                {
                    //El comprobante fue timbrado exitosamente
                    //Guardamos xml cfdi
                    if (resultadoTimbre.GuardaXml(DIRCARPETA, "XML_" + NUMRECIBO))
                    {
                        Query = "update bitacoraFacturas set guardaxml='SI' where numfactura='" + NUMRECIBO + "'";
                        conecta.Excute(Query);
                    }
                    else
                    {
                        Query = "update bitacoraFacturas set guardaxml='NO' where numfactura='" + NUMRECIBO + "'";
                        conecta.Excute(Query);
                    }

                    //Los siguientes datos deberán ir en la respresentación impresa ó PDF

                    //1.- Código bidimensional
                    if (resultadoTimbre.GuardaCodigoBidimensional(DIRCARPETA, "COD_" + NUMRECIBO))
                    {
                        byte[] codigoBidi = resultadoTimbre.CodigoBidimensional;
                        conectorSql mycon = new conectorSql();

                        // primero elimina la foto anterior si tiene
                        Query = "Delete from FacturasBidimensional where numrecibo='" + NUMRECIBO + "'";
                        bool existereg = mycon.Excute(Query);
                        mycon.CierraConexion();

                        string sql = "insert into FacturasBidimensional(numrecibo, codbidimensional)";
                        sql += " Values(@NRecibo, @Imagen)";

                        mycon.Abrirconexion();
                        SqlCommand SqlCom = new SqlCommand(sql, mycon.con);
                        SqlCom.Parameters.Add("@NRecibo", System.Data.SqlDbType.Int);
                        SqlCom.Parameters["@NRecibo"].Value = NUMRECIBO;
                        SqlCom.Parameters.Add("@Imagen", System.Data.SqlDbType.Image);
                        SqlCom.Parameters["@Imagen"].Value = codigoBidi;
                        SqlCom.ExecuteNonQuery();
                        mycon.CierraConexion();
                        BitacoraParaFacturas(NUMRECIBO, "Se guardo el codigo Bidimensional", "");
                    }
                    else
                    {
                        BitacoraParaFacturas(NUMRECIBO, "Error en el codigo Bidimensional", "");
                    }

                    //2.- Folio fiscal
                    string folioFiscal = resultadoTimbre.FolioUUID;

                    //3.- No. Certificado del Emisor
                    string noCertificado = resultadoTimbre.No_Certificado;

                    //4.- No. Certificado del SAT
                    string noCertificadoSAT = resultadoTimbre.No_Certificado_SAT;

                    //5.- Fecha y Hora de certificación
                    string fechaCert = resultadoTimbre.FechaCertificacion;

                    //6.- Sello del cfdi
                    string selloCFDI = resultadoTimbre.SelloCFDI;

                    //7.- Sello del SAT
                    string selloSAT = resultadoTimbre.SelloSAT;

                    //8.- Cadena original de complemento de certificación
                    string cadena = resultadoTimbre.CadenaTimbre;


                    Query = "Update facturas set";
                    Query = Query + " Fechafactura='" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
                    Query = Query + " ,Fcodfactura='" + DateTime.Now.ToString("yyyyMMdd") + "'";
                    Query = Query + " ,Horafactura='" + DateTime.Now.ToString("yyyyMMdd") + "'";
                    Query = Query + " ,cadenaOriginal='" + cadena + "'";
                    Query = Query + " ,UUID='" + folioFiscal + "'";
                    Query = Query + " ,selloCFD='" + selloCFDI + "'";
                    Query = Query + " ,selloSat='" + selloSAT + "'";
                    Query = Query + " ,estatus='FACTURADO'";

                    Query = Query + " ,folioFiscal='" + folioFiscal + "'";
                    Query = Query + " ,certificadoEmisor='" + noCertificado + "'";
                    Query = Query + " ,certificadoSat='" + noCertificadoSAT + "'";
                    Query = Query + " ,fechahoracert='" + fechaCert + "'";
                    Query = Query + " ,selloCDFI='" + selloCFDI + "'";
                    Query = Query + " ,selloSATM='" + selloSAT + "'";
                    Query = Query + " ,Cadena='" + cadena + "'";

                    Query = Query + "  where numfactura='" + NUMRECIBO + "'";
                    Query = Query + "  and ayo='" + ayo + "'";
                    conecta.Excute(Query);

                    Query = "Update Detallesfacturas set numfactura='" + NUMRECIBO + "' where numpedido='" + NUMRECIBO + "'";
                    conecta.Excute(Query);

                    Query = "Update pedidos set estatuspedido='FACTURADO' where numpedido='" + NUMRECIBO + "'";
                    conecta.Excute(Query);

                    //actualizar el registro para pasarlo a la facturacion
                    FueFacturado = true;
                    BitacoraParaFacturas(NUMRECIBO, "Timbrado Exitoso", "SI");
                }
                else
                {
                    FueFacturado = false;
                    //No se pudo timbrar, mostramos respuesta
                    BitacoraParaFacturas(NUMRECIBO, resultadoTimbre.Descripcion, "NO");
                    //MessageBox.Show(resultadoTimbre.Descripcion);
                }
            }
            catch (Exception)
            {

                FueFacturado = false;
            }


            return FueFacturado;
        }

        public void BitacoraParaFacturas(string NUMRECIBO, string mensaje, string timbrado)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from bitacorafacturas where numfactura='" + NUMRECIBO + "'";
            bool existe = conecta.ExisteRegistro(Query);
            if (existe == false)
            {
                Query = "Insert into bitacorafacturas(Numfactura";
                Query = Query + ", mensaje";
                Query = Query + ",timbrado";
                Query = Query + ",fecha";
                Query = Query + ",fechacod";
                Query = Query + ",hora";
                Query = Query + ",guardapdf";
                Query = Query + ",guardaxml";
                Query = Query + ",nombrepdf";
                Query = Query + ",nombrexml";
                Query = Query + ",correo";
                Query = Query + ",enviomail)";
                Query = Query + " values(";
                Query = Query + "'" + NUMRECIBO + "'";
                Query = Query + ",'" + mensaje + "'";
                Query = Query + ",'" + timbrado + "'";
                Query = Query + ",'" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
                Query = Query + ",'" + DateTime.Now.ToString("yyyyMMdd") + "'";
                Query = Query + ",'" + DateTime.Now.ToString("HH:mm:ss") + "'";
                Query = Query + ",'NO'";
                Query = Query + ",'NO'";
                Query = Query + ",'FACTURA_" + NUMRECIBO + ".pdf'";
                Query = Query + ",'XML_" + NUMRECIBO + ".xml'";
                Query = Query + ",'s/c'";
                Query = Query + ",'NO')";
                conecta.Excute(Query);

            }
            else
            {
                Query = "update bitacorafacturas set ";
                Query = Query + " mensaje='" + mensaje + "'";
                Query = Query + ",timbrado='" + timbrado + "'";
                Query = Query + ", fecha='" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
                Query = Query + ", fechacod='" + DateTime.Now.ToString("yyyyMMdd") + "'";
                Query = Query + ",hora='" + DateTime.Now.ToString("HH:mm:ss") + "'";
                Query = Query + " where Numfactura='" + NUMRECIBO + "'";
                conecta.Excute(Query);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string numpedido = textBox1.Text;
            string ayo = valoresg.AYOPEDIDO;
            valoresg.NUMPEDIDOCARGAR = numpedido;
            if (fuefacturado(numpedido, ayo) == true)
                ImpresiondePDFYatimbrada(numpedido, ayo);
        }

        public bool fuefacturado(string numpedido, string ayo)
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from facturas where numfactura='" + numpedido + "'";
            bool encontrado = conecta.ExisteRegistro(Query);
            return encontrado;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(sender, e);
        }


        public void TotalFacturasReg()
        {
            int totalFacturado = 0;
            int totalCancelado = 0;

            conectorSql conecta = new conectorSql();
            string Query = "Select count(*) total from facturas where estatus='FACTURADO' ";
            Query = Query + " and ayo='" + DateTime.Now.Year + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                totalFacturado = int.Parse(leer["total"].ToString());
            }
            conecta.CierraConexion();


            Query = "Select count(*) total from facturas where estatus='CANCELADO' ";
            Query = Query + " and ayo='" + DateTime.Now.Year + "'";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                totalCancelado = int.Parse(leer["total"].ToString());
            }
            conecta.CierraConexion();
            totalCancelado = totalCancelado * 2;
            int total = totalFacturado + totalCancelado;
            label6.Text = total.ToString();

            // Licenciamiento licencia = new Licenciamiento();
            int totalComprado = 0;
            int totalRegistrado = 0;
            string LicenciaTotal = "";
            Query = "Select cvllave from LlavesSistema where cvllave<>'' ";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                LicenciaTotal = leer["cvllave"].ToString();
                totalComprado = totalComprado; // + licencia.CuantosTimbresGeneral(LicenciaTotal);
                totalRegistrado = totalRegistrado; // + licencia.SaberCuantosTimbres(LicenciaTotal);
            }
            conecta.CierraConexion();

            int Restan = totalComprado - total;
            label3.Text = Restan.ToString();
            if (Restan < 10)
            {
                MessageBox.Show("Comuniquese con  su proveedor Soluciones SIA.\nTelefono: 01 (777) 289-68-19\nCorreo: ventas@soluciones-sia.com\nQuedan pocos timbres para facturar\nQuedan " + Restan + " Timbres.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


        }
    }
}
