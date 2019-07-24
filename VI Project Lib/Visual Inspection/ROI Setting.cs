using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visual_Inspection
{
    public partial class ROI_Setting : Form
    {
        private bool mouseClicked { get; set; } = false;
        public ROI_Setting()
        {
            InitializeComponent();
        }

        private void PictureBoxIpl1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseClicked = true;
        }

        private void PictureBoxIpl1_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
