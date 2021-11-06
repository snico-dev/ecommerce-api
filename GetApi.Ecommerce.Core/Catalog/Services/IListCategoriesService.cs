using GetApi.Ecommerce.Core.Catalog.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Core.Catalog.Services
{
    public interface IListCategoriesService
    {
        Task<IEnumerable<CategoryDto>> List(CancellationToken cancellationToken);
    }
}
