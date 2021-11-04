using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GetApi.Ecommerce.Core.Catalog.Requests
{
    public class ProductRequest : IValidatable
    {
        [Required(ErrorMessage = "The field Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field Skus is required")]
        public IEnumerable<SkuDto> Skus { get; set; }

        [Required(ErrorMessage = "The field CategoryIds is required")]
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
