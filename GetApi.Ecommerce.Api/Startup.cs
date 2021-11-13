using FluentValidation.AspNetCore;
using GetApi.Ecommerce.Api.Helpers;
using GetApi.Ecommerce.Api.Helpers.Options;
using GetApi.Ecommerce.Core.Catalog.Requests;
using GetApi.Ecommerce.Infra.Catalog.Extensions;
using GetApi.Ecommerce.Infra.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
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
                .BuildHealthCheckers((app) => Configuration.GetSection(ApplicationInfo.Key).Bind(app))
                .AddMongoDbHealthChecker((config) => config.AddCriticalConnection("MongoDb", Configuration["MongoDb:ConnectionString"], timeout: TimeSpan.FromSeconds(10)));

            services
                .AddCatalogMongoRepositories(Configuration)
                .AddCatalogServices()
                .AddWrappers()
                .AddControllers()
                .AddJsonOptions(options => AddApiJsonConfiguration(options))
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<CategoryRequest>();
                    fv.RegisterValidatorsFromAssemblyContaining<PaginationRequest>();
                    fv.RegisterValidatorsFromAssemblyContaining<ProductRequest>();
                });
            
            AddSwagger(services, Configuration);
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

            app.UseReadinessChecks("/ready", false);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GetApi Ecommerce API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddSwagger(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Description = "This document describes the operations available from GetApi - Ecommerce.",
                    Title = "GetApi - Ecommerce API",
                    Version = Configuration.GetValue<string>("ApplicationInfo:Version"),
                    Contact = new OpenApiContact
                    {
                        Email = configuration.GetValue<string>("ApplicationInfo:Contact:Email"),
                        Name = configuration.GetValue<string>("ApplicationInfo:Contact:Name")
                    }
                });
            });
        }
    }
}
