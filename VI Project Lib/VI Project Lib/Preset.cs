using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VI_Project_Lib
{
    public class Preset
    {
        public string PresetName { get; set; }

        public List<ROI> ROIs { get; set; }

        public Preset(string Name = "PresetNo.XX")
        {
            PresetName = Name;
        }
    }
}
