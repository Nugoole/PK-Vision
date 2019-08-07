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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VI_Project_Lib;

namespace Visual_Inspection
{
    public partial class MainForm : Form
    {
        public List<ROI> ROIs { get; set; }
        public ImProcess process { get; private set; }
        public Preset selectedPreset { get; set; }
        public int cnt { get; set; }
        public MainForm()
        {
            InitializeComponent();

            string filepath = "..\\..\\ImageSources\\sample0.png";
            process = new ImProcess(filepath);
        }

        private void btnDoProcess_Click(object sender, EventArgs e)
        {
            string filepath2 = "..\\..\\ImageSources\\sample7.jpg";

            while (true)
            {
                process.MatchBySurf(filepath2);

                foreach (var roi in selectedPreset.ROIs)
                {
                    if (roi.checkType == CheckType.BarCode)
                        barcode.Text = roi.Check(process.processImg);
                    else
                        roi.Check(process.processImg);
                }

                pictureBoxIpl1.Image = process.GetBitmap(process.processImg);
                pictureBoxIpl1.SizeMode = PictureBoxSizeMode.StretchImage;

                MessageBox.Show(cnt.ToString());

                cnt++;
                //Cv2.WaitKey(2000);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ROI_Setting form = new ROI_Setting(process.originalImg);
            if(form.ShowDialog(this) == DialogResult.OK)
            {
                selectedPreset = form.presets.Find(x => x.PresetName == form.listbxPreset.SelectedItem.ToString());
            }

            form.Dispose();
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
