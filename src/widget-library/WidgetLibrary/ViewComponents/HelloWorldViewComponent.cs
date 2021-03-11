using System;
using Microsoft.AspNetCore.Mvc;
using WidgetLibrary.Entities.HelloWorld;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace WidgetLibrary.ViewComponents
{
    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget(Title = "Hello World")]
    [ViewComponent(Name = "MyCompanyPrefixHelloWorld")]
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
