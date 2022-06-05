using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

/// <summary>
/// Descripción breve de formatopdf
/// </summary>
public class formatopdf
{
    public static string ttf;
    public static Font font;
    public static Font fontnegrita;
    public static iTextSharp.text.Table Tabla;
    public static int ColumnaCount;
    public static int FILACount;


	public formatopdf()
	{
        // TODO: Agregar aquí la lógica del constructor
	}

    public static void IniciaTabla(int Columnas, int Filas)
    {
        ColumnaCount = Columnas;
        FILACount = Filas;
        int tableRows = FILACount;
        Tabla= new iTextSharp.text.Table(ColumnaCount, tableRows);
        Tabla.Width = 100;
        Tabla.BorderWidth = 0;
        Tabla.BorderColor = new Color(0, 0, 0);
        Tabla.Cellpadding = 1;
        Tabla.Cellspacing = 1;
    }

    public static void Fuente()
    {
        ttf = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\arial.ttf";
        BaseFont fuenteArial = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        font = new Font(fuenteArial);

        fontnegrita = new Font(fuenteArial, 11, iTextSharp.text.Font.BOLD);

    }

    public static void Fuente11()
    {
        font.Size = 11;
    }

   

    public static void FuenteNum(int num)
    {
        font.Size = num;
        fontnegrita.Size = num;
    }

    public static void FuenteCOlor(int pri, int seg, int ter)
    {
        font.Color = new Color(pri, seg, ter);
        fontnegrita.Color = new Color(pri, seg, ter);
    }

    public static void ColocaBannerTabla()
    {
        string CadenaGral = AppDomain.CurrentDomain.BaseDirectory.ToString() + "images\\Cepamm.jpg";
        iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(CadenaGral);

        Cell c0 = new Cell();
        c0.BorderWidth = 0;
        c0.Add(gif);
        c0.Header = true;
        c0.Colspan = ColumnaCount;
        Tabla.AddCell(c0);
    }
    
    public static void IngresaTextoBarraArribaNegrita(string Texto, int union)
    {
        Cell c0 = new Cell();
        c0.BorderWidth = 0;
        c0.BorderWidthTop = 1;
        c0.Colspan = union;
        c0.Add(new Paragraph(Texto, fontnegrita));
        Tabla.AddCell(c0);
    }

    public static void ColocaImagenEmpleadoTabla()
    {
        string CadenaGral = AppDomain.CurrentDomain.BaseDirectory.ToString() + "imgsTemporal\\temp.Jpeg";
        iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(CadenaGral);

        Cell c0 = new Cell();
        c0.BorderWidth = 1;
        c0.Add(gif);
        c0.Header = true;
        c0.Colspan = ColumnaCount;
        Tabla.AddCell(c0);
    }
    public static void IngresaTexto(string Texto, int union)
    {
        Cell c0 = new Cell();
        c0.BorderWidth = 0;
        c0.Colspan = union;
        c0.Add(new Paragraph(Texto, font));
        Tabla.AddCell(c0);
    }


    public static void IngresaTextoDerecha(string Texto, int union)
    {
        Cell c0 = new Cell();
        c0.BorderWidth = 0;


        c0.Colspan = union;
        c0.Add(new Paragraph(Texto, font));
        //c0.PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        c0.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
        c0.VerticalAlignment = PdfPCell.ALIGN_TOP;
        Tabla.AddCell(c0);
    }

    public static void IngresaTextoDerechaLineaArriba(string Texto, int union)
    {
        Cell c0 = new Cell();
        c0.BorderWidth = 0;
        c0.BorderWidthTop = 1;
        c0.Colspan = union;
        c0.Add(new Paragraph(Texto, font));
        //c0.PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        c0.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
        c0.VerticalAlignment = PdfPCell.ALIGN_TOP;
        Tabla.AddCell(c0);
    }

    

    public static void IngresaTextoNegrita(string Texto, int union)
    {
        Cell c0 = new Cell();
        c0.BorderWidth = 0;
        c0.Colspan = union;
        c0.Add(new Paragraph(Texto, fontnegrita)); // para hacerla negrita paso la fontnegrita
        Tabla.AddCell(c0);
    }

    public static void IngresaTextoBarraArriba(string Texto, int union)
    {
        Cell c0 = new Cell();
        c0.BorderWidth = 0;
        c0.BorderWidthTop = 1;
        c0.Colspan = union;
        c0.Add(new Paragraph(Texto, font));
        Tabla.AddCell(c0);
    }

    public static void IngresaTextoBarraAbajo(string Texto, int union)
    {
        Cell c0 = new Cell();
        c0.BorderWidth = 0;
        c0.BorderWidthBottom = 1;
        c0.Colspan = union;
        c0.Add(new Paragraph(Texto, font));
        Tabla.AddCell(c0);
    }
    public static void IngresaTextoBarraAbajoNegrita(string Texto, int union)
    {
        Cell c0 = new Cell();
        c0.BorderWidth = 0;
        c0.BorderWidthBottom = 1;
        c0.Colspan = union;
        c0.Add(new Paragraph(Texto, fontnegrita));
        Tabla.AddCell(c0);
    }
    public static void IngresaTextoFondo(string Texto, int union,int pri, int seg, int ter)
    {
        Cell c0 = new Cell();
        c0.BorderWidth = 0;
        c0.BackgroundColor = new Color(pri, seg, ter);
        c0.Add(new Paragraph(Texto, font));
        c0.Colspan = union;
        Tabla.AddCell(c0);
    }

        public static void IngresaTextoFondonNegrita(string Texto, int union,int pri, int seg, int ter)
    {
        Cell c0 = new Cell();
        c0.BorderWidth = 0;
        c0.BorderWidthTop = 1;
        c0.BackgroundColor = new Color(pri, seg, ter);
        c0.Add(new Paragraph(Texto, fontnegrita));
        c0.Colspan = union;
        Tabla.AddCell(c0);
    }

    public static void TituloTabla(string Titulo)
    {
        Cell c1 = new Cell();
        c1.BorderWidth = 0;
        c1.Header = true;
        c1.Colspan = ColumnaCount;
        c1.Add(new Paragraph(Titulo, font));
        Tabla.AddCell(c1);
    }

    public static void Cierradoc(string nombredoc, string NUsuario, string Direccion)
    {
        Document Doc = new Document();
        PdfWriter.GetInstance(Doc, new FileStream(Direccion, FileMode.Create));
        
        Doc.Open();

        HeaderFooter footer = new HeaderFooter(new Phrase("USUARIO:" + NUsuario +  "\n  pág: ",font), true);
        footer.Border = Rectangle.NO_BORDER;
        Doc.Footer = footer;

        Doc.Add(Tabla);
        //Doc.PageSize.Rotate();
        
       // Doc.NewPage();

        Doc.Close();

    }

    public static void CierradocHorizontal(string nombredoc, string NUsuario, string Direccion)
    {
        Document Doc = new Document(PageSize.LETTER.Rotate());
        PdfWriter.GetInstance(Doc, new FileStream(Direccion, FileMode.Create));

        Doc.Open();

        HeaderFooter footer = new HeaderFooter(new Phrase("USUARIO:" + NUsuario + "\n FECHA:" + DateTime.Now.ToShortDateString() + "  pág: ", font), true);
        footer.Border = Rectangle.NO_BORDER;
        Doc.Footer = footer;

        Doc.Add(Tabla);
        //Doc.PageSize.Rotate();


        // Doc.NewPage();
        Doc.Close();
    }
}