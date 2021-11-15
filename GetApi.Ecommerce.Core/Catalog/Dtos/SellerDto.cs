namespace GetApi.Ecommerce.Core.Catalog.Dtos
{
    public class SellerDto
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal ListPrice { get; set; }
        public int AvailableQuantity { get; set; }
        public bool HasAwaysAvailable { get; set; }
        public bool Available { get; set; }
    }
}
