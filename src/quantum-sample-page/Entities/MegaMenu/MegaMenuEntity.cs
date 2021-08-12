using System.Collections.Generic;
using System.ComponentModel;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.RestSdk;

namespace Renderer.Entities
{
    public class MegaMenuEntity : NavigationEntity
    {
        [ContentSection("First Section", 0)]
        [DisplayName("First page")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext FirstPage { get; set; }

        [ContentSection("First Section", 1)]
        [DisplayName("First section css")]
        public string FirstSectionCss { get; set; }

        [ContentSection("First Section", 2)]
        [DisplayName("First section proportions")]
        public IList<string> FirstSectionProportions { get; set; }

        [ContentSection("Second Section", 0)]
        [DisplayName("Second page")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext SecondPage { get; set; }

        [ContentSection("Second Section", 1)]
        [DisplayName("Second section css")]
        public string SecondSectionCss { get; set; }

        [ContentSection("Second Section", 2)]
        [DisplayName("Second section proportions")]
        public IList<string> SecondSectionProportions { get; set; }

        [ContentSection("Third Section", 0)]
        [DisplayName("Third page")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext ThirdPage { get; set; }

        [ContentSection("Third Section", 1)]
        [DisplayName("Third section css")]
        public string ThirdSectionCss { get; set; }

        [ContentSection("Third Section", 2)]
        [DisplayName("Third section proportions")]
        public IList<string> ThirdSectionProportions { get; set; }

        public bool HideSectionsInEdit { get; set; }
    }
}