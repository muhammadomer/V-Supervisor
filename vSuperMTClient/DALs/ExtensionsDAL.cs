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
    public class ExtensionsDAL
    {
        MySqlConnection con;
        public ExtensionsDAL(string ClientDB)
        {
            string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
            con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB));
        }

        public List<CloudNumberingPlan> GetNumberingPlanList(string ClientDB, string CloudDB)
        {
            try
            {
                var AccountId = ClientDB.Substring(ClientDB.LastIndexOf('_') + 1);
                //DbRawSqlQuery<string> data = obj.Database.SqlQuery<string>("Select AccountId from vCloud.accounts where UserId='" + Client + "' or UserFriendlyId='"+Client+ "' and IsDeleted=false");
                Log4Net.WriteLog("Numbering Plan : AccountId:" + AccountId + "     CloudDB:" + CloudDB, LogType.GENERALLOG);
                string strQuery = "SELECT Prefix,CONCAT(Prefix, StartRange) as StartRange, CONCAT(Prefix, EndRange) as EndRange FROM " + CloudDB + ".numberingplan where AccountId='" + AccountId + "'";
                Log4Net.WriteLog("Query = " + strQuery, LogType.GENERALLOG);

                List<CloudNumberingPlan> lstCloudNumberingPlan = new List<CloudNumberingPlan>();

                MySqlCommand cmd = new MySqlCommand(strQuery, con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow row in dt.Rows)
                {
                    var objCloudNumberingPlan = new CloudNumberingPlan();
                    objCloudNumberingPlan.Prefix = row["Prefix"].ToString();
                    objCloudNumberingPlan.StartRange = Convert.ToInt32(row["StartRange"]);
                    objCloudNumberingPlan.EndRange = Convert.ToInt32(row["EndRange"]);
                    lstCloudNumberingPlan.Add(objCloudNumberingPlan);
                }
                return lstCloudNumberingPlan;

            }            
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
        }

        public List<ExtensionsEntity> GetExtensions()
        {
            try
            {
                List<ExtensionsEntity> ExtensionsEntityList = new List<ExtensionsEntity>();
                MySqlCommand cmd = new MySqlCommand("Select * From Extensions where IsDeleted=false order by Extension", con);
                DataTable dt = new DataTable();
                DataTable dtDept = new DataTable();
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    ExtensionsEntity ExtensionEntityObj = new ExtensionsEntity();
                    ExtensionEntityObj.Id = Convert.ToInt32(row["Id"].ToString());
                    ExtensionEntityObj.Extension = row["Extension"].ToString();
                    ExtensionEntityObj.FirstName = row["FirstName"].ToString();
                    ExtensionEntityObj.LastName = row["LastName"].ToString();
                    ExtensionEntityObj.IsDashboard = DBNull.Value == row["IsDashboard"] ? true : Convert.ToBoolean(row["IsDashboard"]);
                    ExtensionEntityObj.IsConsole = DBNull.Value == row["IsConsole"] ? false : Convert.ToBoolean(row["IsConsole"]);
                    ExtensionEntityObj.Email = DBNull.Value == row["Email"] ? string.Empty : row["Email"].ToString();
                    ExtensionEntityObj.Enabled = Convert.ToBoolean(row["Enabled"]);
                  // ExtensionEntityObj.IsDashboard = Convert.ToBoolean(row["IsDashboard"]);
                    cmd = new MySqlCommand("Select * From DepartmentExtensions where ExtensionId=" + ExtensionEntityObj.Id, con);
                    da = new MySqlDataAdapter(cmd);
                    dtDept = new DataTable();
                    da.Fill(dtDept);
                    ExtensionEntityObj.Departments = new List<Department>();
                    foreach (DataRow dept in dtDept.Rows)
                    {
                        ExtensionEntityObj.Departments.Add(new Department
                        {
                            DepartmentId = Convert.ToInt32(dept["DepartmentId"].ToString())
                        });
                    }
                    ExtensionsEntityList.Add(ExtensionEntityObj);
                }

                con.Close();
                return ExtensionsEntityList;
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

        public List<ContactEntity> GetContacts()
        {
            try
            {
                List<ContactEntity> ExtensionsEntityList = new List<ContactEntity>();
                MySqlCommand cmd = new MySqlCommand("Select * From Contacts order by FirstName", con);
                DataTable dt = new DataTable();
                DataTable dtDept = new DataTable();
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    ContactEntity ExtensionEntityObj = new ContactEntity();
                    ExtensionEntityObj.Id = Convert.ToInt32(row["Id"].ToString());                   
                    ExtensionEntityObj.FirstName = DBNull.Value == row["FirstName"] ? string.Empty : row["FirstName"].ToString();
                    ExtensionEntityObj.LastName = DBNull.Value == row["LastName"] ? string.Empty : row["LastName"].ToString();
                    ExtensionEntityObj.Company = DBNull.Value == row["Company"] ? string.Empty : row["Company"].ToString();
                    ExtensionEntityObj.PhoneHome = DBNull.Value == row["PhoneHome"] ? string.Empty : row["PhoneHome"].ToString();
                    ExtensionEntityObj.PhoneMobile = DBNull.Value == row["PhoneMobile"] ? string.Empty : row["PhoneMobile"].ToString();
                    ExtensionEntityObj.PhoneWork = DBNull.Value == row["PhoneWork"] ? string.Empty : row["PhoneWork"].ToString();
                    ExtensionsEntityList.Add(ExtensionEntityObj);
                }

                con.Close();
                return ExtensionsEntityList;
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

        public List<ExtensionsEntity> GetExtensionsWithCallslogging()
        {
            try
            {
                List<ExtensionsEntity> ExtensionsEntityList = new List<ExtensionsEntity>();
                MySqlCommand cmd = new MySqlCommand("Select Extension, FirstName, LastName From Extensions where IsDeleted=false union all Select distinct extension, FirstName, LastName from callslogging where FirstName = 'Hunt' and LastName = 'Group'; ", con);
                DataTable dt = new DataTable();
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    ExtensionsEntity ExtensionEntityObj = new ExtensionsEntity();
                    
                    ExtensionEntityObj.Extension = row["Extension"].ToString();
                    ExtensionEntityObj.FirstName = row["FirstName"].ToString();
                    ExtensionEntityObj.LastName = row["LastName"].ToString();
                    
                    ExtensionsEntityList.Add(ExtensionEntityObj);
                    
                }
                
                con.Close();
                return ExtensionsEntityList;
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
        public string AddExtension(ExtensionsEntity ExtensionEntityObj)
        {
            try
            {
                string query = "insert into Extensions(Extension,FirstName,LastName,Enabled,Email,IsDashboard,IsConsole) values(@Extension,@FirstName,@LastName,@Enabled,@Email,@IsDashboard,@IsConsole); select LAST_INSERT_ID();";
               
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Extension", ExtensionEntityObj.Extension); 
                cmd.Parameters.AddWithValue("@FirstName", ExtensionEntityObj.FirstName);
                cmd.Parameters.AddWithValue("@LastName", ExtensionEntityObj.LastName);
                cmd.Parameters.AddWithValue("@Enabled", ExtensionEntityObj.Enabled);
                cmd.Parameters.AddWithValue("@IsDashboard", ExtensionEntityObj.IsDashboard);
                cmd.Parameters.AddWithValue("@IsConsole", ExtensionEntityObj.IsConsole);
                cmd.Parameters.AddWithValue("@Email", ExtensionEntityObj.Email);
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

        public string AddContact(ContactEntity ContactEntityObj)
        {
            try
            {
                string query = "insert into Contacts(FirstName,LastName,Company,PhoneWork,PhoneHome,PhoneMobile) values(@FirstName,@LastName,@Company,@PhoneWork,@PhoneHome,@PhoneMobile); select LAST_INSERT_ID();";

                MySqlCommand cmd = new MySqlCommand(query, con);             
                cmd.Parameters.AddWithValue("@FirstName", ContactEntityObj.FirstName);
                cmd.Parameters.AddWithValue("@LastName", ContactEntityObj.LastName);
                cmd.Parameters.AddWithValue("@Company", ContactEntityObj.Company);
                cmd.Parameters.AddWithValue("@PhoneHome", ContactEntityObj.PhoneHome);
                cmd.Parameters.AddWithValue("@PhoneMobile", ContactEntityObj.PhoneMobile);
                cmd.Parameters.AddWithValue("@PhoneWork", ContactEntityObj.PhoneWork);
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
        public bool UpdateExtensionOnId(ExtensionsEntity ExtensionEntityObj)
        {
            try
            {
                string query = "Update Extensions Set Extension=@Extension,FirstName=@FirstName,LastName=@LastName,Enabled=@Enabled,Email=@Email,IsDashboard=@IsDashboard,IsConsole=@IsConsole where Id=" + ExtensionEntityObj.Id + "";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Extension", ExtensionEntityObj.Extension);
                cmd.Parameters.AddWithValue("@FirstName", ExtensionEntityObj.FirstName);
                cmd.Parameters.AddWithValue("@LastName", ExtensionEntityObj.LastName);
                cmd.Parameters.AddWithValue("@Enabled", ExtensionEntityObj.Enabled);
                cmd.Parameters.AddWithValue("@IsDashboard", ExtensionEntityObj.IsDashboard);
                cmd.Parameters.AddWithValue("@IsConsole", ExtensionEntityObj.IsConsole);
                cmd.Parameters.AddWithValue("@Email", ExtensionEntityObj.Email);
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
        public bool UpdateContactOnId(ContactEntity ContactEntityObj)
        {
            try
            {
                string query = "Update Contacts Set FirstName=@FirstName,LastName=@LastName,Company=@Company,PhoneWork=@PhoneWork,PhoneHome=@PhoneHome,PhoneMobile=@PhoneMobile where Id=" + ContactEntityObj.Id + "";
                MySqlCommand cmd = new MySqlCommand(query, con);               
                cmd.Parameters.AddWithValue("@FirstName", ContactEntityObj.FirstName);
                cmd.Parameters.AddWithValue("@LastName", ContactEntityObj.LastName);
                cmd.Parameters.AddWithValue("@Company", ContactEntityObj.Company);
                cmd.Parameters.AddWithValue("@PhoneHome", ContactEntityObj.PhoneHome);
                cmd.Parameters.AddWithValue("@PhoneMobile", ContactEntityObj.PhoneMobile);
                cmd.Parameters.AddWithValue("@PhoneWork", ContactEntityObj.PhoneWork);
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

        public List<Department> GetAllDepartments()
        {
            try
            {
                List<Department> entitylist = new List<Department>();
                DataTable dt = new DataTable();
                string query = "select 	DepartmentId, Name, DepartmentAbr from Departments ";
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    Department entity = new Department();
                    entity.DepartmentId = Convert.ToInt32(row["DepartmentId"].ToString());
                    entity.Name = Convert.ToString(row["Name"].ToString());
                    entity.DepartmentAbr = Convert.ToString(row["DepartmentAbr"].ToString());

                    entitylist.Add(entity);
                }
                con.Close();
                return entitylist;
            }
            catch (Exception)
            {
                return null;
            }


        }
        public bool DeleteExtensionOnId(int ExtensionId)
        {
            try
            {
                string query = "Update Extensions Set IsDeleted=@IsDeleted where Id=" + ExtensionId + "";
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

        public bool DeleteContactOnId(int Id)
        {
            try
            {
                string query = "delete from contacts where Id=@Id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", Id);
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
        public bool AddExtensionDepartment(List<Department> Departments,string extensionId)
        {
            try
            {
                // delete exisiting records
                string deleteQuery = "delete from departmentextensions where ExtensionId = " + extensionId;
                MySqlCommand cmdDelete = new MySqlCommand(deleteQuery, con);
                con.Open();
                cmdDelete.ExecuteScalar();
                //int insertedID = Convert.ToInt32(.ToString());
                //cmd.ExecuteNonQuery();
                con.Close();
                foreach (var item in Departments)
                {

                    string query = "insert into departmentextensions(DepartmentId,ExtensionId) values(@DepartmentId,@ExtensionId); select LAST_INSERT_ID();";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@DepartmentId", item.DepartmentId);
                    cmd.Parameters.AddWithValue("@ExtensionId", extensionId);
                    con.Open();
                    cmd.ExecuteScalar();
                    //int insertedID = Convert.ToInt32(.ToString());
                    //cmd.ExecuteNonQuery();
                    con.Close();
                }
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
        public ExtensionsEntity GetExtensionOnId(int ExtensionId)
        {
            try
            {
                ExtensionsEntity ExtensionEntityObj = new ExtensionsEntity();
                MySqlCommand cmd = new MySqlCommand("Select* From Extensions where Id=" + ExtensionId + "", con);
                DataTable dt = new DataTable();
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    ExtensionEntityObj.Id = Convert.ToInt32(row["Id"].ToString());
                    ExtensionEntityObj.Extension = row["Extension"].ToString();
                    ExtensionEntityObj.FirstName = row["FirstName"].ToString();
                    ExtensionEntityObj.LastName = row["LastName"].ToString();
                    ExtensionEntityObj.Enabled = Convert.ToBoolean(row["Enabled"]);
                    ExtensionEntityObj.IsDashboard = DBNull.Value == row["IsDashboard"] ? true : Convert.ToBoolean(row["IsDashboard"]);
                    ExtensionEntityObj.IsConsole = DBNull.Value == row["IsConsole"] ? false : Convert.ToBoolean(row["IsConsole"]);
                    ExtensionEntityObj.Email = DBNull.Value == row["Email"] ? string.Empty : row["Email"].ToString();
                }
                con.Close();

                return ExtensionEntityObj;
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

        public bool CheckIfExtensionAlreadyExists(int id, string extension)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select * From Extensions where Extension='" + extension + "' and Id!=" + id + " and IsDeleted=false" , con);
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

        public bool CheckIfPhoneAlreadyExists(int id, string number)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select * From Contacts where (PhoneWork='" + number + "' or PhoneHome='" + number + "' or PhoneMobile='" + number + "') and Id!=" + id + "", con);
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

    [Serializable()]
    public class CloudNumberingPlan
    {
        public string Prefix { get; set; }
        public int StartRange { get; set; }
        public int EndRange { get; set; }

    }
}