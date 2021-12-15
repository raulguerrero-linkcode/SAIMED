using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Imaging;


   public class ClaseArchivos
    {
       public static string Nombre_Archivo = "";
       public static string Ext_Archivo = "";
       public static bool GuardarArchivo(string Direccion, string clave, string cvpaciente)
       {

           if (Direccion != null && Direccion != "Label" && Direccion != "")
           {
             
               string Query = "";
               bool existereg;

               System.Drawing.Image i = System.Drawing.Image.FromFile(Direccion);
               MemoryStream m = new MemoryStream();

               i.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
               byte[] imagenDatos = m.ToArray();
               m.Close();

               string clavecontrol = clave;
               conectorSql mycon = new conectorSql();

               Query = "Delete from archivos where clave='" + clave + "' and cvpaciente='" +  cvpaciente + "'";
               existereg = mycon.Excute(Query);
               string sql = "insert into archivos(clave, cvpaciente, archivoM)";
               sql += " Values(@NFolio,@cvpaciente, @Imagen)";

               mycon.Abrirconexion();
               SqlCommand SqlCom = new SqlCommand(sql, mycon.con);

               SqlCom.Parameters.Add("@NFolio", System.Data.SqlDbType.Int, 20);
               SqlCom.Parameters["@NFolio"].Value = clave;
               SqlCom.Parameters.Add("@cvpaciente", System.Data.SqlDbType.Int, 20);
               SqlCom.Parameters["@cvpaciente"].Value = cvpaciente;
               SqlCom.Parameters.Add("@Imagen", System.Data.SqlDbType.Binary);
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

       public static bool GuardarArchivoBytes(string Direccion, string clave, string cvpaciente)
       {
           if (Direccion != null && Direccion != "Label" && Direccion != "")
           {

               byte[] picArray = System.IO.File.ReadAllBytes(Direccion);
               conectorSql mycon = new conectorSql();

               string Query = "Delete from archivos where clave='" + clave + "' and cvpaciente='" + cvpaciente + "'";
               bool existereg = mycon.Excute(Query);
               string sql = "insert into archivos(clave, cvpaciente, archivoM)";
               sql += " Values(@NFolio,@cvpaciente, @Imagen)";

               mycon.Abrirconexion();
               SqlCommand SqlCom = new SqlCommand(sql, mycon.con);

               SqlCom.Parameters.Add("@NFolio", System.Data.SqlDbType.Int, 20);
               SqlCom.Parameters["@NFolio"].Value = clave;
               SqlCom.Parameters.Add("@cvpaciente", System.Data.SqlDbType.Int, 20);
               SqlCom.Parameters["@cvpaciente"].Value = cvpaciente;
               SqlCom.Parameters.Add("@Imagen", System.Data.SqlDbType.Binary);
               SqlCom.Parameters["@Imagen"].Value = picArray;

               SqlCom.ExecuteNonQuery();
               mycon.CierraConexion();

               return true;
           }
           else
           {
               return false;
           }
       }

       public static string EscribirArchivoBytes(string clave, string cvpaciente, string extension)
       {
           byte[] myArrayOfBytes = null;
           string CadenaGral = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ArchivosPac\\";
           conectorSql conecta= new conectorSql();
           string Query = "Select * from  archivos where clave='" + clave + "' and cvpaciente='" + cvpaciente + "'";
           SqlDataReader leer = conecta.RecordInfo(Query);
           while (leer.Read())
           {
               if (leer["archivoM"] != DBNull.Value)
               {
                   myArrayOfBytes = (byte[])leer["archivoM"];
               }
           }
           conecta.CierraConexion();
           CadenaGral = CadenaGral + "Archivotemp." + extension;
           File.WriteAllBytes(CadenaGral, myArrayOfBytes);
           return CadenaGral;
       }

       public static string AbrirExplorar(OpenFileDialog OpenFiledialog)
       {
           string DireccionR = "";
           OpenFiledialog.Filter = "Image Files|*.pdf;*.doc;*.docx;*.xls;*.xlsx|All Files|*.*";
           OpenFiledialog.InitialDirectory = @"C:\";
           OpenFiledialog.FilterIndex = 1;
           
           OpenFiledialog.Title = "Seleccione el archivo";
           if (OpenFiledialog.ShowDialog() == DialogResult.OK)
           {
               DireccionR = OpenFiledialog.FileName.ToString();
               Nombre_Archivo = OpenFiledialog.SafeFileName.ToString();
               string[] array = Nombre_Archivo.Split('.');
               Nombre_Archivo = array[0].ToString();
               Ext_Archivo = array[1].ToString();
           }
           return DireccionR;
       }

    }

