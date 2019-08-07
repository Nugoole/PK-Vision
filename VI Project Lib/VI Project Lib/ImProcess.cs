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

        public Mat originalImg;
        public Mat processImg;
        public ImProcess(Mat img = null)
        {
            processImg = new Mat();
            if (img != null)
                img.CopyTo(processImg);
        }

        public ImProcess(string filepath)
        {
            processImg = new Mat();
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

        public static System.Drawing.Bitmap toBitmap(Mat mat)
        {
            return BitmapConverter.ToBitmap(mat);
        }
    }
}
