using AutoFixture;
using FluentValidation.TestHelper;
using GetApi.Ecommerce.Core.Catalog.Requests;
using GetApi.Ecommerce.Core.Catalog.Requests.Validations;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Core.Catalog.Requests.Validations
{
    public class ProductRequestValidatorTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void Given_InvalidProduct_When_Validate_Should_BeInvalid()
        {
            var product = new ProductRequest();

            var validation = new ProductRequestValidator();
            var result = validation.TestValidate(product);

            result.ShouldHaveValidationErrorFor(product => product.Name);
            result.ShouldHaveValidationErrorFor(product => product.Description);
            result.ShouldHaveValidationErrorFor(product => product.CategoryIds);
            result.ShouldHaveValidationErrorFor(product => product.Skus)
                    .WithErrorCode("PredicateValidator");
        }

        [Fact]
        public void Given_ValidProduct_When_Validate_Should_BeValid()
        {
            var product = _fixture.Create<ProductRequest>();

            var validation = new ProductRequestValidator();
            var result = validation.TestValidate(product);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.CategoryIds);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }
    }
}
