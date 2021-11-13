using FluentValidation;

namespace GetApi.Ecommerce.Core.Catalog.Requests.Validations
{
    public class CategoryRequestValidator: AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("The field name is required");

            RuleFor(x => x.FriendlyUrl)
                .NotNull()
                .NotEmpty()
                .WithMessage("The field friendly url is required");
        }
    }
}
