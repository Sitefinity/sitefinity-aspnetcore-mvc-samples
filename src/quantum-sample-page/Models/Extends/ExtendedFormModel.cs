using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Form;
using Progress.Sitefinity.RestSdk.OData;
using Renderer.Entities.Extends;
using Renderer.ViewModels.Extends;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Microsoft.Extensions.Localization;

namespace Renderer.Models.Extends
{
    public class ExtendedFormModel : FormModel
    {
        public ExtendedFormModel(IODataRestClient restService, IRequestContext requestContext, IStyleClassesProvider styles, IViewComponentTreeBuilder viewComponentTreeBuilder, IRenderContext renderContext, IStringLocalizer<FormModel> localizer, ISitefinityConfig sfConfig)
            : base(restService, requestContext, styles, viewComponentTreeBuilder, renderContext, localizer, sfConfig)
        {
        }
        
        public override async Task<FormViewModel> InitializeViewModel(FormEntity entity, IQueryCollection query)
        {
            var extendedEntity = entity as ExtendedFormEntity;
            var viewModel = await base.InitializeViewModel(entity, query);
            var extendedViewModel = new ExtendedFormViewModel(viewModel);

            extendedViewModel.Heading = extendedEntity.Heading;

            return extendedViewModel;
        }
    }
}
