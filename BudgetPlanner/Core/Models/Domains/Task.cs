using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlanner.Core.Models.Domains
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Cena")]
        public decimal Value { get; set; }

        [Display(Name = "Ilość")]
        public decimal Quantity { get; set; }

        [Display(Name = "Cena całkowita")]
        public decimal TotalValue { get; set; }

        [Display(Name = "Zrealizowane")]
        public bool isExecuted { get; set; }

        [Display(Name = "Uwagi")]
        public string Comments { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
