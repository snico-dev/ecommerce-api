using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Core.Catalog.Services
{
    public interface ICatalogService
    {
        public Task Create(ProductRequest product, CancellationToken cancellationToken);
        public Task<PaginationDto<ProductDto>> List(int page, int pageSize, CancellationToken cancellationToken);
    }
}
