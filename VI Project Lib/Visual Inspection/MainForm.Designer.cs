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
            this.btnDoProcess = new System.Windows.Forms.Button();
            this.barcodelabel = new System.Windows.Forms.Label();
            this.barcode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.solderingfails = new System.Windows.Forms.Label();
            this.pictureBoxIpl1 = new OpenCvSharp.UserInterface.PictureBoxIpl();
            this.button2 = new System.Windows.Forms.Button();
            this.Socket = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIpl1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDoProcess
            // 
            this.btnDoProcess.Location = new System.Drawing.Point(694, 65);
            this.btnDoProcess.Name = "btnDoProcess";
            this.btnDoProcess.Size = new System.Drawing.Size(75, 23);
            this.btnDoProcess.TabIndex = 0;
            this.btnDoProcess.Text = "DoProcess";
            this.btnDoProcess.UseVisualStyleBackColor = true;
            this.btnDoProcess.Click += new System.EventHandler(this.btnDoProcess_Click);
            // 
            // barcodelabel
            // 
            this.barcodelabel.AutoSize = true;
            this.barcodelabel.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.barcodelabel.Location = new System.Drawing.Point(522, 382);
            this.barcodelabel.Name = "barcodelabel";
            this.barcodelabel.Size = new System.Drawing.Size(95, 19);
            this.barcodelabel.TabIndex = 2;
            this.barcodelabel.Text = "Barcode : ";
            // 
            // barcode
            // 
            this.barcode.AutoSize = true;
            this.barcode.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.barcode.Location = new System.Drawing.Point(612, 382);
            this.barcode.Name = "barcode";
            this.barcode.Size = new System.Drawing.Size(0, 19);
            this.barcode.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(522, 338);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Soldering Fails : ";
            // 
            // solderingfails
            // 
            this.solderingfails.AutoSize = true;
            this.solderingfails.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.solderingfails.Location = new System.Drawing.Point(658, 338);
            this.solderingfails.Name = "solderingfails";
            this.solderingfails.Size = new System.Drawing.Size(0, 19);
            this.solderingfails.TabIndex = 5;
            // 
            // pictureBoxIpl1
            // 
            this.pictureBoxIpl1.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxIpl1.Name = "pictureBoxIpl1";
            this.pictureBoxIpl1.Size = new System.Drawing.Size(492, 389);
            this.pictureBoxIpl1.TabIndex = 6;
            this.pictureBoxIpl1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(694, 119);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Initialize";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Socket
            // 
            this.Socket.WorkerReportsProgress = true;
            this.Socket.WorkerSupportsCancellation = true;
            this.Socket.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Socket_DoWork);
            this.Socket.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Socket_ProgressChanged);
            this.Socket.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Socket_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBoxIpl1);
            this.Controls.Add(this.solderingfails);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.barcode);
            this.Controls.Add(this.barcodelabel);
            this.Controls.Add(this.btnDoProcess);
            this.Name = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIpl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDoProcess;
        private System.Windows.Forms.Label barcodelabel;
        private System.Windows.Forms.Label barcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label solderingfails;
        private OpenCvSharp.UserInterface.PictureBoxIpl pictureBoxIpl1;
        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker Socket;
    }
}

