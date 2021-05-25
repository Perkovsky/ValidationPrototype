using ValidationPrototype.Models;

namespace ValidationPrototype.Services
{
	public interface IEntityValidationService : 
		IValidationService<EntityDetailRequestModel>,
		IValidationService<EntityFilterRequestModel>
	{
	}
}
