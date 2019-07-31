using OpenCvSharp;
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
        public List<ROI> ROIs { get; set; }
        public ImProcess process { get; private set; }
        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string filepath = "..\\..\\ImageSources\\pcb4.jpg";
            string filepath2 = "..\\..\\ImageSources\\observed.png";
            process = new ImProcess(filepath);
            ROIs = new List<ROI>();
            
            barcode.Text = process.GetBarcodeCode();

            if (ROIs.Count == 0)
            {
                //process.CircleDetect();

                pictureBoxIpl1.Image = process.GetBitmap();
                pictureBoxIpl1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ROI_Setting form = new ROI_Setting(process.imgData);
            form.Show();
        }
    }
}
