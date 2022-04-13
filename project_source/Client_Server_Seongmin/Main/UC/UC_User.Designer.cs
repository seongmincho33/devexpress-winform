using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace Main.UC
{
    partial class UC_User
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Retrieve_User = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Insert_User = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Save_User = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Delete_User = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.vGridControl1 = new DevExpress.XtraVerticalGrid.VGridControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Retrieve_Dept = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Insert_Dept = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Save_Dept = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Delete_Dept = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.splitContainerControl1);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(868, 602);
            this.panelControl1.TabIndex = 1;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 35);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.treeList1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl3);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(868, 567);
            this.splitContainerControl1.SplitterPosition = 417;
            this.splitContainerControl1.TabIndex = 8;
            // 
            // treeList1
            // 
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(417, 567);
            this.treeList1.TabIndex = 7;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.panelControl5);
            this.panelControl3.Controls.Add(this.splitterControl1);
            this.panelControl3.Controls.Add(this.vGridControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(441, 567);
            this.panelControl3.TabIndex = 1;
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.panelControl4);
            this.panelControl5.Controls.Add(this.gridControl1);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl5.Location = new System.Drawing.Point(2, 266);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(437, 296);
            this.panelControl5.TabIndex = 8;
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.labelControl1);
            this.panelControl4.Controls.Add(this.btn_Retrieve_User);
            this.panelControl4.Controls.Add(this.btn_Insert_User);
            this.panelControl4.Controls.Add(this.btn_Save_User);
            this.panelControl4.Controls.Add(this.btn_Delete_User);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl4.Location = new System.Drawing.Point(2, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(433, 34);
            this.panelControl4.TabIndex = 7;
            // 
            // btn_Retrieve
            // 
            this.btn_Retrieve_User.Location = new System.Drawing.Point(5, 5);
            this.btn_Retrieve_User.Name = "btn_Retrieve";
            this.btn_Retrieve_User.Size = new System.Drawing.Size(75, 23);
            this.btn_Retrieve_User.TabIndex = 3;
            this.btn_Retrieve_User.Text = "조회";
            // 
            // btn_Insert
            // 
            this.btn_Insert_User.Location = new System.Drawing.Point(86, 5);
            this.btn_Insert_User.Name = "btn_Insert";
            this.btn_Insert_User.Size = new System.Drawing.Size(75, 23);
            this.btn_Insert_User.TabIndex = 4;
            this.btn_Insert_User.Text = "추가";
            // 
            // btn_Update
            // 
            this.btn_Save_User.Location = new System.Drawing.Point(248, 5);
            this.btn_Save_User.Name = "btn_Update";
            this.btn_Save_User.Size = new System.Drawing.Size(75, 23);
            this.btn_Save_User.TabIndex = 5;
            this.btn_Save_User.Text = "저장";
            // 
            // btn_Delete
            // 
            this.btn_Delete_User.Location = new System.Drawing.Point(167, 5);
            this.btn_Delete_User.Name = "btn_Delete";
            this.btn_Delete_User.Size = new System.Drawing.Size(75, 23);
            this.btn_Delete_User.TabIndex = 6;
            this.btn_Delete_User.Text = "삭제";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(433, 292);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(2, 256);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(437, 10);
            this.splitterControl1.TabIndex = 1;
            this.splitterControl1.TabStop = false;
            // 
            // vGridControl1
            // 
            this.vGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.vGridControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.vGridControl1.Location = new System.Drawing.Point(2, 2);
            this.vGridControl1.Name = "vGridControl1";
            this.vGridControl1.Size = new System.Drawing.Size(437, 254);
            this.vGridControl1.TabIndex = 2;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panelControl6);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(868, 35);
            this.panelControl2.TabIndex = 6;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(329, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 14);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "사용자 추가";
            // 
            // panelControl6
            // 
            this.panelControl6.Controls.Add(this.btn_Retrieve_Dept);
            this.panelControl6.Controls.Add(this.btn_Insert_Dept);
            this.panelControl6.Controls.Add(this.btn_Save_Dept);
            this.panelControl6.Controls.Add(this.btn_Delete_Dept);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl6.Location = new System.Drawing.Point(2, 2);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(864, 34);
            this.panelControl6.TabIndex = 8;
            // 
            // btn_Retrieve_Dept
            // 
            this.btn_Retrieve_Dept.Location = new System.Drawing.Point(5, 5);
            this.btn_Retrieve_Dept.Name = "btn_Retrieve_Dept";
            this.btn_Retrieve_Dept.Size = new System.Drawing.Size(75, 23);
            this.btn_Retrieve_Dept.TabIndex = 3;
            this.btn_Retrieve_Dept.Text = "조회";
            // 
            // btn_Insert_Dept
            // 
            this.btn_Insert_Dept.Location = new System.Drawing.Point(86, 5);
            this.btn_Insert_Dept.Name = "btn_Insert_Dept";
            this.btn_Insert_Dept.Size = new System.Drawing.Size(75, 23);
            this.btn_Insert_Dept.TabIndex = 4;
            this.btn_Insert_Dept.Text = "추가";
            // 
            // btn_Save_Dept
            // 
            this.btn_Save_Dept.Location = new System.Drawing.Point(248, 5);
            this.btn_Save_Dept.Name = "btn_Save_Dept";
            this.btn_Save_Dept.Size = new System.Drawing.Size(75, 23);
            this.btn_Save_Dept.TabIndex = 5;
            this.btn_Save_Dept.Text = "저장";
            // 
            // btn_Delete_Dept
            // 
            this.btn_Delete_Dept.Location = new System.Drawing.Point(167, 5);
            this.btn_Delete_Dept.Name = "btn_Delete_Dept";
            this.btn_Delete_Dept.Size = new System.Drawing.Size(75, 23);
            this.btn_Delete_Dept.TabIndex = 6;
            this.btn_Delete_Dept.Text = "삭제";
            // 
            // UC_User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "UC_User";
            this.Size = new System.Drawing.Size(868, 602);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private PanelControl panelControl2;
        private SimpleButton btn_Delete_User;
        private SimpleButton btn_Save_User;
        private SimpleButton btn_Insert_User;
        private SimpleButton btn_Retrieve_User;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private SplitContainerControl splitContainerControl1;
        private GridControl gridControl1;
        private GridView gridView1;
        private PanelControl panelControl3;
        private SplitterControl splitterControl1;
        private DevExpress.XtraVerticalGrid.VGridControl vGridControl1;
        private PanelControl panelControl5;
        private PanelControl panelControl4;
        private LabelControl labelControl1;
        private PanelControl panelControl6;
        private SimpleButton btn_Retrieve_Dept;
        private SimpleButton btn_Insert_Dept;
        private SimpleButton btn_Save_Dept;
        private SimpleButton btn_Delete_Dept;
    }
}
