﻿using OpenCvSharp;
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
            string filepath = "..\\..\\ImageSources\\imgtest3.jpg";
            string filepath2 = "..\\..\\ImageSources\\observed2.jpg";
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
            //TcpListener listener = new TcpListener(IPAddress.Any, 20);
            //listener.Start();

            //byte[] buff = new byte[1024];

            //FileStream file =  File.OpenWrite("..\\..\\ImageSources\\imgtest.jpg");
            

            //TcpClient tc = listener.AcceptTcpClient();

            //NetworkStream stream = tc.GetStream();

            //int nbytes;
            //while ((nbytes = stream.Read(buff, 0, buff.Length)) > 0)
            //{
            //    //MessageBox.Show(buff[0].ToString());
            //    foreach (var buf in buff)
            //    {
                    
            //        file.WriteByte(buf);
            //    }
            //}

            //stream.Close();
            //tc.Close();

        }
    }
}
