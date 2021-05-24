using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationPrototype.Models;
using ValidationPrototype.Services;

namespace ValidationPrototype.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TestController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<TestController> _logger;
		private readonly IEntityService _entityService;

		public TestController(ILogger<TestController> logger, IEntityService entityService)
		{
			_logger = logger;
			_entityService = entityService;
		}

		/// <summary>
		/// Get list of permission requests
		/// </summary>
		[HttpGet("entities/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> GetEntity(EntityDetailRequestModel model, CancellationToken cancellationToken)
		{
			return Ok(await this._pmcPermissionRequestService.GetPermissionRequestList(
					accessStatus,
					from,
					to,
					openSession,
					buildings,
					unit,
					revokeStatus,
					search,
					pageSize > 0 && pageSize <= MAX_PAGE_SIZE ? pageSize : MAX_PAGE_SIZE,
					page > 0 ? page : 1,
					cancellationToken
				)
				.ConfigureAwait(false));
		}
	}
}
