using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VI_Project_Lib
{
    public class ROIs
    {
        public List<Mat> roi { get; private set; }

        public ROIs()
        {
            roi = new List<Mat>();
        }


    }
}
