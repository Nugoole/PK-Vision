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
        public string CircleDetect()
        {


            Cv2.CvtColor(processImg, processImg, ColorConversionCodes.BGR2GRAY);
            Cv2.GaussianBlur(processImg, processImg, new Size(3, 3), 1);
            Cv2.Threshold(processImg, processImg, 110, 255, ThresholdTypes.Binary);
            Cv2.Erode(processImg, processImg, new Mat());
            Mat temp = Mat.Zeros(new Size(processImg.Width, processImg.Height / 2), processImg.Type()).ToMat();
            temp.PushBack(processImg);
            processImg = temp;
            processImg.PushBack(Mat.Zeros(new Size(processImg.Width, processImg.Height / 2), processImg.Type()).ToMat());
            CircleSegment[] circles = Cv2.HoughCircles(processImg, HoughMethods.Gradient, 1, Math.Max(processImg.Cols,processImg.Rows) / 20, 70, 9,7,30);
            //Cv2.Erode(processImg, processImg, new Mat(), null, 2);
            //CircleSegment[] circles2 = Cv2.HoughCircles(processImg, HoughMethods.Gradient, 0.8, Math.Max(processImg.Cols, processImg.Rows) / 20, 10, 15, 1, 7);
            //Cv2.CvtColor(processImg, processImg, ColorConversionCodes.GRAY2BGR);

            Mat test;
            StringBuilder passorfail = new StringBuilder();
            int cnt = -1;
            foreach (var circle in circles.OrderBy(x => x.Center.X).Reverse().ToList())
            {
                int rad = (int)circle.Radius;
                try
                {
                    test = processImg[new Rect((int)(circle.Center.X - rad), (int)(circle.Center.Y - rad), rad * 2, rad * 2)];
                }
                catch (Exception)
                {
                    Cv2.Circle(processImg, (Point)circle.Center, rad, new Scalar(255, 149, 231), 2);
                    Cv2.ImShow($"{rad}", processImg);
                    test = null;
                }

                

                Mat compare = new Mat(test.Size(),test.Type());
                Cv2.Circle(compare, new Point(compare.Cols / 2, compare.Rows / 2), (int)circle.Radius, new Scalar(255,0,255), -1);

                Cv2.BitwiseXor(test, compare, compare);
                //Cv2.ImShow($"{cnt+=2}", compare);
                cnt += 2;
                if (compare.CountNonZero() > (compare.Cols * compare.Rows) * 0.45)
                {
                    passorfail.Append($"{cnt}, ");
                    Cv2.ImShow($"{cnt}", compare);
                }




                    //Cv2.ImShow($"{Math.Min(processImg.Cols, processImg.Rows)}", processImg);
                    test.Dispose();
                compare.Dispose();
            }
            
            //foreach (var circle in circles2)
            //{
            //    //Cv2.Circle(imgData, new OpenCvSharp.Point((int)circle.Center.X, (int)circle.Center.Y), 5, new Scalar(255,255,0));
            //    Cv2.Circle(processImg, new Point((int)circle.Center.X, (int)circle.Center.Y), (int)circle.Radius, new Scalar(0, 255, 255), 2);
            //    //Cv2.ImShow($"{Math.Min(processImg.Cols, processImg.Rows)}", processImg);
            //    //Cv2.WaitKey();
            //    //Cv2.Rectangle(imgData, new Rect((int)circle.Center.X - (int)circle.Radius, (int)circle.Center.Y - (int)circle.Radius, 2 * (int)circle.Radius, 2 * (int)circle.Radius), new Scalar(255,255,0), -1);
            //}

            //Cv2.ImShow($"{Math.Min(processImg.Cols,processImg.Rows)}", processImg);

            return passorfail.ToString();
        }

        internal void PatternCheck()
        {
            
        }
    }
}
