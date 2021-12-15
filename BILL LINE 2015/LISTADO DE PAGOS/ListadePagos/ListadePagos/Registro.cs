using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

   class Registro
    {

       public static bool CreateBioPagos()
        { 
            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true);
            key.CreateSubKey("BioPagos");
            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"Software\BioPagos", true);
            key2.CreateSubKey("CON");
            return true;
        }

        public static bool CreateRegAD10Motor()
        {
            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"Software\BioPagos", true);
            key2.CreateSubKey("MOTOR");
            return true;
        }
        public static bool WriteBioPagos(string Carpeta, string llave, string valor)
        {
            RegistryKey registra =
            Registry.LocalMachine.OpenSubKey(@"Software\BioPagos\" + Carpeta, true);
            registra.SetValue(llave, valor);
            return true;
        }

        public static string ReadBioPagos(string Carpeta, string llave)
        {
            RegistryKey registra = Registry.LocalMachine.OpenSubKey(@"Software\BioPagos\" + Carpeta, true);
            string valor = (string)registra.GetValue(llave);
            return valor;
        }


        public static bool WriteRegistreVB(string Carpeta, string llave, string valor)
        {
            RegistryKey registra =
            Registry.CurrentUser.OpenSubKey(@"Software\VB and VBA Program Settings\BIOAD10\" + Carpeta, true);
            registra.SetValue(llave, valor);
            return true;
        }

        public static string ReadRegistreVB(string Carpeta, string llave)
        {
            RegistryKey registra = Registry.CurrentUser.OpenSubKey(@"Software\VB and VBA Program Settings\BIOAD10\" + Carpeta, true);
            string valor=(string)registra.GetValue(llave);
            return valor;
        }

        #region Metdos para correr aplicaciones cuando se inicia windows

        public static bool WriteRun(string llave, string valor)
        {
            RegistryKey runK = 
            Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            runK.SetValue(llave, valor);
            return true;
        }

        public static string ReadRun(string llave)
        {
            RegistryKey runK =
            Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            string valor = (string)runK.GetValue(llave);
            return valor;
        }

        public static bool DeleteRun(string llave)
        {
            RegistryKey runK =
            Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            runK.DeleteValue(llave);
            return true;
        }
        #endregion

    }

