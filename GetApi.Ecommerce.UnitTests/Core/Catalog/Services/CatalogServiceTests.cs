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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Core.Catalog.Services
{

    public class CatalogServiceTests
    {
        private Mock<ICatalogRepository> _catalogRepositoryMock = new Mock<ICatalogRepository>(MockBehavior.Strict);
        private Mock<IListCategoriesService> _listCategoriesServiceMock = new Mock<IListCategoriesService>(MockBehavior.Strict);
        private Fixture _fixture = new Fixture();
        
        [Fact]
        public async Task Given_ProductData_When_Create_Should_SaveProductAsync()
        {
            // arrange
            var cancellationToken = new CancellationToken();
            
            var parent1Category = _fixture.Build<CategoryDto>().Without(x => x.ParentId).Create();
            var parent2Category = _fixture.Build<CategoryDto>().With(x => x.ParentId, parent1Category.Id).Create();
            var parent3Category = _fixture.Build<CategoryDto>().With(x => x.ParentId, parent2Category.Id).Create();
            
            var categories = new List<CategoryDto>() {
                parent1Category,
                parent2Category,
                parent3Category
            };

            var request = _fixture
                                .Build<ProductRequest>()
                                .With(x =>x.CategoryIds, new List<Guid> { parent3Category.Id })
                                .Create();

            _catalogRepositoryMock
                .Setup(x => x.Create(IsValid(request), cancellationToken))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _listCategoriesServiceMock
                .Setup(x => x.List(cancellationToken))
                .ReturnsAsync(categories);

            // act 
            await GetService().Create(request, cancellationToken);

            // assert
            _catalogRepositoryMock.VerifyAll();
            _listCategoriesServiceMock.VerifyAll();
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
                    .ThrowAsync<ValidationException>()
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
                    .ThrowAsync<ValidationException>()
                    .WithMessage("The product must have at least one sku");
        }

        public CatalogService GetService()
        {
            return new CatalogService(Mock.Of<ILogger<CatalogService>>(), 
                _catalogRepositoryMock.Object,
                _listCategoriesServiceMock.Object);
        }
    }
}
