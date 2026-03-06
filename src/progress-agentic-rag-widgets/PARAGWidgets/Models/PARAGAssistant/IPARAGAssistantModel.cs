using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant;

namespace PARAGWidgets.Models.PARAGAssistant
{
    public interface IPARAGAssistantModel
    {
        Task<PARAGAssistantViewModel> GetViewModel(IViewComponentContext<PARAGAssistantEntity> context);
    }
}
