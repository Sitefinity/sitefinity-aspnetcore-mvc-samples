namespace PARAGWidgets.Models.PARAGResults
{
    /// <summary>
    /// The view model for a single result item.
    /// </summary>
    public class PARAGResultItemViewModel
    {
        /// <summary>
        /// Gets or sets the title of the result.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the link of the result.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the order of the result.
        /// </summary>
        public int Order { get; set; }
    }
}
