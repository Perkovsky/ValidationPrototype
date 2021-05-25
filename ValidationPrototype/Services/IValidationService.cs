namespace ValidationPrototype.Services
{
	public interface IValidationService<T>
		where T : class
	{
		bool Validate(T model);
	}
}
