using System.Collections.Generic;
using System.Threading.Tasks;
using content_selectors.Entities.SelectorDemoUsage;
using content_selectors.ViewModels.SelectorDemoUsage;

namespace content_selectors.Models.SelectorDemoUsage
{
    /// <summary>
    /// The model interface.
    /// </summary>
    public interface ISelectorDemoUsageModel
    {
        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The generated view models.</returns>
        Task<IList<ItemCollection>> GetViewModels(SelectorDemoEntity entity);
    }
}