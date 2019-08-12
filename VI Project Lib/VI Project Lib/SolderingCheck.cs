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
            Cv2.CvtColor(processImg, processImg, ColorConversionCodes.BGR2GRAY);
            Cv2.GaussianBlur(processImg, processImg, new Size(3, 3), 1, 1);
            Cv2.Threshold(processImg, processImg, 130, 255, ThresholdTypes.Binary);
            Cv2.Erode(processImg, processImg, new Mat());
            CircleSegment[] circles = Cv2.HoughCircles(processImg, HoughMethods.Gradient, 0.7, Math.Max(processImg.Cols,processImg.Rows) / 20, 10, 15, 7, 15);
            CircleSegment[] circles2 = Cv2.HoughCircles(processImg, HoughMethods.Gradient, 0.8, Math.Max(processImg.Cols, processImg.Rows) / 20, 10, 15, 1, 7);
            Cv2.CvtColor(processImg, processImg, ColorConversionCodes.GRAY2BGR);
            
            foreach (var circle in circles.OrderBy(x => x.Center.X).ToList())
            {
                Cv2.Circle(processImg, new Point((int)circle.Center.X, (int)circle.Center.Y), (int)circle.Radius, new Scalar(255,0,255), 2);
                //Cv2.ImShow($"{Math.Min(processImg.Cols, processImg.Rows)}", processImg);
                //Cv2.WaitKey();
            }

            foreach (var circle in circles2)
            {
                //Cv2.Circle(imgData, new OpenCvSharp.Point((int)circle.Center.X, (int)circle.Center.Y), 5, new Scalar(255,255,0));
                Cv2.Circle(processImg, new Point((int)circle.Center.X, (int)circle.Center.Y), (int)circle.Radius, new Scalar(0, 255, 255), 2);
                //Cv2.ImShow($"{Math.Min(processImg.Cols, processImg.Rows)}", processImg);
                //Cv2.WaitKey();
                //Cv2.Rectangle(imgData, new Rect((int)circle.Center.X - (int)circle.Radius, (int)circle.Center.Y - (int)circle.Radius, 2 * (int)circle.Radius, 2 * (int)circle.Radius), new Scalar(255,255,0), -1);
            }

            Cv2.ImShow($"{Math.Min(processImg.Cols,processImg.Rows)}", processImg);
        }

        internal void PatternCheck()
        {
            
        }
    }
}
