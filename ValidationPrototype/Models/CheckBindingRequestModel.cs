using FluentValidation;

namespace ValidationPrototype.Models
{
	public class CheckBindingRequestModel
	{
		public int EntityId { get; set; }
		public int SubEntityId { get; set; }
		public int CodeOperation { get; set; }
		public string Email { get; set; }
	}

	public class ECheckBindingRequestModelValidator : AbstractValidator<CheckBindingRequestModel>
	{
		public ECheckBindingRequestModelValidator()
		{
			RuleFor(x => x.EntityId)
				.NotNull()
				.NotEmpty()
				.WithMessage("{PropertyName} cannot be empty.");

			RuleFor(x => x.SubEntityId)
				.NotNull()
				.NotEmpty()
				.WithMessage("{PropertyName} cannot be empty.");

			RuleFor(x => x.CodeOperation)
				.NotEmpty()
				.WithMessage("{PropertyName} cannot be empty.")
				.GreaterThan(3)
				.LessThan(9);

			RuleFor(x => x.Email)
				.EmailAddress();
		}
	}
}
