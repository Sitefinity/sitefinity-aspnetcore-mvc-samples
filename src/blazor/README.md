# Blazor

This sample demonstrates how to use your own blazor components and integrating them with the Sitefinity .Net Renderer.

## How to integrate the sample

### The widget
The sample works by wrapping the [blazor component](./Components/CalculatorComponent.razor) into a [view component](./ViewComponents/CalculatorViewComponent.cs) that can be shown in the WYSIWYG editor as a widget. Using the OOB implementation for view components and their integration into the WYSIWYG editor, the blazor components can be configured via the autogenerated designers and these configured values can be passed into the blazor component as parameters through the [view of the view component](./Views/Shared/Components/Calculator/Default.cshtml).

This is achieved by calling the **RenderComponent method**

``` c#

@(await Html.RenderComponentAsync<CalculatorComponent>(RenderMode.Server, new {Title = @Model.Title }))

```

### The framework
The blazor framework was integrated by modifying the startup file to include the necessary blazor services and endpoints

``` c#

/// adds the needed services
services.AddServerSideBlazor();

...

// configures the blazor endpoints
endpoints.MapBlazorHub();

```

In order to have these methods at your disposal, you need to reference the package [Microsoft.AspNetCore.Components](./blazor.csproj)

Additionally a special [layout file](./Views/Shared/BlazorLayout.cshtml) was created for pages that will hold the blazor components. The key points here are:

``` html

<!-- includes the blazor framework in the html. important to have this right before the closing body tag -->
<script src="_framework/blazor.server.js"></script>

```