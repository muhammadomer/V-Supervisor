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
using ClosedXML.Excel;

namespace vSuperMTClient
{
    public partial class RecorderReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static string GetActivityLogReport(string ReportType, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Users, string WeekDays)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vRecordClientDB = HttpContext.Current.Session["vRecordDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vRecordClientDB);

                List<CallActionsEntity> ReportEntityList = new List<CallActionsEntity>();

                ReportEntityList = ReportsDALObj.GetActivityLogReport(FromDate, ToDate, timeFrom, timeTo, Users, WeekDays);

                if (ReportType == "PDF")
                {
                    string FileName = GenerateActivityLogReport(ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateActivityLogReportCSV(ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetCallNotesReport(string ReportType, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Users, string WeekDays)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vRecordClientDB = HttpContext.Current.Session["vRecordDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vRecordClientDB);

                List<CallNotesEntity> ReportEntityList = new List<CallNotesEntity>();

                ReportEntityList = ReportsDALObj.GetCallNotesReport(FromDate, ToDate, timeFrom, timeTo, Users, WeekDays);

                if (ReportType == "PDF")
                {
                    string FileName = GenerateCallNotesReport(ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateCallNotesReportCSV(ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetDiskUsageCostReport(string ReportType, string Currency, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo,string RecordingPath,float DiskUsageCostPerMin, string WeekDays)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vRecordClientDB = HttpContext.Current.Session["vRecordDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vRecordClientDB);

                List<ReportDiskUsageCostEntity> ReportEntityList = new List<ReportDiskUsageCostEntity>();

                ReportEntityList = ReportsDALObj.GetDiskUsageCostReport(FromDate, ToDate, timeFrom, timeTo, RecordingPath, DiskUsageCostPerMin, WeekDays);
                if (ReportType == "PDF")
                {
                    string FileName = GenerateDiskUsageCostReport(DiskUsageCostPerMin, Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateDiskUsageCostReportCSV(DiskUsageCostPerMin, Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GetDurationCostReport(string ReportType, string Currency, string ReportName, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo,float DurationCostPerMin, string WeekDays)
        {
            try
            {
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vRecordClientDB = HttpContext.Current.Session["vRecordDB"].ToString();
                ReportsDAL ReportsDALObj = new ReportsDAL(vRecordClientDB);

                List<ReportDurationCostEntity> ReportEntityList = new List<ReportDurationCostEntity>();

                ReportEntityList = ReportsDALObj.GetDurationCostReport(FromDate, ToDate, timeFrom, timeTo, DurationCostPerMin, WeekDays);
                if (ReportType == "PDF")
                {
                    string FileName = GenerateDurationCostReport(DurationCostPerMin, Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
                else
                {
                    string FileName = GenerateDurationCostReportCSV(DurationCostPerMin, Currency, ReportName, ReportEntityList, FromDate, ToDate, timeFrom, timeTo);
                    return FileName;
                }
            }
            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
            return null;
        }
        public static string GenerateActivityLogReport(string ReportName, List<CallActionsEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
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
                    Datatable = new PdfPTable(8);
                Datatable.TotalWidth = 782;
                Datatable.LockedWidth = true;
                Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                Datatable.SetWidths(new float[] { 1f, 1f,0.5f, 1f, 1f, 0.7f, 2f, 0.7f });
                Datatable.SpacingBefore = 15f;
                Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Date"));
                Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Full Name"));
                Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("-"));
                Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Call Date"));
                Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("From"));
                Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("To"));
                Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Extension"));
                Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Action"));



                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    CallActionsEntity obj = ReportEntityList[i];

                    if (i == ReportEntityList.Count - 1)
                    {
                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Time_Stamp.ToString()));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.UserName.ToString()));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowCell("-"));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.CallDate.ToString()));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Call_CLI.ToString()));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Call_DDI.ToString()));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.ExtensionWithName.ToString()));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Action.ToString()));


                    }
                    else
                    {
                        Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Time_Stamp.ToString()));
                        Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.UserName.ToString()));
                        Datatable.AddCell(ReportsCommonMethods.RowCell("-"));
                        Datatable.AddCell(ReportsCommonMethods.RowCell(obj.CallDate.ToString()));
                        Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Call_CLI.ToString()));
                        Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Call_DDI.ToString()));
                        Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.ExtensionWithName.ToString()));
                        Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Action.ToString()));

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

        public static string GenerateCallNotesReport(string ReportName, List<CallNotesEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4.Rotate(), 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\" + HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1] + "\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
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
                    Datatable = new PdfPTable(7);
                    Datatable.TotalWidth = 782;
                    Datatable.LockedWidth = true;
                    Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    Datatable.SetWidths(new float[] { 1f, 1f, 1f, 0.5f, 1f, 1f, 4f });
                    Datatable.SpacingBefore = 15f;
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Call Time"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("From"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("To"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("-"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Added At"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Added By"));
                    Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Note"));


                    int LastCallDetailID = 0;
                    for (int i = 0; i < ReportEntityList.Count; i++)
                    {
                        CallNotesEntity obj = ReportEntityList[i];
                        if (LastCallDetailID != obj.CallDetailId)
                        {
                            if (i == ReportEntityList.Count - 1)
                            {
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.CallDate.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Call_CLI.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Call_DDI.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell("-"));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.NotesTime.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.FullName.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Note.ToString()));
                            }
                            else
                            {
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.CallDate.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Call_CLI.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Call_DDI.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.RowCell("-"));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.NotesTime.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.FullName.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Note.ToString()));

                            }
                            LastCallDetailID = obj.CallDetailId;
                        }
                        else
                        {
                            if (i == ReportEntityList.Count - 1)
                            {
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(""));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell("-"));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.NotesTime.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.FullName.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Note.ToString()));
                            }
                            else
                            {
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(""));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(""));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(""));
                                Datatable.AddCell(ReportsCommonMethods.RowCell("-"));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.NotesTime.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.FullName.ToString()));
                                Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Note.ToString()));

                            }
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
        public static string GenerateDiskUsageCostReport(float DiskUsageCostPerMin, string Currency, string ReportName, List<ReportDiskUsageCostEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4, 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
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
                    Datatable = new PdfPTable(4);
                Datatable.TotalWidth = 535;
                Datatable.LockedWidth = true;
                Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f });
                Datatable.SpacingBefore = 15f;
                Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Date"));
                Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Day"));
                Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Disk Usage (MB)"));
                Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Cost (" + Currency + ")"));

                double TotalDiskUsage = 0;
                double TotalCost = 0;
                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    ReportDiskUsageCostEntity obj = ReportEntityList[i];
                    TotalDiskUsage += obj.DiskUsage;
                    TotalCost += obj.Cost;
                    if (i == ReportEntityList.Count - 1)
                    {
                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Time_Stamp.ToShortDateString()));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Day));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowCell(Math.Round((obj.DiskUsage/1024),2).ToString()));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowCell(obj.Cost.ToString()));

                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Cost Per Min: " + DiskUsageCostPerMin + "" + Currency + ""));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored(""));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(Math.Round((TotalDiskUsage / 1024), 2).ToString()));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(TotalCost.ToString()));
                    }
                    else
                    {
                        Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Time_Stamp.ToShortDateString()));
                        Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Day));
                        Datatable.AddCell(ReportsCommonMethods.RowCell(Math.Round((obj.DiskUsage/1024), 2).ToString()));
                        Datatable.AddCell(ReportsCommonMethods.RowCell(obj.Cost.ToString()));
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
        public static string GenerateDurationCostReport(float DurationCostPerMin, string Currency,string ReportName, List<ReportDurationCostEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
        {
            try
            {
                string FileName = ReportName.Replace(" ", "_") + DateTime.UtcNow.Ticks + ".pdf";
                Document document = new Document(PageSize.A4, 0f, 0f, 30f, 30f);
                FileStream output = new FileStream(HttpContext.Current.Server.MapPath("Reports\\"+ HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]+"\\" + FileName), FileMode.Create);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable Headertable = null;
                PdfPTable Datatable = null;
                document.Open();
                Headertable = new PdfPTable(1);
                Headertable.TotalWidth = 535f;
                Headertable.LockedWidth = true;
                Headertable.SetTotalWidth(new float[] { 535f });//fixed widths
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
                    Datatable = new PdfPTable(4);
                Datatable.TotalWidth = 535f;
                Datatable.LockedWidth = true;
                Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                Datatable.SetWidths(new float[] { 1f, 1f, 1f, 1f});
                Datatable.SpacingBefore = 15f;
                Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Date"));
                Datatable.AddCell(ReportsCommonMethods.HeaderRowLeftCell("Day"));
                Datatable.AddCell(ReportsCommonMethods.HeaderRowCell("Duration"));
                Datatable.AddCell(ReportsCommonMethods.HeaderRowRightCell("Cost ("+Currency+")"));

                // List<ReportDurationCostEntity> TempReportEntityGroupedList = ReportEntityList.GroupBy(x => 1)
                //.Select(y => new ReportDurationCostEntity()
                //{
                //    Duration = y.Sum(d => d.Duration),
                //   // Cost = y.Sum(d => d.Cost),


                //}).ToList();

                double TotalDuration = 0;
                double TotalCost = 0;


                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    ReportDurationCostEntity obj = ReportEntityList[i];
                    TotalDuration += obj.Duration;
                    TotalCost += obj.Cost;
                    if (i == ReportEntityList.Count - 1)
                    {
                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Time_Stamp.ToShortDateString()));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCell(obj.Day.ToString()));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration*60)));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowRightCell(Math.Round(obj.Cost,2).ToString()));

                        
                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored("Cost Per Minute: "+ DurationCostPerMin + ""+Currency+""));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowLeftCellBGColored(""));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowCellBGColored(ReportsCommonMethods.GetTimeFromSeconds(TotalDuration * 60)));
                        Datatable.AddCell(ReportsCommonMethods.BottomRowRightCellBGColored(Math.Round(TotalCost,2).ToString()));
                    }
                    else
                    {
                        Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Time_Stamp.ToShortDateString()));
                        Datatable.AddCell(ReportsCommonMethods.RowLeftCell(obj.Day.ToString()));
                        Datatable.AddCell(ReportsCommonMethods.RowCell(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration * 60)));
                        Datatable.AddCell(ReportsCommonMethods.RowRightCell(Math.Round(obj.Cost,2).ToString()));


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

        public static string GenerateActivityLogReportCSV(string ReportName, List<CallActionsEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
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

                Headercell = ws.Cell(Row, 1).SetValue("Date");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Full Name");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("-");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Call Date");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("From");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 6).SetValue("To");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 7).SetValue("Extension");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 8).SetValue("Action");
                Headercell.Style.Font.Bold = true;

                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    CallActionsEntity obj = ReportEntityList[i];

                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.Time_Stamp.ToString());
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.UserName.ToString());
                    BodyCell = ws.Cell(Row, 3).SetValue("-");
                    BodyCell = ws.Cell(Row, 4).SetValue(obj.CallDate.ToString());
                    BodyCell = ws.Cell(Row, 5).SetValue(obj.Call_CLI.ToString());
                    BodyCell = ws.Cell(Row, 6).SetValue(obj.Call_DDI.ToString());
                    BodyCell = ws.Cell(Row, 7).SetValue(obj.ExtensionWithName.ToString());
                    BodyCell = ws.Cell(Row, 8).SetValue(obj.Action.ToString());
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

        public static string GenerateCallNotesReportCSV(string ReportName, List<CallNotesEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
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

                Headercell = ws.Cell(Row, 1).SetValue("Call Time");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("From");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("To");
                Headercell.Style.Font.Bold = true;
              
                Headercell = ws.Cell(Row, 4).SetValue("-");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 5).SetValue("Added At");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 6).SetValue("Added By");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 7).SetValue("Note");
                Headercell.Style.Font.Bold = true;

                int LastCallDetailID = 0;
                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    CallNotesEntity obj = ReportEntityList[i];
                    if (obj.CallDetailId != LastCallDetailID)
                    {
                        IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.CallDate.ToString());
                        BodyCell = ws.Cell(Row, 2).SetValue(obj.Call_CLI.ToString());
                        BodyCell = ws.Cell(Row, 3).SetValue(obj.Call_DDI.ToString());
                        BodyCell = ws.Cell(Row, 4).SetValue("-");
                        BodyCell = ws.Cell(Row, 5).SetValue(obj.NotesTime.ToString());
                        BodyCell = ws.Cell(Row, 6).SetValue(obj.FullName.ToString());
                        BodyCell = ws.Cell(Row, 7).SetValue(obj.Note.ToString());
                     
                        LastCallDetailID = obj.CallDetailId;
                    }
                    else
                    {
                        IXLCell BodyCell = ws.Cell(Row, 1).SetValue("");
                        BodyCell = ws.Cell(Row, 2).SetValue("");
                        BodyCell = ws.Cell(Row, 3).SetValue("");
                        BodyCell = ws.Cell(Row, 4).SetValue("-");
                        BodyCell = ws.Cell(Row, 5).SetValue(obj.NotesTime.ToString());
                        BodyCell = ws.Cell(Row, 6).SetValue(obj.FullName.ToString());
                        BodyCell = ws.Cell(Row, 7).SetValue(obj.Note.ToString());
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
        public static string GenerateDiskUsageCostReportCSV(float DiskUsageCostPerMin, string Currency, string ReportName, List<ReportDiskUsageCostEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
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

                Headercell = ws.Cell(Row, 1).SetValue("Date");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Day");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Disk Usage (MB)");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Cost (" + Currency + ")");
                Headercell.Style.Font.Bold = true;

                double TotalDiskUsage = 0;
                double TotalCost = 0;
                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    ReportDiskUsageCostEntity obj = ReportEntityList[i];
                    TotalDiskUsage += obj.DiskUsage;
                    TotalCost += obj.Cost;

                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.Time_Stamp.ToShortDateString());
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.Day);
                    BodyCell = ws.Cell(Row, 3).SetValue(obj.DiskUsage.ToString());
                    BodyCell = ws.Cell(Row, 4).SetValue(Math.Round(obj.Cost, 2).ToString());

                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Cost Per Min: " + DiskUsageCostPerMin + "" + Currency + "");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(TotalDiskUsage.ToString());
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(Math.Round(TotalCost, 2).ToString());
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
        public static string GenerateDurationCostReportCSV(float DurationCostPerMin, string Currency, string ReportName, List<ReportDurationCostEntity> ReportEntityList, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo)
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

                Headercell = ws.Cell(Row, 1).SetValue("Date");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 2).SetValue("Day");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 3).SetValue("Duration");
                Headercell.Style.Font.Bold = true;
                Headercell = ws.Cell(Row, 4).SetValue("Cost (" + Currency + ")");
                Headercell.Style.Font.Bold = true;

                double TotalDuration = 0;
                double TotalCost = 0;
                for (int i = 0; i < ReportEntityList.Count; i++)
                {
                    Row++;
                    ReportDurationCostEntity obj = ReportEntityList[i];
                    TotalDuration += obj.Duration;
                    TotalCost += obj.Cost;

                    IXLCell BodyCell = ws.Cell(Row, 1).SetValue(obj.Time_Stamp.ToShortDateString());
                    BodyCell = ws.Cell(Row, 2).SetValue(obj.Day);
                    BodyCell = ws.Cell(Row, 3).SetValue(ReportsCommonMethods.GetTimeFromSeconds(obj.Duration * 60));
                    BodyCell = ws.Cell(Row, 4).SetValue(Math.Round(obj.Cost, 2).ToString());

                    if (i == ReportEntityList.Count - 1)
                    {
                        Row++;
                        IXLCell FooterCell = ws.Cell(Row, 1).SetValue("Cost Per Minute: " + DurationCostPerMin + "" + Currency + "");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 2).SetValue("");
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 3).SetValue(ReportsCommonMethods.GetTimeFromSeconds(TotalDuration * 60));
                        FooterCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#c0c0c0");
                        FooterCell = ws.Cell(Row, 4).SetValue(Math.Round(TotalCost, 2).ToString());
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

    }
}