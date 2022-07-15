using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHOPCONTROL.AccessTocken
{
    public partial class Access : Form
    {
        public Access()
        {
            InitializeComponent();
        }

        private void Ingresar_Click(object sender, EventArgs e)
        {
            if (licenseKey.Text == "1584-7894-5588-9899")
            {
                conectorSql conecta = new conectorSql();
                string Query = "";
                Query = "update Consecutivos set active=1";
                conecta.Excute(Query);
                conecta.CierraConexion();
                MessageBox.Show("Gracias, la licencia del software ha sido actualizada!");

                MessageBox.Show("Es necesario reiniciar el sistema");


                String process = Process.GetCurrentProcess().ProcessName;
                Process.Start("cmd.exe", "/c taskkill /F /IM " + process + ".exe /T");

            }
            else
            {
                MessageBox.Show("El número de licencia es incorrecto, favor de intentar nuevamente");

            }
        }

        private void Access_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }
    }
}
