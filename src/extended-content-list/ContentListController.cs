using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.RestSdk;
using System.Linq;
using System.Threading.Tasks;

namespace extended_content_list
{
    public class ContentListController : Controller
    {
        private IContentListModel contentListModel;
        private IRequestContext requestContext;
        private IRestClient restClient;

        public ContentListController(IContentListModel contentListModel, IRequestContext requestContext, IRestClient restClient)
        {
            this.contentListModel = contentListModel;
            this.requestContext = requestContext;
            this.restClient = restClient;
        }

        [HttpPost]
        public async Task<JsonResult> Index([FromBody] ContentListEntity contentListEntity)
        {
            await this.restClient.Init(new RequestArgs());
            this.requestContext.GetType().GetProperty("Model").SetValue(this.requestContext, new PageModel(new Progress.Sitefinity.Clients.LayoutService.Dto.PageModelDto()));

            var responseViewModel = await this.contentListModel.InitializeViewModel(contentListEntity, new System.Collections.ObjectModel.ReadOnlyCollection<string>(new string[0]), this.HttpContext.Request.Query) as ContentListViewModel;
            var items = responseViewModel.Items.Select(x =>
            {
                return new Container()
                {
                    Id = x.Id,
                    Title = x.GetValue<string>("Title")
                };
            });

            return Json(items);
        }
    }

    class Container
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }
}
