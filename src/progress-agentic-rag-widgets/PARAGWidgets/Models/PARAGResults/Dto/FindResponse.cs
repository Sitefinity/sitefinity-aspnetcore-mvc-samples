namespace PARAGWidgets.Models.PARAGResults.Dto
{
    /// <summary>
    /// Represents a response from the Agentic RAG find API.
    /// </summary>
    internal class FindResponse
    {
        /// <summary>
        /// Gets or sets the resources found.
        /// </summary>
        public IDictionary<string, ResourceDto> Resources { get; set; }
    }

    /// <summary>
    /// Represents a resource returned from the Agentic RAG API.
    /// </summary>
    internal class ResourceDto
    {
        /// <summary>
        /// Gets or sets the title of the resource.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail URL of the resource.
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// Gets or sets the summary of the resource.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the origin of the resource.
        /// </summary>
        public OriginDto Origin { get; set; }

        /// <summary>
        /// Gets or sets the fields of the resource.
        /// </summary>
        public IDictionary<string, FieldDto> Fields { get; set; }
    }

    /// <summary>
    /// Represents the origin of a resource returned from the Agentic RAG API.
    /// </summary>
    internal class OriginDto
    {
        /// <summary>
        /// Gets or sets the URL of the origin.
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// Represents a field of a resource returned from the Agentic RAG API.
    /// </summary>
    internal class FieldDto
    {
        /// <summary>
        /// Gets or sets the paragraphs of the field.
        /// </summary>
        public IDictionary<string, ParagraphDto> Paragraphs { get; set; }
    }

    /// <summary>
    /// Represents a paragraph of a field returned from the Agentic RAG API.
    /// </summary>
    internal class ParagraphDto
    {
        /// <summary>
        /// Gets or sets the text of the paragraph.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the order of the paragraph.
        /// </summary>
        public int Order { get; set; }
    }
}
