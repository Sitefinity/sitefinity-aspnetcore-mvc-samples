# Widget library

This project demonstrates how to build a widget library for distributing custom .net core Sitefinity widgets. Using this approach you can embed your widgets and templates into a dll, have them precompiled and distribute them via NuGet easily. 

For more information on Razor class libraries see this [article](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/ui-class?view=aspnetcore-3.1&tabs=visual-studio).

## Embedded scripts
We have a custom solution for delivering scripts per widget in widget libraries. Because widgets can be placed multiple times in a single page, their
scripts must not be duplicated. Thus, we have implemented an approach where all of the scripts are placed under the Scripts folder either in the WebApp or in the WidgetLibrary. If you wish for a script to be returned in the page, then it must be referenced in the view file of the ViewComponent (using the 'section-name' tag helper). For reference see [Default.cshtml](./WidgetLibrary/Views/Shared/Components/MyCompanyPrefixHelloWorld/Default.cshtml). If you wish to override this script, then it must be referenced in a folder called Scripts in the WebApp project.

## Note

### We highly recommend prefixing your widgets(ViewComponents) and Templates(Layout files) with a custom prefix specific to our project. Otherwise there may be name clashes between two different projects.