using AutoFixture;
using GetApi.Ecommerce.Core.Catalog.Entities;
using GetApi.Ecommerce.Infra.Core.Catalog.Repositoreis;
using GetApi.Ecommerce.UnitTests.Helpers;
using MongoDB.Driver;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Infra.Core.Catalog.Repositoreis
{
    public class CategoryRepositoryTests
    {
        private Mock<IMongoCollection<Category>> _collectionMock = new Mock<IMongoCollection<Category>>();
        private Fixture _fixture = new Fixture();

        [Fact]
        public async Task Given_Category_When_Create_Should_InsertCategory()
        {
            // arrange
            var category = _fixture.Create<Category>();
            var cancellationToken = new CancellationToken();

            _collectionMock
               .Setup(x => x.InsertOneAsync(
                   It.Is<Category>(x => x.Id == category.Id),
                   It.Is<InsertOneOptions>(x => x.BypassDocumentValidation == false),
                   cancellationToken)
               )
               .Returns(Task.CompletedTask)
               .Verifiable();

            // act
            await GetRepository().Create(category, cancellationToken);

            // assert
            _collectionMock.VerifyAll();
        }

        [Fact]
        public async Task Given_Filter_When_Find_Should_ReturnsCategory()
        {
            // arrange
            var category = _fixture.Create<Category>();
            var cancellationToken = new CancellationToken();

            var cursorMock = new Mock<IAsyncCursor<Category>>();
            var findOptions = new FindOptions<Category> { };

            cursorMock
                .Setup(x => x.MoveNextAsync(cancellationToken))
                .ReturnsAsync(true)
                .Verifiable();

            cursorMock
                .SetupGet(x => x.Current)
                .Returns(new List<Category> { category })
                .Verifiable();

            _collectionMock
               .Setup(x => x.FindAsync(
                   MoqHelpers.IsValidFilter<Category>(x => x.Id == category.Id),
                   It.Is<FindOptions<Category>>(x => x.BatchSize == findOptions.BatchSize),
                   cancellationToken)
               )
               .ReturnsAsync(cursorMock.Object)
               .Verifiable();

            // act
            await GetRepository().Find(x => x.Id == category.Id, cancellationToken);

            // assert
            _collectionMock.VerifyAll();
            cursorMock.VerifyAll();
        }

        [Fact]
        public async Task Given_Filter_When_List_Should_ReturnsCategory()
        {
            // arrange
            var category = _fixture.Create<Category>();
            var cancellationToken = new CancellationToken();

            var cursorMock = new Mock<IAsyncCursor<Category>>();
            var findOptions = new FindOptions<Category> { };

            cursorMock
                .SetupSequence(x => x.MoveNextAsync(cancellationToken))
                .ReturnsAsync(true)
                .ReturnsAsync(false);

            cursorMock
                .SetupGet(x => x.Current)
                .Returns(new List<Category> { category })
                .Verifiable();

            _collectionMock
               .Setup(x => x.FindAsync(MoqHelpers.IsValidFilter(Builders<Category>.Filter.Empty),
                   It.Is<FindOptions<Category>>(x => x.BatchSize == findOptions.BatchSize),
                   cancellationToken))
               .ReturnsAsync(cursorMock.Object)
               .Verifiable();

            // act
            await GetRepository().List(cancellationToken);

            // assert
            _collectionMock.VerifyAll();
            cursorMock.VerifyAll();
        }

        public CategoryRepository GetRepository() 
            => new CategoryRepository(_collectionMock.Object);
    }
}
