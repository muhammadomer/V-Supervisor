using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    
    public class ReportDurationCostEntity
    {



        public double durationCost;
        public double DurationCost
        {
            get { return durationCost; }
            set { durationCost = value; }
        }
        public DateTime Time_Stamp { get; set; }
        public string Day { get; set; }
        double duration;
        public double Duration
        {
            get { return duration/60000; }
            set { duration = value; }
        }
        public double Cost
        {
            get { return Math.Ceiling(duration /60000) * durationCost; }
            set { Cost = value; }
        }
      

    }
}