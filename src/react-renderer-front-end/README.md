# Using React for Front-end views with Sitefinity .NET Core

This sample demonstrate how to use .NET Core renderer with React framework.

React component are used to visualize data fetched by the .NET Core application. You can take advantage of the React framework for re-using UI elements and easy styling while leave the heavy lifting for fetching data to the .NET Core renderer.

The steps below explains the process needed to bind the React components with the views of the .NET Core.


**Note:** Please use Node version 16 or higher.

1. Open terminal and run `npm i` then `npm run build`. This will generate `main.[hash].js` bundle located `react/build/static/js` directory and will copy it into `main.js` file inside `netcore/wwwroot/main.js`. This file is referenced in the Renderer layout `cshtml` file - `netcore/Views/Shared/_ReactIndexLayout.cshtml`
2. Create container element (ex. `<div>`) in the view for the widget in **.NET Core** application. e.g. `netcore/Views/Shared/Components/Testimonial/Single.cshtml`
3. Set attribute `data-sfwidget="NameOfWidget"` - **NameOfWidget** must be registered into the `registry.js` in the React app. Check **Testimonial.js** component inside this `react/src/Testimonial.js` for reference
4. Add `@JsonConvert.SerializeObject(ModelData)` as content of the `<div>` element - **ModelData** is the view model data that this React component will use