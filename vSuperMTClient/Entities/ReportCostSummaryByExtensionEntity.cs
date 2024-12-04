using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportCostSummaryByExtensionEntity
    {
        public string Extension { get; set; }
        public string Name { get; set; }
        public int TotalCalls { get; set; }
        public Double Duration { get; set; }
        public Double Cost { get; set; }
        
    }
}