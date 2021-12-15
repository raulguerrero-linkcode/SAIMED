using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
   public class ReportesNKB
    {


        public static void MandarCantidadesR(string[,] Datos, int Total)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;
            Excel.Range oResizeRange;
            string filename, filePath;


            try
            {
                //Start Excel and get Application object.
                oXL = new Excel.Application();
                oXL.Visible = false;

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                //Format A1:D1 as bold, vertical alignment = center.
                //oSheet.get_Range("A1", "K1").Font.Background=System.Drawing.Color.Fuchsia;
                int iNumQtrs = 8;

                bool bandera = false;

                oResizeRange = oSheet.get_Range("A1", "H1").get_Resize(Missing.Value, iNumQtrs);
                oSheet.get_Range("A1", "H1").Font.Bold = true;
                oSheet.get_Range("A1", "H1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A1", "H1").Merge(bandera);

                oSheet.Cells[1, 1] = "REGISTRO DE CANTIDADES";

                oSheet.Cells[3, 1] = "Consecutivo";
                oSheet.Cells[3, 2] = "Nombre";
                oSheet.Cells[3, 3] = "Vendedor";
                oSheet.Cells[3, 4] = "Fecha";
                oSheet.Cells[3, 5] = "Num Remision";
                oSheet.Cells[3, 6] = "Num Factura";
                oSheet.Cells[3, 7] = "Total";
                oSheet.Cells[3, 8] = "Cantidad Letra";

                oResizeRange = oSheet.get_Range("A3", "H3").get_Resize(Missing.Value, iNumQtrs);
                oResizeRange.Interior.ColorIndex = 23;
                oResizeRange.Font.ColorIndex = 2;

                oSheet.get_Range("A3", "H3").Font.Bold = true;
                oSheet.get_Range("A3", "H3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A3", "H3").EntireColumn.AutoFit();


                int contar = 0;

                string Formato = "0";

                for (int i = 4; i < (Total + 4); i++)
                {
                    if (Datos[contar, 0] == null) break;

                    oSheet.get_Range("A" + i, "A" + i).Value2 = Datos[contar, 0].ToString();
                    //oSheet.get_Range("A" + i, "A" + i).NumberFormat = Formato;

                    oSheet.get_Range("B" + i, "B" + i).Value2 = Datos[contar, 1].ToString();
                    oSheet.get_Range("C" + i, "C" + i).Value2 = Datos[contar, 2].ToString();
                    oSheet.get_Range("D" + i, "D" + i).Value2 = Datos[contar, 3].ToString();
                    oSheet.get_Range("E" + i, "E" + i).Value2 = Datos[contar, 4].ToString();
                    oSheet.get_Range("F" + i, "F" + i).Value2 = Datos[contar, 5].ToString();
                    oSheet.get_Range("G" + i, "G" + i).Value2 = Datos[contar, 6].ToString();
                    oSheet.get_Range("H" + i, "H" + i).Value2 = Datos[contar, 7].ToString();
                    
                    oSheet.get_Range("A" + i, "H" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    oSheet.get_Range("A" + i, "H" + i).EntireColumn.AutoFit();
                    contar++;

                }

                contar = contar + 5;

                oXL.Visible = true;
                oXL.UserControl = true;

            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Error");
            }

        }
  

    }

