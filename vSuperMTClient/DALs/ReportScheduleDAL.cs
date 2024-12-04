using LogApp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using vSuperMTClient.Entities;
namespace vSuperMTClient.DALs
{
    public class ReportScheduleDAL
    {
        
        MySqlConnection con;
        public ReportScheduleDAL(string ClientDB)
        {
            string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
            con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB));
        }
        public List<ReportScheduleEntity> GetAllSchedules()
        {
            try
            {
              
                List<ReportScheduleEntity> ReportScheduleEntityList = new List<ReportScheduleEntity>();
                DataTable dt = new DataTable();
                //string query = "SELECT rs.Id,rs.Name, rs.DateFilterCriteria,rs.Extensions,rs.Boards,rs.Agents,rs.ScheduleInterval,rs.ScheduleValue,rs.ScheduleTimeHours,rs.ScheduleTimeMinutes,rs.ExecutionTime,rs.DateCreated,rs.DateFrom,rs.DateTo,rs.TimeFrom,rs.TimeTo,r.ReportName,GROUP_CONCAT(Title SEPARATOR ', ') as BoardNames FROM reportschedules rs inner join reports r on rs.ReportId=r.RID inner join scheduleboards sb on sb.ScheduleId=rs.Id inner join boards b on b.BoardID = sb.BoardId group by rs.Id";
                string query = "SELECT rs.Id,rs.Name, rs.DateFilterCriteria,rs.Extensions,rs.Boards,rs.Agents,rs.ScheduleInterval,rs.ScheduleValue,rs.ScheduleTimeHours,rs.ScheduleTimeMinutes,rs.ExecutionTime,rs.DateCreated,rs.DateFrom,rs.DateTo,rs.TimeFrom,rs.TimeTo,r.ReportName FROM reportschedules rs inner join reports r on rs.ReportId=r.RID group by rs.Id";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                con.Close();
                
                foreach (DataRow row in dt.Rows)
                {
                    ReportScheduleEntity ReportScheduleEntityObj = new ReportScheduleEntity();
                    ReportScheduleEntityObj.Id = Convert.ToInt32(row["Id"].ToString());
                    ReportScheduleEntityObj.Name = row["Name"].ToString();
                    //ReportScheduleEntityObj.ReportId = Convert.ToInt32(row["ReportId"].ToString());
                    ReportScheduleEntityObj.DateFilterCriteria = Convert.ToInt32(row["DateFilterCriteria"].ToString());
                    ReportScheduleEntityObj.DateFrom = Convert.ToDateTime(String.IsNullOrEmpty(row["DateFrom"].ToString()) ? DateTime.Now.ToString() : row["DateFrom"].ToString());
                    ReportScheduleEntityObj.DateTo = Convert.ToDateTime(String.IsNullOrEmpty(row["DateTo"].ToString()) ? DateTime.Now.ToString() : row["DateTo"].ToString());
                    ReportScheduleEntityObj.TimeFrom = row["TimeFrom"].ToString();
                    ReportScheduleEntityObj.TimeTo = row["TimeTo"].ToString();
                    ReportScheduleEntityObj.Extensions = row["Extensions"].ToString();
                    ReportScheduleEntityObj.Boards = row["Boards"].ToString();
                    ReportScheduleEntityObj.Agents = row["Agents"].ToString();
                    ReportScheduleEntityObj.ScheduleInterval = Convert.ToInt32(row["ScheduleInterval"].ToString());
                    ReportScheduleEntityObj.ScheduleValue = row["ScheduleValue"].ToString();
                    ReportScheduleEntityObj.ScheduleTimeHours = row["ScheduleTimeHours"].ToString();
                    ReportScheduleEntityObj.ScheduleTimeMinutes = row["ScheduleTimeMinutes"].ToString();
                    ReportScheduleEntityObj.ExecutionTime = Convert.ToDateTime(String.IsNullOrEmpty(row["ExecutionTime"].ToString()) ? DateTime.Now.ToString() : row["ExecutionTime"].ToString());
                    ReportScheduleEntityObj.DateCreated = Convert.ToDateTime(String.IsNullOrEmpty(row["DateCreated"].ToString()) ? DateTime.Now.ToString() : row["DateCreated"].ToString());
                    //ReportScheduleEntityObj.BoardNames = row["BoardNames"].ToString();
                    ReportScheduleEntityObj.ReportName = row["ReportName"].ToString();
                    string query2 = "select *from reportscheduleemails where ScheduleId=" + ReportScheduleEntityObj.Id + "";
                    MySqlDataAdapter da2 = new MySqlDataAdapter(query2, con);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    string Emails = "";
                    foreach (DataRow row2 in dt2.Rows)
                    {
                        Emails += row2["EmailId"].ToString()+";";
                    }
                    ReportScheduleEntityObj.Emails = Emails;

                    ReportScheduleEntityList.Add(ReportScheduleEntityObj);
                }
               
                return ReportScheduleEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public ReportScheduleEntity GetReportScheduleOnId(int ReportScheduleId,string DBType)
        {
            try
            {
                ReportScheduleEntity ReportScheduleEntityObj = new ReportScheduleEntity();
                DataTable dt = new DataTable();
                string query = "select * from reportschedules where Id=" + ReportScheduleId + "";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    ReportScheduleEntityObj.Id = Convert.ToInt32(row["Id"].ToString());
                    ReportScheduleEntityObj.Name = row["Name"].ToString();
                    ReportScheduleEntityObj.ReportId = Convert.ToInt32(row["ReportId"].ToString());
                    string s=row["DateFrom"].ToString();
                    bool c = String.IsNullOrEmpty(s);
                    string d = DateTime.Now.ToString();
                    DateTime date= Convert.ToDateTime(d);
                    ReportScheduleEntityObj.DateFilterCriteria = Convert.ToInt32(row["DateFilterCriteria"].ToString());
                    ReportScheduleEntityObj.DateFrom = Convert.ToDateTime(String.IsNullOrEmpty(row["DateFrom"].ToString()) ? DateTime.Now.ToString() : row["DateFrom"].ToString());
                    ReportScheduleEntityObj.DateTo = Convert.ToDateTime(String.IsNullOrEmpty(row["DateTo"].ToString()) ? DateTime.Now.ToString() : row["DateTo"].ToString());
                    ReportScheduleEntityObj.TimeFrom = row["TimeFrom"].ToString();
                    ReportScheduleEntityObj.TimeTo = row["TimeTo"].ToString();
                    if (DBType == "vRecord")
                    {
                        ReportScheduleEntityObj.Users = row["Users"].ToString();
                    }
                    else if (DBType == "vBoard")
                    {
                        ReportScheduleEntityObj.Extensions = row["Extensions"].ToString();
                        ReportScheduleEntityObj.Boards = row["Boards"].ToString();
                        ReportScheduleEntityObj.Agents = row["Agents"].ToString();
                        ReportScheduleEntityObj.TimeInterval = Convert.ToInt32(row["TimeInterval"].ToString());
                        ReportScheduleEntityObj.ExternalRouting = row["ExternalRouting"].ToString();
                        ReportScheduleEntityObj.ReportType = row["ReportType"].ToString();
                    }

                    ReportScheduleEntityObj.WeekDays = row["WeekDays"].ToString();
                    ReportScheduleEntityObj.ScheduleInterval = Convert.ToInt32(row["ScheduleInterval"].ToString());
                    ReportScheduleEntityObj.ScheduleValue = row["ScheduleValue"].ToString();
                    ReportScheduleEntityObj.ScheduleTimeHours = row["ScheduleTimeHours"].ToString();
                    ReportScheduleEntityObj.ScheduleTimeMinutes = row["ScheduleTimeMinutes"].ToString();
                    ReportScheduleEntityObj.ExecutionTime = Convert.ToDateTime(row["ExecutionTime"].ToString());
                    ReportScheduleEntityObj.DateCreated = Convert.ToDateTime(row["DateCreated"].ToString());
                }
                con.Close();
                dt = new DataTable();
                string query2 = "select * from reportscheduleemails where ScheduleId=" + ReportScheduleId + "";
                con.Open();
                da = new MySqlDataAdapter(query2, con);
                da.Fill(dt);
                con.Close();
                string Emails = "";
                foreach (DataRow row in dt.Rows)
                {
                    Emails+= row["EmailId"].ToString() + ";";
                }
                ReportScheduleEntityObj.Emails = Emails;
                return ReportScheduleEntityObj;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public List<ReportScheduleEntity> GetScheduleOnReportId(int ReportId, string DBType)
        {
            try
            {

                List<ReportScheduleEntity> ReportScheduleEntityList = new List<ReportScheduleEntity>();
                DataTable dt = new DataTable();

                //string query = "SELECT rs.Id,rs.Name, rs.DateFilterCriteria,rs.Extensions,rs.Boards,rs.Agents,rs.ScheduleInterval,rs.ScheduleValue,rs.ScheduleTimeHours,rs.ScheduleTimeMinutes,rs.ExecutionTime,rs.DateCreated,rs.DateFrom,rs.DateTo,rs.TimeFrom,rs.TimeTo,r.ReportName,GROUP_CONCAT(Title SEPARATOR ', ') as BoardNames FROM reportschedules rs inner join reports r on rs.ReportId=r.RID inner join scheduleboards sb on sb.ScheduleId=rs.Id inner join boards b on b.BoardID = sb.BoardId group by rs.Id";
                string query = "";
                if (DBType == "vRecord")
                {
                    query = "SELECT rs.ReportId,rs.Id,rs.Name, rs.DateFilterCriteria,rs.WeekDays,rs.Users,rs.ScheduleInterval,rs.ScheduleValue,rs.ScheduleTimeHours,rs.ScheduleTimeMinutes,rs.ExecutionTime,rs.DateCreated,rs.DateFrom,rs.DateTo,rs.TimeFrom,rs.TimeTo,r.ReportName FROM reportschedules rs inner join reports r on rs.ReportId=r.RID where rs.ReportId=" + ReportId + " group by rs.Id";

                }
                else if (DBType == "vBoard")
                {
                    query = "SELECT rs.ReportId,rs.Id,rs.Name, rs.DateFilterCriteria, rs.CallsOption,rs.WeekDays,rs.Extensions,rs.Boards,rs.Agents,rs.TimeInterval,rs.ScheduleInterval,rs.ScheduleValue,rs.ScheduleTimeHours,rs.ScheduleTimeMinutes,rs.ExecutionTime,rs.DateCreated,rs.DateFrom,rs.DateTo,rs.TimeFrom,rs.TimeTo,rs.ExternalRouting,rs.ReportType,r.ReportName FROM reportschedules rs inner join reports r on rs.ReportId=r.RID where rs.ReportId=" + ReportId + " group by rs.Id";

                }
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    ReportScheduleEntity ReportScheduleEntityObj = new ReportScheduleEntity();
                    ReportScheduleEntityObj.Id = Convert.ToInt32(row["Id"].ToString());
                    ReportScheduleEntityObj.Name = row["Name"].ToString();
                    ReportScheduleEntityObj.ReportId = Convert.ToInt32(row["ReportId"].ToString());
                    //ReportScheduleEntityObj.ReportId = Convert.ToInt32(row["ReportId"].ToString());
                    ReportScheduleEntityObj.DateFilterCriteria = Convert.ToInt32(row["DateFilterCriteria"].ToString());
                    ReportScheduleEntityObj.DateFrom = Convert.ToDateTime(String.IsNullOrEmpty(row["DateFrom"].ToString()) ? DateTime.Now.ToString() : row["DateFrom"].ToString());
                    ReportScheduleEntityObj.DateTo = Convert.ToDateTime(String.IsNullOrEmpty(row["DateTo"].ToString()) ? DateTime.Now.ToString() : row["DateTo"].ToString());
                    ReportScheduleEntityObj.TimeFrom = row["TimeFrom"].ToString();
                    ReportScheduleEntityObj.TimeTo = row["TimeTo"].ToString();
                    ReportScheduleEntityObj.WeekDays = row["WeekDays"].ToString();
                    
                    if (DBType=="vRecord")
                    {
                        ReportScheduleEntityObj.Users = row["Users"].ToString();
                    }
                    else if (DBType=="vBoard")
                    {
                        ReportScheduleEntityObj.Extensions = row["Extensions"].ToString();
                        ReportScheduleEntityObj.Boards = row["Boards"].ToString();
                        ReportScheduleEntityObj.Agents = row["Agents"].ToString();
                        ReportScheduleEntityObj.TimeInterval = Convert.ToInt32(row["TimeInterval"].ToString());
                        ReportScheduleEntityObj.ExternalRouting = row["ExternalRouting"].ToString();
                        ReportScheduleEntityObj.ReportType = row["ReportType"].ToString();
                        ReportScheduleEntityObj.CallsOption = Convert.ToInt32(row["CallsOption"].ToString());
                    }

                    ReportScheduleEntityObj.ScheduleInterval = Convert.ToInt32(row["ScheduleInterval"].ToString());
                    ReportScheduleEntityObj.ScheduleValue = row["ScheduleValue"].ToString();
                    ReportScheduleEntityObj.ScheduleTimeHours = row["ScheduleTimeHours"].ToString();
                    ReportScheduleEntityObj.ScheduleTimeMinutes = row["ScheduleTimeMinutes"].ToString();
                    ReportScheduleEntityObj.ExecutionTime = Convert.ToDateTime(String.IsNullOrEmpty(row["ExecutionTime"].ToString()) ? DateTime.Now.ToString() : row["ExecutionTime"].ToString());
                    ReportScheduleEntityObj.DateCreated = Convert.ToDateTime(String.IsNullOrEmpty(row["DateCreated"].ToString()) ? DateTime.Now.ToString() : row["DateCreated"].ToString());
                    //ReportScheduleEntityObj.BoardNames = row["BoardNames"].ToString();
                    ReportScheduleEntityObj.ReportName = row["ReportName"].ToString();
                    string query2 = "select *from reportscheduleemails where ScheduleId=" + ReportScheduleEntityObj.Id + "";
                    MySqlDataAdapter da2 = new MySqlDataAdapter(query2, con);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    string Emails = "";
                    foreach (DataRow row2 in dt2.Rows)
                    {
                        Emails += row2["EmailId"].ToString() + ";";
                    }
                    ReportScheduleEntityObj.Emails = Emails;

                    ReportScheduleEntityList.Add(ReportScheduleEntityObj);
                }

                return ReportScheduleEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public bool InsertIntoReportSchedule(ReportScheduleEntity ReportScheduleEntityObj, string DBType)
        {
            try
            {
                string query = "";
                if (DBType == "vBoard")
                {
                    query = "Insert into ReportSchedules (Name,ReportId,DateFilterCriteria,CallsOption,DateFrom,DateTo,TimeFrom,TimeTo,WeekDays,Extensions,Boards,Agents,ExternalRouting,TimeInterval,ScheduleInterval,ScheduleValue,ScheduleTimeHours,ScheduleTimeMinutes,ExecutionTime,DateCreated,ReportType) values(@Name,@ReportId,@DateFilterCriteria,@CallsOption,@DateFrom,@DateTo,@TimeFrom,@TimeTo,@WeekDays,@Extensions,@Boards,@Agents,@ExternalRouting,@TimeInterval,@ScheduleInterval,@ScheduleValue,@ScheduleTimeHours,@ScheduleTimeMinutes,@ExecutionTime,@DateCreated,@ReportType); select LAST_INSERT_ID();";
                }
                else if (DBType == "vRecord")
                {
                    query = "Insert into ReportSchedules (Name,ReportId,DateFilterCriteria,DateFrom,DateTo,TimeFrom,TimeTo,WeekDays,Users,ScheduleInterval,ScheduleValue,ScheduleTimeHours,ScheduleTimeMinutes,ExecutionTime,DateCreated) values(@Name,@ReportId,@DateFilterCriteria,@DateFrom,@DateTo,@TimeFrom,@TimeTo,@WeekDays,@Users,@ScheduleInterval,@ScheduleValue,@ScheduleTimeHours,@ScheduleTimeMinutes,@ExecutionTime,@DateCreated); select LAST_INSERT_ID();";
                }

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", ReportScheduleEntityObj.Name == null ? (object)DBNull.Value : ReportScheduleEntityObj.Name.Trim());
                cmd.Parameters.AddWithValue("@ReportId", ReportScheduleEntityObj.ReportId);
                cmd.Parameters.AddWithValue("@DateFilterCriteria",ReportScheduleEntityObj.DateFilterCriteria);
                cmd.Parameters.AddWithValue("@DateFrom", ReportScheduleEntityObj.DateFrom == null ? (object)DBNull.Value : ReportScheduleEntityObj.DateFrom);
                cmd.Parameters.AddWithValue("@DateTo", ReportScheduleEntityObj.DateTo == null ? (object)DBNull.Value : ReportScheduleEntityObj.DateTo);
                cmd.Parameters.AddWithValue("@TimeFrom", ReportScheduleEntityObj.TimeFrom == null ? (object)DBNull.Value : ReportScheduleEntityObj.TimeFrom);
                cmd.Parameters.AddWithValue("@TimeTo", ReportScheduleEntityObj.TimeTo == null ? (object)DBNull.Value : ReportScheduleEntityObj.TimeTo);
                cmd.Parameters.AddWithValue("@WeekDays", ReportScheduleEntityObj.WeekDays == null ? (object)DBNull.Value : ReportScheduleEntityObj.WeekDays);
                if (DBType == "vRecord")
                {
                    cmd.Parameters.AddWithValue("@Users", ReportScheduleEntityObj.Users == null ? (object)DBNull.Value : ReportScheduleEntityObj.Users.Trim());
                }
                
                if (DBType == "vBoard")
                {
                    cmd.Parameters.AddWithValue("@Extensions", ReportScheduleEntityObj.Extensions == null ? (object)DBNull.Value : ReportScheduleEntityObj.Extensions.Trim());
                    cmd.Parameters.AddWithValue("@Boards", ReportScheduleEntityObj.Boards == null ? (object)DBNull.Value : ReportScheduleEntityObj.Boards.Trim());
                    cmd.Parameters.AddWithValue("@Agents", ReportScheduleEntityObj.Agents == null ? (object)DBNull.Value : ReportScheduleEntityObj.Agents.Trim());
                    cmd.Parameters.AddWithValue("@ExternalRouting", ReportScheduleEntityObj.ExternalRouting == null ? (object)DBNull.Value : ReportScheduleEntityObj.ExternalRouting.Trim());
                    cmd.Parameters.AddWithValue("@ReportType", ReportScheduleEntityObj.ReportType == null ? (object)DBNull.Value : ReportScheduleEntityObj.ReportType.Trim());
                    cmd.Parameters.AddWithValue("@TimeInterval", ReportScheduleEntityObj.TimeInterval);
                    cmd.Parameters.AddWithValue("@CallsOption", ReportScheduleEntityObj.CallsOption);
                }

                cmd.Parameters.AddWithValue("@ScheduleInterval", ReportScheduleEntityObj.ScheduleInterval);
                cmd.Parameters.AddWithValue("@ScheduleValue", ReportScheduleEntityObj.ScheduleValue);
                cmd.Parameters.AddWithValue("@ScheduleTimeHours", int.Parse(ReportScheduleEntityObj.ScheduleTimeHours).ToString());
                cmd.Parameters.AddWithValue("@ScheduleTimeMinutes", int.Parse(ReportScheduleEntityObj.ScheduleTimeMinutes).ToString());

                cmd.Parameters.AddWithValue("@ExecutionTime", ReportScheduleEntityObj.ExecutionTime == null ? (object)DBNull.Value : ReportScheduleEntityObj.ExecutionTime);
                cmd.Parameters.AddWithValue("@DateCreated", ReportScheduleEntityObj.DateCreated == null ? (object)DBNull.Value : ReportScheduleEntityObj.DateCreated);
                con.Open();
                int insertedID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                con.Close();

                string[] Emails = ReportScheduleEntityObj.Emails.Trim().Split(';');

                foreach (string s in Emails)
                {
                    if (s != "")
                    {
                        string query2 = "Insert into reportscheduleemails (ScheduleId,EmailId) values(@ScheduleId,@EmailId)";
                        MySqlCommand cmd2 = new MySqlCommand(query2, con);
                        cmd2.Parameters.AddWithValue("@ScheduleId", insertedID);
                        cmd2.Parameters.AddWithValue("@EmailId", s);

                        con.Open();
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                }
                if (DBType == "vBoard")
                {
                    string[] Boards = ReportScheduleEntityObj.Boards.Trim().Split(',');

                    foreach (string s in Boards)
                    {
                        if (s != "")
                        {
                            string query2 = "Insert into scheduleboards (ScheduleId,BoardId) values(@ScheduleId,@BoardId)";
                            MySqlCommand cmd2 = new MySqlCommand(query2, con);
                            cmd2.Parameters.AddWithValue("@ScheduleId", insertedID);
                            cmd2.Parameters.AddWithValue("@BoardId", s);

                            con.Open();
                            cmd2.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return false;

            }
        }
        public bool UpdateIntoReportSchedule(ReportScheduleEntity ReportScheduleEntityObj)
        {
            try
            {
                if (ReportScheduleEntityObj.DateFilterCriteria == 7)//if report date filter criteria is custom , we dont change the from and to dates 
                {
                    if (ReportScheduleEntityObj.ReportType == null || ReportScheduleEntityObj.ReportType.Trim().Length == 0)
                    {
                        string query = "Update ReportSchedules SET Name=@Name,ScheduleInterval=@ScheduleInterval,ScheduleValue=@ScheduleValue,ScheduleTimeHours=@ScheduleTimeHours,ScheduleTimeMinutes=@ScheduleTimeMinutes,ExecutionTime=@ExecutionTime where Id=@Id";
                        MySqlCommand cmd = new MySqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@Id", ReportScheduleEntityObj.Id);
                        cmd.Parameters.AddWithValue("@Name", ReportScheduleEntityObj.Name == null ? (object)DBNull.Value : ReportScheduleEntityObj.Name.Trim());
                        cmd.Parameters.AddWithValue("@ScheduleInterval", ReportScheduleEntityObj.ScheduleInterval);
                        cmd.Parameters.AddWithValue("@ScheduleValue", ReportScheduleEntityObj.ScheduleValue);
                        cmd.Parameters.AddWithValue("@ScheduleTimeHours", ReportScheduleEntityObj.ScheduleTimeHours);
                        cmd.Parameters.AddWithValue("@ScheduleTimeMinutes", ReportScheduleEntityObj.ScheduleTimeMinutes);
                        cmd.Parameters.AddWithValue("@ExecutionTime", ReportScheduleEntityObj.ExecutionTime);
                        //cmd.Parameters.AddWithValue("@ReportType", ReportScheduleEntityObj.ReportType);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        if (ReportScheduleEntityObj.ReportType == null || ReportScheduleEntityObj.ReportType.Trim().Length == 0)
                        {
                            string query = "Update ReportSchedules SET Name=@Name,ScheduleInterval=@ScheduleInterval,ScheduleValue=@ScheduleValue,ScheduleTimeHours=@ScheduleTimeHours,ScheduleTimeMinutes=@ScheduleTimeMinutes,ExecutionTime=@ExecutionTime where Id=@Id";
                            MySqlCommand cmd = new MySqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@Id", ReportScheduleEntityObj.Id);
                            cmd.Parameters.AddWithValue("@Name", ReportScheduleEntityObj.Name == null ? (object)DBNull.Value : ReportScheduleEntityObj.Name.Trim());
                            cmd.Parameters.AddWithValue("@ScheduleInterval", ReportScheduleEntityObj.ScheduleInterval);
                            cmd.Parameters.AddWithValue("@ScheduleValue", ReportScheduleEntityObj.ScheduleValue);
                            cmd.Parameters.AddWithValue("@ScheduleTimeHours", ReportScheduleEntityObj.ScheduleTimeHours);
                            cmd.Parameters.AddWithValue("@ScheduleTimeMinutes", ReportScheduleEntityObj.ScheduleTimeMinutes);
                            cmd.Parameters.AddWithValue("@ExecutionTime", ReportScheduleEntityObj.ExecutionTime);
                           // cmd.Parameters.AddWithValue("@ReportType", ReportScheduleEntityObj.ReportType);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            string query = "Update ReportSchedules SET Name=@Name,ScheduleInterval=@ScheduleInterval,ScheduleValue=@ScheduleValue,ScheduleTimeHours=@ScheduleTimeHours,ScheduleTimeMinutes=@ScheduleTimeMinutes,ExecutionTime=@ExecutionTime,ReportType=@ReportType where Id=@Id";
                            MySqlCommand cmd = new MySqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@Id", ReportScheduleEntityObj.Id);
                            cmd.Parameters.AddWithValue("@Name", ReportScheduleEntityObj.Name == null ? (object)DBNull.Value : ReportScheduleEntityObj.Name.Trim());
                            cmd.Parameters.AddWithValue("@ScheduleInterval", ReportScheduleEntityObj.ScheduleInterval);
                            cmd.Parameters.AddWithValue("@ScheduleValue", ReportScheduleEntityObj.ScheduleValue);
                            cmd.Parameters.AddWithValue("@ScheduleTimeHours", ReportScheduleEntityObj.ScheduleTimeHours);
                            cmd.Parameters.AddWithValue("@ScheduleTimeMinutes", ReportScheduleEntityObj.ScheduleTimeMinutes);
                            cmd.Parameters.AddWithValue("@ExecutionTime", ReportScheduleEntityObj.ExecutionTime);
                            cmd.Parameters.AddWithValue("@ReportType", ReportScheduleEntityObj.ReportType);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                else
                {
                    string query = "Update ReportSchedules SET Name=@Name,DateFrom=@DateFrom,DateTo=@DateTo,ScheduleInterval=@ScheduleInterval,ScheduleValue=@ScheduleValue,ScheduleTimeHours=@ScheduleTimeHours,ScheduleTimeMinutes=@ScheduleTimeMinutes,ExecutionTime=@ExecutionTime,ReportType=@ReportType where Id=@Id";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", ReportScheduleEntityObj.Id);
                    cmd.Parameters.AddWithValue("@Name", ReportScheduleEntityObj.Name == null ? (object)DBNull.Value : ReportScheduleEntityObj.Name.Trim());
                    cmd.Parameters.AddWithValue("@DateFrom", ReportScheduleEntityObj.DateFrom == null ? (object)DBNull.Value : ReportScheduleEntityObj.DateFrom);
                    cmd.Parameters.AddWithValue("@DateTo", ReportScheduleEntityObj.DateTo == null ? (object)DBNull.Value : ReportScheduleEntityObj.DateTo);
                    cmd.Parameters.AddWithValue("@ScheduleInterval", ReportScheduleEntityObj.ScheduleInterval);
                    cmd.Parameters.AddWithValue("@ScheduleValue", ReportScheduleEntityObj.ScheduleValue);
                    cmd.Parameters.AddWithValue("@ScheduleTimeHours", ReportScheduleEntityObj.ScheduleTimeHours);
                    cmd.Parameters.AddWithValue("@ScheduleTimeMinutes", ReportScheduleEntityObj.ScheduleTimeMinutes);
                    cmd.Parameters.AddWithValue("@ExecutionTime", ReportScheduleEntityObj.ExecutionTime);
                    cmd.Parameters.AddWithValue("@ReportType", ReportScheduleEntityObj.ReportType);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                string query1 = "Delete From reportscheduleemails where ScheduleId=" + ReportScheduleEntityObj.Id + "";
                MySqlCommand cmd1 = new MySqlCommand(query1, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
                string[] Emails = ReportScheduleEntityObj.Emails.Split(';');

                foreach (string s in Emails)
                {
                    if (s != "")
                    {
                        string query2 = "Insert into reportscheduleemails (ScheduleId,EmailId) values(@ScheduleId,@EmailId)";
                        MySqlCommand cmd2 = new MySqlCommand(query2, con);
                        cmd2.Parameters.AddWithValue("@ScheduleId", ReportScheduleEntityObj.Id);
                        cmd2.Parameters.AddWithValue("@EmailId", s);

                        con.Open();
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return false;

            }
            //finally
            //{
            //    if (con != null)
            //        ((IDisposable)con).Dispose();
            //}
        }
        public bool UpdateScheduleFilters(ReportScheduleEntity ReportScheduleEntityObj, string DBType)
        {
            try
            {
                string query = "";
                if (DBType == "vBoard")
                {
                    query = "Update ReportSchedules SET DateFilterCriteria=@DateFilterCriteria,CallsOption=@CallsOption,DateFrom=@DateFrom,DateTo=@DateTo,TimeFrom=@TimeFrom,TimeTo=@TimeTo,WeekDays=@WeekDays,Boards=@Boards,Agents=@Agents,ExternalRouting=@ExternalRouting,ReportType=@ReportType,Extensions=@Extensions,TimeInterval=@TimeInterval where Id=@Id";
                }
                else if (DBType == "vRecord")
                {
                    query = "Update ReportSchedules SET DateFilterCriteria=@DateFilterCriteria,DateFrom=@DateFrom,DateTo=@DateTo,TimeFrom=@TimeFrom,TimeTo=@TimeTo,WeekDays=@WeekDays,Users=@Users where Id=@Id";
                }

             
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", ReportScheduleEntityObj.Id);
                cmd.Parameters.AddWithValue("@DateFilterCriteria", ReportScheduleEntityObj.DateFilterCriteria);
                cmd.Parameters.AddWithValue("@DateFrom", ReportScheduleEntityObj.DateFrom);
                cmd.Parameters.AddWithValue("@DateTo", ReportScheduleEntityObj.DateTo);
                cmd.Parameters.AddWithValue("@TimeFrom", ReportScheduleEntityObj.TimeFrom);
                cmd.Parameters.AddWithValue("@TimeTo", ReportScheduleEntityObj.TimeTo);
                cmd.Parameters.AddWithValue("@WeekDays", ReportScheduleEntityObj.WeekDays);

                if (DBType == "vRecord")
                {
                    cmd.Parameters.AddWithValue("@Users", ReportScheduleEntityObj.Users == null ? (object)DBNull.Value : ReportScheduleEntityObj.Users.Trim());
                }

                if (DBType == "vBoard")
                {
                    cmd.Parameters.AddWithValue("@Extensions", ReportScheduleEntityObj.Extensions == null ? (object)DBNull.Value : ReportScheduleEntityObj.Extensions.Trim());
                    cmd.Parameters.AddWithValue("@Boards", ReportScheduleEntityObj.Boards == null ? (object)DBNull.Value : ReportScheduleEntityObj.Boards.Trim());
                    cmd.Parameters.AddWithValue("@Agents", ReportScheduleEntityObj.Agents == null ? (object)DBNull.Value : ReportScheduleEntityObj.Agents.Trim());
                    cmd.Parameters.AddWithValue("@ExternalRouting", ReportScheduleEntityObj.ExternalRouting == null ? (object)DBNull.Value : ReportScheduleEntityObj.ExternalRouting.Trim());
                    cmd.Parameters.AddWithValue("@ReportType", ReportScheduleEntityObj.ReportType == null ? (object)DBNull.Value : ReportScheduleEntityObj.ReportType.Trim());
                    cmd.Parameters.AddWithValue("@TimeInterval", ReportScheduleEntityObj.TimeInterval);
                    cmd.Parameters.AddWithValue("@CallsOption", ReportScheduleEntityObj.CallsOption);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return false;

            }
        }
        public bool DeleteFromReportSchedule(int ReportScheduleId)
        {
            try
            {
                string query = "Delete From reportscheduleemails where ScheduleId=" + ReportScheduleId + "";
                MySqlCommand cmd = new MySqlCommand(query, con);
               
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                string query1 = "Delete From ReportSchedules where Id=" + ReportScheduleId + "";
                cmd = new MySqlCommand(query1, con);
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
        public List<string> GetEmailIdsOnScheduleId(int ReportScheduleId)
        {
            try
            {
                List<string> EmailIdsList = new List<string>();
                DataTable dt = new DataTable();
                string query = "SELECT* from reportscheduleemails where ScheduleId=" + ReportScheduleId + "";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    string EmailId= row["EmailId"].ToString();
                    EmailIdsList.Add(EmailId);
                }

                return EmailIdsList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public bool UpdateFromToAndExecutionTimeOfSchedule(int Id,DateTime ExecutionTime, DateTime DateFrom,DateTime DateTo)
        {
            try
            {
                string query = "Update ReportSchedules SET ExecutionTime=@ExecutionTime,DateFrom=@DateFrom,DateTo=@DateTo where Id=@Id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@ExecutionTime", ExecutionTime);
                cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@DateTo", DateTo);
                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return false;

            }
           
        }
        public bool CheckIfScheduleNameAlreadyExists(int ScheduleId, string Name)
        {
            try
            {

                UsersEntity UsersEntityObj = new UsersEntity();
                DataTable dt = new DataTable();
                string query = "select * from reportschedules where Id != " + ScheduleId + " And Name='" + Name + "'";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return true;

            }
        }
    }
}