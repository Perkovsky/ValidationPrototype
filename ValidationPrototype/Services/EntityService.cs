using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
				throw new ValidationException("This is a message of BadRequest!");
			else
				throw new NotImplementedException("This is a message of InternalServerError!");
		}

		public Task<EntityDetailResponseModel> GetEntityAsync(EntityDetailRequestModel model, CancellationToken cancellationToken)
		{
			return Task.FromResult(new EntityDetailResponseModel
			{
				Id = model.Id,
				Name = $"Entity #{model.Id}",
				Status = true
			});
		}

		public Task<IEnumerable<EntityDetailResponseModel>> GetEntitiesAsync(EntityFilterRequestModel model, CancellationToken cancellationToken)
		{
			var result = new List<EntityDetailResponseModel>
			{
				new EntityDetailResponseModel { Id = 1, Name = "Entity #1", Status = true },
				new EntityDetailResponseModel { Id = 2, Name = "Entity #2", Status = true },
				new EntityDetailResponseModel { Id = 3, Name = "Entity #3", Status = true },
				new EntityDetailResponseModel { Id = 4, Name = "Entity #4", Status = true },
			};

			return Task.FromResult(result.AsEnumerable());
		}
	}
}
