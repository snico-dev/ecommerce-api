using System.Collections.Generic;

namespace GetApi.Ecommerce.Core.Catalog.Dtos
{
    public class PaginationDto<T>
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
