using ExpenseReportApp.AuditLogService.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ExpenseReportApp.AuditLogService.DAL
{
    public class AuditLogEntryContext : DbContext
    {

        public AuditLogEntryContext() : base("AuditLogEntryContext")
        {
        }

        public DbSet<AuditLogEntry> AuditLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}