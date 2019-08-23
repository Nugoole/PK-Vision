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
    public enum CheckType { Soldering = 1, Pattern = 2, BarCode = 3};

    public class ROI
    {
        public string Name { get; set; }
        public Point location { get; set; }
        public CheckType checkType { get; set; }
        public Size ROISize { get; set; }

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
            checkType = (CheckType)Enum.Parse(typeof(CheckType), type.ToString());
        }

        public string Check(Mat original)
        {
            Rect roiRect = new Rect(location, ROISize);
            ImProcess process = new ImProcess(original[roiRect]);

            if (checkType == CheckType.Soldering)
            {
                return process.CircleDetect();
                original[roiRect] += process.processImg;
            }
            else if (checkType == CheckType.BarCode)
            {
                return process.GetBarcodeCode(process.processImg);
            }
            else if (checkType == CheckType.Pattern)
            {
                process.PatternCheck();
            }

            return null;            
        }
    }
}
