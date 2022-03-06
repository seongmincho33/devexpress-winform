using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseConnectionTest001
{
    public class ConnectionController
    {
        private TextBox TextBox_ServerConnection { get; set; }
        private TextBox TextBox_DBName { get; set; }
        private TextBox TextBox_User_ID { get; set; }
        private TextBox TextBox_User_Password { get; set; }
        private Button Btn_ServerConnect { get; set; }
        private Button Btn_DBOpen { get; set; }
        private Button Btn_DBClose { get; set; }
        private Button Btn_ServerConnectionClose { get; set; }
        private SqlConnection Conn { get; set; }

        public ConnectionController(
            DataBaseConnectionTest001 dataBaseConnectionTest001
           , Tuple<TextBox, TextBox, TextBox, TextBox> tuple_textbox
           , Tuple<Button, Button, Button, Button> tuple_btn)
        {
            this.TextBox_ServerConnection = tuple_textbox.Item1;
            this.TextBox_DBName = tuple_textbox.Item2;
            this.TextBox_User_ID = tuple_textbox.Item3;
            this.TextBox_User_Password = tuple_textbox.Item4;
            this.Btn_ServerConnect = tuple_btn.Item1;
            this.Btn_DBOpen = tuple_btn.Item2;
            this.Btn_DBClose = tuple_btn.Item3;
            this.Btn_ServerConnectionClose = tuple_btn.Item4;
        }
        internal static ConnectionController SetCurrentViewControllers(
          DataBaseConnectionTest001 dataBaseConnectionTest001
          , Tuple<TextBox, TextBox, TextBox, TextBox> tuple_textbox
          , Tuple<Button, Button, Button, Button> tuple_btn)
        {
            return new ConnectionController(dataBaseConnectionTest001, tuple_textbox, tuple_btn);
        }
        public void Load()
        {
            this.SetButtons();
        }
        private void SetButtons()
        {
            this.Btn_ServerConnect.Click += Btn_ServerConnect_Click;
            this.Btn_DBOpen.Click += Btn_DBOpen_Click;
            this.Btn_DBClose.Click += Btn_DBClose_Click;
            this.Btn_ServerConnectionClose.Click += Btn_ServerConnectionClose_Click;
        }               
        private void Btn_ServerConnect_Click(object sender, EventArgs e)
        {
            String ConnectionString;
            ConnectionString =
                "server=.\\SQLEXPRESS;" +
                "database=" + TextBox_DBName.Text + ";" +
                "user id=" + TextBox_User_ID.Text + ";" +
                "pwd=" + TextBox_User_Password.Text + ";";

            if (Conn != null)
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
                TextBox_ServerConnection.Text = "연결";
            else
                TextBox_ServerConnection.Text = "해제";
        }
        private void Btn_DBOpen_Click(object sender, EventArgs e)
        {
            Conn.Open();
            if (Conn.State == ConnectionState.Open)
            {
                MessageBox.Show("데이터베이스 열었습니다.");
            }
            else
            {
                MessageBox.Show("데이터베이스 Open 에러");
            }
        }
        private void Btn_DBClose_Click(object sender, EventArgs e)
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
        private void Btn_ServerConnectionClose_Click(object sender, EventArgs e)
        {
            Conn.Dispose();
            Conn = null;
            TextBox_ServerConnection.Text = "해제";
            MessageBox.Show("서버 연결 해제");
        }
    }
}
