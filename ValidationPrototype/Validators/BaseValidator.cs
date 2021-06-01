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
					.Custom((model, context) => BusinessLogicValidate(model, context))
			);
		}

		private void BusinessLogicValidate(T model, CustomContext context)
		{
			var result = _validationService.Validate(model);
			if (result.IsValid)
				return;
				
			var propertyName = "Business Logic Validation";
			foreach (var error in result.Errors)
			{
				context.AddFailure(propertyName, error);
			}
		}
	}
}
