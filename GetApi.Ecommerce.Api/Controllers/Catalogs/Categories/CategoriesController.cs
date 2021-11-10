using GetApi.Ecommerce.Api.Helpers;
using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Requests;
using GetApi.Ecommerce.Core.Catalog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Api.Controllers.Catalogs.Categories
{
    [ApiController]
    [Route("catalogs/categories")]
    public class CategoriesController: ControllerBase
    {
        private ILogger<CategoriesController> _logger;

        public CategoriesController(ILogger<CategoriesController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> List([FromServices] ICategoryService service, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Requesting to list categories");

            try
            {
                var categories = await service.List(cancellationToken);

                if (categories is null || categories.Any() is false) return NoContent();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while list categories");

                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromBody] CategoryRequest request, 
            [FromServices] ICategoryService service, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Requesting to list categories");

            var (isValid, validationResults) = request.Validate();

            if (isValid is false) return BadRequest(validationResults);

            try
            {
                await service.Create(request, cancellationToken);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.ValidationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while list categories");

                return StatusCode(500);
            }
        }
    }
}
