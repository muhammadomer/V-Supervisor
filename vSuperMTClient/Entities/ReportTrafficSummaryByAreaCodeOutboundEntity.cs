using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportTrafficSummaryByAreaCodeOutboundEntity
        {
            //public string DDI { get; set; }
            public string AreaCode { get; set; }
        public string AreaDescription { get; set; }

        public int OutgoingCalls { get; set; }
            public int OutgoingAnsweredCalls { get; set; }
            public int OutgoingUnAnsweredCalls { get; set; }
            public double OutgoingCallDuration { get; set; }

        }
}