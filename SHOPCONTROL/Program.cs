using System;
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
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            // System.Timers.Timer timer = new System.Timers.Timer(300000);
            // timer.Elapsed += Timer_Elapsed;
            // timer.Start();

            var timer = new System.Threading.Timer(
                s => Application.Exit(), null, CalcMsToHour(21, 00, 00), Timeout.Infinite);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EntradaUsuario());

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

    }
}
