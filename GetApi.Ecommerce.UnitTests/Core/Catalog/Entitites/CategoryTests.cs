using FluentAssertions;
using GetApi.Ecommerce.Core.Catalog.Entities;
using Xunit;

namespace GetApi.Ecommerce.UnitTests.Core.Catalog.Entitites
{
    public class CategoryTests
    {
        [Fact]
        public void Given_A_ParentCategory_When_CreateChildCategory_Then_Should_LinkChildCategoryWithParentCategory()
        {
            var parent = Category.Create("parent", "friendlyUrl");
            var category = Category.Create("child", "friendlyUrl", parent.Id);
            
            category.ParentId.Should().Be(parent.Id);
        }
    }
}
