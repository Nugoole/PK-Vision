﻿
using OpenCvSharp;
using System.Drawing;
using System.Text;
using VI_DB.DB;
using ZXing;


namespace VI_Project_Lib
{
    public partial class ImProcess
    {
        public string GetBarcodeCode(Mat img = null)
        {
            //Cv2.Resize(img, img, new OpenCvSharp.Size(), 1.0,1.0);
            //Cv2.CvtColor(img, img, ColorConversionCodes.BGR2GRAY);
            //Cv2.Threshold(img, img, 200, 255, ThresholdTypes.Binary);
            //Cv2.ImShow("Barcode", img);
            Bitmap bitmap = GetBitmap(img);
            BarcodeReader reader = new BarcodeReader();
            
            Result results = reader.Decode(bitmap);

            if (results == null)
                return "nothing";
            else
                return results.Text;

            
            //for (int i = 0; i < results.Length; i++)
            //    Cv2.Rectangle(imgData, new Rect((int)results[0].ResultPoints[i].X, (int)results[0].ResultPoints[i].Y, 50, 50), new Scalar(0, 255, 0));
            //StringBuilder builder = new StringBuilder();
            //foreach (var result in results)
            //{
            //    builder.Append(result.Text);
            //}

            //if (resu)
            //    return builder.ToString();
            //else
            //    return null;
            
        }
    }
}
