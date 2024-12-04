using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportCallsbyPrimaryReasonEntity
    {
        public string Board { get; set; }
        public string PrimaryOutcome { get; set; }
        public int PrimaryCount { get; set; }

        public string SecondaryOutcome { get; set; }

        public int SecondaryCount { get; set; }

        public string AgentName { get; set; }

    }
}