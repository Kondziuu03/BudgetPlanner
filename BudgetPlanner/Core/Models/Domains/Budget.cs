using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlanner.Core.Models.Domains
{
    public class Budget
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Wartość")]
        public decimal BudgetValue { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
