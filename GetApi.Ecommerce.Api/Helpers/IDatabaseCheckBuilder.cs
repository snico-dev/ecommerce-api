using System;

namespace GetApi.Ecommerce.Api.Helpers
{
    public interface IDatabaseCheckBuilder
    {
        IDatabaseCheckBuilder AddCriticalConnection(
            string name,
            string credentials,
            TimeSpan? timeout = null);

        IDatabaseCheckBuilder AddNonCriticalConnection(
            string name,
            string credentials,
            TimeSpan? timeout = null);
    }
}
