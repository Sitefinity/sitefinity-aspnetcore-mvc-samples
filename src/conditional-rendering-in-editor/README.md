# Conditional rendering in editor

This project demonstrates how conditional rendering can be applied based on the current context - edit, preview or live.
The project is based on the start template sample found in this repository.

Developers can check the current rendering context by leveraging the IRenderContext interface located under the namespace Progress.Sitefinity.AspNetCore.Web. It has the following signature:

``` c#
namespace Progress.Sitefinity.AspNetCore.Web
{
    /// <summary>
    /// The contract for render context.
    /// </summary>
    public interface IRenderContext
    {
        /// <summary>
        /// Gets a value indicating whether the page is requested in edit.
        /// </summary>
        bool IsEdit { get; }

        /// <summary>
        /// Gets a value indicating whether the page is requested in preview.
        /// </summary>
        bool IsPreview { get; }
    }
}
```

Please refer to the file in this sample [HelloWorldViewComponent.cs](./ViewComponents/HelloWorldViewComponent.cs) on how this interface can be used.
