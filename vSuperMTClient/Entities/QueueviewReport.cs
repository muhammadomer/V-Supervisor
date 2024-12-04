using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class QueueviewReport
    {
        public int BoardID { get; set; }
        public string Title { get; set; }
        public int TotalMissedCalls { get; set; }
        public double AvgMissedDuration { get; set; }
        public int TotalAnsweredCalls { get; set; }
        public int AvgAnswer { get; set; }
        public int AvgResponseTime { get; set; }
        public int TotalAnswerDuration { get; set; }
        public int totalcalls { get; set; }

        public double TotalMissedDuration { get; set; }
        public double TotalResponseTime { get; set; }
    }
}