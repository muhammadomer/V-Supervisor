using LogApp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using vSuperMTClient.Entities;

namespace vSuperMTClient.DALs
{
    public class BoardsDAL
    {
        MySqlConnection con;
        public BoardsDAL(string ClientDB)
        {
            string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
            con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB));
        }
        public List<BoardsEntity> GetAllHuntGroups()
        {
            try
            {
                List<BoardsEntity> HuntGroupsEntityList = new List<BoardsEntity>();
                DataTable dt = new DataTable();
                string query = "select *from boards";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    BoardsEntity HuntGroupsEntityObj = new BoardsEntity();
                    HuntGroupsEntityObj.huntGroupId = Convert.ToInt32(row["boardid"].ToString());
                    HuntGroupsEntityObj.extension = row["extension"].ToString();
                    HuntGroupsEntityObj.GroupNumber = row["GroupNumber"].ToString();
                    HuntGroupsEntityObj.Title = row["Title"].ToString();
                    HuntGroupsEntityObj.Reset = row["Reset"].ToString();
                    HuntGroupsEntityObj.ResetTime = row["ResetTime"].ToString();
                    HuntGroupsEntityObj.ResetDate = row["ResetDate"].ToString();
                    HuntGroupsEntityObj.ResetTime2 = row["ResetTime2"].ToString();
                    HuntGroupsEntityObj.SlaVal = Convert.ToInt32(row["SLAVal"].ToString());
                    HuntGroupsEntityObj.SlaOperator = Convert.ToInt32(row["SlaOperator"].ToString());
                    HuntGroupsEntityObj.AbandonedTime = Convert.ToInt32(row["AbandonedTime"].ToString());
                    HuntGroupsEntityList.Add(HuntGroupsEntityObj);
                }
                FillHuntGroup_DDIs(HuntGroupsEntityList);
                con.Close();
                return HuntGroupsEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }

        public void FillHuntGroup_DDIs(List<BoardsEntity> list)
        {
            try
            {
                foreach (BoardsEntity board in list)
                {
                    try
                    {

                        board.HuntGroup_Extension = new List<Boards_ExtensionEntity>();
                        string query = "Select DDI,Name from board_ddi where boardid=" + board.huntGroupId + ";";

                        using (MySqlCommand cmd = new MySqlCommand(query, con))
                        {
                            using (MySqlDataAdapter adpt = new MySqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                adpt.Fill(dt);
                                foreach (DataRow row in dt.Rows)
                                {
                                    try
                                    {
                                        Boards_ExtensionEntity HuntGroup_ExtensionEntityObj = new Boards_ExtensionEntity();
                                        HuntGroup_ExtensionEntityObj.huntGroupId = board.huntGroupId;
                                        HuntGroup_ExtensionEntityObj.Name = row["Name"].ToString();
                                        HuntGroup_ExtensionEntityObj.DDI = row["DDI"].ToString();
                                        board.HuntGroup_Extension.Add(HuntGroup_ExtensionEntityObj);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        LogApp.Log4Net.WriteException(ex);
                    }
                }
            }

            catch (Exception ex)
            {
                LogApp.Log4Net.WriteException(ex);
            }
        }

        public BoardsEntity GetHuntGroupOnId(int boardid)
        {
            try
            {
                BoardsEntity HuntGroupsEntityObj = new BoardsEntity();
                DataTable dt = new DataTable();
                string query = "select *from boards where boardid=" + boardid + "";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    HuntGroupsEntityObj.huntGroupId = Convert.ToInt32(row["boardid"].ToString());
                    HuntGroupsEntityObj.extension = row["extension"].ToString();
                    HuntGroupsEntityObj.GroupNumber = row["GroupNumber"].ToString();
                    //HuntGroupsEntityObj.DDI = row["DDI"].ToString();
                    HuntGroupsEntityObj.Title = row["Title"].ToString();
                    HuntGroupsEntityObj.Reset = row["Reset"].ToString();
                    HuntGroupsEntityObj.ResetTime = row["ResetTime"].ToString();
                    HuntGroupsEntityObj.ResetDate = row["ResetDate"].ToString();
                    HuntGroupsEntityObj.ResetTime2 = row["ResetTime2"].ToString();
                    HuntGroupsEntityObj.SlaVal = Convert.ToInt32(row["SLAVal"].ToString());
                    HuntGroupsEntityObj.SlaOperator = Convert.ToInt32(row["SlaOperator"].ToString());
                    HuntGroupsEntityObj.AbandonedTime = Convert.ToInt32(row["AbandonedTime"].ToString());
                }
                con.Close();
                dt = new DataTable();
                query = "Select a.boardid,b.DDI from boards a inner join board_ddi b on a.boardid=b.boardid where a.boardid=" + boardid + "";
                con.Open();
                da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    Boards_ExtensionEntity HuntGroup_ExtensionEntityObj = new Boards_ExtensionEntity();
                    HuntGroup_ExtensionEntityObj.huntGroupId = Convert.ToInt32(row["boardid"].ToString());
                    HuntGroup_ExtensionEntityObj.DDI = row["DDI"].ToString();
                    HuntGroupsEntityObj.HuntGroup_Extension.Add(HuntGroup_ExtensionEntityObj);

                }
                con.Close();
                return HuntGroupsEntityObj;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public bool InsertIntoHuntGroups(BoardsEntity HuntGroupsEntityObj)
        {
            try
            {
                string query = "Insert into boards (extension,GroupNumber,Title,Reset,ResetTime,ResetDate,ResetTime2,SlaVal,SlaOperator,AbandonedTime) values (@extension,@GroupNumber,@Title,@Reset,@ResetTime,@ResetDate,null,@SlaVal,@SlaOperator,@AbandonedTime); SELECT LAST_INSERT_ID();";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@extension", HuntGroupsEntityObj.extension == null ? (object)DBNull.Value : HuntGroupsEntityObj.extension.Trim());
                cmd.Parameters.AddWithValue("@GroupNumber", HuntGroupsEntityObj.GroupNumber);

                // cmd.Parameters.AddWithValue("@DDI", HuntGroupsEntityObj.DDI == null ? (object)DBNull.Value : HuntGroupsEntityObj.DDI.Trim());
                cmd.Parameters.AddWithValue("@Title", HuntGroupsEntityObj.Title == null ? (object)DBNull.Value : HuntGroupsEntityObj.Title.Trim());
                cmd.Parameters.AddWithValue("@Reset", HuntGroupsEntityObj.Reset == null ? (object)DBNull.Value : HuntGroupsEntityObj.Reset.Trim());
                cmd.Parameters.AddWithValue("@ResetTime", HuntGroupsEntityObj.ResetTime == null ? (object)DBNull.Value : HuntGroupsEntityObj.ResetTime.Trim());
                //cmd.Parameters.AddWithValue("@ResetDate", HuntGroupsEntityObj.ResetDate == null ? (object)DBNull.Value : HuntGroupsEntityObj.ResetDate.Trim());
                cmd.Parameters.AddWithValue("@ResetDate", DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture));
                //cmd.Parameters.AddWithValue("@ResetTime2", DateTime.Now.ToString("MMM dd yyyy 12:00") + "AM");
                //cmd.Parameters.AddWithValue("@ResetTime2",null);
                cmd.Parameters.AddWithValue("@SlaVal", HuntGroupsEntityObj.SlaVal);
                cmd.Parameters.AddWithValue("@SlaOperator", HuntGroupsEntityObj.SlaOperator);
                cmd.Parameters.AddWithValue("@AbandonedTime", HuntGroupsEntityObj.AbandonedTime);
                con.Open();

                // int insertedID= Convert.ToInt32(cmd.ExecuteScalar());
                int insertedID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                con.Close();

                foreach (Boards_ExtensionEntity HuntGroup_ExtensionEntityObj in HuntGroupsEntityObj.HuntGroup_Extension)
                {
                    string query2 = "Insert into board_ddi (boardid,DDI) values(@boardid,@DDI)";
                    MySqlCommand cmd2 = new MySqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("@boardid", insertedID);
                    cmd2.Parameters.AddWithValue("@DDI", HuntGroup_ExtensionEntityObj.DDI == null ? (object)DBNull.Value : HuntGroup_ExtensionEntityObj.DDI.Trim());
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
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
        public bool UpdateIntoHuntGroups(BoardsEntity HuntGroupsEntityObj)
        {
            try
            {
                string query = "Update boards SET extension=@extension,GroupNumber=@GroupNumber,Title=@Title,Reset=@Reset,ResetTime=@ResetTime,ResetTime2=@ResetTime2,SlaVal=@SlaVal,SlaOperator=@SlaOperator,AbandonedTime=@AbandonedTime where boardid=@boardid";
                //string query = "Update HuntGroups SET extension=@extension,DDI=@DDI,Title=@Title,Reset=@Reset,ResetTime=@ResetTime,ResetDate=@ResetDate,ResetTime2=@ResetTime2,SlaVal=@SlaVal,SlaOperator=@SlaOperator,AbandonedTime=@AbandonedTime where huntGroupId=@huntGroupId";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@boardid", HuntGroupsEntityObj.huntGroupId);
                cmd.Parameters.AddWithValue("@extension", HuntGroupsEntityObj.extension == null ? (object)DBNull.Value : HuntGroupsEntityObj.extension.Trim());
                cmd.Parameters.AddWithValue("@GroupNumber", HuntGroupsEntityObj.GroupNumber);
                //cmd.Parameters.AddWithValue("@DDI", HuntGroupsEntityObj.DDI == null ? (object)DBNull.Value : HuntGroupsEntityObj.DDI.Trim());
                cmd.Parameters.AddWithValue("@Title", HuntGroupsEntityObj.Title == null ? (object)DBNull.Value : HuntGroupsEntityObj.Title.Trim());
                cmd.Parameters.AddWithValue("@Reset", HuntGroupsEntityObj.Reset == null ? (object)DBNull.Value : HuntGroupsEntityObj.Reset.Trim());
                cmd.Parameters.AddWithValue("@ResetTime", HuntGroupsEntityObj.ResetTime == null ? (object)DBNull.Value : HuntGroupsEntityObj.ResetTime.Trim());
                //  cmd.Parameters.AddWithValue("@ResetDate", HuntGroupsEntityObj.ResetDate == null ? (object)DBNull.Value : HuntGroupsEntityObj.ResetDate.Trim());
                cmd.Parameters.AddWithValue("@ResetTime2", DBNull.Value);
                cmd.Parameters.AddWithValue("@SlaVal", HuntGroupsEntityObj.SlaVal);
                cmd.Parameters.AddWithValue("@SlaOperator", HuntGroupsEntityObj.SlaOperator);
                cmd.Parameters.AddWithValue("@AbandonedTime", HuntGroupsEntityObj.AbandonedTime);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                string query1 = "Delete From board_ddi where boardid=" + HuntGroupsEntityObj.huntGroupId + "";
                MySqlCommand cmd1 = new MySqlCommand(query1, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
                foreach (Boards_ExtensionEntity HuntGroup_ExtensionEntityObj in HuntGroupsEntityObj.HuntGroup_Extension)
                {
                    string query2 = "Insert into board_ddi (boardid,DDI) values(@boardid,@DDI)";
                    MySqlCommand cmd2 = new MySqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("@boardid", HuntGroupsEntityObj.huntGroupId);
                    cmd2.Parameters.AddWithValue("@DDI", HuntGroup_ExtensionEntityObj.DDI == null ? (object)DBNull.Value : HuntGroup_ExtensionEntityObj.DDI.Trim());
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
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
        public bool DeleteFromHuntGroups(int boardid)
        {
            try
            {
                string query = "Delete From boards where boardid=" + boardid + "";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                query = "Delete From board_ddi where boardid=" + boardid + "";
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
            //finally
            //{
            //    if (con != null)
            //        ((IDisposable)con).Dispose();
            //}
        }
        public bool CheckIfBoardAlreadyAssigned(int BoardID, int GroupNumber)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "Select* from  boards where BoardID!=@BoardID and GroupNumber=@GroupNumber";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BoardID", BoardID);
                cmd.Parameters.AddWithValue("@GroupNumber", GroupNumber);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return false;
            }

        }
        public bool ResetHuntGroup(int boardid)
        {
            try
            {

                string ResetHour = DateTime.Now.Hour.ToString();
                string ResetMinute = DateTime.Now.Minute.ToString();
                if (int.Parse(ResetHour) < 10)
                {
                    ResetHour = "0" + ResetHour;
                }
                if (int.Parse(ResetMinute) < 10)
                {
                    ResetMinute = "0" + ResetMinute;
                }
                string ResetTime = ResetHour + ':' + ResetMinute;
                string query = "Update boards SET ResetTime=@ResetTime,ResetDate=@ResetDate where boardid=@boardid";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@boardid", boardid);
                cmd.Parameters.AddWithValue("@ResetTime", ResetTime);
                cmd.Parameters.AddWithValue("@ResetDate", DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture));
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
            //finally
            //{
            //    if (con != null)
            //        ((IDisposable)con).Dispose();
            //}
        }
        public bool CheckIfExtensionAlreadyAssigned(int BoardID, string Extension)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "Select* from  boards where BoardID!=@BoardID and extension=@extension";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BoardID", BoardID);
                cmd.Parameters.AddWithValue("@extension", Extension);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return false;
            }

        }
    }
}