using System.Threading.Tasks;
using sitefinity_data.ViewModels;

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
        Task<NewsViewModel[]> GetViewModels(SitefinityDataEntity entity);
    }
}