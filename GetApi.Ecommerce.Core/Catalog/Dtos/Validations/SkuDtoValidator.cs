using FluentValidation;
using GetApi.Ecommerce.Core.Shared.Validators;
using System.Linq;

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

            RuleFor(x => x.Sellers)
              .NotNull()
              .WithMessage("The field seller is required")
              .Must((x, _) => x.Sellers?.Any() is true)
              .WithMessage("The field sellers must have at least one seller");
            
            RuleForEach(x => x.Sellers).MustBeValid();

            RuleFor(x => x.Medias)
              .NotNull()
              .WithMessage("The field medias is required")
              .Must((x, _) => x.Medias?.Any() is true)
              .WithMessage("The field medias must have at least one media");

            RuleForEach(x => x.Medias).MustBeValid();
        }
    }
}
