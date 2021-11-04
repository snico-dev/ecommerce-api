using System.ComponentModel.DataAnnotations;

namespace GetApi.Ecommerce.Core.Catalog.Dtos
{
    public class SkuDto
    {
        [Required(ErrorMessage = "The field Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field SalesPrice is required")]
        public decimal SalesPrice { get; set; }

        [Required(ErrorMessage = "The field ListPrice is required")]
        public decimal ListPrice { get; set; }

        [Required(ErrorMessage = "The field AvailableQuantity is required")]
        public int AvailableQuantity { get; set; }

        [Required(ErrorMessage = "The field HasAwaysAvailable is required")]
        public bool HasAwaysAvailable { get; set; }

        [Required(ErrorMessage = "The field Available is required")]
        public bool Available { get; set; }

        [Required(ErrorMessage = "The field ImageUri is required")]
        public string ImageUri { get; set; }
    }
}
