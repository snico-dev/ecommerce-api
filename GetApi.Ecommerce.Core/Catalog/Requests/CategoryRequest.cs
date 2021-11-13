using GetApi.Ecommerce.Core.Catalog.Dtos;
using System;

namespace GetApi.Ecommerce.Core.Catalog.Requests
{
    public class CategoryRequest
    {
        public string Name { get; set; }
        public string FriendlyUrl { get; set; }
        public Guid? ParentId { get; set; }

        public CategoryDto ToDto()
        {
            return new CategoryDto()
            {
                ParentId = ParentId,
                FriendlyUrl = FriendlyUrl,
                Name = Name
            };
        }
    }
}
