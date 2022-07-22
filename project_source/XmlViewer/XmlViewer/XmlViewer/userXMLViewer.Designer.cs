namespace Viewer
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFileNameSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFileNameSearch = new System.Windows.Forms.Button();
            this.richTextBox_TEXTView = new System.Windows.Forms.RichTextBox();
            this.dataGridView_XMLDataTable = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblDataSetName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTableName = new System.Windows.Forms.Label();
            this.txtSearchRecord = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSearchRecord = new System.Windows.Forms.Button();
            this.txtSearchRecordColumn = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSearch_FolderPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtXML_FolderPath = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_XMLDataTable)).BeginInit();
            this.groupBox3.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(1487, 817);
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox_TEXTView);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView_XMLDataTable);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(1487, 758);
            this.splitContainer1.SplitterDistance = 427;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 6;
            // 
            // treeView_FolderPath
            // 
            this.treeView_FolderPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_FolderPath.Location = new System.Drawing.Point(0, 100);
            this.treeView_FolderPath.Name = "treeView_FolderPath";
            this.treeView_FolderPath.Size = new System.Drawing.Size(427, 658);
            this.treeView_FolderPath.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtFileNameSearch);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnFileNameSearch);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(427, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "파일 탐색";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(359, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "[2] 데이터 셋 하위의 테이블이 보이면 클릭시 옆에 테이블이 보임";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(251, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "[1] 선택한 파일은 데이터셋일 경우만 해당함.";
            // 
            // txtFileNameSearch
            // 
            this.txtFileNameSearch.Location = new System.Drawing.Point(77, 65);
            this.txtFileNameSearch.Name = "txtFileNameSearch";
            this.txtFileNameSearch.Size = new System.Drawing.Size(162, 21);
            this.txtFileNameSearch.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "파일 검색 : ";
            // 
            // btnFileNameSearch
            // 
            this.btnFileNameSearch.Location = new System.Drawing.Point(245, 64);
            this.btnFileNameSearch.Name = "btnFileNameSearch";
            this.btnFileNameSearch.Size = new System.Drawing.Size(75, 23);
            this.btnFileNameSearch.TabIndex = 5;
            this.btnFileNameSearch.Text = "검색";
            this.btnFileNameSearch.UseVisualStyleBackColor = true;
            // 
            // richTextBox_TEXTView
            // 
            this.richTextBox_TEXTView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_TEXTView.Location = new System.Drawing.Point(0, 100);
            this.richTextBox_TEXTView.Name = "richTextBox_TEXTView";
            this.richTextBox_TEXTView.Size = new System.Drawing.Size(1050, 658);
            this.richTextBox_TEXTView.TabIndex = 4;
            this.richTextBox_TEXTView.Text = "";
            // 
            // dataGridView_XMLDataTable
            // 
            this.dataGridView_XMLDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_XMLDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_XMLDataTable.Location = new System.Drawing.Point(0, 100);
            this.dataGridView_XMLDataTable.Name = "dataGridView_XMLDataTable";
            this.dataGridView_XMLDataTable.RowTemplate.Height = 23;
            this.dataGridView_XMLDataTable.Size = new System.Drawing.Size(1050, 658);
            this.dataGridView_XMLDataTable.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblDataSetName);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.lblTableName);
            this.groupBox3.Controls.Add(this.txtSearchRecord);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnSearchRecord);
            this.groupBox3.Controls.Add(this.txtSearchRecordColumn);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1050, 100);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "테이터 테이블 탐색";
            // 
            // lblDataSetName
            // 
            this.lblDataSetName.AutoSize = true;
            this.lblDataSetName.Location = new System.Drawing.Point(131, 16);
            this.lblDataSetName.Name = "lblDataSetName";
            this.lblDataSetName.Size = new System.Drawing.Size(90, 12);
            this.lblDataSetName.TabIndex = 12;
            this.lblDataSetName.Text = "Data Set Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "[1] 데이터 셋 이름 : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(227, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "레코드 검색 : ";
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(131, 42);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(75, 12);
            this.lblTableName.TabIndex = 10;
            this.lblTableName.Text = "Table Name";
            // 
            // txtSearchRecord
            // 
            this.txtSearchRecord.Location = new System.Drawing.Point(314, 67);
            this.txtSearchRecord.Name = "txtSearchRecord";
            this.txtSearchRecord.Size = new System.Drawing.Size(162, 21);
            this.txtSearchRecord.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "[2] 테이블 이름     : ";
            // 
            // btnSearchRecord
            // 
            this.btnSearchRecord.Location = new System.Drawing.Point(482, 67);
            this.btnSearchRecord.Name = "btnSearchRecord";
            this.btnSearchRecord.Size = new System.Drawing.Size(75, 23);
            this.btnSearchRecord.TabIndex = 6;
            this.btnSearchRecord.Text = "검색";
            this.btnSearchRecord.UseVisualStyleBackColor = true;
            // 
            // txtSearchRecordColumn
            // 
            this.txtSearchRecordColumn.Location = new System.Drawing.Point(59, 66);
            this.txtSearchRecordColumn.Name = "txtSearchRecordColumn";
            this.txtSearchRecordColumn.Size = new System.Drawing.Size(162, 21);
            this.txtSearchRecordColumn.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "컬럼 : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSearch_FolderPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtXML_FolderPath);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1487, 59);
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
            // userXMLViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "userXMLViewer";
            this.Size = new System.Drawing.Size(1487, 817);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_XMLDataTable)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.Button btnSearchRecord;
        private System.Windows.Forms.TextBox txtSearchRecord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSearchRecordColumn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDataSetName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox richTextBox_TEXTView;
    }
}
