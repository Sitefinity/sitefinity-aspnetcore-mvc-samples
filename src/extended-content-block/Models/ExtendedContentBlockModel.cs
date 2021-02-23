using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.SitefinityApi;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentBlock;

namespace extended_content_block.Models
{
    /// <summary>
    /// Extended model for the ContentBlock view component.
    /// </summary>
    public class ExtendedContentBlockModel : ContentBlockModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedContentBlockModel"/> class.
        /// </summary>
        /// <param name="restClient">The HTTP client.</param>
        /// <param name="widgetConfig">The widget config.</param>
        public ExtendedContentBlockModel(IRestClient restClient, IWidgetConfig widgetConfig)
            : base(restClient, widgetConfig)
        {
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The view model.</returns>
        public override async Task<ContentBlockViewModel> InitialzieViewModel(ContentBlockEntity entity)
        {
            var viewModel = await base.InitialzieViewModel(entity).ConfigureAwait(false);
            viewModel.Content += " From extended model";

            return viewModel;
        }
    }
}
