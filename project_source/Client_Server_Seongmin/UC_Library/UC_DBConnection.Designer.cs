namespace UC_Library
{
    partial class UC_DBConnection
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
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_SendQuery_R = new DevExpress.XtraEditors.SimpleButton();
            this.btn_SendQuery_CUD = new DevExpress.XtraEditors.SimpleButton();
            this.checkBox_Save = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btn_ServerDisconnect = new DevExpress.XtraEditors.SimpleButton();
            this.btn_ServerConnect = new DevExpress.XtraEditors.SimpleButton();
            this.btn_DbClose = new DevExpress.XtraEditors.SimpleButton();
            this.btn_DbOpen = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_serverName = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtEdit_ServerName = new DevExpress.XtraEditors.TextEdit();
            this.txtEdit_Password = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtEdit_ID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtEdit_DataBaseName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtEdit_ServerConnection = new DevExpress.XtraEditors.TextEdit();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.groupControl_ServerConnection = new DevExpress.XtraEditors.GroupControl();
            this.groupControl_query = new DevExpress.XtraEditors.GroupControl();
            this.groupControl_ServerInfo = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkBox_Save)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_ServerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_Password.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_ID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_DataBaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_ServerConnection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl_ServerConnection)).BeginInit();
            this.groupControl_ServerConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl_query)).BeginInit();
            this.groupControl_query.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl_ServerInfo)).BeginInit();
            this.groupControl_ServerInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.panelControl1);
            this.panelControl3.Controls.Add(this.panelControl2);
            this.panelControl3.Controls.Add(this.lblTitle);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(276, 479);
            this.panelControl3.TabIndex = 8;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.groupControl_query);
            this.panelControl1.Controls.Add(this.groupControl_ServerConnection);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 226);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(276, 249);
            this.panelControl1.TabIndex = 4;
            // 
            // btn_SendQuery_R
            // 
            this.btn_SendQuery_R.Location = new System.Drawing.Point(5, 55);
            this.btn_SendQuery_R.Name = "btn_SendQuery_R";
            this.btn_SendQuery_R.Size = new System.Drawing.Size(252, 23);
            this.btn_SendQuery_R.TabIndex = 11;
            this.btn_SendQuery_R.Text = "쿼리문 전송(조회)";
            // 
            // btn_SendQuery_CUD
            // 
            this.btn_SendQuery_CUD.Location = new System.Drawing.Point(5, 26);
            this.btn_SendQuery_CUD.Name = "btn_SendQuery_CUD";
            this.btn_SendQuery_CUD.Size = new System.Drawing.Size(252, 23);
            this.btn_SendQuery_CUD.TabIndex = 10;
            this.btn_SendQuery_CUD.Text = "쿼리문 전송";
            // 
            // checkBox_Save
            // 
            this.checkBox_Save.Appearance.BackColor = System.Drawing.Color.White;
            this.checkBox_Save.Appearance.Options.UseBackColor = true;
            this.checkBox_Save.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "작성내용 저장")});
            this.checkBox_Save.Location = new System.Drawing.Point(5, 153);
            this.checkBox_Save.Name = "checkBox_Save";
            this.checkBox_Save.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox_Save.Size = new System.Drawing.Size(252, 24);
            this.checkBox_Save.TabIndex = 5;
            // 
            // btn_ServerDisconnect
            // 
            this.btn_ServerDisconnect.Location = new System.Drawing.Point(5, 116);
            this.btn_ServerDisconnect.Name = "btn_ServerDisconnect";
            this.btn_ServerDisconnect.Size = new System.Drawing.Size(252, 23);
            this.btn_ServerDisconnect.TabIndex = 9;
            this.btn_ServerDisconnect.Text = "서버 연결 해제";
            // 
            // btn_ServerConnect
            // 
            this.btn_ServerConnect.Location = new System.Drawing.Point(5, 29);
            this.btn_ServerConnect.Name = "btn_ServerConnect";
            this.btn_ServerConnect.Size = new System.Drawing.Size(252, 23);
            this.btn_ServerConnect.TabIndex = 6;
            this.btn_ServerConnect.Text = "서버연결";
            // 
            // btn_DbClose
            // 
            this.btn_DbClose.Location = new System.Drawing.Point(5, 87);
            this.btn_DbClose.Name = "btn_DbClose";
            this.btn_DbClose.Size = new System.Drawing.Size(252, 23);
            this.btn_DbClose.TabIndex = 8;
            this.btn_DbClose.Text = "DB 닫기";
            // 
            // btn_DbOpen
            // 
            this.btn_DbOpen.Location = new System.Drawing.Point(5, 58);
            this.btn_DbOpen.Name = "btn_DbOpen";
            this.btn_DbOpen.Size = new System.Drawing.Size(252, 23);
            this.btn_DbOpen.TabIndex = 7;
            this.btn_DbOpen.Text = "DB 열기";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.groupControl_ServerInfo);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 31);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(276, 195);
            this.panelControl2.TabIndex = 5;
            // 
            // lbl_serverName
            // 
            this.lbl_serverName.Location = new System.Drawing.Point(5, 26);
            this.lbl_serverName.Name = "lbl_serverName";
            this.lbl_serverName.Size = new System.Drawing.Size(56, 14);
            this.lbl_serverName.TabIndex = 4;
            this.lbl_serverName.Text = "서버 이름 : ";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 130);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(64, 14);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "비 밀 번 호 : ";
            // 
            // txtEdit_ServerName
            // 
            this.txtEdit_ServerName.Location = new System.Drawing.Point(91, 23);
            this.txtEdit_ServerName.Name = "txtEdit_ServerName";
            this.txtEdit_ServerName.Size = new System.Drawing.Size(166, 20);
            this.txtEdit_ServerName.TabIndex = 0;
            // 
            // txtEdit_Password
            // 
            this.txtEdit_Password.Location = new System.Drawing.Point(91, 127);
            this.txtEdit_Password.Name = "txtEdit_Password";
            this.txtEdit_Password.Size = new System.Drawing.Size(166, 20);
            this.txtEdit_Password.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 104);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(50, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "아 이 디 : ";
            // 
            // txtEdit_ID
            // 
            this.txtEdit_ID.Location = new System.Drawing.Point(91, 101);
            this.txtEdit_ID.Name = "txtEdit_ID";
            this.txtEdit_ID.Size = new System.Drawing.Size(166, 20);
            this.txtEdit_ID.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 78);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "데이터베이스명 : ";
            // 
            // txtEdit_DataBaseName
            // 
            this.txtEdit_DataBaseName.Location = new System.Drawing.Point(91, 75);
            this.txtEdit_DataBaseName.Name = "txtEdit_DataBaseName";
            this.txtEdit_DataBaseName.Size = new System.Drawing.Size(166, 20);
            this.txtEdit_DataBaseName.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 52);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "서버 연결 상태 : ";
            // 
            // txtEdit_ServerConnection
            // 
            this.txtEdit_ServerConnection.Location = new System.Drawing.Point(91, 49);
            this.txtEdit_ServerConnection.Name = "txtEdit_ServerConnection";
            this.txtEdit_ServerConnection.Size = new System.Drawing.Size(166, 20);
            this.txtEdit_ServerConnection.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.FontSizeDelta = 10;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(226, 31);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "데이터 베이스 연결하기";
            // 
            // groupControl_ServerConnection
            // 
            this.groupControl_ServerConnection.Controls.Add(this.btn_ServerConnect);
            this.groupControl_ServerConnection.Controls.Add(this.btn_DbOpen);
            this.groupControl_ServerConnection.Controls.Add(this.btn_ServerDisconnect);
            this.groupControl_ServerConnection.Controls.Add(this.btn_DbClose);
            this.groupControl_ServerConnection.Location = new System.Drawing.Point(3, 6);
            this.groupControl_ServerConnection.Name = "groupControl_ServerConnection";
            this.groupControl_ServerConnection.Size = new System.Drawing.Size(262, 145);
            this.groupControl_ServerConnection.TabIndex = 6;
            this.groupControl_ServerConnection.Text = "서버 연결";
            // 
            // groupControl_query
            // 
            this.groupControl_query.Controls.Add(this.btn_SendQuery_CUD);
            this.groupControl_query.Controls.Add(this.btn_SendQuery_R);
            this.groupControl_query.Location = new System.Drawing.Point(3, 157);
            this.groupControl_query.Name = "groupControl_query";
            this.groupControl_query.Size = new System.Drawing.Size(262, 86);
            this.groupControl_query.TabIndex = 7;
            this.groupControl_query.Text = "전송";
            // 
            // groupControl_ServerInfo
            // 
            this.groupControl_ServerInfo.Controls.Add(this.checkBox_Save);
            this.groupControl_ServerInfo.Controls.Add(this.txtEdit_ServerName);
            this.groupControl_ServerInfo.Controls.Add(this.labelControl4);
            this.groupControl_ServerInfo.Controls.Add(this.lbl_serverName);
            this.groupControl_ServerInfo.Controls.Add(this.labelControl3);
            this.groupControl_ServerInfo.Controls.Add(this.txtEdit_Password);
            this.groupControl_ServerInfo.Controls.Add(this.txtEdit_ServerConnection);
            this.groupControl_ServerInfo.Controls.Add(this.labelControl1);
            this.groupControl_ServerInfo.Controls.Add(this.txtEdit_ID);
            this.groupControl_ServerInfo.Controls.Add(this.txtEdit_DataBaseName);
            this.groupControl_ServerInfo.Controls.Add(this.labelControl2);
            this.groupControl_ServerInfo.Location = new System.Drawing.Point(3, 6);
            this.groupControl_ServerInfo.Name = "groupControl_ServerInfo";
            this.groupControl_ServerInfo.Size = new System.Drawing.Size(262, 183);
            this.groupControl_ServerInfo.TabIndex = 5;
            this.groupControl_ServerInfo.Text = "서버 정보";
            // 
            // UC_DBConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl3);
            this.Name = "UC_DBConnection";
            this.Size = new System.Drawing.Size(276, 479);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkBox_Save)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_ServerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_Password.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_ID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_DataBaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_ServerConnection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl_ServerConnection)).EndInit();
            this.groupControl_ServerConnection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl_query)).EndInit();
            this.groupControl_query.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl_ServerInfo)).EndInit();
            this.groupControl_ServerInfo.ResumeLayout(false);
            this.groupControl_ServerInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl checkBox_Save;
        private DevExpress.XtraEditors.SimpleButton btn_ServerDisconnect;
        private DevExpress.XtraEditors.SimpleButton btn_ServerConnect;
        private DevExpress.XtraEditors.SimpleButton btn_DbClose;
        private DevExpress.XtraEditors.SimpleButton btn_DbOpen;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl lbl_serverName;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtEdit_ServerName;
        private DevExpress.XtraEditors.TextEdit txtEdit_Password;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtEdit_ID;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtEdit_DataBaseName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtEdit_ServerConnection;
        private DevExpress.XtraEditors.SimpleButton btn_SendQuery_CUD;
        private DevExpress.XtraEditors.SimpleButton btn_SendQuery_R;
        private DevExpress.XtraEditors.GroupControl groupControl_query;
        private DevExpress.XtraEditors.GroupControl groupControl_ServerConnection;
        private DevExpress.XtraEditors.GroupControl groupControl_ServerInfo;
    }
}
