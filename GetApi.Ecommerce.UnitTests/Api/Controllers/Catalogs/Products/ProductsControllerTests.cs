using AutoFixture;
using FluentAssertions;
using GetApi.Ecommerce.Api.Controllers.Catalogs.Products;
using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Requests;
using GetApi.Ecommerce.Core.Catalog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Api.Controllers.Catalogs.Products
{
    public class ProductsControllerTests
    {
        private Mock<ICatalogService> _catalogServiceMock = new Mock<ICatalogService>(MockBehavior.Strict);
        private Fixture _fixture = new Fixture();

        [Fact]
        public async Task Given_ProductRequest_When_Create_ShouldReturnsStatus201Created()
        {
            // arrange
            var request = _fixture.Create<ProductRequest>();
            var cancellationToken = new CancellationToken();

            _catalogServiceMock
                .Setup(x => x.Create(request, cancellationToken))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // act
            var response = await GetController().Create(request, _catalogServiceMock.Object, cancellationToken);

            // assert
            response
                .Should()
                .BeOfType<StatusCodeResult>()
                .Subject.StatusCode
                .Should()
                .Be(StatusCodes.Status201Created);

            _catalogServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Given_ProductRequest_When_Create_And_ThrowException_ShouldShouldReturnsStatus500InternalServerError()
        {
            // arrange
            var request = _fixture.Create<ProductRequest>();
            var cancellationToken = new CancellationToken();

            _catalogServiceMock
                .Setup(x => x.Create(request, cancellationToken))
                .Throws(new Exception())
                .Verifiable();

            // act
            var response = await GetController().Create(request, _catalogServiceMock.Object, cancellationToken);

            // assert
            response
                .Should()
                .BeOfType<StatusCodeResult>()
                .Subject.StatusCode
                .Should()
                .Be(StatusCodes.Status500InternalServerError);

            _catalogServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Given_ProductRequest_When_List_ShouldReturnsStatus200()
        {
            // arrange
            var request = _fixture.Create<PaginationRequest>();
            var paginationResult = _fixture.Create<PaginationDto<ProductDto>>();
            var cancellationToken = new CancellationToken();

            _catalogServiceMock
                .Setup(x => x.List(request.Page, request.PageSize, cancellationToken))
                .ReturnsAsync(paginationResult)
                .Verifiable();

            // act
            var response = await GetController().List(request, _catalogServiceMock.Object, cancellationToken);

            // assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeOfType<PaginationDto<ProductDto>>();
            result.Value.Should().Be(paginationResult);

            _catalogServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Given_ProductRequest_When_List_And_ThrowException_ShouldReturnsStatus500InternalServerError()
        {
            // arrange
            var request = _fixture.Create<PaginationRequest>();
            var paginationResult = _fixture.Create<PaginationDto<ProductDto>>();
            var cancellationToken = new CancellationToken();

            _catalogServiceMock
                .Setup(x => x.List(request.Page, request.PageSize, cancellationToken))
                .Throws(new Exception())
                .Verifiable();

            // act
            var response = await GetController().List(request, _catalogServiceMock.Object, cancellationToken);

            // assert
            response
               .Should()
               .BeOfType<StatusCodeResult>()
               .Subject.StatusCode
               .Should()
               .Be(StatusCodes.Status500InternalServerError);

            _catalogServiceMock.VerifyAll();
        }

        public ProductsController GetController(HttpContext httpContext = null)
        {
            if (httpContext is null) httpContext = new DefaultHttpContext();

            return new ProductsController(Mock.Of<ILogger<ProductsController>>())
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };
        }
    }
}
