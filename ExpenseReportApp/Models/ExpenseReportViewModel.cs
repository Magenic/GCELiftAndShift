using ExpenseReportApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseReportApp.Models
{
    public class ExpenseReportViewModel
    {
        public ExpenseReport ExpenseReport { get; set; }
        public LineItem LineItem { get; set; }
        public List<LineItem> LineItems { get; set; }
    }
}