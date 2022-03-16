using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentBlock;

namespace extended_content_block.ViewModels
{
    /// <summary>
    /// Extended viewModel for the ContentBlock view component.
    /// </summary>
    public class ExtendedContentBlockViewModel : ContentBlockViewModel
    {
        public ExtendedContentBlockViewModel(ContentBlockViewModel source)
        {
            this.Content = source.Content;
            this.WrapperCssClass = source.WrapperCssClass;
            this.TagName = source.TagName;
        }

        public string ModifiedContent { get; set; }
    }
}
