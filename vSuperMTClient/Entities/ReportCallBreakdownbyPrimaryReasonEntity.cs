using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportCallBreakdownbyPrimaryReasonEntity
    {
        public string Board { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string CLI { get; set; }
        public string DDI { get; set; }
        public string PrimaryOutcome { get; set; }
        public string SecondaryOutcome { get; set; }
        public Double Duration { get; set; }
    }

    public class ReportCallBreakdownbyPrimaryReasonEntity_DEKRA
    {     
        public string AgentExtension { get; set; }
        public string AgentName { get; set; }
        public string PrimaryOutcome { get; set; }
        public string SecondaryOutcome { get; set; }
        public int PrimaryCount { get; set; }
        public int SecondaryCount { get; set; }      
        public int PrimaryCallDuration { get; set; }
        public int SecondaryCallDuration { get; set; }
    }
}