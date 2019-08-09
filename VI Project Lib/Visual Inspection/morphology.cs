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
    public partial class morphology : Form
    {
        public ImProcess process { get; set; }
        public morphology()
        {
            InitializeComponent();
            string filepath = "..\\..\\ImageSources\\sample0.png";
            process = new ImProcess(filepath);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            pictureBoxIpl1.Image = process.CannyImage();
            pictureBoxIpl1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxIpl2.Image = process.Morphology();
            pictureBoxIpl2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
