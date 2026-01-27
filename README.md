# Sitefinity .NET Core samples
Sample widgets for the .NET Core renderer

This is a repository contains the samples to demonstrate how to develop your frontend applications with Sitefinity.

## How to setup
Follow the instructions [here](https://www.progress.com/documentation/sitefinity-cms/setup-the-asp.net-core-renderer)

## How to use the Rest SDK
Follow the instructions [here](./RestSDK.md)

## Samples list
* accessing-context-information - for accessing context information such as current user, language, page, site
* all-fields - for reading & rendering the different kinds of custom fields on the front-end
* all-properties - for creating designers
* angular-renderer-front-end - for scripting client side angular web components
* blazor - for integrating blazor components in the .Net ViewComponent widgets
* blazor-dev-tools - for integrating Telerik UI for blazor components in the .Net ViewComponent widgets
* bootstrap-overides - for overriding the default boostrap classes
* captcha - for creating a captcha widget for forms by using google recaptcha v2 or v3.
* change-grid-system - for using a TailWind, a different css grid system
* color-palettes - for configuring the default color pickers and registering new color palettes to be used in the widget designers
* conditional-rendering-in-editor - demonstrates how conditional rendering can be applied based on the current context - edit, preview or live
* content-selectors - for working with Sitefinity content in the widget designers
* custom-form-fields - for building a custom hidden input widget and a custom date input widget.
* custom-layout - for creating custom base templates
* custom-section - for creating a custom layout widget with hard-coded columns and passing data to child widgets.
* error-handling - for handling errors and responding to different status codes (error pages)
* extended-content-block - for overriding the logic of the content block. (valid for any OOB widget)
* extended-content-list - for creating custom views for the content list widget.
* graphql - for querying data with graphql
* hello-world - for creating a hello world widget and configuring the different types of properties of the SitefinityWidget attribute.
* html-sanitizer - for overriding the logic for html sanitizer.
* integration-tests - for writing integration tests.
* language-selector - for creating a language selector dropdown widget that holds the languages of the current site.
* localization - for localizing your Layouts and ViewComponents
* master-detail - for implementing a custom master/detail navigation from an external data source.
* mega-menu - for building a complex menu with dropdowns and placing custom/personalized content in those dropdowns.
* migrate-data-to-cms - how to migrate your external data to the Sitefinity CMS using the C# REST SDK
* output-cache - for configuring the output cache per page.
* quantum - for working with the Quantum sample demo.
* react-renderer-front-end - for scripting client side react components
* registration-widget-with-custom-fields - for addding custom fields to the registration widget.
* resource-package-foundation - for implementing the "Foundation" framework
* resource-package-minimal - for implementing the "Minimal" framework
* script-widget - sample script widget implementation for placing runtime JavaScript on the page.
* deprecated ~~separate-widget-per-component - example on how to create a separate entry for each widget view inside the Select widget dialog.~~
* share-data-between-widgets - for sharing data between two widgets in a very early stage of the pipeline before the page is executed.
* sitefinity-data - basic example for accessing data through OData Web Services and limiting access to the Sitefinity OData service.
* sitefinity-insight - for submitting custom data to sitefinity insight connector
* starter-template - for basing your project with everything set-upped.
* tabs-section - for building custom widget that can be used to create 2 tabs layout
* widget-library - for building your custom distributed widget libraries and templates. Additionally this sample includes information on how to add your own custom scripts alongside the views.
* component-filter - for filtering the components visible in the widget selectors for Pages, Templates and Forms based on a certain criteria.