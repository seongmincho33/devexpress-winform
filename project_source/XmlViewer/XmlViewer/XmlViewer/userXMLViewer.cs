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
using Viewer.Properties;

namespace Viewer
{
    public partial class userXMLViewer : UserControl
    {
        private List<(DataSet, string)> DsFiles { get; set; }

        private List<(string[], string)> TextFiles { get; set; }

        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();

        private int LastNodeIndex = 0;

        private string LastSearchText;

        public string ViewerName { get; set; }

        private string _PathXML_FolderPath;

        public string PathXML_FolderPath
        {
            get { return _PathXML_FolderPath; }
            set 
            {
                _PathXML_FolderPath = value;
                this.txtXML_FolderPath.Text = value;
            }
        }       

        public userXMLViewer(string viewerName = "")
        {
            InitializeComponent();
            ViewerName = viewerName;
            this.SetControl();            
        }

        private void SetControl()
        {            
            this.dataGridView_XMLDataTable.RowTemplate.Resizable = DataGridViewTriState.True;
            this.dataGridView_XMLDataTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.richTextBox_TEXTView.Visible = false;
            this.btnSearch_FolderPath.Click += Btn_Search_Click;
            this.btnFileNameSearch.Click += BtnFileNameSearch_Click;
            this.txtFileNameSearch.KeyDown += TxtFileNameSearch_KeyDown;
            this.txtSearchRecord.KeyDown += TxtSearchRecord_KeyDown;
            this.txtXML_FolderPath.KeyDown += TxtXML_FolderPath_KeyDown;
            this.btnSearchRecord.Click += BtnSearchRecord_Click;
            this.treeView_FolderPath.AfterSelect += TreeView_FolderPath_AfterSelect;
            this.dataGridView_XMLDataTable.MouseHover += DataGridView_XMLDataTable_MouseHover;
            this.dataGridView_XMLDataTable.MouseLeave += DataGridView_XMLDataTable_MouseLeave;
            this.txtXML_FolderPath.TextChanged += TxtXML_FolderPath_TextChanged;
        }

        private void TxtXML_FolderPath_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            this.PathXML_FolderPath = box.Text;
        }

        private void DataGridView_XMLDataTable_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void DataGridView_XMLDataTable_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Cross;
        }

        private void TxtXML_FolderPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSearch_FolderPath.PerformClick();
            }
        }

        private void TxtSearchRecord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSearchRecord.PerformClick();
            }
        }

        private void TxtFileNameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnFileNameSearch.PerformClick();
            }
        }      

        private void BtnSearchRecord_Click(object sender, EventArgs e)
        {
            string searchValue = this.txtSearchRecord.Text;

            //this.dataGridView_XMLDataTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.txtSearchRecordColumn.Text != null && this.txtSearchRecordColumn.Text != "")
                {
                    foreach (DataGridViewRow row in this.dataGridView_XMLDataTable.Rows)
                    {                        
                        if (row.Cells[this.txtSearchRecordColumn.Text].Value != null && row.Cells[this.txtSearchRecordColumn.Text].Value.ToString().ToLower().Contains(searchValue.ToLower()))
                        {
                            row.Selected = true;                            
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in this.dataGridView_XMLDataTable.Rows)
                    {
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            if (row.Cells[i].Value != null && row.Cells[i].Value.ToString().ToLower().Contains(searchValue.ToLower()))
                            {
                                row.Selected = true;
                                break;
                            }
                        }
                    }
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            //this.dataGridView_XMLDataTable.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }     

        private void TreeView_FolderPath_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                this.dataGridView_XMLDataTable.DataSource = null;
                this.richTextBox_TEXTView.Visible = false;
                this.richTextBox_TEXTView.Clear();
                dynamic aa = e.Node.Tag;
                if (e.Node.Tag.GetType() == typeof(DataTable))
                {
                    this.dataGridView_XMLDataTable.DataSource = (DataTable)e.Node.Tag;
                    this.lblTableName.Text = e.Node.Tag.ToString();
                    this.lblDataSetName.Text = e.Node.Parent.Text;
                }
                else if (aa.Item2.Contains(".txt"))
                {
                    this.richTextBox_TEXTView.Visible = true;
                    foreach (var ss in this.TextFiles)
                    {
                        foreach (var ii in ss.Item1)
                        {
                            this.richTextBox_TEXTView.AppendText(ii);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnFileNameSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            string searchText = this.txtFileNameSearch.Text;
            if (String.IsNullOrEmpty(searchText))
            {
                return;
            };

            if (LastSearchText != searchText)
            {
                //It's a new Search
                CurrentNodeMatches.Clear();
                LastSearchText = searchText;
                LastNodeIndex = 0;
                SearchNodes(searchText, this.treeView_FolderPath.Nodes[0]);
            }

            if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
            {
                TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                LastNodeIndex++;
                this.treeView_FolderPath.SelectedNode = selectedNode;
                this.treeView_FolderPath.SelectedNode.Expand();
                this.treeView_FolderPath.Select();
            }
            this.Cursor = Cursors.Default;
        }

        private void SearchNodes(string SearchText, TreeNode StartNode)
        {            
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    CurrentNodeMatches.Add(StartNode);
                };
                if (StartNode.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search 
                };
                StartNode = StartNode.NextNode;
            };

        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            //Settings.Default.SearchXMLPath = this.txtXML_FolderPath.Text;
            //Settings.Default.Save();
            this.Cursor = Cursors.WaitCursor;
            this.SetXMLFilesToDataSetList();
            this.SetTextFilesToList();
            this.SetDataSetListToTreeView();
            this.Cursor = Cursors.Default;
        }

        private void SetXMLFilesToDataSetList()
        {
            try
            {
                this.DsFiles = new List<(DataSet, string)>();
                string[] files = Directory.GetFiles(this.txtXML_FolderPath.Text);
                foreach (var file in files)
                {
                    DataSet ds = this.XmlToDataSet(file);
                    if (ds != null)
                    {
                        this.DsFiles.Add((ds, Path.GetFileName(file)));
                    }             
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetTextFilesToList()
        {
            try
            {
                this.TextFiles = new List<(string[], string)>();
                string[] files = Directory.GetFiles(this.txtXML_FolderPath.Text);
                foreach (var file in files)
                {                    
                    if (Path.GetFileName(file).Contains(".txt"))
                    {
                        this.TextFiles.Add((System.IO.File.ReadAllLines(file), Path.GetFileName(file)));
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private DataSet XmlToDataSet(string file_path)
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(file_path);
            }
            catch (Exception ex)
            {
                return null;
            }
            return ds;
        }

        private void SetDataSetListToTreeView()
        {
            this.treeView_FolderPath.Nodes.Clear();
            foreach ((DataSet, string) dsFile in DsFiles)
            {
                TreeNode dsNode = new TreeNode(dsFile.Item2, 0, 0);
                dsNode.Tag = dsFile;
                foreach (DataTable dtFile in dsFile.Item1.Tables)
                {
                    TreeNode dtNode = new TreeNode(dtFile.TableName, 0, 0);
                    dtNode.Tag = dtFile;
                    dsNode.Nodes.Add(dtNode);
                }
                this.treeView_FolderPath.Nodes.Add(dsNode);
            }

            if(this.TextFiles.Count > 0)
            {
                foreach (var textfile in this.TextFiles)
                {
                    TreeNode dsNode = new TreeNode(textfile.Item2, 0, 0);
                    dsNode.Tag = textfile;
                    this.treeView_FolderPath.Nodes.Add(dsNode);
                }
            }
        }
    }
}
