using Microsoft.AspNetCore.Components;

namespace blazor.Components
{
    public class BlazorBase: ComponentBase 
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }
        
        public bool IsEdit
        {
            get
            {
                return this.navigationManager.Uri.Contains("sfaction=edit");
            }
        }

        public bool IsPreview
        {
            get
            {
                return this.navigationManager.Uri.Contains("sfaction=preview");
            }
        }
    }
}