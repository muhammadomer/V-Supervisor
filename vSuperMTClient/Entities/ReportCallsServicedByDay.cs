using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportCallsServicedByDay
    {
        public string Date { get; set; }
        public int AnsweredCalls { get; set; }
        public int LostCalls { get; set; }
    }
}