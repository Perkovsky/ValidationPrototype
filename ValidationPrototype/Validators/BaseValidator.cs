using FluentValidation;
using ValidationPrototype.Services;

namespace ValidationPrototype.Validators
{
	public abstract class BaseValidator<T, TBusinessLogicValidationService> : AbstractValidator<T>
		where T : class
		where TBusinessLogicValidationService : IValidationService<T>
	{
		private readonly IValidationService<T> _validationService;

		public BaseValidator(IValidationService<T> validationService)
		{
			_validationService = validationService;

			CascadeMode = CascadeMode.Stop;

			PrimitiveLogicValidation();
			BusinessLogicValidation();
		}

		protected virtual void PrimitiveLogicValidation()
		{
		}

		protected virtual void BusinessLogicValidation()
		{
			RuleFor(x => x)
				.Must(x => _validationService.Validate(x));
		}
	}
}
