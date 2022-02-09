using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Dto;
using Renderer.Dto;
using Renderer.Entities.LoginStatus;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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
        /// Initializes a new instance of the <see cref="TestimonialViewComponent"/> class.
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
        public async Task<LoginStatusItem> GetViewModels(LoginStatusEntity entity)
        {
            var viewModel = new LoginStatusItem();

            viewModel.LoginPage = await this.GetItem<PageNodeDto>(entity.LoginPage).ConfigureAwait(true);
            viewModel.RegistrationPage = await this.GetItem<PageNodeDto>(entity.RegistrationPage).ConfigureAwait(true);

            return viewModel;
        }

        private async Task<T> GetItem<T>(MixedContentContext contentContext)
           where T : class, ISdkItem
        {
            if (contentContext == null)
                return null;

            var getAllArgsDictionary = contentContext.Content.ToDictionary(x => x.Type, y => new GetAllArgs());
            var result = await this.restService.GetItems<T>(contentContext, getAllArgsDictionary);

            if (result.Items.Count != 1)
            {
                return null;
            }

            return result.Items.FirstOrDefault();
        }
    }
}