using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportAgentScoringEntity
    {
        public int boardID { get; set; }
        public string Title { get; set; }
        public string AgentName { get; set; }
        public string StartTime { get; set; }
        public int AvailableTime { get; set; }
        public int CallsHandled { get; set; }
        public int CallsServed { get; set; }
        public int TotalAnsweredWithIn { get; set; }
        public int TotalTime { get; set; }
        public float AnsweredWithInPercent { get; set; }
        public float CallAcceptedPercent { get; set; }
        public float AgentAvailabilityPercent { get; set; }


    }

    public class ReportAgentLeadEntity
    {      
        public string AgentName { get; set; }
        public string StartTime { get; set; }
        public int AvailableTime { get; set; }
        public int CallsHandled { get; set; }
        public int CallsServed { get; set; }
        public int TotalAnsweredWithIn { get; set; }
        public int TotalTime { get; set; }
        public float AnsweredWithInPercent { get; set; }
        public float CallAcceptedPercent { get; set; }
        public float AgentAvailabilityPercent { get; set; }


    }
}