using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class CostingEntity
    {
        public int Id { get; set; }
        public string CostType { get; set; }
        public float CostPerSec { get; set; }
        public float ConnectCost { get; set; }
        public float CallSetupCost { get; set; }
        public string DialNumber { get; set; }
    }
}