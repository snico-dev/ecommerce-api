using GetApi.Ecommerce.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GetApi.Ecommerce.Core.Catalog.Entities
{
    public class Product: Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Ean { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public ICollection<Sku> Skus { get; private set; }
        public ICollection<Guid> CategoryIds { get; private set; }

        private Product(string name, string description, string ean) : base()
        {
            Name = name;
            Description = description;
            Ean = ean;
            Skus = new List<Sku>();
            CategoryIds = new List<Guid>();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Product Create(string name, string description, string ean)
        {
            return new Product(name, description, ean);
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

            CategoryIds = CategoryIds.Distinct().ToArray();
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
