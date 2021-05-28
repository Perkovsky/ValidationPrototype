using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ValidationPrototype.Attributes;
using ValidationPrototype.Services;

namespace ValidationPrototype.Middlewares
{
	public class CustomAuthorizationMiddleware : BaseMiddleware
	{
		private readonly RequestDelegate _nextDelegate;

		public CustomAuthorizationMiddleware(RequestDelegate nextDelegate)
		{
			_nextDelegate = nextDelegate;
		}

		#region Private Methods

		private async Task CheckPermissionsAsync(HttpContext context, IPermissionCheckerService permissionCheckerService)
		{
			//TODO: get necessary params from query/body
			int userId = 44;
			var model = string.Empty;

			var attribute = GetAttribute<CheckPermissionsAttribute>(context);

			if (permissionCheckerService.HasAccess(userId, attribute.Feature, attribute.Action, model))
				await _nextDelegate.Invoke(context);
			else
				await WriteAsync(context.Response, StatusCodes.Status403Forbidden, "Authorization failed.");
		}

		#endregion

		public async Task InvokeAsync(HttpContext context, IPermissionCheckerService permissionCheckerService)
		{
			// [AllowAccessWithoutCheckPermissions]
			if (IsMetadataDecoratedWithAttribute<AllowAccessWithoutCheckPermissionsAttribute>(context))
			{
				await _nextDelegate.Invoke(context);
			}

			// [CheckPermissionsAttribute]
			else if (IsMetadataDecoratedWithAttribute<CheckPermissionsAttribute>(context))
			{
				await CheckPermissionsAsync(context, permissionCheckerService);
			}

			// Without any permission attributes (by default)
			else
			{
				await _nextDelegate.Invoke(context);
			}
		}
	}
}
