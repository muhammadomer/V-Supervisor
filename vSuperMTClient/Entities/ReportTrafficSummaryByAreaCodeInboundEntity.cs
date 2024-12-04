using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportTrafficSummaryByAreaCodeInboundEntity
    {
        public string AreaCode { get; set; }
        public string AreaDescription { get; set; }

        public int IncomingCalls { get; set; }
        public int IncomingAnsweredCalls { get; set; }
        public int IncomingUnAnsweredCalls { get; set; }
        public double IncomingCallDuration { get; set; }

    }
}