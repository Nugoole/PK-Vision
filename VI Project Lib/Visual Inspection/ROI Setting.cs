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
            if (NowPreset != null)
            {
                roitemp.location = new OpenCvSharp.Point(e.X, e.Y);
                mouseClicked = true;
            }
            else
                MessageBox.Show("프리셋을 선택해주세요!");
        }

        private void PictureBoxIpl1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBoxIpl1.Image = BitmapConverter.ToBitmap(temp);
            roitemp.location = roiRect.TopLeft;
            roitemp.ROISize = roiRect.Size;
            mouseClicked = false;
        }

        private void PictureBoxIpl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseClicked)
            {
                int X_Point = e.X;
                int Y_Point = e.Y;

                if (e.X >= pictureBoxIpl1.Width)
                    X_Point = pictureBoxIpl1.Width;

                if (e.Y >= pictureBoxIpl1.Height)
                    Y_Point = pictureBoxIpl1.Height;

                roiRect.Location = new OpenCvSharp.Point(Math.Min(roitemp.location.X, X_Point) * original.Cols / pictureBoxIpl1.Size.Width, Math.Min(roitemp.location.Y, Y_Point) * original.Rows / pictureBoxIpl1.Size.Height);
                roiRect.Size = new OpenCvSharp.Size(Math.Abs(roitemp.location.X - X_Point) * original.Cols / pictureBoxIpl1.Size.Width, Math.Abs(roitemp.location.Y - Y_Point) * original.Rows / pictureBoxIpl1.Size.Height);

                original.CopyTo(temp);
                Cv2.Rectangle(temp, roiRect, new Scalar(0, 255, 0), 3);

                NowPreset = presets.Find(x => x.PresetName == listbxPreset.SelectedItem.ToString());
                if (NowPreset.ROIs != null)
                    NowPreset.ROIs.ForEach(x => Cv2.Rectangle(temp, new Rect(x.location.X, x.location.Y, x.ROISize.Width, x.ROISize.Height), new Scalar(0, 255, 0), 3));
                pictureBoxIpl1.Image.Dispose();
                pictureBoxIpl1.Image = BitmapConverter.ToBitmap(temp);

            }
        }

        private void ListbxPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listbxPreset.SelectedItem != null)
            {
                lstbxROI.BeginUpdate();
                ROI_list.Clear();
                NowPreset = presets.Find(x => x.PresetName == listbxPreset.SelectedItem.ToString());
                if (NowPreset.ROIs.Count != 0)
                    ROI_list.AddRange(NowPreset.ROIs.Select(x => x.Name).ToArray());
                lstbxROI.EndUpdate();
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
            roitemp.checkType = (CheckType)Enum.Parse(typeof(CheckType), (comboBox1.SelectedIndex + 1).ToString());
            NowPreset.ROIs.Add(roitemp);
            lstbxROI.BeginUpdate();
            ROI_list.Add(roitemp.Name);
            lstbxROI.EndUpdate();
            MessageBox.Show($"{roitemp.Name} , {(int)roitemp.checkType}, {roitemp.checkType}");
            roitemp = new ROI($"ROI {NowPreset.ROIs.Count}", (CheckType)Enum.Parse(typeof(CheckType), (comboBox1.SelectedIndex + 1).ToString()));
            
        }

        private void ROI_Setting_Load(object sender, EventArgs e)
        {
            listbxPreset.BeginUpdate();
            Preset.LoadJson(presets);
            presets.ForEach(x => listbxPreset.Items.Add(x.PresetName));
            listbxPreset.EndUpdate();
            //TODO : 콤보박스에 타입 넣기
            comboBox1.Items.Add("Soldering");
            comboBox1.Items.Add("Pattern");
            comboBox1.Items.Add("Barcode");
        }

        private void LstbxROI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                NowPreset.ROIs.Remove(NowPreset.ROIs.Find(x => x.Name == ROI_list[lstbxROI.SelectedIndex].ToString()));
                lstbxROI.BeginUpdate();
                ROI_list.Clear();
                NowPreset = presets.Find(x => x.PresetName == listbxPreset.SelectedItem.ToString());
                if (NowPreset.ROIs.Count != 0)
                    ROI_list.AddRange(NowPreset.ROIs.Select(x => x.Name).ToArray());
                lstbxROI.EndUpdate();

                NowPreset.SaveJson();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
