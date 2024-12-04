using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportOverflowedCalls
    {
        public string StartDate { get; set; }
        public string Title { get; set; }
        public int Totaloverflowedcall { get; set; }
        public int Totaloverflowedcallwaittime { get; set; }
        public int Totaloverflowedcallerwaiting { get; set; }
        public int TotaloverflowedcallOUTOFHOUR { get; set; }
        public int TotaloverflowedcallDTMF { get; set; }
        public int TotalTransfersIn { get; set; }
        public string OverFlowInWaitTime { get; set; }
        public int OverFlowInTotalWaitTime { get; set; }
        public string OverFlowInAVGWaitTime { get; set; }
        public int TotaloverflowedcallInQueues { get; set; }

        public int TotaloverflowedcallNOAgent { get; set; }
    }
}