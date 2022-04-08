using DataHelperLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            this.SetMemoEdit();
            this.SetButtons();
        }

        private void SetButtons()
        {
            this.btn_retrieve.Click += Btn_retrieve_Click;
        }

        private void Btn_retrieve_Click(object sender, EventArgs e)
        {
            if(this.uC_DBConnection1.UC_DataTable != null)
            {
                this.gridControl1.DataSource = this.uC_DBConnection1.UC_DataTable;
                this.gridControl1.DefaultView.PopulateColumns();
            }
        }

        private void SetMemoEdit()
        {
            this.uC_DBConnection1.queryString = this.memoEdit1.Text;
            this.memoEdit1.EditValueChanged += MemoEdit1_EditValueChanged;
            void MemoEdit1_EditValueChanged(object sender, EventArgs e)
            {
                this.uC_DBConnection1.queryString = this.memoEdit1.Text;
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
        }
    }    
}
