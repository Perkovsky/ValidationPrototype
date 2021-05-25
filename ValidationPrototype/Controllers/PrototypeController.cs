using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
		/// Get entity list: test request model validation
		/// </summary>
		[HttpGet("entities")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> GetEntities([FromQuery] EntityFilterRequestModel model, CancellationToken cancellationToken)
		{
			var result = await _entityService.GetEntitiesAsync(model, cancellationToken)
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

		/// <summary>
		/// Check data annotation approach
		/// </summary>
		[HttpPost("check-data-annotation-approach")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult CheckDataAnnotationApproach(CheckDataAnnotationApproachRequestModel model)
		{
			return Ok(new { Status = "OK" });
		}

		/// <summary>
		/// Check validatable object approach
		/// </summary>
		[HttpPost("check-validatable-object-approach")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult CheckValidatableObjectApproach(CheckValidatableObjectApproachRequestModel model)
		{
			return Ok(new { Status = "OK" });
		}
	}
}
