namespace Visual_Inspection
{
    partial class ROI_Setting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxIpl1 = new OpenCvSharp.UserInterface.PictureBoxIpl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startLocationVal = new System.Windows.Forms.Label();
            this.NowLocationVal = new System.Windows.Forms.Label();
            this.lstbxROI = new System.Windows.Forms.ListBox();
            this.listbxPreset = new System.Windows.Forms.ListBox();
            this.btnSavePreset = new System.Windows.Forms.Button();
            this.btnNewPreset = new System.Windows.Forms.Button();
            this.btnSaveROI = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIpl1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxIpl1
            // 
            this.pictureBoxIpl1.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxIpl1.Name = "pictureBoxIpl1";
            this.pictureBoxIpl1.Size = new System.Drawing.Size(483, 414);
            this.pictureBoxIpl1.TabIndex = 0;
            this.pictureBoxIpl1.TabStop = false;
            this.pictureBoxIpl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxIpl1_MouseDown);
            this.pictureBoxIpl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxIpl1_MouseMove);
            this.pictureBoxIpl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBoxIpl1_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(529, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "startLocation : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(529, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Now Location : ";
            // 
            // startLocationVal
            // 
            this.startLocationVal.AutoSize = true;
            this.startLocationVal.Location = new System.Drawing.Point(624, 71);
            this.startLocationVal.Name = "startLocationVal";
            this.startLocationVal.Size = new System.Drawing.Size(0, 12);
            this.startLocationVal.TabIndex = 2;
            // 
            // NowLocationVal
            // 
            this.NowLocationVal.AutoSize = true;
            this.NowLocationVal.Location = new System.Drawing.Point(630, 137);
            this.NowLocationVal.Name = "NowLocationVal";
            this.NowLocationVal.Size = new System.Drawing.Size(0, 12);
            this.NowLocationVal.TabIndex = 2;
            // 
            // lstbxROI
            // 
            this.lstbxROI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstbxROI.FormattingEnabled = true;
            this.lstbxROI.ItemHeight = 12;
            this.lstbxROI.Location = new System.Drawing.Point(662, 190);
            this.lstbxROI.Name = "lstbxROI";
            this.lstbxROI.Size = new System.Drawing.Size(99, 194);
            this.lstbxROI.TabIndex = 3;
            // 
            // listbxPreset
            // 
            this.listbxPreset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listbxPreset.FormattingEnabled = true;
            this.listbxPreset.ItemHeight = 12;
            this.listbxPreset.Location = new System.Drawing.Point(531, 190);
            this.listbxPreset.Name = "listbxPreset";
            this.listbxPreset.Size = new System.Drawing.Size(99, 194);
            this.listbxPreset.TabIndex = 3;
            this.listbxPreset.SelectedIndexChanged += new System.EventHandler(this.ListbxPreset_SelectedIndexChanged);
            // 
            // btnSavePreset
            // 
            this.btnSavePreset.Location = new System.Drawing.Point(522, 403);
            this.btnSavePreset.Name = "btnSavePreset";
            this.btnSavePreset.Size = new System.Drawing.Size(108, 23);
            this.btnSavePreset.TabIndex = 4;
            this.btnSavePreset.Text = "Save Preset";
            this.btnSavePreset.UseVisualStyleBackColor = true;
            this.btnSavePreset.Click += new System.EventHandler(this.BtnSavePreset_Click);
            // 
            // btnNewPreset
            // 
            this.btnNewPreset.Location = new System.Drawing.Point(525, 21);
            this.btnNewPreset.Name = "btnNewPreset";
            this.btnNewPreset.Size = new System.Drawing.Size(93, 23);
            this.btnNewPreset.TabIndex = 5;
            this.btnNewPreset.Text = "New Preset";
            this.btnNewPreset.UseVisualStyleBackColor = true;
            this.btnNewPreset.Click += new System.EventHandler(this.BtnNewPreset_Click);
            // 
            // btnSaveROI
            // 
            this.btnSaveROI.Location = new System.Drawing.Point(662, 21);
            this.btnSaveROI.Name = "btnSaveROI";
            this.btnSaveROI.Size = new System.Drawing.Size(75, 23);
            this.btnSaveROI.TabIndex = 4;
            this.btnSaveROI.Text = "Save ROI";
            this.btnSaveROI.UseVisualStyleBackColor = true;
            this.btnSaveROI.Click += new System.EventHandler(this.BtnSaveROI_Click);
            // 
            // ROI_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnNewPreset);
            this.Controls.Add(this.btnSavePreset);
            this.Controls.Add(this.btnSaveROI);
            this.Controls.Add(this.listbxPreset);
            this.Controls.Add(this.lstbxROI);
            this.Controls.Add(this.NowLocationVal);
            this.Controls.Add(this.startLocationVal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxIpl1);
            this.Name = "ROI_Setting";
            this.Text = "ROI_Setting";
            this.Load += new System.EventHandler(this.ROI_Setting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIpl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenCvSharp.UserInterface.PictureBoxIpl pictureBoxIpl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label startLocationVal;
        private System.Windows.Forms.Label NowLocationVal;
        private System.Windows.Forms.ListBox lstbxROI;
        private System.Windows.Forms.ListBox listbxPreset;
        private System.Windows.Forms.Button btnSavePreset;
        private System.Windows.Forms.Button btnNewPreset;
        private System.Windows.Forms.Button btnSaveROI;
    }
}