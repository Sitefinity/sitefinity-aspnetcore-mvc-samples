using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;

namespace Renderer.ViewModels.Extends
{
    public class ExtendedContentListViewModel: ContentListViewModel
    {
        public ExtendedContentListViewModel(ContentListViewModel source)
        {
            this.Pager = source.Pager;
        }
        public string Heading { get; set; }

        public bool IsNarrowed { get; set; }
    }
}
