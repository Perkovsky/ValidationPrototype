using System;

namespace ValidationPrototype.Services
{
	public class PermissionCheckerService : IPermissionCheckerService
	{
		public bool HasAccess(int userId, string feature, string action, string model)
		{
			//TODO: cast model by feature

			return new Random().Next() % 2 > 0;
		}
	}
}
