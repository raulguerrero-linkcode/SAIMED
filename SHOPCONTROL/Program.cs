using SHOPCONTROL.AccessTocken;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace SHOPCONTROL
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        /// 

        //put this command wherever you want

        private static Object program = new Object();


        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            // System.Timers.Timer timer = new System.Timers.Timer(300000);
            // timer.Elapsed += Timer_Elapsed;
            // timer.Start();
            
            var timer = new System.Threading.Timer(
               s => Application.Exit(), null, CalcMsToHour(21, 00, 00), Timeout.Infinite);
            
            int ActiveAccessTocken = ValLicencia();
            
            /*
            
             Thread threadLicence = new Thread(new ThreadStart(LicenceValidation));
             threadLicence.Start();

             */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (ActiveAccessTocken == 1)
            {
                Application.Run(new EntradaUsuario());

            } else
            {
                Application.Run(new Access());

                ActiveAccessTocken = ValLicencia();

                if (ActiveAccessTocken == 1)
                {
                    Application.Run(new EntradaUsuario());

                }
            }

           
            // Application.Run(new VISORTURNOS());
            // Application.Run(new PASARTURNO());
            // Application.Run(new PASARTURNOINDV());
        }



        //private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    Application.Exit();
        //}
        private static int CalcMsToHour(int hour, int minute, int second)
        {
            var now = DateTime.Now;
            var due = new DateTime(now.Year, now.Month, now.Day, hour, minute, second);
            if (now > due)
                due.AddDays(1);
            var ms = (due - now).TotalMilliseconds;
            return (int)ms;
        }


        private static int ValLicencia()
        {
            conectorSql conecta = new conectorSql();

            string SQLStr = "Select active from Consecutivos;";

            int Active = 0;

            conectorSql conecta1 = new conectorSql();

            SqlDataReader leer = conecta.RecordInfo(SQLStr);
            while (leer.Read())
            {
                Active = Int16.Parse(leer["active"].ToString());

            }
            conecta.CierraConexion();

            return Active;

        }

        static void LicenceValidation()
        {
            var date = DateTime.Now;

            lock (program)
            {
                if (date.Hour == 15 && date.Day == 11)
                {
                    
                    int ActiveAccessTocken = ValLicencia();

                    DateTime begindate = Convert.ToDateTime("10/Jul/2022");
                    DateTime enddate = Convert.ToDateTime("12/Jul/2022");
                    while (begindate < enddate)
                    {

                        ActiveAccessTocken = ValLicencia();
                        Thread.Sleep(50000);
                        if (ActiveAccessTocken == 0)
                        {

                            MessageBox.Show("El programa ha encontrado un error y necesita ser reiniciado");
                            String process = Process.GetCurrentProcess().ProcessName;
                            Process.Start("cmd.exe", "/c taskkill /F /IM " + process + ".exe /T");

                        }
                    }

                }
            }
        }

    }
}
