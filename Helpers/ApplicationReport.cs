using System;
using System.Collections.Generic;

namespace GetApi.Ecommerce.Api.Helpers
{
    public sealed class ApplicationReport
    {
        private ApplicationReport() { }

        public ApplicationComponent Application { get; private set; }
        public OperatingSystem OS { get; private set; }
        public string MachineName { get; private set; }
        public string Timestamp { get; private set; }
        public ICollection<ApplicationComponent> Components { get; set; }

        public void AddComponents(params ApplicationComponent[] args)
        {
            foreach (var component in args) Components.Add(component);
        }

        public static ApplicationReport Create(ApplicationComponent application)
        {
            var infra = InfrastructureInfo.FromEnvironment();

            return new ApplicationReport
            {
                MachineName = infra.MachineName,
                OS = infra.OperatingSystem,
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                Components = new List<ApplicationComponent>(),
                Application = application
            };
        }
    }
}
