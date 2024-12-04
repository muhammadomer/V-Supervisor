using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class AgentScoringReportSettingParms
    {
        public int AgentCallsAnsweredScore { get; set; }
        public int AgentCallAnsweredInNSecScore { get; set; }
        public int AgentWaitingTimeScore { get; set; }
        public int AgentCallAnsweredInNSec { get; set; }
        public int AgentWaitingTimeInNSec { get; set; }
        public float AgentAvaliablityHours { get; set; }
        public int AgentAvaiabilityScore { get; set; }
    }
}