using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ValidationPrototype.Models;

namespace ValidationPrototype.Services
{
	public interface IEntityService
	{
		Task<int> CreateEntityAsync(CancellationToken cancellationToken);
		Task<EntityDetailResponseModel> GetEntityAsync(EntityDetailRequestModel model, CancellationToken cancellationToken);
		Task<IEnumerable<EntityDetailResponseModel>> GetEntitiesAsync(EntityFilterRequestModel model, CancellationToken cancellationToken);
	}
}
