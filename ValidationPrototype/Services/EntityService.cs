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
		EntityDetailResponseModel GetEntity(EntityDetailRequestModel model, CancellationToken cancellationToken);
	}

	public class EntityService : IEntityService
	{

	}
}
