using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportCostSummaryByExtensionItemisedEntity
    {
        public string Extension { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string PhoneNumber { get; set; }
        
        public Double Duration { get; set; }
        public Double Cost { get; set; }
        public int CallCount { get; set; }

    }
}