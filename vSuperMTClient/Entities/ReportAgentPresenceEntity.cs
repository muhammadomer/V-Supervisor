using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportAgentPresenceEntity
    {
        public string AgentName { get; set; }
        public string GroupName { get; set; }
        public string Action { get; set; }
        public string StartTime { get; set; }

    }
}