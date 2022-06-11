using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using SHOPCONTROL.Analisys;
using SHOPCONTROL.Inventarios;

namespace SHOPCONTROL
{
    public partial class Bienvenidos : Form
    {


        public string HoraEntradaDoc = "";
        public string HoraSalidaDoc = "";

        public string HoraEntradaComedor = "";
        public string HoraSalidaComedor = "";
        public string DCOMEDOR = "";
        public int tiempoCon = 0;
        public string cvdoctor = "";
        public decimal Tminentrada = 0;
        public decimal Tminsalida = 0;

        public decimal tminEntraComedor = 0;
        public decimal tminSalComedor = 0;
        public int TiempoPred = 0;
        public string cvpaciente = "";
        public string nombrepaciente = "";
        public string Sel_cvdoctor = "";
        public string Sel_FechaCitare = "";

        public Bienvenidos()
        {
            InitializeComponent();
        }

        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {

        }

        private void municipiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clientes cliente = new Clientes();
            cliente.Show();
        }

        private void nuevoContratoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Productos producto = new Productos();
            producto.Show();
        }

        public void VersionVerificar()
        {
            string VERSION = "";
            conectorSql conecta = new conectorSql();
            string Query = "Select * from parametros where versionbill<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                VERSION = leer["versionbill"].ToString();
            }
            conecta.CierraConexion();

            if (VERSION == "FACTURA" || VERSION=="")
            {
                reciboToolStripMenuItem.Visible = false;
                pagosDePedidosToolStripMenuItem.Visible = false;
                importaciónDeProductosserviciosExcelToolStripMenuItem.Visible = false;
                reporteDeRecibosToolStripMenuItem.Visible = false;
            }
            else
            {
                reciboToolStripMenuItem.Visible = true;
                pagosDePedidosToolStripMenuItem.Visible = true;
                importaciónDeProductosserviciosExcelToolStripMenuItem.Visible = true;
                reporteDeRecibosToolStripMenuItem.Visible = true;
            }
        }


        public string ENTRA1;
        public string ENTRA2;
        public string ENTRA3;
        public string ENTRA4;
        public string ENTRA5;
        public string ENTRA6;
        public string ENTRA7;
        public string ENTRA8;
        public string ENTRA9;
        public string ENTRA10;
        public string ENTRA11;
        public string ENTRA12;
        public string ENTRA13;
        public string ENTRA14;
        public string ENTRA15;
        public string ENTRA16;
        public string ENTRA17;
        public string ENTRA18;

        public string ENTRA19;
        public string ENTRA20;
        public string ENTRA21;
        public string ENTRA22;
        public string ENTRA23;
        public string ENTRA24;
        public string ENTRA25;
        public string ENTRA26;
        public string ENTRA27;
        public string ENTRA28;
        public string ENTRA29;
        public string ENTRA30;


        public void PermisosUsuario()
        {

            if (valoresg.USUARIOSIS == "ADMIN") return;

            empresaToolStripMenuItem.Enabled = false;
            estadosToolStripMenuItem.Enabled = false;
            municipiosToolStripMenuItem.Enabled = false;
            vendedoresToolStripMenuItem.Enabled = false;
            formaDePagoToolStripMenuItem.Enabled = false;
            bancosToolStripMenuItem.Enabled = false;
            nuevoContratoToolStripMenuItem.Enabled = false;

            operacionesToolStripMenuItem.Enabled = false;
            notasDeRemisionToolStripMenuItem.Enabled = false;
            reciboToolStripMenuItem.Enabled = false;
            pagosDePedidosToolStripMenuItem.Enabled = false;

            reporteDeFacturaciónToolStripMenuItem.Enabled = false;
           
            cosecutivosToolStripMenuItem.Enabled = false;
            configuraciónToolStripMenuItem.Enabled = false;
            configurarFacturacionElectronicaToolStripMenuItem.Enabled = false;
            importaciónDeProductosserviciosExcelToolStripMenuItem.Enabled = false;
            baseDeDatosToolStripMenuItem.Enabled = false;

            accesoAUsuariosToolStripMenuItem.Enabled = false;
            linkLabel1.Enabled = false;
            linkLabel2.Enabled = false;
            linkLabel4.Enabled = false;
            linkLabel3.Enabled = false;


      
            reimpresionDeFacturaToolStripMenuItem.Enabled = false;
            reporteDeRecibosToolStripMenuItem.Enabled = false;

            catalogoDeDoctoresToolStripMenuItem.Enabled = false;
            catalogoDeServiciosToolStripMenuItem.Enabled = false;
            registroDeGastosToolStripMenuItem.Enabled = false;
            toolStripMenuItem2.Enabled = false;
            linkLabel4.Enabled = false;

            notasDeEvoluciónToolStripMenuItem.Enabled = false;
            linkLabel5.Enabled = false;

            toolStripMenuItem3.Enabled = false;
            historiaClinicaOftamologiaToolStripMenuItem1.Enabled = false;
            ginecologiaToolStripMenuItem.Enabled = false;

            registroDeCitasToolStripMenuItem.Enabled = false;
            linkLabel3.Enabled = false;

            registroDeGastosToolStripMenuItem.Enabled = false;
            configurarCorreoElectronicoToolStripMenuItem.Enabled = false;
      

            conectorSql conecta = new conectorSql();
            string Query = "Select * from usuarios where cvusuario='" + valoresg.USUARIOSIS + "'";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {

                label5.Text = leer["nombre"].ToString();

                ENTRA1 = leer["entra1"].ToString();
                ENTRA2 = leer["entra2"].ToString();
                ENTRA3 = leer["entra3"].ToString();
                ENTRA4 = leer["entra4"].ToString();
                ENTRA5 = leer["entra5"].ToString();
                ENTRA6 = leer["entra6"].ToString();
                ENTRA7 = leer["entra7"].ToString();
                ENTRA8 = leer["entra8"].ToString();
                ENTRA9 = leer["entra9"].ToString();
                ENTRA10 = leer["entra10"].ToString();
                ENTRA11 = leer["entra11"].ToString();
                ENTRA12 = leer["entra12"].ToString();
                ENTRA13 = leer["entra13"].ToString();
                ENTRA14 = leer["entra14"].ToString();
                ENTRA15 = leer["entra15"].ToString();
                ENTRA16 = leer["entra16"].ToString();
                ENTRA17 = leer["entra17"].ToString();
                ENTRA18 = leer["entra18"].ToString();

                ENTRA19 = leer["entra19"].ToString();

                ENTRA20 = leer["entra20"].ToString();
                ENTRA21 = leer["entra21"].ToString();
                ENTRA22 = leer["entra22"].ToString();
                ENTRA23 = leer["entra23"].ToString();
                ENTRA24 = leer["entra24"].ToString();
                ENTRA25 = leer["entra25"].ToString();
                ENTRA26 = leer["entra26"].ToString();
                ENTRA27 = leer["entra27"].ToString();
                ENTRA28 = leer["entra28"].ToString();
                ENTRA29 = leer["entra29"].ToString();
                ENTRA30 = leer["entra30"].ToString();


                if (ENTRA1 == "SI") empresaToolStripMenuItem.Enabled = true;
                if (ENTRA2 == "SI") estadosToolStripMenuItem.Enabled = true;
                if (ENTRA3 == "SI") municipiosToolStripMenuItem.Enabled = true;
                if (ENTRA4 == "SI") vendedoresToolStripMenuItem.Enabled = true;
                if (ENTRA5 == "SI") formaDePagoToolStripMenuItem.Enabled = true;
                if (ENTRA6 == "SI") bancosToolStripMenuItem.Enabled = true;
                if (ENTRA7 == "SI")
                {
                    linkLabel2.Enabled = true;
                    nuevoContratoToolStripMenuItem.Enabled = true;
                }

                if (ENTRA8 == "SI") operacionesToolStripMenuItem.Enabled = true;
                if (ENTRA9 == "SI")
                {
                    notasDeRemisionToolStripMenuItem.Enabled = true;
                    linkLabel1.Enabled = true;
               
                }
                if (ENTRA10 == "SI") reciboToolStripMenuItem.Enabled = true;
                if (ENTRA11 == "SI") pagosDePedidosToolStripMenuItem.Enabled = true;

                if (ENTRA12 == "SI") reporteDeFacturaciónToolStripMenuItem.Enabled = true;

                if (ENTRA13 == "SI") cosecutivosToolStripMenuItem.Enabled = true;
                if (ENTRA14 == "SI") configuraciónToolStripMenuItem.Enabled = true;
                if (ENTRA15 == "SI")
                {
                  
                    configurarFacturacionElectronicaToolStripMenuItem.Enabled = true;
                }

                if (ENTRA16 == "SI") importaciónDeProductosserviciosExcelToolStripMenuItem.Enabled = true;
                if (ENTRA17 == "SI") baseDeDatosToolStripMenuItem.Enabled = true;

                if (ENTRA18 == "SI") accesoAUsuariosToolStripMenuItem.Enabled = true;

                if (ENTRA19 == "SI") reimpresionDeFacturaToolStripMenuItem.Enabled = true;
                if (ENTRA20 == "SI") reporteDeRecibosToolStripMenuItem.Enabled = true;

                if (ENTRA21 == "SI") catalogoDeServiciosToolStripMenuItem.Enabled = true;
                if (ENTRA22 == "SI") catalogoDeDoctoresToolStripMenuItem.Enabled = true;
                if (ENTRA23 == "SI") registroDeGastosToolStripMenuItem.Enabled = true;

                if (ENTRA24 == "SI")
                {
                    toolStripMenuItem2.Enabled = true;
                    linkLabel4.Enabled = true;
                }

                if (ENTRA25 == "SI")
                {
                    notasDeEvoluciónToolStripMenuItem.Enabled = true;
                    linkLabel5.Enabled = true;
                }
                if (ENTRA26 == "SI") toolStripMenuItem3.Enabled = true;
                if (ENTRA27 == "SI") historiaClinicaOftamologiaToolStripMenuItem1.Enabled = true;
                if (ENTRA28 == "SI") ginecologiaToolStripMenuItem.Enabled = true;
                if (ENTRA29 == "SI")
                {
                    registroDeCitasToolStripMenuItem.Enabled = true;
                    linkLabel3.Enabled = true;
                }

                if (ENTRA30 == "SI")
                {
                    registroDeGastosToolStripMenuItem.Enabled = true;
                    configurarCorreoElectronicoToolStripMenuItem.Enabled = true;
                }
                


            }
            conecta.CierraConexion();
        
        }

        public void InformacionEmpresa()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from ParametrosRecibo where NombreComercial<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                label1.Text = leer["NombreComercial"].ToString();
                label6.Text = leer["InfoAdicional"].ToString();
            }
            conecta.CierraConexion();
        }

        private void Bienvenidos_Load(object sender, EventArgs e)
        {
            string opcionserver = Registro.ReadRegSHOPCONTROL("CON", "OPCIONSERVER");
            if (opcionserver == "0") label7.Text = "CUERNAVACA";
            if (opcionserver == "1") label7.Text = "SUCURSAL MOLINA";
            if (opcionserver == "2") label7.Text = "SUCURSAL BELLAS ARTES";
            this.Text = this.Text + " -" + label7.Text;
           // CTablas.CATALOGOS();
            VersionVerificar();
            valoresg.VIENEBUSQUEDAPEDIDO = "NO";
            InformacionEmpresa();
            NombredeUsuario();
          
            PreEntradaLimpia();
           
            toolStripStatusLabel1.Text = DateTime.Now.ToString("dd/MM/yyyy");
           
            bool entroLinea = InternetDisponible.IsConnectedToInternet();

            if (entroLinea == true) label3.BackColor = Color.Lime;
            else label3.BackColor = Color.Red;

            if (DatosdeFacturacion() == true)
            {
                MessageBox.Show("Ingrese los datos del cliente para facturación electronica", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FacElectronica datos = new FacElectronica();
                datos.Show();
            }
            Image imagenempresa = ClaseFotos.ConsultarFotoEmpresa("0");
            if (imagenempresa != null)
                pictureBox2.Image = imagenempresa;

            PermisosUsuario();
          
        }

        public void PreEntradaLimpia()
        {
            /*
            Licenciamiento prelic = new Licenciamiento();
            string Generado=prelic.PreLicenciaBillLine();

            conectorSql conecta = new conectorSql();
            string Query = "Select * from parametros where modelosis=''";
          
            bool existeVacio = conecta.ExisteRegistro(Query);
            if (existeVacio == true)
            {
                Query = "Update parametros set modelosis='" + Generado + "'";
                conecta.Excute(Query);

                LicenciaRegistro registro = new LicenciaRegistro();
                registro.Show();
            }
            else
            {
                Query = "Select * from parametros where sistema=''";
                existeVacio = conecta.ExisteRegistro(Query);
                if (existeVacio == true)
                {
                    LicenciaRegistro registro = new LicenciaRegistro();
                    registro.Show();
                }
            }
            */
           
        }

        public bool DatosdeFacturacion()
        {

            conectorSql conecta = new conectorSql();
            string Query = "Select *  from ParametrosFactura  where nombre='' ";
            bool existe = conecta.ExisteRegistro(Query);
            return existe;

        }

        public void NombredeUsuario()
        {
            label5.Text = "";
            valoresg.NOMBREUSUARIO = "";
            conectorSql conecta = new conectorSql();
            string Query = "Select *  from usuarios  where cvusuario='" + valoresg.USUARIOSIS +"'";
            SqlDataReader leer= conecta.RecordInfo(Query);
            while (leer.Read())
            {
                label5.Text = leer["nombre"].ToString();
                valoresg.NOMBREUSUARIO = label5.Text;
            }
            conecta.CierraConexion();
        }

     
     
        private void notasDeRemisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pedidospro remitir = new Pedidospro();
            remitir.Show();
        }

        private void estadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Proveedores pantalla = new Proveedores();
            pantalla.Show();
        }

        private void reporteGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("En mantenimiento contacte a su proveedor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void carteraDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("En mantenimiento contacte a su proveedor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void registroDePagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PagosCreditos pagos = new PagosCreditos();
            pagos.Show();
        }

        private void cosecutivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consecutivos pantalla = new Consecutivos();
            pantalla.Show();
        }

        private void empresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Empresa pantalla = new Empresa();
            pantalla.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Recibos entrarecibo = new Recibos();
            entrarecibo.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Productos nuevopro = new Productos();
            nuevopro.Show();
        }

        private void reporteDeFacturaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            valoresg.CVREPORTE = "FACTURACION";
            ReporteGeneral reporte = new ReporteGeneral();
            reporte.Show();
        }

       

        private void Bienvenidos_Activated(object sender, EventArgs e)
        {
            bool entroLinea = InternetDisponible.IsConnectedToInternet(); 
            if (entroLinea == true) label3.BackColor = Color.Lime;
            else label3.BackColor = Color.Red;
        }

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfiguracionBill configurar = new ConfiguracionBill();
            configurar.Show();
        }

        private void bancosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CatBancos catalogobancos = new CatBancos();
            catalogobancos.Show();
        }

        private void conceptoDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CatConceptospago conceptos = new CatConceptospago();
            conceptos.Show();
        }

        private void vendedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CatVendedores vendedores = new CatVendedores();
            vendedores.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FacturarPedidoM facturapendiente = new FacturarPedidoM();
            facturapendiente.Show();
        }

        private void pagosDePedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistroPagos regpagos = new RegistroPagos();
            regpagos.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CTablas.CATALOGOS();
            MessageBox.Show("termino de actualizar", "Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void configurarFacturacionElectronicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FacElectronica configurarfac = new FacElectronica();
            configurarfac.Show();
        }

        private void importaciónDeProductosExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reciboToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Recibos nuevorecibo = new Recibos();
            nuevorecibo.Show();
        }

        private void comprobaciónDeTablasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CTablas.CATALOGOS();
            CTablas.Funciones();
            MessageBox.Show("Se ajustaron las tablas correctamente" ,"Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void importaciónDeProductosserviciosExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LeerArchivoExcel IMPORTAR = new LeerArchivoExcel();
            IMPORTAR.Show();
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            try
            {

            string DireccionFoto = "";
            DireccionFoto = ClaseFotos.AbrirExplorar(openFileDialog1);
            pictureBox2.Image = System.Drawing.Image.FromFile(DireccionFoto);
            ClaseFotos.GuardarFotoEmpresa(DireccionFoto, "0");
            pictureBox2.Image = ClaseFotos.ConsultarFotoEmpresa("0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void verificarValoresDeFabricaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CTablas.FabricaValores();
            CTablas.CreaRespaldo();
            MessageBox.Show("Se verificaron los valores precargados al sistema", "Valores Precargados", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void formaDePagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CatFormasdepago formapago = new CatFormasdepago();
            formapago.Show();
        }

        private void respaldoDeBaseBillLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conectorSql conecta = new conectorSql();
            conecta.Excute("exec backubill");
            MessageBox.Show("Se realizo el respaldo en C:\\RESPALDOS ", "Respaldos", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            valoresg.SOLICITUDTIMBRES = "SI";
            ConfiguracionBill solicitud = new ConfiguracionBill();
            solicitud.Show();
        }

        private void accesoAUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccesoUsuarios usuarios = new AccesoUsuarios();
            usuarios.Show();
        }

   
     
      
        private void reimpresionDeFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReimpresionFacturarPedido reimpresion = new ReimpresionFacturarPedido();
            reimpresion.Show();
        }

        private void reporteDeRecibosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteGeneralRecibo reporecibos = new ReporteGeneralRecibo();
            reporecibos.Show();
        }

        private void importarAdministrarProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportarExportarExistencias reporecibos = new ImportarExportarExistencias();
            reporecibos.ShowDialog();
        }

        private void importarExportarPreciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportarExportarPrecios reporecibos = new ImportarExportarPrecios();
            reporecibos.ShowDialog();
        }

        private void administrarExistenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void registroDeGastosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GastosEmpresa gastos = new GastosEmpresa();
            gastos.Show();
        }

        private void configurarCorreoElectronicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CorreoElectronico correo = new CorreoElectronico();
            correo.Show();
        }

        private void historiaClinicaOftamologiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
          

        }

        private void pacienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void estudioColposcopicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void dentalToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void controlPrenatalToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void historiaClinicaOftamologiaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HistorialClinica.HCOftalmologia HCO = new HistorialClinica.HCOftalmologia();
            HCO.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            HistorialClinica.Pacientes paciente = new HistorialClinica.Pacientes();
            paciente.Show();
        }

        private void estudioColposcopicoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            HistorialClinica.Dental dental = new HistorialClinica.Dental();
            dental.Show();
        }

        private void controlPrenatalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void registroDeCitasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistorialClinica.RegistroCitas rcitas = new HistorialClinica.RegistroCitas();
            rcitas.Show();
        }

        private void catalogoDeServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistorialClinica.Catservicios servicios = new HistorialClinica.Catservicios();
            servicios.Show();
        }

        private void catalogoDeDoctoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistorialClinica.CatDoctores doctores = new HistorialClinica.CatDoctores();
            doctores.Show();
        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HistorialClinica.RegistroCitas citas = new HistorialClinica.RegistroCitas();
            citas.Show();
        }

        private void notasDeEvoluciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistorialClinica.NotasEvolucion notas = new HistorialClinica.NotasEvolucion();
            notas.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            valoresg.CLAVEPAC = "";
            HistorialClinica.Pacientes npaciente = new HistorialClinica.Pacientes();
            npaciente.Show();
        }

        private void estudioColposcopicoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            HistorialClinica.EstudioColposcopico EC = new HistorialClinica.EstudioColposcopico();
            EC.Show();

        }

        private void controlPrenatalToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            HistorialClinica.ControlPrenatal CPrenatal = new HistorialClinica.ControlPrenatal();
            CPrenatal.Show();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            VerificarListadoCitas();
        }

        public void VerificarListadoCitas()
        {
            string miValor = Registro.ReadRegSHOPCONTROL("CON", "BADMAQUINASERVER");
            if (miValor == "1")
            {
                GenerarCitasDoctores();
            }
        }

        public void GenerarCitasDoctores()
        {
            SqlDataReader leer = null;
            conectorSql conecta = new conectorSql();
            string Query = "Select * from  doctores where cvdoctor<>''";
            leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                HoraEntradaDoc = "";
                HoraSalidaDoc = "";

                HoraEntradaComedor = "";
                HoraSalidaComedor = "";
                DCOMEDOR = "";
                tiempoCon = 0;
                Tminentrada = 0;
                Tminsalida = 0;

                tminEntraComedor = 0;
                tminSalComedor = 0;
                TiempoPred = 0;
                cvpaciente = "";
                nombrepaciente = "";
                Sel_cvdoctor = "";
                Sel_FechaCitare = "";
                
                string cvdoctor =leer["cvdoctor"].ToString();
                BuscarinfoDoctor(cvdoctor);
                for (int i = 0; i < 30; i++)
                {
                    DateTime FechaReg = DateTime.Now;
                    if (i>0) FechaReg = FechaReg.AddDays(i);
                    GenerarCitasdiarias(FechaReg, cvdoctor);
                }
            }
            conecta.CierraConexion();
        }

        public void GenerarCitasdiarias(DateTime fechagenera, string Gcvdoctor)
        {

            string Query = "Select * from citas where fechacod='" + fechagenera.ToString("yyyyMMdd") + "' and cvdoctor='" + Gcvdoctor + "' AND (ESTATUS='SIN PAGAR' OR ESTATUS='OCUPADO'  or ESTATUS='PAGADO' or ESTATUS='CANCELADO')";
            conectorSql conecta = new conectorSql();
            bool existecitas = conecta.ExisteRegistro(Query);
            conecta.CierraConexion();
            int contador = 1;
            if (existecitas == false)
            {

                Query = "DELETE from citas where fechacod='" + fechagenera.ToString("yyyyMMdd") + "' and cvdoctor='" + Gcvdoctor + "'";
                conecta.Excute(Query);
                conecta.CierraConexion();
                
                if (DCOMEDOR == "SI")
                {

                    decimal trecorre = Tminentrada;
                    while (trecorre <= (tminEntraComedor - tiempoCon))
                    {
                        decimal ConverHora = trecorre / 60;
                        decimal ConMinutos = trecorre % 60;
                        cvdoctor = Gcvdoctor;
                        string CadHora = ConverHora.ToString();
                        string cadena = CadHora;
                        CadHora = "";
                        for (int i = 0; i < cadena.Length; i++)
                        {
                            if (cadena.Substring(i, 1) == ".") break;
                            CadHora = CadHora + cadena.Substring(i, 1);
                        }


                        string Cadmin = ConMinutos.ToString();
                        if (CadHora.Length <= 1) CadHora = "0" + CadHora;
                        if (Cadmin.Length <= 1) Cadmin = "0" + Cadmin;

                        string ReHoraEntrada = CadHora + ":" + Cadmin;

                        DateTime ReFecha = fechagenera;

                        Query = "Insert into citas(cvdoctor";
                        Query = Query + ",numexpediente";
                        Query = Query + ",horainicia";
                        Query = Query + ",horatermina";
                        Query = Query + ",hmininicia";
                        Query = Query + ",hmintermina";
                        Query = Query + ",ttiempo";
                        Query = Query + ",estatus";
                        Query = Query + ",tipo";
                        Query = Query + ",fecha";
                        Query = Query + ",fechacod";
                        Query = Query + ",emite";
                        Query = Query + ",observa";
                        Query = Query + ",progresivo";
                        Query = Query + ",cvservicio)";

                        Query = Query + " values(";
                        Query = Query + "'" + cvdoctor + "'";
                        Query = Query + ",'0'";
                        Query = Query + ",'" + ReHoraEntrada + "'";
                        Query = Query + ",'00:00'";
                        Query = Query + ",'" + trecorre + "'";
                        Query = Query + ",'0'";
                        Query = Query + ",'" + tiempoCon + "'";
                        Query = Query + ",'LIBRE'";
                        Query = Query + ",'NO REGISTRADO'";
                        Query = Query + ",'" + ReFecha.ToString("dd/MM/yyyy") + "'";
                        Query = Query + ",'" + ReFecha.ToString("yyyyMMdd") + "'";
                        Query = Query + ",'" + valoresg.USUARIOSIS + "'";
                        Query = Query + ",''";
                        Query = Query + ",'" + contador.ToString() + "'";
                        Query = Query + ",'0')";
                        conecta.Excute(Query);
                        conecta.CierraConexion();
                        contador++;
                        trecorre = trecorre + tiempoCon;
                    }


                    trecorre = tminSalComedor;
                    while (trecorre <= Tminsalida)
                    {
                        decimal ConverHora = trecorre / 60;
                        decimal ConMinutos = trecorre % 60;
                        cvdoctor = Gcvdoctor;
                        string CadHora = ConverHora.ToString();
                        string cadena = CadHora;
                        CadHora = "";
                        for (int i = 0; i < cadena.Length; i++)
                        {
                            if (cadena.Substring(i, 1) == ".") break;
                            CadHora = CadHora + cadena.Substring(i, 1);
                        }


                        string Cadmin = ConMinutos.ToString();
                        if (CadHora.Length <= 1) CadHora = "0" + CadHora;
                        if (Cadmin.Length <= 1) Cadmin = "0" + Cadmin;

                        string ReHoraEntrada = CadHora + ":" + Cadmin;

                        DateTime ReFecha = fechagenera;

                        Query = "Insert into citas(cvdoctor";
                        Query = Query + ",numexpediente";
                        Query = Query + ",horainicia";
                        Query = Query + ",horatermina";
                        Query = Query + ",hmininicia";
                        Query = Query + ",hmintermina";
                        Query = Query + ",ttiempo";
                        Query = Query + ",estatus";
                        Query = Query + ",tipo";
                        Query = Query + ",fecha";
                        Query = Query + ",fechacod";
                        Query = Query + ",emite";
                        Query = Query + ",observa";
                        Query = Query + ",progresivo";
                        Query = Query + ",cvservicio)";

                        Query = Query + " values(";
                        Query = Query + "'" + cvdoctor + "'";
                        Query = Query + ",'0'";
                        Query = Query + ",'" + ReHoraEntrada + "'";
                        Query = Query + ",'00:00'";
                        Query = Query + ",'" + trecorre + "'";
                        Query = Query + ",'0'";
                        Query = Query + ",'" + tiempoCon + "'";
                        Query = Query + ",'LIBRE'";
                        Query = Query + ",'NO REGISTRADO'";
                        Query = Query + ",'" + ReFecha.ToString("dd/MM/yyyy") + "'";
                        Query = Query + ",'" + ReFecha.ToString("yyyyMMdd") + "'";
                        Query = Query + ",'" + valoresg.USUARIOSIS + "'";
                        Query = Query + ",''";
                        Query = Query + ",'" + contador.ToString() + "'";
                        Query = Query + ",'0')";
                        conecta.Excute(Query);
                        conecta.CierraConexion();
                        contador++;
                        trecorre = trecorre + tiempoCon;
                    }
                }
                else // cuando no tiene comedor
                {
                    decimal trecorre = Tminentrada;
                    while (trecorre <= (Tminsalida - tiempoCon))
                    {
                        decimal ConverHora = trecorre / 60;
                        decimal ConMinutos = trecorre % 60;
                        cvdoctor = Gcvdoctor;
                        string CadHora = ConverHora.ToString();
                        string cadena = CadHora;
                        CadHora = "";
                        for (int i = 0; i < cadena.Length; i++)
                        {
                            if (cadena.Substring(i, 1) == ".") break;
                            CadHora = CadHora + cadena.Substring(i, 1);
                        }


                        string Cadmin = ConMinutos.ToString();
                        if (CadHora.Length <= 1) CadHora = "0" + CadHora;
                        if (Cadmin.Length <= 1) Cadmin = "0" + Cadmin;

                        string ReHoraEntrada = CadHora + ":" + Cadmin;

                        DateTime ReFecha = fechagenera;

                        Query = "Insert into citas(cvdoctor";
                        Query = Query + ",numexpediente";
                        Query = Query + ",horainicia";
                        Query = Query + ",horatermina";
                        Query = Query + ",hmininicia";
                        Query = Query + ",hmintermina";
                        Query = Query + ",ttiempo";
                        Query = Query + ",estatus";
                        Query = Query + ",tipo";
                        Query = Query + ",fecha";
                        Query = Query + ",fechacod";
                        Query = Query + ",emite";
                        Query = Query + ",observa";
                        Query = Query + ",progresivo";
                        Query = Query + ",cvservicio)";

                        Query = Query + " values(";
                        Query = Query + "'" + cvdoctor + "'";
                        Query = Query + ",'0'";
                        Query = Query + ",'" + ReHoraEntrada + "'";
                        Query = Query + ",'00:00'";
                        Query = Query + ",'" + trecorre + "'";
                        Query = Query + ",'0'";
                        Query = Query + ",'" + tiempoCon + "'";
                        Query = Query + ",'LIBRE'";
                        Query = Query + ",'NO REGISTRADO'";
                        Query = Query + ",'" + ReFecha.ToString("dd/MM/yyyy") + "'";
                        Query = Query + ",'" + ReFecha.ToString("yyyyMMdd") + "'";
                        Query = Query + ",'" + valoresg.USUARIOSIS + "'";
                        Query = Query + ",''";
                        Query = Query + ",'" + contador.ToString() + "'";
                        Query = Query + ",'0')";
                        conecta.Excute(Query);
                        conecta.CierraConexion();
                        contador++;
                        trecorre = trecorre + tiempoCon;
                    }


                }

            }


            //contador--;
            //Query = "Update consecutivos set turnos='" + contador.ToString() + "'";
            //conecta.Excute(Query);
            //conecta.CierraConexion();

        }


        public void BuscarinfoDoctor(string Gcvdoctor)
        {
            conectorSql conecta = new conectorSql();
            string query = "Select * from doctores where cvdoctor='" +  Gcvdoctor+ "'";
            SqlDataReader leer = conecta.RecordInfo(query);
            while (leer.Read())
            {
                HoraEntradaDoc = leer["HoraEntrada"].ToString();
                HoraSalidaDoc = leer["HoraSalida"].ToString();

                HoraEntradaComedor = leer["HoraEComedor"].ToString();
                HoraSalidaComedor = leer["HoraSComedor"].ToString();
                tiempoCon = int.Parse(leer["TiempoConsulta"].ToString());
                TiempoPred = tiempoCon;
                DCOMEDOR = leer["dcomedor"].ToString();

            }
            conecta.CierraConexion();

            DateTime tentrada = DateTime.Parse(HoraEntradaDoc);
            Tminentrada = (tentrada.Hour * 60) + tentrada.Minute;

            DateTime tsalida = DateTime.Parse(HoraSalidaDoc);
            Tminsalida = (tsalida.Hour * 60) + tsalida.Minute;

            DateTime tentradacomedor = DateTime.Parse(HoraEntradaComedor);
            tminEntraComedor = (tentradacomedor.Hour * 60) + tentradacomedor.Minute;

            DateTime tsalidaComedor = DateTime.Parse(HoraSalidaComedor);
            tminSalComedor = (tsalidaComedor.Hour * 60) + tsalidaComedor.Minute;

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HistorialClinica.NotasEvolucion notas = new HistorialClinica.NotasEvolucion();
            notas.Show();
        }

        private void clienteBloqueadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClienteBloqueado.ClienteBloqueadoC blo = new ClienteBloqueado.ClienteBloqueadoC();
            blo.Show();
        }

        private void tIPOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TIPOS IR = new TIPOS();
            IR.Show();
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void inventarioToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            JOSEFORMS.INVENTARIO inv = new JOSEFORMS.INVENTARIO();
            inv.Show();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void pendientesDePagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotificacionClientesSMS pagos = new NotificacionClientesSMS();
            pagos.Show();

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Analisys.IngresosPorArea ingresos = new Analisys.IngresosPorArea();
            ingresos.Show();
        }

        private void listadoDePacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListadoPacientes listado = new ListadoPacientes();
            listado.Show();
        }

        private void linkLabel6_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CapturaInventarios inventarios = new CapturaInventarios();
            inventarios.Show();

        }
    }
}
