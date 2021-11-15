using AutoFixture;
using FluentAssertions;
using GetApi.Ecommerce.Api.Controllers.Catalogs.Categories;
using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Requests;
using GetApi.Ecommerce.Core.Catalog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
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
        public async Task Given_Request_When_List_ShouldReturnsStatus200()
        {
            // arrange
            var result = _fixture.CreateMany<CategoryDto>();
            var cancellationToken = new CancellationToken();

            _categoryServiceMock
                .Setup(x => x.List(cancellationToken))
                .ReturnsAsync(result)
                .Verifiable();

            // act
            var response = await GetController().List(_categoryServiceMock.Object, cancellationToken);

            // assert
            var objectResult = response.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            var categories = objectResult.Value.Should().BeAssignableTo<IEnumerable<CategoryDto>>().Subject;
            categories.Should().BeEquivalentTo(result);
            _categoryServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Given_Request_When_List_And_ThereArentCategories_Then_ShouldReturnsStatus204NoContent()
        {
            // arrange
            var result = new List<CategoryDto>();
            var cancellationToken = new CancellationToken();

            _categoryServiceMock
                .Setup(x => x.List(cancellationToken))
                .ReturnsAsync(result)
                .Verifiable();

            // act
            var response = await GetController().List(_categoryServiceMock.Object, cancellationToken);

            // assert
            var objectResult = response.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
            _categoryServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Given_Request_When_List_ShouldReturnsStatus500InternalServerError()
        {
            // arrange
            var result = _fixture.CreateMany<CategoryDto>();
            var cancellationToken = new CancellationToken();

            _categoryServiceMock
                .Setup(x => x.List(cancellationToken))
                .Throws(new Exception())
                .Verifiable();

            // act
            var response = await GetController().List(_categoryServiceMock.Object, cancellationToken);

            // assert
            var objectResult = response.Should().BeOfType<StatusCodeResult>().Subject;
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _categoryServiceMock.VerifyAll();
        }

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
                .Throws(new InvalidOperationException())
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
