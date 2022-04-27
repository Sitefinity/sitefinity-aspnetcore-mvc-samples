using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using tabs_section.ViewModels;

namespace tabs_section.ViewComponents
{
    [SitefinityWidget(Title = "Tabs Section", Category = WidgetCategory.Layout, EmptyIconText = "No tabs has been created", EmptyIconAction = EmptyLinkAction.None, EmptyIcon = "file-text-o")]
    public class TabsSectionViewComponent : ViewComponent
    {
        private IRenderContext renderContext;

        public TabsSectionViewComponent(IRenderContext renderContext)
        {
            this.renderContext = renderContext;
        }

        public IViewComponentResult Invoke(ICompositeViewComponentContext<TabsSectionEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = new TabsSectionViewModel();

            viewModel.FirstTabLabel = context.Entity.FirstTabLabel;
            viewModel.FirstTabSectionCss = context.Entity.FirstTabSectionCss;
            viewModel.FirstTabSectionProportions = context.Entity.FirstTabSectionProportions ?? new List<string>();

            viewModel.SecondTabLabel = context.Entity.SecondTabLabel;
            viewModel.SecondTabSectionCss = context.Entity.SecondTabSectionCss;
            viewModel.SecondTabSectionProportions = context.Entity.SecondTabSectionProportions ?? new List<string>();
            viewModel.Context = context;

            return this.View(viewModel);
        }
    }

    public class TabsSectionEntity
    {
        [Required]
        [DisplayName("First tab label")]
        public string FirstTabLabel { get; set; }

        [ContentSection("First Tab Section", 1)]
        [DisplayName("First tab section css")]
        public string FirstTabSectionCss { get; set; }

        [Required]
        [ContentSection("First Tab Section", 2)]
        [DisplayName("First tab section proportions")]
        public IList<string> FirstTabSectionProportions { get; set; }

        [Required]
        [DisplayName("Second tab label")]
        public string SecondTabLabel { get; set; }

        [ContentSection("Second Tab Section", 1)]
        [DisplayName("Second tab section css")]
        public string SecondTabSectionCss { get; set; }

        [Required]
        [ContentSection("Second Tab Section", 2)]
        [DisplayName("Second tab section proportions")]
        public IList<string> SecondTabSectionProportions { get; set; }
    }
}
