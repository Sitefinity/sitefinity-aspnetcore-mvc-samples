using System;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace sitefinity_insight.ViewComponents
{
    /// <summary>
    /// Hello Insight widget.
    /// </summary>
    [SitefinityWidget(Title = "Hello Insight")]
    public class HelloInsightViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(IViewComponentContext context)
        {
            return this.View();
        }
    }
}
