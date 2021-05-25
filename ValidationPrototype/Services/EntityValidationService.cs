using System;
using System.ComponentModel.DataAnnotations;
using ValidationPrototype.Models;

namespace ValidationPrototype.Services
{
	public class EntityValidationService : IEntityValidationService
	{
		public bool Validate(EntityDetailRequestModel model)
		{
			static int GetUnitId(int buildingId) => new Random(buildingId).Next();

			if (model.Id % 2 > 0)
				throw new ValidationException("Entity not found.");

			int unitId = model.UnitId > 0 ? model.UnitId : GetUnitId(model.BuildingId);
			if (unitId % 2 > 0)
				throw new ValidationException("The unit does not belong to the building.");

			return true;
		}

		public bool Validate(EntityFilterRequestModel model)
		{
			if (model.BuildingId % 2 > 0)
				throw new ValidationException("Building not found.");

			return true;
		}
	}
}
