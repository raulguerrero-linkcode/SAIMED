using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

public class ImpresionPDF
    {

        public static void AgregarPrintScript(string Original, string Copia)
        {
            PdfReader reader = new PdfReader(Original);
            PdfStamper stamper = new PdfStamper(reader, new FileStream(Copia, FileMode.Create));
            AcroFields fields = stamper.AcroFields;
            stamper.JavaScript = "this.print(true);\r";
            stamper.FormFlattening = true;
            stamper.Close();
            reader.Close();
        }

        //AgregarPrintScript("C:\\Test\\Original.pdf", "C:\\Test\\Copia.pdf");
        public void ImpresionDirecta(string Direccion, string NombreImpresora)
        { 
           Process oProc = new Process();
           oProc.StartInfo.Arguments = "/print /copies:1 /printer:" + NombreImpresora + " /pdffile:" + Direccion;
           oProc.Start();
           oProc.Close();
        }
    }

