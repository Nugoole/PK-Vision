using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.UI;
using Emgu.CV.XFeatures2D;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using ZXing;
using Emgu.CV.Features2D;
using Emgu.CV.Cuda;

namespace VI_Project_Lib
{
    public class ImProcess
    {
        public ImProcess(string filepath)
        {
            ReadImage(filepath);
        }
        
        public Mat imgData { get; private set; }

        public Bitmap GetBitmap(Mat image = null)
        {
            if (image == null)
            {
                if (imgData != null)
                    return imgData.ToImage<Bgr, Byte>().ToBitmap();
                else
                    return null;
            }
            else
                return image.ToImage<Bgr, Byte>().ToBitmap();

            //CvInvoke.CvtColor(imgData, imgData, ColorConversion.Gray2Rgba);

        }
        public void ReadImage(string filepath)
        {
            imgData = CvInvoke.Imread(filepath, ImreadModes.Color);
        }

        public void printImage(string windowName)
        {
            //CvInvoke.Resize(image, image,new Size(image.Size.Width / 3, image.Size.Height / 3));

            CvInvoke.Imshow(windowName, imgData);
        }

        public void ChangeContrast(double value)
        {
            //imgData += 30;
            //imgData *= value;
        }
        public string GetBarcodeCode()
        {
            Bitmap bitmap = GetBitmap();
            IBarcodeReader reader = new BarcodeReader();

            Result[] results = reader.DecodeMultiple(bitmap);
            for (int i = 0; i < 2; i++)
                CvInvoke.Rectangle(imgData, new Rectangle((int)results[0].ResultPoints[i].X, (int)results[0].ResultPoints[i].Y, 10, 10), new MCvScalar(0, 255, 0));
            StringBuilder builder = new StringBuilder();
            foreach (var result in results)
            {
                builder.Append(result.Text);
            }

            if (builder != null)
                return builder.ToString();
            else
                return null;
        }

        public void CircleDetect()
        {
            CvInvoke.CvtColor(imgData, imgData, ColorConversion.Bgr2Gray);
            CvInvoke.GaussianBlur(imgData, imgData, new Size(9, 9), 3, 3);
            CircleF[] circles = CvInvoke.HoughCircles(imgData, HoughType.Gradient, 1, imgData.Rows / 50,50,40,0,30);

            foreach(var circle in circles)
            {
                CvInvoke.Circle(imgData, new Point((int)circle.Center.X, (int)circle.Center.Y), 5, new MCvScalar(255));
                //CvInvoke.Circle(imgData, new Point((int)circle.Center.X, (int)circle.Center.Y), (int)circle.Radius, new MCvScalar(255));
                CvInvoke.Rectangle(imgData, new Rectangle((int)circle.Center.X - (int)circle.Radius, (int)circle.Center.Y - (int)circle.Radius, 2 * (int)circle.Radius, 2 * (int)circle.Radius),new MCvScalar(255));
            }
        }

        public void doSurf(Mat observedImage, out VectorOfKeyPoint modelKeyPoints, out VectorOfKeyPoint observedKeyPoints,
            VectorOfVectorOfDMatch matches, out Mat mask, out Mat homography)
        {
            modelKeyPoints = new VectorOfKeyPoint();
            observedKeyPoints = new VectorOfKeyPoint();
            homography = null;

            double hessianThresh = 300;
            using (UMat uImage = imgData.GetUMat(AccessType.Read))
            using (UMat uObservedImage = observedImage.GetUMat(AccessType.Read))
            {
                
                SURF surf = new SURF(hessianThresh);
                UMat modelScriptors = new UMat();
                surf.DetectAndCompute(uImage, null, modelKeyPoints, modelScriptors, false);


                UMat observedDescriptors = new UMat();
                surf.DetectAndCompute(uObservedImage, null, observedKeyPoints, observedDescriptors, false);
                BFMatcher matcher = new BFMatcher(DistanceType.L2);
                matcher.Add(modelScriptors);

                matcher.KnnMatch(observedDescriptors, matches, 2, null);
                mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                mask.SetTo(new MCvScalar(255));
                Features2DToolbox.VoteForUniqueness(matches, 0.8, mask);

                int nonZeroCount = CvInvoke.CountNonZero(mask);

                if(nonZeroCount >=4)
                {
                    nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, matches, mask, 1.5, 20);

                    if (nonZeroCount >= 4)
                        homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints, matches, mask, 2);
                }
            }
        }

        public Mat getSurfResult()
        {
            string observedImagePath = "..\\..\\ImageSources\\observed.png";

            Mat homography;
            Mat observedImage = new ImProcess(observedImagePath).imgData;
            VectorOfKeyPoint modelKeyPoints;
            VectorOfKeyPoint observedKeyPoints;

            using(VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch())
            {
                Mat mask;
                
                
                doSurf(observedImage, out modelKeyPoints, out observedKeyPoints, matches,
                    out mask, out homography);

                Mat result = new Mat();
                Features2DToolbox.DrawMatches(imgData, modelKeyPoints, observedImage, observedKeyPoints, matches, result,
                    new MCvScalar(255, 255, 255), new MCvScalar(255, 255, 255), mask);
                
                
                if(homography != null)
                {
                    Rectangle rect = new Rectangle(Point.Empty, imgData.Size);
                    PointF[] pts = new PointF[]
                    {
                        new PointF(rect.Left, rect.Bottom),
                        new PointF(rect.Right, rect.Bottom),
                        new PointF(rect.Right, rect.Top),
                        new PointF(rect.Left,rect.Top)
                    };

                    pts = CvInvoke.PerspectiveTransform(pts, homography);

                    Point[] points = Array.ConvertAll<PointF, Point>(pts, Point.Round);
                    using(VectorOfPoint vp = new VectorOfPoint(points))
                    {
                        CvInvoke.Polylines(result, vp, true, new MCvScalar(255, 0, 0, 255), 5);
                    }

                    
                }
                return result;

            }
        }
    }
}
