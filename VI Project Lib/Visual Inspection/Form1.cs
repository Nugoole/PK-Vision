using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VI_Project_Lib;

namespace Visual_Inspection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string filepath = "..\\..\\ImageSources\\pcb_barcode.jpg";
            ImProcess process = new ImProcess(filepath);
            //MessageBox.Show(Directory.GetCurrentDirectory());
            outputImage.Image = process.GetBitmap();
            outputImage.SizeMode = PictureBoxSizeMode.StretchImage;
            barcode.Text = process.getBarcodeCode();
        }
    }
}
