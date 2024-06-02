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
using VincentTaskManagementAPI.Services;
using Microsoft.AspNetCore.Diagnostics;
using VincentTaskManagementAPI.HandleRecordResult;

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

				private readonly ITaskService _ITasksrc;


				/// <summary>
				/// Create a constructor for the VincTaskController
				/// </summary>
				/// <param name="context"></param>
				public VincTaskController(ITaskService ITaskSrc)
				{
						_ITasksrc = ITaskSrc;
				}

				[HttpGet]
				public async Task<ActionResult<IEnumerable<VincTaskModel>>> GetTasks([FromQuery] PagingParameters pagingParameters)
				{
						var tasks = _ITasksrc.GetTasks(pagingParameters);
						if (tasks == null)
						{
								return NotFound("No data found");
						}
						return Ok(tasks);
				}

				[HttpGet("{id}")]
				public async Task<ActionResult<VincTaskModel>> GetTaskById([FromRoute] int id)
				{
						var task = _ITasksrc.GetTaskById(id);
						if (task == null)
						{
								return NotFound("No data found");
						}
						return Ok(task);
				}

				[HttpPost]
				public async Task<ActionResult<VincTaskModel>> CreateTask([FromBody] VincTaskModel task)
				{
						var create = _ITasksrc.CreateTask(task);
            if (create == null)
            {
                return BadRequest("Task already exists");
            }
						return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);

				}

				[HttpPut("{id}")]
				public async Task<IActionResult> UpdateTask([FromRoute] int id, [FromBody] VincTaskModel task)
				{
						var updatedTask = _ITasksrc.UpdateTask(id,task);
						if (updatedTask == null)
						{
								return NotFound("Task not found");
						}
						return Ok(updatedTask);

				}

				[HttpDelete("{id}")]
				public async Task<IActionResult> DeleteTask([FromRoute] int id)
				{
						var task = _ITasksrc.DeleteTask(id);
						if (task == null)
						{
								return NotFound("Task not found");
						}
						
						return Ok(task);
				}

				[HttpGet]
				[Route("/error-development")]
				public IActionResult HandleErrorDevelopment(
		[FromServices] IHostEnvironment hostEnvironment)
				{
						if (!hostEnvironment.IsDevelopment())
						{
								return NotFound();
						}

						var exceptionHandlerFeature =
								HttpContext.Features.Get<IExceptionHandlerFeature>()!;

						return Problem(
								detail: exceptionHandlerFeature.Error.StackTrace,
								title: exceptionHandlerFeature.Error.Message);
				}
				[HttpGet]
				[Route("/error")]
				public IActionResult HandleError() =>
						Problem();

		}
}
