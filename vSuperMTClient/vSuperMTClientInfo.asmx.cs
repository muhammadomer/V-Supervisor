using LogApp;
using Secure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;

namespace vSuperMTClient
{
    ///// <summary>
    ///// Summary description for vSuperMTClientInfo
    ///// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    //// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //// [System.Web.Script.Services.ScriptService]
    public class vSuperMTClientInfo : System.Web.Services.WebService
    {

        [WebMethod]
        public bool TData(string DataString)
        {
            try
            {
                string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
                string DataStringDEC = Decrypt(DataString, "vSuperTr1@l");
                string AccountId = DataStringDEC.Split(new string[] { "$$$" }, StringSplitOptions.None)[0];
                DateTime DateTime = DateTime.Parse(DataStringDEC.Split(new string[] { "$$$" }, StringSplitOptions.None)[1]);
                int TrialDays = int.Parse(DataStringDEC.Split(new string[] { "$$$" }, StringSplitOptions.None)[2]);
                string ClientDB = vSupervisorDB+"_" + AccountId;
                string ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB);
                LicInformation objLicInformation = new LicInformation(ConnectionString, ClientDB);
                objLicInformation.GenerateTrialLicenseModule(TrialDays,LicInformation.ServerLicense.vSUPERVISORCallLogging);
                return true;
            }
            catch (Exception ex) 
            {
                Log4Net.WriteException(ex);
                return false;
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
    }
}
