using System;
using System.Collections.Generic;

namespace GetApi.Ecommerce.Core.Catalog.Dtos
{
    public class SkuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SellerDto> Sellers { get; set; }
        public IEnumerable<MediaDto> Medias { get; set; }
    }
}
