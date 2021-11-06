using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Requests;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Core.Catalog.Services
{
    public interface ICategoryService : IListCategoriesService
    {
        Task Create(CategoryRequest categoryDto, CancellationToken cancellationToken);
        Task Update(Guid id, CategoryRequest categoryDto, CancellationToken cancellationToken);
    }
}
