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

       public static void RBusquedaRemisionesRecibos(string Fecha1, string Fecha2, string numpedido, bool Todas)
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

               //filePath = oXL.GetSaveAsFilename("HUELLASEMPLEADOS", "Archivos de Excel (*.xlsx), *.xlsx", 1, "Guardar huellas empleados", Missing.Value).ToString(); 

               //Get a new workbook.
               oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
               oSheet = (Excel._Worksheet)oWB.ActiveSheet;



               //Format A1:D1 as bold, vertical alignment = center.
               //oSheet.get_Range("A1", "K1").Font.Background=System.Drawing.Color.Fuchsia;
               int iNumQtrs = 10;
               bool bandera = false;
               oResizeRange = oSheet.get_Range("A1", "E1").get_Resize(Missing.Value, iNumQtrs);
               oSheet.get_Range("A1", "E1").Font.Bold = true;
               oSheet.get_Range("A1", "E1").Font.Size = 16;
               oSheet.get_Range("A1", "E1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
               oSheet.get_Range("A1", "E1").Merge(bandera);

               oSheet.Cells[1, 1] = "LISTA DE RECIBOS " + DateTime.Now.ToString("dd/MM/yyyy");

               //oSheet.get_Range("A1", "A1").Value2 = "LISTADO DE PROVEEDORES";


               //Add table headers going cell by cell.

               oSheet.Cells[3, 1] = "Num. Recibo";
               oSheet.Cells[3, 2] = "Nombre";
               oSheet.Cells[3, 3] = "Fecha";
               oSheet.Cells[3, 4] = "Subtotal   ";
               oSheet.Cells[3, 5] = "IVA   ";
               oSheet.Cells[3, 6] = "Total       ";
              //oSheet.Cells[3, 7] = "                           Descripción de compra                             ";
               oSheet.Cells[3, 7] = "Estatus";
               oSheet.Cells[3, 8] = "Entregado";
               oSheet.Cells[3, 9] = "Tipo de Pago";
               oSheet.Cells[3, 10] = "Emitio";
               oSheet.Cells[3, 11] = "Compro";



               oResizeRange = oSheet.get_Range("A3", "E3").get_Resize(Missing.Value, iNumQtrs);
               oResizeRange.Interior.ColorIndex = 23;
               oResizeRange.Font.ColorIndex = 2;

               oSheet.get_Range("A3", "K3").Font.Bold = true;
               oSheet.get_Range("A3", "K3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
               oSheet.get_Range("A3", "K3").EntireColumn.AutoFit();
               int contar = 0;

               string Formato = "0";
               conectorSql conecta = new conectorSql();
               conectorSql conecta2 = new conectorSql();
               string Query = "Select distinct(numrecibo), nombrerecibo as Nombrecliente,compro ";
               Query = Query + ",fecha,total,iva,totalgeneral,estatusrecibo,tiporecibo,emitio,entregado";
               Query = Query + " from recibos ";
               Query = Query + " where numrecibo<>''";

               if (Todas == false) Query = Query + " and fechacod between '" + Fecha1 + "' and '" + Fecha2 + "'";
               if (numpedido != "") Query = Query + " and numrecibo='" + numpedido + "'";
               Query = Query + " order by numrecibo asc";

               int i = 4;
               SqlDataReader leer = conecta.RecordInfo(Query);
               while (leer.Read())
               {
                   string clave = leer["numrecibo"].ToString();
                   string estatuspedido = leer["estatusrecibo"].ToString();

                   oSheet.get_Range("A" + i, "A" + i).Value2 = clave;
                   oSheet.get_Range("B" + i, "B" + i).Value2 = leer["nombrecliente"].ToString();
                   oSheet.get_Range("C" + i, "C" + i).Value2 = leer["fecha"].ToString();
                   decimal total = decimal.Parse(leer["total"].ToString());
                   oSheet.get_Range("D" + i, "D" + i).Value2 = "$ " + total.ToString("##.00", CultureInfo.InvariantCulture);

                   decimal iva = decimal.Parse(leer["iva"].ToString());
                   oSheet.get_Range("E" + i, "E" + i).Value2 = "$ " + iva.ToString("##.00", CultureInfo.InvariantCulture);

                   decimal totalneto = decimal.Parse(leer["totalgeneral"].ToString());
                   oSheet.get_Range("F" + i, "F" + i).Value2 = "$ " + totalneto.ToString("##.00", CultureInfo.InvariantCulture);

                   //oSheet.get_Range("G" + i, "G" + i).Value2 = leer["compro"].ToString();
                   oSheet.get_Range("G" + i, "G" + i).Value2 = estatuspedido;
                   
                   oSheet.get_Range("H" + i, "H" + i).Value2 = leer["entregado"].ToString();

                   string tiporecibo = leer["tiporecibo"].ToString();
                   if (tiporecibo == "CREDITO") tiporecibo = "POR PAGAR";

                   oSheet.get_Range("I" + i, "I" + i).Value2 = tiporecibo;
                   oSheet.get_Range("J" + i, "J" + i).Value2 = leer["emitio"].ToString();
                   oSheet.get_Range("K" + i, "K" + i).Value2 = leer["compro"].ToString();

                   oSheet.get_Range("A" + i, "J" + i).Font.Size = 9;
                   oSheet.get_Range("A" + i, "J" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                   oSheet.get_Range("A" + i, "J" + i).EntireColumn.AutoFit();
                   i++;
               }
               conecta.CierraConexion();

               //Make sure Excel is visible and give the user control
               //of Microsoft Excel's lifetime.
               oXL.Visible = true;
               oXL.UserControl = true;
               ////------ proceso para guardarlo y cerrarlo 
               //oXL.ActiveWorkbook.Close(true, filePath, Type.Missing); 
               //oXL.Quit();
               //System.Diagnostics.Process[] myProcesses;
               //myProcesses = System.Diagnostics.Process.GetProcessesByName("EXCEL");
               //foreach (System.Diagnostics.Process instance in myProcesses)
               //{
               //    instance.CloseMainWindow();
               //    instance.Kill();
               //    instance.Close();
               //}
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
   
        public static void RBusquedaProductos(string nombre, string categoria, string marca, string clavep, string unidad, string porsurtir)
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

                //filePath = oXL.GetSaveAsFilename("HUELLASEMPLEADOS", "Archivos de Excel (*.xlsx), *.xlsx", 1, "Guardar huellas empleados", Missing.Value).ToString(); 

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;



                //Format A1:D1 as bold, vertical alignment = center.
                //oSheet.get_Range("A1", "K1").Font.Background=System.Drawing.Color.Fuchsia;
                int iNumQtrs = 13;
                bool bandera = false;
                oResizeRange = oSheet.get_Range("A1", "M1").get_Resize(Missing.Value, iNumQtrs);
                oSheet.get_Range("A1", "M1").Font.Bold = true;
                oSheet.get_Range("A1", "M1").Font.Size = 16;
                oSheet.get_Range("A1", "M1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A1", "M1").Merge(bandera);

                oSheet.Cells[1, 1] = "LISTA DE PRODUCTOS / SERVICIOS  DE LA FECHA " + DateTime.Now.ToString("dd/MM/yyyy");

                //oSheet.get_Range("A1", "A1").Value2 = "LISTADO DE PROVEEDORES";


                //Add table headers going cell by cell.

                oSheet.Cells[3, 1] = "Clave";
                oSheet.Cells[3, 2] = "Nombre";
                oSheet.Cells[3, 3] = "Categoría";
                oSheet.Cells[3, 4] = "Unidad";
                oSheet.Cells[3, 5] = "Existencia";
                oSheet.Cells[3, 6] = "Minimo";
                oSheet.Cells[3, 7] = "Maximo";
                oSheet.Cells[3, 8] = "Descripción";
                oSheet.Cells[3, 9] = "Precio Distribuidor";
                oSheet.Cells[3, 10] = "Precio Publico 1";
                oSheet.Cells[3, 11] = "Precio Publico 2";
                oSheet.Cells[3, 12] = "Precio Publico 3";
                oSheet.Cells[3, 13] = "Fecha de MOdificación";

                oResizeRange = oSheet.get_Range("A3", "E3").get_Resize(Missing.Value, iNumQtrs);
                oResizeRange.Interior.ColorIndex = 23;
                oResizeRange.Font.ColorIndex = 2;

                oSheet.get_Range("A3", "M3").Font.Bold = true;
                oSheet.get_Range("A3", "M3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A3", "M3").EntireColumn.AutoFit();
                int contar = 0;

                string Formato = "0";
                conectorSql conecta = new conectorSql();
                conectorSql conecta2 = new conectorSql();
                string Query = "Select * from productos where cvproducto<>''";
                if (nombre!= "") Query = Query + " and nombre like '%" + nombre + "%'";
                if (categoria!= "") Query = Query + " and categoria='" + categoria+ "'";
                if (clavep != "") Query = Query + " and cvproducto='" + clavep + "'";
                if (marca != "") Query = Query + " and marca='" + marca+ "'";
                if (unidad != "") Query = Query + " and unidad='" + unidad+ "'";
                if (porsurtir == "SI")  Query = Query + " and cantidad<=(minimo+2)";

                Query = Query + " order by nombre desc ";
                int i = 4;
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    string clave=leer["cvproducto"].ToString();
                    oSheet.get_Range("A" + i, "A" + i).Value2 = clave;
                    oSheet.get_Range("B" + i, "B" + i).Value2 = leer["nombre"].ToString();
                    oSheet.get_Range("C" + i, "C" + i).Value2 = leer["categoria"].ToString();
                    oSheet.get_Range("D" + i, "D" + i).Value2 = leer["unidad"].ToString();

                    oSheet.get_Range("E" + i, "E" + i).Value2 = leer["cantidad"].ToString();
                    oSheet.get_Range("F" + i, "F" + i).Value2 = leer["minimo"].ToString();
                    oSheet.get_Range("G" + i, "G" + i).Value2 = leer["maximo"].ToString();
                    oSheet.get_Range("H" + i, "H" + i).Value2 = leer["descripcion"].ToString();

                    string consulta = "Select * from ListaPrecios where cvproducto='" + clave + "'";
                    SqlDataReader leer2 = conecta2.RecordInfo(consulta);
                    while (leer2.Read())
                    {
                        decimal distribuidor = decimal.Parse(leer2["distribuidor"].ToString());
                        decimal publico1 = decimal.Parse(leer2["publico1"].ToString());
                        decimal publico2 = decimal.Parse(leer2["publico2"].ToString());
                        decimal publico3 = decimal.Parse(leer2["publico3"].ToString());
                        
                        oSheet.get_Range("I" + i, "I" + i).Value2 =distribuidor.ToString("##.00", CultureInfo.InvariantCulture);
                        oSheet.get_Range("J" + i, "J" + i).Value2 =publico1.ToString("##.00", CultureInfo.InvariantCulture);
                        oSheet.get_Range("K" + i, "K" + i).Value2 = publico2.ToString("##.00", CultureInfo.InvariantCulture);
                        oSheet.get_Range("L" + i, "L" + i).Value2 = publico3.ToString("##.00", CultureInfo.InvariantCulture);
                    }
                    conecta2.CierraConexion();

                    oSheet.get_Range("M" + i, "M" + i).Value2 = leer["fechaModifica"].ToString();

                    oSheet.get_Range("A" + i, "M" + i).Font.Size = 9;
                    oSheet.get_Range("A" + i, "M" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    oSheet.get_Range("A" + i, "M" + i).EntireColumn.AutoFit();
                    i++;
                }
                conecta.CierraConexion();

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;
                oXL.UserControl = true;
                ////------ proceso para guardarlo y cerrarlo 
                //oXL.ActiveWorkbook.Close(true, filePath, Type.Missing); 
                //oXL.Quit();
                //System.Diagnostics.Process[] myProcesses;
                //myProcesses = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                //foreach (System.Diagnostics.Process instance in myProcesses)
                //{
                //    instance.CloseMainWindow();
                //    instance.Kill();
                //    instance.Close();
                //}
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

        public static void RBusquedaProveedores(string nombre)
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

                //filePath = oXL.GetSaveAsFilename("HUELLASEMPLEADOS", "Archivos de Excel (*.xlsx), *.xlsx", 1, "Guardar huellas empleados", Missing.Value).ToString(); 

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                oSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperLetter;

                //Format A1:D1 as bold, vertical alignment = center.
                //oSheet.get_Range("A1", "K1").Font.Background=System.Drawing.Color.Fuchsia;
                int iNumQtrs = 12;
                bool bandera = false;
                oResizeRange = oSheet.get_Range("A1", "E1").get_Resize(Missing.Value, iNumQtrs);
                oSheet.get_Range("A1", "E1").Font.Bold = true;
                oSheet.get_Range("A1", "E1").Font.Size = 16;
                oSheet.get_Range("A1", "E1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A1", "E1").Merge(bandera);

                oSheet.Cells[1, 1] = "LISTA DE PROVEEDORES " + DateTime.Now.ToString("dd/MM/yyyy");

                //oSheet.get_Range("A1", "A1").Value2 = "LISTADO DE PROVEEDORES";


                //Add table headers going cell by cell.

                oSheet.Cells[3, 1] = "Clave";
                oSheet.Cells[3, 2] = "Razón Social";
                oSheet.Cells[3, 3] = "Nombre Comercial";
                oSheet.Cells[3, 4] = "Telefono";
                oSheet.Cells[3, 5] = "Celular";
                oSheet.Cells[3, 6] = "Correo 1";
                oSheet.Cells[3, 7] = "Correo 2";
                oSheet.Cells[3, 8] = "Direccion";
                oSheet.Cells[3, 9] = "RFC";
                oSheet.Cells[3, 10] = "Direccion Fiscal";
                oSheet.Cells[3, 11] = "¿Esta Activo?";

                oResizeRange = oSheet.get_Range("A3", "E3").get_Resize(Missing.Value, iNumQtrs);
                oResizeRange.Interior.ColorIndex = 23;
                oResizeRange.Font.ColorIndex = 2;

                oSheet.get_Range("A3", "J3").Font.Bold = true;
                oSheet.get_Range("A3", "J3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A3", "J3").EntireColumn.AutoFit();
                int contar = 0;

                string Formato = "0";
                conectorSql conecta = new conectorSql();
                conectorSql conecta2 = new conectorSql();
                string Query = "Select * from proveedores where cvprov<>''";
                if (nombre != "") Query = Query + " and nombre like '%" + nombre + "%'";
                int i = 4;
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    string clave = leer["cvcliente"].ToString();
                    oSheet.get_Range("A" + i, "A" + i).Value2 = clave;
                    oSheet.get_Range("B" + i, "B" + i).Value2 = leer["nombre"].ToString();
                    oSheet.get_Range("C" + i, "C" + i).Value2 = leer["empresa"].ToString();
                    oSheet.get_Range("D" + i, "D" + i).Value2 = leer["telefono"].ToString();

                    oSheet.get_Range("E" + i, "E" + i).Value2 = leer["celular"].ToString();
                    oSheet.get_Range("F" + i, "F" + i).Value2 = leer["email"].ToString();
                    oSheet.get_Range("G" + i, "G" + i).Value2 = leer["email2"].ToString();
                    oSheet.get_Range("H" + i, "H" + i).Value2 = leer["direccion"].ToString();

                    oSheet.get_Range("I" + i, "I" + i).Value2 = leer["RFC"].ToString();
                    oSheet.get_Range("J" + i, "J" + i).Value2 = leer["direfiscal"].ToString();
                    oSheet.get_Range("K" + i, "K" + i).Value2 = leer["activo"].ToString();

                    oSheet.get_Range("A" + i, "L" + i).Font.Size = 10;
                    oSheet.get_Range("A" + i, "L" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    oSheet.get_Range("A" + i, "L" + i).EntireColumn.AutoFit();
                    i++;
                }
                conecta.CierraConexion();

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;
                oXL.UserControl = true;
                ////------ proceso para guardarlo y cerrarlo 
                //oXL.ActiveWorkbook.Close(true, filePath, Type.Missing); 
                //oXL.Quit();
                //System.Diagnostics.Process[] myProcesses;
                //myProcesses = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                //foreach (System.Diagnostics.Process instance in myProcesses)
                //{
                //    instance.CloseMainWindow();
                //    instance.Kill();
                //    instance.Close();
                //}
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
        public static void RBusquedaClientes(string nombre)
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

                //filePath = oXL.GetSaveAsFilename("HUELLASEMPLEADOS", "Archivos de Excel (*.xlsx), *.xlsx", 1, "Guardar huellas empleados", Missing.Value).ToString(); 

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                oSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperLetter;

                //Format A1:D1 as bold, vertical alignment = center.
                //oSheet.get_Range("A1", "K1").Font.Background=System.Drawing.Color.Fuchsia;
                int iNumQtrs = 11;
                bool bandera = false;
                oResizeRange = oSheet.get_Range("A1", "E1").get_Resize(Missing.Value, iNumQtrs);
                oSheet.get_Range("A1", "E1").Font.Bold = true;
                oSheet.get_Range("A1", "E1").Font.Size = 16;
                oSheet.get_Range("A1", "E1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A1", "E1").Merge(bandera);

                oSheet.Cells[1, 1] = "LISTA DE CLIENTES " + DateTime.Now.ToString("dd/MM/yyyy");

                //oSheet.get_Range("A1", "A1").Value2 = "LISTADO DE PROVEEDORES";


                //Add table headers going cell by cell.

                oSheet.Cells[3, 1] = "Clave";
                oSheet.Cells[3, 2] = "Razón Social";
                oSheet.Cells[3, 3] = "Nombre Comercial";
                oSheet.Cells[3, 4] = "Telefono";
                oSheet.Cells[3, 5] = "Celular";
                oSheet.Cells[3, 6] = "Correo 1";
                oSheet.Cells[3, 7] = "Correo 2";
                oSheet.Cells[3, 8] = "Direccion";
                oSheet.Cells[3, 9] = "RFC";
                oSheet.Cells[3, 10] = "Direccion Fiscal";
                oSheet.Cells[3, 11] = "¿Factura?";
                //oSheet.Cells[3, 12] = "¿Esta Activo?";

                oResizeRange = oSheet.get_Range("A3", "E3").get_Resize(Missing.Value, iNumQtrs);
                oResizeRange.Interior.ColorIndex = 23;
                oResizeRange.Font.ColorIndex = 2;

                oSheet.get_Range("A3", "J3").Font.Bold = true;
                oSheet.get_Range("A3", "J3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A3", "J3").EntireColumn.AutoFit();
                int contar = 0;

                string Formato = "0";
                conectorSql conecta = new conectorSql();
                conectorSql conecta2 = new conectorSql();
                string Query = "Select * from clientes where cvcliente<>''";
                if (nombre != "") Query = Query + " and nombre like '%" + nombre + "%'";
                int i = 4;
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    string clave = leer["cvcliente"].ToString();
                    oSheet.get_Range("A" + i, "A" + i).Value2 = clave;
                    oSheet.get_Range("B" + i, "B" + i).Value2 = leer["nombre"].ToString();
                    oSheet.get_Range("C" + i, "C" + i).Value2 = leer["empresa"].ToString();
                    oSheet.get_Range("D" + i, "D" + i).Value2 = leer["telefono"].ToString();

                    oSheet.get_Range("E" + i, "E" + i).Value2 = leer["celular"].ToString();
                    oSheet.get_Range("F" + i, "F" + i).Value2 = leer["email"].ToString();
                    oSheet.get_Range("G" + i, "G" + i).Value2 = leer["email2"].ToString();
                    oSheet.get_Range("H" + i, "H" + i).Value2 = leer["direccion"].ToString();

                    oSheet.get_Range("I" + i, "I" + i).Value2 = leer["RFC"].ToString();
                    oSheet.get_Range("J" + i, "J" + i).Value2 = leer["direfiscal"].ToString();
                    oSheet.get_Range("K" + i, "K" + i).Value2 = leer["factura"].ToString();
                  //  oSheet.get_Range("L" + i, "L" + i).Value2 = leer["activo"].ToString();

                    oSheet.get_Range("A" + i, "L" + i).Font.Size = 10;
                    oSheet.get_Range("A" + i, "L" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    oSheet.get_Range("A" + i, "L" + i).EntireColumn.AutoFit();
                    i++;
                }
                conecta.CierraConexion();

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;
               
                oXL.UserControl = true;

                ////------ proceso para guardarlo y cerrarlo 
                //oXL.ActiveWorkbook.Close(true, filePath, Type.Missing); 
                //oXL.Quit();
                //System.Diagnostics.Process[] myProcesses;
                //myProcesses = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                //foreach (System.Diagnostics.Process instance in myProcesses)
                //{
                //    instance.CloseMainWindow();
                //    instance.Kill();
                //    instance.Close();
                //}
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

        public static void RBusquedaBancos(string nombre)
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

                //filePath = oXL.GetSaveAsFilename("HUELLASEMPLEADOS", "Archivos de Excel (*.xlsx), *.xlsx", 1, "Guardar huellas empleados", Missing.Value).ToString(); 

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                oSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperLetter;

                //Format A1:D1 as bold, vertical alignment = center.
                //oSheet.get_Range("A1", "K1").Font.Background=System.Drawing.Color.Fuchsia;
                int iNumQtrs = 6;
                bool bandera = false;
                oResizeRange = oSheet.get_Range("A1", "E1").get_Resize(Missing.Value, iNumQtrs);
                oSheet.get_Range("A1", "E1").Font.Bold = true;
                oSheet.get_Range("A1", "E1").Font.Size = 16;
                oSheet.get_Range("A1", "E1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A1", "E1").Merge(bandera);

                oSheet.Cells[1, 1] = "LISTA DE BANCOS " + DateTime.Now.ToString("dd/MM/yyyy");

                //oSheet.get_Range("A1", "A1").Value2 = "LISTADO DE PROVEEDORES";


                //Add table headers going cell by cell.

                oSheet.Cells[3, 1] = "Clave";
                oSheet.Cells[3, 2] = "Nombre";
                oSheet.Cells[3, 3] = "Cuenta";
                oSheet.Cells[3, 4] = "Sucursal";
                oSheet.Cells[3, 5] = "Clabe";
                oSheet.Cells[3, 6] = "Depositar a";

                oResizeRange = oSheet.get_Range("A3", "E3").get_Resize(Missing.Value, iNumQtrs);
                oResizeRange.Interior.ColorIndex = 23;
                oResizeRange.Font.ColorIndex = 2;

                oSheet.get_Range("A3", "J3").Font.Bold = true;
                oSheet.get_Range("A3", "J3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A3", "J3").EntireColumn.AutoFit();
                int contar = 0;

                string Formato = "0";
                conectorSql conecta = new conectorSql();
                conectorSql conecta2 = new conectorSql();
                string Query = "Select * from bancos where cvbanco<>''";
                if (nombre != "") Query = Query + " and nombre like '%" + nombre + "%'";
                int i = 4;
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    string clave = leer["cvbanco"].ToString();
                    oSheet.get_Range("A" + i, "A" + i).Value2 = clave;
                    oSheet.get_Range("B" + i, "B" + i).Value2 = leer["nombre"].ToString();
                    oSheet.get_Range("C" + i, "C" + i).Value2 = leer["cuenta"].ToString();
                    
                    oSheet.get_Range("D" + i, "D" + i).Value2 = leer["sucursal"].ToString();

                    oSheet.get_Range("E" + i, "E" + i).NumberFormat="@";
                    oSheet.get_Range("E" + i, "E" + i).Value2 = leer["interbancaria"].ToString();
                    oSheet.get_Range("F" + i, "F" + i).Value2 = leer["nombredeposito"].ToString();
                    oSheet.get_Range("A" + i, "F" + i).Font.Size = 10;
                    oSheet.get_Range("A" + i, "F" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    oSheet.get_Range("A" + i, "F" + i).EntireColumn.AutoFit();
                    i++;
                }
                conecta.CierraConexion();

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;

                oXL.UserControl = true;

                ////------ proceso para guardarlo y cerrarlo 
                //oXL.ActiveWorkbook.Close(true, filePath, Type.Missing); 
                //oXL.Quit();
                //System.Diagnostics.Process[] myProcesses;
                //myProcesses = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                //foreach (System.Diagnostics.Process instance in myProcesses)
                //{
                //    instance.CloseMainWindow();
                //    instance.Kill();
                //    instance.Close();
                //}
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

        public static void RBusquedaFormasPago(string nombre)
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

                //filePath = oXL.GetSaveAsFilename("HUELLASEMPLEADOS", "Archivos de Excel (*.xlsx), *.xlsx", 1, "Guardar huellas empleados", Missing.Value).ToString(); 

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                oSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperLetter;

                //Format A1:D1 as bold, vertical alignment = center.
                //oSheet.get_Range("A1", "K1").Font.Background=System.Drawing.Color.Fuchsia;
                int iNumQtrs = 2;
                bool bandera = false;
                oResizeRange = oSheet.get_Range("A1", "E1").get_Resize(Missing.Value, iNumQtrs);
                oSheet.get_Range("A1", "E1").Font.Bold = true;
                oSheet.get_Range("A1", "E1").Font.Size = 16;
                oSheet.get_Range("A1", "E1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A1", "E1").Merge(bandera);

                oSheet.Cells[1, 1] = "LISTA DE FORMAS DE PAGO " + DateTime.Now.ToString("dd/MM/yyyy");

                //oSheet.get_Range("A1", "A1").Value2 = "LISTADO DE PROVEEDORES";


                //Add table headers going cell by cell.

                oSheet.Cells[3, 1] = "Clave";
                oSheet.Cells[3, 2] = "Nombre";

                oResizeRange = oSheet.get_Range("A3", "E3").get_Resize(Missing.Value, iNumQtrs);
                oResizeRange.Interior.ColorIndex = 23;
                oResizeRange.Font.ColorIndex = 2;

                oSheet.get_Range("A3", "J3").Font.Bold = true;
                oSheet.get_Range("A3", "J3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A3", "J3").EntireColumn.AutoFit();
                int contar = 0;

                string Formato = "0";
                conectorSql conecta = new conectorSql();
                conectorSql conecta2 = new conectorSql();
                string Query = "Select * from formasdepago where cvforma<>''";
                if (nombre != "") Query = Query + " and nombre like '%" + nombre + "%'";
                int i = 4;
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    string clave = leer["cvforma"].ToString();
                    oSheet.get_Range("A" + i, "A" + i).Value2 = clave;
                    oSheet.get_Range("B" + i, "B" + i).Value2 = leer["nombre"].ToString();
               
                    oSheet.get_Range("A" + i, "F" + i).Font.Size = 9;
                    oSheet.get_Range("A" + i, "F" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    oSheet.get_Range("A" + i, "F" + i).EntireColumn.AutoFit();
                    i++;
                }
                conecta.CierraConexion();

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;

                oXL.UserControl = true;

                ////------ proceso para guardarlo y cerrarlo 
                //oXL.ActiveWorkbook.Close(true, filePath, Type.Missing); 
                //oXL.Quit();
                //System.Diagnostics.Process[] myProcesses;
                //myProcesses = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                //foreach (System.Diagnostics.Process instance in myProcesses)
                //{
                //    instance.CloseMainWindow();
                //    instance.Kill();
                //    instance.Close();
                //}
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

        public static void RBusquedaConceptosPago(string nombre)
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

                //filePath = oXL.GetSaveAsFilename("HUELLASEMPLEADOS", "Archivos de Excel (*.xlsx), *.xlsx", 1, "Guardar huellas empleados", Missing.Value).ToString(); 

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                oSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperLetter;

                //Format A1:D1 as bold, vertical alignment = center.
                //oSheet.get_Range("A1", "K1").Font.Background=System.Drawing.Color.Fuchsia;
                int iNumQtrs = 3;
                bool bandera = false;
                oResizeRange = oSheet.get_Range("A1", "E1").get_Resize(Missing.Value, iNumQtrs);
                oSheet.get_Range("A1", "E1").Font.Bold = true;
                oSheet.get_Range("A1", "E1").Font.Size = 16;
                oSheet.get_Range("A1", "E1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A1", "E1").Merge(bandera);

                oSheet.Cells[1, 1] = "LISTA DE CONCEPTOS DE PAGO " + DateTime.Now.ToString("dd/MM/yyyy");

                //oSheet.get_Range("A1", "A1").Value2 = "LISTADO DE PROVEEDORES";


                //Add table headers going cell by cell.

                oSheet.Cells[3, 1] = "Clave";
                oSheet.Cells[3, 2] = "Nombre";
                oSheet.Cells[3, 3] = "Precio";

                oResizeRange = oSheet.get_Range("A3", "E3").get_Resize(Missing.Value, iNumQtrs);
                oResizeRange.Interior.ColorIndex = 23;
                oResizeRange.Font.ColorIndex = 2;

                oSheet.get_Range("A3", "J3").Font.Bold = true;
                oSheet.get_Range("A3", "J3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A3", "J3").EntireColumn.AutoFit();
                int contar = 0;

                string Formato = "0";
                conectorSql conecta = new conectorSql();
                conectorSql conecta2 = new conectorSql();
                string Query = "Select * from ConceptosPago where cvconcepto<>''";
                if (nombre != "") Query = Query + " and nombre like '%" + nombre + "%'";
                int i = 4;
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    string clave = leer["cvconcepto"].ToString();
                    oSheet.get_Range("A" + i, "A" + i).Value2 = clave;
                    oSheet.get_Range("B" + i, "B" + i).Value2 = leer["nombre"].ToString();
                    oSheet.get_Range("C" + i, "C" + i).Value2 = leer["precio"].ToString();

                    oSheet.get_Range("A" + i, "F" + i).Font.Size = 10;
                    oSheet.get_Range("A" + i, "F" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    oSheet.get_Range("A" + i, "F" + i).EntireColumn.AutoFit();
                    i++;
                }
                conecta.CierraConexion();

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;

                oXL.UserControl = true;

                ////------ proceso para guardarlo y cerrarlo 
                //oXL.ActiveWorkbook.Close(true, filePath, Type.Missing); 
                //oXL.Quit();
                //System.Diagnostics.Process[] myProcesses;
                //myProcesses = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                //foreach (System.Diagnostics.Process instance in myProcesses)
                //{
                //    instance.CloseMainWindow();
                //    instance.Kill();
                //    instance.Close();
                //}
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



        public static void RBusquedaVendedores(string nombre)
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

                //filePath = oXL.GetSaveAsFilename("HUELLASEMPLEADOS", "Archivos de Excel (*.xlsx), *.xlsx", 1, "Guardar huellas empleados", Missing.Value).ToString(); 

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                oSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperLetter;

                //Format A1:D1 as bold, vertical alignment = center.
                //oSheet.get_Range("A1", "K1").Font.Background=System.Drawing.Color.Fuchsia;
                int iNumQtrs = 3;
                bool bandera = false;
                oResizeRange = oSheet.get_Range("A1", "E1").get_Resize(Missing.Value, iNumQtrs);
                oSheet.get_Range("A1", "E1").Font.Bold = true;
                oSheet.get_Range("A1", "E1").Font.Size = 16;
                oSheet.get_Range("A1", "E1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A1", "E1").Merge(bandera);

                oSheet.Cells[1, 1] = "LISTA DE VENDEDORES HASTA EL " + DateTime.Now.ToString("dd/MM/yyyy");

                //oSheet.get_Range("A1", "A1").Value2 = "LISTADO DE PROVEEDORES";


                //Add table headers going cell by cell.

                oSheet.Cells[3, 1] = "Clave";
                oSheet.Cells[3, 2] = "Nombre";
                oSheet.Cells[3, 3] = "% de Comision";

                oResizeRange = oSheet.get_Range("A3", "E3").get_Resize(Missing.Value, iNumQtrs);
                oResizeRange.Interior.ColorIndex = 23;
                oResizeRange.Font.ColorIndex = 2;

                oSheet.get_Range("A3", "J3").Font.Bold = true;
                oSheet.get_Range("A3", "J3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A3", "J3").EntireColumn.AutoFit();
                int contar = 0;

                string Formato = "0";
                conectorSql conecta = new conectorSql();
                conectorSql conecta2 = new conectorSql();
                string Query = "Select * from vendedores where cvvendedor<>''";
                if (nombre != "") Query = Query + " and nombre like '%" + nombre + "%'";
                int i = 4;
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    string clave = leer["cvvendedor"].ToString();
                    oSheet.get_Range("A" + i, "A" + i).Value2 = clave;
                    oSheet.get_Range("B" + i, "B" + i).Value2 = leer["nombre"].ToString();
                    oSheet.get_Range("C" + i, "C" + i).Value2 = leer["porcentaje"].ToString();

                    oSheet.get_Range("A" + i, "F" + i).Font.Size = 10;
                    oSheet.get_Range("A" + i, "F" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    oSheet.get_Range("A" + i, "F" + i).EntireColumn.AutoFit();
                    i++;
                }
                conecta.CierraConexion();

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;

                oXL.UserControl = true;

                ////------ proceso para guardarlo y cerrarlo 
                //oXL.ActiveWorkbook.Close(true, filePath, Type.Missing); 
                //oXL.Quit();
                //System.Diagnostics.Process[] myProcesses;
                //myProcesses = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                //foreach (System.Diagnostics.Process instance in myProcesses)
                //{
                //    instance.CloseMainWindow();
                //    instance.Kill();
                //    instance.Close();
                //}
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
        public static void RBusquedaRemisiones(string Fecha1 , string Fecha2 , string numpedido, string Estatus, bool Todas)
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

                //filePath = oXL.GetSaveAsFilename("HUELLASEMPLEADOS", "Archivos de Excel (*.xlsx), *.xlsx", 1, "Guardar huellas empleados", Missing.Value).ToString(); 

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;



                //Format A1:D1 as bold, vertical alignment = center.
                //oSheet.get_Range("A1", "K1").Font.Background=System.Drawing.Color.Fuchsia;
                int iNumQtrs = 9;
                bool bandera = false;
                oResizeRange = oSheet.get_Range("A1", "E1").get_Resize(Missing.Value, iNumQtrs);
                oSheet.get_Range("A1", "E1").Font.Bold = true;
                oSheet.get_Range("A1", "E1").Font.Size = 16;
                oSheet.get_Range("A1", "E1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A1", "E1").Merge(bandera);

                oSheet.Cells[1, 1] = "LISTA DE PEDIDOS " + DateTime.Now.ToString("dd/MM/yyyy");

                //oSheet.get_Range("A1", "A1").Value2 = "LISTADO DE PROVEEDORES";


                //Add table headers going cell by cell.

                oSheet.Cells[3, 1] = "Num. Pedido";
                oSheet.Cells[3, 2] = "Nombre";
                oSheet.Cells[3, 3] = "Fecha";
                oSheet.Cells[3, 4] = "Subtotal";
                oSheet.Cells[3, 5] = "IVA";
                oSheet.Cells[3, 6] = "Total";
                oSheet.Cells[3, 7] = "Tipo de pago";
                oSheet.Cells[3, 8] = "                           Descripción de compra                             ";
                oSheet.Cells[3, 9] = "Estatus";

                oResizeRange = oSheet.get_Range("A3", "E3").get_Resize(Missing.Value, iNumQtrs);
                oResizeRange.Interior.ColorIndex = 23;
                oResizeRange.Font.ColorIndex = 2;

                oSheet.get_Range("A3", "J3").Font.Bold = true;
                oSheet.get_Range("A3", "J3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A3", "J3").EntireColumn.AutoFit();
                int contar = 0;

                string Formato = "0";
                conectorSql conecta = new conectorSql();
                conectorSql conecta2 = new conectorSql();
                string Query = "Select distinct(numpedido), clientes.nombre as Nombrecliente ";
                Query = Query + ",fecha,total,iva,totalgeneral,status,compro,estatuspedido";
                Query = Query + " from Pedidos ";
                Query = Query + " inner join clientes on clientes.cvcliente=pedidos.cvcliente";
                Query = Query + " where numpedido<>''";

                if (Todas== false) Query = Query + " and fechacod between '" + Fecha1 + "' and '" + Fecha2 + "'";
                if (numpedido != "") Query = Query + " and numpedido='" + numpedido + "'";
                if (Estatus != "") Query = Query + " and status='" + Estatus + "'";
                Query = Query + " order by numpedido desc";

                int i = 4;
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    string clave = leer["numpedido"].ToString();
                    string estatuspedido = leer["estatuspedido"].ToString();

                    oSheet.get_Range("A" + i, "A" + i).Value2 = clave;
                    oSheet.get_Range("B" + i, "B" + i).Value2 = leer["nombrecliente"].ToString();
                    oSheet.get_Range("C" + i, "C" + i).Value2 = leer["fecha"].ToString();
                    decimal total = decimal.Parse(leer["total"].ToString());
                    oSheet.get_Range("D" + i, "D" + i).Value2 = "$ " + total.ToString("##.00", CultureInfo.InvariantCulture);

                    decimal iva = decimal.Parse(leer["iva"].ToString());
                    oSheet.get_Range("E" + i, "E" + i).Value2 = "$ " + iva.ToString("##.00", CultureInfo.InvariantCulture);

                    decimal totalneto = decimal.Parse(leer["totalgeneral"].ToString());
                    oSheet.get_Range("F" + i, "F" + i).Value2 = "$ " + totalneto.ToString("##.00", CultureInfo.InvariantCulture);

                    oSheet.get_Range("G" + i, "G" + i).Value2 = leer["status"].ToString();
                    oSheet.get_Range("H" + i, "H" + i).Value2 = leer["compro"].ToString();
                    oSheet.get_Range("I" + i, "I" + i).Value2 = estatuspedido;

                    oSheet.get_Range("A" + i, "I" + i).Font.Size = 9;
                    oSheet.get_Range("A" + i, "I" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    oSheet.get_Range("A" + i, "I" + i).EntireColumn.AutoFit();
                    i++;
                }
                conecta.CierraConexion();

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;
                oXL.UserControl = true;
                ////------ proceso para guardarlo y cerrarlo 
                //oXL.ActiveWorkbook.Close(true, filePath, Type.Missing); 
                //oXL.Quit();
                //System.Diagnostics.Process[] myProcesses;
                //myProcesses = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                //foreach (System.Diagnostics.Process instance in myProcesses)
                //{
                //    instance.CloseMainWindow();
                //    instance.Kill();
                //    instance.Close();
                //}
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

        public static void importaExistencias(string[,] Datos, int Total)
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
                int iNumQtrs = 3;

                bool bandera = false;

                oResizeRange = oSheet.get_Range("A1", "C1").get_Resize(Missing.Value, iNumQtrs);
                oSheet.get_Range("A1", "C1").Font.Bold = true;
                oSheet.get_Range("A1", "C1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A1", "C1").Merge(bandera);

                oSheet.Cells[1, 1] = "IMPORTACIÓN DE EXISTENCIAS";

                oSheet.Cells[3, 1] = "Clave";
                oSheet.Cells[3, 2] = "Productos";
                oSheet.Cells[3, 3] = "Existencia";


                oResizeRange = oSheet.get_Range("A3", "C3").get_Resize(Missing.Value, iNumQtrs);
                oResizeRange.Interior.ColorIndex = 23;
                oResizeRange.Font.ColorIndex = 2;

                oSheet.get_Range("A3", "C3").Font.Bold = true;
                oSheet.get_Range("A3", "C3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A3", "C3").EntireColumn.AutoFit();


                int contar = 0;

                string Formato = "0";

                for (int i = 4; i < (Total + 4); i++)
                {
                    if (Datos[contar, 0] == null) break;

                    oSheet.get_Range("A" + i, "A" + i).Value2 = Datos[contar, 0].ToString();
                    //oSheet.get_Range("A" + i, "A" + i).NumberFormat = Formato;

                    oSheet.get_Range("B" + i, "B" + i).Value2 = Datos[contar, 1].ToString();
                    oSheet.get_Range("C" + i, "C" + i).Value2 = Datos[contar, 2].ToString();
                    oSheet.get_Range("A" + i, "C" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    oSheet.get_Range("A" + i, "C" + i).EntireColumn.AutoFit();
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

        public static void importaPrecios(string[,] Datos, int Total)
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
                int iNumQtrs = 4;

                bool bandera = false;

                oResizeRange = oSheet.get_Range("A1", "D1").get_Resize(Missing.Value, iNumQtrs);
                oSheet.get_Range("A1", "D1").Font.Bold = true;
                oSheet.get_Range("A1", "D1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A1", "D1").Merge(bandera);

                oSheet.Cells[1, 1] = "IMPORTACIÓN DE PRECIOS";

                oSheet.Cells[3, 1] = "Clave";
                oSheet.Cells[3, 2] = "Productos";
                oSheet.Cells[3, 3] = "Precio público 1";
                oSheet.Cells[3, 3] = "Precio público 2";


                oResizeRange = oSheet.get_Range("A3", "D3").get_Resize(Missing.Value, iNumQtrs);
                oResizeRange.Interior.ColorIndex = 23;
                oResizeRange.Font.ColorIndex = 2;

                oSheet.get_Range("A3", "D3").Font.Bold = true;
                oSheet.get_Range("A3", "D3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A3", "D3").EntireColumn.AutoFit();


                int contar = 0;

                string Formato = "0";

                for (int i = 4; i < (Total + 4); i++)
                {
                    if (Datos[contar, 0] == null) break;

                    oSheet.get_Range("A" + i, "A" + i).Value2 = Datos[contar, 0].ToString();
                    //oSheet.get_Range("A" + i, "A" + i).NumberFormat = Formato;

                    oSheet.get_Range("B" + i, "B" + i).Value2 = Datos[contar, 1].ToString();
                    oSheet.get_Range("C" + i, "C" + i).Value2 = Datos[contar, 2].ToString();
                    oSheet.get_Range("D" + i, "D" + i).Value2 = Datos[contar, 2].ToString();
                    oSheet.get_Range("A" + i, "D" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    oSheet.get_Range("A" + i, "D" + i).EntireColumn.AutoFit();
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


        public static void RinformeAtencionMedica(string Consulta, string area, string Fecha)
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

                //filePath = oXL.GetSaveAsFilename("HUELLASEMPLEADOS", "Archivos de Excel (*.xlsx), *.xlsx", 1, "Guardar huellas empleados", Missing.Value).ToString(); 

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                oSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperLetter;

                //Format A1:D1 as bold, vertical alignment = center.
                //oSheet.get_Range("A1", "K1").Font.Background=System.Drawing.Color.Fuchsia;
                int iNumQtrs = 10;
                bool bandera = false;
                oResizeRange = oSheet.get_Range("A1", "J1").get_Resize(Missing.Value, iNumQtrs);
                oSheet.get_Range("A1", "J1").Font.Bold = true;
                oSheet.get_Range("A1", "J1").Font.Size = 11;
                oSheet.get_Range("A1", "J1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A1", "J1").Merge(bandera);

                oSheet.Cells[1, 1] = "INFORME DEL AREA " + area + "  EL DIA " + Fecha;

                //oSheet.get_Range("A1", "A1").Value2 = "LISTADO DE PROVEEDORES";


                //Add table headers going cell by cell.

                oSheet.Cells[3, 1] = "Ticket";
                oSheet.Cells[3, 2] = "Clave";
                oSheet.Cells[3, 3] = "Nombre";
                oSheet.Cells[3, 4] = "Expediente";
                oSheet.Cells[3, 5] = "Fecha";
                oSheet.Cells[3, 6] = "Hora Inicia";
                oSheet.Cells[3, 7] = "Estatus";
                oSheet.Cells[3, 8] = "Servicio";
                oSheet.Cells[3, 9] = "Emitio";
                oSheet.Cells[3, 10] = "Recibo de Pago";


                oResizeRange = oSheet.get_Range("A3", "J3").get_Resize(Missing.Value, iNumQtrs);
                oResizeRange.Interior.ColorIndex = 23;
                oResizeRange.Font.ColorIndex = 2;

                oSheet.get_Range("A3", "J3").Font.Bold = true;
                oSheet.get_Range("A3", "J3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A3", "J3").EntireColumn.AutoFit();
                int contar = 0;

                string Formato = "0";
                conectorSql conecta = new conectorSql();
                conectorSql conecta2 = new conectorSql();
                string Query = Consulta;
              
                int i = 4;
                SqlDataReader leer = conecta.RecordInfo(Query);
                while (leer.Read())
                {
                    string clave = leer["progresivo"].ToString();
                    oSheet.get_Range("A" + i, "A" + i).Value2 = clave;
                    oSheet.get_Range("B" + i, "B" + i).Value2 = leer["cvpaciente"].ToString();
                    oSheet.get_Range("C" + i, "C" + i).Value2 = leer["nombrepac"].ToString();
                    oSheet.get_Range("D" + i, "D" + i).Value2 = leer["numexpediente"].ToString();
                    oSheet.get_Range("E" + i, "E" + i).Value2 = leer["fecha"].ToString();
                    oSheet.get_Range("F" + i, "F" + i).Value2 = leer["horainicia"].ToString();
                    oSheet.get_Range("G" + i, "G" + i).Value2 = leer["estatus"].ToString();
                    oSheet.get_Range("H" + i, "H" + i).Value2 = leer["nombreservicio"].ToString();
                    oSheet.get_Range("I" + i, "I" + i).Value2 = leer["emite"].ToString();
                    oSheet.get_Range("J" + i, "J" + i).Value2 = leer["recibopago"].ToString();

                    oSheet.get_Range("A" + i, "J" + i).Font.Size = 10;
                    oSheet.get_Range("A" + i, "J" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    oSheet.get_Range("A" + i, "J" + i).EntireColumn.AutoFit();
                    i++;
                }
                conecta.CierraConexion();



    
                oXL.Visible = true;

                oXL.UserControl = true;

                ////------ proceso para guardarlo y cerrarlo 
                //oXL.ActiveWorkbook.Close(true, filePath, Type.Missing); 
                //oXL.Quit();
                //System.Diagnostics.Process[] myProcesses;
                //myProcesses = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                //foreach (System.Diagnostics.Process instance in myProcesses)
                //{
                //    instance.CloseMainWindow();
                //    instance.Kill();
                //    instance.Close();
                //}
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

