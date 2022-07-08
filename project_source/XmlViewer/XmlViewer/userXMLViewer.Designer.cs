namespace XmlViewer
{
    partial class userXMLViewer
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView_FolderPath = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFileNameSearch = new System.Windows.Forms.Button();
            this.txtFileNameSearch = new System.Windows.Forms.TextBox();
            this.dataGridView_XMLDataTable = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSearchRecord = new System.Windows.Forms.Button();
            this.txtSearchRecord = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSearch_FolderPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtXML_FolderPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearchRecordColumn = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_XMLDataTable)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1142, 639);
            this.panel1.TabIndex = 8;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 59);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView_FolderPath);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView_XMLDataTable);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(1142, 580);
            this.splitContainer1.SplitterDistance = 328;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 6;
            // 
            // treeView_FolderPath
            // 
            this.treeView_FolderPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_FolderPath.Location = new System.Drawing.Point(0, 37);
            this.treeView_FolderPath.Name = "treeView_FolderPath";
            this.treeView_FolderPath.Size = new System.Drawing.Size(328, 543);
            this.treeView_FolderPath.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnFileNameSearch);
            this.panel2.Controls.Add(this.txtFileNameSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(328, 37);
            this.panel2.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "파일 검색 : ";
            // 
            // btnFileNameSearch
            // 
            this.btnFileNameSearch.Location = new System.Drawing.Point(242, 7);
            this.btnFileNameSearch.Name = "btnFileNameSearch";
            this.btnFileNameSearch.Size = new System.Drawing.Size(75, 23);
            this.btnFileNameSearch.TabIndex = 5;
            this.btnFileNameSearch.Text = "검색";
            this.btnFileNameSearch.UseVisualStyleBackColor = true;
            // 
            // txtFileNameSearch
            // 
            this.txtFileNameSearch.Location = new System.Drawing.Point(74, 8);
            this.txtFileNameSearch.Name = "txtFileNameSearch";
            this.txtFileNameSearch.Size = new System.Drawing.Size(162, 21);
            this.txtFileNameSearch.TabIndex = 4;
            // 
            // dataGridView_XMLDataTable
            // 
            this.dataGridView_XMLDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_XMLDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_XMLDataTable.Location = new System.Drawing.Point(0, 37);
            this.dataGridView_XMLDataTable.Name = "dataGridView_XMLDataTable";
            this.dataGridView_XMLDataTable.RowTemplate.Height = 23;
            this.dataGridView_XMLDataTable.Size = new System.Drawing.Size(804, 543);
            this.dataGridView_XMLDataTable.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtSearchRecordColumn);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.btnSearchRecord);
            this.panel3.Controls.Add(this.txtSearchRecord);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(804, 37);
            this.panel3.TabIndex = 1;
            // 
            // btnSearchRecord
            // 
            this.btnSearchRecord.Location = new System.Drawing.Point(473, 9);
            this.btnSearchRecord.Name = "btnSearchRecord";
            this.btnSearchRecord.Size = new System.Drawing.Size(75, 23);
            this.btnSearchRecord.TabIndex = 6;
            this.btnSearchRecord.Text = "검색";
            this.btnSearchRecord.UseVisualStyleBackColor = true;
            // 
            // txtSearchRecord
            // 
            this.txtSearchRecord.Location = new System.Drawing.Point(305, 9);
            this.txtSearchRecord.Name = "txtSearchRecord";
            this.txtSearchRecord.Size = new System.Drawing.Size(162, 21);
            this.txtSearchRecord.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "레코드 검색 : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSearch_FolderPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtXML_FolderPath);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1142, 59);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "입력창";
            // 
            // btnSearch_FolderPath
            // 
            this.btnSearch_FolderPath.Location = new System.Drawing.Point(705, 24);
            this.btnSearch_FolderPath.Name = "btnSearch_FolderPath";
            this.btnSearch_FolderPath.Size = new System.Drawing.Size(75, 23);
            this.btnSearch_FolderPath.TabIndex = 2;
            this.btnSearch_FolderPath.Text = "검색";
            this.btnSearch_FolderPath.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "파일 위치 경로 : ";
            // 
            // txtXML_FolderPath
            // 
            this.txtXML_FolderPath.Location = new System.Drawing.Point(115, 24);
            this.txtXML_FolderPath.Name = "txtXML_FolderPath";
            this.txtXML_FolderPath.Size = new System.Drawing.Size(584, 21);
            this.txtXML_FolderPath.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "컬럼 : ";
            // 
            // txtSearchRecordColumn
            // 
            this.txtSearchRecordColumn.Location = new System.Drawing.Point(50, 8);
            this.txtSearchRecordColumn.Name = "txtSearchRecordColumn";
            this.txtSearchRecordColumn.Size = new System.Drawing.Size(162, 21);
            this.txtSearchRecordColumn.TabIndex = 8;
            // 
            // userXMLViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "userXMLViewer";
            this.Size = new System.Drawing.Size(1142, 639);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_XMLDataTable)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView_FolderPath;
        private System.Windows.Forms.DataGridView dataGridView_XMLDataTable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFileNameSearch;
        private System.Windows.Forms.TextBox txtFileNameSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch_FolderPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtXML_FolderPath;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSearchRecord;
        private System.Windows.Forms.TextBox txtSearchRecord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSearchRecordColumn;
        private System.Windows.Forms.Label label4;
    }
}
