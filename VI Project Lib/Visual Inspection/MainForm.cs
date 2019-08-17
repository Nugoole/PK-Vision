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
using VI_DB;
using VI_DB.Data;
using VI_DB.DB;
using VI_Project_Lib;

namespace Visual_Inspection
{
    public partial class MainForm : Form
    {
        public List<ROI> ROIs { get; set; }
        public ImProcess process { get; private set; }
        public Preset selectedPreset { get; set; }
        public int cnt { get; set; }
        public ROI_Setting form { get; set; }
        public MainForm()
        {
            InitializeComponent();

            string filepath = "..\\..\\ImageSources\\sample0.jpg";
            process = new ImProcess(filepath);
            form = new ROI_Setting(process.originalImg);
            selectedPreset = form.presets.Find(x => x.PresetName == "New");
        }

        private void btnDoProcess_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                selectedPreset = form.presets.Find(x => x.PresetName == form.listbxPreset.SelectedItem.ToString());
            }

            form.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Socket.RunWorkerAsync();
        }

        private void Socket_DoWork(object sender, DoWorkEventArgs e)
        {
            int cnt = 1;
            string filename = "sample" + cnt.ToString() + ".jpg";

            TcpClient tc = new TcpClient();
            NetworkStream stream;
            while (true)
            {
                tc.Connect("169.254.195.141", 50003);
                if (tc.Connected)
                    break;
            }
            
            FileStream file = new FileStream("..\\..\\ImageSources\\" + filename, FileMode.Create);
            byte[] buffer = new byte[1024];
            int nbytes;
            stream = tc.GetStream();

            while (true)
            {
                stream.Read(buffer, 0, buffer.Length);
                string received = Encoding.ASCII.GetString(buffer);
                MessageBox.Show(received);
                if (string.Compare(received, "sending") == 0)
                    break;
            }
            //stream.Write(buffer, 0, sizeof(int));
            nbytes = stream.Read(buffer, 0, sizeof(int));
            int fileSize = BitConverter.ToInt32(buffer, 0);


            stream.Write(buffer, 0, sizeof(int));
            //FileStream file = new FileStream("..\\..\\ImageSources\\imgtest.jpg", FileMode.Create);
            nbytes = 10;
            int receivedSize = 0;
            while (receivedSize <= fileSize)
            {
                nbytes = stream.Read(buffer, 0, buffer.Length);
                file.Write(buffer, 0, nbytes);
                receivedSize += nbytes;

                //MessageBox.Show(receivedSize.ToString() + "/ " +fileSize.ToString());
            }
            //MessageBox.Show(receivedSize.ToString() + "/ " + fileSize.ToString());




            stream.Close();
            tc.Close();
            file.Close();
            
            try
            {
                Socket.ReportProgress(100, filename);
            }
            catch (Exception e2)
            {

            }
            Thread.Sleep(1000);

            //if (MessageBox.Show("종료?", "??", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.OK)
            //    break;

        }

        private void Socket_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string filepath2 = "..\\..\\ImageSources\\" + e.UserState as string;

            process.MatchBySurf(filepath2);

            foreach (var roi in selectedPreset.ROIs)
            {
                if (roi.checkType == CheckType.BarCode)
                    barcode.Text = roi.Check(process.processImg);
                //DB 바코드에 올리기
                //var a = new Barcode();
                //a.BarcodeCode = int.Parse(barcode.Text);
                //DB.BarCode.Insert(a);
            }

            foreach (var roi in selectedPreset.ROIs)
            {
                if (roi.checkType == CheckType.BarCode)
                    continue;
                else
                    MessageBox.Show(roi.Check(process.processImg));
            }

            pictureBoxIpl1.Image = process.GetBitmap(process.processImg);
            pictureBoxIpl1.SizeMode = PictureBoxSizeMode.StretchImage;

            //MessageBox.Show(cnt.ToString());

            cnt++;
            //Cv2.WaitKey(2000);

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Dispose();
            Socket.CancelAsync();
        }

        private void Socket_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Socket.RunWorkerAsync();
        }
    }
}
