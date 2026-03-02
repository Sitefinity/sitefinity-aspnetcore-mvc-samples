using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant.Dto;

namespace PARAGWidgets
{
    public interface IPARAGClient
    {
        Task<VersionInfoDto> GetVersionInfoAsync();
    }
}
