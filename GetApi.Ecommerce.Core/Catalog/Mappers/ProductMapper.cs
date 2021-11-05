using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Core.Catalog.Mappers
{
    public static class ProductMapper
    {
        public static ValueTask<ProductDto[]> MapToDto(this IAsyncEnumerable<Product> products, IEnumerable<Category> categories)
        {
            return products.Select(x => MapToDto(x, categories)).ToArrayAsync();
        }

        public static IEnumerable<ProductDto> MapToDto(this IEnumerable<Product> products, IEnumerable<Category> categories)
        {
            return products.Select(x => MapToDto(x, categories)).ToArray();
        }

        public static ProductDto MapToDto(this Product product, IEnumerable<Category> categories)
        {
            return new ProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Categories = categories.Where(x => product.CategoryIds.Contains(x.Id)).Select(x => FromEntityToDto(x)).ToArray(),
                Skus = product.Skus.Select(x => FromEntityToDto(x)).ToArray(),
            };
        }

        private static SkuDto FromEntityToDto(Sku sku)
        {
            return new SkuDto
            {
                Name = sku.Name,
                SalesPrice = sku.SalesPrice,
                ListPrice = sku.ListPrice,
                AvailableQuantity = sku.AvailableQuantity,
                HasAwaysAvailable = sku.HasAwaysAvailable,
                Available = sku.Available,
                ImageUri = sku.ImageUri,
            };
        }

        private static CategoryDto FromEntityToDto(Category category)
        {
            return new CategoryDto
            {
                Name = category.Name,
                ParentId = category.ParentId,
                FriendlyUrl = category.FriendlyUrl,
            };
        }
    }
}
