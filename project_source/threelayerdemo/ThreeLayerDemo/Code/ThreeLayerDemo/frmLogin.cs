using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThreeLayerDemo.Core;

namespace ThreeLayerDemo
{
    public partial class frmLogin : Form
    {
        private UserBUS _userBUS;

        public frmLogin()
        {
            InitializeComponent();
             _userBUS = new UserBUS();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            UserVO _userVO = new UserVO();
            _userVO = _userBUS.getUserEmailByName(txtUsername.Text);
            if (_userVO.email == null)
                MessageBox.Show("No Match Found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show(_userVO.email ,"Result",MessageBoxButtons.OK,MessageBoxIcon.Information);
                
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
