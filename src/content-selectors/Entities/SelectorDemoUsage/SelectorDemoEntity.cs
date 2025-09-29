using System.ComponentModel;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace content_selectors.Entities.SelectorDemoUsage
{
    /// <summary>
    /// The entity class.
    /// </summary>
    public class SelectorDemoEntity
    {
        [Content(Type = KnownContentTypes.News)]
        public MixedContentContext News { get; set; }

        [Content(Type = KnownContentTypes.News, LiveData = true)]
        public MixedContentContext NewsLive { get; set; }

        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext Page { get; set; }

        [Content(
            Type = KnownContentTypes.Pages,
            AllowMultipleItemsSelection = true,
            OpenMultipleItemsSelection = true,
            ManualSelectionTabTitle = "External URLs",
            ManualSelectionMainFieldName = "Title",
            ManualSelectionIconClass = "redirecting-page",
            ManualSelectionBreadcrumbText ="External URL",
            ManualSelectionEntityType = typeof(ExternalUrlEntity))]
        public MixedContentContext PagesWithManualSelection { get; set; }

        [Content(Type = "Telerik.Sitefinity.DynamicTypes.Model.Pressreleases.PressRelease")]
        public MixedContentContext PressReleases { get; set; }

        [Content(Type = KnownContentTypes.Images)]
        public MixedContentContext Images { get; set; }

        [Content(Type = KnownContentTypes.Albums)]
        public MixedContentContext Albums { get; set; }

        [Content(Type = KnownContentTypes.DocumentLibraries, AllowMultipleItemsSelection = false, AllowCreate = false, Provider = "secondlibraries")]
        public MixedContentContext DocumentLibrary { get; set; }

        [TaxonomyContent(Type = KnownContentTypes.Tags)]
        public MixedContentContext Tags { get; set; }

        [TaxonomyContent(Type = "GeographicalRegions")]
        public MixedContentContext GeographicalRegions { get; set; }
    }
}