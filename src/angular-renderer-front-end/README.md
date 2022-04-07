# Using Angular for Front-end views with Sitefinity .NET Core

This sample demonstrate how to use .NET Core renderer with the Angular framework.

## Responsibilities of frameworks
### Angular
Angular is responsible for the visualization and binding of the data fetched by the .NET Core application.

### .NET Renderer
.NET renderer is responsible for generating the widget designers and for fetching of the data from Sitefinity.

## The steps below explains the process needed to bind the Angular components with the views of the .NET Core.
**Note:** Please use Node version 16 or higher.

1. Open terminal in `angular` directory and run `npm i` and afterwords `ng build`
2. Reference the angular component (`sf-testimonial`) in the view for the widget in **.NET Renderer** application. e.g. `netcore/Views/Shared/Components/Testimonial/Single.cshtml`
3. Set the attribute model=`@JsonConvert.SerializeObject(ModelData)` of the `sf-angular` element - **ModelData** is the view model data that this Angular component will use
4. In the `angular` directory run `npm run build`. This will generate three bundles inside the `netcore/wwwroot/` directory. These javascipt bundles are referenced in the Renderer layout `cshtml` file - `netcore/Views/Shared/_AngularIndexLayout.cshtml`