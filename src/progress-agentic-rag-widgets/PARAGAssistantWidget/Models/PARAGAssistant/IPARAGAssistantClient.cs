using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    internal interface IPARAGAssistantClient
    {
        Task<VersionInfoDto> GetVersionInfoAsync();
    }
}
