using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportYBVCallsEntity
    {
        public string Date { get; set; }
        public int TransferCount { get; set; }
        public int FirstCount { get; set; }
        public int SecondCount { get; set; }
        public int ThirdCount { get; set; }
        public string TimeKey { get; set; }

        public string FirstCountHeader { get; set; }
        public string SecondCountHeader { get; set; }
        public string ThirdCountHeader { get; set; }

    }
}