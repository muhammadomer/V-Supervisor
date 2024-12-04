using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    
    public class ReportDiskUsageCostEntity
    {
        public DateTime Time_Stamp { get; set; }
        public string Day { get; set; }
        public double DiskUsage { get; set; }
        public double Cost { get; set; }
    }
}