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
    public class AreaCodesDAL
    {
        MySqlConnection con;
        public AreaCodesDAL(string ClientDB)
        {
            string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
            con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB));
        }

        public List<AreaCodesEntity> GetAreaCodes()
        {
            try
            {
                List<AreaCodesEntity> AreaCodesEntityList = new List<AreaCodesEntity>();
                MySqlCommand cmd = new MySqlCommand("Select * From AreaCodes where IsDeleted=false", con);
                DataTable dt = new DataTable();
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    AreaCodesEntity AreaCodeEntityObj = new AreaCodesEntity();
                    AreaCodeEntityObj.Id = Convert.ToInt32(row["Id"].ToString());
                    AreaCodeEntityObj.AreaCode = row["AreaCode"].ToString();
                    AreaCodeEntityObj.AreaDescription = row["AreaDescription"].ToString();
                    AreaCodesEntityList.Add(AreaCodeEntityObj);
                }

                con.Close();
                return AreaCodesEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {
                if (con != null)
                    ((IDisposable)con).Dispose();
            }
        }
        public string AddAreaCode(AreaCodesEntity AreaCodeEntityObj)
        {
            try
            {
                string query = "insert into AreaCodes(AreaCode,AreaDescription) values(@AreaCode,@AreaDescription); select LAST_INSERT_ID();";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AreaCode", AreaCodeEntityObj.AreaCode);
                cmd.Parameters.AddWithValue("@AreaDescription", AreaCodeEntityObj.AreaDescription);
                
                con.Open();
                int insertedID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                //cmd.ExecuteNonQuery();
                con.Close();
                return insertedID.ToString();
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return "Failed to insert";

            }
            finally
            {
                if (con != null)
                    ((IDisposable)con).Dispose();
            }
        }
        public bool UpdateAreaCodeOnId(AreaCodesEntity AreaCodeEntityObj)
        {
            try
            {
                string query = "Update AreaCodes Set AreaCode=@AreaCode,AreaDescription=@AreaDescription where Id=" + AreaCodeEntityObj.Id + "";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AreaCode", AreaCodeEntityObj.AreaCode);
                cmd.Parameters.AddWithValue("@AreaDescription", AreaCodeEntityObj.AreaDescription);
                
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
            finally
            {
                if (con != null)
                    ((IDisposable)con).Dispose();
            }
        }
        public bool DeleteAreaCodeOnId(int AreaCodeId)
        {
            try
            {
                string query = "Update AreaCodes Set IsDeleted=@IsDeleted where Id=" + AreaCodeId + "";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IsDeleted", true);
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
            finally
            {
                if (con != null)
                    ((IDisposable)con).Dispose();
            }
        }
        public AreaCodesEntity GetAreaCodeOnId(int AreaCodeId)
        {
            try
            {
                AreaCodesEntity AreaCodeEntityObj = new AreaCodesEntity();
                MySqlCommand cmd = new MySqlCommand("Select * From AreaCodes where Id=" + AreaCodeId + "", con);
                DataTable dt = new DataTable();
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    AreaCodeEntityObj.Id = Convert.ToInt32(row["Id"].ToString());
                    AreaCodeEntityObj.AreaCode = row["AreaCode"].ToString();
                    AreaCodeEntityObj.AreaDescription = row["AreaDescription"].ToString();
                    
                }
                con.Close();

                return AreaCodeEntityObj;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return null;
            }
            finally
            {
                if (con != null)
                    ((IDisposable)con).Dispose();
            }

        }

        public bool CheckIfAreaCodeAlreadyExists(int id, string AreaCode)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select * From AreaCodes where AreaCode='" + AreaCode + "' and Id!=" + id + " and IsDeleted=false", con);
                DataTable dt = new DataTable();
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else return false;


            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return true;
            }
            finally
            {
                if (con != null)
                    ((IDisposable)con).Dispose();
            }
        }
    }
}