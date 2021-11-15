using FluentValidation;
using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Dtos.Validations;

namespace GetApi.Ecommerce.Core.Shared.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TElement> LinkMustBeValid<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new LinkValidatorProperty<T, TElement>());
        }

        public static IRuleBuilderOptions<T, SkuDto> MustBeValid<T>(this IRuleBuilder<T, SkuDto> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new SkuDtoValidator());
        }
        public static IRuleBuilderOptions<T, SellerDto> MustBeValid<T>(this IRuleBuilder<T, SellerDto> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new SellerDtoValidator());
        }
    }
}
