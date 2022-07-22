namespace Viewer
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTabName = new System.Windows.Forms.TextBox();
            this.btnRemoveTab = new System.Windows.Forms.Button();
            this.btnCreateSearchTab = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_Search
            // 
            this.tabControl_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Search.Location = new System.Drawing.Point(0, 99);
            this.tabControl_Search.Name = "tabControl_Search";
            this.tabControl_Search.SelectedIndex = 0;
            this.tabControl_Search.Size = new System.Drawing.Size(1476, 610);
            this.tabControl_Search.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTabName);
            this.groupBox1.Controls.Add(this.btnRemoveTab);
            this.groupBox1.Controls.Add(this.btnCreateSearchTab);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1476, 58);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "창 관리";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "창 이름 : ";
            // 
            // txtTabName
            // 
            this.txtTabName.Location = new System.Drawing.Point(75, 20);
            this.txtTabName.Name = "txtTabName";
            this.txtTabName.Size = new System.Drawing.Size(115, 21);
            this.txtTabName.TabIndex = 2;
            // 
            // btnRemoveTab
            // 
            this.btnRemoveTab.Location = new System.Drawing.Point(284, 20);
            this.btnRemoveTab.Name = "btnRemoveTab";
            this.btnRemoveTab.Size = new System.Drawing.Size(82, 23);
            this.btnRemoveTab.TabIndex = 1;
            this.btnRemoveTab.Text = "검색창 삭제";
            this.btnRemoveTab.UseVisualStyleBackColor = true;
            // 
            // btnCreateSearchTab
            // 
            this.btnCreateSearchTab.Location = new System.Drawing.Point(196, 20);
            this.btnCreateSearchTab.Name = "btnCreateSearchTab";
            this.btnCreateSearchTab.Size = new System.Drawing.Size(82, 23);
            this.btnCreateSearchTab.TabIndex = 0;
            this.btnCreateSearchTab.Text = "검색창 생성";
            this.btnCreateSearchTab.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1474, 29);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Save,
            this.ToolStripMenuItem_Open});
            this.ToolStripMenuItem.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(49, 25);
            this.ToolStripMenuItem.Text = "파일";
            // 
            // ToolStripMenuItem_Save
            // 
            this.ToolStripMenuItem_Save.Name = "ToolStripMenuItem_Save";
            this.ToolStripMenuItem_Save.Size = new System.Drawing.Size(98, 22);
            this.ToolStripMenuItem_Save.Text = "저장";
            // 
            // ToolStripMenuItem_Open
            // 
            this.ToolStripMenuItem_Open.Name = "ToolStripMenuItem_Open";
            this.ToolStripMenuItem_Open.Size = new System.Drawing.Size(98, 22);
            this.ToolStripMenuItem_Open.Text = "열기";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1476, 31);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 31);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panel2.Size = new System.Drawing.Size(1476, 68);
            this.panel2.TabIndex = 4;
            // 
            // frmViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1476, 709);
            this.Controls.Add(this.tabControl_Search);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmViewer";
            this.Text = "XmlViewer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl_Search;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCreateSearchTab;
        private System.Windows.Forms.Button btnRemoveTab;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Save;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Open;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTabName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

