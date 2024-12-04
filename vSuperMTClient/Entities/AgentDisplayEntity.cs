using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSuperMTClient.Entities
{
    public class AgentDisplayEntity
    {

        public AgentDisplayEntity()
        {
            QueueList = new List<string>();
            AgentState = new List<string>();
        }


        public int UserId { get; set; }

        public string SortBy { get; set; }

        public List<string> QueueList { get; set; }

        // public string QueueList { get; set; }
        // public string AgentState { get; set; }

        public List<string> AgentState { get; set; }


        public string NumberOfAgents { get; set; }

        public string DisplayAvatar { get; set; }

    }
}
