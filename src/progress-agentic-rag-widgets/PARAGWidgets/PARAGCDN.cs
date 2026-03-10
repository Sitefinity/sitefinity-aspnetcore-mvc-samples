using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant.Dto;

namespace PARAGWidgets
{
    public class PARAGCDN : IPARAGCDN
    {
        private readonly string hostName;
        private readonly IPARAGClient assistantClient;
        private VersionInfoDto versionInfo;

        public PARAGCDN(IConfiguration configuration, IPARAGClient assistantClient)
        {
            var config = new PARAGAssistantConfig();
            configuration.Bind(PARAGAssistantConfig.SectionName, config);
            this.hostName = config.CdnHostName;

            this.assistantClient = assistantClient;
            this.versionInfo = this.assistantClient.GetVersionInfoAsync().Result;
        }

        public string GetUrl(string filePath)
        {
            string versionSuffix = string.IsNullOrEmpty(versionInfo?.ProductVersion) ? string.Empty : $"?ver={versionInfo.ProductVersion}";

            return $"https://{this.hostName}/{filePath}{versionSuffix}";
        }
    }
}
