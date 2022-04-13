using SMJODBConnect;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect.Data.DataAccessComponent
{
    public class DAC_Department
    {
        public SqlConnection sqlCon { get; set; }
        private DataSet DepartmentSet { get; set; }
        private DataTable DepartmentTable { get; set; }

        public DAC_Department(SqlConnection sqlCon)
        {
            this.sqlCon = sqlCon;
            this.DepartmentSet = new DataSet();
            this.DepartmentTable = new DataTable();
            DataTableHelper dth = DataTableHelperFactory.CreateDataTableHelper(this.DepartmentTable);
            using (dth)
            {
                dth.SetColunms(
                    (typeof(Guid), "DEPARTMENT_ID")
                    , (typeof(Guid), "DEPARTMENT_PARENT_ID")
                    , (typeof(Guid), "USER_ID")
                    , (typeof(string), "DEPARTMENT_NAME")
                    , (typeof(string), "NOTES")                    
                    );
                this.DepartmentTable = dth.MainDataTable.Clone();
            }
        }

        public void FillParameters(SqlCommand cmd, DataRow item)
        {
            SqlParameter paramDepartment_id = new SqlParameter("DEPARTMENT_ID", System.Data.SqlDbType.UniqueIdentifier);
            paramDepartment_id.Value = item["DEPARTMENT_ID"];

            SqlParameter paramDepartment_Parent_id = new SqlParameter("DEPARTMENT_PARENT_ID", System.Data.SqlDbType.UniqueIdentifier);
            paramDepartment_Parent_id.Value = item["DEPARTMENT_PARENT_ID"];

            SqlParameter paramDepartment_Name = new SqlParameter("DEPARTMENT_NAME", System.Data.SqlDbType.NVarChar, 20);
            paramDepartment_Name.Value = item["DEPARTMENT_NAME"];

            SqlParameter paramUser_User_id = new SqlParameter("USER_ID", System.Data.SqlDbType.UniqueIdentifier);
            paramUser_User_id.Value = item["USER_ID"];

            SqlParameter paramNotes = new SqlParameter("NOTES", System.Data.SqlDbType.Bit);
            paramNotes.Value = item["NOTES"];
        
            cmd.Parameters.Add(paramDepartment_id);
            cmd.Parameters.Add(paramDepartment_Parent_id);
            cmd.Parameters.Add(paramDepartment_Name);
            cmd.Parameters.Add(paramUser_User_id);
            cmd.Parameters.Add(paramNotes);            
        }

        public void Insert(DataRow item)
        {
            string txt =
                "INSERT INTO [DEPARTMENT]" +
                "(" +
                "DEPARTMENT_ID" +
                ", DEPARTMENT_PARENT_ID" +
                ", DEPARTMENT_NAME" +
                ", USER_ID" +
                ", NOTES" +             
                ") " +
                "VALUES" +
                "(" +
                  "@DEPARTMENT_ID" +
                ", @DEPARTMENT_PARENT_ID" +
                ", @DEPARTMENT_NAME" +
                ", @USER_ID" +
                ", @NOTES" +
                ")";

            try
            {
                SqlCommand cmd = new SqlCommand(txt, sqlCon);
                FillParameters(cmd, item);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(DataRow item)
        {
            string txt =
                "UPDATE [DEPARTMENT] " +
                "SET " +
                "DEPARTMENT_ID=@DEPARTMENT_ID" +
                ", DEPARTMENT_PARENT_ID=@DEPARTMENT_PARENT_ID" +
                ", USER_ID=@USER_ID" +
                ", DEPARTMENT_NAME=@DEPARTMENT_NAME" +
                ", NOTES=@NOTES" +
                "WHERE " +
                "DEPARTMENT_ID=@DEPARTMENT_ID";
            try
            {
                SqlCommand cmd = new SqlCommand(txt, sqlCon);
                FillParameters(cmd, item);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public void Delete(DataRow item)
        {
            string txt = "DELETE FROM [DEPARTMENT] WHERE DEPARTMENT_ID=@DEPARTMENT_ID";

            try
            {
                SqlCommand cmd = new SqlCommand(txt, sqlCon);
                FillParameters(cmd, item);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public DataSet DataSet_SelectAll()
        {
            string txt = "SELECT * FROM [DEPARTMENT]";
            try
            {
                this.DepartmentSet.Tables.Clear();
                SqlDataAdapter adapter = new SqlDataAdapter(txt, sqlCon);
                adapter.Fill(this.DepartmentSet, "DEPARTMENT");
            }
            catch (Exception ex)
            {

            }
            return DepartmentSet;
        }
    }
}
