using CompanyX.Data;
using ExpenseReportApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseReportApp.Service
{
    public class LineItemService
    {
        private ExpenseReportEntities context;

        public LineItemService()
        {
            context = new ExpenseReportEntities();
        }

        public LineItem GetLineItem(int? lineItemId)
        {
            return context.LineItems.Where(l => l.LineItemID == lineItemId).FirstOrDefault();
        }

        public List<LineItem> GetLineItems(int? expReportId)
        {
            return context.LineItems.Where(l => l.ExpenseReportID == expReportId).ToList();
        }

        public int Create(LineItem lineItem)
        {
            var id = context.InsertLineItem(lineItem.CategoryID, lineItem.Description, lineItem.TimeStamp, lineItem.Amount, 
                lineItem.ExpenseReportID).FirstOrDefault();
            var lineItemId = Convert.ToInt32(id);
            return lineItemId;
        }

        public void Update(LineItem lineItem)
        {
            context.UpdateLineItem(lineItem.LineItemID, lineItem.CategoryID, lineItem.Description, 
                lineItem.TimeStamp, lineItem.Amount, lineItem.ExpenseReportID);
        }

        public void Delete(int? lineItemId)
        {
            var item = GetLineItem(lineItemId);
            context.LineItems.Remove(item);
            context.SaveChanges();
        }
    }
}
