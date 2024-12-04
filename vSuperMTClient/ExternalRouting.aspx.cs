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
    public partial class ExternalRouting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod]
        public static List<ExternalRoutingEntity> GetExternalRouting()
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ExternalRoutingDAL ExternalRoutingDALObj = new ExternalRoutingDAL(vBoardClientDB);
                    List<ExternalRoutingEntity> ExternalRoutingList = ExternalRoutingDALObj.GetExternalRouting();
                    return ExternalRoutingList;
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
        public static string AddExternalRouting(ExternalRoutingEntity ExternalRoutingEntityObj)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ExternalRoutingDAL ExternalRoutingDALObj = new ExternalRoutingDAL(vBoardClientDB);
                    if (!ExternalRoutingDALObj.CheckIfExternalRoutingAlreadyExists(ExternalRoutingEntityObj.Id, ExternalRoutingEntityObj.Name))
                    {
                        try
                        {
                            if (ExternalRoutingDALObj.AddExternalRouting(ExternalRoutingEntityObj))
                            {
                                return "1:External Routing added successfully.";
                            }
                            else
                            {
                                return "2:An error occured while adding the External Routing.";
                            }
                        }
                        catch (Exception ex)
                        {
                            Log4Net.WriteException(ex);
                            return "2:An error occured while adding the External Routing.";
                        }
                    }
                    else
                    {
                        return "3:External Routing with same name already exists";
                    }



                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "2:An error occured while adding the extension.";
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
        public static string UpdateExternalRouting(ExternalRoutingEntity ExternalRoutingEntityObj)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ExternalRoutingDAL ExternalRoutingDALObj = new ExternalRoutingDAL(vBoardClientDB);
                    if (!ExternalRoutingDALObj.CheckIfExternalRoutingAlreadyExists(ExternalRoutingEntityObj.Id, ExternalRoutingEntityObj.Name))
                    {
                        try
                        {
                            if (ExternalRoutingDALObj.UpdateExternalRoutingOnId(ExternalRoutingEntityObj))
                            {
                                return "1:External Routing updated successfully.";
                            }
                            else
                            {
                                return "2:An error occured while updating the External Routing.";
                            }

                        }
                        catch (Exception ex)
                        {
                            Log4Net.WriteException(ex);
                            return "2:An error occured while updating the External Routing.";
                        }
                    }
                    else
                    {
                        return "3:External Routing with same name already exists";
                    }

                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "2:An error occured while updating the External Routing.";
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
        public static string DeleteExternalRoutingOnId(int ExternalRoutingId)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ExternalRoutingDAL ExternalRoutingDALObj = new ExternalRoutingDAL(vBoardClientDB);

                    if (ExternalRoutingDALObj.DeleteExternalRoutingOnId(ExternalRoutingId))
                    {
                        return "1:External Routing deleted successfully.";
                    }
                    else
                    {
                        return "2:An error occured while deleting the External Routing.";
                    }
                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "2:An error occured while deleting the External Routing.";
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "";
            }
        }

    }
}