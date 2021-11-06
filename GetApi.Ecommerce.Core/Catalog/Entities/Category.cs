using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Shared.Entities;
using System;

namespace GetApi.Ecommerce.Core.Catalog.Entities
{
    public class Category : Entity
    {
        public Category(string name, string friendlyUrl, Guid? parentId) : base()
        {
            Name = name;
            FriendlyUrl = friendlyUrl;
            ParentId = parentId;
        }

        public string Name { get; private set; }
        public string FriendlyUrl { get; private set; }
        public Guid? ParentId { get; private set; }

        public static Category Create(string name, string friendlyUrl, Guid? parentId = null)
            => new Category(name, friendlyUrl, parentId);

        public void Update(CategoryDto categoryDto)
        {
            Name = categoryDto.Name;
            FriendlyUrl = categoryDto.FriendlyUrl;
            ParentId = categoryDto.ParentId;
        }
    }
}