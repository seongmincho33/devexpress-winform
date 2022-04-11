using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMJODBConnect
{
    public class DataTableHelperFactory
    {
        public static DataTableHelper CreateDataTableHelper(DataTable dataTable)
        {
            DataTableHelper dataTableHelper = new DataTableHelper();
            dataTableHelper.MainDataTable = dataTable;
            return dataTableHelper;
        }
    }    
}
