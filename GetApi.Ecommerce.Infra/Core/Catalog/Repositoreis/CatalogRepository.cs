using GetApi.Ecommerce.Core.Catalog.Entities;
using GetApi.Ecommerce.Core.Catalog.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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

        public async IAsyncEnumerable<Product> List(Expression<Func<Product, bool>> filter, int skip, int take, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken);

            while (await cursor.MoveNextAsync())
            {
                foreach (var item in cursor.Current)
                {
                    yield return item;
                }
            }
        }

        public async IAsyncEnumerable<Product> List(int skip, int take, [EnumeratorCancellation]  CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(Builders<Product>.Filter.Empty, cancellationToken: cancellationToken);

            while (await cursor.MoveNextAsync())
            {
                foreach (var item in cursor.Current)
                {
                    yield return item;
                }
            }
        }
    }
}
