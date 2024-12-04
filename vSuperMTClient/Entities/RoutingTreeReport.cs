using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class RoutingTreeReport
    {
        public int BoardID { get; set; }
        public string Title { get; set; }
        public int TotalCalls { get; set; }
        public int TotalNotAccepted { get; set; }
        public int ABoardID { get; set; }
        public string CLI { get; set; }
        //public Nullable<DateTime> CallArrived { get; set; }
        //public Nullable<DateTime> CallEnded { get; set; }
        public string CallArrived { get; set; }
        public DateTime DTCallArrived { get; set; }
        public string CallEnded { get; set; }
        public string CallID { get; set; }
        public string AgentStartTime { get; set; }
        public DateTime DTAgentStartTime { get; set; }
        public string AgentExtension { get; set; }
        public int Duration { get; set; }
        public string AgentName { get; set; }
        public string Status { get; set; }
        public int EventId { get; set; }

        public string DurationofLeg { get; set; }

    }
}