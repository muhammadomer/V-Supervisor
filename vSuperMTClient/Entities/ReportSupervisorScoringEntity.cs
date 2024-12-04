using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportSupervisorScoringEntity
    {
        public string BoardTitle { get; set; }
        public string boardid { get; set; }
        public int TotalCalls { get; set; }
        public float AnsweredPercent { get; set; }
        public int TotalAnsweredCalls { get; set; }
        public int AnsweredWithInTotal { get; set; }
        public int WaitingWithInTotal { get; set; }
        public float AnsweredWithInPercent { get; set; }
        public float WaitingWithInPercent { get; set; }
        public int TotalWait { get; set; }
        public float AvgAbandoned { get; set; }


    }
}