# Using React for Front-end views with Sitefinity .NET Core

This sample demonstrate how to use .NET Core renderer with React framework.

## Responsibilities of frameworks
### React
React is responsible for the visualization and binding of the data fetched by the .NET Core application.

### .NET Renderer
.NET renderer is responsible for generating the widget designers and for fetching of the data from Sitefinity.

## The steps below explains the process needed to bind the React components with the views of the .NET Core.
**Note:** Please use Node version 16 or higher.

1. Open terminal in `react` directory and run `npm i`
2. Create container element (ex. `<div>`) in the view for the widget in **.NET Core** application. e.g. `netcore/Views/Shared/Components/Testimonial/Single.cshtml`
3. Set attribute `data-sfwidget="NameOfWidget"` - **NameOfWidget** must be registered into the `registry.js` in the React app. Check **Testimonial.js** component inside this `react/src/Testimonial.js` for reference
4. Add `@JsonConvert.SerializeObject(ModelData)` as content of the `<div>` element - **ModelData** is the view model data that this React component will use
5. In the `react` directory run `npm run build`. This will generate `main.[hash].js` bundle located `react/build/static/js` directory and will copy it into `main.js` file inside `netcore/wwwroot/main.js`. This file is referenced in the Renderer layout `cshtml` file - `netcore/Views/Shared/_ReactIndexLayout.cshtml`