using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
namespace TurnosPacientes
{
    public partial class PruebadeVoz : Form
    {

        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        List<VoiceInfo> vocesInfo = new List<VoiceInfo>();

        public PruebadeVoz()
        {
            InitializeComponent();
            foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
            {
                vocesInfo.Add(voice.VoiceInfo);
                cbVoces.Items.Add(voice.VoiceInfo.Name);

            }
            cbVoces.SelectedIndex = 0;
        }

        private void PruebadeVoz_Load(object sender, EventArgs e)
        {

        }

        private void BtnHablar_Click(object sender, EventArgs e)
        {
            int indice;

            double Volumen = tbVolumen.Value;
            double Rate = tbRate.Value;

            indice = cbVoces.SelectedIndex;
            String nombre = vocesInfo.ElementAt(indice).Name;
            synthesizer.SelectVoice(nombre);

            synthesizer.Volume = (int)Volumen;
            synthesizer.Rate = (int)Rate;
            synthesizer.Speak(txtTexto.Text);
        }
    }
}
