using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Linq.Expressions;
using ValidationPrototype.Services;

namespace ValidationPrototype.Validators
{
	public abstract class BaseValidator<T, TBusinessLogicValidationService> : AbstractValidator<T>
		where T : class
		where TBusinessLogicValidationService : IValidationService<T>
	{
		private readonly IValidationService<T> _validationService;

		protected bool isPrimitiveLogicValidationFaulted;

		public BaseValidator(IValidationService<T> validationService)
		{
			_validationService = validationService;

			PrimitiveLogicValidation();
			BusinessLogicValidation();
		}

		public new IRuleBuilderOptions<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> expression)
		{
			return base.RuleFor(expression)
				.Must(x => true)
				.OnAnyFailure(x => isPrimitiveLogicValidationFaulted = true);
		}

		protected virtual void PrimitiveLogicValidation()
		{
		}

		protected virtual void BusinessLogicValidation()
		{
			When(x => !isPrimitiveLogicValidationFaulted, () =>
				base.RuleFor(x => x)
					.Must((model, property, context) => BusinessLogicValidate(model, context))
					.WithName("Business Logic Validation")
			);
		}

		private bool BusinessLogicValidate(T model, PropertyValidatorContext context)
		{
			var result = _validationService.Validate(model);
			if (result.IsValid)
			{
				return true;
			}
			else
			{
				context.Rule.MessageBuilder = ctx =>
				{
					//TODO: add errors as an array
					return result.Errors.ToString();
				};
				return false;
			}
		}
	}
}
