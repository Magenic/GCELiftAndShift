using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CompanyX.Data.ModelMetadata
{
    public interface ILineItemMetadata
    {
        [Required(ErrorMessage = "Category is required")]
        Nullable<int> CategoryID { get; set; }
        [Required(ErrorMessage = "Description is required")]
        string Description { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        Nullable<System.DateTime> TimeStamp { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        Nullable<decimal> Amount { get; set; }
        int LineItemID { get; set; }
        Nullable<int> ExpenseReportID { get; set; }

    }
}
