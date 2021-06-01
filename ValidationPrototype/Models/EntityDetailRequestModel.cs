using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using ValidationPrototype.Services;
using ValidationPrototype.Validators;

namespace ValidationPrototype.Models
{
	public class EntityDetailRequestModel
	{
		[FromRoute]
		public int Id { get; set; }

		public int UnitId { get; set; }
		public int BuildingId { get; set; }

		public DateTime From { get; set; }
		public DateTime To { get; set; }
	}

	public class EntityDetailRequestModelValidator : BaseValidator<EntityDetailRequestModel, IEntityValidationService>
	{
		public EntityDetailRequestModelValidator(IEntityValidationService entityValidationService)
			: base(entityValidationService)
		{
		}

		protected override void PrimitiveLogicValidation()
		{
			RuleFor(x => x.Id)
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

			RuleFor(x => new { x.BuildingId, x.UnitId })
				.Must(x => x.BuildingId > 0 || x.UnitId > 0)
				.WithName("BuildingId / UnitId")
				.WithMessage("Building ID or Unit ID is required.")
				.OnAnyFailure(x =>
				{
					 isPrimitiveLogicValidationFaulted = true;
					 Debug.WriteLine("This is a logic emulator.");
				});
		}
	}
}
