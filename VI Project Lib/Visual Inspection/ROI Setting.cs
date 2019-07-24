using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace Visual_Inspection
{
    public partial class ROI_Setting : Form
    {
        private bool mouseClicked { get; set; } = false;
        private Rect roiRect { get; set; }
        private Mat original { get; set; }
        public Mat temp { get; set; }

        private OpenCvSharp.Point startLocation { get; set; }

        public ROI_Setting(Mat img)
        {
            InitializeComponent();
            original = new Mat();
            img.CopyTo(original);
            pictureBoxIpl1.Image = BitmapConverter.ToBitmap(img);
            pictureBoxIpl1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void PictureBoxIpl1_MouseDown(object sender, MouseEventArgs e)
        {
            startLocation = new OpenCvSharp.Point(e.X, e.Y);
            mouseClicked = true;
        }

        private void PictureBoxIpl1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseClicked = false;
        }

        private void PictureBoxIpl1_MouseMove(object sender, MouseEventArgs e)
        {
            if(mouseClicked)
            {
                roiRect = new Rect(Math.Min(startLocation.X, e.X), Math.Min(startLocation.Y, e.Y), Math.Abs(startLocation.X - e.X), Math.Abs(startLocation.Y - e.Y));
                if(temp != null)
                    temp.Dispose();
                temp = new Mat();
                original.CopyTo(temp);
                Cv2.Rectangle(temp, roiRect, new Scalar(0, 255, 0));
                pictureBoxIpl1.Image = BitmapConverter.ToBitmap(temp);
            }
        }
    }
}
