using LogApp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using vSuperMTClient.Entities;

namespace vSuperMTClient.DALs
{
    public class UserDAL
    {
        MySqlConnection con;
        public UserDAL(string ClientDB)
        {
            string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
           // ClientDB = "vboardmtclient_29";
            con =new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB));
        }

        public List<UsersEntity> GetRecorderUsers()
        {

            try
            {
                List<UsersEntity> UsersEntityList = new List<UsersEntity>();
                DataTable dt = new DataTable();
                //string query = "select *from user_profile where Serial!=1";
                string query = "select * from user_profile";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    UsersEntity UsersEntityObj = new UsersEntity();
                    UsersEntityObj.UserId = Convert.ToInt32(row["Serial"].ToString());
                    UsersEntityObj.UserName = row["User_Name"].ToString();
                    UsersEntityList.Add(UsersEntityObj);
                }
                con.Close();
                return UsersEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<UsersEntity> GetAllUsers()
        {
            try
            {
                List<UsersEntity> UsersEntityList = new List<UsersEntity>();
                DataTable dt = new DataTable();
                string query = "select *from user_profile where Serial!=1";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    UsersEntity UsersEntityObj = new UsersEntity();
                    UsersEntityObj.UserId = Convert.ToInt32(row["serial"].ToString());
                    UsersEntityObj.UserName = row["UserName"].ToString();
                    UsersEntityObj.Password = row["Password"].ToString();
                    UsersEntityObj.DisplayName = row["DisplayName"].ToString();
                    UsersEntityObj.EmailAddress = row["EmailAddress"].ToString();
                    UsersEntityList.Add(UsersEntityObj);
                }
                con.Close();
                return UsersEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public List<Modules> GetAllModules()
        {
            try
            {
                List<Modules> moduleEntityList = new List<Modules>();
                DataTable dt = new DataTable();
                string query = "select *from app_module order by MenuOrder asc";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    Modules moduleEntityObj = new Modules();
                    moduleEntityObj.moduleID = Convert.ToInt32(row["moduleID"].ToString());
                    moduleEntityObj.Name = row["Name"].ToString();
                    moduleEntityList.Add(moduleEntityObj);
                }
                con.Close();
                return moduleEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public DataTable GetUserOnId(int UserId)
        {
            try
            {
                UsersEntity UsersEntityObj = new UsersEntity();
                DataTable dt = new DataTable();
                string query = "select * from user_profile where serial=" + UserId + "";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                //foreach (DataRow row in dt.Rows)
                //{
                //    UsersEntityObj.UserId = Convert.ToInt32(row["serial"].ToString());
                //    UsersEntityObj.username = row["username"].ToString();
                //    UsersEntityObj.password = row["password"].ToString();
                //    UsersEntityObj.lastName = row["lastName"].ToString();
                //    UsersEntityObj.firstName = row["firstName"].ToString();
                    
                //}
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public DataTable GetUserModuleOnId(int UserId)
        {
            try
            {
                UsersEntity UsersEntityObj = new UsersEntity();
                DataTable dt = new DataTable();
                string query = "select * from app_module_access where UserProfileID=" + UserId + "";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public bool InsertIntoUsers(UsersEntity UsersEntityObj)
        {
            try
            {
                string query = "Insert into user_profile (UserName,Password,DisplayName,EmailAddress,Enable) values(@UserName,@Password,@DisplayName,@EmailAddress,@Enable);  SELECT LAST_INSERT_ID();";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserName", UsersEntityObj.UserName == null ? (object)DBNull.Value : UsersEntityObj.UserName.Trim());
                cmd.Parameters.AddWithValue("@Password", UsersEntityObj.Password == null ? (object)DBNull.Value : UsersEntityObj.Password.Trim());
                cmd.Parameters.AddWithValue("@DisplayName", UsersEntityObj.DisplayName == null ? (object)DBNull.Value : UsersEntityObj.DisplayName.Trim());
                cmd.Parameters.AddWithValue("@EmailAddress", UsersEntityObj.EmailAddress == null ? (object)DBNull.Value : UsersEntityObj.EmailAddress.Trim());
                cmd.Parameters.AddWithValue("@Enable", UsersEntityObj.Enable);
                con.Open();
                int insertedID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                con.Close();
                foreach (int module in UsersEntityObj.Permissions)
                {
                    string query2 = "Insert into app_module_access (UserProfileID,AppModuleID) values(@UserProfileID,@AppModuleID)";
                    MySqlCommand cmd2 = new MySqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("@UserProfileID", insertedID);
                    cmd2.Parameters.AddWithValue("@AppModuleID", module);
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return false;

            }
            //finally
            //{
            //    if (con != null)
            //        ((IDisposable)con).Dispose();
            //}
        }
        public bool UpdateIntoUsers(UsersEntity UsersEntityObj)
        {
            try
            {
                string query = "Update user_profile SET UserName=@UserName,Password=@Password,DisplayName=@DisplayName,EmailAddress=@EmailAddress,Enable=@Enable where serial=@UserId";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", UsersEntityObj.UserId);
                cmd.Parameters.AddWithValue("@UserName", UsersEntityObj.UserName == null ? (object)DBNull.Value : UsersEntityObj.UserName.Trim());
                cmd.Parameters.AddWithValue("@Password", UsersEntityObj.Password == null ? (object)DBNull.Value : UsersEntityObj.Password.Trim());
                cmd.Parameters.AddWithValue("@DisplayName", UsersEntityObj.DisplayName == null ? (object)DBNull.Value : UsersEntityObj.DisplayName.Trim());
                cmd.Parameters.AddWithValue("@EmailAddress", UsersEntityObj.EmailAddress == null ? (object)DBNull.Value : UsersEntityObj.EmailAddress.Trim());
                cmd.Parameters.AddWithValue("@Enable",UsersEntityObj.Enable);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                foreach (int module in UsersEntityObj.Permissions)
                {
                    string query2 = "Insert into app_module_access (UserProfileID,AppModuleID) values(@UserProfileID,@AppModuleID)";
                    MySqlCommand cmd2 = new MySqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("@UserProfileID", UsersEntityObj.UserId);
                    cmd2.Parameters.AddWithValue("@AppModuleID", module);
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }


                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return false;

            }
            //finally
            //{
            //    if (con != null)
            //        ((IDisposable)con).Dispose();
            //}
        }
        public void UpdatePasswordOnUserId(UsersEntity UsersEntityObj, string newPassword)
        {
            try
            {
                string query = "Update user_profile SET password=@password where serial=@UserId";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", UsersEntityObj.UserId);
                cmd.Parameters.AddWithValue("@password", newPassword == null ? (object)DBNull.Value : newPassword.Trim());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
            }

        }
        public bool DeleteFromUsers(int UserId)
        {
            try
            {
                string query = "Delete From user_profile where serial=" + UserId + "";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return false;
            }
            //finally
            //{
            //    if (con != null)
            //        ((IDisposable)con).Dispose();
            //}
        }

        public bool DeleteFromAppModule(int UserId)
        {
            try
            {
                string query = "Delete From app_module_access where UserProfileID=" + UserId + "";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return false;
            }
        }

        public UsersEntity getClientAdmin()
        {
            try
            {

                UsersEntity UsersEntityObj = new UsersEntity();
                List<int> PermissionsList = new List<int>();
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                string query = "select * from user_profile where serial=1";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    UsersEntityObj.UserId = Convert.ToInt32(row["serial"].ToString());
                    UsersEntityObj.UserName = row["UserName"].ToString();
                    UsersEntityObj.Password = row["Password"].ToString();
                    UsersEntityObj.DisplayName = row["DisplayName"].ToString();
                    UsersEntityObj.EmailAddress = row["EmailAddress"].ToString();
                }
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    query = "select* from app_module_access where UserProfileID = " + UsersEntityObj.UserId + "";
                    con.Open();
                    da = new MySqlDataAdapter(query, con);
                    da.Fill(dt2);
                    con.Close();
                    foreach (DataRow row in dt2.Rows)
                    {
                        PermissionsList.Add(Convert.ToInt32(row["AppModuleID"].ToString()));
                    }
                    UsersEntityObj.Permissions = PermissionsList;
                    return UsersEntityObj;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return null;
            }
        }
        public string GetDbToConnect(string Client,string vCloudDB)
        {
            try
            {
                MySqlConnection SqlCon = new MySqlConnection(ConfigurationManager.ConnectionStrings["vCloudConnection"].ConnectionString);
                string AccountId = "";
                MySqlCommand cmd = new MySqlCommand("Select AccountId from "+vCloudDB +".accounts where  UserFriendlyId='" + Client + "' and IsDeleted=false and IsEnabled=true", SqlCon);
                SqlCon.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AccountId = reader.GetValue(0).ToString();
                }
                reader.Close();
                SqlCon.Close();
                return AccountId;
            }
            catch (Exception ex)
            {    
                Log4Net.WriteException(ex);
                return null;
            }
        }
        public string GetDbToConnectOnPremise(string vCloudDB)
        {
            try
            {
                MySqlConnection SqlCon = new MySqlConnection(ConfigurationManager.ConnectionStrings["vCloudConnection"].ConnectionString);
                string AccountId = "";
                MySqlCommand cmd = new MySqlCommand("Select AccountId from "+vCloudDB+".accounts where IsDeleted = false and IsEnabled = true Limit 1", SqlCon);
                SqlCon.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AccountId = reader.GetValue(0).ToString();
                }
                reader.Close();
                SqlCon.Close();
                return AccountId;
                

            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
        }
        public string GetUFID(string DastabaseId, string vCloudDB)
        {
            try
            {
                string UserFriendlyId = "";
                MySqlConnection SqlCon = new MySqlConnection(ConfigurationManager.ConnectionStrings["vCloudConnection"].ConnectionString);
                MySqlCommand cmd = new MySqlCommand("Select UserFriendlyId from "+vCloudDB+".accounts where AccountId=" + DastabaseId + "", SqlCon);
                SqlCon.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserFriendlyId = reader.GetValue(0).ToString();
                }
                reader.Close();
                SqlCon.Close();
                return UserFriendlyId;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
        }
        public UsersEntity getAdminUserOnUserNameAndPassword(string UserName, string Password)
        {
            try
            {
                UsersEntity UsersEntityObj = new UsersEntity();
                List<int> PermissionsList = new List<int>();
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                //UserName = Regex.Replace(UserName, @"[^0-9a-zA-Z].,+", ",");
                string query = "select * from user_profile where Username = @Username and Password = @Password";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
              
                foreach (DataRow row in dt.Rows)
                {
                    UsersEntityObj.UserId = Convert.ToInt32(row["serial"].ToString());
                    UsersEntityObj.UserName = row["UserName"].ToString();
                    UsersEntityObj.Password = row["Password"].ToString();
                    UsersEntityObj.DisplayName = row["DisplayName"].ToString();
                }
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    query = "select * from app_module_access where UserProfileID = " + UsersEntityObj.UserId + "";
                    con.Open();
                    da = new MySqlDataAdapter(query, con);
                    da.Fill(dt2);
                    con.Close();
                    foreach (DataRow row in dt2.Rows)
                    {
                        PermissionsList.Add(Convert.ToInt32(row["AppModuleID"].ToString()));
                    }
                    UsersEntityObj.Permissions = PermissionsList;
                    return UsersEntityObj;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return null;
            }
        }
        public UsersEntity getUesrOnUserNameAndPassword(string UserName, string Password)
        {
            try
            {
               
                UsersEntity UsersEntityObj = new UsersEntity();
                DataTable dt = new DataTable();
                string query = "select * from user_profile where username = '" + UserName + "' and password = '" + Password + "'";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    UsersEntityObj.UserId = Convert.ToInt32(row["serial"].ToString());
                    UsersEntityObj.UserName = row["UserName"].ToString();
                    UsersEntityObj.Password = row["Password"].ToString();
                    UsersEntityObj.DisplayName = row["DisplayName"].ToString();
                    UsersEntityObj.EmailAddress = row["EmailAddress"].ToString();
                }
                con.Close();
                if (dt.Rows.Count > 0)
                    return UsersEntityObj;
                else
                    return null;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
            
           
        }
        public bool CheckIfUserNameAlreadyExists(int userid, string username)
        {
            try
            {
                
                UsersEntity UsersEntityObj = new UsersEntity();
                DataTable dt = new DataTable();
                string query = "select * from user_profile where serial != " + userid + " And username='" + username + "'";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return true;

            }
        }
        //public void UpdateUserSetAuthToken(UsersEntity UsersEntityObj)
        //{
        //    try
        //    {
        //        string query = "Update user_profile SET AuthToken=@AuthToken,AuthDateTime=@AuthDateTime where serial=@UserId";
        //        MySqlCommand cmd = new MySqlCommand(query, con);
        //        cmd.Parameters.AddWithValue("@UserId", UsersEntityObj.UserId);
        //        cmd.Parameters.AddWithValue("@AuthToken", UsersEntityObj.AuthToken == null ? (object)DBNull.Value : UsersEntityObj.AuthToken.Trim());
        //        cmd.Parameters.AddWithValue("@AuthDateTime", UsersEntityObj.AuthDateTime == null ? (object)DBNull.Value : UsersEntityObj.AuthDateTime);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log4Net.WriteException(ex);
        //    }
        //}
        //public void UpdateUserUnSetAuthToken(UsersEntity UsersEntityObj)
        //{
        //    try
        //    {
        //        string query = "Update user_profile SET AuthToken=@AuthToken,AuthDateTime=@AuthDateTimewhere serial=@UserId";
        //        MySqlCommand cmd = new MySqlCommand(query, con);
        //        cmd.Parameters.AddWithValue("@UserId", UsersEntityObj.UserId);
        //        cmd.Parameters.AddWithValue("@AuthToken", UsersEntityObj.AuthToken == null ? (object)DBNull.Value : UsersEntityObj.AuthToken.Trim());
        //        cmd.Parameters.AddWithValue("@AuthDateTime", UsersEntityObj.AuthDateTime == null ? (object)DBNull.Value : UsersEntityObj.AuthDateTime);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log4Net.WriteException(ex);
        //    }
        //}
        //public UsersEntity getUesrOnUserNameAndAuthToken(string UserName, string AuthToken)
        //{
        //    try
        //    {

        //        UsersEntity UsersEntityObj = new UsersEntity();
        //        List<int> PermissionsList = new List<int>();
        //        DataTable dt = new DataTable();
        //        DataTable dt2 = new DataTable();
        //        DateTime t = DateTime.Now.AddMinutes(-5);
        //        string query = "select * from user_profile where username = '" + UserName + "' and AuthToken = '" + AuthToken + "'";// and AuthDateTime > '" + t.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
        //        /*LogApp.Log4Net.WriteLog("Get Client Query : " + query,LogType.GENERALLOG);*/
        //        con.Open();
        //        MySqlDataAdapter da = new MySqlDataAdapter(query, con);
        //        da.Fill(dt);
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            UsersEntityObj.UserId = Convert.ToInt32(row["serial"].ToString());
        //            UsersEntityObj.username = row["username"].ToString();
        //            UsersEntityObj.password = row["password"].ToString();
        //            UsersEntityObj.lastName = row["lastName"].ToString();
        //            UsersEntityObj.firstName = row["firstName"].ToString();
        //        }
        //        con.Close();
        //        if (dt.Rows.Count > 0)
        //        {
        //            query = "select* from app_module_access where UserProfileID = " + UsersEntityObj.UserId + "";
        //            con.Open();
        //            da = new MySqlDataAdapter(query, con);
        //            da.Fill(dt2);
        //            con.Close();
        //            foreach (DataRow row in dt2.Rows)
        //            {
        //                PermissionsList.Add(Convert.ToInt32(row["AppModuleID"].ToString()));
        //            }
        //            UsersEntityObj.Permissions = PermissionsList;
        //            return UsersEntityObj;
        //        }
        //        else
        //            return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        con.Close();
        //        LogApp.Log4Net.WriteException(ex);
        //        return null;

        //    }
            
        //}

    }
}