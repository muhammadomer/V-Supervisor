using LogApp;
using Secure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using vSuperMTClient.Entities;

namespace vSuperMTClient
{
    public class AvailableLicenses
    {
        public string License { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
    public class License
    {
        public string LicenseID { get; set; }
        public string Module { get; set; }
        public string ModuleCode { get; set; }
        public string Type { get; set; }
        public string Quantity { get; set; }
        public string TimePeriod { get; set; }
        public string PCKey { get; set; }
    }
    public partial class Licensing : System.Web.UI.Page
    {
        public LicInformation objLicInformation;
        public int ShowLicensing;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null && Session["vSupervisorDB"] != null)
            {
                try
                {
                    
                    if (!IsPostBack)
                    {
                        PageNotPostBack();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupScript", "OnFieldNameChange();", true);
                    }

                    string ClientDB = Session["vSupervisorDB"].ToString();
                    string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
                    string ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB);
                    objLicInformation = new LicInformation(ConnectionString, ClientDB);
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnCreateLicenseKey);
                    if (Session["SuperAdmin"] != null || Session["OnPremise"] != null)
                    {
                        ShowLicensing = 1;
                    }
                    else
                    {
                        ShowLicensing = 0;
                    }
                   
                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        private void PageNotPostBack()
        {
            try
            {
                List<License> LicenseList = new List<License>();
                Session["GridViewData"] = LicenseList;
                Log4Net.WriteLog("Page called with URL [ " + Request.Url.ToString() + " ]", LogType.GENERALLOG);
                cboLicense.Focus();
                cboLicense.Attributes.Add("onkeydown", "return OnFieldNameChange();");
                cboLicense.Attributes.Add("onkeyup", "return OnFieldNameChange();");
                cboLicense.Attributes.Add("onchange", "return OnFieldNameChange();");
                PopulateComboBoxes();
                CheckLicenseStatus();
                //MakeLicenseTable();
                Page oPage = (Page)HttpContext.Current.Handler;
                Type oType = oPage.GetType();
                string scriptText = "OnFieldNameChange();";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(scriptText);
                ScriptManager.RegisterStartupScript(oPage, oType, "StartScript", sb.ToString(), true);
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
        }
        private void PopulateComboBoxes()
        {
            try
            {
                cboQuantity.Items.Add(new ListItem("----- Select Quantity -----", "0"));
                for (int i = 1; i <= 99; i++)
                {
                    cboQuantity.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }

            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
        }
        private void MakeLicenseTable()
        {
            try
            {
                string strFilePath = Request.PhysicalApplicationPath + "License";

                if (!Directory.Exists(strFilePath))
                    Directory.CreateDirectory(strFilePath);

                strFilePath += "\\PCKey.lic";
                if (!File.Exists(strFilePath))
                {
                    FileInfo fi = new FileInfo(strFilePath);
                    fi.Create();
                    fi.Refresh();
                    fi = null;
                }

            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
        }
        private void CheckLicenseStatus()
        {
            try
            {
                string ClientDB = Session["vSupervisorDB"].ToString();
                string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
                string ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB);
                LicInformation objLicInformation = new LicInformation(ConnectionString, ClientDB);

                cboLicense.Items.Clear();
                cboLicense.Items.Add(new ListItem("----- Select License -----", "0"));
                foreach (LicInformation.ServerLicense serverLicense in LicInformation.EnumToList<LicInformation.ServerLicense>())
                {
                    if (objLicInformation.serverLicenseStatus(serverLicense) != LicInformation.ServerStatus.Full)
                    {
                        EnumDescriptionData objEnumData = LicInformation.GetEnumDescription(serverLicense);
                        cboLicense.Items.Add(new ListItem(objEnumData.Name, LicInformation.LicenseType.Server.ToString() + "," + objEnumData.Code));         //Key in value
                    }
                }

                foreach (LicInformation.ClientLicense clientLicense in LicInformation.EnumToList<LicInformation.ClientLicense>())
                {
                    EnumDescriptionData objEnumData = LicInformation.GetEnumDescription(clientLicense);
                    cboLicense.Items.Add(new ListItem(objEnumData.Name, LicInformation.LicenseType.Client.ToString() + "," + objEnumData.Code));         //Key in value
                }
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
        }
       
        protected void AsyncFileUpload1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            try
            {

                if (AsyncFileUpload1.HasFile)
                {
                    if (e.FileName.EndsWith(".lic"))
                    {
                        string FilePath = MapPath("~/License/") + "LicenseKey.lic";
                        AsyncFileUpload1.SaveAs(FilePath);
                        Session["SavePath"] = FilePath;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "LicensceUploadInvalidLicense", "notifyMe('showErrorToast',\"Only 'lic' extension files supported.\");", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
        }
    
        private void UpdateLicenseKey(ArrayList aLicense)
        {
            try
            {
                bool TrialLicense = false;
                bool ShowMessage = false;
                bool OldLicenseFile = false;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
                Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                foreach (string strLicense in aLicense)
                {
                    string strAppliedKey = strLicense.Split('[')[1].Split(']')[0];
                    string[] strLicenseInformation = LicInformation.DecryptData(strAppliedKey);
                    DateTime applyDate = Convert.ToDateTime(strLicenseInformation[0]);
                    string ModuleName = strLicenseInformation[6];
                    bool LicenseSaved = false;
                    bool SkipClient = false;


                    if (strLicenseInformation[5] == LicInformation.LicenseType.Server.ToString())
                    {
                        if (strLicenseInformation[14].ToLower().Contains(LicInformation.ServerStatus.Trial.ToString().ToLower()))
                        {
                            strAppliedKey = objLicInformation.UpdateTrialKey(strAppliedKey, true);
                            SkipClient = true;
                            ShowMessage = true;
                            TrialLicense = true;
                        }
                    }

                    string[] strPreviousLicenseInformation = objLicInformation.getKeyFromXML(ModuleName);
                    if (strPreviousLicenseInformation != null)
                    {
                        Log4Net.WriteLog("Previous license information found in XML", LogType.GENERALLOG);
                        DateTime prevDate = Convert.ToDateTime(strPreviousLicenseInformation[0]);
                        if (prevDate < applyDate && strPreviousLicenseInformation[14].ToLower() != LicInformation.ServerStatus.Full.ToString().ToLower())
                        {
                            objLicInformation.saveValuesInXML(ModuleName, strAppliedKey);
                            //objLicInformation.saveValuesInRegistry(ModuleName, strAppliedKey);
                            LicenseSaved = true;
                        }
                        else
                        {
                            LicenseSaved = OldLicenseFile = true;
                        }
                    }
                    else
                    {
                        Log4Net.WriteLog("Previous license information not found in XML. Checking other information", LogType.GENERALLOG);
                        Log4Net.WriteLog("Saving values in XML", LogType.GENERALLOG);
                        objLicInformation.saveValuesInXML(ModuleName, strAppliedKey);
                        Log4Net.WriteLog("Saving values in other source", LogType.GENERALLOG);
                        //objLicInformation.saveValuesInRegistry(ModuleName, strAppliedKey);
                        LicenseSaved = true;
                    }

                    Log4Net.WriteLog("Save license and skip client: " + LicenseSaved.ToString() + " | " + SkipClient.ToString(), LogType.GENERALLOG);

                    if (LicenseSaved && SkipClient)
                        break;

                    if (LicenseSaved)
                        ShowMessage = true;
                }
                if (ShowMessage && !TrialLicense)
                {
                    LicInformation.ServerStatus obj = objLicInformation.serverLicenseStatus(LicInformation.ServerLicense.vSUPERVISORCallLogging);
                    if (obj != LicInformation.ServerStatus.Full)
                    {
                        //LicInformation.removeServerLicense(LicInformation.ServerLicense.vBoardServer);
                    }
                }
                Log4Net.WriteLog("Loading License information....", LogType.GENERALLOG);
                if (ShowMessage)

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LicensceUploadSuccess", "notifyMe('showSuccessToast','License applied successfully.');", true);
                else

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LicensceUploadFail", "notifyMe('showErrorToast',' Invalid or Old License File.');", true);

            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
        }
        private ArrayList LoadFile(string strFilePath)
        {
            ArrayList aList = new ArrayList();
            bool bValidLicense = true;
            try
            {
                FileInfo fi = new FileInfo(strFilePath);
                if (fi.Length == 0)
                {
                    Log4Net.WriteLog("File has no data", LogType.GENERALLOG);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LicensceUploadNotFound", "notifyMe('showErrorToast',' License not found.');", true);
                }
                else
                {
                    Log4Net.WriteLog("File found, now reading the File", LogType.GENERALLOG);
                    string LicenseKey = "";
                    StreamReader LicenseFile = File.OpenText(strFilePath);

                    Log4Net.WriteLog("Reading a text from file", LogType.GENERALLOG);
                    while ((LicenseKey = LicenseFile.ReadLine()) != null)
                    {
                        if (objLicInformation.isValidLicense(LicenseKey))
                        {
                            aList.Add(LicenseKey);
                        }
                        else
                        {
                            bValidLicense = false;
                            break;
                        }
                    }
                    LicenseFile.Close();
                    LicenseFile = null;
                }
            }
            catch (Exception ex)
            {
                bValidLicense = false;
                Log4Net.WriteException(ex);
            }

            if (!bValidLicense)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "LicensceUploadInvalid", "notifyMe('showErrorToast',' Invalid License Key.');", true);
                aList.Clear();
            }
            return aList;
        }
        [WebMethod]
        [ScriptMethod]
        public static List<License> AddLicenses(string Lic, string Quantity, string LicText)
        {
            Page page = (Page)HttpContext.Current.Handler;
            try
            {
                //List<License> LicenseList = new List<License>();
                List<License> LicenseList = (List<License>)HttpContext.Current.Session["GridViewData"];
                string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
                string ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB);
                LicInformation objLicInformation = new LicInformation(ConnectionString, ClientDB);
                License LicenseObj = new License();
                string strQuantity = "N/A";
                string strLicensePeriod = "N/A";
                string strPreviousLicense = "N/A";
                string NA1 = "N/A";
                string NA2 = "N/A";
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
                Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

                string LicenseType = Lic.Split(',')[0];
                string ModuleCode = Lic.Split(',')[1];


                if (Lic.Contains(LicInformation.LicenseType.Server.ToString()))
                {
                    strLicensePeriod = LicInformation.ServerStatus.Full.ToString();

                    string[] KeyDetail = objLicInformation.getKeyFromXML(LicText);
                    if (KeyDetail != null)
                    {
                        strPreviousLicense = KeyDetail[8];
                    }
                }
                else
                {
                    strQuantity = Quantity;
                    string[] KeyDetail = objLicInformation.getKeyFromXML(LicText);
                    if (KeyDetail != null)
                    {
                        strPreviousLicense = KeyDetail[13];
                    }
                    else
                    {
                        strPreviousLicense = "0";
                    }
                }

                string strAssemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.ToString();
                ArrayList objMACList = Secure.MachineInformtion.GetMACAddress();
                string MACID = objMACList[0].ToString();

                string DatabaseName = HttpContext.Current.Session["vSupervisorDB"].ToString();
                string strRequestKey = DateTime.Now.ToString() + "|Request|" + MACID + "|" + LicText + "|" + LicenseType + "|" + DatabaseName + "|" + strQuantity + "|" + strLicensePeriod + "|" + LicInformation.ApplicationName + "|" + strAssemblyVersion + "|" + LicInformation.ApplicationCode + "|" + NA1 + "|" + NA2 + "|" + Secure.MachineInformtion.GetOSSerial() + "|" + strPreviousLicense;
                string strEncryptedKey = LicInformation.EncryptData(strRequestKey);

                try
                {
                    License License = LicenseList.Where(x => x.Module == LicText).SingleOrDefault();
                    if (License != null)
                    {
                        LicenseList.Remove(License);
                    }
                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                }

                LicenseObj.LicenseID = DateTime.Now.ToString("mmssfff");
                LicenseObj.Module = LicText;
                LicenseObj.ModuleCode = ModuleCode;
                LicenseObj.Type = LicenseType;
                LicenseObj.Quantity = strQuantity;
                LicenseObj.TimePeriod = strLicensePeriod;
                LicenseObj.PCKey = LicInformation.ApplicationName + "," + LicText + "," + LicenseType + "," + strQuantity + "," + strLicensePeriod + "[" + strEncryptedKey + "]";

                if (LicenseType == LicInformation.LicenseType.Server.ToString())
                    LicenseList.Insert(0, LicenseObj);

                else
                    LicenseList.Add(LicenseObj);

                Log4Net.WriteLog("License Information has been added to grid", LogType.GENERALLOG);
                HttpContext.Current.Session["GridViewData"] = LicenseList;
                return LicenseList;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
        }
        [WebMethod]
        [ScriptMethod]
        public static List<AvailableLicenses> GetAvailableLicenses()
        {
            List<AvailableLicenses> AvailableLicensesList = new List<AvailableLicenses>();
            string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
            string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
            string ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB);
            LicInformation objLicInformation = new LicInformation(ConnectionString, ClientDB);
            try
            {
                foreach (LicInformation.ServerLicense serverLicense in LicInformation.EnumToList<LicInformation.ServerLicense>())
                {
                    string ModuleName = LicInformation.GetEnumDescription(serverLicense).Name;
                    string[] KeyDetail = objLicInformation.getKeyFromXML(ModuleName);
                    if (KeyDetail != null)
                    {
                        AvailableLicenses AvailableLicensesObj = new AvailableLicenses();
                        AvailableLicensesObj.License = KeyDetail[6];
                        AvailableLicensesObj.Description = KeyDetail[13];
                        AvailableLicensesObj.Status = KeyDetail[14];
                        if (KeyDetail[14].ToLower() != LicInformation.ServerStatus.Full.ToString().ToLower())
                        {
                            try
                            {
                                AvailableLicensesObj.License = ModuleName;
                                AvailableLicensesObj.Description = "Trial License";
                                DateTime dtSaved = Convert.ToDateTime(KeyDetail[11]);
                                DateTime dtExpire = Convert.ToDateTime(KeyDetail[12]);

                                if (dtSaved >= dtExpire || dtSaved > DateTime.Now)
                                    AvailableLicensesObj.Status = "Expired";//LicInformation.ServerStatus.Expire.ToString();
                                else
                                {
                                    TimeSpan ts = dtExpire - dtSaved;
                                    double days = Math.Round(ts.TotalDays);
                                    AvailableLicensesObj.Status = days + " days left";
                                    if (days < 1)
                                    {
                                        //AvailableLicensesObj.Status = "Less than 24 hours left";
                                        AvailableLicensesObj.Status = (int)ts.TotalHours + " hours left";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Log4Net.WriteException(ex);
                            }
                        }
                        else
                        {
                            AvailableLicensesObj.Description = "Full License";
                        }
                        AvailableLicensesList.Add(AvailableLicensesObj);
                    }

                }
                foreach (LicInformation.ClientLicense clientLicense in LicInformation.EnumToList<LicInformation.ClientLicense>())
                {
                    string ModuleName = LicInformation.GetEnumDescription(clientLicense).Name;
                    string[] KeyDetail = objLicInformation.getKeyFromXML(ModuleName);

                    if (KeyDetail != null)
                    {

                        AvailableLicenses AvailableLicensesObj = new AvailableLicenses();
                        AvailableLicensesObj.License = KeyDetail[6];
                        AvailableLicensesObj.Description = KeyDetail[13] + " License";
                        AvailableLicensesObj.Status = KeyDetail[14];

                        AvailableLicensesList.Add(AvailableLicensesObj);
                    }
                }
                return AvailableLicensesList;

            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
        }

        protected void btnCreateLicenseKey_Click(object sender, EventArgs e)
        {
            try
            {
                List<License> LicenseList = (List<License>)HttpContext.Current.Session["GridViewData"];
                FileStream outputStream = null;
                StreamWriter sWriter = null;
                string filename = "";
                string dirName = Request.PhysicalApplicationPath + "License";
                filename = dirName + @"\PCKey.lic";

                Log4Net.WriteLog("Making PCKeys.lic file at " + filename, LogType.GENERALLOG);
                outputStream = new FileStream(filename, FileMode.Create);

                sWriter = new StreamWriter(outputStream);
                cboLicense.SelectedIndex = 0;
                for (int intRowCounter = 0; intRowCounter < LicenseList.Count; intRowCounter++)
                {
                    Log4Net.WriteLog(intRowCounter.ToString() + ". Writing Key " + LicenseList[intRowCounter].PCKey.ToString(), LogType.GENERALLOG);
                    sWriter.WriteLine(LicenseList[intRowCounter].PCKey.ToString());
                }
                sWriter.Flush();
                sWriter.Close();
                sWriter = null;
                cboLicense.SelectedIndex = 0;
                Session["GridViewData"] = null;
                MakeLicenseTable();
                try
                {
                    Response.AddHeader("Content-Disposition", "attachment;filename=PCKey.lic");
                    Response.TransmitFile(filename);
                    Response.Flush();
                    Response.End();
                }
                catch (Exception exp)
                {
                    Log4Net.WriteException(exp);
                }
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string FilePath = Session["SavePath"].ToString();

                ArrayList aLicense = LoadFile(FilePath);
                if (aLicense.Count > 0)
                    UpdateLicenseKey(aLicense);
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
        }

    }
}