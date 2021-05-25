using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationPrototype.Models;

namespace ValidationPrototype.Services
{
	public class EntityService : IEntityService
	{
		public Task<int> CreateEntityAsync(CancellationToken cancellationToken)
		{
			var rng = new Random();
			if (rng.Next() % 2 > 0)
				throw new ArgumentException("This is a message of BadRequest!");
			else
				throw new NotImplementedException("This is a message of InternalServerError!");
		}

		public Task<EntityDetailResponseModel> GetEntityAsync(EntityDetailRequestModel model, CancellationToken cancellationToken)
		{
			return Task.FromResult(new EntityDetailResponseModel
			{
				Id = model.Id,
				Name = "Some entity",
				Status = true
			});
		}
	}
}
