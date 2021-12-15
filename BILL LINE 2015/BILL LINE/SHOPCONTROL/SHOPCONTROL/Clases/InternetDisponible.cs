using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Runtime.InteropServices; 

 public static  class InternetDisponible
    {

        //Importamos la funcion de wininet.dll, 
        //no hay que reinventar la rueda cuando se quiere verificar la conexión.
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(int Description, int ReservedValue);

        //Aqui se crea la funcion que utiliza la API
        public static bool IsConnectedToInternet()
        {
            int Desc = 0;
            return InternetGetConnectedState(Desc, 0);
        }
	
    }

