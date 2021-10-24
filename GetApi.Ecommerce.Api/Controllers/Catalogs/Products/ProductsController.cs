using GetApi.Ecommerce.Api.Controllers.Catalogs.Products.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Api.Controllers.Catalogs.Products
{
    [ApiController]
    [Route("products")]
    public class ProductsController: ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> List(CancellationToken cancellationToken)
        {
            return Ok(new {  Products = new List<ProductResponse>() });
        }
    }
}
