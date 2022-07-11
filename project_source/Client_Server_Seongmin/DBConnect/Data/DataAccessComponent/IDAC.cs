using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect.Data.DataAccessComponent
{
    public interface IDAC<T>
    {
        SqlConnection sqlCon { set; }
        void FillParameters(SqlCommand cmd, T item);
        void Insert(T item);
        void Update(T item);
        void Delete(T item);
        DataSet DataSet_SelectAll();
    }
}
