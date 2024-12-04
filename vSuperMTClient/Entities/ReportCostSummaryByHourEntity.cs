using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportCostSummaryByHourEntity
    {
        
        public string Hour { get; set; }
        public int TotalCalls { get; set; }
        public Double Duration { get; set; }
        public Double AvgDuration { get; set; }
        public Double Cost { get; set; }
        
    }
}