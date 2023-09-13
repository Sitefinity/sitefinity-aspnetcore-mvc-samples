using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Renderer.Entities.LoginStatus;
using Renderer.ViewModels.LoginStatus;

namespace Renderer.Models.LoginStatus
{
    /// <summary>
    /// The model class.
    /// </summary>
    public class LoginStatusModel : ILoginStatusModel
    {
        private readonly IRestClient restService;
        private readonly IRenderContext renderContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginStatusModel"/> class.
        /// </summary>
        /// <param name="restService">The rest service.</param>
        /// <param name="renderContext">The render context.</param>
        public LoginStatusModel(IRestClient restService, IRenderContext renderContext)
        {
            this.restService = restService;
            this.renderContext = renderContext;
        }

        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The generated view models.</returns>
        public async Task<LoginStatusViewModel> GetViewModels(LoginStatusEntity entity)
        {
            var viewModel = new LoginStatusViewModel();

            if (entity.LoginPage.HasSelectedItems())
            {
                viewModel.LoginPage = await this.restService.GetItem<PageNodeDto>(entity.LoginPage);
            }

            if (entity.RegistrationPage.HasSelectedItems())
            {
                viewModel.RegistrationPage = await this.restService.GetItem<PageNodeDto>(entity.RegistrationPage);
            }

            return viewModel;
        }
    }
}