using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class AgentsEntity
    {
        public int AgentId { get; set; }
        public string Extension { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Reset { get; set; }
        public string ResetTime { get; set; }
        public string ResetDate { get; set; }
        public string ResetTime2 { get; set; }
        public int HuntGroupId { get; set; }
        public string PhysicalExtension { get; set; }
    }
}