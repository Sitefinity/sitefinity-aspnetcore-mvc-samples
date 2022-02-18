using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentBlock;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using extended_content_block.ViewModels;
using extended_content_block.Entities;
namespace extended_content_block.Models
{
    /// <summary>
    /// Extended model for the ContentBlock view component.
    /// </summary>
    public class ExtendedContentBlockModel : ContentBlockModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedContentBlockModel"/> class.
        /// </summary>
        /// <param name="restClient">The HTTP client.</param>
        /// <param name="styles">The style classes provider.</param>
        public ExtendedContentBlockModel(IRestClient restClient, IStyleClassesProvider styles)
            : base(restClient, styles)
        {
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The view model.</returns>
        public override async Task<ContentBlockViewModel> InitializeViewModel(ContentBlockEntity entity)
        {
            var viewModel = await base.InitializeViewModel(entity).ConfigureAwait(false);
            return new ExtendedContentBlockViewModel(viewModel)
            {
                ModifiedContent = viewModel.Content + (entity as ExtendedContentBlockEntity).TextToAppend
            };
        }
    }
}
