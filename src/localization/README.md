# Localization

This project serves the purpose of providing an example of how localization can be used in .Net Core projects. 
The project is based on the start template sample found in this repository. More info on localization can be found in [this article](https://www.progress.com/documentation/sitefinity-cms/localization-net-core).
 
## How it works

When the renderer attempts to render a page, the culture for that page is passed along as information to the renderer. The Renderer uses
this culture to set the CultureInfo.CurrentUICulture and CultureInfo.CurrentCulture properties to match the value of the Sitefinity page.
Thus any kind of resource would be resolved in accordance to that culture. 

## API

The API for localized strings is the same [OOB API for developing .NET Core applications](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization).