namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    /// <summary>
    /// Provides urls to static files.
    /// </summary>
    public interface IPARAGAssistantCDN
    {
        /// <summary>
        /// Gets the url of a static file.
        /// </summary>
        /// <param name="filePath">The relative file path.</param>
        /// <param name="version">The version which will be appended as a query string to the url.</param>
        /// <returns>The url.</returns>
        string GetUrl(string filePath, string version);
    }
}
