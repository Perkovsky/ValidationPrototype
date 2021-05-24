﻿using Microsoft.AspNetCore.Http;
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
	[Route("api/prototype/")]
	public class PrototypeController : ControllerBase
	{
		private readonly ILogger<PrototypeController> _logger;
		private readonly IEntityService _entityService;

		public PrototypeController(ILogger<PrototypeController> logger, IEntityService entityService)
		{
			_logger = logger;
			_entityService = entityService;
		}

		/// <summary>
		/// Get entity detail: test request model validation
		/// </summary>
		[HttpGet("entities/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> GetEntity([FromQuery]EntityDetailRequestModel model, CancellationToken cancellationToken)
		{
			var result = await _entityService.GetEntityAsync(model, cancellationToken)
				.ConfigureAwait(false);

			return Ok(result);
		}

		/// <summary>
		/// Create entity: test exception
		/// </summary>
		[HttpPost("entities")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> CreateEntity(CancellationToken cancellationToken)
		{
			var result = await _entityService.CreateEntityAsync(cancellationToken)
				.ConfigureAwait(false);

			return Ok(result);
		}
	}
}