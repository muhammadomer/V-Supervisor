using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportAgentAvailabilityEntity
    {
        public string AgentName { get; set; }
        public string GroupName { get; set; }
        public Double LoggedInTime { get; set; }
        public Double LoggedOutTime { get; set; }
        public Double ACDBusyTime { get; set; }
        public Double NonACDBusyTime { get; set; }
        public Double ClerikalBusyTime { get; set; }
        public Double TempAbsTime { get; set; }
        public int LoggedInCount { get; set; }
        public int LoggedOutCount { get; set; }
        public int TempAbsCount { get; set; }
    }
}