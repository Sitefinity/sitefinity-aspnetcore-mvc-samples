using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant;

namespace PARAGAssistantWidget.Models.PARAGAssistant
{
    public interface IPARAGAssistantModel
    {
        Task<PARAGAssistantViewModel> GetViewModel(IViewComponentContext<PARAGAssistantEntity> context);
    }
}
