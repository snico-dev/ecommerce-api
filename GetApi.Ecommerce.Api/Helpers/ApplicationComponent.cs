using GetApi.Ecommerce.Api.Helper;
using System;
using System.Collections.Generic;

namespace GetApi.Ecommerce.Api.Helpers
{
    public class ApplicationComponent
    {
        protected ApplicationComponent(
            string applicationName, 
            string applicationVersion, 
            ApplicationType type, 
            ApplicationStatus status,
            IReadOnlyDictionary<string, object> data,
            TimeSpan duration,
            Exception exception)
        {
            Name = applicationName;
            Type = type;
            Status = status;
            Version = applicationVersion;
            Data = data;
            Duration = duration;
            Exception = exception;
        }

        public string Name { get; }
        public string Version { get; set; }
        public ApplicationType Type { get; }
        public ApplicationStatus Status { get; }
        public IReadOnlyDictionary<string, object> Data { get; }
        public TimeSpan Duration { get; }
        public Exception? Exception { get; }

        public static ApplicationComponent Create(
            string applicationName, 
            string applicationVersion, 
            ApplicationType type, 
            ApplicationStatus status)
        {
            return new ApplicationComponent(
                applicationName, 
                applicationVersion, 
                type, 
                status, 
                null, 
                TimeSpan.Zero, 
                null);
        }

        public static ApplicationComponent Create(
            string applicationName,
            string applicationVersion,
            ApplicationType type,
            ApplicationStatus status,
            IReadOnlyDictionary<string, object>  data,
            TimeSpan duration,
            Exception exception)
        {
            return new ApplicationComponent(
                applicationName, 
                applicationVersion, 
                type, 
                status, 
                data, 
                duration, 
                exception);
        }
    }
}
