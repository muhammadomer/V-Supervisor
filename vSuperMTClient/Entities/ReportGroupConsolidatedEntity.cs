using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportGroupConsolidatedEntity
    {
        //public string StartDate { get; set; }
        //public string Title { get; set; }
        //public string AvgWaitTime { get; set; }

        //public string LongestWaitAnswer { get; set; }
        //public string AvgAbandonedTime { get; set; }
        //public string LongestWaitAbandoned { get; set; }
        //public string TotalInternalTime { get; set; } //Internal Handling Time

        //public Double AVGInternalTime { get; set; } //Av. Internal Call Time
        //public Double AbandonedPer { get; set; }
        //public Double SLAPer { get; set; } //Service Level
        //public Double AvgCallsHour { get; set; }
        //public Double AvgAvailableTime { get; set; } //Average FTE Available
        //public Double AvgHold { get; set; }


        //public int TotalCalls { get; set; }
        //public int TotalAnswered { get; set; }
        //public int TotalAbanodoned { get; set; }
        //public int TransferACD { get; set; } //Total Transferred Out 
        //public int TotalInternalCall { get; set; }// Total Internal made
        //public int TotalTransfersIn { get; set; }
        //public int LoggedInCount { get; set; } //Total Staff Logged in


        /////

        public string StartDate { get; set; }
        public string Title { get; set; }
        public string AvgWaitTime { get; set; }

        public string LongestWaitAnswer { get; set; }
        public string AvgWaitAbandonedTime { get; set; } 
        public string LongestWaitAbandoned { get; set; }
        public string TotalInternalTime { get; set; } //Internal Handling Time

        public string AVGInternalTime { get; set; } //Av. Internal Call Time
        public string AbandonedPer { get; set; }
        public string SLAPer { get; set; } //Service Level
        public string AvgCallsHour { get; set; }
        public string AvgAvailableTime { get; set; } //Average FTE Available
        public string AvgHold { get; set; }


        public string TotalCalls { get; set; }
        public string TotalAnswered { get; set; }
        public string TotalAbanodoned { get; set; }
        public string TransferACD { get; set; } //Total Transferred Out 
        public string TotalInternalCall { get; set; }// Total Internal made
        public string TotalTransfersIn { get; set; }
        public string LoggedInCount { get; set; } //Total Staff Logged in


    }
}