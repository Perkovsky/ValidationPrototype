using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValidationPrototype.Middlewares
{
	public class PermissionAccessValidationMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<PermissionAccessValidationMiddleware> _logger;

		public PermissionAccessValidationMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
		{
			_next = next;
			_logger = loggerFactory?.CreateLogger<PermissionAccessValidationMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
		}

		public async Task InvokeAsync(HttpContext context/*, IPermissionAccessValidationService ValidationService*/)
		{
			await _next.Invoke(context);
		}
	}
}
