using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VI_Project_Lib
{
    public enum CheckType { Soldering = 1, Pattern = 2};

    public class ROI
    {
        public string Name { get; private set; }
        public Point location { get; private set; }
        public Mat roi { get; private set; }
        public CheckType checkType { get; private set; }
        public ROI(Mat crop, Point point, CheckType type, string RoiName)
        {
            Name = RoiName;
            roi = crop;
            location = point;
            checkType = type;
        }

        public void Check(Mat original)
        {
            ImProcess process = new ImProcess(original);
            process.CircleDetect();
            Rect roiRect = new Rect(location, roi.Size());
            original[roiRect] += process.imgData;
        }
    }
}
