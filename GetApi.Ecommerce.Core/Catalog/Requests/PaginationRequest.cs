using System.ComponentModel.DataAnnotations;

namespace GetApi.Ecommerce.Core.Catalog.Requests
{
    public class PaginationRequest : IValidatable
    {
        [Required(ErrorMessage = "The field Page is required")]
        public int Page { get; set; }

        [Required(ErrorMessage = "The field PageSize is required")]
        public int PageSize { get; set; }
    }
}
