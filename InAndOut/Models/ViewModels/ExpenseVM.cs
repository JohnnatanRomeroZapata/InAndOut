using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace InAndOut.Models.ViewModels
{
    public class ExpenseVM
    {
        public Expense TheExpense { get; set; }

        public IEnumerable<SelectListItem> ddl  { get; set; }
    }
}
