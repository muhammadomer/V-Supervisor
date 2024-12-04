using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportAgentCallsTakenEntity
    {
        public string AgentName { get; set; }
        public string GroupName { get; set; }
        public string CallType { get; set; }
        public string Extension { get; set; }
        public string StartTime { get; set; }
        public Double Duration { get; set; }
        public Double QueueDuration { get; set; }
        public string CLI { get; set; }


    }
}