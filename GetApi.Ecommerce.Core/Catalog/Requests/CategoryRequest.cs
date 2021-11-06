using GetApi.Ecommerce.Core.Catalog.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace GetApi.Ecommerce.Core.Catalog.Requests
{
    public class CategoryRequest: IValidatable
    {
        [Required(ErrorMessage = "The field Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field FriendlyUrl is required")]
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
