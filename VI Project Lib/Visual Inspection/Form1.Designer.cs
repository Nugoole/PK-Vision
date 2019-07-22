namespace Visual_Inspection
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.outputImage = new System.Windows.Forms.PictureBox();
            this.barcodelabel = new System.Windows.Forms.Label();
            this.barcode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.outputImage)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(683, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // outputImage
            // 
            this.outputImage.Location = new System.Drawing.Point(12, 22);
            this.outputImage.Name = "outputImage";
            this.outputImage.Size = new System.Drawing.Size(485, 399);
            this.outputImage.TabIndex = 1;
            this.outputImage.TabStop = false;
            // 
            // barcodelabel
            // 
            this.barcodelabel.AutoSize = true;
            this.barcodelabel.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.barcodelabel.Location = new System.Drawing.Point(503, 123);
            this.barcodelabel.Name = "barcodelabel";
            this.barcodelabel.Size = new System.Drawing.Size(95, 19);
            this.barcodelabel.TabIndex = 2;
            this.barcodelabel.Text = "Barcode : ";
            // 
            // barcode
            // 
            this.barcode.AutoSize = true;
            this.barcode.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.barcode.Location = new System.Drawing.Point(593, 123);
            this.barcode.Name = "barcode";
            this.barcode.Size = new System.Drawing.Size(0, 19);
            this.barcode.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.barcode);
            this.Controls.Add(this.barcodelabel);
            this.Controls.Add(this.outputImage);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.outputImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox outputImage;
        private System.Windows.Forms.Label barcodelabel;
        private System.Windows.Forms.Label barcode;
    }
}

