using LogApp;
using System;
using System.Collections.Generic;
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
    public partial class AreaCodes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod]
        public static List<AreaCodesEntity> GetAreaCodes()
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    AreaCodesDAL AreaCodesDALObj = new AreaCodesDAL(vBoardClientDB);
                    List<AreaCodesEntity> AreaCodesList = AreaCodesDALObj.GetAreaCodes();
                    return AreaCodesList;
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
        public static string AddAreaCode(AreaCodesEntity AreaCodesEntityObj)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    string AccountId = vBoardClientDB.Split('_')[1];
                    AreaCodesDAL AreaCodesDALObj = new AreaCodesDAL(vBoardClientDB);
                    if (!AreaCodesDALObj.CheckIfAreaCodeAlreadyExists(AreaCodesEntityObj.Id, AreaCodesEntityObj.AreaCode))
                    {
                        try
                        {
                            string AreaCodeAdded = AreaCodesDALObj.AddAreaCode(AreaCodesEntityObj);
                            if (AreaCodeAdded.StartsWith("Failed"))
                            {
                                return "2_An error occured while adding the AreaCode.";
                            }
                            else
                            {
                                return "1_" + AreaCodeAdded + "_" + AccountId + "";
                            }
                        }
                        catch (Exception ex)
                        {
                            Log4Net.WriteException(ex);
                            return "2_An error occured while adding the AreaCode.";
                        }
                    }
                    else
                    {
                        return "3_AreaCode with same name already exists";
                    }



                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "2:An error occured while adding the AreaCode.";
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
        public static string UpdateAreaCode(AreaCodesEntity AreaCodesEntityObj)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    string AccountId = vBoardClientDB.Split('_')[1];
                    AreaCodesDAL AreaCodesDALObj = new AreaCodesDAL(vBoardClientDB);
                    if (!AreaCodesDALObj.CheckIfAreaCodeAlreadyExists(AreaCodesEntityObj.Id, AreaCodesEntityObj.AreaCode))
                    {
                        try
                        {
                            if (AreaCodesDALObj.UpdateAreaCodeOnId(AreaCodesEntityObj))
                            {
                                //return "1_AreaCode updated successfully.";
                                return "1_" + AreaCodesEntityObj.Id + "_" + AccountId + "";
                            }
                            else
                            {
                                return "2_An error occured while updating the AreaCode.";
                            }

                        }
                        catch (Exception ex)
                        {
                            Log4Net.WriteException(ex);
                            return "2_An error occured while updating the AreaCode.";
                        }
                    }
                    else
                    {
                        return "3_AreaCode with same name already exists";
                    }



                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "2:An error occured while updating the AreaCode.";
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
        public static string DeleteAreaCodeOnId(int AreaCodeId)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    string AccountId = vBoardClientDB.Split('_')[1];
                    AreaCodesDAL AreaCodesDALObj = new AreaCodesDAL(vBoardClientDB);

                    if (AreaCodesDALObj.DeleteAreaCodeOnId(AreaCodeId))
                    {
                        //return "1:AreaCode deleted successfully.";
                        return "1_" + AreaCodeId + "_" + AccountId + "";
                    }
                    else
                    {
                        return "2_An error occured while deleting the AreaCode.";
                    }
                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "2_An error occured while deleting the AreaCode.";
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
        public static AreaCodesEntity GetAreaCodeOnId(int AreaCodeId)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    AreaCodesDAL AreaCodesDALObj = new AreaCodesDAL(vBoardClientDB);
                    return AreaCodesDALObj.GetAreaCodeOnId(AreaCodeId);
                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return null;
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return null;
            }
        }
    }
}