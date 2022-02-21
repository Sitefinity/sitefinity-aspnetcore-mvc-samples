using extended_content_list.ViewModels;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.RestSdk.OData;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace extended_content_list.Models
{
    public class ExtendedContentListModel : ContentListModel
    {
        public ExtendedContentListModel(IODataRestClient restService, IWidgetConfig widgetConfig, IRequestContext requestContext)
            : base(restService, widgetConfig, requestContext)
        {
        }

        public override async Task<object> InitializeViewModel(ContentListEntity entity, ReadOnlyCollection<string> urlParameters, IQueryCollection query)
        {
            var viewModel = await base.InitializeViewModel(entity, urlParameters, query);
            var asListViewModel = viewModel as ContentListViewModel;
            if (asListViewModel != null)
            {
                var extendedModel = new ExtendedContentListViewModel()
                {
                    CssClasses = asListViewModel.CssClasses,
                    DetailItemUrl = asListViewModel.DetailItemUrl,
                    Entity = entity,
                    Items = asListViewModel.Items,
                    ListFieldMapping = asListViewModel.ListFieldMapping,
                    Pager = asListViewModel.Pager,
                    RenderLinks = asListViewModel.RenderLinks,
                };

                return extendedModel;
            }

            return viewModel;
        }
    }
}
