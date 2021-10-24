using GetApi.Ecommerce.Api.Helper;

namespace GetApi.Ecommerce.Api.Helpers
{
    public class ApplicationComponent
    {
        protected ApplicationComponent(string applicationName, string applicationVersion, ApplicationType type, ApplicationStatus status)
        {
            Name = applicationName;
            Type = type;
            Status = status;
            Version = applicationVersion;
        }

        public string Name { get; }
        public string Version { get; set; }
        public ApplicationType Type { get; }

        public ApplicationStatus Status { get; }

        public static ApplicationComponent Create(string applicationName, string applicationVersion, ApplicationType type, ApplicationStatus status)
        {
            return new ApplicationComponent(applicationName, applicationVersion, type, status);
        }
    }
}
