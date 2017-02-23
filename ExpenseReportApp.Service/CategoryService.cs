using ExpenseReportApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseReportApp.Service
{
    public class CategoryService
    {
        private ExpenseReportEntities context;

        public CategoryService()
        {
            context = new ExpenseReportEntities();
        }

        public List<Category> GetCategories()
        {
            var categories = context.Categories.ToList();
            return categories;
        }
    }
}
