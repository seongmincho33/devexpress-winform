namespace XmlViewer
{
    partial class frmViewer
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
            this.tabControl_Search = new System.Windows.Forms.TabControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemoveTab = new System.Windows.Forms.Button();
            this.btnCreateSearchTab = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_Search
            // 
            this.tabControl_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Search.Location = new System.Drawing.Point(0, 52);
            this.tabControl_Search.Name = "tabControl_Search";
            this.tabControl_Search.SelectedIndex = 0;
            this.tabControl_Search.Size = new System.Drawing.Size(1476, 657);
            this.tabControl_Search.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRemoveTab);
            this.groupBox1.Controls.Add(this.btnCreateSearchTab);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1476, 52);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "창 관리";
            // 
            // btnRemoveTab
            // 
            this.btnRemoveTab.Location = new System.Drawing.Point(154, 20);
            this.btnRemoveTab.Name = "btnRemoveTab";
            this.btnRemoveTab.Size = new System.Drawing.Size(136, 23);
            this.btnRemoveTab.TabIndex = 1;
            this.btnRemoveTab.Text = "검색창 삭제";
            this.btnRemoveTab.UseVisualStyleBackColor = true;
            // 
            // btnCreateSearchTab
            // 
            this.btnCreateSearchTab.Location = new System.Drawing.Point(12, 20);
            this.btnCreateSearchTab.Name = "btnCreateSearchTab";
            this.btnCreateSearchTab.Size = new System.Drawing.Size(136, 23);
            this.btnCreateSearchTab.TabIndex = 0;
            this.btnCreateSearchTab.Text = "검색창 생성";
            this.btnCreateSearchTab.UseVisualStyleBackColor = true;
            // 
            // frmViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1476, 709);
            this.Controls.Add(this.tabControl_Search);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmViewer";
            this.Text = "XmlViewer";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl_Search;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCreateSearchTab;
        private System.Windows.Forms.Button btnRemoveTab;
    }
}

