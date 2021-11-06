using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Entities;

namespace GetApi.Ecommerce.Core.Catalog.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto MapToDto(this Category category)
        {
            return new CategoryDto()
            {
                Id = category.Id,
                FriendlyUrl = category.FriendlyUrl,
                Name = category.Name,
                ParentId = category.ParentId
            };
        }
    }
}
