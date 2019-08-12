using OpenCvSharp;
using OpenCvSharp.XFeatures2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VI_Project_Lib
{
    partial class ImProcess
    {
        public void MatchBySurf(string filepath)
        {
            Mat src1 = new Mat();
            originalImg.CopyTo(src1);
            Mat src2 = new Mat(filepath);

            //크기 조정
            Cv2.Resize(src2, src2, new Size(src2.Size().Width / 2, src2.Size().Height / 2));
            //Cv2.Resize(src1, src1, new Size(src2.Size().Width / 2, src2.Size().Height / 2));


            var gray1 = new Mat();
            var gray2 = new Mat();

            Cv2.CvtColor(src1, gray1, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(src2, gray2, ColorConversionCodes.BGR2GRAY);

            var surf = SIFT.Create(400);


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



            // Draw matches
            var flannView = new Mat();

            Cv2.DrawMatches(gray1, keypoints1, gray2, keypoints2, goodMatches, flannView, new Scalar(0, 255, 0), new Scalar(255, 255, 0), null, DrawMatchesFlags.Default);

            List<Point2f> obj = new List<Point2f>();
            List<Point2f> scene = new List<Point2f>();
            if (goodMatches.Count == 0) return;

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

            if (H.Empty()) return;
            scene_corners = Cv2.PerspectiveTransform(obj_corners, H).ToList();

            Mat h2 = Cv2.GetPerspectiveTransform(scene_corners.ConvertAll(Point2dToPoint2f), obj_corners.ConvertAll(Point2dToPoint2f));

            Mat crop = new Mat();

            Cv2.WarpPerspective(src2, crop, h2, src1.Size());
            //Cv2.ImShow("original", src1);
            new Window("Cropped", WindowMode.AutoSize, crop);

            crop.CopyTo(processImg);
        }

        private static Point2d Point2fToPoint2d(Point2f pf)
        {
            return new Point2d(((int)pf.X), ((int)pf.Y));
        }

        private static Point2f Point2dToPoint2f(Point2d pf)
        {
            return new Point2f(((float)pf.X), ((float)pf.Y));
        }
    }
}
