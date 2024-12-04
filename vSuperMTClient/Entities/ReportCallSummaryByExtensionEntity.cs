using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportCallSummaryByExtensionEntity
    {
        public string Extension { get; set; }
        public string Name { get; set; }
        public int IncomingCalls { get; set; }
        public string IncomingCallDuration { get; set; }
        public int OutgoingCalls { get; set; }
        public string OutgoingCallDuration { get; set; }
        public string TotalDuration { get; set; }
        public int TotalCalls { get; set; }

    }
}