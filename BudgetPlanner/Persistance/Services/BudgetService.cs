using BudgetPlanner.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlanner.Persistance.Services
{
    public class BudgetService
    {
        private UnitOfWork _unitOfWork;
        public BudgetService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Budget budget)
        {
            _unitOfWork.Budget.Add(budget);
            _unitOfWork.Complete();
        }

        public Budget Get(string userId)
        {
            return _unitOfWork.Budget.Get(userId);
        }

        public void AddToBudget(string userId, decimal value=0)
        {
            var budget = _unitOfWork.Budget.Get(userId);
            if (budget != null)
                budget.BudgetValue += value;
            _unitOfWork.Complete();
        }

        public void RemoveFromBudget(string userId, decimal value)
        {
            var budget = _unitOfWork.Budget.Get(userId);
            if (budget != null)
            {
                if (!isPossibleToRemove(budget.BudgetValue, value))
                    budget.BudgetValue = 0;
                else
                    budget.BudgetValue -= value;
            }
            _unitOfWork.Complete();
        }

        public bool isPossibleToRemove(decimal budget, decimal value)
        {
            if (budget >= value)
                return true;
            else
                return false;
        }
    }
}
