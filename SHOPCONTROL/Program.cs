using System;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EntradaUsuario());

            // Application.Run(new VISORTURNOS());
            // Application.Run(new PASARTURNO());
            // Application.Run(new PASARTURNOINDV());
        }
    }
}
