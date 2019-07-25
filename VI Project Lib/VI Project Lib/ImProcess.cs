﻿using System;
using System.Linq;
using System.Text;
using ZXing;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.XFeatures2D;
using System.Drawing;

namespace VI_Project_Lib
{
    public partial class ImProcess
    {

        public Mat imgData;
        public ImProcess(ref Mat img)
        {
            imgData = new Mat();
            img.CopyTo(imgData);
        }

        public ImProcess(string filepath)
        {
            ReadImage(filepath);
        }

        public System.Drawing.Bitmap GetBitmap(Mat image = null)
        {
            if (image == null)
            {
                if (imgData != null)
                    return BitmapConverter.ToBitmap(imgData);
                else
                    return null;
            }
            else
                return BitmapConverter.ToBitmap(image);

            //CvInvoke.CvtColor(imgData, imgData, ColorConversion.Gray2Rgba);

        }

        public void ReadImage(string filepath)
        {
            imgData = Cv2.ImRead(filepath, ImreadModes.Color);
        }

        public void printImage(string windowName)
        {
            //CvInvoke.Resize(image, image,new Size(image.Size.Width / 3, image.Size.Height / 3));

            Cv2.ImShow(windowName, imgData);
        }

        

        public void ChangeContrast(double value)
        {
            //imgData += 30;
            //imgData *= value;
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

        public static Bitmap toBitmap(Mat mat)
        {
            return BitmapConverter.ToBitmap(mat);
        }
    }
}
