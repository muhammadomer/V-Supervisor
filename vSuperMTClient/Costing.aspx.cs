using LogApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using vSuperMTClient.DALs;
using vSuperMTClient.Entities;

namespace vSuperMTClient
{
   
    public partial class Costing : System.Web.UI.Page
    {
        
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
                        //else if (userProfileobj.userType != "Supervisor")
                        //{
                        //    Response.Redirect("Login.aspx");
                        //}
                        //else if (userProfileobj.Permissions.Find(x => x == 9) != 9)
                        //{
                        //    Response.Redirect("Login.aspx");
                        //}
                    }
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
        public static List<CostingEntity> GetAllCostings()
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();

                    CostingDAL CostingDALObj = new CostingDAL(vBoardClientDB);
                    List<CostingEntity> CostingEntityList = new List<CostingEntity>();
                    CostingEntityList = CostingDALObj.GetAllCostings();
                    return CostingEntityList;
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
        public static string InsertIntoCosting(CostingEntity CostingEntityObj)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    CostingDAL CostingDALObj = new CostingDAL(vBoardClientDB);
                    CostingDALObj.InsertIntoCosting(CostingEntityObj);
                    return "1";
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
                return "";
            }
        }


        [WebMethod]
        [ScriptMethod]
        public static bool UpdateIntoCosting(CostingEntity CostingEntityObj)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    CostingDAL CostingDALObj = new CostingDAL(vBoardClientDB);
                    CostingDALObj.UpdateIntoCosting(CostingEntityObj);
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
        public static string DeleteFromCostings(int CostingId)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    CostingDAL CostingDALObj = new CostingDAL(vBoardClientDB);
                    CostingDALObj.DeleteFromCosting(CostingId);
                    return "1";
                }
                catch (Exception ex)
                {
                    LogApp.Log4Net.WriteException(ex); ;
                    return "0";
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
        public static CostingEntity GetCostingOnId(int CostingId)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    CostingDAL CostingDALObj = new CostingDAL(vBoardClientDB);
                    CostingEntity CostingEntityObj = new CostingEntity();
                    CostingEntityObj = CostingDALObj.GetCostingOnId(CostingId);

                    return CostingEntityObj;
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
        public static bool CheckIfCostingNameAlreadyExists(int CostingId, string CostType)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    CostingDAL CostingDALObj = new CostingDAL(vBoardClientDB);
                    return CostingDALObj.CheckIfCostingNameAlreadyExists(CostingId, CostType);
                }
                catch (Exception ex)
                {
                    LogApp.Log4Net.WriteException(ex);
                    return true;
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return true;
            }
        }

    }
}