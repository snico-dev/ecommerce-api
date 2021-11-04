using GetApi.Ecommerce.Core.Shared.Entities;

namespace GetApi.Ecommerce.Core.Catalog.Entities
{
    public class Sku: Entity
    {
        public string Name { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal ListPrice { get; set; }
        public int AvailableQuantity { get; set; }
        public bool HasAwaysAvailable { get; set; }
        public bool Available { get; set; }
        public string ImageUri { get; set; }
    }
}
