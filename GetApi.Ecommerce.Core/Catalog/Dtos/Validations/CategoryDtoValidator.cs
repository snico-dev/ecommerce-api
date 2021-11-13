using FluentValidation;

namespace GetApi.Ecommerce.Core.Catalog.Dtos.Validations
{
    public class CategoryDtoValidator: AbstractValidator<CategoryDto> 
    {
        public CategoryDtoValidator()
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
