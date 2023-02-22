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

namespace BudgetPlanner.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private TaskService _taskService;
        private BudgetService _budgetService;

        public TaskController(ApplicationDbContext context)
        {
            _taskService = new TaskService(new UnitOfWork(context));
            _budgetService = new BudgetService(new UnitOfWork(context));
        }

        public IActionResult Tasks()
        {
            var userId = User.GetUserId();
            var tasks = _taskService.Get(userId);
            var budget = _budgetService.Get(userId);

            var vm = new TasksViewModel {
                Heading = "Strona główna",
                Budget = budget,
                Tasks = tasks
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Tasks(TasksViewModel viewModel)
        {
            string userId = User.GetUserId();

            var tasks = _taskService.Get(userId, viewModel.FilterTasks.Name, viewModel.FilterTasks.MinValue, viewModel.FilterTasks.MaxValue,viewModel.FilterTasks.isExecuted);

            return PartialView("_tasksTable", tasks);
        }

        public IActionResult Task(int id = 0)
        {
            string userId = User.GetUserId();

            var task = id == 0 ?
                new Task { Id = 0, UserId = userId } :
                _taskService.GetTask(id, userId);

            var vm = new TaskViewModel
            {
                Task = task,
                Heading = id == 0 ? "Dodawanie zadania" : "Edycja zadania"
            };  

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Task(Task task)
        {
            string userId = User.GetUserId();
            task.UserId = userId;

            if(!ModelState.IsValid)
            {
                var vm = new TaskViewModel
                {
                    Task = task,
                    Heading = task.Id == 0 ? "Dodawanie zadania" : "Edycja zadania"
                };
                return View("Task", vm);
            }

            if (task.Id == 0) 
                _taskService.Add(task);
            else 
                _taskService.Update(task,userId);

            return RedirectToAction("Tasks");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                string userId = User.GetUserId();
                _taskService.Delete(id, userId);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, messagge = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Finish(int id)
        {
            try
            {
                string userId = User.GetUserId();  
                var taskToFinish = _taskService.GetTask(id, userId);
                var budget = _budgetService.Get(userId);
                if (_budgetService.isPossibleToRemove(budget.BudgetValue, taskToFinish.TotalValue))
                {
                    _budgetService.RemoveFromBudget(userId, taskToFinish.TotalValue);
                    _taskService.Finish(id, userId);
                }
                else
                    throw new Exception("Za mało środków, by zrealizować dany cel!");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, messagge = ex.Message });
            }
            return Json(new { success = true });
        }
    }
}
