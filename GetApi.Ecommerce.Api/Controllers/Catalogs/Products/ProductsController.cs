using GetApi.Ecommerce.Api.Helpers;
using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Requests;
using GetApi.Ecommerce.Core.Catalog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Api.Controllers.Catalogs.Products
{
    [ApiController]
    [Route("catalogs/products")]
    public class ProductsController : ControllerBase
    {
        private ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginationDto<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> List([FromQuery] PaginationRequest request,
            [FromServices] ICatalogService service,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Requesting to list products");

            var (isValid, validationResults) = request.Validate();

            if (isValid is false) return BadRequest(validationResults);

            try
            {
                var pagination = await service.List(request.Page, request.PageSize, cancellationToken);

                if (pagination.Data.Any() is false) return NoContent();

                return Ok(pagination);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while list products.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromBody] ProductRequest request,
            [FromServices] ICatalogService service,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Requesting to create a product");

            var (isValid, validationResults) = request.Validate();

            if (isValid is false) return BadRequest(validationResults);

            try
            {
                await service.Create(request, cancellationToken);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while creating product.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
