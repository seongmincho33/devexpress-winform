namespace Viewer
{
    partial class frmRegistryViewer
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
            this.richTextBox_Result = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_TrackStart = new System.Windows.Forms.Button();
            this.btn_TrackStop = new System.Windows.Forms.Button();
            this.txtRegistryValueName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_SetTrackValues = new System.Windows.Forms.Button();
            this.txtRegistryKeyName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView_RegistryInfos = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_TrackDelete = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_ClearRichResult = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_RegistryInfos)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox_Result
            // 
            this.richTextBox_Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Result.Location = new System.Drawing.Point(3, 49);
            this.richTextBox_Result.Name = "richTextBox_Result";
            this.richTextBox_Result.Size = new System.Drawing.Size(779, 454);
            this.richTextBox_Result.TabIndex = 0;
            this.richTextBox_Result.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_TrackStart);
            this.panel1.Controls.Add(this.btn_TrackStop);
            this.panel1.Controls.Add(this.txtRegistryValueName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_SetTrackValues);
            this.panel1.Controls.Add(this.txtRegistryKeyName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(785, 64);
            this.panel1.TabIndex = 1;
            // 
            // btn_TrackStart
            // 
            this.btn_TrackStart.Location = new System.Drawing.Point(701, 8);
            this.btn_TrackStart.Name = "btn_TrackStart";
            this.btn_TrackStart.Size = new System.Drawing.Size(75, 23);
            this.btn_TrackStart.TabIndex = 7;
            this.btn_TrackStart.Text = "추적 시작";
            this.btn_TrackStart.UseVisualStyleBackColor = true;
            // 
            // btn_TrackStop
            // 
            this.btn_TrackStop.Location = new System.Drawing.Point(701, 33);
            this.btn_TrackStop.Name = "btn_TrackStop";
            this.btn_TrackStop.Size = new System.Drawing.Size(75, 23);
            this.btn_TrackStop.TabIndex = 6;
            this.btn_TrackStop.Text = "추적 종료";
            this.btn_TrackStop.UseVisualStyleBackColor = true;
            // 
            // txtRegistryValueName
            // 
            this.txtRegistryValueName.Location = new System.Drawing.Point(158, 35);
            this.txtRegistryValueName.Name = "txtRegistryValueName";
            this.txtRegistryValueName.Size = new System.Drawing.Size(435, 21);
            this.txtRegistryValueName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "레지스트리 Value Name : ";
            // 
            // btn_SetTrackValues
            // 
            this.btn_SetTrackValues.Location = new System.Drawing.Point(599, 8);
            this.btn_SetTrackValues.Name = "btn_SetTrackValues";
            this.btn_SetTrackValues.Size = new System.Drawing.Size(96, 48);
            this.btn_SetTrackValues.TabIndex = 2;
            this.btn_SetTrackValues.Text = " 추적리스트\r\n추가";
            this.btn_SetTrackValues.UseVisualStyleBackColor = true;
            // 
            // txtRegistryKeyName
            // 
            this.txtRegistryKeyName.Location = new System.Drawing.Point(158, 8);
            this.txtRegistryKeyName.Name = "txtRegistryKeyName";
            this.txtRegistryKeyName.Size = new System.Drawing.Size(435, 21);
            this.txtRegistryKeyName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "레지스트리 Key : ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(785, 699);
            this.panel2.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(785, 635);
            this.splitContainer1.SplitterDistance = 125;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView_RegistryInfos);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(785, 125);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "추적 목록";
            // 
            // dataGridView_RegistryInfos
            // 
            this.dataGridView_RegistryInfos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_RegistryInfos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_RegistryInfos.Location = new System.Drawing.Point(3, 49);
            this.dataGridView_RegistryInfos.Name = "dataGridView_RegistryInfos";
            this.dataGridView_RegistryInfos.RowTemplate.Height = 23;
            this.dataGridView_RegistryInfos.Size = new System.Drawing.Size(779, 73);
            this.dataGridView_RegistryInfos.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_TrackDelete);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 17);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(779, 32);
            this.panel3.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "선택 레지스트리 추적 제거 : ";
            // 
            // btn_TrackDelete
            // 
            this.btn_TrackDelete.Location = new System.Drawing.Point(176, 5);
            this.btn_TrackDelete.Name = "btn_TrackDelete";
            this.btn_TrackDelete.Size = new System.Drawing.Size(75, 23);
            this.btn_TrackDelete.TabIndex = 1;
            this.btn_TrackDelete.Text = "제거";
            this.btn_TrackDelete.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btn_ClearRichResult);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 17);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(779, 32);
            this.panel4.TabIndex = 2;
            // 
            // btn_ClearRichResult
            // 
            this.btn_ClearRichResult.Location = new System.Drawing.Point(90, 5);
            this.btn_ClearRichResult.Name = "btn_ClearRichResult";
            this.btn_ClearRichResult.Size = new System.Drawing.Size(75, 23);
            this.btn_ClearRichResult.TabIndex = 1;
            this.btn_ClearRichResult.Text = "Clear";
            this.btn_ClearRichResult.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "화면 Clear : ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBox_Result);
            this.groupBox2.Controls.Add(this.panel4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(785, 506);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Value Data";
            // 
            // frmPDMSRegistry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 699);
            this.Controls.Add(this.panel2);
            this.Name = "frmPDMSRegistry";
            this.Text = "frmPDMSRegistry";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_RegistryInfos)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_Result;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_SetTrackValues;
        private System.Windows.Forms.TextBox txtRegistryKeyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtRegistryValueName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_TrackStop;
        private System.Windows.Forms.Button btn_TrackStart;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView_RegistryInfos;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_TrackDelete;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_ClearRichResult;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}