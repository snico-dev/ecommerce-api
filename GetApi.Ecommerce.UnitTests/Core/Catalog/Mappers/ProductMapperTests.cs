using AutoFixture;
using FluentAssertions;
using GetApi.Ecommerce.Core.Catalog.Dtos;
using GetApi.Ecommerce.Core.Catalog.Entities;
using GetApi.Ecommerce.Core.Catalog.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Core.Catalog.Mappers
{
    public class ProductMapperTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void Given_Product_When_MapToDto_Should_ReturnProductDto()
        {
            var parent1Category = _fixture.Build<CategoryDto>().Without(x => x.ParentId).Create();
            var parent2Category = _fixture.Build<CategoryDto>().With(x => x.ParentId, parent1Category.Id).Create();
            var parent3Category = _fixture.Build<CategoryDto>().With(x => x.ParentId, parent2Category.Id).Create();

            var categories = new List<CategoryDto>() {
                parent1Category,
                parent2Category,
                parent3Category
            };

            var product = _fixture.Create<Product>();
            product.AddCategoryRange(new List<Guid> { parent3Category.Id });

            var productDto = product.MapToDto(categories);

            productDto.Name.Should().Be(product.Name);
            productDto.Description.Should().Be(product.Description);

            productDto.Skus
                        .All(x => product.Skus.Any(y => y.Id == x.Id))
                        .Should()
                        .BeTrue();
            
            productDto.Categories
                        .All(x => categories.Any(y => y.Id == x.Id))
                        .Should()
                        .BeTrue();
        }
    }
}
