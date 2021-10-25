using GetApi.Ecommerce.Api.Helper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;

namespace GetApi.Ecommerce.Api.Helpers
{
    internal class MongoDbCheckBuilder : IDatabaseCheckBuilder
    {
        private IHealthChecksBuilder _healthChecksBuilder;

        public MongoDbCheckBuilder(IHealthChecksBuilder healthChecksBuilder)
        {
            _healthChecksBuilder = healthChecksBuilder;
        }

        protected IHealthChecksBuilder HealthChecksBuilder { get; }

        public IDatabaseCheckBuilder AddCriticalConnection(string name, string credentials,
           TimeSpan? timeout = null)
        {
            return AddConnection(name, credentials, HealthStatus.Unhealthy, timeout);
        }

        public IDatabaseCheckBuilder AddNonCriticalConnection(string name, string credentials,
            TimeSpan? timeout = null)
        {
            return AddConnection(name, credentials, HealthStatus.Degraded, timeout);
        }

        public IDatabaseCheckBuilder AddConnection(string name,
            string credentials,
            HealthStatus failureStatus,
            TimeSpan? timeout)
        {
            Console.WriteLine($"Credenciais: {credentials}");

            _healthChecksBuilder.AddMongoDb(credentials,
                name,
                failureStatus,
                new[] { ApplicationType.Database.ToString() });

            return this;
        }
    }
}
