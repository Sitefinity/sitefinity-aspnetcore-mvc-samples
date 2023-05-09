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
using Progress.Sitefinity.Renderer.Designers.Attributes;
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

            if (context.Entity.Page.HasSelectedItems())
            {
                 var page = await this.restClient.GetItems<SdkItem>(context.Entity.Page);
                viewModels.Add(new ItemCollection()
                {
                    GroupTitle = "Page",
                    Items = page.Items.ToArray(),
                });
            }

            if (context.Entity.Images.HasSelectedItems())
            {
                var images = await this.restClient.GetItems<SdkItem>(context.Entity.Images);
                viewModels.Add(new ItemCollection()
                {
                    GroupTitle = "Image",
                    Items = images.Items.ToArray()
                });
            }

            if (context.Entity.Albums.HasSelectedItems())
            {
                var albums = await this.restClient.GetItems<SdkItem>(context.Entity.Albums);

                // get folders
                context.Entity.Albums.Content[0].Type = KnownContentTypes.Folders;
                var folders = await this.restClient.GetItems<SdkItem>(context.Entity.Albums);
                viewModels.Add(new ItemCollection()
                {
                    GroupTitle = "Albums",
                    Items = albums.Items.Concat(folders.Items).ToArray()
                });
            }

            if (context.Entity.DocumentLibrary.HasSelectedItems())
            {
                var documentLibrary = await this.restClient.GetItems<SdkItem>(context.Entity.DocumentLibrary);

                // get folders
                context.Entity.DocumentLibrary.Content[0].Type = KnownContentTypes.Folders;
                var folders = await this.restClient.GetItems<SdkItem>(context.Entity.DocumentLibrary);
                viewModels.Add(new ItemCollection()
                {
                    GroupTitle = "Document library",
                    Items = documentLibrary.Items.Concat(folders.Items).ToArray()
                });
            }

            if (context.Entity.Tags.HasSelectedItems())
            {
                var tags = await this.restClient.GetItems<SdkItem>(context.Entity.Tags);
                viewModels.Add(new ItemCollection()
                {
                    GroupTitle = "Tags",
                    Items = tags.Items.ToArray()
                });
            }
            
            // combine same types of items into a single request - e.g. news + news
            // do not use with different types of items - e.g. news + images
            var bulkResponse = await this.restClient.GetItemsBulk<SdkItem>(new[] { context.Entity.News, context.Entity.NewsLive });
            if (context.Entity.News.HasSelectedItems())
            {
                var newsItems = bulkResponse[context.Entity.News];
                viewModels.Add(new ItemCollection()
                {
                    GroupTitle = "News items draft",
                    Items = newsItems.ToArray(),
                });
            }

            var newsItemsLive = bulkResponse[context.Entity.NewsLive];
            if (context.Entity.NewsLive.HasSelectedItems())
            {
                viewModels.Add(new ItemCollection()
                {
                    GroupTitle = "News items live",
                    Items = newsItemsLive.ToArray(),
                });
            }

            try
            {
                if (context.Entity.GeographicalRegions.HasSelectedItems())
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
                if (context.Entity.PressReleases.HasSelectedItems())
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
    }
}


