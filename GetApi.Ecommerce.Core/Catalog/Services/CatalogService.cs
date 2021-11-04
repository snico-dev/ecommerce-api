using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Entities;
using GetApi.Ecommerce.Core.Catalog.Mappers;
using GetApi.Ecommerce.Core.Catalog.Repositories;
using GetApi.Ecommerce.Core.Catalog.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Core.Catalog.Services
{
    public class CatalogService : ICatalogService
    {
        private ICatalogRepository _repository;
        private ILogger<CatalogService> _logger;

        public CatalogService(ILogger<CatalogService> logger,
            ICatalogRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository)); ;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Create(ProductRequest productDto, CancellationToken cancellationToken)
        {
            var product = Product.Create(productDto.Name, productDto.Description);

            if (HasValue(productDto.CategoryIds) is false)
                throw new InvalidOperationException("The product must have at least one category");
            
            if (HasValue(productDto.Skus) is false)
                throw new InvalidOperationException("The product must have at least one sku");

            product.AddCategoryRange(productDto.CategoryIds);
            product.AddSkuRange(productDto.MapToSkus());

            await _repository.Create(product, cancellationToken);
        }

        public bool HasValue<T>(IEnumerable<T> list)
        {
            return list != null && list.Any();
        }

        public async Task<PaginationDto<ProductDto>> List(int page, int pageSize, CancellationToken cancellationToken)
        {
            var cursor = _repository.List(0, 1000, cancellationToken);

            return new PaginationDto<ProductDto> { Data = await cursor.MapToDto(new List<Category>() { }) };
        }
    }
}
