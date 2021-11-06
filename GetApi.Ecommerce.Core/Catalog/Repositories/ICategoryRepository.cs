using GetApi.Ecommerce.Core.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Core.Catalog.Repositories
{
    public interface ICategoryRepository
    {
        Task Create(Category category, CancellationToken cancellationToken);
        Task<IEnumerable<Category>> List(CancellationToken cancellationToken);
        Task<Category> Find(Expression<Func<Category, bool>> filter, CancellationToken cancellationToken);
        Task Update(Expression<Func<Category, bool>> filter, Category category, CancellationToken cancellationToken);
    }
}
