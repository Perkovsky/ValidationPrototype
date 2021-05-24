using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
		{
			return app.UseExceptionHandler(errorApp =>
			{
				//errorApp.Run(async context =>
				//{
				//	var emailSender = errorApp.ApplicationServices.GetService<IEmailSendService>();

				//	context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				//	context.Response.ContentType = "application/json";

				//	var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
				//	if (contextFeature != null)
				//	{
				//		string msg = "Internal Server Error from .NET Core (Global Error Handler)";
				//		Log.Error(contextFeature.Error, msg);
				//		emailSender.SendCriticalFailedEmail(contextFeature.Error, msg);
				//		await context.Response.WriteAsync(contextFeature.Error.Message);
				//	}
				//});
			});
		}
	}
}
