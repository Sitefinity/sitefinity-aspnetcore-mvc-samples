using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace blazor_dev_tools.Components
{
    public class BlazorBase : ComponentBase 
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
            if (this.IsEdit)
            {
                await JS.InvokeVoidAsync("componentRendered");
            } 

            await base.OnAfterRenderAsync(firstRender);
        }
    }
}