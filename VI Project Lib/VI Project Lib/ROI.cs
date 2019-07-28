using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace VI_Project_Lib
{
    public enum CheckType { Soldering = 1, Pattern = 2};

    public class ROI
    {
        public string Name { get; set; }
        public Point location { get; set; }
        public CheckType checkType { get; private set; }

        [ScriptIgnore]
        public Mat roi;

        [JsonConstructor]
        public ROI(string name, Point _location, CheckType type)
        {
            
            Name = name;
            location = _location;
            checkType = type;
        }

        public ROI(string RoiName, CheckType type)
        {
            Name = RoiName;
            checkType = type;
            roi = new Mat();
        }

        public void Check(Mat original)
        {
            ImProcess process = new ImProcess(ref roi);
            
            Rect roiRect = new Rect(location, roi.Size());
            process.CircleDetect();
            original[roiRect] += process.imgData;
        }
    }
}
