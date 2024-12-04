using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{

    public class ReportOutboundSchedulerDetailEntity
    {

        public string DialerType { get; set; }
        public string DialledNumber { get; set; }
        public string AgentNumber { get; set; }
        public string DateTime { get; set; }
        public Double Duration { get; set; }
        public string Status { get; set; }
        public int CallCount { get; set; }
        public string ScheduleName { get; set; }
        public string AbandonedQueue { get; set; }
        public string ScheduleQueue { get; set; }
        public string AgentName {get; set; }
        public int Attempts { get; set; }
        public string AttemptID { get; set; }


    }
    
}