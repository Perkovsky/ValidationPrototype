namespace ValidationPrototype.Services
{
	public interface IPermissionCheckerService
	{
		bool HasAccess(int userId, string feature, string action, string model);
	}
}
