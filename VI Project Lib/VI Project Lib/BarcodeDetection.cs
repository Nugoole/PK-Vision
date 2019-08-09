
using OpenCvSharp;
using System.Drawing;
using System.Text;
using ZXing;

namespace VI_Project_Lib
{
    public partial class ImProcess
    {
        public string GetBarcodeCode()
        {
            Bitmap bitmap = GetBitmap();
            IBarcodeReader reader = new BarcodeReader();


            Result[] results = reader.DecodeMultiple(bitmap);

            if (results == null)
                return "nothing";

            for (int i = 0; i < 2; i++)
                Cv2.Rectangle(originalImg, new Rect((int)results[0].ResultPoints[i].X, (int)results[0].ResultPoints[i].Y, 50, 50), new Scalar(0, 255, 0));
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
