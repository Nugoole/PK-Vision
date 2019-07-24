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
            Cv2.GaussianBlur(imgData, imgData, new OpenCvSharp.Size(7, 7), 3, 3);
            CircleSegment[] circles = Cv2.HoughCircles(imgData, HoughMethods.Gradient, 1, imgData.Rows / 50, 50, 40, 0, 30);
            Cv2.CvtColor(imgData, imgData, ColorConversionCodes.GRAY2BGR);
            foreach (var circle in circles)
            {
                //Cv2.Circle(imgData, new OpenCvSharp.Point((int)circle.Center.X, (int)circle.Center.Y), 5, new Scalar(255,255,0));
                Cv2.Circle(imgData, new Point((int)circle.Center.X, (int)circle.Center.Y), (int)circle.Radius, new Scalar(255,255,255), -1);
                //Cv2.Rectangle(imgData, new Rect((int)circle.Center.X - (int)circle.Radius, (int)circle.Center.Y - (int)circle.Radius, 2 * (int)circle.Radius, 2 * (int)circle.Radius), new Scalar(255,255,0), -1);
            }

            
        }
    }
}
