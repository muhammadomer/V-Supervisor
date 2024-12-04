using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class BoardsEntity
    {
        public BoardsEntity()
        {
            HuntGroup_Extension = new List<Boards_ExtensionEntity>();
        }
        public int huntGroupId{get; set; }
        public string extension { get; set; }
        public string GroupNumber { get; set; }
        public string DDI { get; set; }
        public string Title { get; set; }
        public string Reset { get; set; }
        public string ResetTime { get; set;}
        public string ResetDate { get; set; }
        public string ResetTime2 { get; set; }
        public int SlaVal { get; set; }
        public int SlaOperator { get; set; }
        public int AbandonedTime { get; set; }
       
        public List<Boards_ExtensionEntity> HuntGroup_Extension { get; set; }

    }
}