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
   
    public partial class Schedules : System.Web.UI.Page
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
                        //else if (userProfileobj.Permissions.Find(x => x == 8) != 8)
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
        public static string AddReportSchedule(ReportScheduleEntity ReportScheduleEntityObj, string DateFrom, string DateTo, string DBType)
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
                    ReportScheduleDAL ReportScheduleDALObj = new ReportScheduleDAL(ClientDB);
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    string format = "dd/MM/yyyy HH:mm:ss";
                    DateTime d1 = DateTime.Now;
                    DateTime d2 = DateTime.Now;
                    //DateTime d3 = DateTime.Now;//schedule created date time
                    //DateTime d4 = DateTime.Now;//schedule execution  date time
                    DateTime CreatedDate = DateTime.Now;
                    DateTime NextExecuteAt = DateTime.Now;
                    NextExecuteAt = GetNextExecuteAt(ReportScheduleEntityObj.ScheduleInterval, ReportScheduleEntityObj.ScheduleValue, ReportScheduleEntityObj.ScheduleTimeHours, ReportScheduleEntityObj.ScheduleTimeMinutes);
                    if (ReportScheduleEntityObj.DateFilterCriteria == 7)
                    {
                        d1 = DateTime.ParseExact(DateFrom, format, provider);
                        d2 = DateTime.ParseExact(DateTo, format, provider);
                        ReportScheduleEntityObj.TimeFrom = d1.TimeOfDay.ToString();
                        ReportScheduleEntityObj.TimeTo = d2.TimeOfDay.ToString();


                    }
                    else
                    {
                        d2 = NextExecuteAt;

                        if (ReportScheduleEntityObj.DateFilterCriteria == 1)
                        {
                            d1 = NextExecuteAt.Date;
                            d2 = d1.Date.AddDays(1).AddSeconds(-1);
                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 2)
                        {
                            d1 = NextExecuteAt.Date.AddDays(-1);
                            d2 = d1.Date.AddDays(1).AddSeconds(-1);
                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 3)
                        {

                            d1 = NextExecuteAt.Date.AddDays((int)DayOfWeek.Monday - (int)NextExecuteAt.DayOfWeek);
                            d2 = NextExecuteAt.Date.AddDays(1).AddSeconds(-1);
                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 4)
                        {
                            d1 = NextExecuteAt.Date.AddDays((int)DayOfWeek.Monday - (int)NextExecuteAt.DayOfWeek - 7);
                            d2 = NextExecuteAt.Date.AddDays((int)DayOfWeek.Monday - (int)NextExecuteAt.DayOfWeek).AddSeconds(-1);

                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 5)
                        {
                            d1 = NextExecuteAt.Date.AddDays(1 - NextExecuteAt.Day);
                            d2 = NextExecuteAt.Date.AddDays(1).AddSeconds(-1);
                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 6)
                        {
                            d1 = NextExecuteAt.Date.AddDays(1 - NextExecuteAt.Day).AddMonths(-1);
                            d2 = NextExecuteAt.Date.AddDays(1 - NextExecuteAt.Day).AddSeconds(-1);
                        }

                    }
                    ReportScheduleEntityObj.DateFrom = d1;
                    ReportScheduleEntityObj.DateTo = d2;
                    ReportScheduleEntityObj.DateCreated = CreatedDate;
                    ReportScheduleEntityObj.ExecutionTime = NextExecuteAt;



                    ReportScheduleDALObj.InsertIntoReportSchedule(ReportScheduleEntityObj, DBType);

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
        public static bool UpdateScheduleFilters(ReportScheduleEntity ReportScheduleEntityObj, string DateFrom, string DateTo, string DBType)
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

                    ReportScheduleDAL ReportScheduleDALObj = new ReportScheduleDAL(ClientDB);
                    ReportScheduleEntity TempReportScheduleEntityObj = ReportScheduleDALObj.GetReportScheduleOnId(ReportScheduleEntityObj.Id, ClientDB);
                    ReportScheduleEntityObj.ScheduleInterval = TempReportScheduleEntityObj.ScheduleInterval;
                    ReportScheduleEntityObj.ScheduleValue = TempReportScheduleEntityObj.ScheduleValue;
                    ReportScheduleEntityObj.ScheduleTimeHours = TempReportScheduleEntityObj.ScheduleTimeHours;
                    ReportScheduleEntityObj.ScheduleTimeMinutes = TempReportScheduleEntityObj.ScheduleTimeMinutes;


                    CultureInfo provider = CultureInfo.InvariantCulture;
                    string format = "dd/MM/yyyy HH:mm:ss";
                    DateTime d1 = DateTime.Now;
                    DateTime d2 = DateTime.Now;
                    DateTime CreatedDate = DateTime.Now;
                    DateTime NextExecuteAt = DateTime.Now;
                    NextExecuteAt = GetNextExecuteAt(ReportScheduleEntityObj.ScheduleInterval, ReportScheduleEntityObj.ScheduleValue, ReportScheduleEntityObj.ScheduleTimeHours, ReportScheduleEntityObj.ScheduleTimeMinutes);
                    if (ReportScheduleEntityObj.DateFilterCriteria == 7)
                    {
                        d1 = DateTime.ParseExact(DateFrom, format, provider);
                        d2 = DateTime.ParseExact(DateTo, format, provider);
                        ReportScheduleEntityObj.TimeFrom = d1.TimeOfDay.ToString();
                        ReportScheduleEntityObj.TimeTo = d2.TimeOfDay.ToString();
                    }
                    else
                    {
                        d2 = NextExecuteAt;

                        if (ReportScheduleEntityObj.DateFilterCriteria == 1)
                        {
                            d1 = NextExecuteAt.Date;
                            d2 = d1.Date.AddDays(1).AddSeconds(-1);
                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 2)
                        {
                            d1 = NextExecuteAt.Date.AddDays(-1);
                            d2 = d1.Date.AddDays(1).AddSeconds(-1);
                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 3)
                        {

                            d1 = NextExecuteAt.Date.AddDays((int)DayOfWeek.Monday - (int)NextExecuteAt.DayOfWeek);
                            d2 = NextExecuteAt.Date.AddDays(1).AddSeconds(-1);
                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 4)
                        {
                            d1 = NextExecuteAt.Date.AddDays((int)DayOfWeek.Monday - (int)NextExecuteAt.DayOfWeek - 7);
                            d2 = NextExecuteAt.Date.AddDays((int)DayOfWeek.Monday - (int)NextExecuteAt.DayOfWeek).AddSeconds(-1);

                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 5)
                        {
                            d1 = NextExecuteAt.Date.AddDays(1 - NextExecuteAt.Day);
                            d2 = NextExecuteAt.Date.AddDays(1).AddSeconds(-1);
                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 6)
                        {
                            d1 = NextExecuteAt.Date.AddDays(1 - NextExecuteAt.Day).AddMonths(-1);
                            d2 = NextExecuteAt.Date.AddDays(1 - NextExecuteAt.Day).AddSeconds(-1);
                        }

                    }
                    ReportScheduleEntityObj.DateFrom = d1;
                    ReportScheduleEntityObj.DateTo = d2;
                    //ReportScheduleEntityObj.DateCreated = CreatedDate;
                    ReportScheduleEntityObj.ExecutionTime = NextExecuteAt;

                    //ReportScheduleDAL ReportScheduleDALObj = new ReportScheduleDAL(ClientDB);
                    ReportScheduleDALObj.UpdateScheduleFilters(ReportScheduleEntityObj, DBType);
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
        public static bool UpdateIntoSchedules(ReportScheduleEntity ReportScheduleEntityObj, string DBType)
        {
            if (HttpContext.Current.Session["vSupervisorDB"] != null)
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
                    ReportScheduleDAL ReportScheduleDALObj = new ReportScheduleDAL(ClientDB);
                    ReportScheduleEntity TempReportScheduleEntityObj = ReportScheduleDALObj.GetReportScheduleOnId(ReportScheduleEntityObj.Id, ClientDB);
                    ReportScheduleEntityObj.DateFilterCriteria = TempReportScheduleEntityObj.DateFilterCriteria;
                    ReportScheduleEntityObj.ExecutionTime = TempReportScheduleEntityObj.ExecutionTime;
                    ReportScheduleEntityObj.DateFrom = TempReportScheduleEntityObj.DateFrom;
                    ReportScheduleEntityObj.DateTo = TempReportScheduleEntityObj.DateTo;

                    DateTime d1 = DateTime.Now;
                    DateTime d2 = DateTime.Now;
                    DateTime CreatedDate = DateTime.Now;
                    DateTime NextExecuteAt = DateTime.Now;
                    NextExecuteAt = GetNextExecuteAt(ReportScheduleEntityObj.ScheduleInterval, ReportScheduleEntityObj.ScheduleValue, ReportScheduleEntityObj.ScheduleTimeHours, ReportScheduleEntityObj.ScheduleTimeMinutes);
                    if (ReportScheduleEntityObj.DateFilterCriteria == 7)
                    {
                        d1 = ReportScheduleEntityObj.DateFrom;
                        d2 = ReportScheduleEntityObj.DateTo;
                        ReportScheduleEntityObj.TimeFrom = d1.TimeOfDay.ToString();
                        ReportScheduleEntityObj.TimeTo = d2.TimeOfDay.ToString();

                    }
                    else
                    {
                        d2 = NextExecuteAt;

                        if (ReportScheduleEntityObj.DateFilterCriteria == 1)
                        {
                            d1 = NextExecuteAt.Date;
                            d2 = d1.Date.AddDays(1).AddSeconds(-1);
                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 2)
                        {
                            d1 = NextExecuteAt.Date.AddDays(-1);
                            d2 = d1.Date.AddDays(1).AddSeconds(-1);
                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 3)
                        {

                            d1 = NextExecuteAt.Date.AddDays((int)DayOfWeek.Monday - (int)NextExecuteAt.DayOfWeek);
                            d2 = NextExecuteAt.Date.AddDays(1).AddSeconds(-1);
                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 4)
                        {
                          
                            d1 = NextExecuteAt.Date.AddDays((int)DayOfWeek.Monday - (int)NextExecuteAt.DayOfWeek - 7);
                            d2 = NextExecuteAt.Date.AddDays((int)DayOfWeek.Monday - (int)NextExecuteAt.DayOfWeek).AddSeconds(-1);

                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 5)
                        {
                            d1 = NextExecuteAt.Date.AddDays(1 - NextExecuteAt.Day);
                            d2 = NextExecuteAt.Date.AddDays(1).AddSeconds(-1);
                        }
                        else if (ReportScheduleEntityObj.DateFilterCriteria == 6)
                        {
                            d1 = NextExecuteAt.Date.AddDays(1 - NextExecuteAt.Day).AddMonths(-1);
                            d2 = NextExecuteAt.Date.AddDays(1 - NextExecuteAt.Day).AddSeconds(-1);
                        }
                    }

                    ReportScheduleEntityObj.DateFrom = d1;
                    ReportScheduleEntityObj.DateTo = d2;
                    ReportScheduleEntityObj.ExecutionTime = NextExecuteAt;
                    ReportScheduleDALObj.UpdateIntoReportSchedule(ReportScheduleEntityObj);
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
        public static bool DeleteFromSchedules(int ScheduleId, string DBType)
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
                    ReportScheduleDAL ReportScheduleDALObj = new ReportScheduleDAL(ClientDB);
                    ReportScheduleDALObj.DeleteFromReportSchedule(ScheduleId);
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
        public static List<ReportScheduleEntity> GetScheduleOnReportId(int ReportId,string DBType)
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
                    ReportScheduleDAL ReportScheduleDALObj = new ReportScheduleDAL(ClientDB);
                    List<ReportScheduleEntity> ReportScheduleEntityList = new List<ReportScheduleEntity>();
                    ReportScheduleEntityList = ReportScheduleDALObj.GetScheduleOnReportId(ReportId, DBType);

                    return ReportScheduleEntityList;
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

        public static DateTime GetNextExecuteAt(int ScheduleInterval, string ScheduleValue, string ScheduleTimeHours, string ScheduleTimeMinutes)
        {
            try
            {
                DateTime NextExecuteAt = DateTime.Today;
                int Hour = int.Parse(ScheduleTimeHours);
                int Minute = int.Parse(ScheduleTimeMinutes);
                
                if (ScheduleInterval == 1)
                {
                    LogApp.Log4Net.WriteLog("Calling Next Execute Daily", LogType.GENERALLOG);
                    DateTime today = DateTime.Today;
                    //   int Hour = int.Parse(ScheduleTimeHours);
                    //   int Minute = int.Parse(ScheduleTimeMinutes);
                    NextExecuteAt = new DateTime(NextExecuteAt.Year, NextExecuteAt.Month, NextExecuteAt.Day, Hour, Minute, 0);
                    if (NextExecuteAt < DateTime.Now)
                    {
                        NextExecuteAt = NextExecuteAt.AddDays(1);
                        NextExecuteAt = new DateTime(NextExecuteAt.Year, NextExecuteAt.Month, NextExecuteAt.Day, Hour, Minute, 0);
                    }
                    LogApp.Log4Net.WriteLog("Calling Next Execute Daily End", LogType.GENERALLOG);
                }
                else if (ScheduleInterval == 2)
                {
                  
                    LogApp.Log4Net.WriteLog("Calling Next Execute Weekly", LogType.GENERALLOG);
                    DateTime today = DateTime.Today;
                    //DateTime dt = new DateTime(today.Year, today.Month, Day);
                    while (today.DayOfWeek.ToString().ToLower() != ScheduleValue.ToLower())
                    {
                        today = today.AddDays(1);
                    }
                    //today = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
                    //NextExecuteAt.Date.AddDays((int)DayOfWeek.Monday - (int)DateTime.Today.DayOfWeek);
                    DateTime next = new DateTime(today.Year, today.Month, today.Day, Hour, Minute, 0);
                    while (next <= DateTime.Now)
                    {
                        next = next.AddDays(7);
                    }
                    NextExecuteAt = new DateTime(next.Year, next.Month, next.Day, Hour, Minute, 0);
                    LogApp.Log4Net.WriteLog("Calling Next Execute Weekly End", LogType.GENERALLOG);
                }
                else if (ScheduleInterval == 3)
                {
                    int Day = int.Parse(ScheduleValue);
                    DateTime today = DateTime.Now;
                    //today = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
                    DateTime next = new DateTime(today.Year, today.Month, Day, Hour, Minute, 0);
                    while (next <= today)
                    {
                        next = next.AddMonths(1);
                    }
                    NextExecuteAt = new DateTime(next.Year, next.Month, next.Day, Hour, Minute, 0);

                }
                LogApp.Log4Net.WriteLog("Next Execute at:" + NextExecuteAt.ToString("dd, ddd MMM yyyy, HH:mm:ss"), LogType.GENERALLOG);
                return NextExecuteAt;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return DateTime.Now;
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static bool CheckIfScheduleNameAlreadyExists(int ScheduleId, string Name, string DBType)
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
                    ReportScheduleDAL ReportScheduleDALObj = new ReportScheduleDAL(ClientDB);
                    return ReportScheduleDALObj.CheckIfScheduleNameAlreadyExists(ScheduleId, Name);
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