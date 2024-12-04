using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{

    public class ReportAllCallsEntity
    {
        public string BoardTitle { get; set; }
        public string CLI { get; set; }
        public string DDI { get; set; }
        public string DateTime { get; set; }
        public Double Duration { get; set; }
        public string Status { get; set; }
        public int CallCount { get; set; }
        public int TotalAgentDuration { get; set; }

    }
   
}