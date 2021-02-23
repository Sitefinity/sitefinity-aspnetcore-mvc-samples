using System.Collections.Generic;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.SitefinityApi;
using Progress.Sitefinity.AspNetCore.SitefinityApi.Dto;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Section;
using mega_menu.Entities;
using mega_menu.ViewModels;

namespace mega_menu.Models
{
    public class MegaMenuModel : IMegaMenuModel
    {
        private IRestClient restClient;
        private ISectionModel sectionModel;
        private INavigationModel navigationModel;

        public MegaMenuModel(IRestClient restClient, ISectionModel sectionModel, INavigationModel navigationModel)
        {
            this.restClient = restClient;
            this.sectionModel = sectionModel;
            this.navigationModel = navigationModel;
        }

        public async Task<MegaMenuViewModel> InitializeViewModel(MegaMenuEntity entity, IRenderContext renderContext)
        {
            var sectionEntity = new SectionEntity()
            {
                ColumnsCount = entity.ColumnsCount,
                ColumnsBackground = entity.ColumnsBackground,
                ColumnsPadding = entity.ColumnsPadding,
                CustomCssClass = entity.CustomCssClass,
                Labels = entity.Labels,
                SectionMargin = entity.SectionMargin,
                SectionPadding = entity.SectionPadding,
            };

            var display = ";display:none";
            if (renderContext.IsEdit && !entity.HideSectionsInEdit)
                display = string.Empty;

            var sectionViewModel = await this.sectionModel.InitializeViewModel(sectionEntity);
            sectionViewModel.SectionStyle += display;
            sectionViewModel.ColumnProportions = new List<string>()
            {
                // first row
                "4",
                "4",
                "4",

                // second row
                "6",
                "6",

                // third row
                "4",
                "4",
                "4",
            };

            var viewModel = new MegaMenuViewModel();
            viewModel.SectionViewModel = sectionViewModel;

            var secondSectionEntity = new SectionEntity()
            {
                SectionMargin = entity.SecondSectionMargin,
                SectionPadding = entity.SecondSectionPadding,
            };

            viewModel.SecondSectionViewModel = await this.sectionModel.InitializeViewModel(secondSectionEntity);
            viewModel.SecondSectionViewModel.SectionStyle += display;

            var thirdSectionEntity = new SectionEntity()
            {
                SectionMargin = entity.ThirdSectionMargin,
                SectionPadding = entity.ThirdSectionPadding,
            };

            viewModel.ThirdSectionViewModel = await this.sectionModel.InitializeViewModel(thirdSectionEntity);
            viewModel.ThirdSectionViewModel.SectionStyle += display;

            var allContexts = new[] { entity.FirstPage, entity.SecondPage, entity.ThirdPage };
            var allPagesResponse = await this.restClient.GetItems<PageNodeDto>(allContexts, new GetAllArgs()).ConfigureAwait(true);

            if (allPagesResponse.Items.Count > 0)
                viewModel.FirstPageId = allPagesResponse.Items[0].Id;

            if (allPagesResponse.Items.Count > 1)
                viewModel.SecondPageId = allPagesResponse.Items[1].Id;

            if (allPagesResponse.Items.Count > 2)
                viewModel.ThirdPageId = allPagesResponse.Items[2].Id;

            viewModel.NavigationViewModel = await this.navigationModel.InitializeViewModel(entity).ConfigureAwait(true);

            return viewModel;
        }
    }
}
