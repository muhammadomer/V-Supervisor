using LogApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using vSuperMTClient.DALs;
using vSuperMTClient.Entities;

namespace vSuperMTClient
{
    public partial class Settings : System.Web.UI.Page
    {
        public string ClientDB = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null && Session["vSupervisorDB"] != null)
            {
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
                    //else if (userProfileobj.Permissions.Find(x => x == 5) != 5)
                    //{
                    //    Response.Redirect("Login.aspx");
                    //}
                }
                if (Session["SuperAdmin"] == null && Session["OnPremise"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                if (Session["SuperAdmin"] != null)
                {
                    try
                    {
                        ClientDB = Session["vSupervisorDB"].ToString();


                    }
                    catch (Exception ex)
                    {
                        Log4Net.WriteException(ex);
                        return;
                    }
                }
                else if (Session["OnPremise"] != null)
                {
                    try
                    {
                        ClientDB = Session["vSupervisorDB"].ToString();


                    }
                    catch (Exception ex)
                    {
                        Log4Net.WriteException(ex);
                        return;
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        [WebMethod]
        [ScriptMethod]
        public static SettingsEntity GetSettings()
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    SettingsDAL SettingssDALObj = new SettingsDAL(ClientDB);
                    SettingsEntity SettingsList = SettingssDALObj.GetSettingsFromvBoard();
                    string PBXIP = SettingsList.PBXIP;
                    ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    SettingssDALObj = new SettingsDAL(ClientDB);
                    SettingsList = SettingssDALObj.GetSettings();
                    SettingsList.PBXIP = PBXIP;
                    return SettingsList;
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

        [WebMethod]
        [ScriptMethod]
        public static string SaveSMTPSettings(string SMTPHost, string SMTPPort, string SMTPUserName, string SMTPPassword, bool EnableSSL)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    SettingsDAL SettingsDALObj = new SettingsDAL(ClientDB);
                    SettingsEntity SettingsEntityObj = new SettingsEntity();
                    SettingsEntityObj.SMTPHost = SMTPHost;
                    SettingsEntityObj.SMTPPort = SMTPPort;

                    SettingsEntityObj.SMTPUserName = SMTPUserName;
                    SettingsEntityObj.SMTPPassword = SMTPPassword;
                    SettingsEntityObj.EnableSSL = EnableSSL;

                    bool result = SettingsDALObj.UpdateSMTPSettings(SettingsEntityObj);
                    if (result)
                        return "1";
                    else
                        return "0";

                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "0";

                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "0";
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static string TestSMTPSettings(string TestEmail, string SMTPHost, string SMTPPort, string SMTPUserName, string SMTPPassword, bool EnableSSL)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null)
            {
                try
                {
                    MailMessage message = new MailMessage();
                    message.To.Add(new MailAddress(TestEmail));
                    message.From = new MailAddress(SMTPUserName);
                    message.Subject = "vSupervisor - SMTP Test Message";
                    message.Body = "This is an email message sent by vSupervisor application while testing the SMTP settings.";
                    message.IsBodyHtml = true;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11;
                    SmtpClient client = new SmtpClient();
                    client.Host = SMTPHost;
                    client.Port = Convert.ToInt32(SMTPPort);
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(SMTPUserName, SMTPPassword);

                    //client.EnableSsl = false;
                    client.EnableSsl = EnableSSL;
                    client.Send(message);

                    return "1";
                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "0";

                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "0";
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static string UpdatePrefrences(SettingsEntity SettingsUpdatedInfo)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    SettingsDAL SettingssDALObj = new SettingsDAL(ClientDB);
                    SettingssDALObj.UpdatePBXIPInvBorad(SettingsUpdatedInfo);

                    ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    SettingssDALObj = new SettingsDAL(ClientDB);

                    SettingssDALObj.UpdatePrefrences(SettingsUpdatedInfo);
                    return "1";
                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "0";
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "0";
            }
        }



        [WebMethod]
        [ScriptMethod]
        public static string UpdateReportOptions(SettingsEntity SettingsUpdatedInfo)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    SettingsDAL SettingssDALObj = new SettingsDAL(ClientDB);
                    SettingssDALObj.UpdatePBXIPInvBorad(SettingsUpdatedInfo);

                    ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    SettingssDALObj = new SettingsDAL(ClientDB);

                    SettingssDALObj.UpdateReportOptions(SettingsUpdatedInfo);
                    return "1";
                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "0";
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "0";
            }
        }






        [WebMethod]
        [ScriptMethod]
        public static string UpdateRecordingRates(SettingsEntity SettingsUpdatedInfo)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    SettingsDAL SettingssDALObj = new SettingsDAL(ClientDB);
                    SettingssDALObj.UpdateRecordingRates(SettingsUpdatedInfo);
                    return "1";


                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "0";
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "0";
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static string SaveCompanyInfoSettings(string CompanyName, string CompanyAddress, string CompanyLogo)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    SettingsDAL SettingsDALObj = new SettingsDAL(ClientDB);
                    SettingsEntity SettingsEntityObj = new SettingsEntity();
                    SettingsEntityObj.CompanyName = CompanyName;
                    SettingsEntityObj.CompanyAddress = CompanyAddress;
                    SettingsEntityObj.CompanyLogo = CompanyLogo;

                    bool result = SettingsDALObj.UpdateCompanyInfoSettings(SettingsEntityObj);
                    if (result)
                        return "1";
                    else
                        return "0";

                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "0";

                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "0";
            }
        }
    }
}