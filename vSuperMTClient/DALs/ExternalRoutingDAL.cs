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
    public class ExternalRoutingDAL
    {
        MySqlConnection con;
        public ExternalRoutingDAL(string ClientDB)
        {
            string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
            con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB));
        }
        public List<ExternalRoutingEntity> GetExternalRouting()
        {
            try
            {
                List<ExternalRoutingEntity> ExternalRoutingEntityList = new List<ExternalRoutingEntity>();
                MySqlCommand cmd = new MySqlCommand("Select *From External_Routing where IsDeleted=false", con);
                DataTable dt = new DataTable();
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    ExternalRoutingEntity ExternalRoutingEntityObj = new ExternalRoutingEntity();
                    ExternalRoutingEntityObj.Id = Convert.ToInt32(row["Id"].ToString());
                    ExternalRoutingEntityObj.Name = row["Name"].ToString();
                    ExternalRoutingEntityObj.Number = row["Number"].ToString();
                    ExternalRoutingEntityObj.Enabled = Convert.ToBoolean(row["Enabled"]);
                    ExternalRoutingEntityList.Add(ExternalRoutingEntityObj);
                }

                con.Close();
                return ExternalRoutingEntityList;
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
        public bool AddExternalRouting(ExternalRoutingEntity ExternalRoutingEntityObj)
        {
            try
            {
                string query = "insert into External_Routing(Name,Number,Enabled) values(@Name,@Number,@Enabled)";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", ExternalRoutingEntityObj.Name);
                cmd.Parameters.AddWithValue("@Number", ExternalRoutingEntityObj.Number);
                cmd.Parameters.AddWithValue("@Enabled", ExternalRoutingEntityObj.Enabled);
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
        public bool UpdateExternalRoutingOnId(ExternalRoutingEntity ExternalRoutingEntityObj)
        {
            try
            {
                string query = "Update External_Routing Set Name=@Name,Number=@Number,Enabled=@Enabled where Id=" + ExternalRoutingEntityObj.Id + "";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", ExternalRoutingEntityObj.Name);
                cmd.Parameters.AddWithValue("@Number", ExternalRoutingEntityObj.Number);
                cmd.Parameters.AddWithValue("@Enabled", ExternalRoutingEntityObj.Enabled);
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
        public bool DeleteExternalRoutingOnId(int ExternalRoutingId)
        {
            try
            {
                string query = "Update External_Routing Set IsDeleted=@IsDeleted where Id=" + ExternalRoutingId + "";
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
        public bool CheckIfExternalRoutingAlreadyExists(int id, string name)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select * From External_Routing where Name='" + name + "' and Id!=" + id + " and IsDeleted=false", con);
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