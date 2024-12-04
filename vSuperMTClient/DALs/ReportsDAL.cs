using LogApp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using vSuperMTClient.Entities;
namespace vSuperMTClient.DALs
{
    public class ReportsDAL
    {
        MySqlConnection con;

        public int ConvertStringToInteger(string val)
        {
            int iReturn = 0;
            try
            {
                iReturn = int.Parse(val);
            }
            catch { iReturn = 0; }
            return iReturn;
        }

        public double ConvertStringToDouble(string val)
        {
            double iReturn = 0;
            try
            {
                iReturn = double.Parse(val);
            }
            catch { iReturn = 0; }
            return iReturn;
        }

        public float ConvertStringToFloat(string val)
        {
            float iReturn = 0;
            try
            {
                iReturn = float.Parse(val);
            }
            catch { iReturn = 0; }
            return iReturn;
        }

        public ReportsDAL(string ClientDB)
        {
            string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
            con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB));
        }

        public List<ReportCallsSummaryEntity> GetCallsSummaryReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string WeekDays)
        {
            try
            {

               
                List<ReportCallsSummaryEntity> ReportEntityList = new List<ReportCallsSummaryEntity>();
                

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_CallsSummaryReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
               
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCallsSummaryEntity ReportEntityObj = new ReportCallsSummaryEntity();
                    ReportEntityObj.BoardTitle = row["BoardTitle"].ToString();
                    ReportEntityObj.TotalCalls = ConvertStringToInteger(row["TotalCalls"].ToString());
                    ReportEntityObj.TotalAnsweredCalls = ConvertStringToInteger(row["TotalAnsweredCalls"].ToString());
                    ReportEntityObj.TotalAbandonedCalls = ConvertStringToInteger(row["TotalAbandonedCalls"].ToString());
                    ReportEntityObj.AbandonedCallsPercentage = ConvertStringToFloat(row["AbandonedCallsPercentage"].ToString());
                    ReportEntityObj.AVGWaitTime = ConvertStringToDouble(row["AVGWaitTime"].ToString());
                    ReportEntityObj.LongestWaitingTime = ConvertStringToDouble(row["LongestWaitingTime"].ToString());
                   

                    ReportEntityList.Add(ReportEntityObj);
                }
               
                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportOverflowedCalls> GetCallsoverflowedReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, int WorkingHours, string WeekDays)
        {
            try
            {
                List<ReportOverflowedCalls> ReportEntityList = new List<ReportOverflowedCalls>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_OverflowedCallReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WorkingHours", WorkingHours));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                cmd.CommandTimeout = 0;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportOverflowedCalls ReportEntityObj = new ReportOverflowedCalls();
                    ReportEntityObj.Title = row["Title"].ToString();
                    DateTime dt1 = DateTime.ParseExact(row["StartDate"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info);
                    ReportEntityObj.StartDate = datedisplay;
                    //ReportEntityObj.StartDate = row["StartDate"].ToString();
                    ReportEntityObj.Totaloverflowedcall = ConvertStringToInteger(row["Totaloverflowedcall"].ToString());
                    ReportEntityObj.Totaloverflowedcallwaittime = ConvertStringToInteger(row["Totaloverflowedcallwaittime"].ToString());
                    ReportEntityObj.Totaloverflowedcallerwaiting = ConvertStringToInteger(row["Totaloverflowedcallerwaiting"].ToString());
                    ReportEntityObj.TotaloverflowedcallOUTOFHOUR = ConvertStringToInteger(row["TotaloverflowedcallOUTOFHOUR"].ToString());
                    ReportEntityObj.TotaloverflowedcallDTMF = ConvertStringToInteger(row["TotaloverflowedcallDTMF"].ToString());
                    ReportEntityObj.TotalTransfersIn = ConvertStringToInteger(row["TotalTransfersIn"].ToString());
                    ReportEntityObj.OverFlowInTotalWaitTime = ConvertStringToInteger(row["OverFlowInTotalWaitTime"].ToString());
                    ReportEntityObj.TotaloverflowedcallInQueues = ConvertStringToInteger(row["TotaloverflowedcallInQueues"].ToString()); 
                    ReportEntityObj.OverFlowInWaitTime = row["OverFlowInWaitTime"].ToString();
                    ReportEntityObj.TotaloverflowedcallNOAgent = ConvertStringToInteger(row["TotaloverflowedcallNOAgent"].ToString());
                    if (ReportEntityObj.OverFlowInTotalWaitTime > 0 && ReportEntityObj.Totaloverflowedcall>0)
                    {
                        ReportEntityObj.OverFlowInAVGWaitTime = (ReportEntityObj.OverFlowInTotalWaitTime / ReportEntityObj.Totaloverflowedcall).ToString();
                    }
                    else
                    {
                        ReportEntityObj.OverFlowInAVGWaitTime = "0";
                    }
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<RoutingTreeReport> GetCallsRoutingReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string WeekDays)
        {
            try
            {
                List<RoutingTreeReport> ReportEntityList = new List<RoutingTreeReport>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_CallsRoutingReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                cmd.CommandTimeout = 0;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    RoutingTreeReport ReportEntityObj = new RoutingTreeReport();
                    ReportEntityObj.BoardID =Convert.ToInt32(row["BoardID"]);
                    ReportEntityObj.ABoardID = Convert.ToInt32(row["ABoardID"]);
                    ReportEntityObj.CallID = row["CallID"].ToString();
                    ReportEntityObj.EventId = Convert.ToInt32(row["EventId"]);
                    ReportEntityObj.Title = row["Title"].ToString();
                    ReportEntityObj.AgentExtension = row["AgentExtension"].ToString();
                    ReportEntityObj.AgentName = row["AgentName"].ToString();

                    DateTime dt1 = DateTime.ParseExact(row["AgentStartTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info);
                    ReportEntityObj.AgentStartTime = datedisplay;
                    ReportEntityObj.DTAgentStartTime = dt1;
                    //ReportEntityObj.AgentStartTime = row["AgentStartTime"].ToString();

                    dt1 = DateTime.ParseExact(row["CallArrived"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);                  
                    datedisplay = dt1.ToString(info);
                    ReportEntityObj.CallArrived = datedisplay;
                    ReportEntityObj.DTCallArrived = dt1;
                    //ReportEntityObj.CallArrived = Convert.ToDateTime(row["CallArrived"].ToString());
                    dt1 = DateTime.ParseExact(row["CallEnded"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    datedisplay = dt1.ToString(info);
                    ReportEntityObj.CallEnded = datedisplay;
                    // ReportEntityObj.CallEnded = Convert.ToDateTime(row["CallEnded"].ToString());
                    ReportEntityObj.CLI = row["CLI"].ToString();
                    ReportEntityObj.Duration = Convert.ToInt32(row["Duration"].ToString());
                    ReportEntityObj.TotalCalls = Convert.ToInt32(row["TotalCalls"]);
                    ReportEntityObj.TotalNotAccepted = Convert.ToInt32(row["TotalNotAccepted"]);
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportCallsSummaryEntity> GetCallsSummaryReport2(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string WeekDays)
        {
            try
            {


                List<ReportCallsSummaryEntity> ReportEntityList = new List<ReportCallsSummaryEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_CallsSummaryReport2", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;

                con.Open();
                da.Fill(dt);
                con.Close();
                
                foreach (DataRow row in dt.Rows)
                {

                    //foreach (DataColumn col in dt.Columns)
                    //{
                    //    //test for null here
                    //    if (row[col] == DBNull.Value)
                    //    {
                    //        //tableHasNull = true;
                    //        throw new Exception("custom message : " + col);

                    //        //LogApp.Log4Net.WriteException();
                    //    }
                    //}

                    ReportCallsSummaryEntity ReportEntityObj = new ReportCallsSummaryEntity();
                    ReportEntityObj.BoardTitle = row["BoardTitle"].ToString();
                    ReportEntityObj.TotalCalls = ConvertStringToInteger(row["TotalCalls"].ToString());
                    ReportEntityObj.TotalAnsweredCalls = ConvertStringToInteger(row["TotalAnsweredCalls"].ToString());
                    ReportEntityObj.TotalAbandonedCalls = ConvertStringToInteger(row["TotalAbandonedCalls"].ToString());
                    ReportEntityObj.AbandonedCallsPercentage = ConvertStringToFloat(row["AbandonedCallsPercentage"].ToString());
                    ReportEntityObj.AVGWaitTime = ConvertStringToDouble(row["AVGWaitTime"].ToString());
                    ReportEntityObj.LongestWaitingTime = ConvertStringToDouble(row["LongestWaitingTime"].ToString());

                    ReportEntityObj.TotalTransferredIn = ConvertStringToInteger(row["TotalTransferredIn"].ToString());
                    ReportEntityObj.OverFlowInAnsweredCalls = ConvertStringToInteger(row["OverFlowInAnsweredCalls"].ToString());
                    ReportEntityObj.OverFlowInAbndCalls = ConvertStringToInteger(row["OverFlowInAbndCalls"].ToString());
                    ReportEntityObj.OverFlowInAbandonedCallsPercentage = ConvertStringToFloat(row["OverFlowInAbandonedCallsPercentage"].ToString());
                    ReportEntityObj.OverFlowInAVGWaitTime = ConvertStringToDouble(row["OverFlowInAVGWaitTime"].ToString());
                    ReportEntityObj.OverFlowInLongestWaitingTime = ConvertStringToDouble(row["OverFlowInLongestWaitingTime"].ToString());
                    ReportEntityObj.TotalYBVTansferredOut = ConvertStringToInteger(row["TotalYBVTansferredOut"].ToString());
                    ReportEntityObj.ShortestWaitingTime = ConvertStringToDouble(row["ShortestWaitingTime"].ToString());
                    ReportEntityObj.VoicemailCall = ConvertStringToInteger(row["VoicemailCalls"].ToString());

                    try
                    {
                        ReportEntityObj.CallDays = ConvertStringToInteger(row["CallDays"].ToString());
                    }
                    catch(Exception ex)
                    {
                        ReportEntityObj.CallDays = 0;
                        Log4Net.WriteException(ex);
                    }
                    try
                    {
                        ReportEntityObj.LoginDate = ConvertStringToInteger(row["LoginDate"].ToString());
                    }
                    catch (Exception ex)
                    {
                        ReportEntityObj.LoginDate = 0;
                        Log4Net.WriteException(ex);
                    }

                    try
                    {
                        ReportEntityObj.LoginAgent = ConvertStringToInteger(row["LoginAgent"].ToString());
                    }
                    catch (Exception ex)
                    {
                        ReportEntityObj.LoginAgent = 0;
                        Log4Net.WriteException(ex);
                    }
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList.OrderBy(w => w.BoardTitle).ToList();
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public List<ReportDDIsSummaryEntity> GetDDIsSummaryReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string WeekDays)
        {
            try
            {


                List<ReportDDIsSummaryEntity> ReportEntityList = new List<ReportDDIsSummaryEntity>();

                Log4Net.WriteLog("getting DB", LogType.GENERALLOG);
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_IVR_DDIsSummaryReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;

                con.Open();
                da.Fill(dt);
                con.Close();
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                    Log4Net.WriteLog("getting DB result:" + dt.Rows.Count, LogType.GENERALLOG);
                else
                    Log4Net.WriteLog("DB result:null", LogType.GENERALLOG);

                foreach (DataRow row in dt.Rows)
                {
                    ReportDDIsSummaryEntity ReportEntityObj = new ReportDDIsSummaryEntity();
                    ReportEntityObj.BoardTitle = row["BoardTitle"].ToString();
                    ReportEntityObj.TotalIVRCalls = ConvertStringToInteger(row["TotalIVRCalls"].ToString());
                    ReportEntityObj.TotalAnsweredCalls = ConvertStringToInteger(row["TotalAnsweredCalls"].ToString());
                    ReportEntityObj.TotalCallsInQueue = ConvertStringToInteger(row["TotalCallsInQueue"].ToString());

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList.OrderBy(w => w.BoardTitle).ToList();
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public List<ReportIVRDtmfEntity> GetIVRDTMFReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string WeekDays)
        {
            try
            {

                List<ReportIVRDtmfEntity> ReportEntityList = new List<ReportIVRDtmfEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_IVR_DtmftoQueue", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;

                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {

                    //foreach (DataColumn col in dt.Columns)
                    //{
                    //    //test for null here
                    //    if (row[col] == DBNull.Value)
                    //    {
                    //        //tableHasNull = true;
                    //        throw new Exception("custom message : " + col);

                    //        //LogApp.Log4Net.WriteException();
                    //    }
                    //}

                    ReportIVRDtmfEntity ReportEntityObj = new ReportIVRDtmfEntity();
                    ReportEntityObj.BoardTitle = row["BoardTitle"].ToString();
                    ReportEntityObj.QueueName = row["QueueName"].ToString();
                    ReportEntityObj.QueueNumber = row["QueueNumber"].ToString();
                    ReportEntityObj.DTMF = row["DTMF"].ToString();
                    ReportEntityObj.NodeName = row["NodeName"].ToString();
                    ReportEntityObj.TotalCalls = ConvertStringToInteger(row["TotalCalls"].ToString());

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList.OrderBy(w => w.BoardTitle).ToList();
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public List<ReportAbandonedCallsEntity> GetAbandonedCallsReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string WeekDays)
        {
            try
            {
         

                List<ReportAbandonedCallsEntity> ReportEntityList = new List<ReportAbandonedCallsEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_AbandonedCallsReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportAbandonedCallsEntity ReportEntityObj = new ReportAbandonedCallsEntity();
                    
                    ReportEntityObj.BoardTitle = row["BoardTitle"].ToString();
                    ReportEntityObj.CLI = row["CLI"].ToString();
                    ReportEntityObj.DDI = row["DDI"].ToString();
                    DateTime dt1 = DateTime.ParseExact(row["DateTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info);
                    ReportEntityObj.DateTime = datedisplay;
                    // ReportEntityObj.DateTime =   DateTime.ParseExact(row["DateTime"].ToString(), "dd-MM-yyyy HH:mm:ss",null).ToString("dd-MM-yyyy HH:mm:ss");
                    //ReportEntityObj.DateTime = row["DateTime"].ToString();
                    ReportEntityObj.Duration = Convert.ToDouble(row["Duration"].ToString() == "" ? "0" : row["Duration"].ToString());

                    ReportEntityList.Add(ReportEntityObj);
                }
               
                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportAllCallsEntity> GetAllCallsReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string WeekDays)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            //Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            try
            {
                List<ReportAllCallsEntity> ReportEntityList = new List<ReportAllCallsEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_AllCallsReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();

                da.Fill(dt);
                con.Close();
                Log4Net.WriteLog("Current Culture:" + System.Globalization.CultureInfo.CurrentCulture.Name + " CurrentUICulture:" + System.Globalization.CultureInfo.CurrentUICulture + " ShortDatePattern:" + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " ShortDatePatternUI:" + System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern, LogType.GENERALLOG);
                foreach (DataRow row in dt.Rows)
                {
                    ReportAllCallsEntity ReportEntityObj = new ReportAllCallsEntity();
                    ReportEntityObj.BoardTitle = row["BoardTitle"].ToString();
                    ReportEntityObj.CLI = row["CLI"].ToString();
                    ReportEntityObj.DDI = row["DDI"].ToString();
                    //string s = row["DateTime"].ToString();
                    
                    DateTime dt1 = DateTime.ParseExact(row["DateTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                 
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info);
                    ReportEntityObj.DateTime = datedisplay;
                    // ReportEntityObj.DateTime = DateTime.ParseExact(row["DateTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString(System.Globalization.CultureInfo.CurrentUICulture);


                    //ReportEntityObj.DateTime = row["DateTime"].ToString();
                    ReportEntityObj.Duration = Convert.ToDouble(row["Duration"].ToString() == "" ? "0" : row["Duration"].ToString());
                    ReportEntityObj.TotalAgentDuration =DBNull.Value== row["TotalAgentDuration"]  ? 0 : Convert.ToInt32(row["TotalAgentDuration"]);
                    ReportEntityObj.Status = row["Status"].ToString();

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }


        public List<ReportOutboundSchedulerDetailEntity> GetOutboundDetailReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Status)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            //Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            try
            {
                List<ReportOutboundSchedulerDetailEntity> ReportEntityList = new List<ReportOutboundSchedulerDetailEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_OutboundDetails", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@SStatus", Status));
                // cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                //  cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();

                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportOutboundSchedulerDetailEntity ReportEntityObj = new ReportOutboundSchedulerDetailEntity();

                    ReportEntityObj.DialledNumber = row["DialedNumber"].ToString();
                    ReportEntityObj.AgentNumber = row["AgentNumber"].ToString();

                    DateTime dt1 = DateTime.ParseExact(row["DateTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info);
                    ReportEntityObj.DateTime = datedisplay;
                    //ReportEntityObj.DateTime = DateTime.ParseExact(row["DateTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MM-yyyy HH:mm:ss");

                    //ReportEntityObj.DateTime = row["DateTime"].ToString();
                    ReportEntityObj.DialerType = row["DialerType"].ToString();
                    ReportEntityObj.Status = row["Status"].ToString();
                    ReportEntityObj.ScheduleName = DBNull.Value == row["ScheduleName"] ? "N/A" : row["ScheduleName"].ToString();
                    ReportEntityObj.AbandonedQueue = DBNull.Value == row["AbandonedQueue"] ? "N/A" : row["AbandonedQueue"].ToString();
                    ReportEntityObj.ScheduleQueue = DBNull.Value == row["ScheduleQueue"] ? "N/A" : row["ScheduleQueue"].ToString();
                    ReportEntityObj.AgentName = DBNull.Value == row["AgentName"] ? ReportEntityObj.AgentNumber : row["AgentName"].ToString();

                    ReportEntityObj.Attempts = DBNull.Value == row["Attempts"] ? 0 : Convert.ToInt32(row["Attempts"]);
                    ReportEntityObj.AttemptID = DBNull.Value == row["AttemptID"] ? DateTime.Now.Ticks + "" : row["AttemptID"].ToString();

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }


        public List<ReportCallBreakdownByIntervalEntity> GetCallBreakdownByHourReport( DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string WeekDays, int TimeInterval)
        {
            try
            {
                               
                List<ReportCallBreakdownByIntervalEntity> ReportEntityList = new List<ReportCallBreakdownByIntervalEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_CallBreakdownByIntervalReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@TimeInterval", TimeInterval));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                cmd.CommandTimeout = 0;
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportCallBreakdownByIntervalEntity ReportEntityObj = new ReportCallBreakdownByIntervalEntity();
                    ReportEntityObj.BoardTitle = row["BoardTitle"].ToString();
                    ReportEntityObj.TotalCalls = ConvertStringToInteger(row["TotalCalls"].ToString());
                    ReportEntityObj.TotalAnsweredCalls = ConvertStringToInteger(row["TotalAnsweredCalls"].ToString());
                    ReportEntityObj.TotalAbandonedCalls = ConvertStringToInteger(row["TotalAbandonedCalls"].ToString());
                    ReportEntityObj.AbandonedCallsPercentage = ConvertStringToDouble(row["AbandonedCallsPercentage"].ToString());
                    ReportEntityObj.TimeKey = row["TimeKey"].ToString();
                    ReportEntityObj.LongestWaitingTime = ConvertStringToDouble(row["LongestWaitingTime"].ToString());
                    //string s = row["Date"].ToString();
                    
                    DateTime dt1 = DateTime.ParseExact(row["Date"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.Date = datedisplay;
                   // ReportEntityObj.Date  = DateTime.ParseExact(row["Date"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString();
                     

                    ReportEntityList.Add(ReportEntityObj);
                }
                
                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportAgentAvailabilityEntity> GetAgentAvailabilityReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string Agents, string WeekDays)
        {
            try
            {
                List<ReportAgentAvailabilityEntity> ReportEntityList = new List<ReportAgentAvailabilityEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_AgentAvailabilityReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@Agents", Agents));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportAgentAvailabilityEntity ReportEntityObj = new ReportAgentAvailabilityEntity();
                    ReportEntityObj.AgentName = row["AgentName"].ToString();
                    ReportEntityObj.GroupName = row["GroupTitle"].ToString();
                    ReportEntityObj.LoggedInTime = Convert.ToDouble(row["LoggedInTime"].ToString() == "" ? "0" : row["LoggedInTime"].ToString());
                    ReportEntityObj.LoggedOutTime = Convert.ToDouble(row["LoggedOutTime"].ToString() == "" ? "0" : row["LoggedOutTime"].ToString());
                    
                    ReportEntityObj.ACDBusyTime = Convert.ToDouble(row["ACDBusyTime"].ToString() == "" ? "0" : row["ACDBusyTime"].ToString());
                    ReportEntityObj.NonACDBusyTime = Convert.ToDouble(row["NonACDBusyTime"].ToString() == "" ? "0" : row["NonACDBusyTime"].ToString());
                    ReportEntityObj.ClerikalBusyTime = Convert.ToDouble(row["ClerikalBusyTime"].ToString() == "" ? "0" : row["ClerikalBusyTime"].ToString());
                    ReportEntityObj.TempAbsTime = Convert.ToDouble(row["TempAbsTime"].ToString() == "" ? "0" : row["TempAbsTime"].ToString());
                    ReportEntityObj.LoggedInCount = ConvertStringToInteger(row["LoggedInCount"].ToString() == "" ? "0" : row["LoggedInCount"].ToString());
                    ReportEntityObj.LoggedOutCount = ConvertStringToInteger(row["LoggedOutCount"].ToString() == "" ? "0" : row["LoggedOutCount"].ToString());
                    ReportEntityObj.TempAbsCount = ConvertStringToInteger(row["TempAbsCount"].ToString() == "" ? "0" : row["TempAbsCount"].ToString());

                    ReportEntityList.Add(ReportEntityObj);
                }
                
                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportAgentCallsCountEntity> GetCallVolumeByAgentReport( DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string Agents, string WeekDays)
        {
            try
            {
                List<ReportAgentCallsCountEntity> ReportEntityList = new List<ReportAgentCallsCountEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_AgentCallsCountReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@Agents", Agents));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
              
                con.Open();

                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportAgentCallsCountEntity ReportEntityObj = new ReportAgentCallsCountEntity();

                    ReportEntityObj.AgentName = row["AgentName"].ToString();
                    ReportEntityObj.GroupName = row["GroupName"].ToString();
                    ReportEntityObj.TotalCalls = row["TotalCalls"].ToString();
                    ReportEntityObj.Hour = row["Hour"].ToString();
                    DateTime dt1 = DateTime.ParseExact(row["Date"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.Date = datedisplay;
                    //string datedisplay = dt1.ToString(info);
                   // ReportEntityObj.Date = datedisplay;
                    // ReportEntityObj.Date = DateTime.ParseExact(row["Date"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString();
                    ReportEntityList.Add(ReportEntityObj);
                }
                
                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportAgentPresenceEntity> GetAgentPresenceReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string Agents, string WeekDays)
        {
            try
            {
                List<ReportAgentPresenceEntity> ReportEntityList = new List<ReportAgentPresenceEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_AgentPresenceReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@Agents", Agents));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportAgentPresenceEntity ReportEntityObj = new ReportAgentPresenceEntity();

                    ReportEntityObj.GroupName = row["GroupName"].ToString();
                    DateTime dt1 = DateTime.ParseExact(row["StartTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info);
                    ReportEntityObj.StartTime = datedisplay;
                    // ReportEntityObj.StartTime = DateTime.ParseExact(row["StartTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MM-yyyy HH:mm:ss");
                    ReportEntityObj.AgentName = row["AgentName"].ToString();
                    ReportEntityObj.Action = row["Action"].ToString();
             
                    
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportAgentCallsSummaryEntity> GetTalkTimebyAgentReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string Agents, string WeekDays)
        {
            try
            {
                List<ReportAgentCallsSummaryEntity> ReportEntityList = new List<ReportAgentCallsSummaryEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_AgentCallsSummaryReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@Agents", Agents));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportAgentCallsSummaryEntity ReportEntityObj = new ReportAgentCallsSummaryEntity();
                    ReportEntityObj.AgentName = row["AgentName"].ToString();
                    ReportEntityObj.GroupName = row["GroupName"].ToString();
                    ReportEntityObj.TotalCalls = Convert.ToDouble(row["TotalCalls"].ToString() == "" ? "0" : row["TotalCalls"].ToString());
                    ReportEntityObj.TotalTalkTime = Convert.ToDouble(row["TotalTalkTime"].ToString() == "" ? "0" : row["TotalTalkTime"].ToString());
                    ReportEntityObj.AvgTalkTime = Convert.ToDouble(row["AvgTalkTime"].ToString() == "" ? "0" : row["AvgTalkTime"].ToString());
                    ReportEntityList.Add(ReportEntityObj);
                }
                
                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportAgentCallsTakenEntity> GetItemisedCallsByAgentReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string Agents, string WeekDays)
        {
            try
            {
                List<ReportAgentCallsTakenEntity> ReportEntityList = new List<ReportAgentCallsTakenEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_AgentCallsTakenReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@Agents", Agents));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                string IsOXO = ConfigurationManager.AppSettings["IsOXO"];
                foreach (DataRow row in dt.Rows)
                {
                    ReportAgentCallsTakenEntity ReportEntityObj = new ReportAgentCallsTakenEntity();
                    ReportEntityObj.AgentName = row["AgentName"].ToString();
                    ReportEntityObj.GroupName = row["GroupName"].ToString();
                    ReportEntityObj.CallType = row["CallType"].ToString();
                    if (IsOXO == "0")
                    {
                        ReportEntityObj.CallType = ReportEntityObj.CallType.Replace("ACD", "CCD");
                    }
                    if (DBNull.Value != row["CallID"])
                    {
                        ReportEntityObj.Extension = row["Extension"].ToString();
                        DateTime dt1 = DateTime.ParseExact(row["TimeofCall"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                        info.DateTimeFormat.DateSeparator = "-";
                        info.DateTimeFormat.TimeSeparator = ":";
                        string datedisplay = dt1.ToString(info);
                        ReportEntityObj.StartTime = datedisplay;
                        // ReportEntityObj.StartTime = row["StartTime"].ToString();
                        ReportEntityObj.Duration = Convert.ToDouble(row["Duration"].ToString() == "" ? "0" : row["Duration"].ToString());
                        ReportEntityObj.QueueDuration = Convert.ToDouble(row["QueueDuration"].ToString() == "" ? "0" : row["QueueDuration"].ToString());
                        ReportEntityObj.CLI = row["CLI"].ToString();
                        ReportEntityList.Add(ReportEntityObj);
                    }
                }
                
                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportGroupAgentCallVolumeEntity> GetAgentSummaryByGroupReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string Agents, string WeekDays)
        {
            //not being used currently
            try
            {
                List<ReportGroupAgentCallVolumeEntity> ReportEntityList = new List<ReportGroupAgentCallVolumeEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_GroupAgentCallVolume", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@Agents", Agents));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportGroupAgentCallVolumeEntity ReportEntityObj = new ReportGroupAgentCallVolumeEntity();
                    ReportEntityObj.GroupNumber = row["GroupNumber"].ToString();
                    ReportEntityObj.Title = row["Title"].ToString();
                    DateTime dt1 = DateTime.ParseExact(row["DateOnly"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.DateOnly = datedisplay;
                    //ReportEntityObj.DateOnly = DateTime.ParseExact(row["DateOnly"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString();
                    ReportEntityObj.HourOnly = row["HourOnly"].ToString();
                    ReportEntityObj.AgentLoggedCount = row["AgentLoggedCount"].ToString();
                    ReportEntityObj.CallsCount = row["CallsCount"].ToString();
                    ReportEntityObj.AVGLogIn = row["AVGLogIn"].ToString();
                    ReportEntityObj.AVGCallBusy = row["AVGCallBusy"].ToString();
                    ReportEntityObj.AVGNonCallBusy = row["AVGNonCallBusy"].ToString();
                    ReportEntityObj.AVGBusy = row["AVGBusy"].ToString();
                    ReportEntityList.Add(ReportEntityObj);
                }
                
                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportAgentConsolidatedEntity> GetAgentConsolidatedReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string Agents, int HangUpThreshold, string WeekDays)
        {
            try
            {
                List<ReportAgentConsolidatedEntity> ReportEntityList = new List<ReportAgentConsolidatedEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_AgentConsolidatedReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@Agents", Agents));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                cmd.CommandTimeout = 0;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportAgentConsolidatedEntity ReportEntityObj = new ReportAgentConsolidatedEntity();
                    ReportEntityObj.AgentName = row["AgentName"].ToString();
                    ReportEntityObj.GroupName = row["GroupTitle"].ToString();

                    DateTime dt1 = DateTime.ParseExact(row["EventDate"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.EventDate = datedisplay;
                    //ReportEntityObj.EventDate = Convert.ToDateTime(row["EventDate"].ToString()).ToShortDateString();
                    //ReportEntityObj.LogInTime = Convert.ToDateTime(row["LogInTime"].ToString());
                    ReportEntityObj.LogInTime = row["LogInTime"].ToString() == "" ? TimeSpan.MaxValue : Convert.ToDateTime(row["LogInTime"].ToString()).TimeOfDay;
                    ReportEntityObj.LogOutTime = row["TotalLoggedInTime"].ToString().Contains("-") || row["LogOutTime"].ToString() == "" ? TimeSpan.MaxValue : Convert.ToDateTime(row["LogOutTime"].ToString()).TimeOfDay;
                    
                         ReportEntityObj.TotalLoggedOutTime = row["TotalLoggedOutTime"].ToString().Contains("-") || row["TotalLoggedOutTime"].ToString().Equals("") ? 0 : Convert.ToDouble(row["TotalLoggedOutTime"].ToString());
                    //ReportEntityObj.TotalLoggedInTime = row["TotalLoggedInTime"].ToString().Contains("-") ? "" : row["TotalLoggedInTime"].ToString();
                    ReportEntityObj.TotalLoggedInTime = row["TotalLoggedInTime"].ToString().Contains("-") || row["TotalLoggedInTime"].ToString().Equals("") ? 0 : Convert.ToDouble(row["TotalLoggedInTime"].ToString());
                    ReportEntityObj.ACDBusyTime = Convert.ToDouble(row["ACDBusyTime"].ToString() == "" ? "0" : row["ACDBusyTime"].ToString());
                    ReportEntityObj.ACDCallnotaccept = Convert.ToDouble(row["ACDnotacceptCount"].ToString() == "" ? "0" : row["ACDnotacceptCount"].ToString());
                    ReportEntityObj.NonACDBusyTime = Convert.ToDouble(row["NonACDBusyTime"].ToString() == "" ? "0" : row["NonACDBusyTime"].ToString());
                    ReportEntityObj.ClerikalBusyTime = Convert.ToDouble(row["ClerikalBusyTime"].ToString() == "" ? "0" : row["ClerikalBusyTime"].ToString());
                    ReportEntityObj.TempAbsTime = Convert.ToDouble(row["TempAbsTime"].ToString() == "" ? "0" : row["TempAbsTime"].ToString());
                    ReportEntityObj.UnHoldTime = Convert.ToDouble(row["UnHoldTime"].ToString() == "" ? "0" : row["UnHoldTime"].ToString());
                    ReportEntityObj.LongestUnHoldTime = Convert.ToDouble(row["LongestUnHoldTime"].ToString() == "" ? "0" : row["LongestUnHoldTime"].ToString());
                    ReportEntityObj.TotalIdleTime = Convert.ToDouble(row["TotalIdleTime"].ToString() == "" ? "0" : row["TotalIdleTime"].ToString());

                    ReportEntityObj.ACDBusyCount = Convert.ToInt32(row["ACDBusyCount"].ToString() == "" ? "0" : row["ACDBusyCount"].ToString());
                    ReportEntityObj.LoggedInCount = Convert.ToInt32(row["LoggedInCount"].ToString() == "" ? "0" : row["LoggedInCount"].ToString());
                    ReportEntityObj.LoggedOutCount = Convert.ToInt32(row["LoggedOutCount"].ToString() == "" ? "0" : row["LoggedOutCount"].ToString());
                    ReportEntityObj.ClerikalBusyCount = Convert.ToInt32(row["ClerikalBusyCount"].ToString() == "" ? "0" : row["ClerikalBusyCount"].ToString());
                    ReportEntityObj.TempAbsCount = Convert.ToInt32(row["TempAbsCount"].ToString() == "" ? "0" : row["TempAbsCount"].ToString());
                    ReportEntityObj.OutBoundCallCount = Convert.ToInt32(row["OutBoundCallCount"].ToString() == "" ? "0" : row["OutBoundCallCount"].ToString());
                    ReportEntityObj.TransferCount = Convert.ToInt32(row["TransferCount"].ToString() == "" ? "0" : row["TransferCount"].ToString());
                    ReportEntityObj.UnHoldCount = Convert.ToInt32(row["UnHoldCount"].ToString() == "" ? "0" : row["UnHoldCount"].ToString());
                    ReportEntityObj.HangUpCount = Convert.ToInt32(row["HangUpCount"].ToString() == "" ? "0" : row["HangUpCount"].ToString());
                    ReportEntityObj.ACDnotacceptCount = Convert.ToInt32(row["ACDnotacceptCount"].ToString() == "" ? "0" : row["ACDnotacceptCount"].ToString());
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public List<ReportAgentOverviewEntity> GetAgentOverviewReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string Agents, int InternalLength, string WeekDays)
        {
            try
            {
                List<ReportAgentOverviewEntity> ReportEntityList = new List<ReportAgentOverviewEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_AgentOverviewReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@Agents", Agents));
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                cmd.CommandTimeout = 0;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportAgentOverviewEntity ReportEntityObj = new ReportAgentOverviewEntity();
                    ReportEntityObj.AgentName = row["AgentName"].ToString();
                    ReportEntityObj.GroupTitle = row["GroupTitle"].ToString();

                    DateTime dt1 = DateTime.ParseExact(row["EventDate"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.EventDate = datedisplay;
                    //ReportEntityObj.EventDate = Convert.ToDateTime(row["EventDate"].ToString()).ToShortDateString();
                    //ReportEntityObj.LogInTime = Convert.ToDateTime(row["LogInTime"].ToString());
                    ReportEntityObj.LogInTime = row["LogInTime"].ToString() == "" ? TimeSpan.MaxValue : Convert.ToDateTime(row["LogInTime"].ToString()).TimeOfDay;

                    ReportEntityObj.ACDBusyCount = Convert.ToInt32(row["ACDBusyCount"].ToString() == "" ? "0" : row["ACDBusyCount"].ToString());
                    ReportEntityObj.ACDBusyTime = Convert.ToDouble(row["ACDBusyTime"].ToString() == "" ? "0" : row["ACDBusyTime"].ToString());
                    ReportEntityObj.ACDnotacceptCount = Convert.ToInt32(row["ACDnotacceptCount"].ToString() == "" ? "0" : row["ACDnotacceptCount"].ToString());
                    ReportEntityObj.ACDRingTime = Convert.ToDouble(row["ACDRingTime"].ToString() == "" ? "0" : row["ACDRingTime"].ToString());
                    ReportEntityObj.ACWCount = Convert.ToInt32(row["ACWCount"].ToString() == "" ? "0" : row["ACWCount"].ToString());
                    ReportEntityObj.ACWTime = Convert.ToDouble(row["ACWTime"].ToString() == "" ? "0" : row["ACWTime"].ToString());
                    
                    ReportEntityObj.AUX1Count= Convert.ToInt32(row["AUX1Count"].ToString() == "" ? "0" : row["AUX1Count"].ToString());
                    ReportEntityObj.AUX1Time = Convert.ToDouble(row["AUX1Time"].ToString() == "" ? "0" : row["AUX1Time"].ToString());
                    ReportEntityObj.AUX2Count = Convert.ToInt32(row["AUX2Count"].ToString() == "" ? "0" : row["AUX2Count"].ToString());
                    ReportEntityObj.AUX2Time = Convert.ToDouble(row["AUX2Time"].ToString() == "" ? "0" : row["AUX2Time"].ToString());
                    ReportEntityObj.AUX3Count = Convert.ToInt32(row["AUX3Count"].ToString() == "" ? "0" : row["AUX3Count"].ToString());
                    ReportEntityObj.AUX3Time = Convert.ToDouble(row["AUX3Time"].ToString() == "" ? "0" : row["AUX3Time"].ToString());
                    ReportEntityObj.AUX4Count = Convert.ToInt32(row["AUX4Count"].ToString() == "" ? "0" : row["AUX4Count"].ToString());
                    ReportEntityObj.AUX4Time = Convert.ToDouble(row["AUX4Time"].ToString() == "" ? "0" : row["AUX4Time"].ToString());
                    ReportEntityObj.ClerikalBusyCount = Convert.ToInt32(row["ClerikalBusyCount"].ToString() == "" ? "0" : row["ClerikalBusyCount"].ToString());
                    ReportEntityObj.ClerikalBusyTime = Convert.ToDouble(row["ClerikalBusyTime"].ToString() == "" ? "0" : row["ClerikalBusyTime"].ToString());
                    ReportEntityObj.ExternalExtOutCalls = Convert.ToInt32(row["ExternalExtOutCalls"].ToString() == "" ? "0" : row["ExternalExtOutCalls"].ToString());
                    ReportEntityObj.ExternalExtOutCallsHoldTime = Convert.ToDouble(row["ExternalExtOutCallsHoldTime"].ToString() == "" ? "0" : row["ExternalExtOutCallsHoldTime"].ToString());
                    ReportEntityObj.ExternalExtOutCallsTime = Convert.ToDouble(row["ExternalExtOutCallsTime"].ToString() == "" ? "0" : row["ExternalExtOutCallsTime"].ToString());
                    ReportEntityObj.ExtInCalls = Convert.ToInt32(row["ExtInCalls"].ToString() == "" ? "0" : row["ExtInCalls"].ToString());
                    ReportEntityObj.ExtInCallsHoldTime = Convert.ToDouble(row["ExtInCallsHoldTime"].ToString() == "" ? "0" : row["ExtInCallsHoldTime"].ToString());
                    ReportEntityObj.ExtInCallsTime = Convert.ToDouble(row["ExtInCallsTime"].ToString() == "" ? "0" : row["ExtInCallsTime"].ToString());
                    ReportEntityObj.ExtOutCalls = Convert.ToInt32(row["ExtOutCalls"].ToString() == "" ? "0" : row["ExtOutCalls"].ToString());
                    ReportEntityObj.ExtOutCallsHoldTime = Convert.ToDouble(row["ExtOutCallsHoldTime"].ToString() == "" ? "0" : row["ExtOutCallsHoldTime"].ToString());
                    ReportEntityObj.ExtOutCallsTime = Convert.ToDouble(row["ExtOutCallsTime"].ToString() == "" ? "0" : row["ExtOutCallsTime"].ToString());
                    ReportEntityObj.TempAbsCount = Convert.ToInt32(row["TempAbsCount"].ToString() == "" ? "0" : row["TempAbsCount"].ToString());
                    ReportEntityObj.TempAbsTime = Convert.ToDouble(row["TempAbsTime"].ToString() == "" ? "0" : row["TempAbsTime"].ToString());
                    ReportEntityObj.TotalIdleTime = Convert.ToDouble(row["TotalIdleTime"].ToString() == "" ? "0" : row["TotalIdleTime"].ToString());
                    ReportEntityObj.TotalLoggedInTime = Convert.ToDouble(row["TotalLoggedInTime"].ToString() == "" ? "0" : row["TotalLoggedInTime"].ToString());
                    ReportEntityObj.UnHoldTime = Convert.ToDouble(row["UnHoldTime"].ToString() == "" ? "0" : row["UnHoldTime"].ToString());

                  
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportAgentOverviewEntity> GetAgentOverviewSummaryReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string Agents, int InternalLength, string WeekDays)
        {
            try
            {
                List<ReportAgentOverviewEntity> ReportEntityList = new List<ReportAgentOverviewEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_AgentOverviewSummaryReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@Agents", Agents));
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                cmd.CommandTimeout = 0;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportAgentOverviewEntity ReportEntityObj = new ReportAgentOverviewEntity();
                    ReportEntityObj.AgentName = row["AgentName"].ToString();
                  //  ReportEntityObj.GroupTitle = row["GroupTitle"].ToString();

                    DateTime dt1 = DateTime.ParseExact(row["EventDate"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.EventDate = datedisplay;
                    //ReportEntityObj.EventDate = Convert.ToDateTime(row["EventDate"].ToString()).ToShortDateString();
                    //ReportEntityObj.LogInTime = Convert.ToDateTime(row["LogInTime"].ToString());
                    ReportEntityObj.LogInTime = row["LogInTime"].ToString() == "" ? TimeSpan.MaxValue : Convert.ToDateTime(row["LogInTime"].ToString()).TimeOfDay;

                    ReportEntityObj.ACDBusyCount = Convert.ToInt32(row["ACDBusyCount"].ToString() == "" ? "0" : row["ACDBusyCount"].ToString());
                    ReportEntityObj.ACDBusyTime = Convert.ToDouble(row["ACDBusyTime"].ToString() == "" ? "0" : row["ACDBusyTime"].ToString());
                    ReportEntityObj.ACDnotacceptCount = Convert.ToInt32(row["ACDnotacceptCount"].ToString() == "" ? "0" : row["ACDnotacceptCount"].ToString());
                    ReportEntityObj.ACDRingTime = Convert.ToDouble(row["ACDRingTime"].ToString() == "" ? "0" : row["ACDRingTime"].ToString());
                    ReportEntityObj.ACWCount = Convert.ToInt32(row["ACWCount"].ToString() == "" ? "0" : row["ACWCount"].ToString());
                    ReportEntityObj.ACWTime = Convert.ToDouble(row["ACWTime"].ToString() == "" ? "0" : row["ACWTime"].ToString());

                    ReportEntityObj.AUX1Count = Convert.ToInt32(row["AUX1Count"].ToString() == "" ? "0" : row["AUX1Count"].ToString());
                    ReportEntityObj.AUX1Time = Convert.ToDouble(row["AUX1Time"].ToString() == "" ? "0" : row["AUX1Time"].ToString());
                    ReportEntityObj.AUX2Count = Convert.ToInt32(row["AUX2Count"].ToString() == "" ? "0" : row["AUX2Count"].ToString());
                    ReportEntityObj.AUX2Time = Convert.ToDouble(row["AUX2Time"].ToString() == "" ? "0" : row["AUX2Time"].ToString());
                    ReportEntityObj.AUX3Count = Convert.ToInt32(row["AUX3Count"].ToString() == "" ? "0" : row["AUX3Count"].ToString());
                    ReportEntityObj.AUX3Time = Convert.ToDouble(row["AUX3Time"].ToString() == "" ? "0" : row["AUX3Time"].ToString());
                    ReportEntityObj.AUX4Count = Convert.ToInt32(row["AUX4Count"].ToString() == "" ? "0" : row["AUX4Count"].ToString());
                    ReportEntityObj.AUX4Time = Convert.ToDouble(row["AUX4Time"].ToString() == "" ? "0" : row["AUX4Time"].ToString());
                    ReportEntityObj.ClerikalBusyCount = Convert.ToInt32(row["ClerikalBusyCount"].ToString() == "" ? "0" : row["ClerikalBusyCount"].ToString());
                    ReportEntityObj.ClerikalBusyTime = Convert.ToDouble(row["ClerikalBusyTime"].ToString() == "" ? "0" : row["ClerikalBusyTime"].ToString());
                    ReportEntityObj.ExternalExtOutCalls = Convert.ToInt32(row["ExternalExtOutCalls"].ToString() == "" ? "0" : row["ExternalExtOutCalls"].ToString());
                    ReportEntityObj.ExternalExtOutCallsHoldTime = Convert.ToDouble(row["ExternalExtOutCallsHoldTime"].ToString() == "" ? "0" : row["ExternalExtOutCallsHoldTime"].ToString());
                    ReportEntityObj.ExternalExtOutCallsTime = Convert.ToDouble(row["ExternalExtOutCallsTime"].ToString() == "" ? "0" : row["ExternalExtOutCallsTime"].ToString());
                    ReportEntityObj.ExtInCalls = Convert.ToInt32(row["ExtInCalls"].ToString() == "" ? "0" : row["ExtInCalls"].ToString());
                    ReportEntityObj.ExtInCallsHoldTime = Convert.ToDouble(row["ExtInCallsHoldTime"].ToString() == "" ? "0" : row["ExtInCallsHoldTime"].ToString());
                    ReportEntityObj.ExtInCallsTime = Convert.ToDouble(row["ExtInCallsTime"].ToString() == "" ? "0" : row["ExtInCallsTime"].ToString());
                    ReportEntityObj.ExtOutCalls = Convert.ToInt32(row["ExtOutCalls"].ToString() == "" ? "0" : row["ExtOutCalls"].ToString());
                    ReportEntityObj.ExtOutCallsHoldTime = Convert.ToDouble(row["ExtOutCallsHoldTime"].ToString() == "" ? "0" : row["ExtOutCallsHoldTime"].ToString());
                    ReportEntityObj.ExtOutCallsTime = Convert.ToDouble(row["ExtOutCallsTime"].ToString() == "" ? "0" : row["ExtOutCallsTime"].ToString());
                    ReportEntityObj.TempAbsCount = Convert.ToInt32(row["TempAbsCount"].ToString() == "" ? "0" : row["TempAbsCount"].ToString());
                    ReportEntityObj.TempAbsTime = Convert.ToDouble(row["TempAbsTime"].ToString() == "" ? "0" : row["TempAbsTime"].ToString());
                    ReportEntityObj.TotalIdleTime = Convert.ToDouble(row["TotalIdleTime"].ToString() == "" ? "0" : row["TotalIdleTime"].ToString());
                    ReportEntityObj.TotalLoggedInTime = Convert.ToDouble(row["TotalLoggedInTime"].ToString() == "" ? "0" : row["TotalLoggedInTime"].ToString());
                    ReportEntityObj.UnHoldTime = Convert.ToDouble(row["UnHoldTime"].ToString() == "" ? "0" : row["UnHoldTime"].ToString());


                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public List<ReportAgentOverviewEntity> GetAgentConsolidatedSummaryReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string Agents, int InternalLength, string WeekDays)
        {
            try
            {
                List<ReportAgentOverviewEntity> ReportEntityList = new List<ReportAgentOverviewEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_AgentConsolidatedSummaryReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@Agents", Agents));
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                cmd.CommandTimeout = 0;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportAgentOverviewEntity ReportEntityObj = new ReportAgentOverviewEntity();
                    ReportEntityObj.AgentName = row["AgentName"].ToString();
                    //  ReportEntityObj.GroupTitle = row["GroupTitle"].ToString();

                    DateTime dt1 = DateTime.ParseExact(row["EventDate"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.EventDate = datedisplay;
                    //ReportEntityObj.EventDate = Convert.ToDateTime(row["EventDate"].ToString()).ToShortDateString();
                    //ReportEntityObj.LogInTime = Convert.ToDateTime(row["LogInTime"].ToString());
                    ReportEntityObj.LogInTime = row["LogInTime"].ToString() == "" ? TimeSpan.MaxValue : Convert.ToDateTime(row["LogInTime"].ToString()).TimeOfDay;

                    ReportEntityObj.ACDBusyCount = Convert.ToInt32(row["ACDBusyCount"].ToString() == "" ? "0" : row["ACDBusyCount"].ToString());
                    ReportEntityObj.ACDBusyTime = Convert.ToDouble(row["ACDBusyTime"].ToString() == "" ? "0" : row["ACDBusyTime"].ToString());
                    ReportEntityObj.ACDnotacceptCount = Convert.ToInt32(row["ACDnotacceptCount"].ToString() == "" ? "0" : row["ACDnotacceptCount"].ToString());
                    ReportEntityObj.ACDRingTime = Convert.ToDouble(row["ACDRingTime"].ToString() == "" ? "0" : row["ACDRingTime"].ToString());
                    ReportEntityObj.ClerikalBusyCount = Convert.ToInt32(row["ClerikalBusyCount"].ToString() == "" ? "0" : row["ClerikalBusyCount"].ToString());
                    ReportEntityObj.ClerikalBusyTime = Convert.ToDouble(row["ClerikalBusyTime"].ToString() == "" ? "0" : row["ClerikalBusyTime"].ToString());

                    ReportEntityObj.TempAbsCount = Convert.ToInt32(row["TempAbsCount"].ToString() == "" ? "0" : row["TempAbsCount"].ToString());
                    ReportEntityObj.TempAbsTime = Convert.ToDouble(row["TempAbsTime"].ToString() == "" ? "0" : row["TempAbsTime"].ToString());

                    //ReportEntityObj.AUX1Count = Convert.ToInt32(row["AUX1Count"].ToString() == "" ? "0" : row["AUX1Count"].ToString());
                    //ReportEntityObj.AUX1Time = Convert.ToDouble(row["AUX1Time"].ToString() == "" ? "0" : row["AUX1Time"].ToString());
                    //ReportEntityObj.AUX2Count = Convert.ToInt32(row["AUX2Count"].ToString() == "" ? "0" : row["AUX2Count"].ToString());
                    //ReportEntityObj.AUX2Time = Convert.ToDouble(row["AUX2Time"].ToString() == "" ? "0" : row["AUX2Time"].ToString());
                    //ReportEntityObj.AUX3Count = Convert.ToInt32(row["AUX3Count"].ToString() == "" ? "0" : row["AUX3Count"].ToString());
                    //ReportEntityObj.AUX3Time = Convert.ToDouble(row["AUX3Time"].ToString() == "" ? "0" : row["AUX3Time"].ToString());
                    //ReportEntityObj.AUX4Count = Convert.ToInt32(row["AUX4Count"].ToString() == "" ? "0" : row["AUX4Count"].ToString());
                    //ReportEntityObj.AUX4Time = Convert.ToDouble(row["AUX4Time"].ToString() == "" ? "0" : row["AUX4Time"].ToString());
                    ReportEntityObj.ExternalExtOutCalls = Convert.ToInt32(row["ExternalExtOutCalls"].ToString() == "" ? "0" : row["ExternalExtOutCalls"].ToString());
                    ReportEntityObj.ExternalExtOutCallsHoldTime = Convert.ToDouble(row["ExternalExtOutCallsHoldTime"].ToString() == "" ? "0" : row["ExternalExtOutCallsHoldTime"].ToString());
                    ReportEntityObj.ExternalExtOutCallsTime = Convert.ToDouble(row["ExternalExtOutCallsTime"].ToString() == "" ? "0" : row["ExternalExtOutCallsTime"].ToString());
                    ReportEntityObj.ExtInCalls = Convert.ToInt32(row["ExtInCalls"].ToString() == "" ? "0" : row["ExtInCalls"].ToString());
                    ReportEntityObj.ExtInCallsHoldTime = Convert.ToDouble(row["ExtInCallsHoldTime"].ToString() == "" ? "0" : row["ExtInCallsHoldTime"].ToString());
                    ReportEntityObj.ExtInCallsTime = Convert.ToDouble(row["ExtInCallsTime"].ToString() == "" ? "0" : row["ExtInCallsTime"].ToString());
                    ReportEntityObj.ExtOutCalls = Convert.ToInt32(row["ExtOutCalls"].ToString() == "" ? "0" : row["ExtOutCalls"].ToString());
                    ReportEntityObj.ExtOutCallsHoldTime = Convert.ToDouble(row["ExtOutCallsHoldTime"].ToString() == "" ? "0" : row["ExtOutCallsHoldTime"].ToString());
                    ReportEntityObj.ExtOutCallsTime = Convert.ToDouble(row["ExtOutCallsTime"].ToString() == "" ? "0" : row["ExtOutCallsTime"].ToString());
                    ReportEntityObj.TotalIdleTime = Convert.ToDouble(row["TotalIdleTime"].ToString() == "" ? "0" : row["TotalIdleTime"].ToString());
                    ReportEntityObj.TotalLoggedInTime = Convert.ToDouble(row["TotalLoggedInTime"].ToString() == "" ? "0" : row["TotalLoggedInTime"].ToString());
                    ReportEntityObj.UnHoldTime = Convert.ToDouble(row["UnHoldTime"].ToString() == "" ? "0" : row["UnHoldTime"].ToString());


                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public List<ReportAgentUnavailbeEntity> GetAgentUnavailableReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Agents, string WeekDays)
        {
            try
            {
                List<ReportAgentUnavailbeEntity> ReportEntityList = new List<ReportAgentUnavailbeEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_AgentUnavailableReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
               // cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@Agents", Agents));
               // cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                cmd.CommandTimeout = 0;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportAgentUnavailbeEntity ReportEntityObj = new ReportAgentUnavailbeEntity();
                    ReportEntityObj.AgentName = row["AgentName"].ToString();
                    ReportEntityObj.Reason = row["Reason"].ToString();
                    //DateTime dt1 = DateTime.ParseExact(row["EventDate"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    //CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    //info.DateTimeFormat.DateSeparator = "-";
                    //info.DateTimeFormat.TimeSeparator = ":";
                    //string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                   // ReportEntityObj.EventDate = datedisplay;                  
                    ReportEntityObj.ReasonCount = Convert.ToInt32(row["ReasonCount"].ToString() == "" ? "0" : row["ReasonCount"].ToString());
                    ReportEntityObj.ReasonTime = Convert.ToDouble(row["ReasonTime"].ToString() == "" ? "0" : row["ReasonTime"].ToString());
                    
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public List<ReportSLAPerformance> GetSLAPerformanceReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups,  int WorkingHours, string WeekDays)
        {
            try
            {
                List<ReportSLAPerformance> ReportEntityList = new List<ReportSLAPerformance>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_SLAReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WorkingHours", WorkingHours));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                cmd.CommandTimeout = 0;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportSLAPerformance ReportEntityObj = new ReportSLAPerformance();
                    ReportEntityObj.BoardId = Convert.ToInt32(row["BoardId"].ToString());
                    DateTime dt1 = DateTime.ParseExact(row["StartDate"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.StartDate = datedisplay;
                    // ReportEntityObj.StartDate = Convert.ToDateTime(row["StartDate"].ToString()).ToShortDateString();
                    ReportEntityObj.Title = row["Title"].ToString();
                    ReportEntityObj.TotalCalls = row["TotalCalls"].ToString();
                    ReportEntityObj.TotalAnswered = row["TotalAnswered"].ToString();
                    ReportEntityObj.SLAWithIn = row["SLAWithIn"].ToString();
                    ReportEntityObj.SLAOutside = row["SLAOutside"].ToString();
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportGroupConsolidatedEntity> GetGroupConsolidatedReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups,  int WorkingHours, string WeekDays)
        {
            try
            {
                List<ReportGroupConsolidatedEntity> ReportEntityList = new List<ReportGroupConsolidatedEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_GroupConsolidatedReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WorkingHours", WorkingHours));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                cmd.CommandTimeout = 0;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportGroupConsolidatedEntity ReportEntityObj = new ReportGroupConsolidatedEntity();

                    DateTime dt1 = DateTime.ParseExact(row["StartDate"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.StartDate = datedisplay;
                    //ReportEntityObj.StartDate = Convert.ToDateTime(row["StartDate"].ToString()).ToShortDateString();
                    ReportEntityObj.Title = row["Title"].ToString();
                    ReportEntityObj.TotalCalls = row["TotalCalls"].ToString();
                    ReportEntityObj.TotalAnswered = row["TotalAnswered"].ToString();
                    ReportEntityObj.TotalAbanodoned = row["TotalAbanodoned"].ToString();
                    ReportEntityObj.LoggedInCount = row["LoggedInCount"].ToString();
                    ReportEntityObj.AbandonedPer = row["AbandonedPer"].ToString();
                    ReportEntityObj.SLAPer = row["SLAPer"].ToString();

                    ReportEntityObj.AvgWaitTime = row["AvgWaitTime"].ToString();
                    ReportEntityObj.LongestWaitAnswer = row["LongestWaitAnswer"].ToString();
                    ReportEntityObj.AvgWaitAbandonedTime = row["AvgWaitAbandonedTime"].ToString();
                    ReportEntityObj.LongestWaitAbandoned = row["LongestWaitAbandoned"].ToString();

                    ReportEntityObj.AvgCallsHour = row["AvgCallsHour"].ToString();
                    ReportEntityObj.AvgAvailableTime = row["AvgAvailableTime"].ToString();
                    ReportEntityObj.AvgHold = row["AvgHold"].ToString();
                    ReportEntityObj.TransferACD = row["TransferACD"].ToString();
                    ReportEntityObj.TotalInternalCall = row["TotalInternalCall"].ToString();
                    ReportEntityObj.TotalInternalTime = row["TotalInternalTime"].ToString();
                    ReportEntityObj.AVGInternalTime = row["AVGInternalTime"].ToString();
                    ReportEntityObj.TotalTransfersIn = row["TotalTransfersIn"].ToString();
                  

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public List<ReportQueueAnalysisEntity> GetQueueAnalysisReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, int WorkingHours, string WeekDays)
        {
            try
            {
                List<ReportQueueAnalysisEntity> ReportEntityList = new List<ReportQueueAnalysisEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_QueueAnalysisReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WorkingHours", WorkingHours));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                cmd.CommandTimeout = 0;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportQueueAnalysisEntity ReportEntityObj = new ReportQueueAnalysisEntity();
               
                    ReportEntityObj.Title = row["Title"].ToString();
                    ReportEntityObj.TotalAnswered = row["TotalAnswered"].ToString();
                    ReportEntityObj.TotalAbanodoned = row["TotalAbanodoned"].ToString();
                  
                    ReportEntityObj.TotalCalls = row["TotalCalls"].ToString();
        
                    ReportEntityObj.Totaloverflowedcall = row["Totaloverflowedcall"].ToString();

                    ReportEntityObj.TotaloverflowedcallInQueues = row["TotaloverflowedcallInQueues"].ToString();
                    ReportEntityObj.TotalACDTime = row["TotalACDTime"].ToString();
                    ReportEntityObj.AvgWaitAbandonedTime = row["AvgWaitAbandonedTime"].ToString();
                    ReportEntityObj.AvgWaitTime = row["AvgWaitTime"].ToString();

                    ReportEntityObj.SLAPerl = row["SLAPer"].ToString();
                    ReportEntityObj.LongestWait = row["LongestWait"].ToString();
                    ReportEntityObj.TotalAgentTime = row["TotalAgentTime"].ToString();

                    ReportEntityObj.TotalRingDurationAnswerCalls = row["TotalRingDurationAnswerCalls"].ToString();
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }


        public List<ReportQueueAnalysisEntity> Get_DEKRA_QueueAnalysisReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, int WorkingHours, string WeekDays)
        {
            try
            {
                List<ReportQueueAnalysisEntity> ReportEntityList = new List<ReportQueueAnalysisEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_DEKRA_QueueAnalysisReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WorkingHours", WorkingHours));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                cmd.CommandTimeout = 0;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportQueueAnalysisEntity ReportEntityObj = new ReportQueueAnalysisEntity();

                    ReportEntityObj.Title = row["Title"].ToString();
                    ReportEntityObj.GroupNumber = row["GroupNumber"].ToString();
                    ReportEntityObj.TotalAnswered = row["TotalAnswered"].ToString();
                    ReportEntityObj.TotalAbanodoned = row["TotalAbanodoned"].ToString();
                    ReportEntityObj.TotalAbanodonedAfter = row["TotalAbanodonedAfter"].ToString();
                    ReportEntityObj.TotalCallsNotAccepted = row["TotalCallsNotAccepted"].ToString();
                    ReportEntityObj.AbandonedThreshold = row["AbandonedThreshold"].ToString();

                    ReportEntityObj.TotalCalls = row["TotalCalls"].ToString();

                    ReportEntityObj.Totaloverflowedcall = row["Totaloverflowedcall"].ToString();

                    ReportEntityObj.TotaloverflowedcallInQueues = row["TotaloverflowedcallInQueues"].ToString();
                    ReportEntityObj.TotalACDTime = row["TotalACDTime"].ToString();
                    ReportEntityObj.AvgWaitAbandonedTime = row["AvgWaitAbandonedTime"].ToString();
                    ReportEntityObj.AvgWaitTime = row["AvgWaitTime"].ToString();

                    ReportEntityObj.SLAPerl = row["SLAPer"].ToString();
                    ReportEntityObj.LongestWait = row["LongestWait"].ToString();
                    ReportEntityObj.TotalAgentTime = row["TotalAgentTime"].ToString();

                    ReportEntityObj.TotalRingDurationAnswerCalls = row["TotalRingDurationAnswerCalls"].ToString();
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }




        public List<ReportYBVCallsEntity> GetYBVCallsReport(DateTime FromDate, DateTime ToDate,string timeFrom,string timeTo, string WeekDays)
        {
            try
            {
                List<ReportYBVCallsEntity> ReportEntityList = new List<ReportYBVCallsEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_YBVCallsReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));

                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportYBVCallsEntity ReportEntityObj = new ReportYBVCallsEntity();
                    DateTime dt1 = DateTime.ParseExact(row["DateObj"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.Date = datedisplay;
                    // ReportEntityObj.Date = Convert.ToDateTime(row["DateObj"].ToString()).ToShortDateString(); 
                    ReportEntityObj.TransferCount = Convert.ToInt32(row["Total"].ToString());
                    ReportEntityObj.FirstCount = Convert.ToInt32(row[2].ToString());
                    ReportEntityObj.SecondCount = Convert.ToInt32(row[3].ToString());
                    ReportEntityObj.ThirdCount = Convert.ToInt32(row[4].ToString());

                    ReportEntityObj.FirstCountHeader = Convert.ToString(dt.Columns[2].ColumnName);
                    ReportEntityObj.SecondCountHeader = Convert.ToString(dt.Columns[3].ColumnName);
                    ReportEntityObj.ThirdCountHeader = Convert.ToString(dt.Columns[4].ColumnName);

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportYBVCallsEntity> GetYBVCallsByIntervalReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string WeekDays,int TimeInterval)
        {
            try
            {
                List<ReportYBVCallsEntity> ReportEntityList = new List<ReportYBVCallsEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_YBVCallsReportByIntervals", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@TimeInterval", TimeInterval));

                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportYBVCallsEntity ReportEntityObj = new ReportYBVCallsEntity();
                    DateTime dt1 = DateTime.ParseExact(row["DateObj"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.Date = datedisplay;
                    // ReportEntityObj.Date = Convert.ToDateTime(row["DateObj"].ToString()).ToShortDateString();
                    ReportEntityObj.TransferCount = Convert.ToInt32(row["Total"].ToString());
                    ReportEntityObj.TimeKey= row["timekey"].ToString();

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportYBVCutOffCallsEntity> GetYBVCutOffCallsReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string WeekDays)
        {
            try
            {
                List<ReportYBVCutOffCallsEntity> ReportEntityList = new List<ReportYBVCutOffCallsEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_YBVCutOffCallsReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportYBVCutOffCallsEntity ReportEntityObj = new ReportYBVCutOffCallsEntity();
                    //ReportEntityObj.DateTime = DateTime.ParseExact(row["DateTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MM-yyyy HH:mm:ss");

                    DateTime dt1 = DateTime.ParseExact(row["StartTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info);
                    ReportEntityObj.StartTime = datedisplay;
                    //ReportEntityObj.StartTime = DateTime.ParseExact(row["StartTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MM-yyyy HH:mm:ss");

                    dt1 = DateTime.ParseExact(row["EndTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);                  
                    datedisplay = dt1.ToString(info);
                    ReportEntityObj.EndTime = datedisplay;
                    //ReportEntityObj.EndTime = DateTime.ParseExact(row["EndTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MM-yyyy HH:mm:ss");
                    ReportEntityObj.Duration = Convert.ToInt32(row["Duration"]);
                    ReportEntityObj.Title = row["Title"].ToString();
                    ReportEntityObj.CLI = row["CLI"].ToString();
                    ReportEntityObj.DDI = row["DDI"].ToString();
                    ReportEntityObj.GroupNumber = Convert.ToInt32(row["GroupNumber"]);

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;
            }
        }
        public List<QueueviewReport> GetQueueOverVReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string WeekDays, int WorkingHours,  string Groups)
        {
            try
            {
                List<QueueviewReport> ReportEntityList = new List<QueueviewReport>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_QueueOverviewReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@WorkingHours", WorkingHours));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    QueueviewReport ReportEntityObj = new QueueviewReport();

                    ReportEntityObj.BoardID = Convert.ToInt32(row["BoardID"]);
                    ReportEntityObj.Title = row["Title"].ToString();
                    ReportEntityObj.AvgMissedDuration = Convert.ToDouble(row["AvgMissedDuration"].ToString());
                    ReportEntityObj.TotalAnsweredCalls = Convert.ToInt32(row["TotalAnsweredCalls"]);
                    ReportEntityObj.TotalMissedCalls = Convert.ToInt32(row["TotalMissedCalls"]);
                    ReportEntityObj.AvgAnswer = Convert.ToInt32(row["AvgAnswer"]);
                    ReportEntityObj.AvgResponseTime = Convert.ToInt32(row["AvgResponseTime"]);
                    ReportEntityObj.TotalAnswerDuration = Convert.ToInt32(row["TotalAnswerDuration"].ToString());
                    ReportEntityObj.totalcalls = ReportEntityObj.TotalMissedCalls + ReportEntityObj.TotalAnsweredCalls;
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList.OrderBy(w => w.Title).ToList();
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public List<ReportYBVCallsEntity> GetAHTCallsReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string WeekDays)
        {
            try
            {
                List<ReportYBVCallsEntity> ReportEntityList = new List<ReportYBVCallsEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_YBVCallsReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));

                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportYBVCallsEntity ReportEntityObj = new ReportYBVCallsEntity();
                    DateTime dt1 = DateTime.ParseExact(row["DateObj"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.Date = datedisplay;
                    //ReportEntityObj.Date = Convert.ToDateTime(row["DateObj"].ToString()).ToShortDateString();
                    ReportEntityObj.TransferCount = Convert.ToInt32(row["Total"].ToString());
                    ReportEntityObj.FirstCount = Convert.ToInt32(row["FirstCount"].ToString());
                    ReportEntityObj.SecondCount = Convert.ToInt32(row["SecondCount"].ToString());
                    ReportEntityObj.ThirdCount = Convert.ToInt32(row["ThirdCount"].ToString());

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportYBVCallsEntity> GetAHTCallsByIntervalReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string WeekDays, int TimeInterval)
        {
            try
            {
                List<ReportYBVCallsEntity> ReportEntityList = new List<ReportYBVCallsEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_YBVCallsReportByIntervals", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@TimeInterval", TimeInterval));

                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportYBVCallsEntity ReportEntityObj = new ReportYBVCallsEntity();
                    DateTime dt1 = DateTime.ParseExact(row["DateObj"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.Date = datedisplay;
                    // ReportEntityObj.Date = Convert.ToDateTime(row["DateObj"].ToString()).ToShortDateString();
                    ReportEntityObj.TransferCount = Convert.ToInt32(row["Total"].ToString());
                    ReportEntityObj.TimeKey = row["timekey"].ToString();

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportCallsbyPrimaryReasonEntity> GetCallsbyPrimaryReasonReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string WeekDays)
        {
            try
            {
                List<ReportCallsbyPrimaryReasonEntity> ReportEntityList = new List<ReportCallsbyPrimaryReasonEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_CallsbyPrimaryReason", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCallsbyPrimaryReasonEntity ReportEntityObj = new ReportCallsbyPrimaryReasonEntity();
                    ReportEntityObj.PrimaryOutcome = row["PrimaryOutcome"].ToString();
                    ReportEntityObj.Board = row["Board"].ToString();                    
                    if (!string.IsNullOrEmpty(row["PrimaryCount"].ToString()))
                    {
                        ReportEntityObj.PrimaryCount = Convert.ToInt32(row["PrimaryCount"].ToString());
                    }
                    ReportEntityObj.SecondaryOutcome = row["SecondaryOutcome"].ToString();
                    if (!string.IsNullOrEmpty(row["SecondaryCount"].ToString()))
                    {
                        ReportEntityObj.SecondaryCount = Convert.ToInt32(row["SecondaryCount"].ToString());
                    }
                    ReportEntityObj.AgentName = row["AgentName"].ToString();
                    if (string.IsNullOrEmpty(ReportEntityObj.AgentName))
                        ReportEntityObj.AgentName = "N/A";
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;
            }
        }
        public List<ReportCallBreakdownbyPrimaryReasonEntity> GetCallBreakdownbyPrimaryReasonReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string WeekDays)
        {
            try
            {
                List<ReportCallBreakdownbyPrimaryReasonEntity> ReportEntityList = new List<ReportCallBreakdownbyPrimaryReasonEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_CallBreakdownbyPrimaryReason", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCallBreakdownbyPrimaryReasonEntity ReportEntityObj = new ReportCallBreakdownbyPrimaryReasonEntity();
                    ReportEntityObj.Board = row["Board"].ToString();
                    ReportEntityObj.PrimaryOutcome = row["PrimaryOutcome"].ToString();
                    ReportEntityObj.SecondaryOutcome = row["SecondaryOutcome"].ToString();
                    ReportEntityObj.Extension = row["Extension"].ToString();

                    DateTime dt1 = DateTime.ParseExact(row["StartTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info);
                    ReportEntityObj.StartTime = datedisplay;
                    // ReportEntityObj.StartTime = DateTime.Parse(row["StartTime"].ToString());

                    dt1 = DateTime.ParseExact(row["EndTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);                  
                     datedisplay = dt1.ToString(info);
                    ReportEntityObj.EndTime = datedisplay;
                   // ReportEntityObj.EndTime = DateTime.Parse(row["EndTime"].ToString());
                    ReportEntityObj.CLI = row["CLI"].ToString();
                    ReportEntityObj.DDI = row["DDI"].ToString();
                    ReportEntityObj.Name = row["Name"].ToString();
                    ReportEntityObj.Duration = Convert.ToDouble(row["Duration"]);

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;
            }
        }

        public List<ReportCallBreakdownbyPrimaryReasonEntity_DEKRA> Get_DEKRA_CallBreakdownbyPrimaryReasonReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string Agents, string WeekDays)
        {
            try
            {
                List<ReportCallBreakdownbyPrimaryReasonEntity_DEKRA> ReportEntityList = new List<ReportCallBreakdownbyPrimaryReasonEntity_DEKRA>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_DEKRA_CallsbyPrimaryReason", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@Agents", Agents));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCallBreakdownbyPrimaryReasonEntity_DEKRA ReportEntityObj = new ReportCallBreakdownbyPrimaryReasonEntity_DEKRA();
                    ReportEntityObj.AgentExtension = row["AgentExtension"].ToString();
                    ReportEntityObj.AgentName = row["AgentName"].ToString();
                    ReportEntityObj.PrimaryOutcome = row["PrimaryOutcome"].ToString();
                    ReportEntityObj.SecondaryOutcome = row["SecondaryOutcome"].ToString();
                    ReportEntityObj.PrimaryCallDuration = DBNull.Value == row["PrimaryCallDuration"] ? 0 : Convert.ToInt32(row["PrimaryCallDuration"]);
                    ReportEntityObj.SecondaryCallDuration = DBNull.Value == row["SecondaryCallDuration"] ? 0 : Convert.ToInt32(row["SecondaryCallDuration"]);
                    ReportEntityObj.PrimaryCount = DBNull.Value == row["PrimaryCount"] ? 0 : Convert.ToInt32(row["PrimaryCount"]);
                    ReportEntityObj.SecondaryCount = DBNull.Value == row["SecondaryCount"] ? 0 : Convert.ToInt32(row["SecondaryCount"]);
                   
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;
            }
        }

        public List<ReportExternalRoutingCallsEntity> GetExternalRoutingCallsReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string WeekDays, string ExternalRouting)
        {
            try
            {
                List<ReportExternalRoutingCallsEntity> ReportEntityList = new List<ReportExternalRoutingCallsEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_ExternalRoutingCallsReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@ExternalRouting", ExternalRouting));

                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportExternalRoutingCallsEntity ReportEntityObj = new ReportExternalRoutingCallsEntity();
                    DateTime dt1 = DateTime.ParseExact(row["DateObj"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.Date = datedisplay;
                    //ReportEntityObj.Date = Convert.ToDateTime(row["DateObj"].ToString()).ToShortDateString();
                    ReportEntityObj.DeviceID = row["DeviceID"].ToString();
                    ReportEntityObj.TransferCount = Convert.ToInt32(row["Total"].ToString());
                    ReportEntityObj.FirstCount = Convert.ToInt32(row[3].ToString());
                    ReportEntityObj.SecondCount = Convert.ToInt32(row[4].ToString());
                    ReportEntityObj.ThirdCount = Convert.ToInt32(row[5].ToString());

                    ReportEntityObj.FirstCountHeader = Convert.ToString(dt.Columns[3].ColumnName);
                    ReportEntityObj.SecondCountHeader = Convert.ToString(dt.Columns[4].ColumnName);
                    ReportEntityObj.ThirdCountHeader = Convert.ToString(dt.Columns[5].ColumnName);

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportExternalRoutingCallsEntity> GetExternalRoutingCallsByIntervalReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string WeekDays, int TimeInterval, string ExternalRouting)
        {
            try
            {
                List<ReportExternalRoutingCallsEntity> ReportEntityList = new List<ReportExternalRoutingCallsEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_ExternalRoutingCallsReportByIntervals", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@TimeInterval", TimeInterval));
                cmd.Parameters.Add(new MySqlParameter("@ExternalRouting", ExternalRouting));

                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportExternalRoutingCallsEntity ReportEntityObj = new ReportExternalRoutingCallsEntity();
                    DateTime dt1 = DateTime.ParseExact(row["DateObj"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.Date = datedisplay;
                    // ReportEntityObj.Date = Convert.ToDateTime(row["DateObj"].ToString()).ToShortDateString();
                    ReportEntityObj.DeviceID = row["DeviceID"].ToString();
                    ReportEntityObj.TransferCount = Convert.ToInt32(row["Total"].ToString());
                    ReportEntityObj.TimeKey = row["timekey"].ToString();

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportCostSummaryByTypeEntity> GetCostSummaryByRegionReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {
                List<ReportCostSummaryByTypeEntity> ReportEntityList = new List<ReportCostSummaryByTypeEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_CostSummaryByRegion", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCostSummaryByTypeEntity ReportEntityObj = new ReportCostSummaryByTypeEntity();
                    ReportEntityObj.CostType = row["Region"].ToString();
                    ReportEntityObj.TotalCalls = Convert.ToInt32(row["TotalCalls"].ToString() == "" ? "0" : row["TotalCalls"].ToString());
                    ReportEntityObj.Duration = Convert.ToDouble(row["Duration"].ToString() == "" ? "0" : row["Duration"].ToString());
                    ReportEntityObj.Cost = Math.Round(Convert.ToDouble(row["Cost"].ToString() == "" ? "0" : row["Cost"].ToString()),2);
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportCostSummaryByExtensionEntity> GetCostSummaryByExtensionReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {


                List<ReportCostSummaryByExtensionEntity> ReportEntityList = new List<ReportCostSummaryByExtensionEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_CostSummaryByExtension", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCostSummaryByExtensionEntity ReportEntityObj = new ReportCostSummaryByExtensionEntity();
                    ReportEntityObj.Extension = row["Extension"].ToString();
                    ReportEntityObj.Name = row["Name"].ToString();
                    ReportEntityObj.TotalCalls = Convert.ToInt32(row["TotalCalls"].ToString() == "" ? "0" : row["TotalCalls"].ToString());
                    ReportEntityObj.Duration = Convert.ToDouble(row["Duration"].ToString() == "" ? "0" : row["Duration"].ToString());
                    ReportEntityObj.Cost = Math.Round(Convert.ToDouble(row["Cost"].ToString() == "" ? "0" : row["Cost"].ToString()), 2);
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportCostSummaryByPhoneEntity> GetCostSummaryByPhoneReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {


                List<ReportCostSummaryByPhoneEntity> ReportEntityList = new List<ReportCostSummaryByPhoneEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_CostSummaryByPhone", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCostSummaryByPhoneEntity ReportEntityObj = new ReportCostSummaryByPhoneEntity();
                    ReportEntityObj.DialledNumber = row["DialledNumber"].ToString();
                    ReportEntityObj.TotalCalls = Convert.ToInt32(row["TotalCalls"].ToString() == "" ? "0" : row["TotalCalls"].ToString());
                    ReportEntityObj.Duration = Convert.ToDouble(row["Duration"].ToString() == "" ? "0" : row["Duration"].ToString());
                    ReportEntityObj.Cost = Math.Round(Convert.ToDouble(row["Cost"].ToString() == "" ? "0" : row["Cost"].ToString()), 2);
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportCostSummaryByHourEntity> GetCostSummaryByHourReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {
                List<ReportCostSummaryByHourEntity> ReportEntityList = new List<ReportCostSummaryByHourEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_CostSummaryByHour", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCostSummaryByHourEntity ReportEntityObj = new ReportCostSummaryByHourEntity();
                    ReportEntityObj.Hour = row["Hour"].ToString();
                    ReportEntityObj.TotalCalls = Convert.ToInt32(row["TotalCalls"].ToString() == "" ? "0" : row["TotalCalls"].ToString());
                    ReportEntityObj.Duration = Convert.ToDouble(row["Duration"].ToString() == "" ? "0" : row["Duration"].ToString());
                    ReportEntityObj.AvgDuration = Convert.ToDouble(row["AvgDuration"].ToString() == "" ? "0" : row["AvgDuration"].ToString());
                    ReportEntityObj.Cost = Math.Round(Convert.ToDouble(row["Cost"].ToString() == "" ? "0" : row["Cost"].ToString()), 2);
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportCostSummaryByDayEntity> GetCostSummaryByDayReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {
                List<ReportCostSummaryByDayEntity> ReportEntityList = new List<ReportCostSummaryByDayEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_CostSummaryByDay", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCostSummaryByDayEntity ReportEntityObj = new ReportCostSummaryByDayEntity();
                    ReportEntityObj.Date = DateTime.Parse(row["Date"].ToString()).ToShortDateString();
                    ReportEntityObj.Day =  DateTime.Parse(row["Date"].ToString()).DayOfWeek.ToString();
                    //ReportEntityObj.Day = row["Day"].ToString();
                    ReportEntityObj.TotalCalls = Convert.ToInt32(row["TotalCalls"].ToString() == "" ? "0" : row["TotalCalls"].ToString());
                    ReportEntityObj.Duration = Convert.ToDouble(row["Duration"].ToString() == "" ? "0" : row["Duration"].ToString());
                    ReportEntityObj.Cost = Math.Round(Convert.ToDouble(row["Cost"].ToString() == "" ? "0" : row["Cost"].ToString()), 2);
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportCostSummaryByExtensionItemisedEntity> GetCostSummaryByExtensionItemisedReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {


                List<ReportCostSummaryByExtensionItemisedEntity> ReportEntityList = new List<ReportCostSummaryByExtensionItemisedEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_CostSummaryByExtensionItemised", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCostSummaryByExtensionItemisedEntity ReportEntityObj = new ReportCostSummaryByExtensionItemisedEntity();
                    ReportEntityObj.Extension = row["Extension"].ToString();
                    if (!string.IsNullOrEmpty(ReportEntityObj.Extension))
                    {
                        ReportEntityObj.Date = DateTime.Parse(row["Date"].ToString()).ToShortDateString();
                        ReportEntityObj.PhoneNumber = row["PhoneNumber"].ToString();
                        ReportEntityObj.Time = row["Time"].ToString();
                        ReportEntityObj.Duration = Convert.ToDouble(row["Duration"].ToString() == "" ? "0" : row["Duration"].ToString());
                        ReportEntityObj.Cost = Math.Round(Convert.ToDouble(row["Cost"].ToString() == "" ? "0" : row["Cost"].ToString()), 2);
                        ReportEntityList.Add(ReportEntityObj);
                    }
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;
            }
        }
        public List<ReportResponseSummaryByHourEntity> GetResponseSummaryByHourReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {
                List<ReportResponseSummaryByHourEntity> ReportEntityList = new List<ReportResponseSummaryByHourEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_ResponseSummaryByHour", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportResponseSummaryByHourEntity ReportEntityObj = new ReportResponseSummaryByHourEntity();
                    ReportEntityObj.Hour = row["Hour"].ToString();
                    ReportEntityObj.TotalCalls = Convert.ToInt32(row["TotalCalls"].ToString() == "" ? "0" : row["TotalCalls"].ToString());
                    ReportEntityObj.AnsweredCalls = Convert.ToInt32(row["AnsweredCalls"].ToString() == "" ? "0" : row["AnsweredCalls"].ToString());
                    ReportEntityObj.AvgRingAnswered = Math.Round(Convert.ToDouble(row["AvgRingAnswered"].ToString() == "" ? "0" : row["AvgRingAnswered"].ToString()),0);
                    ReportEntityObj.MaxRingAnswered = Convert.ToInt32(row["MaxRingAnswered"].ToString() == "" ? "0" : row["MaxRingAnswered"].ToString());
                    ReportEntityObj.WithinThresholdCount = Convert.ToInt32(row["WithinThresholdCount"].ToString() == "" ? "0" : row["WithinThresholdCount"].ToString());
                    ReportEntityObj.UnAnsweredCalls = Convert.ToInt32(row["UnAnsweredCalls"].ToString() == "" ? "0" : row["UnAnsweredCalls"].ToString());
                    ReportEntityObj.LostCalls = Convert.ToInt32(row["LostCalls"].ToString() == "" ? "0" : row["LostCalls"].ToString());
                    ReportEntityObj.AvgRingLost = Math.Round(Convert.ToDouble(row["AvgRingLost"].ToString() == "" ? "0" : row["AvgRingLost"].ToString()),0);
                    ReportEntityObj.MaxRingLost = Convert.ToInt32(row["MaxRingLost"].ToString() == "" ? "0" : row["MaxRingLost"].ToString());
                    ReportEntityObj.TotalRingAnswered = Convert.ToInt32(row["TotalRingAnswered"].ToString() == "" ? "0" : row["TotalRingAnswered"].ToString());
                    ReportEntityObj.TotalRingUnAnswered = Convert.ToInt32(row["TotalRingUnAnswered"].ToString() == "" ? "0" : row["TotalRingUnAnswered"].ToString());

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportResponseSummaryByDayEntity> GetResponseSummaryByDayReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {


                List<ReportResponseSummaryByDayEntity> ReportEntityList = new List<ReportResponseSummaryByDayEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_ResponseSummaryByDay", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportResponseSummaryByDayEntity ReportEntityObj = new ReportResponseSummaryByDayEntity();

                    DateTime dt1 = DateTime.ParseExact(row["Date"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.Date = datedisplay;
                    // ReportEntityObj.Date = DateTime.Parse(row["Date"].ToString()).ToShortDateString();
                    ReportEntityObj.Day = DateTime.Parse(row["Date"].ToString()).DayOfWeek.ToString();
                    ReportEntityObj.TotalCalls = Convert.ToInt32(row["TotalCalls"].ToString() == "" ? "0" : row["TotalCalls"].ToString());
                    ReportEntityObj.AnsweredCalls = Convert.ToInt32(row["AnsweredCalls"].ToString() == "" ? "0" : row["AnsweredCalls"].ToString());
                    ReportEntityObj.AvgRingAnswered = Math.Round(Convert.ToDouble(row["AvgRingAnswered"].ToString() == "" ? "0" : row["AvgRingAnswered"].ToString()), 0);
                    ReportEntityObj.MaxRingAnswered = Convert.ToInt32(row["MaxRingAnswered"].ToString() == "" ? "0" : row["MaxRingAnswered"].ToString());
                    ReportEntityObj.WithinThresholdCount = Convert.ToInt32(row["WithinThresholdCount"].ToString() == "" ? "0" : row["WithinThresholdCount"].ToString());
                    ReportEntityObj.UnAnsweredCalls = Convert.ToInt32(row["UnAnsweredCalls"].ToString() == "" ? "0" : row["UnAnsweredCalls"].ToString());
                    ReportEntityObj.LostCalls = Convert.ToInt32(row["LostCalls"].ToString() == "" ? "0" : row["LostCalls"].ToString());
                    ReportEntityObj.AvgRingLost = Math.Round(Convert.ToDouble(row["AvgRingLost"].ToString() == "" ? "0" : row["AvgRingLost"].ToString()), 0);
                    ReportEntityObj.MaxRingLost = Convert.ToInt32(row["MaxRingLost"].ToString() == "" ? "0" : row["MaxRingLost"].ToString());
                    ReportEntityObj.TotalRingAnswered = Convert.ToInt32(row["TotalRingAnswered"].ToString() == "" ? "0" : row["TotalRingAnswered"].ToString());
                    ReportEntityObj.TotalRingUnAnswered = Convert.ToInt32(row["TotalRingUnAnswered"].ToString() == "" ? "0" : row["TotalRingUnAnswered"].ToString());

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportResponseSummaryByExtensionEntity> GetResponseSummaryByExtensionReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {


                List<ReportResponseSummaryByExtensionEntity> ReportEntityList = new List<ReportResponseSummaryByExtensionEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_ResponseSummaryByExtension", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportResponseSummaryByExtensionEntity ReportEntityObj = new ReportResponseSummaryByExtensionEntity();
                    ReportEntityObj.Extension = row["Extension"].ToString();
                    ReportEntityObj.TotalCalls = Convert.ToInt32(row["TotalCalls"].ToString() == "" ? "0" : row["TotalCalls"].ToString());
                    ReportEntityObj.AnsweredCalls = Convert.ToInt32(row["AnsweredCalls"].ToString() == "" ? "0" : row["AnsweredCalls"].ToString());
                    ReportEntityObj.AvgRingAnswered = Math.Round(Convert.ToDouble(row["AvgRingAnswered"].ToString() == "" ? "0" : row["AvgRingAnswered"].ToString()), 0);
                    ReportEntityObj.MaxRingAnswered = Convert.ToInt32(row["MaxRingAnswered"].ToString() == "" ? "0" : row["MaxRingAnswered"].ToString());
                    ReportEntityObj.WithinThresholdCount = Convert.ToInt32(row["WithinThresholdCount"].ToString() == "" ? "0" : row["WithinThresholdCount"].ToString());
                    ReportEntityObj.UnAnsweredCalls = Convert.ToInt32(row["UnAnsweredCalls"].ToString() == "" ? "0" : row["UnAnsweredCalls"].ToString());
                    ReportEntityObj.LostCalls = Convert.ToInt32(row["LostCalls"].ToString() == "" ? "0" : row["LostCalls"].ToString());
                    ReportEntityObj.AvgRingLost = Math.Round(Convert.ToDouble(row["AvgRingLost"].ToString() == "" ? "0" : row["AvgRingLost"].ToString()), 0);
                    ReportEntityObj.MaxRingLost = Convert.ToInt32(row["MaxRingLost"].ToString() == "" ? "0" : row["MaxRingLost"].ToString());
                    ReportEntityObj.CallsMade = Convert.ToInt32(row["CallsMade"].ToString() == "" ? "0" : row["CallsMade"].ToString());
                    ReportEntityObj.TotalRingAnswered = Convert.ToInt32(row["TotalRingAnswered"].ToString() == "" ? "0" : row["TotalRingAnswered"].ToString());
                    ReportEntityObj.TotalRingUnAnswered = Convert.ToInt32(row["TotalRingUnAnswered"].ToString() == "" ? "0" : row["TotalRingUnAnswered"].ToString());

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportResponseSummaryByPhoneEntity> GetResponseSummaryByPhoneReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {


                List<ReportResponseSummaryByPhoneEntity> ReportEntityList = new List<ReportResponseSummaryByPhoneEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_ResponseSummaryByPhone", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportResponseSummaryByPhoneEntity ReportEntityObj = new ReportResponseSummaryByPhoneEntity();
                    ReportEntityObj.DialledNumber = row["DialledNumber"].ToString();
                    ReportEntityObj.Duration = Convert.ToDouble(row["Duration"].ToString() == "" ? "0" : row["Duration"].ToString());
                    ReportEntityObj.TotalCalls = Convert.ToInt32(row["TotalCalls"].ToString() == "" ? "0" : row["TotalCalls"].ToString());
                    

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportResponseSummaryByExtensionItemisedEntity> GetResponseSummaryByExtensionItemisedReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {


                List<ReportResponseSummaryByExtensionItemisedEntity> ReportEntityList = new List<ReportResponseSummaryByExtensionItemisedEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_ResponseSummaryByExtensionItemised", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportResponseSummaryByExtensionItemisedEntity ReportEntityObj = new ReportResponseSummaryByExtensionItemisedEntity();

                    ReportEntityObj.Extension = row["Extension"].ToString();
                    DateTime dt1 = DateTime.ParseExact(row["Date"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.Date = datedisplay;
                    // ReportEntityObj.Date = DateTime.Parse(row["Date"].ToString()).ToShortDateString();
                    ReportEntityObj.Time = row["Time"].ToString();
                    ReportEntityObj.Duration = Convert.ToDouble(row["Duration"].ToString() == "" ? "0" : row["Duration"].ToString());
                    ReportEntityObj.Direction = row["Direction"].ToString();
                    ReportEntityObj.CLI = row["CLI"].ToString();
                    ReportEntityObj.DDI = row["DDI"].ToString();
                    ReportEntityObj.LastState = row["LastState"].ToString();
                    ReportEntityObj.InitialState = row["InitialState"].ToString();
                    ReportEntityObj.RingDuration = Convert.ToDouble(row["RingDuration"].ToString() == "" ? "0" : row["RingDuration"].ToString());


                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportTrafficSummaryByHourEntity> GetTrafficSummaryByHourReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {


                List<ReportTrafficSummaryByHourEntity> ReportEntityList = new List<ReportTrafficSummaryByHourEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_TrafficSummaryByHour", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportTrafficSummaryByHourEntity ReportEntityObj = new ReportTrafficSummaryByHourEntity();
                    ReportEntityObj.Hour = row["Hour"].ToString();
                    ReportEntityObj.IncomingCalls = Convert.ToInt32(row["IncomingCalls"].ToString() == "" ? "0" : row["IncomingCalls"].ToString());
                    ReportEntityObj.IncomingAnsweredCalls = Convert.ToInt32(row["IncomingAnsweredCalls"].ToString() == "" ? "0" : row["IncomingAnsweredCalls"].ToString());
                    ReportEntityObj.IncomingUnAnsweredCalls = Convert.ToInt32(row["IncomingUnAnsweredCalls"].ToString() == "" ? "0" : row["IncomingUnAnsweredCalls"].ToString());
                    ReportEntityObj.IncomingCallDuration = Convert.ToDouble(row["IncomingCallDuration"].ToString() == "" ? "0" : row["IncomingCallDuration"].ToString());
                    ReportEntityObj.OutgoingCalls = Convert.ToInt32(row["OutgoingCalls"].ToString() == "" ? "0" : row["OutgoingCalls"].ToString());
                    ReportEntityObj.OutgoingAnsweredCalls = Convert.ToInt32(row["OutgoingAnsweredCalls"].ToString() == "" ? "0" : row["OutgoingAnsweredCalls"].ToString());
                    ReportEntityObj.OutgoingUnAnsweredCalls = Convert.ToInt32(row["OutgoingUnAnsweredCalls"].ToString() == "" ? "0" : row["OutgoingUnAnsweredCalls"].ToString());
                    ReportEntityObj.OutgoingCallDuration = Convert.ToDouble(row["OutgoingCallDuration"].ToString() == "" ? "0" : row["OutgoingCallDuration"].ToString());
                    ReportEntityObj.Cost = Math.Round(Convert.ToDouble(row["Cost"].ToString() == "" ? "0" : row["Cost"].ToString()), 2);

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportTrafficSummaryByDayEntity> GetTrafficSummaryByDayReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {


                List<ReportTrafficSummaryByDayEntity> ReportEntityList = new List<ReportTrafficSummaryByDayEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_TrafficSummaryByDay", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportTrafficSummaryByDayEntity ReportEntityObj = new ReportTrafficSummaryByDayEntity();
                    DateTime dt1 = DateTime.ParseExact(row["Date"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.Date = datedisplay;
                    // ReportEntityObj.Date = DateTime.Parse(row["Date"].ToString()).ToShortDateString();
                    ReportEntityObj.Day = DateTime.Parse(row["Date"].ToString()).DayOfWeek.ToString();
                    ReportEntityObj.IncomingCalls = Convert.ToInt32(row["IncomingCalls"].ToString() == "" ? "0" : row["IncomingCalls"].ToString());
                    ReportEntityObj.IncomingAnsweredCalls = Convert.ToInt32(row["IncomingAnsweredCalls"].ToString() == "" ? "0" : row["IncomingAnsweredCalls"].ToString());
                    ReportEntityObj.IncomingUnAnsweredCalls = Convert.ToInt32(row["IncomingUnAnsweredCalls"].ToString() == "" ? "0" : row["IncomingUnAnsweredCalls"].ToString());
                    ReportEntityObj.IncomingCallDuration = Convert.ToDouble(row["IncomingCallDuration"].ToString() == "" ? "0" : row["IncomingCallDuration"].ToString());
                    ReportEntityObj.OutgoingCalls = Convert.ToInt32(row["OutgoingCalls"].ToString() == "" ? "0" : row["OutgoingCalls"].ToString());
                    ReportEntityObj.OutgoingAnsweredCalls = Convert.ToInt32(row["OutgoingAnsweredCalls"].ToString() == "" ? "0" : row["OutgoingAnsweredCalls"].ToString());
                    ReportEntityObj.OutgoingUnAnsweredCalls = Convert.ToInt32(row["OutgoingUnAnsweredCalls"].ToString() == "" ? "0" : row["OutgoingUnAnsweredCalls"].ToString());
                    ReportEntityObj.OutgoingCallDuration = Convert.ToDouble(row["OutgoingCallDuration"].ToString() == "" ? "0" : row["OutgoingCallDuration"].ToString());
                    ReportEntityObj.Cost = Math.Round(Convert.ToDouble(row["Cost"].ToString() == "" ? "0" : row["Cost"].ToString()), 2);

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportTrafficSummaryByExtensionEntity> GetTrafficSummaryByExtensionReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {
                List<ReportTrafficSummaryByExtensionEntity> ReportEntityList = new List<ReportTrafficSummaryByExtensionEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_TrafficSummaryByExtension", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportTrafficSummaryByExtensionEntity ReportEntityObj = new ReportTrafficSummaryByExtensionEntity();
                    ReportEntityObj.Extension = row["Extension"].ToString();
                    ReportEntityObj.IncomingCalls = Convert.ToInt32(row["IncomingCalls"].ToString() == "" ? "0" : row["IncomingCalls"].ToString());
                    ReportEntityObj.IncomingAnsweredCalls = Convert.ToInt32(row["IncomingAnsweredCalls"].ToString() == "" ? "0" : row["IncomingAnsweredCalls"].ToString());
                    ReportEntityObj.IncomingUnAnsweredCalls = Convert.ToInt32(row["IncomingUnAnsweredCalls"].ToString() == "" ? "0" : row["IncomingUnAnsweredCalls"].ToString());
                    ReportEntityObj.IncomingCallDuration = Convert.ToDouble(row["IncomingCallDuration"].ToString() == "" ? "0" : row["IncomingCallDuration"].ToString());
                    ReportEntityObj.OutgoingCalls = Convert.ToInt32(row["OutgoingCalls"].ToString() == "" ? "0" : row["OutgoingCalls"].ToString());
                    ReportEntityObj.OutgoingAnsweredCalls = Convert.ToInt32(row["OutgoingAnsweredCalls"].ToString() == "" ? "0" : row["OutgoingAnsweredCalls"].ToString());
                    ReportEntityObj.OutgoingUnAnsweredCalls = Convert.ToInt32(row["OutgoingUnAnsweredCalls"].ToString() == "" ? "0" : row["OutgoingUnAnsweredCalls"].ToString());
                    ReportEntityObj.OutgoingCallDuration = Convert.ToDouble(row["OutgoingCallDuration"].ToString() == "" ? "0" : row["OutgoingCallDuration"].ToString());
                    ReportEntityObj.Cost = Math.Round(Convert.ToDouble(row["Cost"].ToString() == "" ? "0" : row["Cost"].ToString()), 2);

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;
            }
        }
        public List<ReportTrafficSummaryByPhoneEntity> GetTrafficSummaryByPhoneReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {


                List<ReportTrafficSummaryByPhoneEntity> ReportEntityList = new List<ReportTrafficSummaryByPhoneEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_TrafficSummaryByPhone", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportTrafficSummaryByPhoneEntity ReportEntityObj = new ReportTrafficSummaryByPhoneEntity();
                    ReportEntityObj.DialledNumber = row["DialledNumber"].ToString();
                    ReportEntityObj.OutgoingCalls = Convert.ToInt32(row["OutgoingCalls"].ToString() == "" ? "0" : row["OutgoingCalls"].ToString());
                    ReportEntityObj.OutgoingAnsweredCalls = Convert.ToInt32(row["OutgoingAnsweredCalls"].ToString() == "" ? "0" : row["OutgoingAnsweredCalls"].ToString());
                    ReportEntityObj.OutgoingUnAnsweredCalls = Convert.ToInt32(row["OutgoingUnAnsweredCalls"].ToString() == "" ? "0" : row["OutgoingUnAnsweredCalls"].ToString());
                    ReportEntityObj.OutgoingCallDuration = Convert.ToDouble(row["OutgoingCallDuration"].ToString() == "" ? "0" : row["OutgoingCallDuration"].ToString());
                    ReportEntityObj.Cost = Math.Round(Convert.ToDouble(row["Cost"].ToString() == "" ? "0" : row["Cost"].ToString()), 2);


                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportTrafficSummaryByExtensionItemisedEntity> GetTrafficSummaryByExtensionItemisedReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays, int CallsOption)
        {
            try
            {


                List<ReportTrafficSummaryByExtensionItemisedEntity> ReportEntityList = new List<ReportTrafficSummaryByExtensionItemisedEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_TrafficSummaryByExtensionItemised", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportTrafficSummaryByExtensionItemisedEntity ReportEntityObj = new ReportTrafficSummaryByExtensionItemisedEntity();

                    ReportEntityObj.Extension = row["Extension"].ToString();
                    DateTime dt1 = DateTime.ParseExact(row["Date"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.Date = datedisplay;
                    //ReportEntityObj.Date = DateTime.Parse(row["Date"].ToString()).ToShortDateString();
                    ReportEntityObj.Time = row["Time"].ToString();
                    ReportEntityObj.Duration = Convert.ToDouble(row["Duration"].ToString() == "" ? "0" : row["Duration"].ToString());
                    ReportEntityObj.Direction = row["Direction"].ToString();
                    ReportEntityObj.CLI = row["CLI"].ToString();
                    ReportEntityObj.DDI = row["DDI"].ToString();
                    ReportEntityObj.Cost = Math.Round(Convert.ToDouble(row["Cost"].ToString() == "" ? "0" : row["Cost"].ToString()), 2);
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportCallTrailEntity> GetCallTrailReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {


                List<ReportCallTrailEntity> ReportEntityList = new List<ReportCallTrailEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_CallTrailReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCallTrailEntity ReportEntityObj = new ReportCallTrailEntity();

                    ReportEntityObj.Status = row["Status"].ToString();
                    ReportEntityObj.MSerial = row["MSerial"].ToString();
                    ReportEntityObj.Extension = row["Extension"].ToString();
                    ReportEntityObj.direction = row["Direction"].ToString();
                    ReportEntityObj.CLI = row["CLI"].ToString();
                    ReportEntityObj.DDI = row["DDI"].ToString();
                    DateTime dt1 = DateTime.ParseExact(row["StartTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info);
                    ReportEntityObj.StartTime = datedisplay;
                    // ReportEntityObj.StartTime = DateTime.Parse(row["StartTime"].ToString());
                    ReportEntityObj.HoldDuration = Convert.ToDouble(row["HoldDuration"].ToString() == "" ? "0" : row["HoldDuration"].ToString());
                    ReportEntityObj.TotalConversation = Convert.ToDouble(row["TotalConversation"].ToString() == "" ? "0" : row["TotalConversation"].ToString());
                    ReportEntityObj.TotalRingDuration = Convert.ToDouble(row["TotalRingDuration"].ToString() == "" ? "0" : row["TotalRingDuration"].ToString());
                    ReportEntityObj.TotalDuration = Convert.ToDouble(row["TotalDuration"].ToString() == "" ? "0" : row["TotalDuration"].ToString());
                    ReportEntityObj.MExtension = row["MExtension"].ToString();
                    ReportEntityObj.mdirection = row["MDirection"].ToString();
                    ReportEntityObj.MCLI = row["MCLI"].ToString();
                    ReportEntityObj.MDDI = row["MDDI"].ToString();

                    dt1 = DateTime.ParseExact(row["MStartTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    datedisplay = dt1.ToString(info);
                    ReportEntityObj.MStartTime = datedisplay;
                    //ReportEntityObj.MStartTime = DateTime.Parse(row["MStartTime"].ToString());
                    ReportEntityObj.MHoldDuration = Convert.ToDouble(row["MHoldDuration"].ToString() == "" ? "0" : row["MHoldDuration"].ToString());
                    ReportEntityObj.MTotalConversation = Convert.ToDouble(row["MTotalConversation"].ToString() == "" ? "0" : row["MTotalConversation"].ToString());
                    ReportEntityObj.MTotalRingDuration = Convert.ToDouble(row["MTotalRingDuration"].ToString() == "" ? "0" : row["MTotalRingDuration"].ToString());
                    ReportEntityObj.MTotalDuration = Convert.ToDouble(row["MTotalDuration"].ToString() == "" ? "0" : row["MTotalDuration"].ToString());
                   // ReportEntityObj.OrignalNumber = row["OrignalNumber"].ToString();
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportTrafficSummaryByAreaCodeOutboundEntity> GetTrafficSummaryByAreaCodeOutboundReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {
                List<ReportTrafficSummaryByAreaCodeOutboundEntity> ReportEntityList = new List<ReportTrafficSummaryByAreaCodeOutboundEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_TrafficSummaryByAreaCodeOutbound", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportTrafficSummaryByAreaCodeOutboundEntity ReportEntityObj = new ReportTrafficSummaryByAreaCodeOutboundEntity();
                    //ReportEntityObj.DDI = row["DDI"].ToString();
                    ReportEntityObj.AreaCode = row["AreaCode"].ToString();
                    ReportEntityObj.AreaDescription = row["AreaDescription"].ToString();

                    ReportEntityObj.OutgoingCalls = Convert.ToInt32(row["OutgoingCalls"].ToString() == "" ? "0" : row["OutgoingCalls"].ToString());
                    ReportEntityObj.OutgoingAnsweredCalls = Convert.ToInt32(row["OutgoingAnsweredCalls"].ToString() == "" ? "0" : row["OutgoingAnsweredCalls"].ToString());
                    ReportEntityObj.OutgoingUnAnsweredCalls = Convert.ToInt32(row["OutgoingUnAnsweredCalls"].ToString() == "" ? "0" : row["OutgoingUnAnsweredCalls"].ToString());
                    ReportEntityObj.OutgoingCallDuration = Convert.ToDouble(row["OutgoingCallDuration"].ToString() == "" ? "0" : row["OutgoingCallDuration"].ToString());

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;
            }
        }
        public List<ReportTrafficSummaryByAreaCodeInboundEntity> GetTrafficSummaryByAreaCodeInboundReport(int InternalLength, int HangUpThreshold, DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Extensions, string WeekDays)
        {
            try
            {
                List<ReportTrafficSummaryByAreaCodeInboundEntity> ReportEntityList = new List<ReportTrafficSummaryByAreaCodeInboundEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Log_TrafficSummaryByAreaCodeInbound", con);
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@HangUpThreshold", HangUpThreshold));
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Extensions", Extensions));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportTrafficSummaryByAreaCodeInboundEntity ReportEntityObj = new ReportTrafficSummaryByAreaCodeInboundEntity();
                    //ReportEntityObj.DDI = row["DDI"].ToString();
                    ReportEntityObj.AreaCode = row["AreaCode"].ToString();
                    ReportEntityObj.AreaDescription = row["AreaDescription"].ToString();

                    ReportEntityObj.IncomingCalls = Convert.ToInt32(row["IncomingCalls"].ToString() == "" ? "0" : row["IncomingCalls"].ToString());
                    ReportEntityObj.IncomingAnsweredCalls = Convert.ToInt32(row["IncomingAnsweredCalls"].ToString() == "" ? "0" : row["IncomingAnsweredCalls"].ToString());
                    ReportEntityObj.IncomingUnAnsweredCalls = Convert.ToInt32(row["IncomingUnAnsweredCalls"].ToString() == "" ? "0" : row["IncomingUnAnsweredCalls"].ToString());
                    ReportEntityObj.IncomingCallDuration = Convert.ToDouble(row["IncomingCallDuration"].ToString() == "" ? "0" : row["IncomingCallDuration"].ToString());

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;
            }
        }
        public List<ReportTrafficSummaryByDayEntity> GetCallSummaryByDayGraphReport(DateTime FromDate, DateTime ToDate, int InternalLength, int CallsOption)
        {
            try
            {


                List<ReportTrafficSummaryByDayEntity> ReportEntityList = new List<ReportTrafficSummaryByDayEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Graph_CallSummaryByDay", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportTrafficSummaryByDayEntity ReportEntityObj = new ReportTrafficSummaryByDayEntity();
                    DateTime dt1 = DateTime.ParseExact(row["Date"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.Date = datedisplay;
                    //ReportEntityObj.Date = DateTime.Parse(row["Date"].ToString()).ToShortDateString();
                    ReportEntityObj.Day = DateTime.Parse(row["Date"].ToString()).DayOfWeek.ToString();
                    ReportEntityObj.IncomingCalls = Convert.ToInt32(row["IncomingCalls"].ToString() == "" ? "0" : row["IncomingCalls"].ToString());
                    ReportEntityObj.IncomingAnsweredCalls = Convert.ToInt32(row["IncomingAnsweredCalls"].ToString() == "" ? "0" : row["IncomingAnsweredCalls"].ToString());
                    ReportEntityObj.IncomingUnAnsweredCalls = Convert.ToInt32(row["IncomingUnAnsweredCalls"].ToString() == "" ? "0" : row["IncomingUnAnsweredCalls"].ToString());
                    ReportEntityObj.IncomingCallDuration = Convert.ToDouble(row["IncomingCallDuration"].ToString() == "" ? "0" : row["IncomingCallDuration"].ToString());
                    ReportEntityObj.OutgoingCalls = Convert.ToInt32(row["OutgoingCalls"].ToString() == "" ? "0" : row["OutgoingCalls"].ToString());
                    ReportEntityObj.OutgoingAnsweredCalls = Convert.ToInt32(row["OutgoingAnsweredCalls"].ToString() == "" ? "0" : row["OutgoingAnsweredCalls"].ToString());
                    ReportEntityObj.OutgoingUnAnsweredCalls = Convert.ToInt32(row["OutgoingUnAnsweredCalls"].ToString() == "" ? "0" : row["OutgoingUnAnsweredCalls"].ToString());
                    ReportEntityObj.OutgoingCallDuration = Convert.ToDouble(row["OutgoingCallDuration"].ToString() == "" ? "0" : row["OutgoingCallDuration"].ToString());
                    ReportEntityObj.Cost = Math.Round(Convert.ToDouble(row["Cost"].ToString() == "" ? "0" : row["Cost"].ToString()), 2);

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportOutBoundCallsByRegion> GetOutboundCallsByRegionGraphReport(DateTime FromDate, DateTime ToDate, int InternalLength, int CallsOption)
        {
            try
            {
                List<ReportOutBoundCallsByRegion> ReportEntityList = new List<ReportOutBoundCallsByRegion>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Graph_OutboundCallsByRegion", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                int i = 0;
                int indexOther = -1;
                var random = new Random();//Zeeshan
                foreach (DataRow row in dt.Rows)
                {
                    ReportOutBoundCallsByRegion ReportEntityObj = new ReportOutBoundCallsByRegion();
                    string Region = row["Region"].ToString();
                    int TotalCalls= Convert.ToInt32(row["TotalCalls"].ToString());
                    if (TotalCalls <= 0)
                        continue;
                    ReportEntityObj.TotalCalls = TotalCalls;
                    ReportEntityObj.Region = row["Region"].ToString();
                    ReportEntityList.Add(ReportEntityObj);

                    if (Region.Trim().ToLower() == "Internal".ToLower())
                    {
                        ReportEntityObj.color = "#000000";
                    }
                    else if (Region.Trim().ToLower() == "International".ToLower())
                    {
                        ReportEntityObj.color = "#f91627";
                    }
                    else
                    {
                        indexOther++;
                        if (indexOther == 0)
                        {
                            ReportEntityObj.color = "#d6d6d6";
                        }
                        else if (indexOther == 1)
                        {
                            ReportEntityObj.color = "#757575";
                        }
                        else if (indexOther == 2)
                        {
                            ReportEntityObj.color = "#ababab";
                        }
                        //int colorvalue = random.Next(0, 230);
                        //string color = String.Format("{0:X2}", colorvalue);
                        //color = "#" + color + color + color;
                        //ReportEntityObj.color = color;
                    }
                  //  ReportEntityObj.color = String.Format("#{0:X6}", random.Next(0x1000000));//Zeeshan
                    /*Zeeshan
                    if (i == 0)
                    {
                        ReportEntityObj.color = "#000000";
                    }
                    else if (i == 1)
                    {
                        ReportEntityObj.color = "#57565c";
                    }
                    else if (i == 2)
                    {
                        ReportEntityObj.color = "#a89fa0";
                    }
                    else if (i == 3)
                    {
                        ReportEntityObj.color = "#f91627";
                    }*/
                    i++;
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportCallsServicedByDay> GetCallsServicedByDayGraphReport(DateTime FromDate, DateTime ToDate, int InternalLength, int CallsOption)
        {
            try
            {
                List<ReportCallsServicedByDay> ReportEntityList = new List<ReportCallsServicedByDay>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Graph_CallsServicedByDay", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCallsServicedByDay ReportEntityObj = new ReportCallsServicedByDay();

                    DateTime dt1 = DateTime.ParseExact(row["Date"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info.DateTimeFormat.ShortDatePattern);
                    ReportEntityObj.Date = datedisplay;
                    //ReportEntityObj.Date = DateTime.Parse(row["Date"].ToString()).ToShortDateString();
                    ReportEntityObj.AnsweredCalls = Convert.ToInt32(row["AnsweredCalls"].ToString());
                    ReportEntityObj.LostCalls = Convert.ToInt32(row["LostCalls"].ToString());
                    ReportEntityList.Add(ReportEntityObj);
                    
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportCostSummaryEntity> GetCostSummaryGraphReport1(DateTime FromDate, DateTime ToDate,int InternalLength)
        {
            try
            {
                List<ReportCostSummaryEntity> ReportEntityList = new List<ReportCostSummaryEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Graph_CostSummary", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                ReportCostSummaryEntity ReportEntityObj = new ReportCostSummaryEntity();
                ReportEntityObj.color = "#f91627";
                ReportEntityObj.CostType = "Outbound Cost";
                foreach (DataRow row in dt.Rows)
                {
                    Log4Net.WriteLog("----\n", LogType.GENERALLOG);
                    Log4Net.WriteLog(row["Cost"].ToString(), LogType.GENERALLOG);
                    ReportEntityObj.Cost = Math.Round(Convert.ToDouble(row["Cost"].ToString()==""?"0": row["Cost"].ToString()),2);
                    ReportEntityList.Add(ReportEntityObj);

                }
                if (dt.Rows.Count==0)
                {
                    ReportEntityObj.Cost = 0;
                    ReportEntityList.Add(ReportEntityObj);
                }
                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportCostSummaryEntity> GetCostSummaryGraphReport2(List<ReportCostSummaryEntity> ReportEntityList,DateTime FromDate, DateTime ToDate,float DiskUsageCostPerMin,string RecordingPath,float DurationCostPerMin)
        {
            try
            {
                
                List<ReportDiskUsageCostEntity> ReportEntityList1 = ReportsCommonMethods.GetDiskUsageCosting(DiskUsageCostPerMin, RecordingPath).Where(x => x.Time_Stamp >= FromDate && x.Time_Stamp <= ToDate).ToList();
                ReportCostSummaryEntity ReportCostSummaryEntityObj = new ReportCostSummaryEntity();
                ReportCostSummaryEntityObj.color = "#000000";
                ReportCostSummaryEntityObj.CostType = "Disk Cost";
                foreach (ReportDiskUsageCostEntity obj in ReportEntityList1)
                {
                    ReportCostSummaryEntityObj.Cost+= obj.Cost;  
                }
                if (ReportEntityList1.Count == 0)
                {
                    ReportCostSummaryEntityObj.Cost = 0;
                }
                ReportCostSummaryEntityObj.Cost=Math.Round(ReportCostSummaryEntityObj.Cost, 2);
                ReportEntityList.Add(ReportCostSummaryEntityObj);
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_DurationCostGraphReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCostSummaryEntityObj = new ReportCostSummaryEntity();
                    
                    double result;
                    string s = row["Duration"].ToString();
                    double duration=Double.TryParse(row["Duration"].ToString(), out result) ? result : 0.00;
                    ReportCostSummaryEntityObj.Cost = Math.Round(duration/60000 * DurationCostPerMin,2);
                    ReportCostSummaryEntityObj.CostType = "Duration Cost";
                    ReportCostSummaryEntityObj.color = "#57565c";
                    ReportEntityList.Add(ReportCostSummaryEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportInboundCallsByHourEntity> GetInboundCallsByHourGraphReport(DateTime FromDate, DateTime ToDate, int InternalLength, int CallsOption)
        {
            try
            {
                List<ReportInboundCallsByHourEntity> ReportEntityList = new List<ReportInboundCallsByHourEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Graph_InboundCallsByHour", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportInboundCallsByHourEntity ReportEntityObj = new ReportInboundCallsByHourEntity();
                    ReportEntityObj.Hour = GetHourFormatString(row["Hour"].ToString());
                    ReportEntityObj.Calls = Convert.ToInt32(row["Calls"].ToString());
                    ReportEntityList.Add(ReportEntityObj);

                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportOutboundCallsByHourEntity> GetOutboundCallsByHourGraphReport(DateTime FromDate, DateTime ToDate, int InternalLength, int CallsOption)
        {
            try
            {
                List<ReportOutboundCallsByHourEntity> ReportEntityList = new List<ReportOutboundCallsByHourEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Graph_OutboundCallsByHour", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportOutboundCallsByHourEntity ReportEntityObj = new ReportOutboundCallsByHourEntity();
                    ReportEntityObj.Hour = GetHourFormatString(row["Hour"].ToString());
                    ReportEntityObj.Calls = Convert.ToInt32(row["Calls"].ToString());
                    ReportEntityList.Add(ReportEntityObj);

                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportCallsLostByHourEntity> GetCallsLostByHourGraphReport(DateTime FromDate, DateTime ToDate, int InternalLength, int CallsOption)
        {
            try
            {
                List<ReportCallsLostByHourEntity> ReportEntityList = new List<ReportCallsLostByHourEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Graph_CallsLostByHour", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCallsLostByHourEntity ReportEntityObj = new ReportCallsLostByHourEntity();
                    ReportEntityObj.Hour = GetHourFormatString(row["Hour"].ToString());
                    ReportEntityObj.Calls = Convert.ToInt32(row["Calls"].ToString());
                    ReportEntityList.Add(ReportEntityObj);

                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportLongestRingTimeByHourEntity> GetLongestRingTimeByHourGraphReport(DateTime FromDate, DateTime ToDate, int InternalLength, int CallsOption)
        {
            try
            {
                List<ReportLongestRingTimeByHourEntity> ReportEntityList = new List<ReportLongestRingTimeByHourEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Graph_LongestRingTimeByHour", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportLongestRingTimeByHourEntity ReportEntityObj = new ReportLongestRingTimeByHourEntity();
                    ReportEntityObj.Hour = GetHourFormatString(row["Hour"].ToString());
                    ReportEntityObj.MaxRing = Convert.ToDouble(row["MaxRing"].ToString() == "" ? "0" : row["MaxRing"].ToString());
                    ReportEntityList.Add(ReportEntityObj);

                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportCallSummaryByExtensionEntity> GetCallSummaryByExtensionGraphReport(DateTime FromDate, DateTime ToDate, int InternalLength, int CallsOption)
        {
            try
            {
                List<ReportCallSummaryByExtensionEntity> ReportEntityList = new List<ReportCallSummaryByExtensionEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_Graph_CallSummaryByExtension", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@InternalLength", InternalLength));
                cmd.Parameters.Add(new MySqlParameter("@CallsOption", CallsOption));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportCallSummaryByExtensionEntity ReportEntityObj = new ReportCallSummaryByExtensionEntity();
                    ReportEntityObj.Extension = row["Extension"].ToString();
                    ReportEntityObj.Name = row["Name"].ToString();
                    ReportEntityObj.IncomingCalls = Convert.ToInt32(row["IncomingCalls"].ToString());
                    ReportEntityObj.IncomingCallDuration = ReportsCommonMethods.GetTimeFromSeconds(Convert.ToDouble(row["IncomingCallDuration"].ToString() == "" ? "0" : row["IncomingCallDuration"].ToString()));
                    ReportEntityObj.OutgoingCalls = Convert.ToInt32(row["OutgoingCalls"].ToString());
                    ReportEntityObj.OutgoingCallDuration = ReportsCommonMethods.GetTimeFromSeconds(Convert.ToDouble(row["OutgoingCallDuration"].ToString() == "" ? "0" : row["OutgoingCallDuration"].ToString())); 
                    ReportEntityObj.TotalCalls = Convert.ToInt32(row["TotalCalls"].ToString());
                    ReportEntityObj.TotalDuration = ReportsCommonMethods.GetTimeFromSeconds(Convert.ToDouble(row["TotalDuration"].ToString() == "" ? "0" : row["TotalDuration"].ToString())); 

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<CallActionsEntity> GetActivityLogReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Users, string WeekDays)
        {
            try
            {


                List<CallActionsEntity> ReportEntityList = new List<CallActionsEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ActivityReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Users", Users));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    CallActionsEntity ReportEntityObj = new CallActionsEntity();
                    ReportEntityObj.Time_Stamp = DateTime.Parse(row["Time_Stamp"].ToString());
                    ReportEntityObj.UserName = row["UserName"].ToString();
                    ReportEntityObj.CallDate = DateTime.Parse(row["CallDate"].ToString());
                    ReportEntityObj.Call_CLI = row["Call_CLI"].ToString();
                    ReportEntityObj.Call_DDI = row["Call_DDI"].ToString();
                    ReportEntityObj.Extension = row["Extension"].ToString();
                    ReportEntityObj.FirstName = row["FirstName"].ToString();
                    ReportEntityObj.LastName = row["LastName"].ToString();
                    ReportEntityObj.Action = row["Action"].ToString();


                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public List<CallNotesEntity> GetCallNotesReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Users, string WeekDays)
        {
            try
            {


                List<CallNotesEntity> ReportEntityList = new List<CallNotesEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_CallNotesReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Users", Users));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    CallNotesEntity ReportEntityObj = new CallNotesEntity();
                    DateTime dt1 = DateTime.ParseExact(row["CallDate"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info);
                    ReportEntityObj.CallDate = datedisplay;
                    //ReportEntityObj.CallDate = DateTime.Parse(row["Time_Stamp"].ToString());
                    ReportEntityObj.FirstName = row["FirstName"].ToString();
                    ReportEntityObj.LastName = row["LastName"].ToString();
                    dt1 = DateTime.ParseExact(row["NotesTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);                  
                    datedisplay = dt1.ToString(info);
                    ReportEntityObj.NotesTime = datedisplay;
                    // ReportEntityObj.CallDate = DateTime.Parse(row["CallDate"].ToString());
                    ReportEntityObj.CallDetailId = Convert.ToInt32(row["CallDetailId"]);
                    ReportEntityObj.Call_CLI = row["Call_CLI"].ToString();
                    ReportEntityObj.Call_DDI = row["Call_DDI"].ToString();                   
                    ReportEntityObj.Note = row["Note"].ToString();                

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportDiskUsageCostEntity> GetDiskUsageCostReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo,string RecordingPath,float DiskUsageCostPerMin, string WeekDays)
        {
            try
            {
                
                List<ReportDiskUsageCostEntity> ReportEntityList=ReportsCommonMethods.GetDiskUsageCosting(DiskUsageCostPerMin,RecordingPath).Where(x => x.Time_Stamp >= FromDate && x.Time_Stamp <= ToDate).ToList();
                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportDurationCostEntity> GetDurationCostReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo,float DurationCostPerMin, string WeekDays)
        {
            try
            {


                List<ReportDurationCostEntity> ReportEntityList = new List<ReportDurationCostEntity>();


                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_DurationCostReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    ReportDurationCostEntity ReportEntityObj = new ReportDurationCostEntity();
                    ReportEntityObj.Time_Stamp = DateTime.Parse(row["Time_Stamp"].ToString());
                    ReportEntityObj.Day = DateTime.Parse(row["Time_Stamp"].ToString()).DayOfWeek.ToString();
                    ReportEntityObj.Duration = Convert.ToDouble(row["Duration"].ToString() == "" ? "0" : row["Duration"].ToString());
                    ReportEntityObj.DurationCost = DurationCostPerMin;


                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        // Mazhar -- Supervisor scoring report 44
        public List<ReportAgentScoringEntity> GetAgentScoringReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string Agents, string WeekDays, int AnsweredInterval, float WaitingTimeInterval)
        {
            try
            {
                List<ReportAgentScoringEntity> ReportEntityList = new List<ReportAgentScoringEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_AgentScoringReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Agents", Agents));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@AnswerInterval", AnsweredInterval));
                cmd.Parameters.Add(new MySqlParameter("@AvailableInterval", WaitingTimeInterval));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportAgentScoringEntity ReportEntityObj = new ReportAgentScoringEntity();
                    ReportEntityObj.boardID = Convert.ToInt32(row["boardID"].ToString() == "" ? "0" : row["boardID"].ToString());
                    ReportEntityObj.Title = row["Title"].ToString();
                    ReportEntityObj.AgentName = row["AgentName"].ToString();
                    DateTime dt1 = DateTime.ParseExact(row["StartTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info);
                    ReportEntityObj.StartTime = datedisplay;
                    // ReportEntityObj.StartTime = row["StartTime"].ToString();
                    ReportEntityObj.AvailableTime = Convert.ToInt32(row["AvailableTime"].ToString() == "" ? "0" : row["AvailableTime"].ToString());
                    ReportEntityObj.CallsHandled = Convert.ToInt32(row["CallsHandled"].ToString() == "" ? "0" : row["CallsHandled"].ToString());

                    ReportEntityObj.CallsServed = Convert.ToInt32(row["CallsServed"].ToString() == "" ? "0" : row["CallsServed"].ToString());
                    ReportEntityObj.TotalAnsweredWithIn = Convert.ToInt32(row["TotalAnsweredWithIn"].ToString() == "" ? "0" : row["TotalAnsweredWithIn"].ToString());
                    ReportEntityObj.AnsweredWithInPercent = float.Parse(row["AnsweredWithInPercent"].ToString() == "" ? "0" : row["AnsweredWithInPercent"].ToString());
                    ReportEntityObj.TotalTime = Convert.ToInt32(row["TotalTime"].ToString() == "" ? "0" : row["TotalTime"].ToString());
                    //ReportEntityObj.AnsweredWithInPercent = float.Parse(row["AnsweredWithInPercent"].ToString() == "" ? "0" : row["AnsweredWithInPercent"].ToString());
                    ReportEntityObj.CallAcceptedPercent = float.Parse(row["CallAcceptedPercent"].ToString() == "" ? "0" : row["CallAcceptedPercent"].ToString());
                    ReportEntityObj.AgentAvailabilityPercent = float.Parse(row["AgentAvailabilityPercent"].ToString() == "" ? "0" : row["AgentAvailabilityPercent"].ToString());
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public List<ReportAgentLeadEntity> GetAgentLeadReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string Agents, string WeekDays, int AnsweredInterval, float WaitingTimeInterval)
        {
            try
            {
                List<ReportAgentLeadEntity> ReportEntityList = new List<ReportAgentLeadEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_AgentLeadReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Agents", Agents));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                cmd.Parameters.Add(new MySqlParameter("@AnswerInterval", AnsweredInterval));
                cmd.Parameters.Add(new MySqlParameter("@AvailableInterval", WaitingTimeInterval));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportAgentLeadEntity ReportEntityObj = new ReportAgentLeadEntity();
                    //ReportEntityObj.boardID = Convert.ToInt32(row["boardID"].ToString() == "" ? "0" : row["boardID"].ToString());
                   // ReportEntityObj.Title = row["Title"].ToString();
                    ReportEntityObj.AgentName = row["AgentName"].ToString();
                    DateTime dt1 = DateTime.ParseExact(row["StartTime"].ToString(), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    CultureInfo info = new CultureInfo(System.Globalization.CultureInfo.CurrentUICulture.Name);
                    info.DateTimeFormat.DateSeparator = "-";
                    info.DateTimeFormat.TimeSeparator = ":";
                    string datedisplay = dt1.ToString(info);
                    ReportEntityObj.StartTime = datedisplay;
                    // ReportEntityObj.StartTime = row["StartTime"].ToString();
                    ReportEntityObj.AvailableTime = Convert.ToInt32(row["AvailableTime"].ToString() == "" ? "0" : row["AvailableTime"].ToString());
                    ReportEntityObj.CallsHandled = Convert.ToInt32(row["CallsHandled"].ToString() == "" ? "0" : row["CallsHandled"].ToString());

                    ReportEntityObj.CallsServed = Convert.ToInt32(row["CallsServed"].ToString() == "" ? "0" : row["CallsServed"].ToString());
                    ReportEntityObj.TotalAnsweredWithIn = Convert.ToInt32(row["TotalAnsweredWithIn"].ToString() == "" ? "0" : row["TotalAnsweredWithIn"].ToString());
                    ReportEntityObj.AnsweredWithInPercent = float.Parse(row["AnsweredWithInPercent"].ToString() == "" ? "0" : row["AnsweredWithInPercent"].ToString());
                    ReportEntityObj.TotalTime = Convert.ToInt32(row["TotalTime"].ToString() == "" ? "0" : row["TotalTime"].ToString());
                    //ReportEntityObj.AnsweredWithInPercent = float.Parse(row["AnsweredWithInPercent"].ToString() == "" ? "0" : row["AnsweredWithInPercent"].ToString());
                    ReportEntityObj.CallAcceptedPercent = float.Parse(row["CallAcceptedPercent"].ToString() == "" ? "0" : row["CallAcceptedPercent"].ToString());
                    ReportEntityObj.AgentAvailabilityPercent = float.Parse(row["AgentAvailabilityPercent"].ToString() == "" ? "0" : row["AgentAvailabilityPercent"].ToString());
                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        // Mazhar -- Supervisor scoring report 44
        public List<ReportSupervisorScoringEntity> GetSupervisorScoringReport(DateTime FromDate, DateTime ToDate, string timeFrom, string timeTo, string Groups, string Agents, string WeekDays,int AnsweredInterval , int WaitingTimeInterval)
        {
            try
            {
                List<ReportSupervisorScoringEntity> ReportEntityList = new List<ReportSupervisorScoringEntity>();
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("sp_ACD_SupervisorScoringReport", con);
                cmd.Parameters.Add(new MySqlParameter("@SDate", FromDate));
                cmd.Parameters.Add(new MySqlParameter("@EDate", ToDate));
                cmd.Parameters.Add(new MySqlParameter("@STime", timeFrom));
                cmd.Parameters.Add(new MySqlParameter("@ETime", timeTo));
                cmd.Parameters.Add(new MySqlParameter("@Groups", Groups));
                cmd.Parameters.Add(new MySqlParameter("@AnsweredInterval", AnsweredInterval));
                cmd.Parameters.Add(new MySqlParameter("@WaitingTimeInterval", WaitingTimeInterval));
                cmd.Parameters.Add(new MySqlParameter("@WeekDays", WeekDays));
                //cmd.Parameters.Add(new MySqlParameter("@DurationOption", DurationOption));
                //cmd.Parameters.Add(new MySqlParameter("@DurationVal", DurationVal));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                con.Open();

                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportSupervisorScoringEntity ReportEntityObj = new ReportSupervisorScoringEntity();
                    ReportEntityObj.BoardTitle = row["BoardTitle"].ToString();
                    ReportEntityObj.boardid = row["boardid"].ToString();
                    ReportEntityObj.TotalCalls= Convert.ToInt32(row["TotalCalls"].ToString() == "" ? "0" : row["TotalCalls"].ToString());
                    ReportEntityObj.TotalAnsweredCalls = Convert.ToInt32(row["TotalAnsweredCalls"].ToString() == "" ? "0" : row["TotalAnsweredCalls"].ToString());
                    ReportEntityObj.AnsweredPercent = float.Parse(row["AnsweredPercent"].ToString() == "" ? "0" : row["AnsweredPercent"].ToString());

                    ReportEntityObj.AnsweredWithInTotal = Convert.ToInt32(row["AnsweredWithInTotal"].ToString() == "" ? "0" : row["AnsweredWithInTotal"].ToString());
                    ReportEntityObj.WaitingWithInTotal = Convert.ToInt32(row["WaitingWithInTotal"].ToString() == "" ? "0" : row["WaitingWithInTotal"].ToString());
                    ReportEntityObj.AnsweredWithInPercent = float.Parse(row["AnsweredWithInPercent"].ToString() == "" ? "0" : row["AnsweredWithInPercent"].ToString());
                    ReportEntityObj.WaitingWithInPercent = float.Parse(row["WaitingWithInPercent"].ToString() == "" ? "0" : row["WaitingWithInPercent"].ToString());
                    ReportEntityObj.TotalWait = Convert.ToInt32(row["TotalWait"].ToString() == "" ? "0" : row["TotalWait"].ToString());
                    ReportEntityObj.AvgAbandoned = float.Parse(row["AvgAbandoned"].ToString() == "" ? "0" : row["AvgAbandoned"].ToString());

                    ReportEntityList.Add(ReportEntityObj);
                }

                return ReportEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<ReportsEntity> GetAllReports()
        {
            try
            {
                List<ReportsEntity> ReportsEntityList = new List<ReportsEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("Select* From Reports order by VisibleIndex", con);
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportsEntity ReportEntityObj = new ReportsEntity();
                    ReportEntityObj.RID = ConvertStringToInteger(row["RID"].ToString());
                    ReportEntityObj.ReportName = row["ReportName"].ToString();
                    ReportsEntityList.Add(ReportEntityObj);
                }
                
                return ReportsEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return null;
            }
        }
        public List<ReportsEntity> GetReportsByType(string ReportType)
        {
            try
            {
                List<ReportsEntity> ReportsEntityList = new List<ReportsEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();

                // this query is't working
                //cmd = new MySqlCommand("Select r.RID,r.VisibleIndex,r.ReportName,Count(rs.ReportId) as ScheduleCount From Reports r left join reportschedules rs on r.RID=rs.ReportId where r.ReportType=@ReportType and r.Visible=@Visible group by r.ReportName order by r.VisibleIndex asc", con);


               
               if (ReportType== "")
                {
                    cmd = new MySqlCommand("Select r.RID,r.VisibleIndex,r.ReportName,Count(rs.ReportId) as ScheduleCount From Reports r left join reportschedules rs on r.RID=rs.ReportId where  r.RID in(" + System.Web.Configuration.WebConfigurationManager.AppSettings["ReportIds"] + ") group by r.ReportName order by r.VisibleIndex asc", con);

                }
                else
                {
                    cmd = new MySqlCommand("Select r.RID,r.VisibleIndex,r.ReportName,Count(rs.ReportId) as ScheduleCount From Reports r left join reportschedules rs on r.RID=rs.ReportId where r.ReportType  =@ReportType and r.Visible=@Visible group by r.ReportName order by r.VisibleIndex asc", con);

                    cmd.Parameters.Add(new MySqlParameter("@ReportType", ReportType));
                    cmd.Parameters.Add(new MySqlParameter("@Visible", true));

                }
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportsEntity ReportEntityObj = new ReportsEntity();
                    ReportEntityObj.RID = ConvertStringToInteger(row["RID"].ToString());
                    ReportEntityObj.ReportName = row["ReportName"].ToString();
                    ReportEntityObj.ScheduleCount = row["ScheduleCount"].ToString();
                    ReportsEntityList.Add(ReportEntityObj);
                }

                return ReportsEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return null;
            }
        }
        public List<ReportsEntity> GetACDReports()
        {
            try
            {
                List<ReportsEntity> ReportsEntityList = new List<ReportsEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("Select* From Reports where ReportType='ACD' order by VisibleIndex", con);
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportsEntity ReportEntityObj = new ReportsEntity();
                    ReportEntityObj.RID = ConvertStringToInteger(row["RID"].ToString());
                    ReportEntityObj.ReportName = row["ReportName"].ToString();
                    ReportsEntityList.Add(ReportEntityObj);
                }

                return ReportsEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return null;
            }
        }
        public List<ReportsEntity> GetLoggingReports(string ReportType)
        {
            try
            {
                List<ReportsEntity> ReportsEntityList = new List<ReportsEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("Select* From Reports  where ReportType='Logging' order by VisibleIndex", con);
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportsEntity ReportEntityObj = new ReportsEntity();
                    ReportEntityObj.RID = ConvertStringToInteger(row["RID"].ToString());
                    ReportEntityObj.ReportName = row["ReportName"].ToString();
                    ReportsEntityList.Add(ReportEntityObj);
                }

                return ReportsEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return null;
            }
        }
        public string GetReportNameOnReportId(int RID)
        {
            try
            {
                List<ReportCallsSummaryEntity> ReportEntityList = new List<ReportCallsSummaryEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("Select ReportName From reports where RID=@RID", con);
                cmd.Parameters.Add(new MySqlParameter("@RID", RID));
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                string ReportName="";
                foreach (DataRow row in dt.Rows)
                {
                    ReportName = row["ReportName"].ToString();
                }

                return ReportName+" ";
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public string GetHourFormatString(string Hour)
        {
            string HourString = "";
            if (Convert.ToInt32(Hour) < 23)
            {
                HourString = Hour + ":00-" + (Convert.ToInt32(Hour) + 1).ToString() + ":00";
            }
            else
            {
                HourString = Hour + ":00-" +"00:00";
            }
            return HourString;
        }

        public string GetCompanyLogo()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd = new MySqlCommand("Select CompanyLogo From settings  LIMIT 1", con);
            con.Open();
            string logo = (string)cmd.ExecuteScalar();
            con.Close();
            return logo;
        }

        public SupervisorScoringReportSettingParms GetSupervisorReportSettingParm()
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            cmd = new MySqlCommand("Select * From Settings", con);
            da.SelectCommand = cmd;
            con.Open();
            da.Fill(dt);
            con.Close();

            SupervisorScoringReportSettingParms ReportSettingparm = new SupervisorScoringReportSettingParms();
            foreach (DataRow row in dt.Rows)
            {
                ReportSettingparm.QCallAnsweredInNSec = ConvertStringToInteger(row["QCallAnsweredInNSec"].ToString());
                ReportSettingparm.QCallAnsweredInNSecScore = ConvertStringToInteger(row["QCallAnsweredInNSecScore"].ToString());
                ReportSettingparm.QWaitingTimeScore = ConvertStringToInteger(row["QWaitingTimeScore"].ToString());
                ReportSettingparm.QCallsAnsweredScore = ConvertStringToInteger(row["QCallsAnsweredScore"].ToString());
                ReportSettingparm.QWaitingTimeInNSec = ConvertStringToInteger(row["QWaitingTimeInNSec"].ToString());

            }

            return ReportSettingparm;
        }

        public AgentScoringReportSettingParms GetAgentReportSettingParm()
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            cmd = new MySqlCommand("Select * From Settings", con);
            da.SelectCommand = cmd;
            con.Open();
            da.Fill(dt);
            con.Close();

            AgentScoringReportSettingParms ReportSettingparm = new AgentScoringReportSettingParms();
            foreach (DataRow row in dt.Rows)
            {
                ReportSettingparm.AgentCallAnsweredInNSec = ConvertStringToInteger(row["AgentCallsAnsweredWithInNSec"].ToString());
                ReportSettingparm.AgentCallAnsweredInNSecScore = ConvertStringToInteger(row["AgentCallAnsweredInNSecScore"].ToString());
                ReportSettingparm.AgentWaitingTimeScore = ConvertStringToInteger(row["AgentCallsAnsweredScore"].ToString());
                ReportSettingparm.AgentCallsAnsweredScore = ConvertStringToInteger(row["AgentCallsAnsweredScore"].ToString());
                ReportSettingparm.AgentWaitingTimeInNSec = ConvertStringToInteger(row["AgentCallsAnsweredWithInNSec"].ToString());
                ReportSettingparm.AgentAvaliablityHours = ConvertStringToInteger(row["AgentAvalibalityHours"].ToString());
                ReportSettingparm.AgentAvaiabilityScore = ConvertStringToInteger(row["AgentAvaiabilityScore"].ToString());

            }

            return ReportSettingparm;
        }

    }
}