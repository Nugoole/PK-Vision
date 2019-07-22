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
        public ImProcess(string filepath)
        {
            ReadImage(filepath);
        }
        
        private Mat imgData { get; set; }

        public Bitmap GetBitmap()
        {
            return imgData.ToImage<Bgr, Byte>().ToBitmap();
        }
        public void ReadImage(string filepath)
        {
            imgData = CvInvoke.Imread(filepath,ImreadModes.Grayscale);
        }

        public void printImage(string windowName)
        {
            //CvInvoke.Resize(image, image,new Size(image.Size.Width / 3, image.Size.Height / 3));
                
            CvInvoke.Imshow(windowName, imgData);
        }   
        
        public string getBarcodeCode()
        {
            Bitmap bitmap = GetBitmap();
            IBarcodeReader reader = new BarcodeReader();

            Result[] results = reader.DecodeMultiple(bitmap);
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
    }
}
