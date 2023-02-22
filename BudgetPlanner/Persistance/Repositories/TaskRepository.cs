using BudgetPlanner.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetPlanner.Persistance.Repositories
{
    public class TaskRepository
    {
        private ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Task> Get(string userId)
        {
            return _context.Tasks.Where(x => x.UserId == userId && x.isExecuted == false);
        }

        public IEnumerable<Task> Get(string userId, string name, decimal minValue, decimal maxValue, bool isExecuted)
        {
            var tasks = _context.Tasks.Where(x => x.UserId == userId && x.isExecuted == isExecuted);

            if (!string.IsNullOrWhiteSpace(name))
                tasks = tasks.Where(x => x.Name == name);
            if (minValue != 0)
                tasks = tasks.Where(x => x.TotalValue >= minValue);
            if(maxValue != 0)
                tasks = tasks.Where(x => x.TotalValue <= maxValue);

            return tasks;
        }

        public Task GetTask(int id, string userId)
        {
            return _context.Tasks.Single(x => x.Id == id && x.UserId == userId);
        }

        public void Add(Task task)
        {
            _context.Tasks.Add(task);
        }

        public void Delete(int id, string userId)
        {
            var taskToDelete = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);
            _context.Tasks.Remove(taskToDelete);
        }
   
    }
}
