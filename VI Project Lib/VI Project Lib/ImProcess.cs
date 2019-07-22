using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using ZXing;

namespace VI_Project_Lib
{
    public class ImProcess
    {
        public ImProcess(string filepath)
        {
            ReadImage(filepath);
        }
        
        private Mat imgData { get; set; }

        public Bitmap GetBitmap()
        {
            //CvInvoke.CvtColor(imgData, imgData, ColorConversion.Gray2Rgba);
            return imgData.ToImage<Bgr, Byte>().ToBitmap();
        }
        public void ReadImage(string filepath)
        {
            imgData = CvInvoke.Imread(filepath, ImreadModes.Grayscale);
        }

        public void printImage(string windowName)
        {
            //CvInvoke.Resize(image, image,new Size(image.Size.Width / 3, image.Size.Height / 3));

            CvInvoke.Imshow(windowName, imgData);
        }

        public void ChangeContrast(double value)
        {
            imgData += 30;
            imgData *= value;
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
            VectorOfFloat circles = new VectorOfFloat(3);
            CvInvoke.HoughCircles(imgData, circles, HoughType.Gradient, 1,imgData.Rows / 8, 200, 50, 0 ,0);
        }
    }
}
