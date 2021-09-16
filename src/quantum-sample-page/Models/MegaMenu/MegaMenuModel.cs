using System.Collections.Generic;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation;
using Renderer.Entities;
using Renderer.ViewModels;
using System;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.AspNetCore.RestSdk;

namespace Renderer.Models
{
    public class MegaMenuModel : IMegaMenuModel
    {
        private IRestClient restClient;
        private INavigationModel navigationModel;

        public MegaMenuModel(IRestClient restClient, INavigationModel navigationModel)
        {
            this.restClient = restClient;
            this.navigationModel = navigationModel;
        }

        public async Task<MegaMenuViewModel> InitializeViewModel(MegaMenuEntity entity, IRenderContext renderContext)
        {
            var viewModel = new MegaMenuViewModel();
            viewModel.HideSectionsInEdit = entity.HideSectionsInEdit;

            viewModel.FirstSectionCss = entity.FirstSectionCss;
            viewModel.FirstSectionProportions = entity.FirstSectionProportions ?? new List<string>();

            viewModel.SecondSectionCss = entity.SecondSectionCss;
            viewModel.SecondSectionProportions = entity.SecondSectionProportions ?? new List<string>();

            viewModel.ThirdSectionCss = entity.ThirdSectionCss;
            viewModel.ThirdSectionProportions = entity.ThirdSectionProportions ?? new List<string>();

            var allContexts = new[] { entity.FirstPage, entity.SecondPage, entity.ThirdPage };
            var allPagesResponse = await this.restClient.GetItems<PageNodeDto>(allContexts, new GetAllArgs()).ConfigureAwait(true);

            if (allPagesResponse.Items.Count > 0)
                viewModel.FirstPageId = allPagesResponse.Items[0].Id;
            else
                viewModel.FirstPageId = Guid.Empty.ToString();

            if (allPagesResponse.Items.Count > 1)
                viewModel.SecondPageId = allPagesResponse.Items[1].Id;
            else
                viewModel.SecondPageId = Guid.Empty.ToString();

            if (allPagesResponse.Items.Count > 2)
                viewModel.ThirdPageId = allPagesResponse.Items[2].Id;
            else
                viewModel.ThirdPageId = Guid.Empty.ToString();

            viewModel.NavigationViewModel = await this.navigationModel.InitializeViewModel(entity).ConfigureAwait(true);

            return viewModel;
        }
    }
}