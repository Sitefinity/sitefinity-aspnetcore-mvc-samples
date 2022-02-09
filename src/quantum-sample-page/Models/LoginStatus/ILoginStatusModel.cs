using System.Threading.Tasks;
using Renderer.Entities.LoginStatus;
using Renderer.ViewModels.LoginStatus;

namespace Renderer.Models.LoginStatus
{
    /// <summary>
    /// The model interface.
    /// </summary>
    public interface ILoginStatusModel
    {
        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <returns>The generated view models.</returns>
        Task<LoginStatusViewModel> GetViewModels(LoginStatusEntity entity);
    }
}