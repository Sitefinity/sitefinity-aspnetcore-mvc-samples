using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using sitefinity_data.Models.SitefinityData;

namespace sitefinity_data.ViewComponents
{
    /// <summary>
    /// The view component for accessing Sitefinity data.
    /// </summary>
    [SitefinityWidget]
    public class SitefinityDataViewComponent : ViewComponent
    {
        private ISitefinityDataModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityDataViewComponent"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SitefinityDataViewComponent(ISitefinityDataModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="context">The view component context.</param>
        /// <returns>The view component result.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<SitefinityDataEntity> context)
        {
            var items = await this.model.GetViewModels(context.Entity);
            return this.View(items);
        }
    }
}


