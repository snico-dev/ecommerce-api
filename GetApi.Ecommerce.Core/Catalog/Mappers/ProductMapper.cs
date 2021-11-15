using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Core.Catalog.Mappers
{
    public static class ProductMapper
    {
        public static ValueTask<ProductDto[]> MapToDto(this IAsyncEnumerable<Product> products, IEnumerable<CategoryDto> categories)
        {
            return products.Select(x => MapToDto(x, categories)).ToArrayAsync();
        }

        public static IEnumerable<ProductDto> MapToDto(this IEnumerable<Product> products, IEnumerable<CategoryDto> categories)
        {
            return products.Select(x => MapToDto(x, categories)).ToArray();
        }

        public static ProductDto MapToDto(this Product product, IEnumerable<CategoryDto> categories)
        {
            var threeCategories = product.CategoryIds.SelectMany(x => GetParentCategories(x, categories)).ToArray();
            
            return new ProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Ean = product.Ean,
                Categories = threeCategories,
                Skus = product.Skus.Select(x => FromEntityToDto(x)).ToArray(),
            };
        }

        public static ICollection<CategoryDto> GetParentCategories(Guid? parentId, IEnumerable<CategoryDto> categories)
        {
            if (parentId.HasValue is false) return new List<CategoryDto> { };

            return categories
                        .Where(x => x.Id == parentId)
                        .SelectMany(parentCategory =>
                        {
                            var parentCategories = GetParentCategories(parentCategory.ParentId, categories);
                            parentCategories.Add(parentCategory);
                            return parentCategories;
                        }).ToList();
        }

        private static SkuDto FromEntityToDto(Sku sku)
        {
            return new SkuDto
            {
                Id = sku.Id,
                Name = sku.Name,
                Sellers = sku.Sellers,
                Medias = sku.Medias,
            };
        }
    }
}
