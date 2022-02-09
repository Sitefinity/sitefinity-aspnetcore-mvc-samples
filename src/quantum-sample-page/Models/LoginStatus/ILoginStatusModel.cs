using System.Collections.Generic;
using System.Threading.Tasks;
using Renderer.Entities.LoginStatus;
using Renderer.Dto;

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
        Task<LoginStatusItem> GetViewModels(LoginStatusEntity entity);
    }
}