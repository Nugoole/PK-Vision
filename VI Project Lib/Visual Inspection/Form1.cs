using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            string filepath = "d:\\opencv\\pcb2.jpg";
            ImProcess.printImage("img", ImProcess.readImage(filepath,Emgu.CV.CvEnum.ImreadModes.AnyColor));
            MessageBox.Show(ImProcess.getBarcodeCode(filepath));

        }
    }
}
