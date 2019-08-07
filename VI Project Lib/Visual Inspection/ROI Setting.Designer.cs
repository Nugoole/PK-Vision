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
            this.lstbxROI = new System.Windows.Forms.ListBox();
            this.listbxPreset = new System.Windows.Forms.ListBox();
            this.btnSavePreset = new System.Windows.Forms.Button();
            this.btnNewPreset = new System.Windows.Forms.Button();
            this.btnSaveROI = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
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
            // lstbxROI
            // 
            this.lstbxROI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstbxROI.FormattingEnabled = true;
            this.lstbxROI.ItemHeight = 12;
            this.lstbxROI.Location = new System.Drawing.Point(662, 158);
            this.lstbxROI.Name = "lstbxROI";
            this.lstbxROI.Size = new System.Drawing.Size(99, 194);
            this.lstbxROI.TabIndex = 3;
            this.lstbxROI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstbxROI_KeyDown);
            // 
            // listbxPreset
            // 
            this.listbxPreset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listbxPreset.FormattingEnabled = true;
            this.listbxPreset.ItemHeight = 12;
            this.listbxPreset.Location = new System.Drawing.Point(525, 158);
            this.listbxPreset.Name = "listbxPreset";
            this.listbxPreset.Size = new System.Drawing.Size(99, 194);
            this.listbxPreset.TabIndex = 3;
            this.listbxPreset.SelectedIndexChanged += new System.EventHandler(this.ListbxPreset_SelectedIndexChanged);
            // 
            // btnSavePreset
            // 
            this.btnSavePreset.Location = new System.Drawing.Point(525, 358);
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
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(525, 91);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(523, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(543, 403);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(676, 403);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ROI_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnNewPreset);
            this.Controls.Add(this.btnSavePreset);
            this.Controls.Add(this.btnSaveROI);
            this.Controls.Add(this.listbxPreset);
            this.Controls.Add(this.lstbxROI);
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
        private System.Windows.Forms.ListBox lstbxROI;
        public System.Windows.Forms.ListBox listbxPreset;
        private System.Windows.Forms.Button btnSavePreset;
        private System.Windows.Forms.Button btnNewPreset;
        private System.Windows.Forms.Button btnSaveROI;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}