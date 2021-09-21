using System;
using Microsoft.AspNetCore.Mvc;
using Renderer.Entities.LanguageSelector;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Renderer.Models.LanguageSelector;
using System.Threading.Tasks;

namespace Renderer.ViewComponents
{
    /// <summary>
    /// Language selector widget.
    /// </summary>
    [SitefinityWidget(Title="Language selector")]
    public class LanguageSelectorViewComponent : ViewComponent
    {
        private LanguageSelectorModel languageSelectorModel;

        public LanguageSelectorViewComponent(LanguageSelectorModel languageSelectorModel)
        {
            this.languageSelectorModel = languageSelectorModel;
        }

        /// <inheritdoc/>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<LanguageSelectorEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var viewModel = await this.languageSelectorModel.GetViewModel(context.Entity);
            return this.View(viewModel);
        }
    }
}
