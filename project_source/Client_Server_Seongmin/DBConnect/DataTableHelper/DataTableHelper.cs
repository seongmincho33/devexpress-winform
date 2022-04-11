using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SMJODBConnect
{
    public class DataTableHelper : IDisposable
    {
        private bool _disposed = false;
        public DataTable MainDataTable { get; set; }

        public DataTableHelper()
        {
            this.MainDataTable = new DataTable();
        }

        public void AddColunms(string columnName, Type columnType)
        {
            this.MainDataTable.Columns.Add(new DataColumn(columnName, columnType));
        }

        /// <summary>
        /// 컬럼을 셋팅합니다.
        /// </summary>
        /// <param name="columnNames">컬럼의 형식과 이름을 튜플로 받습니다.</param>
        public void SetColunms(params (Type, string)[] columnNames)
        {
            foreach (var columnName in columnNames)
            {
                this.MainDataTable.Columns.Add(new DataColumn(columnName.Item2, columnName.Item1));
            }
        }

        public void SetData(int rowIndex, string columnName, object value)
        {
            if (this.MainDataTable.Rows[rowIndex] != null)
                this.MainDataTable.Rows[rowIndex][columnName] = value;
        }

        public void SetDataRow(params object[] data)
        {
            DataRow dataRow = null;
            dataRow = this.MainDataTable.NewRow();
            try
            {
                for (int i = 0; i < this.MainDataTable.Columns.Count; i++)
                {
                    dataRow[i] = data[i];
                }
            }
            catch (Exception ex)
            {
                //인덱스 에러
            }
            this.MainDataTable.Rows.Add(dataRow);
        }

        public void PrintDataTable(Action<string, string> func)
        {
            foreach (DataRow dataRow in this.MainDataTable.Rows)
            {
                foreach (DataColumn column in this.MainDataTable.Columns)
                {
                    func(column.ColumnName, dataRow[column].ToString());
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }
    }
}
