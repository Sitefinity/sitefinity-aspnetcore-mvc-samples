# Standalone renderers

## Developing with the Client Side Renderers 

In addition to the .NET Core Renderer, Sitefinity offers samples of how to work with both React and Angular as standalone renderers. These types of renderers are called 'standalone', because they are not dependent on anything external other than the CMS itself. This means that developers can build widgets **only** knowing a specific client-side technology - React or Angular. Integrations with other client-side frameworks is possible by [building a custom Renderer](./CustomRenderers.md).

The source code of the two samples for [Angular](https://github.com/Sitefinity/angular-standalone-renderer) and [React](https://github.com/Sitefinity/react-standalone-renderer)

One of the issues that needs to be addressed is the way developers will be working with these client-side renderers during the ‘development phase’ and in the ‘production phase’. Or put in other words - how will the developers work locally and how will they deploy their changes.

There are two approaches that we will look at – one for PaaS and one for SaaS.  

### In а PaaS Scenario (CMS access available)

In a PaaS Scenario the developer has access to the CMS files. He can clone the repo and run it locally. When deploying he can choose which files he wishes to deploy. In order to minimize the cost and not host two applications (as the case with the .NET Renderer), the developer can host the production files on the file system of the CMS application under the following folder template(casing is important for the renderer folder):

/sitefinity/public/renderers/{rendererName} 
/sitefinity/public/renderers/Angular  
/sitefinity/public/renderers/React 

He can additionally use the above folders to develop his changes with the client-side renderers and to see how they would look like. Using a command like: 
ng build --watch  
--output-path="C:\...\SitefinityWebApp\Sitefinity\Public\Renderers\Angular" 

This approach eliminates the use of a proxy in the middle and enables a cheaper hosting model.  

### In а SaaS Scenario (CMS access NOT available)

In this scenario the user does not have access to the CMS files and there should be a way for the developer to develop his changes and host them afterwards.

One option is to use the .NET Renderer (only the proxy part) to enable end-user development of the client-side renderers. Since the renderer is already configured to host both itself and the CMS under the same domain, it can serve the files from its own file system and both the admin UI and iframe will be loaded under the domain of the renderer. The developer can host the production files on the file system of the renderer application under the following folder: 

/wwwroot/sfrenderer/renderers/{rendererName} 
/wwwroot/sfrenderer/renderers/Angular 
/wwwroot/sfrenderer/renderers/React 
 
This approach can be used for production deployment as well and will enables the seemless switch between .NET, Angular and React technologies by just changing the template. Additionally, this approach can be used for the PaaS scenario as well.

### Seeing it all in action

Once you have deployed the files through one of the above methods, go to administration to create a page and you will automatically be presented with an option to create a page based on the ‘Default’ React/Angular template. Once you click on it, you will be navigated to the page editor interface and will be able to select any of the widgets that are present in the renderer.