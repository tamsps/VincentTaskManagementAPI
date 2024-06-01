using log4net.Config;
using log4net.Core;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using VincentTaskManagementAPI.Db;
using VincentTaskManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace VincentTaskManagementAPI.Controllers
{
		[ApiController]
		[Route("api/tasks")]
		public class VincTaskController : ControllerBase
		{
				/// <summary>
				/// Create a new logger for the VincTaskController
				/// </summary>
				ILog _logger = LogManager.GetLogger(typeof(VincTaskController));

				private readonly VincDbContext _context;


				/// <summary>
				/// Create a constructor for the VincTaskController
				/// </summary>
				/// <param name="context"></param>
				public VincTaskController(VincDbContext context)
				{
						_context = context;
				}

				[HttpGet]
				public async Task<ActionResult<IEnumerable<VincTaskModel>>> GetTasks()
				{
						var tasks = await _context.VincTasksModel.ToListAsync();
						if (tasks == null)
						{
								return NotFound("No data found");
						}
						_logger.Info("Get all tasks");
						return Ok(tasks);
				}

				[HttpGet("{id}")]
				public async Task<ActionResult<VincTaskModel>> GetTaskById([FromRoute] int id)
				{
						var task = await _context.VincTasksModel.FindAsync(id);
						if (task == null)
						{
								return NotFound("No data found");
						}
						return Ok(task);
				}

				[HttpPost]
				public async Task<ActionResult<VincTaskModel>> CreateTask([FromBody] VincTaskModel task)
				{
						_context.VincTasksModel.Add(task);
						await _context.SaveChangesAsync();
						return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);

				}

				[HttpPut("{id}")]
				public async Task<IActionResult> UpdateTask([FromRoute] int id, [FromBody] VincTaskModel task)
				{
						var updatedTask = await _context.VincTasksModel.FindAsync(id);
						if (updatedTask == null)
						{
								return NotFound("Task not found");
						}

						updatedTask.Title = task.Title;
						updatedTask.Priority = task.Priority;
						updatedTask.Description = task.Description;
						updatedTask.DueDate = task.DueDate;

						await _context.SaveChangesAsync();

						return NoContent();

				}

				[HttpDelete("{id}")]
				public async Task<IActionResult> DeleteTask([FromRoute] int id)
				{
						var task = await _context.VincTasksModel.FindAsync(id);
						if (task == null)
						{
								return NotFound("Task not found");
						}
						_context.VincTasksModel.Remove(task);
						await _context.SaveChangesAsync();
						return NoContent();
				}
		}
}
