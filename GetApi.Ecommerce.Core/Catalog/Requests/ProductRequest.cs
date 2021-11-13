using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GetApi.Ecommerce.Core.Catalog.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<SkuDto> Skus { get; set; }
        public IEnumerable<Guid> CategoryIds { get; set; }

        public IEnumerable<Sku> MapToSkus()
        {
            return Skus.Select(x => new Sku()
            {
                Name = x.Name,
                SalesPrice = x.SalesPrice,
                ListPrice = x.ListPrice,
                AvailableQuantity = x.AvailableQuantity,
                HasAwaysAvailable = x.HasAwaysAvailable,
                Available = x.Available,
                ImageUri = x.ImageUri,
            }).ToArray();
        }
    }
}
