using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlanner.Core.Models.Domains
{
    public class FilterTasks
    {
        public string Name { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        [Display(Name = "Zrealizowane")]
        public bool isExecuted { get; set; }
    }
}
