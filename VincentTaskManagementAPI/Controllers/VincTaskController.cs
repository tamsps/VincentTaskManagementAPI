using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Xml.Linq;
using VincentTaskManagementAPI.Db;
using VincentTaskManagementAPI.Models;

namespace VincentTaskManagementAPI.Controllers
{
		[ApiController]
		[Route("api/tasks")]
		public class VincTaskController : ControllerBase
		{
				/// <summary>
				/// Create a new logger for the VincTaskController
				/// </summary>
				private readonly ILogger<VincTaskController> _logger;

				/// <summary>
				/// Create a constructor for the VincTaskController
				/// </summary>
				/// <param name="logger"></param>
				public VincTaskController(ILogger<VincTaskController> logger)
				{
						_logger = logger;
				}

				[HttpGet]
				public async Task<ActionResult<IEnumerable<VincTask>>> GetTasks()
				{
						var tasks = VincTasksStore.MyTasks;
						if(tasks == null)
						{
								return NotFound("No data found");
						}
						return Ok(tasks);
				}

				[HttpGet("{id}")]
				public async Task<ActionResult<VincTask>> GetTaskById([FromRoute] int id)
				{
						var task = VincTasksStore.MyTasks.First(d=>d.Id.Equals(id));
						if (task == null)
						{
								return NotFound("No data found");
						}
						return Ok(task);
				}

				[HttpPost]
				public async Task<ActionResult<VincTask>> CreateTask([FromBody] VincTask task)
				{
						_ = VincTasksStore.MyTasks.Append(task);
						return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
				}

				[HttpPut("{id}")]
				public async Task<IActionResult> UpdateTask([FromRoute] int id, [FromBody] VincTask task)
				{
						var updatedTask = VincTasksStore.MyTasks.First(d => d.Id.Equals(id));

						if (updatedTask == null)
						{
								return NotFound("No data found");
						}
						updatedTask.Title = task.Title;
						updatedTask.Priority = task.Priority;
						updatedTask.Description = task.Description;
						updatedTask.DueDate = task.DueDate;

						return NoContent();
				}

				[HttpDelete("{id}")]
				public async Task<IActionResult> DeleteTask([FromRoute] int id)
				{
						var deleteTask = VincTasksStore.MyTasks.First(d => d.Id.Equals(id));
						if (deleteTask == null)
						{
								return NotFound("Task not found");
						}
						VincTasksStore.MyTasks.ToList().Remove(deleteTask);
						return NoContent();
				}
		}
}
