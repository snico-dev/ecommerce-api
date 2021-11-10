using AutoFixture;
using FluentAssertions;
using GetApi.Ecommerce.Core.Catalog.Entities;
using GetApi.Ecommerce.Core.Catalog.Repositories;
using GetApi.Ecommerce.Core.Catalog.Requests;
using GetApi.Ecommerce.Core.Catalog.Services;
using GetApi.Ecommerce.UnitTests.Extensions;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Core.Catalog.Services
{
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> _repositoryMock = new Mock<ICategoryRepository>();
        private Fixture _fixture = new Fixture();

        [Fact]
        public async Task Given_CategoryResquest_When_Create_Then_ShouldCreateCategory()
        {
            // arrange
            var request = _fixture.Build<CategoryRequest>().Without(x => x.ParentId).Create();
            var cancellationToken = new CancellationToken();

            _repositoryMock
                .Setup(x => x.Create(It.Is<Category>(y => y.Name == request.Name), cancellationToken))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // act
            await GetService().Create(request, cancellationToken);

            // assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Given_CategoryResquest_With_ParentId_When_Create_Then_ShouldCreateCategory_With_ParentCategoryId()
        {
            // arrange
            var parentCategory = _fixture.Create<Category>();
            var request = _fixture.Build<CategoryRequest>().With(x => x.ParentId, parentCategory.Id).Create();
            var cancellationToken = new CancellationToken();

            _repositoryMock
                .Setup(x => x.Find(It.Is<Expression<Func<Category, bool>>>(x => x.AreEquals(z => z.Id == request.ParentId)), cancellationToken))
                .ReturnsAsync(parentCategory)
                .Verifiable();

            _repositoryMock
                .Setup(x => x.Create(It.Is<Category>(y => y.Name == request.Name), cancellationToken))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // act
            await GetService().Create(request, cancellationToken);

            // assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Given_Service_When_List_Then_ShouldReturnCategories()
        {
            // arrange
            var categories = _fixture.CreateMany<Category>(10);
            var cancellationToken = new CancellationToken();

            _repositoryMock
                .Setup(x => x.List(cancellationToken))
                .ReturnsAsync(categories)
                .Verifiable();

            // act
            var response = await GetService().List(cancellationToken);

            // assert
            _repositoryMock.VerifyAll();
            response.All(x => categories.Any(y => y.Id == x.Id)).Should().BeTrue();
        }

        public CategoryService GetService() => new CategoryService(_repositoryMock.Object);
    }
}
