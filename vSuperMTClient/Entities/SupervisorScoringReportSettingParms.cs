using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class SupervisorScoringReportSettingParms
    {
        public int QCallsAnsweredScore { get; set; }
        public int QCallAnsweredInNSecScore { get; set; }
        public int QWaitingTimeScore { get; set; }
        public int QCallAnsweredInNSec { get; set; }
        public int QWaitingTimeInNSec { get; set; }
    }
}