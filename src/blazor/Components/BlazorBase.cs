using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace blazor.Components
{
    public class BlazorBase: ComponentBase 
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private IJSRuntime JS { get; set; }

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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JS.InvokeVoidAsync("componentRendered");
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}