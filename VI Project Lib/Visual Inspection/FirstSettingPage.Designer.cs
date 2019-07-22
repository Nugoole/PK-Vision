namespace Visual_Inspection
{
    partial class FirstSettingPage
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
            this.outputPic = new Emgu.CV.UI.PanAndZoomPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.outputPic)).BeginInit();
            this.SuspendLayout();
            // 
            // outputPic
            // 
            this.outputPic.Location = new System.Drawing.Point(12, 12);
            this.outputPic.Name = "outputPic";
            this.outputPic.Size = new System.Drawing.Size(703, 590);
            this.outputPic.TabIndex = 0;
            this.outputPic.TabStop = false;
            this.outputPic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OutputPic_MouseDown);
            // 
            // FirstSettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 640);
            this.Controls.Add(this.outputPic);
            this.Name = "FirstSettingPage";
            this.Text = "FirstSettingPage";
            ((System.ComponentModel.ISupportInitialize)(this.outputPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.PanAndZoomPictureBox outputPic;
    }
}