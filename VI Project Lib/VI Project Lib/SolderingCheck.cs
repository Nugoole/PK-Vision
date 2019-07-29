using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VI_Project_Lib
{
    public partial class ImProcess
    {
        public void CircleDetect()
        {
            Cv2.CvtColor(imgData, imgData, ColorConversionCodes.BGR2GRAY);
            Cv2.GaussianBlur(imgData, imgData, new Size(7, 7), 1, 1);
            CircleSegment[] circles = Cv2.HoughCircles(imgData, HoughMethods.Gradient, 0.7, imgData.Rows / 20, 50, 40);
            Cv2.CvtColor(imgData, imgData, ColorConversionCodes.GRAY2BGR);
            foreach (var circle in circles)
            {
                //Cv2.Circle(imgData, new OpenCvSharp.Point((int)circle.Center.X, (int)circle.Center.Y), 5, new Scalar(255,255,0));
                Cv2.Circle(imgData, new Point((int)circle.Center.X, (int)circle.Center.Y), (int)circle.Radius, new Scalar(255,0,255), 2);
                //Cv2.Rectangle(imgData, new Rect((int)circle.Center.X - (int)circle.Radius, (int)circle.Center.Y - (int)circle.Radius, 2 * (int)circle.Radius, 2 * (int)circle.Radius), new Scalar(255,255,0), -1);
            }

            Cv2.ImShow("circle", imgData);
        }
    }
}
