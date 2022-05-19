using System;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using System.Threading.Tasks;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using System.Collections.Generic;

namespace breadcrumb.ViewComponents
{
    /// <summary>
    /// Language selector widget.
    /// </summary>
    [SitefinityWidget(Title="Breadcrumb")]
    public class BreadcrumbViewComponent : ViewComponent
    {
        private IRestClient restClient;
        private IRequestContext requestContext;
        public BreadcrumbViewComponent(IRestClient restClient, IRequestContext requestContext)
        {
            this.restClient = restClient;
            this.requestContext = requestContext;
        }

        /// <inheritdoc/>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext context)
        {
            var currentPageId = this.requestContext.PageNode.Id;
            var currentRootId = this.requestContext.PageNode.GetValue<string>("RootId");

            var parentId = this.requestContext.PageNode.ParentId;
            var path = new List<PageNodeDto>();
            path.Add(this.requestContext.PageNode);

            while (currentRootId != parentId)
            {
                var parent = await this.restClient.GetItem<PageNodeDto>(new GetItemArgs()
                {
                    Id = parentId
                });

                path.Add(parent);
                parentId = parent.ParentId;
            }

            path.Reverse();
            return this.View("Default", path);
        }
    }
}
