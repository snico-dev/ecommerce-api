using System;

namespace GetApi.Ecommerce.Core.Catalog.Dtos
{
    public class SkuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal ListPrice { get; set; }
        public int AvailableQuantity { get; set; }
        public bool HasAwaysAvailable { get; set; }
        public bool Available { get; set; }
        public string ImageUri { get; set; }
    }
}
