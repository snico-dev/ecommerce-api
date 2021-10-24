using GetApi.Ecommerce.Api.Helpers.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GetApi.Ecommerce.Api.Helpers
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthChecksWithConfig(this IServiceCollection services, Action<ApplicationInfo> appInfoConfiguration)
        {
            services.AddHealthChecks();
            services.Configure(appInfoConfiguration);
            return services;
        }

        public static IApplicationBuilder UseLivenessChecks(this IApplicationBuilder app, PathString endpoint)
        {
            return app.UseHealthChecks(endpoint, new HealthCheckOptions
            {
                ResponseWriter = ResponseFormatter.WriteResponseAsync,
                Predicate = item => false
            });
        }
    }
}
