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
    public class DAC_User
    {
        public SqlConnection sqlCon { get; set; }
        private DataSet UserSet { get; set; }
        private DataTable UserTable { get; set; }

        public DAC_User(SqlConnection sqlCon)
        {
            this.sqlCon = sqlCon;
            this.UserSet = new DataSet();
            this.UserTable = new DataTable();
            DataTableHelper dth = DataTableHelperFactory.CreateDataTableHelper(this.UserTable);
            using (dth)
            {
                dth.SetColunms(
                    (typeof(Guid), "USER_ID")
                    , (typeof(string), "USER_NAME")
                    , (typeof(string), "USER_PASSWORD")
                    , (typeof(DateTime), "PASSWORD_EXPIRY_DATE")
                    , (typeof(bool), "INACTIVE")
                    , (typeof(string), "EMPLOY_NUMBER")
                    , (typeof(string), "LANGUAGE")
                    , (typeof(string), "NOTES")
                    );
                this.UserTable = dth.MainDataTable.Clone();
            }
        }

        public void FillParameters(SqlCommand cmd, DataRow item)
        {
            SqlParameter paramUser_id = new SqlParameter("USER_ID", System.Data.SqlDbType.UniqueIdentifier, 20);
            paramUser_id.Value = item["USER_ID"];

            SqlParameter paramUser_Name = new SqlParameter("USER_NAME", System.Data.SqlDbType.NVarChar, 20);
            paramUser_Name.Value = item["USER_NAME"];

            SqlParameter paramUser_Password = new SqlParameter("USER_PASSWORD", System.Data.SqlDbType.NVarChar, 20);
            paramUser_Password.Value = item["USER_PASSWORD"];

            SqlParameter paramPassword_Expiry_Date = new SqlParameter("PASSWORD_EXPIRY_DATE", System.Data.SqlDbType.Date);
            paramPassword_Expiry_Date.Value = item["PASSWORD_EXPIRY_DATE"];

            SqlParameter paramInactive = new SqlParameter("INACTIVE", System.Data.SqlDbType.Bit);
            paramInactive.Value = item["INACTIVE"];

            SqlParameter paramEmploy_Number = new SqlParameter("EMPLOY_NUMBER", System.Data.SqlDbType.NVarChar, 20);
            paramEmploy_Number.Value = item["EMPLOY_NUMBER"];

            SqlParameter paramLanguage = new SqlParameter("LANGUAGE", System.Data.SqlDbType.NVarChar, 20);
            paramLanguage.Value = item["LANGUAGE"];

            SqlParameter paramNotes = new SqlParameter("NOTES", System.Data.SqlDbType.NVarChar, 20);
            paramNotes.Value = item["NOTES"];

            cmd.Parameters.Add(paramUser_id);
            cmd.Parameters.Add(paramUser_Name);
            cmd.Parameters.Add(paramUser_Password);
            cmd.Parameters.Add(paramPassword_Expiry_Date);
            cmd.Parameters.Add(paramInactive);
            cmd.Parameters.Add(paramEmploy_Number);
            cmd.Parameters.Add(paramLanguage);
            cmd.Parameters.Add(paramNotes);
        }

        public void Insert(DataRow item)
        {
            string txt =
                "INSERT INTO [USER]" +
                "(" +
                "USER_ID" +
                ", USER_NAME" +
                ", USER_PASSWORD" +
                ", PASSWORD_EXPIRY_DATE" +
                ", INACTIVE" +
                ", EMPLOY_NUMBER" +
                ", LANGUAGE" +
                ", NOTES" +
                ") " +
                "VALUES" +
                "(" +
                "@USER_ID" +
                ", @USER_NAME" +
                ", @USER_PASSWORD" +
                ", @PASSWORD_EXPIRY_DATE" +
                ", @INACTIVE" +
                ", @EMPLOY_NUMBER" +
                ", @LANGUAGE" +
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
                "UPDATE [USER] " +
                "SET " +
                "MEMBER_ID=@MEMBER_ID" +
                ", NAME=@NAME" +
                ", BIRTH=@BIRTH" +
                ", FAMILY=@FAMILY " +
                "USER_ID=@USER_ID" +
                ", USER_NAME=@USER_NAME" +
                ", USER_PASSWORD=@USER_PASSWORD" +
                ", PASSWORD_EXPIRY_DATE=@PASSWORD_EXPIRY_DATE" +
                ", INACTIVE=@INACTIVE" +
                ", EMPLOY_NUMBER=@EMPLOY_NUMBER" +
                ", LANGUAGE=@LANGUAGE" +
                ", NOTES=@NOTES" +
                "WHERE " +
                "USER_ID=@USER_ID";
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
            string txt = "DELETE FROM [USER] WHERE USER_ID=@USER_ID";

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
            string txt = "SELECT * FROM [USER]";
            try
            {
                this.UserSet.Tables.Clear();
                SqlDataAdapter adapter = new SqlDataAdapter(txt, sqlCon);
                adapter.Fill(this.UserSet, "USER");
            }
            catch (Exception ex)
            {

            }
            return UserSet;
        }
    }
}
