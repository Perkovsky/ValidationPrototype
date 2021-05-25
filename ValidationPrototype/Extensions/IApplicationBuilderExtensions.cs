using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ValidationPrototype.Middlewares;

namespace ValidationPrototype.Extensions
{
	public static class IApplicationBuilderExtensions
	{
		public static IApplicationBuilder UsePermissionAccessValidation(this IApplicationBuilder app)
		{
			return app.UseMiddleware<PermissionAccessValidationMiddleware>();
		}

		#region GlobalErrorHandling

		private static int GetStatusCode(Exception ex)
		{
			// Status = 400
			if (ex is ValidationException)
				return StatusCodes.Status400BadRequest;

			//// Status = 401
			//if (ex is AuthorizationFailedException)
			//	return StatusCodes.Status401Unauthorized;

			//// Status = 403
			//if (ex is AccessDeniedException)
			//	return StatusCodes.Status403Forbidden;

			//// Status = 404
			//if (ex is NotFoundException)
			//	return StatusCodes.Status404NotFound;

			// Status = 499
			if (ex is OperationCanceledException || ex is TaskCanceledException)
				return 499; // Client Closed Request

			// Status = 500
			return StatusCodes.Status500InternalServerError;
		}

		public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder app)
		{
			// .NET 5.x
			return app.UseExceptionHandler(c => c.Run(async context =>
			{
				var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
				var exception = exceptionHandlerPathFeature.Error;

				context.Response.StatusCode = GetStatusCode(exception);
				await context.Response.WriteAsJsonAsync(new { error = exception.Message });
			}));

			//// .NET Core (older versions)
			//return app.UseExceptionHandler(a => a.Run(async context =>
			//{
			//	var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
			//	var exception = exceptionHandlerPathFeature.Error;

			//	context.Response.StatusCode = GetStatusCode(exception);
			//	context.Response.ContentType = "application/json";
			//	await context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = exception.Message }));
			//}));
		}

		#endregion
	}
}
