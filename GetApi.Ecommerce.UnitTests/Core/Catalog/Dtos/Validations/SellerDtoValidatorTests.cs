using AutoFixture;
using FluentValidation.TestHelper;
using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Dtos.Validations;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Core.Catalog.Dtos.Validations
{
    public class SellerDtoValidatorTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void Given_InvalidSku_When_Validate_Should_BeInvalid()
        {
            var sku = new SellerDto();

            var validation = new SellerDtoValidator();
            var result = validation.TestValidate(sku);

            result.ShouldHaveValidationErrorFor(sku => sku.Name);
            result.ShouldHaveValidationErrorFor(sku => sku.SalesPrice);
            result.ShouldHaveValidationErrorFor(sku => sku.AvailableQuantity);
            result.ShouldHaveValidationErrorFor(sku => sku.SalesPrice);
        }

        [Fact]
        public void Given_Sku_With_InvalidUri_When_Validate_Should_BeInvalid()
        {
            var sku = _fixture.Build<SellerDto>().Create();
            var validation = new SellerDtoValidator();
            var result = validation.TestValidate(sku);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
