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
        public DataTable SelectedTable { get; set; }
        
        public DBConnection()
        {
            this.Conn = new SqlConnection();
            this.SelectedTable = new DataTable();
        }

        #region CRUD
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
        public bool SendQuerystring_R(string queryString, out string message, out DataTable dataTable)
        {
            bool IsSuccess = false;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conn;
                cmd.CommandText = queryString;                
                SqlDataReader reader = cmd.ExecuteReader();             
                this.SelectedTable.Load(reader);
            }
            catch (Exception ex)
            {
                dataTable = null;
                message = ex.ToString();
                return IsSuccess = false;
            }

            dataTable = this.SelectedTable;
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
            return IsSuccess = true;
        }
        #endregion
    }
}
