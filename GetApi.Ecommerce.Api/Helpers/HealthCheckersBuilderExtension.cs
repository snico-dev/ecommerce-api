using Microsoft.Extensions.DependencyInjection;
using System;

namespace GetApi.Ecommerce.Api.Helpers
{
    public static class HealthCheckersBuilderExtension
    {
        public static IHealthChecksBuilder AddMongoDbHealthChecker(this IHealthChecksBuilder healthChecksBuilder, 
            Action<IDatabaseCheckBuilder> databaseCheckBuilder)
        {
            databaseCheckBuilder(new MongoDbCheckBuilder(healthChecksBuilder));
            return healthChecksBuilder;
        }
    }
}
