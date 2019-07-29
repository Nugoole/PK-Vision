using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using VI_Project_Lib;

namespace Visual_Inspection
{
    public partial class ROI_Setting : Form
    {
        private bool mouseClicked { get; set; } = false;
        public Rect roiRect;
        private Mat original { get; set; }
        public List<Preset> presets { get; set; }
        public Preset NowPreset { get; set; }
        public Mat temp { get; set; }

        public ROI roitemp { get; set; }
        public ListBox.ObjectCollection ROI_list { get; set; }


        public ROI_Setting(Mat img)
        {
            InitializeComponent();

            //원 이미지 저장
            original = new Mat();
            img.CopyTo(original);


            temp = new Mat();
            presets = new List<Preset>();
            roitemp = new ROI(null, CheckType.Soldering);
            roiRect = new Rect();
            ROI_list = new ListBox.ObjectCollection(lstbxROI);
            

            pictureBoxIpl1.Image = BitmapConverter.ToBitmap(img);
            pictureBoxIpl1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void PictureBoxIpl1_MouseDown(object sender, MouseEventArgs e)
        {
            roitemp.location = new OpenCvSharp.Point(e.X , e.Y );
            mouseClicked = true;
        }

        private void PictureBoxIpl1_MouseUp(object sender, MouseEventArgs e)
        {
            Cv2.Line(temp, 137, 0, 137, 200, new Scalar(255, 255, 255), 5);
            pictureBoxIpl1.Image = BitmapConverter.ToBitmap(temp);
            roitemp.location  = new OpenCvSharp.Point(roitemp.location.X * original.Cols / pictureBoxIpl1.Size.Width, roitemp.location.Y * original.Rows / pictureBoxIpl1.Size.Height);
            roitemp.ROISize = roiRect.Size;//new OpenCvSharp.Size(roiRect.Width * original.Cols / pictureBoxIpl1.Size.Width, roiRect.Size.Height * original.Rows / pictureBoxIpl1.Size.Height);
            roitemp.Check(temp);
            //Cv2.ImShow("cropped", roitemp.roi);
            mouseClicked = false;
        }

        private void PictureBoxIpl1_MouseMove(object sender, MouseEventArgs e)
        {
            if(mouseClicked)
            {
                roiRect.Location = new OpenCvSharp.Point(Math.Min(roitemp.location.X, e.X) * original.Cols / pictureBoxIpl1.Size.Width , Math.Min(roitemp.location.Y, e.Y) * original.Rows / pictureBoxIpl1.Size.Height);
                roiRect.Size = new OpenCvSharp.Size(Math.Abs(roitemp.location.X - e.X) * original.Cols / pictureBoxIpl1.Size.Width, Math.Abs(roitemp.location.Y - e.Y) * original.Rows / pictureBoxIpl1.Size.Height);
                
                original.CopyTo(temp);
                Cv2.Rectangle(temp, roiRect, new Scalar(0, 255, 0), 3);

                NowPreset = presets.Where(x => x.PresetName == listbxPreset.SelectedItem.ToString()).ToList()[0];
                if(NowPreset.ROIs != null)
                    NowPreset.ROIs.ForEach(x => Cv2.Rectangle(temp, new Rect(x.location.X , x.location.Y , x.ROISize.Width, x.ROISize.Height), new Scalar(0, 255, 0), 3));
                pictureBoxIpl1.Image.Dispose();
                pictureBoxIpl1.Image = BitmapConverter.ToBitmap(temp);
            }
        }


        private void ListbxPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listbxPreset.SelectedItem != null)
            {
                lstbxROI.Items.Clear();
                NowPreset = presets.Where(x => x.PresetName == listbxPreset.SelectedItem.ToString()).ToList()[0];
                if (NowPreset.ROIs.Count != 0)
                    lstbxROI.Items.AddRange(NowPreset.ROIs.Select(x => x.Name).ToArray());
            }
        }

        private void BtnNewPreset_Click(object sender, EventArgs e)
        {
            PresetInput presetNameForm = new PresetInput();
            Preset preset = new Preset();

            if (presetNameForm.ShowDialog(this) == DialogResult.OK)
            {
                preset.PresetName = presetNameForm.textBox1.Text;
                listbxPreset.Items.Add(preset.PresetName);
                presets.Add(preset);
            }
            
            presetNameForm.Dispose();
        }

        private void BtnSavePreset_Click(object sender, EventArgs e)
        {
            NowPreset.SaveJson();
        }

        private void BtnSaveROI_Click(object sender, EventArgs e)
        {
            NowPreset = presets.Find(x => x.PresetName == listbxPreset.SelectedItem.ToString());
            roitemp.Name = $"ROI {NowPreset.ROIs.Count}";
            NowPreset.ROIs.Add(roitemp);
            lstbxROI.BeginUpdate();
            ROI_list.Add(roitemp.Name);
            lstbxROI.EndUpdate();

            roitemp = new ROI($"ROI {NowPreset.ROIs.Count}", CheckType.Soldering);
            MessageBox.Show(roitemp.Name);
        }

        //TODO : 로드되어 리스트에는 나오나 이미지 작업이 불가능 이를 수정
        private void ROI_Setting_Load(object sender, EventArgs e)
        {
            listbxPreset.BeginUpdate();
            Preset.LoadJson(presets);
            presets.ForEach(x => listbxPreset.Items.Add(x.PresetName));
            listbxPreset.EndUpdate();
        }
    }
}
