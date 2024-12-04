using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportAgentCallsSummaryEntity
    {
        public string AgentName { get; set; }
        public string GroupName { get; set; }
        public Double TotalCalls { get; set; }
        public Double TotalTalkTime { get; set; }
        public Double AvgTalkTime { get; set; }
        //public string TotalACDTalkTime { get; set; }
        //public string TotalNonACDTalkTime { get; set; }
        //public string AvgACDTalkTime { get; set; }
        //public string AvgNonACDTalkTime { get; set; }
    }
}