using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using MODI;

namespace SHOPCONTROL
{
    public partial class ConvertirImagen : Form
    {
        public ConvertirImagen()
        {
            InitializeComponent();
        }

        private void ConvertirImagen_Load(object sender, EventArgs e)
        {

        }


string strFilename = @"mifoto.gif";


//static string GetOCRText(string strFilename)
//        {
//            Document myOCRDoc = new Document();
//            myOCRDoc.Create(strFilename);
//            myOCRDoc.OCR(MODI.MiLANGUAGES.miLANG_ENGLISH, true, true);
//            Image image = (Image)myOCRDoc.Images[0];
//            Layout layout = image.Layout;
//            string strRetVal = "";
//            foreach (Word word in layout.Words)
//            {
//                Console.WriteLine("Word: {0} confidence: {1}", word.Text, word.RecognitionConfidence);
//            }                      

//            return strRetVal;
//        }
    }
}
