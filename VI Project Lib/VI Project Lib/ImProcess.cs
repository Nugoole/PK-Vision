﻿using System;
using System.Linq;
using System.Text;
using ZXing;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.XFeatures2D;
using System.Collections.Generic;

namespace VI_Project_Lib
{
    public partial class ImProcess
    {

        public Mat imgData;
        public ImProcess(Mat img)
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

            Cv2.Resize(src1, src1, new Size(src1.Size().Width / 3, src1.Size().Height / 3));
            Cv2.Resize(src2, src2, new Size(src2.Size().Width / 3, src2.Size().Height / 3));

            var gray1 = new Mat();
            var gray2 = new Mat();

            Cv2.CvtColor(src1, gray1, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(src2, gray2, ColorConversionCodes.BGR2GRAY);

            Cv2.ImShow("gray1", gray1);
            Cv2.ImShow("gray2", gray2);
            var surf = SIFT.Create(10000,2, 0.04,5,1.6);
            //var surf = SURF.Create(10000);

            // Detect the keypoints and generate their descriptors using SURF
            KeyPoint[] keypoints1, keypoints2;
            var descriptors1 = new Mat();
            var descriptors2 = new Mat();
            surf.DetectAndCompute(gray1, null, out keypoints1, descriptors1);
            surf.DetectAndCompute(gray2, null, out keypoints2, descriptors2);



            // Match descriptor vectors 
            var bfMatcher = new BFMatcher(NormTypes.L2, false);
            OpenCvSharp.Flann.SearchParams sParam = new OpenCvSharp.Flann.SearchParams(50);
            
            var flannMatcher = new FlannBasedMatcher(null, sParam);
            DMatch[][] bfMatches = bfMatcher.KnnMatch(descriptors1, descriptors2, 2);
            DMatch[][] flannMatches = flannMatcher.KnnMatch(descriptors1, descriptors2, 3);

            //find goodMatches
            const float ratio_thresh = 0.6f;
            DMatch[] goodMatches = new DMatch[flannMatches.Length];
            
            for (int i = 0; i < flannMatches.Length; i++)
            {
                if((flannMatches[i][0].Distance < ratio_thresh * flannMatches[i][1].Distance) && (flannMatches[i][0].Distance > 0.3 * flannMatches[i][1].Distance))
                {
                    goodMatches[i] = flannMatches[i][0];
                }
            }


            // Draw matches
            
            //var bfView = new Mat();
            
            //Cv2.DrawMatches(gray1, keypoints1, gray2, keypoints2, goodMatches, bfView, null, null, null, DrawMatchesFlags.NotDrawSinglePoints);
            var flannView = new Mat();
            
            Cv2.DrawMatches(gray1, keypoints1, gray2, keypoints2, goodMatches, flannView,new Scalar(0,255,0),new Scalar(255,255,0),null, DrawMatchesFlags.Default);

            List<Point2f> obj = new List<Point2f>();
            List<Point2f> scene = new List<Point2f>();
            if (goodMatches.Length == 0) return 0;

            for (int i = 0; i < goodMatches.Length; i++)
            {
                obj.Add(keypoints1[goodMatches[i].QueryIdx].Pt);
                scene.Add(keypoints2[goodMatches[i].TrainIdx].Pt);
            }

            List<Point2d> obj_corners = new List<Point2d>();
            obj_corners.Add(new Point(0, 0));
            obj_corners.Add(new Point(src1.Cols, 0));
            obj_corners.Add(new Point(src1.Cols, src1.Rows));
            obj_corners.Add(new Point(0, src1.Rows));
            List<Point2d> scene_corners = new List<Point2d>();

            Mat H = Cv2.FindHomography(obj.ConvertAll(Point2fToPoint2d), scene.ConvertAll(Point2fToPoint2d), HomographyMethods.None);

            if (H.Empty()) return 0;
            scene_corners = Cv2.PerspectiveTransform(obj_corners, H).ToList();



            //List<List<OpenCvSharp.Point>> listoflistofpoint2d = new List<List<OpenCvSharp.Point>>();
            //List<OpenCvSharp.Point> listofPoint2D = new List<OpenCvSharp.Point>();
            //listofPoint2D.Add(new OpenCvSharp.Point(sceneCornersData[0].X + src1.Cols, sceneCornersData[0].Y));
            //listofPoint2D.Add(new OpenCvSharp.Point(sceneCornersData[1].X + src1.Cols, sceneCornersData[1].Y));
            //listofPoint2D.Add(new OpenCvSharp.Point(sceneCornersData[2].X + src1.Cols, sceneCornersData[2].Y));
            //listofPoint2D.Add(new OpenCvSharp.Point(sceneCornersData[3].X + src1.Cols, sceneCornersData[3].Y));
            //listoflistofpoint2d.Add(listofPoint2D);
            //flannView.Polylines(listoflistofpoint2d,true, Scalar.LimeGreen, 2);


            Cv2.Line(flannView, (Point)(scene_corners[0] + new Point2d(src1.Cols, 0)), (Point)(scene_corners[1] + new Point2d(src1.Cols, 0)), Scalar.LimeGreen, 3);
            Cv2.Line(flannView, (Point)(scene_corners[1] + new Point2d(src1.Cols, 0)), (Point)(scene_corners[2] + new Point2d(src1.Cols, 0)), Scalar.LimeGreen, 3);
            Cv2.Line(flannView, (Point)(scene_corners[2] + new Point2d(src1.Cols, 0)), (Point)(scene_corners[3] + new Point2d(src1.Cols, 0)), Scalar.LimeGreen, 3);
            Cv2.Line(flannView, (Point)(scene_corners[3] + new Point2d(src1.Cols, 0)), (Point)(scene_corners[0] + new Point2d(src1.Cols, 0)), Scalar.LimeGreen, 3);

            //using (new Window("SURF matching (by BFMather)", WindowMode.AutoSize, bfView))
            using (new Window("SURF matching (by FlannBasedMatcher)", WindowMode.AutoSize, flannView))
            {
                Cv2.WaitKey();
            }

            return keypoints2.Count();
        }

        public static System.Drawing.Bitmap toBitmap(Mat mat)
        {
            return BitmapConverter.ToBitmap(mat);
        }

        static Point2d[] MyPerspectiveTransform3(Point2f[] yourData, Mat transformationMatrix)
        {
            Point2f[] ret = Cv2.PerspectiveTransform(yourData, transformationMatrix);
            return ret.Select(x => Point2fToPoint2d(x)).ToArray();
        }

        private static Point2d Point2fToPoint2d(Point2f pf)
        {
            return new Point2d(((int)pf.X), ((int)pf.Y));
        }
    }
}
