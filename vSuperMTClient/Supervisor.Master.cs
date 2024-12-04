using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogApp;
using vSuperMTClient.Entities;
using vSuperMTClient.DALs;
using Secure;
using System.Configuration;

namespace vSuperMTClient.Supervisor
{
    public partial class Supervisor : System.Web.UI.MasterPage
    {
        public  string ClientDB = "";
        public List<Modules> ListModuleObj = new List<Modules>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null && Session["vSupervisorDB"] != null)
            {
                try
                {
                   

                    ClientDB = Session["vSupervisorDB"].ToString();
                    UserDAL UserDALObj = new UserDAL(ClientDB);
                    ListModuleObj = UserDALObj.GetAllModules();
                    UsersEntity UsersEntityObj = (UsersEntity)Session["User"];



                    string vCloudDB = System.Configuration.ConfigurationManager.AppSettings["vCloudDB"];
                    ExtensionsDAL objExtension = new ExtensionsDAL(ClientDB);
                    if (!IsPostBack)
                    {
                        Session["NumberingPlan"] = objExtension.GetNumberingPlanList(ClientDB, vCloudDB);
                    }


                    if (Session["OnPremise"] != null)
                    {
                        //imgLogo.Src = "Content/images/Logo2.png";
                        string StringVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.ToString();
                        //VersionInfo.InnerText = "v" + StringVersion.Substring(0, StringVersion.LastIndexOf("."));
                        
                        string UserId = Session["UFID"].ToString();
                        UserName.InnerText = UsersEntityObj.UserName;

                    }
                    else if (Session["SuperAdmin"] != null)
                    {
                        string StringVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.ToString();
                        //VersionInfo.InnerText = "v" + StringVersion.Substring(0, StringVersion.LastIndexOf("."));
                        
                        string UserId = Session["UFID"].ToString();
                        UserName.InnerText = "Cloud Admin(" + UserId + ")";
                    }

                    else
                    {
                        string StringVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.ToString();
                        //VersionInfo.InnerText = "v" + StringVersion.Substring(0, StringVersion.LastIndexOf("."));

                       
                        string UserId = Session["UFID"].ToString();
                        UserName.InnerText = UsersEntityObj.UserName + "(" + UserId + ")";
                    }
                    int EnableSettings = 0;
                    int EnableAgents = 0;
                    int EnableLicensing = 0;
                    foreach (int i in UsersEntityObj.Permissions)
                    {
                        if (i == 1)
                        {
                            EnableSettings = 1;
                        }
                        else if(i == 2)
                        {
                            EnableAgents = 1;
                        }
                        else if (i == 3)
                        {
                            EnableLicensing = 1;
                        }
                    }

                    if (EnableSettings == 0)
                    {
                       
                        //HeaderLinkSettings.Disabled = true;
                        MenuSettings.Visible = false;
                        //MenuSettings.Style.Add("visibility", "hidden");


                    }
                    if (EnableAgents == 0)
                    {
                        MenuAgents.Visible = false;
                        //MenuAgents.Style.Add("visibility", "hidden");
                    }
                    if (EnableLicensing == 0)
                    {
                        MenuLicensing.Visible = false;
                        //MenuLicensing.Style.Add("visibility", "hidden");
                    }

                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }
     
        public void ErrorMessage(string ErrorText)
        {

        }
       
      
     

     

    }
}