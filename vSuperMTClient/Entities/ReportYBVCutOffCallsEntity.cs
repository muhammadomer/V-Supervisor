using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportYBVCutOffCallsEntity
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public int Duration { get; set; }
        public string Title { get; set; }
        public string CLI { get; set; }
        public string DDI { get; set; }
        public int GroupNumber { get; set; }
        public int CallCount { get; set; }
    }
}