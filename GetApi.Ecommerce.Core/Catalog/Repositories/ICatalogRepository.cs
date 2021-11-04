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
        public Task Create(Product product, CancellationToken cancellationToken);
        public IAsyncEnumerable<Product> List(Expression<Func<Product, bool>> filter, int skip, int take, CancellationToken cancellationToken);
        public IAsyncEnumerable<Product> List(int skip, int take, CancellationToken cancellationToken);
    }
}
