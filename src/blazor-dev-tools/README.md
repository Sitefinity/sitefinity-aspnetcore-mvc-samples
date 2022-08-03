# Blazor

This sample demonstrates how to use the Telerik UI components for Blazor in the .Net Renderer Project.
This sample builds on top of the blazor sample [here](../blazor/Readme.md)

## How to integrate the sample

1. Follow the instructions [here](../blazor/Readme.md)
2. Follow the instructions for integrating the Telerik UI Components [here](https://docs.telerik.com/blazor-ui/getting-started/server-blazor)

### The framework
The blazor and telerik frameworks were integrated by modifying the startup file to include the necessary services and endpoints

``` c#

/// adds the needed services
builder.Services.AddServerSideBlazor();
builder.Services.AddTelerikBlazor();

/// static files are necessary in order to be able to deliver the blazor framework scripts 
app.UseStaticFiles();

// configures the blazor endpoints
endpoints.MapBlazorHub();

```

In order to have these methods at your disposal, you need to reference the packages

[Microsoft.AspNetCore.Components](./blazor-dev-tools.csproj)
[Telerik.UI.for.Blazor.Trial](./blazor-dev-tools.csproj)

Additionally a special [layout file](./Views/Shared/BlazorLayout.cshtml) was created for pages that will hold the blazor components. The key points here are:

``` html

<!-- Includes styles for the Telerik components -->
<link rel="stylesheet" href="_content/Telerik.UI.for.Blazor.Trial/css/kendo-theme-default/all.css" />

<!-- includes the blazor framework in the html. important to have this right before the closing body tag -->
<script src="_framework/blazor.server.js"></script>
<script src="_content/Telerik.UI.for.Blazor.Trial/js/telerik-blazor.js" defer></script>

```

Finally create an _Imports.razor file that contains the following imports:

``` c#

@using System.Net.Http
@using Microsoft.AspNetCore.Authorization
@using Microsoft.AspNetCore.Components.Authorization
@using Microsoft.AspNetCore.Components.Forms
@using Microsoft.AspNetCore.Components.Routing
@using Microsoft.AspNetCore.Components.Web
@using Microsoft.JSInterop
@using System.IO

@using Telerik.Blazor
@using Telerik.Blazor.Components

```

### The chart component

The chart component(./ViewComponents/ChartViewComponent) uses the [**TelerikChart** blazor component](./Components/ChartComponent.razor) by rendering it directly through the view(./Views/Shared/Components/Chart/Default.cshtml).

One thing that is specific for the TelerikChart component is that it cannot render standalone (e.g. when a widget is dropped). For this reason a custom script is injected only in edit mode of the page to ensure that the page is loaded and is responsive when the widget is inserted. For reference see the view(./Views/Shared/Components/Chart/Default.cshtml) of the chart component.