using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Input;
//Debe agregar antes la referencia Framework System.Speech
using System.Speech.Synthesis;

//HECHO POR YENIER VENEGAS SANCHEZ


namespace TextoAVozCSharp
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        List<VoiceInfo> vocesInfo = new List<VoiceInfo>();

        public Form1()
        {
            InitializeComponent();
            foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
            {
                vocesInfo.Add(voice.VoiceInfo);
                cbVoces.Items.Add(voice.VoiceInfo.Name);
                
            }
            cbVoces.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
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
