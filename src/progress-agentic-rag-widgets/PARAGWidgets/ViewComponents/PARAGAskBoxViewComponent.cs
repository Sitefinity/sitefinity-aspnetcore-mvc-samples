using Microsoft.AspNetCore.Mvc;
using PARAGWidgets.Models.PARAGAskBox;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace PARAGWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the AskBox widget.
    /// </summary>
    [SitefinityWidget(Title = "AI ask box", EmptyIconText = "Set where to seach", Order = 0, Section = "AI search", IconName = "search", EmptyIconAction = EmptyLinkAction.Edit, EmptyIcon = "search")]
    [ViewComponent(Name = "PARAGAskBox")]
    public class PARAGAskBoxViewComponent : ViewComponent
    {
        private readonly IPARAGAskBoxModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="AskBoxViewComponent"/> class.
        /// </summary>
        /// <param name="askBoxModel">The AskBox model.</param>
        public PARAGAskBoxViewComponent(IPARAGAskBoxModel askBoxModel)
        {
            this.model = askBoxModel;
        }

        /// <summary>
        /// Invokes the AskBox widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<PARAGAskBoxEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
