using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VincentTaskManagementAPI.Db;
using VincentTaskManagementAPI.HandleRecordResult;
using VincentTaskManagementAPI.Models;

namespace VincentTaskManagementAPI.Services
{
		public class TaskService:ITaskService
		{
				private readonly VincDbContext _context;
        public TaskService(VincDbContext db)
        {
            _context = db;
        }

				public VincTaskModel CreateTask([FromBody] VincTaskModel task)
				{
						var existTask = _context.VincTasksModel.Find(task.Id);
						if (existTask != null)
						{
								return null;
						}
						_context.VincTasksModel.Add(task);
						_context.SaveChangesAsync();
						return task;
				}

				public VincTaskModel DeleteTask([FromRoute] int id)
				{
						var task = _context.VincTasksModel.Find(id);
						if (task != null)
						{
								_context.VincTasksModel.Remove(task);
								_context.SaveChangesAsync();
						}
						return task;
				}

				public VincTaskModel GetTaskById([FromRoute] int id)
				{
						var task = _context.VincTasksModel.Find(id);
						return task;
				}

				public List<VincTaskModel> GetTasks(PagingParameters pagingParameters)
				{
						var tasks = _context.VincTasksModel
								.OrderBy(d => (int)d.Priority);
								
						if(string.Equals(pagingParameters.OrderBy, "DESC", System.StringComparison.OrdinalIgnoreCase))
						{
								tasks = tasks.OrderByDescending(p => p.Priority);
						}
						var tasksRes = tasks
								.Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
								.Take(pagingParameters.PageSize)
								.Where(t => string.IsNullOrEmpty(pagingParameters.SearchTitle) ||t.Title.Contains(pagingParameters.SearchTitle))
								.ToList();
						return tasksRes;
				}

				public VincTaskModel UpdateTask([FromRoute] int id, [FromBody] VincTaskModel task)
				{
						var updatedTask = _context.VincTasksModel.Find(id);
						if (updatedTask != null)
						{
								updatedTask.Title = task.Title;
								updatedTask.Priority = task.Priority;
								updatedTask.Description = task.Description;
								updatedTask.DueDate = task.DueDate;

								_context.SaveChangesAsync();
						}
						return updatedTask;
						
				}
		}
}
