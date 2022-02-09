
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Dto;

namespace Renderer.Dto
{
    public class LoginStatusItem: SdkItem
    {
        public PageNodeDto LoginPage { get; set; }
        public PageNodeDto RegistrationPage { get; set; }
    }
}
