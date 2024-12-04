using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ReportScheduleEntity
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReportId { get; set; }
        public int DateFilterCriteria { get; set; }
        public int CallsOption { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string Boards { get; set; }
        public string ExternalRouting { get; set; }
        public string Agents { get; set; }
        public string Extensions { get; set; }
        public int TimeInterval { get; set; }
        public string Users { get; set; }
        public string WeekDays { get; set; }
        public int ScheduleInterval { get; set; }
        public DateTime ExecutionTime { get; set; }
        public DateTime DateCreated { get; set; }
        public string Emails { get; set; }
        public string ReportName { get; set; }
        public string BoardNames { get; set; }
        public string ScheduleValue { get; set; }
        public string ScheduleTimeHours { get; set; }
        public string ScheduleTimeMinutes { get; set; }
        public string ReportType { get; set; }

    }
}