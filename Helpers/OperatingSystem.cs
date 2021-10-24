namespace GetApi.Ecommerce.Api.Helpers
{
    public class OperatingSystem
    {
        public OperatingSystem(string name, string version)
        {
            Name = name;
            Version = version;
        }

        public string Name { get; }
        public string Version { get; }
    }
}
