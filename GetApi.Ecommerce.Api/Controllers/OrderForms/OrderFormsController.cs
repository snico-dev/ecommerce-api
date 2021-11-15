using GetApi.Ecommerce.Core.OrderForm.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Api.Controllers.OrderForms
{

    [ApiController]
    [Route("orderforms")]
    public class OrderFormsController: ControllerBase
    {
        private ILogger<OrderFormsController> _logger;

        public OrderFormsController(ILogger<OrderFormsController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderFormDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Requesting to get order form");

            try
            {
                return Ok(new { });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while get order form.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
