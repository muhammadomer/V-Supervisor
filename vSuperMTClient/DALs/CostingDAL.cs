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
    public class CostingDAL
    {
        
        MySqlConnection con;
        public CostingDAL(string ClientDB)
        {
            string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
            con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB));
        }
        public List<CostingEntity> GetAllCostings()
        {
            try
            {
              
                List<CostingEntity> CostingEntityList = new List<CostingEntity>();
                DataTable dt = new DataTable();
                string query = "Select * from call_cost";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    CostingEntity CostingEntityObj = new CostingEntity();
                    CostingEntityObj.Id = Convert.ToInt32(row["Id"].ToString());
                    CostingEntityObj.CostType = row["CostType"].ToString();
                    CostingEntityObj.CostPerSec = float.Parse(String.IsNullOrEmpty(row["CostPerSec"].ToString()) ? "0" : row["CostPerSec"].ToString());  
                    CostingEntityObj.ConnectCost = float.Parse(String.IsNullOrEmpty(row["ConnectCost"].ToString()) ? "0": row["ConnectCost"].ToString());
                    CostingEntityObj.CallSetupCost = float.Parse(String.IsNullOrEmpty(row["CallSetupCost"].ToString()) ? "0" : row["CallSetupCost"].ToString());

                    string query2 = "select *from call_cost_numbers where Call_Cost_Id=" + CostingEntityObj.Id + "";
                    MySqlDataAdapter da2 = new MySqlDataAdapter(query2, con);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    string DialNumber = "";
                    foreach (DataRow row2 in dt2.Rows)
                    {
                        DialNumber += row2["DialNumber"].ToString()+";";
                    }
                    CostingEntityObj.DialNumber = DialNumber;

                    CostingEntityList.Add(CostingEntityObj);
                }
               
                return CostingEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public CostingEntity GetCostingOnId(int CostingId)
        {
            try
            {
                CostingEntity CostingEntityObj = new CostingEntity();
                DataTable dt = new DataTable();
                string query = "select *from call_cost where Id=" + CostingId + "";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    CostingEntityObj.Id = Convert.ToInt32(row["Id"].ToString());
                    CostingEntityObj.CostType = row["CostType"].ToString();
                    CostingEntityObj.CostPerSec = float.Parse(String.IsNullOrEmpty(row["CostPerSec"].ToString()) ? "0" : row["CostPerSec"].ToString());
                    CostingEntityObj.ConnectCost = float.Parse(String.IsNullOrEmpty(row["ConnectCost"].ToString()) ? "0" : row["ConnectCost"].ToString());
                    CostingEntityObj.CallSetupCost = float.Parse(String.IsNullOrEmpty(row["CallSetupCost"].ToString()) ? "0" : row["CallSetupCost"].ToString());
                }
                con.Close();
                dt = new DataTable();
                string query2 = "select *from call_cost_numbers where Call_Cost_Id=" + CostingId + "";
                con.Open();
                da = new MySqlDataAdapter(query2, con);
                da.Fill(dt);
                con.Close();
                string DialNumber = "";
                foreach (DataRow row in dt.Rows)
                {
                    DialNumber += row["DialNumber"].ToString() + ";";
                }
                CostingEntityObj.DialNumber = DialNumber;
                return CostingEntityObj;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
        public bool InsertIntoCosting(CostingEntity CostingEntityObj)
        {
            try
            {
                string query = "Insert into call_cost (CostType,CostPerSec,ConnectCost,CallSetupCost) values(@CostType,@CostPerSec,@ConnectCost,@CallSetupCost); select LAST_INSERT_ID();";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CostType", CostingEntityObj.CostType);
                cmd.Parameters.AddWithValue("@CostPerSec", CostingEntityObj.CostPerSec);
                cmd.Parameters.AddWithValue("@ConnectCost", CostingEntityObj.ConnectCost);
                cmd.Parameters.AddWithValue("@CallSetupCost", CostingEntityObj.CallSetupCost);
                con.Open();
                int insertedID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                con.Close();

                string[] DialNumber = CostingEntityObj.DialNumber.Trim().Split(';');

                foreach (string s in DialNumber)
                {
                    if (s != "")
                    {
                        string query2 = "Insert into call_cost_numbers (Call_Cost_Id,DialNumber) values(@Call_Cost_Id,@DialNumber)";
                        MySqlCommand cmd2 = new MySqlCommand(query2, con);
                        cmd2.Parameters.AddWithValue("@Call_Cost_Id", insertedID);
                        cmd2.Parameters.AddWithValue("@DialNumber", s);

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
        }
        public bool UpdateIntoCosting(CostingEntity CostingEntityObj)
        {
            try
            {
             
                string query = "Update call_cost SET CostType=@CostType,CostPerSec=@CostPerSec,ConnectCost=@ConnectCost,CallSetupCost=@CallSetupCost where Id=@Id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", CostingEntityObj.Id);
                cmd.Parameters.AddWithValue("@CostType", CostingEntityObj.CostType);
                cmd.Parameters.AddWithValue("@CostPerSec", CostingEntityObj.CostPerSec);
                cmd.Parameters.AddWithValue("@ConnectCost", CostingEntityObj.ConnectCost);
                cmd.Parameters.AddWithValue("@CallSetupCost", CostingEntityObj.CallSetupCost);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                string query1 = "Delete From call_cost_numbers where Call_Cost_Id=" + CostingEntityObj.Id + "";
                MySqlCommand cmd1 = new MySqlCommand(query1, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
                string[] DialNumber = CostingEntityObj.DialNumber.Split(';');

                foreach (string s in DialNumber)
                {
                    if (s != "")
                    {
                        string query2 = "Insert into call_cost_numbers (Call_Cost_Id,DialNumber) values(@Call_Cost_Id,@DialNumber)";
                        MySqlCommand cmd2 = new MySqlCommand(query2, con);
                        cmd2.Parameters.AddWithValue("@Call_Cost_Id", CostingEntityObj.Id);
                        cmd2.Parameters.AddWithValue("@DialNumber", s);

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
        }
        public bool DeleteFromCosting(int CostingId)
        {
            try
            {
                string query = "Delete From call_cost_numbers where Call_Cost_Id=" + CostingId + "";
                MySqlCommand cmd = new MySqlCommand(query, con);
               
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                string query1 = "Delete From call_cost where Id=" + CostingId + "";
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
        public List<string> GetDialNumbersOnCostingId(int CostingId)
        {
            try
            {
                List<string> DialNumbersList = new List<string>();
                DataTable dt = new DataTable();
                string query = "SELECT* from call_cost_numbers where Call_Cost_Id=" + CostingId + "";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    string DialNumber = row["DialNumber"].ToString();
                    DialNumbersList.Add(DialNumber);
                }

                return DialNumbersList;
            }
            catch (Exception ex)
            {
                con.Close();
                LogApp.Log4Net.WriteException(ex);
                return null;

            }
        }
       
        public bool CheckIfCostingNameAlreadyExists(int CostingId, string CostType)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "select * from call_cost where Id != " + CostingId + " And CostType='" + CostType + "'";
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