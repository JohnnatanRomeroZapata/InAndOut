using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Models
{
    public class ExpenseType
    {
        [Key]
        public int ExpenseTypeId { get; set; }

        [DisplayName("Expense Type")]
        public string ExpenseTypeName { get; set; }
    }
}
