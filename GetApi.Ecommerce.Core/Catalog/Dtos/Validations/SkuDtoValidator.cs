using FluentValidation;
using GetApi.Ecommerce.Core.Shared.Validators;

namespace GetApi.Ecommerce.Core.Catalog.Dtos.Validations
{
    public class SkuDtoValidator: AbstractValidator<SkuDto>
    {
        public SkuDtoValidator()
        {
            RuleFor(x => x.Name)
               .NotNull()
               .NotEmpty()
               .WithMessage("The field name is required");

            RuleFor(x => x.ImageUri)
               .NotNull()
               .NotEmpty()
               .WithMessage("The field image uri is required")
               .LinkMustBeValid()
               .WithMessage("The field image uri is not a valid link");

            RuleFor(x => x.Available)
                .NotNull()
                .WithMessage("The field available is required");

            RuleFor(x => x.AvailableQuantity)
                .NotNull()
                .WithMessage("This field is required")
                .GreaterThanOrEqualTo(1)
                .WithMessage("The field available quantity must be greater than or equal to 1");

            RuleFor(x => x.ListPrice)
              .NotNull()
              .WithMessage("The field list price is required")
              .GreaterThan(0.1m)
              .WithMessage("The field list price must be greater than or equal to 0.1");

            RuleFor(x => x.SalesPrice)
              .NotNull()
              .WithMessage("The field sales price is required")
              .GreaterThanOrEqualTo(0.1m)
              .WithMessage("The field sales price must be greater than or equal to 0.1");

            RuleFor(x => x.HasAwaysAvailable)
              .NotNull()
              .WithMessage("The field has aways available is required");
        }
    }
}
