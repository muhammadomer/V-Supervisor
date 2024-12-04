using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportResponseSummaryByHourEntity
    {
        public string Hour { get; set; }
        public int TotalCalls { get; set; }
        public int TotalRingAnswered { get; set; }
        public int TotalRingUnAnswered { get; set; }
        public int AnsweredCalls { get; set; }
        public Double AvgRingAnswered { get; set; }
        public int MaxRingAnswered { get; set; }
        public int WithinThresholdCount { get; set; }
        public int UnAnsweredCalls { get; set; }
        public int LostCalls { get; set; }
        public Double AvgRingLost { get; set; }
        public int MaxRingLost { get; set; }
    }
}