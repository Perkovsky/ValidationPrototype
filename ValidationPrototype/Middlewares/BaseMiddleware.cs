using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ValidationPrototype.Middlewares
{
	public class BaseMiddleware
	{
		protected TAttribute GetAttribute<TAttribute>(HttpContext context)
			where TAttribute : Attribute
		{
			var endpoint = context.GetEndpoint();
			return endpoint?.Metadata?.GetMetadata<TAttribute>();
		}

		protected bool IsMetadataDecoratedWithAttribute<TAttribute>(HttpContext context)
			where TAttribute : Attribute
		{
			return GetAttribute<TAttribute>(context) != null;
		}

		protected Task WriteAsync(HttpResponse response, int statusCode, string msg)
		{
			response.StatusCode = statusCode;
			return response.WriteAsync(msg, Encoding.UTF8);
		}
	}
}
