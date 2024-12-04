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
    public class ReportSectionsDAL
    {
        MySqlConnection con;
        public ReportSectionsDAL(string ClientDB)
        {
            string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
            con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB));
        }

        public List<SectionsEntity> GetSectionsOnReportID(int ReportId)
        {
            try
            {
                List<SectionsEntity> SectionsEntityList = new List<SectionsEntity>();

                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("Select S.SID,S.SectionName From Sections S inner join Report_Sections RS on S.SID=RS.SID where RS.RID=" + ReportId + "", con);
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    SectionsEntity SectionsEntityObj = new SectionsEntity();
                    SectionsEntityObj.SID = int.Parse(row["SID"].ToString());
                    SectionsEntityObj.SectionName = row["SectionName"].ToString();
                    SectionsEntityList.Add(SectionsEntityObj);
                }
                con.Close();
                return SectionsEntityList;
            }
            catch (Exception ex)
            {
                con.Close();
                Log4Net.WriteException(ex);
                return null;
            }
        }
    }
}