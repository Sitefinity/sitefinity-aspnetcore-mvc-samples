using System;
using Progress.Sitefinity.AspNetCore.SitefinityApi.Dto;

namespace content_selectors.ViewModels.SelectorDemoUsage
{
    /// <summary>
    /// The view model.
    /// </summary>
    public class ItemDto : ISdkItem
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <inheritdoc/>
        public string Id { get; set; }
    }
}
