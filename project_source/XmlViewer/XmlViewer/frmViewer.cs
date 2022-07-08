using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlViewer.Properties;

namespace XmlViewer
{
    public partial class frmViewer : Form
    {      
        public frmViewer()
        {
            InitializeComponent();
            this.btnCreateSearchTab.Click += BtnCreateSearchTab_Click;
            this.btnRemoveTab.Click += BtnRemoveTab_Click;
        }

        private void BtnCreateSearchTab_Click(object sender, EventArgs e)
        {
            userXMLViewer myUserControl = new userXMLViewer();
            myUserControl.Dock = DockStyle.Fill;
            string title = "검색창" + (tabControl_Search.TabCount + 1).ToString();
            TabPage myTabPage = new TabPage(title);//Create new tabpage
            myTabPage.Controls.Add(myUserControl);
            this.tabControl_Search.TabPages.Add(myTabPage);
        }

        private void BtnRemoveTab_Click(object sender, EventArgs e)
        {
            // Removes the selected tab:  
            this.tabControl_Search.TabPages.Remove(this.tabControl_Search.SelectedTab);          
        }       
    }
}
