using BudgetPlanner.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BudgetPlanner.Core.ViewModels
{
    public class TasksViewModel
    {
        public string Heading { get; set; }
        public Budget Budget { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
        public FilterTasks FilterTasks { get; set; }
    }
}
