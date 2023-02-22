using BudgetPlanner.Core.Models.Domains;
using BudgetPlanner.Core.ViewModels;
using BudgetPlanner.Persistance;
using BudgetPlanner.Persistance.Extensions;
using BudgetPlanner.Persistance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlanner.Controllers
{
    [Authorize]
    public class BudgetController : Controller
    {
        private BudgetService _budgetService;
        public BudgetController(ApplicationDbContext context)
        {
            _budgetService = new BudgetService(new UnitOfWork(context));
        }

        public IActionResult Budget()
        {
            var budget = new Budget();
            return View(budget);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Budget(Budget budget)
        {
            string userId = User.GetUserId();
            budget.UserId = userId;

            if (ModelState.IsValid)
                _budgetService.Add(budget);

            return RedirectToAction("Tasks","Task");
        }


        [HttpPost]
        public IActionResult AddToBudget(decimal val)
        {
            try
            {
                var userId = User.GetUserId();
                _budgetService.AddToBudget(userId, val);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, messagge = ex.Message });
            }
            return Json(new { success = true });
        }


        [HttpPost]
        public IActionResult RemoveFromBudget(decimal val)
        {
            try
            {
                var userId = User.GetUserId();
                _budgetService.RemoveFromBudget(userId, val);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, messagge = ex.Message });
            }
            return Json(new { success = true });
        }
    }
}
