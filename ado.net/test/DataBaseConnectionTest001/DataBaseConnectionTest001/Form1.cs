using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseConnectionTest001
{
    public partial class Form1 : Form
    {
        SqlConnection Conn = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            textBox_ServerConnection.Text = "해제";
        }

        //서버 연결
        private void btn_ServerConnect_Click(object sender, EventArgs e)
        {
            String ConnectionString;
            ConnectionString =
                "server=.\\SQLEXPRESS;" +
                "database=" + textBox_DBName.Text + ";" +
                "user id=" + textBox_User_ID.Text + ";" +
                "pwd=" + textBox_User_Password.Text + ";";

            if(Conn != null)
            {
                Conn.Dispose(); //Close()역할 까지 함
            }

            Conn = new SqlConnection(ConnectionString);

            /*
            //아래와 같이 해도 된다.
            Conn = new SqlConnection();
            Conn.ConnectionString =
                "server=.\\SQLEXPRESS;" +
                "database=" + textBox_DBName.Text + ";" +
                "user id=" + textBox_User_ID.Text + ";" +
                "pwd=" + textBox_User_Password.Text + ";";
            */


            if (Conn != null)
                textBox_ServerConnection.Text = "연결";
            else
                textBox_ServerConnection.Text = "해제";
        }

        private void btn_DBOpen_Click(object sender, EventArgs e)
        {
            Conn.Open();
            if(Conn.State == ConnectionState.Open)
            {
                MessageBox.Show("데이터베이스 열었습니다.");
            }
            else
            {
                MessageBox.Show("데이터베이스 Open 에러");
            }
        }

        private void btn_DBClose_Click(object sender, EventArgs e)
        {
            Conn.Close();
            if (Conn.State == ConnectionState.Closed)
            {
                MessageBox.Show("데이터베이스 닫았습니다.");
            }
            else
            {
                MessageBox.Show("데이터베이스 Close 에러");
            }
        }

        private void btn_ServerConnectionClose_Click(object sender, EventArgs e)
        {
            Conn.Dispose();
            Conn = null;
            textBox_ServerConnection.Text= "해제";
            MessageBox.Show("서버 연결 해제");
        }
    }
}
