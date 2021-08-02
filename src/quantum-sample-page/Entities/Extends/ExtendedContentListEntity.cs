using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;

namespace Renderer.Entities.Extends
{
    public class ExtendedContentListEntity: ContentListEntity
    {
        public string Heading { get; set; }

        public bool SmallList { get; set; }
    }
}
