using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace VincentTaskManagementAPI.Authentication
{
		public sealed class BearerPolicyEvaluator : IPolicyEvaluator
		{
				private const string Scheme = "Bearer";

				public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy _, HttpContext context)
				{
						if (!context.Request.Headers.ContainsKey("Authorization"))
								return Task.FromResult(AuthenticateResult.Fail("No Authorization header found!"));

						string authHeader = context.Request.Headers["Authorization"];

						string bearerToken = authHeader?.Replace("Bearer ", string.Empty);
						IConfiguration config = (IConfiguration)context.RequestServices.GetService(typeof(IConfiguration))!;
						string keyFromConfig = config.GetValue<string>("ApiKey")!;

						if (!string.Equals(bearerToken, keyFromConfig, StringComparison.Ordinal))
						{
								return Task.FromResult(AuthenticateResult.Fail("Invalid token"));
						}

						var claims = new[]
						{
			new Claim(ClaimTypes.NameIdentifier, "1000"),
			new Claim(ClaimTypes.Name, "Deepu Madhusoodanan")
		};

						var identity = new ClaimsIdentity(claims, Scheme);

						var principal = new ClaimsPrincipal(identity);

						var ticket = new AuthenticationTicket(principal, Scheme);

						var authenticateResult = AuthenticateResult.Success(ticket);

						return Task.FromResult(authenticateResult);
				}

				public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy _,
					AuthenticateResult authenticationResult, HttpContext context,
					object resource)
				{
						var authorizeResult = authenticationResult.Succeeded
							? PolicyAuthorizationResult.Success()
							: PolicyAuthorizationResult.Challenge();

						return Task.FromResult(authorizeResult);
				}
		}
}
