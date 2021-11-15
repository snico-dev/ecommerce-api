using FluentValidation;
using GetApi.Ecommerce.Core.Shared.Validators;

namespace GetApi.Ecommerce.Core.Catalog.Dtos.Validations
{
    public class MediaDtoValidator : AbstractValidator<MediaDto>
    {
        public MediaDtoValidator()
        {
            RuleFor(x => x.ImageUri)
             .NotNull()
             .NotEmpty()
             .WithMessage("The field image uri is required")
             .LinkMustBeValid()
             .WithMessage("The field image uri is not a valid link");
        }
    }
}
