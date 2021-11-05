using System.Collections.Generic;

namespace GetApi.Ecommerce.Core.Catalog.Dtos
{
    public class PaginationDto<T>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int NextPageNumber { get; set; }
        public int PreviousPageNumber { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        
        public IEnumerable<T> Data { get; set; }
    }
}
