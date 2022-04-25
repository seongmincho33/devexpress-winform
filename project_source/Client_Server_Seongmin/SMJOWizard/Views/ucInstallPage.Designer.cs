namespace SMJOWizard
{
    partial class ucInstallPage
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
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.startButton = new DevExpress.XtraEditors.SimpleButton();
            this.progressEdit = new DevExpress.XtraEditors.ProgressBarControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            this.SuspendLayout();
            //
            // bgWorker
            //
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            //
            // layoutControl1
            //
            this.layoutControl1.AllowCustomization = false;
            this.layoutControl1.Controls.Add(this.startButton);
            this.layoutControl1.Controls.Add(this.progressEdit);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(-1469, 300, 450, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(759, 354);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            //
            // startButton
            //
            this.startButton.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.startButton.Appearance.Options.UseFont = true;
            this.startButton.AutoWidthInLayoutControl = true;
            this.startButton.Location = new System.Drawing.Point(527, 160);
            this.startButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startButton.MinimumSize = new System.Drawing.Size(112, 0);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(112, 28);
            this.startButton.StyleController = this.layoutControl1;
            this.startButton.TabIndex = 9;
            this.startButton.Text = "Install";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            //
            // progressEdit
            //
            this.progressEdit.EditValue = null;
            this.progressEdit.Location = new System.Drawing.Point(120, 114);
            this.progressEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressEdit.MinimumSize = new System.Drawing.Size(0, 26);
            this.progressEdit.Name = "progressEdit";
            this.progressEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressEdit.Size = new System.Drawing.Size(519, 26);
            this.progressEdit.StyleController = this.layoutControl1;
            this.progressEdit.TabIndex = 8;
            //
            // layoutControlGroup1
            //
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.simpleLabelItem1,
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.simpleLabelItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(40, 40, 0, 40);
            this.layoutControlGroup1.Size = new System.Drawing.Size(759, 354);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            //
            // layoutControlItem2
            //
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.progressEdit;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.layoutControlItem2.CustomizationFormText = "Installation Folder:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(80, 80, 80, 20);
            this.layoutControlItem2.Size = new System.Drawing.Size(679, 126);
            this.layoutControlItem2.Text = "Installation Folder:";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            this.layoutControlItem2.TrimClientAreaToControl = false;
            //
            // simpleLabelItem1
            //
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.CustomizationFormText = "Installation progress:";
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(679, 34);
            this.simpleLabelItem1.Text = "Installation progress:";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(200, 30);
            //
            // layoutControlItem1
            //
            this.layoutControlItem1.Control = this.startButton;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.FillControlToClientArea = false;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 160);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(80, 80, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(679, 28);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.SupportHorzAlignment;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            //
            // emptySpaceItem1
            //
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 188);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(679, 113);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            //
            // simpleLabelItem2
            //
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.CustomizationFormText = "Click Install to begin installation";
            this.simpleLabelItem2.Location = new System.Drawing.Point(0, 301);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(80, 0, 0, 0);
            this.simpleLabelItem2.Size = new System.Drawing.Size(679, 13);
            this.simpleLabelItem2.Text = "Click Install to begin installation";
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(200, 13);
            //
            // ucInstallPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucInstallPage";
            this.Size = new System.Drawing.Size(759, 354);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgWorker;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.ProgressBarControl progressEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraEditors.SimpleButton startButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
    }
}
