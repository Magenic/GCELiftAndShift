using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseReportApp.AuditLogService.Models
{
    public class AuditLogEntry
    {
        public int Id { get; set; }
        public string ApplicationName { get; set; }
        public string Action { get; set; }
        public DateTime TimeStamp { get; set; }
        public string EntityId { get; set; }
    }
}