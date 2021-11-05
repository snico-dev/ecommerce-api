using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Entities;
using GetApi.Ecommerce.Core.Catalog.Repositories;
using GetApi.Ecommerce.Infra.Extensions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Infra.Core.Catalog.Repositoreis
{
    public class CatalogRepository : ICatalogRepository
    {
        private IMongoCollection<Product> _collection;

        public CatalogRepository(IMongoCollection<Product> collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public async Task Create(Product product, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(product, new InsertOneOptions { BypassDocumentValidation = false }, cancellationToken);
        }
       
        public Task<PaginationDto<Product>> List(Expression<Func<Product, bool>> filter, int page, int pageSize, CancellationToken cancellationToken) 
            => _collection.QueryByPageAsync(page, pageSize, cancellationToken, filter);

        public Task<PaginationDto<Product>> List(int page, int pageSize, CancellationToken cancellationToken) 
            => _collection.QueryByPageAsync(page, pageSize, cancellationToken);
    }
}
