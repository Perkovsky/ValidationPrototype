using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValidationPrototype.Services;

namespace ValidationPrototype.Middlewares
{
	public class PermissionAccessValidationMiddleware
	{
		private readonly RequestDelegate _next;

		public PermissionAccessValidationMiddleware(RequestDelegate next) => _next = next;

		public async Task InvokeAsync(HttpContext context, IIdentityService identityService)
		{
			await _next.Invoke(context);
		}
	}
}
