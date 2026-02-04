using Progress.Sitefinity.AspNetCore.Configuration;

namespace PARAGWidgets
{
    public class PARAGCDN : IPARAGCDN
    {
        private readonly string hostName;
        private readonly string rootRelativePath;

        public PARAGCDN(IConfiguration configuration)
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
