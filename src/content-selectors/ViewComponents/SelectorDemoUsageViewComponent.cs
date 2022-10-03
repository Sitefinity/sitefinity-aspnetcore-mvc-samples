using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using content_selectors.Entities.SelectorDemoUsage;
using content_selectors.ViewModels.SelectorDemoUsage;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Exceptions;

namespace content_selectors.ViewComponents
{
    /// <summary>
    /// The view component for working with content selectors.
    /// </summary>
    [SitefinityWidget]
    public class SelectorDemoUsageViewComponent : ViewComponent
    {
        private IRestClient restClient;
        private IRenderContext renderContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorDemoUsageViewComponent"/> class.
        /// </summary>
        public SelectorDemoUsageViewComponent(IRestClient restClient, IRenderContext renderContext)
        {
            this.restClient = restClient;
            this.renderContext = renderContext;
        }

        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="context">The view component context.</param>
        /// <returns>The view component result.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<SelectorDemoEntity> context)
        {
            var viewModels = new List<ItemCollection>();

            // GetItems is an extension method from the class RestClientExtensions in the namespace Progress.Sitefinity.RestSdk
            // In order to use it the generic type must inherit from ISdkItem
            if (this.HasSelectedItems(context.Entity.News))
            {
                var newsItems = await this.restClient.GetItems<SdkItem>(context.Entity.News);
                viewModels.Add(new ItemCollection()
                {
                    GroupTitle = "News items draft",
                    Items = newsItems.Items.ToArray(),
                });
            }

            if (this.HasSelectedItems(context.Entity.NewsLive))
            {
                var newsItemsLive = await this.restClient.GetItems<SdkItem>(context.Entity.NewsLive);
                viewModels.Add(new ItemCollection()
                {
                    GroupTitle = "News items live",
                    Items = newsItemsLive.Items.ToArray(),
                });
            }

            if (this.HasSelectedItems(context.Entity.Page))
            {
                 var page = await this.restClient.GetItems<SdkItem>(context.Entity.Page);
                viewModels.Add(new ItemCollection()
                {
                    GroupTitle = "Page",
                    Items = page.Items.ToArray(),
                });
            }

            if (this.HasSelectedItems(context.Entity.Images))
            {
                var images = await this.restClient.GetItems<SdkItem>(context.Entity.Images);
                viewModels.Add(new ItemCollection()
                {
                    GroupTitle = "Image",
                    Items = images.Items.ToArray()
                });
            }

            if (this.HasSelectedItems(context.Entity.Tags))
            {
                var tags = await this.restClient.GetItems<SdkItem>(context.Entity.Tags);
                viewModels.Add(new ItemCollection()
                {
                    GroupTitle = "Tags",
                    Items = tags.Items.ToArray()
                });
            }

            try
            {
                if (this.HasSelectedItems(context.Entity.GeographicalRegions))
                {
                    var geoRegions = await this.restClient.GetItems<SdkItem>(context.Entity.GeographicalRegions).ConfigureAwait(true);
                    viewModels.Add(new ItemCollection()
                    {
                        GroupTitle = "Geographical Regions",
                        Items = geoRegions.Items.ToArray(),
                    });
                }
            }
            catch (ErrorCodeException err)
            {
                if (this.renderContext.IsEdit)
                {
                    viewModels.Add(new ItemCollection()
                    {
                        GroupTitle = $"Geographical Regions - {err.Message}",
                        Items = Array.Empty<SdkItem>(),
                    });
                }
            }

            try
            {
                if (this.HasSelectedItems(context.Entity.PressReleases))
                {
                    var pressReleases = await this.restClient.GetItems<SdkItem>(context.Entity.PressReleases);
                    viewModels.Add(new ItemCollection()
                    {
                        GroupTitle = "Press Releases",
                        Items = pressReleases.Items.ToArray(),
                    });
                }
            }
            catch (Exception err)
            {
                if (this.renderContext.IsEdit)
                {
                    viewModels.Add(new ItemCollection()
                    {
                        GroupTitle = $"Press Releases - {err.Message}",
                        Items = Array.Empty<SdkItem>(),
                    });
                }
            }

            return this.View(viewModels);
        }

        private bool HasSelectedItems(MixedContentContext mixedContentContext)
        {
            return mixedContentContext.ItemIdsOrdered != null && mixedContentContext.ItemIdsOrdered.Length > 0;
        }
    }
}


