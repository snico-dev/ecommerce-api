using GetApi.Ecommerce.Api.Helper;
using GetApi.Ecommerce.Api.Helpers.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetApi.Ecommerce.Api.Helpers
{
    internal class ResponseFormatter
    {
        private static readonly JsonSerializerSettings SerializerSettings = GetSerializerSettings();

        private static JsonSerializerSettings GetSerializerSettings()
        {
            var settings = new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore,
            };

            settings.Converters.Add(new StringEnumConverter());

            return settings;
        }

        public static Task WriteResponseAsync(HttpContext context, HealthReport result)
        {
            ConfigureResponseDefaults(context.Response);

            var applicationInfo = GetApplicationInfo(context.RequestServices);
            
            var applicationReport = ApplicationReport.Create(ApplicationComponent.Create(
                applicationInfo.Name,
                applicationInfo.Version,
                applicationInfo.Type,
                MapToApplicationStatus(result.Status)
            ));

            var componentsReport = CreateComponentsReport(result.Entries);
            
            applicationReport
                .AddComponents(componentsReport);

            return context.Response.WriteAsync(SerializeResponse(applicationReport), Encoding.UTF8);
        }

        private static string SerializeResponse(ApplicationReport applicationReport)
        {
            return JsonConvert.SerializeObject(applicationReport, Formatting.Indented, SerializerSettings);
        }

        public static ApplicationComponent[] CreateComponentsReport(IReadOnlyDictionary<string, HealthReportEntry> healthReportEntries)
        {
            return healthReportEntries.Select(kvp => ApplicationComponent.Create(
                       kvp.Key,
                       string.Empty,
                       MapToComponentType(kvp.Value.Tags),
                       MapToApplicationStatus(kvp.Value.Status),
                       kvp.Value.Data,
                       kvp.Value.Duration,
                       kvp.Value.Exception)).ToArray();
        }

        private static ApplicationType MapToComponentType(IEnumerable<string> entryTags)
        {
            foreach (var tag in entryTags)
            {
                if (Enum.TryParse<ApplicationType>(tag, out var type))
                {
                    return type;
                }
            }

            return ApplicationType.Other;
        }

        private static ApplicationStatus MapToApplicationStatus(HealthStatus status)
        {
            return status switch
            {
                HealthStatus.Healthy => ApplicationStatus.Ok,
                HealthStatus.Degraded => ApplicationStatus.PartiallyAvailable,
                HealthStatus.Unhealthy => ApplicationStatus.Critical,
                _ => throw new ArgumentOutOfRangeException(nameof(status))
            };
        }

        public static ApplicationInfo GetApplicationInfo(IServiceProvider services)
            => services.GetRequiredService<IOptions<ApplicationInfo>>().Value;

        private static void ConfigureResponseDefaults(HttpResponse response)
        {
            response.ContentType = "application/json";
        }
    }
}
