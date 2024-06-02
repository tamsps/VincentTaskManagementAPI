using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace VincentTaskManagementAPI.Authentication
{
		public sealed class BearerAuthorizeFilter : IAsyncAuthorizationFilter
		{
				public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
				{
						if (context?.HttpContext?.Request?.Headers == null) throw new ArgumentNullException(nameof(context));

						if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
								context.Result = CreateUnauthorized();

						//var policyEvaluator = context.HttpContext.RequestServices.GetRequiredService<IPolicyEvaluator>();
						var policyEvaluator = new BearerPolicyEvaluator();
						var authenticateResult = await policyEvaluator.AuthenticateAsync(default, context.HttpContext);
						var authorizeResult = await policyEvaluator.AuthorizeAsync(default, authenticateResult, context.HttpContext, context);

						if (authorizeResult.Challenged)
						{
								context.Result = CreateUnauthorized();
								return;
						}

						context.HttpContext.User = authenticateResult.Principal;

						static IActionResult CreateUnauthorized() => new UnauthorizedObjectResult(new ErrorMessage("Unauthorized", 401));
				}
		}
}
