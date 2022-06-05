using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using DGMC.TimbraCFDI;
namespace SHOPCONTROL
{
    public partial class CancelarFactura : Form
    {
        public CancelarFactura()
        {
            InitializeComponent();
        }

        public string USUARIOFAC = "";
        public string CONTRASEÑAFAC = "";
        public string CORREOFAC = "";
        public string CADDIRECCION = "";

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
        public void ParametrosFacturacion()
        {
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
                //RFC = "AAA010101AAA";
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
             
            }
            conecta.CierraConexion();
        }

        private void CancelarFactura_Load(object sender, EventArgs e)
        {
            textBox1.Text = valoresg.NUMPEDIDO;
            ParametrosFacturacion();
            BusquedaUsuario();
        }

        public void BusquedaUsuario()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from parametros where usuariofolio<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                USUARIOFAC = leer["usuarioFolio"].ToString();
                CONTRASEÑAFAC = leer["contraFolio"].ToString();
                CORREOFAC = leer["correover"].ToString();
                CADDIRECCION = leer["dirRespaldo"].ToString();
            }
            conecta.CierraConexion();
        }
        public void CancelarFacturaMe(string numpedido, string ayo)
        {
         
            string idcomprobante = "";
            string folioFiscal = "";
            string cvcliente="";
            conectorSql conecta = new conectorSql();
              conectorSql conecta3 = new conectorSql();
            string Query = "Select * from pedidos where numpedido='" + numpedido + "' and ayo='" + ayo + "'";
            Query = "Select numfactura,folioFiscal,cvcliente from facturas where numpedido='" + numpedido + "' and ayo='" + ayo + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                idcomprobante = leer["numfactura"].ToString();
                folioFiscal = leer["folioFiscal"].ToString();
                cvcliente = leer["cvcliente"].ToString();
            }
            conecta.CierraConexion();

            if (folioFiscal != "" && folioFiscal != "0")
            {
                bool banderapro = false;
                if (PASARAPRODUCTIVO == "SI") banderapro = true;
                Conector conector = new Conector(banderapro);
                try
                {

                    conector.EstableceCredenciales(USUARIO);
                    //Rfc Emisor label1
                    string rfcEmisor = RFC;
                    //Folio Fiscal - UUID
                    ResultadoCancelacion resultadoCancelacion;
                    resultadoCancelacion = conector.CancelaCFDI(rfcEmisor, folioFiscal);
                    //Verificamos el resultado
                    if (resultadoCancelacion.Exitoso)
                    {
                        //El comprobante fue cancelado exitosamente
                       string  consulta = "update facturas set estatus='CANCELADO' where numfactura='" + numpedido + "' and cvcliente='" + cvcliente + "'";
                        conecta3.ExisteRegistro(consulta);
                        conecta3.CierraConexion();
                        BitacoraParaFacturas(numpedido, "Cancelación exitosa " + resultadoCancelacion.Descripcion, "");
                    }
                    else
                    {
                        //No se pudo cancelar, mostramos respuesta
                        BitacoraParaFacturas(numpedido, resultadoCancelacion.Descripcion, "");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "\nLa Factura ya se encuentra cancelada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                Query = "Update facturas set estatus='CANCELADO' where numpedido='" + numpedido + "' and ayo='" + ayo + "'";
                conecta.Excute(Query);

                Query = "Update pedidos set estatuspedido='CANCELADO' where numpedido='" + numpedido + "' and ayo='" + ayo + "'";
                conecta.Excute(Query);

                MessageBox.Show("Se cancelo correctamente la factura", "Cancelación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("No se encuentra facturado el pedido ", "Cancelación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Query = "Update facturas set estatus='CANCELADO' where numpedido='" + numpedido + "' and ayo='" + ayo + "'";
                conecta.Excute(Query);

                Query = "Update pedidos set estatuspedido='CANCELADO' where numpedido='" + numpedido + "' and ayo='" + ayo + "'";
                conecta.Excute(Query);

                MessageBox.Show("Se cancelo correctamente la factura", "Cancelación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();

                textBox1.Focus();
            }

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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string numpedido = textBox1.Text;
            string ayo = "";
            conectorSql conecta = new conectorSql();
            string Query = "Select ayo from pedidos where numpedido='" + textBox1.Text + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ayo = leer["ayo"].ToString();
            }
            conecta.CierraConexion();


            CancelarFacturaMe(numpedido, ayo);

            Query = "Insert into cancelaciones (numpedido,observacion,emitio,fecha,fechacod) values(";
            Query = Query + "'" + textBox1.Text + "'";
            Query = Query + ",'CANCELACION DE FACTURA'";
            Query = Query + ",'" + valoresg.USUARIOSIS + "'";
            Query = Query + ",'" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
            Query = Query + ",'" + DateTime.Now.ToString("yyyyMMdd") + "')";
            conecta.Excute(Query);

            Query = "Update pagos set estatus='CANCELADO' where numpedido='" + numpedido + "'";
            conecta.Excute(Query);

            Query = "Update Creditos set estatus='CANCELADO' where numpedido='" + numpedido + "'";
            conecta.Excute(Query);

            Query = "Update Pedidos set estatuspedido='CANCELADO' where numpedido='" + numpedido + "' and ayo='" + ayo + "'";
            conecta.Excute(Query);

            //Query = "Delete from CobroenVentana where numpedido='" + numpedido + "'";
            //conecta.Excute(Query);
        }
    }
}
