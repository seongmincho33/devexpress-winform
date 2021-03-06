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
    public partial class UC_Member : DevExpress.XtraEditors.XtraUserControl
    {
        private DBConnection dbConn;
        public UC_Member(DBConnection dbConn)
        {
            InitializeComponent();
            this.SetControls();
            this.dbConn = dbConn;
        }
        private void SetControls()
        {
            this.SetButtons();
        }
        private void SetButtons()
        {
            this.btn_Retrieve.Click += Btn_Click;
            this.btn_Insert.Click += Btn_Click;
            this.btn_Update.Click += Btn_Click;
            this.btn_Delete.Click += Btn_Click;
            void Btn_Click(object sender, EventArgs e)
            {
                SimpleButton btn = sender as SimpleButton;                
                if (btn.Text == "조회")
                {
                    this.DataRetrieve();
                }
                else if (btn.Text == "추가")
                {
                    if (this.gridControl1.DataSource != null)
                    {
                        DataRow row = (this.gridControl1.DataSource as DataTable).NewRow();
                        row["MEMBER_ID"] = Guid.NewGuid().ToString();
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
                            this.dbConn.DAC_Insert_MemberInfo(row);
                        }
                        if (row.RowState == DataRowState.Modified)
                        {
                            this.dbConn.DAC_Update_MemberInfo(row);
                        }
                    }
                    (this.gridControl1.DataSource as DataTable).AcceptChanges();
                    this.DataRetrieve();
                }
                else if (btn.Text == "삭제")
                {
                    if (XtraMessageBox.Show("삭제하시겠습니까?", "삭제", MessageBoxButtons.OKCancel) == DialogResult.No)
                    {
                        return;
                    }

                    int[] selRows = ((GridView)gridControl1.MainView).GetSelectedRows();
                    DataRowView selRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(selRows[0]));
                    //_ = selRow["name"].ToString();               
                    this.dbConn.DAC_Delete_MemberInfo((DataRow)selRow.Row);
                    this.DataRetrieve();
                }
            }
        }
        private void DataRetrieve()
        {
            try
            {
                this.gridControl1.DataBindings.Clear();
                this.gridControl1.DataSource = null;
                this.gridControl1.DataSource = this.dbConn.DAC_SelectAll_MemberInfo().Tables[0];
                this.gridControl1.DefaultView.PopulateColumns();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show("데이터 베이스 연결 안됨" + ex.ToString());
            }           
        }
    }
}
