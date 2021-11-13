using AutoFixture;
using FluentValidation.TestHelper;
using GetApi.Ecommerce.Core.Catalog.Requests;
using GetApi.Ecommerce.Core.Catalog.Requests.Validations;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Core.Catalog.Requests.Validations
{
    public class PaginationRequestValidatorTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void Given_InvalidPagination_When_Validate_Should_BeInvalid()
        {
            var pagination = new PaginationRequest();

            var validation = new PaginationRequestValidator();
            var result = validation.TestValidate(pagination);

            result.ShouldHaveValidationErrorFor(pagination => pagination.Page);
            result.ShouldHaveValidationErrorFor(pagination => pagination.PageSize);
        }

        [Fact]
        public void Given_ValidPagination_When_Validate_Should_BeValid()
        {
            var category = _fixture.Create<PaginationRequest>();

            var validation = new PaginationRequestValidator();
            var result = validation.TestValidate(category);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
