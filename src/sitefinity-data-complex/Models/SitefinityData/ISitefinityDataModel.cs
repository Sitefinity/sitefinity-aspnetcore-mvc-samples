using System.Collections.Generic;
using System.Threading.Tasks;
using sitefinity_data.Dto;
using sitefinity_data.ViewModels;
using sitefinity_data.ViewModels.SitefinityData;

namespace sitefinity_data.Models.SitefinityData
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
        Task<IList<ItemGroup>> GetViewModels(SitefinityDataEntity entity);
    }
}