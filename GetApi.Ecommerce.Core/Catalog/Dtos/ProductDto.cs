using GetApi.Ecommerce.Core.Catalog.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GetApi.Ecommerce.Core.Catalog.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ean { get; set; }
        public IEnumerable<SkuDto> Skus { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }

        public IEnumerable<Sku> MapToSkus()
        {
            return Skus.Select(x => new Sku()
            {
                Name = x.Name,
                Sellers = x.Sellers,
                Medias = x.Medias,
            }).ToArray();
        }
    }
}
