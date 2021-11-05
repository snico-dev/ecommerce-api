using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Core.Catalog.Repositories
{
    public interface ICatalogRepository
    {
        Task Create(Product product, CancellationToken cancellationToken);
        Task<PaginationDto<Product>> List(Expression<Func<Product, bool>> filter, int page, int pageSize, CancellationToken cancellationToken);
        Task<PaginationDto<Product>> List(int page, int pageSize, CancellationToken cancellationToken);
    }
}
