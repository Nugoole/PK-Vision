using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using ZXing;

namespace VI_Project_Lib
{
    public class ImProcess
    { 
        public static Mat readImage(string filepath, ImreadModes mode)
        {
            return CvInvoke.Imread(filepath, mode);
        }

        public static void printImage(string windowName, Mat image)
        {
            CvInvoke.Imshow(windowName, image);
        }
        
        public static string getBarcodeCode(string filepath)
        {
            Bitmap bitmap = new Bitmap(filepath);
            IBarcodeReader reader = new BarcodeReader();

            Result result = reader.Decode(bitmap);

            if (result != null)
                return result.Text;
            else
                return "No Barcodes";
        }
    }
}
