using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
            string filepath = "..\\..\\ImageSources\\sample0.png";
            string filepath2 = "..\\..\\ImageSources\\sample6.jpg";
            process = new ImProcess(filepath);
            ROIs = new List<ROI>();

            barcode.Text = process.GetBarcodeCode();

            process.MatchBySurf(filepath, filepath2);
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            //TcpClient tc = new TcpClient("169.254.195.141", 50001);

            //FileStream file = new FileStream("..\\..\\ImageSources\\sample1.jpg", FileMode.Create);
            //byte[] buffer = new byte[1024];
            //int nbytes;
            //NetworkStream stream = tc.GetStream();

            ////FileStream file = new FileStream("..\\..\\ImageSources\\imgtest.jpg", FileMode.Create);

            //while ((nbytes = stream.Read(buffer, 0, buffer.Length)) > 0)
            //{
            //    file.Write(buffer, 0, nbytes);
            //}

            //stream.Close();
            //tc.Close();
        }
    }
}
