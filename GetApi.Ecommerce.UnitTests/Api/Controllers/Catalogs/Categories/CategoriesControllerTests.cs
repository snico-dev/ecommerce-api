using AutoFixture;
using FluentAssertions;
using GetApi.Ecommerce.Api.Controllers.Catalogs.Categories;
using GetApi.Ecommerce.Core.Catalog.Requests;
using GetApi.Ecommerce.Core.Catalog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Api.Controllers.Catalogs.Categories
{
    public class CategoriesControllerTests
    {
        private Mock<ICategoryService> _categoryServiceMock = new Mock<ICategoryService>();
        private Fixture _fixture = new Fixture();

        [Fact]
        public async Task Given_CategoryRequest_When_Create_ShouldReturnsStatus201Created()
        {
            // arrange
            var request = _fixture.Create<CategoryRequest>();
            var cancellationToken = new CancellationToken();

            _categoryServiceMock
                .Setup(x => x.Create(request, cancellationToken))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // act
            var response = await GetController().Create(request, _categoryServiceMock.Object, cancellationToken);

            // assert
            response
                .Should()
                .BeOfType<StatusCodeResult>()
                .Subject.StatusCode
                .Should()
                .Be(StatusCodes.Status201Created);

            _categoryServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Given_CategoryRequest_When_Create_ShouldReturnsStatus400BadRequest()
        {
            // arrange
            var request = _fixture.Create<CategoryRequest>();
            var cancellationToken = new CancellationToken();

            _categoryServiceMock
                .Setup(x => x.Create(request, cancellationToken))
                .Throws(new ValidationException())
                .Verifiable();

            // act
            var response = await GetController().Create(request, _categoryServiceMock.Object, cancellationToken);

            // assert
            response
                .Should()
                .BeOfType<BadRequestObjectResult>()
                .Subject.StatusCode
                .Should()
                .Be(StatusCodes.Status400BadRequest);

            _categoryServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Given_CategoryRequest_When_Create_ShouldReturnsStatus500InternalServerError()
        {
            // arrange
            var request = _fixture.Create<CategoryRequest>();
            var cancellationToken = new CancellationToken();

            _categoryServiceMock
                .Setup(x => x.Create(request, cancellationToken))
                .Throws(new Exception())
                .Verifiable();

            // act
            var response = await GetController().Create(request, _categoryServiceMock.Object, cancellationToken);

            // assert
            response
                .Should()
                .BeOfType<StatusCodeResult>()
                .Subject.StatusCode
                .Should()
                .Be(StatusCodes.Status500InternalServerError);

            _categoryServiceMock.VerifyAll();
        }

        public CategoriesController GetController(HttpContext httpContext = null)
        {
            if (httpContext is null) httpContext = new DefaultHttpContext();

            return new CategoriesController(Mock.Of<ILogger<CategoriesController>>())
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };
        }
    }
}
