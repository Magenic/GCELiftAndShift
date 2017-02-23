using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CompanyX.Data.ModelMetadata
{
    public interface IExpenseReportMetadata
    {
        int ExpenseReportID { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        string LastName { get; set; }
        [Required(ErrorMessage = "Company Name is required")]
        string CompanyName { get; set; }
        [Required(ErrorMessage = " The Date is required")]
        [DataType(DataType.Date)]
        Nullable<System.DateTime> Date { get; set; }
        string Description { get; set; }
        string UserID { get; set; }
        Nullable<decimal> ExpenseTotal { get; set; }
    }
}
