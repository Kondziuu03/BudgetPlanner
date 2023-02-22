using BudgetPlanner.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlanner.Persistance.Repositories
{
    public class BudgetRepository
    {
        private ApplicationDbContext _context;
        public BudgetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Budget budget)
        {
            _context.Budgets.Add(budget);
        }

        public Budget Get(string userId)
        {
            return _context.Budgets.FirstOrDefault(x => x.UserId == userId);
        }

    }
}
