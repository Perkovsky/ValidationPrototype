using System;
using System.Text;
using ValidationPrototype.Models;

namespace ValidationPrototype.Services
{
	public class EntityValidationService : IEntityValidationService
	{
		private static CustomValidationResult BuildCustomValidationResult(StringBuilder sb)
		{
			return new CustomValidationResult
			{
				IsValid = sb.Length == 0,
				Errors = sb.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
			};
		}

		public CustomValidationResult Validate(EntityDetailRequestModel model)
		{
			static int GetUnitId(int buildingId) => new Random(buildingId).Next();

			int unitId = model.UnitId > 0 ? model.UnitId : GetUnitId(model.BuildingId);

			var errors = new StringBuilder();
			if (model.Id % 2 > 0)
				errors.AppendLine("Entity not found.");
			if (unitId > 10)
				errors.AppendLine("Unit not found.");
			if (unitId % 2 > 0)
				errors.AppendLine("The unit does not belong to the building.");

			return BuildCustomValidationResult(errors);
		}

		public CustomValidationResult Validate(EntityFilterRequestModel model)
		{
			var errors = new StringBuilder();
			if (model.BuildingId % 2 > 0)
				errors.AppendLine("Building not found.");

			return BuildCustomValidationResult(errors);
		}
	}
}
