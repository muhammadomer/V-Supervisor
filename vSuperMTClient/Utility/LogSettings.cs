using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using LogApp;
namespace vSuperMTClient
{
    public class AlertMessage
    {
        public static void Show(string Message)
        {
            try
            {
                System.Web.UI.Page oPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
                Type oType = oPage.GetType();
                string scriptText = "ShowMessage(\"" + Message + "\");";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(scriptText);
                System.Web.UI.ScriptManager.RegisterStartupScript(oPage, oType, "clientscript2", sb.ToString(), true);
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
        }
    }

    public class LogSettings
    {
        private static string GetAppConfigValue(string Key)
        {
            string strReturn = "";
            try
            {
                //Log4Net.WriteLog("Key to be found in the config [ " + Key + " ]", LogType.GENERALLOG);
                strReturn = System.Configuration.ConfigurationSettings.AppSettings[Key];
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
            //Log4Net.WriteLog("Returning value is [ " + strReturn.ToString() + " ]", LogType.GENERALLOG);
            return strReturn;
        }

        private static bool Get_General_Log()
        {
            bool bReturn = false;
            try
            {
                bReturn = Convert.ToBoolean(GetAppConfigValue("LogGENERAL"));
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
            Log4Net.WriteLog("Returning value is [ " + bReturn.ToString() + " ]", LogType.GENERALLOG);
            return bReturn;
        }

        private static bool Get_Error_Log()
        {
            bool bReturn = false;
            try
            {
                bReturn = Convert.ToBoolean(GetAppConfigValue("LogERROR"));
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
            Log4Net.WriteLog("Returning value is [ " + bReturn.ToString() + " ]", LogType.GENERALLOG);
            return bReturn;
        }

        private static bool Get_DB_Log()
        {
            bool bReturn = false;
            try
            {
                bReturn = Convert.ToBoolean(GetAppConfigValue("LogDB"));
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
            Log4Net.WriteLog("Returning value is [ " + bReturn.ToString() + " ]", LogType.GENERALLOG);
            return bReturn;
        }

        private static string Get_Log_Path()
        {
            string strReturn = "";
            try
            {
                strReturn = GetAppConfigValue("LogPath");
                if (!Directory.Exists(strReturn))
                {
                    strReturn = System.Web.HttpContext.Current.Server.MapPath(".") + @"\Logs";
                }
            }
            catch (Exception ex)
            {
                strReturn = System.Web.HttpContext.Current.Server.MapPath(".") + @"\Logs";
                Log4Net.WriteException(ex);
            }
            Log4Net.WriteLog("Returning value is [ " + strReturn.ToString() + " ]", LogType.GENERALLOG);
            return strReturn;
        }

        private static string Get_Log_Name()
        {
            string strReturn = "";
            try
            {
                strReturn = GetAppConfigValue("LogFile");
                if (strReturn.Trim() == "")
                {
                    strReturn = "Log";
                }
            }
            catch (Exception ex)
            {
                strReturn = "Log";
                Log4Net.WriteException(ex);
            }
            Log4Net.WriteLog("Returning value is [ " + strReturn.ToString() + " ]", LogType.GENERALLOG);
            return strReturn + ".txt";
        }

        private static int Get_Log_Size()
        {
            int iReturn = 5;
            try
            {
                string strReturn = GetAppConfigValue("LogSize");
                int iVal = 5;
                try
                {
                    iVal = Convert.ToInt32(strReturn);
                    if (iVal < 1 && iVal > 20)
                        iVal = 5;
                }
                catch
                {
                    iVal = 5;
                    Log4Net.WriteLog("Invalid value found in config.", LogType.ERRORLOG);
                }
                iReturn = iVal;
            }
            catch (Exception ex)
            {
                iReturn = 5;
                Log4Net.WriteException(ex);
            }
            Log4Net.WriteLog("Returning value is [ " + iReturn.ToString() + " ]", LogType.GENERALLOG);
            return iReturn;
        }

        private static int Get_Log_Numbers()
        {
            int iReturn = 10;
            try
            {
                string strReturn = GetAppConfigValue("LogTotalFiles");
                int iVal = 10;
                try
                {
                    iVal = Convert.ToInt32(strReturn);
                    if (iVal < 5 && iVal > 100)
                        iVal = 20;
                }
                catch
                {
                    iVal = 10;
                    Log4Net.WriteLog("Invalid value found in config.", LogType.ERRORLOG);
                }
                iReturn = iVal;
            }
            catch (Exception ex)
            {
                iReturn = 10;
                Log4Net.WriteException(ex);
            }
            Log4Net.WriteLog("Returning value is [ " + iReturn.ToString() + " ]", LogType.GENERALLOG);
            return iReturn;
        }

        public static void Initialize()
        {
            try
            {
                Log4Net.FilePath = Get_Log_Path();
                Log4Net.FileName = Get_Log_Name();
                Log4Net.EnableDBLOG = Get_DB_Log();
                Log4Net.EnableERRORLOG = Get_Error_Log();
                Log4Net.EnableGENERALLOG = Get_General_Log();
                Log4Net.FileSize = Get_Log_Size();
                Log4Net.TotalFiles = Get_Log_Numbers();
                Log4Net.Activate(true);
                Log4Net.WriteLog("Application Log initiazlize successfully", LogType.GENERALLOG);
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
        }

        public static void InitializeDB(bool enable)
        {
            try
            {
                Log4Net.EnableDBLOG = enable;
                Log4Net.EnableERRORLOG = enable;
                Log4Net.EnableGENERALLOG = enable;
                Log4Net.EnableCOMMSLOG = enable;
                Log4Net.WriteLog("Application Log db enable = " + enable.ToString(), LogType.GENERALLOG);
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
        }
    }
}