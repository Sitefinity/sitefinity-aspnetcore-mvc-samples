using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

class CustomViewLocationExpander : IViewLocationExpander
{
    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        return viewLocations.Concat(new string[]
        {
            $"/{{0}}.cshtml",
        });
    }

    public void PopulateValues(ViewLocationExpanderContext context)
    {
    }
}