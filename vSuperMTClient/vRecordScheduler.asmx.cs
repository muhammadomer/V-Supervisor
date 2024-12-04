using LogApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using vSuperMTClient.DALs;
using vSuperMTClient.Entities;

namespace vSuperMTClient
{
    
    public class vRecordScheduler : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public void RunSchedule(int ScheduleId, string AccountId)
        {
            try
            {
                string vRecordDB = System.Configuration.ConfigurationManager.AppSettings["vRecordDB"];
                string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
                Session["vSupervisorDB"] = vSupervisorDB + "_" + AccountId; 
                Session["vRecordDB"] = vRecordDB + "_" + AccountId; 
                vRecordDB = vRecordDB + "_" + AccountId;
                vSupervisorDB = vSupervisorDB + "_" + AccountId;
                ReportScheduleDAL ReportScheduleDALObj = new ReportScheduleDAL(vRecordDB);
                ReportScheduleEntity ReportScheduleEntityObj = ReportScheduleDALObj.GetReportScheduleOnId(ScheduleId, "vRecord");
                string FileName = ReportsCommonMethods.GetReport("PDF",ReportScheduleEntityObj.ReportId, ReportScheduleEntityObj.DateFilterCriteria, ReportScheduleEntityObj.CallsOption, ReportScheduleEntityObj.DateFrom.ToString(), ReportScheduleEntityObj.DateTo.ToString(), ReportScheduleEntityObj.TimeFrom.ToString(), ReportScheduleEntityObj.TimeTo.ToString(), true, "", "", "","", ReportScheduleEntityObj.Users, ReportScheduleEntityObj.WeekDays, ReportScheduleEntityObj.TimeInterval, "vRecord","");
                string Emails = ReportScheduleEntityObj.Emails;
                List<string> EmailIdsList = new List<string>();
                //Email Report
                EmailIdsList = ReportScheduleDALObj.GetEmailIdsOnScheduleId(ScheduleId);

                if (CheckIfSMTPCredentialsAreProvided(vSupervisorDB))
                {
                    SendMail(vSupervisorDB, FileName, EmailIdsList, ReportScheduleEntityObj.Name);
                }
                Schedules.UpdateIntoSchedules(ReportScheduleEntityObj, "vRecord");
                //UpdateNextExecutionTime(ClientDB, ReportScheduleEntityObj);
                Session["vSupervisorDB"] = null;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
        }
        public static string SendMail(string vSupervisorDB, string FileName, List<string> EmailIdsList, string ScheduleName)
        {

            try
            {
                if (EmailIdsList.Count > 0)
                {
                    SettingsDAL SettingsDALObj = new SettingsDAL(vSupervisorDB);
                    SettingsEntity SettingsEntityObj = SettingsDALObj.GetSettings();

                    MailMessage message = new MailMessage();
                    foreach (string To in EmailIdsList)
                    {
                        message.To.Add(new MailAddress(To));
                    }
                    message.From = new MailAddress(SettingsEntityObj.SMTPUserName);
                    message.Subject = "vRecord-" + ScheduleName;
                    message.IsBodyHtml = true;

                    string body = HttpUtility.HtmlDecode("");
                    string FilePath = HttpContext.Current.Server.MapPath(".") + "\\Reports\\" + vSupervisorDB.Split('_')[1] + "\\" + FileName;
                    if (File.Exists(FilePath))
                    {

                        Attachment data = new Attachment(FilePath, MediaTypeNames.Application.Octet);
                        message.Attachments.Add(data);
                    }

                    Log4Net.WriteLog("Mail Credentials:" + SettingsEntityObj.SMTPUserName + ":" + SettingsEntityObj.SMTPHost, LogType.GENERALLOG);
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11;
                    SmtpClient client = new SmtpClient();
                    client.Host = SettingsEntityObj.SMTPHost;
                    client.Port = int.Parse(SettingsEntityObj.SMTPPort);
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(SettingsEntityObj.SMTPUserName, SettingsEntityObj.SMTPPassword);
                    client.EnableSsl = SettingsEntityObj.EnableSSL;
                    client.Send(message);
                    Log4Net.WriteLog("Eamil sent.", LogType.GENERALLOG);
                    return "1";
                }
                else
                {
                    Log4Net.WriteLog("Failed to sent email.", LogType.GENERALLOG);
                    return "0";
                }
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                if (ex.InnerException != null)
                    return "" + ex.Message + ".<br/>" + ex.InnerException.Message + "";
                else
                {
                    return "" + ex.Message + "";
                }
            }
            finally
            {

            }

        }
        public string Decrypt(string SecretKey, string EncryptionKey)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(SecretKey);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        SecretKey = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return SecretKey;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }

        }

        public bool CheckIfSMTPCredentialsAreProvided(string ClientDB)
        {
            try
            {
                SettingsDAL SettingsDALObj = new SettingsDAL(ClientDB);
                return SettingsDALObj.CheckIfSMTPCredentialsAreProvided();
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return false;
            }
        }
    }
}
