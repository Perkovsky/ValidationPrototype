using HybridModelBinding;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using ValidationPrototype.Attributes;
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
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> GetEntity([FromQuery] EntityDetailRequestModel model, CancellationToken cancellationToken)
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
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> GetEntities([FromQuery] EntityFilterRequestModel model, CancellationToken cancellationToken)
		{
			var result = await _entityService.GetEntitiesAsync(model, cancellationToken)
				.ConfigureAwait(false);

			return Ok(result);
		}

		/// <summary>
		/// Check binding mix-model
		/// </summary>
		[HttpPost("entities/{entityId}/sub-entity/{subEntityId}/check-post")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult CheckBindingPost([FromHybrid] CheckBindingRequestModel model)
		{
			return Ok(new { Status = "OK" });
		}

		/// <summary>
		/// Check binding mix-model
		/// </summary>
		[HttpPut("entities/{entityId}/sub-entity/{subEntityId}/check-put")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult CheckBindingPut([FromHybrid] CheckBindingRequestModel model)
		{
			return Ok(new { Status = "OK" });
		}

		/// <summary>
		/// Create entity: test exception
		/// </summary>
		[HttpPost("entities")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> CreateEntity(CancellationToken cancellationToken)
		{
			var result = await _entityService.CreateEntityAsync(cancellationToken)
				.ConfigureAwait(false);

			return Ok(result);
		}

		/// <summary>
		/// Check data annotation approach
		/// </summary>
		[AllowAnonymous]
		[HttpPost("check-data-annotation-approach")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult CheckDataAnnotationApproach(CheckDataAnnotationApproachRequestModel model)
		{
			return Ok(new { Status = "OK" });
		}

		/// <summary>
		/// Check validatable object approach
		/// </summary>
		[AllowAnonymous]
		[HttpPost("check-validatable-object-approach")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult CheckValidatableObjectApproach(CheckValidatableObjectApproachRequestModel model)
		{
			return Ok(new { Status = "OK" });
		}

		/// <summary>
		/// Check permissions
		/// </summary>
		[CheckPermissions("KeyBox")]
		[HttpPost("check-permissions")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult CheckPermissions()
		{
			return Ok(new { Status = "OK" });
		}

		/// <summary>
		/// Check permissions with action
		/// </summary>
		[CheckPermissions("KeyBox", Action = "Read")]
		[HttpPost("check-permissions-with-action")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult CheckPermissionsWithAction()
		{
			return Ok(new { Status = "OK" });
		}
	}
}
