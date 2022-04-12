using DevExpress.XtraEditors;

namespace UC_Library
{
    partial class FileCopy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileCopy));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSyncCopy = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pbCopy = new DevExpress.XtraEditors.ProgressBarControl();
            this.btnAsyncCopy = new DevExpress.XtraEditors.SimpleButton();
            this.btnFindTarget = new DevExpress.XtraEditors.SimpleButton();
            this.btnFindSource = new DevExpress.XtraEditors.SimpleButton();
            this.txtTarget = new DevExpress.XtraEditors.TextEdit();
            this.txtSource = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCopy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTarget.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSource.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSyncCopy);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.pbCopy);
            this.panelControl1.Controls.Add(this.btnAsyncCopy);
            this.panelControl1.Controls.Add(this.btnFindTarget);
            this.panelControl1.Controls.Add(this.btnFindSource);
            this.panelControl1.Controls.Add(this.txtTarget);
            this.panelControl1.Controls.Add(this.txtSource);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(520, 113);
            this.panelControl1.TabIndex = 0;
            // 
            // btnSyncCopy
            // 
            this.btnSyncCopy.Location = new System.Drawing.Point(195, 57);
            this.btnSyncCopy.Name = "btnSyncCopy";
            this.btnSyncCopy.Size = new System.Drawing.Size(128, 23);
            this.btnSyncCopy.TabIndex = 11;
            this.btnSyncCopy.Text = "Sync Copy";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(329, 57);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(128, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            // 
            // pbCopy
            // 
            this.pbCopy.Location = new System.Drawing.Point(61, 86);
            this.pbCopy.Name = "pbCopy";
            this.pbCopy.Size = new System.Drawing.Size(396, 18);
            this.pbCopy.TabIndex = 9;
            // 
            // btnAsyncCopy
            // 
            this.btnAsyncCopy.Location = new System.Drawing.Point(61, 57);
            this.btnAsyncCopy.Name = "btnAsyncCopy";
            this.btnAsyncCopy.Size = new System.Drawing.Size(128, 23);
            this.btnAsyncCopy.TabIndex = 6;
            this.btnAsyncCopy.Text = "Async Copy";
            // 
            // btnFindTarget
            // 
            this.btnFindTarget.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.btnFindTarget.Location = new System.Drawing.Point(463, 30);
            this.btnFindTarget.Name = "btnFindTarget";
            this.btnFindTarget.Size = new System.Drawing.Size(51, 21);
            this.btnFindTarget.TabIndex = 5;
            this.btnFindTarget.Text = "...";
            // 
            // btnFindSource
            // 
            this.btnFindSource.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.btnFindSource.Location = new System.Drawing.Point(463, 5);
            this.btnFindSource.Name = "btnFindSource";
            this.btnFindSource.Size = new System.Drawing.Size(51, 21);
            this.btnFindSource.TabIndex = 4;
            this.btnFindSource.Text = "...";
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(61, 31);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(396, 20);
            this.txtTarget.TabIndex = 3;
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(61, 5);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(396, 20);
            this.txtSource.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 34);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(49, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Target : ";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Source : ";
            // 
            // FileCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "FileCopy";
            this.Size = new System.Drawing.Size(520, 113);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCopy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTarget.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSource.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private SimpleButton btnSyncCopy;
        private SimpleButton btnCancel;
        private ProgressBarControl pbCopy;
        private SimpleButton btnAsyncCopy;
        private SimpleButton btnFindTarget;
        private SimpleButton btnFindSource;
        private TextEdit txtTarget;
        private TextEdit txtSource;
    }
}
