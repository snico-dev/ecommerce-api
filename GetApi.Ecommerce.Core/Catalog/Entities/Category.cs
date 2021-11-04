using GetApi.Ecommerce.Core.Shared.Entities;
using System;

namespace GetApi.Ecommerce.Core.Catalog.Entities
{
    public class Category : Entity
    {
        public Category(string name, string friendlyUrl, Guid? parentId)
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
    }
}