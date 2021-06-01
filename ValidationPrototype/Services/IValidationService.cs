using ValidationPrototype.Models;

namespace ValidationPrototype.Services
{
	public interface IValidationService<T>
		where T : class
	{
		CustomValidationResult Validate(T model);
	}
}
