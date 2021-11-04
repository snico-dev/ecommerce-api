using System;
using System.Collections.Generic;

namespace GetApi.Ecommerce.Core.Catalog.Entities
{
    public class Product
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public ICollection<Sku> Skus { get; private set; }
        public ICollection<Guid> CategoryIds { get; private set; }

        private Product(string name, string description)
        {
            Name = name;
            Description = description;
            Skus = new List<Sku>();
            CategoryIds = new List<Guid>();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Product Create(string name, string description)
        {
            return new Product(name, description);
        }

        public void AddSkuRange(IEnumerable<Sku> skus)
        {
            foreach (var sku in skus)
            {
                Skus.Add(sku);
            }
        }

        public void AddCategoryRange(IEnumerable<Guid> categoriesIds)
        {
            foreach (var category in categoriesIds)
            {
                CategoryIds.Add(category);
            }
        }

        public void Inactive()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Active()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
