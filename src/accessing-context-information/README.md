# Accessing context information

This project demonstrates how context information is provided for use in the widgets. The contextual information contains the following:

* Current user
* Current culture
* Current site
* Current page

Additionally examples are added how to retrieve custom field data for the current user and page

Developers can check the current request context by leveraging the IRequestContext interface located under the namespace Progress.Sitefinity.AspNetCore.Web. It has the following signature:

``` c#
namespace Progress.Sitefinity.AspNetCore.Web
{
    /// <summary>
    /// The request context contract.
    /// </summary>
    public interface IRequestContext
    {
        /// <summary>
        /// Gets the current user.
        /// </summary>
        IPrincipal User { get; }

        /// <summary>
        /// Gets the current culture.
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// Gets the current site id.
        /// </summary>
        SiteObject Site { get; }

        /// <summary>
        /// Gets the ID of the current page node.
        /// </summary>
        PageNodeDto PageNode { get; }
    }
}
```

Please refer to [the file in this sample](./Views/Shared/Components/ContextDemo/Default.cshtml) on how this interface can be used.
