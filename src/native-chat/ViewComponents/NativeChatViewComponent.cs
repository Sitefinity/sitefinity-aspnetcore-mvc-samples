using System;
using Microsoft.AspNetCore.Mvc;
using native_chat.Entities.NativeChat;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace native_chat.ViewComponents
{
    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget(Title="Native chat")]
    public class NativeChatViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(IViewComponentContext<NativeChatEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.View(context.Entity);
        }
    }
}
