using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportGroupAgentCallVolumeEntity
    {
        public string GroupNumber { get; set; }
        public string Title { get; set; }
        public string DateOnly { get; set; }
        public string HourOnly { get; set; }
        public string AgentLoggedCount { get; set; }
        public string CallsCount { get; set; }
        public string AVGLogIn { get; set; }
        public string AVGCallBusy { get; set; }
        public string AVGNonCallBusy { get; set; }
        public string AVGBusy { get; set; }
    }
}