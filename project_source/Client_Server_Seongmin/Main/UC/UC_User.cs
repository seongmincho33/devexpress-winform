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
using UC_Library;
using DevExpress.XtraGrid.Views.Grid;
using SMJODBConnect;

namespace Main.UC
{
    public partial class UC_User : DevExpress.XtraEditors.XtraUserControl
    {
        DBConnection dbConn;
        public UC_User(DBConnection dbConn)
        {
            InitializeComponent();
            this.dbConn = dbConn;
            this.SetControls();
            
           
        }

        private void SetControls()
        {
            this.SetButtons_Department();
            this.SetButtons_User();
            this.SetTreeListControl();
        }

        private void SetTreeListControl()
        {
            this.treeList1.KeyFieldName = "DEPARTMENT_ID";
            this.treeList1.ParentFieldName = "DEPARTMENT_PARENT_ID";
            this.treeList1.RootValue = 0;
        }

        private void SetButtons_User()
        {
            this.btn_Retrieve_User.Click += Btn_Click_User;
            this.btn_Insert_User.Click += Btn_Click_User;
            this.btn_Save_User.Click += Btn_Click_User;
            this.btn_Delete_User.Click += Btn_Click_User;
            void Btn_Click_User(object sender, EventArgs e)
            {
                SimpleButton btn = sender as SimpleButton;
                if (btn.Text == "조회")
                {
                    this.DataRetrieve_User();
                }
                else if (btn.Text == "추가")
                {
                    if (this.gridControl1.DataSource != null)
                    {
                        DataRow row = (this.gridControl1.DataSource as DataTable).NewRow();
                        row["USER_ID"] = Guid.NewGuid().ToString();
                        (this.gridControl1.DataSource as DataTable).Rows.Add(row);
                    }
                }
                else if (btn.Text == "저장")
                {
                    if (XtraMessageBox.Show("저장하시겠습니까?", "저장", MessageBoxButtons.OKCancel) == DialogResult.No)
                    {
                        return;
                    }

                    foreach (DataRow row in (this.gridControl1.DataSource as DataTable).Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            this.dbConn.DAC_Insert_User(row);
                        }
                        if (row.RowState == DataRowState.Modified)
                        {
                            this.dbConn.DAC_Update_User(row);
                        }
                    }
                    (this.gridControl1.DataSource as DataTable).AcceptChanges();
                    this.DataRetrieve_User();
                }
                else if (btn.Text == "삭제")
                {
                    if (XtraMessageBox.Show("삭제하시겠습니까?", "삭제", MessageBoxButtons.OKCancel) == DialogResult.No)
                    {
                        return;
                    }
                    
                    //DataRowView selRow = (DataRowView)gridControl1.GetFocusedRow();
                    int[] selRows = ((GridView)gridControl1.MainView).GetSelectedRows();
                    DataRowView selRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(selRows[0]));
                    //_ = selRow["name"].ToString();               
                    this.dbConn.DAC_Delete_User((DataRow)selRow.Row);
                    this.DataRetrieve_User();
                }
            }
        }

        private void SetButtons_Department()
        {
            this.btn_Retrieve_Dept.Click += Btn_Dept_Click;
            this.btn_Insert_Dept.Click += Btn_Dept_Click;
            this.btn_Save_Dept.Click += Btn_Dept_Click;
            this.btn_Delete_Dept.Click += Btn_Dept_Click;
            void Btn_Dept_Click(object sender, EventArgs e)
            {
                SimpleButton btn = sender as SimpleButton;
                if (btn.Text == "조회")
                {
                    this.DataRetrieve_Department();
                }
                else if (btn.Text == "추가")
                {
                    try
                    {
                        if (this.treeList1.DataSource != null
                            && this.treeList1.AllNodesCount != 0)
                        {
                            DataRow row = (this.treeList1.DataSource as DataTable).NewRow();
                            row["DEPARTMENT_ID"] = Guid.NewGuid().ToString();
                            row["DEPARTMENT_PARENT_ID"] = this.treeList1.GetFocusedRowCellValue("DEPARTMENT_ID");
                            (this.treeList1.DataSource as DataTable).Rows.Add(row);
                        }
                        else
                        {
                            DataRow row = (this.treeList1.DataSource as DataTable).NewRow();
                            row["DEPARTMENT_ID"] = Guid.NewGuid().ToString();
                            row["DEPARTMENT_PARENT_ID"] = Guid.NewGuid().ToString();
                            (this.treeList1.DataSource as DataTable).Rows.Add(row);
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message);
                    }
                }
                else if (btn.Text == "저장")
                {
                    if (XtraMessageBox.Show("저장하시겠습니까?", "저장", MessageBoxButtons.OKCancel) == DialogResult.No)
                    {
                        return;
                    }

                    foreach (DataRow row in (this.treeList1.DataSource as DataTable).Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            this.dbConn.DAC_Insert_Department(row);
                        }
                        if (row.RowState == DataRowState.Modified)
                        {
                            this.dbConn.DAC_Update_Department(row);
                        }
                    }
                    (this.treeList1.DataSource as DataTable).AcceptChanges();
                    this.DataRetrieve_Department();
                }
                else if (btn.Text == "삭제")
                {
                    if (XtraMessageBox.Show("삭제하시겠습니까?", "삭제", MessageBoxButtons.OKCancel) == DialogResult.No)
                    {
                        return;
                    }                                       
                    this.dbConn.DAC_Delete_Department(this.treeList1.GetFocusedDataRow());
                    this.DataRetrieve_Department();
                }
            }
        }        

        private void DataRetrieve_User()
        {
            try
            {
                this.gridControl1.DataBindings.Clear();
                this.gridControl1.DataSource = null;
                this.gridControl1.DataSource = this.dbConn.DAC_SelectAll_User().Tables[0];
                this.gridControl1.DefaultView.PopulateColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("데이터 베이스 연결 안됨" + ex.ToString());
            }
        }

        private void DataRetrieve_Department()
        {
            try
            {
                this.treeList1.DataBindings.Clear();
                this.treeList1.DataSource = null;
                this.treeList1.DataSource = this.dbConn.DAC_SelectAll_Department().Tables[0];
                this.treeList1.PopulateColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("데이터 베이스 연결 안됨" + ex.ToString());
            }
        }
    }
}
