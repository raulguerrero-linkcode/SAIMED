using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

public class Licenciamiento
    {

    public string PreLicenciaBillLine()
    {
        // Creamos el objeto random, simplemente
       // Random r = new Random();
        int[] ArrayNumeros = new int[21];
        // O para los más puritamos, 
        // añadimos un plus de aleatoriedad eligiendo una semilla con cierto grado de pseudoaleatoriedad 
        Random r = new Random(DateTime.Now.Millisecond);
        for (int i = 0; i < 20; i++)
        {
            int aleat1 = r.Next(i+10, 99);
            string cad = aleat1.ToString();
            ArrayNumeros[i] = aleat1;
        }

        int contador=1;
        string cadena="";
        for (int i = 0; i < 20; i++)
        {
            if (contador >= 20) break;

            cadena =  cadena +  ArrayNumeros[i].ToString();
            cadena = cadena + ArrayNumeros[contador].ToString();
            contador = contador + 2;
            cadena = cadena + "-";
        }
        cadena = cadena.Substring(0, cadena.Length - 1);
        return cadena;
  
    }

    public string LicenciaFinal(int Cantidad)
    {      
        Random r = new Random(DateTime.Now.Millisecond);
        int aleat1 = r.Next(10, 99);
        string cad = aleat1.ToString();
        cad = aleat1.ToString();
        int  aleat2= r.Next(10, 99);
        cad = aleat1.ToString() + aleat2.ToString() + "-";

        string LETRA = "";

        if (Cantidad==25) LETRA = "FHZ"; // 25 licencia.
        if (Cantidad == 50) LETRA = "MHW"; //50
        if (Cantidad == 100) LETRA = "KHQ"; //100
        if (Cantidad == 300) LETRA = "DHQ"; //300
        if (Cantidad == 500) LETRA = "GHF"; //500
        if (Cantidad == 1000) LETRA = "8MH"; //1000
        if (Cantidad == 1500) LETRA = "OH9"; //1500 +80
        if (Cantidad == 2000) LETRA = "5HK"; //2000
        if (Cantidad == 3000) LETRA = "RTH"; //3000W

        cad = cad + LETRA;      
        return cad;
    }


    public int SaberCuantosTimbres(string Cadena)
    {
        if (Cadena == "") return 0;
        string [] Matrizcad=Cadena.Split('-');


        int acumulador = 0;
        for (int i = 0; i < 1; i++)
        {
            string LETRA = Matrizcad[1].Substring(i, 1);
            switch (LETRA)
            {
                case "F":
                    acumulador = acumulador + 25;
                    break;
                case "H":
                    acumulador = acumulador + 0;
                    break;

                case "M":
                    acumulador = acumulador + 50;
                    break;

                case "K":
                    acumulador = acumulador + 100;
                    break;

                case "D":
                    acumulador = acumulador + 300;
                    break;

                case "G":
                    acumulador = acumulador + 500;
                    break;

                case "8":
                    acumulador = acumulador + 1000;
                    break;
                case "5":
                    acumulador = acumulador + 2000;
                    break;
                case "O":
                    acumulador = acumulador + 1500;
                    break;
                case "R":
                    acumulador = acumulador + 3000;
                    break;


                case "Z":
                    acumulador = acumulador + 5;
                    break;
                case "W":
                    acumulador = acumulador + 10;
                    break;
                case "Q":
                    acumulador = acumulador + 20;
                    break;

                case "T":
                    acumulador = acumulador + 150;
                    break;
                case "9":
                    acumulador = acumulador + 80;
                    break;
                default:
                    break;
            }
        }
        return acumulador;
    }

    public int CuantosTimbresGeneral(string Cadena)
    {
        if (Cadena == "") return 0;
        string[] Matrizcad = Cadena.Split('-');


        int acumulador = 0;
        for (int i = 0; i < 2; i++)
        {
            string LETRA = Matrizcad[1].Substring(i, 1);
            switch (LETRA)
            {
                case "F":
                    acumulador = acumulador + 25;
                    break;
                case "H":
                    acumulador = acumulador + 0;
                    break;

                case "M":
                    acumulador = acumulador + 50;
                    break;

                case "K":
                    acumulador = acumulador + 100;
                    break;

                case "D":
                    acumulador = acumulador + 300;
                    break;

                case "G":
                    acumulador = acumulador + 500;
                    break;

                case "8":
                    acumulador = acumulador + 1000;
                    break;
                case "5":
                    acumulador = acumulador + 2000;
                    break;
                case "O":
                    acumulador = acumulador + 1500;
                    break;
                case "R":
                    acumulador = acumulador + 3000;
                    break;


                case "Z":
                    acumulador = acumulador + 5;
                    break;
                case "W":
                    acumulador = acumulador + 10;
                    break;
                case "Q":
                    acumulador = acumulador + 20;
                    break;

                case "T":
                    acumulador = acumulador + 150;
                    break;
                case "9":
                    acumulador = acumulador + 80;
                    break;
                default:
                    break;
            }
        }
        return acumulador;
    }
    }
