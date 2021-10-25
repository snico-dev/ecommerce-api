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
        public static IHealthChecksBuilder BuildHealthCheckers(this IServiceCollection services, Action<ApplicationInfo> appInfoConfiguration)
        {
            var healthChecksBuilder = services.AddHealthChecks();

            services.Configure(appInfoConfiguration);
            
            return healthChecksBuilder;
        }

        public static IApplicationBuilder UseLivenessChecks(this IApplicationBuilder app, PathString endpoint)
        {
            return app.UseHealthChecks(endpoint, new HealthCheckOptions
            {
                ResponseWriter = ResponseFormatter.WriteResponseAsync,
                Predicate = item => false
            });
        }

        public static IApplicationBuilder UseReadinessChecks(this IApplicationBuilder app, PathString endpoint, bool forceHealthy)
        {
            return app.UseHealthChecks(endpoint, new HealthCheckOptions
            {
                ResponseWriter = ResponseFormatter.WriteResponseAsync,
                Predicate = item => !forceHealthy
            });
        }
    }
}
