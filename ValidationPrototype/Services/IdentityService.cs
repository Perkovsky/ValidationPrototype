using System;

namespace ValidationPrototype.Services
{
	public class IdentityService : IIdentityService
	{
		public bool IsAuthorized(string url, string token)
		{
			return new Random().Next() % 2 > 0;
		}

		public bool IsDevelopment(string url)
		{
			return url == "localhost";
		}

		public bool IsInternalAuthorized(string token)
		{
			return token == "E706A287-DF38-492B-955A-C822A2E7DFD8";
		}
	}
}
