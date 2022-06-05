using System;
using System.Linq;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

class CTablas
    {

        private static SqlConnection con;
        private static SqlCommand comm;
        private static SqlConnection conCad;
        public static int MaximoT;
        public static int VProceso;

        private static void Abrirconexion()
        {
            con = new SqlConnection();

            string cfnFile = "//SRV-DATACENTER/tmp/EmailConf.xml";
            bool cfnExist = File.Exists(cfnFile);
            XDocument xdoc = XDocument.Load(cfnExist ? "//SRV-DATACENTER/tmp/EmailConf.xml" : "C:\\tmp\\EmailConf.xml");
            string vserver = xdoc.Descendants("ConnStr").First().Value;
            
            con.ConnectionString = @vserver;

            con.Open();
        }

        private static void CierraConexion()
        {
            con.Close();
        }

        public  static  void Funciones()
        {

            conectorSql sql = new conectorSql();
            string ejecuta = "CREATE function [dbo].[ContarProductos]";
            ejecuta = (((ejecuta + " (@cvproducto Nvarchar(20)) returns int" + " as") + " begin" + " Declare @Result as float") + " set @Result = (\tSelect cantidad as TOTAL from Productos" + " where cvproducto=@cvproducto )") + " return @Result" + " end";
            sql.Excute(ejecuta);
            sql.CierraConexion();
            ejecuta = " CREATE TRIGGER [dbo].[DisparaRecibos] ON [dbo].[DetallesRecibos]";
            ejecuta = (((((((ejecuta + "\n AFTER INSERT AS BEGIN SET NOCOUNT ON;") + "\n Declare @cvproducto nvarchar(20)" + "\n Declare @Cantidad float") + "\n Declare @CantidadReg float" + "\n Declare @Resultado float") + "\n SET @cvproducto = (SELECT cvproducto FROM inserted )" + "\n SET @Cantidad = (SELECT cantidad FROM inserted )") + "\n if (@cvproducto<>'')" + "\n BEGIN") + "\n set @CantidadReg= (Select dbo.ContarProductos(@cvproducto))" + "\n set @Resultado=@CantidadReg-@Cantidad") + "\n if  (@Resultado<0 ) set @Resultado=0" + "\n update productos set cantidad=@Resultado where cvproducto=@cvproducto") + "\n END" + "\n END";
            sql.Excute(ejecuta);
            sql.CierraConexion();
            ejecuta = " CREATE TRIGGER [dbo].[DisparaCancelados] ON [dbo].[DetalleCancela]";
            ejecuta = (((((((ejecuta + "\n AFTER INSERT AS BEGIN SET NOCOUNT ON;") + "\n Declare @cvproducto nvarchar(20)" + "\n Declare @Cantidad float") + "\n Declare @CantidadReg float" + "\n Declare @Resultado float") + "\n SET @cvproducto = (SELECT clave FROM inserted )" + "\n SET @Cantidad = (SELECT cantidad FROM inserted )") + "\n if (@cvproducto<>'')" + "\n BEGIN") + "\n set @CantidadReg= (Select dbo.ContarProductos(@cvproducto))" + "\n set @Resultado=@CantidadReg+@Cantidad") + "\n if  (@Resultado<0 ) set @Resultado=0" + "\n update productos set cantidad=@Resultado where cvproducto=@cvproducto") + "\n END" + "\n END";
            sql.Excute(ejecuta);
            sql.CierraConexion();
            ejecuta = " CREATE TRIGGER [dbo].[DisparaInventario] ON [dbo].[DetalleProvnota]";
            ejecuta = (((((((ejecuta + "\n AFTER INSERT AS BEGIN SET NOCOUNT ON;") + "\n Declare @cvproducto nvarchar(20)" + "\n Declare @Cantidad float") + "\n Declare @CantidadReg float" + "\n Declare @Resultado float") + "\n SET @cvproducto = (SELECT Clave FROM inserted )" + "\n SET @Cantidad = (SELECT Cantidad FROM inserted )") + "\n if (@cvproducto<>'')" + "\n BEGIN") + "\n set @CantidadReg= (Select dbo.ContarProductos(@cvproducto))" + "\n set @Resultado=@CantidadReg+@Cantidad") + "\n if  (@Resultado<0 ) set @Resultado=0" + "\n update productos set cantidad=@Resultado where cvproducto=@cvproducto") + "\n END" + "\n END";
            sql.Excute(ejecuta);
            sql.CierraConexion();

        }

        private static bool Excute(String Ejecuta, string Ctable, string Ccampo, string Tipo, string Valor)
        {
            string Query = "";
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
                if (exx.Number == 4902 || exx.Number == 156)
                {
                    Query = "CREATE TABLE " + Ctable + " (" + Ccampo + " " + Tipo + " " + Valor +")";
                    Excute(Query, Ctable, Ccampo, Tipo, Valor);
                }

                if (exx.Number == 4924)
                {
                    Query = "ALTER TABLE  " + Ctable + " ADD " + Ccampo + " " + Tipo + " " + Valor ;
                    Excute(Query,Ctable,Ccampo,Tipo,Valor);
                }
                CierraConexion();
                return false;
            }
        }

        public static void TablaUsuario()
        {

            ExeTabla("Usuarios", "cvusuario", "nvarchar", "(50)");
            ExeTabla("Usuarios", "nombre", "nvarchar", "(150)");
            ExeTabla("Usuarios", "contra", "nvarchar", "(50)");
            ExeTabla("Usuarios", "entra1", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra2", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra3", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra4", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra5", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra6", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra7", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra8", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra9", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra10", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra11", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra12", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra13", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra14", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra15", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra16", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra17", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra18", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra19", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra20", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra21", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra22", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra23", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra24", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra25", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra26", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra27", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra28", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra29", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra30", "nvarchar", "(2)");
            ExeTabla("Usuarios", "ESDOCTOR", "nvarchar", "(2)");
            ExeTabla("Usuarios", "CVDOCTOR", "nvarchar", "(20)");
            conectorSql conecta = new conectorSql();
            string Query = "Select * from usuarios where cvusuario='ADMIN'";
            bool EXISTE = conecta.ExisteRegistro(Query);
            if (EXISTE == false)
            {
                Query = "Insert into usuarios( cvusuario,contra,nombre,esdoctor) values('ADMIN','ADMIN','ADMINISTRADOR DEL SISTEMA','NO')";
                conecta.Excute(Query);
            }
        }

        public static void CreaRespaldo()
        {
            string pathString = "C:\\RESPALDOS";
            if (!System.IO.File.Exists(pathString))
            {
                System.IO.Directory.CreateDirectory(pathString);
            }
            string vbase = Registro.ReadRegSHOPCONTROL("CON", "BD");
            conectorSql conecta = new conectorSql();
            string Query="";

            try
            {

            Query=Query + " CREATE procedure [dbo].[backubill]";
           
            Query=Query + " as";
            Query=Query + " declare @fechaactual as nvarchar(10)";
            Query=Query + " declare @Cadena as nvarchar(300)";
            Query=Query + " declare @CadenaRespaldo nvarchar(100)";
            Query=Query + " set @fechaactual=CONVERT(VARCHAR(8), GETDATE(), 112)";
            Query=Query + " print @fechaactual";
            Query = Query + " set @CadenaRespaldo='C:\\RESPALDOS\\RESPALDOBILL_' + @fechaactual +'.bak'";
            Query = Query + " BACKUP DATABASE [" + vbase + "] TO  DISK =@CadenaRespaldo";
            Query=Query + " WITH NOFORMAT, NOINIT, NAME = N'BILLLINE-Completa', SKIP,NOREWIND, NOUNLOAD,  STATS = 10";
            bool Aceptado= conecta.Excute2(Query);
            if (Aceptado == false)
            {

                Query = Query + " ALTER procedure [dbo].[backubill]";
                Query = Query + " as";
                Query = Query + " declare @fechaactual as nvarchar(10)";
                Query = Query + " declare @Cadena as nvarchar(300)";
                Query = Query + " declare @CadenaRespaldo nvarchar(100)";
                Query = Query + " set @fechaactual=CONVERT(VARCHAR(8), GETDATE(), 112)";
                Query = Query + " print @fechaactual";
                Query = Query + " set @CadenaRespaldo='C:\\RESPALDOS\\RESPALDOBILL_' + @fechaactual +'.bak'";
                Query = Query + " BACKUP DATABASE [" + vbase + "] TO  DISK =@CadenaRespaldo";
                Query = Query + " WITH NOFORMAT, NOINIT, NAME = N'BILLLINE-Completa', SKIP,NOREWIND, NOUNLOAD,  STATS = 10";
                conecta.Excute2(Query);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            }

        public static void CATALOGOS()
        {

            ExeTabla("gastosreg", "cvgasto", "nvarchar", "(50)");
            ExeTabla("gastosreg", "nombre", "nvarchar", "(200)");
            ExeTabla("gastosreg", "descripcion", "nvarchar", "(500)");
            ExeTabla("gastosreg", "costo", "float", "");
            ExeTabla("gastosreg", "fecha", "nvarchar", "(10)");
            ExeTabla("gastosreg", "fechacod", "nvarchar", "(10)");
            ExeTabla("gastosreg", "hora", "nvarchar", "(10)");
            ExeTabla("gastosreg", "horacod", "nvarchar", "(10)");
            ExeTabla("gastosreg", "emitio", "nvarchar", "(20)");
            ExeTabla("gastosreg", "fecharealizo", "nvarchar", "(10)");
            ExeTabla("gastosreg", "frealizocod", "nvarchar", "(10)");
            ExeTabla("gastosreg", "telefono", "nvarchar", "(50)");
            ExeTabla("gastosreg", "tipo", "nvarchar", "(50)");
            ExeTabla("bancos", "cvbanco", "nvarchar", "(50)");
            ExeTabla("bancos", "nombre", "nvarchar", "(50)");
            ExeTabla("bancos", "cuenta", "nvarchar", "(50)");
            ExeTabla("bancos", "interbancaria", "nvarchar", "(50)");
            ExeTabla("bancos", "sucursal", "int", "");
            ExeTabla("bancos", "nombredeposito", "nvarchar", "(500)");
            ExeTabla("Cancelaciones", "numpedido", "int", "");
            ExeTabla("Cancelaciones", "numcancela", "int", "");
            ExeTabla("Cancelaciones", "cvcliente", "nvarchar", "(50)");
            ExeTabla("Cancelaciones", "ncliente", "nvarchar", "(250)");
            ExeTabla("Cancelaciones", "observacion", "nvarchar", "(200)");
            ExeTabla("Cancelaciones", "emitio", "nvarchar", "(50)");
            ExeTabla("Cancelaciones", "fecha", "nvarchar", "(10)");
            ExeTabla("Cancelaciones", "fechacod", "nvarchar", "(10)");
            ExeTabla("Cancelaciones", "CancelaTodo", "nvarchar", "(2)");
            ExeTabla("Cancelaciones", "TotalC", "float", "");
            ExeTabla("Cancelaciones", "CantidadLetra", "nvarchar", "(200)");
            ExeTabla("Cancelaciones", "vendedor", "nvarchar", "(50)");
            ExeTabla("DetalleCancela", "numpedido", "nvarchar", "(10)");
            ExeTabla("DetalleCancela", "numcancela", "nvarchar", "(10)");
            ExeTabla("DetalleCancela", "Clave", "nvarchar", "(20)");
            ExeTabla("DetalleCancela", "Unidad", "nvarchar", "(50)");
            ExeTabla("DetalleCancela", "Describe", "nvarchar", "(500)");
            ExeTabla("DetalleCancela", "Cantidad", "float", "");
            ExeTabla("DetalleCancela", "Unitario", "float", "");
            ExeTabla("DetalleCancela", "Total", "float", "");
            ExeTabla("Formadepago", "cvforma", "nvarchar", "(50)");
            ExeTabla("Formadepago", "nombre", "nvarchar", "(250)");
            ExeTabla("Comisiones", "cvvendedor", "nvarchar", "(50)");
            ExeTabla("Comisiones", "numrecibo", "nvarchar", "(50)");
            ExeTabla("Comisiones", "Total", "float", "");
            ExeTabla("Comisiones", "cancelado", "nvarchar", "(2)");
            ExeTabla("clientes", "cvcliente", "nvarchar", "(50)");
            ExeTabla("clientes", "nombre", "nvarchar", "(500)");
            ExeTabla("clientes", "telefono", "nvarchar", "(50)");
            ExeTabla("clientes", "email", "nvarchar", "(50)");
            ExeTabla("clientes", "email2", "nvarchar", "(50)");
            ExeTabla("clientes", "celular", "nvarchar", "(50)");
            ExeTabla("clientes", "direccion", "nvarchar", "(500)");
            ExeTabla("clientes", "rfc", "nvarchar", "(50)");
            ExeTabla("clientes", "direfiscal", "nvarchar", "(500)");
            ExeTabla("clientes", "empresa", "nvarchar", "(200)");
            ExeTabla("clientes", "factura", "nvarchar", "(2)");
            ExeTabla("clientes", "activo", "nvarchar", "(2)");
            ExeTabla("clientes", "calleE", "nvarchar", "(100)");
            ExeTabla("clientes", "ColoniaE", "nvarchar", "(100)");
            ExeTabla("clientes", "MunicipioE", "nvarchar", "(100)");
            ExeTabla("clientes", "EstadoE", "nvarchar", "(100)");
            ExeTabla("clientes", "CodE", "nvarchar", "(10)");
            ExeTabla("clientes", "PaisE", "nvarchar", "(100)");
            ExeTabla("clientes", "CalleF", "nvarchar", "(100)");
            ExeTabla("clientes", "ColoniaF", "nvarchar", "(100)");
            ExeTabla("clientes", "MunicipioF", "nvarchar", "(100)");
            ExeTabla("clientes", "EstadoF", "nvarchar", "(100)");
            ExeTabla("clientes", "CodF", "nvarchar", "(10)");
            ExeTabla("clientes", "PaisF", "nvarchar", "(100)");
            ExeTabla("clientes", "fechamod", "nvarchar", "(10)");
            ExeTabla("clientes", "fcodmod", "nvarchar", "(10)");
            ExeTabla("clientes", "sincronizado", "nvarchar", "(10)");
            ExeTabla("clientes", "actividad", "nvarchar", "(50)");
            ExeTabla("clientes", "numF", "nvarchar", "(50)");
            ExeTabla("clientes", "observafact", "nvarchar", "(500)");
            ExeTabla("clientes", "numcuenta", "nvarchar", "(50)");
            ExeTabla("clientes", "cvbanco", "nvarchar", "(50)");
            ExeTabla("clientes", "metodopago", "nvarchar", "(150)");
            ExeTabla("clientes", "vendedor", "nvarchar", "(50)");
            ExeTabla("clientes", "formapago", "nvarchar", "(50)");
            ExeTabla("clientes", "tipopago", "nvarchar", "(50)");
            ExeTabla("clientes", "diascredito", "nvarchar", "(50)");
            ExeTabla("clientes", "nombrefactura", "nvarchar", "(50)");
            ExeTabla("clientes", "activo", "nvarchar", "(2)");
            ExeTabla("clientes", "saldo", "float", "");
            ExeTabla("clientes", "dcredito", "nvarchar", "(2)");
            ExeTabla("clientes", "saldocredito", "float", "");
            ExeTabla("clientes", "monedero", "float", "");
            ExeTabla("clientes", "claveAnterior", "nvarchar", "(50)");
            ExeTabla("clientes", "sector", "nvarchar", "(20)");
            ExeTabla("clientes", "ruta", "nvarchar", "(20)");
            ExeTabla("clientes", "descuento", "float", "");
            ExeTabla("configuracorreo", "correo", "nvarchar", "(400)");
            ExeTabla("configuracorreo", "nombre", "nvarchar", "(400)");
            ExeTabla("configuracorreo", "contrase\x00f1a", "nvarchar", "(400)");
            ExeTabla("configuracorreo", "puerto", "nvarchar", "(10)");
            ExeTabla("configuracorreo", "ssl", "nvarchar", "(2)");
            ExeTabla("configuracorreo", "Cuerpo", "nvarchar", "(400)");
            ExeTabla("configuracorreo", "salidasmtp", "nvarchar", "(200)");
            ExeTabla("BitacoraFacturas", "Numfactura", "int", "");
            ExeTabla("BitacoraFacturas", "mensaje", "nvarchar", "(500)");
            ExeTabla("BitacoraFacturas", "timbrado", "nvarchar", "(2)");
            ExeTabla("BitacoraFacturas", "fecha", "nvarchar", "(10)");
            ExeTabla("BitacoraFacturas", "hora", "nvarchar", "(10)");
            ExeTabla("BitacoraFacturas", "guardapdf", "nvarchar", "(2)");
            ExeTabla("BitacoraFacturas", "guardaxml", "nvarchar", "(2)");
            ExeTabla("BitacoraFacturas", "nombrepdf", "nvarchar", "(500)");
            ExeTabla("BitacoraFacturas", "nombrexml", "nvarchar", "(500)");
            ExeTabla("BitacoraFacturas", "correo", "nvarchar", "(500)");
            ExeTabla("BitacoraFacturas", "enviomail", "nvarchar", "(2)");
            ExeTabla("BitacoraFacturas", "fechacod", "nvarchar", "(10)");
            ExeTabla("Bitacora", "emitio", "nvarchar", "(50)");
            ExeTabla("Bitacora", "realizo", "nvarchar", "(400)");
            ExeTabla("Bitacora", "modulo", "nvarchar", "(200)");
            ExeTabla("Bitacora", "fecha", "nvarchar", "(10)");
            ExeTabla("Bitacora", "fechacod", "nvarchar", "(10)");
            ExeTabla("Bitacora", "hora", "nvarchar", "(10)");
            ExeTabla("ConceptosPago", "cvconcepto", "int", "");
            ExeTabla("ConceptosPago", "nombre", "nvarchar", "(200)");
            ExeTabla("ConceptosPago", "precio", "float", "");



            ExeTabla("Consecutivos", "numcliente", "int", "");
            ExeTabla("Consecutivos", "numremision", "int", "");
            ExeTabla("Consecutivos", "numprov", "int", "");
            ExeTabla("Consecutivos", "numbanco", "int", "");
            ExeTabla("Consecutivos", "numempresa", "int", "");
            ExeTabla("Consecutivos", "numsucursal", "int", "");
            ExeTabla("Consecutivos", "numproducto", "int", "");
            ExeTabla("Consecutivos", "numpedido", "int", "");
            ExeTabla("Consecutivos", "numcotiza", "int", "");
            ExeTabla("Consecutivos", "numrecibo", "int", "");
            ExeTabla("Consecutivos", "numpago", "int", "");
            ExeTabla("Consecutivos", "numgasto", "int", "");

            ExeTabla("Consecutivos", "dental", "int", "");
            ExeTabla("Consecutivos", "colposcopico", "int", "");
            ExeTabla("Consecutivos", "oftamologia", "int", "");
            ExeTabla("Consecutivos", "prenatal", "int", "");
            ExeTabla("Consecutivos", "paciente", "int", "");
            ExeTabla("Consecutivos", "notas", "int", "");
            ExeTabla("Consecutivos", "turnos", "int", "");
            ExeTabla("Consecutivos", "preserv", "int", "");
            ExeTabla("Consecutivos", "numexpe", "int", "");
            ExeTabla("Consecutivos", "numexpegine", "int", "");
            ExeTabla("Consecutivos", "numexpedental", "int", "");
            ExeTabla("Consecutivos", "numexpeofta", "int", "");
            ExeTabla("Consecutivos", "numarchivos", "int", "");
            


            ExeTabla("Creditos", "cvcliente", "nvarchar", "(50)");
            ExeTabla("Creditos", "numpedido", "int", "");
            ExeTabla("Creditos", "total", "float", "");
            ExeTabla("Creditos", "primerpago", "float", "");
            ExeTabla("Creditos", "porpagar", "float", "");
            ExeTabla("Creditos", "tipopago", "nvarchar", "(50)");
            ExeTabla("Creditos", "numpagos", "float", "");
            ExeTabla("Creditos", "estatus", "nvarchar", "(50)");
            ExeTabla("Creditos", "fechacod", "nvarchar", "(10)");
            ExeTabla("Creditos", "fecha", "nvarchar", "(10)");
            ExeTabla("Creditos", "emite", "nvarchar", "(50)");
            ExeTabla("Convenios", "numconvenio", "int", "");
            ExeTabla("Convenios", "numrecibo", "int", "");
            ExeTabla("Convenios", "numfactura", "int", "");
            ExeTabla("Convenios", "nombre", "nvarchar", "(250)");
            ExeTabla("Convenios", "direccion", "nvarchar", "(250)");
            ExeTabla("Convenios", "colonia", "nvarchar", "(250)");
            ExeTabla("Convenios", "total", "float", "");
            ExeTabla("Convenios", "iva", "float", "");
            ExeTabla("Convenios", "totalgeneral", "float", "");
            ExeTabla("Convenios", "abonocon", "float", "");
            ExeTabla("Convenios", "acumulado", "float", "");
            ExeTabla("Convenios", "porpagar", "float", "");
            ExeTabla("Convenios", "entregado", "nvarchar", "(2)");
            ExeTabla("Convenios", "emitio", "nvarchar", "(50)");
            ExeTabla("Convenios", "vendedor", "nvarchar", "(150)");
            ExeTabla("Convenios", "fecharealizo", "nvarchar", "(10)");
            ExeTabla("Convenios", "fcodrealizo", "nvarchar", "(10)");
            ExeTabla("Convenios", "hora", "nvarchar", "(10)");
            ExeTabla("Convenios", "fecharecibo", "nvarchar", "(10)");
            ExeTabla("Convenios", "fechafactura", "nvarchar", "(10)");
            ExeTabla("Convenios", "tip\x00f2pago", "nvarchar", "(50)");
            ExeTabla("Convenios", "numpagos", "int", "");
            ExeTabla("Convenios", "descricorta", "nvarchar", "(150)");
            ExeTabla("DetallesPedido", "numpedido", "int", "");
            ExeTabla("DetallesPedido", "cvcliente", "nvarchar", "(50)");
            ExeTabla("DetallesPedido", "cvproducto", "nvarchar", "(50)");
            ExeTabla("DetallesPedido", "descripcion", "nvarchar", "(500)");
            ExeTabla("DetallesPedido", "cantidad", "float", "");
            ExeTabla("DetallesPedido", "preunitario", "float", "");
            ExeTabla("DetallesPedido", "precio", "float", "");
            ExeTabla("DetallesPedido", "fecha", "nvarchar", "(10)");
            ExeTabla("DetallesPedido", "fechacod", "nvarchar", "(10)");
            ExeTabla("DetallesPedido", "mes", "int", "");
            ExeTabla("DetallesPedido", "ayo", "int", "");
            ExeTabla("DetallesPedido", "emitio", "nvarchar", "(50)");
            ExeTabla("DetallesPedido", "tdistribuidor", "float", "");
            ExeTabla("DetallesPedido", "tganancia", "float", "");
            ExeTabla("DetallesPedido", "unidad", "nvarchar", "(50)");
            ExeTabla("DetallesPedido", "nota1", "nvarchar", "(999)");
            ExeTabla("DetallesPedido", "cvunica", "nvarchar", "(50)");
            ExeTabla("DetallesPedido", "causaiva", "nvarchar", "(2)");
            ExeTabla("Empresa", "cvempresa", "int", "");
            ExeTabla("Empresa", "nombre", "nvarchar", "(500)");
            ExeTabla("Empresa", "telefono", "nvarchar", "(50)");
            ExeTabla("Empresa", "email", "nvarchar", "(50)");
            ExeTabla("Empresa", "email2", "nvarchar", "(50)");
            ExeTabla("Empresa", "celular", "nvarchar", "(50)");
            ExeTabla("Empresa", "direccion", "nvarchar", "(500)");
            ExeTabla("Empresa", "rfc", "nvarchar", "(50)");
            ExeTabla("Empresa", "direfiscal", "nvarchar", "(590)");
            ExeTabla("Empresa", "empresa", "nvarchar", "(200)");
            ExeTabla("Empresa", "factura", "nvarchar", "(2)");
            ExeTabla("Empresa", "activo", "nvarchar", "(2)");
            ExeTabla("Empresa", "calleE", "nvarchar", "(100)");
            ExeTabla("Empresa", "ColoniaE", "nvarchar", "(100)");
            ExeTabla("Empresa", "MunicipioE", "nvarchar", "(100)");
            ExeTabla("Empresa", "EstadoE", "nvarchar", "(100)");
            ExeTabla("Empresa", "CodE", "nvarchar", "(10)");
            ExeTabla("Empresa", "PaisE", "nvarchar", "(100)");
            ExeTabla("Empresa", "CalleF", "nvarchar", "(100)");
            ExeTabla("Empresa", "ColoniaF", "nvarchar", "(100)");
            ExeTabla("Empresa", "MunicipioF", "nvarchar", "(100)");
            ExeTabla("Empresa", "EstadoF", "nvarchar", "(100)");
            ExeTabla("Empresa", "CodF", "nvarchar", "(10)");
            ExeTabla("Empresa", "PaisF", "nvarchar", "(100)");
            ExeTabla("Empresa", "fechamod", "nvarchar", "(10)");
            ExeTabla("Empresa", "fcodmod", "nvarchar", "(10)");
            ExeTabla("Empresa", "sincronizado", "nvarchar", "(10)");
            ExeTabla("Empresa", "actividad", "nvarchar", "(50)");
            ExeTabla("DetallesRecibos", "numrecibo", "int", "");
            ExeTabla("DetallesRecibos", "cvcliente", "nvarchar", "(50)");
            ExeTabla("DetallesRecibos", "cvproducto", "nvarchar", "(50)");
            ExeTabla("DetallesRecibos", "descripcion", "nvarchar", "(500)");
            ExeTabla("DetallesRecibos", "cantidad", "float", "");
            ExeTabla("DetallesRecibos", "preunitario", "float", "");
            ExeTabla("DetallesRecibos", "precio", "float", "");
            ExeTabla("DetallesRecibos", "fecha", "nvarchar", "(10)");
            ExeTabla("DetallesRecibos", "fechacod", "nvarchar", "(10)");
            ExeTabla("DetallesRecibos", "mes", "int", "");
            ExeTabla("DetallesRecibos", "ayo", "int", "");
            ExeTabla("DetallesRecibos", "emitio", "nvarchar", "(50)");
            ExeTabla("DetallesRecibos", "tdistribuidor", "float", "");
            ExeTabla("DetallesRecibos", "tganancia", "float", "");
            ExeTabla("DetallesRecibos", "unidad", "nvarchar", "(50)");
            ExeTabla("DetallesRecibos", "emitio", "nvarchar", "(50)");
            ExeTabla("DetallesRecibos", "nota1", "nvarchar", "(900)");
            ExeTabla("DetallesRecibos", "cvunica", "nvarchar", "(50)");
            ExeTabla("DetallesRecibos", "causaiva", "nvarchar", "(2)");
            ExeTabla("DetallesRecibos", "descuento", "float", "");
            ExeTabla("DetallesRecibos", "comision", "float", "");
            ExeTabla("DetallesRecibos", "progresivo", "int", "");
            ExeTabla("Recibos", "numrecibo", "int", "");
            ExeTabla("Recibos", "cvcliente", "nvarchar", "(50)");
            ExeTabla("Recibos", "nombrerecibo", "nvarchar", "(250)");
            ExeTabla("Recibos", "ncliente", "nvarchar", "(250)");
            ExeTabla("Recibos", "direccion", "nvarchar", "(500)");
            ExeTabla("Recibos", "fecha", "nvarchar", "(10)");
            ExeTabla("Recibos", "fechacod", "nvarchar", "(10)");
            ExeTabla("Recibos", "total", "float", "");
            ExeTabla("Recibos", "iva", "float", "");
            ExeTabla("Recibos", "totalgeneral", "float", "");
            ExeTabla("Recibos", "emitio", "nvarchar", "(50)");
            ExeTabla("Recibos", "mes", "int", "");
            ExeTabla("Recibos", "ayo", "int", "");
            ExeTabla("Recibos", "totalletra", "nvarchar", "(500)");
            ExeTabla("Recibos", "tdistribuidor", "float", "");
            ExeTabla("Recibos", "tganancia", "float", "");
            ExeTabla("Recibos", "compro", "nvarchar", "(3500)");
            ExeTabla("Recibos", "cantidades", "nvarchar", "(3500)");
            ExeTabla("Recibos", "precunitarios", "nvarchar", "(3500)");
            ExeTabla("Recibos", "pretotales", "nvarchar", "(3500)");
            ExeTabla("Recibos", "unidades", "nvarchar", "(3500)");
            ExeTabla("Recibos", "claves", "nvarchar", "(3500)");
            ExeTabla("Recibos", "numcuenta", "nvarchar", "(50)");
            ExeTabla("Recibos", "notas", "nvarchar", "(500)");
            ExeTabla("Recibos", "condicionPago", "nvarchar", "(500)");
            ExeTabla("Recibos", "fcodcotiza", "nvarchar", "(10)");
            ExeTabla("Recibos", "fechacotiza", "nvarchar", "(10)");
            ExeTabla("Recibos", "numremision", "int", "");
            ExeTabla("Recibos", "fcodremision", "nvarchar", "(10)");
            ExeTabla("Recibos", "fecharemision", "nvarchar", "(10)");
            ExeTabla("Recibos", "estatusrecibo", "nvarchar", "(50)");
            ExeTabla("Recibos", "vendedor", "nvarchar", "(200)");
            ExeTabla("Recibos", "tiporecibo", "nvarchar", "(50)");
            ExeTabla("Recibos", "colonia", "nvarchar", "(50)");
            ExeTabla("Recibos", "aproxpeso", "nvarchar", "(50)");
            ExeTabla("Recibos", "entregado", "nvarchar", "(10)");
            ExeTabla("Recibos", "fechaentrega", "nvarchar", "(10)");
            ExeTabla("Recibos", "fcodentrega", "nvarchar", "(10)");
            ExeTabla("Recibos", "pagado", "nvarchar", "(2)");
            ExeTabla("Recibos", "telefono", "nvarchar", "(20)");
            ExeTabla("Recibos", "hora", "nvarchar", "(10)");
            ExeTabla("Recibos", "horacod", "nvarchar", "(10)");
            ExeTabla("Recibos", "tdescuento", "float", "");
            ExeTabla("ParametrosFactura", "usuario", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "contrafac", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "nombre", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "fechainicia", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "RFC", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "IDSerial", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "LugarExpedicion", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "Regimen", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "DirCarpeta", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "calle", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "numext", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "numint", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "colonia", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "municipio", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "localidad", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "estado", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "pais", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "codpostal", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "correo", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "productivo", "nvarchar", "(2)");
            ExeTabla("ParametrosFactura", "numcopias", "int", "");


            ExeTabla("ParametrosFactura", "nombrecomercial", "nvarchar", "(200)");
            ExeTabla("ParametrosFactura", "infoadicional", "nvarchar", "(200)");
            ExeTabla("ParametrosRecibo", "NombreComercial", "nvarchar", "(200)");
            ExeTabla("ParametrosRecibo", "Nombreencargado", "nvarchar", "(200)");
            ExeTabla("ParametrosRecibo", "InfoAdicional", "nvarchar", "(200)");
            ExeTabla("ParametrosRecibo", "direccion", "nvarchar", "(200)");
            ExeTabla("ParametrosRecibo", "LugarExpedicion", "nvarchar", "(200)");


            ExeTabla("FacturasBidimensional", "numrecibo", "int", "");
            ExeTabla("FacturasBidimensional", "codbidimensional", "varbinary(MAX)", "");
            ExeTabla("LogoEmpresa", "cvempresa", "int", "");
            ExeTabla("LogoEmpresa", "foto", "varbinary(MAX)", "");
            ExeTabla("DetallesFacturas", "numfactura", "int", "");
            ExeTabla("DetallesFacturas", "numpartida", "int", "");
            ExeTabla("DetallesFacturas", "cantidad", "float", "");
            ExeTabla("DetallesFacturas", "descripcion", "nvarchar", "(500)");
            ExeTabla("DetallesFacturas", "importe", "float", "");
            ExeTabla("DetallesFacturas", "cvproducto", "nvarchar", "(250)");
            ExeTabla("DetallesFacturas", "unidad", "nvarchar", "(50)");
            ExeTabla("DetallesFacturas", "valorUnitario", "float", "");
            ExeTabla("DetallesFacturas", "pedimentonum", "nvarchar", "(900)");
            ExeTabla("DetallesFacturas", "pedimentonombre", "nvarchar", "(900)");
            ExeTabla("DetallesFacturas", "pedimentofecha", "nvarchar", "(10)");
            ExeTabla("DetallesFacturas", "iva", "float", "");
            ExeTabla("DetallesFacturas", "notas1", "nvarchar", "(900)");
            ExeTabla("DetallesFacturas", "notas2", "nvarchar", "(900)");
            ExeTabla("DetallesFacturas", "numpedido", "int", "");
            ExeTabla("DetallesFacturas", "adicional", "float", "");
            ExeTabla("DetallesFacturas", "cvunica", "nvarchar", "(50)");
            ExeTabla("DetallesFacturas", "fechacod", "nvarchar", "(10)");
            ExeTabla("DetallesFacturas", "fecha", "nvarchar", "(10)");
            ExeTabla("DetallesFacturas", "mes", "int", "");
            ExeTabla("DetallesFacturas", "ayo", "int", "");
            ExeTabla("DetallesFacturas", "cvcliente", "nvarchar", "(50)");
            ExeTabla("DetallesFacturas", "Valoriva", "float", "");
            ExeTabla("Facturas", "numfactura", "int", "");
            ExeTabla("Facturas", "estatus", "nvarchar", "(100)");
            ExeTabla("Facturas", "idsistemapadre", "nvarchar", "(50)");
            ExeTabla("Facturas", "edocomprobante", "int", "");
            ExeTabla("Facturas", "tipo", "nvarchar", "(2)");
            ExeTabla("Facturas", "RFCEmitio", "nvarchar", "(50)");
            ExeTabla("Facturas", "CondicionesPago", "nvarchar", "(50)");
            ExeTabla("Facturas", "FormaPago", "nvarchar", "(50)");
            ExeTabla("Facturas", "Descuento", "float", "");
            ExeTabla("Facturas", "motivoDescuento", "nvarchar", "(50)");
            ExeTabla("Facturas", "metodoPago", "nvarchar", "(50)");
            ExeTabla("Facturas", "subtotal", "float", "");
            ExeTabla("Facturas", "total", "float", "");
            ExeTabla("Facturas", "REClave", "nvarchar", "(50)");
            ExeTabla("Facturas", "ReNombre", "nvarchar", "(250)");
            ExeTabla("Facturas", "ReRFC", "nvarchar", "(50)");
            ExeTabla("Facturas", "ReCalle", "nvarchar", "(250)");
            ExeTabla("Facturas", "ReCodpostal", "nvarchar", "(50)");
            ExeTabla("Facturas", "ReColonia", "nvarchar", "(50)");
            ExeTabla("Facturas", "ReEstado", "nvarchar", "(50)");
            ExeTabla("Facturas", "ReLocalidad", "nvarchar", "(50)");
            ExeTabla("Facturas", "ReMunicipio", "nvarchar", "(50)");
            ExeTabla("Facturas", "ReNoExterior", "nvarchar", "(50)");
            ExeTabla("Facturas", "ReNoInterior", "nvarchar", "(50)");
            ExeTabla("Facturas", "ReTel", "nvarchar", "(50)");
            ExeTabla("Facturas", "RePais", "nvarchar", "(50)");
            ExeTabla("Facturas", "ReReferencia", "nvarchar", "(250)");
            ExeTabla("Facturas", "Recorreo", "nvarchar", "(250)");
            ExeTabla("Facturas", "TImpuestosRetenido", "float", "");
            ExeTabla("Facturas", "TImpuestoTrasladado", "float", "");
            ExeTabla("Facturas", "RImpuesto", "nvarchar", "(3)");
            ExeTabla("Facturas", "RImporte", "float", "");
            ExeTabla("Facturas", "TImpuesto", "nvarchar", "(3)");
            ExeTabla("Facturas", "TImporte", "float", "");
            ExeTabla("Facturas", "TTasa", "int", "");
            ExeTabla("Facturas", "Notas", "nvarchar", "(950)");
            ExeTabla("Facturas", "moneda", "nvarchar", "(50)");
            ExeTabla("Facturas", "TipoCambio", "float", "");
            ExeTabla("Facturas", "Vendedor", "nvarchar", "(250)");
            ExeTabla("Facturas", "OrdCompra", "nvarchar", "(100)");
            ExeTabla("Facturas", "Otros", "nvarchar", "(3500)");
            ExeTabla("Facturas", "numCtaPago", "nvarchar", "(100)");
            ExeTabla("Facturas", "numpedido", "int", "");
            ExeTabla("Facturas", "ayo", "int", "");
            ExeTabla("Facturas", "mes", "int", "");
            ExeTabla("Facturas", "Fecha", "nvarchar", "(10)");
            ExeTabla("Facturas", "FechaCod", "nvarchar", "(10)");
            ExeTabla("Facturas", "Hora", "nvarchar", "(10)");
            ExeTabla("Facturas", "Fechafactura", "nvarchar", "(10)");
            ExeTabla("Facturas", "Fcodfactura", "nvarchar", "(10)");
            ExeTabla("Facturas", "Horafactura", "nvarchar", "(10)");
            ExeTabla("Facturas", "imagenCBB", "text", "");
            ExeTabla("Facturas", "cadenaOriginal", "text", "");
            ExeTabla("Facturas", "UUID", "text", "");
            ExeTabla("Facturas", "selloCFD", "text", "");
            ExeTabla("Facturas", "selloSat", "text", "");
            ExeTabla("Facturas", "serieSat", "text", "");
            ExeTabla("Facturas", "Emitio", "nvarchar", "(50)");
            ExeTabla("Facturas", "cvcliente", "nvarchar", "(50)");
            ExeTabla("Facturas", "direccion", "nvarchar", "(900)");
            ExeTabla("Facturas", "observaciones", "nvarchar", "(1500)");
            ExeTabla("Facturas", "cantletra", "nvarchar", "(1500)");
            ExeTabla("Facturas", "folioFiscal", "nvarchar", "(1500)");
            ExeTabla("Facturas", "certificadoEmisor", "nvarchar", "(3500)");
            ExeTabla("Facturas", "certificadoSat", "nvarchar", "(3500)");
            ExeTabla("Facturas", "fechahoracert", "nvarchar", "(3500)");
            ExeTabla("Facturas", "SelloCDFI", "nvarchar", "(3500)");
            ExeTabla("Facturas", "SelloSATM", "nvarchar", "(3500)");
            ExeTabla("Facturas", "Cadena", "nvarchar", "(3500)");
            ExeTabla("ListaPrecios", "cvproducto", "nvarchar", "(100)");
            ExeTabla("ListaPrecios", "distribuidor", "float", "");
            ExeTabla("ListaPrecios", "publico1", "float", "");
            ExeTabla("ListaPrecios", "porciento1", "float", "");
            ExeTabla("ListaPrecios", "ganancia1", "float", "");
            ExeTabla("ListaPrecios", "publico2", "float", "");
            ExeTabla("ListaPrecios", "porciento2", "float", "");
            ExeTabla("ListaPrecios", "ganancia2", "float", "");
            ExeTabla("ListaPrecios", "publico3", "float", "");
            ExeTabla("ListaPrecios", "porciento3", "float", "");
            ExeTabla("ListaPrecios", "ganancia3", "float", "");
            ExeTabla("ListaPrecios", "PorcDescuento", "float", "");
            ExeTabla("LlavesSistema", "cvllave", "nvarchar", "(500)");
            ExeTabla("LlavesSistema", "fechacod", "nvarchar", "(10)");
            ExeTabla("Pagos", "cvcliente", "nvarchar", "(50)");
            ExeTabla("Pagos", "numpedido", "int", "");
            ExeTabla("Pagos", "cantidad", "float", "");
            ExeTabla("Pagos", "fecha", "nvarchar", "(10)");
            ExeTabla("Pagos", "fechacod", "nvarchar", "(10)");
            ExeTabla("Pagos", "concepto", "nvarchar", "(250)");
            ExeTabla("Pagos", "cvconcepto", "nvarchar", "(50)");
            ExeTabla("Pagos", "remisionHist", "int", "");
            ExeTabla("Pagos", "estatus", "nvarchar", "(100)");
            ExeTabla("Pagos", "fechapago", "nvarchar", "(10)");
            ExeTabla("Pagos", "fcodpago", "nvarchar", "(10)");
            ExeTabla("Pagos", "emitiopago", "nvarchar", "(50)");
            ExeTabla("Pagos", "pagocon", "nvarchar", "(50)");
            ExeTabla("Pagos", "observacion", "nvarchar", "(500)");
            ExeTabla("Pagos", "numremision", "int", "");
            ExeTabla("Pagos", "ayo", "int", "");
            ExeTabla("Pagos", "mes", "int", "");
            ExeTabla("Pagos", "numRecibo", "int", "");
            ExeTabla("Pagos", "tipopago", "nvarchar", "(50)");
            ExeTabla("Pagos", "observa", "nvarchar", "(500)");
            ExeTabla("Pagos", "numpago", "int", "");
            ExeTabla("Pagos", "bandera", "int", "");
            ExeTabla("Pagos", "Horapago", "nvarchar", "(10)");
            ExeTabla("Pagos", "Horacodpago", "nvarchar", "(10)");
            ExeTabla("parametros", "iva", "float", "");
            ExeTabla("parametros", "usuarioFolio", "nvarchar", "(50)");
            ExeTabla("parametros", "contraFolio", "nvarchar", "(50)");
            ExeTabla("parametros", "correover", "nvarchar", "(50)");
            ExeTabla("parametros", "dirRespaldo", "nvarchar", "(200)");
            ExeTabla("parametros", "modelosis", "nvarchar", "(100)");
            ExeTabla("parametros", "sistema", "nvarchar", "(100)");
            ExeTabla("parametros", "nombrecliente", "nvarchar", "(300)");
            ExeTabla("parametros", "notificarcorreo", "nvarchar", "(300)");
            ExeTabla("parametros", "ObligatorioVendedor", "nvarchar", "(2)");
            ExeTabla("parametros", "pasarpagado", "nvarchar", "(2)");
            ExeTabla("parametros", "version", "nvarchar", "(10)");
            ExeTabla("parametros", "FechaActualizacion", "nvarchar", "(10)");
            ExeTabla("parametros", "habilitarprecio", "nvarchar", "(2)");
            ExeTabla("parametros", "ventcobro", "nvarchar", "(2)");
            ExeTabla("parametros", "ocultardistribuidor", "nvarchar", "(2)");
            ExeTabla("parametros", "conveniopago", "nvarchar", "(2)");
            ExeTabla("parametros", "porcenpagare", "int", "");
            ExeTabla("parametros", "porcenadicional", "float", "");
            ExeTabla("parametros", "idicepago", "int", "");
            ExeTabla("parametros", "mostrarCod", "nvarchar", "(2)");
            ExeTabla("parametros", "abrirfactura", "nvarchar", "(2)");
            ExeTabla("parametros", "impresiondirecta", "nvarchar", "(2)");
            ExeTabla("parametros", "versionbill", "nvarchar", "(50)");
            ExeTabla("parametros", "calculaiva", "nvarchar", "(2)");
            ExeTabla("parametros", "ultimoRecibo", "nvarchar", "(20)");
            ExeTabla("parametros", "modificaRecibo", "nvarchar", "(2)");
            ExeTabla("parametros", "BactivaFacturacion", "nvarchar", "(2)");
            ExeTabla("Pedidos", "numpedido", "int", "");
            ExeTabla("Pedidos", "cvcliente", "nvarchar", "(50)");
            ExeTabla("Pedidos", "nombre", "nvarchar", "(500)");
            ExeTabla("Pedidos", "fecha", "nvarchar", "(10)");
            ExeTabla("Pedidos", "fechacod", "nvarchar", "(10)");
            ExeTabla("Pedidos", "total", "float", "");
            ExeTabla("Pedidos", "iva", "float", "");
            ExeTabla("Pedidos", "totalgeneral", "float", "");
            ExeTabla("Pedidos", "status", "nvarchar", "(50)");
            ExeTabla("Pedidos", "emitio", "nvarchar", "(50)");
            ExeTabla("Pedidos", "facturada", "nvarchar", "(2)");
            ExeTabla("Pedidos", "mes", "int", "");
            ExeTabla("Pedidos", "ayo", "int", "");
            ExeTabla("Pedidos", "totalletra", "nvarchar", "(500)");
            ExeTabla("Pedidos", "tdistribuidor", "float", "");
            ExeTabla("Pedidos", "tganancia", "float", "");
            ExeTabla("Pedidos", "compro", "nvarchar", "(3900)");
            ExeTabla("Pedidos", "cantidades", "nvarchar", "(1500)");
            ExeTabla("Pedidos", "precunitarios", "nvarchar", "(1500)");
            ExeTabla("Pedidos", "pretotales", "nvarchar", "(1500)");
            ExeTabla("Pedidos", "unidades", "nvarchar", "(1500)");
            ExeTabla("Pedidos", "claves", "nvarchar", "(1500)");
            ExeTabla("Pedidos", "tipopago", "nvarchar", "(150)");
            ExeTabla("Pedidos", "banco", "nvarchar", "(50)");
            ExeTabla("Pedidos", "numcuenta", "nvarchar", "(50)");
            ExeTabla("Pedidos", "notas", "nvarchar", "(1500)");
            ExeTabla("Pedidos", "condicionPago", "nvarchar", "(100)");
            ExeTabla("Pedidos", "numremision", "int", "");
            ExeTabla("Pedidos", "numcotizacion", "int", "");
            ExeTabla("Pedidos", "fcodcotiza", "nvarchar", "(10)");
            ExeTabla("Pedidos", "fechacotiza", "nvarchar", "(10)");
            ExeTabla("Pedidos", "fcodremision", "nvarchar", "(10)");
            ExeTabla("Pedidos", "fecharemision", "nvarchar", "(10)");
            ExeTabla("Pedidos", "estatuspedido", "nvarchar", "(50)");
            ExeTabla("Pedidos", "estatuspago", "nvarchar", "(50)");
            ExeTabla("Pedidos", "vendedor", "nvarchar", "(200)");
            ExeTabla("Pedidos", "adicional", "float", "");
            ExeTabla("Productos", "cvproducto", "nvarchar", "(50)");
            ExeTabla("Productos", "nombre", "nvarchar", "(900)");
            ExeTabla("Productos", "descripcion", "nvarchar", "(900)");
            ExeTabla("Productos", "categoria", "nvarchar", "(100)");
            ExeTabla("Productos", "unidad", "nvarchar", "(50)");
            ExeTabla("Productos", "cantidad", "float", "");
            ExeTabla("Productos", "minimo", "float", "");
            ExeTabla("Productos", "maximo", "float", "");
            ExeTabla("Productos", "causaiva", "nvarchar", "(2)");
            ExeTabla("Productos", "marca", "nvarchar", "(100)");
            ExeTabla("Productos", "codbarras", "nvarchar", "(50)");
            ExeTabla("Productos", "ubicacion", "nvarchar", "(150)");
            ExeTabla("Productos", "fechaModifica", "nvarchar", "(10)");
            ExeTabla("Productos", "fcodmodifica", "nvarchar", "(10)");
            ExeTabla("Productos", "emitio", "nvarchar", "(50)");
            ExeTabla("Productos", "causaAdicional", "nvarchar", "(2)");
            ExeTabla("Productos", "pasillo", "nvarchar", "(20)");
            ExeTabla("Productos", "altura", "nvarchar", "(20)");
            ExeTabla("Productos", "sucursal", "nvarchar", "(20)");
            ExeTabla("Proveedores", "cvprov", "nvarchar", "(50)");
            ExeTabla("Proveedores", "nombre", "nvarchar", "(500)");
            ExeTabla("Proveedores", "telefono", "nvarchar", "(50)");
            ExeTabla("Proveedores", "email", "nvarchar", "(50)");
            ExeTabla("Proveedores", "email2", "nvarchar", "(50)");
            ExeTabla("Proveedores", "celular", "nvarchar", "(50)");
            ExeTabla("Proveedores", "direccion", "nvarchar", "(500)");
            ExeTabla("Proveedores", "rfc", "nvarchar", "(50)");
            ExeTabla("Proveedores", "direfiscal", "nvarchar", "(500)");
            ExeTabla("Proveedores", "empresa", "nvarchar", "(250)");
            ExeTabla("Proveedores", "factura", "nvarchar", "(2)");
            ExeTabla("Proveedores", "activo", "nvarchar", "(2)");
            ExeTabla("Proveedores", "calleE", "nvarchar", "(150)");
            ExeTabla("Proveedores", "ColoniaE", "nvarchar", "(150)");
            ExeTabla("Proveedores", "MunicipioE", "nvarchar", "(150)");
            ExeTabla("Proveedores", "EstadoE", "nvarchar", "(150)");
            ExeTabla("Proveedores", "CodE", "nvarchar", "(150)");
            ExeTabla("Proveedores", "PaisE", "nvarchar", "(150)");
            ExeTabla("Proveedores", "CalleF", "nvarchar", "(150)");
            ExeTabla("Proveedores", "ColoniaF", "nvarchar", "(150)");
            ExeTabla("Proveedores", "MunicipioF", "nvarchar", "(150)");
            ExeTabla("Proveedores", "EstadoF", "nvarchar", "(150)");
            ExeTabla("Proveedores", "CodF", "nvarchar", "(10)");
            ExeTabla("Proveedores", "PaisF", "nvarchar", "(150)");
            ExeTabla("Proveedores", "fechamod", "nvarchar", "(10)");
            ExeTabla("Proveedores", "fcodmod", "nvarchar", "(10)");
            ExeTabla("Proveedores", "sincronizado", "nvarchar", "(10)");
            ExeTabla("Proveedores", "actividad", "nvarchar", "(50)");
            ExeTabla("Proveedores", "PorGanancia", "float", "");
            ExeTabla("Provnota", "numnota", "nvarchar", "(50)");
            ExeTabla("Provnota", "cvprov", "nvarchar", "(50)");
            ExeTabla("Provnota", "emitio", "nvarchar", "(50)");
            ExeTabla("Provnota", "fecha", "nvarchar", "(10)");
            ExeTabla("Provnota", "fechacod", "nvarchar", "(10)");
            ExeTabla("Provnota", "Facturado", "nvarchar", "(2)");
            ExeTabla("Provnota", "TotalC", "float", "");
            ExeTabla("DetalleProvnota", "numnota", "nvarchar", "(50)");
            ExeTabla("DetalleProvnota", "cvprov", "nvarchar", "(50)");
            ExeTabla("DetalleProvnota", "Clave", "nvarchar", "(20)");
            ExeTabla("DetalleProvnota", "Claveprov", "nvarchar", "(50)");
            ExeTabla("DetalleProvnota", "Unidad", "nvarchar", "(50)");
            ExeTabla("DetalleProvnota", "Describe", "nvarchar", "(990)");
            ExeTabla("DetalleProvnota", "Cantidad", "float", "");
            ExeTabla("DetalleProvnota", "Unitario", "float", "");
            ExeTabla("DetalleProvnota", "Total", "float", "");
            ExeTabla("DetalleProvnota", "BelPrecio", "nvarchar", "(10)");
            ExeTabla("DetalleProvnota", "fecha", "nvarchar", "(10)");
            ExeTabla("DetalleProvnota", "fechacod", "nvarchar", "(10)");
            ExeTabla("DetalleProvnota", "dia", "float", "");
            ExeTabla("DetalleProvnota", "mes", "float", "");


            ExeTabla("ControlTurnos", "numturno", "nvarchar", "(50)");
            ExeTabla("ControlTurnos", "atiende", "nvarchar", "(250)");
            ExeTabla("ControlTurnos", "horareg", "nvarchar", "(10)");
            ExeTabla("ControlTurnos", "horacod", "nvarchar", "(10)");
            ExeTabla("ControlTurnos", "fecha", "nvarchar", "(10)");
            ExeTabla("ControlTurnos", "fechacod", "nvarchar", "(10)");
            ExeTabla("ControlTurnos", "emitio", "nvarchar", "(20)");
            ExeTabla("ControlTurnos", "progresivo", "float", "");
            ExeTabla("ControlTurnos", "atendido", "nvarchar", "(2)");
            ExeTabla("ControlTurnos", "cancelada", "nvarchar", "(2)");
            ExeTabla("ControlTurnos", "horainicia", "nvarchar", "(10)");
            ExeTabla("ControlTurnos", "horatermina", "nvarchar", "(10)");
            ExeTabla("ControlTurnos", "hmininicia", "float", "");
            ExeTabla("ControlTurnos", "hmintermina", "float", "");
            ExeTabla("ControlTurnos", "activo", "nvarchar", "(2)");

            ExeTabla("Citas", "cvdoctor", "nvarchar", "(10)");
            ExeTabla("Citas", "cvpaciente", "int", "");
            ExeTabla("Citas", "numexpediente", "nvarchar", "(50)");
            ExeTabla("Citas", "horainicia", "nvarchar", "(10)");
            ExeTabla("Citas", "horatermina", "nvarchar", "(10)");
            ExeTabla("Citas", "hmininicia", "float", "");
            ExeTabla("Citas", "hmintermina", "float", "");
            ExeTabla("Citas", "ttiempo", "float", "");
            ExeTabla("Citas", "estatus", "nvarchar", "(20)");
            ExeTabla("Citas", "tipo", "nvarchar", "(20)");
            ExeTabla("Citas", "fecha", "nvarchar", "(10)");
            ExeTabla("Citas", "fechacod", "nvarchar", "(10)");
            ExeTabla("Citas", "emite", "nvarchar", "(20)");
            ExeTabla("Citas", "observa", "nvarchar", "(200)");
            ExeTabla("Citas", "cvservicio", "nvarchar", "(20)");
            ExeTabla("Citas", "progresivo", "int", "");
            ExeTabla("Citas", "primeravez", "nvarchar", "(2)");
            ExeTabla("Citas", "nombre", "nvarchar", "(200)");
            ExeTabla("Citas", "telefono", "nvarchar", "(200)");
            ExeTabla("Citas", "ReciboPago", "nvarchar", "(20)");
            ExeTabla("Citas", "NombreServicio", "nvarchar", "(800)");
            ExeTabla("Citas", "healinicia", "nvarchar", "(10)");
            ExeTabla("Citas", "hrealmininicia", "float", "");


            ExeTabla("NEvolucion", "cvnota", "int", "");
            ExeTabla("NEvolucion", "cvpaciente", "int", "");
            ExeTabla("NEvolucion", "cvdoctor", "nvarchar", "(10)");
            ExeTabla("NEvolucion", "numexpediente", "nvarchar", "(50)");
            ExeTabla("NEvolucion", "horainicia", "nvarchar", "(10)");
            ExeTabla("NEvolucion", "horatermina", "nvarchar", "(10)");
            ExeTabla("NEvolucion", "hmininicia", "float", "");
            ExeTabla("NEvolucion", "hmintermina", "float", "");
            ExeTabla("NEvolucion", "ttiempo", "float", "");
            ExeTabla("NEvolucion", "fecha", "nvarchar", "(10)");
            ExeTabla("NEvolucion", "fechacod", "nvarchar", "(10)");
            ExeTabla("NEvolucion", "emite", "nvarchar", "(20)");
            ExeTabla("NEvolucion", "informe", "nvarchar", "(999)");
            ExeTabla("NEvolucion", "edad", "float", "");
            ExeTabla("NEvolucion", "fechaprox", "nvarchar", "(10)");
            ExeTabla("NEvolucion", "fcodprox", "nvarchar", "(10)");
            ExeTabla("NEvolucion", "numturno", "int", "");
            ExeTabla("NEvolucion", "numrecibo", "int", "");


            ExeTabla("CitasProx", "cvpaciente", "int", "");
            ExeTabla("CitasProx", "numturno", "int", "");
            ExeTabla("CitasProx", "cvdoctor", "nvarchar", "(10)");
            ExeTabla("CitasProx", "fecha", "nvarchar", "(10)");
            ExeTabla("CitasProx", "fechacod", "nvarchar", "(10)");
            ExeTabla("CitasProx", "realizar", "nvarchar", "(900)");
            ExeTabla("CitasProx", "estatus", "nvarchar", "(20)");
            ExeTabla("CitasProx", "fechaAgenda", "nvarchar", "(10)");
            ExeTabla("CitasProx", "fcodAgenda", "nvarchar", "(10)");
            ExeTabla("CitasProx", "trataterminado", "nvarchar", "(10)");

            ExeTabla("CitasReprog", "cvpaciente", "int", "");
            ExeTabla("CitasReprog", "numturnonuevo", "int", "");
            ExeTabla("CitasReprog", "cvdoctor", "nvarchar", "(10)");
            ExeTabla("CitasReprog", "fecha", "nvarchar", "(10)");
            ExeTabla("CitasReprog", "fechacod", "nvarchar", "(10)");
            ExeTabla("CitasReprog", "fechaAntes", "nvarchar", "(10)");
            ExeTabla("CitasReprog", "fcodantes", "nvarchar", "(10)");
            ExeTabla("CitasReprog", "realizar", "nvarchar", "(900)");
            ExeTabla("CitasReprog", "fechaAgenda", "nvarchar", "(10)");
            ExeTabla("CitasReprog", "fcodAgenda", "nvarchar", "(10)");
            ExeTabla("CitasReprog", "horaagenda", "nvarchar", "(10)");
            ExeTabla("CitasReprog", "servicio", "nvarchar", "(200)");
            ExeTabla("CitasReprog", "observa", "nvarchar", "(200)");
            ExeTabla("CitasReprog", "numrecibo", "int", "");


            ExeTabla("DetallesPreServicio", "cvpreserv", "int", "");
            ExeTabla("DetallesPreServicio", "cvpaciente", "int", "");
            ExeTabla("DetallesPreServicio", "cantidad", "float", "");
            ExeTabla("DetallesPreServicio", "cvproducto", "nvarchar", "(20)");
            ExeTabla("DetallesPreServicio", "estatus", "nvarchar", "(20)");
            ExeTabla("DetallesPreServicio", "emitio", "nvarchar", "(20)");
            ExeTabla("DetallesPreServicio", "fechacod", "nvarchar", "(20)");
            ExeTabla("DetallesPreServicio", "fecha", "nvarchar", "(20)");
            ExeTabla("DetallesPreServicio", "numrecibo", "int", "");
            ExeTabla("DetallesPreServicio", "fechacita", "nvarchar", "(20)");
            ExeTabla("DetallesPreServicio", "numticket", "int", "");


            ExeTabla("CatServicios", "cvservicio", "nvarchar", "(20)");
            ExeTabla("CatServicios", "nombre", "nvarchar", "(200)");
            ExeTabla("CatServicios", "cvdoctor", "nvarchar", "(10)");


            
            ExeTabla("Vendedores", "cvvendedor", "nvarchar", "(50)");
            ExeTabla("Vendedores", "nombre", "nvarchar", "(250)");
            ExeTabla("Vendedores", "comision", "nvarchar", "(2)");
            ExeTabla("Vendedores", "porcentaje", "float", "");

            ExeTabla("Doctores", "cvdoctor", "nvarchar", "(50)");
            ExeTabla("Doctores", "nombre", "nvarchar", "(250)");
            ExeTabla("Doctores", "especialidad", "nvarchar", "(200)");
            ExeTabla("Doctores", "HoraEntrada", "nvarchar", "(10)");
            ExeTabla("Doctores", "HoraSalida", "nvarchar", "(10)");
            ExeTabla("Doctores", "TiempoConsulta", "int", "");
            ExeTabla("Doctores", "HoraEComedor", "nvarchar", "(10)");
            ExeTabla("Doctores", "HoraSComedor", "nvarchar", "(10)");
            ExeTabla("Doctores", "DComedor", "nvarchar", "(2)");
            ExeTabla("Doctores", "Areatratar", "nvarchar", "(100)");
            ExeTabla("Doctores", "tipoexpediente", "nvarchar", "(100)");

            ExeTabla("Requisicion", "clave", "nvarchar", "(50)");
            ExeTabla("Requisicion", "nombre", "nvarchar", "(50)");
            ExeTabla("Requisicion", "justifica", "nvarchar", "(900)");
            ExeTabla("Requisicion", "cantidad", "int", "");
            ExeTabla("Requisicion", "fechacod", "nvarchar", "(10)");
            ExeTabla("Requisicion", "fecha", "nvarchar", "(10)");
            ExeTabla("Requisicion", "emite", "nvarchar", "(10)");
            ExeTabla("Requisicion", "estatus", "nvarchar", "(10)");
            ExeTabla("Requisicion", "respuesta", "nvarchar", "(900)");
            ExeTabla("Requisicion", "fcodresp", "nvarchar", "(10)");
            ExeTabla("Requisicion", "fresp", "nvarchar", "(10)");


            ExeTabla("Usuarios", "cvusuario", "nvarchar", "(50)");
            ExeTabla("Usuarios", "nombre", "nvarchar", "(150)");
            ExeTabla("Usuarios", "contra", "nvarchar", "(50)");
            ExeTabla("Usuarios", "entra1", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra2", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra3", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra4", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra5", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra6", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra7", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra8", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra9", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra10", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra11", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra12", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra13", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra14", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra15", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra16", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra17", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra18", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra19", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra20", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra21", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra22", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra23", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra24", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra25", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra26", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra27", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra28", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra29", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra30", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra31", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra32", "nvarchar", "(2)");
            ExeTabla("Usuarios", "entra33", "nvarchar", "(2)");
            ExeTabla("Usuarios", "ESDOCTOR", "nvarchar", "(2)");
            ExeTabla("Usuarios", "CVDOCTOR", "nvarchar", "(20)");

            ExeTabla("Pagos", "recibio", "float", "");
            ExeTabla("Pagos", "cambio", "float", "");

            ExeTabla("imgpantalla", "clave", "int", "");
            ExeTabla("imgpantalla", "foto", "varbinary(MAX)", "");


            ExeTabla("archivos", "cvpaciente", "int", "");
            ExeTabla("archivos", "clave", "int", "");
            ExeTabla("archivos", "nombre", "nvarchar", "(200)");
            ExeTabla("archivos", "descripcion", "nvarchar", "(900)");
            ExeTabla("archivos", "archivoM", "varbinary(MAX)", "");
            ExeTabla("archivos", "emite", "nvarchar", "(20)");
            ExeTabla("archivos", "estatus", "nvarchar", "(20)");
            ExeTabla("archivos", "fecha", "nvarchar", "(10)");
            ExeTabla("archivos", "fechacod", "nvarchar", "(10)");
            ExeTabla("archivos", "hora", "nvarchar", "(10)");
            ExeTabla("archivos", "extension", "nvarchar", "(10)");
            ExeTabla("archivos", "cvdoctor", "nvarchar", "(20)");



            #region camposHistoriaClinicaOftalmologia
            ExeTabla("HClinicaO", "HCCVCliente", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCFecha", "nvarchar", "(10)");
            ExeTabla("HClinicaO", "HCCODFecha", "nvarchar", "(10)");
            ExeTabla("HClinicaO", "HCInterrogatorio", "nvarchar", "(250)");
            ExeTabla("HClinicaO", "HCMConsulta", "nvarchar", "(350)");
            ExeTabla("HClinicaO", "HCPActual", "nvarchar", "(350)");
            ExeTabla("HClinicaO", "HCHFSDM", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCHFSHAS", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCHFSCA", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCHFSOtros", "nvarchar", "(250)");
            ExeTabla("HClinicaO", "HCHFODM", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCHFOHAS", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCHFOCA", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCHFOOtros", "nvarchar", "(250)");
            ExeTabla("HClinicaO", "HCAPSDM", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPSEvolucion", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPSUG", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPSControl", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCAPSCancer", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCAPSTransfuncionales", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCAPSAlergicos", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCAPSQuirurgicos", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCAPSMedicamentos", "nvarchar", "(250)");
            ExeTabla("HClinicaO", "HCAPSHAS", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPSEvolucion2", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPSControl2", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPSCardiopatia", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPSENFEndocrina", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPSENFNeurologica", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPSAR", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPSInfecciosos", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPSOtros", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPCIRUGIAP1", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPCIRUGIAF1", "nvarchar", "(10)");
            ExeTabla("HClinicaO", "HCAPCIRUGIAP2", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPCIRUGIAF2", "nvarchar", "(10)");
            ExeTabla("HClinicaO", "HCAPCIRUGIAP3", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCAPCIRUGIAF3", "nvarchar", "(10)");
            ExeTabla("HClinicaO", "HCAPOCatarata", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCAPOGlaucoma", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCAPORetinopatia", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCAPOEstrabismo", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCAPOTruma", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCAPOOtros", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCAPOUltimoExamen", "nvarchar", "(250)");
            ExeTabla("HClinicaO", "HCAPOMedicamentos", "nvarchar", "(250)");
            ExeTabla("HClinicaO", "HCLP1", "nvarchar", "(150)");
            ExeTabla("HClinicaO", "HCLPF1", "nvarchar", "(10)");
            ExeTabla("HClinicaO", "HCLP2", "nvarchar", "(150)");
            ExeTabla("HClinicaO", "HCLPF2", "nvarchar", "(10)");
            ExeTabla("HClinicaO", "HCAMSLER1", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCAMSLER2", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCEOAVSCOD", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCEOAVSCEST", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCEOAVSCOI", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCEOCCOD", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCEOCCEST", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCEOCCOI", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCLOD", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCLOI", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCLQMOD", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCLADD1", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCLADD2", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCLQMOI", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCLROD", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCLROI", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCLRSOD", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCLRSOI", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCLRSDIP", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCLRSCV", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCLRSADDOD", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCLRSOI2", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCLRSVC", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCLRSObservaciones", "nvarchar", "(350)");
            ExeTabla("HClinicaO", "HCMOculares", "nvarchar", "(500)");
            ExeTabla("HClinicaO", "HCRFMotorOD", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCRFMotorOI", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCRConsensualOD", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCRConsensualOI", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCRDEFPUPILAROD", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCRDEFPUPILAROI", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCTOAIOTRO", "nvarchar", "(50)");
            ExeTabla("HClinicaO", "HCTOAIOD", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCTOAIOI", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCPExcavacionOD", "nvarchar", "(5)");
            ExeTabla("HClinicaO", "HCPColoracionOD", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCPBordesOD", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCPExcavacionOI", "nvarchar", "(5)");
            ExeTabla("HClinicaO", "HCPColoracionOI", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCPBordesOI", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCVDPVODPARCIAL", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCVDPVODTOTAL", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCVDPVOIPARCIAL", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCVDPVOITOTAL", "nvarchar", "(25)");
            ExeTabla("HClinicaO", "HCVSineresis", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCVHialosis", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCVHemorragia", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCVSineresisOI", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCVHialosisOI", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCVHemorragiaOI", "nvarchar", "(100)");
            ExeTabla("HClinicaO", "HCDiagnostico", "nvarchar", "(600)");
            ExeTabla("HClinicaO", "HCPlan", "nvarchar", "(600)");
            ExeTabla("HClinicaO", "HCComentario", "nvarchar", "(1000)");
            ExeTabla("HClinicaO", "consecutivo", "int", "");
            #endregion

            #region camposDatosPacientes
            ExeTabla("Pacientes", "clave", "int", "");
            ExeTabla("Pacientes", "NOMBRE", "nvarchar", "(50)");
            ExeTabla("Pacientes", "APATERNO", "nvarchar", "(50)");
            ExeTabla("Pacientes", "AMATERNO", "nvarchar", "(50)");
            ExeTabla("Pacientes", "GENERO", "nvarchar", "(15)");
            ExeTabla("Pacientes", "ESCOLARIDAD", "nvarchar", "(25)");
            ExeTabla("Pacientes", "EMAIL", "nvarchar", "(100)");
            ExeTabla("Pacientes", "EDAD", "nvarchar", "(3)");
            ExeTabla("Pacientes", "ECivil", "nvarchar", "(20)");
            ExeTabla("Pacientes", "NoHijos", "nvarchar", "(2)");
            ExeTabla("Pacientes", "OCUPACION", "nvarchar", "(50)");
            ExeTabla("Pacientes", "TELEFONO", "nvarchar", "(12)");
            ExeTabla("Pacientes", "CALLE", "nvarchar", "(50)");
            ExeTabla("Pacientes", "NoCalle", "nvarchar", "(50)");
            ExeTabla("Pacientes", "CP", "nvarchar", "(25)");
            ExeTabla("Pacientes", "COLONIA", "nvarchar", "(50)");
            ExeTabla("Pacientes", "MUNICIPIO", "nvarchar", "(50)");
            ExeTabla("Pacientes", "CIUDAD", "nvarchar", "(50)");
            ExeTabla("Pacientes", "ESTADO", "nvarchar", "(50)");
            ExeTabla("Pacientes", "Pregunta1", "nvarchar", "(150)");
            ExeTabla("Pacientes", "Pregunta2", "nvarchar", "(150)");
            ExeTabla("Pacientes", "Pregunta3", "nvarchar", "(150)");
            ExeTabla("Pacientes", "RecibeAvisos", "nvarchar", "(2)");
            ExeTabla("Pacientes", "NoExpediente", "nvarchar", "(10)");
            ExeTabla("Pacientes", "SERVICIO", "nvarchar", "(100)");
            ExeTabla("Pacientes", "MEDICO", "nvarchar", "(80)");
            ExeTabla("Pacientes", "TURNO", "nvarchar", "(15)");
            ExeTabla("Pacientes", "OBSERVACIONES", "nvarchar", "(250)");
            ExeTabla("Pacientes", "FECHA", "nvarchar", "(10)");
            ExeTabla("Pacientes", "FCOD", "nvarchar", "(10)");
            ExeTabla("Pacientes", "LUGARNAC", "nvarchar", "(150)");
            ExeTabla("Pacientes", "FECHANAC", "nvarchar", "(10)");
            ExeTabla("Pacientes", "STATUS", "nvarchar", "(1)");
            ExeTabla("Pacientes", "CELULAR", "nvarchar", "(50)");
            ExeTabla("Pacientes", "EMAIL2", "nvarchar", "(50)");
            ExeTabla("Pacientes", "expdental", "nvarchar", "(50)");
            ExeTabla("Pacientes", "expgineco", "nvarchar", "(50)");
            ExeTabla("Pacientes", "expoftamolgo", "nvarchar", "(50)");

            ExeTabla("FotosPacientes", "NoExpediente", "nvarchar", "(10)");
            ExeTabla("FotosPacientes", "Foto", "varbinary(MAX)", "");
            #endregion

            #region camposEstudioColposcopico
            ExeTabla("EColposcopico", "cvpaciente", "int", "");
            ExeTabla("EColposcopico", "NoExpediente", "nvarchar", "(20)");
            ExeTabla("EColposcopico", "ECFecha", "nvarchar", "(10)");
            ExeTabla("EColposcopico", "ECCODFecha", "nvarchar", "(10)");
            ExeTabla("EColposcopico", "TABLA1", "nvarchar", "(100)");
            ExeTabla("EColposcopico", "TABLA2", "nvarchar", "(100)");
            ExeTabla("EColposcopico", "TABLA3", "nvarchar", "(100)");
            ExeTabla("EColposcopico", "ECTratMedico", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTratBiopsia", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTratCono", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTratPAP", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTratNEG", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTratPositivo", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECRHVCervix", "nvarchar", "(50)");
            ExeTabla("EColposcopico", "ECRHEColposcopia", "nvarchar", "(50)");
            ExeTabla("EColposcopico", "ECRHZTransformacion", "nvarchar", "(50)");
            ExeTabla("EColposcopico", "ECRHEAcetoblanco", "nvarchar", "(50)");
            ExeTabla("EColposcopico", "ECRHPuntilleo", "nvarchar", "(50)");
            ExeTabla("EColposcopico", "ECRHMosaico", "nvarchar", "(50)");
            ExeTabla("EColposcopico", "ECRHEGlandular", "nvarchar", "(50)");
            ExeTabla("EColposcopico", "ECRHAEpitelial", "nvarchar", "(50)");
            ExeTabla("EColposcopico", "ECRHEVaginal", "nvarchar", "(50)");
            ExeTabla("EColposcopico", "ECRHShiller", "nvarchar", "(50)");
            ExeTabla("EColposcopico", "ECRHVAnormales", "nvarchar", "(50)");
            ExeTabla("EColposcopico", "ECRHQNAboth", "nvarchar", "(50)");
            ExeTabla("EColposcopico", "ECObservaciones", "nvarchar", "(300)");

            ExeTabla("EColposcopico", "ECTPUM", "nvarchar", "(10)");
            ExeTabla("EColposcopico", "ECTANTFAM", "nvarchar", "(500)");
            ExeTabla("EColposcopico", "ECTALERGIAS", "nvarchar", "(500)");
            ExeTabla("EColposcopico", "ECTCOMEZON", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTPAP", "nvarchar", "(200)");
            ExeTabla("EColposcopico", "ECTPLOMO", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTDIABETES", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTTABACO", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTFLUJO", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTMPF", "nvarchar", "(500)");
            ExeTabla("EColposcopico", "ECTIVSA", "nvarchar", "(10)");
            ExeTabla("EColposcopico", "ECTHIPERTENSION", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTALCOHOL", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTSANGRADO", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTDOCOLPO", "nvarchar", "(10)");
            ExeTabla("EColposcopico", "ECTPS", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTCANCER", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTDROGAS", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTDOCPAP", "nvarchar", "(10)");
            ExeTabla("EColposcopico", "ECTG", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTP", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTC", "nvarchar", "(2");
            ExeTabla("EColposcopico", "ECTA", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTCIRUGIAS", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTOTROS", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTN1", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTN2", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTN3", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTN4", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTN5", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTN6", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTN7", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTN8", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTN9", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTN10", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTN11", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTN12", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTNTUMOR", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTD1", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTD2", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTD3", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTD4", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTD5", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTD6", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTD7", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTD8", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "ECTD9", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "consecutivo", "int", "");
            ExeTabla("EColposcopico", "emitio", "nvarchar", "(25)");
            ExeTabla("EColposcopico", "fechamod", "nvarchar", "(10)");
            ExeTabla("EColposcopico", "mo_fechapap", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "mo_fechadcol", "nvarchar", "(2)");
            ExeTabla("EColposcopico", "numpasex", "int", "");
            ExeTabla("EColposcopico", "textocomezon", "nvarchar", "(200)");
            ExeTabla("EColposcopico", "textotabaco", "nvarchar", "(200)");
            ExeTabla("EColposcopico", "textoplomo", "nvarchar", "(200)");
            ExeTabla("EColposcopico", "textoflujo", "nvarchar", "(200)");
            ExeTabla("EColposcopico", "textoalchol", "nvarchar", "(200)");
            ExeTabla("EColposcopico", "textosangrado", "nvarchar", "(200)");
            ExeTabla("EColposcopico", "textodrogas", "nvarchar", "(200)");
            ExeTabla("EColposcopico", "textocirugia", "nvarchar", "(200)");
            ExeTabla("EColposcopico", "textodiabet", "nvarchar", "(200)");
            ExeTabla("EColposcopico", "textohiper", "nvarchar", "(200)");
            ExeTabla("EColposcopico", "textocancer", "nvarchar", "(200)");
            ExeTabla("EColposcopico", "textootro", "nvarchar", "(200)");

            #endregion

            ExeTabla("imagenesclinica", "noExpediente", "nvarchar", "(15)");
            ExeTabla("imagenesclinica", "nombre", "nvarchar", "(15)");
            ExeTabla("imagenesclinica", "imagenEdit", "varbinary(MAX)", "");
            ExeTabla("imagenesclinica", "consecutivo", "int", "");

            #region camposControlDental
            ExeTabla("EDental", "NoExpediente", "nvarchar", "(10)");
            ExeTabla("EDental", "ODONTOLOGO", "nvarchar", "(50)");
            ExeTabla("EDental", "FECHA", "nvarchar", "(10)");
            ExeTabla("EDental", "FCOD", "nvarchar", "(10)");
            ExeTabla("EDental", "ANTFAM", "nvarchar", "(350)");
            ExeTabla("EDental", "MOTIVO", "nvarchar", "(100)");
            ExeTabla("EDental", "DOLOR", "nvarchar", "(100)");
            ExeTabla("EDental", "CONTROL", "nvarchar", "(100)");
            ExeTabla("EDental", "ENCIAS", "nvarchar", "(100)");
            ExeTabla("EDental", "PROTESICA", "nvarchar", "(100)");
            ExeTabla("EDental", "OTRO", "nvarchar", "(100)");
            ExeTabla("EDental", "EACTUAL", "nvarchar", "(250)");
            ExeTabla("EDental", "P1", "nvarchar", "(2)");
            ExeTabla("EDental", "P2", "nvarchar", "(2)");
            ExeTabla("EDental", "P3", "nvarchar", "(2)");
            ExeTabla("EDental", "P4", "nvarchar", "(2)");
            ExeTabla("EDental", "P5", "nvarchar", "(2)");
            ExeTabla("EDental", "P6", "nvarchar", "(2)");
            ExeTabla("EDental", "P7", "nvarchar", "(2)");
            ExeTabla("EDental", "P8", "nvarchar", "(2)");
            ExeTabla("EDental", "P9", "nvarchar", "(2)");
            ExeTabla("EDental", "P10", "nvarchar", "(2)");
            ExeTabla("EDental", "P11", "nvarchar", "(2)");
            ExeTabla("EDental", "P12", "nvarchar", "(2)");
            ExeTabla("EDental", "P13", "nvarchar", "(2)");
            ExeTabla("EDental", "P14", "nvarchar", "(2)");
            ExeTabla("EDental", "P15", "nvarchar", "(2)");
            ExeTabla("EDental", "P16", "nvarchar", "(2)");
            ExeTabla("EDental", "P17", "nvarchar", "(2)");
            ExeTabla("EDental", "P18", "nvarchar", "(2)");
            ExeTabla("EDental", "P19", "nvarchar", "(2)");
            ExeTabla("EDental", "P20", "nvarchar", "(2)");
            ExeTabla("EDental", "P21", "nvarchar", "(2)");
            ExeTabla("EDental", "P22", "nvarchar", "(2)");
            ExeTabla("EDental", "P23", "nvarchar", "(2)");
            ExeTabla("EDental", "P24", "nvarchar", "(2)");
            ExeTabla("EDental", "P25", "nvarchar", "(2)");
            ExeTabla("EDental", "P26", "nvarchar", "(2)");
            ExeTabla("EDental", "P27", "nvarchar", "(2)");
            ExeTabla("EDental", "P28", "nvarchar", "(2)");
            ExeTabla("EDental", "P29", "nvarchar", "(2)");
            ExeTabla("EDental", "P30", "nvarchar", "(2)");
            ExeTabla("EDental", "P31", "nvarchar", "(2)");
            ExeTabla("EDental", "P32", "nvarchar", "(2)");
            ExeTabla("EDental", "P33", "nvarchar", "(2)");
            ExeTabla("EDental", "P34", "nvarchar", "(2)");
            ExeTabla("EDental", "P35", "nvarchar", "(2)");
            ExeTabla("EDental", "P36", "nvarchar", "(2)");
            ExeTabla("EDental", "P37", "nvarchar", "(2)");
            ExeTabla("EDental", "P38", "nvarchar", "(2)");
            ExeTabla("EDental", "P39", "nvarchar", "(2)");
            ExeTabla("EDental", "P40", "nvarchar", "(2)");
            ExeTabla("EDental", "ASPECTO", "nvarchar", "(100)");
            ExeTabla("EDental", "CARA", "nvarchar", "(50)");
            ExeTabla("EDental", "LABIOS", "nvarchar", "(50)");
            ExeTabla("EDental", "PALPACIONG", "nvarchar", "(100)");
            ExeTabla("EDental", "GANGLIOS", "nvarchar", "(100)");
            ExeTabla("EDental", "ATM", "nvarchar", "(50)");
            ExeTabla("EDental", "OREJAS", "nvarchar", "(50)");
            ExeTabla("EDental", "REGIONHT", "nvarchar", "(50)");
            ExeTabla("EDental", "CARRILLOS", "nvarchar", "(50)");
            ExeTabla("EDental", "MUCOSA", "nvarchar", "(50)");
            ExeTabla("EDental", "ENCIA", "nvarchar", "(50)");
            ExeTabla("EDental", "LENGUA", "nvarchar", "(50)");
            ExeTabla("EDental", "PALADAR", "nvarchar", "(50)");
            ExeTabla("EDental", "LABORATORIO", "nvarchar", "(50)");
            ExeTabla("EDental", "MODELO", "nvarchar", "(50)");
            ExeTabla("EDental", "TENSIONART", "nvarchar", "(50)");
            ExeTabla("EDental", "OBSERVACIONES", "nvarchar", "(250)");
            ExeTabla("EDental", "DERIVACIONES", "nvarchar", "(100)");
            ExeTabla("EDental", "DIAGNOSTICO", "nvarchar", "(500)");
            ExeTabla("EDental", "SECFECHA1", "nvarchar", "(10)");
            ExeTabla("EDental", "SECDESCRIPCION1", "nvarchar", "(250)");
            ExeTabla("EDental", "SECFECHA2", "nvarchar", "(10)");
            ExeTabla("EDental", "SECDESCRIPCION2", "nvarchar", "(250)");
            ExeTabla("EDental", "SECFECHA3", "nvarchar", "(10)");
            ExeTabla("EDental", "SECDESCRIPCION3", "nvarchar", "(250)");
            ExeTabla("EDental", "SECFECHA4", "nvarchar", "(10)");
            ExeTabla("EDental", "SECDESCRIPCION4", "nvarchar", "(250)");
            ExeTabla("EDental", "SECFECHA5", "nvarchar", "(10)");
            ExeTabla("EDental", "SECDESCRIPCION5", "nvarchar", "(250)");
            ExeTabla("EDental", "SECFECHA6", "nvarchar", "(10)");
            ExeTabla("EDental", "SECDESCRIPCION6", "nvarchar", "(250)");
            ExeTabla("EDental", "derivPeriodoncia", "nvarchar", "(1)");
            ExeTabla("EDental", "derivEndodoncia", "nvarchar", "(1)");
            ExeTabla("EDental", "derivCirugia", "nvarchar", "(1)");
            ExeTabla("EDental", "derivEstomatologia", "nvarchar", "(1)");
            ExeTabla("EDental", "derivRadiologia", "nvarchar", "(1)");
            ExeTabla("EDental", "derivOtros", "nvarchar", "(1)");
            ExeTabla("EDental", "consecutivo", "int", "");
            #endregion

            #region camposControlPrenatal
            ExeTabla("CPrenatalH", "cvprenatalH", "nvarchar", "(10)");
            ExeTabla("CPrenatalH", "cvpaciente", "nvarchar", "(10)");
            ExeTabla("CPrenatalH", "NoExpediente", "nvarchar", "(10)");
            ExeTabla("CPrenatalH", "HFECHA", "nvarchar", "(10)");
            ExeTabla("CPrenatalH", "HEDAD", "nvarchar", "(3)");
            ExeTabla("CPrenatalH", "HPESO", "nvarchar", "(10)");
            ExeTabla("CPrenatalH", "Htalla", "nvarchar", "(10)");
            ExeTabla("CPrenatalH", "HFOCO", "nvarchar", "(100)");
            ExeTabla("CPrenatalH", "cervix", "nvarchar", "(100)");
            ExeTabla("CPrenatalH", "consistencia", "nvarchar", "(100)");
            ExeTabla("CPrenatalH", "dilatacion", "nvarchar", "(100)");
            ExeTabla("CPrenatalH", "borramiento", "nvarchar", "(100)");
            ExeTabla("CPrenatalH", "pelvis", "nvarchar", "(100)");
            ExeTabla("CPrenatalH", "alturapres", "nvarchar", "(100)");
            ExeTabla("CPrenatalH", "salidaliqami", "nvarchar", "(100)");
            ExeTabla("CPrenatalH", "HUTERO", "nvarchar", "(50)");
            ExeTabla("CPrenatalH", "HPRODUCTO", "nvarchar", "(150)");
            ExeTabla("CPrenatalH", "HEDEMA", "nvarchar", "(100)");
            ExeTabla("CPrenatalH", "HTRATAMIENTO", "nvarchar", "(250)");
            ExeTabla("CPrenatalH", "CARACTERISTICA", "nvarchar", "(250)");
            ExeTabla("CPrenatalH", "PRESENTACION", "nvarchar", "(250)");
            ExeTabla("CPrenatalH", "HIDX", "nvarchar", "(250)");
            ExeTabla("CPrenatalH", "TACTOVAGINAL", "nvarchar", "(250)");
            ExeTabla("CPrenatalH", "ANTHERE", "nvarchar", "(250)");
            ExeTabla("CPrenatalH", "PERSONALPAT", "nvarchar", "(250)");
            ExeTabla("CPrenatalH", "ULTRAPREV", "nvarchar", "(250)");
            ExeTabla("CPrenatalH", "LABPREV", "nvarchar", "(250)");

            ExeTabla("CPrenatalH", "HTA", "nvarchar", "(10)");
            ExeTabla("CPrenatalH", "HOBSERVACIONES", "nvarchar", "(250)");
            ExeTabla("CPrenatalH", "consecutivo", "int", "");



            ExeTabla("CPrenatal", "NoExpediente", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "FECHA", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "FCOD", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "GSANGUINEO", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "FRR", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "GESTA", "nvarchar", "(15)");
            ExeTabla("CPrenatal", "PARA", "nvarchar", "(15)");
            ExeTabla("CPrenatal", "ABORTOS", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "CESAREAS", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "OBSERVACIONES1", "nvarchar", "(250)");
            ExeTabla("CPrenatal", "PFECHA", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "PFCOD", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "PHORA", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "PARTO", "nvarchar", "(2)");
            ExeTabla("CPrenatal", "PARTOCAUSA1", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "PARTOCAUSA2", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "PALUMBRAMIENTO", "nvarchar", "(2)");
            ExeTabla("CPrenatal", "PEPISTOTOMIA", "nvarchar", "(2)");
            ExeTabla("CPrenatal", "PDESGARROS", "nvarchar", "(2)");
            ExeTabla("CPrenatal", "PRCAVIDAD", "nvarchar", "(2)");
            ExeTabla("CPrenatal", "PDURACION", "nvarchar", "(2)");
            ExeTabla("CPrenatal", "PROSEXO", "nvarchar", "(2)");
            ExeTabla("CPrenatal", "PROPESO", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "PROMIN", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "PROANORMALIDADES", "nvarchar", "(2)");
            ExeTabla("CPrenatal", "PROSITIPO", "nvarchar", "(2)");
            ExeTabla("CPrenatal", "PROTIPO", "nvarchar", "(100)");
            ExeTabla("CPrenatal", "AAGRUPO1", "nvarchar", "(2)");
            ExeTabla("CPrenatal", "AARESULTADO1", "nvarchar", "(100)");
            ExeTabla("CPrenatal", "AAGRUPO2", "nvarchar", "(2)");
            ExeTabla("CPrenatal", "AARESULTADO2", "nvarchar", "(100)");
            ExeTabla("CPrenatal", "AAOTROS", "nvarchar", "(250)");
            ExeTabla("CPrenatal", "AAPSICO", "nvarchar", "(2)");
            ExeTabla("CPrenatal", "AARESULTADO", "nvarchar", "(100)");
            ExeTabla("CPrenatal", "AAPBSERVACIONES", "nvarchar", "(250)");
            ExeTabla("CPrenatal", "AAPRESENTE", "nvarchar", "(2)");
            ExeTabla("CPrenatal", "RPPFECHA", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "RPPPESO", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "RPPTA", "nvarchar", "(10)");
            ExeTabla("CPrenatal", "RPPLACTANCIA", "nvarchar", "(15)");
            ExeTabla("CPrenatal", "RPPUERPERIO", "nvarchar", "(2)");
            ExeTabla("CPrenatal", "RPPCAUSA", "nvarchar", "(100)");
            ExeTabla("CPrenatal", "RPPOTROS", "nvarchar", "(250)");
            ExeTabla("CPrenatal", "EABDOMEN", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "EGMAMARIAS", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "EVULVA", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "EEPISIOTOMIA", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "EVVAGINOTOMIA", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "EVDESGARROS", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "EVLEUCEMIA", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "ECDESGARROS", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "ECEROSIONES", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "EUTERO", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "EANEXOS", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "EEPEDIDOS", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "EPAPANICOLAOU", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "ETRATAMIENTO", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "EMETODO", "nvarchar", "(150)");
            ExeTabla("CPrenatal", "consecutivo", "int", "");
            #endregion
        }

        public static void FabricaValores()
        {
            conectorSql conecta=new conectorSql();
            string query = "Select * from vendedores where cvvendedor='0'";
            bool existe = conecta.ExisteRegistro(query);

            if (existe == false)
            { 
               query="Insert into vendedores(cvvendedor,nombre,comision,porcentaje) values('2','NO APLICA','NO','0')";
               conecta.Excute(query);
               query = "Insert into vendedores(cvvendedor,nombre,comision,porcentaje) values('0','MOSTRADOR','NO','0')";
               conecta.Excute(query);
               query = "Insert into vendedores(cvvendedor,nombre,comision,porcentaje) values('1','PEDIDO','NO','0')";
               conecta.Excute(query);
            }


            query = "Select * from bancos where cvbanco='0'";
            existe = conecta.ExisteRegistro(query);

            if (existe == false)
            {
                query = "Insert into bancos(cvbanco,nombre,cuenta,interbancaria,sucursal,nombredeposito) values('0','NO APLICA','0000','0000','0','NO APLICA')";
                conecta.Excute(query);
            }


            query = "Select * from consecutivos where numpedido>-1";
            existe = conecta.ExisteRegistro(query);

            if (existe == false)
            {
                query = "Insert into consecutivos(numcliente,numremision,numprov,numbanco,numempresa,numsucursal,numproducto,numpedido,numcotiza,numrecibo) values('1','1','1','1','1','1','1','1','1','1')";
                conecta.Excute(query);
            }

            
            query = "Select * from Formadepago where cvforma<>''";
            existe = conecta.ExisteRegistro(query);

            if (existe == false)
            {
                query = "Insert into Formadepago(cvforma,nombre) values('Efectivo','Efectivo')";
                conecta.Excute(query);
                query = "Insert into Formadepago(cvforma,nombre) values('Transfer','Transferencia Electronica')";
                conecta.Excute(query);
                query = "Insert into Formadepago(cvforma,nombre) values('NoIdentificado','No Identificado')";
                conecta.Excute(query);
                query = "Insert into Formadepago(cvforma,nombre) values('Cheque','Cheque')";
                conecta.Excute(query);
                query = "Insert into Formadepago(cvforma,nombre) values('Debito','Tarjeta de Debito')";
                conecta.Excute(query);
                query = "Insert into Formadepago(cvforma,nombre) values('Credito','Tarjeta de Credito')";
                conecta.Excute(query);
                query = "Insert into Formadepago(cvforma,nombre) values('Fondos','Transferencia Electrónica de Fondos')";
                conecta.Excute(query);
            }
        
        }

        public static void FabricaIngresoUsuario()
        {
            conectorSql conecta = new conectorSql();
            string query = "";

            query = "Select * from usuarios where cvusuario='ADMIN'";
           bool exitereg = conecta.ExisteRegistro(query);
            conecta.CierraConexion();
            if (exitereg == false)
            {
                query = "Insert into USUARIOS(cvusuario,contra,cvempleado";
                query = query + ",statustrab";
                query = query + ",ads";
                query = query + ",puestos";
                query = query + ",plazas";
                query = query + ",nombra";
                query = query + ",funciones";
                query = query + ",ocupacional";
                query = query + ",gradoest";
                query = query + ",profesion";
                query = query + ",maestria";
                query = query + ",doctorado";
                query = query + ",especialidad";
                query = query + ",lugartrab";
                query = query + ",unidadadm";
                query = query + ",direccion";
                query = query + ",subdireccion";
                query = query + ",gerencia";
                query = query + ",areacap";
                query = query + ",cursos";
                query = query + ",nomhorario";
                query = query + ",retardos";
                query = query + ",relacionhorario";
                query = query + ",configurahor";
                query = query + ",diafestivo";
                query = query + ",nacionalidad";
                query = query + ",edentidad";
                query = query + ",delegacion";
                query = query + ",estadocivil";
                query = query + ",cambiocontra";
                query = query + ",alta";
                query = query + ",listadotrab";
                query = query + ",activar";
                query = query + ",asignahor";
                query = query + ",asignaexp";
                query = query + ",eslistas";
                query = query + ",contancias";
                query = query + ",congresos";
                query = query + ",cuidadosmat";
                query = query + ",cursoscap";
                query = query + ",diaseconomicos";
                query = query + ",guardia";
                query = query + ",incapacidad";
                query = query + ",omision";
                query = query + ",comisionsindical";
                query = query + ",comisionoficial";
                query = query + ",volante";
                query = query + ",jretardos";
                query = query + ",licenciacongoce";
                query = query + ",licenciasingoce";
                query = query + ",onomastico";
                query = query + ",pases";
                query = query + ",suspension";
                query = query + ",vacaciones";
                query = query + ",probchecador";
                query = query + ",diatrabajador";
                query = query + ",verificacion";
                query = query + ",autorizacion";
                query = query + ",resumen";
                query = query + ",comprimir";
                query = query + ",entradasalida";
                query = query + ",faltas";
                query = query + ",repretardos";
                query = query + ",reptolerancia";
                query = query + ",repomisionE";
                query = query + ",repomisionS";
                query = query + ",repsalidaAnt";
                query = query + ",repincidencias";
                query = query + ",reglajustifica";
                query = query + ",configura";
                query = query + ",altauser";
                query = query + ",importaemple";
                query = query + ",respaldo";
                query = query + ",ReDetallado";
                query = query + ",bitacora";
                query = query + ",consulta";
                query = query + ",modifica";
                query = query + ",elimina";
                query = query + ",docentes";
                query = query + ",cambioplaza";
                query = query + ",convocacurso";
                query = query + ",reglainciden";
                query = query + ",listaestimulo";
                query = query + ",listaextra)";
                query = query + ") values('ADMIN','1234',''";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI'";
                query = query + ",'SI')";
                conecta.Excute(query);
            }

        }

        
   
        private  static void ExeTabla(string Ctable, string Ccampo, string Tipo, string Valor)
        {
            string Query="";
                Query="ALTER TABLE " + Ctable + " ALTER COLUMN " + Ccampo + " " + Tipo + " " +Valor;
                Excute(Query,Ctable,Ccampo,Tipo,Valor);
        }

        public static void TodasTablas()
        {
            CATALOGOS();
        }

        public static void ProcTodasTablas(Label Etiq, ProgressBar Proceso)
        {
            Proceso.Maximum = 107;
            CATALOGOS();
            Etiq.Text= VProceso.ToString();
            Etiq.Refresh();
            Proceso.Refresh();
            Proceso.Value = VProceso;

        }

    }

