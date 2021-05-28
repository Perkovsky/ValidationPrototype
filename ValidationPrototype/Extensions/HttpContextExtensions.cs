using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ValidationPrototype.Extensions
{
	public static class HttpContextExtensions
	{
		public static string GetAuthorizationToken(this HttpContext httpContext)
		{
			return httpContext.Request.Headers["Authorization"].FirstOrDefault();
		}
	}
}
