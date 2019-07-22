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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string filepath = "..\\..\\ImageSources\\pcb_barcode.jpg";
            ImProcess process = new ImProcess(filepath);
            //MessageBox.Show(Directory.GetCurrentDirectory());
            
            barcode.Text = process.GetBarcodeCode();
            process.ChangeContrast(0.2);
            process.CircleDetect();
            outputImage.Image = process.GetBitmap();
            outputImage.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
