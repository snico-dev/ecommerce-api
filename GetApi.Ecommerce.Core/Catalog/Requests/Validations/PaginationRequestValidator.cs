using FluentValidation;

namespace GetApi.Ecommerce.Core.Catalog.Requests.Validations
{
    public class PaginationRequestValidator: AbstractValidator<PaginationRequest>
    {
        public PaginationRequestValidator()
        {
            RuleFor(x => x.Page)
                .NotNull()
                .WithMessage("The field page is required")
                .GreaterThanOrEqualTo(1)
                .WithMessage("The field page must be equals or greater than 1");

            RuleFor(x => x.PageSize)
                .NotNull()
                .WithMessage("The field page size required")
                .GreaterThanOrEqualTo(1)
                .WithMessage("The field page size must be equals or greater than 1");
        }
    }
}
