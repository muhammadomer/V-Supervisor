using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportTrafficSummaryByExtensionItemisedEntity
    {
        
        public string Extension { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public Double Duration { get; set; }
        public string Direction { get; set; }
        public string CLI { get; set; }
        public string DDI { get; set; }
        public double Cost { get; set; }
        public int CallCount { get; set; }
        public int InboundCallCount { get; set; }
        public int OutbountCallCount { get; set; }


    }
}