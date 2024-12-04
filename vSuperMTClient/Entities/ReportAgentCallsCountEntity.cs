using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportAgentCallsCountEntity
    {
        public string AgentName { get; set; }
        public string GroupName { get; set; }
        public string TotalCalls { get; set; }

        //public string TotalACDCalls { get; set; }
        //public string TotalNonACDCalls { get; set; }
        //public string TotalOutboundCalls { get; set; }
        
        public string Hour { get; set; }
        public string Date { get; set; }

    }
}