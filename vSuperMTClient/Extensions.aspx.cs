using LogApp;
using System;
using System.Collections.Generic;
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
    public partial class Extensions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod]
        public static List<ExtensionsEntity> GetExtensions()
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ExtensionsDAL ExtensionsDALObj = new ExtensionsDAL(vBoardClientDB);
                    List<ExtensionsEntity> ExtensionsList = ExtensionsDALObj.GetExtensions();
                    return ExtensionsList;
                }
                catch (Exception ex)
                {

                    LogApp.Log4Net.WriteException(ex);
                    return null;
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return null;
            }
        }
        [WebMethod]
        [ScriptMethod]
        public static List<ContactEntity> GetContacts()
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ExtensionsDAL ExtensionsDALObj = new ExtensionsDAL(vBoardClientDB);
                    List<ContactEntity> ExtensionsList = ExtensionsDALObj.GetContacts();
                    return ExtensionsList;
                }
                catch (Exception ex)
                {

                    LogApp.Log4Net.WriteException(ex);
                    return null;
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return null;
            }
        }
        [WebMethod]
        [ScriptMethod]
        public static string AddExtension(ExtensionsEntity ExtensionsEntityObj)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    string AccountId = vBoardClientDB.Split('_')[1];
                    ExtensionsDAL ExtensionsDALObj = new ExtensionsDAL(vBoardClientDB);
                    if (!ExtensionsDALObj.CheckIfExtensionAlreadyExists(ExtensionsEntityObj.Id, ExtensionsEntityObj.Extension))
                    {
                        if (!CheckIfNumberingPlanExist(ExtensionsEntityObj.Extension))
                        {
                            return "4_Extension not exist in Account numbering plan.";
                        }
                        else
                        {
                            try
                            {
                                string ExtensionAdded = ExtensionsDALObj.AddExtension(ExtensionsEntityObj);
                                // add extension departments
                                bool result = ExtensionsDALObj.AddExtensionDepartment(ExtensionsEntityObj.Departments, ExtensionAdded);
                                if (ExtensionAdded.StartsWith("Failed"))
                                {
                                    return "2_An error occured while adding the extension.";
                                }
                                else
                                {
                                    return "1_" + ExtensionAdded + "_" + AccountId + "";
                                }
                            }
                            catch (Exception ex)
                            {
                                Log4Net.WriteException(ex);
                                return "2_An error occured while adding the extension.";
                            }
                        }
                    }
                    else
                    {
                        return "3_Extension with same name already exists";
                    }



                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "2:An error occured while adding the extension.";
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "";
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static string AddContact(ContactEntity ContactEntityObj)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    string AccountId = vBoardClientDB.Split('_')[1];
                    ExtensionsDAL ExtensionsDALObj = new ExtensionsDAL(vBoardClientDB);
                    if (ContactEntityObj.PhoneWork.Length > 0 && ExtensionsDALObj.CheckIfPhoneAlreadyExists(ContactEntityObj.Id, ContactEntityObj.PhoneWork))
                    {
                        return "3_Phone Work already exists";
                    }
                    else if (ContactEntityObj.PhoneHome.Length > 0 && ExtensionsDALObj.CheckIfPhoneAlreadyExists(ContactEntityObj.Id, ContactEntityObj.PhoneHome))
                    {
                        return "4_Phone_Home already exists";
                    }
                    else if (ContactEntityObj.PhoneMobile.Length > 0 && ExtensionsDALObj.CheckIfPhoneAlreadyExists(ContactEntityObj.Id, ContactEntityObj.PhoneMobile))
                    {
                        return "5_Home Phone already exists";
                    }
                    else
                    {
                        try
                        {
                            string ExtensionAdded = ExtensionsDALObj.AddContact(ContactEntityObj);    
                            if (ExtensionAdded.StartsWith("Failed"))
                            {
                                return "2_An error occured while adding the contact.";
                            }
                            else
                            {
                                return "1_" + ExtensionAdded + "_" + AccountId + "";
                            }
                        }
                        catch (Exception ex)
                        {
                            Log4Net.WriteException(ex);
                            return "2_An error occured while adding the contact.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "2:An error occured while adding the contact.";
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "";
            }
        }

        private static bool CheckIfNumberingPlanExist(string Extension)
        {
            try
            {
                int ext = 0;
                if (HttpContext.Current.Session["OnPremise"] == null && HttpContext.Current.Session["NumberingPlan"] != null)
                {
                    if (int.TryParse(Extension, out ext))
                    {
                        List<CloudNumberingPlan> numberingPlanList = (List<CloudNumberingPlan>)HttpContext.Current.Session["NumberingPlan"];
                        if (numberingPlanList != null && numberingPlanList.Count > 0 && ext > 0)
                        {
                            foreach (var item in numberingPlanList)
                            {
                                if (ext >= item.StartRange && ext <= item.EndRange)
                                    return true;
                            }
                        }
                        else
                            return true;
                    }
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
            }
            return false;
        }

        [WebMethod]
        [ScriptMethod]
        public static string UpdateExtension(ExtensionsEntity ExtensionsEntityObj)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    string AccountId = vBoardClientDB.Split('_')[1];
                    ExtensionsDAL ExtensionsDALObj = new ExtensionsDAL(vBoardClientDB);
                    if (!ExtensionsDALObj.CheckIfExtensionAlreadyExists(ExtensionsEntityObj.Id, ExtensionsEntityObj.Extension))
                    {
                        if (!CheckIfNumberingPlanExist(ExtensionsEntityObj.Extension))
                        {
                            return "4_Extension not exist in Account numbering plan.";
                        }
                        else
                        {
                            try
                            {
                                if (ExtensionsDALObj.UpdateExtensionOnId(ExtensionsEntityObj))
                                {
                                    bool result = ExtensionsDALObj.AddExtensionDepartment(ExtensionsEntityObj.Departments, ExtensionsEntityObj.Id.ToString());

                                    //return "1_Extension updated successfully.";
                                    return "1_" + ExtensionsEntityObj.Id + "_" + AccountId + "";
                                }
                                else
                                {
                                    return "2_An error occured while updating the extension.";
                                }

                            }
                            catch (Exception ex)
                            {
                                Log4Net.WriteException(ex);
                                return "2_An error occured while updating the extension.";
                            }
                        }
                    }
                    else
                    {
                        return "3_Extension with same name already exists";
                    }



                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "2:An error occured while updating the extension.";
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "";
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static string UpdateContact(ContactEntity ContactEntityObj)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    string AccountId = vBoardClientDB.Split('_')[1];
                    ExtensionsDAL ExtensionsDALObj = new ExtensionsDAL(vBoardClientDB);
                    if (ContactEntityObj.PhoneWork.Length > 0 && ExtensionsDALObj.CheckIfPhoneAlreadyExists(ContactEntityObj.Id, ContactEntityObj.PhoneWork))
                    {
                        return "3_Phone Work already exists";
                    }
                    else if (ContactEntityObj.PhoneHome.Length > 0 && ExtensionsDALObj.CheckIfPhoneAlreadyExists(ContactEntityObj.Id, ContactEntityObj.PhoneHome))
                    {
                        return "4_Phone_Home already exists";
                    }
                    else if (ContactEntityObj.PhoneMobile.Length > 0 && ExtensionsDALObj.CheckIfPhoneAlreadyExists(ContactEntityObj.Id, ContactEntityObj.PhoneMobile))
                    {
                        return "5_Home Phone already exists";
                    }
                    else
                    {
                        try
                        {
                            if (ExtensionsDALObj.UpdateContactOnId(ContactEntityObj))
                            {                              
                                return "1_" + ContactEntityObj.Id + "_" + AccountId + "";
                            }
                            else
                            {
                                return "2_An error occured while updating the contact.";
                            }

                        }
                        catch (Exception ex)
                        {
                            Log4Net.WriteException(ex);
                            return "2_An error occured while updating the contact.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "2:An error occured while updating the contact.";
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "";
            }
        }


        [WebMethod]
        [ScriptMethod]
        public static List<Department> GetAllDepartments()
        {
            if (HttpContext.Current.Session["User"] != null)
            {
                try
                {
                    var vIvrDb= System.Configuration.ConfigurationManager.AppSettings["vIvrDB"];
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    string AccountId = vBoardClientDB.Split('_')[1];
                    if (!string.IsNullOrEmpty(AccountId))
                    {
                        vIvrDb = vIvrDb + "_" + AccountId;
                    }
                    ExtensionsDAL ExtensionsDALObj = new ExtensionsDAL(vIvrDb);

                    List<Department> EntityList = new List<Department>();
                    EntityList = ExtensionsDALObj.GetAllDepartments();
                    return EntityList;
                }
                catch (Exception ex)
                {
                    LogApp.Log4Net.WriteException(ex);
                    return null;
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return null;
            }
        }
        [WebMethod]
        [ScriptMethod]
        public static string DeleteExtensionOnId(int ExtensionId)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    string AccountId = vBoardClientDB.Split('_')[1];
                    ExtensionsDAL ExtensionsDALObj = new ExtensionsDAL(vBoardClientDB);

                    if (ExtensionsDALObj.DeleteExtensionOnId(ExtensionId))
                    {
                        //return "1:Extension deleted successfully.";
                        return "1_" + ExtensionId + "_" + AccountId + "";
                    }
                    else
                    {
                        return "2_An error occured while deleting the extension.";
                    }
                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "2_An error occured while deleting the extension.";
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "";
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static string DeleteContactOnId(int Id)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    string AccountId = vBoardClientDB.Split('_')[1];
                    ExtensionsDAL ExtensionsDALObj = new ExtensionsDAL(vBoardClientDB);

                    if (ExtensionsDALObj.DeleteContactOnId(Id))
                    {
                        //return "1:Extension deleted successfully.";
                        return "1_" + Id + "_" + AccountId + "";
                    }
                    else
                    {
                        return "2_An error occured while deleting the contact.";
                    }
                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return "2_An error occured while deleting the contact.";
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return "";
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static ExtensionsEntity GetExtensionOnId(int ExtensionId)
        {
            if (HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["vSupervisorDB"] != null && HttpContext.Current.Session["vBoardDB"] != null)
            {
                try
                {
                    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
                    string vBoardClientDB = HttpContext.Current.Session["vBoardDB"].ToString();
                    ExtensionsDAL ExtensionsDALObj = new ExtensionsDAL(vBoardClientDB);
                    return ExtensionsDALObj.GetExtensionOnId(ExtensionId);
                }
                catch (Exception ex)
                {
                    Log4Net.WriteException(ex);
                    return null;
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
                return null;
            }
        }
    }
}