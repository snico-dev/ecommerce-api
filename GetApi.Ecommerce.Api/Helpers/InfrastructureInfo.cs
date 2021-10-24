using System;

namespace GetApi.Ecommerce.Api.Helpers
{
    public class InfrastructureInfo
    {
        private InfrastructureInfo(string osName, string osVersion, string machineName)
        {
            OperatingSystem = new OperatingSystem(osName, osVersion);
            MachineName = machineName;
        }

        public OperatingSystem OperatingSystem { get; }

        public string MachineName { get; }

        public static InfrastructureInfo FromEnvironment() => new InfrastructureInfo(
                Environment.OSVersion.Platform.ToString(),
                Environment.OSVersion.VersionString,
                Environment.MachineName);
    }
}
