# Using React for Front-end views with Sitefinity .NET Core

1. Open terminal and run `npm i` then `npm run build`
2. Copy the content from `main.[hash].js` bundle located `react/build` directory into `main.js` file inside `netcore/wwwroot/main.js`. This file is referenced in the Renderer layout `cshtml` file - `netcore/Views/Shared/_ReactIndexLayout.cshtml`
3. Create container element (ex. `<div>`) in the view for the widget in **.NET Core** application. e.g. `netcore/Views/Shared/Components/Testimonial/Single.cshtml`
4. Set attribute `data-sfwidget="NameOfWidget"` - **NameOfWidget** must be registered into the `registry.js` in the React app. Check **Testimonial.js** component inside this `react/src/Testimonial.js` for reference
5. Add `@JsonConvert.SerializeObject(ModelData)` as content of the `<div>` element - **ModelData** is the view model data that this React component will use