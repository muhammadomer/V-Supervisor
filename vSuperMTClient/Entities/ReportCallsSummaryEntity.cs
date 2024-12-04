using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportCallsSummaryEntity
    {
        public string BoardTitle { get; set; }
        public int TotalCalls { get; set; }
        public int TotalAnsweredCalls { get; set; }
        public int TotalAbandonedCalls { get; set; }
        public float AbandonedCallsPercentage { get; set; }
        public Double AVGWaitTime { get; set; }
        public Double LongestWaitingTime { get; set; }

        public int TotalTransferredIn { get; set; }
        public int OverFlowInAnsweredCalls { get; set; }
        public int OverFlowInAbndCalls { get; set; }
        public float OverFlowInAbandonedCallsPercentage { get; set; }
        public Double OverFlowInAVGWaitTime { get; set; }
        public Double OverFlowInLongestWaitingTime { get; set; }
        public int TotalYBVTansferredOut { get; set; }
        public Double ShortestWaitingTime { get; set; }
        public int VoicemailCall { get; set; }
        public int CallDays { get; set; }
        public int LoginDate { get; set; }
        public int LoginAgent { get; set; }

    }

    public class ReportDDIsSummaryEntity
    {
        public string BoardTitle { get; set; }
        public int TotalIVRCalls { get; set; }

        public int TotalAnsweredCalls { get; set; }

        public int TotalCallsInQueue { get; set; }
    }

    public class ReportIVRDtmfEntity
    {
        public int TotalCalls { get; set; }
        public string BoardTitle { get; set; }

        public string QueueNumber { get; set; }

        public string QueueName { get; set; }

        public string DTMF { get; set; }

        public string NodeName { get; set; }
    }
}