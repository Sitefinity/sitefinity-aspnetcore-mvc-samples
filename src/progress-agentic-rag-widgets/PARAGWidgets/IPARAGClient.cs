using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant.Dto;

namespace PARAGWidgets
{
    internal interface IPARAGClient
    {
        Task<VersionInfoDto> GetVersionInfoAsync();
    }
}
