using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class AgentStatsEntity
    {
        public int ID { get; set; }
        public string Extension { get; set; }
        public string AgentName { get; set; }
        public int EventID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string AgentGroups { get; set; }
        
    }
}