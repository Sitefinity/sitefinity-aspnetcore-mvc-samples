namespace Progress.Sitefinity.AspNetCore.Configuration
{
    /// <summary>
    /// Defines the configuration of the Sitefinity Assistant feature.
    /// </summary>
    public class PARAGAssistantConfig
    {
        /// <summary>
        /// Gets the name of the configuration section.
        /// </summary>
        public const string SectionName = "SitefinityAssistant";

        /// <summary>
        /// Gets or sets the static files CDN host name.
        /// </summary>
        public string CdnHostName { get; set; }

        /// <summary>
        /// Gets or sets the relative path to the CDN root folder.
        /// </summary>
        public string CdnRootFolderRelativePath { get; set; }
    }
}
