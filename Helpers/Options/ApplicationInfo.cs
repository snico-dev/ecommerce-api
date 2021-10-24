using GetApi.Ecommerce.Api.Helper;

namespace GetApi.Ecommerce.Api.Helpers.Options
{
    public class ApplicationInfo
    {
        public const string Key = nameof(ApplicationInfo);
        public string Name { get; set; }
        public string Version { get; set; }
        public ApplicationType Type { get; set; }
    }
}
