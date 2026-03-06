namespace PARAGWidgets.Models.PARAGResults
{
    /// <summary>
    /// Interface for the PARAGResults model.
    /// </summary>
    public interface IPARAGResultsModel
    {
        /// <summary>
        /// Initializes the view model for the PARAGResults widget.
        /// </summary>
        /// <param name="entity">The results entity.</param>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns>The initialized view model.</returns>
        Task<PARAGResultsViewModel> InitializeViewModel(PARAGResultsEntity entity, HttpContext httpContext);
    }
}
