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
        public Point location { get; set; }
        public Mat roi;
        public CheckType checkType { get; private set; }
        public ROI( string RoiName, CheckType type)
        {
            Name = RoiName;
            checkType = type;
            roi = new Mat();
        }

        public int Check(Mat original)
        {
            ImProcess process = new ImProcess(ref roi);
            
            Rect roiRect = new Rect(location, roi.Size());
            return process.CircleDetect();
            original[roiRect] += process.imgData;
        }
    }
}
