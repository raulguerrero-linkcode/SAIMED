using System;
using Excel = Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
public class ReportesNKB
    {

    public static void ReporteRecibos(string Fecha1, string Fecha2,string categ, string numpedido, string nom, bool Todas)
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
            int iNumQtrs = 7;
            bool bandera = false;
            oResizeRange = oSheet.get_Range("A1", "E1").get_Resize(Missing.Value, iNumQtrs);
            oSheet.get_Range("A1", "E1").Font.Bold = true;
            oSheet.get_Range("A1", "E1").Font.Size = 16;
            oSheet.get_Range("A1", "E1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            oSheet.get_Range("A1", "E1").Merge(bandera);

            oSheet.Cells[1, 1] = "INVENTARIO DE PRO/SERV " + DateTime.Now.ToString("dd/MM/yyyy");

            //oSheet.get_Range("A1", "A1").Value2 = "LISTADO DE PROVEEDORES";


            //Add table headers going cell by cell.

            oSheet.Cells[3, 1] = "Num. Recibo";
            oSheet.Cells[3, 2] = "Nombre Cliente";
            oSheet.Cells[3, 3] = "Categoria     ";
            oSheet.Cells[3, 4] = "Fecha   ";
            oSheet.Cells[3, 5] = "SubTotal  ";
            oSheet.Cells[3, 6] = "Descripcion        ";                           
            oSheet.Cells[3, 7] = "Clave Producto";



            oResizeRange = oSheet.get_Range("A3", "E3").get_Resize(Missing.Value, iNumQtrs);
            oResizeRange.Interior.ColorIndex = 23;
            oResizeRange.Font.ColorIndex = 2;

            oSheet.get_Range("A3", "G3").Font.Bold = true;
            oSheet.get_Range("A3", "G3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            oSheet.get_Range("A3", "G3").EntireColumn.AutoFit();
            int contar = 0;

            string Formato = "0";
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            string Query = "Select DetallesRecibos.numrecibo as clave, recibos.nombrerecibo as Nombrecliente,";
            Query = Query + " DetallesRecibos.fechacod,DetallesRecibos.cvproducto as cvpr, Cat_Categorias.descripcion as nomcat,";
            Query = Query + " DetallesRecibos.fecha as date, DetallesRecibos.precio as tol, DetallesRecibos.descripcion as nomdes";

            Query = Query + " from productos ";
            Query = Query + " inner join DetallesRecibos on DetallesRecibos.cvproducto=Productos.cvproducto ";
            Query = Query + " inner join Recibos on Recibos.numrecibo=DetallesRecibos.numrecibo ";
            Query = Query + " inner join Cat_Categorias on Cat_Categorias.idcategoria=productos.categoria ";

            Query = Query + " where DetallesRecibos.cvproducto <>''";

            if (Todas == false) Query = Query + " and DetallesRecibos.fechacod between '" + Fecha1 + "' and '" + Fecha2 + "'";
            if (numpedido != "") Query = Query + " and DetallesRecibos.numrecibo='" + numpedido + "'";
            if (nom != "") Query = Query + " and Productos.nombre like '%" + nom+ "%'";

            if (categ != "") Query = Query + " and  categoria='" + categ + "'";


            Query = Query + " order by DetallesRecibos.fecha asc";

            int i = 4;
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string clave = leer["clave"].ToString();
                //string estatuspedido = leer["estatusrecibo"].ToString();

                oSheet.get_Range("A" + i, "A" + i).Value2 = clave;
                oSheet.get_Range("B" + i, "B" + i).Value2 = leer["NombreCliente"].ToString();
                oSheet.get_Range("C" + i, "C" + i).Value2 = leer["nomcat"].ToString();
                oSheet.get_Range("D" + i, "D" + i).Value2 = leer["date"].ToString();
                decimal total = decimal.Parse(leer["tol"].ToString());
                oSheet.get_Range("E" + i, "E" + i).Value2 = "$ " + total.ToString("##.00", CultureInfo.InvariantCulture);

                oSheet.get_Range("F" + i, "F" + i).Value2 = leer["nomdes"].ToString();
                oSheet.get_Range("G" + i, "G" + i).Value2 = leer["cvpr"].ToString();

                oSheet.get_Range("A" + i, "G" + i).Font.Size = 9;
                oSheet.get_Range("A" + i, "G" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A" + i, "G" + i).EntireColumn.AutoFit();
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



    public static void ReportePacientes()
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
            int iNumQtrs = 7;
            bool bandera = false;
            oResizeRange = oSheet.get_Range("A1", "AO1").get_Resize(Missing.Value, iNumQtrs);
            oSheet.get_Range("A1", "AN1").Font.Bold = true;
            oSheet.get_Range("A1", "AN1").Font.Size = 16;
            oSheet.get_Range("A1", "AN1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            oSheet.get_Range("A1", "AN1").Merge(bandera);

            oSheet.Cells[1, 1] = "Listado de Pacientes actualizado al " + DateTime.Now.ToString("dd/MM/yyyy");

            //oSheet.get_Range("A1", "A1").Value2 = "LISTADO DE PROVEEDORES";


            //Add table headers going cell by cell.

            oSheet.Cells[3, 1] = "clave";
            oSheet.Cells[3, 2] = "NOMBRE";
            oSheet.Cells[3, 3] = "APATERNO";
            oSheet.Cells[3, 4] = "AMATERNO";
            oSheet.Cells[3, 5] = "GENERO";
            oSheet.Cells[3, 6] = "ESCOLARIDAD";
            oSheet.Cells[3, 7] = "EMAIL";
            oSheet.Cells[3, 8] = "EDAD";
            oSheet.Cells[3, 9] = "ECivil";
            oSheet.Cells[3, 10] = "NoHijos";
            oSheet.Cells[3, 11] = "OCUPACION";
            oSheet.Cells[3, 12] = "TELEFONO";
            oSheet.Cells[3, 13] = "CALLE";
            oSheet.Cells[3, 14] = "NoCalle";
            oSheet.Cells[3, 15] = "CP";
            oSheet.Cells[3, 16] = "COLONIA";
            oSheet.Cells[3, 17] = "MUNICIPIO";
            oSheet.Cells[3, 18] = "CIUDAD";
            oSheet.Cells[3, 19] = "ESTADO";
            oSheet.Cells[3, 20] = "Pregunta1";
            oSheet.Cells[3, 21] = "Pregunta2";
            oSheet.Cells[3, 22] = "Pregunta3";
            oSheet.Cells[3, 23] = "RecibeAvisos";
            oSheet.Cells[3, 24] = "NoExpediente";
            oSheet.Cells[3, 25] = "SERVICIO";
            oSheet.Cells[3, 26] = "MEDICO";
            oSheet.Cells[3, 27] = "TURNO";
            oSheet.Cells[3, 28] = "OBSERVACIONES";
            oSheet.Cells[3, 29] = "FECHA";
            oSheet.Cells[3, 30] = "FCOD";
            oSheet.Cells[3, 31] = "LUGARNAC";
            oSheet.Cells[3, 32] = "FECHANAC";
            oSheet.Cells[3, 33] = "STATUS";
            oSheet.Cells[3, 34] = "CELULAR";
            oSheet.Cells[3, 35] = "EMAIL2";
            oSheet.Cells[3, 36] = "expdental";
            oSheet.Cells[3, 37] = "expgineco";
            oSheet.Cells[3, 38] = "expoftamolgo";
            oSheet.Cells[3, 39] = "curp";
            // oSheet.Cells[3, 40] = "NomCompleto";

            oResizeRange = oSheet.get_Range("A3", "AN3").get_Resize(Missing.Value, iNumQtrs);      
            oResizeRange.Interior.ColorIndex = 23;
            oResizeRange.Font.ColorIndex = 2;

            oSheet.get_Range("A3", "AO3").Font.Bold = true;
            oSheet.get_Range("A3", "AO3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            oSheet.get_Range("A3", "AO3").EntireColumn.AutoFit();
            int contar = 0;

            string Formato = "0";
            conectorSql conecta = new conectorSql();
            conectorSql conecta2 = new conectorSql();
            string Query = "Select top 100 clave,	NOMBRE,	APATERNO,	AMATERNO,	GENERO,	ESCOLARIDAD,	EMAIL,	EDAD,	ECivil,	NoHijos,	OCUPACION,	TELEFONO,	CALLE,	NoCalle,	CP,	COLONIA,	MUNICIPIO,	CIUDAD,	ESTADO,	Pregunta1,	Pregunta2,	Pregunta3,	RecibeAvisos,	NoExpediente,	SERVICIO,	MEDICO,	TURNO,	OBSERVACIONES,	FECHA,	FCOD,	LUGARNAC,	FECHANAC,	STATUS,	CELULAR,	EMAIL2,	expdental,	expgineco,	expoftamolgo,	curp from pacientes;";
            
            int i = 4;
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string clave = leer["clave"].ToString();
                //string estatuspedido = leer["estatusrecibo"].ToString();

                oSheet.get_Range("A" + i, "A" + i).Value2 = clave;
                oSheet.get_Range("B" + i, "B" + i).Value2 = leer["NOMBRE"].ToString();
                oSheet.get_Range("C" + i, "C" + i).Value2 = leer["APATERNO"].ToString();
                oSheet.get_Range("D" + i, "D" + i).Value2 = leer["AMATERNO"].ToString();
                oSheet.get_Range("E" + i, "E" + i).Value2 = leer["GENERO"].ToString();
                oSheet.get_Range("F" + i, "F" + i).Value2 = leer["ESCOLARIDAD"].ToString();
                oSheet.get_Range("G" + i, "G" + i).Value2 = leer["EMAIL"].ToString();
                oSheet.get_Range("H" + i, "H" + i).Value2 = leer["EDAD"].ToString();
                oSheet.get_Range("I" + i, "I" + i).Value2 = leer["ECivil"].ToString();
                oSheet.get_Range("J" + i, "J" + i).Value2 = leer["NoHijos"].ToString();
                oSheet.get_Range("K" + i, "K" + i).Value2 = leer["OCUPACION"].ToString();
                oSheet.get_Range("L" + i, "L" + i).Value2 = leer["TELEFONO"].ToString();
                oSheet.get_Range("M" + i, "M" + i).Value2 = leer["CALLE"].ToString();
                oSheet.get_Range("N" + i, "N" + i).Value2 = leer["NoCalle"].ToString();
                oSheet.get_Range("O" + i, "O" + i).Value2 = leer["CP"].ToString();
                oSheet.get_Range("P" + i, "P" + i).Value2 = leer["COLONIA"].ToString();
                oSheet.get_Range("Q" + i, "Q" + i).Value2 = leer["MUNICIPIO"].ToString();
                oSheet.get_Range("R" + i, "R" + i).Value2 = leer["CIUDAD"].ToString();
                oSheet.get_Range("S" + i, "S" + i).Value2 = leer["ESTADO"].ToString();
                oSheet.get_Range("T" + i, "T" + i).Value2 = leer["Pregunta1"].ToString();
                oSheet.get_Range("U" + i, "U" + i).Value2 = leer["Pregunta2"].ToString();
                oSheet.get_Range("V" + i, "V" + i).Value2 = leer["Pregunta3"].ToString();
                oSheet.get_Range("W" + i, "W" + i).Value2 = leer["RecibeAvisos"].ToString();
                oSheet.get_Range("X" + i, "X" + i).Value2 = leer["NoExpediente"].ToString();
                oSheet.get_Range("Y" + i, "Y" + i).Value2 = leer["SERVICIO"].ToString();
                oSheet.get_Range("Z" + i, "Z" + i).Value2 = leer["MEDICO"].ToString();
                oSheet.get_Range("AA" + i, "AA" + i).Value2 = leer["TURNO"].ToString();
                oSheet.get_Range("AB" + i, "AB" + i).Value2 = leer["OBSERVACIONES"].ToString();
                oSheet.get_Range("AC" + i, "AC" + i).Value2 = leer["FECHA"].ToString();
                oSheet.get_Range("AD" + i, "AD" + i).Value2 = leer["FCOD"].ToString();
                oSheet.get_Range("AE" + i, "AE" + i).Value2 = leer["LUGARNAC"].ToString();
                oSheet.get_Range("AF" + i, "AF" + i).Value2 = leer["FECHANAC"].ToString();
                oSheet.get_Range("AG" + i, "AG" + i).Value2 = leer["STATUS"].ToString();
                oSheet.get_Range("AH" + i, "AH" + i).Value2 = leer["CELULAR"].ToString();
                oSheet.get_Range("AI" + i, "AI" + i).Value2 = leer["EMAIL2"].ToString();
                oSheet.get_Range("AJ" + i, "AJ" + i).Value2 = leer["expdental"].ToString();
                oSheet.get_Range("AK" + i, "AK" + i).Value2 = leer["expgineco"].ToString();
                oSheet.get_Range("AL" + i, "AL" + i).Value2 = leer["expoftamolgo"].ToString();
                oSheet.get_Range("AM" + i, "AM" + i).Value2 = leer["curp"].ToString();
                // oSheet.get_Range("AN" + i, "AN" + i).Value2 = leer["NomCompleto"].ToString();

                
                oSheet.get_Range("A" + i, "AO" + i).Font.Size = 9;
                oSheet.get_Range("A" + i, "AO" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A" + i, "AO" + i).EntireColumn.AutoFit();
                i++;
            }
            conecta.CierraConexion();

            //Make sure Excel is visible and give the user control
            //of Microsoft Excel's lifetime.
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
   
        public static void RBusquedaProductos(string nombre, string categoria,string tipos, string marca, string clavep, string unidad, string porsurtir)
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
                int iNumQtrs = 7;
                bool bandera = false;
                oResizeRange = oSheet.get_Range("A1", "G1").get_Resize(Missing.Value, iNumQtrs);
                oSheet.get_Range("A1", "G1").Font.Bold = true;
                oSheet.get_Range("A1", "G1").Font.Size = 16;
                oSheet.get_Range("A1", "G1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A1", "G1").Merge(bandera);

                oSheet.Cells[1, 1] = "LISTA DE PRODUCTOS / SERVICIOS  DE LA FECHA " + DateTime.Now.ToString("dd/MM/yyyy");

                //oSheet.get_Range("A1", "A1").Value2 = "LISTADO DE PROVEEDORES";


                //Add table headers going cell by cell.

                oSheet.Cells[3, 1] = "Clave";
                oSheet.Cells[3, 2] = "Nombre";
                oSheet.Cells[3, 3] = "Categoría";
                oSheet.Cells[3, 4] = "Tipo";
                oSheet.Cells[3, 5] = "Unidad";
                oSheet.Cells[3, 6] = "Existencia";
            //oSheet.Cells[3, 6] = "Minimo"; //                //MODIF POR JOSE29-11-2019
            //oSheet.Cells[3, 7] = "Maximo";//                 //MODIF POR JOSE29-11-2019
            //oSheet.Cells[3, 8] = "Descripción";             //MODIF POR JOSE29-11-2019
            //oSheet.Cells[3, 9] = "Precio Distribuidor";     //MODIF POR JOSE29-11-2019
            oSheet.Cells[3, 7] = "Precio Publico 1";
            //oSheet.Cells[3, 11] = "Precio Publico 2";       //MODIF POR JOSE29-11-2019
            //oSheet.Cells[3, 12] = "Precio Publico 3";       //MODIF POR JOSE29-11-2019
            //oSheet.Cells[3, 13] = "Fecha de MOdificación"; //MODIF POR JOSE29-11-2019

            oResizeRange = oSheet.get_Range("A3", "E3").get_Resize(Missing.Value, iNumQtrs);
                oResizeRange.Interior.ColorIndex = 23;
                oResizeRange.Font.ColorIndex = 2;

                oSheet.get_Range("A3", "G3").Font.Bold = true;
                oSheet.get_Range("A3", "G3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A3", "G3").EntireColumn.AutoFit();
                int contar = 0;

                string Formato = "0";
                conectorSql conecta = new conectorSql();
                conectorSql conecta2 = new conectorSql();
            //string Query = "Select * from Productos where cvproducto<>''"; //MODIF POR JOSE29-11-2019
            string Query = "Select cvproducto,nombre,categoria,unidad,cantidad";
            Query = Query + ",Cat_Categorias.descripcion as nomcategoria, Cat_tipos.descripcion as nomtipo";
            Query = Query + " from productos ";
            Query = Query + " inner join Cat_Categorias on Cat_Categorias.idcategoria=productos.categoria"; //MODIF POR JOSE 04-12-2019
            Query = Query + " inner join Cat_tipos on Cat_tipos.idtipo = Productos.idtipo";  //MODIF POR JOSE 04-12-2019
            Query = Query + " where cvproducto<>'' ";  //MODIF POR JOSE29-11-2019
                
                if (nombre!= "") Query = Query + " and nombre like '%" + nombre + "%'";
                if (categoria!= "") Query = Query + " and categoria='" + categoria+ "'";
                if (tipos != "") Query = Query + "and Cat_tipos.idtipo='" + tipos + "'";
                if (clavep != "") Query = Query + " and cvproducto='" + clavep + "'";
                if (marca != "") Query = Query + " and marca='" + marca+ "'";
                if (unidad != "") Query = Query + " and unidad='" + unidad+ "'";
                if (porsurtir == "SI")  Query = Query + " and cantidad<=(minimo+2)";

                Query = Query + " order by Cat_Categorias.descripcion asc, nombre asc "; //" order by nombre desc ";  //MODIF POR JOSE29-11-2019
                int i = 4;
                SqlDataReader leer = conecta.RecordInfo(Query); 
            
                while (leer.Read())
                {
                
                    string clave=leer["cvproducto"].ToString();
                    oSheet.get_Range("A" + i, "A" + i).Value2 = clave;
                    oSheet.get_Range("B" + i, "B" + i).Value2 = leer["nombre"].ToString();
                    oSheet.get_Range("C" + i, "C" + i).Value2 = leer["nomcategoria"].ToString();
                    oSheet.get_Range("D" + i, "D" + i).Value2 = leer["nomtipo"].ToString();
                    oSheet.get_Range("E" + i, "E" + i).Value2 = leer["unidad"].ToString();

                    oSheet.get_Range("F" + i, "F" + i).Value2 = leer["cantidad"].ToString();
                //oSheet.get_Range("F" + i, "F" + i).Value2 = leer["minimo"].ToString();        //MODIF POR JOSE29-11-2019
                //oSheet.get_Range("G" + i, "G" + i).Value2 = leer["maximo"].ToString();        //MODIF POR JOSE29-11-2019
                //oSheet.get_Range("H" + i, "H" + i).Value2 = leer["descripcion"].ToString();  //MODIF POR JOSE29-11-2019

                string consulta = "Select * from ListaPrecios where cvproducto='" + clave + "'";
                    SqlDataReader leer2 = conecta2.RecordInfo(consulta);
                    while (leer2.Read())
                    {
                    //decimal distribuidor = decimal.Parse(leer2["distribuidor"].ToString()); //MODIF POR JOSE29-11-2019
                    decimal publico1 = decimal.Parse(leer2["publico1"].ToString());
                    //decimal publico2 = decimal.Parse(leer2["publico2"].ToString());   //MODIF POR JOSE29-11-2019
                    //decimal publico3 = decimal.Parse(leer2["publico3"].ToString());   //MODIF POR JOSE29-11-2019

                    //oSheet.get_Range("F" + i, "F" + i).Value2 =distribuidor.ToString("##.00", CultureInfo.InvariantCulture);  //MODIF POR JOSE29-11-2019
                    oSheet.get_Range("G" + i, "G" + i).Value2 =publico1.ToString("##.00", CultureInfo.InvariantCulture);
                    //oSheet.get_Range("K" + i, "K" + i).Value2 = publico2.ToString("##.00", CultureInfo.InvariantCulture); //MODIF POR JOSE29-11-2019
                    //oSheet.get_Range("L" + i, "L" + i).Value2 = publico3.ToString("##.00", CultureInfo.InvariantCulture);  //MODIF POR JOSE29-11-2019
                }
                conecta2.CierraConexion();

                    //oSheet.get_Range("H" + i, "G" + i).Value2 = leer["fechaModifica"].ToString(); //MODIF POR JOSE29-11-2019

                    oSheet.get_Range("A" + i, "G" + i).Font.Size = 9;
                    oSheet.get_Range("A" + i, "G" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    oSheet.get_Range("A" + i, "G" + i).EntireColumn.AutoFit();
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


    public static void ReporteIngresosUnidad(string Fecha1, string Fecha2)
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
            oXL.Visible = true;

            //Get a new workbook.
            oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;



            //Format A1:D1 as bold, vertical alignment = center.
            //oSheet.get_Range("A1", "K1").Font.Background=System.Drawing.Color.Fuchsia;
            int iNumQtrs = 7;
            bool bandera = false;
            oResizeRange = oSheet.get_Range("A1", "E1").get_Resize(Missing.Value, iNumQtrs);
            oSheet.get_Range("A1", "E1").Font.Bold = true;
            oSheet.get_Range("A1", "E1").Font.Size = 16;
            oSheet.get_Range("A1", "E1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            oSheet.get_Range("A1", "E1").Merge(bandera);

            oSheet.Cells[1, 1] = "Reporte de Ingresos por unidad "; //  + DateTime.Now.ToString("dd/MM/yyyy");
                                                                    //Add table headers going cell by cell.

            oSheet.Cells[3, 1] = "Fecha";
            oSheet.Cells[3, 2] = "TOTAL";
            oSheet.Cells[3, 3] = "DENTAL";
            oSheet.Cells[3, 4] = "ENDODONCIA";
            oSheet.Cells[3, 5] = "FARMACIA";
            oSheet.Cells[3, 6] = "GINECOLOGIA";
            oSheet.Cells[3, 7] = "INVENTARIO";
            oSheet.Cells[3, 8] = "LABORATORIO";
            oSheet.Cells[3, 9] = "MXILOFACIAL";
            oSheet.Cells[3, 10] = "OFTALMOLOGIA";
            oSheet.Cells[3, 11] = "OPTICA";
            oSheet.Cells[3, 12] = "ORTODONCIA";
            oSheet.Cells[3, 13] = "ORTOPEDIA_ORTODONCIA";
            oSheet.Cells[3, 14] = "PAPANICOLAO";
            oSheet.Cells[3, 15] = "PAPELERIA";
            oSheet.Cells[3, 16] = "PROTESIS";
            oSheet.Cells[3, 17] = "RAYOS_X_DENTAL";
            oSheet.Cells[3, 18] = "SIN_CATEGORIA";
            oSheet.Cells[3, 19] = "TIENDA";
            oSheet.Cells[3, 20] = "ULTRASONIDO";



            oResizeRange = oSheet.get_Range("A3", "T3").get_Resize(Missing.Value, iNumQtrs);
            oResizeRange.Interior.ColorIndex = 23;
            oResizeRange.Font.ColorIndex = 2;

            oSheet.get_Range("A3", "T3").Font.Bold = true;
            oSheet.get_Range("A3", "T3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            oSheet.get_Range("A3", "T3").EntireColumn.AutoFit();
            int contar = 0;

            string Formato = "0";
            conectorSql conecta = new conectorSql();

            string Query = "SELECT * FROM [CEPAMM].[dbo].[v_ingresos_area] where Fecha between '" + Fecha1 + "' and '" + Fecha2  + "' order by Fecha asc ";

            int i = 4;
            SqlDataReader leer = conecta.RecordInfo(Query);
            while (leer.Read())
            {
                string clave = leer["FECHA"].ToString();
                //string estatuspedido = leer["estatusrecibo"].ToString();
                
                oSheet.get_Range("A" + i, "A" + i).Value2 = clave;

                oSheet.get_Range("B" + i, "B" + i).Value2 = "$ " + decimal.Parse(leer["TOTAL"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("C" + i, "C" + i).Value2 = "$ " + decimal.Parse(leer["DENTAL"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("D" + i, "D" + i).Value2 = "$ " + decimal.Parse(leer["ENDODONCIA"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("E" + i, "E" + i).Value2 = "$ " + decimal.Parse(leer["FARMACIA"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("F" + i, "F" + i).Value2 = "$ " + decimal.Parse(leer["GINECOLOGIA"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("G" + i, "G" + i).Value2 = "$ " + decimal.Parse(leer["INVENTARIO"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("H" + i, "H" + i).Value2 = "$ " + decimal.Parse(leer["LABORATORIO"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("I" + i, "I" + i).Value2 = "$ " + decimal.Parse(leer["MXILOFACIAL"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("J" + i, "J" + i).Value2 = "$ " + decimal.Parse(leer["OFTALMOLOGIA"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("K" + i, "K" + i).Value2 = "$ " + decimal.Parse(leer["OPTICA"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("L" + i, "L" + i).Value2 = "$ " + decimal.Parse(leer["ORTODONCIA"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("M" + i, "M" + i).Value2 = "$ " + decimal.Parse(leer["ORTOPEDIA_ORTODONCIA"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("N" + i, "N" + i).Value2 = "$ " + decimal.Parse(leer["PAPANICOLAO"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("O" + i, "O" + i).Value2 = "$ " + decimal.Parse(leer["PAPELERIA"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("P" + i, "P" + i).Value2 = "$ " + decimal.Parse(leer["PROTESIS"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("Q" + i, "Q" + i).Value2 = "$ " + decimal.Parse(leer["RAYOS_X_DENTAL"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("R" + i, "R" + i).Value2 = "$ " + decimal.Parse(leer["SIN_CATEGORIA"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("S" + i, "S" + i).Value2 = "$ " + decimal.Parse(leer["TIENDA"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);
                oSheet.get_Range("T" + i, "T" + i).Value2 = "$ " + decimal.Parse(leer["ULTRASONIDO"].ToString()).ToString("##.00", CultureInfo.InvariantCulture);

                oSheet.get_Range("A" + i, "T" + i).Font.Size = 9;
                oSheet.get_Range("A" + i, "T" + i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oSheet.get_Range("A" + i, "T" + i).EntireColumn.AutoFit();
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



}

