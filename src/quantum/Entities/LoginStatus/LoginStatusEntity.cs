using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Renderer.Entities.LoginStatus
{
    public class LoginStatusEntity
    {
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false, DisableInteraction = true, ShowSiteSelector = true)]
        public MixedContentContext LoginPage { get; set; }

        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false, DisableInteraction = true, ShowSiteSelector = true)]
        public MixedContentContext RegistrationPage { get; set; }
    }
}
