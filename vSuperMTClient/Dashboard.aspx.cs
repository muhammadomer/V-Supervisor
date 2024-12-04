using iTextSharp.text;
using iTextSharp.text.pdf;
using LogApp;
using Secure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using vSuperMTClient.DALs;
using vSuperMTClient.Entities;

namespace vSuperMTClient
{
    public partial class Dashboard : System.Web.UI.Page
    {
        public static int vBoardAppInstalled = 0;
        public static int vRecordAppInstalled = 0;
        public static int LoggingReportsLicense=0;
        public static int ACDReportsLicense=0;

        public string IsOXO = "1";

        protected List<ExtensionsEntity> ExtensionsEntityList = new List<ExtensionsEntity>();
        protected List<BoardsEntity> BoardsEntityList = new List<BoardsEntity>();
        protected List<AgentStatsEntity> AgentStatsEntityList = new List<AgentStatsEntity>();
        protected List<UsersEntity> UsersEntityList = new List<UsersEntity>();
        protected List<ExternalRoutingEntity> ExternalRoutingEntityList = new List<ExternalRoutingEntity>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string AccountId = ClientDB.Split('_')[1];

                    string path = HttpContext.Current.Server.MapPath("Reports\\" + AccountId + "\\");
                    //Check if directory exist
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                    }

                    string vBoardDB = ConfigurationManager.AppSettings["vBoardDB"]+"_"+ AccountId;
                    string vRecordDB= ConfigurationManager.AppSettings["vRecordDB"] + "_" + AccountId;

                    string vSupervisorDB = ConfigurationManager.AppSettings["vSupervisorDB"];

                    if (!IsPostBack)
                    {
                        string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();

                        ExtensionsDAL ExtensionsDALObj = new ExtensionsDAL(vBoardClientDB);
                        ExtensionsEntityList = ExtensionsDALObj.GetExtensionsWithCallslogging();

                        BoardsDAL BoardsDALObj = new BoardsDAL(vBoardClientDB);
                        BoardsEntityList = BoardsDALObj.GetAllHuntGroups();

                        AgentStatsDAL AgentStatsDALObj = new AgentStatsDAL(vBoardClientDB);
                        AgentStatsEntityList = AgentStatsDALObj.GetDistinctAgentsNames();

                        ExternalRoutingDAL ExternalRoutingDALObj = new ExternalRoutingDAL(vBoardClientDB);
                        ExternalRoutingEntityList = ExternalRoutingDALObj.GetExternalRouting();

                        try
                        {
                            string vRecordClientDB = HttpContext.Current.Session["vRecordDB"].ToString();
                            UserDAL UsersDALObj = new UserDAL(vRecordClientDB);
                            UsersEntityList = UsersDALObj.GetRecorderUsers();
                        }
                        catch (Exception ex)
                        {
                            LogApp.Log4Net.WriteException(ex);
                        }

                    }

