using log4net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace VincentTaskManagementAPI.Controllers
{
		public class BaseController : ControllerBase
		{
				ILog _logger = LogManager.GetLogger(typeof(BaseController));

				#region HANDLE EXCEPTIION
				[HttpGet("Throw")]
				public IActionResult Throw() => throw new Exception("Sample exception.");


				[Route("/error-development")]
				public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
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

				[Route("/error")]
				public IActionResult HandleError() => Problem();
				#endregion
		}
}
