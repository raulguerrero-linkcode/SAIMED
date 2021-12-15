using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Data.SqlClient;

namespace SHOPCONTROL.HistorialClinica
{
    public partial class CamaraWeb : Form
    {
        public CamaraWeb()
        {
            InitializeComponent();
            BuscarDispositivos();
        }
        private bool ExistenDispositivos = false;
        private FilterInfoCollection DispositivosDeVideo;
        private VideoCaptureDevice FuenteDeVideo = null;

        private void CamaraWeb_Load(object sender, EventArgs e)
        {
  
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GuardarFotoCamara(label1.Text.Trim());   
        }

        public bool GuardarFotoCamara( string cvpaciente)
        {

            if (pbFotoUser.Image != null)
            {
                
                string Query = "";
                bool existereg;
                System.Drawing.Image i = pbFotoUser.Image;
                MemoryStream m = new MemoryStream();

                i.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imagenDatos = m.ToArray();
                m.Close();

                string numControl = cvpaciente;

                conectorSql mycon = new conectorSql();

                // primero elimina la foto anterior si tiene
                Query = "Delete from FotosPacientes where NoExpediente='" + cvpaciente + "'";
                existereg = mycon.Excute(Query);

                string sql = "insert into FotosPacientes(NoExpediente, foto)";
                sql += " Values(@NFolio, @Imagen)";

                mycon.Abrirconexion();
                SqlCommand SqlCom = new SqlCommand(sql, mycon.con);

                SqlCom.Parameters.Add("@NFolio", System.Data.SqlDbType.NVarChar, 20);
                SqlCom.Parameters["@NFolio"].Value = cvpaciente;
                SqlCom.Parameters.Add("@Imagen", System.Data.SqlDbType.Image);
                SqlCom.Parameters["@Imagen"].Value = imagenDatos;

                SqlCom.ExecuteNonQuery();
                mycon.CierraConexion();

                if (FuenteDeVideo.IsRunning)
                {
                    TerminarFuenteDeVideo();
                    btnIniciar.Text = "Iniciar";
                    cboDispositivos.Enabled = true;
                }
                MessageBox.Show("Se guardo correctamente la foto al paciente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.Dispose();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CargarDispositivos(FilterInfoCollection Dispositivos)
        {
            for (int i = 0; i < Dispositivos.Count; i++)
                cboDispositivos.Items.Add(Dispositivos[i].Name.ToString()); //cboDispositivos es nuestro combobox
            cboDispositivos.Text = cboDispositivos.Items[0].ToString();
        }

        public void BuscarDispositivos()
        {
            DispositivosDeVideo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (DispositivosDeVideo.Count == 0)
                ExistenDispositivos = false;
            else
            {
                ExistenDispositivos = true;
                CargarDispositivos(DispositivosDeVideo);
            }
        }

        public void TerminarFuenteDeVideo()
        {
            if (!(FuenteDeVideo == null))
                if (FuenteDeVideo.IsRunning)
                {
                    FuenteDeVideo.SignalToStop();
                    FuenteDeVideo = null;
                }
        }

        private void video_NuevoFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Imagen = (Bitmap)eventArgs.Frame.Clone();
            pbFotoUser.Image = Imagen; //pbFotoUser es nuestro pictureBox
        }

        private void btnIniciar_Click_1(object sender, EventArgs e)
        {

            if (btnIniciar.Text == "Iniciar")
            {
                if (ExistenDispositivos)
                {
                    FuenteDeVideo = new VideoCaptureDevice(DispositivosDeVideo[cboDispositivos.SelectedIndex].MonikerString);
                    FuenteDeVideo.NewFrame += new NewFrameEventHandler(video_NuevoFrame);
                    FuenteDeVideo.Start();
                    btnIniciar.Text = "Detener";
                    cboDispositivos.Enabled = false;
                    //gbMenu.Text = DispositivosDeVideo[cboDispositivos.SelectedIndex].Name.ToString();
                }
                else
                    MessageBox.Show("Error: No se encuentra dispositivo.");
            }
            else
            {
                if (FuenteDeVideo.IsRunning)
                {
                    TerminarFuenteDeVideo();
                    btnIniciar.Text = "Iniciar";
                    cboDispositivos.Enabled = true;
                }
            }
        }

        private void CamaraWeb_Activated(object sender, EventArgs e)
        {
            if (valoresg.CLAVEPAC!="")
            {
                label1.Text = valoresg.CLAVEPAC;
                valoresg.CLAVEPAC = "";
            }
        }

    }
}
