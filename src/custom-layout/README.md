# Custom layout sample
This sample demonstrates how to setup a custom layout file that can be used as a base template for building your .NET Core application

Your MVC .Net Core pages start from layout file just like in native site. This layout file is meant to contain the common HTML page structure like its head, body tags, common scripts and the styles for the page.

We provide a default layout file which you can use to start your pages even when you don’t have any custom layouts created. The default layout file, provided with the renderer, references Bootstrap5 css, since it’s one of the most popular and used css frameworks. The default section widget templates also contain the Bootstrap5 grid system markup.

Template inheritance is available for Sitefinity Templates as well. You can base your Sitefinity templates on physical layout files from the .NET Core Renderer. 

You can create a new layout file inside your .Net Core project under the folders /Views/Shared. The new file must contain the words “Layout” or “Template” in its name (valid for builds prior to 14.3.8023(excluding), for builds after 14.3.8023(including) this is not required).

The layout files are integrated in the backend UI of Sitefinity and you can choose between them when you create new page. To do this go to Sitefinity backend -> Pages-> Create a page, enter page title and click “Continue”. Your layout files will be available in the lower part of the template selector under the section “External .NET core renderer”

To define your own custom sections, you can decorate several html tags (div, header, footer, aside, section, main, body) with the data-sfcontainer="your_placeholder_name" attribute, which will enable users to drop into different placeholders from scratch

## Layout Requirements
* Every layout file must have a defined section named Scripts.

* Every layout file must have a reference to the taghelpers defined in the namespace Progress.Sitefinity.AspNetCore like so:

@addTagHelper *, Progress.Sitefinity.AspNetCore

* Every layout must have a model defined like so:

@model Progress.Sitefinity.AspNetCore.Models.PageModel

* Every layout can render the SEO metadata from Sitefinity by using the custom method:

@Html.RenderSeoMeta(this.Model)
