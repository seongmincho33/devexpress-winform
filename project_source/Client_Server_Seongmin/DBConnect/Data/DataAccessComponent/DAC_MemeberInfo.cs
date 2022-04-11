using DBConnect.Data.DataContainer_POCO;
using SMJODBConnect;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect.Data.DataAccessComponent
{
    public class DAC_MemeberInfo
    {        
        public SqlConnection sqlCon { get; set; }        
        private DataSet MemberInfoSet { get; set; }
        private DataTable MemberInfoTable { get; set; }

        public DAC_MemeberInfo(SqlConnection sqlCon)
        {
            this.sqlCon = sqlCon;
            this.MemberInfoSet = new DataSet();
            this.MemberInfoTable = new DataTable();
            DataTableHelper dth = DataTableHelperFactory.CreateDataTableHelper(this.MemberInfoTable);
            using (dth)
            {
                dth.SetColunms(
                    (typeof(string), "Name")
                    , (typeof(DateTime), "Birth")
                    , (typeof(string), "Email")
                    , (typeof(byte), "Family")
                    );
                this.MemberInfoTable = dth.MainDataTable.Clone();
            }
        }

        public void FillParameters(SqlCommand cmd, DataRow item)
        {
            SqlParameter paramMember_id = new SqlParameter("MEMBER_ID", System.Data.SqlDbType.UniqueIdentifier, 20);
            paramMember_id.Value = item["MEMBER_ID"];

            SqlParameter paramName = new SqlParameter("Name", System.Data.SqlDbType.NVarChar, 20);            
            paramName.Value = item["Name"];

            SqlParameter paramBirth = new SqlParameter("Birth", System.Data.SqlDbType.Date);           
            paramBirth.Value = item["Birth"];

            SqlParameter paramEmail = new SqlParameter("Email", System.Data.SqlDbType.NVarChar, 100);
            paramEmail.Value = item["Email"];

            SqlParameter paramFamily = new SqlParameter("Family", System.Data.SqlDbType.TinyInt);
            paramFamily.Value = item["Family"];

            cmd.Parameters.Add(paramMember_id);
            cmd.Parameters.Add(paramName);
            cmd.Parameters.Add(paramBirth);
            cmd.Parameters.Add(paramEmail);
            cmd.Parameters.Add(paramFamily);
        }

        public void Insert(DataRow item)
        {
            string txt = "INSERT INTO MEMBERINFO(MEMBER_ID, NAME, BIRTH, EMAIL, FAMILY) VALUES(@MEMBER_ID, @NAME, @BIRTH, @EMAIL, @FAMILY)";

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
            string txt = "UPDATE MEMBERINFO SET MEMBER_ID=@MEMBER_ID, NAME=@NAME, BIRTH=@BIRTH, FAMILY=@FAMILY WHERE MEMBER_ID=@MEMBER_ID";
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
            string txt = "DELETE FROM MEMBERINFO WHERE MEMBER_ID=@MEMBER_ID";

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

        public MemberInfo[] SelectAll()
        {
            string txt = "SELECT * FROM MEMBERINFO";
            try
            {
                this.MemberInfoSet.Tables.Clear();
                ArrayList list = new ArrayList();

                SqlCommand cmd = new SqlCommand(txt, sqlCon);

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        MemberInfo item = new MemberInfo();

                        item.NAME = reader.GetString(0);
                        item.BIRTH = reader.GetDateTime(1);
                        item.EMAIL = reader.GetString(2);
                        item.FAMILY = reader.GetByte(3);

                        list.Add(item);
                    }
                }
                return list.ToArray(typeof(MemberInfo)) as MemberInfo[];
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public DataSet DataSet_SelectAll()
        {
            string txt = "SELECT * FROM MEMBERINFO";
            try
            {
                this.MemberInfoSet.Tables.Clear();
                SqlDataAdapter adapter = new SqlDataAdapter(txt, sqlCon);
                adapter.Fill(this.MemberInfoSet, "MemberInfo");
            }
            catch (Exception ex)
            {
                
            }        
            return MemberInfoSet;            
        }        
    }
}
