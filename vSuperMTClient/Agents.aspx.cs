using LogApp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using vSuperMTClient.DALs;
using vSuperMTClient.Entities;

namespace vSuperMTClient
{
    public partial class Agents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        [ScriptMethod]
        public static SocketAuthenticationEntity GetSocketAuthentications()
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["UFID"] != null)
            {
                try
                {
                    UsersEntity UsersEntityObj = (UsersEntity)HttpContext.Current.Session["User"];
                    string UFID = HttpContext.Current.Session["UFID"].ToString();
                    string ServerIp = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                    string Port = ConfigurationManager.AppSettings["WSPort"];
                    //string WSServerUri = "ws://" + ServerIp + ":" + Port + "";

                    //string WSServerUri = "ws://192.168.2.45:" + Port + "";
                    string protocol = "ws://";
                    if (HttpContext.Current.Request.IsSecureConnection)
                    {
                        protocol = "wss://";
                    }

                    string WSServerUri = protocol + ServerIp + ":" + Port + "";
                   // string WSServerUri = "ws://192.168.0.53:" + Port + "";
                    
                    SocketAuthenticationEntity SocketAuthenticationEntityObj = new SocketAuthenticationEntity();
                    SocketAuthenticationEntityObj.UFID = UFID;
                    SocketAuthenticationEntityObj.UserName = UsersEntityObj.UserName;
                    SocketAuthenticationEntityObj.Password = UsersEntityObj.Password;
                    SocketAuthenticationEntityObj.WSURI = WSServerUri;
                    SocketAuthenticationEntityObj.ServerIp = ServerIp;
                    return SocketAuthenticationEntityObj;
                }
                catch (Exception ex)
                {
                    LogApp.Log4Net.WriteException(ex);
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        [WebMethod]
        [ScriptMethod]

        public static string saveAgentDisplay(AgentDisplayEntity agentDisplayObj)
        {
            try
            {

                UsersEntity UsersEntityObj = (UsersEntity)HttpContext.Current.Session["User"];

                agentDisplayObj.UserId = UsersEntityObj.UserId;



                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                SettingsDAL settingsDAL = new SettingsDAL(ClientDB);

                settingsDAL.UpdateAgentDisplay(agentDisplayObj);
                return "1";


                // string queslist= String.Join(", ", agentDisplayObj.QueueList.ToArray<string>());

            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return "0";

            }

        }
        [WebMethod]
        [ScriptMethod]
        public static AgentDisplayEntity GetAgentDisplay()
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    UsersEntity UsersEntityObj = (UsersEntity)HttpContext.Current.Session["User"];


                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();

                    SettingsDAL SettingssDALObj = new SettingsDAL(ClientDB);

                    AgentDisplayEntity agentDisplay = new AgentDisplayEntity();
                    agentDisplay = SettingssDALObj.GetAgentDisplay(UsersEntityObj.UserId);

                    return agentDisplay;

                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


    }
}