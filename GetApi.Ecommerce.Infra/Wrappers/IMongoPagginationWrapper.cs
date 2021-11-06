using GetApi.Ecommerce.Core.Catalog.Dtos;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Infra.Wrappers
{
    public interface IMongoPagginationWrapper
    {
        Task<PaginationDto<T>> QueryByPageAsync<T>(IMongoCollection<T> collection,
            int page,
            int pageSize,
            CancellationToken cancellationToken,
            Expression<Func<T, bool>> filter = null,
            SortDefinition<T> sort = null);
    }
}
