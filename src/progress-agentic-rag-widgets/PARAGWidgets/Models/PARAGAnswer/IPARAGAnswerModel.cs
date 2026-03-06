namespace PARAGWidgets.Models.PARAGAnswer
{
    /// <summary>
    /// Interface for the PARAGAnswer model.
    /// </summary>
    public interface IPARAGAnswerModel
    {
        /// <summary>
        /// Initializes the view model for the PARAGAnswer widget.
        /// </summary>
        /// <param name="entity">The answer entity.</param>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns>The initialized view model.</returns>
        Task<PARAGAnswerViewModel> InitializeViewModel(PARAGAnswerEntity entity, HttpContext httpContext);
    }
}
