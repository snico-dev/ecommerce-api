using AutoFixture;
using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Entities;
using GetApi.Ecommerce.Infra.Core.Catalog.Repositoreis;
using GetApi.Ecommerce.UnitTests.Extensions;
using GetApi.Ecommerce.Infra.Wrappers;
using MongoDB.Driver;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Infra.Core.Catalog.Repositoreis
{
    public class CatalogRepositoryTests
    {
        public Mock<IMongoCollection<Product>> _collectionMock = new Mock<IMongoCollection<Product>>(MockBehavior.Strict);
        public Mock<IMongoPagginationWrapper> _mongoPagginationWrapperMock = new Mock<IMongoPagginationWrapper>(MockBehavior.Strict);
        private Fixture _fixture = new Fixture();

        [Fact]
        public async Task Given_Product_When_InsertOneAsyc_Should_SaveProductAsync()
        {
            // arrange
            var cancellationToken = new CancellationToken();
            var product = _fixture.Create<Product>();
            
            _collectionMock
                .Setup(x => x.InsertOneAsync(
                    It.Is<Product>(x => x.Id == product.Id), 
                    It.Is<InsertOneOptions>(x => x.BypassDocumentValidation == false),
                    cancellationToken)
                )
                .Returns(Task.CompletedTask)
                .Verifiable();

            // act
            await GetRepository().Create(product, cancellationToken);

            // assert
            _collectionMock.VerifyAll();
        }

        [Fact]
        public async Task Given_Product_When_List_Should_ListProductsWithPagination()
        {
            // arrange
            var cancellationToken = new CancellationToken();
            var product = _fixture.Create<Product>();

            _mongoPagginationWrapperMock
                .Setup(x => x.QueryByPageAsync(_collectionMock.Object, 1, 100, cancellationToken, null, null))
                .ReturnsAsync(() => new PaginationDto<Product>())
                .Verifiable();

            // act
            await GetRepository().List(1, 100, cancellationToken);

            // assert
            _collectionMock.VerifyAll();
        }

        [Fact]
        public async Task Given_Product_When_ListWithFilter_Should_ListProducts()
        {
            // arrange
            var cancellationToken = new CancellationToken();
            var product = _fixture.Create<Product>();

            _mongoPagginationWrapperMock
                .Setup(x => x.QueryByPageAsync(_collectionMock.Object, 1, 100, cancellationToken, It.Is<Expression<Func<Product, bool>>>(y => y.AreEquals(x => x.Id == product.Id)), null))
                .ReturnsAsync(() => new PaginationDto<Product>())
                .Verifiable();

            // act
            await GetRepository().List(x => x.Id == product.Id, 1, 100, cancellationToken);

            // assert
            _collectionMock.VerifyAll();
        }

        public CatalogRepository GetRepository() => 
            new CatalogRepository(_collectionMock.Object, _mongoPagginationWrapperMock.Object);
    }
}
