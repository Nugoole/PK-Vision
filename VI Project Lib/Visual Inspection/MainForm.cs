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
        public Barcode detectedBarcode { get; set; }
        public StringBuilder errorPinsBuilder { get; set; } = new StringBuilder();
        public NetworkStream commandStream { get; set; }
        public TcpClient commandClient { get; set; }
        Dictionary<int, int> dateDict;
        public MainForm()
        {
            InitializeComponent();

            string filepath = "..\\..\\ImageSources\\sample0.jpg";
            process = new ImProcess(filepath);
            //selectedPreset = form.presets.Find(x => x.PresetName == "test");
            AddDate();

        }

        private void AddDate()
        {
            dateDict = new Dictionary<int, int>();
            dateDict.Add(1, 31);
            dateDict.Add(2, 28);
            dateDict.Add(3, 31);
            dateDict.Add(4, 30);
            dateDict.Add(5, 31);
            dateDict.Add(6, 30);
            dateDict.Add(7, 31);
            dateDict.Add(8, 31);
            dateDict.Add(9, 30);
            dateDict.Add(10, 31);
            dateDict.Add(11, 30);
            dateDict.Add(12, 31);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            form = new ROI_Setting(process.originalImg);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                selectedPreset = form.presets.Find(x => x.PresetName == form.listbxPreset.SelectedItem.ToString());
            }

            form.Dispose();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ImageSocket.RunWorkerAsync();
            //CommandSocket.RunWorkerAsync();
        }

        private void Socket_DoWork(object sender, DoWorkEventArgs e)
        {
            int cnt = 1;
            string filename = "sample" + cnt.ToString() + ".jpg";

            TcpClient tc = new TcpClient();
            NetworkStream stream;
            while (!tc.Connected)
            {
                try
                {
                    tc.Connect("192.168.0.2", 50003);
                }
                catch (Exception)
                { }
            }

            FileStream file = new FileStream("..\\..\\ImageSources\\" + filename, FileMode.Create);
            byte[] buffer = new byte[1024];
            int nbytes;
            stream = tc.GetStream();

            while (true)
            {
                stream.Read(buffer, 0, buffer.Length);
                string received = Encoding.ASCII.GetString(buffer);
                //MessageBox.Show(received);
                if (string.Compare(received, "sending") == 0)
                    break;
            }

            nbytes = stream.Read(buffer, 0, sizeof(int));
            int fileSize = BitConverter.ToInt32(buffer, 0);


            stream.Write(buffer, 0, sizeof(int));

            nbytes = 10;
            int receivedSize = 0;
            while (receivedSize <= fileSize)
            {
                nbytes = stream.Read(buffer, 0, buffer.Length);
                file.Write(buffer, 0, nbytes);
                receivedSize += nbytes;
            }

            stream.Close();
            tc.Close();
            file.Close();

            tc.Dispose();

            try
            {
                ImageSocket.ReportProgress(100, filename);
            }
            catch (Exception e2)
            {

            }
            Thread.Sleep(30);

            //if (MessageBox.Show("종료?", "??", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.OK)
            //    break;

        }

        private void Socket_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string filepath2 = "..\\..\\ImageSources\\" + e.UserState as string;

            process.MatchBySurf(filepath2);


            //바코드 체크 부분
            foreach (var roi in selectedPreset.ROIs)
            {
                if (roi.checkType == CheckType.BarCode)
                {
                    barcode.Text = roi.Check(process.processImg);


                    if(barcode.Text == "nothing")
                    {
                        detectedBarcode = new Barcode();
                        detectedBarcode.ErrorCodeId = 13;
                        detectedBarcode.State = "Fail";
                        detectedBarcode.ItemId = 2;
                        detectedBarcode.InspectionDate = DateTime.Now;
                        detectedBarcode.ProcessId = 5;
                        DB.BarCode.InsertOrUpdate(detectedBarcode);
                        return;
                    }

                    int barcodeNum = int.Parse(barcode.Text);

                    if(CheckBarcode(barcodeNum) == false)
                    {
                        detectedBarcode = new Barcode();
                        detectedBarcode.ErrorCodeId = 14;
                        detectedBarcode.State = "Fail";
                        detectedBarcode.ItemId = 2;
                        detectedBarcode.InspectionDate = DateTime.Now;
                        detectedBarcode.ProcessId = 5;
                        DB.BarCode.InsertOrUpdate(detectedBarcode);
                        return;
                    }


                    detectedBarcode = DB.BarCode.Search(barcodeNum);
                    detectedBarcode.State = "Pass";
                    detectedBarcode.ItemId = 2;
                    detectedBarcode.InspectionDate = DateTime.Now;
                    detectedBarcode.ProcessId = 5;
                }
            }

            errorPinsBuilder.Clear();


            //솔더링 체크 부분
            foreach (var roi in selectedPreset.ROIs)
            {

                if (roi.checkType == CheckType.BarCode)
                    continue;
                else
                    errorPinsBuilder.Append(roi.Check(process.processImg));
            }

            faillabel.Text = errorPinsBuilder.ToString();

            //DB 올리기전 솔더링 체크 Pass or Fail
            if (!string.IsNullOrEmpty(errorPinsBuilder.ToString()))
            {
                detectedBarcode.State = "Fail";
                detectedBarcode.ErrorCodeId = 5;
            }

            pictureBoxIpl1.Image = process.GetBitmap(process.processImg);
            pictureBoxIpl1.SizeMode = PictureBoxSizeMode.StretchImage;



            cnt++;
            DB.BarCode.InsertOrUpdate(detectedBarcode);
        }

        private bool CheckBarcode(int barcodeCode)
        {


            int itemNo = int.Parse(barcodeCode.ToString()[0].ToString());
            int month = int.Parse(barcodeCode.ToString().Substring(4, 2));           
            int date = int.Parse(barcodeCode.ToString().Substring(6, 2));
            if (barcodeCode.ToString().Length == 8)
            {
                if (itemNo < 1 || itemNo > 3)
                {
                    detectedBarcode.ErrorCodeId = 8;
                    detectedBarcode.State = "Fail";
                    return false;
                }

                if (!dateDict.ContainsKey(month))
                {
                    detectedBarcode.ErrorCodeId = 8;
                    detectedBarcode.State = "Fail";
                    return false;
                }
                
                if (!(dateDict[month] >= date && date > 0))
                {
                    detectedBarcode.ErrorCodeId = 8;
                    detectedBarcode.State = "Fail";
                    return false;
                }
            }
            else
                return false;

            return true;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (form != null)
                form.Dispose();
            ImageSocket.CancelAsync();
            CommandSocket.CancelAsync();
        }

        private void Socket_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommandSocket.RunWorkerAsync();
        }

        private void CommandSocket_DoWork(object sender, DoWorkEventArgs e)
        {
            commandClient = new TcpClient();

            while (!commandClient.Connected)
            {
                try
                {
                    commandClient.Connect("192.168.0.2", 50004);
                }
                catch (Exception exp)
                {
                }
            }
            commandStream = commandClient.GetStream();
            CommandSocket.ReportProgress(100, detectedBarcode.State);
        }

        private void CommandSocket_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            ImageSocket.RunWorkerAsync();
        }

        private void CommandSocket_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int command = 1;
            Thread.Sleep(30);
            if ((e.UserState as string) == "Pass")
                command = 1;
            else if ((e.UserState as string) == "Fail")
                command = -1;


            byte[] buffer = BitConverter.GetBytes(command);
            commandStream.Write(buffer, 0, buffer.Length);
            commandStream.Close();
            commandClient.Close();
        }
    }
}
