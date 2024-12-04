using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Script.Services;
using System.Threading;
using System.Globalization;
using vSuperMTClient.Entities;
using vSuperMTClient.DALs;
using LogApp;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Linq;
using Secure;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing.Imaging;
using ClosedXML.Excel;
using System.Text;

namespace vSuperMTClient
{
    public partial class LoggingReports : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null && Session["vSupervisorDB"] != null)
            {
                try
                {
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
                        //else if (userProfileobj.Permissions.Find(x => x == 7) != 7)
                        //{
                        //    Response.Redirect("Login.aspx");
                        //}
                        //ReportsCommonMethods.GetAvailableLicenses();
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
                Response.Redirect("~/Login.aspx");
            }
        }

        public static string GetCostSummaryByRegionReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportCostSummaryByTypeEntity> ReportEntityList = new List<ReportCostSummaryByTypeEntity>();
                ReportEntityList = ReportsDALObj.GetCostSummaryByRegionReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);

                if (ReportType == "PDF")
                {
                    string FileName = GenerateCostSummaryByRegionReport(Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateCostSummaryByRegionReportCSV(Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }

            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetCostSummaryByExtensionReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportCostSummaryByExtensionEntity> ReportEntityList = new List<ReportCostSummaryByExtensionEntity>();
                ReportEntityList = ReportsDALObj.GetCostSummaryByExtensionReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);
                if (ReportType == "PDF")
                {
                    string FileName = GenerateCostSummaryByExtensionReport(Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateCostSummaryByExtensionReportCSV(Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetCostSummaryByPhoneReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportCostSummaryByPhoneEntity> ReportEntityList = new List<ReportCostSummaryByPhoneEntity>();
                ReportEntityList = ReportsDALObj.GetCostSummaryByPhoneReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);
                if (ReportType == "PDF")
                {
                    string FileName = GenerateCostSummaryByPhoneReport(Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateCostSummaryByPhoneReportCSV(Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetCostSummaryByHourReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportCostSummaryByHourEntity> ReportEntityList = new List<ReportCostSummaryByHourEntity>();
                ReportEntityList = ReportsDALObj.GetCostSummaryByHourReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);

                if (ReportType == "PDF")
                {
                    string FileName = GenerateCostSummaryByHourReport(Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateCostSummaryByHourReportCSV(Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetCostSummaryByDayReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportCostSummaryByDayEntity> ReportEntityList = new List<ReportCostSummaryByDayEntity>();
                ReportEntityList = ReportsDALObj.GetCostSummaryByDayReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);
                if (ReportType == "PDF")
                {
                    string FileName = GenerateCostSummaryByDayReport(Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateCostSummaryByDayReportCSV(Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetCostSummaryByExtensionItemisedReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportCostSummaryByExtensionItemisedEntity> ReportEntityList = new List<ReportCostSummaryByExtensionItemisedEntity>();
                ReportEntityList = ReportsDALObj.GetCostSummaryByExtensionItemisedReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);
                if (ReportType == "PDF")
                {
                    string FileName = GenerateCostSummaryByExtensionItemisedReport(Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateCostSummaryByExtensionItemisedReportCSV(Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetResponseSummaryByHourReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportResponseSummaryByHourEntity> ReportEntityList = new List<ReportResponseSummaryByHourEntity>();
                ReportEntityList = ReportsDALObj.GetResponseSummaryByHourReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);
                if (ReportType == "PDF")
                {
                    string FileName = GenerateResponseSummaryByHourReport(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else if (ReportType == "CSV")
                {
                    string FileName = GenerateResponseSummaryByHourReportCSV(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                 else
                {
                    string FileName = GenerateResponseSummaryByHourReportExcel(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }

            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetResponseSummaryByDayReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportResponseSummaryByDayEntity> ReportEntityList = new List<ReportResponseSummaryByDayEntity>();
                ReportEntityList = ReportsDALObj.GetResponseSummaryByDayReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);
                if (ReportType == "PDF")
                {
                    string FileName = GenerateResponseSummaryByDayReport(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else if (ReportType == "CSV")

                {
                    string FileName = GenerateResponseSummaryByDayReportCSV(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateResponseSummaryByDayReportExcel(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }


            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetResponseSummaryByExtensionReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportResponseSummaryByExtensionEntity> ReportEntityList = new List<ReportResponseSummaryByExtensionEntity>();
                ReportEntityList = ReportsDALObj.GetResponseSummaryByExtensionReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);
                if (ReportType == "PDF")
                {
                    string FileName = GenerateResponseSummaryByExtensionReport(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else if (ReportType == "CSV")
                {
                    string FileName = GenerateResponseSummaryByExtensionReportCSV(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                 else
                {
                    string FileName = GenerateResponseSummaryByExtensionReportExcel(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }


            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetResponseSummaryByPhoneReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportResponseSummaryByPhoneEntity> ReportEntityList = new List<ReportResponseSummaryByPhoneEntity>();
                ReportEntityList = ReportsDALObj.GetResponseSummaryByPhoneReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);
                if (ReportType == "PDF")
                {
                    string FileName = GenerateResponseSummaryByPhoneReport(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else if (ReportType == "CSV")

                {
                    string FileName = GenerateResponseSummaryByPhoneReportCSV(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateResponseSummaryByPhoneReportExcel(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }

            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetResponseSummaryByExtensionItemisedReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportResponseSummaryByExtensionItemisedEntity> ReportEntityList = new List<ReportResponseSummaryByExtensionItemisedEntity>();
                ReportEntityList = ReportsDALObj.GetResponseSummaryByExtensionItemisedReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);

                if (ReportType == "PDF")
                {
                    string FileName = GenerateResponseSummaryByExtensionItemisedReport(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else if (ReportType == "CSV")
                {
                    string FileName = GenerateResponseSummaryByExtensionItemisedReportCSV(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateResponseSummaryByExtensionItemisedReportExcel(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }

            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetTrafficSummaryByHourReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportTrafficSummaryByHourEntity> ReportEntityList = new List<ReportTrafficSummaryByHourEntity>();
                ReportEntityList = ReportsDALObj.GetTrafficSummaryByHourReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);

                //foreach (ReportTrafficSummaryByHourEntity obj in ReportEntityList)
                //{
                //    TimeSpan t = TimeSpan.FromSeconds(obj.IncomingCallDuration);

                //    string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                //                    t.Hours,
                //                    t.Minutes,
                //                    t.Seconds,
                //                    t.Milliseconds);
                //}
                
                if (ReportType == "PDF")
                {
                    string FileName = GenerateTrafficSummaryByHourReport(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else if (ReportType == "CSV")
                {
                    string FileName = GenerateTrafficSummaryByHourReportCSV(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateTrafficSummaryByHourReportExcel(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }

            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetTrafficSummaryByDayReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportTrafficSummaryByDayEntity> ReportEntityList = new List<ReportTrafficSummaryByDayEntity>();
                ReportEntityList = ReportsDALObj.GetTrafficSummaryByDayReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);

                if (ReportType == "PDF")
                {
                    string FileName = GenerateTrafficSummaryByDayReport(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else if (ReportType == "CSV")
                {
                    string FileName = GenerateTrafficSummaryByDayReportCSV(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateTrafficSummaryByDayReportExcel(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }


            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetTrafficSummaryByExtensionReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportTrafficSummaryByExtensionEntity> ReportEntityList = new List<ReportTrafficSummaryByExtensionEntity>();
                ReportEntityList = ReportsDALObj.GetTrafficSummaryByExtensionReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);

                if (ReportType == "PDF")
                {
                    string FileName = GenerateTrafficSummaryByExtensionReport(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else if (ReportType == "CSV")

                {
                    string FileName = GenerateTrafficSummaryByExtensionReportCSV(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateTrafficSummaryByExtensionReportExcel(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }



            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetTrafficSummaryByPhoneReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportTrafficSummaryByPhoneEntity> ReportEntityList = new List<ReportTrafficSummaryByPhoneEntity>();
                ReportEntityList = ReportsDALObj.GetTrafficSummaryByPhoneReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);

                if (ReportType == "PDF")
                {
                    string FileName = GenerateTrafficSummaryByPhoneReport(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else if (ReportType == "CSV")

                {
                    string FileName = GenerateTrafficSummaryByPhoneReportCSV(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateTrafficSummaryByPhoneReportExcel(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }


            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetTrafficSummaryByExtensionItemisedReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportTrafficSummaryByExtensionItemisedEntity> ReportEntityList = new List<ReportTrafficSummaryByExtensionItemisedEntity>();
                ReportEntityList = ReportsDALObj.GetTrafficSummaryByExtensionItemisedReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays, CallsOption);

                if (ReportType == "PDF")
                {
                    string FileName = GenerateTrafficSummaryByExtensionItemisedReport(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else if (ReportType == "CSV")
                {
                    string FileName = GenerateTrafficSummaryByExtensionItemisedReportCSV(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                 else
                {
                    string FileName = GenerateTrafficSummaryByExtensionItemisedReportExcel(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }

            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetCallTrailReport(string ReportType, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportCallTrailEntity> ReportEntityList = new List<ReportCallTrailEntity>();
                ReportEntityList = ReportsDALObj.GetCallTrailReport(FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);

                if (ReportType == "PDF")
                {
                    string FileName = GenerateCallTrailReport(ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else if (ReportType == "CSV")
                {
                    string FileName = GenerateCallTrailReportCSV(ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateCallTrailReportExcel(ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetTrafficSummaryByAreaCodeOutboundReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportTrafficSummaryByAreaCodeOutboundEntity> ReportEntityList = new List<ReportTrafficSummaryByAreaCodeOutboundEntity>();
                ReportEntityList = ReportsDALObj.GetTrafficSummaryByAreaCodeOutboundReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);

                if (ReportType == "PDF")
                {
                    string FileName = GenerateTrafficSummaryByAreaCodeOutboundReport(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else if (ReportType == "CSV")

                {
                    string FileName = GenerateTrafficSummaryByAreaCodeOutboundReportCSV(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
else
                {
                    string FileName = GenerateTrafficSummaryByAreaCodeOutboundReportExcel(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }

            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetTrafficSummaryByAreaCodeInboundReport(string ReportType, string Currency, int InternalLength, int RingTimeThreshold, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vBoardClientDB);
                List<ReportTrafficSummaryByAreaCodeInboundEntity> ReportEntityList = new List<ReportTrafficSummaryByAreaCodeInboundEntity>();
                ReportEntityList = ReportsDALObj.GetTrafficSummaryByAreaCodeInboundReport(InternalLength, RingTimeThreshold, FromDate, ToDate, timeFrom, timeTo, Extensions, WeekDays);

                if (ReportType == "PDF")
                {
                    string FileName = GenerateTrafficSummaryByAreaCodeInboundReport(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else if (ReportType == "CSV")
                {
                    string FileName = GenerateTrafficSummaryByAreaCodeInboundReportCSV(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
else
                {
                    string FileName = GenerateTrafficSummaryByAreaCodeInboundReportExcel(Currency, RingTimeThreshold, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }

            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }

        //****************************************************************************//

        public static string GenerateCostSummaryByRegionReport(string Currency, string ReportName, List<ReportCostSummaryByTypeEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                //Document document = new Document(PageSize.A4.Rotate(), 15f, 15f, 15f, 15f);595
                Document document = new Document(PageSize.A4, 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 535f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 535 });//fixed widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);
                if (ReportEntityList.Count > 0)
                {
                    Graphtable = new PdfPTable(2);
                    Graphtable.TotalWidth = 600f;
                    Graphtable.LockedWidth = true;
                    Graphtable.SetWidths(new float[] { 1f, 1f });


                    Image pdfImage1 = Image.GetInstance(GraphImageCostSummaryByRegionReport1(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage1, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);
                    Image pdfImage2 = Image.GetInstance(GraphImageCostSummaryByRegionReport2(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage2, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);

                    document.Add(Graphtable);


                    Datatable = new PdfPTable(4);
                    Datatable.TotalWidth = 535f;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Call Type"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Cost (" + Currency + ")"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Duration"));


                    List<ReportCostSummaryByTypeEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                    .Select(y => new ReportCostSummaryByTypeEntity
                    {
                        Duration = y.Sum(d => d.Duration),
                        Cost = y.Sum(d => d.Cost),
                        TotalCalls = y.Sum(d => d.TotalCalls),

                    }).ToList();

                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportCostSummaryByTypeEntity obj = ReportEntityList[i];

                        if (i == ReportEntityList.Count - 1)
                        {
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.CostType));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));



                            ReportCostSummaryByTypeEntity obj1 = TempReportEntityGroupedList[0];
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration)));



                        }
                        else
                        {
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.CostType));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));


                        }
                    }
                    document.Add(Datatable);

                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }

                document.Close();


                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateCostSummaryByExtensionReport(string Currency, string ReportName, List<ReportCostSummaryByExtensionEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                //Document document = new Document(PageSize.A4.Rotate(), 15f, 15f, 15f, 15f);
                Document document = new Document(PageSize.A4, 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 535f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 535 });//fixed widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);

                if (ReportEntityList.Count > 0)
                {
                    Graphtable = new PdfPTable(1);
                    Graphtable.TotalWidth = 650f;
                    Graphtable.LockedWidth = true;
                    Graphtable.SetWidths(new float[] { 1f });

                    Image pdfImage1 = Image.GetInstance(GraphImageCostSummaryByExtensionReport(ReportEntityList).GetBuffer());

                    cell = ReportsCommonMethods.GraphImageCell(pdfImage1, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);
                    //Image pdfImage2 = GraphImageCostByTypeReport2(ReportEntityList);
                    //cell = GraphImageCell(pdfImage2, 100f, PdfPCell.ALIGN_LEFT);
                    //Graphtable.AddCell(cell);

                    document.Add(Graphtable);


                    Datatable = new PdfPTable(5);
                    Datatable.TotalWidth = 535f;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Extension"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Name"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Cost (" + Currency + ")"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Duration"));


                    List<ReportCostSummaryByExtensionEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                   .Select(y => new ReportCostSummaryByExtensionEntity
                   {
                       Duration = y.Sum(d => d.Duration),
                       Cost = y.Sum(d => d.Cost),
                       TotalCalls = y.Sum(d => d.TotalCalls),

                   }).ToList();

                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportCostSummaryByExtensionEntity obj = ReportEntityList[i];

                        if (i == ReportEntityList.Count - 1)
                        {
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Extension));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Name));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));


                            ReportCostSummaryByExtensionEntity obj1 = TempReportEntityGroupedList[0];
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration)));



                        }
                        else
                        {
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Extension));
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Name));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));


                        }
                    }
                    document.Add(Datatable);
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }


                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateCostSummaryByPhoneReport(string Currency, string ReportName, List<ReportCostSummaryByPhoneEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);//842
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 782f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 782 });//fixed widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);

                //Graphtable = new PdfPTable(1);
                //Graphtable.TotalWidth = 782f;
                //Graphtable.LockedWidth = true;
                //Graphtable.SetWidths(new float[] { 1f });


                //Image pdfImage1 = GraphImageCostSummaryByPhoneReport(ReportEntityList);
                //cell = GraphImageCell(pdfImage1, 100f, PdfPCell.ALIGN_CENTER);
                //Graphtable.AddCell(cell);
                //document.Add(Graphtable);


                if (ReportEntityList.Count > 0)
                {
                    Datatable = new PdfPTable(4);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Dialled Number"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Cost (" + Currency + ")"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Duration"));


                    List<ReportCostSummaryByPhoneEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                      .Select(y => new ReportCostSummaryByPhoneEntity
                      {
                          Duration = y.Sum(d => d.Duration),
                          Cost = y.Sum(d => d.Cost),
                          TotalCalls = y.Sum(d => d.TotalCalls),

                      }).ToList();
                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportCostSummaryByPhoneEntity obj = ReportEntityList[i];

                        if (i == ReportEntityList.Count - 1)
                        {
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.DialledNumber));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));


                            ReportCostSummaryByPhoneEntity obj1 = TempReportEntityGroupedList[0];
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration)));



                        }
                        else
                        {
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.DialledNumber));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));


                        }
                    }
                    document.Add(Datatable);
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }


                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateCostSummaryByHourReport(string Currency, string ReportName, List<ReportCostSummaryByHourEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                //Document document = new Document(PageSize.A4.Rotate(), 15f, 15f, 15f, 15f);
                Document document = new Document(PageSize.A4, 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 535f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 535 });//fixed widths
                //Headertable.SetWidths(new float[] { 1f, 7f });//relative column widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);

                if (ReportEntityList.Count > 0)
                {
                    Graphtable = new PdfPTable(1);
                    Graphtable.TotalWidth = 650f;
                    Graphtable.LockedWidth = true;
                    Graphtable.SetWidths(new float[] { 1f });



                    Image pdfImage1 = Image.GetInstance(GraphImageCostSummaryByHourReport(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage1, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);
                    document.Add(Graphtable);


                    Datatable = new PdfPTable(5);
                    Datatable.TotalWidth = 535;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Time Period"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Cost (" + Currency + ")"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Duration"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Avg. Duration"));


                    List<ReportCostSummaryByHourEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                   .Select(y => new ReportCostSummaryByHourEntity
                   {
                       Duration = y.Sum(d => d.Duration),
                       Cost = y.Sum(d => d.Cost),
                       TotalCalls = y.Sum(d => d.TotalCalls),

                   }).ToList();

                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportCostSummaryByHourEntity obj = ReportEntityList[i];


                        if (i == ReportEntityList.Count - 1)
                        {
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(ReportsCommonMethods.GetHourFormat(obj.Hour)));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(ReportsCommonMethods.GetMinutesFromSeconds2(obj.AvgDuration)));


                            ReportCostSummaryByHourEntity obj1 = TempReportEntityGroupedList[0];
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration)));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(""));




                        }
                        else
                        {
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(ReportsCommonMethods.GetHourFormat(obj.Hour)));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));
                            Datatable.AddCell(ReportsCommonMethods.RowRightCell(ReportsCommonMethods.GetMinutesFromSeconds2(obj.AvgDuration)));
                        }
                    }
                    document.Add(Datatable);
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }
                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateCostSummaryByDayReport(string Currency, string ReportName, List<ReportCostSummaryByDayEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                //Document document = new Document(PageSize.A4.Rotate(), 15f, 15f, 15f, 15f);
                Document document = new Document(PageSize.A4, 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 535f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 535 });//fixed widths
                //Headertable.SetWidths(new float[] { 1f, 7f });//relative column widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);

                if (ReportEntityList.Count > 0)
                {
                    Graphtable = new PdfPTable(1);
                    Graphtable.TotalWidth = 650f;
                    Graphtable.LockedWidth = true;
                    Graphtable.SetWidths(new float[] { 1f });



                    Image pdfImage1 = Image.GetInstance(GraphImageCostSummaryByDayReport(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage1, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);
                    document.Add(Graphtable);


                    Datatable = new PdfPTable(5);
                    Datatable.TotalWidth = 535;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Date"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Day"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Cost (" + Currency + ")"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Duration"));


                    List<ReportCostSummaryByDayEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                      .Select(y => new ReportCostSummaryByDayEntity
                      {
                          Duration = y.Sum(d => d.Duration),
                          Cost = y.Sum(d => d.Cost),
                          TotalCalls = y.Sum(d => d.TotalCalls),

                      }).ToList();

                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportCostSummaryByDayEntity obj = ReportEntityList[i];

                        if (i == ReportEntityList.Count - 1)
                        {
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Date));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Day));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));


                            ReportCostSummaryByDayEntity obj1 = TempReportEntityGroupedList[0];
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration)));



                        }
                        else
                        {
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Date));
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Day));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Cost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.TotalCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));


                        }
                    }
                    document.Add(Datatable);
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }



                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateCostSummaryByExtensionItemisedReport(string Currency, string ReportName, List<ReportCostSummaryByExtensionItemisedEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                //Document document = new Document(PageSize.A4.Rotate(), 15f, 15f, 15f, 15f);
                Document document = new Document(PageSize.A4, 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable SubSectionHeadertable = null;
                //PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 535f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 535 });//fixed widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);

                //Graphtable = new PdfPTable(1);
                //Graphtable.TotalWidth = 782f;
                //Graphtable.LockedWidth = true;
                //Graphtable.SetWidths(new float[] { 1f });


                //Image pdfImage1 = GraphImageCostSummaryByExtensionItemisedReport(ReportEntityList);
                //cell = GraphImageCell(pdfImage1, 100f, PdfPCell.ALIGN_CENTER);
                //Graphtable.AddCell(cell);
                //document.Add(Graphtable);
                if (ReportEntityList.Count > 0)
                {
                    List<ReportCostSummaryByExtensionItemisedEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => x.Extension)
                .Select(y => new ReportCostSummaryByExtensionItemisedEntity
                {
                    Extension = y.First().Extension,
                    Duration = y.Sum(d => d.Duration),
                    Cost = y.Sum(d => d.Cost),
                    CallCount = y.Count(),

                }).ToList();

                    List<List<ReportCostSummaryByExtensionItemisedEntity>> ReportEntityGroupedList = ReportEntityList.GroupBy(x => new { x.Extension }).Select(y => y.ToList()).ToList();
                    foreach (List<ReportCostSummaryByExtensionItemisedEntity> ReportEntityLists in ReportEntityGroupedList)
                    {
                        SubSectionHeadertable = new PdfPTable(1);
                        SubSectionHeadertable.TotalWidth = 535f;
                        SubSectionHeadertable.LockedWidth = true;
                        SubSectionHeadertable.SetTotalWidth(new float[] { 535 });//fixed widths

                        phrase = new Phrase();
                        phrase.Add(new Chunk("Extension: ", FontFactory.GetFont("Arial", 14, Font.BOLD, Color.BLACK)));
                        phrase.Add(new Chunk(ReportEntityLists[0].Extension, FontFactory.GetFont("Arial", 14, Font.NORMAL, Color.BLACK)));
                        cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                        cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                        cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                        cell.PaddingTop = 10f;
                        cell.PaddingBottom = 0f;
                        SubSectionHeadertable.AddCell(cell);
                        document.Add(SubSectionHeadertable);

                        Datatable = new PdfPTable(5);
                        Datatable.TotalWidth = 535;
                        Datatable.LockedWidth = true;
                        Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                        Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f });
                        Datatable.SpacingBefore = 15f;

                        Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Date"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Time"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Phone Number"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Cost (" + Currency + ")"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Duration"));

                        for (int i = 0; i < ReportEntityLists.Count; i++)
                        {
                            ReportCostSummaryByExtensionItemisedEntity obj = ReportEntityLists[i];

                            if (i == ReportEntityLists.Count - 1)
                            {
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Date));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Time));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.PhoneNumber));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Cost.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));


                                ReportCostSummaryByExtensionItemisedEntity obj1 = TempReportEntityGroupedList.Find(x => x.Extension == obj.Extension);
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total: " + obj1.CallCount.ToString() + ""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.Cost.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration)));



                            }
                            else
                            {
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Date));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Time));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(obj.PhoneNumber));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Cost.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.RowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));

                            }
                        }
                        document.Add(Datatable);
                    }
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }

                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateResponseSummaryByHourReport(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByHourEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();

                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 782f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 782 });//fixed widths
                //Headertable.SetWidths(new float[] { 1f, 7f });//relative column widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);

                if (ReportEntityList.Count > 0)
                {
                    Graphtable = new PdfPTable(2);
                    Graphtable.TotalWidth = 820f;
                    Graphtable.LockedWidth = true;
                    Graphtable.SetWidths(new float[] { 1f, 1f });

                    Image pdfImage1 = Image.GetInstance(GraphImageResponseSummaryByHourReport1(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage1, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);

                    Image pdfImage2 = Image.GetInstance(GraphImageResponseSummaryByHourReport2(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage2, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);
                    document.Add(Graphtable);

                    Datatable = new PdfPTable(5);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 6f, 1f, 3f, 1f });
                    Datatable.SpacingBefore = 15f;

                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Answered Calls"));
                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered Calls"));
                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    document.Add(Datatable);

                    Datatable = new PdfPTable(11);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1.5f, 1.5f, 1f, 1.5f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Hour"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Avg. Ring"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Long Ring"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Within " + RingTimeThreshold + " Sec Total"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Within " + RingTimeThreshold + " Sec %"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered Calls"));
                    //Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Avg. Ring"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Long Ring"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Total Calls"));

                    List<ReportResponseSummaryByHourEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                    .Select(y => new ReportResponseSummaryByHourEntity
                    {
                        TotalCalls = y.Sum(d => d.TotalCalls),
                        AnsweredCalls = y.Sum(d => d.AnsweredCalls),
                        //LostCalls = y.Sum(d => d.LostCalls),
                        UnAnsweredCalls = y.Sum(d => d.UnAnsweredCalls),
                    // Math.Round(Convert.ToDouble(row["Cost"].ToString()), 2);
                    AvgRingLost = Math.Round(y.Average(d => d.AvgRingLost), 1),
                        AvgRingAnswered = Math.Round(y.Average(d => d.AvgRingAnswered), 1),
                        MaxRingAnswered = y.Max(d => d.MaxRingAnswered),
                        MaxRingLost = y.Max(d => d.MaxRingLost),
                        WithinThresholdCount = y.Sum(d => d.WithinThresholdCount),
                        TotalRingAnswered = y.Sum(d => d.TotalRingAnswered),
                        TotalRingUnAnswered = y.Sum(d => d.TotalRingUnAnswered),

                    }).ToList();


                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportResponseSummaryByHourEntity obj = ReportEntityList[i];

                        string WithinRingTimePercent = "0";
                        if (obj.AnsweredCalls != 0)
                        {
                            WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(obj.WithinThresholdCount) / Convert.ToDouble(obj.AnsweredCalls) * 100), 0).ToString();
                        }

                        if (i == ReportEntityList.Count - 1)
                        {
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(ReportsCommonMethods.GetHourFormat(obj.Hour)));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.AnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.AvgRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.MaxRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.WithinThresholdCount.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(WithinRingTimePercent+"%"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.UnAnsweredCalls.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.LostCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.AvgRingLost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.MaxRingLost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBold(obj.TotalCalls.ToString()));

                            ReportResponseSummaryByHourEntity obj1 = TempReportEntityGroupedList[0];
                            if (obj1.AnsweredCalls != 0)
                            {
                                WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(obj1.WithinThresholdCount) / Convert.ToDouble(obj1.AnsweredCalls) * 100), 0).ToString();
                            }
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.AnsweredCalls.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.AvgRingAnswered.ToString()));
                            string val = (Math.Round((double)obj1.TotalRingAnswered / (double)obj1.AnsweredCalls)).ToString();
                            val = val == "NaN" ? "0" : val;
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(val));                            
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.MaxRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.WithinThresholdCount.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(WithinRingTimePercent + "%"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.UnAnsweredCalls.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.LostCalls.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.AvgRingLost.ToString()));
                            val = (Math.Round((double)obj1.TotalRingUnAnswered / (double)obj1.UnAnsweredCalls)).ToString();
                            val = val == "NaN" ? "0" : val;
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(val));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.MaxRingLost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(obj1.TotalCalls.ToString()));
                           
                        }
                        else
                        {
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(ReportsCommonMethods.GetHourFormat(obj.Hour)));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.AnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.AvgRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.MaxRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.WithinThresholdCount.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(WithinRingTimePercent + "%"));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.UnAnsweredCalls.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.RowCell(obj.LostCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.AvgRingLost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.MaxRingLost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowRightCellBold(obj.TotalCalls.ToString()));
                        }
                    }
                    document.Add(Datatable);
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }

                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateResponseSummaryByDayReport(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByDayEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 782f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 782 });//fixed widths
                //Headertable.SetWidths(new float[] { 1f, 7f });//relative column widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);


                if (ReportEntityList.Count > 0)
                {
                    Graphtable = new PdfPTable(2);
                    Graphtable.TotalWidth = 820f;
                    Graphtable.LockedWidth = true;
                    Graphtable.SetWidths(new float[] { 1f, 1f });


                    Image pdfImage1 = Image.GetInstance(GraphImageResponseSummaryByDayReport1(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage1, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);

                    Image pdfImage2 = Image.GetInstance(GraphImageResponseSummaryByDayReport2(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage2, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);
                    document.Add(Graphtable);

                    Datatable = new PdfPTable(5);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 2f, 6f, 1f, 3f, 1f });
                    Datatable.SpacingBefore = 15f;

                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Answered Calls"));
                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered Calls"));
                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    document.Add(Datatable);

                    Datatable = new PdfPTable(12);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f, 1.5f, 1.5f, 1f, 2f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Date"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Day"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Avg. Ring"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Long Ring"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Within " + RingTimeThreshold + " Sec Total"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Within " + RingTimeThreshold + " Sec %"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered Calls"));
                    //Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Avg. Ring"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Long Ring"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Total Calls"));

                    List<ReportResponseSummaryByDayEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                    .Select(y => new ReportResponseSummaryByDayEntity
                    {
                        TotalCalls = y.Sum(d => d.TotalCalls),
                        AnsweredCalls = y.Sum(d => d.AnsweredCalls),
                        //LostCalls = y.Sum(d => d.LostCalls),
                        UnAnsweredCalls = y.Sum(d => d.UnAnsweredCalls),
                        AvgRingLost = Math.Round(y.Average(d => d.AvgRingLost), 1),
                        AvgRingAnswered = Math.Round(y.Average(d => d.AvgRingAnswered), 1),
                        MaxRingAnswered = y.Max(d => d.MaxRingAnswered),
                        MaxRingLost = y.Max(d => d.MaxRingLost),
                        WithinThresholdCount = y.Sum(d => d.WithinThresholdCount),
                        TotalRingAnswered = y.Sum(d => d.TotalRingAnswered),
                        TotalRingUnAnswered = y.Sum(d => d.TotalRingUnAnswered),

                    }).ToList();

                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportResponseSummaryByDayEntity obj = ReportEntityList[i];

                        string WithinRingTimePercent = "0";
                        if (obj.AnsweredCalls != 0)
                        {
                            WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(obj.WithinThresholdCount) / Convert.ToDouble(obj.AnsweredCalls) * 100), 0).ToString();
                        }
                        if (i == ReportEntityList.Count - 1)
                        {
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Date));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Day));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.AnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.AvgRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.MaxRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.WithinThresholdCount.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(WithinRingTimePercent+"%"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.UnAnsweredCalls.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.LostCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.AvgRingLost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.MaxRingLost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBold(obj.TotalCalls.ToString()));

                            ReportResponseSummaryByDayEntity obj1 = TempReportEntityGroupedList[0];
                            if (obj1.AnsweredCalls != 0)
                            {
                                WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(obj1.WithinThresholdCount) / Convert.ToDouble(obj1.AnsweredCalls) * 100), 0).ToString();
                            }
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.AnsweredCalls.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.AvgRingAnswered.ToString()));
                            string val = (Math.Round((double)obj1.TotalRingAnswered / (double)obj1.AnsweredCalls)).ToString();
                            val = val == "NaN" ? "0" : val;
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(val));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.MaxRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.WithinThresholdCount.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(WithinRingTimePercent + "%"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.UnAnsweredCalls.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.LostCalls.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.AvgRingLost.ToString()));
                            val = (Math.Round((double)obj1.TotalRingUnAnswered / (double)obj1.UnAnsweredCalls)).ToString();
                            val = val == "NaN" ? "0" : val;
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(val));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.MaxRingLost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(obj1.TotalCalls.ToString()));
                        }
                        else
                        {
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Date));
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Day));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.AnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.AvgRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.MaxRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.WithinThresholdCount.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(WithinRingTimePercent + "%"));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.UnAnsweredCalls.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.RowCell(obj.LostCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.AvgRingLost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.MaxRingLost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowRightCellBold(obj.TotalCalls.ToString()));
                        }
                    }
                    document.Add(Datatable);
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }

                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateResponseSummaryByExtensionReport(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByExtensionEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();

                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 782f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 782 });//fixed widths
                //Headertable.SetWidths(new float[] { 1f, 7f });//relative column widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);


                if (ReportEntityList.Count > 0)
                {
                    Graphtable = new PdfPTable(2);
                    Graphtable.TotalWidth = 820f;
                    Graphtable.LockedWidth = true;
                    Graphtable.SetWidths(new float[] { 1f, 1f });

                    Image pdfImage1 = Image.GetInstance(GraphImageResponseSummaryByExtensionReport1(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage1, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);

                    Image pdfImage2 = Image.GetInstance(GraphImageResponseSummaryByExtensionReport2(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage2, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);
                    document.Add(Graphtable);

                    Datatable = new PdfPTable(5);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 6f, 1f, 3f, 1f });
                    Datatable.SpacingBefore = 15f;

                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Answered Calls"));
                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered Calls"));
                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    document.Add(Datatable);

                    Datatable = new PdfPTable(11);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1.5f, 1.5f, 1f, 1.5f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Extension"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Avg. Ring"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Long Ring"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Within " + RingTimeThreshold + " Sec Total"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Within " + RingTimeThreshold + " Sec %"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered Calls"));
                    //Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Avg. Ring"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Long Ring"));
                    //Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Calls Made"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Total Calls"));

                    List<ReportResponseSummaryByExtensionEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                   .Select(y => new ReportResponseSummaryByExtensionEntity
                   {
                       TotalCalls = y.Sum(d => d.TotalCalls),
                       AnsweredCalls = y.Sum(d => d.AnsweredCalls),
                       //LostCalls = y.Sum(d => d.LostCalls),
                       UnAnsweredCalls = y.Sum(d => d.UnAnsweredCalls),
                       AvgRingLost = Math.Round(y.Average(d => d.AvgRingLost), 1),
                       AvgRingAnswered = Math.Round(y.Average(d => d.AvgRingAnswered), 1),
                       MaxRingAnswered = y.Max(d => d.MaxRingAnswered),
                       MaxRingLost = y.Max(d => d.MaxRingLost),
                       WithinThresholdCount = y.Sum(d => d.WithinThresholdCount),
                       TotalRingAnswered = y.Sum(d => d.TotalRingAnswered),
                       TotalRingUnAnswered = y.Sum(d => d.TotalRingUnAnswered),

                   }).ToList();

                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportResponseSummaryByExtensionEntity obj = ReportEntityList[i];

                        string WithinRingTimePercent = "0";
                        if (obj.AnsweredCalls != 0)
                        {
                            WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(obj.WithinThresholdCount) / Convert.ToDouble(obj.AnsweredCalls) * 100), 0).ToString();
                        }
                        if (i == ReportEntityList.Count - 1)
                        {
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Extension));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.AnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.AvgRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.MaxRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.WithinThresholdCount.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(WithinRingTimePercent+"%"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.UnAnsweredCalls.ToString()));
                            ///Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.LostCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.AvgRingLost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.MaxRingLost.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.CallsMade.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBold(obj.TotalCalls.ToString()));

                            ReportResponseSummaryByExtensionEntity obj1 = TempReportEntityGroupedList[0];
                            if (obj1.AnsweredCalls != 0)
                            {
                                WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(obj1.WithinThresholdCount) / Convert.ToDouble(obj1.AnsweredCalls) * 100), 0).ToString();
                            }
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.AnsweredCalls.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.AvgRingAnswered.ToString()));
                            string val = (Math.Round((double)obj1.TotalRingAnswered / (double)obj1.AnsweredCalls)).ToString();
                            val = val == "NaN" ? "0" : val;
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(val));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.MaxRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.WithinThresholdCount.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(WithinRingTimePercent + "%"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.UnAnsweredCalls.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.LostCalls.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.AvgRingLost.ToString()));
                            val = (Math.Round((double)obj1.TotalRingUnAnswered / (double)obj1.UnAnsweredCalls)).ToString();
                            val = val == "NaN" ? "0" : val;
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(val));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.MaxRingLost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(obj1.TotalCalls.ToString()));
                        }
                        else
                        {
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Extension));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.AnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.AvgRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.MaxRingAnswered.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.WithinThresholdCount.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(WithinRingTimePercent + "%"));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.UnAnsweredCalls.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.RowCell(obj.LostCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.AvgRingLost.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.MaxRingLost.ToString()));
                            //Datatable.AddCell(ReportsCommonMethods.RowCell(obj.CallsMade.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowRightCellBold(obj.TotalCalls.ToString()));
                        }
                    }
                    document.Add(Datatable);
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }

                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateResponseSummaryByPhoneReport(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByPhoneEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                //Document document = new Document(PageSize.A4.Rotate(), 15f, 15f, 15f, 15f);
                Document document = new Document(PageSize.A4, 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 535f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 535 });//fixed widths
                //Headertable.SetWidths(new float[] { 1f, 7f });//relative column widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);

                //Graphtable = new PdfPTable(2);
                //Graphtable.TotalWidth = 782f;
                //Graphtable.LockedWidth = true;
                //Graphtable.SetWidths(new float[] { 1f, 1f });

                //Image pdfImage1 = GraphImageResponseSummaryByDayReport1(ReportEntityList);
                //cell = ReportsCommonMethods.GraphImageCell(pdfImage1, 100f, PdfPCell.ALIGN_CENTER);
                //Graphtable.AddCell(cell);
                //Image pdfImage2 = GraphImageResponseSummaryByDayReport2(ReportEntityList);
                //cell = ReportsCommonMethods.GraphImageCell(pdfImage2, 100f, PdfPCell.ALIGN_CENTER);
                //Graphtable.AddCell(cell);
                //document.Add(Graphtable);


                if (ReportEntityList.Count > 0)
                {
                    Datatable = new PdfPTable(3);
                    Datatable.TotalWidth = 535;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;
                  //  Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Date/Time"));     
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Dialled Number"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Duration"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Total Calls"));

                    List<ReportResponseSummaryByPhoneEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                     .Select(y => new ReportResponseSummaryByPhoneEntity
                     {
                         TotalCalls = y.Sum(d => d.TotalCalls),
                         Duration = y.Sum(d => d.Duration),
                     }).ToList();

                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportResponseSummaryByPhoneEntity obj = ReportEntityList[i];

                        if (i == ReportEntityList.Count - 1)
                        {
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.DialledNumber));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(obj.TotalCalls.ToString()));


                            ReportResponseSummaryByPhoneEntity obj1 = TempReportEntityGroupedList[0];
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration)));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(obj1.TotalCalls.ToString()));


                        }
                        else
                        {
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.DialledNumber));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));
                            Datatable.AddCell(ReportsCommonMethods.RowRightCell(obj.TotalCalls.ToString()));


                        }
                    }
                    document.Add(Datatable);
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }
                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateResponseSummaryByExtensionItemisedReport(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByExtensionItemisedEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable SubSectionHeadertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 782f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 782 });//fixed widths
                //Headertable.SetWidths(new float[] { 1f, 7f });//relative column widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);

                //Graphtable = new PdfPTable(2);
                //Graphtable.TotalWidth = 782f;
                //Graphtable.LockedWidth = true;
                //Graphtable.SetWidths(new float[] { 1f, 1f });

                //Image pdfImage1 = GraphImageResponseSummaryByDayReport1(ReportEntityList);
                //cell = ReportsCommonMethods.GraphImageCell(pdfImage1, 100f, PdfPCell.ALIGN_CENTER);
                //Graphtable.AddCell(cell);
                //Image pdfImage2 = GraphImageResponseSummaryByDayReport2(ReportEntityList);
                //cell = ReportsCommonMethods.GraphImageCell(pdfImage2, 100f, PdfPCell.ALIGN_CENTER);
                //Graphtable.AddCell(cell);
                //document.Add(Graphtable);
                if (ReportEntityList.Count > 0)
                {
                    List<ReportResponseSummaryByExtensionItemisedEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => x.Extension)
               .Select(y => new ReportResponseSummaryByExtensionItemisedEntity
               {
                   Extension = y.First().Extension,
                   Duration = y.Sum(d => d.Duration),
                   CallCount = y.Count(),
                   RingDuration = y.Sum(d => d.RingDuration)
               }).ToList();

                    List<List<ReportResponseSummaryByExtensionItemisedEntity>> ReportEntityGroupedList = ReportEntityList.GroupBy(x => new { x.Extension }).Select(y => y.ToList()).ToList();
                    foreach (List<ReportResponseSummaryByExtensionItemisedEntity> ReportEntityLists in ReportEntityGroupedList)
                    {
                        SubSectionHeadertable = new PdfPTable(1);
                        SubSectionHeadertable.TotalWidth = 782f;
                        SubSectionHeadertable.LockedWidth = true;
                        SubSectionHeadertable.SetTotalWidth(new float[] { 782 });//fixed widths

                        phrase = new Phrase();
                        phrase.Add(new Chunk("Extension: ", FontFactory.GetFont("Arial", 14, Font.BOLD, Color.BLACK)));
                        phrase.Add(new Chunk(ReportEntityLists[0].Extension, FontFactory.GetFont("Arial", 14, Font.NORMAL, Color.BLACK)));
                        cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                        cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                        cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                        cell.PaddingTop = 10f;
                        cell.PaddingBottom = 0f;
                        SubSectionHeadertable.AddCell(cell);
                        document.Add(SubSectionHeadertable);

                        Datatable = new PdfPTable(8);
                        Datatable.TotalWidth = 782;
                        Datatable.LockedWidth = true;
                        Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                        Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f });
                        Datatable.SpacingBefore = 15f;
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Date"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Time"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Duration"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Type"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("CLI"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("DDI"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Result"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Ring Duration"));
                        for (int i = 0; i < ReportEntityLists.Count; i++)
                        {
                            ReportResponseSummaryByExtensionItemisedEntity obj = ReportEntityLists[i];

                            if (i == ReportEntityLists.Count - 1)
                            {
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Date));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Time));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Direction));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.CLI));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.DDI));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(ReportsCommonMethods.GetCallStatus(int.Parse(obj.LastState), int.Parse(obj.InitialState), obj.Duration, obj.Direction)));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.RingDuration)));

                                ReportResponseSummaryByExtensionItemisedEntity obj1 = TempReportEntityGroupedList.Find(x => x.Extension == obj.Extension);
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total: " + obj1.CallCount.ToString() + ""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration)));

                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.RingDuration)));
                            }
                            else
                            {
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Date));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Time));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Direction));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.CLI));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.DDI));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(ReportsCommonMethods.GetCallStatus(int.Parse(obj.LastState), int.Parse(obj.InitialState), obj.Duration, obj.Direction)));
                                //Datatable.AddCell(ReportsCommonMethods.RowLeftCell( obj.LastState));
                                Datatable.AddCell(ReportsCommonMethods.RowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.RingDuration)));
                            }
                        }
                        document.Add(Datatable);
                    }
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }
                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateTrafficSummaryByHourReport(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByHourEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 782f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 782 });//fixed widths
                //Headertable.SetWidths(new float[] { 1f, 7f });//relative column widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);

                if (ReportEntityList.Count > 0)
                {
                    Graphtable = new PdfPTable(2);
                    Graphtable.TotalWidth = 820f;
                    Graphtable.LockedWidth = true;
                    Graphtable.SetWidths(new float[] { 1f, 1f });

                    Image pdfImage1 = Image.GetInstance(GraphImageTrafficSummaryByHourReport1(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage1, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);

                    Image pdfImage2 = Image.GetInstance(GraphImageTrafficSummaryByHourReport2(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage2, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);
                    document.Add(Graphtable);

                    Datatable = new PdfPTable(4);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 4f, 1f, 5f });
                    Datatable.SpacingBefore = 15f;

                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Inbound Calls"));
                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Outbound Calls"));
                    document.Add(Datatable);

                    Datatable = new PdfPTable(10);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Hour"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Answered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Duration"));

                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Answered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Duration"));
                   // Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Cost (" + Currency + ")"));

                    List<ReportTrafficSummaryByHourEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                    .Select(y => new ReportTrafficSummaryByHourEntity
                    {
                        IncomingCalls = y.Sum(d => d.IncomingCalls),
                        IncomingAnsweredCalls = y.Sum(d => d.IncomingAnsweredCalls),
                        IncomingUnAnsweredCalls = y.Sum(d => d.IncomingUnAnsweredCalls),
                        IncomingCallDuration = y.Sum(d => d.IncomingCallDuration),
                        OutgoingCalls = y.Sum(d => d.OutgoingCalls),
                        OutgoingAnsweredCalls = y.Sum(d => d.OutgoingAnsweredCalls),
                        OutgoingUnAnsweredCalls = y.Sum(d => d.OutgoingUnAnsweredCalls),
                        OutgoingCallDuration = y.Sum(d => d.OutgoingCallDuration),
                        Cost = y.Sum(d => d.Cost),


                    }).ToList();

                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportTrafficSummaryByHourEntity obj = ReportEntityList[i];
                        if (i == ReportEntityList.Count - 1)
                        {
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(ReportsCommonMethods.GetHourFormat(obj.Hour)));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.IncomingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.IncomingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.IncomingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration)));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration)));
                       //     Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(obj.Cost.ToString()));

                            ReportTrafficSummaryByHourEntity obj1 = TempReportEntityGroupedList[0];
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.IncomingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.IncomingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.IncomingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.IncomingCallDuration)));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.OutgoingCallDuration)));
                          //  Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(obj1.Cost.ToString()));



                        }
                        else
                        {
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(ReportsCommonMethods.GetHourFormat(obj.Hour)));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.IncomingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.IncomingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.IncomingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration)));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration)));
                       //     Datatable.AddCell(ReportsCommonMethods.RowRightCell(obj.Cost.ToString()));
                        }
                    }
                    document.Add(Datatable);
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }
                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateTrafficSummaryByDayReport(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByDayEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 782f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 782 });//fixed widths
                //Headertable.SetWidths(new float[] { 1f, 7f });//relative column widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);

                if (ReportEntityList.Count > 0)
                {
                    Graphtable = new PdfPTable(2);
                    Graphtable.TotalWidth = 820f;
                    Graphtable.LockedWidth = true;
                    Graphtable.SetWidths(new float[] { 1f, 1f });


                    Image pdfImage1 = Image.GetInstance(GraphImageTrafficSummaryByDayReport1(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage1, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);

                    Image pdfImage2 = Image.GetInstance(GraphImageTrafficSummaryByDayReport2(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage2, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);
                    document.Add(Graphtable);

                    Datatable = new PdfPTable(4);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 2f, 4f, 1f, 5f });
                    Datatable.SpacingBefore = 15f;

                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Inbound Calls"));
                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Outbound Calls"));
                    document.Add(Datatable);

                    Datatable = new PdfPTable(11);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Date"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Day"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Answered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Duration"));

                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Answered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Duration"));
                  //  Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Cost (" + Currency + ")"));

                    List<ReportTrafficSummaryByDayEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                     .Select(y => new ReportTrafficSummaryByDayEntity
                     {
                         IncomingCalls = y.Sum(d => d.IncomingCalls),
                         IncomingAnsweredCalls = y.Sum(d => d.IncomingAnsweredCalls),
                         IncomingUnAnsweredCalls = y.Sum(d => d.IncomingUnAnsweredCalls),
                         IncomingCallDuration = y.Sum(d => d.IncomingCallDuration),
                         OutgoingCalls = y.Sum(d => d.OutgoingCalls),
                         OutgoingAnsweredCalls = y.Sum(d => d.OutgoingAnsweredCalls),
                         OutgoingUnAnsweredCalls = y.Sum(d => d.OutgoingUnAnsweredCalls),
                         OutgoingCallDuration = y.Sum(d => d.OutgoingCallDuration),
                         Cost = y.Sum(d => d.Cost),


                     }).ToList();

                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportTrafficSummaryByDayEntity obj = ReportEntityList[i];
                        if (i == ReportEntityList.Count - 1)
                        {
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Date));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Day.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.IncomingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.IncomingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.IncomingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration)));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration)));
                          //  Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(obj.Cost.ToString()));

                            ReportTrafficSummaryByDayEntity obj1 = TempReportEntityGroupedList[0];
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.IncomingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.IncomingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.IncomingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.IncomingCallDuration)));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.OutgoingCallDuration)));
                           // Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(obj1.Cost.ToString()));

                        }
                        else
                        {
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Date));
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Day.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.IncomingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.IncomingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.IncomingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration)));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration)));
                          //  Datatable.AddCell(ReportsCommonMethods.RowRightCell(obj.Cost.ToString()));
                        }
                    }
                    document.Add(Datatable);
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }

                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateTrafficSummaryByExtensionReport(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByExtensionEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 782f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 782 });//fixed widths
                //Headertable.SetWidths(new float[] { 1f, 7f });//relative column widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);


                if (ReportEntityList.Count > 0)
                {
                    Graphtable = new PdfPTable(2);
                    Graphtable.TotalWidth = 820f;
                    Graphtable.LockedWidth = true;
                    Graphtable.SetWidths(new float[] { 1f, 1f });


                    Image pdfImage1 = Image.GetInstance(GraphImageTrafficSummaryByExtensionReport1(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage1, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);

                    Image pdfImage2 = Image.GetInstance(GraphImageTrafficSummaryByExtensionReport2(ReportEntityList).GetBuffer());
                    cell = ReportsCommonMethods.GraphImageCell(pdfImage2, 33.3f, PdfPCell.ALIGN_CENTER);
                    Graphtable.AddCell(cell);
                    document.Add(Graphtable);

                    Datatable = new PdfPTable(4);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 4f, 1f, 5f });
                    Datatable.SpacingBefore = 15f;

                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Inbound Calls"));
                    Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Outbound Calls"));
                    document.Add(Datatable);

                    Datatable = new PdfPTable(10);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Extension"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Answered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Duration"));

                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell(""));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Answered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Duration"));
                    //Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Cost (" + Currency + ")"));

                    List<ReportTrafficSummaryByExtensionEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                     .Select(y => new ReportTrafficSummaryByExtensionEntity
                     {
                         IncomingCalls = y.Sum(d => d.IncomingCalls),
                         IncomingAnsweredCalls = y.Sum(d => d.IncomingAnsweredCalls),
                         IncomingUnAnsweredCalls = y.Sum(d => d.IncomingUnAnsweredCalls),
                         IncomingCallDuration = y.Sum(d => d.IncomingCallDuration),
                         OutgoingCalls = y.Sum(d => d.OutgoingCalls),
                         OutgoingAnsweredCalls = y.Sum(d => d.OutgoingAnsweredCalls),
                         OutgoingUnAnsweredCalls = y.Sum(d => d.OutgoingUnAnsweredCalls),
                         OutgoingCallDuration = y.Sum(d => d.OutgoingCallDuration),
                         Cost = y.Sum(d => d.Cost),


                     }).ToList();

                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportTrafficSummaryByExtensionEntity obj = ReportEntityList[i];
                        if (i == ReportEntityList.Count - 1)
                        {
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Extension));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.IncomingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.IncomingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.IncomingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration)));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration)));
                           // Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(obj.Cost.ToString()));

                            ReportTrafficSummaryByExtensionEntity obj1 = TempReportEntityGroupedList[0];
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.IncomingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.IncomingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.IncomingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.IncomingCallDuration)));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.OutgoingCallDuration)));
                           // Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(obj1.Cost.ToString()));

                        }
                        else
                        {
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Extension));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.IncomingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.IncomingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.IncomingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration)));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(""));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration)));
                          //  Datatable.AddCell(ReportsCommonMethods.RowRightCell(obj.Cost.ToString()));
                        }
                    }
                    document.Add(Datatable);
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }
                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateTrafficSummaryByPhoneReport(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByPhoneEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 782f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 782 });//fixed widths
                //Headertable.SetWidths(new float[] { 1f, 7f });//relative column widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);


                if (ReportEntityList.Count > 0)
                {
                    Datatable = new PdfPTable(5);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Dialled Number"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Answered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Duration"));
                 //   Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Cost (" + Currency + ")"));


                    List<ReportTrafficSummaryByPhoneEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                     .Select(y => new ReportTrafficSummaryByPhoneEntity
                     {
                         OutgoingCalls = y.Sum(d => d.OutgoingCalls),
                         OutgoingAnsweredCalls = y.Sum(d => d.OutgoingAnsweredCalls),
                         OutgoingUnAnsweredCalls = y.Sum(d => d.OutgoingUnAnsweredCalls),
                         OutgoingCallDuration = y.Sum(d => d.OutgoingCallDuration),
                         Cost = y.Sum(d => d.Cost),


                     }).ToList();

                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportTrafficSummaryByPhoneEntity obj = ReportEntityList[i];

                        if (i == ReportEntityList.Count - 1)
                        {
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.DialledNumber));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration)));
                           // Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(obj.Cost.ToString()));

                            ReportTrafficSummaryByPhoneEntity obj1 = TempReportEntityGroupedList[0];
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.OutgoingCallDuration)));
                          //  Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(obj1.Cost.ToString()));
                        }
                        else
                        {
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.DialledNumber));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration)));
                        //    Datatable.AddCell(ReportsCommonMethods.RowRightCell(obj.Cost.ToString()));
                        }
                    }
                    document.Add(Datatable);
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }
                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateTrafficSummaryByExtensionItemisedReport(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByExtensionItemisedEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable SubSectionHeadertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 782f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 782 });//fixed widths
                //Headertable.SetWidths(new float[] { 1f, 7f });//relative column widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);

                //Graphtable = new PdfPTable(2);
                //Graphtable.TotalWidth = 782f;
                //Graphtable.LockedWidth = true;
                //Graphtable.SetWidths(new float[] { 1f, 1f });

                //Image pdfImage1 = GraphImageTrafficSummaryByDayReport1(ReportEntityList);
                //cell = ReportsCommonMethods.GraphImageCell(pdfImage1, 100f, PdfPCell.ALIGN_CENTER);
                //Graphtable.AddCell(cell);
                //Image pdfImage2 = GraphImageTrafficSummaryByDayReport2(ReportEntityList);
                //cell = ReportsCommonMethods.GraphImageCell(pdfImage2, 100f, PdfPCell.ALIGN_CENTER);
                //Graphtable.AddCell(cell);
                //document.Add(Graphtable);

                if (ReportEntityList.Count > 0)
                {
                    List<ReportTrafficSummaryByExtensionItemisedEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => x.Extension)
               .Select(y => new ReportTrafficSummaryByExtensionItemisedEntity
               {
                   Extension = y.First().Extension,
                   Duration = y.Sum(d => d.Duration),
                   Cost = y.Sum(d => d.Cost),
                   CallCount = y.Count(),
                   InboundCallCount = y.Count(d => d.Direction == "Inbound"),
                   OutbountCallCount = y.Count(d => d.Direction == "Outbound"),

               }).ToList();

                    List<List<ReportTrafficSummaryByExtensionItemisedEntity>> ReportEntityGroupedList = ReportEntityList.OrderBy(y => y.Date).GroupBy(x => x.Extension).Select(y => y.ToList()).ToList();
                    foreach (List<ReportTrafficSummaryByExtensionItemisedEntity> ReportEntityLists in ReportEntityGroupedList)
                    {
                        SubSectionHeadertable = new PdfPTable(1);
                        SubSectionHeadertable.TotalWidth = 782f;
                        SubSectionHeadertable.LockedWidth = true;
                        SubSectionHeadertable.SetTotalWidth(new float[] { 782 });//fixed widths

                        phrase = new Phrase();
                        phrase.Add(new Chunk("Extension: ", FontFactory.GetFont("Arial", 14, Font.BOLD, Color.BLACK)));
                        phrase.Add(new Chunk(ReportEntityLists[0].Extension, FontFactory.GetFont("Arial", 14, Font.NORMAL, Color.BLACK)));
                        cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                        cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                        cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                        cell.PaddingTop = 10f;
                        cell.PaddingBottom = 0f;
                        SubSectionHeadertable.AddCell(cell);
                        document.Add(SubSectionHeadertable);

                        Datatable = new PdfPTable(6);
                        Datatable.TotalWidth = 782;
                        Datatable.LockedWidth = true;
                        Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                        Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f, 1f });
                        Datatable.SpacingBefore = 15f;
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Date"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Time"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Duration"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Type"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("CLI"));
                        Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("DDI"));
                        //Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Cost (" + Currency + ")"));
                        for (int i = 0; i < ReportEntityLists.Count; i++)
                        {
                            ReportTrafficSummaryByExtensionItemisedEntity obj = ReportEntityLists[i];

                            if (i == ReportEntityLists.Count - 1)
                            {
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Date));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Time));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Direction));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.CLI));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.DDI));
                              //  Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(obj.Cost.ToString()));


                                ReportTrafficSummaryByExtensionItemisedEntity obj1 = TempReportEntityGroupedList.Find(x => x.Extension == obj.Extension);
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total: " + obj1.CallCount.ToString() + ""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration)));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored("Inbound:" + obj1.InboundCallCount + ""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Outbound:" + obj1.OutbountCallCount + ""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored(""));
                              //  Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(obj1.Cost.ToString()));
                            }
                            else
                            {
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Date));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Time));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration)));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Direction));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.CLI));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.DDI));
                             //   Datatable.AddCell(ReportsCommonMethods.RowRightCell(obj.Cost.ToString()));
                            }
                        }
                        document.Add(Datatable);
                    }
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }

                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }

        public static string GenerateCallTrailReport(string ReportName, List<ReportCallTrailEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                //Document document = new Document(PageSize.A4.Rotate(), 15f, 15f, 15f, 15f);595
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 738f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 738 });//fixed widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/" + ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);

                Log4Net.WriteLog("Call Trail Rport parameter set", LogType.GENERALLOG);



                Log4Net.WriteLog("Call Trail List Length:: " + ReportEntityList.Count + "", LogType.GENERALLOG);

                if (ReportEntityList.Count > 0)
                {

                    Datatable = new PdfPTable(8);
                    Datatable.TotalWidth = 738f;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 2f, 1f, 1f, 1f, 1f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;

                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("StartTime"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("CLI"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("DDI"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Answered By"));
                    // Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Direction"));

                    // Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("StartTime"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Hold Duration"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Conversation"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Ring Duration"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Total Duration"));


                    Log4Net.WriteLog("Call Trail calling loop", LogType.GENERALLOG);
                    bool isLastGroup = false;
                    int LastBoldIndex = -1;
                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        // string OrignalNumber = string.Empty;
                        ReportCallTrailEntity obj = ReportEntityList[i];

                        if (obj.Status == "M")
                        {
                            LastBoldIndex = i;
                            if (i > 0 && !isLastGroup)
                            {
                                Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                                Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                                Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                                Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                                // Datatable.AddCell(ReportsCommonMethods.RowLeftCell(""));

                                Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                                Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                                Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                                Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                            }
                            isLastGroup = true;
                            string MSerial = obj.MSerial;
                            //  OrignalNumber = obj.OrignalNumber;
                            if (i == ReportEntityList.Count - 1)
                            {
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBoldBGColoredTrail(obj.MStartTime.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBoldBGColoredTrail(obj.MCLI));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBoldBGColoredTrail(obj.MDDI));

                                string AnswerBy = obj.MExtension;
                                if (obj.MDirection == "Inbound")
                                    AnswerBy = obj.MExtension;
                                else
                                    AnswerBy = obj.MDDI;
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBoldBGColoredTrail(AnswerBy));
                                // Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBoldBGColored(obj.MDirection));

                                //  Datatable.AddCell(ReportsCommonMethods.BottomRowCellBold(obj.MStartTime.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBoldBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MHoldDuration)));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBoldBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalConversation)));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBoldBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalRingDuration)));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBoldBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalDuration)));

                            }
                            else
                            {
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCellBoldBGColoredTrail(obj.MStartTime.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCellBoldBGColoredTrail(obj.MCLI));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCellBoldBGColoredTrail(obj.MDDI));

                                string AnswerBy = obj.MExtension;
                                if (obj.MDirection == "Inbound")
                                    AnswerBy = obj.MExtension;
                                else
                                    AnswerBy = obj.MDDI;
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCellBoldBGColoredTrail(AnswerBy));

                                //   Datatable.AddCell(ReportsCommonMethods.RowLeftCellBoldBGColored(obj.MDirection));

                                //  Datatable.AddCell(ReportsCommonMethods.RowCellBold(obj.MStartTime.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.RowCellBoldBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MHoldDuration)));
                                Datatable.AddCell(ReportsCommonMethods.RowCellBoldBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalConversation)));
                                Datatable.AddCell(ReportsCommonMethods.RowCellBoldBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalRingDuration)));
                                Datatable.AddCell(ReportsCommonMethods.RowRightCellBoldBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalDuration)));
                            }

                            Log4Net.WriteLog("Call Trail calling loop within loop", LogType.GENERALLOG);
                            //string StartTime = DateTime.Now;
                            while (i < ReportEntityList.Count && ReportEntityList[i].MSerial == MSerial)
                            {
                                // i++;
                                obj = ReportEntityList[i];
                                //int j = i + 1;
                                //if (j < ReportEntityList.Count && obj.StartTime == ReportEntityList[j].StartTime && MSerial == obj.MSerial && MSerial == ReportEntityList[j].MSerial)
                                //{
                                //    i += 2;
                                //    continue;
                                //}

                                // if (obj.StartTime != StartTime)
                                {

                                    Log4Net.WriteLog("MSerial:" + MSerial + " Direction:" + obj.direction + " CLI:" + obj.CLI + " DDI:" + obj.DDI + " Extension:" + obj.Extension, LogType.GENERALLOG);
                                    //  StartTime = obj.StartTime;
                                    if (i == ReportEntityList.Count - 1)
                                    {
                                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColoredTrail(obj.StartTime, true));
                                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColoredTrail(obj.CLI, true));
                                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColoredTrail(obj.DDI, true));

                                        string AnswerBy = obj.Extension;
                                        if (obj.Direction == "Inbound")
                                            AnswerBy = obj.Extension;
                                        else
                                            AnswerBy = obj.DDI;
                                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColoredTrail(AnswerBy, true));

                                        //  Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored(obj.Direction));

                                        //string tempOrignalNumber = obj.OrignalNumber;
                                        //if (OrignalNumber != null && OrignalNumber.Trim().Length > 0 && tempOrignalNumber != null && tempOrignalNumber == OrignalNumber)
                                        //    Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Consulting"));
                                        //else
                                        //    Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Transferred"));

                                        //  Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj.StartTime.ToString()));
                                        Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.HoldDuration), true));
                                        Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.TotalConversation), true));
                                        Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.TotalRingDuration), true));
                                        Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.TotalDuration), true));

                                    }
                                    else
                                    {
                                        Datatable.AddCell(ReportsCommonMethods.RowLeftCellBGColoredTrail(obj.StartTime.ToString(), true));
                                        Datatable.AddCell(ReportsCommonMethods.RowLeftCellBGColoredTrail(obj.CLI, true));
                                        Datatable.AddCell(ReportsCommonMethods.RowLeftCellBGColoredTrail(obj.DDI, true));

                                        string AnswerBy = obj.Extension;
                                        if (obj.Direction == "Inbound")
                                            AnswerBy = obj.Extension;
                                        else
                                            AnswerBy = obj.DDI;
                                        Datatable.AddCell(ReportsCommonMethods.RowLeftCellBGColoredTrail(AnswerBy, true));

                                        // Datatable.AddCell(ReportsCommonMethods.RowLeftCellBGColored(obj.Direction));


                                        //string tempOrignalNumber = obj.OrignalNumber;
                                        //if (OrignalNumber != null && OrignalNumber.Trim().Length > 0 && tempOrignalNumber != null && tempOrignalNumber == OrignalNumber)
                                        //    Datatable.AddCell(ReportsCommonMethods.RowLeftCellBGColored("Consulting"));
                                        //else
                                        //    Datatable.AddCell(ReportsCommonMethods.RowLeftCellBGColored("Transferred"));

                                        //  Datatable.AddCell(ReportsCommonMethods.RowCellBold(obj.StartTime.ToString()));
                                        Datatable.AddCell(ReportsCommonMethods.RowCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.HoldDuration), true));
                                        Datatable.AddCell(ReportsCommonMethods.RowCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.TotalConversation), true));
                                        Datatable.AddCell(ReportsCommonMethods.RowCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.TotalRingDuration), true));
                                        Datatable.AddCell(ReportsCommonMethods.RowRightCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.TotalDuration), true));
                                    }
                                }
                                int j = i + 1;
                                if (j < ReportEntityList.Count && obj.StartTime == ReportEntityList[j].StartTime && MSerial == obj.MSerial && MSerial == ReportEntityList[j].MSerial)
                                {
                                    i += 2;
                                    continue;
                                }
                                if ((i + 1) < ReportEntityList.Count && ReportEntityList[i + 1].MSerial == MSerial)
                                    i++;
                                else
                                    break;
                            }


                            Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                            Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                            Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                            Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                            //    Datatable.AddCell(ReportsCommonMethods.RowLeftCell(""));

                            Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                            Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                            Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));
                            Datatable.AddCell(ReportsCommonMethods.EmptyRowLeftCellTrail(""));

                        }
                        else
                        {
                            isLastGroup = false;
                            Log4Net.WriteLog("Call Trail Single call found", LogType.GENERALLOG);

                            if (i == ReportEntityList.Count - 1)
                            {
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColoredTrail(obj.MStartTime, false));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColoredTrail(obj.MCLI, false));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColoredTrail(obj.MDDI, false));

                                string AnswerBy = obj.MExtension;
                                if (obj.MDirection == "Inbound")
                                    AnswerBy = obj.MExtension;
                                else
                                    AnswerBy = obj.MDDI;
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColoredTrail(AnswerBy, false));

                                //    Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.MDirection));

                                //  Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.MStartTime.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MHoldDuration), false));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalConversation), false));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalRingDuration), false));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalDuration), false));

                            }
                            else
                            {
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCellBGColoredTrail(obj.MStartTime.ToString(), false));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCellBGColoredTrail(obj.MCLI, false));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCellBGColoredTrail(obj.MDDI, false));

                                string AnswerBy = obj.MExtension;
                                if (obj.MDirection == "Inbound")
                                    AnswerBy = obj.MExtension;
                                else
                                    AnswerBy = obj.MDDI;
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCellBGColoredTrail(AnswerBy, false));

                                //    Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.MDirection));

                                //  Datatable.AddCell(ReportsCommonMethods.RowCellBGColored(obj.MStartTime.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.RowCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MHoldDuration), false));
                                Datatable.AddCell(ReportsCommonMethods.RowCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalConversation), false));
                                Datatable.AddCell(ReportsCommonMethods.RowCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalRingDuration), false));
                                Datatable.AddCell(ReportsCommonMethods.RowRightCellBGColoredTrail(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalDuration), false));

                            }
                        }
                    }

                    Log4Net.WriteLog("Call Trail calling loop ended", LogType.GENERALLOG);

                    document.Add(Datatable);

                }
                else
                {
                    Log4Net.WriteLog("Call Trail no record found", LogType.GENERALLOG);

                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }
                Log4Net.WriteLog("Call Trail closing document", LogType.GENERALLOG);


                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateCallTrailReport_(string ReportName, List<ReportCallTrailEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                //Document document = new Document(PageSize.A4.Rotate(), 15f, 15f, 15f, 15f);595
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 738f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 738 });//fixed widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/"+ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);

                Log4Net.WriteLog("Call Trail Rport parameter set", LogType.GENERALLOG);



                Log4Net.WriteLog("Call Trail List Length:: " + ReportEntityList.Count + "", LogType.GENERALLOG);
                if (ReportEntityList.Count > 0)
                {

                    Datatable = new PdfPTable(9);
                    Datatable.TotalWidth = 738f;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 2f, 1f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;

                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Extension"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Direction"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("CLI"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("DDI"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("StartTime"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Hold Duration"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Conversation"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Ring Duration"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Total Duration"));


                    Log4Net.WriteLog("Call Trail calling loop", LogType.GENERALLOG);

                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportCallTrailEntity obj = ReportEntityList[i];

                        if (obj.Status == "M")
                        {
                            string MSerial = obj.MSerial;
                            if (i == ReportEntityList.Count - 1)
                            {
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.MExtension));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.MDirection));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.MCLI));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.MDDI));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.MStartTime.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MHoldDuration)));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalConversation)));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalRingDuration)));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalDuration)));

                            }
                            else
                            {
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.MExtension));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.MDirection));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.MCLI));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.MDDI));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(obj.MStartTime.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MHoldDuration)));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalConversation)));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalRingDuration)));
                                Datatable.AddCell(ReportsCommonMethods.RowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalDuration)));
                            }

                            Log4Net.WriteLog("Call Trail calling loop within loop", LogType.GENERALLOG);

                            while (ReportEntityList[i].MSerial == MSerial)
                            {
                                obj = ReportEntityList[i];
                                if (i == ReportEntityList.Count - 1)
                                {
                                    Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBold(obj.Extension));
                                    Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBold(obj.Direction));
                                    Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBold(obj.CLI));
                                    Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBold(obj.DDI));
                                    Datatable.AddCell(ReportsCommonMethods.BottomRowCellBold(obj.StartTime.ToString()));
                                    Datatable.AddCell(ReportsCommonMethods.BottomRowCellBold(ReportsCommonMethods.GetTimeFromSeconds(obj.HoldDuration)));
                                    Datatable.AddCell(ReportsCommonMethods.BottomRowCellBold(ReportsCommonMethods.GetTimeFromSeconds(obj.TotalConversation)));
                                    Datatable.AddCell(ReportsCommonMethods.BottomRowCellBold(ReportsCommonMethods.GetTimeFromSeconds(obj.TotalRingDuration)));
                                    Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBold(ReportsCommonMethods.GetTimeFromSeconds(obj.TotalDuration)));

                                }
                                else
                                {
                                    Datatable.AddCell(ReportsCommonMethods.RowLeftCellBold(obj.Extension));
                                    Datatable.AddCell(ReportsCommonMethods.RowLeftCellBold(obj.Direction));
                                    Datatable.AddCell(ReportsCommonMethods.RowLeftCellBold(obj.CLI));
                                    Datatable.AddCell(ReportsCommonMethods.RowLeftCellBold(obj.DDI));
                                    Datatable.AddCell(ReportsCommonMethods.RowCellBold(obj.StartTime.ToString()));
                                    Datatable.AddCell(ReportsCommonMethods.RowCellBold(ReportsCommonMethods.GetTimeFromSeconds(obj.HoldDuration)));
                                    Datatable.AddCell(ReportsCommonMethods.RowCellBold(ReportsCommonMethods.GetTimeFromSeconds(obj.TotalConversation)));
                                    Datatable.AddCell(ReportsCommonMethods.RowCellBold(ReportsCommonMethods.GetTimeFromSeconds(obj.TotalRingDuration)));
                                    Datatable.AddCell(ReportsCommonMethods.RowRightCellBold(ReportsCommonMethods.GetTimeFromSeconds(obj.TotalDuration)));
                                }
                                i++;
                            }
                        }
                        else
                        {

                            Log4Net.WriteLog("Call Trail Single call found", LogType.GENERALLOG);

                            if (i == ReportEntityList.Count - 1)
                            {
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.MExtension));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.MDirection));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.MCLI));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.MDDI));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.MStartTime.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MHoldDuration)));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalConversation)));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalRingDuration)));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalDuration)));

                            }
                            else
                            {
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.MExtension));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.MDirection));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.MCLI));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.MDDI));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(obj.MStartTime.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MHoldDuration)));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalConversation)));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalRingDuration)));
                                Datatable.AddCell(ReportsCommonMethods.RowRightCell(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalDuration)));

                            }
                        }
                    }

                    Log4Net.WriteLog("Call Trail calling loop ended", LogType.GENERALLOG);

                    document.Add(Datatable);

                }
                else
                {
                    Log4Net.WriteLog("Call Trail no record found", LogType.GENERALLOG);

                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }
                Log4Net.WriteLog("Call Trail closing document", LogType.GENERALLOG);


                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateTrafficSummaryByAreaCodeOutboundReport(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByAreaCodeOutboundEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 782f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 782 });//fixed widths
                //Headertable.SetWidths(new float[] { 1f, 7f });//relative column widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/" + ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);


                if (ReportEntityList.Count > 0)
                {
                    Graphtable = new PdfPTable(2);
                    Graphtable.TotalWidth = 820f;
                    Graphtable.LockedWidth = true;
                    Graphtable.SetWidths(new float[] { 1f, 1f });

                    Datatable = new PdfPTable(2);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 5f });
                    Datatable.SpacingBefore = 15f;

                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Outbound Calls"));
                    document.Add(Datatable);

                    Datatable = new PdfPTable(6);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;
                   
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Area Code"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Area"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Answered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Duration"));
                    //Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Cost (" + Currency + ")"));

                    List<ReportTrafficSummaryByAreaCodeOutboundEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                     .Select(y => new ReportTrafficSummaryByAreaCodeOutboundEntity
                     {
                        
                         OutgoingCalls = y.Sum(d => d.OutgoingCalls),
                         OutgoingAnsweredCalls = y.Sum(d => d.OutgoingAnsweredCalls),
                         OutgoingUnAnsweredCalls = y.Sum(d => d.OutgoingUnAnsweredCalls),
                         OutgoingCallDuration = y.Sum(d => d.OutgoingCallDuration),


                     }).ToList();

                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportTrafficSummaryByAreaCodeOutboundEntity obj = ReportEntityList[i];
                        if (i == ReportEntityList.Count - 1)
                        {
                            
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.AreaCode.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.AreaDescription.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration)));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(obj.Cost.ToString()));

                            ReportTrafficSummaryByAreaCodeOutboundEntity obj1 = TempReportEntityGroupedList[0];
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored(""));
                            
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.OutgoingCallDuration)));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(obj1.Cost.ToString()));

                        }
                        else
                        {
                            
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.AreaCode.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.AreaDescription.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.OutgoingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration)));
                            //Datatable.AddCell(ReportsCommonMethods.RowRightCell(obj.Cost.ToString()));
                        }
                    }
                    document.Add(Datatable);
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }
                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateTrafficSummaryByAreaCodeInboundReport(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByAreaCodeInboundEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Graphtable = null;
                PdfPTable Datatable = null;
                document.Open();


                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 782f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 782 });//fixed widths
                //Headertable.SetWidths(new float[] { 1f, 7f });//relative column widths
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                cell = ReportsCommonMethods.ImageCell("~/" + ReportsDALObj.GetCompanyLogo(), 50f, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(ReportName, FontFactory.GetFont("Arial", 18, Font.BOLD, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                phrase.Add(new Chunk("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern), FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Time Between: " + timeFrom + "  and: " + timeTo, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                cell = ReportsCommonMethods.PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                Headertable.AddCell(cell);

                document.Add(Headertable);


                if (ReportEntityList.Count > 0)
                {
                    Graphtable = new PdfPTable(2);
                    Graphtable.TotalWidth = 820f;
                    Graphtable.LockedWidth = true;
                    Graphtable.SetWidths(new float[] { 1f, 1f });

                    Datatable = new PdfPTable(2);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 5f });
                    Datatable.SpacingBefore = 15f;

                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Inbound Calls"));
                    document.Add(Datatable);

                    Datatable = new PdfPTable(6);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f, 1f });
                    Datatable.SpacingBefore = 15f;

                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Area Code"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Area"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Total Calls"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Answered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Unanswered"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Duration"));
                    //Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Cost (" + Currency + ")"));

                    List<ReportTrafficSummaryByAreaCodeInboundEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                     .Select(y => new ReportTrafficSummaryByAreaCodeInboundEntity
                     {

                         IncomingCalls = y.Sum(d => d.IncomingCalls),
                         IncomingAnsweredCalls = y.Sum(d => d.IncomingAnsweredCalls),
                         IncomingUnAnsweredCalls = y.Sum(d => d.IncomingUnAnsweredCalls),
                         IncomingCallDuration = y.Sum(d => d.IncomingCallDuration),


                     }).ToList();

                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        ReportTrafficSummaryByAreaCodeInboundEntity obj = ReportEntityList[i];
                        if (i == ReportEntityList.Count - 1)
                        {

                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.AreaCode.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.AreaDescription.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.IncomingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.IncomingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.IncomingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration)));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(obj.Cost.ToString()));

                            ReportTrafficSummaryByAreaCodeInboundEntity obj1 = TempReportEntityGroupedList[0];
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Total"));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored(""));

                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.IncomingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.IncomingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(obj1.IncomingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(obj1.IncomingCallDuration)));
                            //Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(obj1.Cost.ToString()));

                        }
                        else
                        {

                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.AreaCode.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.AreaDescription.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.IncomingCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.IncomingAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(obj.IncomingUnAnsweredCalls.ToString()));
                            Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration)));
                            //Datatable.AddCell(ReportsCommonMethods.RowRightCell(obj.Cost.ToString()));
                        }
                    }
                    document.Add(Datatable);
                }
                else
                {
                    document.Add(ReportsCommonMethods.EmptyDataTable());
                }
                document.Close();
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }

        //****************************************************************************//
        public static string GenerateCostSummaryByRegionReportCSV(string Currency, string ReportName, List<ReportCostSummaryByTypeEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Length >= 31 ? ReportName.Substring(0, 30) : ReportName);// IXLWorksheet ws = wb.Worksheets.Add(ReportName);
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {


                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row = 4;
                    ws.Range("A1:E3").Merge();
                }

                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                ws.Range("A4:E4").Merge();
                Row = 5;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                ws.Range("A5:E5").Merge();
                Row = 6;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                ws.Range("A6:E6").Merge();
                Row = 7;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                ws.Range("A7:E7").Merge();
                Row = 9;

                var Graphimage1 = ws.AddPicture(GraphImageCostSummaryByRegionReport1(ReportEntityList));
                Graphimage1.MoveTo(ws.Cell(Row, 1).Address);
                Graphimage1.Scale(.3);

                var Graphimage2 = ws.AddPicture(GraphImageCostSummaryByRegionReport2(ReportEntityList));
                Graphimage2.MoveTo(ws.Cell(Row, 5).Address);
                Graphimage2.Scale(.3);
                // optional: resize picture
                Row = 24;
                ws.Range("A8:H22").Merge();


                Headercell = ws.Cell(Row, 1).SetValue("Call Type");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Total Cost (" + Currency + ")");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Duration");
                Headercell.Style.Font.Bold = true;

                List<ReportCostSummaryByTypeEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                .Select(y => new ReportCostSummaryByTypeEntity
                {
                    Duration = y.Sum(d => d.Duration),
                    Cost = y.Sum(d => d.Cost),
                    TotalCalls = y.Sum(d => d.TotalCalls),

                }).ToList();

                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    ReportCostSummaryByTypeEntity obj = ReportEntityList[i];
                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.CostType);
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.Cost.ToString());
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.TotalCalls.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration));

                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportCostSummaryByTypeEntity obj1 = TempReportEntityGroupedList[0];
                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue(obj1.Cost.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(obj1.TotalCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");

                        Row++;
                    }

                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateCostSummaryByExtensionReportCSV(string Currency, string ReportName, List<ReportCostSummaryByExtensionEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Length >= 31 ? ReportName.Substring(0, 30) : ReportName);//IXLWorksheet ws = wb.Worksheets.Add(ReportName);
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);
                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }
                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                ws.Range("A4:E4").Merge();
                Row = 5;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                ws.Range("A5:E5").Merge();
                Row = 6;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                ws.Range("A6:E6").Merge();
                Row = 7;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                ws.Range("A7:E7").Merge();
                Row = 9;

                var Graphimage1 = ws.AddPicture(GraphImageCostSummaryByExtensionReport(ReportEntityList));
                Graphimage1.MoveTo(ws.Cell(Row, 1).Address);
                Graphimage1.Scale(.3);

                //var Graphimage2 = ws.AddPicture(GraphImageCostSummaryByRegionReport2(ReportEntityList));
                //Graphimage2.MoveTo(ws.Cell(Row, 5).Address);
                //Graphimage2.Scale(.3);
                // optional: resize picture
                Row = 24;
                ws.Range("A8:H22").Merge();

                Headercell = ws.Cell(Row, 1).SetValue("Extension");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Name");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Total Cost (" + Currency + ")");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("Duration");
                Headercell.Style.Font.Bold = true;


                List<ReportCostSummaryByExtensionEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
               .Select(y => new ReportCostSummaryByExtensionEntity
               {
                   Duration = y.Sum(d => d.Duration),
                   Cost = y.Sum(d => d.Cost),
                   TotalCalls = y.Sum(d => d.TotalCalls),

               }).ToList();

                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    ReportCostSummaryByExtensionEntity obj = ReportEntityList[i];
                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.Extension);
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.Name);
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.Cost.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(obj.TotalCalls.ToString());
                    BodyCell = ws.Cell(Row, 5).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration));

                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportCostSummaryByExtensionEntity obj1 = TempReportEntityGroupedList[0];
                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(obj1.Cost.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(obj1.TotalCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 5).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");

                        Row++;
                    }

                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateCostSummaryByPhoneReportCSV(string Currency, string ReportName, List<ReportCostSummaryByPhoneEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Length >= 31 ? ReportName.Substring(0, 30) : ReportName);//IXLWorksheet ws = wb.Worksheets.Add(ReportName);
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                Row++;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                Row++;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                Row++;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                Row += 2;

                Headercell = ws.Cell(Row, 1).SetValue("Dialled Number");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Total Cost (" + Currency + ")");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Duration");
                Headercell.Style.Font.Bold = true;

                List<ReportCostSummaryByPhoneEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                  .Select(y => new ReportCostSummaryByPhoneEntity
                  {
                      Duration = y.Sum(d => d.Duration),
                      Cost = y.Sum(d => d.Cost),
                      TotalCalls = y.Sum(d => d.TotalCalls),

                  }).ToList();
                for (int i = 0; i < ReportEntityList.Count; i++)
                {

                    Row++;
                    ReportCostSummaryByPhoneEntity obj = ReportEntityList[i];
                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.DialledNumber);
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.Cost.ToString());
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.TotalCalls.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration));

                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportCostSummaryByPhoneEntity obj1 = TempReportEntityGroupedList[0];
                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue(obj1.Cost.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(obj1.TotalCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");

                    }
                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateCostSummaryByHourReportCSV(string Currency, string ReportName, List<ReportCostSummaryByHourEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Length >= 31 ? ReportName.Substring(0, 30) : ReportName);//IXLWorksheet ws = wb.Worksheets.Add(ReportName);
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                ws.Range("A4:E4").Merge();
                Row = 5;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                ws.Range("A5:E5").Merge();
                Row = 6;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                ws.Range("A6:E6").Merge();
                Row = 7;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                ws.Range("A7:E7").Merge();
                Row = 9;

                var Graphimage1 = ws.AddPicture(GraphImageCostSummaryByHourReport(ReportEntityList));
                Graphimage1.MoveTo(ws.Cell(Row, 1).Address);
                Graphimage1.Scale(.3);

                //var Graphimage2 = ws.AddPicture(GraphImageCostSummaryByRegionReport2(ReportEntityList));
                //Graphimage2.MoveTo(ws.Cell(Row, 5).Address);
                //Graphimage2.Scale(.3);
                // optional: resize picture
                Row = 24;
                ws.Range("A8:H22").Merge();


                Headercell = ws.Cell(Row, 1).SetValue("Time Period");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Total Cost (" + Currency + ")");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Duration");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("Avg. Duration");
                Headercell.Style.Font.Bold = true;


                List<ReportCostSummaryByHourEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
               .Select(y => new ReportCostSummaryByHourEntity
               {
                   Duration = y.Sum(d => d.Duration),
                   Cost = y.Sum(d => d.Cost),
                   TotalCalls = y.Sum(d => d.TotalCalls),

               }).ToList();

                for (int i = 0; i < ReportEntityList.Count; i++)
                {

                    Row++;
                    ReportCostSummaryByHourEntity obj = ReportEntityList[i];
                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(ReportsCommonMethods.GetHourFormat(obj.Hour));
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.Cost.ToString());
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.TotalCalls.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration));
                    BodyCell = ws.Cell(Row, 5).SetValue(ReportsCommonMethods.GetMinutesFromSeconds2(obj.AvgDuration));

                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportCostSummaryByHourEntity obj1 = TempReportEntityGroupedList[0];
                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue(obj1.Cost.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(obj1.TotalCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 5).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");

                        Row++;
                    }


                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }
        public static string GenerateCostSummaryByDayReportCSV(string Currency, string ReportName, List<ReportCostSummaryByDayEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Length >= 31 ? ReportName.Substring(0, 30) : ReportName);//IXLWorksheet ws = wb.Worksheets.Add(ReportName);
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                ws.Range("A4:E4").Merge();
                Row = 5;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                ws.Range("A5:E5").Merge();
                Row = 6;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                ws.Range("A6:E6").Merge();
                Row = 7;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                ws.Range("A7:E7").Merge();
                Row = 9;

                var Graphimage1 = ws.AddPicture(GraphImageCostSummaryByDayReport(ReportEntityList));
                Graphimage1.MoveTo(ws.Cell(Row, 1).Address);
                Graphimage1.Scale(.3);

                //var Graphimage2 = ws.AddPicture(GraphImageCostSummaryByRegionReport2(ReportEntityList));
                //Graphimage2.MoveTo(ws.Cell(Row, 5).Address);
                //Graphimage2.Scale(.3);
                // optional: resize picture
                Row = 24;
                ws.Range("A8:H22").Merge();

                Headercell = ws.Cell(Row, 1).SetValue("Date");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Day");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Total Cost (" + Currency + ")");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("Duration");
                Headercell.Style.Font.Bold = true;


                List<ReportCostSummaryByDayEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                  .Select(y => new ReportCostSummaryByDayEntity
                  {
                      Duration = y.Sum(d => d.Duration),
                      Cost = y.Sum(d => d.Cost),
                      TotalCalls = y.Sum(d => d.TotalCalls),

                  }).ToList();

                for (int i = 0; i < ReportEntityList.Count; i++)
                {

                    Row++;
                    ReportCostSummaryByDayEntity obj = ReportEntityList[i];
                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.Date);
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.Day);
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.Cost.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(obj.TotalCalls.ToString());
                    BodyCell = ws.Cell(Row, 5).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration));

                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportCostSummaryByDayEntity obj1 = TempReportEntityGroupedList[0];
                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(obj1.Cost.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(obj1.TotalCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 5).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");

                        Row++;
                    }

                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }
        }
        public static string GenerateCostSummaryByExtensionItemisedReportCSV(string Currency, string ReportName, List<ReportCostSummaryByExtensionItemisedEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Substring(0, 29));
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                Row++;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                Row++;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                Row++;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                Row += 2;

                List<ReportCostSummaryByExtensionItemisedEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => x.Extension)
                .Select(y => new ReportCostSummaryByExtensionItemisedEntity
                {
                    Extension = y.First().Extension,
                    Duration = y.Sum(d => d.Duration),
                    Cost = y.Sum(d => d.Cost),
                    CallCount = y.Count(),

                }).ToList();

                List<List<ReportCostSummaryByExtensionItemisedEntity>> ReportEntityGroupedList = ReportEntityList.GroupBy(x => new { x.Extension }).Select(y => y.ToList()).ToList();

                foreach (List<ReportCostSummaryByExtensionItemisedEntity> ReportEntityLists in ReportEntityGroupedList)
                {

                    Row++;
                    Headercell = ws.Cell(Row, 1).SetValue("Extension: " + ReportEntityLists[0].Extension + " ");
                    Headercell.RichText.Substring(0, 10).Bold = true;
                    Headercell.Style.Font.FontSize = 13;
                    Row++;
                    Headercell = ws.Cell(Row, 1).SetValue("Date");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 2).SetValue("Time");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 3).SetValue("Phone Number");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 4).SetValue("Total Cost (" + Currency + ")");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 5).SetValue("Duration");
                    Headercell.Style.Font.Bold = true;


                    for (int i = 0; i < ReportEntityLists.Count; i++)
                    {
                        Row++;
                        ReportCostSummaryByExtensionItemisedEntity obj = ReportEntityLists[i];

                        IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.Date);
                        BodyCell = ws.Cell(Row, 2).SetValue(obj.Time);
                        BodyCell = ws.Cell(Row, 3).SetValue(obj.PhoneNumber);
                        BodyCell = ws.Cell(Row, 4).SetValue(obj.Cost.ToString());
                        BodyCell = ws.Cell(Row, 5).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration));


                        if (i == ReportEntityLists.Count - 1)
                        {

                            Row++;
                            ReportCostSummaryByExtensionItemisedEntity obj1 = TempReportEntityGroupedList.Find(x => x.Extension == obj.Extension);
                            IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total: " + obj1.CallCount.ToString() + "");
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 2).SetValue("");
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 3).SetValue("");
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 4).SetValue(obj1.Cost.ToString());
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 5).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration));
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");

                        }
                    }
                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }
        }
        public static string GenerateResponseSummaryByHourReportCSV(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByHourEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Length >= 31 ? ReportName.Substring(0, 30) : ReportName);//IXLWorksheet ws = wb.Worksheets.Add(ReportName);
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                ws.Range("A4:E4").Merge();
                Row = 5;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                ws.Range("A5:E5").Merge();
                Row = 6;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                ws.Range("A6:E6").Merge();
                Row = 7;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                ws.Range("A7:E7").Merge();
                Row = 9;

                var Graphimage1 = ws.AddPicture(GraphImageResponseSummaryByHourReport1(ReportEntityList));
                Graphimage1.MoveTo(ws.Cell(Row, 1).Address);
                Graphimage1.Scale(.3);

                var Graphimage2 = ws.AddPicture(GraphImageResponseSummaryByHourReport2(ReportEntityList));
                Graphimage2.MoveTo(ws.Cell(Row, 7).Address);
                Graphimage2.Scale(.3);
                // optional: resize picture
                Row = 28;
                ws.Range("A8:L27").Merge();


                Headercell = ws.Cell(Row, 1).SetValue("");
                Headercell = ws.Cell(Row, 2).SetValue("");
                Headercell = ws.Cell(Row, 3).SetValue("Answered Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("");
                Headercell = ws.Cell(Row, 5).SetValue("");
                Headercell = ws.Cell(Row, 6).SetValue("");
                Headercell = ws.Cell(Row, 7).SetValue("");
                Headercell = ws.Cell(Row, 8).SetValue("");
                //Headercell = ws.Cell(Row, 9).SetValue("");
                Headercell = ws.Cell(Row, 9).SetValue("Unanswered Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 10).SetValue("");
                Headercell = ws.Cell(Row, 11).SetValue("");

                Row++;

                Headercell = ws.Cell(Row, 1).SetValue("Hour");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Avg. Ring");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Long Ring");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("Within " + RingTimeThreshold + " Sec Total");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 6).SetValue("Within " + RingTimeThreshold + " Sec %");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 7).SetValue("");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 8).SetValue("Unanswered Calls");
                Headercell.Style.Font.Bold = true;
                //Headercell = ws.Cell(Row, 9).SetValue("Total Calls");
                //Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 9).SetValue("Avg. Ring");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 10).SetValue("Long Ring");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 11).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;

                List<ReportResponseSummaryByHourEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                .Select(y => new ReportResponseSummaryByHourEntity
                {
                    TotalCalls = y.Sum(d => d.TotalCalls),
                    AnsweredCalls = y.Sum(d => d.AnsweredCalls),
                    //LostCalls = y.Sum(d => d.LostCalls),
                    UnAnsweredCalls = y.Sum(d => d.UnAnsweredCalls),
                    // Math.Round(Convert.ToDouble(row["Cost"].ToString()), 2);
                    AvgRingLost = Math.Round(y.Average(d => d.AvgRingLost), 1),
                    AvgRingAnswered = Math.Round(y.Average(d => d.AvgRingAnswered), 1),
                    MaxRingAnswered = y.Max(d => d.MaxRingAnswered),
                    MaxRingLost = y.Max(d => d.MaxRingLost),
                    WithinThresholdCount = y.Sum(d => d.WithinThresholdCount),
                    TotalRingAnswered = y.Sum(d => d.TotalRingAnswered),
                    TotalRingUnAnswered = y.Sum(d => d.TotalRingUnAnswered),

                }).ToList();


                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    ReportResponseSummaryByHourEntity obj = ReportEntityList[i];

                    string WithinRingTimePercent = "0";
                    if (obj.AnsweredCalls != 0)
                    {
                        WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(obj.WithinThresholdCount) / Convert.ToDouble(obj.AnsweredCalls) * 100), 0).ToString();
                    }
                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(ReportsCommonMethods.GetHourFormat(obj.Hour));
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.AnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.AvgRingAnswered.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(obj.MaxRingAnswered.ToString());
                    BodyCell = ws.Cell(Row, 5).SetValue(obj.WithinThresholdCount.ToString());
                    BodyCell = ws.Cell(Row, 6).SetValue(WithinRingTimePercent +"%");
                    BodyCell = ws.Cell(Row, 7).SetValue("");
                    BodyCell = ws.Cell(Row, 8).SetValue(obj.UnAnsweredCalls.ToString());
                    //BodyCell = ws.Cell(Row, 9).SetValue(obj.LostCalls.ToString());
                    BodyCell = ws.Cell(Row, 9).SetValue(obj.AvgRingLost.ToString());
                    BodyCell = ws.Cell(Row, 10).SetValue(obj.MaxRingLost.ToString());                    
                    BodyCell = ws.Cell(Row, 11).SetValue(obj.TotalCalls.ToString());
                    BodyCell.Style.Font.Bold = true;
                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportResponseSummaryByHourEntity obj1 = TempReportEntityGroupedList[0];
                        if (obj1.AnsweredCalls != 0)
                        {
                            WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(obj1.WithinThresholdCount) / Convert.ToDouble(obj1.AnsweredCalls) * 100), 0).ToString();
                        }
                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue(obj1.AnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        //FooterCell = ws.Cell(Row, 3).SetValue(obj1.AvgRingAnswered.ToString());
                        string val = (Math.Round((double)obj1.TotalRingAnswered / (double)obj1.AnsweredCalls)).ToString();
                        val = val == "NaN" ? "0" : val;
                        FooterCell = ws.Cell(Row, 3).SetValue(val);
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(obj1.MaxRingAnswered.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 5).SetValue(obj1.WithinThresholdCount.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 6).SetValue(WithinRingTimePercent + "%");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 7).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 8).SetValue(obj1.UnAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        //FooterCell = ws.Cell(Row, 9).SetValue(obj1.LostCalls.ToString());
                        //FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        //FooterCell = ws.Cell(Row, 10).SetValue(obj1.AvgRingLost.ToString());
                        val = (Math.Round((double)obj1.TotalRingUnAnswered / (double)obj1.UnAnsweredCalls)).ToString();
                        val = val == "NaN" ? "0" : val;
                        FooterCell = ws.Cell(Row, 9).SetValue(val);
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 10).SetValue(obj1.MaxRingLost.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 11).SetValue(obj1.TotalCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                    }


                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }

        public static string GenerateResponseSummaryByHourReportExcel(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByHourEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                String separator = ",";
                StringBuilder output = new StringBuilder();


               




                String[] headings = { "Hour", "Total Calls", "Avg. Ring", "Long Ring", "Within " + RingTimeThreshold + " Sec Total", "Within " + RingTimeThreshold + " Sec %", "Unanswered Calls", "Avg. Ring", "Long Ring", "Total Calls" };
                output.AppendLine(string.Join(separator, headings));

                foreach (var col in ReportEntityList.OrderBy(x => x.Hour))
                {

                    string WithinRingTimePercent = "0";
                    if (col.AnsweredCalls != 0)
                    {
                        WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(col.WithinThresholdCount) / Convert.ToDouble(col.AnsweredCalls) * 100), 0).ToString();
                    }

                    String[] newLine = { ReportsCommonMethods.GetHourFormat(col.Hour), col.AnsweredCalls.ToString(), col.AvgRingAnswered.ToString(), col.MaxRingAnswered.ToString(), col.WithinThresholdCount.ToString(), WithinRingTimePercent + "%", col.UnAnsweredCalls.ToString(), col.AvgRingLost.ToString(), col.MaxRingLost.ToString(), col.TotalCalls.ToString() };
                    output.AppendLine(string.Join(separator, newLine));
                }

                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".csv";

                File.AppendAllText(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), output.ToString());




                return FileName;

            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }


        public static string GenerateResponseSummaryByDayReportCSV(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByDayEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Length >= 31 ? ReportName.Substring(0, 30) : ReportName);//IXLWorksheet ws = wb.Worksheets.Add(ReportName);
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                ws.Range("A4:E4").Merge();
                Row = 5;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                ws.Range("A5:E5").Merge();
                Row = 6;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                ws.Range("A6:E6").Merge();
                Row = 7;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                ws.Range("A7:E7").Merge();
                Row = 9;

                var Graphimage1 = ws.AddPicture(GraphImageResponseSummaryByDayReport1(ReportEntityList));
                Graphimage1.MoveTo(ws.Cell(Row, 1).Address);
                Graphimage1.Scale(.3);

                var Graphimage2 = ws.AddPicture(GraphImageResponseSummaryByDayReport2(ReportEntityList));
                Graphimage2.MoveTo(ws.Cell(Row, 7).Address);
                Graphimage2.Scale(.3);
                // optional: resize picture
                Row = 28;
                ws.Range("A8:L27").Merge();

                Headercell = ws.Cell(Row, 1).SetValue("");
                Headercell = ws.Cell(Row, 2).SetValue("");
                Headercell = ws.Cell(Row, 3).SetValue("Answered Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("");
                Headercell = ws.Cell(Row, 5).SetValue("");
                Headercell = ws.Cell(Row, 6).SetValue("");
                Headercell = ws.Cell(Row, 7).SetValue("");
                Headercell = ws.Cell(Row, 8).SetValue("");
                Headercell = ws.Cell(Row, 9).SetValue("");
                Headercell = ws.Cell(Row, 10).SetValue("Unanswered Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 11).SetValue("");
                Headercell = ws.Cell(Row, 12).SetValue("");

                Row++;

                Headercell = ws.Cell(Row, 1).SetValue("Date");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Day");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Avg. Ring");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("Long Ring");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 6).SetValue("Within " + RingTimeThreshold + " Sec Total");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 7).SetValue("Within " + RingTimeThreshold + " Sec %");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 8).SetValue("");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 9).SetValue("Unanswered Calls");
                Headercell.Style.Font.Bold = true;
                //Headercell = ws.Cell(Row, 10).SetValue("Total Calls");
                //Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 10).SetValue("Avg. Ring");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 11).SetValue("Long Ring");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 12).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;

                List<ReportResponseSummaryByDayEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                .Select(y => new ReportResponseSummaryByDayEntity
                {
                    TotalCalls = y.Sum(d => d.TotalCalls),
                    AnsweredCalls = y.Sum(d => d.AnsweredCalls),
                    //LostCalls = y.Sum(d => d.LostCalls),
                    UnAnsweredCalls = y.Sum(d => d.UnAnsweredCalls),
                    AvgRingLost = Math.Round(y.Average(d => d.AvgRingLost), 1),
                    AvgRingAnswered = Math.Round(y.Average(d => d.AvgRingAnswered), 1),
                    MaxRingAnswered = y.Max(d => d.MaxRingAnswered),
                    MaxRingLost = y.Max(d => d.MaxRingLost),
                    WithinThresholdCount = y.Sum(d => d.WithinThresholdCount),
                    TotalRingAnswered = y.Sum(d => d.TotalRingAnswered),
                    TotalRingUnAnswered = y.Sum(d => d.TotalRingUnAnswered),

                }).ToList();

                for (int i = 0; i < ReportEntityList.Count; i++)
                {

                    Row++;
                    ReportResponseSummaryByDayEntity obj = ReportEntityList[i];

                    string WithinRingTimePercent = "0";
                    if (obj.AnsweredCalls != 0)
                    {
                        WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(obj.WithinThresholdCount) / Convert.ToDouble(obj.AnsweredCalls) * 100), 0).ToString();
                    }
                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.Date);
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.Day);
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.AnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(obj.AvgRingAnswered.ToString());
                    BodyCell = ws.Cell(Row, 5).SetValue(obj.MaxRingAnswered.ToString());
                    BodyCell = ws.Cell(Row, 6).SetValue(obj.WithinThresholdCount.ToString());
                    BodyCell = ws.Cell(Row, 7).SetValue(WithinRingTimePercent+"%");
                    BodyCell = ws.Cell(Row, 8).SetValue("");
                    BodyCell = ws.Cell(Row, 9).SetValue(obj.UnAnsweredCalls.ToString());
                    //BodyCell = ws.Cell(Row, 10).SetValue(obj.LostCalls.ToString());
                    BodyCell = ws.Cell(Row, 10).SetValue(obj.AvgRingLost.ToString());
                    BodyCell = ws.Cell(Row, 11).SetValue(obj.MaxRingLost.ToString());
                    BodyCell = ws.Cell(Row, 12).SetValue(obj.TotalCalls.ToString());
                    BodyCell.Style.Font.Bold = true;
                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportResponseSummaryByDayEntity obj1 = TempReportEntityGroupedList[0];
                        if (obj1.AnsweredCalls != 0)
                        {
                            WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(obj1.WithinThresholdCount) / Convert.ToDouble(obj1.AnsweredCalls) * 100), 0).ToString();
                        }
                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(obj1.AnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        //FooterCell = ws.Cell(Row, 4).SetValue(obj1.AvgRingAnswered.ToString());
                        string val = (Math.Round((double)obj1.TotalRingAnswered / (double)obj1.AnsweredCalls)).ToString();
                        val = val == "NaN" ? "0" : val;
                        FooterCell = ws.Cell(Row, 4).SetValue(val);
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 5).SetValue(obj1.MaxRingAnswered.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 6).SetValue(obj1.WithinThresholdCount.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 7).SetValue(WithinRingTimePercent + "%");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 8).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 9).SetValue(obj1.UnAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        //FooterCell = ws.Cell(Row, 10).SetValue(obj1.LostCalls.ToString());
                        //FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        //FooterCell = ws.Cell(Row, 11).SetValue(obj1.AvgRingLost.ToString());
                        val = (Math.Round((double)obj1.TotalRingUnAnswered / (double)obj1.UnAnsweredCalls)).ToString();
                        val = val == "NaN" ? "0" : val;
                        FooterCell = ws.Cell(Row, 10).SetValue(val);
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 11).SetValue(obj1.MaxRingLost.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 12).SetValue(obj1.TotalCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                    }
                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }


        public static string GenerateResponseSummaryByDayReportExcel(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByDayEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                String separator = ",";
                StringBuilder output = new StringBuilder();


               

                String[] headings = { "Date", "Day", "Total Calls", "Avg. Ring", "Long Ring", "Within " + RingTimeThreshold + " Sec Total", "Within " + RingTimeThreshold + " Sec %", "Unanswered Calls", "Avg. Ring", "Long Ring", "Total Calls" };
                output.AppendLine(string.Join(separator, headings));

                foreach (var col in ReportEntityList.OrderBy(x => x.Date))
                {
                    string WithinRingTimePercent = "0";
                    if (col.AnsweredCalls != 0)
                    {
                        WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(col.WithinThresholdCount) / Convert.ToDouble(col.AnsweredCalls) * 100), 0).ToString();
                    }

                    String[] newLine = { col.Date, col.Day, col.AnsweredCalls.ToString(), col.AvgRingAnswered.ToString(), col.MaxRingAnswered.ToString(), col.WithinThresholdCount.ToString(), WithinRingTimePercent + "%", col.UnAnsweredCalls.ToString(), col.AvgRingLost.ToString(), col.MaxRingLost.ToString(), col.TotalCalls.ToString() };
                    output.AppendLine(string.Join(separator, newLine));
                }

                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".csv";

                File.AppendAllText(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), output.ToString());




                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }


        public static string GenerateResponseSummaryByExtensionReportCSV(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByExtensionEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Length >= 31 ? ReportName.Substring(0, 30) : ReportName);//IXLWorksheet ws = wb.Worksheets.Add(ReportName);
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                ws.Range("A4:E4").Merge();
                Row = 5;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                ws.Range("A5:E5").Merge();
                Row = 6;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                ws.Range("A6:E6").Merge();
                Row = 7;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                ws.Range("A7:E7").Merge();
                Row = 9;

                var Graphimage1 = ws.AddPicture(GraphImageResponseSummaryByExtensionReport1(ReportEntityList));
                Graphimage1.MoveTo(ws.Cell(Row, 1).Address);
                Graphimage1.Scale(.3);

                var Graphimage2 = ws.AddPicture(GraphImageResponseSummaryByExtensionReport2(ReportEntityList));
                Graphimage2.MoveTo(ws.Cell(Row, 7).Address);
                Graphimage2.Scale(.3);
                // optional: resize picture
                Row = 28;
                ws.Range("A8:L27").Merge();


                Headercell = ws.Cell(Row, 1).SetValue("");
                Headercell = ws.Cell(Row, 2).SetValue("");
                Headercell = ws.Cell(Row, 3).SetValue("Answered Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("");
                Headercell = ws.Cell(Row, 5).SetValue("");
                Headercell = ws.Cell(Row, 6).SetValue("");
                Headercell = ws.Cell(Row, 7).SetValue("");
                Headercell = ws.Cell(Row, 8).SetValue("");
                Headercell = ws.Cell(Row, 9).SetValue("Unanswered Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 10).SetValue("");
                Headercell = ws.Cell(Row, 11).SetValue("");

                Row++;

                Headercell = ws.Cell(Row, 1).SetValue("Extension");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Avg. Ring");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Long Ring");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("Within " + RingTimeThreshold + " Sec Total");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 6).SetValue("Within " + RingTimeThreshold + " Sec %");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 7).SetValue("");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 8).SetValue("Unanswered Calls");
                Headercell.Style.Font.Bold = true;
                //Headercell = ws.Cell(Row, 9).SetValue("Total Calls");
                //Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 9).SetValue("Avg. Ring");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 10).SetValue("Long Ring");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 11).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;

                List<ReportResponseSummaryByExtensionEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
               .Select(y => new ReportResponseSummaryByExtensionEntity
               {
                   TotalCalls = y.Sum(d => d.TotalCalls),
                   AnsweredCalls = y.Sum(d => d.AnsweredCalls),
                   //LostCalls = y.Sum(d => d.LostCalls),
                   UnAnsweredCalls = y.Sum(d => d.UnAnsweredCalls),
                   AvgRingLost = Math.Round(y.Average(d => d.AvgRingLost), 1),
                   AvgRingAnswered = Math.Round(y.Average(d => d.AvgRingAnswered), 1),
                   MaxRingAnswered = y.Max(d => d.MaxRingAnswered),
                   MaxRingLost = y.Max(d => d.MaxRingLost),
                   WithinThresholdCount = y.Sum(d => d.WithinThresholdCount),
                   TotalRingAnswered = y.Sum(d => d.TotalRingAnswered),
                   TotalRingUnAnswered = y.Sum(d => d.TotalRingUnAnswered),

               }).ToList();

                for (int i = 0; i < ReportEntityList.Count; i++)
                {

                    Row++;
                    ReportResponseSummaryByExtensionEntity obj = ReportEntityList[i];

                    string WithinRingTimePercent = "0";
                    if (obj.AnsweredCalls != 0)
                    {
                        WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(obj.WithinThresholdCount) / Convert.ToDouble(obj.AnsweredCalls) * 100), 0).ToString();
                    }
                    // IXLCell BodyCell = ws.Cell(Row, 1).SetValue(ReportsCommonMethods.GetHourFormat(obj.Extension));
                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.Extension.ToString());

                    BodyCell = ws.Cell(Row, 2).SetValue(obj.AnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.AvgRingAnswered.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(obj.MaxRingAnswered.ToString());
                    BodyCell = ws.Cell(Row, 5).SetValue(obj.WithinThresholdCount.ToString());
                    BodyCell = ws.Cell(Row, 6).SetValue(WithinRingTimePercent+"%");
                    BodyCell = ws.Cell(Row, 7).SetValue("");
                    BodyCell = ws.Cell(Row, 8).SetValue(obj.UnAnsweredCalls.ToString());
                    //BodyCell = ws.Cell(Row, 9).SetValue(obj.LostCalls.ToString());
                    BodyCell = ws.Cell(Row, 9).SetValue(obj.AvgRingLost.ToString());
                    BodyCell = ws.Cell(Row, 10).SetValue(obj.MaxRingLost.ToString());
                    BodyCell = ws.Cell(Row, 11).SetValue(obj.TotalCalls.ToString());
                    BodyCell.Style.Font.Bold = true;
                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportResponseSummaryByExtensionEntity obj1 = TempReportEntityGroupedList[0];
                        if (obj1.AnsweredCalls != 0)
                        {
                            WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(obj1.WithinThresholdCount) / Convert.ToDouble(obj1.AnsweredCalls) * 100), 0).ToString();
                        }
                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue(obj1.AnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        //FooterCell = ws.Cell(Row, 3).SetValue(obj1.AvgRingAnswered.ToString());
                        string val = (Math.Round((double)obj1.TotalRingAnswered / (double)obj1.AnsweredCalls)).ToString();
                        val = val == "NaN" ? "0" : val;
                        FooterCell = ws.Cell(Row, 3).SetValue(val);
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(obj1.MaxRingAnswered.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 5).SetValue(obj1.WithinThresholdCount.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 6).SetValue(WithinRingTimePercent + "%");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 7).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 8).SetValue(obj1.UnAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        //FooterCell = ws.Cell(Row, 9).SetValue(obj1.LostCalls.ToString());
                        //FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        //FooterCell = ws.Cell(Row, 10).SetValue(obj1.AvgRingLost.ToString());
                        val = (Math.Round((double)obj1.TotalRingUnAnswered / (double)obj1.UnAnsweredCalls)).ToString();
                        val = val == "NaN" ? "0" : val;
                        FooterCell = ws.Cell(Row, 9).SetValue(val);
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 10).SetValue(obj1.MaxRingLost.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 11).SetValue(obj1.TotalCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                    }

                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }


        public static string GenerateResponseSummaryByExtensionReportExcel(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByExtensionEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {

                String separator = ",";
                StringBuilder output = new StringBuilder();


                

                String[] headings = { "Extension", "Total Calls", "Avg. Ring", "Long Ring", "Within " + RingTimeThreshold + " Sec Total", "Within " + RingTimeThreshold + " Sec %", "Unanswered Calls", "Avg. Ring", "Long Ring", "Total Calls" };
                output.AppendLine(string.Join(separator, headings));

                foreach (var col in ReportEntityList.OrderBy(x => x.Extension))
                {
                    string WithinRingTimePercent = "0";
                    if (col.AnsweredCalls != 0)
                    {
                        WithinRingTimePercent = Math.Round(Convert.ToDouble(Convert.ToDouble(col.WithinThresholdCount) / Convert.ToDouble(col.AnsweredCalls) * 100), 0).ToString();
                    }

                    String[] newLine = { col.Extension, col.AnsweredCalls.ToString(), col.AvgRingAnswered.ToString(), col.MaxRingAnswered.ToString(), col.WithinThresholdCount.ToString(), WithinRingTimePercent + "%", col.UnAnsweredCalls.ToString(), col.AvgRingLost.ToString(), col.MaxRingLost.ToString(), col.TotalCalls.ToString() };
                    output.AppendLine(string.Join(separator, newLine));
                }

                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".csv";

                File.AppendAllText(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), output.ToString());

                return FileName;

            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }



        public static string GenerateResponseSummaryByPhoneReportCSV(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByPhoneEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Substring(0, 29));
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                Row++;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                Row++;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                Row++;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                Row += 2;

                Headercell = ws.Cell(Row, 1).SetValue("Dialled Number");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Duration");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;



                List<ReportResponseSummaryByPhoneEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                     .Select(y => new ReportResponseSummaryByPhoneEntity
                     {
                         TotalCalls = y.Sum(d => d.TotalCalls),
                         Duration = y.Sum(d => d.Duration),
                     }).ToList();

                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    ReportResponseSummaryByPhoneEntity obj = ReportEntityList[i];
                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.DialledNumber);
                    BodyCell = ws.Cell(Row, 2).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration));
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.TotalCalls.ToString());

                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportResponseSummaryByPhoneEntity obj1 = TempReportEntityGroupedList[0];

                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");

                        FooterCell = ws.Cell(Row, 2).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(obj1.TotalCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        Row++;
                    }

                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }

        public static string GenerateResponseSummaryByPhoneReportExcel(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByPhoneEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {

                String separator = ",";
                StringBuilder output = new StringBuilder();

                String[] headings = { "Dialled Number", "Duration", "Total Calls" };
                output.AppendLine(string.Join(separator, headings));

                foreach (var col in ReportEntityList.OrderBy(x => x.DialledNumber))
                {
                    String[] newLine = { col.DialledNumber, ReportsCommonMethods.GetTimeFromSeconds(col.Duration), col.TotalCalls.ToString() };
                    output.AppendLine(string.Join(separator, newLine));
                }

                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".csv";

                File.AppendAllText(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), output.ToString());




                return FileName;



            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }

        public static string GenerateResponseSummaryByExtensionItemisedReportCSV(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByExtensionItemisedEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Substring(0, 29));
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                Row++;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                Row++;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                Row++;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                Row += 2;
                List<ReportResponseSummaryByExtensionItemisedEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => x.Extension)
               .Select(y => new ReportResponseSummaryByExtensionItemisedEntity
               {
                   Extension = y.First().Extension,
                   Duration = y.Sum(d => d.Duration),
                   CallCount = y.Count(),
                   RingDuration = y.Sum(d => d.RingDuration)
               }).ToList();

                List<List<ReportResponseSummaryByExtensionItemisedEntity>> ReportEntityGroupedList = ReportEntityList.GroupBy(x => new { x.Extension }).Select(y => y.ToList()).ToList();

                foreach (List<ReportResponseSummaryByExtensionItemisedEntity> ReportEntityLists in ReportEntityGroupedList)
                {
                    Row++;
                    Headercell = ws.Cell(Row, 1).SetValue("Extension: " + ReportEntityLists[0].Extension + " ");
                    Headercell.RichText.Substring(0, 10).Bold = true;
                    Headercell.Style.Font.FontSize = 13;
                    Row++;
                    Headercell = ws.Cell(Row, 1).SetValue("Date");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 2).SetValue("Time");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 3).SetValue("Duration");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 4).SetValue("Type");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 5).SetValue("CLI");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 6).SetValue("DDI");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 7).SetValue("Result");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 8).SetValue("Ring Duration");
                    Headercell.Style.Font.Bold = true;

                    for (int i = 0; i < ReportEntityLists.Count; i++)
                    {
                        Row++;
                        ReportResponseSummaryByExtensionItemisedEntity obj = ReportEntityLists[i];

                        IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.Date);
                        BodyCell = ws.Cell(Row, 2).SetValue(obj.Time);
                        BodyCell = ws.Cell(Row, 3).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration));
                        BodyCell = ws.Cell(Row, 4).SetValue(obj.Direction);
                        BodyCell = ws.Cell(Row, 5).SetValue(obj.CLI);
                        BodyCell = ws.Cell(Row, 6).SetValue(obj.DDI);
                        BodyCell = ws.Cell(Row, 7).SetValue(ReportsCommonMethods.GetCallStatus(int.Parse(obj.LastState), int.Parse(obj.InitialState), obj.Duration, obj.Direction));
                        BodyCell = ws.Cell(Row, 8).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.RingDuration));
                        if (i == ReportEntityLists.Count - 1)
                        {

                            Row++;
                            ReportResponseSummaryByExtensionItemisedEntity obj1 = TempReportEntityGroupedList.Find(x => x.Extension == obj.Extension);
                            IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total: " + obj1.CallCount.ToString() + "");
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 2).SetValue("");
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 3).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration));
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 4).SetValue("");
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 5).SetValue("");
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 6).SetValue("");
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 7).SetValue("");
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 8).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.RingDuration));
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            Row++;
                        }

                    }
                }

                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }


        public static string GenerateResponseSummaryByExtensionItemisedReportExcel(string Currency, int RingTimeThreshold, string ReportName, List<ReportResponseSummaryByExtensionItemisedEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {

               


                String separator = ",";
                StringBuilder output = new StringBuilder();

                String[] headings = { "Extension", "Date", "Time", "Duration", "Type", "CLI", "DDI", "Result", "Ring Duration" };
                output.AppendLine(string.Join(separator, headings));

                foreach (var col in ReportEntityList.OrderBy(x => x.Extension))
                {
                    String[] newLine = { col.Extension, col.Date, col.Time, ReportsCommonMethods.GetTimeFromSeconds(col.Duration), col.Direction, col.CLI, col.DDI, ReportsCommonMethods.GetCallStatus(int.Parse(col.LastState), int.Parse(col.InitialState), col.Duration, col.Direction), ReportsCommonMethods.GetTimeFromSeconds(col.RingDuration) };
                    output.AppendLine(string.Join(separator, newLine));
                }

                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".csv";

                File.AppendAllText(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), output.ToString());




                return FileName;

            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }


        public static string GenerateTrafficSummaryByHourReportCSV(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByHourEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Length >= 31 ? ReportName.Substring(0, 30) : ReportName);//IXLWorksheet ws = wb.Worksheets.Add(ReportName);
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                ws.Range("A4:E4").Merge();
                Row = 5;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                ws.Range("A5:E5").Merge();
                Row = 6;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                ws.Range("A6:E6").Merge();
                Row = 7;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                ws.Range("A7:E7").Merge();
                Row = 9;

                var Graphimage1 = ws.AddPicture(GraphImageTrafficSummaryByHourReport1(ReportEntityList));
                Graphimage1.MoveTo(ws.Cell(Row, 1).Address);
                Graphimage1.Scale(.3);

                var Graphimage2 = ws.AddPicture(GraphImageTrafficSummaryByHourReport2(ReportEntityList));
                Graphimage2.MoveTo(ws.Cell(Row, 7).Address);
                Graphimage2.Scale(.3);
                // optional: resize picture
                Row = 28;
                ws.Range("A8:L27").Merge();

                Headercell = ws.Cell(Row, 1).SetValue("");
                Headercell = ws.Cell(Row, 2).SetValue("");
                Headercell = ws.Cell(Row, 3).SetValue("Inbound Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("");
                Headercell = ws.Cell(Row, 5).SetValue("");
                Headercell = ws.Cell(Row, 6).SetValue("");
                Headercell = ws.Cell(Row, 7).SetValue("");
                Headercell = ws.Cell(Row, 8).SetValue("");
                Headercell = ws.Cell(Row, 9).SetValue("Outbound Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 10).SetValue("");
               // Headercell = ws.Cell(Row, 11).SetValue("");


                Row++;

                Headercell = ws.Cell(Row, 1).SetValue("Hour");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Answered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Unanswered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("Duration");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 6).SetValue("");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 7).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 8).SetValue("Answered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 9).SetValue("Unanswered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 10).SetValue("Duration");
                Headercell.Style.Font.Bold = true;
              //  Headercell = ws.Cell(Row, 11).SetValue("Cost (" + Currency + ")");
                //Headercell.Style.Font.Bold = true;

                List<ReportTrafficSummaryByHourEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                .Select(y => new ReportTrafficSummaryByHourEntity
                {
                    IncomingCalls = y.Sum(d => d.IncomingCalls),
                    IncomingAnsweredCalls = y.Sum(d => d.IncomingAnsweredCalls),
                    IncomingUnAnsweredCalls = y.Sum(d => d.IncomingUnAnsweredCalls),
                    IncomingCallDuration = y.Sum(d => d.IncomingCallDuration),
                    OutgoingCalls = y.Sum(d => d.OutgoingCalls),
                    OutgoingAnsweredCalls = y.Sum(d => d.OutgoingAnsweredCalls),
                    OutgoingUnAnsweredCalls = y.Sum(d => d.OutgoingUnAnsweredCalls),
                    OutgoingCallDuration = y.Sum(d => d.OutgoingCallDuration),
                    Cost = y.Sum(d => d.Cost),


                }).ToList();

                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    ReportTrafficSummaryByHourEntity obj = ReportEntityList[i];


                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(ReportsCommonMethods.GetHourFormat(obj.Hour));
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.IncomingCalls.ToString());
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.IncomingAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(obj.IncomingUnAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 5).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration));
                    BodyCell = ws.Cell(Row, 6).SetValue("");
                    BodyCell = ws.Cell(Row, 7).SetValue(obj.OutgoingCalls.ToString());
                    BodyCell = ws.Cell(Row, 8).SetValue(obj.OutgoingAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 9).SetValue(obj.OutgoingUnAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 10).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration));
                  //  BodyCell = ws.Cell(Row, 11).SetValue(obj.Cost.ToString());


                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportTrafficSummaryByHourEntity obj1 = TempReportEntityGroupedList[0];

                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue(obj1.IncomingCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(obj1.IncomingAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(obj1.IncomingUnAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 5).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.IncomingCallDuration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 6).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 7).SetValue(obj1.OutgoingCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 8).SetValue(obj1.OutgoingAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 9).SetValue(obj1.OutgoingUnAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 10).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.OutgoingCallDuration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                       // FooterCell = ws.Cell(Row, 11).SetValue(obj1.Cost.ToString());
                       // FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");

                    }

                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }

        public static string GenerateTrafficSummaryByHourReportExcel(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByHourEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                String separator = ",";
                StringBuilder output = new StringBuilder();


              
                   

                String[] headings = { "Hour", "Total Calls-Inbound Calls", "Answered-Inbound Calls", "Unanswered-Inbound Calls", "Duration-Inbound Calls", "Total Calls-Outbound Calls", "Answered-Outbound Calls", "Unanswered-Outbound Calls", "Duration-Outbound Calls" };
                output.AppendLine(string.Join(separator, headings));

                foreach (var col in ReportEntityList)
                {
                    String[] newLine = { ReportsCommonMethods.GetHourFormat(col.Hour), col.IncomingCalls.ToString(), col.IncomingAnsweredCalls.ToString(), col.IncomingUnAnsweredCalls.ToString(), ReportsCommonMethods.GetTimeFromSeconds(col.IncomingCallDuration), col.OutgoingCalls.ToString(), col.OutgoingAnsweredCalls.ToString(), col.OutgoingUnAnsweredCalls.ToString(), ReportsCommonMethods.GetTimeFromSeconds(col.OutgoingCallDuration) };
                    output.AppendLine(string.Join(separator, newLine));
                }

                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".csv";

                File.AppendAllText(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), output.ToString());




                return FileName;


            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }


        public static string GenerateTrafficSummaryByDayReportCSV(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByDayEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Length >= 31 ? ReportName.Substring(0, 30) : ReportName);//IXLWorksheet ws = wb.Worksheets.Add(ReportName);
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                ws.Range("A4:E4").Merge();
                Row = 5;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                ws.Range("A5:E5").Merge();
                Row = 6;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                ws.Range("A6:E6").Merge();
                Row = 7;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                ws.Range("A7:E7").Merge();
                Row = 9;

                var Graphimage1 = ws.AddPicture(GraphImageTrafficSummaryByDayReport1(ReportEntityList));
                Graphimage1.MoveTo(ws.Cell(Row, 1).Address);
                Graphimage1.Scale(.3);

                var Graphimage2 = ws.AddPicture(GraphImageTrafficSummaryByDayReport2(ReportEntityList));
                Graphimage2.MoveTo(ws.Cell(Row, 7).Address);
                Graphimage2.Scale(.3);
                // optional: resize picture
                Row = 28;
                ws.Range("A8:L27").Merge();

                Headercell = ws.Cell(Row, 1).SetValue("");
                Headercell = ws.Cell(Row, 2).SetValue("");
                Headercell = ws.Cell(Row, 3).SetValue("Inbound Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("");
                Headercell = ws.Cell(Row, 5).SetValue("");
                Headercell = ws.Cell(Row, 6).SetValue("");
                Headercell = ws.Cell(Row, 7).SetValue("");
                Headercell = ws.Cell(Row, 8).SetValue("");
                Headercell = ws.Cell(Row, 9).SetValue("Outbound Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 10).SetValue("");
                Headercell = ws.Cell(Row, 11).SetValue("");
                //Headercell = ws.Cell(Row, 12).SetValue("");

                Row++;

                Headercell = ws.Cell(Row, 1).SetValue("Date");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Day");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Answered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("Unanswered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 6).SetValue("Duration");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 7).SetValue("");
                Headercell = ws.Cell(Row, 8).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 9).SetValue("Answered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 10).SetValue("Unanswered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 11).SetValue("Duration");
                Headercell.Style.Font.Bold = true;
              //  Headercell = ws.Cell(Row, 12).SetValue("Cost (" + Currency + ")");
               // Headercell.Style.Font.Bold = true;

                List<ReportTrafficSummaryByDayEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                 .Select(y => new ReportTrafficSummaryByDayEntity
                 {
                     IncomingCalls = y.Sum(d => d.IncomingCalls),
                     IncomingAnsweredCalls = y.Sum(d => d.IncomingAnsweredCalls),
                     IncomingUnAnsweredCalls = y.Sum(d => d.IncomingUnAnsweredCalls),
                     IncomingCallDuration = y.Sum(d => d.IncomingCallDuration),
                     OutgoingCalls = y.Sum(d => d.OutgoingCalls),
                     OutgoingAnsweredCalls = y.Sum(d => d.OutgoingAnsweredCalls),
                     OutgoingUnAnsweredCalls = y.Sum(d => d.OutgoingUnAnsweredCalls),
                     OutgoingCallDuration = y.Sum(d => d.OutgoingCallDuration),
                     Cost = y.Sum(d => d.Cost),


                 }).ToList();

                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    ReportTrafficSummaryByDayEntity obj = ReportEntityList[i];

                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.Date);
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.Day.ToString());
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.IncomingCalls.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(obj.IncomingAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 5).SetValue(obj.IncomingUnAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 6).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration));
                    BodyCell = ws.Cell(Row, 7).SetValue("");
                    BodyCell = ws.Cell(Row, 8).SetValue(obj.OutgoingCalls.ToString());
                    BodyCell = ws.Cell(Row, 9).SetValue(obj.OutgoingAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 10).SetValue(obj.OutgoingUnAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 11).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration));
                   // BodyCell = ws.Cell(Row, 12).SetValue(obj.Cost.ToString());


                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportTrafficSummaryByDayEntity obj1 = TempReportEntityGroupedList[0];

                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue(obj1.IncomingCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(obj1.IncomingAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(obj1.IncomingUnAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 5).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.IncomingCallDuration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 6).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 7).SetValue(obj1.OutgoingCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 8).SetValue(obj1.OutgoingAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 9).SetValue(obj1.OutgoingUnAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 10).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.OutgoingCallDuration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                       // FooterCell = ws.Cell(Row, 11).SetValue(obj1.Cost.ToString());
                       // FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        Row++;
                    }
                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }


        public static string GenerateTrafficSummaryByDayReportExcel(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByDayEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                String separator = ",";
                StringBuilder output = new StringBuilder();



                  

                String[] headings = { "Date", "Day", "Total Calls-Inbound Calls", "Answered-Inbound Calls", "Unanswered-Inbound Calls", "Duration-Inbound Calls", "Total Calls-Outbound Calls", "Answered-Outbound Calls", "Unanswered-Outbound Calls", "Duration-Outbound Calls" };
                output.AppendLine(string.Join(separator, headings));

                foreach (var col in ReportEntityList)
                {
                    String[] newLine = { col.Date, col.Day, col.IncomingCalls.ToString(), col.IncomingAnsweredCalls.ToString(), col.IncomingUnAnsweredCalls.ToString(), ReportsCommonMethods.GetTimeFromSeconds(col.IncomingCallDuration), col.OutgoingCalls.ToString(), col.OutgoingAnsweredCalls.ToString(), col.OutgoingUnAnsweredCalls.ToString(), ReportsCommonMethods.GetTimeFromSeconds(col.OutgoingCallDuration) };
                    output.AppendLine(string.Join(separator, newLine));
                }

                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".csv";

                File.AppendAllText(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), output.ToString());




                return FileName;

            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }


        public static string GenerateTrafficSummaryByExtensionReportCSV(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByExtensionEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Length >= 31 ? ReportName.Substring(0, 30) : ReportName);//IXLWorksheet ws = wb.Worksheets.Add(ReportName);
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                ws.Range("A4:E4").Merge();
                Row = 5;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                ws.Range("A5:E5").Merge();
                Row = 6;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                ws.Range("A6:E6").Merge();
                Row = 7;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                ws.Range("A7:E7").Merge();
                Row = 9;

                var Graphimage1 = ws.AddPicture(GraphImageTrafficSummaryByExtensionReport1(ReportEntityList));
                Graphimage1.MoveTo(ws.Cell(Row, 1).Address);
                Graphimage1.Scale(.3);

                var Graphimage2 = ws.AddPicture(GraphImageTrafficSummaryByExtensionReport2(ReportEntityList));
                Graphimage2.MoveTo(ws.Cell(Row, 7).Address);
                Graphimage2.Scale(.3);
                // optional: resize picture
                Row = 28;
                ws.Range("A8:L27").Merge();

                Headercell = ws.Cell(Row, 1).SetValue("");
                Headercell = ws.Cell(Row, 2).SetValue("");
                Headercell = ws.Cell(Row, 3).SetValue("Inbound Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("");
                Headercell = ws.Cell(Row, 5).SetValue("");
                Headercell = ws.Cell(Row, 6).SetValue("");
                Headercell = ws.Cell(Row, 7).SetValue("");
                Headercell = ws.Cell(Row, 8).SetValue("");
                Headercell = ws.Cell(Row, 9).SetValue("Outbound Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 10).SetValue("");
                //Headercell = ws.Cell(Row, 11).SetValue("");


                Row++;

                Headercell = ws.Cell(Row, 1).SetValue("Extension");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Answered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Unanswered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("Duration");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 6).SetValue("");
                Headercell = ws.Cell(Row, 7).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 8).SetValue("Answered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 9).SetValue("Unanswered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 10).SetValue("Duration");
                Headercell.Style.Font.Bold = true;
               // Headercell = ws.Cell(Row, 11).SetValue("Cost (" + Currency + ")");
              //  Headercell.Style.Font.Bold = true;

                List<ReportTrafficSummaryByExtensionEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                 .Select(y => new ReportTrafficSummaryByExtensionEntity
                 {
                     IncomingCalls = y.Sum(d => d.IncomingCalls),
                     IncomingAnsweredCalls = y.Sum(d => d.IncomingAnsweredCalls),
                     IncomingUnAnsweredCalls = y.Sum(d => d.IncomingUnAnsweredCalls),
                     IncomingCallDuration = y.Sum(d => d.IncomingCallDuration),
                     OutgoingCalls = y.Sum(d => d.OutgoingCalls),
                     OutgoingAnsweredCalls = y.Sum(d => d.OutgoingAnsweredCalls),
                     OutgoingUnAnsweredCalls = y.Sum(d => d.OutgoingUnAnsweredCalls),
                     OutgoingCallDuration = y.Sum(d => d.OutgoingCallDuration),
                     Cost = y.Sum(d => d.Cost),


                 }).ToList();

                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    ReportTrafficSummaryByExtensionEntity obj = ReportEntityList[i];


                    //IXLCell BodyCell = ws.Cell(Row, 1).SetValue(ReportsCommonMethods.GetHourFormat(obj.Extension));
                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.Extension.ToString());
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.IncomingCalls.ToString());
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.IncomingAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(obj.IncomingUnAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 5).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration));
                    BodyCell = ws.Cell(Row, 6).SetValue("");
                    BodyCell = ws.Cell(Row, 7).SetValue(obj.OutgoingCalls.ToString());
                    BodyCell = ws.Cell(Row, 8).SetValue(obj.OutgoingAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 9).SetValue(obj.OutgoingUnAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 10).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration));
                  //  BodyCell = ws.Cell(Row, 11).SetValue(obj.Cost.ToString());


                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportTrafficSummaryByExtensionEntity obj1 = TempReportEntityGroupedList[0];

                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue(obj1.IncomingCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(obj1.IncomingAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(obj1.IncomingUnAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 5).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.IncomingCallDuration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 6).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 7).SetValue(obj1.OutgoingCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 8).SetValue(obj1.OutgoingAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 9).SetValue(obj1.OutgoingUnAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 10).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.OutgoingCallDuration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        //FooterCell = ws.Cell(Row, 11).SetValue(obj1.Cost.ToString());
                       // FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        Row++;
                    }

                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }

        public static string GenerateTrafficSummaryByExtensionReportExcel(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByExtensionEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                String separator = ",";
                StringBuilder output = new StringBuilder();


               

                String[] headings = { "Extension-Inbound Calls", "Total Calls-Inbound Calls", "Answered-Inbound Calls", "Unanswered-Inbound Calls", "Duration-Inbound Calls", "Total Calls-Outbound Calls", "Answered-Outbound Calls", "Unanswered-Outbound Calls", "Duration-Outbound Calls" };
                output.AppendLine(string.Join(separator, headings));

                foreach (var col in ReportEntityList)
                {
                    String[] newLine = { col.Extension, col.IncomingCalls.ToString(), col.IncomingAnsweredCalls.ToString(), col.IncomingUnAnsweredCalls.ToString(), ReportsCommonMethods.GetTimeFromSeconds(col.IncomingCallDuration), col.OutgoingCalls.ToString(), col.OutgoingAnsweredCalls.ToString(), col.OutgoingUnAnsweredCalls.ToString(), ReportsCommonMethods.GetTimeFromSeconds(col.OutgoingCallDuration) };
                    output.AppendLine(string.Join(separator, newLine));
                }

                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".csv";

                File.AppendAllText(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), output.ToString());




                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }


        public static string GenerateTrafficSummaryByPhoneReportCSV(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByPhoneEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Substring(0, 29));
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                Row++;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                Row++;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                Row++;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                Row += 2;

                Headercell = ws.Cell(Row, 1).SetValue("Dialled Number");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Answered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Unanswered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("Duration");
                Headercell.Style.Font.Bold = true;
               // Headercell = ws.Cell(Row, 6).SetValue("Cost (" + Currency + ")");
               // Headercell.Style.Font.Bold = true;

                List<ReportTrafficSummaryByPhoneEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                 .Select(y => new ReportTrafficSummaryByPhoneEntity
                 {
                     OutgoingCalls = y.Sum(d => d.OutgoingCalls),
                     OutgoingAnsweredCalls = y.Sum(d => d.OutgoingAnsweredCalls),
                     OutgoingUnAnsweredCalls = y.Sum(d => d.OutgoingUnAnsweredCalls),
                     OutgoingCallDuration = y.Sum(d => d.OutgoingCallDuration),
                     Cost = y.Sum(d => d.Cost),


                 }).ToList();

                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    ReportTrafficSummaryByPhoneEntity obj = ReportEntityList[i];

                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.DialledNumber);
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.OutgoingCalls.ToString());
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.OutgoingAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(obj.OutgoingUnAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 5).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration));
                  //  BodyCell = ws.Cell(Row, 6).SetValue(obj.Cost.ToString());
                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportTrafficSummaryByPhoneEntity obj1 = TempReportEntityGroupedList[0];
                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue(obj1.OutgoingCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(obj1.OutgoingAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(obj1.OutgoingUnAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 5).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.OutgoingCallDuration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                       // FooterCell = ws.Cell(Row, 6).SetValue(obj1.Cost.ToString());
                      //  FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        Row++;
                    }

                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }


        public static string GenerateTrafficSummaryByPhoneReportExcel(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByPhoneEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                String separator = ",";
                StringBuilder output = new StringBuilder();


                 

                String[] headings = { "Dialled Number", "Total Calls", "Answered", "Unanswered", "Duration" };
                output.AppendLine(string.Join(separator, headings));

                foreach (var col in ReportEntityList)
                {
                    String[] newLine = { col.DialledNumber, col.OutgoingCalls.ToString(), col.OutgoingAnsweredCalls.ToString(), col.OutgoingUnAnsweredCalls.ToString(), ReportsCommonMethods.GetTimeFromSeconds(col.OutgoingCallDuration) };
                    output.AppendLine(string.Join(separator, newLine));
                }

                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".csv";

                File.AppendAllText(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), output.ToString());




                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }

        public static string GenerateTrafficSummaryByExtensionItemisedReportCSV(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByExtensionItemisedEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Substring(0, 29));
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/"+ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                Row++;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                Row++;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                Row++;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                Row += 2;

                List<ReportTrafficSummaryByExtensionItemisedEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => x.Extension)
               .Select(y => new ReportTrafficSummaryByExtensionItemisedEntity
               {
                   Extension = y.First().Extension,
                   Duration = y.Sum(d => d.Duration),
                   Cost = y.Sum(d => d.Cost),
                   CallCount = y.Count(),
                   InboundCallCount = y.Count(d => d.Direction == "Inbound"),
                   OutbountCallCount = y.Count(d => d.Direction == "Outbound"),

               }).ToList();

                List<List<ReportTrafficSummaryByExtensionItemisedEntity>> ReportEntityGroupedList = ReportEntityList.OrderBy(y => y.Date).GroupBy(x => x.Extension).Select(y => y.ToList()).ToList();

                foreach (List<ReportTrafficSummaryByExtensionItemisedEntity> ReportEntityLists in ReportEntityGroupedList)
                {
                    Row++;

                    Headercell = ws.Cell(Row, 1).SetValue("Extension: " + ReportEntityLists[0].Extension + " ");
                    Headercell.RichText.Substring(0, 10).Bold = true;
                    Headercell.Style.Font.FontSize = 13;
                    Row++;
                    Headercell = ws.Cell(Row, 1).SetValue("Date");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 2).SetValue("Time");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 3).SetValue("Duration");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 4).SetValue("Type");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 5).SetValue("CLI");
                    Headercell.Style.Font.Bold = true;
                    Headercell = ws.Cell(Row, 6).SetValue("DDI");
                    Headercell.Style.Font.Bold = true;
                   // Headercell = ws.Cell(Row, 7).SetValue("Cost (" + Currency + ")");
                  //  Headercell.Style.Font.Bold = true;

                    for (int i = 0; i < ReportEntityLists.Count; i++)
                    {
                        Row++;
                        ReportTrafficSummaryByExtensionItemisedEntity obj = ReportEntityLists[i];

                        IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.Date);
                        BodyCell = ws.Cell(Row, 2).SetValue(obj.Time);
                        BodyCell = ws.Cell(Row, 3).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration));
                        BodyCell = ws.Cell(Row, 4).SetValue(obj.Direction);
                        BodyCell = ws.Cell(Row, 5).SetValue(obj.CLI);
                        BodyCell = ws.Cell(Row, 6).SetValue(obj.DDI);
                     //   BodyCell = ws.Cell(Row, 7).SetValue(obj.Cost.ToString());

                        if (i == ReportEntityLists.Count - 1)
                        {

                            Row++;
                            ReportTrafficSummaryByExtensionItemisedEntity obj1 = TempReportEntityGroupedList.Find(x => x.Extension == obj.Extension);
                            IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total: " + obj1.CallCount.ToString() + "");
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 2).SetValue("");
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 3).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.Duration));
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 4).SetValue("Inbound:" + obj1.InboundCallCount + "");
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 5).SetValue("Outbound:" + obj1.OutbountCallCount + "");
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                            FooterCell = ws.Cell(Row, 6).SetValue("");
                            FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                           // FooterCell = ws.Cell(Row, 7).SetValue(obj1.Cost.ToString());
                           // FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        }
                    }
                }

                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }

        public static string GenerateTrafficSummaryByExtensionItemisedReportExcel(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByExtensionItemisedEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                String separator = ",";
                StringBuilder output = new StringBuilder();

               



                String[] headings = { "Extension", "Date", "Time", "Duration", "Type", "CLI", "DDI" };
                output.AppendLine(string.Join(separator, headings));

                foreach (var col in ReportEntityList)
                {
                    String[] newLine = { col.Extension,col.Date, col.Time, ReportsCommonMethods.GetTimeFromSeconds(col.Duration), col.Direction, col.CLI, col.DDI };
                    output.AppendLine(string.Join(separator, newLine));
                }

                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".csv";

                File.AppendAllText(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), output.ToString());




                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }



        public static string GenerateCallTrailReportCSV(string ReportName, List<ReportCallTrailEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Length >= 31 ? ReportName.Substring(0, 30) : ReportName);//IXLWorksheet ws = wb.Worksheets.Add(ReportName);
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/" + ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);
                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                Row++;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                Row++;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                Row++;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                Row += 2;

                Headercell = ws.Cell(Row, 1).SetValue("Extension");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Direction");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("CLI");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("DDI");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("StartTime");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 6).SetValue("Hold Duration");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 7).SetValue("Conversation");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 8).SetValue("Ring Duration");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 9).SetValue("Total Duration");
                Headercell.Style.Font.Bold = true;


                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    ReportCallTrailEntity obj = ReportEntityList[i];
                    if (obj.Status == "M")
                    {
                        string MSerial = obj.MSerial;
                        IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.MExtension);
                        BodyCell = ws.Cell(Row, 2).SetValue(obj.MDirection);
                        BodyCell = ws.Cell(Row, 3).SetValue(obj.MCLI);
                        BodyCell = ws.Cell(Row, 4).SetValue(obj.MDDI);
                        BodyCell = ws.Cell(Row, 5).SetValue(obj.MStartTime.ToString());
                        BodyCell = ws.Cell(Row, 6).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.MHoldDuration));
                        BodyCell = ws.Cell(Row, 7).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalConversation));
                        BodyCell = ws.Cell(Row, 8).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalRingDuration));
                        BodyCell = ws.Cell(Row, 9).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalDuration));
                        while (i < ReportEntityList.Count && ReportEntityList[i].MSerial == MSerial)
                        {
                            Row++;
                            obj = ReportEntityList[i];
                            BodyCell = ws.Cell(Row, 1).SetValue(obj.MExtension);
                            BodyCell = ws.Cell(Row, 2).SetValue(obj.MDirection);
                            BodyCell = ws.Cell(Row, 3).SetValue(obj.MCLI);
                            BodyCell = ws.Cell(Row, 4).SetValue(obj.MDDI);
                            BodyCell = ws.Cell(Row, 5).SetValue(obj.MStartTime.ToString());
                            BodyCell = ws.Cell(Row, 6).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.MHoldDuration));
                            BodyCell = ws.Cell(Row, 7).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalConversation));
                            BodyCell = ws.Cell(Row, 8).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalRingDuration));
                            BodyCell = ws.Cell(Row, 9).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalDuration));
                            //i++;
                            if ((i + 1) < ReportEntityList.Count && ReportEntityList[i + 1].MSerial == MSerial)
                                i++;
                            else
                                break;
                        }
                    }
                    else
                    {
                        IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.MExtension);
                        BodyCell = ws.Cell(Row, 2).SetValue(obj.MDirection);
                        BodyCell = ws.Cell(Row, 3).SetValue(obj.MCLI);
                        BodyCell = ws.Cell(Row, 4).SetValue(obj.MDDI);
                        BodyCell = ws.Cell(Row, 5).SetValue(obj.MStartTime.ToString());
                        BodyCell = ws.Cell(Row, 6).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.MHoldDuration));
                        BodyCell = ws.Cell(Row, 7).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalConversation));
                        BodyCell = ws.Cell(Row, 8).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalRingDuration));
                        BodyCell = ws.Cell(Row, 9).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalDuration));
                    }

                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }



        public static string GenerateCallTrailReportExcel(string ReportName, List<ReportCallTrailEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
           


             
              

                String separator = ",";
                StringBuilder output = new StringBuilder();

                String[] headings = { "Extension", "Direction", "CLI", "DDI", "StartTime", "Hold Duration", "Conversation", "Ring Duration", "Total Duration" };
                output.AppendLine(string.Join(separator, headings));







                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    ReportCallTrailEntity obj = ReportEntityList[i];
                    if (obj.Status == "M")
                    {
                        string MSerial = obj.MSerial;
                    


                        String[] newLine = { obj.MExtension, obj.MDirection, obj.MCLI, obj.MDDI, obj.MStartTime.ToString(), ReportsCommonMethods.GetTimeFromSeconds(obj.MHoldDuration), ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalConversation), ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalRingDuration), ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalDuration) };
                        output.AppendLine(string.Join(separator, newLine));


                        while (i < ReportEntityList.Count && ReportEntityList[i].MSerial == MSerial)
                        {
                            Row++;
                            obj = ReportEntityList[i];
                            String[] newLine1 = { obj.MExtension, obj.MDirection, obj.MCLI, obj.MDDI, obj.MStartTime.ToString(), ReportsCommonMethods.GetTimeFromSeconds(obj.MHoldDuration), ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalConversation), ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalRingDuration), ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalDuration) };
                            output.AppendLine(string.Join(separator, newLine1));
                            //i++;
                            if ((i + 1) < ReportEntityList.Count && ReportEntityList[i + 1].MSerial == MSerial)
                                i++;
                            else
                                break;
                        }
                    }
                    else
                    {
                        String[] newLine = { obj.MExtension, obj.MDirection, obj.MCLI, obj.MDDI, obj.MStartTime.ToString(), ReportsCommonMethods.GetTimeFromSeconds(obj.MHoldDuration), ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalConversation), ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalRingDuration), ReportsCommonMethods.GetTimeFromSeconds(obj.MTotalDuration) };
                        output.AppendLine(string.Join(separator, newLine));
                    }

                }


                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".csv";

                File.AppendAllText(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), output.ToString());




                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }










        public static string GenerateTrafficSummaryByAreaCodeOutboundReportCSV(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByAreaCodeOutboundEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Length >= 31 ? ReportName.Substring(0, 30) : ReportName);//IXLWorksheet ws = wb.Worksheets.Add(ReportName);
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/" + ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                ws.Range("A4:E4").Merge();
                Row = 5;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                ws.Range("A5:E5").Merge();
                Row = 6;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                ws.Range("A6:E6").Merge();
                Row = 7;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                ws.Range("A7:E7").Merge();
                Row = 9;

                //var Graphimage1 = ws.AddPicture(GraphImageTrafficSummaryByExtensionReport1(ReportEntityList));
                //Graphimage1.MoveTo(ws.Cell(Row, 1).Address);
                //Graphimage1.Scale(.3);

                //var Graphimage2 = ws.AddPicture(GraphImageTrafficSummaryByExtensionReport2(ReportEntityList));
                //Graphimage2.MoveTo(ws.Cell(Row, 7).Address);
                //Graphimage2.Scale(.3);
                //// optional: resize picture
                //Row = 28;
                //ws.Range("A8:L27").Merge();

                Headercell = ws.Cell(Row, 1).SetValue("");
                Headercell = ws.Cell(Row, 2).SetValue("");
                Headercell = ws.Cell(Row, 3).SetValue("Outbound Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("");
                Headercell = ws.Cell(Row, 5).SetValue("");
                Headercell = ws.Cell(Row, 6).SetValue("");                

                Row++;
                
                Headercell = ws.Cell(Row, 1).SetValue("Area Code");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Area");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Answered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("Unanswered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 6).SetValue("Duration");
                Headercell.Style.Font.Bold = true;
                //Headercell = ws.Cell(Row, 11).SetValue("Cost (" + Currency + ")");
                //Headercell.Style.Font.Bold = true;

                List<ReportTrafficSummaryByAreaCodeOutboundEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                 .Select(y => new ReportTrafficSummaryByAreaCodeOutboundEntity
                 {
                     
                     OutgoingCalls = y.Sum(d => d.OutgoingCalls),
                     OutgoingAnsweredCalls = y.Sum(d => d.OutgoingAnsweredCalls),
                     OutgoingUnAnsweredCalls = y.Sum(d => d.OutgoingUnAnsweredCalls),
                     OutgoingCallDuration = y.Sum(d => d.OutgoingCallDuration),
                     
                 }).ToList();

                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    ReportTrafficSummaryByAreaCodeOutboundEntity obj = ReportEntityList[i];

                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.AreaCode);
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.AreaDescription.ToString());
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.OutgoingCalls.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(obj.OutgoingAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 5).SetValue(obj.OutgoingUnAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 6).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration));

                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportTrafficSummaryByAreaCodeOutboundEntity obj1 = TempReportEntityGroupedList[0];

                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(obj1.OutgoingCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(obj1.OutgoingAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 5).SetValue(obj1.OutgoingUnAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 6).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.OutgoingCallDuration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        
                        Row++;
                    }

                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }

        public static string GenerateTrafficSummaryByAreaCodeOutboundReportExcel(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByAreaCodeOutboundEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                String separator = ",";
                StringBuilder output = new StringBuilder();


       


                String[] headings = { "Area Code", "Area", "Total Calls", "Answered", "Unanswered", "Duration" };
                output.AppendLine(string.Join(separator, headings));

                foreach (var col in ReportEntityList)
                {
                    String[] newLine = {
                        col.AreaCode,
                        col.AreaDescription,
                        col.OutgoingCalls.ToString(),
                        col.OutgoingAnsweredCalls.ToString(),
                        col.OutgoingUnAnsweredCalls.ToString(),
                        ReportsCommonMethods.GetTimeFromSeconds(col.OutgoingCallDuration) };
                    output.AppendLine(string.Join(separator, newLine));
                }

                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".csv";

                File.AppendAllText(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), output.ToString());




                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }


        public static string GenerateTrafficSummaryByAreaCodeInboundReportCSV(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByAreaCodeInboundEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                int Row = 1;
                XLWorkbook wb = new XLWorkbook();
                IXLWorksheet ws = wb.Worksheets.Add(ReportName.Length >= 31 ? ReportName.Substring(0, 30) : ReportName);//IXLWorksheet ws = wb.Worksheets.Add(ReportName);
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".xlsx";
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(ClientDB);
                string ImageLocation = HttpContext.Current.Server.MapPath("~/" + ReportsDALObj.GetCompanyLogo());
                if (File.Exists(ImageLocation))
                {

                    var image = ws.AddPicture(ImageLocation);

                    image.MoveTo(ws.Cell(Row, 1).Address);
                    image.Scale(0.50);
                    // optional: resize picture
                    Row += 3;
                    ws.Range("A1:D3").Merge();
                }


                IXLCell Headercell = ws.Cell(Row, 1).SetValue(ReportName);
                Headercell.Style.Font.Bold = true;
                Headercell.Style.Font.FontSize = 18;
                ws.Range("A4:E4").Merge();
                Row = 5;
                CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                info.DateTimeFormat.DateSeparator = "-";
                info.DateTimeFormat.TimeSeparator = ":";
                Headercell = ws.Cell(Row, 1).SetValue("Date From: " + FromDate.ToString(info.DateTimeFormat.ShortDatePattern) + "  To: " + ToDate.ToString(info.DateTimeFormat.ShortDatePattern));
                Headercell.Style.Font.Bold = true;
                ws.Range("A5:E5").Merge();
                Row = 6;
                Headercell = ws.Cell(Row, 1).SetValue("Time Between: " + timeFrom + "  and: " + timeTo);
                Headercell.Style.Font.Bold = true;
                ws.Range("A6:E6").Merge();
                Row = 7;
                Headercell = ws.Cell(Row, 1).SetValue("Printed on: " + DateTime.Today.ToString(info.DateTimeFormat.ShortDatePattern) + "  at " + DateTime.Now.ToString("HH:mm:ss") + "");
                Headercell.Style.Font.Bold = true;
                ws.Range("A7:E7").Merge();
                Row = 9;

                //var Graphimage1 = ws.AddPicture(GraphImageTrafficSummaryByExtensionReport1(ReportEntityList));
                //Graphimage1.MoveTo(ws.Cell(Row, 1).Address);
                //Graphimage1.Scale(.3);

                //var Graphimage2 = ws.AddPicture(GraphImageTrafficSummaryByExtensionReport2(ReportEntityList));
                //Graphimage2.MoveTo(ws.Cell(Row, 7).Address);
                //Graphimage2.Scale(.3);
                //// optional: resize picture
                //Row = 28;
                //ws.Range("A8:L27").Merge();

                Headercell = ws.Cell(Row, 1).SetValue("");
                Headercell = ws.Cell(Row, 2).SetValue("");
                Headercell = ws.Cell(Row, 3).SetValue("Inbound Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("");
                Headercell = ws.Cell(Row, 5).SetValue("");
                Headercell = ws.Cell(Row, 6).SetValue("");

                Row++;

                Headercell = ws.Cell(Row, 1).SetValue("Area Code");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Area");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Total Calls");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Answered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("Unanswered");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 6).SetValue("Duration");
                Headercell.Style.Font.Bold = true;
                //Headercell = ws.Cell(Row, 11).SetValue("Cost (" + Currency + ")");
                //Headercell.Style.Font.Bold = true;

                List<ReportTrafficSummaryByAreaCodeInboundEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                 .Select(y => new ReportTrafficSummaryByAreaCodeInboundEntity
                 {

                     IncomingCalls = y.Sum(d => d.IncomingCalls),
                     IncomingAnsweredCalls = y.Sum(d => d.IncomingAnsweredCalls),
                     IncomingUnAnsweredCalls = y.Sum(d => d.IncomingUnAnsweredCalls),
                     IncomingCallDuration = y.Sum(d => d.IncomingCallDuration),

                 }).ToList();

                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    ReportTrafficSummaryByAreaCodeInboundEntity obj = ReportEntityList[i];

                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.AreaCode);
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.AreaDescription.ToString());
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.IncomingCalls.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(obj.IncomingAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 5).SetValue(obj.IncomingUnAnsweredCalls.ToString());
                    BodyCell = ws.Cell(Row, 6).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration));

                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        ReportTrafficSummaryByAreaCodeInboundEntity obj1 = TempReportEntityGroupedList[0];

                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Total");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");

                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(obj1.IncomingCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(obj1.IncomingAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 5).SetValue(obj1.IncomingUnAnsweredCalls.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 6).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj1.IncomingCallDuration));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");

                        Row++;
                    }

                }
                wb.SaveAs(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName));
                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }



        public static string GenerateTrafficSummaryByAreaCodeInboundReportExcel(string Currency, int RingTimeThreshold, string ReportName, List<ReportTrafficSummaryByAreaCodeInboundEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                String separator = ",";
                StringBuilder output = new StringBuilder();


              


                String[] headings = { "Area Code", "Area", "Total Calls", "Answered", "Unanswered", "Duration" };
                output.AppendLine(string.Join(separator, headings));

                foreach (var col in ReportEntityList)
                {
                    String[] newLine = { col.AreaCode, col.AreaDescription, col.IncomingCalls.ToString(), col.IncomingAnsweredCalls.ToString(), col.IncomingUnAnsweredCalls.ToString(), ReportsCommonMethods.GetTimeFromSeconds(col.IncomingCallDuration) };
                    output.AppendLine(string.Join(separator, newLine));
                }

                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".csv";

                File.AppendAllText(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), output.ToString());




                return FileName;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {

            }

        }

        //****************************************************************************//

        //public static Image GraphImageCostSummaryByRegionReport1(List<ReportCostSummaryByTypeEntity> ReportEntityList)
        //{
        //    Series s = new Series("X");
        //    s.ChartType = SeriesChartType.Pie;
        //    s.IsValueShownAsLabel = false;
        //    s.Font = new System.Drawing.Font("Calibri", 8f);
        //    s.LabelForeColor = System.Drawing.Color.White;

        //    Chart c = new Chart();
        //    c.Width = 840;
        //    c.Height =840;
        //    c.Series.Add(s);
        //    ChartArea chartArea = new ChartArea();

        //    c.ChartAreas.Add(chartArea);
        //    //c.ChartAreas[0].Position.Auto = false;
        //    c.Titles.Add("Total Calls by Region");
        //    c.Titles[0].Font= new System.Drawing.Font("Calibri", 24f);
        //    c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });
        //    c.Series["X"]["PieLabelStyle"] = "Disabled";
        //    for (int i = 0; i < ReportEntityList.Count; i++)
        //    {
        //        ReportCostSummaryByTypeEntity obj = ReportEntityList[i];
        //        c.Series["X"].Points.AddXY(obj.CostType, obj.TotalCalls);
        //    }

        //    c.Series["X"].LegendText = "#VALX (#PERCENT)";
        //    c.DataManipulator.Sort(PointSortOrder.Descending, c.Series["X"]);

        //    var ChartImage = new MemoryStream();
        //    c.SaveImage(ChartImage, ChartImageFormat.Png);
        //    Image pdfImage = Image.GetInstance(ChartImage.GetBuffer());
        //    return pdfImage;
        //}
        public static MemoryStream GraphImageCostSummaryByRegionReport1(List<ReportCostSummaryByTypeEntity> ReportEntityList)
        {
            Series s = new Series("X");
            s.ChartType = SeriesChartType.Pie;
            s.IsValueShownAsLabel = false;
            s.Font = new System.Drawing.Font("Calibri", 8f);
            s.LabelForeColor = System.Drawing.Color.White;

            Chart c = new Chart();
            c.Width = 840;
            c.Height = 840;
            c.Series.Add(s);
            ChartArea chartArea = new ChartArea();

            c.ChartAreas.Add(chartArea);
            //c.ChartAreas[0].Position.Auto = false;
            c.Titles.Add("Total Calls by Call Type");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });
            c.Series["X"]["PieLabelStyle"] = "Disabled";
            for (int i = 0; i < ReportEntityList.Count; i++)
            {
                ReportCostSummaryByTypeEntity obj = ReportEntityList[i];
                c.Series["X"].Points.AddXY(obj.CostType, obj.TotalCalls);
            }

            c.Series["X"].LegendText = "#VALX (#PERCENT)";
            c.DataManipulator.Sort(PointSortOrder.Descending, c.Series["X"]);

            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Png);
            //Image pdfImage = Image.GetInstance(ChartImage.GetBuffer());
            return ChartImage;
        }
        //public static Image GraphImageCostSummaryByRegionReport2(List<ReportCostSummaryByTypeEntity> ReportEntityList)
        //{
        //    Series s = new Series("X");
        //    s.ChartType = SeriesChartType.Pie;
        //    s.IsValueShownAsLabel = false;
        //    s.Font = new System.Drawing.Font("Calibri", 24f);
        //    s.LabelForeColor = System.Drawing.Color.White;

        //    Chart c = new Chart();
        //    c.Width = 840;
        //    c.Height = 840;
        //    c.Series.Add(s);
        //    c.BackColor = System.Drawing.Color.White;
        //    ChartArea chartArea = new ChartArea();
        //    c.ChartAreas.Add(chartArea);
        //    c.Titles.Add("Total Cost by Region");
        //    c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
        //    c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });
        //    c.Series["X"]["PieLabelStyle"] = "Disabled";

        //    for (int i = 0; i < ReportEntityList.Count; i++)
        //    {
        //        ReportCostSummaryByTypeEntity obj = ReportEntityList[i];
        //        c.Series["X"].Points.AddXY(obj.CostType, obj.Cost);

        //    }

        //    //c.Series["X"].Label = "#PERCENT{P2}";
        //    c.Series["X"].LegendText = "#VALX (#PERCENT)";
        //    c.DataManipulator.Sort(PointSortOrder.Descending, c.Series["X"]);
        //    var ChartImage = new MemoryStream();
        //    c.SaveImage(ChartImage, ChartImageFormat.Png);
        //    Image pdfImage = Image.GetInstance(ChartImage.GetBuffer());
        //    return pdfImage;
        //}
        public static MemoryStream GraphImageCostSummaryByRegionReport2(List<ReportCostSummaryByTypeEntity> ReportEntityList)
        {
            Series s = new Series("X");
            s.ChartType = SeriesChartType.Pie;
            s.IsValueShownAsLabel = false;
            s.Font = new System.Drawing.Font("Calibri", 24f);
            s.LabelForeColor = System.Drawing.Color.White;

            Chart c = new Chart();
            c.Width = 840;
            c.Height = 840;
            c.Series.Add(s);
            c.BackColor = System.Drawing.Color.White;
            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Total Cost by Call Type");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });
            c.Series["X"]["PieLabelStyle"] = "Disabled";

            for (int i = 0; i < ReportEntityList.Count; i++)
            {
                ReportCostSummaryByTypeEntity obj = ReportEntityList[i];
                c.Series["X"].Points.AddXY(obj.CostType, obj.Cost);

            }
            //c.Series["X"].Label = "#PERCENT{P2}";
            c.Series["X"].LegendText = "#VALX (#PERCENT)";
            c.DataManipulator.Sort(PointSortOrder.Descending, c.Series["X"]);
            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Png);

            return ChartImage;
        }
        public static MemoryStream GraphImageCostSummaryByExtensionReport(List<ReportCostSummaryByExtensionEntity> ReportEntityList)
        {
            Series s1 = new Series("Total Cost");
            s1.ChartType = SeriesChartType.Column;
            s1.IsValueShownAsLabel = true;
            s1.Font = new System.Drawing.Font("Calibri", 24f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            s1.LabelForeColor = System.Drawing.Color.White;
            s1["LabelStyle"] = "Bottom";

            Chart c = new Chart();
            c.Width = 1750;
            c.Height = 840;
            c.Series.Add(s1);
            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            //c.ChartAreas[0].Position.X = 0;
            //c.ChartAreas[0].Position.Y = 0;
            //c.ChartAreas[0].AxisX.IsMarginVisible = false;
            //c.ChartAreas[0].AxisX.LineWidth = 0;
            //c.ChartAreas[0].InnerPlotPosition = new ElementPosition(0,0,1695,840) ;
            c.Titles.Add("Total Cost by Extension");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            //c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 7f) });

            c.ChartAreas[0].AxisX.Title = "Extension";
            c.ChartAreas[0].AxisY.Title = "Total Cost";
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 21f);
            //c.ChartAreas[0].AxisX.Maximum = 20;

            for (int i = 0; i < ReportEntityList.Count; i++)
            {
                if (i < 20)
                {
                    ReportCostSummaryByExtensionEntity obj = ReportEntityList[i];
                    c.Series["Total Cost"].Points.AddXY(obj.Extension, obj.Cost);
                }

            }
            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);
            return ChartImage;
        }
        public static MemoryStream GraphImageCostSummaryByPhoneReport(List<ReportCostSummaryByPhoneEntity> ReportEntityList)
        {
            Series s1 = new Series("Total Cost");
            s1.ChartType = SeriesChartType.Bar;
            s1.IsValueShownAsLabel = true;
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            //x.IsVisibleInLegend = true;

            s1.Font = new System.Drawing.Font("Calibri", 8f);

            Chart c = new Chart();
            c.Width = 800;
            c.Height = 200;
            c.Series.Add(s1);
            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Total Cost by Dialled Number");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 8f);

            c.ChartAreas[0].AxisX.Title = "Dialled Number";
            c.ChartAreas[0].AxisY.Title = "Total Cost";
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 7f);
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 7f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 8f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 8f);
            c.ChartAreas[0].AxisX.Maximum = 20;
            for (int i = 0; i < ReportEntityList.Count; i++)
            {
                ReportCostSummaryByPhoneEntity obj = ReportEntityList[i];
                c.Series["Total Cost"].Points.AddXY(obj.DialledNumber, obj.Cost);

            }
            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static MemoryStream GraphImageCostSummaryByHourReport(List<ReportCostSummaryByHourEntity> ReportEntityList)
        {
            Series s1 = new Series("Total Cost");
            s1.ChartType = SeriesChartType.Column;
            s1.IsValueShownAsLabel = true;
            s1.Font = new System.Drawing.Font("Calibri", 24f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            s1.LabelForeColor = System.Drawing.Color.White;
            s1["LabelStyle"] = "Bottom";

            Series s2 = new Series("Avg. Duration");
            s2.ChartType = SeriesChartType.Line;
            s2.IsValueShownAsLabel = true;
            s2.Font = new System.Drawing.Font("Calibri", 24f);
            s2.Color = System.Drawing.ColorTranslator.FromHtml("#86878b"); ;
            s2.LabelForeColor = System.Drawing.Color.Black;


            Chart c = new Chart();
            c.Width = 1750;
            c.Height = 840;
            c.Series.Add(s1);
            c.Series.Add(s2);
            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Total Cost by Hour");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            c.ChartAreas[0].AxisX.Title = "Hour";
            c.ChartAreas[0].AxisY.Title = "Total Cost";
            c.ChartAreas[0].AxisY2.Title = "Duration (mm:ss)";
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisY2.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY2.TitleFont = new System.Drawing.Font("Calibri", 24f);

            c.Series[0].YAxisType = AxisType.Primary;
            c.Series[1].YAxisType = AxisType.Secondary;
            c.Series[1].BorderWidth = 10;
            c.Series[1].YValueType = ChartValueType.Time;
            c.Series[1].LabelFormat = "  mm:ss";
            c.ChartAreas[0].AxisY2.LabelStyle.Format = "  mm:ss";
            for (int i = 0; i < ReportEntityList.Count; i++)
            {
                ReportCostSummaryByHourEntity obj = ReportEntityList[i];
                c.Series["Total Cost"].Points.AddXY(ReportsCommonMethods.GetHourFormat(obj.Hour), obj.Cost);
                c.Series["Avg. Duration"].Points.AddXY(obj.Hour, ReportsCommonMethods.GetMinutesFromSeconds(obj.AvgDuration));

            }
            //c.ChartAreas[0].AxisX.Minimum = 0;
            //c.ChartAreas[0].AxisY2.MajorTickMark.Interval = 60 * 60;  // one tick per hour
            //addCustomLabels(c.ChartAreas[0], s2, 60 * 30);
            //

            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static MemoryStream GraphImageCostSummaryByDayReport(List<ReportCostSummaryByDayEntity> ReportEntityList)
        {
            Series s1 = new Series("Total Cost");
            s1.ChartType = SeriesChartType.Column;
            s1.IsValueShownAsLabel = true;
            s1.Font = new System.Drawing.Font("Calibri", 24f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            s1.LabelForeColor = System.Drawing.Color.White;
            s1["LabelStyle"] = "Bottom";

            Chart c = new Chart();
            c.Width = 1750;
            c.Height = 840;
            c.Series.Add(s1);
            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Total Cost by Day");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            //c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 7f) });

            c.ChartAreas[0].AxisX.Title = "Day";
            c.ChartAreas[0].AxisY.Title = "Total Cost";
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 24f);

            List<ReportCostSummaryByDayEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => x.Day)
            .Select(y => new ReportCostSummaryByDayEntity
            {
                Day = y.First().Day,
                TotalCalls = y.Sum(d => d.TotalCalls),
                Cost = y.Sum(d => d.Cost),
            }).ToList();

            for (int i = 0; i < TempReportEntityGroupedList.Count; i++)
            {
                ReportCostSummaryByDayEntity obj = TempReportEntityGroupedList[i];
                c.Series["Total Cost"].Points.AddXY(obj.Day, obj.Cost);


            }
            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static MemoryStream GraphImageCostSummaryByExtensionItemisedReport(List<ReportCostSummaryByExtensionItemisedEntity> ReportEntityList)
        {
            Series s1 = new Series("Total Calls");
            s1.ChartType = SeriesChartType.Bar;
            s1.IsValueShownAsLabel = true;
            s1.Font = new System.Drawing.Font("Calibri", 24f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            Chart c = new Chart();
            c.Width = 800;
            c.Height = 200;
            c.Series.Add(s1);
            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Total Calls by Dialled Number");
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 7f) });
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 8f);
            c.ChartAreas[0].AxisX.Title = "Extension";
            c.ChartAreas[0].AxisY.Title = "Calls";
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 7f);
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 7f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 8f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 8f);

            for (int i = 0; i < ReportEntityList.Count; i++)
            {
                ReportCostSummaryByExtensionItemisedEntity obj = ReportEntityList[i];
                c.Series["Total Calls"].Points.AddXY(obj.Extension, obj.Cost);

            }
            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static MemoryStream GraphImageResponseSummaryByHourReport1(List<ReportResponseSummaryByHourEntity> ReportEntityList)
        {
            Series s1 = new Series("Longest Ring");
            s1.ChartType = SeriesChartType.StackedBar;
            s1.IsValueShownAsLabel = false;
            s1.Font = new System.Drawing.Font("Calibri", 24f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            s1.LabelForeColor = System.Drawing.Color.Black;

            Series s2 = new Series("Avg. Ring");
            s2.ChartType = SeriesChartType.StackedBar;
            s2.IsValueShownAsLabel = false;
            s2.Font = new System.Drawing.Font("Calibri", 24f);
            s2.Color = System.Drawing.ColorTranslator.FromHtml("#86878b"); ;
            s2.LabelForeColor = System.Drawing.Color.Black;

            Chart c = new Chart();
            c.Width = 1210;
            c.Height = 1110;
            c.Series.Add(s2);
            c.Series.Add(s1);

            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Answered Calls (Ring Time)");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });

            c.ChartAreas[0].AxisX.Title = "Hour";
            c.ChartAreas[0].AxisY.Title = "Seconds";
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 24f);


            for (int i = 0; i < ReportEntityList.Count; i++)
            {
                ReportResponseSummaryByHourEntity obj = ReportEntityList[i];
                c.Series["Avg. Ring"].Points.AddXY(ReportsCommonMethods.GetHourFormat(obj.Hour), obj.AvgRingAnswered);
                c.Series["Longest Ring"].Points.AddXY(ReportsCommonMethods.GetHourFormat(obj.Hour), obj.MaxRingAnswered - obj.AvgRingAnswered);

            }
            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static MemoryStream GraphImageResponseSummaryByHourReport2(List<ReportResponseSummaryByHourEntity> ReportEntityList)
        {
            Series s1 = new Series("Answered");
            s1.ChartType = SeriesChartType.StackedBar;
            s1.IsValueShownAsLabel = true;
            s1.Font = new System.Drawing.Font("Calibri", 24f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            s1.LabelForeColor = System.Drawing.Color.Black;



            Series s2 = new Series("Unanswered");
            s2.ChartType = SeriesChartType.StackedBar;
            s2.IsValueShownAsLabel = true;
            //s1.Color = System.Drawing.Color.White;
            s2.Font = new System.Drawing.Font("Calibri", 24f);
            s2.Color = System.Drawing.ColorTranslator.FromHtml("#86878b"); ;
            s2.LabelForeColor = System.Drawing.Color.Black;

            Chart c = new Chart();
            c.Width = 1210;
            c.Height = 1110;
            c.Series.Add(s1);
            c.Series.Add(s2);
            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Total Calls by Hour");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });

            c.ChartAreas[0].AxisX.Title = "Hour";
            c.ChartAreas[0].AxisY.Title = "Calls";
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 24f);

            for (int i = 0; i < ReportEntityList.Count; i++)
            {
                ReportResponseSummaryByHourEntity obj = ReportEntityList[i];
                if (obj.AnsweredCalls <= 0)
                {
                    obj.AnsweredCalls = 0;
                }
                if (obj.LostCalls <= 0)
                {
                    obj.LostCalls = 0;
                }
                c.Series["Answered"].Points.AddXY(ReportsCommonMethods.GetHourFormat(obj.Hour), obj.AnsweredCalls);
                c.Series["Unanswered"].Points.AddXY(ReportsCommonMethods.GetHourFormat(obj.Hour), obj.LostCalls);

                if (obj.AnsweredCalls <= 0)
                {
                    c.Series["Answered"].Points[i].Label = " ";
                }
                if (obj.LostCalls <= 0)
                {
                    c.Series["Unanswered"].Points[i].Label = " ";
                }
            }
            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static MemoryStream GraphImageResponseSummaryByDayReport1(List<ReportResponseSummaryByDayEntity> ReportEntityList)
        {
            Series s1 = new Series("Longest Ring");
            s1.ChartType = SeriesChartType.StackedBar;
            s1.IsValueShownAsLabel = false;
            s1.Font = new System.Drawing.Font("Calibri", 24f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            s1.LabelForeColor = System.Drawing.Color.Black;

            Series s2 = new Series("Avg. Ring");
            s2.ChartType = SeriesChartType.StackedBar;
            s2.IsValueShownAsLabel = false;
            s2.Font = new System.Drawing.Font("Calibri", 24f);
            s2.Color = System.Drawing.ColorTranslator.FromHtml("#86878b"); ;
            s2.LabelForeColor = System.Drawing.Color.Black;

            Chart c = new Chart();
            c.Width = 1210;
            c.Height = 1110;
            c.Series.Add(s2);
            c.Series.Add(s1);

            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Answered Calls (Ring Time)");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });

            c.ChartAreas[0].AxisX.Title = "Day";
            c.ChartAreas[0].AxisY.Title = "Seconds";
            c.ChartAreas[0].AxisX.Interval = 1;

            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 24f);

            List<ReportResponseSummaryByDayEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => x.Day)
            .Select(y => new ReportResponseSummaryByDayEntity
            {
                Day = y.First().Day,
                TotalCalls = y.Sum(d => d.TotalCalls),
                AvgRingAnswered = y.Average(d => d.AvgRingAnswered),
                MaxRingAnswered = Convert.ToInt32(y.Average(d => d.MaxRingAnswered))
            }).ToList();

            for (int i = 0; i < TempReportEntityGroupedList.Count; i++)
            {
                ReportResponseSummaryByDayEntity obj = TempReportEntityGroupedList[i];
                c.Series["Avg. Ring"].Points.AddXY(obj.Day, obj.AvgRingAnswered);
                c.Series["Longest Ring"].Points.AddXY(obj.Day, obj.MaxRingAnswered - obj.AvgRingAnswered);
            }

            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static MemoryStream GraphImageResponseSummaryByDayReport2(List<ReportResponseSummaryByDayEntity> ReportEntityList)
        {

            Series s1 = new Series("Answered");
            s1.ChartType = SeriesChartType.StackedBar;
            s1.IsValueShownAsLabel = true;
            s1.Font = new System.Drawing.Font("Calibri", 24f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            s1.LabelForeColor = System.Drawing.Color.Black;


            Series s2 = new Series("Unanswered");
            s2.ChartType = SeriesChartType.StackedBar;
            s2.IsValueShownAsLabel = true;
            s2.Font = new System.Drawing.Font("Calibri", 24f);
            s2.Color = System.Drawing.ColorTranslator.FromHtml("#86878b"); ;
            s2.LabelForeColor = System.Drawing.Color.Black;

            Chart c = new Chart();
            c.Width = 1210;
            c.Height = 1110;
            c.Series.Add(s2);
            c.Series.Add(s1);

            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Total Calls by Day");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });

            c.ChartAreas[0].AxisX.Title = "Day";
            c.ChartAreas[0].AxisY.Title = "Calls";
            c.ChartAreas[0].AxisX.Interval = 1;

            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 24f);

            List<ReportResponseSummaryByDayEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => x.Day)
            .Select(y => new ReportResponseSummaryByDayEntity
            {
                Day = y.First().Day,
                TotalCalls = y.Sum(d => d.TotalCalls),
                AnsweredCalls = y.Sum(d => d.AnsweredCalls),
                LostCalls = y.Sum(d => d.LostCalls),
            }).ToList();

            for (int i = 0; i < TempReportEntityGroupedList.Count; i++)
            {
                ReportResponseSummaryByDayEntity obj = TempReportEntityGroupedList[i];

                if (obj.AnsweredCalls <= 0)
                {
                    obj.AnsweredCalls = 0;
                }
                if (obj.LostCalls <= 0)
                {
                    obj.LostCalls = 0;
                }


                c.Series["Answered"].Points.AddXY(obj.Day, obj.AnsweredCalls);

                c.Series["Unanswered"].Points.AddXY(obj.Day, obj.LostCalls);

                if (obj.AnsweredCalls <= 0)
                {
                    c.Series["Answered"].Points[i].Label = " ";
                }
                if (obj.LostCalls <= 0)
                {
                    c.Series["Unanswered"].Points[i].Label = " ";
                }
            }

            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static MemoryStream GraphImageResponseSummaryByExtensionReport1(List<ReportResponseSummaryByExtensionEntity> ReportEntityList)
        {
            Series s1 = new Series("Longest Ring");
            s1.ChartType = SeriesChartType.StackedBar;
            s1.IsValueShownAsLabel = false;
            s1.Font = new System.Drawing.Font("Calibri", 24f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            s1.LabelForeColor = System.Drawing.Color.Black;

            Series s2 = new Series("Avg. Ring");
            s2.ChartType = SeriesChartType.StackedBar;
            s2.IsValueShownAsLabel = false;
            s2.Font = new System.Drawing.Font("Calibri", 24f);
            s2.Color = System.Drawing.ColorTranslator.FromHtml("#86878b"); ;
            s2.LabelForeColor = System.Drawing.Color.Black;

            Chart c = new Chart();
            c.Width = 1210;
            c.Height = 1110;
            c.Series.Add(s2);
            c.Series.Add(s1);

            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Answered Calls (Ring Time)");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });

            c.ChartAreas[0].AxisX.Title = "Extension";
            c.ChartAreas[0].AxisY.Title = "Seconds";
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 24f);
            //c.ChartAreas[0].AxisX.Maximum = 20;
            List<ReportResponseSummaryByExtensionEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => x.Extension)
            .Select(y => new ReportResponseSummaryByExtensionEntity
            {
                Extension = y.First().Extension,
                TotalCalls = y.Sum(d => d.TotalCalls),
                AvgRingAnswered = y.Average(d => d.AvgRingAnswered),
                MaxRingAnswered = Convert.ToInt32(y.Average(d => d.MaxRingAnswered))
            }).ToList();

            for (int i = 0; i < TempReportEntityGroupedList.Count; i++)
            {
                if (i < 20)
                {
                    ReportResponseSummaryByExtensionEntity obj = TempReportEntityGroupedList[i];
                    c.Series["Avg. Ring"].Points.AddXY(obj.Extension, obj.AvgRingAnswered);
                    c.Series["Longest Ring"].Points.AddXY(obj.Extension, obj.MaxRingAnswered - obj.AvgRingAnswered);
                }
            }

            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static MemoryStream GraphImageResponseSummaryByExtensionReport2(List<ReportResponseSummaryByExtensionEntity> ReportEntityList)
        {

            Series s1 = new Series("Answered");
            s1.ChartType = SeriesChartType.StackedBar;
            s1.IsValueShownAsLabel = true;            
            s1.Font = new System.Drawing.Font("Calibri", 24f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            s1.LabelForeColor = System.Drawing.Color.Black;
           // s1.
            //s1.LabelFormat

            Series s2 = new Series("Unanswered");
            s2.ChartType = SeriesChartType.StackedBar;
            s2.IsValueShownAsLabel = true;
            s2.Font = new System.Drawing.Font("Calibri", 24f);
            s2.Color = System.Drawing.ColorTranslator.FromHtml("#86878b"); ;
            s2.LabelForeColor = System.Drawing.Color.Black;

            Chart c = new Chart();
            c.Width = 1210;
            c.Height = 1110;
            c.Series.Add(s2);
            c.Series.Add(s1);

            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Total Calls by Extension");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });


            c.ChartAreas[0].AxisX.Title = "Extension";
            c.ChartAreas[0].AxisY.Title = "Calls";
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 24f);
            // c.ChartAreas[0].AxisX.Maximum = 20;
            for (int i = 0; i < ReportEntityList.Count; i++)
            {
                if (i < 20)
                {
                    ReportResponseSummaryByExtensionEntity obj = ReportEntityList[i];
                    if (obj.AnsweredCalls <= 0)
                    {
                        obj.AnsweredCalls = 0;
                    }
                    if (obj.LostCalls <= 0)
                    {
                        obj.LostCalls = 0;
                    }

                    
                    c.Series["Answered"].Points.AddXY(obj.Extension, obj.AnsweredCalls);
                    c.Series["Unanswered"].Points.AddXY(obj.Extension, obj.LostCalls);

                    if (obj.AnsweredCalls <= 0)
                    {
                        c.Series["Answered"].Points[i].Label = " ";
                    }
                    if (obj.LostCalls <= 0)
                    {
                        c.Series["Unanswered"].Points[i].Label = " ";
                    }
                   // c.AlignDataPointsByAxisLabel();
                }
            }

            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static MemoryStream GraphImageTrafficSummaryByHourReport1(List<ReportTrafficSummaryByHourEntity> ReportEntityList)
        {
            Series s1 = new Series("Inbound");
            s1.ChartType = SeriesChartType.Bar;
            s1.IsValueShownAsLabel = true;
            s1.Font = new System.Drawing.Font("Calibri", 20f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            //s1.LabelForeColor = System.Drawing.ColorTranslator.FromHtml("#800000");
            s1.LabelForeColor = System.Drawing.Color.Black;
            s1["BarLabelStyle"] = "Center";
            //s1["BarLabelStyle"] = "Outside";

            Series s2 = new Series("Outbound");
            s2.ChartType = SeriesChartType.Bar;
            s2.IsValueShownAsLabel = true;
            s2.Font = new System.Drawing.Font("Calibri", 20f);
            s2.Color = System.Drawing.ColorTranslator.FromHtml("#86878b");
            //s2.LabelForeColor = System.Drawing.ColorTranslator.FromHtml("#86878b");
            s2.LabelForeColor = System.Drawing.Color.Black;
            s2["BarLabelStyle"] = "Center";
            //s2["BarLabelStyle"] = "Outside";

            Chart c = new Chart();
            c.Width = 1210;
            c.Height = 1110;
            c.Series.Add(s1);
            c.Series.Add(s2);


            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Calls by Hour");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });

            c.ChartAreas[0].AxisX.Title = "Hour";
            c.ChartAreas[0].AxisY.Title = "Calls";
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 24f);
            for (int i = 0; i < ReportEntityList.Count; i++)
            {
                ReportTrafficSummaryByHourEntity obj = ReportEntityList[i];
                if (obj.IncomingCalls > 0)
                    c.Series["Inbound"].Points.AddXY(ReportsCommonMethods.GetHourFormat(obj.Hour), obj.IncomingCalls);
                if (obj.OutgoingCalls > 0)
                    c.Series["Outbound"].Points.AddXY(ReportsCommonMethods.GetHourFormat(obj.Hour), obj.OutgoingCalls);
            }
            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static MemoryStream GraphImageTrafficSummaryByHourReport2(List<ReportTrafficSummaryByHourEntity> ReportEntityList)
        {
            Series s1 = new Series("Inbound");
            s1.ChartType = SeriesChartType.Bar;
            //s1.IsValueShownAsLabel = true;
            s1.Font = new System.Drawing.Font("Calibri", 20f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            s1.LabelForeColor = System.Drawing.Color.Black;
            s1["BarLabelStyle"] = "Center";
            //s1.LabelForeColor = System.Drawing.ColorTranslator.FromHtml("#800000");
            //s1["BarLabelStyle"] = "Right";
            //s1["BarLabelStyle"] = "Outside";

            Series s2 = new Series("Outbound");
            s2.ChartType = SeriesChartType.Bar;
            //s2.IsValueShownAsLabel = true;
            s2.Font = new System.Drawing.Font("Calibri", 20f);
            s2.Color = System.Drawing.ColorTranslator.FromHtml("#86878b");
            s2.LabelForeColor = System.Drawing.Color.Black;
            s2["BarLabelStyle"] = "Center";
            //s2.LabelForeColor = System.Drawing.ColorTranslator.FromHtml("#86878b");
            ////s2["BarLabelStyle"] = "Right";
            //s2["BarLabelStyle"] = "Outside";

            Chart c = new Chart();
            c.Width = 1210;
            c.Height = 1110;
            c.Series.Add(s1);
            c.Series.Add(s2);
            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Duration by Hour");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });

            c.ChartAreas[0].AxisX.Title = "Hour";
            c.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            c.ChartAreas[0].AxisY.Title = "Duration (HH:MM:SS)";
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 24f);

            for (int i = 0; i < ReportEntityList.Count; i++)
            {
                ReportTrafficSummaryByHourEntity obj = ReportEntityList[i];
                c.Series["Inbound"].Points.AddXY(ReportsCommonMethods.GetHourFormat(obj.Hour), obj.IncomingCallDuration / 3600);
                c.Series["Outbound"].Points.AddXY(ReportsCommonMethods.GetHourFormat(obj.Hour), obj.OutgoingCallDuration / 3600);
                if (obj.IncomingCallDuration > 0)
                    c.Series["Inbound"].Points[i].Label = ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration);
                if (obj.OutgoingCallDuration > 0)
                    c.Series["Outbound"].Points[i].Label = ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration);
            }
            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static MemoryStream GraphImageTrafficSummaryByDayReport1(List<ReportTrafficSummaryByDayEntity> ReportEntityList)
        {
            Series s1 = new Series("Inbound");
            s1.ChartType = SeriesChartType.Bar;
            s1.IsValueShownAsLabel = true;
            s1.Font = new System.Drawing.Font("Calibri", 24f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            s1.LabelForeColor =System.Drawing.Color.Black ; //System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            s1["BarLabelStyle"] = "Center";
          //  s1["BarLabelStyle"] = "Outside";

            Series s2 = new Series("Outbound");
            s2.ChartType = SeriesChartType.Bar;
            s2.IsValueShownAsLabel = true;
            s2.Font = new System.Drawing.Font("Calibri", 24f);
            s2.Color = System.Drawing.ColorTranslator.FromHtml("#86878b");
            s2.LabelForeColor = System.Drawing.Color.Black;// System.Drawing.ColorTranslator.FromHtml("#86878b");
            s2["BarLabelStyle"] = "Center";
            //s2["BarLabelStyle"] = "Outside";

            Chart c = new Chart();
            c.Width = 1210;
            c.Height = 1110;
            c.Series.Add(s1);
            c.Series.Add(s2);

            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Calls by Day");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });

            c.ChartAreas[0].AxisX.Title = "Day";
            c.ChartAreas[0].AxisY.Title = "Calls";
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 24f);

            List<ReportTrafficSummaryByDayEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => x.Day)
            .Select(y => new ReportTrafficSummaryByDayEntity
            {
                Day = y.First().Day,
                IncomingCalls = y.Sum(d => d.IncomingCalls),
                OutgoingCalls = y.Sum(d => d.OutgoingCalls),

            }).ToList();

            for (int i = 0; i < TempReportEntityGroupedList.Count; i++)
            {
                ReportTrafficSummaryByDayEntity obj = TempReportEntityGroupedList[i];
                if (obj.IncomingCalls > 0)
                    c.Series["Inbound"].Points.AddXY(obj.Day, obj.IncomingCalls);

                if (obj.OutgoingCalls > 0)
                    c.Series["Outbound"].Points.AddXY(obj.Day, obj.OutgoingCalls);

            }
            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static MemoryStream GraphImageTrafficSummaryByDayReport2(List<ReportTrafficSummaryByDayEntity> ReportEntityList)
        {

            Series s1 = new Series("Inbound");
            s1.ChartType = SeriesChartType.Bar;
            //s1.IsValueShownAsLabel = true;
            s1.Font = new System.Drawing.Font("Calibri", 24f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            s1.LabelForeColor = System.Drawing.Color.Black; //System.Drawing.ColorTranslator.FromHtml("#800000");
            s1["BarLabelStyle"] = "Center";
            //s1["BarLabelStyle"] = "Outside";

            Series s2 = new Series("Outbound");
            s2.ChartType = SeriesChartType.Bar;
            //s2.IsValueShownAsLabel = true;
            s2.Font = new System.Drawing.Font("Calibri", 24f);
            s2.Color = System.Drawing.ColorTranslator.FromHtml("#86878b");
            s2.LabelForeColor = System.Drawing.Color.Black;// System.Drawing.ColorTranslator.FromHtml("#86878b");
            s2["BarLabelStyle"] = "Center";
            //s2["BarLabelStyle"] = "Outside";

            Chart c = new Chart();
            c.Width = 1210;
            c.Height = 1110;
            c.Series.Add(s1);
            c.Series.Add(s2);
            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Duration by Day");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });

            c.ChartAreas[0].AxisX.Title = "Day";

            c.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            c.ChartAreas[0].AxisY.Title = "Duration (HH:MM:SS)";
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 24f);

            List<ReportTrafficSummaryByDayEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => x.Day)
           .Select(y => new ReportTrafficSummaryByDayEntity
           {
               Day = y.First().Day,
               IncomingCalls = y.Sum(d => d.IncomingCalls),
               OutgoingCalls = y.Sum(d => d.OutgoingCalls),

           }).ToList();


            for (int i = 0; i < TempReportEntityGroupedList.Count; i++)
            {
                ReportTrafficSummaryByDayEntity obj = ReportEntityList[i];
                c.Series["Inbound"].Points.AddXY(obj.Day, obj.IncomingCallDuration / 3600);
                c.Series["Outbound"].Points.AddXY(obj.Day, obj.OutgoingCallDuration / 3600);
                if (obj.IncomingCallDuration > 0)
                    c.Series["Inbound"].Points[i].Label= ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration);

                if (obj.OutgoingCallDuration > 0)
                    c.Series["Outbound"].Points[i].Label= ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration);

            }
            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static MemoryStream GraphImageTrafficSummaryByExtensionReport1(List<ReportTrafficSummaryByExtensionEntity> ReportEntityList)
        {
            Series s1 = new Series("Inbound");
            s1.ChartType = SeriesChartType.Bar;
            s1.IsValueShownAsLabel = true;
            s1.Font = new System.Drawing.Font("Calibri", 20f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            s1.LabelForeColor = System.Drawing.Color.Black;// System.Drawing.ColorTranslator.FromHtml("#800000"); 
            s1["BarLabelStyle"] = "Center";
            //s1["BarLabelStyle"] = "Outside";

            Series s2 = new Series("Outbound");
            s2.ChartType = SeriesChartType.Bar;
            s2.IsValueShownAsLabel = true;
            s2.Font = new System.Drawing.Font("Calibri", 20f);
            s2.Color = System.Drawing.ColorTranslator.FromHtml("#86878b");
            s2.LabelForeColor = System.Drawing.Color.Black;//System.Drawing.ColorTranslator.FromHtml("#86878b");
            s2["BarLabelStyle"] = "Center";
            //s2["BarLabelStyle"] = "Outside";

            Chart c = new Chart();
            c.Width = 1210;
            c.Height = 1110;
            c.Series.Add(s1);
            c.Series.Add(s2);

            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Top 10 Extensions for most calls made");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });

            c.ChartAreas[0].AxisX.Title = "Extension";
            c.ChartAreas[0].AxisY.Title = "Calls";
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 24f);
            //c.ChartAreas[0].AxisX.Maximum = 20;
            List<ReportTrafficSummaryByExtensionEntity> ReportEntityListReduced = ReportEntityList.OrderByDescending(x => x.IncomingCallDuration).Take(10).ToList().OrderBy(x => x.IncomingCallDuration).ToList();
            for (int i = 0; i < ReportEntityListReduced.Count; i++)
            {
                //if (i < 20)
                //{
                    ReportTrafficSummaryByExtensionEntity obj = ReportEntityListReduced[i];
                if (obj.IncomingCalls > 0)
                    c.Series["Inbound"].Points.AddXY(obj.Extension, obj.IncomingCalls);

                if (obj.OutgoingCalls > 0)
                    c.Series["Outbound"].Points.AddXY(obj.Extension, obj.OutgoingCalls);
                //}
            }
            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);
            return ChartImage;
        }
        public static MemoryStream GraphImageTrafficSummaryByExtensionReport2(List<ReportTrafficSummaryByExtensionEntity> ReportEntityList)
        {

            Series s1 = new Series("Inbound");
            s1.ChartType = SeriesChartType.Bar;
            //s1.IsValueShownAsLabel = true;
            s1.Font = new System.Drawing.Font("Calibri", 20f);
            s1.Color = System.Drawing.ColorTranslator.FromHtml("#ed1b24");
            //s1.LabelForeColor = System.Drawing.ColorTranslator.FromHtml("#800000");
            //s1["BarLabelStyle"] = "Center";
            //s1["BarLabelStyle"] = "Outside";
            s1.LabelForeColor = System.Drawing.Color.Black;
            s1["BarLabelStyle"] = "Center";

            Series s2 = new Series("Outbound");
            s2.ChartType = SeriesChartType.Bar;
            //s2.IsValueShownAsLabel = true;
            s2.Font = new System.Drawing.Font("Calibri", 20f);
            s2.Color = System.Drawing.ColorTranslator.FromHtml("#86878b");
            //s2.LabelForeColor = System.Drawing.ColorTranslator.FromHtml("#86878b"); ;
            //s2["BarLabelStyle"] = "Center";
            //s2["BarLabelStyle"] = "Outside";
            s2.LabelForeColor = System.Drawing.Color.Black;
            s2["BarLabelStyle"] = "Center";

            Chart c = new Chart();
            c.Width = 1210;
            c.Height = 1110;
            c.Series.Add(s1);
            c.Series.Add(s2);
            c.BackColor = System.Drawing.Color.White;

            ChartArea chartArea = new ChartArea();
            c.ChartAreas.Add(chartArea);
            c.Titles.Add("Total Duration");
            c.Titles[0].Font = new System.Drawing.Font("Calibri", 24f);
            c.Legends.Add(new Legend("Default") { Docking = Docking.Bottom, Alignment = System.Drawing.StringAlignment.Center, Font = new System.Drawing.Font("Calibri", 21f) });

            c.ChartAreas[0].AxisX.Title = "Extension";
            c.ChartAreas[0].AxisY.LabelStyle.ForeColor=System.Drawing.Color.White;
            c.ChartAreas[0].AxisY.Title =  "Duration (HH:MM:SS)";
            c.ChartAreas[0].AxisX.Interval = 1;
            c.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibri", 21f);
            c.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibri", 24f);
            c.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibri", 24f);
            //c.ChartAreas[0].AxisX.Maximum = 2;
            //c.ChartAreas[0].AxisX.IsMarginVisible = false;
            List<ReportTrafficSummaryByExtensionEntity> ReportEntityListReduced = ReportEntityList.OrderByDescending(x => x.IncomingCallDuration).Take(10).ToList().OrderBy(x => x.IncomingCallDuration).ToList();
            for (int i = 0; i < ReportEntityListReduced.Count; i++)
            {
                //if (i < 20)
                //{
                    ReportTrafficSummaryByExtensionEntity obj = ReportEntityListReduced[i];
                    c.Series["Inbound"].Points.AddXY(obj.Extension, obj.IncomingCallDuration/3600);
                    c.Series["Outbound"].Points.AddXY(obj.Extension, obj.OutgoingCallDuration/3600);
                if (obj.IncomingCallDuration > 0)
                    c.Series["Inbound"].Points[i].Label = ReportsCommonMethods.GetTimeFromSeconds(obj.IncomingCallDuration);

                if (obj.OutgoingCallDuration > 0)
                    c.Series["Outbound"].Points[i].Label = ReportsCommonMethods.GetTimeFromSeconds(obj.OutgoingCallDuration);
                //}
            }
            var ChartImage = new MemoryStream();
            c.SaveImage(ChartImage, ChartImageFormat.Bmp);

            return ChartImage;
        }
        public static string mm_ss(int seconds)
        {
            int sec = seconds % 60;
            int min = ((seconds - sec) / 60) % 60;
            return min + ":" + sec.ToString("00");
        }
        public static void addCustomLabels(ChartArea ca, Series series, int interval)
        {
            // we get the maximum form the 1st y-value
            int max = (int)series.Points.Select(x => x.YValues[0]).Max();
            // we delete any CLs we have
            ca.AxisY2.CustomLabels.Clear();
            // now we add new custom labels
            for (int i = 0; i < max; i += interval)
            {
                CustomLabel cl = new CustomLabel();
                cl.FromPosition = i - interval / 2;
                cl.ToPosition = i + interval / 2;
                cl.Text = mm_ss(i);
                ca.AxisY2.CustomLabels.Add(cl);
            }
        }

    }
}