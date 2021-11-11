
namespace delegateSample003
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
            this.lstBox = new System.Windows.Forms.ListBox();
            this.txtCaption2 = new System.Windows.Forms.TextBox();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnTest = new delegateSample003.MyButton();
            this.myButton1 = new delegateSample003.MyButton();
            this.SuspendLayout();
            // 
            // lstBox
            // 
            this.lstBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBox.FormattingEnabled = true;
            this.lstBox.ItemHeight = 12;
            this.lstBox.Location = new System.Drawing.Point(13, 43);
            this.lstBox.Name = "lstBox";
            this.lstBox.Size = new System.Drawing.Size(775, 388);
            this.lstBox.TabIndex = 1;
            // 
            // txtCaption2
            // 
            this.txtCaption2.Location = new System.Drawing.Point(193, 13);
            this.txtCaption2.Name = "txtCaption2";
            this.txtCaption2.Size = new System.Drawing.Size(189, 21);
            this.txtCaption2.TabIndex = 2;
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(389, 13);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(116, 23);
            this.btnChange.TabIndex = 3;
            this.btnChange.Text = "Caption2 변경";
            this.btnChange.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Caption2 = "테스트2";
            this.btnTest.Location = new System.Drawing.Point(13, 13);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            // 
            // myButton1
            // 
            this.myButton1.Caption2 = "aaaa";
            this.myButton1.Location = new System.Drawing.Point(649, 13);
            this.myButton1.Name = "myButton1";
            this.myButton1.Size = new System.Drawing.Size(75, 23);
            this.myButton1.TabIndex = 4;
            this.myButton1.Text = "myButton1";
            this.myButton1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.myButton1);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.txtCaption2);
            this.Controls.Add(this.lstBox);
            this.Controls.Add(this.btnTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyButton btnTest;
        private System.Windows.Forms.ListBox lstBox;
        private System.Windows.Forms.TextBox txtCaption2;
        private System.Windows.Forms.Button btnChange;
        private MyButton myButton1;
    }
}

