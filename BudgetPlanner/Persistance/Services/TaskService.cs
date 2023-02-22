using BudgetPlanner.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetPlanner.Persistance.Services
{
    public class TaskService
    {
        private UnitOfWork _unitOfWork;

        public TaskService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Task> Get(string userId)
        {
            return _unitOfWork.Task.Get(userId);
        }

        public IEnumerable<Task> Get(string userId, string name=null, decimal minValue=0, decimal maxValue=0, bool isExecuted=false)
        {
            return _unitOfWork.Task.Get(userId,name,minValue,maxValue,isExecuted);
        }

        public Task GetTask(int id, string userId)
        {
            return _unitOfWork.Task.GetTask(id, userId);
        }

        public void Add(Task task)
        {
            task.isExecuted = false;
            task.TotalValue = task.Value * task.Quantity;
            _unitOfWork.Task.Add(task);
            _unitOfWork.Complete();
        }

        public void Update(Task task,string userId)
        {
            var taskToEdit = _unitOfWork.Task.GetTask(task.Id, userId);
            taskToEdit.Name = task.Name;
            taskToEdit.Value = task.Value;
            taskToEdit.Quantity = task.Quantity;
            taskToEdit.Comments = task.Comments;
            taskToEdit.TotalValue = task.Value * task.Quantity;

            _unitOfWork.Complete();
        }

        public void Delete(int id, string userId)
        {
            _unitOfWork.Task.Delete(id, userId);
            _unitOfWork.Complete();
        }

        public void Finish(int id, string userId)
        {
            var taskToEdit = _unitOfWork.Task.GetTask(id, userId);
            taskToEdit.isExecuted = true;
            _unitOfWork.Complete();
        }
    }
}
