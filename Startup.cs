using GetApi.Ecommerce.Api.Helpers;
using GetApi.Ecommerce.Api.Helpers.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace GetApi.Ecommerce.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHealthChecksWithConfig((app) => Configuration.GetSection(ApplicationInfo.Key).Bind(app))
                .AddControllers()
                .AddJsonOptions(options => AddApiJsonConfiguration(options));
        }

        private static void AddApiJsonConfiguration(JsonOptions options)
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UseApiConfiguration(app, env);
        }

        private void UseApiConfiguration(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseLivenessChecks("/appinfo");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
