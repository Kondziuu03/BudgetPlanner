using BudgetPlanner.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetPlanner.Core.ViewModels
{
    public class TaskViewModel
    {
        public string Heading { get; set; }
        public Task Task { get; set; }
    }
}
