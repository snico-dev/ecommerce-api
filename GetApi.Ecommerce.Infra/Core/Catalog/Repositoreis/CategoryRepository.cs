using GetApi.Ecommerce.Core.Catalog.Entities;
using GetApi.Ecommerce.Core.Catalog.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Infra.Core.Catalog.Repositoreis
{
    public class CategoryRepository : ICategoryRepository
    {
        private IMongoCollection<Category> _collection;

        public CategoryRepository(IMongoCollection<Category> collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public async Task Create(Category category, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(category, new InsertOneOptions { BypassDocumentValidation = false }, cancellationToken);
        }

        public async Task<Category> Find(Expression<Func<Category, bool>> filter, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(filter, new FindOptions<Category> { }, cancellationToken);

            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> List(CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(Builders<Category>.Filter.Empty, 
                                            new FindOptions<Category> { }, 
                                            cancellationToken);
            
            return await cursor.ToListAsync();
        }

        public Task Update(Expression<Func<Category, bool>> filter, Category category, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
