using ExpenseReportApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseReportApp.Service
{
    public class ExpenseReportService
    {
        private ExpenseReportEntities context;
        private HttpClient httpClient;
        public ExpenseReportService()
        {
            context = new ExpenseReportEntities();
            httpClient = new HttpClient();
        }

        public ExpenseReport GetByExpenseReportID(int? id)
        {
            return context.ExpenseReports.Where(e => e.ExpenseReportID == id).FirstOrDefault();
        }

        public List<ExpenseReport> GetByUserID(string user)
        {
            return context.ExpenseReports.Where(e => e.UserID == user).ToList();
        }

        public int Create(ExpenseReport expReport)
        {
            var id = context.InsertExpenseReport(expReport.FirstName, expReport.LastName,
                expReport.CompanyName, expReport.Date, expReport.Description, expReport.UserID, expReport.ExpenseTotal).FirstOrDefault();

            int expReportId = Convert.ToInt32(id);
            Log("Create", expReportId);
            return expReportId;
        }

        public int Update(ExpenseReport expReport)
        {
            context.UpdateExpenseReport(expReport.ExpenseReportID, expReport.FirstName, expReport.LastName,
                expReport.CompanyName, expReport.Date, expReport.Description, expReport.UserID, expReport.ExpenseTotal);
            Log("Update", expReport.ExpenseReportID);
            return expReport.ExpenseReportID;
        }

        public void Delete(int? expReportId)
        {
            Log("Delete", (int)expReportId);
            context.DeleteExpenseReport(expReportId);
        }

        private void Log(string action, int id)
        {
            httpClient.BaseAddress = new Uri("http://gcp-iis/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            
            var logEntry = new { ApplicationName = "ReportExpense", Action = action,
                            TimeStamp = DateTime.Now, EntityId = id.ToString() };
            var response = httpClient.PostAsJsonAsync("api/AuditLog", logEntry).Result;
            if (response.IsSuccessStatusCode)
            {

            }
        }
    }
}
