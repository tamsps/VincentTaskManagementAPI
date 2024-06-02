using Microsoft.AspNetCore.Mvc;
using VincentTaskManagementAPI.HandleRecordResult;
using VincentTaskManagementAPI.Models;

namespace VincentTaskManagementAPI.Services
{
		public interface ITaskService
		{
				public List<VincTaskModel> GetTasks(PagingParameters pagingParameters);
				public  VincTaskModel GetTaskById([FromRoute] int id);
				public  VincTaskModel CreateTask([FromBody] VincTaskModel task);
				public VincTaskModel UpdateTask([FromRoute] int id, [FromBody] VincTaskModel task);
				public VincTaskModel DeleteTask([FromRoute] int id);

		}
}
