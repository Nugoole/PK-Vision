using System;
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
            Cv2.Resize(src2, src2, new Size(src2.Size().Width / 4, src2.Size().Height / 4));
            Cv2.Resize(src1, src1, new Size(src2.Size().Width / 2, src2.Size().Height / 2));


            var gray1 = new Mat();
            var gray2 = new Mat();

            Cv2.CvtColor(src1, gray1, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(src2, gray2, ColorConversionCodes.BGR2GRAY);

            Cv2.ImShow("gray1", gray1);

            int octaves = (int)Math.Log(Math.Min(src2.Size().Width, src2.Size().Height), 2);
            var surf = SIFT.Create(400);
            //var surf = SURF.Create(10000);

            // Detect the keypoints and generate their descriptors using SURF
            KeyPoint[] keypoints1, keypoints2;
            var descriptors1 = new Mat();
            var descriptors2 = new Mat();
            surf.DetectAndCompute(gray1, null, out keypoints1, descriptors1);
            surf.DetectAndCompute(gray2, null, out keypoints2, descriptors2);




            // Match descriptor vectors 
            
            OpenCvSharp.Flann.SearchParams sParam = new OpenCvSharp.Flann.SearchParams(50);
            var flannMatcher = new FlannBasedMatcher(null, sParam);
            
            DMatch[][] flannMatches = flannMatcher.KnnMatch(descriptors1, descriptors2, 2);

            double max_dist = 0;
            double min_dist = 100;

            for (int i = 0; i < descriptors1.Rows; i++)
            {
                double dist = flannMatches[i][1].Distance;
                if (dist < min_dist) min_dist = dist;
                if (dist > max_dist) max_dist = dist;
            }




            //find goodMatches
            float ratio_thresh = 3 * (float)min_dist;
            List<DMatch> goodMatches = new List<DMatch>();

            for (int i = 0; i < flannMatches.Length; i++)
            {
                if ((flannMatches[i][0].Distance < ratio_thresh)) //* flannMatches[i][1].Distance))// && (flannMatches[i][0].Distance > 0.4 * flannMatches[i][1].Distance))
                {
                    goodMatches.Add(flannMatches[i][0]);
                }
            }

            //(1000, 0.6, 0.4)

            // Draw matches

            //var bfView = new Mat();

            //Cv2.DrawMatches(gray1, keypoints1, gray2, keypoints2, goodMatches, bfView, null, null, null, DrawMatchesFlags.NotDrawSinglePoints);
            var flannView = new Mat();

            Cv2.DrawMatches(gray1, keypoints1, gray2, keypoints2, goodMatches, flannView, new Scalar(0, 255, 0), new Scalar(255, 255, 0), null, DrawMatchesFlags.Default);

            List<Point2f> obj = new List<Point2f>();
            List<Point2f> scene = new List<Point2f>();
            if (goodMatches.Count == 0) return 0;

            for (int i = 0; i < goodMatches.Count; i++)
            {
                obj.Add(keypoints1[goodMatches[i].QueryIdx].Pt);
                scene.Add(keypoints2[goodMatches[i].TrainIdx].Pt);
            }

            List<Point2d> obj_corners = new List<Point2d>();
            obj_corners.Add(new Point(0, 0));
            obj_corners.Add(new Point(gray1.Cols, 0));
            obj_corners.Add(new Point(gray1.Cols, gray1.Rows));
            obj_corners.Add(new Point(0, gray1.Rows));
            List<Point2d> scene_corners = new List<Point2d>();

            Mat H = Cv2.FindHomography(obj.ConvertAll(Point2fToPoint2d), scene.ConvertAll(Point2fToPoint2d), HomographyMethods.Ransac);

            Cv2.Circle(gray2, (Point)scene[0], 10, new Scalar(0, 255, 0), 2);
            Cv2.ImShow("gray2", gray2);
            if (H.Empty()) return 0;
            scene_corners = Cv2.PerspectiveTransform(obj_corners, H).ToList();

            Cv2.Line(flannView, (Point)(scene_corners[0] + new Point2d(gray1.Cols, 0)), (Point)(scene_corners[1] + new Point2d(gray1.Cols, 0)), Scalar.DarkRed, 3);
            Cv2.Line(flannView, (Point)(scene_corners[1] + new Point2d(gray1.Cols, 0)), (Point)(scene_corners[2] + new Point2d(gray1.Cols, 0)), Scalar.DarkRed, 3);
            Cv2.Line(flannView, (Point)(scene_corners[2] + new Point2d(gray1.Cols, 0)), (Point)(scene_corners[3] + new Point2d(gray1.Cols, 0)), Scalar.DarkRed, 3);
            Cv2.Line(flannView, (Point)(scene_corners[3] + new Point2d(gray1.Cols, 0)), (Point)(scene_corners[0] + new Point2d(gray1.Cols, 0)), Scalar.DarkRed, 3);

            Mat crop = new Mat();

            //TODO : 이미지 뽑아내기

            Cv2.Resize(flannView, flannView, new Size(1280, 960));
            
            using (new Window($"{min_dist}   SURF matching (by FlannBasedMatcher)", WindowMode.AutoSize, flannView))
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
