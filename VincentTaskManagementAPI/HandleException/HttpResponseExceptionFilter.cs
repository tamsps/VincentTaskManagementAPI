using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using log4net;
using VincentTaskManagementAPI.Controllers;
using System.Diagnostics;

namespace VincentTaskManagementAPI.HandleException
{
		public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
		{
				public int Order => int.MaxValue - 10;
				ILog _logger = LogManager.GetLogger(typeof(VincTaskController));


				public void OnActionExecuting(ActionExecutingContext context) {
						Log("START", context.RouteData);
				}

				public void OnActionExecuted(ActionExecutedContext context)
				{
						if (context.Exception is HttpResponseException httpResponseException)
						{
								context.Result = new ObjectResult(httpResponseException.Value)
								{
										StatusCode = httpResponseException.StatusCode
								};

								context.ExceptionHandled = true;
						}

						if (context.Exception is not null)
						{
								_logger.Error(context.Exception);
						}
						Log("END", context.RouteData);

				}
				private void Log(string methodName, RouteData routeData)
				{
						var controllerName = routeData.Values["controller"];
						var actionName = routeData.Values["action"];
						var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
						_logger.Debug(message);

				}

		}
}
