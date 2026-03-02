using Microsoft.AspNetCore.Mvc;
using PARAGWidgets.Models.PARAGResults;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace PARAGWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the PARAGResults widget.
    /// </summary>
    [SitefinityWidget(Title = "PARAG results", Order = 2, Section = "AI search", IconName = "ai-search-sparkle")]
    [ViewComponent(Name = "PARAGResults")]
    public class PARAGResultsViewComponent : ViewComponent
    {
        private readonly IPARAGResultsModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="PARAGResultsViewComponent"/> class.
        /// </summary>
        /// <param name="resultsModel">The Results model.</param>
        public PARAGResultsViewComponent(IPARAGResultsModel resultsModel)
        {
            this.model = resultsModel;
        }

        /// <summary>
        /// Invokes the PARAGResults widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<PARAGResultsEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity, this.HttpContext);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
