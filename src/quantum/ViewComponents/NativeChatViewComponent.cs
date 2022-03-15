using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Renderer.Entities.NativeChat;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Renderer.Models.NativeChat;

namespace Renderer.ViewComponents
{
    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget(Title="Native chat", EmptyIconText = "Select a chatbot", EmptyIcon = "")]
    public class NativeChatViewComponent : ViewComponent
    {
        private INativeChatModel nativeChatModel;

        public NativeChatViewComponent(INativeChatModel nativeChatModel)
        {
            this.nativeChatModel = nativeChatModel;
        }

        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<NativeChatEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var viewModel = await this.nativeChatModel.GetViewModel(context.Entity);

            return this.View(viewModel);
        }
    }
}
