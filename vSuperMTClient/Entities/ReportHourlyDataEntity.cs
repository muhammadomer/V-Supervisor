using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportCallBreakdownByIntervalEntity
    {
        public string BoardTitle { get; set; }
        public int TotalCalls { get; set; }
        public int TotalAnsweredCalls { get; set; }
        public int TotalAbandonedCalls { get; set; }
        public Double AbandonedCallsPercentage { get; set; }
        public string TimeKey { get; set; }
        public Double LongestWaitingTime { get; set; }
        public string Date { get; set; }

    }
}