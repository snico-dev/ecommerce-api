using System;

namespace GetApi.Ecommerce.Core.OrderFrom.Dtos
{
    public class OrderFormItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid SkuId { get; set; }
        public string SkuName { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
