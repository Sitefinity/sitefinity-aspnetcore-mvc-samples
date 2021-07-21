using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.RestSdk.OData;
using Renderer.Entities.Extends;
using Renderer.ViewModels.Extends;
using System.Threading.Tasks;

namespace Renderer.Models.Extends
{
    public class ExtendedContentListModel : ContentListModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedContentListModel"/> class.
        /// </summary>
        /// <param name="restClient">The HTTP client.</param>
        /// <param name="widgetConfig">The widget config.</param>
        /// <param name="requestContext">The context.</param>
        public ExtendedContentListModel(IODataRestClient restClient, IWidgetConfig widgetConfig, IRequestContext requestContext)
            : base(restClient, widgetConfig, requestContext)
        {
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The view model.</returns>
        public override async Task<object> InitializeViewModel(ContentListEntity entity)
        {
            var extendedEntity = entity as ExtendedContentListEntity;
            ContentListViewModel viewModel = await base.InitializeViewModel(entity).ConfigureAwait(false) as ContentListViewModel;
            var extendedViewModel = new ExtendedContentListViewModel(viewModel);

            extendedViewModel.Heading = extendedEntity.Heading;
            extendedViewModel.IsNarrowed = extendedEntity.IsNarrowed;

            return extendedViewModel;
        }
    }
}