                    string ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB);
                    Secure.LicInformation objLicInformation = new LicInformation(ConnectionString, ClientDB);

                    IsOXO = ConfigurationManager.AppSettings["IsOXO"];

                    LoggingReportsLicense = 1;
                    //Commented for Premia
                    if (objLicInformation.serverLicenseStatus(Secure.LicInformation.ServerLicense.vSUPERVISORCallLogging) == LicInformation.ServerStatus.Expire)
                    {
                        LoggingReportsLicense = 0;
                    }
                    else
                    {
                        LoggingReportsLicense = 1;
                    }
                    /////
                    string TData = Encrypt(AccountId + "$$$" + DateTime.Now, "CallFromvSuper");
                    vBoardServiceRef.vBoardMTClientInfo CallWebService = new vBoardServiceRef.vBoardMTClientInfo();
                    bool b = CallWebService.isAdvancedACDPackAvailable(TData);
                    if (!b)
                    {
                        ACDReportsLicense = 0;
                    }
                    else
                    {
                        ACDReportsLicense = 1;
                    }
                    
                }
                catch (Exception ex)
                {
                    LogApp.Log4Net.WriteException(ex);
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
            }
        }
       
        [WebMethod]
        [ScriptMethod]
        public static List<ReportTrafficSummaryByDayEntity> GetCallSummaryGraphReport(int DateRangeOption, int CallsOption)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    LogApp.Log4Net.WriteLog("Started: GetCallSummaryGraphReport", LogType.GENERALLOG);
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                    DateTime FromDate = DateTime.Now;
                    DateTime ToDate = DateTime.Now;
                    if (Convert.ToInt32(DateRangeOption) > 0)
                    {
                        FromDate = GetFromDate(Convert.ToInt32(DateRangeOption));
                        ToDate = GetToDate(Convert.ToInt32(DateRangeOption));
                    }

                    //FromDate = DateTime.Now.AddDays(-60);
                    List<ReportTrafficSummaryByDayEntity> ReportEntityList = new List<ReportTrafficSummaryByDayEntity>();
                    SettingsDAL SettingsDALObj = new SettingsDAL(ClientDB);
                    SettingsEntity SettingsEntityObj = SettingsDALObj.GetSettings();
                    int InternalLength = SettingsEntityObj.InternalLength;
                    ReportEntityList = ReportsDALObj.GetCallSummaryByDayGraphReport(FromDate, ToDate, InternalLength, CallsOption);

                    LogApp.Log4Net.WriteLog("Completed: GetCallSummaryGraphReport", LogType.GENERALLOG);
                    return ReportEntityList;
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
        public static List<ReportOutBoundCallsByRegion> GetOutboundCallsByRegionGraphReport(int DateRangeOption, int CallsOption)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    LogApp.Log4Net.WriteLog("Started: GetOutboundCallsByRegionGraphReport", LogType.GENERALLOG);
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                    DateTime FromDate = DateTime.Now;
                    DateTime ToDate = DateTime.Now;
                    if (Convert.ToInt32(DateRangeOption) > 0)
                    {
                        FromDate = GetFromDate(Convert.ToInt32(DateRangeOption));
                        ToDate = GetToDate(Convert.ToInt32(DateRangeOption));
                    }
                    //FromDate = DateTime.Now.AddDays(-60);
                    SettingsDAL SettingsDALObj = new SettingsDAL(ClientDB);
                    SettingsEntity SettingsEntityObj = SettingsDALObj.GetSettings();
                    int InternalLength = SettingsEntityObj.InternalLength;
                    List<ReportOutBoundCallsByRegion> ReportEntityList = new List<ReportOutBoundCallsByRegion>();
                    ReportEntityList = ReportsDALObj.GetOutboundCallsByRegionGraphReport(FromDate, ToDate, InternalLength, CallsOption);

                    LogApp.Log4Net.WriteLog("Completed: GetOutboundCallsByRegionGraphReport", LogType.GENERALLOG);
                    return ReportEntityList;
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
        public static List<ReportCallsServicedByDay> GetCallsServicedByDayGraphReport(int DateRangeOption, int CallsOption)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {

                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                    DateTime FromDate = DateTime.Now;
                    DateTime ToDate = DateTime.Now;
                    if (Convert.ToInt32(DateRangeOption) > 0)
                    {
                        FromDate = GetFromDate(Convert.ToInt32(DateRangeOption));
                        ToDate = GetToDate(Convert.ToInt32(DateRangeOption));
                    }

                    //FromDate = DateTime.Now.AddDays(-60);
                    SettingsDAL SettingsDALObj = new SettingsDAL(ClientDB);
                    SettingsEntity SettingsEntityObj = SettingsDALObj.GetSettings();
                    int InternalLength = SettingsEntityObj.InternalLength;
                    List<ReportCallsServicedByDay> ReportEntityList = new List<ReportCallsServicedByDay>();
                    ReportEntityList = ReportsDALObj.GetCallsServicedByDayGraphReport(FromDate, ToDate, InternalLength, CallsOption);

                    return ReportEntityList;
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
        public static List<ReportCostSummaryEntity> GetCostSummaryGraphReport(int DateRangeOption)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null && HttpContext.Current.Session["vRecordDB"] != null)
            {
                try
                {
                    LogApp.Log4Net.WriteLog("GetCostSummaryGraphReport: started", LogType.GENERALLOG);

                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    string vRecordClientDB = HttpContext.Current.Session["vRecordDB"].ToString();
                    string vCloudDB = System.Configuration.ConfigurationManager.AppSettings["vCloudDB"];
                    string FTPRepoisory = "FTPRepository";
                    string AccountID = ClientDB.Split('_')[1];
                    SettingsDAL SettingsDALObj = new SettingsDAL(ClientDB);
                    string RecordingPath = SettingsDALObj.GetRecordigPath(vCloudDB) + @"\" + AccountID + @"\" + FTPRepoisory;


                    SettingsEntity SettingsEntityObj = SettingsDALObj.GetSettings();
                    int InternalLength = SettingsEntityObj.InternalLength;
                    int RingTimeThreshold = SettingsEntityObj.RingTimeThreshold;
                    string Currency = SettingsEntityObj.Currency;
                    float DiskCostPerMin = SettingsEntityObj.DiskCostPerMin;
                    float DurationCostPerMin = SettingsEntityObj.DurationCostPerMin;

                    DateTime FromDate = DateTime.Now;
                    DateTime ToDate = DateTime.Now;
                    if (Convert.ToInt32(DateRangeOption) > 0)
                    {
                        FromDate = GetFromDate(Convert.ToInt32(DateRangeOption));
                        ToDate = GetToDate(Convert.ToInt32(DateRangeOption));
                    }

                    //FromDate = DateTime.Now.AddDays(-60);
                    List<ReportCostSummaryEntity> ReportEntityListVBoardRecords = new List<ReportCostSummaryEntity>();
                    ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                    ReportEntityListVBoardRecords = ReportsDALObj.GetCostSummaryGraphReport1(FromDate, ToDate, InternalLength);

                    List<ReportCostSummaryEntity> ReportEntityList = new List<ReportCostSummaryEntity>();
                    ReportsDALObj = new ReportsDAL(vRecordClientDB);
                    ReportEntityList = ReportsDALObj.GetCostSummaryGraphReport2(ReportEntityListVBoardRecords, FromDate, ToDate, DiskCostPerMin, RecordingPath, DurationCostPerMin);

                    bool flag = false;
                    foreach (ReportCostSummaryEntity obj in ReportEntityList)
                    {
                        if (obj.Cost > 0)
                        {
                            flag = true;
                        }
                    }
                    if (flag == true)
                    {
                        LogApp.Log4Net.WriteLog("GetCostSummaryGraphReport: completed", LogType.GENERALLOG);
                        return ReportEntityList;
                    }
                    else return new List<ReportCostSummaryEntity>();

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
        public static List<ReportInboundCallsByHourEntity> GetInboundCallsByHourGraphReport(int CallsOption)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {

                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                    DateTime FromDate = DateTime.Today;
                    //DateTime FromDate = DateTime.Today.AddDays(-60);
                    DateTime ToDate = DateTime.Now;

                    //
                    //FromDate = DateTime.Now.AddDays(-60);
                    SettingsDAL SettingsDALObj = new SettingsDAL(ClientDB);
                    SettingsEntity SettingsEntityObj = SettingsDALObj.GetSettings();
                    int InternalLength = SettingsEntityObj.InternalLength;
                    List<ReportInboundCallsByHourEntity> ReportEntityList = new List<ReportInboundCallsByHourEntity>();
                    ReportEntityList = ReportsDALObj.GetInboundCallsByHourGraphReport(FromDate, ToDate, InternalLength, CallsOption);

                    return ReportEntityList;
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
        public static List<ReportOutboundCallsByHourEntity> GetOutboundCallsByHourGraphReport(int CallsOption)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {

                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                    DateTime FromDate = DateTime.Today;
                    DateTime ToDate = DateTime.Now;

                    //
                    //FromDate = DateTime.Now.AddDays(-60);
                    SettingsDAL SettingsDALObj = new SettingsDAL(ClientDB);
                    SettingsEntity SettingsEntityObj = SettingsDALObj.GetSettings();
                    int InternalLength = SettingsEntityObj.InternalLength;
                    List<ReportOutboundCallsByHourEntity> ReportEntityList = new List<ReportOutboundCallsByHourEntity>();
                    ReportEntityList = ReportsDALObj.GetOutboundCallsByHourGraphReport(FromDate, ToDate, InternalLength, CallsOption);

                    return ReportEntityList;
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
        public static List<ReportCallsLostByHourEntity> GetCallsLostByHourGraphReport(int CallsOption)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {

                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                    DateTime FromDate = DateTime.Today;
                    DateTime ToDate = DateTime.Now;

                    //
                    //FromDate = DateTime.Now.AddDays(-60);
                    SettingsDAL SettingsDALObj = new SettingsDAL(ClientDB);
                    SettingsEntity SettingsEntityObj = SettingsDALObj.GetSettings();
                    int InternalLength = SettingsEntityObj.InternalLength;
                    List<ReportCallsLostByHourEntity> ReportEntityList = new List<ReportCallsLostByHourEntity>();
                    ReportEntityList = ReportsDALObj.GetCallsLostByHourGraphReport(FromDate, ToDate, InternalLength, CallsOption);

                    return ReportEntityList;
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
        public static List<ReportLongestRingTimeByHourEntity> GetLongestRingTimeByHourGraphReport(int CallsOption)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {

                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                    DateTime FromDate = DateTime.Today;
                    DateTime ToDate = DateTime.Now;


                    // FromDate = DateTime.Now.AddDays(-60);
                    SettingsDAL SettingsDALObj = new SettingsDAL(ClientDB);
                    SettingsEntity SettingsEntityObj = SettingsDALObj.GetSettings();
                    int InternalLength = SettingsEntityObj.InternalLength;
                    List<ReportLongestRingTimeByHourEntity> ReportEntityList = new List<ReportLongestRingTimeByHourEntity>();
                    ReportEntityList = ReportsDALObj.GetLongestRingTimeByHourGraphReport(FromDate, ToDate, InternalLength, CallsOption);

                    return ReportEntityList;
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
        public static List<ReportCallSummaryByExtensionEntity> GetCallSummaryByExtensionGraphReport(int DateRangeOption, int CallsOption)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {

                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                    

                    DateTime FromDate = DateTime.Now;
                    DateTime ToDate = DateTime.Now;
                    if (Convert.ToInt32(DateRangeOption) > 0)
                    {
                        FromDate = GetFromDate(Convert.ToInt32(DateRangeOption));
                        ToDate = GetToDate(Convert.ToInt32(DateRangeOption));
                    }
                    SettingsDAL SettingsDALObj = new SettingsDAL(ClientDB);
                    SettingsEntity SettingsEntityObj = SettingsDALObj.GetSettings();
                    int InternalLength = SettingsEntityObj.InternalLength;
                    List<ReportCallSummaryByExtensionEntity> ReportEntityList = new List<ReportCallSummaryByExtensionEntity>();
                    ReportEntityList = ReportsDALObj.GetCallSummaryByExtensionGraphReport(FromDate, ToDate, InternalLength, CallsOption);
                    //ReportEntityList.Add(new ReportCallSummaryByExtensionEntity()
                    //{
                    //    Extension = "Extension",
                    //    Name = "Hunt Group Customer Contact Center",
                    //    IncomingCalls = 2,
                    //    IncomingCallDuration = "02",
                    //    OutgoingCalls = 2,
                    //    OutgoingCallDuration = "2",
                    //    TotalCalls = 2,
                    //    TotalDuration = "2"
                    //});
                    //for (int i = 0; i < 100; i++)
                    //{
                    //    ReportEntityList.Add(new ReportCallSummaryByExtensionEntity()
                    //    {
                    //        Extension = "Extension",
                    //        Name = "Hunt",
                    //        IncomingCalls = 2,
                    //        IncomingCallDuration = "02",
                    //        OutgoingCalls = 2,
                    //        OutgoingCallDuration = "2",
                    //        TotalCalls = 2,
                    //        TotalDuration = "2"
                    //    });
                    //}
                    return ReportEntityList;
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


        public static string Encrypt(string SecretKey, string EncryptionKey)
        {
            try
            {
                //string EncryptionKey = "SuperAdmin";
                byte[] clearBytes = Encoding.Unicode.GetBytes(SecretKey);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        SecretKey = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return SecretKey;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
        }
        public static DateTime GetFromDate(int DateRangeOption)
        {
            DateTime FromDate = DateTime.Now;
            try
            {

                if (DateRangeOption == 1)
                {
                    FromDate = DateTime.Today;
                }
                else if (DateRangeOption == 2)
                {
                    FromDate = DateTime.Today.AddDays(-1).Date;
                }
                else if (DateRangeOption == 3)
                {

                    //FromDate = DateTime.Today.AddDays((int)DayOfWeek.Monday - (int)DateTime.Today.DayOfWeek);
                    FromDate = DateTime.Now.AddDays(-7).Date;
                }
                else if (DateRangeOption == 4)
                {
                    FromDate = DateTime.Now.AddDays(-14).Date;
                    

                }
                else if (DateRangeOption == 5)
                {
                    FromDate = DateTime.Now.AddDays(-30).Date;
                }
                else if (DateRangeOption == 6)
                {
                    FromDate = DateTime.Today.AddDays(1 - DateTime.Now.Day);
                }
                return FromDate;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return FromDate;
            }
        }
        public static DateTime GetToDate(int DateRangeOption)
        {
            DateTime ToDate = DateTime.Now;
            try
            {

                if (DateRangeOption ==1 || (DateRangeOption>2&& DateRangeOption<=6))
                {
                    ToDate = DateTime.Today.AddDays(1).AddSeconds(-1);
                }
                else if (DateRangeOption == 2)
                {
                    ToDate = DateTime.Today.AddSeconds(-1);
                }
                return ToDate;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return ToDate;
            }
        }
        

    }
}