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
    public class AgentStatsDAL
    {
        
        MySqlConnection con;
        public AgentStatsDAL(string ClientDB)
        {
            string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
            con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB));
        }
        public List<AgentStatsEntity> GetAllAgentStats()
        {
            try
            {
                List<AgentStatsEntity> AgentStatsEntityList = new List<AgentStatsEntity>();
                DataTable dt = new DataTable();
                string query = "select * from agent_stats";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    AgentStatsEntity AgentStatsEntityObj = new AgentStatsEntity();
                    AgentStatsEntityObj.ID = Convert.ToInt32(row["ID"].ToString());
                    AgentStatsEntityObj.Extension = row["Extension"].ToString();
                    AgentStatsEntityObj.AgentName = row["AgentName"].ToString();
                    AgentStatsEntityObj.EventID = Convert.ToInt32(row["EventID"].ToString());
                    AgentStatsEntityObj.StartTime = Convert.ToDateTime(row["StartTime"].ToString());
                    AgentStatsEntityObj.EndTime = Convert.ToDateTime(row["EndTime"].ToString());
                    AgentStatsEntityObj.AgentGroups = row["AgentGroups"].ToString();
                   
                    AgentStatsEntityList.Add(AgentStatsEntityObj);
                }
                con.Close();
                return AgentStatsEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public List<AgentStatsEntity> GetDistinctAgentsNames()
        {
            try
            {
                List<AgentStatsEntity> AgentStatsEntityList = new List<AgentStatsEntity>();
                DataTable dt = new DataTable();

                string query = "select DISTINCT AgentName from agent_stats order by AgentName";

                //string IsACD = ConfigurationManager.AppSettings["IsACD"];
                //if (IsACD == "1")
                //{
                //    query = "";
                //}

                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    AgentStatsEntity AgentStatsEntityObj = new AgentStatsEntity();
                    AgentStatsEntityObj.AgentName = row["AgentName"].ToString();
                    AgentStatsEntityList.Add(AgentStatsEntityObj);
                }
                con.Close();
                return AgentStatsEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        //public AgentsEntity GetAgentOnId(int AgentId)
        //{
        //    try
        //    {
        //        AgentsEntity AgentStatsEntityObj = new AgentsEntity();
        //        DataTable dt = new DataTable();
        //        string query = "select *from Agents where AgentId="+AgentId+"";
        //        con.Open();
        //        MySqlDataAdapter da = new MySqlDataAdapter(query, con);
        //        da.Fill(dt);
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            AgentStatsEntityObj.AgentId = Convert.ToInt32(row["AgentId"].ToString());
        //            AgentStatsEntityObj.Extension = row["Extension"].ToString();
        //            AgentStatsEntityObj.FirstName = row["FirstName"].ToString();
        //            AgentStatsEntityObj.LastName = row["LastName"].ToString();
        //            AgentStatsEntityObj.Reset = row["Reset"].ToString();
        //            AgentStatsEntityObj.ResetTime = row["ResetTime"].ToString();
        //            AgentStatsEntityObj.ResetDate = row["ResetDate"].ToString();
        //            AgentStatsEntityObj.ResetTime2 = row["ResetTime2"].ToString();
        //            AgentStatsEntityObj.HuntGroupId = Convert.ToInt32(row["HuntGroupId"].ToString());
        //            AgentStatsEntityObj.PhysicalExtension = row["PhysicalExtension"].ToString();
        //        }
        //        con.Close();
        //        return AgentStatsEntityObj;
        //    }
        //    catch (Exception ex)
        //    {
        //        con.Close();
        //        LogApp.Log4Net.WriteException(ex);
        //        return null;

        //    }
        //}
        //public bool InsertIntoAgents(AgentsEntity AgentStatsEntityObj)
        //{
        //    try
        //    {
        //        string query = "Insert into Agents (Extension,FirstName,LastName,Reset,ResetTime,ResetDate,ResetTime2,HuntGroupId,PhysicalExtension) values(@Extension,@FirstName,@LastName,@Reset,@ResetTime,@ResetDate,@ResetTime2,@HuntGroupId,@PhysicalExtension); select LAST_INSERT_ID();";
        //        MySqlCommand cmd = new MySqlCommand(query, con);
        //        cmd.Parameters.AddWithValue("@Extension", AgentStatsEntityObj.Extension == null ? (object)DBNull.Value : AgentStatsEntityObj.Extension.Trim());
        //        cmd.Parameters.AddWithValue("@FirstName", AgentStatsEntityObj.FirstName == null ? (object)DBNull.Value : AgentStatsEntityObj.FirstName.Trim());
        //        cmd.Parameters.AddWithValue("@LastName", AgentStatsEntityObj.LastName == null ? (object)DBNull.Value : AgentStatsEntityObj.LastName.Trim());
        //        cmd.Parameters.AddWithValue("@Reset", AgentStatsEntityObj.Reset == null ? (object)DBNull.Value : AgentStatsEntityObj.Reset.Trim());
        //        cmd.Parameters.AddWithValue("@ResetTime", AgentStatsEntityObj.ResetTime == null ? (object)DBNull.Value : AgentStatsEntityObj.ResetTime.Trim());
        //        cmd.Parameters.AddWithValue("@ResetDate", AgentStatsEntityObj.ResetDate == null ? (object)DBNull.Value : AgentStatsEntityObj.ResetDate.Trim());
        //        cmd.Parameters.AddWithValue("@ResetTime2", DateTime.Now.ToString("MMM dd yyyy 12:00AM"));
        //        cmd.Parameters.AddWithValue("@HuntGroupId", AgentStatsEntityObj.HuntGroupId);
        //        cmd.Parameters.AddWithValue("@PhysicalExtension", AgentStatsEntityObj.PhysicalExtension == null ? (object)DBNull.Value : AgentStatsEntityObj.PhysicalExtension.Trim());
        //        con.Open();
        //        int insertedID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        //        con.Close();

        //        //foreach (HuntGroup_ExtensionEntity HuntGroup_ExtensionEntityObj in HuntGroupsEntityObj.HuntGroup_Extension)
        //        //{
        //        //    string query2 = "Insert into HuntGroup_Extension (huntGroupId,DDI) values(@huntGroupId,@DDI)";
        //        //    MySqlCommand cmd2 = new MySqlCommand(query2, con);
        //        //    cmd2.Parameters.AddWithValue("@huntGroupId", insertedID);
        //        //    cmd2.Parameters.AddWithValue("@DDI", HuntGroup_ExtensionEntityObj.DDI == null ? (object)DBNull.Value : HuntGroupsEntityObj.DDI.Trim());
        //        //    con.Open();
        //        //    cmd2.ExecuteNonQuery();
        //        //    con.Close();
        //        //}
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        con.Close();
        //        LogApp.Log4Net.WriteException(ex);
        //        return false;

        //    }
        //    //finally
        //    //{
        //    //    if (con != null)
        //    //        ((IDisposable)con).Dispose();
        //    //}
        //}
        //public bool UpdateIntoAgents(AgentsEntity AgentStatsEntityObj)
        //{
        //    try
        //    {
        //        string query = "Update Agents SET Extension=@Extension,FirstName=@FirstName,LastName=@LastName,Reset=@Reset,ResetTime=@ResetTime,ResetDate=@ResetDate,ResetTime2=@ResetTime2,HuntGroupId=@HuntGroupId,PhysicalExtension=@PhysicalExtension where AgentId=@AgentId";
        //        MySqlCommand cmd = new MySqlCommand(query, con);
        //        cmd.Parameters.AddWithValue("@AgentId", AgentStatsEntityObj.AgentId);
        //        cmd.Parameters.AddWithValue("@Extension", AgentStatsEntityObj.Extension == null ? (object)DBNull.Value : AgentStatsEntityObj.Extension.Trim());
        //        cmd.Parameters.AddWithValue("@FirstName", AgentStatsEntityObj.FirstName == null ? (object)DBNull.Value : AgentStatsEntityObj.FirstName.Trim());
        //        cmd.Parameters.AddWithValue("@LastName", AgentStatsEntityObj.LastName == null ? (object)DBNull.Value : AgentStatsEntityObj.LastName.Trim());
        //        cmd.Parameters.AddWithValue("@Reset", AgentStatsEntityObj.Reset == null ? (object)DBNull.Value : AgentStatsEntityObj.Reset.Trim());
        //        cmd.Parameters.AddWithValue("@ResetTime", AgentStatsEntityObj.ResetTime == null ? (object)DBNull.Value : AgentStatsEntityObj.ResetTime.Trim());
        //        cmd.Parameters.AddWithValue("@ResetDate", AgentStatsEntityObj.ResetDate == null ? (object)DBNull.Value : AgentStatsEntityObj.ResetDate.Trim());
        //        cmd.Parameters.AddWithValue("@ResetTime2", DateTime.Now.ToString("MMM dd yyyy 12:00AM"));
        //        cmd.Parameters.AddWithValue("@HuntGroupId", AgentStatsEntityObj.HuntGroupId);
        //        cmd.Parameters.AddWithValue("@PhysicalExtension", AgentStatsEntityObj.PhysicalExtension == null ? (object)DBNull.Value : AgentStatsEntityObj.PhysicalExtension.Trim());
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //        //string query1 = "Delete From HuntGroup_Extension where huntGroupId=" + HuntGroupsEntityObj.huntGroupId + "";
        //        //MySqlCommand cmd1 = new MySqlCommand(query1, con);
        //        //con.Open();
        //        //cmd1.ExecuteNonQuery();
        //        //con.Close();
        //        //foreach (HuntGroup_ExtensionEntity HuntGroup_ExtensionEntityObj in HuntGroupsEntityObj.HuntGroup_Extension)
        //        //{
        //        //    string query2 = "Insert into HuntGroup_Extension (huntGroupId,DDI) values(@huntGroupId,@DDI)";
        //        //    MySqlCommand cmd2 = new MySqlCommand(query2, con);
        //        //    cmd2.Parameters.AddWithValue("@huntGroupId", HuntGroupsEntityObj.huntGroupId);
        //        //    cmd2.Parameters.AddWithValue("@DDI", HuntGroup_ExtensionEntityObj.DDI == null ? (object)DBNull.Value : HuntGroupsEntityObj.DDI.Trim());
        //        //    con.Open();
        //        //    cmd2.ExecuteNonQuery();
        //        //    con.Close();
        //        //}
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        con.Close();
        //        LogApp.Log4Net.WriteException(ex);
        //        return false;

        //    }
        //    //finally
        //    //{
        //    //    if (con != null)
        //    //        ((IDisposable)con).Dispose();
        //    //}
        //}
        //public bool DeleteFromAgents(int AgentId)
        //{
        //    try
        //    {
        //        string query = "Delete From Agents where AgentId=" + AgentId + "";
        //        MySqlCommand cmd = new MySqlCommand(query, con);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        //query = "Delete From HuntGroup_Extension where huntGroupId=" + huntGroupId + "";
        //        //cmd.ExecuteNonQuery();
        //        //con.Close();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        con.Close();
        //        Log4Net.WriteException(ex);
        //        return false;
        //    }
        //    //finally
        //    //{
        //    //    if (con != null)
        //    //        ((IDisposable)con).Dispose();
        //    //}
        //}
        //public bool ResetAgents(int AgentId)
        //{
        //    try
        //    {
        //        string query = "update Agents set ResetTime2 = '" + DateTime.Now.ToString("MMM dd yyyy hh:mmtt") + "' where AgentId=" + AgentId + "";
        //        MySqlCommand cmd = new MySqlCommand(query, con);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        con.Close();
        //        Log4Net.WriteException(ex);
        //        return false;
        //    }
        //    //finally
        //    //{
        //    //    if (con != null)
        //    //        ((IDisposable)con).Dispose();
        //    //}
        //}
    }
}