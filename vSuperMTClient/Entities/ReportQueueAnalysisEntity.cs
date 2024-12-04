using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportQueueAnalysisEntity
    {


        public string GroupNumber { get; set; }//
        public string Title { get; set; }


        public string TotalAnswered { get; set; }
        public string TotalACDTime { get; set; }
        public string TotalAbanodoned { get; set; }

        public string TotalAbanodonedAfter { get; set; }//
        public string TotalCallsNotAccepted { get; set; }//

        public string AbandonedThreshold { get; set; }//

        public string AvgWaitAbandonedTime { get; set; }

        public string TotalCalls { get; set; }
        public string AvgWaitTime { get; set; }
        public string TotaloverflowedcallInQueues { get; set; }
        public string Totaloverflowedcall { get; set; }
        public string SLAPerl { get; set; }
        public string LongestWait { get; set; }
        public string TotalAgentTime { get; set; }

        public string TotalRingDurationAnswerCalls { get; set; }

    
    
    }
}