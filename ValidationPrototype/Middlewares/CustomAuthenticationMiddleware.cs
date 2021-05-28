using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using ValidationPrototype.Attributes;
using ValidationPrototype.Extensions;
using ValidationPrototype.Services;

namespace ValidationPrototype.Middlewares
{
	public class CustomAuthenticationMiddleware : BaseMiddleware
	{
		private readonly RequestDelegate _nextDelegate;

		public CustomAuthenticationMiddleware(RequestDelegate nextDelegate) => _nextDelegate = nextDelegate;

		#region Private Methods

		private Task WriteUnauthorizedAsync(HttpResponse response, string msg)
		{
			return WriteAsync(response, StatusCodes.Status401Unauthorized, msg);
		}

		/// <summary>
		/// Add "Authorization" to headers of request.
		/// <example>
		///		For example:
		///		<code>"Authorization": "4F0A6D68-3A85-4A38-8F08-D0D04E7E4785"</code>
		/// </example>
		/// </summary>
		/// <returns></returns>
		private async Task InternalAuthorizationAsync(HttpContext context, IIdentityService identityService)
		{
			if (identityService.IsInternalAuthorized(context.GetAuthorizationToken()))
				await _nextDelegate.Invoke(context);
			else
				await WriteUnauthorizedAsync(context.Response, "Authorization failed.");
		}

		private async Task CustomAuthorizationAsync(HttpContext context, IIdentityService identityService)
		{
			if (context.GetAuthorizationToken()?.Any() ?? false)
				await CheckCustomAuthorizationAsync(context, identityService);
			else
				await WriteUnauthorizedAsync(context.Response, "Api key is missing");
		}

		private async Task CheckCustomAuthorizationAsync(HttpContext context, IIdentityService identityService)
		{
			if (identityService.IsAuthorized(context.Request.Host.Host, context.GetAuthorizationToken()))
				await _nextDelegate.Invoke(context);
			else
				await WriteUnauthorizedAsync(context.Response, "Authorization failed.");
		}

		#endregion

		public async Task InvokeAsync(HttpContext context, IIdentityService identityService)
		{
			// [AllowAnonymous]
			if (IsMetadataDecoratedWithAttribute<AllowAnonymousAttribute>(context))
			{
				await _nextDelegate.Invoke(context);
			}

			// [InternalAuthorization]
			else if (IsMetadataDecoratedWithAttribute<InternalAuthorizationAttribute>(context))
			{
				await InternalAuthorizationAsync(context, identityService);
			}

			// Development
			else if (identityService.IsDevelopment(context.Request.Host.Host))
			{
				await _nextDelegate.Invoke(context);
			}

			// Custom Authorization (by default)
			else
			{
				await CustomAuthorizationAsync(context, identityService);
			}
		}
	}
}
