using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationPrototype.Models;

namespace ValidationPrototype.Services
{
	public interface IEntityService
	{
		Task<int> CreateEntityAsync(CancellationToken cancellationToken);
		Task<EntityDetailResponseModel> GetEntityAsync(EntityDetailRequestModel model, CancellationToken cancellationToken);
	}

	public class EntityService : IEntityService
	{
		public Task<int> CreateEntityAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException("This is a test exception message!");
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
