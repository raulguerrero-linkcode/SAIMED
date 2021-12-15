using System;
using System.Drawing; 
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Imaging;

/// <summary>
/// Descripción breve de ClaseFotos
/// </summary>
/// 
public class ClaseFotos
{
	public ClaseFotos()
	{
	
	}

    public static  string AbrirExplorar(OpenFileDialog OpenFiledialog)
    {
        string DireccionR = "";
        OpenFiledialog.Filter = "Image Files|*.jpg;*.gif;*.bmp;*.png;*.jpeg|All Files|*.*";
        OpenFiledialog.InitialDirectory = @"C:\";
        OpenFiledialog.FilterIndex = 1;

        OpenFiledialog.Title = "Seleccione la Imagen";
        if (OpenFiledialog.ShowDialog() == DialogResult.OK)
        {
            DireccionR = OpenFiledialog.FileName.ToString();
            //Imagen.Image = System.Drawing.Image.FromFile(OpenFiledialog.FileName);
        }
        return DireccionR;
    }

    public static Image ConsultarImagenExpediente(string Expediente, string Control, int _consecutivo)
    {
        Image Resultado = null;
        byte[] mbytes;
        string Query = "";
        bool existereg;
        conectorSql mycon = new conectorSql();

        Query = "Select noExpediente, imagenEdit as Photo from imagenesclinica where noExpediente='" + Expediente + "' and nombre='" + Control + "'";
        Query += "and consecutivo=" + _consecutivo + "";
        existereg = mycon.ExisteRegistro(Query);
        if (existereg == false) return Resultado;

        SqlDataReader miRegistro = mycon.RecordInfo(Query);

        while (miRegistro.Read())
        {
            if (miRegistro["Photo"] != DBNull.Value)
            {
                mbytes = (byte[])miRegistro["Photo"];
                Resultado = Bytes2Image(mbytes);
            }

        }
        mycon.CierraConexion();
        return Resultado;
    }

