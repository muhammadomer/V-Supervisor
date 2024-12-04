using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class SettingsEntity
    {

        public int SettingID { get; set; }
        public string SMTPHost { get; set; }
        public string SMTPPort { get; set; }

        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }
        public bool EnableSSL { get; set; }
        public int InternalLength { get; set; }
        public int AgentHangUpThreshold { get; set; }
        public int AgentWorkingHours { get; set; }
        public int RingTimeThreshold { get; set; }
        public string Currency { get; set; }
        public float DiskCostPerMin { get; set; }
        public float DurationCostPerMin { get; set; }
        public string PBXIP { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyLogo { get; set; }
        public float AgentAvalibalityHours { get; set; }
        public int AgentCallsAnsweredWithInNSec { get; set; }
        public int QCallsAnsweredWithInNSec { get; set; }
        public int AgentAvaiabilityScore { get; set; }
        public int AgentCallAnsweredInNSecScore { get; set; }
        public int AgentCallsAnsweredScore { get; set; }

        public int QCallsAnsweredScore { get; set; }
        public int QCallAnsweredInNSecScore { get; set; }
        public int QWaitingTimeInNSec { get; set; }
        public int  QWaitingTimeScore { get; set; }


    }

    public class csPrimaryOutcomeData : IDisposable
    {
        public csPrimaryOutcomeData()
        {
            SecondaryOutComes = new List<csSecondaryOutcomeData>();
        }

        public void Dispose()
        {
            try
            {
                SecondaryOutComes.Clear();
                SecondaryOutComes = null;
            }
            catch (Exception ex)
            {

            }

        }

        public int Id
        {
            get; set;
        }

        public string Code
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public List<csSecondaryOutcomeData> SecondaryOutComes
        {
            get; set;
        }

        public override string ToString()
        {
            return Title;
        }
    }
  
    public class csSecondaryOutcomeData
    {
        public csSecondaryOutcomeData()
        {

        }

        public int Id
        {
            get; set;
        }

        public string Code
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public override string ToString()
        {
            return Title;
        }

    }
}