using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using LogApp;
using vSuperMTClient.DALs;
using vSuperMTClient.Entities;
using System.Configuration;

using System.Web.Script.Services;
using System.Net;
using System.Net.Sockets;
using Secure;

namespace vSuperMTClient
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
                string vCloudDB = System.Configuration.ConfigurationManager.AppSettings["vCloudDB"];
                string BoardDB = System.Configuration.ConfigurationManager.AppSettings["vBoardDB"];
                string AcdDB = System.Configuration.ConfigurationManager.AppSettings["vAcdDB"];
                string vIvrDB = System.Configuration.ConfigurationManager.AppSettings["vIvrDB"];
                string RecordDB = System.Configuration.ConfigurationManager.AppSettings["vRecordDB"];
                NameValueCollection nvc = Request.Form;
                if (!string.IsNullOrEmpty(nvc["AccountId"]))
                {
                    try
                    {
                        string Account = nvc["AccountId"];
                        string AccountDEC = Decrypt(Account,"SuperAdmin");

                        string AccountId = AccountDEC.Split(new string[] { "$$$" }, StringSplitOptions.None)[0];
                        string UFID = AccountDEC.Split(new string[] { "$$$" }, StringSplitOptions.None)[1];

                        string ClientDb = vSupervisorDB+"_" + AccountId;
                        string vBoardDB = BoardDB + "_" + AccountId;
                        string vAcdDB = AcdDB + "_" + AccountId;
                        string vRecordDB = RecordDB + "_" + AccountId;
                        string IvrDB = vIvrDB + "_" + AccountId;
                        UserDAL userObj = new UserDAL(ClientDb);
                        UsersEntity UsersEntityObj = userObj.getClientAdmin();

                        if (UsersEntityObj != null)
                        {
                            Session["UFID"] = UFID;
                            Session["User"] = UsersEntityObj;
                            Session["vSupervisorDB"] = ClientDb;
                            Session["vBoardDB"] = vBoardDB;
                            Session["vAcdDB"] = vAcdDB;
                            Session["vIvrDB"] = IvrDB;
                            Session["vRecordDB"] = vRecordDB;
                            Session["SuperAdmin"] = "SuperAdmin";
                            //UpgradeLicenseStatus(ClientDb);
                            Log4Net.WriteLog("Adming User LoggedIn", LogType.GENERALLOG);
                            Response.Redirect("Dashboard.aspx");
                        }
                    }
                    catch (Exception ex)
                    {

                        Log4Net.WriteException(ex);
                    }
                }
                else
                {
                    Session["SuperAdmin"] = null;
                    string OnPremise = System.Configuration.ConfigurationManager.AppSettings["OnPremise"];
                    if (OnPremise == "1")
                    {
                        imgLogo.Src = "Content/images/Logo2.png";
                        rowUFID.Visible = false;
                        Session["OnPremise"] = "OnPremise";
                    }
                    else
                    {
                        Session["OnPremise"] = null;
                    }
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                    lblVersion.InnerText = "v" + fvi.FileVersion;
                    lblCopyRights.InnerText = fvi.LegalCopyright;
                    txbUserName.Focus();
                    if (!Page.IsPostBack)
                    {
                        // LoadClients();
                    }
                }
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }

            if (!IsPostBack)
            {
                //SessionUser();
               

            }

        }
        public static string LogMeIn(string UserName, string Password, string Client)
        {
            try
            {
                string SupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
                string BoardDB = System.Configuration.ConfigurationManager.AppSettings["vBoardDB"];
                string IvrDB = System.Configuration.ConfigurationManager.AppSettings["vIvrDB"];
                string AcdDB = System.Configuration.ConfigurationManager.AppSettings["vAcdDB"];
                
                string RecordDB = System.Configuration.ConfigurationManager.AppSettings["vRecordDB"];
                string vCloudDB = System.Configuration.ConfigurationManager.AppSettings["vCloudDB"];
                UserDAL userDALObj = new UserDAL(vCloudDB);
                string DastabaseId = ""; //Account Id 
                if (HttpContext.Current.Session["OnPremise"] == null)
                {
                    DastabaseId = userDALObj.GetDbToConnect(Client, vCloudDB);
                }
                else if (HttpContext.Current.Session["OnPremise"] != null)
                {
                    DastabaseId = userDALObj.GetDbToConnectOnPremise(vCloudDB);
                }
                string UFID = userDALObj.GetUFID(DastabaseId, vCloudDB);
                string vSupervisorDB = SupervisorDB +"_"+ DastabaseId;
                string vBoardDB = BoardDB + "_" + DastabaseId;
                string _IvrDB = IvrDB + "_" + DastabaseId;

                string vAcdDB = AcdDB + "_" + DastabaseId;
                string vRecordDB = RecordDB + "_" + DastabaseId;
                userDALObj = new UserDAL(vSupervisorDB);
                UsersEntity UsersEntityObj = userDALObj.getAdminUserOnUserNameAndPassword(UserName, Password);

                if (UsersEntityObj != null)
                {
                        try
                        {
                            //bool SupervisorLicenseAvailable = isSupervisorLicenseAvailable();
                            bool SupervisorLicenseAvailable = true;
                            if (SupervisorLicenseAvailable == true)
                            {
                                HttpContext.Current.Session["User"] = UsersEntityObj;
                                HttpContext.Current.Session["vSupervisorDB"] = vSupervisorDB;
                                HttpContext.Current.Session["vBoardDB"] = vBoardDB;
                            HttpContext.Current.Session["vIvrDB"] = _IvrDB;

                            HttpContext.Current.Session["vAcdDB"] = vAcdDB;
                            HttpContext.Current.Session["vRecordDB"] = vRecordDB;
                                HttpContext.Current.Session["UFID"] = UFID;
                                Log4Net.WriteLog("Supervisor User Logged In", LogType.GENERALLOG);
                            //UpgradeLicenseStatus(ClientDb);

                            

                            return "Supervisor";
                            }
                            else
                            {
                                return "LicenseExpired";
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            Log4Net.WriteException(ex);
                            return null;
                        }
                }
                else
                {
                  
                        Log4Net.WriteLog("User Entered Wrong Credentionals.Login Failed", LogType.GENERALLOG);
                        return "Invalid Uername or Password";
                    
                }
                
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
        }
        public static void UpgradeLicenseStatus(string ClientDb)
        {
            try
            {
                string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
                string ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDb);
                Secure.LicInformation obj = new Secure.LicInformation(ConnectionString, ClientDb);
               // obj.serverLicenseStatus(Secure.LicInformation.ServerLicense.vBOARDServer);

            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
        }
        public string Decrypt(string SecretKey, string EncryptionKey)
        {
            try
            {
                SecretKey = SecretKey.Replace(" ", "+");
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
        [WebMethod]
        [ScriptMethod]
        public static string LogIn(string UserName, string Password, string Client)
        {
            return LogMeIn(UserName, Password, Client);
        }
        private static bool isSupervisorLicenseAvailable()
        {
            bool bReturn = false;

            try
            {
                LicInformation objLicInformation;
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
                string ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB);
                objLicInformation = new LicInformation(ConnectionString, ClientDB);
                
                //if (objLicInformation.serverLicenseStatus(LicInformation.ServerLicense.vBOARDServer) == LicInformation.ServerStatus.Trial)
                //{
                //    bReturn = true;
                //}

                //else if (objLicInformation.serverLicenseStatus(LicInformation.ServerLicense.vBOARDSUPERVISOR) == LicInformation.ServerStatus.Full)
                //{
                //    bReturn = true;
                //}
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
            return bReturn;
        }



    }
}