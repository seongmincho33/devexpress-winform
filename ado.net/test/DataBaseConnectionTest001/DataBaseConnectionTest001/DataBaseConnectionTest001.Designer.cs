namespace DataBaseConnectionTest001
{
    partial class DataBaseConnectionTest001
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ServerConnection = new System.Windows.Forms.TextBox();
            this.textBox_DBName = new System.Windows.Forms.TextBox();
            this.textBox_User_ID = new System.Windows.Forms.TextBox();
            this.textBox_User_Password = new System.Windows.Forms.TextBox();
            this.btn_ServerConnect = new System.Windows.Forms.Button();
            this.btn_DBOpen = new System.Windows.Forms.Button();
            this.btn_DBClose = new System.Windows.Forms.Button();
            this.btn_ServerConnectionClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "서버연결상태 :\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "데이터베이스명 : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "아   이   디 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "비 밀 번 호 :";
            // 
            // textBox_ServerConnection
            // 
            this.textBox_ServerConnection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_ServerConnection.Location = new System.Drawing.Point(137, 34);
            this.textBox_ServerConnection.Name = "textBox_ServerConnection";
            this.textBox_ServerConnection.ReadOnly = true;
            this.textBox_ServerConnection.Size = new System.Drawing.Size(179, 14);
            this.textBox_ServerConnection.TabIndex = 4;
            // 
            // textBox_DBName
            // 
            this.textBox_DBName.Location = new System.Drawing.Point(135, 54);
            this.textBox_DBName.Name = "textBox_DBName";
            this.textBox_DBName.Size = new System.Drawing.Size(179, 21);
            this.textBox_DBName.TabIndex = 5;
            // 
            // textBox_User_ID
            // 
            this.textBox_User_ID.Location = new System.Drawing.Point(135, 87);
            this.textBox_User_ID.Name = "textBox_User_ID";
            this.textBox_User_ID.Size = new System.Drawing.Size(179, 21);
            this.textBox_User_ID.TabIndex = 6;
            // 
            // textBox_User_Password
            // 
            this.textBox_User_Password.Location = new System.Drawing.Point(135, 116);
            this.textBox_User_Password.Name = "textBox_User_Password";
            this.textBox_User_Password.Size = new System.Drawing.Size(179, 21);
            this.textBox_User_Password.TabIndex = 7;
            // 
            // btn_ServerConnect
            // 
            this.btn_ServerConnect.Location = new System.Drawing.Point(28, 3);
            this.btn_ServerConnect.Name = "btn_ServerConnect";
            this.btn_ServerConnect.Size = new System.Drawing.Size(286, 33);
            this.btn_ServerConnect.TabIndex = 8;
            this.btn_ServerConnect.Text = "서버연결";
            this.btn_ServerConnect.UseVisualStyleBackColor = true;
            // 
            // btn_DBOpen
            // 
            this.btn_DBOpen.Location = new System.Drawing.Point(28, 42);
            this.btn_DBOpen.Name = "btn_DBOpen";
            this.btn_DBOpen.Size = new System.Drawing.Size(71, 33);
            this.btn_DBOpen.TabIndex = 9;
            this.btn_DBOpen.Text = "DB 열기";
            this.btn_DBOpen.UseVisualStyleBackColor = true;
            // 
            // btn_DBClose
            // 
            this.btn_DBClose.Location = new System.Drawing.Point(105, 42);
            this.btn_DBClose.Name = "btn_DBClose";
            this.btn_DBClose.Size = new System.Drawing.Size(71, 33);
            this.btn_DBClose.TabIndex = 10;
            this.btn_DBClose.Text = "DB 닫기";
            this.btn_DBClose.UseVisualStyleBackColor = true;
            // 
            // btn_ServerConnectionClose
            // 
            this.btn_ServerConnectionClose.Location = new System.Drawing.Point(182, 42);
            this.btn_ServerConnectionClose.Name = "btn_ServerConnectionClose";
            this.btn_ServerConnectionClose.Size = new System.Drawing.Size(132, 33);
            this.btn_ServerConnectionClose.TabIndex = 11;
            this.btn_ServerConnectionClose.Text = "서버 연결 해제";
            this.btn_ServerConnectionClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox_DBName);
            this.panel1.Controls.Add(this.textBox_User_Password);
            this.panel1.Controls.Add(this.textBox_User_ID);
            this.panel1.Controls.Add(this.textBox_ServerConnection);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(333, 152);
            this.panel1.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_DBOpen);
            this.panel2.Controls.Add(this.btn_ServerConnect);
            this.panel2.Controls.Add(this.btn_ServerConnectionClose);
            this.panel2.Controls.Add(this.btn_DBClose);
            this.panel2.Location = new System.Drawing.Point(3, 157);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(333, 82);
            this.panel2.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(344, 247);
            this.panel3.TabIndex = 14;
            // 
            // DataBaseConnectionTest001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 285);
            this.Controls.Add(this.panel3);
            this.Name = "DataBaseConnectionTest001";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ServerConnection;
        private System.Windows.Forms.TextBox textBox_DBName;
        private System.Windows.Forms.TextBox textBox_User_ID;
        private System.Windows.Forms.TextBox textBox_User_Password;
        private System.Windows.Forms.Button btn_ServerConnect;
        private System.Windows.Forms.Button btn_DBOpen;
        private System.Windows.Forms.Button btn_DBClose;
        private System.Windows.Forms.Button btn_ServerConnectionClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}

