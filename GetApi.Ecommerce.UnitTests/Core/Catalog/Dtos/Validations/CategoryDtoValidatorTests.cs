using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Dtos.Validations;
using Xunit;
using FluentValidation.TestHelper;
using AutoFixture;

namespace GetApi.Ecommerce.UnitTests.Core.Catalog.Dtos.Validations
{
    public class CategoryDtoValidatorTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void Given_InvalidCategory_When_Validate_Should_BeInvalid()
        {
            var category = new CategoryDto();

            var validation = new CategoryDtoValidator();
            var result = validation.TestValidate(category);

            result.ShouldHaveValidationErrorFor(category => category.Name);
            result.ShouldHaveValidationErrorFor(category => category.FriendlyUrl);
        }

        [Fact]
        public void Given_ValidCategory_When_Validate_Should_BeValid()
        {
            var category = _fixture.Create<CategoryDto>();

            var validation = new CategoryDtoValidator();
            var result = validation.TestValidate(category);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
