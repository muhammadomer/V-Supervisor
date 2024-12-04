using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using LogApp;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using vSuperMTClient.Entities;

namespace vSuperMTClient.DALs
{
    public class SettingsDAL
    {
        MySqlConnection con;
        public SettingsDAL(string ClientDB)
        {
            string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
            //ClientDB = "vboardmtclient_29";
            con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB));
        }
        public SettingsEntity GetSettings()
        {
            try
            {
                SettingsEntity SettingsObj = new SettingsEntity();
                MySqlCommand cmd = new MySqlCommand("Select *From settings", con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    SettingsObj.SettingID = Convert.ToInt32(row["SettingID"].ToString());
                    SettingsObj.InternalLength = Convert.ToInt32(row["InternalLength"].ToString() != null ? row["InternalLength"].ToString() : "3");
                    SettingsObj.SMTPHost = row["SMTPHost"].ToString();
                    SettingsObj.SMTPPort = row["SMTPPort"].ToString();
                    SettingsObj.SMTPUserName = row["SMTPUserName"].ToString();
                    SettingsObj.SMTPPassword = row["SMTPPassword"].ToString();
                    SettingsObj.EnableSSL = Convert.ToBoolean(row["EnableSSL"]);
                    SettingsObj.AgentWorkingHours = Convert.ToInt32(row["AgentWorkingHours"] != null ? row["AgentWorkingHours"].ToString() : "7");
                    SettingsObj.AgentHangUpThreshold = Convert.ToInt32(row["AgentHangUpThreshold"].ToString() != null ? row["AgentHangUpThreshold"].ToString() : "10");
                    SettingsObj.RingTimeThreshold = Convert.ToInt32(row["RingTimeThreshold"].ToString() != null ? row["RingTimeThreshold"].ToString() : "7");
                    SettingsObj.Currency = row["Currency"].ToString();
                    SettingsObj.DiskCostPerMin = float.Parse(row["DiskCostPerMin"] != null ? row["DiskCostPerMin"].ToString() : "7");
                    SettingsObj.DurationCostPerMin = float.Parse(row["DurationCostPerMin"].ToString() != null ? row["DurationCostPerMin"].ToString() : "10");
                    SettingsObj.CompanyName = row["CompanyName"].ToString();
                    SettingsObj.CompanyAddress = row["CompanyAddress"].ToString();
                    SettingsObj.CompanyLogo = row["CompanyLogo"].ToString();
                    SettingsObj.AgentAvalibalityHours = float.Parse(row["AgentAvalibalityHours"].ToString() != null ? row["AgentAvalibalityHours"].ToString() : "0");
                    SettingsObj.AgentCallsAnsweredWithInNSec = int.Parse(row["AgentCallsAnsweredWithInNSec"].ToString() != null ? row["AgentCallsAnsweredWithInNSec"].ToString() : "0");
                    SettingsObj.QCallsAnsweredWithInNSec = int.Parse(row["QCallAnsweredInNSec"].ToString() != null ? row["QCallAnsweredInNSec"].ToString() : "0");

                    SettingsObj.AgentAvaiabilityScore = int.Parse(row["AgentAvaiabilityScore"].ToString() != null ? row["AgentAvaiabilityScore"].ToString() : "0");
                    SettingsObj.AgentCallAnsweredInNSecScore = int.Parse(row["AgentCallAnsweredInNSecScore"].ToString() != null ? row["AgentCallAnsweredInNSecScore"].ToString() : "0");
                    SettingsObj.AgentCallsAnsweredScore = int.Parse(row["AgentCallsAnsweredScore"].ToString() != null ? row["AgentCallsAnsweredScore"].ToString() : "0");
                    SettingsObj.QCallAnsweredInNSecScore = int.Parse(row["QCallAnsweredInNSecScore"].ToString() != null ? row["QCallAnsweredInNSecScore"].ToString() : "0");
                    SettingsObj.QWaitingTimeInNSec = int.Parse(row["QWaitingTimeInNSec"].ToString() != null ? row["QWaitingTimeInNSec"].ToString() : "0");
                    SettingsObj.QWaitingTimeScore = int.Parse(row["QWaitingTimeScore"].ToString() != null ? row["QWaitingTimeScore"].ToString() : "0");
                    SettingsObj.QCallsAnsweredScore = int.Parse(row["QCallsAnsweredScore"].ToString() != null ? row["QCallsAnsweredScore"].ToString() : "0");
                }
               
                con.Close();
                return SettingsObj;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return null;
            }
        }

        public bool GetSecondaryOutcomeBit()
        {
            try
            {
               // string vAcdClientDB = HttpContext.Current.Session["vAcdDB"].ToString();
              //  string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
              //  MySqlConnection con2 = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, vAcdClientDB));

                MySqlCommand cmd = new MySqlCommand("Select SecondaryOutcomeEnable From settings", con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                bool SecondaryOutcomeEnable = false;
                foreach (DataRow row in dt.Rows)
                {
                    SecondaryOutcomeEnable = Convert.ToBoolean(row["SecondaryOutcomeEnable"]);
                }

                con.Close();
                return SecondaryOutcomeEnable;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return false;
            }
        }

        public static Hashtable GetUnavailableReasons()
        {
            Hashtable tablereasons = new Hashtable();
            try
            {
                string vAcdClientDB = HttpContext.Current.Session["vAcdDB"].ToString();
                string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
                MySqlConnection con2 = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, vAcdClientDB));

                MySqlCommand cmd = new MySqlCommand("Select Code,Description From agent_reasons", con2);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                con2.Open();
                da.Fill(dt);
                con2.Close();

                foreach (DataRow row in dt.Rows)
                {

                    string Code = row["Code"].ToString();
                    string Description = row["Description"].ToString().Trim().ToLower();
                    if (!tablereasons.ContainsKey(Description))
                    {
                        tablereasons.Add(Description, Code);
                    }
                }

            }
            catch (Exception ex)
            {
               
                Log4Net.WriteException(ex);
            }
            return tablereasons;
        }
        public static List<csPrimaryOutcomeData> GetPrimaryOutcomes()
        {
            List<csPrimaryOutcomeData> list = new List<csPrimaryOutcomeData>();
            try
            {
                string vAcdClientDB = HttpContext.Current.Session["vAcdDB"].ToString();
                string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
                MySqlConnection con2 = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, vAcdClientDB));

                MySqlCommand cmd = new MySqlCommand("SELECT * from primary_outcome;", con2);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dtable = new DataTable();
                da.SelectCommand = cmd;
                con2.Open();
                da.Fill(dtable);
                con2.Close();



                for (int i = 0; i < dtable.Rows.Count; i++)
                {
                    try
                    {
                        int Id = dtable.Rows[i]["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dtable.Rows[i]["Id"]);
                        string Code = dtable.Rows[i]["Code"] == DBNull.Value ? string.Empty : Convert.ToString(dtable.Rows[i]["Code"]);
                        string Title = dtable.Rows[i]["Title"] == DBNull.Value ? string.Empty : Convert.ToString(dtable.Rows[i]["Title"]);
                        csPrimaryOutcomeData data = new csPrimaryOutcomeData();
                        data.Id = Id;
                        data.Code = Code;
                        data.Title = Title;
                        data.SecondaryOutComes = GetSecondaryOutcomes( Id);
                        list.Add(data);
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
            catch (Exception E)
            {
                LogApp.Log4Net.WriteException(E);
            }

            return list;
        }

        public static List<csSecondaryOutcomeData> GetSecondaryOutcomes(int id)
        {
            List<csSecondaryOutcomeData> list = new List<csSecondaryOutcomeData>();
            try
            {
                string vAcdClientDB = HttpContext.Current.Session["vAcdDB"].ToString();
                string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
                MySqlConnection con2 = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, vAcdClientDB));

                MySqlCommand cmd = new MySqlCommand("SELECT * from secondary_outcome where PrimaryOutcomeId=" + id + ";", con2);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dtable = new DataTable();
                da.SelectCommand = cmd;
                con2.Open();
                da.Fill(dtable);
                con2.Close();

                for (int i = 0; i < dtable.Rows.Count; i++)
                {
                    try
                    {
                        int Id = dtable.Rows[i]["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dtable.Rows[i]["Id"]);
                        string Code = dtable.Rows[i]["Code"] == DBNull.Value ? string.Empty : Convert.ToString(dtable.Rows[i]["Code"]);
                        string Title = dtable.Rows[i]["Title"] == DBNull.Value ? string.Empty : Convert.ToString(dtable.Rows[i]["Title"]);
                        csSecondaryOutcomeData data = new csSecondaryOutcomeData();
                        data.Id = Id;
                        data.Code = Code;
                        data.Title = Title;
                        list.Add(data);
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
            catch (Exception E)
            {
                LogApp.Log4Net.WriteException(E);
            }

            return list;
        }

        public bool UpdateSMTPSettings(SettingsEntity SettingsObj)
        { 
            try
            {
                string query = "Update settings SET SMTPHost=@SMTPHost,SMTPPort=@SMTPPort,SMTPUserName=@SMTPUserName,SMTPPassword=@SMTPPassword,EnableSSL=@EnableSSL";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SMTPHost", SettingsObj.SMTPHost == null ? (object)DBNull.Value : SettingsObj.SMTPHost.Trim());
                cmd.Parameters.AddWithValue("@SMTPPort", SettingsObj.SMTPPort);
                cmd.Parameters.AddWithValue("@SMTPUserName", SettingsObj.SMTPUserName == null ? (object)DBNull.Value : SettingsObj.SMTPUserName.Trim());
                cmd.Parameters.AddWithValue("@SMTPPassword", SettingsObj.SMTPPassword == null ? (object)DBNull.Value : SettingsObj.SMTPPassword.Trim());
                cmd.Parameters.AddWithValue("@EnableSSL", SettingsObj.EnableSSL);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return false;

            }
        }
        public bool UpdateCompanyInfoSettings(SettingsEntity SettingsObj)
        {
            try
            {
                string imgPath = SettingsObj.CompanyLogo;
                if (SettingsObj.CompanyLogo.StartsWith("data:"))
                {
                     imgPath = SaveImage(SettingsObj.CompanyLogo, SettingsObj.CompanyName);
                }

                string query = "Update settings SET CompanyName=@CompanyName,CompanyAddress=@CompanyAddress,CompanyLogo=@CompanyLogo";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CompanyName", SettingsObj.CompanyName);
                cmd.Parameters.AddWithValue("@CompanyAddress", SettingsObj.CompanyAddress);
                cmd.Parameters.AddWithValue("@CompanyLogo", imgPath);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return false;
            }
        }
        public bool UpdatePrefrences(SettingsEntity SettingsObj)
        {
            try
            {
                string query = "Update settings SET AgentWorkingHours=@AgentWorkingHours,AgentHangUpThreshold=@AgentHangUpThreshold," +
                    "InternalLength=@InternalLength,RingTimeThreshold=@RingTimeThreshold,Currency=@Currency"
                   ;
                MySqlCommand cmd = new MySqlCommand(query, con);
               // cmd.Parameters.AddWithValue("@PBXIP", SettingsObj.PBXIP);
                cmd.Parameters.AddWithValue("@AgentWorkingHours", SettingsObj.AgentWorkingHours);
                cmd.Parameters.AddWithValue("@AgentHangUpThreshold", SettingsObj.AgentHangUpThreshold);
                cmd.Parameters.AddWithValue("@InternalLength", SettingsObj.InternalLength);
                cmd.Parameters.AddWithValue("@RingTimeThreshold", SettingsObj.RingTimeThreshold);
                cmd.Parameters.AddWithValue("@Currency", SettingsObj.Currency);
                //cmd.Parameters.AddWithValue("@AgentAvalibalityHours", SettingsObj.AgentAvalibalityHours);
                //cmd.Parameters.AddWithValue("@AgentCallsAnsweredWithInNSec", SettingsObj.AgentCallsAnsweredWithInNSec);
                //cmd.Parameters.AddWithValue("@QCallAnsweredInNSec", SettingsObj.QCallsAnsweredWithInNSec);

                //cmd.Parameters.AddWithValue("@AgentAvaiabilityScore", SettingsObj.AgentAvaiabilityScore);
                //cmd.Parameters.AddWithValue("@AgentCallAnsweredInNSecScore", SettingsObj.AgentCallAnsweredInNSecScore);
                //cmd.Parameters.AddWithValue("@AgentCallsAnsweredScore", SettingsObj.AgentCallsAnsweredScore);
                //cmd.Parameters.AddWithValue("@QCallAnsweredInNSecScore", SettingsObj.QCallAnsweredInNSecScore);
                //cmd.Parameters.AddWithValue("@QWaitingTimeInNSec", SettingsObj.QWaitingTimeInNSec);
                //cmd.Parameters.AddWithValue("@QWaitingTimeScore", SettingsObj.QWaitingTimeScore);
                //cmd.Parameters.AddWithValue("@QCallsAnsweredScore", SettingsObj.QCallsAnsweredScore);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);


                return false;

            }
        }
        public bool UpdateReportOptions(SettingsEntity SettingsObj)
        {
            try
            {
                string query = "Update settings SET " +
                    //"AgentWorkingHours=@AgentWorkingHours,AgentHangUpThreshold=@AgentHangUpThreshold," +
                    //"InternalLength=@InternalLength,RingTimeThreshold=@RingTimeThreshold,Currency=@Currency," +
                    "AgentAvalibalityHours=@AgentAvalibalityHours,AgentCallsAnsweredWithInNSec=@AgentCallsAnsweredWithInNSec, QCallAnsweredInNSec =@QCallAnsweredInNSec," +
                    " AgentAvaiabilityScore=@AgentAvaiabilityScore, AgentCallAnsweredInNSecScore=@AgentCallAnsweredInNSecScore, AgentCallsAnsweredScore=@AgentCallsAnsweredScore, QCallAnsweredInNSecScore=@QCallAnsweredInNSecScore," +
                    " QWaitingTimeInNSec=@QWaitingTimeInNSec, QWaitingTimeScore=@QWaitingTimeScore, QCallsAnsweredScore=@QCallsAnsweredScore;";
                MySqlCommand cmd = new MySqlCommand(query, con);
                //cmd.Parameters.AddWithValue("@AgentWorkingHours", SettingsObj.AgentWorkingHours);
                //cmd.Parameters.AddWithValue("@AgentHangUpThreshold", SettingsObj.AgentHangUpThreshold);
                //cmd.Parameters.AddWithValue("@InternalLength", SettingsObj.InternalLength);
                //cmd.Parameters.AddWithValue("@RingTimeThreshold", SettingsObj.RingTimeThreshold);
                //cmd.Parameters.AddWithValue("@Currency", SettingsObj.Currency);
                cmd.Parameters.AddWithValue("@AgentAvalibalityHours", SettingsObj.AgentAvalibalityHours);
                cmd.Parameters.AddWithValue("@AgentCallsAnsweredWithInNSec", SettingsObj.AgentCallsAnsweredWithInNSec);
                cmd.Parameters.AddWithValue("@QCallAnsweredInNSec", SettingsObj.QCallsAnsweredWithInNSec);

                cmd.Parameters.AddWithValue("@AgentAvaiabilityScore", SettingsObj.AgentAvaiabilityScore);
                cmd.Parameters.AddWithValue("@AgentCallAnsweredInNSecScore", SettingsObj.AgentCallAnsweredInNSecScore);
                cmd.Parameters.AddWithValue("@AgentCallsAnsweredScore", SettingsObj.AgentCallsAnsweredScore);
                cmd.Parameters.AddWithValue("@QCallAnsweredInNSecScore", SettingsObj.QCallAnsweredInNSecScore);
                cmd.Parameters.AddWithValue("@QWaitingTimeInNSec", SettingsObj.QWaitingTimeInNSec);
                cmd.Parameters.AddWithValue("@QWaitingTimeScore", SettingsObj.QWaitingTimeScore);
                cmd.Parameters.AddWithValue("@QCallsAnsweredScore", SettingsObj.QCallsAnsweredScore);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);


                return false;

            }
        }

        public AgentDisplayEntity GetAgentDisplay(int UserId)
        {

            AgentDisplayEntity agentDisplayObj = new AgentDisplayEntity();

            try
            {
              
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                MySqlCommand cmd = new MySqlCommand("Select * From AgentDisplay where userid="+UserId, con);
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);



                
                foreach (DataRow row in dt.Rows)
                {
                    agentDisplayObj.SortBy = row["SortBy"].ToString();
                    agentDisplayObj.NumberOfAgents= row["NumberOfAgents"].ToString();
                    agentDisplayObj.DisplayAvatar = row["DisplayAvatar"].ToString();

                    //string tags = "9,3,12,43,2"

                    string queueList = row["queuelist"].ToString();
                    string agentState = row["agentState"].ToString();

                    agentDisplayObj.QueueList=queueList.Split(',').ToList();
                    agentDisplayObj.AgentState = agentState.Split(',').ToList();


                    // List<int> TagIds = tags.Split(',').Select(int.Parse).ToList();

                }

                //return agentDisplayObj;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
               // return 0;

            }
            return agentDisplayObj;

        }

        public bool UpdateAgentDisplay(AgentDisplayEntity agentDisplayObj)
        {

            try
            {



                string queslist = String.Join(",", agentDisplayObj.QueueList.ToArray<string>());
                string agentState = String.Join(",", agentDisplayObj.AgentState.ToArray<string>());               
               
                               
                MySqlDataAdapter da = new MySqlDataAdapter();
                MySqlCommand cmd1 = new MySqlCommand("Select * From AgentDisplay where userid=" + agentDisplayObj.UserId, con);
                DataTable dt = new DataTable();
                string query = "";
                MySqlCommand cmd = new MySqlCommand(query, con);

                da.SelectCommand = cmd1;
                con.Open();
                da.Fill(dt);



                if (dt.Rows.Count == 0)
                {
                    query = "insert into agentDisplay " +
                       "(SortBy,QueueList,AgentState,NumberOfAgents," +
                                      "DisplayAvatar,UserId)" +
                                      "values" +
                                      "('" + agentDisplayObj.SortBy + "','" + queslist + "','" + agentState + "','" + agentDisplayObj.NumberOfAgents + "','" + agentDisplayObj.DisplayAvatar + "'," + agentDisplayObj.UserId + ")";

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                }
                else
                {
                    query = "Update agentDisplay SET SortBy=@SortBy,QueueList=@QueueList,AgentState=@AgentState,NumberOfAgents=@NumberOfAgents," +
                                       "DisplayAvatar=@DisplayAvatar where UserId=@UserId;";

                    cmd.Parameters.AddWithValue("@SortBy", agentDisplayObj.SortBy);
                    cmd.Parameters.AddWithValue("@QueueList", queslist);
                    cmd.Parameters.AddWithValue("@AgentState", agentState);
                    cmd.Parameters.AddWithValue("@NumberOfAgents", agentDisplayObj.NumberOfAgents);
                    cmd.Parameters.AddWithValue("@DisplayAvatar", agentDisplayObj.DisplayAvatar);
                    cmd.Parameters.AddWithValue("@UserId", agentDisplayObj.UserId);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;


                }








                //  con.Open();
                cmd.ExecuteNonQuery();
                con.Close();



                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return false;

                //}

            }


        }
            public bool UpdateRecordingRates(SettingsEntity SettingsObj)
        {
            try
            {
                string query = "Update settings SET DiskCostPerMin=@DiskCostPerMin,DurationCostPerMin=@DurationCostPerMin";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DiskCostPerMin", SettingsObj.DiskCostPerMin);
                cmd.Parameters.AddWithValue("@DurationCostPerMin", SettingsObj.DurationCostPerMin);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return false;

            }
        }
        public int GetInternalLengthFromSettings()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                MySqlCommand cmd = new MySqlCommand("Select *From settings", con);
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                int  InternalLength = 0;
                foreach (DataRow row in dt.Rows)
                {
                    InternalLength = Convert.ToInt32(row["InternalLength"].ToString());
                }

                return InternalLength;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return 0;

            }
        }
        public int GetRingTimeThresholdFromSettings()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                MySqlCommand cmd = new MySqlCommand("Select *From settings", con);
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                int RingTimeThreshold = 0;
                foreach (DataRow row in dt.Rows)
                {
                    RingTimeThreshold = Convert.ToInt32(row["RingTimeThreshold"].ToString());
                }

                return RingTimeThreshold;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return 0;

            }
        }
        public bool CheckIfSMTPCredentialsAreProvided()
        {
            try
            {
                SettingsEntity SettingsObj = new SettingsEntity();
                MySqlCommand cmd = new MySqlCommand("Select *From settings", con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    SettingsObj.SMTPHost = row["SMTPHost"].ToString();
                    SettingsObj.SMTPPort = row["SMTPPort"].ToString();
                    SettingsObj.SMTPUserName = row["SMTPUserName"].ToString();
                    SettingsObj.SMTPPassword = row["SMTPPassword"].ToString();
                }
                con.Close();

                if (SettingsObj.SMTPHost != "" && SettingsObj.SMTPPort != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return false;
            }
        }
        public string GetRecordigPath(string vCloudDB)
        {
            try
            {
                SettingsEntity SettingsObj = new SettingsEntity();
                MySqlCommand cmd = new MySqlCommand("Select Recording_Path from " + vCloudDB + ".app_setting", con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                string Recording_Path = "";
                foreach (DataRow row in dt.Rows)
                {
                     Recording_Path = row["Recording_Path"].ToString();
                   
                }
                return Recording_Path;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return "";
            }
            
        }
        public bool UpdatePBXIPInvBorad(SettingsEntity SettingsUpdatedInfo)
        {
            try
            {

                string query = "Update settings SET pbxIP=@pbxIP";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@pbxIP", SettingsUpdatedInfo.PBXIP);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return false;
            }
        }
        public SettingsEntity GetSettingsFromvBoard()
        {
            try
            {
                SettingsEntity SettingsObj = new SettingsEntity();
                MySqlCommand cmd = new MySqlCommand("Select pbxIP From settings", con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    SettingsObj.PBXIP = row["pbxIP"].ToString();
                }

                con.Close();
                return SettingsObj;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return null;
            }
        }

        //public string SaveImage(string ImgStr, string ImgName)
        //{

        //    ImgStr = ImgStr.Replace('-', '+');
        //    ImgStr = ImgStr.Replace('_', '/');
        //    string NewImgStr = ImgStr.Replace(ImgStr.Substring(0, ImgStr.IndexOf(',') + 1), "");
        //    String path = HttpContext.Current.Server.MapPath("~/Content/Images/"); //Path

        //    //Check if directory exist
        //    if (!System.IO.Directory.Exists(path))
        //    {
        //        System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
        //    }

        //    string imageName = ImgName + "_logo.jpg";

        //    //set the image path
        //    string imgPath = Path.Combine(path, imageName);

        //    byte[] imageBytes = Convert.FromBase64String(NewImgStr);

        //    File.WriteAllBytes(imgPath, imageBytes);

        //    return "Content/Images/"+ imageName;
        //}

        public string SaveImage(string ImgStr, string ImgName)
        {

            ImgStr = ImgStr.Replace('-', '+');
            ImgStr = ImgStr.Replace('_', '/');
            string NewImgStr = ImgStr.Replace(ImgStr.Substring(0, ImgStr.IndexOf(',') + 1), "");
            String path = HttpContext.Current.Server.MapPath("~/Content/Images/"); //Path

            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            string imageName = ImgName + "_logo.jpg";

            //set the image path
            string imgPath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(NewImgStr);
            //File.WriteAllBytes(imgPath, imageBytes);

            ////////////////////////////////////
            Image image = Image.FromStream(new System.IO.MemoryStream(imageBytes));
            float h = image.Height;
            float w = image.Width;
            float _widthToRemove = w;

            float heightToFix = 100;
            if (h > 100)
            {                
                float _perExtra = (heightToFix / h) * 100;
                float perToRemove = 100 - _perExtra;

                _widthToRemove = (perToRemove * w) / 100;
                w = w - _widthToRemove;
            }

            Bitmap bmp;
            using (var ms = new MemoryStream(imageBytes))
            {
                bmp = new Bitmap(ms);
                Bitmap resized = new Bitmap(bmp, new Size(Convert.ToInt32(w), Convert.ToInt32(heightToFix)));
                resized.Save(imgPath);
            }

            //bmp.Save(imgPath);

            return "Content/Images/" + imageName;
        }
    }
}