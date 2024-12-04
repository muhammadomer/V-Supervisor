using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportSLAPerformance
    {
        public int BoardId { get; set; }
        public string StartDate { get; set; }
        public string Title { get; set; }
        public string TotalCalls { get; set; }
        public string TotalAnswered { get; set; }
        public string SLAWithIn { get; set; }
        public string SLAOutside { get; set; }
    }
}