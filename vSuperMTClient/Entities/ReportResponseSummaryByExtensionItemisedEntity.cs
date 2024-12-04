using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportResponseSummaryByExtensionItemisedEntity
    {
        
        public string Extension { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public Double Duration { get; set; }
        public string Direction { get; set; }
        public string CLI { get; set; }
        public string DDI { get; set; }
        public string LastState { get; set; }
        public string InitialState { get; set; }
        
        public Double RingDuration { get; set; }
        public int CallCount { get; set; }


    }
}