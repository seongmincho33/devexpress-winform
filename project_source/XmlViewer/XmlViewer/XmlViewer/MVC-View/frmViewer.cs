using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace XmlViewer
{
    public partial class frmViewer : Form
    {
        List<userXMLViewer> ListuserXmlVier = new List<userXMLViewer>();

        public frmViewer()
        {
            InitializeComponent();
            this.btnCreateSearchTab.Click += BtnCreateSearchTab_Click;
            this.btnRemoveTab.Click += BtnRemoveTab_Click;
            this.ToolStripMenuItem_Save.Click += ToolStripMenuItem_Save_Click;
            this.ToolStripMenuItem_Open.Click += ToolStripMenuItem_Open_Click;
        }

        private void ToolStripMenuItem_Open_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    DataSet ds = new DataSet();
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        ds.ReadXml(ofd.FileName);
                        DataTable dt = new DataTable();
                        dt = ds.Tables["XML_PATH"];
                        if (dt.Rows.Count > 0)
                        {
                            this.tabControl_Search.TabPages.Clear();
                            ListuserXmlVier.Clear();
                            foreach (DataRow dr in dt.Rows)
                            {
                                userXMLViewer myUserControl = new userXMLViewer(dr["TAB_NAME"].ToString());
                                myUserControl.PathXML_FolderPath = dr["SEARCH_FILE_PATH"].ToString();
                                ListuserXmlVier.Add(myUserControl);
                                myUserControl.Dock = DockStyle.Fill;
                                string title = "";
                                if (dr["TAB_NAME"].ToString() != null && dr["TAB_NAME"].ToString() != "")
                                {
                                    title = dr["TAB_NAME"].ToString();
                                }
                                else
                                {
                                    title = "검색창" + (tabControl_Search.TabCount + 1).ToString();
                                }
                                TabPage myTabPage = new TabPage(title);//Create new tabpage
                                myTabPage.Controls.Add(myUserControl);
                                this.tabControl_Search.TabPages.Add(myTabPage);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ToolStripMenuItem_Save_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet("DATASET_XML");
            DataTable dt = new DataTable("XML_PATH");
            ds.Tables.Add(dt);
            dt.Columns.Add("TAB_NAME");
            dt.Columns.Add("SEARCH_FILE_PATH");
            foreach (userXMLViewer viewer in this.ListuserXmlVier)
            {
                DataRow dr = dt.NewRow();
                dr["TAB_NAME"] = viewer.ViewerName;
                dr["SEARCH_FILE_PATH"] = viewer.PathXML_FolderPath;
                dt.Rows.Add(dr);
            }
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                //saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                //saveFileDialog.FilterIndex = 2;                
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = "";
                //saveFileDialog.InitialDirectory = @"C:";
                saveFileDialog.Title = "탭 화면 저장";
                saveFileDialog.DefaultExt = "xml";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ds.WriteXml(saveFileDialog.FileName);
                }
            }
        }

        private void BtnCreateSearchTab_Click(object sender, EventArgs e)
        {
            userXMLViewer myUserControl = new userXMLViewer(this.txtTabName.Text);
            ListuserXmlVier.Add(myUserControl);
            myUserControl.Dock = DockStyle.Fill;
            string title = "";
            if (this.txtTabName.Text != null && this.txtTabName.Text != "")
            {
                title = this.txtTabName.Text;
            }
            else
            {
                title = "검색창" + (tabControl_Search.TabCount + 1).ToString();
            }
            TabPage myTabPage = new TabPage(title);//Create new tabpage
            myTabPage.Controls.Add(myUserControl);
            this.tabControl_Search.TabPages.Add(myTabPage);
        }

        private void BtnRemoveTab_Click(object sender, EventArgs e)
        {
            // Removes the selected tab:  
            if (this.tabControl_Search.SelectedTab != null)
            {
                //foreach(var ss in this.ListuserXmlVier)
                for (int i = 0; i < this.ListuserXmlVier.Count; i++)
                {
                    if (ListuserXmlVier[i] == this.tabControl_Search.SelectedTab.Controls[0] as userXMLViewer)
                    {
                        this.ListuserXmlVier.RemoveAt(i);
                    }
                }
                this.tabControl_Search.TabPages.Remove(this.tabControl_Search.SelectedTab);
                this.txtTabName.Text = null;
            }
        }
    }
}
