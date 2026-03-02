namespace PARAGWidgets.Models.PARAGResults.Dto
{
    /// <summary>
    /// Represents a request to find resources in the Agentic RAG API.
    /// </summary>
    internal class FindRequest
    {
        /// <summary>
        /// Gets or sets the name of the knowledge box.
        /// </summary>
        public string KnowledgeBoxName { get; set; }

        /// <summary>
        /// Gets or sets the search query.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the configuration name.
        /// </summary>
        public string ConfigurationName { get; set; }

        /// <summary>
        /// Gets or sets the number of results to return.
        /// </summary>
        public int Take { get; set; }
    }
}
