using AutoFixture;
using FluentValidation.TestHelper;
using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Dtos.Validations;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Core.Catalog.Dtos.Validations
{
    public class SkuDtoValidatorTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void Given_InvalidSku_When_Validate_Should_BeInvalid()
        {
            var sku = new SkuDto();

            var validation = new SkuDtoValidator();
            var result = validation.TestValidate(sku);

            result.ShouldHaveValidationErrorFor(sku => sku.Name);
            result.ShouldHaveValidationErrorFor(sku => sku.Medias);
            result.ShouldHaveValidationErrorFor(sku => sku.Sellers);
        }

        [Fact]
        public void Given_ValidSku_When_Validate_Should_BeValid()
        {
            var sku = _fixture.Build<SkuDto>().Create();

            var validation = new SkuDtoValidator();
            var result = validation.TestValidate(sku);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Medias);
            result.ShouldNotHaveValidationErrorFor(x => x.Sellers);
        }
    }
}
