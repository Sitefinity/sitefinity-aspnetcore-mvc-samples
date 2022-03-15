# Quantum sample demo
An example on how to build your own page with custom styling and scripting. Additionally this example demonstrates how to submit a form to Sitefinity.

## Sitefinity setup

In order to run this sample, you must setup the Quantum sample found [here](https://github.com/Sitefinity/Telerik.Sitefinity.Samples.Quantum/#net-core-renderer-setup).

Once you have everything set-upped, you should see a page called "Quantum sample page" with the label "new-editor". Navigate to that page
on the backend and frontend to view it.

## Styling of the page sample
In `client-src` directory you can find all the assets needed for the front-end representation of the Quantum sample demo. If you want to play around with them all you have to do is to install [NodeJS](https://nodejs.org/) (version 14.15.1) navigate to `client-src` folder and run `npm i` followed by `npm start`. This will precompile all the SCSS file to CSS, copy all the needed assets (fonts) and uglify sample's JavaScript file. On top of this, it will run a watch task that will do the previous tasks when SCSS file or JS file is modified.
Following the [Bootstrap customization](https://getbootstrap.com/docs/5.0/customize/overview/) instructions in `client-src/scss/vendors/_bootstrap.scss` we have overridden some of the variables that did not meet our design requirements, such as spacings, breakpoints and container widths. We urge you to follow this approach when using [Bootstrap](https://getbootstrap.com/) as "This is the way" (yes, Mandalorian pun intended). If you can use the Bootstrap "as is" you can check its usage in the [_Layout.cshtml](https://github.com/Sitefinity/sitefinity-aspnetcore-mvc-samples/blob/gebov/samples-for-13.2/src/starter-template/Views/Shared/_Layout.cshtml#L14) within the [.NET Core starter template sample](https://github.com/Sitefinity/sitefinity-aspnetcore-mvc-samples/tree/gebov/samples-for-13.2/src/starter-template).