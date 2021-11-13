using FluentValidation;
using GetApi.Ecommerce.Core.Shared.Validators;
using System.Linq;

namespace GetApi.Ecommerce.Core.Catalog.Requests.Validations
{
    public class ProductRequestValidator : AbstractValidator<ProductRequest>
    {
        public ProductRequestValidator()
        {
            RuleFor(x => x.CategoryIds)
                .NotNull()
                .WithMessage("The field category ids is required")
                .Must((x, context) => x.CategoryIds?.Any() is true)
                .WithMessage("The field category ids must be equals or greater than 1");

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("The field description is required")
                .MinimumLength(10)
                .WithMessage("The field description must have at least minimum 10 of length");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("The field name is required")
                .MinimumLength(10)
                .WithMessage("The field name must have at least minimum 10 of length");

            RuleFor(x => x.Skus)
                .Must((x, _) => x.Skus?.Any() is true)
                .WithMessage("The field skus must have at least one sku");

            RuleForEach(x => x.Skus)
                .NotNull()
                .WithMessage("The field skus is required")
                .MustBeValid();
        }
    }
}
