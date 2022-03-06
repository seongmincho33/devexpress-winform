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
    public partial class DataBaseConnectionTest001 : Form
    {       
        public DataBaseConnectionTest001()
        {
            InitializeComponent();
            this.Load += DataBaseConnectionTest001_Load;
        }

        private void DataBaseConnectionTest001_Load(object sender, EventArgs e)
        {                     
            ConnectionController connectionController = new ConnectionController(
                this,
                Tuple.Create<TextBox, TextBox, TextBox, TextBox>(this.textBox_ServerConnection, this.textBox_DBName, this.textBox_User_ID, this.textBox_User_Password),
                Tuple.Create<Button, Button, Button, Button>(this.btn_ServerConnect, this.btn_DBOpen, this.btn_DBClose, this.btn_ServerConnectionClose)
                );
            connectionController.Load();
        }      
    }
}
