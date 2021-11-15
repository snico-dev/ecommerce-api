using AutoFixture;
using FluentAssertions;
using GetApi.Ecommerce.Core.Catalog.Entities;
using System;
using System.Linq;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Core.Catalog.Entitites
{
    public class ProductTests
    {
        private Fixture _fixture = new Fixture();
        
        [Fact]
        public void Given_A_Product_When_AddCategoryRange_Than_TheProduct_Should_Belong_A_Category()
        {
            // arrange
            var product = Product.Create("test product", "teste description", Guid.NewGuid().ToString());
            var categoryIds = _fixture.CreateMany<Guid>(10).ToArray();

            // act
            product.AddCategoryRange(categoryIds);

            // assert
            product.CategoryIds.Should().HaveSameCount(categoryIds);
            product.CategoryIds
                .Except(categoryIds)
                .Should()
                .BeEmpty();
        }

        [Fact]
        public void Given_A_Product_When_AddSkuRange_Than_TheProduct_Should_HaveSkus()
        {
            // arrange
            var product = Product.Create("test product", "teste description", Guid.NewGuid().ToString());
            var skus = _fixture.CreateMany<Sku>(10).ToArray();

            // act
            product.AddSkuRange(skus);

            // assert
            product.Skus.Should().HaveSameCount(skus);
            product.Skus
                .Except(skus)
                .Should()
                .BeEmpty();
        }

        [Fact]
        public void Given_A_ProductData_When_Create_Then_TheProduct_Should_Be_Actived()
        {
            // arrange
            // act 
            var product = Product.Create("test product", "teste description", Guid.NewGuid().ToString());

            // arrange
            product.IsActive.Should().BeTrue();
        }


        [Fact]
        public void Given_A_Product_When_Inactive_Then_TheProduct_Should_Be_Inactived()
        {
            // arrange
            var product = Product.Create("test product", "teste description", Guid.NewGuid().ToString());

            // act
            product.Inactive();

            // assert
            product.IsActive.Should().BeFalse();
        }

        [Fact]
        public void Given_A_InactiveProduct_When_Active_Then_TheProduct_Should_Be_Actived()
        {
            // arrange
            var product = Product.Create("test product", "teste description", Guid.NewGuid().ToString());
            product.Inactive();

            // act
            product.Active();

            // assert
            product.IsActive.Should().BeTrue();
        }
    }
}
