using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using mega_menu.Entities;
using mega_menu.Models;

namespace mega_menu.ViewComponents
{
    [SitefinityWidget(Title = "Mega Menu", Category = WidgetCategory.Content, Section = WidgetSection.NavigationAndSearch, EmptyIconText = "No pages have been published", EmptyIconAction = EmptyLinkAction.None, EmptyIcon = "file-text-o")]
    public class MegaMenuViewComponent : ViewComponent
    {
        private IMegaMenuModel megaMenuModel;
        private IRenderContext renderContext;

        public MegaMenuViewComponent(IMegaMenuModel megaMenuModel, IRenderContext renderContext)
        {
            this.megaMenuModel = megaMenuModel;
            this.renderContext = renderContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(ICompositeViewComponentContext<MegaMenuEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.megaMenuModel.InitializeViewModel(context.Entity, this.renderContext);
            viewModel.Context = context;

            return this.View(viewModel);
        }
    }
}
