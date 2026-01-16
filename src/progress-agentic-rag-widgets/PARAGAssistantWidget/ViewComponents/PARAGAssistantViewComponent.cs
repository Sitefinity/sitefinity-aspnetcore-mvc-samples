using Microsoft.AspNetCore.Mvc;
using PARAGAssistantWidget.Models.PARAGAssistant;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace PARAGAssistantWidget.ViewComponents
{
    /// <summary>
    /// The view component for the widget.
    /// </summary>
    [SitefinityWidget(Title = "PARAG assistant", EmptyIconText = "Select an AI assistant", EmptyIcon = "pencil", Order = 1, Section = WidgetSection.Marketing, IconName = "chat")]
    [ViewComponent(Name = "PARAGAssistant")]
    public class PARAGAssistantViewComponent : ViewComponent
    {
        private readonly IPARAGAssistantModel sitefinityAssistantModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityAssistantViewComponent"/> class.
        /// </summary>
        /// <param name="sitefinityAssistantModel">The sitefinityAssistatModel parameter.</param>
        public PARAGAssistantViewComponent(IPARAGAssistantModel sitefinityAssistantModel)
        {
            this.sitefinityAssistantModel = sitefinityAssistantModel;
        }

        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context">The context parameter.</param>
        /// <returns>The view.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<PARAGAssistantEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var viewModel = await this.sitefinityAssistantModel.GetViewModel(context);

            return this.View(viewModel);
        }
    }
}
