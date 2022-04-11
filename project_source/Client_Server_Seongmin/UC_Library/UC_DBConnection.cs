using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using UC_Library.Properties;
using SMJODBConnect;

namespace UC_Library
{
    public partial class UC_DBConnection : DevExpress.XtraEditors.XtraUserControl
    {             
        public DBConnection dbConn = new DBConnection();
        public string queryString { get; set; }
        public DataSet UC_DataSet { get; set; }        

        public UC_DBConnection()
        {
            InitializeComponent();
            this.SetControls();            
        }

        private void SetControls()
        {
            SetForm();
            SetButtons();
            SetTextEdits();
            SetMemoEdit();

            void SetButtons()
            {
                this.btn_ServerConnect.Click += btn_ServerConnect_Click;
                this.btn_DbOpen.Click += btn_DbOpen_Click;
                this.btn_DbClose.Click += btn_DbClose_Click;
                this.btn_ServerDisconnect.Click += btn_ServerDisconnect_Click;
                this.btn_SendQuery_CUD.Click += Btn_SendQuery_CUD_Click;
                this.btn_SendQuery_R.Click += Btn_SendQuery_R_Click;
            }
            void SetTextEdits()
            {
                this.txtEdit_ServerConnection.Text = "해제";
                this.txtEdit_ServerConnection.Enabled = false;
            }            
            void SetMemoEdit()
            {
                this.queryString = this.memoEdit_QueryString.Text;
                this.memoEdit_QueryString.EditValueChanged += memoEdit_QueryString_EditValueChanged;
                void memoEdit_QueryString_EditValueChanged(object sender, EventArgs e)
                {
                    this.queryString = this.memoEdit_QueryString.Text;
                }
            }
            void SetForm()
            {
                this.Load += Form1_Load;                
            }
            void Form1_Load(object sender, EventArgs e)
            {
                //기본셋팅값을 불러옵니다.

                if (Settings.Default.WindowLocation != null)
                {
                    ((Form)this.Parent).Location = Settings.Default.WindowLocation;
                }

                if (Settings.Default.WindowSize != null)
                {
                    ((Form)this.Parent).Size = Settings.Default.WindowSize;
                }

                if (Settings.Default.ServerName != null)
                {
                    this.txtEdit_ServerName.Text = Settings.Default.ServerName;
                }

                if (Settings.Default.DataBaseName != null)
                {
                    this.txtEdit_DataBaseName.Text = Settings.Default.DataBaseName;
                }

                if (Settings.Default.UserID != null)
                {
                    this.txtEdit_ID.Text = Settings.Default.UserID;
                }

                if (Settings.Default.Password != null)
                {
                    this.txtEdit_Password.Text = Settings.Default.Password;
                }

                ((Form)this.Parent).FormClosing += FormMain_FormClosing;

            }

            void FormMain_FormClosing(object sender, FormClosingEventArgs e)
            {
                Settings.Default.WindowLocation = ((Form)this.Parent).Location;

                // Copy window size to app settings
                if (((Form)this.Parent).WindowState == FormWindowState.Normal)
                {
                    Settings.Default.WindowSize = ((Form)this.Parent).Size;
                }
                else
                {
                    Settings.Default.WindowSize = ((Form)this.Parent).RestoreBounds.Size;
                }

                //작성내용 저장버튼 눌려있다면
                if(this.checkBox_Save.Items[0].CheckState == CheckState.Checked)
                {
                    Settings.Default.ServerName = this.txtEdit_ServerName.Text;
                    Settings.Default.DataBaseName = this.txtEdit_DataBaseName.Text;
                    Settings.Default.UserID = this.txtEdit_ID.Text;
                    Settings.Default.Password = this.txtEdit_Password.Text;
                }
                
                // Save settings
                Settings.Default.Save();
            }
        }

        #region 버튼 이벤트
        private void Btn_SendQuery_R_Click(object sender, EventArgs e)
        {
            this.dbConn.SendQuerystring_R(this.queryString, out string message, out DataSet selectedDataSet);            
            this.UC_DataSet = selectedDataSet;           
            if (message != null)
            {
                MessageBox.Show(message);
            }
        }

        private void Btn_SendQuery_CUD_Click(object sender, EventArgs e)
        {
            this.dbConn.SendQuerystring_CUD(this.queryString, out string message);
            if(message != null)
            {
                MessageBox.Show(message);
            }
        }

        //서버 연결
        private void btn_ServerConnect_Click(object sender, EventArgs e)
        {
            this.dbConn.ServerConnect(this.txtEdit_ServerName.Text, this.txtEdit_DataBaseName.Text, this.txtEdit_ID.Text, this.txtEdit_Password.Text, out string message);
            if(message != null)
            {
                MessageBox.Show(message);
                txtEdit_ServerConnection.Text = message;
            }   
        }

        private void btn_DbOpen_Click(object sender, EventArgs e)
        {
            this.dbConn.DBOpen(out string message);
            if (message != null)
            {
                MessageBox.Show(message);
            }           
        }

        private void btn_DbClose_Click(object sender, EventArgs e)
        {

            this.dbConn.DBClose(out string message);
            if (message != null)
            {
                MessageBox.Show(message);
            }          
        }

        private void btn_ServerDisconnect_Click(object sender, EventArgs e)
        {
            this.dbConn.ServerDisconnect(out string message);
            if (message != null)
            {
                MessageBox.Show(message);
                txtEdit_ServerConnection.Text = message;                
            }      
        }
        #endregion
    }
}
