using BudgetPlanner.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlanner.Persistance
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository Task; 
        public BudgetRepository Budget; 

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Task = new TaskRepository(context);
            Budget = new BudgetRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
