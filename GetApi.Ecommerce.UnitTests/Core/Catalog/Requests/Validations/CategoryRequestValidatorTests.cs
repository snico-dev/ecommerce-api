using AutoFixture;
using FluentValidation.TestHelper;
using GetApi.Ecommerce.Core.Catalog.Requests;
using GetApi.Ecommerce.Core.Catalog.Requests.Validations;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Core.Catalog.Requests.Validations
{
    public class CategoryRequestValidatorTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void Given_InvalidCategory_When_Validate_Should_BeInvalid()
        {
            var category = new CategoryRequest();

            var validation = new CategoryRequestValidator();
            var result = validation.TestValidate(category);

            result.ShouldHaveValidationErrorFor(category => category.Name);
            result.ShouldHaveValidationErrorFor(category => category.FriendlyUrl);
        }

        [Fact]
        public void Given_ValidCategory_When_Validate_Should_BeValid()
        {
            var category = _fixture.Create<CategoryRequest>();

            var validation = new CategoryRequestValidator();
            var result = validation.TestValidate(category);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
