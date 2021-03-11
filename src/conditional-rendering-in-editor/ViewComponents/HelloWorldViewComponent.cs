using System;
using Microsoft.AspNetCore.Mvc;
using conditional_rendering_in_editor.Entities.HelloWorld;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;

namespace conditional_rendering_in_editor.ViewComponents
{
    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget]
    public class HelloWorldViewComponent : ViewComponent
    {
        private IRenderContext renderContext;
        public HelloWorldViewComponent(IRenderContext renderContext)
        {
            this.renderContext = renderContext;
        }

        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(IViewComponentContext<HelloWorldEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (this.renderContext.IsEdit)
            {
                context.Entity.Message = $"{context.Entity.Message} in edit";
            }
            else if (this.renderContext.IsPreview)
            {
                context.Entity.Message = $"{context.Entity.Message} in preview";
            }

            return this.View(context.Entity);
        }
    }
}
