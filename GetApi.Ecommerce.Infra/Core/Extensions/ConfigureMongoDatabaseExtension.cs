using GetApi.Ecommerce.Core.Catalog.Entities;
using GetApi.Ecommerce.Core.Catalog.Repositories;
using GetApi.Ecommerce.Core.Shared.Entities;
using GetApi.Ecommerce.Infra.Core.Catalog.Repositoreis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;

namespace GetApi.Ecommerce.Infra.Catalog.Extensions
{
    public static class ConfigureMongoDatabaseExtension
    {
        public static IServiceCollection AddCatalogMongoRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var db = GetMongoDatabase(configuration);

            BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));

            ConfigurePackConvention();
            ConfigureCollections(services, db);
            ConfigureRepositories(services);
            ConfigureMappers();
            
            return services;
        }

        private static void ConfigurePackConvention()
        {
            var pack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register("", pack, t => true);
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<ICatalogRepository, CatalogRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }

        private static void ConfigureMappers()
        {
            BsonClassMap.RegisterClassMap<Entity>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.SetIdMember(map.GetMemberMap(c => c.Id).SetIdGenerator(CombGuidGenerator.Instance));
                map.MapIdMember(d => d.Id).SetSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
            });

            BsonClassMap.RegisterClassMap<Product>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });
        }

        private static IMongoDatabase GetMongoDatabase(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("MongoDb:ConnectionString");
            var mongoUrl = MongoUrl.Create(connectionString);
            var client = new MongoClient(mongoUrl);

            return client.GetDatabase(mongoUrl.DatabaseName);
        }

        private static void ConfigureCollections(IServiceCollection services, IMongoDatabase db)
        {
            services.AddScoped(x => db.GetCollection<Product>("products"));
        }
    }
}
