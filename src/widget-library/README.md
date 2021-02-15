# Widget library

This project demonstrates how to build a widget library for distributing custom .net core Sitefinity widgets. Using this approach you can embed your widgets and templates into a dll, have them precompiled and distribute them via NuGet easily. 

For more information on Razor class libraries see this [article](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/ui-class?view=aspnetcore-3.1&tabs=visual-studio) 

## Note

### We highly recommend prefixing your widgets(ViewComponents) and Templates(Layout files) with a custom prefix specific to our project. Otherwise there may be name clashes between two different projects.