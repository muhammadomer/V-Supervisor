using LogApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using vSuperMTClient.DALs;
using vSuperMTClient.Entities;
using vSuperMTClient.Utility;

namespace vSuperMTClient
{
    public partial class Users : System.Web.UI.Page
    {
        private  UserDAL UserDALObj;
     
        public List<Modules> ListModuleObj=new List<Modules>();
        public  string ClientDB = "";
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["User"] != null && Session["vSupervisorDB"] != null)
            {
                try
                {
                    ClientDB = Session["vSupervisorDB"].ToString();
                    if (!IsPostBack)
                    {
                        UsersEntity userProfileobj = (UsersEntity)Session["User"];
                        if (Session["SuperAdmin"] != null)
                        {
                        }
                        //else if (userProfileobj.userType != "Admin")
                        //{
                        //    Response.Redirect("Login.aspx");
                        //}
                        //else if (userProfileobj.Permissions.Find(x => x == 4) != 4)
                        //{
                        //    Response.Redirect("Login.aspx");
                        //}
                    }
                    UserDALObj = new UserDAL(ClientDB);
                    ListModuleObj = UserDALObj.GetAllModules();


                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static List<UsersEntity> GetAllUsers()
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    UserDAL UserDALObj = new UserDAL(ClientDB);
                    List<UsersEntity> UsersEntityList = new List<UsersEntity>();
                    UsersEntityList = UserDALObj.GetAllUsers();
                    return UsersEntityList;
                }
                catch (Exception ex)
                {
                    LogApp.Log4Net.WriteException(ex);
                    return null;
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return null;
            }
        }
        [WebMethod]
        [ScriptMethod]
        public static string  InsertIntoUsers(UsersEntity UsersEntityObj)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    UserDAL UserDALObj = new UserDAL(ClientDB);
                    if (UserDALObj.CheckIfUserNameAlreadyExists(0, UsersEntityObj.UserName))
                    {
                        return "User with same name already exists";
                    }
                    else
                    {
                        UserDALObj.InsertIntoUsers(UsersEntityObj);
                        return "success";
                    }

                }
                catch (Exception ex)
                {
                    LogApp.Log4Net.WriteException(ex); ;
                    return ex.ToString();
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "";
            }
        }
        [WebMethod]
        [ScriptMethod]
        public static string UpdateIntoUsers(UsersEntity UsersEntityObj)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    UserDAL UserDALObj = new UserDAL(ClientDB);
                    if (UserDALObj.CheckIfUserNameAlreadyExists(UsersEntityObj.UserId, UsersEntityObj.UserName))
                    {
                        return "User with same name already exists";
                    }
                    else
                    {
                        UserDALObj.DeleteFromAppModule(UsersEntityObj.UserId);
                        UserDALObj.UpdateIntoUsers(UsersEntityObj);
                        return "success";
                    }
                }
                catch (Exception ex)
                {
                    LogApp.Log4Net.WriteException(ex); ;
                    return ex.ToString();
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "";
            }
        }
        [WebMethod]
        [ScriptMethod]
        public static bool DeleteFromUsers(int UserId)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    UserDAL UserDALObj = new UserDAL(ClientDB);
                    UserDALObj.DeleteFromAppModule(UserId);
                    UserDALObj.DeleteFromUsers(UserId);
                    return true;
                }
                catch (Exception ex)
                {
                    LogApp.Log4Net.WriteException(ex); ;
                    return false;
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return false;
            }
        }
        
       [WebMethod]
        [ScriptMethod]
        public static UsersEntity GetUserOnId(int UserId)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    UserDAL UserDALObj = new UserDAL(ClientDB);
                    UsersEntity UsersEntityObj = new UsersEntity();
                    DataTable dt = UserDALObj.GetUserOnId(UserId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        UsersEntityObj.UserId = Convert.ToInt32(dt.Rows[0]["serial"].ToString());
                        UsersEntityObj.UserName = dt.Rows[0]["UserName"].ToString();
                        UsersEntityObj.Password = dt.Rows[0]["Password"].ToString();
                        UsersEntityObj.DisplayName = dt.Rows[0]["DisplayName"].ToString();
                        UsersEntityObj.EmailAddress = dt.Rows[0]["EmailAddress"].ToString();
                        UsersEntityObj.Enable = Convert.ToBoolean(dt.Rows[0]["Enable"]);
                        int userID = Convert.ToInt32(dt.Rows[0]["serial"]);
                        DataTable dts = UserDALObj.GetUserModuleOnId(userID);
                        if (dts != null && dts.Rows.Count > 0)
                        {
                            foreach (DataRow row in dts.Rows)
                            {
                                int moduleID = Convert.ToInt32(row["AppModuleID"]);
                                UsersEntityObj.Permissions.Add(moduleID);
                            }

                        }


                    }
                    return UsersEntityObj;
                }
                catch (Exception ex)
                {
                    LogApp.Log4Net.WriteException(ex);
                    return null;
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return null;
            }

        }


        [WebMethod]
        [ScriptMethod]
        public static string ChangePassword(string password, string oldPassword)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null)
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string changeStatus = "Sorry, Unable to change your password";
                try
                {
                    UsersEntity userLogedIn = (UsersEntity)HttpContext.Current.Session["User"];
                    //bool isSingleInstance = (bool)HttpContext.Current.Session["IsSingleInstance"];
                    int userId = userLogedIn.UserId;
                    bool isOldPasswordVerified = (oldPassword == userLogedIn.Password);
                    if (!isOldPasswordVerified)
                    {
                        return "Invalid old password";
                    }
                    //bool isPasswordSame = Cryptography.VerifyHash(password, userLogedIn.password);
                    //if (isPasswordSame)
                    //{
                    //    return "New Password should not be same as old password";
                    //}

                    string userID = (string)HttpContext.Current.Session["UserID"];
                    UserDAL newUserDalObj = new UserDAL(ClientDB);
                    newUserDalObj.UpdatePasswordOnUserId(userLogedIn, password);
                    changeStatus = "Password Changed Sucessfully";
                    //ActivityLogger.LogActivity("Changed Password");

                    return changeStatus;
                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return changeStatus;
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "";
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static string LogOut()
        {
            HttpContext.Current.Session.Abandon();
            return "Login.aspx";
        }


    }
}