namespace ValidationPrototype.Services
{
	public interface IIdentityService
	{
		bool IsDevelopment(string url);
		bool IsAuthorized(string url, string token);
		bool IsInternalAuthorized(string token);
	}
}
