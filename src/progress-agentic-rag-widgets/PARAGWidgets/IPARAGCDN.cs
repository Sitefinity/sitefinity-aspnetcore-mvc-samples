namespace PARAGWidgets
{
    /// <summary>
    /// Provides urls to static files.
    /// </summary>
    public interface IPARAGCDN
    {
        /// <summary>
        /// Gets the url of a static file.
        /// </summary>
        /// <param name="filePath">The relative file path.</param>
        /// <returns>The url.</returns>
        string GetUrl(string filePath);
    }
}
