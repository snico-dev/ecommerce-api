using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace GetApi.Ecommerce.Infra.Extensions
{
    public static class LoggerConfigurationExtensions
    {
        public static IHostBuilder ConfigureLogs(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseSerilog((hostingContext, loggerConfiguration) => 
                ConfigureKeyInLogs(hostingContext, loggerConfiguration)
            );
        }

        private static LoggerConfiguration ConfigureKeyInLogs(HostBuilderContext hostingContext, LoggerConfiguration loggerConfiguration)
        {
            return loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.WithProperty("InstanceKey", Guid.NewGuid().ToString("N"));
        }
    }
}
