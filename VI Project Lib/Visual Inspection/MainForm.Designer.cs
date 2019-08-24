namespace Visual_Inspection
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.barcodelabel = new System.Windows.Forms.Label();
            this.barcode = new System.Windows.Forms.Label();
            this.pictureBoxIpl1 = new OpenCvSharp.UserInterface.PictureBoxIpl();
            this.button2 = new System.Windows.Forms.Button();
            this.ImageSocket = new System.ComponentModel.BackgroundWorker();
            this.CommandSocket = new System.ComponentModel.BackgroundWorker();
            this.faillabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIpl1)).BeginInit();
            this.SuspendLayout();
            // 
            // barcodelabel
            // 
            this.barcodelabel.AutoSize = true;
            this.barcodelabel.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.barcodelabel.Location = new System.Drawing.Point(597, 478);
            this.barcodelabel.Name = "barcodelabel";
            this.barcodelabel.Size = new System.Drawing.Size(120, 24);
            this.barcodelabel.TabIndex = 2;
            this.barcodelabel.Text = "Barcode : ";
            // 
            // barcode
            // 
            this.barcode.AutoSize = true;
            this.barcode.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.barcode.Location = new System.Drawing.Point(699, 478);
            this.barcode.Name = "barcode";
            this.barcode.Size = new System.Drawing.Size(0, 24);
            this.barcode.TabIndex = 3;
            // 
            // pictureBoxIpl1
            // 
            this.pictureBoxIpl1.Location = new System.Drawing.Point(14, 15);
            this.pictureBoxIpl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBoxIpl1.Name = "pictureBoxIpl1";
            this.pictureBoxIpl1.Size = new System.Drawing.Size(562, 486);
            this.pictureBoxIpl1.TabIndex = 6;
            this.pictureBoxIpl1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(793, 149);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 29);
            this.button2.TabIndex = 7;
            this.button2.Text = "Initialize";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // ImageSocket
            // 
            this.ImageSocket.WorkerReportsProgress = true;
            this.ImageSocket.WorkerSupportsCancellation = true;
            this.ImageSocket.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Socket_DoWork);
            this.ImageSocket.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Socket_ProgressChanged);
            this.ImageSocket.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Socket_RunWorkerCompleted);
            // 
            // CommandSocket
            // 
            this.CommandSocket.WorkerReportsProgress = true;
            this.CommandSocket.WorkerSupportsCancellation = true;
            this.CommandSocket.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CommandSocket_DoWork);
            this.CommandSocket.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.CommandSocket_ProgressChanged);
            this.CommandSocket.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CommandSocket_RunWorkerCompleted);
            // 
            // faillabel
            // 
            this.faillabel.AutoSize = true;
            this.faillabel.Location = new System.Drawing.Point(620, 345);
            this.faillabel.Name = "faillabel";
            this.faillabel.Size = new System.Drawing.Size(0, 15);
            this.faillabel.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 562);
            this.Controls.Add(this.faillabel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBoxIpl1);
            this.Controls.Add(this.barcode);
            this.Controls.Add(this.barcodelabel);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIpl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label barcodelabel;
        private System.Windows.Forms.Label barcode;
        private OpenCvSharp.UserInterface.PictureBoxIpl pictureBoxIpl1;
        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker ImageSocket;
        private System.ComponentModel.BackgroundWorker CommandSocket;
        private System.Windows.Forms.Label faillabel;
    }
}

