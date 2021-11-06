using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Entities;
using GetApi.Ecommerce.Core.Catalog.Repositories;
using GetApi.Ecommerce.Infra.Extensions;
using GetApi.Ecommerce.Infra.Wrappers;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Infra.Core.Catalog.Repositoreis
{
    public class CatalogRepository : ICatalogRepository
    {
        private IMongoCollection<Product> _collection;
        private IMongoPagginationWrapper _pagginationWrapper;

        public CatalogRepository(IMongoCollection<Product> collection, IMongoPagginationWrapper pagginationWrapper)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
            _pagginationWrapper = pagginationWrapper ?? throw new ArgumentNullException(nameof(pagginationWrapper));
        }

        public async Task Create(Product product, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(
                product, 
                new InsertOneOptions { BypassDocumentValidation = false }, 
                cancellationToken);
        }
       
        public Task<PaginationDto<Product>> List(Expression<Func<Product, bool>> filter, int page, int pageSize, CancellationToken cancellationToken) 
            => _pagginationWrapper.QueryByPageAsync(_collection, page, pageSize, cancellationToken, filter);

        public Task<PaginationDto<Product>> List(int page, int pageSize, CancellationToken cancellationToken) 
            => _pagginationWrapper.QueryByPageAsync(_collection, page, pageSize, cancellationToken);
    }
}
