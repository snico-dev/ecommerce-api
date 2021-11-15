using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Shared.Entities;
using System.Collections.Generic;

namespace GetApi.Ecommerce.Core.Catalog.Entities
{
    public class Sku: Entity
    {
        public string Name { get; set; }
        public IEnumerable<SellerDto> Sellers { get; set; }
        public IEnumerable<MediaDto> Medias { get; set; }
    }
}
