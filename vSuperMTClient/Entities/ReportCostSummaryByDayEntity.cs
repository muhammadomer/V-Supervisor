using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportCostSummaryByDayEntity
    {
        public string Date { get; set; }
        public string Day { get; set; }
        public int TotalCalls { get; set; }
        public Double Duration { get; set; }
        public Double Cost { get; set; }

    }
}