using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.OData;
using Renderer.Entities.Extends;
using Renderer.ViewModels.Extends;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;

namespace Renderer.Models.Extends
{
    public class ExtendedContentListModel : ContentListModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedContentListModel"/> class.
        /// </summary>
        /// <param name="restClient">The HTTP client.</param>
        /// <param name="requestContext">The context.</param>
        public ExtendedContentListModel(IODataRestClient restClient, IRequestContext requestContext, IStyleClassesProvider styles)
            : base(restClient, requestContext, styles)
        {
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The view model.</returns>
        public override async Task<object> HandleListView(ContentListEntityBase entity, ReadOnlyCollection<string> urlParameters, HttpContext httpContext)
        {
            var extendedEntity = entity as ExtendedContentListEntity;
            var viewModel = await base.HandleListView(entity, urlParameters, httpContext);

            if (viewModel is ContentListViewModel listViewModel)
            {
                var extendedViewModel = new ExtendedContentListViewModel(listViewModel);

                extendedViewModel.Heading = extendedEntity.Heading;
                extendedViewModel.SmallList = extendedEntity.SmallList;

                return extendedViewModel;
            }

            return viewModel;
        }
    }
}