    public static  bool GuardarFoto(string DireccionFoto, string Contrato)
    {

        if (DireccionFoto != null && DireccionFoto != "Label" && DireccionFoto != "")
        {
            string Direccion = DireccionFoto;
            string Query = "";
            bool existereg;
            System.Drawing.Image i = System.Drawing.Image.FromFile(Direccion);
            MemoryStream m = new MemoryStream();

            i.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imagenDatos = m.ToArray();
            m.Close();

            string numControl = Contrato;

            conectorSql mycon = new conectorSql();

            // primero elimina la foto anterior si tiene
            Query = "Delete from Fotos where cvempleado='" + Contrato + "'";
            existereg = mycon.Excute(Query);

            string sql = "insert into Fotos(cvempleado, Foto)";
            sql += " Values(@NFolio, @Imagen)";

            mycon.Abrirconexion();
            SqlCommand SqlCom = new SqlCommand(sql, mycon.con);

            SqlCom.Parameters.Add("@NFolio", System.Data.SqlDbType.NVarChar, 20);
            SqlCom.Parameters["@NFolio"].Value = Contrato;
            SqlCom.Parameters.Add("@Imagen", System.Data.SqlDbType.Image);
            SqlCom.Parameters["@Imagen"].Value = imagenDatos;

            SqlCom.ExecuteNonQuery();
            mycon.CierraConexion();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool GuardarFotoEmpresa(string DireccionFoto, string Cvempresa)
    {

        if (DireccionFoto != null && DireccionFoto != "Label" && DireccionFoto!="")
        {
            string Direccion = DireccionFoto;
            string Query = "";
            bool existereg;
            System.Drawing.Image i = System.Drawing.Image.FromFile(Direccion);
            MemoryStream m = new MemoryStream();

            i.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imagenDatos = m.ToArray();
            m.Close();

            string numControl = Cvempresa;

            conectorSql mycon = new conectorSql();

            // primero elimina la foto anterior si tiene
            Query = "Delete from LogoEmpresa where cvempresa='" + Cvempresa + "'";
            existereg = mycon.Excute(Query);

            string sql = "insert into LogoEmpresa(cvempresa, foto)";
            sql += " Values(@NFolio, @Imagen)";

            mycon.Abrirconexion();
            SqlCommand SqlCom = new SqlCommand(sql, mycon.con);

            SqlCom.Parameters.Add("@NFolio", System.Data.SqlDbType.NVarChar, 20);
            SqlCom.Parameters["@NFolio"].Value = Cvempresa;
            SqlCom.Parameters.Add("@Imagen", System.Data.SqlDbType.Image);
            SqlCom.Parameters["@Imagen"].Value = imagenDatos;

            SqlCom.ExecuteNonQuery();
            mycon.CierraConexion();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Image ConsultarFoto(string Contrato)
    {
        Image Resultado = null;
        string Query = "";
        bool existereg;
        conectorSql mycon = new conectorSql();
        byte[] mbytes;
        Query = "Select cvempleado, foto as Photo from Fotos where cvempleado='" + Contrato + "'";
        existereg = mycon.ExisteRegistro(Query);
        if (existereg == false) return Resultado;
        mycon.CierraConexion();

        SqlDataReader miRegistro = mycon.RecordInfo(Query);

        while (miRegistro.Read())
        {
            mbytes = null;
            Contrato = miRegistro["cvempleado"].ToString();

            if (miRegistro["Photo"] != DBNull.Value)
            {
                mbytes = (byte[])miRegistro["Photo"];
                Resultado=Bytes2Image(mbytes);
            }

        }
        mycon.CierraConexion();
        return Resultado;
    }

    public static Image ConsultarFotoEmpresa(string Contrato)
    {
        Image Resultado = null;
        byte[] mbytes;
        string Query = "";
        bool existereg;
        conectorSql mycon = new conectorSql();

        Query = "Select cvempresa, foto as Photo from Logoempresa where cvempresa='" + Contrato + "'";
        existereg = mycon.ExisteRegistro(Query);
        if (existereg == false) return Resultado;

        SqlDataReader miRegistro = mycon.RecordInfo(Query);

        while (miRegistro.Read())
        {
            Contrato = miRegistro["cvempresa"].ToString();

            if (miRegistro["Photo"] != DBNull.Value)
            {
                mbytes = (byte[])miRegistro["Photo"];
                Resultado = Bytes2Image(mbytes);

            }

        }
        mycon.CierraConexion();
        return Resultado;
    }


    public static Image ConsultarFotoExpediente(string Expediente, string Control)
    {
        Image Resultado = null;
        byte[] mbytes;
        string Query = "";
        bool existereg;
        conectorSql mycon = new conectorSql();

        Query = "Select noExpediente, imagenEdit as Photo from imagenesclinica where noExpediente='" + Expediente + "' and nombre='" + Control+"'";
        existereg = mycon.ExisteRegistro(Query);
        if (existereg == false) return Resultado;

        SqlDataReader miRegistro = mycon.RecordInfo(Query);

        while (miRegistro.Read())
        {
            if (miRegistro["Photo"] != DBNull.Value)
            {
                mbytes = (byte[])miRegistro["Photo"];
                Resultado = Bytes2Image(mbytes);
            }

        }
        mycon.CierraConexion();
        return Resultado;
    }

    public static Image Bytes2Image(byte[] bytes)
    {
        Image imagenNueva = null;
        MemoryStream ms;
        Bitmap nimagen = null;
        try
        {
            if (bytes == null) return imagenNueva;
        {

            //string tempFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "images\\temp.Jpeg";

            ms = new MemoryStream(bytes);
            imagenNueva = Image.FromStream(ms);
            //nimagen = new Bitmap(imagenNueva);
            //nimagen.Save(tempFile, imagenNueva.RawFormat);
            //nimagen.Dispose();



            //nimagen.Save(tempFile, imagenNueva.RawFormat);
            //nimagen.Dispose();
            //imagenNueva.Dispose();
            //imagenNueva = null;
            ms.Flush();
            ms.Close();
            ms.Dispose();
        }
        return imagenNueva;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return imagenNueva;
        }
    }


    
    public static void BoorrarImagenesTemp()
    {
        string tempFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "images\\temp.Jpeg";
        File.Delete(tempFile);

        tempFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "images\\tempfirm.Jpeg";
        File.Delete(tempFile);
    }

    public static Image Bytes2ImageFirma(byte[] bytes)
    {
        Image imagenNueva = null;
        MemoryStream ms;
        try
        {
            if (bytes == null) return null;
            {
                string tempFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "images\\tempfirm.Jpeg";
                File.Delete(tempFile);

                ms = new MemoryStream(bytes);
                imagenNueva = Image.FromStream(ms);
                imagenNueva.Save(tempFile);
                imagenNueva.Dispose();
                ms.Close();
                ms.Dispose();
            }
            return imagenNueva;
        }
        catch (Exception e)
        {
            return imagenNueva;
        }
    }

    public static void CargaFoto(PictureBox control)
    {
        string tempFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "images\\temp.Jpeg";
        control.Image = System.Drawing.Image.FromFile(tempFile);
        
    }

    public static void CargaFirma(PictureBox control)
    {
        string TempFile = AppDomain.CurrentDomain.BaseDirectory.ToString() + "images\\tempfirm.Jpeg";
        control.Image = System.Drawing.Image.FromFile(TempFile);
    }
    public static bool GuardarFotoPaciente(string DireccionFoto, string numPaciente)
    {
        if (DireccionFoto != null && DireccionFoto != "Label" && DireccionFoto != "")
        {
            string Direccion = DireccionFoto;
            string Query = "";
            bool existereg;
            System.Drawing.Image i = System.Drawing.Image.FromFile(Direccion);
            MemoryStream m = new MemoryStream();

            i.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imagenDatos = m.ToArray();
            m.Close();

            conectorSql mycon = new conectorSql();

            Query = "Delete from FotosPacientes where NoExpediente='" + numPaciente + "'";
            existereg = mycon.Excute(Query);

            string sql = "INSERT into FotosPacientes(NoExpediente, Foto)";
            sql += " Values(@NumExpediente, @Imagen)";

            mycon.Abrirconexion();
            SqlCommand SqlCom = new SqlCommand(sql, mycon.con);

            SqlCom.Parameters.Add("@NumExpediente", System.Data.SqlDbType.NVarChar, 20);
            SqlCom.Parameters["@NumExpediente"].Value = numPaciente;
            SqlCom.Parameters.Add("@Imagen", System.Data.SqlDbType.Image);
            SqlCom.Parameters["@Imagen"].Value = imagenDatos;

            SqlCom.ExecuteNonQuery();
            mycon.CierraConexion();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool GuardarFotoPantalla(string DireccionFoto, string clave)
    {
        if (DireccionFoto != null && DireccionFoto != "Label" && DireccionFoto != "")
        {
            string Direccion = DireccionFoto;
            string Query = "";
            bool existereg;
            System.Drawing.Image i = System.Drawing.Image.FromFile(Direccion);
            MemoryStream m = new MemoryStream();

            i.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imagenDatos = m.ToArray();
            m.Close();

            conectorSql mycon = new conectorSql();

            Query = "Delete from imgpantalla where clave='" + clave + "'";
            existereg = mycon.Excute(Query);

            string sql = "INSERT into imgpantalla (clave, Foto)";
            sql += " Values(@clave, @Imagen)";

            mycon.Abrirconexion();
            SqlCommand SqlCom = new SqlCommand(sql, mycon.con);

            SqlCom.Parameters.Add("@clave", System.Data.SqlDbType.NVarChar, 20);
            SqlCom.Parameters["@clave"].Value = clave;
            SqlCom.Parameters.Add("@Imagen", System.Data.SqlDbType.Image);
            SqlCom.Parameters["@Imagen"].Value = imagenDatos;

            SqlCom.ExecuteNonQuery();
            mycon.CierraConexion();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Image ConsultarFotoPaciente(string NumExpediente)
    {
        Image Resultado = null;
        string Query = "";
        bool existereg;
        conectorSql mycon = new conectorSql();
        byte[] mbytes;
        Query = "Select NoExpediente, Foto as Photo from FotosPacientes where NoExpediente='" + NumExpediente + "'";
        existereg = mycon.ExisteRegistro(Query);
        if (existereg == false) return Resultado;
        mycon.CierraConexion();

        SqlDataReader miRegistro = mycon.RecordInfo(Query);

        while (miRegistro.Read())
        {
            mbytes = null;
            NumExpediente = miRegistro["NoExpediente"].ToString();

            if (miRegistro["Photo"] != DBNull.Value)
            {
                mbytes = (byte[])miRegistro["Photo"];
                Resultado = Bytes2Image(mbytes);
            }

        }
        mycon.CierraConexion();
        return Resultado;
    }
}
