using System.Collections.Generic;
using System.Threading.Tasks;
using sitefinity_data_taxa_filter.ViewModels.SitefinityData;

namespace sitefinity_data_taxa_filter.Models.SitefinityData
{
    /// <summary>
    /// The model interface.
    /// </summary>
    public interface ISitefinityDataModel
    {
        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The generated view models.</returns>
        Task<IList<ItemViewModel>> GetViewModels(SitefinityDataEntity entity);
    }
}