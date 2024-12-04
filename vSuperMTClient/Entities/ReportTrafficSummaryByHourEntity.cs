using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportTrafficSummaryByHourEntity
    {
        public string Hour { get; set; }
        public int IncomingCalls { get; set; }
        public int IncomingAnsweredCalls { get; set; }
        public int IncomingUnAnsweredCalls { get; set; }
        public double IncomingCallDuration { get; set; }
        public int OutgoingCalls { get; set; }
        public int OutgoingAnsweredCalls { get; set; }
        public int OutgoingUnAnsweredCalls { get; set; }
        public double OutgoingCallDuration { get; set; }
        public double Cost { get; set; }
    }
}