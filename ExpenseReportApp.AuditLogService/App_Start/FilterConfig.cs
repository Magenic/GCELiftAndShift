using System.Web;
using System.Web.Mvc;

namespace ExpenseReportApp.AuditLogService
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
