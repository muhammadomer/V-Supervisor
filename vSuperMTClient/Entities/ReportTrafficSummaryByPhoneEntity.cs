using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportTrafficSummaryByPhoneEntity
    {
        public string DialledNumber { get; set; }
        public string Day { get; set; }
        public int OutgoingCalls { get; set; }
        public int OutgoingAnsweredCalls { get; set; }
        public int OutgoingUnAnsweredCalls { get; set; }
        public double OutgoingCallDuration { get; set; }
        public double Cost { get; set; }
    }
}