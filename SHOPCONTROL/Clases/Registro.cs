class Registro
    {

        public static bool CreateRegSHOPCONTROL()
        {
        /*
            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true);
            //RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);  OpenSubKey("SOFTWARE");

            key.CreateSubKey("BioStarSHOPCONTROL");
            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"Software\BioStarSHOPCONTROL", true);
            key2.CreateSubKey("CON");
            key2.CreateSubKey("CONCUERNAVACA");
            key2.CreateSubKey("CONMOLINA");
            key2.CreateSubKey("CONBELLASARTES");
        */
        return true;
        }

        public static bool WriteRegSHOPCONTROL(string Carpeta, string llave, string valor)
        {
            // RegistryKey registra =
            // Registry.LocalMachine.OpenSubKey(@"Software\BioStarSHOPCONTROL\" + Carpeta, true);
            // registra.SetValue(llave, valor);
            return true;
        }

        public static string ReadRegSHOPCONTROL(string Carpeta, string llave)
        {
            /*RegistryKey registra = 
                Registry.LocalMachine.OpenSubKey(@"Software\BioStarSHOPCONTROL\" + Carpeta, true);
                string valor = (string)registra.GetValue(llave);
            */
            return "";
        }


        public static bool WriteRegistreVB(string Carpeta, string llave, string valor)
        {
            /*
                RegistryKey registra =
                Registry.CurrentUser.OpenSubKey(@"Software\VB and VBA Program Settings\SHOPCONTROL\" + Carpeta, true);
                registra.SetValue(llave, valor);
            */
            return true;
        }

        public static string ReadRegistreVB(string Carpeta, string llave)
        {
            // RegistryKey registra = Registry.CurrentUser.OpenSubKey(@"Software\VB and VBA Program Settings\SHOPCONTROL\" + Carpeta, true);
            // string valor=(string)registra.GetValue(llave);
            return "";
        }

        #region Metdos para correr aplicaciones cuando se inicia windows

        public static bool WriteRun(string llave, string valor)
        {
           /* 
                RegistryKey runK = 
                Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                runK.SetValue(llave, valor);
            */
            return true;
        }

        public static string ReadRun(string llave)
        {
        /*
            RegistryKey runK =
            Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            string valor = (string)runK.GetValue(llave);
            return valor;
        */
        return "";
        }

        public static bool DeleteRun(string llave)
        {
            /*
                RegistryKey runK =
                Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                runK.DeleteValue(llave);
            */
           return true;
        }
        #endregion

    }

