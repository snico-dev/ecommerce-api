using AutoFixture;
using FluentAssertions;
using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Entities;
using GetApi.Ecommerce.Core.Catalog.Repositories;
using GetApi.Ecommerce.Core.Catalog.Requests;
using GetApi.Ecommerce.Core.Catalog.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Core.Catalog.Services
{

    public class CatalogServiceTests
    {
        private Mock<ICatalogRepository> _catalogRepositoryMock = new Mock<ICatalogRepository>();
        private Fixture _fixture = new Fixture();
        
        [Fact]
        public async Task Given_ProductData_When_Create_Should_SaveProductAsync()
        {
            // arrange
            var request = _fixture.Create<ProductRequest>();
            var cancellationToken = new CancellationToken();
            _catalogRepositoryMock
                .Setup(x => x.Create(IsValid(request), cancellationToken))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // act 
            await GetService().Create(request, cancellationToken);

            // assert
            _catalogRepositoryMock.VerifyAll();
        }

        private static Product IsValid(ProductRequest productDto)
        {
            return It.Is<Product>(x => x.Name == productDto.Name && productDto.Description == productDto.Description);
        }

        [Fact]
        public async Task Given_ProductDataWithoutCategories_When_Create_Should_ThrowInvalidOperationException()
        {
            // arrange
            var request = _fixture.Build<ProductRequest>().Without(x => x.CategoryIds).Create();
            var cancellationToken = new CancellationToken();

            // act 
            // assert
            await GetService()
                    .Invoking(x => x.Create(request, cancellationToken))
                    .Should()
                    .ThrowAsync<InvalidOperationException>()
                    .WithMessage("The product must have at least one category");
        }

        [Fact]
        public async Task Given_ProductDataWithoutSkus_When_Create_Should_ThrowInvalidOperationException()
        {
            // arrange
            var request = _fixture.Build<ProductRequest>().Without(x => x.Skus).Create();
            var cancellationToken = new CancellationToken();

            // act 
            // assert
            await GetService()
                    .Invoking(x => x.Create(request, cancellationToken))
                    .Should()
                    .ThrowAsync<InvalidOperationException>()
                    .WithMessage("The product must have at least one sku");
        }

        public CatalogService GetService()
        {
            return new CatalogService(Mock.Of<ILogger<CatalogService>>(), _catalogRepositoryMock.Object);
        }
    }
}
