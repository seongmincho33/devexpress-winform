using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using UC_Library;
namespace Main
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
            this.SetGridControl();            
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
                            this.uC_DBConnection1.dbConn.DAC_Insert(row);
                        }
                        if (row.RowState == DataRowState.Modified)
                        {
                            this.uC_DBConnection1.dbConn.DAC_Update(row);
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
                    this.uC_DBConnection1.dbConn.DAC_Delete((DataRow)selRow.Row);
                    this.DataRetrieve();
                }
            }
        }

        private void SetGridControl()
        {
            //DataTable somethingDataTable = new DataTable();

            //DataTableHelper somethinghelpedtable = DataTableHelperFactory.CreateDataTableHelper(somethingDataTable);

            //using (somethinghelpedtable)
            //{
            //    somethinghelpedtable.SetColunms((typeof(string), "NAME"), (typeof(int), "AGE"), (typeof(string), "TEL"));

            //    somethinghelpedtable.SetDataRow("성민", 100, "010-2222-3333");
            //}

            //this.gridControl1.DataSource = somethingDataTable;

            if (this.uC_DBConnection1.UC_DataSet != null)
            {
                this.gridControl1.DataBindings.Clear();
                this.gridControl1.DataSource = null;
                this.gridControl1.DataSource = this.uC_DBConnection1.UC_DataSet;
                this.gridControl1.DefaultView.PopulateColumns();
            }
        }

        private void DataRetrieve()
        {
            this.gridControl1.DataBindings.Clear();
            this.gridControl1.DataSource = null;
            this.gridControl1.DataSource = this.uC_DBConnection1.dbConn.DAC_SelectAll().Tables[0];
            this.gridControl1.DefaultView.PopulateColumns();
        }
    }    
}
