using DBConnect.Data.DataAccessComponent;
using DBConnect.Data.DataContainer_POCO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMJODBConnect
{
    public class DBConnection
    {
        private SqlConnection Conn { get; set; }        
        private DataSet SelectedDataSet { get; set; }
        private DAC_MemeberInfo Dac_MemberInfo { get; set; }
        private DAC_User DAC_User { get; set; }
        private DAC_Department DAC_Department { get; set; }
        
        public DBConnection()
        {
            this.Conn = new SqlConnection();            
            this.SelectedDataSet = new DataSet();
            this.CreateDAC_MemberInfo(this.Conn);
            this.CreateDAC_User(this.Conn);
            this.CreateDAC_Department(this.Conn);
        }

        #region DAC MemberInfo
        public void CreateDAC_MemberInfo(SqlConnection Conn)
        {
            this.Dac_MemberInfo = new DAC_MemeberInfo(Conn);
        }

        public void DAC_Insert_MemberInfo(DataRow item)
        {
            this.Dac_MemberInfo.Insert(item);
        }

        public void DAC_Update_MemberInfo(DataRow item)
        {
            this.Dac_MemberInfo.Update(item);
        }

        public void DAC_Delete_MemberInfo(DataRow item)
        {
            this.Dac_MemberInfo.Delete(item);
        }

        public DataSet DAC_SelectAll_MemberInfo()
        {
            return this.Dac_MemberInfo.DataSet_SelectAll();
        }
        #endregion

        #region DAC User
        public void CreateDAC_User(SqlConnection Conn)
        {
            this.DAC_User = new DAC_User(Conn);
        }

        public void DAC_Insert_User(DataRow item)
        {
            this.DAC_User.Insert(item);
        }

        public void DAC_Update_User(DataRow item)
        {
            this.DAC_User.Update(item);
        }

        public void DAC_Delete_User(DataRow item)
        {
            this.DAC_User.Delete(item);
        }

        public DataSet DAC_SelectAll_User()
        {
            return this.DAC_User.DataSet_SelectAll();
        }
        #endregion

        #region DAC Department
        public void CreateDAC_Department(SqlConnection Conn)
        {
            this.DAC_Department = new DAC_Department(Conn);
        }

        public void DAC_Insert_Department(DataRow item)
        {
            this.DAC_Department.Insert(item);
        }

        public void DAC_Update_Department(DataRow item)
        {
            this.DAC_Department.Update(item);
        }

        public void DAC_Delete_Department(DataRow item)
        {
            this.DAC_Department.Delete(item);
        }

        public DataSet DAC_SelectAll_Department()
        {
            return this.DAC_Department.DataSet_SelectAll();
        }
        #endregion

        #region 쿼리문 직접 받을때
        public bool SendQuerystring_CUD(string queryString, out string message)
        {
            bool IsSuccess = false;

            try
            {                        
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conn;
                cmd.CommandText = queryString;
                int affectedCount = cmd.ExecuteNonQuery();
                //Console.WriteLine(affectedCount); //출력결과 : 1
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                return IsSuccess = false;
            }

            message = "쿼리 성공";
            return IsSuccess = true;
        }
        public bool SendQuerystring_R(string queryString, out string message, out DataSet dataSet)
        {
            bool IsSuccess = false;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conn;
                cmd.CommandText = queryString;                
                //SqlDataReader reader = cmd.ExecuteReader();             
                //this.SelectedTable.Load(reader);
                this.SelectedDataSet.Tables.Clear();
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, Conn);
                adapter.Fill(this.SelectedDataSet);
            }
            catch (Exception ex)
            {
                dataSet = null;
                message = ex.ToString();
                return IsSuccess = false;
            }

            //dataTable = this.SelectedTable;
            dataSet = this.SelectedDataSet;
            message = "쿼리 성공";
            return IsSuccess = true;
        }
        #endregion

        #region DB연결 및 해제
        public bool ServerConnect(string serverName, string databaseName, string userID, string password, out string message)
        {
            bool IsSuccess = false;

            try
            {
                if (Conn != null)
                {
                    Conn.Dispose(); // Conn있으면 Close()해줌
                }
                Conn.ConnectionString =
                "server=.\\" + serverName + ";" +
                "database=" + databaseName + ";" +
                "user id=" + userID + ";" +
                "pwd=" + password + ";";
                if (Conn != null)
                {
                    message = "연결";
                    return IsSuccess = true;
                }
                else
                {
                    message = "해제";
                    return IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                return IsSuccess = false;
            }            
        }

        public bool DBOpen(out string message)
        {
            bool IsSuccess = false;

            if (Conn != null)
            {
                try
                {
                    Conn.Open();
                    if (Conn.State == ConnectionState.Open)
                    {
                        message = "데이터베이스 열었습니다.";
                        return IsSuccess = true;
                    }
                    else
                    {                        
                        message = "데이터베이스 Open 에러. 서버와 연결되지 않았습니다.";
                        return IsSuccess = false;
                    }
                }
                catch (Exception ex)
                {                    
                    message = ex.ToString();
                    return IsSuccess = false;
                }

            }
            else
            {               
                message = "연결된 DB가 없습니다.";
                return IsSuccess = false;
            }
        }

        public bool DBClose(out string message)
        {
            bool IsSuccess = false;

            if (Conn != null)
            {
                try
                {
                    Conn.Close();
                    if (Conn.State == ConnectionState.Closed && Conn.ConnectionString != "")
                    {
                        message = "데이터베이스 닫았습니다.";
                        return IsSuccess = true;
                    }
                    else
                    {
                        message = "데이터베이스 Close 에러. 서버와 연결되지 않았습니다.";
                        return IsSuccess = false;
                    }
                }
                catch (Exception ex)
                {
                    message = ex.ToString();
                    return IsSuccess = false;
                }

            }
            else
            {
                message = "연결된 DB가 없습니다.";
                return IsSuccess = false;
            }
        }

        public bool ServerDisconnect(out string message)
        {
            bool IsSuccess = false;

            try
            {
                Conn.Dispose();
                Conn = null;
                message = "해제";
            }
            catch(Exception ex)
            {
                message = ex.ToString();
                message = "해제 실패";
            }

            this.Conn = new SqlConnection(); //기존 연결 해지후 새로운 SqlConnection할당       
            this.CreateDAC_MemberInfo(this.Conn); //데이터엑세스컨트롤러(DAC)이 있다면 새로운 SqlConnection을 데이터엑세스컨트롤러에게 주어야 합니다.
            return IsSuccess = true;
        }
        #endregion
    }
}
