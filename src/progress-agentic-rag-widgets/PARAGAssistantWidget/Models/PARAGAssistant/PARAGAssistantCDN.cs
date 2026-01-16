using Progress.Sitefinity.AspNetCore.Configuration;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    public class PARAGAssistantCDN : IPARAGAssistantCDN
    {
        private readonly string hostName;
        private readonly string rootRelativePath;

        public PARAGAssistantCDN(IConfiguration configuration)
        {
            var config = new PARAGAssistantConfig();
            configuration.Bind(PARAGAssistantConfig.SectionName, config);
            this.hostName = config.CdnHostName;
            this.rootRelativePath = config.CdnRootFolderRelativePath == null ?
                "staticfiles/" :
                (string.IsNullOrEmpty(config.CdnRootFolderRelativePath) ? string.Empty : $"{config.CdnRootFolderRelativePath.Trim('/')}/");
        }

        public string GetUrl(string filePath, string version)
        {
            string versionSuffix = string.IsNullOrEmpty(version) ? string.Empty : $"?ver={version}";

            return $"https://{this.hostName}/{this.rootRelativePath}{filePath}{versionSuffix}";
        }
    }
}
