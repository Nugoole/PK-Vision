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
using VI_Project_Lib;

namespace Visual_Inspection
{
    public partial class ROI_Setting : Form
    {
        private bool mouseClicked { get; set; } = false;
        public Rect roiRect;
        private Mat original { get; set; }
        public List<ROI> rois { get; set; }
        public Mat temp { get; set; }

        public ROI roitemp { get; set; }


        public ROI_Setting(Mat img)
        {
            InitializeComponent();
            original = new Mat();
            temp = new Mat();
            img.CopyTo(original);
            rois = new List<ROI>();
            roitemp = new ROI($"ROI {rois.Count}", CheckType.Soldering);
            roiRect = new Rect();
            bdsROIs.DataSource = rois;
            listBox1.DataSource = bdsROIs;
            pictureBoxIpl1.Image = BitmapConverter.ToBitmap(img);
            pictureBoxIpl1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void PictureBoxIpl1_MouseDown(object sender, MouseEventArgs e)
        {
            roitemp.location = new OpenCvSharp.Point(e.X, e.Y);
            mouseClicked = true;
        }

        private void PictureBoxIpl1_MouseUp(object sender, MouseEventArgs e)
        {
            Cv2.Line(temp, 137, 0, 137, 200, new Scalar(255, 255, 255), 5);
            pictureBoxIpl1.Image = BitmapConverter.ToBitmap(temp);
            original[roiRect].CopyTo(roitemp.roi);
            roitemp.Check(temp);
            Cv2.ImShow("cropped", roitemp.roi);
            mouseClicked = false;
        }

        private void PictureBoxIpl1_MouseMove(object sender, MouseEventArgs e)
        {
            if(mouseClicked)
            {
                startLocationVal.Text = roitemp.location.X.ToString();
                NowLocationVal.Text = e.X.ToString();
                roiRect.Location = new OpenCvSharp.Point(Math.Min(roitemp.location.X, e.X) * original.Cols / pictureBoxIpl1.Size.Width , Math.Min(roitemp.location.Y, e.Y) * original.Rows / pictureBoxIpl1.Size.Height);
                roiRect.Size = new OpenCvSharp.Size(Math.Abs(roitemp.location.X - e.X) * original.Cols / pictureBoxIpl1.Size.Width, Math.Abs(roitemp.location.Y - e.Y) * original.Rows / pictureBoxIpl1.Size.Height);
                
                original.CopyTo(temp);
                Cv2.Rectangle(temp, roiRect, new Scalar(0, 255, 0), 3);
                
                rois.ForEach(x => Cv2.Rectangle(temp, new Rect(x.location.X * original.Cols / pictureBoxIpl1.Size.Width, x.location.Y * original.Rows / pictureBoxIpl1.Size.Height, x.roi.Size().Width, x.roi.Size().Height), new Scalar(0, 255, 0), 3));
                pictureBoxIpl1.Image.Dispose();
                pictureBoxIpl1.Image = BitmapConverter.ToBitmap(temp);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            listBox1.BeginUpdate();
            rois.Add(roitemp);
            listBox1.DataSource = rois;
            listBox1.EndUpdate();
            
            roitemp = new ROI($"ROI {rois.Count}", CheckType.Soldering);
            MessageBox.Show(roitemp.Name);
        }
    }
}
