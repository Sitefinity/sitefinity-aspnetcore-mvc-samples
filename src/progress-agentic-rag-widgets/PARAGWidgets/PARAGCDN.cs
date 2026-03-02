using Progress.Sitefinity.AspNetCore.Configuration;

namespace PARAGWidgets
{
    public class PARAGCDN : IPARAGCDN
    {
        private readonly string hostName;

        public PARAGCDN(IConfiguration configuration)
        {
            var config = new PARAGAssistantConfig();
            configuration.Bind(PARAGAssistantConfig.SectionName, config);
            this.hostName = config.CdnHostName;
        }

        public string GetUrl(string filePath, string version)
        {
            string versionSuffix = string.IsNullOrEmpty(version) ? string.Empty : $"?ver={version}";

            return $"https://{this.hostName}/{filePath}{versionSuffix}";
        }
    }
}
