using System.Threading.Tasks;
using content_selectors.Entities.SelectorDemoUsage;
using content_selectors.Models.SelectorDemoUsage;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace content_selectors.ViewComponents
{
    /// <summary>
    /// The view component for working with content selectors.
    /// </summary>
    [SitefinityWidget]
    public class SelectorDemoUsageViewComponent : ViewComponent
    {
        private ISelectorDemoUsageModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorDemoUsageViewComponent"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SelectorDemoUsageViewComponent(ISelectorDemoUsageModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="context">The view component context.</param>
        /// <returns>The view component result.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<SelectorDemoEntity> context)
        {
            var viewModels = await this.model.GetViewModels(context.Entity);
            return this.View(viewModels);
        }
    }
}


