using FluentValidation;
using System;
using ValidationPrototype.Services;
using ValidationPrototype.Validators;

namespace ValidationPrototype.Models
{
	public class EntityFilterRequestModel
	{
		public int BuildingId { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public string Search { get; set; }
	}

	public class EntityFilterRequestModelValidator : BaseValidator<EntityFilterRequestModel, IEntityValidationService>
	{
		public EntityFilterRequestModelValidator(IEntityValidationService entityValidationService)
			: base(entityValidationService)
		{
		}

		protected override void PrimitiveLogicValidation()
		{
			RuleFor(x => x.BuildingId)
				.NotNull()
				.NotEmpty()
				.WithMessage("{PropertyName} cannot be empty.");

			RuleFor(x => x.From)
				.NotNull()
				.NotEmpty()
				.WithMessage("{PropertyName} cannot be empty.");

			RuleFor(x => x.To)
				.NotNull()
				.NotEmpty()
				.WithMessage("{PropertyName} cannot be empty.");

			RuleFor(x => new { x.From, x.To })
				.Must(x => x.To > x.From)
				.WithMessage("To must be greater than From.");
		}
	}
}
