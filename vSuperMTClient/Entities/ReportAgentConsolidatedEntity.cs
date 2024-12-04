using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportAgentConsolidatedEntity
    {
        public string AgentName { get; set; }
        public string GroupName { get; set; }
        public string EventDate { get; set; }

        public TimeSpan LogInTime { get; set; }
        public TimeSpan LogOutTime { get; set; }
        public Double TotalLoggedInTime { get; set; }

        public double TotalLoggedOutTime { get; set; }

        public Double ACDBusyTime { get; set; }
        public Double ACDCallnotaccept { get; set; }
        public Double NonACDBusyTime { get; set; }
        public Double ClerikalBusyTime { get; set; }
        public Double TempAbsTime { get; set; }
        public Double UnHoldTime { get; set; }
        public Double LongestUnHoldTime { get; set; }
        public Double TotalIdleTime { get; set; }

        public int ACDBusyCount { get; set; }
        public int LoggedInCount { get; set; }
        public int LoggedOutCount { get; set; }
        public int ClerikalBusyCount { get; set; }
        public int TempAbsCount { get; set; }
        public int OutBoundCallCount { get; set; }
        public int TransferCount { get; set; }
        public int UnHoldCount { get; set; }
        public int HangUpCount { get; set; }

        public int ACDnotacceptCount { get; set; }
    }

    public class ReportAgentOverviewEntity
    {
        public string AgentName { get; set; }
        public string GroupTitle { get; set; }
        public string EventDate { get; set; }

        public TimeSpan LogInTime { get; set; }
        public Double TotalLoggedInTime { get; set; }



        public Double TotalIdleTime { get; set; }
        public Double ACDBusyTime { get; set; }
        public Double ACDRingTime { get; set; }
        public Double ClerikalBusyTime { get; set; }
        public Double TempAbsTime { get; set; }
        public Double UnHoldTime { get; set; }

        public int ACDBusyCount { get; set; }
        public int ClerikalBusyCount { get; set; }
        public int TempAbsCount { get; set; }
        public int ACWCount { get; set; }
        public Double ACWTime { get; set; }
        public int AUX1Count { get; set; }
        public Double AUX1Time { get; set; }
        public int AUX2Count { get; set; }
        public Double AUX2Time { get; set; }
        public int AUX3Count { get; set; }
        public Double AUX3Time { get; set; }
        public int AUX4Count { get; set; }
        public Double AUX4Time { get; set; }
        public int ExtOutCalls { get; set; }
        public Double ExtOutCallsTime { get; set; }
        public Double ExtOutCallsHoldTime { get; set; }

        public int ExternalExtOutCalls { get; set; }
        public Double ExternalExtOutCallsTime { get; set; }
        public Double ExternalExtOutCallsHoldTime { get; set; }

        public int ExtInCalls { get; set; }
        public Double ExtInCallsTime { get; set; }
        public Double ExtInCallsHoldTime { get; set; }
        public int ACDnotacceptCount { get; set; }
    }

    public class ReportAgentUnavailbeEntity
    {
       // public string EventDate { get; set; }
        public string AgentName { get; set; }
        public string Reason { get; set; }  
        public int ReasonCount { get; set; }
        public Double ReasonTime { get; set; }
    }
}