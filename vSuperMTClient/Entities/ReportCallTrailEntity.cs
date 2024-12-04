using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportCallTrailEntity
    {

        public string Status { get; set; }
        public string MSerial { get; set; }
        public string Extension { get; set; }
        //public string Direction { get; set; }
        public string CLI { get; set; }
        public string DDI { get; set; }
        public string StartTime { get; set; }
        public double HoldDuration { get; set; }
        public double TotalConversation { get; set; }
        public double TotalRingDuration { get; set; }
        public double TotalDuration { get; set; }
        public string MExtension { get; set; }
        //public string MDirection { get; set; }
        public string MCLI { get; set; }
        public string MDDI { get; set; }
        public string MStartTime { get; set; }
        public double MHoldDuration { get; set; }
        public double MTotalConversation { get; set; }
        public double MTotalRingDuration { get; set; }
        public double MTotalDuration { get; set; }
        public string direction{ get; set;}
        public string mdirection { get; set; }

      //  public string OrignalNumber { get; set; }

        public string Direction
        {
            get
            {
                if (this.direction == "1") return "Inbound";
                else if (this.direction == "0") return "Outbound";
                else return "";
            }
        }
        public string MDirection
        {
            get
            {
                if (this.mdirection == "1") return "Inbound";
                else if (this.mdirection == "0") return "Outbound";
                else return "";
            }
        }


    }
}