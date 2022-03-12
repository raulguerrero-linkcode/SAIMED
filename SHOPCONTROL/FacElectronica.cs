using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SHOPCONTROL
{
    public partial class FacElectronica : Form
    {
        public string CALLE;
        public string COLONIA;
        public string NUMINT;
        public string NUMEXT;
        public string ESTADO;
        public string LOCALIDAD;
        public string PAIS;
        public string CODPOSTAL;
        public string MUNICIPIO;
        public string IDUNICO;
        public string CORREO;
        public string CONTRASEÑA;

        public FacElectronica()
        {
            InitializeComponent();
        }

        string usuario="";
        string contrafac="";
        string nombre="";
        string fechainicia="";
        string RFC="";
        string IDSerial="";
        string LugarExpedicion="";
        string Regimen="";
        string DirCarpeta = "";
        string PRODUCTIVO = "";
        public string nombrecomercial = "";
        string infoadiciona = "";
        string numcopias = "";
        private void button4_Click(object sender, EventArgs e)
        {
            Recolectar();
            if (ExisteInfo() == false)
            {
                GuardarFacturacion();
                GuardarClienteCero();
                MessageBox.Show("Se guardo correctamente la información de facturación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ActualizarFacturacion();
                GuardarClienteCero();
                MessageBox.Show("Se actualizo correctamente la información de facturación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        public void GuardarClienteCero()
        {

            string ESTADO="";
            string MUNICIPIO = "";
            string CALLE = "";
            string COLONIA = "";
            string NUMEXT = "";
            string PAIS = "";
            string FORMADEPAGO = "PAGO EN UNA SOLA EXHIBICIÓN";
            string VENDEDOR = "NO APLICA";
            string TIPOPAGO = "Contado";
            string CVBANCO= "NO APLICA";
            string NUMCUENTA = "0000";
            string RFCGEN = "XAXX010101000";
            string METODODEPAGO = "NO IDENTIFICADO";
            conectorSql conecta = new conectorSql();
            string Query = "";
            Query = "Delete from clientes where cvcliente='0'";
            conecta.Excute(Query);

            Query = "Select * from ParametrosFactura where nombre<>''";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                ESTADO = leer["estado"].ToString();
                CALLE = leer["calle"].ToString();
                COLONIA = leer["colonia"].ToString();
                MUNICIPIO = leer["municipio"].ToString();
                PAIS = leer["pais"].ToString();
                CODPOSTAL= leer["codpostal"].ToString();
                NUMEXT = leer["numext"].ToString();
            }
            conecta.CierraConexion();

            string direccion="CALLE : "  + CALLE + " Num. " + NUMEXT + ", " + COLONIA + "," + MUNICIPIO + "," + ESTADO;

            Query="Insert into clientes (cvcliente";
            Query=Query + ",nombre";
            Query=Query +",telefono";
            Query=Query +",email";
            Query=Query +",email2";
            Query=Query +",celular";
            Query=Query +",direccion";
            Query=Query +",rfc";
            Query=Query +",direfiscal";
            Query=Query +",empresa";
            Query=Query +",factura";
            Query=Query +",activo";
            Query=Query +",calleE";
            Query=Query +",ColoniaE";
            Query=Query +",MunicipioE";
            Query=Query +",EstadoE";
            Query=Query +",CodE";
            Query=Query +",PaisE";
            Query=Query +",CalleF";
            Query=Query +",ColoniaF";
            Query=Query +",MunicipioF";
            Query=Query +",EstadoF";
            Query=Query +",CodF";
            Query=Query +",PaisF";
            Query=Query +",fechamod";
            Query=Query +",fcodmod";
            Query=Query +",sincronizado";
            Query=Query +",actividad";
            Query=Query +",numF";
            Query=Query +",observafact";
            Query=Query +",numcuenta";
            Query=Query +",cvbanco";
            Query=Query +",metodopago";
            Query=Query +",vendedor";
            Query=Query +",formapago";
            Query=Query +",tipopago";
            Query=Query +",diascredito";
            Query = Query + ",nombrefactura)";
            Query = Query + " values(";
            
            Query = Query + "'0'";
            Query = Query + ",'PUBLICO EN GENERAL'";
            Query = Query + ",'S/N'";
            Query = Query + ",'" + CORREO+"'";
            Query = Query + ",'" + CORREO + "'";
            Query = Query + ",'0'";
            Query = Query + ",'" + direccion + "'";
            Query = Query + ",'" + RFCGEN+ "'";
            Query = Query + ",'" + direccion + "'";
            Query = Query + ",'PUBLICO EN GENERAL'";
            Query = Query + ",'SI'";
            Query = Query + ",'1'";
            Query = Query + ",'" + CALLE + "'";
            Query = Query + ",'" + COLONIA+ "'";
            Query = Query + ",'" + MUNICIPIO + "'";
            Query = Query + ",'" + ESTADO+ "'";
            Query = Query + ",'" + CODPOSTAL + "'";
            Query = Query + ",'" + PAIS + "'";
            Query = Query + ",'" + CALLE + "'";
            Query = Query + ",'" + COLONIA + "'";
            Query = Query + ",'" + MUNICIPIO + "'";
            Query = Query + ",'" + ESTADO + "'";
            Query = Query + ",'" + CODPOSTAL + "'";
            Query = Query + ",'" + PAIS + "'"; Query = Query + ",'" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
            Query = Query + ",'" + DateTime.Now.ToString("yyyyMMdd") + "'";
            Query = Query + ",'0'";
            Query = Query + ",'FISICA'";
            Query = Query + ",'" + NUMEXT+ "'";
            Query = Query + ",''";
            Query = Query + ",'" + NUMCUENTA+ "'";
            Query = Query + ",'" + CVBANCO + "'";
            Query = Query + ",'" + METODODEPAGO+ "'";
            Query = Query + ",'" + VENDEDOR+ "'";
            Query = Query + ",'" + FORMADEPAGO + "'";
            Query = Query + ",'" + TIPOPAGO + "'";
            Query = Query + ",'0'";
            Query = Query + ",'')";
            
            conecta.Excute(Query);

        }

        public bool ExisteInfo()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Select * from parametrosFactura where nombre<>''";
            return conecta.ExisteRegistro(Query);
        }

        public void GuardarFacturacion()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Insert into parametrosFactura(usuario,contrafac,nombre,RFC,IDSerial,LugarExpedicion,Regimen,DirCarpeta";
            Query = Query + ",calle,numext,numint,colonia,municipio,localidad,estado,pais,codpostal,correo,productivo,nombrecomercial,infoadicional,numcopias";
            Query = Query + ") values(";           
            Query = Query + "'"  + usuario +  "'";
            Query = Query + ",'" + contrafac + "'";
            Query = Query + ",'" + nombre + "'";
            Query = Query + ",'" + RFC + "'";
            Query = Query + ",'" + IDSerial + "'";
            Query = Query + ",'" + LugarExpedicion + "'";
            Query = Query + ",'" + Regimen + "'";
            Query = Query + ",'" + DirCarpeta + "'";

            Query = Query + ",'" + CALLE + "'";
            Query = Query + ",'" + NUMEXT+ "'";
            Query = Query + ",'" + NUMINT+ "'";
            Query = Query + ",'" + COLONIA+ "'";
            Query = Query + ",'" + MUNICIPIO+ "'";
            Query = Query + ",'" + LOCALIDAD+ "'";
            Query = Query + ",'" + ESTADO+ "'";
            Query = Query + ",'" + PAIS+ "'";
            Query = Query + ",'" + CODPOSTAL+ "'";
            Query = Query + ",'" + CORREO + "'";
            Query = Query + ",'" + PRODUCTIVO + "'";
            Query = Query + ",'" + nombrecomercial + "'";
            Query = Query + ",'" + infoadiciona + "'";
            Query = Query + ",'" + numcopias + "')";

            conecta.Excute(Query);

        }

        public void ActualizarFacturacion()
        {
            conectorSql conecta = new conectorSql();
            string Query = "Update parametrosFactura set ";
            Query = Query + " usuario='" + usuario + "'";
            Query = Query + ", contrafac='" + contrafac + "'";
            Query = Query + ", nombre='" + nombre + "'";
            Query = Query + ", RFC='" + RFC + "'";
            Query = Query + ", IDSerial='" + IDSerial + "'";
            Query = Query + ", LugarExpedicion='" + LugarExpedicion + "'";
            Query = Query + ", Regimen='" + Regimen + "'";
            Query = Query + ", DirCarpeta='" + DirCarpeta + "'";
            Query = Query + ", correo='" + CORREO+ "'";
            Query = Query + ", productivo='" + PRODUCTIVO+ "'";

            Query = Query + ", calle='" + CALLE + "'";
            Query = Query + ", numext='" + NUMEXT + "'";
            Query = Query + ", numint='" + NUMINT + "'";
            Query = Query + ", colonia='" + COLONIA + "'";
            Query = Query + ", municipio='" + MUNICIPIO+ "'";
            Query = Query + ", localidad='" + LOCALIDAD + "'";
            Query = Query + ", estado='" + ESTADO+ "'";
            Query = Query + ", pais='" + PAIS + "'";
            Query = Query + ", codpostal='" + CODPOSTAL + "'";
            Query = Query + ", nombrecomercial='" + nombrecomercial+ "'";
            Query = Query + ", numcopias='" + numcopias + "'";
            Query = Query + ", infoadicional='" + infoadiciona + "'";
            
            conecta.Excute(Query);

        }


        public void Recolectar()
        {
            nombre = textBox1.Text;
            RFC=textBox2.Text;
            Regimen=textBox3.Text;
            LugarExpedicion=textBox4.Text;
            usuario="mZqX99s0lqQT32sZXqfJWg==";
            contrafac=textBox6.Text ;
            DirCarpeta=textBox7.Text;
            nombrecomercial = textBox18.Text.Trim();

            CORREO = textBox17.Text;
            IDSerial = textBox5.Text;

            if (radioButton1.Checked == true) PRODUCTIVO = "SI";
            else PRODUCTIVO = "NO";

            if (PRODUCTIVO == "NO") usuario = "mvpNUXmQfK8=";

            CALLE = textBox8.Text;
            NUMEXT = textBox9.Text;
            NUMINT = textBox10.Text;
            COLONIA= textBox11.Text;
            MUNICIPIO = textBox12.Text;
            LOCALIDAD = textBox13.Text;
            ESTADO = textBox14.Text;
            PAIS = textBox15.Text;
            CODPOSTAL= textBox16.Text;
            numcopias = textBox19.Text;
            infoadiciona=textBox20.Text;
        }

        private void FacElectronica_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;

            conectorSql conecta = new conectorSql();
            string Query = "Select * from ParametrosFactura ";
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                usuario = leer["usuario"].ToString();
                contrafac = leer["contrafac"].ToString();
                nombre = leer["nombre"].ToString();
                fechainicia = leer["fechainicia"].ToString();
                RFC = leer["RFC"].ToString();
                IDSerial = leer["IDSerial"].ToString();
                LugarExpedicion = leer["LugarExpedicion"].ToString();
                Regimen = leer["Regimen"].ToString();
                DirCarpeta = leer["DirCarpeta"].ToString();

                CALLE= leer["calle"].ToString();
                NUMEXT= leer["numext"].ToString();
                NUMINT= leer["numint"].ToString();
                COLONIA= leer["colonia"].ToString();
                MUNICIPIO= leer["municipio"].ToString();
                LOCALIDAD= leer["localidad"].ToString();
                ESTADO= leer["estado"].ToString();
                PAIS= leer["pais"].ToString();
                CODPOSTAL= leer["codpostal"].ToString();
                CORREO = leer["correo"].ToString();
                numcopias = leer["numcopias"].ToString();
                nombrecomercial = leer["nombrecomercial"].ToString();
                infoadiciona = leer["infoadicional"].ToString();
                string valor = leer["productivo"].ToString();
                if (valor == "SI") radioButton1.Checked = true;
                else radioButton2.Checked = true;
            }
            conecta.CierraConexion();

            textBox1.Text = nombre;
            textBox2.Text = RFC;
            textBox3.Text = Regimen;
            textBox4.Text = LugarExpedicion;
            textBox5.Text = IDSerial;
            textBox6.Text = contrafac;
            textBox7.Text = DirCarpeta;
            textBox8.Text = CALLE;
            textBox9.Text = NUMEXT;
            textBox10.Text = NUMINT;
            textBox11.Text = COLONIA;
            textBox12.Text = MUNICIPIO;
            textBox13.Text = LOCALIDAD;
            textBox14.Text = ESTADO;
            textBox15.Text = PAIS;
            textBox16.Text = CODPOSTAL;
            textBox17.Text = CORREO;
            textBox18.Text = nombrecomercial;
            textBox19.Text = numcopias;
            textBox20.Text = infoadiciona;
            // Licenciamiento preidfactura=new Licenciamiento();
            // if (IDSerial == "") textBox5.Text=preidfactura.PreLicenciaBillLine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionaFolder();
        }

        public void SeleccionaFolder()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox7.Text = folderBrowserDialog1.SelectedPath;
            }
            else
            {
                textBox7.Text = "C:\\RESFACTURAS";
            }
        }
    }
}
