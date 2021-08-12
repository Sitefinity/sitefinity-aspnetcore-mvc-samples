using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client.Exceptions;
using Progress.Sitefinity.AspNetCore.SitefinityApi;
using content_selectors.ViewModels;
using content_selectors.ViewModels.SelectorDemoUsage;
using content_selectors.Entities.SelectorDemoUsage;
using Progress.Sitefinity.AspNetCore.Web;
using System.Linq;

namespace content_selectors.Models.SelectorDemoUsage
{
    /// <summary>
    /// The model class.
    /// </summary>
    public class SelectorDemoUsageModel : ISelectorDemoUsageModel
    {
        private readonly IRestClient restService;
        private readonly IRenderContext renderContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityDataViewComponent"/> class.
        /// </summary>
        /// <param name="restService">The rest service.</param>
        /// <param name="renderContext">The render context.</param>
        public SelectorDemoUsageModel(IRestClient restService, IRenderContext renderContext)
        {
            this.restService = restService;
            this.renderContext = renderContext;
        }


        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The generated view models.</returns>
        public async Task<IList<ItemCollection>> GetViewModels(SelectorDemoEntity entity)
        {
            var viewModels = new List<ItemCollection>();

            // GetItems is an extension method from the class RestClientExtensions in the namespace Progress.Sitefinity.RestSdk
            // In order to use it the generic type must inherit from ISdkItem
            var newsItems = await this.restService.GetItems<ItemDto>(entity.News).ConfigureAwait(true);
            viewModels.Add(new ItemCollection()
            {
                GroupTitle = "News items draft",
                Items = newsItems.Items.ToArray(),
            });

            var newsItemsLive = await this.restService.GetItems<ItemDto>(entity.NewsLive).ConfigureAwait(true);
            viewModels.Add(new ItemCollection()
            {
                GroupTitle = "News items live",
                Items = newsItemsLive.Items.ToArray(),
            });

            var page = await this.restService.GetItems<ItemDto>(entity.Page).ConfigureAwait(true);
            viewModels.Add(new ItemCollection()
            {
                GroupTitle = "Page",
                Items = page.Items.ToArray(),
            });

            var images = await this.restService.GetItems<ImageDto>(entity.Images).ConfigureAwait(true);
            viewModels.Add(new ItemCollection()
            {
                GroupTitle = "Image",
                Items = images.Items.ToArray()
            });

            var tags = await this.restService.GetItems<ItemDto>(entity.Tags).ConfigureAwait(true);
            viewModels.Add(new ItemCollection()
            {
                GroupTitle = "Tags",
                Items = tags.Items.ToArray()
            });

            try
            {
                var pressReleases = await this.restService.GetItems<ItemDto>(entity.GeographicalRegions).ConfigureAwait(true);
                viewModels.Add(new ItemCollection()
                {
                    GroupTitle = "Geographical Regions",
                    Items = pressReleases.Items.ToArray(),
                });
            }
            catch (ErrorCodeException err)
            {
                if (this.renderContext.IsEdit)
                {
                    viewModels.Add(new ItemCollection()
                    {
                        GroupTitle = $"Geographical Regions - {err.Message}",
                        Items = Array.Empty<ItemDto>(),
                    });
                }
            }

            try
            {
                var pressReleases = await this.restService.GetItems<ItemDto>(entity.PressReleases).ConfigureAwait(true);
                viewModels.Add(new ItemCollection()
                {
                    GroupTitle = "Press Releases",
                    Items = pressReleases.Items.ToArray(),
                });
            }
            catch (ErrorCodeException err)
            {
                if (this.renderContext.IsEdit)
                {
                    viewModels.Add(new ItemCollection()
                    {
                        GroupTitle = $"Press Releases - {err.Message}",
                        Items = Array.Empty<ItemDto>(),
                    });
                }
            }

            return viewModels;
        }
    }
}