using GetApi.Ecommerce.Core.Catalog.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GetApi.Ecommerce.Infra.Catalog.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection AddCatalogServices(this IServiceCollection collection)
        {
            collection.AddScoped<ICatalogService, CatalogService>();
            collection.AddScoped<ICategoryService, CategoryService>();
            collection.AddScoped<IListCategoriesService, CategoryService>();

            return collection;
        }
    }
}
