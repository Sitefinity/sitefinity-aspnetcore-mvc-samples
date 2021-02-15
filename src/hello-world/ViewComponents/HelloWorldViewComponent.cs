using System;
using Microsoft.AspNetCore.Mvc;
using hello_world.Entities.HelloWorld;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace hello_world.ViewComponents
{
    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget(Title="Hello World", EmptyIconText = "Custom empty text", EmptyIcon = "hand-peace-o")]
    public class HelloWorldViewComponent : ViewComponent
    {
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

            return this.View(context.Entity);
        }
    }
}
