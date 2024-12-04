using LogApp;
using System;
using System.Collections.Generic;
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
    public partial class ReportsCommonMethods : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        [ScriptMethod]
        public static List<ReportsEntity> GetReports(string ReportType, string DBType)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null && HttpContext.Current.Session["vRecordDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    if (DBType == "vBoard")
                    {
                        ClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    }
                    else if (DBType == "vRecord")
                    {
                        ClientDB = HttpContext.Current.Session["vRecordDB"].ToString();
                    }
                    ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                    List<ReportsEntity> ReportsEntityList;
                    ReportsEntityList = ReportsDALObj.GetReportsByType(ReportType);
                    return ReportsEntityList;
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


        //Recorder User to Filter Report on User Id 
        [WebMethod]
        [ScriptMethod]
        public static List<UsersEntity> GetUsers()
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vRecordDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vRecordClientDB = HttpContext.Current.Session["vRecordDB"].ToString();
                    UserDAL UsersDALObj = new UserDAL(vRecordClientDB);

                    List<UsersEntity> UsersEntityList = new List<UsersEntity>();
                    UsersEntityList = UsersDALObj.GetRecorderUsers();
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
        public static List<BoardsEntity> GetBoards()
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    BoardsDAL BoardsDALObj = new BoardsDAL(vBoardClientDB);
                    List<BoardsEntity> BoardsEntityList = new List<BoardsEntity>();
                    BoardsEntityList = BoardsDALObj.GetAllHuntGroups();
                    return BoardsEntityList;

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
        public static List<ExternalRoutingEntity> GetExternalRouting()
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ExternalRoutingDAL ExternalRoutingDALObj = new ExternalRoutingDAL(vBoardClientDB);

                    List<ExternalRoutingEntity> ExternalRoutingEntityList = new List<ExternalRoutingEntity>();
                    ExternalRoutingEntityList = ExternalRoutingDALObj.GetExternalRouting();
                    return ExternalRoutingEntityList;
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
        public static List<AgentStatsEntity> GetAgents()
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    AgentStatsDAL AgentStatsDALObj = new AgentStatsDAL(vBoardClientDB);
                    List<AgentStatsEntity> AgentStatsEntityList = new List<AgentStatsEntity>();
                    AgentStatsEntityList = AgentStatsDALObj.GetDistinctAgentsNames();
                    return AgentStatsEntityList;

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
        public static List<ExtensionsEntity> GetExtensions()
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    //string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ExtensionsDAL ExtensionsDALObj = new ExtensionsDAL(vBoardClientDB);
                    List<ExtensionsEntity> ExtensionsEntityList = new List<ExtensionsEntity>();
                    ExtensionsEntityList = ExtensionsDALObj.GetExtensions();
                    return ExtensionsEntityList;

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
        public static List<SectionsEntity> GetSectionsOnReportId(string ReportId, string DBType)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null && HttpContext.Current.Session["vRecordDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    if (DBType == "vBoard")
                    {
                        ClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    }
                    else if (DBType == "vRecord")
                    {
                        ClientDB = HttpContext.Current.Session["vRecordDB"].ToString();
                    }
                    ReportSectionsDAL ReportSectionsDALObj = new ReportSectionsDAL(ClientDB);
                    List<SectionsEntity> SectionsEntityList = new List<SectionsEntity>();
                    SectionsEntityList = ReportSectionsDALObj.GetSectionsOnReportID(Convert.ToInt32(ReportId));
                    return SectionsEntityList;
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

                    FromDate = DateTime.Today.AddDays((int)DayOfWeek.Monday - (int)DateTime.Today.DayOfWeek);
                    //FromDate = DateTime.Now.AddDays(-7).Date;
                }
                else if (DateRangeOption == 4)
                {
                    FromDate = DateTime.Today.AddDays((int)DayOfWeek.Monday - (int)DateTime.Today.DayOfWeek - 7);

                }
                else if (DateRangeOption == 5)
                {
                    FromDate = DateTime.Today.AddDays(1 - DateTime.Now.Day);
                }
                else if (DateRangeOption == 6)
                {
                    FromDate = DateTime.Today.AddDays(1 - DateTime.Now.Day).AddMonths(-1);
                }
                return FromDate;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return FromDate;
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static string GetReport(string ReportType, int ReportId, int DateRangeOption, int CallsOption, string dateFrom, string dateTo, string timeFrom, string timeTo, bool IsSchedule, string Extensions, string Groups, string ExternalRouting, string Agents, string Users, string WeekDays, int TimeInterval, string DBType, string Status)
        {
            if (HttpContext.Current.Session["vSupervisorDB"] != null)
            {
                try
                {

                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    SettingsDAL SettingsDALObj = new SettingsDAL(ClientDB);
                    SettingsEntity SettingsEntityObj = SettingsDALObj.GetSettings();
                    int InternalLength = SettingsEntityObj.InternalLength;
                    int RingTimeThreshold = SettingsEntityObj.RingTimeThreshold;
                    string Currency = SettingsEntityObj.Currency;
                    // HttpContext.Current.D
                    if (Currency == "Euro")
                    {
                        Currency = "€";
                    }
                    else if (Currency == "Dollar")
                    {
                        Currency = "$";
                    }
                    else if (Currency == "Pound")
                    {
                        Currency = "£";
                    }
                    float DiskCostPerMin = SettingsEntityObj.DiskCostPerMin;
                    float DurationCostPerMin = SettingsEntityObj.DurationCostPerMin;

                    DateTime FromDate = Convert.ToDateTime(dateFrom);
                    DateTime ToDate = Convert.ToDateTime(dateTo);

                    if (IsSchedule)
                    {
                        try
                        {
                            string[] temptimeFrom = timeFrom.Split(':');
                            FromDate = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, Convert.ToInt32(temptimeFrom[0]), Convert.ToInt32(temptimeFrom[1]), Convert.ToInt32(temptimeFrom[2]));
                        }
                        catch (Exception ex)
                        {
                            Log4Net.WriteException(ex);
                        }
                        try
                        {
                            //"HH:mm:ss"
                            string[] temptimeFrom = timeTo.Split(':');
                            ToDate = new DateTime(ToDate.Year, ToDate.Month, ToDate.Day, Convert.ToInt32(temptimeFrom[0]), Convert.ToInt32(temptimeFrom[1]), Convert.ToInt32(temptimeFrom[2]));
                        }
                        catch (Exception ex)
                        {
                            Log4Net.WriteException(ex);
                        }
                    }
                    if (Convert.ToInt32(DateRangeOption) > 0 && Convert.ToInt32(DateRangeOption) < 7 && IsSchedule == false)
                    {
                        FromDate = GetFromDate(Convert.ToInt32(DateRangeOption));
                        ToDate = DateTime.Today.AddDays(1).AddSeconds(-1);
                        if (DateRangeOption == 2)
                            ToDate = DateTime.Today.AddSeconds(-1);
                        if (DateRangeOption == 4)
                            ToDate = DateTime.Today.AddDays((int)DayOfWeek.Monday - (int)DateTime.Today.DayOfWeek).AddSeconds(-1);
                        if (DateRangeOption == 6)
                            ToDate = DateTime.Today.AddDays(1 - DateTime.Now.Day).AddSeconds(-1);

                        try
                        {
                            string[] temptimeFrom = timeFrom.Split(':');
                            FromDate = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, Convert.ToInt32(temptimeFrom[0]), Convert.ToInt32(temptimeFrom[1]), Convert.ToInt32(temptimeFrom[2]));
                        }
                        catch (Exception ex)
                        {
                            Log4Net.WriteException(ex);
                        }
                        try
                        {
                            //"HH:mm:ss"
                            string[] temptimeFrom = timeTo.Split(':');
                            ToDate = new DateTime(ToDate.Year, ToDate.Month, ToDate.Day, Convert.ToInt32(temptimeFrom[0]), Convert.ToInt32(temptimeFrom[1]), Convert.ToInt32(temptimeFrom[2]));
                        }
                        catch (Exception ex)
                        {
                            Log4Net.WriteException(ex);
                        }

                    }
                    else if (Convert.ToInt32(DateRangeOption) == 7)
                    {
                        timeFrom = FromDate.TimeOfDay.ToString();
                        timeTo = ToDate.TimeOfDay.ToString();
                    }


                    if (DBType == "vBoard")
                    {
                        ClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                        ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                        string ReportName = ReportsDALObj.GetReportNameOnReportId(ReportId);

                        if (ReportId == 1)
                        {
                            return ACDReports.GetCallsSummaryReport2(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 2)
                        {
                            return ACDReports.GetAllCallsReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays, Status);
                        }
                        else if (ReportId == 3)
                        {
                            return ACDReports.GetAbandonedCallsReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 4)
                        {
                            return ACDReports.GetCallBreakdownByHourReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays, TimeInterval);
                        }
                        else if (ReportId == 5)
                        {
                            return ACDReports.GetAgentAvailabilityReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 6)
                        {
                            return ACDReports.GetCallVolumeByAgentReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 7)
                        {
                            return ACDReports.GetAgentPresenceReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 8)
                        {
                            return ACDReports.GetTalkTimebyAgentReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 9)
                        {
                            return ACDReports.GetItemisedCallsByAgentReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 10)
                        {
                            ///this is not currently in use
                            ///Call ROuting Report
                            //return ACDReports.GetRoutingTreeReport(ReportType, ReportName, FromDate.Date, ToDate.Date.AddSeconds(86399), timeFrom, timeTo, Groups, WeekDays);
                            return ACDReports.GetRoutingTreeReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, WeekDays);
                        }
                        else if (ReportId == 11)
                        {
                            return ACDReports.GetAgentConsolidatedReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 12)
                        {
                            return ACDReports.GetGroupConsolidatedReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, WeekDays);
                        }
                        else if (ReportId == 13)
                        {
                            return ACDReports.GetOverflowCallsReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, WeekDays);
                            //return ACDReports.GetYBVCallsReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, WeekDays);
                        }                       
                        else if (ReportId == 14)
                        {
                            return ACDReports.GetConsolidatedAgentBoardReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 15)
                        {
                            return LoggingReports.GetCostSummaryByRegionReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);
                        }
                        else if (ReportId == 16)
                        {
                            return LoggingReports.GetCostSummaryByExtensionReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);
                        }
                        else if (ReportId == 17)
                        {
                            return LoggingReports.GetCostSummaryByPhoneReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);
                        }
                        else if (ReportId == 18)
                        {
                            return LoggingReports.GetCostSummaryByHourReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);
                        }
                        else if (ReportId == 19)
                        {
                            return LoggingReports.GetCostSummaryByDayReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);
                        }
                        else if (ReportId == 20)
                        {
                            return LoggingReports.GetCostSummaryByExtensionItemisedReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);
                        }
                        else if (ReportId == 21)
                        {
                            return LoggingReports.GetResponseSummaryByHourReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);
                        }
                        else if (ReportId == 22)
                        {
                            return LoggingReports.GetResponseSummaryByDayReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);
                        }
                        else if (ReportId == 23)
                        {
                            return LoggingReports.GetResponseSummaryByExtensionReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);
                        }
                        else if (ReportId == 24)
                        {
                            return LoggingReports.GetResponseSummaryByPhoneReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);
                        }
                        else if (ReportId == 25)
                        {
                            return LoggingReports.GetResponseSummaryByExtensionItemisedReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);
                        }
                        else if (ReportId == 26)
                        {
                            return LoggingReports.GetTrafficSummaryByHourReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);
                        }
                        else if (ReportId == 27)
                        {
                            return LoggingReports.GetTrafficSummaryByDayReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);
                        }
                        else if (ReportId == 28)
                        {
                            return LoggingReports.GetTrafficSummaryByExtensionReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);
                        }
                        else if (ReportId == 29)
                        {
                            return LoggingReports.GetTrafficSummaryByPhoneReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);
                        }
                        else if (ReportId == 30)
                        {
                            return LoggingReports.GetTrafficSummaryByExtensionItemisedReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);
                        }
                        else if (ReportId == 31)
                        {
                            return LoggingReports.GetCallTrailReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);
                        }
                        else if (ReportId == 32)
                        {
                            return ACDReports.GetYBVCallsByIntervalReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, WeekDays, TimeInterval);
                        }                        
                        else if (ReportId == 33)
                        {
                            return ACDReports.GetQueueOverViewReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, WeekDays, Groups);
                            //return ACDReports.GetAHTCallsReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, WeekDays);
                        }
                        else if (ReportId == 34)
                        {
                            return ACDReports.GetAHTCallsByIntervalReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, WeekDays, TimeInterval);
                        }
                        else if (ReportId == 35)
                        {
                            return ACDReports.GetSLAPerfromaceReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, WeekDays);
                        }
                        else if (ReportId == 36)
                        {
                            return ACDReports.GetExternalRoutingCallsByIntervalReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, WeekDays, TimeInterval, ExternalRouting);
                        }
                        else if (ReportId == 37)
                        {
                            return LoggingReports.GetTrafficSummaryByAreaCodeOutboundReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);
                        }
                        else if (ReportId == 38)
                        {
                            return LoggingReports.GetTrafficSummaryByAreaCodeInboundReport(ReportType, Currency, InternalLength, RingTimeThreshold, ReportName, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);
                        }
                        else if (ReportId == 39)
                        {
                            //GetAHTCallsByIntervalReport
                            return ACDReports.GetCallsbyPrimaryReasonReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, WeekDays);
                        }
                        else if (ReportId == 40)
                        {
                            //GetExternalRoutingCallsReport
                            return ACDReports.GetCallBreakdownbyPrimaryReasonReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, WeekDays);
                        }
                        else if (ReportId == 41)
                        {
                            return ACDReports.GetYBVCutOffCallsReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, WeekDays, TimeInterval);
                        }
                        else if (ReportId == 42)
                        {
                            return ACDReports.GetBasicCallsReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 43)
                        {
                            return ACDReports.GetAgentScoringReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 44)
                        {
                            return ACDReports.GetSupervisorScoringReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 45)
                        {
                            return ACDReports.GetDDIsSummaryReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, WeekDays);
                        }
                        else if (ReportId == 46)
                        {
                            return ACDReports.GetIVRDTMFReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, WeekDays);
                        }

                        else if (ReportId == 47)
                        {
                            return ACDReports.GetQueueAnalysisReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, WeekDays);

                        }
                        else if (ReportId == 48)
                        {
                            return ACDReports.GetOutboundDetailReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays, Status, "Abandoned");

                        }
                        else if (ReportId == 49)
                        {
                            return ACDReports.GetOutboundDetailReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays, Status, "Contact");

                        }
                        else if (ReportId == 50)
                        {
                            return ACDReports.GetOutboundDetailReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays, Status, "Callback");

                        }
                        else if (ReportId == 51)
                        {
                            return ACDReports.GetAgentOverviewReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 52)
                        {
                            return ACDReports.GetAgentUnavailbilityReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Agents, WeekDays);
                        }
                        else if (ReportId == 53)
                        {
                            return ACDReports.GetAgentOverviewSummaryReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }

                        else if (ReportId == 54)
                        {
                            return ACDReports.GetAgentConsolidatedSummaryReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 55)
                        {
                            return ACDReports.Get_DEKRA_QueueAnalysisReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, WeekDays);

                        }
                        else if (ReportId == 56)
                        {
                            //GetExternalRoutingCallsReport
                            return ACDReports.Get_DEKRA_CallBreakdownbyPrimaryReasonReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 57)
                        {
                            return ACDReports.GetAgentLeadReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }
                        else if (ReportId == 58)
                        {
                            return ACDReports.GetSupervisorLeadReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Groups, Agents, WeekDays);
                        }

                    }
                    else if (DBType == "vRecord")
                    {
                        ClientDB = HttpContext.Current.Session["vRecordDB"].ToString();

                        ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                        string ReportName = ReportsDALObj.GetReportNameOnReportId(ReportId);
                        if (ReportId == 1)
                        {
                            return RecorderReports.GetActivityLogReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Users, WeekDays);
                        }
                        else if (ReportId == 2)
                        {
                            string vCloudDB = System.Configuration.ConfigurationManager.AppSettings["vCloudDB"];
                            string FTPRepoisory = "FTPRepository";
                            string AccountID = ClientDB.Split('_')[1];
                            string RecordingPath = SettingsDALObj.GetRecordigPath(vCloudDB) + @"\" + AccountID + @"\" + FTPRepoisory; ;

                            return RecorderReports.GetDiskUsageCostReport(ReportType, Currency, ReportName, FromDate, ToDate, timeFrom, timeTo, RecordingPath, DiskCostPerMin, WeekDays);
                        }
                        else if (ReportId == 3)
                        {
                            return RecorderReports.GetDurationCostReport(ReportType, Currency, ReportName, FromDate, ToDate, timeFrom, timeTo, DurationCostPerMin, WeekDays);
                        }
                        else if (ReportId == 4)
                        {
                            return RecorderReports.GetCallNotesReport(ReportType, ReportName, FromDate, ToDate, timeFrom, timeTo, Users, WeekDays);
                        }
                    }
                    return null;
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

    }
}