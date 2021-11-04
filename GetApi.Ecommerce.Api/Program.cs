using GetApi.Ecommerce.Infra.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace GetApi.Ecommerce.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                    .ConfigureHostConfiguration((config) =>
                    {
                        config.AddEnvironmentVariables();
                    })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                        
                        var port = Environment.GetEnvironmentVariable("PORT");
                        
                        if (string.IsNullOrEmpty(port) is false) webBuilder.UseUrls("http://*:" + port);
                    })
                    .ConfigureLogs();
        }       
    }
}
