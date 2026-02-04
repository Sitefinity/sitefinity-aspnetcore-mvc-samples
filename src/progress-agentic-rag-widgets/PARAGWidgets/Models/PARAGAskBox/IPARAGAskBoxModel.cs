namespace PARAGWidgets.Models.PARAGAskBox
{
    /// <summary>
    /// Defines model for the AskBox widget.
    /// </summary>
    public interface IPARAGAskBoxModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The AskBox entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<PARAGAskBoxViewModel> InitializeViewModel(PARAGAskBoxEntity entity);
    }
}
