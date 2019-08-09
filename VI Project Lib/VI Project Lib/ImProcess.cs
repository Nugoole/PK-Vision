using System;
using System.Linq;
using System.Text;
using ZXing;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.XFeatures2D;

namespace VI_Project_Lib
{
    public partial class ImProcess
    {
        
        public Mat originalImg { get; private set; }
        public Mat processImg { get; private set; }

        public ImProcess(string filepath)
        {
            ReadImage(filepath);
        }

        public System.Drawing.Bitmap GetBitmap(Mat image = null)
        {
            if (image == null)
            {
                if (originalImg != null)
                    return BitmapConverter.ToBitmap(originalImg);
                else
                    return null;
            }
            else
                return BitmapConverter.ToBitmap(image);

            //CvInvoke.CvtColor(imgData, imgData, ColorConversion.Gray2Rgba);

        }

        public void ReadImage(string filepath)
        {
            originalImg = Cv2.ImRead(filepath, ImreadModes.Color);
        }

        public void printImage(string windowName)
        {
            //CvInvoke.Resize(image, image,new Size(image.Size.Width / 3, image.Size.Height / 3));

            Cv2.ImShow(windowName, originalImg);
        }

        public void ChangeContrast(double value)
        {
            //imgData += 30;
            //imgData *= value;
        }
        
        public void CircleDetect()
        {
            Cv2.CvtColor(originalImg, originalImg, ColorConversionCodes.BGR2GRAY);
            Cv2.GaussianBlur(originalImg, originalImg, new OpenCvSharp.Size(7, 7), 3, 3);
            CircleSegment[] circles = Cv2.HoughCircles(originalImg,HoughMethods.Gradient, 1, originalImg.Rows / 50,50,40,0,30);

            foreach(var circle in circles)
            {
                Cv2.Circle(originalImg, new OpenCvSharp.Point((int)circle.Center.X, (int)circle.Center.Y), 5, new Scalar(255));
                //CvInvoke.Circle(imgData, new Point((int)circle.Center.X, (int)circle.Center.Y), (int)circle.Radius, new MCvScalar(255));
                Cv2.Rectangle(originalImg, new Rect((int)circle.Center.X - (int)circle.Radius, (int)circle.Center.Y - (int)circle.Radius, 2 * (int)circle.Radius, 2 * (int)circle.Radius),new Scalar(255));
            }
        }

        public int MatchBySurf(string filepath1, string filepath2)
        {
            Mat src1 = new Mat(filepath1);
            Mat src2 = new Mat(filepath2);

            var gray1 = new Mat();
            var gray2 = new Mat();

            Cv2.CvtColor(src1, gray1, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(src2, gray2, ColorConversionCodes.BGR2GRAY);

            var surf = SURF.Create(200, 1, 1, true, true);

            // Detect the keypoints and generate their descriptors using SURF
            KeyPoint[] keypoints1, keypoints2;
            var descriptors1 = new MatOfFloat();
            var descriptors2 = new MatOfFloat();
            surf.DetectAndCompute(gray1, null, out keypoints1, descriptors1);
            surf.DetectAndCompute(gray2, null, out keypoints2, descriptors2);

            // Match descriptor vectors 
            var bfMatcher = new BFMatcher(NormTypes.L2, false);
            var flannMatcher = new FlannBasedMatcher();
            DMatch[] bfMatches = bfMatcher.Match(descriptors1, descriptors2);
            DMatch[] flannMatches = flannMatcher.Match(descriptors1, descriptors2);

            // Draw matches
            var bfView = new Mat();
            Cv2.DrawMatches(gray1, keypoints1, gray2, keypoints2, bfMatches, bfView);
            var flannView = new Mat();
            Cv2.DrawMatches(gray1, keypoints1, gray2, keypoints2, flannMatches, flannView);

            using (new Window("SURF matching (by BFMather)", WindowMode.FreeRatio, bfView))
            using (new Window("SURF matching (by FlannBasedMatcher)", WindowMode.FreeRatio, flannView))
            {
                Cv2.WaitKey();
            }

            return keypoints2.Count();
        }

        public System.Drawing.Bitmap CannyImage()
        {
            Cv2.Canny(originalImg, processImg, 10, 100);
            return BitmapConverter.ToBitmap(processImg);
        }

        public System.Drawing.Bitmap Morphology()
        {

            Cv2.Canny(originalImg, processImg, 50, 230);

            Mat cross = new Mat(3, 3, MatType.CV_8U, new Scalar(0));

            // creating the cross-shaped structuring element
            for (int i = 0; i < 3; i++)
            {
                cross.Set<byte>(2, i, 1);
                cross.Set<byte>(i, 2, 1);
            }

            Mat result = new Mat();
            Cv2.MorphologyEx(processImg, result, MorphTypes.TopHat, cross);

            return BitmapConverter.ToBitmap(result);
        }
    }
}
